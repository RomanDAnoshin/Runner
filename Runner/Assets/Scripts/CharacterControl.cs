using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
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
    public Lanes Lanes;

    public float Speed;

    public float SmoothTime;
    private int targetLane;
    private Vector3 velocity = Vector3.zero;

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
            transform.position = Vector3.SmoothDamp(transform.position, lanePos, ref velocity, SmoothTime);
        }
    }

    private void VerticalMovement()
    {
        if(verticalMove != MoveDirectionVertical.Stay) {
            transform.position += new Vector3(0, 0, Speed * Time.deltaTime);
        }
    }
}
