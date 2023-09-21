using Game;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

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
        [SerializeField] private SwipeManager SwipeManager;

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

        void Start()
        {
            SwipeManager.SwipeHappened += OnSwipeHappened;
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0) && GameData.Status == GameStatus.PrepareToStart) {
                PlayerAction = PlayerAction.Run;
            } else {
                PlayerAction = PlayerAction.None;
            }
        }

        private void OnSwipeHappened(MoveDirection direction)
        {
            if (GameData.Status != GameStatus.PrepareToStart) {
                switch (direction) {
                    case MoveDirection.Left:
                        PlayerAction = PlayerAction.MoveLeft;
                        break;
                    case MoveDirection.Right:
                        PlayerAction = PlayerAction.MoveRight;
                        break;
                    default:
                        PlayerAction = PlayerAction.None;
                        break;
                }
            }
        }

        void OnDestroy()
        {
            SwipeManager.SwipeHappened -= OnSwipeHappened;
        }
    }
}
