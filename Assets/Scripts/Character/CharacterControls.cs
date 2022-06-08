using Game;
using Player;
using Road;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    public class CharacterControls : MonoBehaviour
    {
        [SerializeField] private PlayerInput PlayerInput;
        [SerializeField] private GameData GameData;

        private CharacterMovement characterMovement;

        void Start()
        {
            PlayerInput.Ran += OnPlayerRan;
            PlayerInput.MovedLeft += OnPlayerMovedLeft;
            PlayerInput.MovedRight += OnPlayerMovedRight;
            characterMovement = gameObject.GetComponent<CharacterMovement>();
        }

        private void OnPlayerRan()
        {
            if (GameData.Status != GameStatus.Lose) {
                PlayerInput.Ran -= OnPlayerRan;
                characterMovement.Move();
            }
        }

        private void OnPlayerMovedLeft()
        {
            if (GameData.Status == GameStatus.Play) {
                characterMovement.MoveLeft();
            }
        }

        private void OnPlayerMovedRight()
        {
            if (GameData.Status == GameStatus.Play) {
                characterMovement.MoveRight();
            }
        }

        void OnDestroy()
        {
            PlayerInput.Ran -= OnPlayerRan;
            PlayerInput.MovedLeft -= OnPlayerMovedLeft;
            PlayerInput.MovedRight -= OnPlayerMovedRight;
            characterMovement = null;
        }
    }
}
