using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public Transform HeroTransform;
    public PlayerInput PlayerInput;
    public Lanes Lanes;

    public float Speed;
    public float SmoothTime;
    private int tragetLane;
    private Vector3 velocity = Vector3.zero;


    private void Start()
    {
        tragetLane = Lanes.StartLaneIndex;
    }

    private void Update()
    {
        if(PlayerInput.MoveDirection != MoveDirection.None) {
            if(PlayerInput.MoveDirection == MoveDirection.Left) {
                if(tragetLane != 0) {
                    tragetLane--;
                }
            } else {
                if(tragetLane != Lanes.Positions.Length - 1) {
                    tragetLane++;
                }
            }
        }

        var lanePos = new Vector3(Lanes.Positions[tragetLane].x, HeroTransform.position.y, HeroTransform.position.z);
        // Sideway movement
        HeroTransform.position = Vector3.SmoothDamp(HeroTransform.position, lanePos, ref velocity, SmoothTime);
        // Forward movement
        HeroTransform.position += new Vector3(0, 0, Speed * Time.deltaTime);
    }
}
