using Character;
using Game;
using Player;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Road
{
    public class RoadGenerator : MonoBehaviour
    {
        [SerializeField] private GameObject StartBlockPrefab;
        [SerializeField] private List<GameObject> RoadBlocksPrefabs;
        [SerializeField] private AnimationCurve DifficultyCurve;

        private GenerationMap generationMap;
        private RoadBuffer roadBuffer;
        private int CoinsCanBeCollectedOnStart;

        void Start()
        {
            roadBuffer = gameObject.GetComponent<RoadBuffer>();
            SortPrefabsByDifficult();
            generationMap = new GenerationMap(RoadBlocksPrefabs);
            SpawnStartBlocks();
        }

        void Update()
        {
            if (IsNecessarySpawn()) {
                roadBuffer.DestroyBottomBlock();
                SpawnBlockByCoins(PlayerData.Instance.CurrentCoins + CoinsCanBeCollectedOnStart);
            }
        }

        private bool IsNecessarySpawn()
        {
            var bottomBlockTransform = roadBuffer.CurrentBlocks.First.Value.transform;
            return bottomBlockTransform.position.z + bottomBlockTransform.GetChild(0).localScale.z < GameGenerator.Instance.CharacterStartPosition.z;
        }

        private void SpawnStartBlock()
        {
            var block = Instantiate(StartBlockPrefab, transform);
            roadBuffer.AddBlockToTop(block);
        }

        private void SpawnBlockByCoins(int coins)
        {
            var difficult = DifficultyCurve.Evaluate(coins);
            var block = Instantiate(generationMap.GetBlockByDifficulty(difficult), transform);
            roadBuffer.AddBlockToTop(block);
        }

        private void SpawnStartBlocks()
        {
            SpawnStartBlock();
            CoinsCanBeCollectedOnStart += StartBlockPrefab.GetComponent<RoadBlock>().MaxCoinsCanBeCollected;
            for (var i = roadBuffer.CurrentBlocks.Count; i < roadBuffer.Capacity; i++) {
                SpawnBlockByCoins(CoinsCanBeCollectedOnStart);
                CoinsCanBeCollectedOnStart += roadBuffer.CurrentBlocks.Last.Value.GetComponent<RoadBlock>().MaxCoinsCanBeCollected;
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

        void OnDestroy()
        {
            roadBuffer = null;
        }
    }
}
