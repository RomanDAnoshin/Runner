using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    public class CharacterControls : MonoBehaviour, IPlayerControllable
    {
        [SerializeField] private PlayerInput PlayerInput;
        [SerializeField] private CharacterData CharacterData;
        [SerializeField] private CharacterMovement CharacterMovement;

        public void OnPlayerActed()
        {
            if (CharacterData.State == CharacterData.CharacterState.Alive) {
                switch (PlayerInput.Value) {
                    case PlayerInput.PlayerActions.None:
                        break;
                    case PlayerInput.PlayerActions.MoveLeft:
                        CharacterMovement.MoveLeft();
                        break;
                    case PlayerInput.PlayerActions.MoveRight:
                        CharacterMovement.MoveRight();
                        break;
                    case PlayerInput.PlayerActions.Run:
                        CharacterMovement.Move();
                        break;
                    case PlayerInput.PlayerActions.Stay:
                        CharacterMovement.Stay();
                        break;
                }
            }
        }
    }
}
