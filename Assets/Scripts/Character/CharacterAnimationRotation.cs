using Player;
using Road;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

namespace Character
{
    public class CharacterAnimationRotation : MonoBehaviour, IPlayerControllable
    {
        private GameData gameData;
        private PlayerInput playerInput;
        [SerializeField] private CurveTimer CurveTimer;

        private Quaternion startRotation;
        private bool isRunAnimation;
        private bool isRightRotation;

        void Start()
        {
            gameData = FindObjectOfType<GameData>();
            playerInput = FindObjectOfType<PlayerInput>();
            playerInput.PlayerActed += OnPlayerActed;
            CurveTimer.TimerEnded += OnAnimationCompleted;
            startRotation = transform.localRotation;
        }

        void Update()
        {
            if (isRunAnimation) {
                RunAnimation();
            }
        }

        private void StartAnimationRight()
        {
            CurveTimer.StartCount();
            isRunAnimation = true;
            isRightRotation = true;
        }

        private void StartAnimationLeft()
        {
            CurveTimer.StartCount();
            isRunAnimation = true;
            isRightRotation = false;
        }

        public void StopAnimation()
        {
            CurveTimer.StopCount();
            isRunAnimation = false;
            transform.localRotation = startRotation;
        }

        public void OnAnimationCompleted()
        {
            isRunAnimation = false;
            transform.localRotation = startRotation;
        }

        private void RunAnimation()
        {
            var value = CurveTimer.Curve.Evaluate(CurveTimer.CurrentTime);
            if (isRightRotation) {
                transform.localRotation = new Quaternion(startRotation.x, value, startRotation.z, startRotation.w);
            } else {
                transform.localRotation = new Quaternion(startRotation.x, -value, startRotation.z, startRotation.w);
            }            
        }

        public void OnPlayerActed()
        {
            if (gameData.Status == GameData.GameStatus.Play) {
                switch (playerInput.Value) {
                    case PlayerInput.PlayerActions.MoveLeft:
                        StartAnimationLeft();
                        break;
                    case PlayerInput.PlayerActions.MoveRight:
                        StartAnimationRight();
                        break;
                }
            }
        }
    }
}
