using System.Collections.Generic;
using UnityEngine;

public class RoadGenerator : MonoBehaviour
{
    public GameObject[] RoadBlockPrefabs;
    public GameObject StartBlock;
    public Transform HeroTransform;
    public List<GameObject> CurrentRoadBlocks = new List<GameObject>();

    [Range(1, 30)]
    public int BlockCount;

    private float blockZposition;
    private float blockLenght = 20f;
    private float delayZone = 40;

    void Start()
    {
        blockZposition = StartBlock.transform.position.z;
        CurrentRoadBlocks.Add(StartBlock);
        for(var i = 1; i < BlockCount; i++) {
            SpawnBlock();
        }
    }

    void Update()
    {
        if (IsNecessarySpawn()) {
            SpawnBlock();
            DestroyOldBlock();
        }
    }

    private bool IsNecessarySpawn()
    {
        return HeroTransform.position.z - delayZone > (blockZposition - BlockCount * blockLenght);
    }

    private void SpawnBlock()
    {
        var block = Instantiate(RoadBlockPrefabs[Random.Range(0, RoadBlockPrefabs.Length)], transform);

        blockZposition += blockLenght;
        block.transform.position = new Vector3(0, 0, blockZposition);
        CurrentRoadBlocks.Add(block);
    }

    private void DestroyOldBlock()
    {
        Destroy(CurrentRoadBlocks[0]);
        CurrentRoadBlocks.RemoveAt(0);
    }
}
