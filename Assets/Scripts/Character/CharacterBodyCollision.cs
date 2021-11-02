using Player;
using Road;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace Character
{
    public class CharacterBodyCollision : MonoBehaviour
    {
        public Action CollisionBarricade;
        public Action CollisionCoin;
        public Action Destroying;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.gameObject.tag == "RoadBarricade") {
                CollisionBarricade?.Invoke();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "RoadCoin") {
                CollisionCoin?.Invoke();
            }
        }

        void OnDestroy()
        {
            Destroying?.Invoke();
        }
    }
}
