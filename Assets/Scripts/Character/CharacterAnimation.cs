using UnityEngine;
using Player;

namespace Character
{
    public class CharacterAnimation : MonoBehaviour, IPlayerControllable
    {
        [SerializeField] private Animator Animator;
        [SerializeField] private CharacterData CharacterData;
        [SerializeField] private PlayerInput PlayerInput;

        public void Run()
        {
            Animator.SetTrigger("Run");
        }

        public void Die()
        {
            Animator.speed = 1f;
            Animator.SetTrigger("Die");
        }

        public void OnPlayerActed()
        {
            if (CharacterData.State == CharacterData.CharacterState.Alive &&
                PlayerInput.Value == PlayerInput.PlayerActions.Run
            ) {
                Run();
            }
        }

        public void OnCharacterCollisionBarricade()
        {
            Die();
        }
    }
}
