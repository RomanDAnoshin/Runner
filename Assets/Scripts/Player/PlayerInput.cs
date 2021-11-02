using System;
using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    public enum PlayerActions
    {
        None,
        MoveLeft,
        MoveRight,
        Run
    }

    public class PlayerInput : MonoBehaviour
    {
        public static PlayerInput Instance;

        public Action<PlayerActions> PlayerActed;
        public Action MovedLeft;
        public Action MovedRight;
        public Action Ran;

        private PlayerActions playerAction;
        public PlayerActions PlayerAction
        {
            get {
                return playerAction;
            }
            private set {
                if (playerAction != value) {
                    playerAction = value;
                    if(playerAction != PlayerActions.None) {
                        PlayerActed?.Invoke(playerAction);
                        switch (playerAction) {
                            case PlayerActions.MoveLeft:
                                MovedLeft?.Invoke();
                                break;
                            case PlayerActions.MoveRight:
                                MovedRight?.Invoke();
                                break;
                            case PlayerActions.Run:
                                Ran?.Invoke();
                                break;
                        }
                    }
                }
            }
        }

        void Awake()
        {
            Instance = this;
        }

        void Update()
        {
            if (Input.anyKeyDown) {
                if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) {
                    PlayerAction = PlayerActions.MoveLeft;
                } else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) {
                    PlayerAction = PlayerActions.MoveRight;
                } else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space)) {
                    PlayerAction = PlayerActions.Run;
                }
            } else {
                PlayerAction = PlayerActions.None;
            }
        }
    }
}
