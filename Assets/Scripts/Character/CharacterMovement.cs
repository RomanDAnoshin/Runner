using UnityEngine;
using Road;
using Player;
using Utilities;

namespace Character
{
    public class CharacterMovement : MonoBehaviour
    {
        public enum MoveDirectionVertical
        {
            Stay,
            Run
        }

        public enum MoveDirectionHorizontal
        {
            None,
            Left,
            Right
        }

        [SerializeField] private GameObject Road;
        [SerializeField] private LanesData Lanes;

        [SerializeField] private CharacterData CharacterData;
        [SerializeField] private PlayerData PlayerData;

        [SerializeField] private AnimationCurve RotationAnimation;
        [SerializeField] private Timer RotationTimer;

        private int targetLane;
        private float currentSpeedVertical;
        private MoveDirectionVertical verticalMove;
        private MoveDirectionHorizontal horizontalMove;
        private Vector3 velocitySmoothDamp = Vector3.zero;

        void Start()
        {
            targetLane = Lanes.StartLaneIndex;
        }

        void Update()
        {
            HorizontalMovement();
            VerticalMovement();

            UpdateCurrentDistance();
        }

        private void HorizontalMovement()
        {
            if (
                horizontalMove != MoveDirectionHorizontal.None &&
                transform.position.x != Lanes.Positions[targetLane].x
            ) {
                var lanePos = new Vector3(Lanes.Positions[targetLane].x, transform.position.y, transform.position.z);
                transform.position = Vector3.SmoothDamp(transform.position, lanePos, ref velocitySmoothDamp, CharacterData.SpeedHorizontal / 100f);

                AnimateRotation();
            }
        }

        private void AnimateRotation()
        {
            if(horizontalMove == MoveDirectionHorizontal.Right) {
                StartRightRotation();
            } else {

            }
        }

        private void StartRightRotation()
        {

        }

        private void VerticalMovement()
        {
            if (verticalMove != MoveDirectionVertical.Stay) {
                MoveRoad();
            }
        }

        private void MoveRoad()
        {
            Road.transform.position -= new Vector3(0f, 0f, currentSpeedVertical * Time.deltaTime);
        }

        public void UpdateCurrentSpeed()
        {
            currentSpeedVertical = CharacterData.SpeedVertical + PlayerData.Coins * CharacterData.SpeedVerticalModificator;
        }

        public void UpdateCurrentDistance()
        {
            PlayerData.CurrentDistance = (int) Mathf.Abs(Road.transform.position.z);
        }

        public void MoveLeft()
        {
            if (
                targetLane != 0 &&
                verticalMove != MoveDirectionVertical.Stay
            ) {
                targetLane--;
                horizontalMove = MoveDirectionHorizontal.Left;
            }
        }

        public void MoveRight()
        {
            if (
                targetLane != Lanes.Positions.Length - 1 &&
                verticalMove != MoveDirectionVertical.Stay
            ) {
                targetLane++;
                horizontalMove = MoveDirectionHorizontal.Right;
            }
        }

        public void Stay()
        {
            verticalMove = MoveDirectionVertical.Stay;
            horizontalMove = MoveDirectionHorizontal.None;
        }

        public void Run()
        {
            verticalMove = MoveDirectionVertical.Run;
        }
    }
}
