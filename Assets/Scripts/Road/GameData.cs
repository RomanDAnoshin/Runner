using Character;
using Player;
using System;
using UnityEngine;

namespace Road
{
    public class GameData : MonoBehaviour, IPlayerControllable
    {
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

        private PlayerInput playerInput;
        private CharacterBodyCollision characterBodyCollision;

        void Start()
        {
            characterBodyCollision = FindObjectOfType<CharacterBodyCollision>();
            characterBodyCollision.CollisionBarricade += OnCharacterCollisionBarricade;
            playerInput = FindObjectOfType<PlayerInput>();
            playerInput.PlayerActed += OnPlayerActed;
        }

        public void Play()
        {
            Status = GameStatus.Play;
        }

        public void Lose()
        {
            Status = GameStatus.Lose;
        }

        private void OnCharacterCollisionBarricade()
        {
            Lose();
        }

        public void OnPlayerActed()
        {
            if (Status != GameStatus.Lose &&
                playerInput.Value == PlayerInput.PlayerActions.Run
            ) {
                Play();
            }
        }

        void OnDestroy()
        {
            characterBodyCollision.CollisionBarricade -= OnCharacterCollisionBarricade;
            characterBodyCollision = null;
            playerInput.PlayerActed -= OnPlayerActed;
            playerInput = null;
        }
    }
}
