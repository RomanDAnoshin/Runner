using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Utilities
{
    public class Timer : MonoBehaviour
    {
        public float TargetTime { get; protected set; }
        public UnityEvent TimerEnded;

        private float currentTime;
        private bool isCountDown = false;

        public Timer(float targetTime)
        {
            TargetTime = targetTime;
        }

        public void StartCount()
        {
            isCountDown = true;
            currentTime = TargetTime;
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
            currentTime = TargetTime;
        }

        void Update()
        {
            if (isCountDown) {
                currentTime -= Time.deltaTime;

                if (currentTime <= 0.0f) {
                    TimerEnded?.Invoke();
                    isCountDown = false;
                }
            }
        }
    }
}
