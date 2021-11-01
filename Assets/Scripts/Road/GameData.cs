using Character;
using Player;
using System;
using UnityEngine;

namespace Road
{
    public class GameData : MonoBehaviour, IPlayerControllable
    {
        public static GameData Instance;

        public enum GameStatus
        {
            PrepareToStart,
            Play,
            Lose
        }

        public Action StatusChanged;

        private GameStatus status;
        public GameStatus Status
        {
            get {
                return status;
            }
            protected set {
                status = value;
                StatusChanged?.Invoke();
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
