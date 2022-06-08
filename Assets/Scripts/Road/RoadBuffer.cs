using System;
using UnityEngine;

namespace Road
{
    public class RoadBuffer : MonoBehaviour
    {
        private GameObject[] Buffer;

        [Range(1, 30)]
        [SerializeField]
        private int capacity;
        public int Capacity
        {
            get {
                return capacity;
            }
        }

        private int count;
        public int Count
        {
            get {
                return count;
            }
        }

        private int indexFirst;
        private int indexLast;

        public GameObject TopBlock
        {
            get {
                return Buffer[indexLast];
            }
            private set {
                Buffer[indexLast] = value;
            }
        }

        public GameObject BottomBlock
        {
            get {
                return Buffer[indexFirst];
            }
            private set {
                Buffer[indexFirst] = value;
            }
        }

        void Awake()
        {
            Buffer = new GameObject[capacity];
        }

        public void AddToTop(GameObject block)
        {
            if (count == capacity) {
                DestroyBottomBlock();
            }
            if (count > 0) {
                block.transform.position = new Vector3(0, 0, TopBlock.transform.position.z + TopBlock.transform.GetChild(0).localScale.z);
                UpIndexLast();
            } else {
                block.transform.position = Vector3.zero;
            }
            count++;
            Buffer[indexLast] = block;
        }

        private void DestroyBottomBlock()
        {
            if(count > 0) {
                Destroy(BottomBlock);
                BottomBlock = null;
                count--;
                UpIndexFirst();
            }
        }

        private void UpIndexFirst()
        {
            if (indexFirst < capacity - 1) {
                indexFirst++;
            } else {
                indexFirst = 0;
            }
        }

        private void UpIndexLast()
        {
            if(indexLast < capacity - 1) {
                indexLast++;
            } else {
                indexLast = 0;
            }
        }

        public void ForEach(Action<GameObject> action)
        {
            if (count > 0) {
                if(count == 1) {
                    action(Buffer[indexFirst]);
                } else if (indexFirst < indexLast) {
                    for (var i = indexFirst; i <= indexLast; i++) {
                        action(Buffer[i]);
                    }
                } else {
                    for(var i = 0; i <= indexLast; i++) {
                        action(Buffer[i]);
                    }
                    for(var i = indexFirst; i < capacity; i++) {
                        action(Buffer[i]);
                    }
                }
            }
        }

        void OnDestroy()
        {
            foreach (var block in Buffer) {
                Destroy(block);
            }
            Buffer = null;
        }
    }
}
