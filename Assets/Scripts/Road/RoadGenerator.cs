using System.Collections.Generic;
using UnityEngine;

namespace Road
{
    public class RoadGenerator : MonoBehaviour
    {
        [SerializeField] private GameObject[] RoadBlockPrefabs;
        [SerializeField] private Transform CharacterTransform;
        [SerializeField] private List<GameObject> CurrentRoadBlocks = new List<GameObject>();

        [SerializeField, Range(1, 30)] private int BlockCount;

        private float blockLenght;

        void Start()
        {
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
            return CurrentRoadBlocks[0].transform.position.z + blockLenght < CharacterTransform.position.z;
        }

        private void SpawnBlock()
        {
            var block = Instantiate(RoadBlockPrefabs[GenerateBlockType()], transform);

            var topBlockZPosition = CurrentRoadBlocks[CurrentRoadBlocks.Count - 1].transform.position.z;
            block.transform.position = new Vector3(0, 0, topBlockZPosition + blockLenght);
            CurrentRoadBlocks.Add(block);
        }

        private int GenerateBlockType()
        {
            return Random.Range(0, RoadBlockPrefabs.Length);
        }

        private void DestroyBottomBlock()
        {
            Destroy(CurrentRoadBlocks[0]);
            CurrentRoadBlocks.RemoveAt(0);
        }

        private void AddStartBlockToCollection()
        {
            var startBlock = this.transform.Find("RoadBlockStart").gameObject;
            CurrentRoadBlocks.Add(startBlock);
        }

        private void CollectBlockLenght()
        {
            blockLenght = CurrentRoadBlocks[0].transform.Find("Bottom").transform.localScale.z;
        }

        private void SpawnStartBlocks()
        {
            for (var i = 1; i < BlockCount; i++) {
                SpawnBlock();
            }
        }
    }
}
