using Player;
using Road;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace Character
{
    public class CharacterBodyCollision : MonoBehaviour
    {
        public static CharacterBodyCollision Instance;

        public Action CollisionBarricade;
        public Action CollisionCoin;
        public Action Destroying;

        void Awake()
        {
            Instance = this;
        }

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
