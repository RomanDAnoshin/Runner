using Game;
using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GUI.Road
{
    public class Stickman : MonoBehaviour
    {
        private Animator animator;

        void Start()
        {
            animator = gameObject.GetComponent<Animator>();
            animator.enabled = false;

            PlayerInput.Instance.Ran += OnPlayerRan;
            GameData.Instance.Lost += OnGameLost;
            PlayerData.Instance.CurrentSpeedModificatorChanged += OnSpeedModificatorChanged;
        }

        private void OnPlayerRan()
        {
            PlayerInput.Instance.Ran -= OnPlayerRan;
            animator.enabled = true;
        }

        private void OnGameLost()
        {
            GameData.Instance.Lost -= OnGameLost;
            animator.enabled = false;
        }

        private void OnSpeedModificatorChanged()
        {
            animator.speed = PlayerData.Instance.CurrentSpeedModificator;
        }

        void OnDestroy()
        {
            PlayerData.Instance.CurrentSpeedModificatorChanged -= OnSpeedModificatorChanged;
            PlayerInput.Instance.Ran -= OnPlayerRan;
            GameData.Instance.Lost -= OnGameLost;
        }
    }
}
