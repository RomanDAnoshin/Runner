using UnityEngine;

namespace Utilities
{
    public class ObjectRotation : MonoBehaviour
    {
        public Vector3 RotationSpeed;

        void Update()
        {
            transform.Rotate(RotationSpeed * Time.deltaTime);
        }
    }
}
