using Character;
using Player;
using System;
using UnityEngine;

namespace Road
{
    public class RoadMovement : MonoBehaviour, IMovable, IPlayerControllable
    {
        public Action SpeedModificatorChanged;

        [SerializeField] private float StartSpeed;
        [SerializeField] private int FinalValueOfCoins;
        [SerializeField] private float SpeedGainModifier;

        private RoadGenerator roadGenerator;

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
        [Range(1f, 2f)] private float speedModificator;
        private bool isMoving;
        private float currentSpeed;

        void Start()
        {
            PlayerInput.Instance.PlayerActed += OnPlayerActed;
            PlayerData.Instance.CurrentCoinsChanged += OnCurrentCoinsChanged;
            roadGenerator = gameObject.GetComponent<RoadGenerator>();
            currentSpeed = StartSpeed;
            speedModificator = 1;
            GameData.Instance.StatusChanged += OnGameStatusChanged;
        }

        void Update()
        {
            MoveRoad();
        }

        private void MoveRoad()
        {
            if (isMoving) {
                foreach(var block in roadGenerator.CurrentRoadBlocks) {
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

        public void OnPlayerActed()
        {
            if (GameData.Instance.Status != GameStatus.Lose &&
                PlayerInput.Instance.Value == PlayerInput.PlayerActions.Run
            ) {
                Move();
            }
        }

        private void OnGameStatusChanged(GameStatus gameStatus)
        {
            if (gameStatus == GameStatus.Lose) {
                Stay();
            }
        }

        private void OnCurrentCoinsChanged()
        {
            UpdateSpeedModificator();
        }

        private void UpdateSpeedModificator()
        {
            SpeedModificator = 1f + PlayerData.Instance.CurrentCoins * SpeedGainModifier;
            if (PlayerData.Instance.CurrentCoins == FinalValueOfCoins) {
                PlayerData.Instance.CurrentCoinsChanged -= OnCurrentCoinsChanged;
            }
        }

        void OnDestroy()
        {
            Stay();
            PlayerInput.Instance.PlayerActed -= OnPlayerActed;
            PlayerData.Instance.CurrentCoinsChanged -= OnCurrentCoinsChanged;
            roadGenerator = null;
            GameData.Instance.StatusChanged -= OnGameStatusChanged;
        }
    }
}
