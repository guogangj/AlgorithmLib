using System;
using System.Collections.Generic;

namespace AlgorithmLib {
    /// <summary>
    /// 排列算法类
    /// </summary>
    public static class PermutationHelper {

        /// <summary>
        /// 排列算法(全排列)
        /// </summary>
        /// <param name="arr">要进行排列的数组</param>
        /// <param name="callback">回调方法</param>
        /// <param name="removeDuplicates">是否去除重复结果，默认是(用GetHashCode判断元素是否重复)</param>
        public static void Permutation<T>(T[] arr, Func<T[], bool> callback, bool removeDuplicates = false) {
            for (int n = 1; n <= arr.Length; n++) {
                if (!_Permutation(arr, n, callback, removeDuplicates)) {
                    return;
                }
            }
        }

        /// <summary>
        /// 排列算法(取n1-n2个元素)
        /// </summary>
        /// <param name="arr">要进行排列的数组</param>
        /// <param name="n1">排列结果取n1个数</param>
        /// <param name="n2">到取n2个数</param>
        /// <param name="callback">回调方法</param>
        /// <param name="removeDuplicates">是否去除重复结果，默认是(用GetHashCode判断元素是否重复)</param>
        public static void Permutation<T>(T[] arr, int n1, int n2, Func<T[], bool> callback, bool removeDuplicates = false) {
            for (int n = n1; n <= n2; n++) {
                if (!_Permutation(arr, n, callback, removeDuplicates)) {
                    return;
                }
            }
        }

        /// <summary>
        /// 排列算法(取n个元素)
        /// </summary>
        /// <param name="arr">要进行排列的数组</param>
        /// <param name="n">排列结果取多少个元素</param>
        /// <param name="callback">回调方法</param>
        /// <returns>true为正常结束，false为中断</returns>
        /// <param name="removeDuplicates">是否去除重复结果，默认是(用GetHashCode判断元素是否重复)</param>
        public static void Permutation<T>(T[] arr, int n, Func<T[], bool> callback, bool removeDuplicates = false) {
            _Permutation(arr, n, callback, removeDuplicates);
        }

        private static bool _Permutation<T>(T[] arr, int n, Func<T[], bool> callback, bool removeDuplicates) {

            bool SubPermutation(int len, int start) {
                if (len == 0) {
                    T[] onePerm = new T[start];
                    Array.Copy(arr, onePerm, start);
                    return callback(onePerm);
                }
                HashSet<T> hash = removeDuplicates ? new HashSet<T>() : null;
                for (int i = start; i < arr.Length; i++) {
                    if (hash != null) {
                        if (hash.Contains(arr[i])) { //避免重复排列
                            continue;
                        }
                        hash.Add(arr[i]);
                    }
                    T tmp = arr[i];
                    arr[i] = arr[start];
                    arr[start] = tmp;
                    if (!SubPermutation(len - 1, start + 1)) {
                        return false;
                    }
                    tmp = arr[i];
                    arr[i] = arr[start];
                    arr[start] = tmp;
                }
                return true;
            }

            int arrLen = arr.Length;
            if (arrLen == 0 || n == 0 || n > arrLen || callback == null) {
                return false;
            }
            return SubPermutation(n, 0);
        }
    }
}
