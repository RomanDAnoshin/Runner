using Character;
using Game;
using Player;
using System;
using UnityEngine;

namespace Road
{
    public class RoadMovement : MonoBehaviour, IMovable
    {
        public Action SpeedModificatorChanged;

        [SerializeField] private PlayerInput PlayerInput;
        [SerializeField] private PlayerData PlayerData;
        [SerializeField] private GameData GameData;
        [SerializeField] private float StartSpeed;
        [SerializeField] private int FinalCoinsToStopGain;
        [SerializeField] private float FinalGainedSpeedModificator;

        private RoadBuffer roadBuffer;

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
        private float startSpeedModificator;
        private float speedModificator;
        private float gainSpeedModificator;
        private bool isMoving;
        private float currentSpeed;

        void Start()
        {
            PlayerInput.Ran += OnPlayerRan;
            PlayerData.CurrentCoinsChanged += OnCurrentCoinsChanged;
            roadBuffer = gameObject.GetComponent<RoadBuffer>();
            currentSpeed = StartSpeed;
            startSpeedModificator = 1f;
            speedModificator = startSpeedModificator;
            gainSpeedModificator = (FinalGainedSpeedModificator - startSpeedModificator) / FinalCoinsToStopGain;
            GameData.Lost += OnGameLost;
        }

        void Update()
        {
            MoveRoad();
        }

        private void MoveRoad()
        {
            if (isMoving) {
                roadBuffer.ForEach(block => block.transform.position -= new Vector3(0f, 0f, currentSpeed * Time.deltaTime * SpeedModificator));
                UpdateCurrentDistance();
            }
        }

        public void Stay()
        {
            isMoving = false;
        }

        public void Move()
        {
            isMoving = true;
        }

        public void UpdateCurrentDistance()
        {
            PlayerData.CurrentDistance += currentSpeed * Time.deltaTime * SpeedModificator;
        }

        public void OnPlayerRan()
        {
            if (GameData.Status != GameStatus.Lose) {
                Move();
            }
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
            SpeedModificator = startSpeedModificator + value * gainSpeedModificator;
            if (value == FinalCoinsToStopGain) {
                PlayerData.CurrentCoinsChanged -= OnCurrentCoinsChanged;
            }
        }

        void OnDestroy()
        {
            Stay();
            PlayerInput.Ran -= OnPlayerRan;
            PlayerData.CurrentCoinsChanged -= OnCurrentCoinsChanged;
            roadBuffer = null;
            GameData.Lost -= OnGameLost;
        }
    }
}
