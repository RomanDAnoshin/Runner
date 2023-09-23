using Character;
using GUI.Road;
using Player;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public enum GameStatus
    {
        PrepareToStart,
        Play,
        Lose,
        Pause,
        Unpause
    }

    public class GameData : MonoBehaviour
    {
        public Action<GameStatus> StatusChanged;
        public Action Played;
        public Action Lost;
        public Action Paused;
        public Action Unpaused;

        [SerializeField] private CharacterBodyCollision CharacterBodyCollision;
        [SerializeField] private PlayerInput PlayerInput;
        [SerializeField] private Button ButtonPause; // TODO: Refactoring
        [SerializeField] private GameGUI GameGUI;

        private GameStatus status = GameStatus.PrepareToStart;
        public GameStatus Status
        {
            get {
                return status;
            }
            private set {
                if(status != value) {
                    status = value;
                    StatusChanged?.Invoke(status);
                    switch (status) {
                        case GameStatus.Play:
                            Played?.Invoke();
                            break;
                        case GameStatus.Lose:
                            Lost?.Invoke();
                            break;
                        case GameStatus.Pause:
                            Paused?.Invoke();
                            break;
                        case GameStatus.Unpause:
                            Unpaused?.Invoke();
                            break;
                    }
                }
            }
        }

        void Start()
        {
            PlayerInput.Ran += OnPlayerRan;
            CharacterBodyCollision.CollisionBarricade += OnCharacterBodyCollisionBarricade;
            ButtonPause.onClick.AddListener(OnClickButtonPause);
            GameGUI.ButtonUnpauseClicked += OnClickButtonUnpause;
        }

        private void Play()
        {
            Status = GameStatus.Play;
        }

        private void Lose()
        {
            Status = GameStatus.Lose;
        }

        private void Pause()
        {
            Status = GameStatus.Pause;
        }

        private void Unpause()
        {
            Status = GameStatus.Unpause;
        }

        private void OnCharacterBodyCollisionBarricade()
        {
            CharacterBodyCollision.CollisionBarricade -= OnCharacterBodyCollisionBarricade;
            Lose();
        }

        private void OnPlayerRan()
        {
            if (Status != GameStatus.Lose) {
                PlayerInput.Ran -= OnPlayerRan;
                Play();
            }
        }

        private void OnClickButtonPause()
        {
            Pause();
        }

        private void OnClickButtonUnpause()
        {
            Unpause();
            Play();
        }

        void OnDestroy()
        {
            PlayerInput.Ran -= OnPlayerRan;
            CharacterBodyCollision.CollisionBarricade -= OnCharacterBodyCollisionBarricade;
            ButtonPause.onClick.RemoveListener(OnClickButtonPause);
            GameGUI.ButtonUnpauseClicked -= OnClickButtonUnpause;
        }
    }
}
