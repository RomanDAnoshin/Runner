using Game;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    public enum PlayerAction
    {
        None,
        MoveLeft,
        MoveRight,
        Run
    }

    public class PlayerInput : MonoBehaviour
    {
        public Action<PlayerAction> PlayerActed;
        public Action MovedLeft;
        public Action MovedRight;
        public Action Ran;

        [SerializeField] private GameData GameData;

        private PlayerAction playerAction;
        public PlayerAction PlayerAction
        {
            get {
                return playerAction;
            }
            private set {
                if (playerAction != value) {
                    playerAction = value;
                    if(playerAction != PlayerAction.None) {
                        PlayerActed?.Invoke(playerAction);
                        switch (playerAction) {
                            case PlayerAction.MoveLeft:
                                MovedLeft?.Invoke();
                                break;
                            case PlayerAction.MoveRight:
                                MovedRight?.Invoke();
                                break;
                            case PlayerAction.Run:
                                Ran?.Invoke();
                                break;
                        }
                    }
                }
            }
        }

        void Update()
        {
            if (Input.anyKeyDown) {
                if (Input.GetMouseButtonDown(0)) {
                    if (GameData.Status == GameStatus.PrepareToStart) {
                        PlayerAction = PlayerAction.Run;
                    } else if (Input.mousePosition.x > Screen.width / 2f) {
                        PlayerAction = PlayerAction.MoveRight;
                    } else {
                        PlayerAction = PlayerAction.MoveLeft;
                    }
                }
            } else {
                PlayerAction = PlayerAction.None;
            }
        }
    }
}
