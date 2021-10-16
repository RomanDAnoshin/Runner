using Character;
using Player;
using System.Collections.Generic;
using UnityEngine;

namespace Road
{
    public class RoadGenerator : MonoBehaviour // TODO more complex generation
    {
        public LinkedList<GameObject> CurrentRoadBlocks { get; protected set; }
        [SerializeField] private GameObject[] VeryHardPrefabs;
        [SerializeField] private GameObject[] HardPrefabs;
        [SerializeField] private GameObject[] MediumPrefabs;
        [SerializeField] private GameObject[] EasyPrefabs;
        [SerializeField, Range(1, 30)] private int BlockCount;

        private Transform characterTransform;
        private float blockLenght;

        [SerializeField] private AnimationCurve DifficultyCurve;
        private PlayerData playerData;

        void Start()
        {
            playerData = FindObjectOfType<PlayerData>();
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
            var block = Instantiate(GenerateBlockPrefab(), transform);

            var topBlockZPosition = CurrentRoadBlocks.Last.Value.transform.position.z;
            block.transform.position = new Vector3(0, 0, topBlockZPosition + blockLenght);
            CurrentRoadBlocks.AddLast(block);
        }

        private GameObject GenerateBlockPrefab()
        {
            if(playerData.CurrentCoins > 200) { // TODO finish
                return EasyPrefabs[0];
            }

            var difficult = DifficultyCurve.Evaluate(playerData.CurrentCoins);

            if (difficult > 50f) {
                if (difficult > 75f) {
                    return GetBlockByDifficulty(difficult, VeryHardPrefabs);
                } else {
                    return GetBlockByDifficulty(difficult, HardPrefabs);
                }
            } else {
                if (difficult > 25f) {
                    return GetBlockByDifficulty(difficult, MediumPrefabs);
                } else {
                    return GetBlockByDifficulty(difficult, EasyPrefabs);
                }
            }
        }

        private GameObject GetBlockByDifficulty(float difficult, GameObject[] prefabs)
        {
            if(prefabs.Length == 1) {
                return prefabs[0];
            }

            var leftBorderIndex = 0;
            var rightBorderIndex = 0;
            for(var i = 0; i < prefabs.Length - 1; i++) {
                if(
                    prefabs[i].GetComponent<RoadBlock>().Difficult <= difficult && // TODO may be several blocks with the same difficulty value
                    prefabs[i + 1].GetComponent<RoadBlock>().Difficult > difficult
                ) {
                    leftBorderIndex = i;
                    rightBorderIndex = i + 1;
                    break;
                }
            }
            if(rightBorderIndex != 0) {
                var leftDifficult = prefabs[leftBorderIndex].GetComponent<RoadBlock>().Difficult;
                var rightDifficult = prefabs[rightBorderIndex].GetComponent<RoadBlock>().Difficult;
                if(difficult - leftDifficult < rightDifficult - difficult) {
                    Debug.Log("difficult: " + difficult.ToString() + "\n name: " + prefabs[leftBorderIndex].GetComponent<RoadBlock>().name);
                    return prefabs[leftBorderIndex];
                } else {
                    Debug.Log("difficult: " + difficult.ToString() + "\n name: " + prefabs[rightBorderIndex].GetComponent<RoadBlock>().name);
                    return prefabs[rightBorderIndex];
                }
            } else {
                Debug.Log("difficult: " + difficult.ToString() + "\n name: " + prefabs[leftBorderIndex].GetComponent<RoadBlock>().name);
                return prefabs[leftBorderIndex];
            }
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

        private void SpawnStartBlocks() // TODO generation in advance by the average received number of coins on the block
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
            characterTransform = null;
        }
    }
}
