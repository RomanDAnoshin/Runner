using UnityEngine;

namespace Utilities
{
    public class TargetFollower : MonoBehaviour
    {
        public Vector3 PositionRelativeToTarget;

        public Movement TargetMovement;

        void Start()
        {
            if(TargetMovement != null) {
                TargetMovement.PositionChanged += OnTargetPositionChanged;
                OnTargetPositionChanged(TargetMovement.Position);
            }
        }

        public void OnTargetPositionChanged(Vector3 value)
        {
            transform.position = value + PositionRelativeToTarget;
        }

        void OnDestroy()
        {
            if (TargetMovement != null) {
                TargetMovement.PositionChanged -= OnTargetPositionChanged;
            }
        }
    }
}
