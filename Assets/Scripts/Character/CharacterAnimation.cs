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
        private PlayerData playerData;

        void Start()
        {
            gameData = FindObjectOfType<GameData>();
            playerInput = FindObjectOfType<PlayerInput>();
            playerInput.PlayerActed += OnPlayerActed;
            characterBodyCollision = FindObjectOfType<CharacterBodyCollision>();
            characterBodyCollision.CollisionBarricade += OnCharacterCollisionBarricade;
            playerData = FindObjectOfType<PlayerData>();
            playerData.CurrentCoinsChanged += OnCurrentCoinsChanged;
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

        private void OnCurrentCoinsChanged()
        {
            UpdateSpeedModificator();
        }

        private void UpdateSpeedModificator() // TODO bring animation speed control to a higher level
        {
            if (playerData.CurrentCoins > 100) {
                Animator.speed = 1.5f;
                playerData.CurrentCoinsChanged -= OnCurrentCoinsChanged;
            } else {
                Animator.speed = 1f + playerData.CurrentCoins / 200f;
            }
        }

        void OnDestroy()
        {
            gameData = null;
            playerInput.PlayerActed -= OnPlayerActed;
            playerInput = null;
            characterBodyCollision.CollisionBarricade -= OnCharacterCollisionBarricade;
            characterBodyCollision = null;
            Animator = null;
            playerData.CurrentCoinsChanged -= OnCurrentCoinsChanged;
            playerData = null;
        }
    }
}
