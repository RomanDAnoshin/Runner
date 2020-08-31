using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterBodyCollision : MonoBehaviour
{
    public UnityEvent CollisionEvent;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("RoadBarricade")) {
            CollisionEvent.Invoke();

            // TODO: remove lag on Z axis
        }
    }
}
