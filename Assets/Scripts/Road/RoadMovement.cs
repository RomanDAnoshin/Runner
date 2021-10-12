using Character;
using Player;
using UnityEngine;

namespace Road
{
    public class RoadMovement : MonoBehaviour, IMovable, IPlayerControllable
    {
        [SerializeField] private float StartSpeed;

        private GameData gameData;
        private PlayerInput playerInput;
        private PlayerData playerData;

        private bool isMoving;
        private float currentSpeed;

        void Start()
        {
            gameData = FindObjectOfType<GameData>();
            playerInput = FindObjectOfType<PlayerInput>();
            playerInput.PlayerActed += OnPlayerActed;
            playerData = FindObjectOfType<PlayerData>();
            currentSpeed = StartSpeed;
            var characterBodyCollision = FindObjectOfType<CharacterBodyCollision>();
            characterBodyCollision.CollisionBarricade += OnCharacterCollisionBarricade;
        }

        void Update()
        {
            MoveRoad();
            UpdateCurrentDistance();
        }

        private void MoveRoad()
        {
            if (isMoving) {
                transform.position -= new Vector3(0f, 0f, currentSpeed * Time.deltaTime);
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
            playerData.CurrentDistance = (int)Mathf.Abs(transform.position.z); // TODO remove infinite offset, automat of platforms
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
    }
}
