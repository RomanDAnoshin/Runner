using Character;
using Player;
using UnityEngine;

namespace Road
{
    public class RoadMovement : MonoBehaviour, IMovable
    {
        [SerializeField] private PlayerInput PlayerInput;
        [SerializeField] private PlayerData PlayerData;
        [SerializeField] private CharacterData CharacterData;

        [SerializeField] private float StartSpeed;

        private bool isMoving;
        private float currentSpeed;

        void Start()
        {
            currentSpeed = StartSpeed;
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
            PlayerData.CurrentDistance = (int)Mathf.Abs(transform.position.z); // TODO remove infinite offset
        }

        public void OnPlayerActed()
        {
            if (CharacterData.State == CharacterData.CharacterState.Alive) {
                switch (PlayerInput.Value) {
                    case PlayerInput.PlayerActions.None:
                        break;
                    case PlayerInput.PlayerActions.Run:
                        Move();
                        break;
                    case PlayerInput.PlayerActions.Stay:
                        Stay();
                        break;
                }
            }
        }

        public void OnCharacterCollisionBarricade()
        {
            Stay();
        }
    }
}
