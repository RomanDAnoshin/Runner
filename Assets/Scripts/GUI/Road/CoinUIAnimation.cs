using Character;
using Player;
using UnityEngine;
using Utilities;

namespace GUI.Road
{
    public class CoinUIAnimation : MonoBehaviour
    {
        [SerializeField] private PlayerData PlayerData;
        [SerializeField] private CurveTimer CurveTimer;

        private bool isRunAnimation;
        private Vector3 startLocalScale;

        void Start()
        {
            startLocalScale = transform.localScale;
            CurveTimer.TimerEnded += OnAnimationCompleted;
            PlayerData.CurrentCoinsChanged += OnCurrentCoinsChanged;
        }

        void Update()
        {
            if (isRunAnimation) {
                RunAnimation();
            }
        }

        public void StartAnimation()
        {
            CurveTimer.StartCount();
            isRunAnimation = true;
        }

        public void StopAnimation()
        {
            CurveTimer.StopCount();
            isRunAnimation = false;
        }

        private void OnAnimationCompleted()
        {
            isRunAnimation = false;
        }

        private void RunAnimation()
        {
            var value = CurveTimer.Curve.Evaluate(CurveTimer.CurrentTime);
            transform.localScale = new Vector3(startLocalScale.x * value, startLocalScale.y, startLocalScale.z * value);
        }

        public void OnCurrentCoinsChanged(int value)
        {
            StartAnimation();
        }

        void OnDestroy()
        {
            isRunAnimation = false;
            CurveTimer.TimerEnded -= OnAnimationCompleted;
            CurveTimer = null;
            PlayerData.CurrentCoinsChanged -= OnCurrentCoinsChanged;
        }
    }
}
