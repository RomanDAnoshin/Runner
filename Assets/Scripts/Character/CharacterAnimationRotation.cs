using Game;
using Player;
using UnityEngine;
using Utilities;

namespace Character
{
    public class CharacterAnimationRotation : MonoBehaviour
    {
        [SerializeField] private PlayerInput PlayerInput;
        [SerializeField] private PlayerData PlayerData;
        [SerializeField] private GameData GameData;
        [SerializeField] private CurveTimer CurveTimer;
        [SerializeField] private int FinalCoinsToStopReduce;
        [SerializeField] private float FinalReducedSpeed;

        private float reduceModifier;
        private Quaternion startRotation;
        private bool isRunAnimation;
        private bool isRightRotation;
        private int previousCoins;

        void Start()
        {
            PlayerInput.MovedLeft += OnPlayerMovedLeft;
            PlayerInput.MovedRight += OnPlayerMovedRight;
            CurveTimer.TimerEnded += OnAnimationCompleted;
            startRotation = transform.localRotation;
            reduceModifier = (1f - FinalReducedSpeed) / FinalCoinsToStopReduce;
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
            UpdateRotationSpeed();
        }

        private void StartAnimationLeft()
        {
            CurveTimer.StartCount();
            isRunAnimation = true;
            isRightRotation = false;
            UpdateRotationSpeed();
        }

        private void StopAnimation()
        {
            CurveTimer.StopCount();
            isRunAnimation = false;
            transform.localRotation = startRotation;
        }

        private void OnAnimationCompleted()
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

        private void OnPlayerMovedLeft()
        {
            if (GameData.Status == GameStatus.Play) {
                StartAnimationLeft();
            }
        }

        private void OnPlayerMovedRight()
        {
            if (GameData.Status == GameStatus.Play) {
                StartAnimationRight();
            }
        }

        private void UpdateRotationSpeed()
        {
            if (previousCoins < FinalCoinsToStopReduce) {
                var coinsDifference = 1;
                if (PlayerData.CurrentCoins <= FinalCoinsToStopReduce) {
                    coinsDifference = PlayerData.CurrentCoins - previousCoins;
                } else {
                    coinsDifference = FinalCoinsToStopReduce - previousCoins;
                }

                for (var i = 0; i < CurveTimer.Curve.keys.Length; i++) {
                    CurveTimer.Curve.MoveKey(i, new Keyframe(CurveTimer.Curve.keys[i].time - CurveTimer.Curve.keys[i].time * reduceModifier * coinsDifference, CurveTimer.Curve.keys[i].value));
                }
                previousCoins = PlayerData.CurrentCoins;
            }
        }

        void OnDestroy()
        {
            isRunAnimation = false;
            PlayerInput.MovedLeft -= OnPlayerMovedLeft;
            PlayerInput.MovedRight -= OnPlayerMovedRight;
            CurveTimer.TimerEnded -= OnAnimationCompleted;
        }
    }
}
