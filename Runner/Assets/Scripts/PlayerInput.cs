using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour
{
    public enum PlayerActions
    {
        None,
        MoveLeft,
        MoveRight,
        Run,
        Stay
    }

    public PlayerActions Value;
    public UnityEvent PlayerActed;

    void Update()
    {
        if (Input.anyKeyDown) {
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) {
                Value = PlayerActions.MoveLeft;
                PlayerActed.Invoke();
            } else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) {
                Value = PlayerActions.MoveRight;
                PlayerActed.Invoke();
            } else if(Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space)) {
                Value = PlayerActions.Run;
                PlayerActed.Invoke();
            }
        } else {
            Value = PlayerActions.None;
        }
    }
}
