using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MyMath
{
    public static int GetNearestIndex(int[] collection, float value)
    {
        if (collection.Length > 1) {
            var upperIndex = GetUpperBound(collection, value);
            var lowerIndex = upperIndex - 1;
            if (
                lowerIndex > 0 &&
                value - collection[lowerIndex] < collection[upperIndex] - value
            ) {
                return lowerIndex;
            } else {
                return upperIndex;
            }
        } else {
            return 0;
        }
    }

    public static int GetUpperBound(int[] array, float value)
    {
        var first = 0;
        var count = array.Length - 1 - first;
        int step;
        int i;
        while (count > 0) {
            i = first;
            step = count / 2;
            i += step;
            if (!(value < array[i])) {
                first = ++i;
                count -= step + 1;
            } else {
                count = step;
            }
        }
        return first;
    }

    public static int GetIndexOfMinByIndexes(int[] collection, int[] indexes)
    {
        if (collection.Length > 1) {
            var minValue = collection[indexes[0]];
            var indexOfMin = indexes[0];
            foreach(var i in indexes) {
                if (collection[i] < minValue) {
                    minValue = collection[i];
                    indexOfMin = i;
                }
            }
            return indexOfMin;
        } else {
            return 0;
        }
    }

    public static bool IsContains(int[] collection, int value)
    {
        for (var i = 0; i < collection.Length; i++) {
            if (collection[i] == value) {
                return true;
            }
        }
        return false;
    }
}
