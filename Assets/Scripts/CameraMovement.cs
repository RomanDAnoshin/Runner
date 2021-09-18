using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform Target;
    public Vector3 DistanceToTarget;

    void Update()
    {
        UpdatePosition();
    }

    private void UpdatePosition()
    {
        if (IsOnPosition()) {
            transform.position = new Vector3(transform.position.x - DistanceToTarget.x, transform.position.y - DistanceToTarget.y, Target.position.z - DistanceToTarget.z);
        }
    }

    private bool IsOnPosition()
    {
        return Target.position + DistanceToTarget != transform.position;
    }
}
