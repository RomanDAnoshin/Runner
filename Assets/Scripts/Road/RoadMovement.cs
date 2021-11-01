using Character;
using Player;
using System;
using UnityEngine;

namespace Road
{
    public class RoadMovement : MonoBehaviour, IMovable, IPlayerControllable
    {
        public static RoadMovement Instance;

        public Action SpeedModificatorChanged;

        [SerializeField] private float StartSpeed;

        private RoadGenerator roadGenerator;

        public float SpeedModificator
        {
            get {
                return speedModificator;
            }
            protected set {
                speedModificator = value;
                SpeedModificatorChanged?.Invoke();
            }
        }
        [Range(1f, 2f)] private float speedModificator;
        private bool isMoving;
        private float currentSpeed;

        void Awake()
        {
            Instance = this;
        }

        void Start()
        {
            PlayerInput.Instance.PlayerActed += OnPlayerActed;
            PlayerData.Instance.CurrentCoinsChanged += OnCurrentCoinsChanged;
            roadGenerator = gameObject.GetComponent<RoadGenerator>();
            currentSpeed = StartSpeed;
            speedModificator = 1;
            CharacterBodyCollision.Instance.CollisionBarricade += OnCharacterCollisionBarricade;
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
            if (GameData.Instance.Status != GameData.GameStatus.Lose &&
                PlayerInput.Instance.Value == PlayerInput.PlayerActions.Run
            ) {
                Move();
            }
        }

        private void OnCharacterCollisionBarricade()
        {
            Stay();
        }

        private void OnCurrentCoinsChanged()
        {
            UpdateSpeedModificator();
        }

        private void UpdateSpeedModificator() // TODO bring speed control to a higher level
        {
            //if (playerData.CurrentCoins > 100) {
            //    SpeedModificator = 2f;
            //    playerData.CurrentCoinsChanged -= OnCurrentCoinsChanged;
            //} else {
            //    SpeedModificator = 1f + playerData.CurrentCoins / 100f;
            //}
        }

        void OnDestroy()
        {
            Stay();
            PlayerInput.Instance.PlayerActed -= OnPlayerActed;
            PlayerData.Instance.CurrentCoinsChanged -= OnCurrentCoinsChanged;
            roadGenerator = null;
            CharacterBodyCollision.Instance.CollisionBarricade -= OnCharacterCollisionBarricade;
        }
    }
}
