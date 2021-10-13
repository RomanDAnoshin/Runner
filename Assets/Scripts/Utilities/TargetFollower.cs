using UnityEngine;

namespace Utilities
{
    public class TargetFollower : MonoBehaviour
    {
        [SerializeField] private Transform Target;
        [SerializeField] private Vector3 PositionRelativeToTarget;

        void Update()
        {
            UpdatePosition();
        }

        private void UpdatePosition()
        {
            if (!IsOnPosition()) {
                transform.position = Target.position + PositionRelativeToTarget;
            }
        }

        private bool IsOnPosition()
        {
            return Target.position + PositionRelativeToTarget == transform.position;
        }
    }
}
