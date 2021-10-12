using Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

namespace Character
{
    public class CharacterAnimationRotation : MonoBehaviour, IPlayerControllable
    {
        [SerializeField] private CharacterData CharacterData;
        [SerializeField] private PlayerInput PlayerInput;
        [SerializeField] private CurveTimer CurveTimer;

        private Quaternion startRotation;
        private bool isRunAnimation;
        private bool isRightRotation;

        void Start()
        {
            startRotation = transform.localRotation;
        }

        void Update()
        {
            if (isRunAnimation) {
                RunAnimation();
            }
        }

        public void StartAnimationRight()
        {
            CurveTimer.StartCount();
            isRunAnimation = true;
            isRightRotation = true;
        }

        public void StartAnimationLeft()
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

        public void AnimationCompleted()
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
            if (CharacterData.State == CharacterData.CharacterState.Alive) {
                switch (PlayerInput.Value) {
                    case PlayerInput.PlayerActions.None:
                        break;
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
