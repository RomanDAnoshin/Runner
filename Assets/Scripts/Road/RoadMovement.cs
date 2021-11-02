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
                PlayerData.Instance.CurrentSpeedModificator = value;
            }
        }
        private float startSpeedModificator;
        private float speedModificator;
        private float gainSpeedModificator;
        private bool isMoving;
        private float currentSpeed;

        void Start()
        {
            PlayerInput.Instance.Ran += OnPlayerRan;
            PlayerData.Instance.CurrentCoinsChanged += OnCurrentCoinsChanged;
            roadBuffer = gameObject.GetComponent<RoadBuffer>();
            currentSpeed = StartSpeed;
            startSpeedModificator = 1f;
            speedModificator = startSpeedModificator;
            gainSpeedModificator = (FinalGainedSpeedModificator - startSpeedModificator) / FinalCoinsToStopGain;
            GameData.Instance.Lost += OnGameLost;
        }

        void Update()
        {
            MoveRoad();
        }

        private void MoveRoad()
        {
            if (isMoving) {
                foreach(var block in roadBuffer.CurrentBlocks) {
                    block.transform.position -= new Vector3(0f, 0f, currentSpeed * Time.deltaTime * SpeedModificator);
                }
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
            PlayerData.Instance.CurrentDistance += currentSpeed * Time.deltaTime * SpeedModificator;
        }

        public void OnPlayerRan()
        {
            if (GameData.Instance.Status != GameStatus.Lose) {
                Move();
            }
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
            SpeedModificator = startSpeedModificator + PlayerData.Instance.CurrentCoins * gainSpeedModificator;
            if (PlayerData.Instance.CurrentCoins == FinalCoinsToStopGain) {
                PlayerData.Instance.CurrentCoinsChanged -= OnCurrentCoinsChanged;
            }
        }

        void OnDestroy()
        {
            Stay();
            PlayerInput.Instance.Ran -= OnPlayerRan;
            PlayerData.Instance.CurrentCoinsChanged -= OnCurrentCoinsChanged;
            roadBuffer = null;
            GameData.Instance.Lost -= OnGameLost;
        }
    }
}
