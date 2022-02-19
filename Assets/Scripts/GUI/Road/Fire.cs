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
        [SerializeField] private Color[] Colors;

        private Animator animator;
        private Image image;

        private int colorIndex;
        private float previousSpeedModificator; // TODO remove it by refactoring

        void Start()
        {
            animator = gameObject.GetComponent<Animator>();
            animator.enabled = false;

            PlayerInput.Instance.Ran += OnPlayerRan;
            GameData.Instance.Lost += OnGameLost;

            if(Colors == null || Colors.Length < 10) {
                Debug.LogError("Fire: Colors.Length < 10");
            } else {
                PlayerData.Instance.CurrentSpeedModificatorChanged += OnSpeedModificatorChanged;
                previousSpeedModificator = 1f;

                image = gameObject.GetComponent<Image>();
                image.color = Colors[colorIndex];
            }
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
            var current = MyMath.TruncateToDecimals(PlayerData.Instance.CurrentSpeedModificator);
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
            PlayerData.Instance.CurrentSpeedModificatorChanged -= OnSpeedModificatorChanged;
            PlayerInput.Instance.Ran -= OnPlayerRan;
            GameData.Instance.Lost -= OnGameLost;
        }
    }
}
