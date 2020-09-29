using System;

namespace AlgorithmLib {
    public class PermutationHelper {
        /// <summary>
        /// 排列算法(全排列)
        /// </summary>
        /// <param name="arr">要进行排列的数组</param>
        /// <param name="callback">回调方法</param>
        public static void Permutation1<T>(T[] arr, Action<T[]> callback) {
            for (int n = 1; n <= arr.Length; n++) {
                Permutation1(arr, n, callback);
            }
        }

        private static void SubPermutation<T>(T[] arr, int len, int start, Action<T[]> callback) {
            if (len == 0) {
                T[] onePerm = new T[start];
                Array.Copy(arr, onePerm, start);
                callback(onePerm);
                return;
            }
            for(int i=start; i<arr.Length; i++) {
                T tmp = arr[i];
                arr[i] = arr[start];
                arr[start] = tmp;
                SubPermutation(arr, len-1, start + 1, callback);
                tmp = arr[i];
                arr[i] = arr[start];
                arr[start] = tmp;
            }
        }

        /// <summary>
        /// 排列算法(取n个元素)
        /// </summary>
        /// <param name="arr">要进行排列的数组</param>
        /// <param name="n">要排列的元素数目</param>
        /// <param name="callback">回调方法</param>
        public static void Permutation1<T>(T[] arr, int n, Action<T[]> callback) {
            int arrLen = arr.Length;
            if (arrLen == 0 || n == 0 || n > arrLen || callback == null) {
                return;
            }
            SubPermutation(arr, n, 0, callback);
        }

        /// <summary>
        /// 排列算法(取n1-n2个元素)
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="n1">取n1个数</param>
        /// <param name="n2">到取n2个数</param>
        /// <param name="callback">回调方法</param>
        public static void Permutation1<T>(T[] arr, int n1, int n2, Action<T[]> callback) {
            for (int n = n1; n <= n2; n++) {
                Permutation1(arr, n, callback);
            }
        }
    }
}
