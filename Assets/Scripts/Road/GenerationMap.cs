using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Road
{
    public class GenerationMap
    {
        private GameObject[][] PrefabsWithSegments;
        private int[] PrefabsDifficultWithSegments;
        private int[][] RepetitionCountsMap;

        public GenerationMap(List<GameObject> sortedRoadBlocks)
        {
            PrepareSegmentsMap(sortedRoadBlocks);
        }

        private void PrepareSegmentsMap(List<GameObject> sortedRoadBlocks)
        {
            List<int> uniqueDifficults = new List<int>();
            List<int> uniqueItemIndices = new List<int>();

            uniqueDifficults.Add(sortedRoadBlocks[0].GetComponent<RoadBlock>().Difficult);
            uniqueItemIndices.Add(0);

            for (var i = 1; i < sortedRoadBlocks.Count; i++) {
                var difficult = sortedRoadBlocks[i].GetComponent<RoadBlock>().Difficult;
                if (uniqueDifficults[uniqueDifficults.Count - 1] != difficult) {
                    uniqueDifficults.Add(difficult);
                    uniqueItemIndices.Add(i);
                }
            }
            uniqueItemIndices.Add(sortedRoadBlocks.Count); // add end

            PrefabsDifficultWithSegments = new int[uniqueDifficults.Count];
            for (var i = 0; i < uniqueDifficults.Count; i++) {
                PrefabsDifficultWithSegments[i] = uniqueDifficults[i];
            }

            PrefabsWithSegments = new GameObject[uniqueDifficults.Count][];
            for (var i = 0; i < uniqueDifficults.Count; i++) {
                var dimension = uniqueItemIndices[i + 1] - uniqueItemIndices[i];
                PrefabsWithSegments[i] = new GameObject[dimension];
                for (var j = 0; j < dimension; j++) {
                    PrefabsWithSegments[i][j] = sortedRoadBlocks[uniqueItemIndices[i] + j];
                }
            }

            RepetitionCountsMap = new int[PrefabsWithSegments.Length][];
            for (var i = 0; i < RepetitionCountsMap.Length; i++) {
                RepetitionCountsMap[i] = new int[PrefabsWithSegments[i].Length];
            }
        }

        public GameObject GetBlockByDifficulty(float difficult)
        {
            var indexInFirstRow = 0;
            var difference = float.MaxValue;
            for (var i = 0; i < PrefabsDifficultWithSegments.Length; i++) {
                if (PrefabsDifficultWithSegments[i] > difficult) {
                    break;
                }
                if (difficult - PrefabsDifficultWithSegments[i] < difference) {
                    indexInFirstRow = i;
                    difference = difficult - PrefabsDifficultWithSegments[i];
                }
            }

            return GetBlockWithFewerRepetitions(indexInFirstRow);
        }

        private GameObject GetBlockWithFewerRepetitions(int indexInFirstRow)
        {
            if (RepetitionCountsMap[indexInFirstRow].Length > 1) {
                var minValue = RepetitionCountsMap[indexInFirstRow][0];
                var indexOfMin = 0;
                for (var i = 1; i < RepetitionCountsMap[indexInFirstRow].Length; i++) {
                    if (RepetitionCountsMap[indexInFirstRow][i] < minValue) {
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
    }
}
