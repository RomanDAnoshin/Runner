using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Road
{
    public class GenerationMap
    {
        private GameObject[][] PrefabsWithSegments;
        private int[] UniqueDifficults;
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

            UniqueDifficults = new int[uniqueDifficults.Count];
            for (var i = 0; i < uniqueDifficults.Count; i++) {
                UniqueDifficults[i] = uniqueDifficults[i];
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
            if (UniqueDifficults.Length > 1) {
                var upperIndex = GetUpperBound(UniqueDifficults, difficult);
                var lowerIndex = upperIndex - 1;
                if (
                    lowerIndex > 0 &&
                    difficult - UniqueDifficults[lowerIndex] < UniqueDifficults[upperIndex] - difficult
                ) {
                    indexInFirstRow = lowerIndex;
                } else {
                    indexInFirstRow = upperIndex;
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

        private int GetUpperBound(int[] array, float value)
        {
            var first = 0;
            var count = array.Length - 1 - first;
            int step;
            int i;
            while(count > 0) {
                i = first;
                step = count / 2;
                i += step;
                if(!(value < array[i])) {
                    first = ++i;
                    count -= step + 1;
                } else {
                    count = step;
                }
            }
            return first;
        }
    }
}
