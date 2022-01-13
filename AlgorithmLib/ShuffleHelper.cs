using System;
using System.Diagnostics;

namespace AlgorithmLib {
    /// <summary>
    /// 洗牌算法，随机取值算法
    /// </summary>
    public static class ShuffleHelper {

        private static readonly Random _random = new Random((int)DateTime.Now.Ticks);

        /// <summary>
        /// 洗牌，随机打乱数组的元素，复杂度为 O(n)
        /// </summary>
        public static void Shuffle<T>(T[] arr) {
            for (int i = 0; i < arr.Length; i++) {
                int idx = _random.Next(arr.Length);
                (arr[i], arr[idx]) = (arr[idx], arr[i]);
            }
        }

        /// <summary>
        /// 从数组中随机取n个下标，此方法仅需要传入数组长度，而无需传入数组本身，复杂度为 O(n)
        /// </summary>
        /// <param name="arrLen">数组长度</param>
        /// <param name="n">取n个</param>
        /// <returns>返回下标的集合</returns>
        public static int[] RandomGet(int arrLen, int n) {
            Debug.Assert(n <= arrLen && n >= 0);
            int[] arr = new int[arrLen];
            for (int i = 0; i < arrLen; i++) {
                arr[i] = i;
            }
            Shuffle(arr);
            int[] res = new int[n];
            Array.Copy(arr, res, n);
            return res;
        }

        /// <summary>
        /// 从数组中随机取n个数，复杂度为 O(n)
        /// </summary>
        /// <param name="arr">数组</param>
        /// <param name="n">取n个</param>
        /// <returns>随机取值的结果</returns>
        public static T[] RandomGet<T>(T[] arr, int n) {
            int[] arrIdx = RandomGet(arr.Length, n);
            Debug.Assert(n == arrIdx.Length);
            T[] res = new T[n];
            for (int i = 0; i < arrIdx.Length; i++) {
                res[i] = arr[arrIdx[i]];
            }
            return res;
        }
    }
}
