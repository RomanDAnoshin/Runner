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
        private CharacterMovement characterMovement;

        void Start()
        {
            PlayerInput.Instance.PlayerActed += OnPlayerActed;
            characterMovement = gameObject.GetComponent<CharacterMovement>();
        }

        public void OnPlayerActed(PlayerActions playerAction)
        {
            if (GameData.Instance.Status != GameStatus.Lose) {
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
            PlayerInput.Instance.PlayerActed -= OnPlayerActed;
            characterMovement = null;
        }
    }
}
