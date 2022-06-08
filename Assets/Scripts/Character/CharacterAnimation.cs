using UnityEngine;
using Player;
using Game;

namespace Character
{
    public class CharacterAnimation : MonoBehaviour
    {
        [SerializeField] private float FinalGainedSpeed;
        [SerializeField] private int FinalCoinsToStopGain;
        [SerializeField] private PlayerData PlayerData;
        [SerializeField] private GameData GameData;

        private float speedGainModifier;
        private float startAnimationSpeed;
        private Animator animator;

        void Start()
        {
            animator = gameObject.GetComponentInChildren<Animator>();
            GameData.Lost += OnGameLost;
            GameData.Played += OnGamePlayed;
            PlayerData.CurrentCoinsChanged += OnCurrentCoinsChanged;
            startAnimationSpeed = animator.speed;
            speedGainModifier = (FinalGainedSpeed - startAnimationSpeed) / FinalCoinsToStopGain;
        }

        private void Run()
        {
            animator.SetTrigger("Run");
        }

        private void Die()
        {
            animator.speed = 1f;
            animator.SetTrigger("Die");
        }

        private void OnGamePlayed()
        {
            if (GameData.Status != GameStatus.Lose) {
                GameData.Played -= OnGamePlayed;
                Run();
            }
        }

        private void OnGameLost()
        {
            GameData.Lost -= OnGameLost;
            Die();
        }

        private void OnCurrentCoinsChanged(int value)
        {
            UpdateSpeedModificator(value);
        }

        private void UpdateSpeedModificator(int value)
        {
            animator.speed = startAnimationSpeed + value * speedGainModifier;
            if (value == FinalCoinsToStopGain) {
                PlayerData.CurrentCoinsChanged -= OnCurrentCoinsChanged;
            }
        }

        void OnDestroy()
        {
            animator = null;
            GameData.Played -= OnGamePlayed;
            GameData.Lost -= OnGameLost;
            PlayerData.CurrentCoinsChanged -= OnCurrentCoinsChanged;
        }
    }
}
