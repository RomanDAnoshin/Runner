using UnityEngine;
using Player;
using Road;
using System;

namespace Character
{
    public class CharacterAnimation : MonoBehaviour, IPlayerControllable
    {
        [SerializeField] private int FinalValueOfCoins;
        [SerializeField] private float SpeedGainModifier;

        private Animator animator;
        private CharacterBodyCollision сharacterBodyCollision;

        void Start()
        {
            animator = gameObject.GetComponentInChildren<Animator>();
            сharacterBodyCollision = gameObject.GetComponentInChildren<CharacterBodyCollision>();
            сharacterBodyCollision.CollisionBarricade += OnCharacterCollisionBarricade;
            PlayerInput.Instance.PlayerActed += OnPlayerActed;
            PlayerData.Instance.CurrentCoinsChanged += OnCurrentCoinsChanged;
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

        public void OnPlayerActed()
        {
            if (GameData.Instance.Status != GameStatus.Lose &&
                PlayerInput.Instance.Value == PlayerInput.PlayerActions.Run
            ) {
                Run();
                PlayerInput.Instance.PlayerActed -= OnPlayerActed;
            }
        }

        private void OnCharacterCollisionBarricade()
        {
            Die();
            сharacterBodyCollision.CollisionBarricade -= OnCharacterCollisionBarricade;
        }

        private void OnCurrentCoinsChanged()
        {
            UpdateSpeedModificator();
        }

        private void UpdateSpeedModificator()
        {
            animator.speed = 1f + PlayerData.Instance.CurrentCoins * SpeedGainModifier;
            if (PlayerData.Instance.CurrentCoins == FinalValueOfCoins) {
                PlayerData.Instance.CurrentCoinsChanged -= OnCurrentCoinsChanged;
            }
        }

        void OnDestroy()
        {
            animator = null;
            PlayerData.Instance.CurrentCoinsChanged -= OnCurrentCoinsChanged;
            сharacterBodyCollision.CollisionBarricade -= OnCharacterCollisionBarricade;
            сharacterBodyCollision = null;
        }
    }
}
