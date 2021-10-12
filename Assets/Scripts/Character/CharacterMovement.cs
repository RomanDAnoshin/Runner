using UnityEngine;
using Road;
using Player;
using Utilities;

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

        [SerializeField] private LanesData Lanes;

        [SerializeField] private float HorizontalSpeed;

        private bool isMoving;
        private int targetLane;
        private HorizontalMove horizontalMove;
        private Vector3 velocitySmoothDamp = Vector3.zero;

        void Start()
        {
            targetLane = Lanes.StartLaneIndex;
        }

        void Update()
        {
            HorizontalMovement();
        }

        private void HorizontalMovement()
        {
            if (
                horizontalMove != HorizontalMove.None &&
                transform.position.x != Lanes.Positions[targetLane].x
            ) {
                var lanePos = new Vector3(Lanes.Positions[targetLane].x, transform.position.y, transform.position.z);
                transform.position = Vector3.SmoothDamp(transform.position, lanePos, ref velocitySmoothDamp, HorizontalSpeed / 100f);
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
                targetLane != Lanes.Positions.Length - 1 &&
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

        public void OnCharacterCollisionBarricade()
        {
            Stay();
        }
    }
}
