using UnityEngine;

namespace Road
{
    public class RoadBlockGenerator : MonoBehaviour // TODO more complex generation
    {
        private RoadBlock RoadBlock;

        void Start()
        {
            RoadBlock = gameObject.GetComponent<RoadBlock>();
        }

        void OnDestroy()
        {
            RoadBlock = null;
        }
    }
}
