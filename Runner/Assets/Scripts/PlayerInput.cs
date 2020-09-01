using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public enum InputType
    {
        None,
        MoveLeft,
        MoveRight,
        Run,
        Stay
    }

    public InputType Value;

    private void Update()
    {
        // TODO: on events.
        // TODO: Take away control from the player over the character when death.
        if (Input.anyKeyDown) {
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) {
                Value = InputType.MoveLeft;
            } else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) {
                Value = InputType.MoveRight;
            } else if(Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space)) {
                Value = InputType.Run;
            } else if (Input.GetKeyDown(KeyCode.DownArrow)) {
                Value = InputType.Stay;
            }
        } else {
            Value = InputType.None;
        }
    }
}
