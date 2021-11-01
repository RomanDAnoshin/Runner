using UnityEngine;
using Player;
using Road;

namespace Character
{
    public class CharacterAnimation : MonoBehaviour, IPlayerControllable
    {
        [SerializeField] private Animator Animator;

        void Start()
        {
            PlayerInput.Instance.PlayerActed += OnPlayerActed;
            CharacterBodyCollision.Instance.CollisionBarricade += OnCharacterCollisionBarricade;
            PlayerData.Instance.CurrentCoinsChanged += OnCurrentCoinsChanged;
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
            if (GameData.Instance.Status != GameData.GameStatus.Lose &&
                PlayerInput.Instance.Value == PlayerInput.PlayerActions.Run
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
            if (PlayerData.Instance.CurrentCoins > 100) {
                Animator.speed = 1.5f;
                PlayerData.Instance.CurrentCoinsChanged -= OnCurrentCoinsChanged;
            } else {
                Animator.speed = 1f + PlayerData.Instance.CurrentCoins / 200f;
            }
        }

        void OnDestroy()
        {
            PlayerInput.Instance.PlayerActed -= OnPlayerActed;
            CharacterBodyCollision.Instance.CollisionBarricade -= OnCharacterCollisionBarricade;
            Animator = null;
            PlayerData.Instance.CurrentCoinsChanged -= OnCurrentCoinsChanged;
        }
    }
}
