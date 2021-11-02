using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Road
{
    public class RoadBuffer : MonoBehaviour
    {
        public LinkedList<GameObject> CurrentBlocks { get; private set; }
        [Range(1, 30)] public int Capacity;

        void Start()
        {
            CurrentBlocks = new LinkedList<GameObject>();
        }

        public void AddBlockToTop(GameObject block)
        {
            if(CurrentBlocks.Count < Capacity) {
                if (CurrentBlocks.Count > 0) {
                    var topBlockTransform = CurrentBlocks.Last.Value.transform;
                    block.transform.position = new Vector3(0, 0, topBlockTransform.position.z + topBlockTransform.GetChild(0).localScale.z);
                } else {
                    block.transform.position = Vector3.zero;
                }
                CurrentBlocks.AddLast(block);
            }
        }

        public void DestroyBottomBlock()
        {
            if(CurrentBlocks.Count > 0) {
                Destroy(CurrentBlocks.First.Value);
                CurrentBlocks.RemoveFirst();
            }
        }

        void OnDestroy()
        {
            foreach (var block in CurrentBlocks) {
                Destroy(block);
            }
            CurrentBlocks = null;
        }
    }
}
