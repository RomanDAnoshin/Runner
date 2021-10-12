using System;
using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    public class PlayerInput : MonoBehaviour
    {
        public enum PlayerActions
        {
            None,
            MoveLeft,
            MoveRight,
            Run
        }

        public PlayerActions Value { get; protected set; }
        public Action PlayerActed;

        void Update()
        {
            if (Input.anyKeyDown) {
                if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) {
                    Value = PlayerActions.MoveLeft;
                    PlayerActed?.Invoke();
                } else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) {
                    Value = PlayerActions.MoveRight;
                    PlayerActed?.Invoke();
                } else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space)) {
                    Value = PlayerActions.Run;
                    PlayerActed?.Invoke();
                }
            } else {
                Value = PlayerActions.None;
            }
        }
    }
}
