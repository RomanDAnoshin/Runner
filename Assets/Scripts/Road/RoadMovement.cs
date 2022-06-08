using Character;
using Game;
using Player;
using System;
using UnityEngine;
using Utilities;

namespace Road
{
    public class RoadMovement : Movement
    {
        public Action SpeedModificatorChanged;

        [SerializeField] private PlayerData PlayerData;
        [SerializeField] private GameData GameData;

        [SerializeField] private float StartSpeed;
        [SerializeField] private int FinalCoinsToStopGain;
        [SerializeField] private float FinalGainedSpeedModificator;

        public float SpeedModificator
        {
            get {
                return speedModificator;
            }
            protected set {
                speedModificator = value;
                PlayerData.CurrentSpeedModificator = value;
            }
        }

        private RoadBuffer roadBuffer;

        private float startSpeedModificator;
        private float speedModificator;
        private float gainSpeedModificator;
        private float currentSpeed;

        void Start()
        {
            GameData.Lost += OnGameLost;
            GameData.Played += OnGamePlayed;
            PlayerData.CurrentCoinsChanged += OnCurrentCoinsChanged;
            roadBuffer = gameObject.GetComponent<RoadBuffer>();

            currentSpeed = StartSpeed;
            startSpeedModificator = 1f;
            speedModificator = startSpeedModificator;
            gainSpeedModificator = (FinalGainedSpeedModificator - startSpeedModificator) / FinalCoinsToStopGain;
        }

        void Update()
        {
            MoveRoad();
        }

        private void MoveRoad()
        {
            if (IsMoving) {
                roadBuffer.ForEach(block => block.transform.position -= new Vector3(0f, 0f, currentSpeed * Time.deltaTime * SpeedModificator));
                UpdateCurrentDistance();
            }
        }

        public void UpdateCurrentDistance()
        {
            PlayerData.CurrentDistance += currentSpeed * Time.deltaTime * SpeedModificator;
        }

        public void OnGamePlayed()
        {
            if (GameData.Status != GameStatus.Lose) {
                GameData.Played -= OnGamePlayed;
                Move();
            }
        }

        private void OnGameLost()
        {
            GameData.Lost -= OnGameLost;
            Stay();
        }

        private void OnCurrentCoinsChanged(int value)
        {
            UpdateSpeedModificator(value);
        }

        private void UpdateSpeedModificator(int value)
        {
            SpeedModificator = startSpeedModificator + value * gainSpeedModificator;
            if (value == FinalCoinsToStopGain) {
                PlayerData.CurrentCoinsChanged -= OnCurrentCoinsChanged;
            }
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            GameData.Played -= OnGamePlayed;
            GameData.Lost -= OnGameLost;
            PlayerData.CurrentCoinsChanged -= OnCurrentCoinsChanged;
        }
    }
}
