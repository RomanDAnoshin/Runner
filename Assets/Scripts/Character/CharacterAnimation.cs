using UnityEngine;
using Player;
using Road;

namespace Character
{
    public class CharacterAnimation : MonoBehaviour, IPlayerControllable
    {
        [SerializeField] private Animator Animator;
        private GameData gameData;
        private PlayerInput playerInput;
        private CharacterBodyCollision characterBodyCollision;

        void Start()
        {
            gameData = FindObjectOfType<GameData>();
            playerInput = FindObjectOfType<PlayerInput>();
            playerInput.PlayerActed += OnPlayerActed;
            characterBodyCollision = FindObjectOfType<CharacterBodyCollision>();
            characterBodyCollision.CollisionBarricade += OnCharacterCollisionBarricade;
        }

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
            if (gameData.Status != GameData.GameStatus.Lose &&
                playerInput.Value == PlayerInput.PlayerActions.Run
            ) {
                Run();
            }
        }

        private void OnCharacterCollisionBarricade()
        {
            Die();
        }

        void OnDestroy()
        {
            gameData = null;
            playerInput.PlayerActed -= OnPlayerActed;
            playerInput = null;
            characterBodyCollision.CollisionBarricade -= OnCharacterCollisionBarricade;
            characterBodyCollision = null;
            Animator = null;
        }
    }
}
