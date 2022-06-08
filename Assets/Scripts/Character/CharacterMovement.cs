using UnityEngine;
using Road;
using Player;
using Utilities;
using System;
using Game;

namespace Character
{
    public class CharacterMovement : Movement
    {
        public enum HorizontalMove
        {
            None,
            Left,
            Right
        }

        [SerializeField] private GameData GameData;
        [SerializeField] private LanesData LanesData;
        [SerializeField] private PlayerData PlayerData;
        [SerializeField] private float StartTimeForChangeLane;
        [SerializeField] private float FinalReducedTimeForChangeLane;
        [SerializeField] private int FinalCoinsToStopReduce;

        private float currentTimeForChangeLane;
        private float reduceModifier;
        private int targetLane;
        private HorizontalMove horizontalMove;
        private Vector3 velocitySmoothDamp;

        void Start()
        {
            GameData.Lost += OnGameLost;
            targetLane = LanesData.StartLaneIndex;
            PlayerData.CurrentCoinsChanged += OnCurrentCoinsChanged;
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
                IsMoving != false &&
                horizontalMove != HorizontalMove.None &&
                Position.x != LanesData.Positions[targetLane].x
            ) {
                var lanePos = new Vector3(LanesData.Positions[targetLane].x, Position.y, Position.z);
                Position = Vector3.SmoothDamp(Position, lanePos, ref velocitySmoothDamp, currentTimeForChangeLane);
            }
        }

        public void MoveLeft()
        {
            if (
                targetLane != 0 &&
                IsMoving != false
            ) {
                targetLane--;
                horizontalMove = HorizontalMove.Left;
            }
        }

        public void MoveRight()
        {
            if (
                targetLane != LanesData.Positions.Length - 1 &&
                IsMoving != false
            ) {
                targetLane++;
                horizontalMove = HorizontalMove.Right;
            }
        }

        public override void Stay()
        {
            base.Stay();
            horizontalMove = HorizontalMove.None;
        }

        private void OnGameLost()
        {
            Stay();
        }

        private void OnCurrentCoinsChanged(int value)
        {
            UpdateSpeedModificator(value);
        }

        private void UpdateSpeedModificator(int value)
        {
            currentTimeForChangeLane = StartTimeForChangeLane - value * reduceModifier;
            if (value == FinalCoinsToStopReduce) {
                PlayerData.CurrentCoinsChanged -= OnCurrentCoinsChanged;
            }
        }

        void OnDestroy()
        {
            Stay();
            GameData.Lost -= OnGameLost;
        }
    }
}
