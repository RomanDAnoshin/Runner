using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Road
{
    public class RoadBlock : MonoBehaviour
    {
        [HideInInspector] 
        public int Difficult
        {
            get {
                return difficult;
            }
        }
        [SerializeField, Range(0, 100)] 
        private int difficult;

        [HideInInspector]
        public int MaxCoinsCanBeCollected
        {
            get {
                return maxCoinsCanBeCollected;
            }
        }
        [SerializeField, Range(0, 6)] 
        private int maxCoinsCanBeCollected;
    }
}
