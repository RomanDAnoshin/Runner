using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Utilities
{
    public class CurveTimer : MonoBehaviour
    {
        public Action TimerEnded;

        public AnimationCurve Curve;

        [HideInInspector] public float CurrentTime { get; protected set; }
        private bool isCountDown = false;

        public void StartCount()
        {
            CurrentTime = Curve.keys[0].time;
            isCountDown = true;
        }

        public void ContinueCount()
        {
            isCountDown = true;
        }

        public void StopCount()
        {
            isCountDown = false;
        }

        public void Restart()
        {
            isCountDown = true;
            CurrentTime = Curve.keys[0].time;
        }

        void Update()
        {
            if (isCountDown) {
                CurrentTime += Time.deltaTime;

                if (CurrentTime >= Curve.keys[Curve.keys.Length - 1].time) {
                    TimerEnded?.Invoke();
                    isCountDown = false;
                }
            }
        }

        void OnDestroy()
        {
            StopCount();
            Curve = null;
        }
    }
}
