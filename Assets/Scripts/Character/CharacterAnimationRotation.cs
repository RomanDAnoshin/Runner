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
        [SerializeField] private CurveTimer CurveTimer;
        [SerializeField] private int FinalValueOfCoins;
        [SerializeField] private float SpeedGainModifier;

        private Quaternion startRotation;
        private bool isRunAnimation;
        private bool isRightRotation;

        void Start()
        {
            PlayerData.Instance.CurrentCoinsChanged += OnCurrentCoinsChanged;
            PlayerInput.Instance.PlayerActed += OnPlayerActed;
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
            if (!isRightRotation) {
                value = -value;
            }
            transform.localRotation = new Quaternion(startRotation.x, value, startRotation.z, startRotation.w);
        }

        public void OnPlayerActed()
        {
            if (GameData.Instance.Status == GameStatus.Play) {
                switch (PlayerInput.Instance.Value) {
                    case PlayerInput.PlayerActions.MoveLeft:
                        StartAnimationLeft();
                        break;
                    case PlayerInput.PlayerActions.MoveRight:
                        StartAnimationRight();
                        break;
                }
            }
        }

        private void OnCurrentCoinsChanged()
        {
            UpdateSpeedModificator();
        }

        private void UpdateSpeedModificator()
        {
            for(var i = 0; i < CurveTimer.Curve.keys.Length; i++) { // TODO One recalculation when pressed
                CurveTimer.Curve.MoveKey(i, new Keyframe(CurveTimer.Curve.keys[i].time - CurveTimer.Curve.keys[i].time * SpeedGainModifier, CurveTimer.Curve.keys[i].value));
            }
            if (PlayerData.Instance.CurrentCoins == FinalValueOfCoins) {
                PlayerData.Instance.CurrentCoinsChanged -= OnCurrentCoinsChanged;
            }
        }

        void OnDestroy()
        {
            isRunAnimation = false;
            PlayerData.Instance.CurrentCoinsChanged -= OnCurrentCoinsChanged;
            PlayerInput.Instance.PlayerActed -= OnPlayerActed;
            CurveTimer.TimerEnded -= OnAnimationCompleted;
            CurveTimer = null;
        }
    }
}
