using UnityEngine;
using Road;
using Player;
using Utilities;
using System;

namespace Character
{
    public class CharacterMovement : MonoBehaviour, IMovable
    {
        public enum HorizontalMove
        {
            None,
            Left,
            Right
        }
        public Action PositionChanged;

        [SerializeField] private float StartHorizontalSpeed;
        [SerializeField] private int FinalValueOfCoins;
        [SerializeField] private float SpeedGainModifier;

        private float currentHorizontalSpeed;
        private bool isMoving;
        private int targetLane;
        private HorizontalMove horizontalMove;
        private Vector3 velocitySmoothDamp;

        void Start()
        {
            GameData.Instance.StatusChanged += OnGameStatusChanged;
            targetLane = LanesData.Instance.StartLaneIndex;
            PlayerData.Instance.CurrentCoinsChanged += OnCurrentCoinsChanged;
            currentHorizontalSpeed = StartHorizontalSpeed;
        }

        void Update()
        {
            HorizontalMovement();
        }

        private void HorizontalMovement()
        {
            if (
                horizontalMove != HorizontalMove.None &&
                transform.position.x != LanesData.Instance.Positions[targetLane].x
            ) {
                var lanePos = new Vector3(LanesData.Instance.Positions[targetLane].x, transform.position.y, transform.position.z);
                transform.position = Vector3.SmoothDamp(transform.position, lanePos, ref velocitySmoothDamp, currentHorizontalSpeed);
                PositionChanged?.Invoke();
            }
        }

        public void MoveLeft()
        {
            if (
                targetLane != 0 &&
                isMoving != false
            ) {
                targetLane--;
                horizontalMove = HorizontalMove.Left;
            }
        }

        public void MoveRight()
        {
            if (
                targetLane != LanesData.Instance.Positions.Length - 1 &&
                isMoving != false
            ) {
                targetLane++;
                horizontalMove = HorizontalMove.Right;
            }
        }

        public void Stay()
        {
            isMoving = false;
            horizontalMove = HorizontalMove.None;
        }

        public void Move()
        {
            isMoving = true;
        }

        private void OnGameStatusChanged(GameStatus gameStatus)
        {
            if(gameStatus == GameStatus.Lose) {
                Stay();
            }
        }

        private void OnCurrentCoinsChanged()
        {
            UpdateSpeedModificator();
        }

        private void UpdateSpeedModificator()
        {
            currentHorizontalSpeed = StartHorizontalSpeed - StartHorizontalSpeed * PlayerData.Instance.CurrentCoins * SpeedGainModifier;
            if (PlayerData.Instance.CurrentCoins == FinalValueOfCoins) {
                PlayerData.Instance.CurrentCoinsChanged -= OnCurrentCoinsChanged;
            }
        }

        void OnDestroy()
        {
            Stay();
            GameData.Instance.StatusChanged -= OnGameStatusChanged;
        }
    }
}
