using UnityEngine;

namespace Road
{
    public class LanesData : MonoBehaviour
    {
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
