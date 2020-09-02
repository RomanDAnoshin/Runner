using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform Target;
    public float ZaxisDistance;

    void Update()
    {
        // TODO: replace on cinemachine
        transform.position = new Vector3(transform.position.x, transform.position.y, Target.position.z - ZaxisDistance);
    }
}
