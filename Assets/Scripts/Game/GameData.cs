using Character;
using Player;
using System;
using UnityEngine;

namespace Game
{
    public enum GameStatus
    {
        PrepareToStart,
        Play,
        Lose
    }

    public class GameData : MonoBehaviour
    {
        public Action<GameStatus> StatusChanged;
        public Action Played;
        public Action Lost;

        [SerializeField] private CharacterBodyCollision CharacterBodyCollision;
        [SerializeField] private PlayerInput PlayerInput;

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
                    }
                }
            }
        }

        void Start()
        {
            PlayerInput.Ran += OnPlayerRan;
            CharacterBodyCollision.CollisionBarricade += OnCharacterBodyCollisionBarricade;
        }

        private void Play()
        {
            Status = GameStatus.Play;
        }

        private void Lose()
        {
            Status = GameStatus.Lose;
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

        void OnDestroy()
        {
            PlayerInput.Ran -= OnPlayerRan;
            CharacterBodyCollision.CollisionBarricade -= OnCharacterBodyCollisionBarricade;
        }
    }
}
