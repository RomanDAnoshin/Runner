using Player;
using UnityEngine;

namespace Character
{
    public class CharacterController : MonoBehaviour
    {
        public PlayerInput PlayerInput;
        public PlayerData PlayerData;
        public CharacterData CharacterData;
        public CharacterMovement CharacterMovement;
        public CharacterAnimation CharacterAnimation;

        void Start()
        {
            CharacterMovement.UpdateCurrentSpeed();
            CharacterData.State = CharacterData.CharacterState.Alive;
        }

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
                        CharacterMovement.Run();
                        CharacterAnimation.Run();
                        break;
                    case PlayerInput.PlayerActions.Stay:
                        CharacterMovement.Stay();
                        break;
                }
            }
        }

        public void OnCollisionBarricade()
        {
            CharacterMovement.Stay();
            CharacterData.State = CharacterData.CharacterState.Died;
            CharacterAnimation.Die();
        }

        public void OnCollisionCoin()
        {
            PlayerData.Coins++;
            CharacterMovement.UpdateCurrentSpeed();
        }
    }
}
