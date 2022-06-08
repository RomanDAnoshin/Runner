using UnityEngine;
using Player;
using Game;

namespace Character
{
    public class CharacterAnimation : MonoBehaviour
    {
        [SerializeField] private float FinalGainedSpeed;
        [SerializeField] private int FinalCoinsToStopGain;
        [SerializeField] private PlayerInput PlayerInput;
        [SerializeField] private PlayerData PlayerData;
        [SerializeField] private GameData GameData;

        private float speedGainModifier;
        private float startAnimationSpeed;
        private Animator animator;
        private CharacterBodyCollision characterBodyCollision;

        void Start()
        {
            animator = gameObject.GetComponentInChildren<Animator>();
            characterBodyCollision = gameObject.GetComponentInChildren<CharacterBodyCollision>();
            characterBodyCollision.CollisionBarricade += OnCharacterCollisionBarricade;
            PlayerInput.Ran += OnPlayerRan;
            PlayerData.CurrentCoinsChanged += OnCurrentCoinsChanged;
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
            if (GameData.Status != GameStatus.Lose) {
                Run();
                PlayerInput.Ran -= OnPlayerRan;
            }
        }

        private void OnCharacterCollisionBarricade()
        {
            Die();
            characterBodyCollision.CollisionBarricade -= OnCharacterCollisionBarricade;
        }

        private void OnCurrentCoinsChanged(int value)
        {
            UpdateSpeedModificator();
        }

        private void UpdateSpeedModificator()
        {
            animator.speed = startAnimationSpeed + PlayerData.CurrentCoins * speedGainModifier;
            if (PlayerData.CurrentCoins == FinalCoinsToStopGain) {
                PlayerData.CurrentCoinsChanged -= OnCurrentCoinsChanged;
            }
        }

        void OnDestroy()
        {
            animator = null;
            PlayerData.CurrentCoinsChanged -= OnCurrentCoinsChanged;
            characterBodyCollision.CollisionBarricade -= OnCharacterCollisionBarricade;
            characterBodyCollision = null;
        }
    }
}
