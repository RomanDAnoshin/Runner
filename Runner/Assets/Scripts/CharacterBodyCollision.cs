using UnityEngine;
using UnityEngine.Events;

public class CharacterBodyCollision : MonoBehaviour
{
    public UnityEvent CollisionBarricade;
    public UnityEvent CollisionCoin;

    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag) {
            case "RoadBarricade":
                CollisionBarricade.Invoke();
                break;
            case "RoadCoin":
                CollisionCoin.Invoke();
                Destroy(other.gameObject);
                break;
        }
    }
}
