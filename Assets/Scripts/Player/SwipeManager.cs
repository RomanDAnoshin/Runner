using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Player
{
    public class SwipeManager : MonoBehaviour
    {
        public Action<MoveDirection> SwipeHappened;

        public const float SwipeThreshold = 50;

        private Vector2 startTouchPosition;
        private Vector2 swipeDelta;
        private bool IsTouchMoved;

        private MoveDirection direction;

        void Update()
        {
            CheckAnotherInterface();

            CheckStartFinishSwipe();

            CalculateDistance();

            if (swipeDelta.magnitude >= SwipeThreshold) {
                direction = CheckDirection();
                SendSwipe();
            }
        }

        private Vector2 GetTouchPosition()
        {
            return (Vector2)Input.mousePosition;
        }

        private bool IsTouchBegan()
        {
            return Input.GetMouseButtonDown(0);
        }

        private bool IsTouchEnded()
        {
            return Input.GetMouseButtonUp(0);
        }

        private bool IsTouch()
        {
            return Input.GetMouseButton(0);
        }

        private void SendSwipe()
        {
            SwipeHappened?.Invoke(direction);
            Reset();
        }

        private void CheckStartFinishSwipe()
        {
            if (IsTouchBegan()) {
                startTouchPosition = GetTouchPosition();
                IsTouchMoved = true;
            } else if (IsTouchEnded() && IsTouchMoved == true) {
                IsTouchMoved = false;
                if (swipeDelta.magnitude >= SwipeThreshold) {
                    direction = CheckDirection();
                    SendSwipe();
                }
            }
        }

        private void CalculateDistance()
        {
            swipeDelta = Vector2.zero;
            if (IsTouchMoved && IsTouch()) {
                swipeDelta = GetTouchPosition() - startTouchPosition;
            }
        }

        private MoveDirection CheckDirection()
        {
            if (Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y)) {
                if (swipeDelta.x < 0) {
                    return MoveDirection.Left;
                } else {
                    return MoveDirection.Right;
                }
            } else {
                if (swipeDelta.y < 0) {
                    return MoveDirection.Down;
                } else {
                    return MoveDirection.Up;
                }
            }
        }

        private void CheckAnotherInterface()
        {
            if (EventSystem.current.IsPointerOverGameObject()) {
                return;
            }
        }

        void Reset()
        {
            startTouchPosition = Vector2.zero;
            swipeDelta = Vector2.zero;
            IsTouchMoved = false;
            direction = MoveDirection.None;
        }
    }
}
