using Player;
using Road;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    public class CharacterControls : MonoBehaviour, IPlayerControllable
    {
        private CharacterMovement characterMovement;

        void Start()
        {
            PlayerInput.Instance.PlayerActed += OnPlayerActed;
            characterMovement = gameObject.GetComponent<CharacterMovement>();
        }

        public void OnPlayerActed()
        {
            if (GameData.Instance.Status != GameData.GameStatus.Lose) {
                switch (PlayerInput.Instance.Value) {
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
            PlayerInput.Instance.PlayerActed -= OnPlayerActed;
            characterMovement = null;
        }
    }
}
