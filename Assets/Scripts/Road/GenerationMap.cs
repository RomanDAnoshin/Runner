using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Road
{
    public class GenerationMap
    {
        public GameObject[][] SegmentedPrefabs { get; private set; }
        public RoadBlockData[][] BlocksData { get; private set; }
        public int[][] RepetitionCounts { get; private set; }
        public int[] UniqueDifficults { get; private set; }

        public GenerationMap(List<GameObject> sortedRoadBlocks)
        {
            PrepareSegmentsMap(sortedRoadBlocks);
            PrepareUniqueDifficults();
            PrepareRepetitionCounts();
        }

        private void PrepareSegmentsMap(List<GameObject> sortedRoadBlocks)
        {
            List<int> uniqueItemIndices = new List<int>();
            var notSegmentedBlocksData = new RoadBlockData[sortedRoadBlocks.Count];

            notSegmentedBlocksData[0] = sortedRoadBlocks[0].GetComponent<RoadBlockData>();
            var previousDifficult = notSegmentedBlocksData[0].Difficult;
            uniqueItemIndices.Add(0);

            for (var i = 1; i < sortedRoadBlocks.Count; i++) {
                notSegmentedBlocksData[i] = sortedRoadBlocks[i].GetComponent<RoadBlockData>();
                var difficult = notSegmentedBlocksData[i].Difficult;
                if (previousDifficult != difficult) {
                    previousDifficult = difficult;
                    uniqueItemIndices.Add(i);
                }
            }
            uniqueItemIndices.Add(sortedRoadBlocks.Count);

            SegmentedPrefabs = new GameObject[uniqueItemIndices.Count - 1][];
            BlocksData = new RoadBlockData[SegmentedPrefabs.Length][];
            for (var i = 0; i < uniqueItemIndices.Count - 1; i++) {
                var dimension = uniqueItemIndices[i + 1] - uniqueItemIndices[i];
                SegmentedPrefabs[i] = new GameObject[dimension];
                BlocksData[i] = new RoadBlockData[SegmentedPrefabs[i].Length];
                for (var j = 0; j < dimension; j++) {
                    SegmentedPrefabs[i][j] = sortedRoadBlocks[uniqueItemIndices[i] + j];
                    BlocksData[i][j] = notSegmentedBlocksData[uniqueItemIndices[i] + j];
                }
            }
        }

        private void PrepareRepetitionCounts()
        {
            RepetitionCounts = new int[SegmentedPrefabs.Length][];
            for (var i = 0; i < RepetitionCounts.Length; i++) {
                RepetitionCounts[i] = new int[SegmentedPrefabs[i].Length];
            }
        }

        private void PrepareUniqueDifficults()
        {
            UniqueDifficults = new int[BlocksData.Length];
            for (var i = 0; i < UniqueDifficults.Length; i++) {
                UniqueDifficults[i] = BlocksData[i][0].Difficult;
            }
        }

        public void UpRepetitionCount(int segmentIndex, int elementIndex)
        {
            RepetitionCounts[segmentIndex][elementIndex]++;
        }
    }
}
