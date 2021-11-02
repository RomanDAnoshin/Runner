using UnityEngine;
using Road;
using Player;
using Utilities;
using System;
using Game;

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

        [SerializeField] private float StartTimeForChangeLane;
        [SerializeField] private float FinalReducedTimeForChangeLane;
        [SerializeField] private int FinalCoinsToStopReduce;

        private float currentTimeForChangeLane;
        private float reduceModifier;
        private bool isMoving;
        private int targetLane;
        private HorizontalMove horizontalMove;
        private Vector3 velocitySmoothDamp;

        void Start()
        {
            GameData.Instance.Lost += OnGameLost;
            targetLane = GameGenerator.Instance.LanesData.StartLaneIndex;
            PlayerData.Instance.CurrentCoinsChanged += OnCurrentCoinsChanged;
            currentTimeForChangeLane = StartTimeForChangeLane;
            reduceModifier = (StartTimeForChangeLane - FinalReducedTimeForChangeLane) / FinalCoinsToStopReduce;
        }

        void Update()
        {
            HorizontalMovement();
        }

        private void HorizontalMovement()
        {
            if (
                horizontalMove != HorizontalMove.None &&
                transform.position.x != GameGenerator.Instance.LanesData.Positions[targetLane].x
            ) {
                var lanePos = new Vector3(GameGenerator.Instance.LanesData.Positions[targetLane].x, transform.position.y, transform.position.z);
                transform.position = Vector3.SmoothDamp(transform.position, lanePos, ref velocitySmoothDamp, currentTimeForChangeLane);
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
                targetLane != GameGenerator.Instance.LanesData.Positions.Length - 1 &&
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

        private void OnGameLost()
        {
            Stay();
        }

        private void OnCurrentCoinsChanged()
        {
            UpdateSpeedModificator();
        }

        private void UpdateSpeedModificator()
        {
            currentTimeForChangeLane = StartTimeForChangeLane - PlayerData.Instance.CurrentCoins * reduceModifier;
            if (PlayerData.Instance.CurrentCoins == FinalCoinsToStopReduce) {
                PlayerData.Instance.CurrentCoinsChanged -= OnCurrentCoinsChanged;
            }
        }

        void OnDestroy()
        {
            Stay();
            GameData.Instance.Lost -= OnGameLost;
        }
    }
}
