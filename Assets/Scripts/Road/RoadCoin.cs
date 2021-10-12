using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Road
{
    public class RoadCoin : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Character") {
                Destroy(gameObject);
            }
        }
    }
}
