using Game;
using Player;
using System.Collections.Generic;
using UnityEngine;

namespace Road
{
    public class RoadGenerator : MonoBehaviour
    {
        [SerializeField] private AnimationCurve DifficultyCurve;
        [SerializeField] private int SearchDistance;

        private GameObject StartBlockPrefab;
        private GameObject TransitionBlockPrefab;
        private List<GameObject> RoadBlocksPrefabs;
        private GenerationMap generationMap;
        private RoadBuffer roadBuffer;
        private int CoinsCanBeCollectedOnStart;

        void Start()
        {
            LoadRoadPrefabs();
            LoadSpecialRoadPrefabs();
            roadBuffer = gameObject.GetComponent<RoadBuffer>();
            SortPrefabsByDifficult();
            generationMap = new GenerationMap(RoadBlocksPrefabs);
            SpawnStartBlocks();
        }

        void Update()
        {
            if (IsNecessarySpawn()) {
                SpawnBlockByCoins(PlayerData.Instance.CurrentCoins + CoinsCanBeCollectedOnStart);
            }
        }

        private void LoadSpecialRoadPrefabs()
        {
            var specialBlocks = Resources.LoadAll<GameObject>("RoadBlocks/Special");
            if (specialBlocks[0].name.Contains("Start")) {
                StartBlockPrefab = specialBlocks[0];
                TransitionBlockPrefab = specialBlocks[1];
            } else {
                StartBlockPrefab = specialBlocks[1];
                TransitionBlockPrefab = specialBlocks[0];
            }
        }

        private void LoadRoadPrefabs()
        {
            RoadBlocksPrefabs = new List<GameObject>();
            var prefabs = Resources.LoadAll<GameObject>("RoadBlocks/Common");
            RoadBlocksPrefabs.AddRange(prefabs);
            prefabs = Resources.LoadAll<GameObject>("RoadBlocks/Mirrors");
            RoadBlocksPrefabs.AddRange(prefabs);
        }

        private bool IsNecessarySpawn()
        {
            var bottomBlockTransform = roadBuffer.BottomBlock.transform;
            return bottomBlockTransform.position.z + bottomBlockTransform.GetChild(0).localScale.z < GameGenerator.Instance.CharacterStartPosition.z;
        }

        private void SpawnStartBlock()
        {
            var block = Instantiate(StartBlockPrefab, transform);
            roadBuffer.AddToTop(block);
        }

        private void SpawnBlockByCoins(int coins)
        {
            var compatibleBlock = GetCompatibleBlock(coins);
            var block = Instantiate(compatibleBlock, transform);
            roadBuffer.AddToTop(block);
        }

        private GameObject GetCompatibleBlock(int coins)
        {
            var difficult = DifficultyCurve.Evaluate(coins);
            var startSearchSegmentIndex = MyMath.GetNearestIndex(generationMap.UniqueDifficults, difficult);
            var topBlockExits = roadBuffer.TopBlock.GetComponent<RoadBlockData>().IsNLineAvailableToExit;
            var (сompatibleBlocksIndexes, segmentIndex) = SearchCompatibleBlocksInSegments(generationMap.BlocksData, topBlockExits, startSearchSegmentIndex);
            if (сompatibleBlocksIndexes.Length < 1) {
                return TransitionBlockPrefab;
            } else {
                var indexElementFewerRepetitions = MyMath.GetIndexOfMinByIndexes(generationMap.RepetitionCounts[segmentIndex], сompatibleBlocksIndexes);
                generationMap.UpRepetitionCount(segmentIndex, indexElementFewerRepetitions);
                return generationMap.SegmentedPrefabs[segmentIndex][indexElementFewerRepetitions];
            }
        }

        private (int[] сompatibleBlocksIndexes, int segmentIndex) SearchCompatibleBlocksInSegments(RoadBlockData[][] collection, bool[] exits, int startSegmentIndex)
        {
            var сompatibleBlocksIndexes = GetCompatibleBlocksIndexes(collection[startSegmentIndex], exits);
            var segmentIndex = startSegmentIndex;
            if (сompatibleBlocksIndexes.Length < 1) {
                for (var i = 1; i <= SearchDistance && startSegmentIndex + i < collection.Length; i++) {
                    сompatibleBlocksIndexes = GetCompatibleBlocksIndexes(collection[startSegmentIndex + i], exits);
                    if (сompatibleBlocksIndexes.Length > 0) {
                        segmentIndex = startSegmentIndex + i;
                        break;
                    }
                }
            }
            return (сompatibleBlocksIndexes, segmentIndex);
        }

        private int[] GetCompatibleBlocksIndexes(RoadBlockData[] collection, bool[] availableExits)
        {
            var indexes = new List<int>();
            for (var i = 0; i < collection.Length; i++) {
                if (IsBlocksCompatible(collection[i].IsLineNAvailableToEntry, availableExits)) {
                    indexes.Add(i);
                }
            }
            return indexes.ToArray();
        }

        private void SpawnStartBlocks()
        {
            SpawnStartBlock();
            CoinsCanBeCollectedOnStart += StartBlockPrefab.GetComponent<RoadBlockData>().MaxCoinsCanBeCollected;
            for (var i = roadBuffer.Count; i < roadBuffer.Capacity; i++) {
                SpawnBlockByCoins(CoinsCanBeCollectedOnStart);
                CoinsCanBeCollectedOnStart += roadBuffer.TopBlock.GetComponent<RoadBlockData>().MaxCoinsCanBeCollected;
            }
        }

        private void SortPrefabsByDifficult()
        {
            RoadBlocksPrefabs.Sort(
                (prefab1, prefab2) => { 
                    return prefab1.GetComponent<RoadBlockData>().Difficult.CompareTo(prefab2.GetComponent<RoadBlockData>().Difficult); 
                }
            );
        }

        private bool IsBlocksCompatible(bool[] entrance, bool[] exit)
        {
            for (var i = 0; i < entrance.Length; i++) {
                if (entrance[i] && exit[i]) {
                    return true;
                }
            }
            return false;
        }

        void OnDestroy()
        {
            roadBuffer = null;
            generationMap = null;
        }
    }
}
