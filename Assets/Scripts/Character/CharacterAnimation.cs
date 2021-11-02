using UnityEngine;
using Player;
using Game;

namespace Character
{
    public class CharacterAnimation : MonoBehaviour
    {
        [SerializeField] private float FinalGainedSpeed;
        [SerializeField] private int FinalCoinsToStopGain;

        private float speedGainModifier;
        private float startAnimationSpeed;
        private Animator animator;
        private CharacterBodyCollision characterBodyCollision;

        void Start()
        {
            animator = gameObject.GetComponentInChildren<Animator>();
            characterBodyCollision = gameObject.GetComponentInChildren<CharacterBodyCollision>();
            characterBodyCollision.CollisionBarricade += OnCharacterCollisionBarricade;
            PlayerInput.Instance.Ran += OnPlayerRan;
            PlayerData.Instance.CurrentCoinsChanged += OnCurrentCoinsChanged;
            startAnimationSpeed = animator.speed;
            speedGainModifier = (FinalGainedSpeed - startAnimationSpeed) / FinalCoinsToStopGain;
        }

        public void Run()
        {
            animator.SetTrigger("Run");
        }

        public void Die()
        {
            animator.speed = 1f;
            animator.SetTrigger("Die");
        }

        public void OnPlayerRan()
        {
            if (GameData.Instance.Status != GameStatus.Lose) {
                Run();
                PlayerInput.Instance.Ran -= OnPlayerRan;
            }
        }

        private void OnCharacterCollisionBarricade()
        {
            Die();
            characterBodyCollision.CollisionBarricade -= OnCharacterCollisionBarricade;
        }

        private void OnCurrentCoinsChanged()
        {
            UpdateSpeedModificator();
        }

        private void UpdateSpeedModificator()
        {
            animator.speed = startAnimationSpeed + PlayerData.Instance.CurrentCoins * speedGainModifier;
            if (PlayerData.Instance.CurrentCoins == FinalCoinsToStopGain) {
                PlayerData.Instance.CurrentCoinsChanged -= OnCurrentCoinsChanged;
            }
        }

        void OnDestroy()
        {
            animator = null;
            PlayerData.Instance.CurrentCoinsChanged -= OnCurrentCoinsChanged;
            characterBodyCollision.CollisionBarricade -= OnCharacterCollisionBarricade;
            characterBodyCollision = null;
        }
    }
}
