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

        [SerializeField] private GameObject Character;
        [SerializeField] private PlayerInput PlayerInput;

        private GameStatus status;
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

        private CharacterBodyCollision characterBodyCollision;

        void Start()
        {
            PlayerInput.Ran += OnPlayerRan;
            characterBodyCollision = Character.GetComponentInChildren<CharacterBodyCollision>();
            characterBodyCollision.CollisionBarricade += OnCharacterBodyCollisionBarricade;
        }

        public void Play()
        {
            Status = GameStatus.Play;
        }

        public void Lose()
        {
            Status = GameStatus.Lose;
        }

        private void OnCharacterBodyCollisionBarricade()
        {
            Lose();
            characterBodyCollision.CollisionBarricade -= OnCharacterBodyCollisionBarricade;
        }

        public void OnPlayerRan()
        {
            if (Status != GameStatus.Lose) {
                Play();
                PlayerInput.Ran -= OnPlayerRan;
            }
        }
    }
}
