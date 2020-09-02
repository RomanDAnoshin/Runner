using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
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

    public PlayerInput PlayerInput;
    public PlayerData PlayerData;
    public CharacterData CharacterData;
    public Lanes Lanes;

    private int targetLane;
    private MoveDirectionVertical verticalMove;
    private MoveDirectionHorizontal horizontalMove;

    private void Start()
    {
        targetLane = Lanes.StartLaneIndex;
    }

    private void Update()
    {
        ProcessPlayerInput(PlayerInput.Value);

        HorizontalMovement();
        VerticalMovement();

        UpdateCurrentDistance();
    }

    public void OnCollisionEnter()
    {
        verticalMove = MoveDirectionVertical.Stay;
        horizontalMove = MoveDirectionHorizontal.None;
    }

    private void ProcessPlayerInput(PlayerInput.InputType value)
    {
        if (value != PlayerInput.InputType.None) {
            // Move Left
            if (value == PlayerInput.InputType.MoveLeft) {
                if (targetLane != 0) {
                    targetLane--;
                    horizontalMove = MoveDirectionHorizontal.Left;
                }
                // Move Right
            } else if (value == PlayerInput.InputType.MoveRight) {
                if (targetLane != Lanes.Positions.Length - 1) {
                    targetLane++;
                    horizontalMove = MoveDirectionHorizontal.Right;
                }
                // Run
            } else if (value == PlayerInput.InputType.Run) {
                verticalMove = MoveDirectionVertical.Run;
                // Stay
            } else if (value == PlayerInput.InputType.Stay) {
                verticalMove = MoveDirectionVertical.Stay;
            }
        }
    }

    private void HorizontalMovement()
    {
        if (
            horizontalMove != MoveDirectionHorizontal.None && 
            transform.position.x != Lanes.Positions[targetLane].x
        ) {
            var lanePos = new Vector3(Lanes.Positions[targetLane].x, transform.position.y, transform.position.z);
            transform.position = Vector3.SmoothDamp(transform.position, lanePos, ref CharacterData.Velocity, CharacterData.SmoothTime);
        }
    }

    private void VerticalMovement()
    {
        if(verticalMove != MoveDirectionVertical.Stay) {
            transform.position += new Vector3(0f, 0f, CharacterData.Speed * Time.deltaTime);
        }
    }

    private void UpdateCurrentDistance()
    {
        PlayerData.CurrentDistance = transform.position.z;
    }

    public void RaiseCoin()
    {
        PlayerData.Coins++;
    }
}
