using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Road
{
    public class RoadCoinCollision : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Character") {
                Destroy(gameObject);
            }
        }
    }
}
