using UnityEngine;

namespace Road
{
    public class RoadBlockData : MonoBehaviour
    {
        public int Difficult
        {
            get {
                return difficult;
            }
        }
        [SerializeField, Range(0, 100)] 
        private int difficult;

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
        public bool[] IsLineNAvailableToEntry
        {
            get {
                return isLineNAvailableToEntry;
            }
        }

        [Header("Exit")]
        [SerializeField]
        private bool[] isNLineAvailableToExit;
        public bool[] IsNLineAvailableToExit
        {
            get {
                return isNLineAvailableToExit;
            }
        }

        [Header("Symmetry")]
        [SerializeField]
        private bool isSymmetrical;
        public bool IsSymmetrical
        {
            get {
                return isSymmetrical;
            }
        }

        public void MirrorEntrancesAndExits()
        {
            if (!isSymmetrical) {
                var buffer = isLineNAvailableToEntry[0];
                isLineNAvailableToEntry[0] = isLineNAvailableToEntry[isLineNAvailableToEntry.Length - 1];
                isLineNAvailableToEntry[isLineNAvailableToEntry.Length - 1] = buffer;

                buffer = isNLineAvailableToExit[0];
                isNLineAvailableToExit[0] = isNLineAvailableToExit[isNLineAvailableToExit.Length - 1];
                isNLineAvailableToExit[isNLineAvailableToExit.Length - 1] = buffer;
            }
        }
    }
}
