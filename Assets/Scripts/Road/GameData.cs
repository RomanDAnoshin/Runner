using Character;
using Player;
using System;
using UnityEngine;

namespace Road
{
    public enum GameStatus
    {
        PrepareToStart,
        Play,
        Lose
    }

    public class GameData : MonoBehaviour, IPlayerControllable
    {
        public static GameData Instance;

        public Action<GameStatus> StatusChanged;

        private GameStatus status;
        public GameStatus Status
        {
            get {
                return status;
            }
            protected set {
                if(status != value) {
                    status = value;
                    StatusChanged?.Invoke(status);
                }
            }
        }

        void Awake()
        {
            Instance = this;
        }

        void Start()
        {
            PlayerInput.Instance.PlayerActed += OnPlayerActed;
            CharacterBodyCollision.Instance.CollisionBarricade += OnCharacterBodyCollisionBarricade;
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
            CharacterBodyCollision.Instance.CollisionBarricade -= OnCharacterBodyCollisionBarricade;
        }

        public void OnPlayerActed()
        {
            if (Status != GameStatus.Lose &&
                PlayerInput.Instance.Value == PlayerInput.PlayerActions.Run
            ) {
                Play();
                PlayerInput.Instance.PlayerActed -= OnPlayerActed;
            }
        }
    }
}
