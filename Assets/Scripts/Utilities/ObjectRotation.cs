using UnityEngine;

namespace Utilities
{
    public class ObjectRotation : MonoBehaviour
    {
        public Vector3 RotationSpeed;

        void Update()
        {
            transform.Rotate(RotationSpeed.x * Time.deltaTime, RotationSpeed.y * Time.deltaTime, RotationSpeed.z * Time.deltaTime);
        }
    }
}
