using UnityEngine;
using Road;
using Player;
using Utilities;
using System;

namespace Character
{
    public class CharacterMovement : MonoBehaviour, IMovable
    {
        public static CharacterMovement Instance;

        public enum HorizontalMove
        {
            None,
            Left,
            Right
        }
        public Action PositionChanged;

        [SerializeField] private float HorizontalSpeed;

        private bool isMoving;
        private int targetLane;
        private HorizontalMove horizontalMove;
        private Vector3 velocitySmoothDamp = Vector3.zero;

        void Awake()
        {
            Instance = this;
        }

        void Start()
        {
            CharacterBodyCollision.Instance.CollisionBarricade += OnCharacterCollisionBarricade;
            targetLane = LanesData.Instance.StartLaneIndex;
        }

        void Update()
        {
            HorizontalMovement();
        }

        private void HorizontalMovement()
        {
            if (
                horizontalMove != HorizontalMove.None &&
                transform.position.x != LanesData.Instance.Positions[targetLane].x
            ) {
                var lanePos = new Vector3(LanesData.Instance.Positions[targetLane].x, transform.position.y, transform.position.z);
                transform.position = Vector3.SmoothDamp(transform.position, lanePos, ref velocitySmoothDamp, HorizontalSpeed / 100f);
                PositionChanged?.Invoke();
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
                targetLane != LanesData.Instance.Positions.Length - 1 &&
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

        private void OnCharacterCollisionBarricade()
        {
            Stay();
        }

        void OnDestroy()
        {
            Stay();
            CharacterBodyCollision.Instance.CollisionBarricade -= OnCharacterCollisionBarricade;
        }
    }
}
