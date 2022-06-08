using Game;
using Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GUI.Road
{
    public class Fire : MonoBehaviour
    {
        [SerializeField] private PlayerInput PlayerInput;
        [SerializeField] private PlayerData PlayerData;
        [SerializeField] private GameData GameData;
        [SerializeField] private Color[] Colors;

        private Animator animator;
        private Image image;

        private int colorIndex;
        private float previousSpeedModificator; // TODO remove it by refactoring

        void Start()
        {
            animator = gameObject.GetComponent<Animator>();
            animator.enabled = false;

            PlayerInput.Ran += OnPlayerRan;
            GameData.Lost += OnGameLost;

            if(Colors == null || Colors.Length < 10) {
                Debug.LogError("Fire: Colors.Length < 10");
            } else {
                PlayerData.CurrentSpeedModificatorChanged += OnSpeedModificatorChanged;
                previousSpeedModificator = 1f;

                image = gameObject.GetComponent<Image>();
                image.color = Colors[colorIndex];
            }
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
            var current = MyMath.TruncateToDecimals(value);
            if(previousSpeedModificator < current) {
                previousSpeedModificator = current;

                if(colorIndex >= Colors.Length - 1) {
                    colorIndex = 0;
                } else {
                    colorIndex++;
                }
                image.color = Colors[colorIndex];
            }
        }

        void OnDestroy()
        {
            PlayerData.CurrentSpeedModificatorChanged -= OnSpeedModificatorChanged;
            PlayerInput.Ran -= OnPlayerRan;
            GameData.Lost -= OnGameLost;
        }
    }
}
