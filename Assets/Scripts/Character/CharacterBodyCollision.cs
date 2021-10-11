using UnityEngine;
using UnityEngine.Events;

namespace Character
{
    public class CharacterBodyCollision : MonoBehaviour
    {
        [SerializeField] private UnityEvent CollisionBarricade;
        [SerializeField] private UnityEvent CollisionCoin;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.gameObject.tag == "RoadBarricade") {
                CollisionBarricade.Invoke();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "RoadCoin") {
                CollisionCoin.Invoke();
                Destroy(other.gameObject);
            }
        }
    }
}
