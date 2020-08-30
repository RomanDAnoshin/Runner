using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public MoveDirection MoveDirection;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (Input.anyKeyDown) {
            if (Input.GetKeyDown(KeyCode.LeftArrow)) {
                MoveDirection = MoveDirection.Left;
            } else if (Input.GetKeyDown(KeyCode.RightArrow)) {
                MoveDirection = MoveDirection.Right;
            } 
        } else {
            MoveDirection = MoveDirection.None;
        }
    }
}

public enum MoveDirection
{
    None,
    Left,
    Right
}
