using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public enum CharacterState
    {
        Alive,
        Died
    }

    public PlayerInput PlayerInput;
    public PlayerData PlayerData;
    public CharacterData CharacterData;
    public CharacterMovement CharacterMovement;
    public Animator Animator;

    private CharacterState characterState;

    void Start()
    {
        CharacterMovement.UpdateCurrentSpeed();
    }

    public void OnPlayerActed()
    {
        if(characterState == CharacterState.Alive) {
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
                    Animator.SetTrigger("Run");
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
        characterState = CharacterState.Died;
        Animator.speed = 1;
        Animator.SetTrigger("Die");
    }

    public void OnCollisionCoin()
    {
        PlayerData.Coins++;

        CharacterMovement.UpdateCurrentSpeed();
    }
}
