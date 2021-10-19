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

        private int CoinsCanBeCollectedOnStart;

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
                SpawnBlockByCoins(playerData.CurrentCoins + CoinsCanBeCollectedOnStart);
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
            if(playerData.CurrentCoins > 200) { // TODO finish
                return EasyPrefabs[0];
            }

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

            var index = 0;
            var difference = float.MaxValue;
            for(var i = 0; i < prefabs.Length; i++) {
                var prefabDifficult = prefabs[i].GetComponent<RoadBlock>().Difficult;
                if (prefabDifficult > difficult) {
                    break;
                }
                if(difficult - prefabDifficult < difference) {
                    index = i;
                    difference = difficult - prefabDifficult;
                }
            }

            Debug.Log("difficult: " + difficult.ToString() + "\n name: " + prefabs[index].GetComponent<RoadBlock>().name);
            return prefabs[index];
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

        private void SpawnStartBlocks() // TODO generation in advance by the average received number of coins on the block
        {
            for (var i = CurrentRoadBlocks.Count; i < BlockCount; i++) {
                SpawnBlockByCoins(CoinsCanBeCollectedOnStart);
                CoinsCanBeCollectedOnStart += CurrentRoadBlocks.Last.Value.GetComponent<RoadBlock>().MaxCoinsCanBeCollected;
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
