using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Road
{
    public class RoadBlockData : MonoBehaviour
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

        [Header("Entrance")]
        [SerializeField]
        private bool[] isLineNAvailableToEntry;
        [HideInInspector]
        public bool[] IsLineNAvailableToEntry
        {
            get {
                return isLineNAvailableToEntry;
            }
        }

        [Header("Exit")]
        [SerializeField]
        private bool[] isNLineAvailableToExit;
        [HideInInspector]
        public bool[] IsNLineAvailableToExit
        {
            get {
                return isNLineAvailableToExit;
            }
        }
    }
}
