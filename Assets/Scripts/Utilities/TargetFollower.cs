using UnityEngine;

namespace Utilities
{
    public class TargetFollower : MonoBehaviour
    {
        public Transform Target;
        public Vector3 PositionRelativeToTarget;

        public void OnTargetPositionChanged()
        {
            transform.position = Target.position + PositionRelativeToTarget;
        }
    }
}
