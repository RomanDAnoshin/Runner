using UnityEngine;

namespace Road
{
    public class LanesData : MonoBehaviour
    {
        public static LanesData Instance;

        void Awake()
        {
            Instance = this;
        }

        [SerializeField] private Vector3[] positions;
        [HideInInspector] public Vector3[] Positions
        {
            get {
                return positions;
            }
        }

        [SerializeField] private int startLaneIndex;
        [HideInInspector] public int StartLaneIndex
        {
            get {
                return startLaneIndex;
            }
        }
    }
}
