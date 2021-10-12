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
                transform.position = new Vector3(
                    Target.position.x + PositionRelativeToTarget.x,
                    Target.position.y + PositionRelativeToTarget.y, 
                    Target.position.z + PositionRelativeToTarget.z
                    );
            }
        }

        private bool IsOnPosition()
        {
            return Target.position + PositionRelativeToTarget == transform.position;
        }
    }
}
