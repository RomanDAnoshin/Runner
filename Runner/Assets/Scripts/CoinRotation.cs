using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinRotation : MonoBehaviour
{
    public float RotationSpeed;

    void Update()
    {
        transform.Rotate(0f, 0f, RotationSpeed * Time.deltaTime);
    }
}
