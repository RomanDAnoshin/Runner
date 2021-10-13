using Character;
using System.Collections.Generic;
using UnityEngine;

namespace Road
{
    public class RoadGenerator : MonoBehaviour // TODO more complex generation
    {
        public LinkedList<GameObject> CurrentRoadBlocks { get; protected set; }
        [SerializeField] private GameObject[] RoadBlockPrefabs;
        [SerializeField, Range(1, 30)] private int BlockCount;

        private Transform characterTransform;
        private float blockLenght;

        void Start()
        {
            CurrentRoadBlocks = new LinkedList<GameObject>();
            characterTransform = FindObjectOfType<CharacterMovement>().transform;
            AddStartBlockToCollection();
            CollectBlockLenght();
            SpawnStartBlocks();
        }

        void Update()
        {
            if (IsNecessarySpawn()) {
                SpawnBlock();
                DestroyBottomBlock();
            }
        }

        private bool IsNecessarySpawn()
        {
            return CurrentRoadBlocks.First.Value.transform.position.z + blockLenght < characterTransform.position.z;
        }

        private void SpawnBlock()
        {
            var block = Instantiate(RoadBlockPrefabs[GenerateBlockType()], transform);

            var topBlockZPosition = CurrentRoadBlocks.Last.Value.transform.position.z;
            block.transform.position = new Vector3(0, 0, topBlockZPosition + blockLenght);
            CurrentRoadBlocks.AddLast(block);
        }

        private int GenerateBlockType()
        {
            return Random.Range(0, RoadBlockPrefabs.Length);
        }

        private void DestroyBottomBlock()
        {
            Destroy(CurrentRoadBlocks.First.Value);
            CurrentRoadBlocks.RemoveFirst();
        }

        private void AddStartBlockToCollection()
        {
            var startBlock = transform.Find("RoadBlockStart").gameObject;
            CurrentRoadBlocks.AddFirst(startBlock);
        }

        private void CollectBlockLenght()
        {
            blockLenght = CurrentRoadBlocks.First.Value.transform.Find("Bottom").transform.localScale.z;
        }

        private void SpawnStartBlocks()
        {
            for (var i = 1; i < BlockCount; i++) {
                SpawnBlock();
            }
        }

        void OnDestroy()
        {
            foreach(var block in CurrentRoadBlocks) {
                Destroy(block);
            }
            CurrentRoadBlocks = null;
            RoadBlockPrefabs = null;
            characterTransform = null;
        }
    }
}
