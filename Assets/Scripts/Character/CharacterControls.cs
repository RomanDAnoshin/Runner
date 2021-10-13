using Player;
using Road;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    public class CharacterControls : MonoBehaviour, IPlayerControllable
    {
        private PlayerInput playerInput;
        private GameData gameData;
        private CharacterMovement characterMovement;

        void Start()
        {
            playerInput = FindObjectOfType<PlayerInput>();
            playerInput.PlayerActed += OnPlayerActed;
            gameData = FindObjectOfType<GameData>();
            characterMovement = FindObjectOfType<CharacterMovement>();
        }

        public void OnPlayerActed()
        {
            if (gameData.Status != GameData.GameStatus.Lose) {
                switch (playerInput.Value) {
                    case PlayerInput.PlayerActions.MoveLeft:
                        characterMovement.MoveLeft();
                        break;
                    case PlayerInput.PlayerActions.MoveRight:
                        characterMovement.MoveRight();
                        break;
                    case PlayerInput.PlayerActions.Run:
                        characterMovement.Move();
                        break;
                }
            }
        }

        void OnDestroy()
        {
            playerInput.PlayerActed -= OnPlayerActed;
            playerInput = null;
            gameData = null;
            characterMovement = null;
        }
    }
}
