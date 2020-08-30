using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadSpawner : MonoBehaviour
{
    public GameObject[] RoadBlockPrefabs;
    public GameObject StartBlock;
    public Transform HeroTransform;
    public List<GameObject> RoadBlocks = new List<GameObject>();

    private float blockZposition;
    private float blockLenght = 20f;
    private float delayZone = 40;
    private int blockCount = 7;

    void Start()
    {
        blockZposition = StartBlock.transform.position.z;
        for(var i = 0; i < blockCount; i++) {
            SpawnBlock();
        }
    }

    void Update()
    {
        if (IsNnecessarySpawn()) {
            SpawnBlock();
            DestroyBlock();
        }
    }

    private bool IsNnecessarySpawn()
    {
        if(HeroTransform.position.z - delayZone > (blockZposition - blockCount * blockLenght)) {
            return true;
        } else {
            return false;
        }
    }

    private void SpawnBlock()
    {
        var block = Instantiate(RoadBlockPrefabs[Random.Range(0, RoadBlockPrefabs.Length)], transform);
        blockZposition += blockLenght;
        block.transform.position = new Vector3(0, 0, blockZposition);
        RoadBlocks.Add(block);
    }

    private void DestroyBlock()
    {
        Destroy(RoadBlocks[0]);
        RoadBlocks.RemoveAt(0);
    }
}
