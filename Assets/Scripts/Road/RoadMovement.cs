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

        private GameData gameData;
        private PlayerInput playerInput;
        private PlayerData playerData;
        private RoadGenerator roadGenerator;
        private CharacterBodyCollision characterBodyCollision;

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

        void Start()
        {
            gameData = FindObjectOfType<GameData>();
            playerInput = FindObjectOfType<PlayerInput>();
            playerInput.PlayerActed += OnPlayerActed;
            playerData = FindObjectOfType<PlayerData>();
            playerData.CurrentCoinsChanged += OnCurrentCoinsChanged;
            roadGenerator = FindObjectOfType<RoadGenerator>();
            currentSpeed = StartSpeed;
            speedModificator = 1;
            characterBodyCollision = FindObjectOfType<CharacterBodyCollision>();
            characterBodyCollision.CollisionBarricade += OnCharacterCollisionBarricade;
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
            playerData.CurrentDistance += currentSpeed * Time.deltaTime * SpeedModificator;
        }

        public void OnPlayerActed()
        {
            if (gameData.Status != GameData.GameStatus.Lose &&
                playerInput.Value == PlayerInput.PlayerActions.Run
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
            gameData = null;
            playerInput.PlayerActed -= OnPlayerActed;
            playerInput = null;
            playerData.CurrentCoinsChanged -= OnCurrentCoinsChanged;
            playerData = null;
            roadGenerator = null;
            characterBodyCollision.CollisionBarricade -= OnCharacterCollisionBarricade;
            characterBodyCollision = null;
        }
    }
}
