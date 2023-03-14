using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Road
{
    public class RoadBlockController : MonoBehaviour
    {
        public void RotateObjectsMirrored()
        {
            for(var i = 1; i < transform.childCount; i++) {
                var childTransform = transform.GetChild(i).transform;
                childTransform.localPosition = new Vector3(
                    childTransform.localPosition.x * -1,
                    childTransform.localPosition.y,
                    childTransform.localPosition.z
                );
                if(
                    childTransform.tag != "RoadCoin" &&
                    childTransform.tag != "RoadMarker"
                ) {
                    childTransform.Rotate(Vector3.right * 180, Space.World);
                }
            }
            gameObject.GetComponent<RoadBlockData>().MirrorEntrancesAndExits();
        }
    }
}
