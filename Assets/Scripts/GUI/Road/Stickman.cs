using Game;
using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GUI.Road
{
    public class Stickman : MonoBehaviour
    {
        [SerializeField] private PlayerData PlayerData;
        [SerializeField] private PlayerInput PlayerInput;
        [SerializeField] private GameData GameData;

        private Animator animator;

        void Start()
        {
            animator = gameObject.GetComponent<Animator>();
            animator.enabled = false;

            PlayerInput.Ran += OnPlayerRan;
            GameData.Lost += OnGameLost;
            PlayerData.CurrentSpeedModificatorChanged += OnSpeedModificatorChanged;
        }

        private void OnPlayerRan()
        {
            PlayerInput.Ran -= OnPlayerRan;
            animator.enabled = true;
        }

        private void OnGameLost()
        {
            GameData.Lost -= OnGameLost;
            animator.enabled = false;
        }

        private void OnSpeedModificatorChanged(float value)
        {
            animator.speed = value;
        }

        void OnDestroy()
        {
            PlayerData.CurrentSpeedModificatorChanged -= OnSpeedModificatorChanged;
            PlayerInput.Ran -= OnPlayerRan;
            GameData.Lost -= OnGameLost;
        }
    }
}
