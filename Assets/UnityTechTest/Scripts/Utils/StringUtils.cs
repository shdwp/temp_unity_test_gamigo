using System;

namespace UnityTechTest.Scripts.Utils
{
    /// <summary>
    /// Collection of various string utils
    /// </summary>
    public static class StringUtils
    {
        /// <summary>
        /// Sort byte array based on order specified in sortOrder. Array will be sorted in-place.
        /// 
        /// Uses standard library Array.Sort to perform the actual sorting, with lambda expression as a comparer.
        /// Slower because it always calls Array.Index for both left and right operand.
        /// Based on the circumstances it might be a better idea to use this, because it's much more readable.
        /// </summary>
        /// <param name="inputAndOutput"></param>
        /// <param name="sortOrder"></param>
        public static void SortLettersBuiltInLambda(byte[] inputAndOutput, byte[] sortOrder)
        {
            Array.Sort(inputAndOutput, (a, b) => Array.IndexOf(sortOrder, a).CompareTo(Array.IndexOf(sortOrder, b)));
        }

        /// <summary>
        /// Sort byte array based on order specified in sortOrder. Array will be sorted in-place.
        /// 
        /// Uses custom implementation of quicksort.
        /// Quicker since it doesn't query Array.IndexOf as frequent as comparer lambda does.
        /// </summary>
        /// <param name="inputAndOutput"></param>
        /// <param name="sortOrder"></param>
        public static void SortLetters(byte[] inputAndOutput, byte[] sortOrder)
        {
            QuicksortByOrderArray(inputAndOutput, sortOrder, 0, inputAndOutput.Length - 1);
        }

        private static void QuicksortByOrderArray(byte[] array, byte[] order, int leftIndex, int rightIndex)
        {
            // needed for tail recursion optimization
            while (true)
            {
                var fromIndex = leftIndex;
                var toIndex = rightIndex;
                var pivotSortValue = Array.IndexOf(order, array[leftIndex]);

                while (fromIndex <= toIndex)
                {
                    while (Array.IndexOf(order, array[fromIndex]) < pivotSortValue)
                    {
                        fromIndex++;
                    }

                    while (Array.IndexOf(order, array[toIndex]) > pivotSortValue)
                    {
                        toIndex--;
                    }

                    if (fromIndex <= toIndex)
                    {
                        (array[fromIndex], array[toIndex]) = (array[toIndex], array[fromIndex]);
                        fromIndex++;
                        toIndex--;
                    }
                }

                // left hand has to be recursive
                if (leftIndex < toIndex)
                {
                    QuicksortByOrderArray(array, order, leftIndex, toIndex);
                }

                // right hand can be tail-optimized
                if (fromIndex < rightIndex)
                {
                    leftIndex = fromIndex;
                }
                else
                {
                    break;
                }
            }
        }
    }
}