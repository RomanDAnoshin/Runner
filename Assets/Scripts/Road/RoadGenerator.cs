using Character;
using Player;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Road
{
    public class RoadGenerator : MonoBehaviour
    {
        public LinkedList<GameObject> CurrentRoadBlocks { get; protected set; }

        [SerializeField]
        private List<GameObject> RoadBlocksPrefabs;

        private GameObject[][] PrefabsWithSegments;
        private int[] PrefabsDifficultWithSegments;
        private int[][] RepetitionCountsMap;

        [SerializeField, Range(1, 30)] private int BlockCount;

        private Transform characterTransform;
        private float blockLenght;

        [SerializeField] private AnimationCurve DifficultyCurve;

        private int CoinsCanBeCollectedOnStart;

        void Start()
        {
            CurrentRoadBlocks = new LinkedList<GameObject>();
            characterTransform = CharacterMovement.Instance.transform;
            SortPrefabsByDifficult();
            PrepareSegmentsMap();
            AddStartBlockToCollection();
            CollectBlockLenght();
            SpawnStartBlocks();
        }

        void Update()
        {
            if (IsNecessarySpawn()) {
                SpawnBlockByCoins(PlayerData.Instance.CurrentCoins + CoinsCanBeCollectedOnStart);
                DestroyBottomBlock();
            }
        }

        private bool IsNecessarySpawn()
        {
            return CurrentRoadBlocks.First.Value.transform.position.z + blockLenght < characterTransform.position.z;
        }

        private void SpawnBlockByCoins(int coins)
        {
            var difficult = DifficultyCurve.Evaluate(coins);
            var block = Instantiate(GenerateBlockByDifficult(difficult), transform);
            AddBlockToTop(block);
        }

        private void AddBlockToTop(GameObject block)
        {
            var topBlockZPosition = CurrentRoadBlocks.Last.Value.transform.position.z;
            block.transform.position = new Vector3(0, 0, topBlockZPosition + blockLenght);
            CurrentRoadBlocks.AddLast(block);
        }

        private GameObject GenerateBlockByDifficult(float difficult)
        {
            return GetBlockByDifficulty(difficult); //TODO Clean code
        }

        private GameObject GetBlockByDifficulty(float difficult)
        {
            var indexInFirstRow = 0;
            var difference = float.MaxValue;
            for(var i = 0; i < PrefabsDifficultWithSegments.Length; i++) {
                if(PrefabsDifficultWithSegments[i] > difficult) {
                    break;
                }
                if(difficult - PrefabsDifficultWithSegments[i] < difference) {
                    indexInFirstRow = i;
                    difference = difficult - PrefabsDifficultWithSegments[i];
                }
            }

            return GetBlockWithFewerRepetitions(indexInFirstRow);
        }

        private GameObject GetBlockWithFewerRepetitions(int indexInFirstRow)
        {
            if(RepetitionCountsMap[indexInFirstRow].Length > 1) {
                var minValue = RepetitionCountsMap[indexInFirstRow][0];
                var indexOfMin = 0;
                for(var i = 1; i < RepetitionCountsMap[indexInFirstRow].Length; i++) {
                    if(RepetitionCountsMap[indexInFirstRow][i] < minValue) {
                        minValue = RepetitionCountsMap[indexInFirstRow][i];
                        indexOfMin = i;
                    }
                }

                RepetitionCountsMap[indexInFirstRow][indexOfMin]++;
                return PrefabsWithSegments[indexInFirstRow][indexOfMin];
            } else {
                RepetitionCountsMap[indexInFirstRow][0]++;
                return PrefabsWithSegments[indexInFirstRow][0];
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
            CoinsCanBeCollectedOnStart += startBlock.GetComponent<RoadBlock>().MaxCoinsCanBeCollected;
        }

        private void CollectBlockLenght()
        {
            blockLenght = CurrentRoadBlocks.First.Value.transform.Find("Bottom").transform.localScale.z;
        }

        private void SpawnStartBlocks()
        {
            for (var i = CurrentRoadBlocks.Count; i < BlockCount; i++) {
                SpawnBlockByCoins(CoinsCanBeCollectedOnStart);
                CoinsCanBeCollectedOnStart += CurrentRoadBlocks.Last.Value.GetComponent<RoadBlock>().MaxCoinsCanBeCollected;
            }
        }

        private void SortPrefabsByDifficult()
        {
            RoadBlocksPrefabs.Sort(
                (prefab1, prefab2) => { 
                    return prefab1.GetComponent<RoadBlock>().Difficult.CompareTo(prefab2.GetComponent<RoadBlock>().Difficult); 
                }
            );
        }

        private void PrepareSegmentsMap()
        {
            List<int> uniqueDifficults = new List<int>();
            List<int> uniqueItemIndices = new List<int>();

            uniqueDifficults.Add(RoadBlocksPrefabs[0].GetComponent<RoadBlock>().Difficult);
            uniqueItemIndices.Add(0);

            for (var i = 1; i < RoadBlocksPrefabs.Count; i++) {
                var difficult = RoadBlocksPrefabs[i].GetComponent<RoadBlock>().Difficult;
                if (uniqueDifficults[uniqueDifficults.Count - 1] != difficult) {
                    uniqueDifficults.Add(difficult);
                    uniqueItemIndices.Add(i);
                }
            }
            uniqueItemIndices.Add(RoadBlocksPrefabs.Count); // add end

            PrefabsDifficultWithSegments = new int[uniqueDifficults.Count];
            for (var i = 0; i < uniqueDifficults.Count; i++) {
                PrefabsDifficultWithSegments[i] = uniqueDifficults[i];
            }

            PrefabsWithSegments = new GameObject[uniqueDifficults.Count][];
            for (var i = 0; i < uniqueDifficults.Count; i++) {
                var dimension = uniqueItemIndices[i + 1] - uniqueItemIndices[i];
                PrefabsWithSegments[i] = new GameObject[dimension];
                for (var j = 0; j < dimension; j++) {
                    PrefabsWithSegments[i][j] = RoadBlocksPrefabs[uniqueItemIndices[i] + j];
                }
            }

            RepetitionCountsMap = new int[PrefabsWithSegments.Length][];
            for(var i = 0; i < RepetitionCountsMap.Length; i++) {
                RepetitionCountsMap[i] = new int[PrefabsWithSegments[i].Length];
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
