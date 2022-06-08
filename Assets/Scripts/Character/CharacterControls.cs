using Game;
using Player;
using Road;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    public class CharacterControls : MonoBehaviour
    {
        [SerializeField] PlayerInput PlayerInput;
        [SerializeField] GameData GameData;

        private CharacterMovement characterMovement;

        void Start()
        {
            PlayerInput.PlayerActed += OnPlayerActed;
            characterMovement = gameObject.GetComponent<CharacterMovement>();
        }

        public void OnPlayerActed(PlayerActions playerAction)
        {
            if (GameData.Status != GameStatus.Lose) {
                switch (playerAction) {
                    case PlayerActions.MoveLeft:
                        characterMovement.MoveLeft();
                        break;
                    case PlayerActions.MoveRight:
                        characterMovement.MoveRight();
                        break;
                    case PlayerActions.Run:
                        characterMovement.Move();
                        break;
                }
            }
        }

        void OnDestroy()
        {
            PlayerInput.PlayerActed -= OnPlayerActed;
            characterMovement = null;
        }
    }
}
