using UnityEngine;

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

    public LanesData Lanes;

    public CharacterData CharacterData;
    public PlayerData PlayerData;
    public Animator CharacterAnimator;

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
        // TODO: rotation on change targetLane.
        if (
            horizontalMove != MoveDirectionHorizontal.None &&
            transform.position.x != Lanes.Positions[targetLane].x
        ) {
            var lanePos = new Vector3(Lanes.Positions[targetLane].x, transform.position.y, transform.position.z);
            transform.position = Vector3.SmoothDamp(transform.position, lanePos, ref velocitySmoothDamp, CharacterData.SpeedHorizontal / 100f);
        }
    }

    private void VerticalMovement()
    {
        if (verticalMove != MoveDirectionVertical.Stay) {
            transform.position += new Vector3(0f, 0f, currentSpeedVertical * Time.deltaTime);
        }
    }

    public void UpdateCurrentSpeed()
    {
        currentSpeedVertical = CharacterData.SpeedVertical + PlayerData.Coins * CharacterData.SpeedVerticalModificator;
        CharacterAnimator.speed = 1f + (PlayerData.Coins * CharacterData.SpeedVerticalModificator) / 100f;
    }

    public void UpdateCurrentDistance()
    {
        PlayerData.CurrentDistance = (int)transform.position.z;
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
