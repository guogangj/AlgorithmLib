﻿using System;
using System.Collections.Generic;

namespace AlgorithmLib {

    /// <summary>
    /// 组合算法，实测发现在元素多的时候，回溯法速度更快一些
    /// </summary>
    public enum CombinationMethod {
        /// <summary>
        /// 标志位选择法(无递归)
        /// </summary>
        FlagDrift,
        /// <summary>
        /// 回溯法(有递归)
        /// </summary>
        Backtrack
    }

    /// <summary>
    /// 组合算法类
    /// 提供了“标志位选择法”和“回溯法”两种方法，实测下来，两种方法不管是执行时间还是内存消耗都相差无几
    /// </summary>
    public static class CombinationHelper {

        /// <summary>
        /// 组合算法，可以使用标志位选择法和回溯法，默认是标志位选择法
        /// </summary>
        public static CombinationMethod Method { get; set; } = CombinationMethod.FlagDrift;

        /// <summary>
        /// 组合算法(全组合)
        /// 使用“标志位选择法”，通过移动选择标志位来达成目标，此方法没有使用递归
        /// </summary>
        /// <param name="arr">要进行组合的数组</param>
        /// <param name="callback">回调方法</param>
        public static void Combination<T>(T[] arr, Func<T[], bool> callback) {
            for (int n = 1; n <= arr.Length; n++) {
                switch (Method) {
                    case CombinationMethod.Backtrack: {
                        if(!_Backtrack(arr, n, 0, new List<T>(), callback)) {
                            return;
                        }
                        break;
                    }
                    default: { // CombinationMethod.FlagDrift
                        if (!_CombinationByFlagDrift(arr, n, callback)) {
                            return;
                        }
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// 组合算法(取n个元素)
        /// 使用“标志位选择法”，通过移动选择标志位来达成目标，此方法没有使用递归
        /// </summary>
        /// <param name="arr">要进行组合的数组</param>
        /// <param name="n">要组合的元素数目</param>
        /// <param name="callback">回调方法</param>
        public static void Combination<T>(T[] arr, int n, Func<T[], bool> callback) {
            switch (Method) {
                case CombinationMethod.Backtrack: {
                    _Backtrack(arr, n, 0, new List<T>(), callback);
                    break;
                }
                default: { // CombinationMethod.FlagDrift
                    _CombinationByFlagDrift(arr, n, callback);
                    break;
                }
            }
        }

        /// <summary>
        /// 组合算法(取n1-n2个元素)
        /// 使用“标志位选择法”，通过移动选择标志位来达成目标，此方法没有使用递归
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arr"></param>
        /// <param name="n1">取n1个数</param>
        /// <param name="n2">到取n2个数</param>
        /// <param name="callback">回调方法</param>
        public static void Combination<T>(T[] arr, int n1, int n2, Func<T[], bool> callback) {
            for (int n = n1; n <= n2; n++) {
                switch (Method) {
                    case CombinationMethod.Backtrack: {
                        if(!_Backtrack(arr, n, 0, new List<T>(), callback)) {
                            return;
                        }
                        break;
                    }
                    default: { // CombinationMethod.FlagDrift
                        if (!_CombinationByFlagDrift(arr, n, callback)) {
                            return;
                        }
                        break;
                    }
                }

            }
        }

        /// <summary>
        /// 组合算法：标志位选择法
        /// </summary>
        private static bool _CombinationByFlagDrift<T>(T[] arr, int n, Func<T[], bool> callback) {
            int arrLen = arr.Length;
            if (arrLen == 0 || n == 0 || n > arrLen || callback == null) {
                return false;
            }
            bool[] choosingFlags = new bool[arrLen];
            for (int i = 0; i < n; i++) {
                choosingFlags[i] = true;
            }
            do {
                T[] oneComb = new T[n];
                int j = 0;
                for (int i = 0; i < choosingFlags.Length; i++) {
                    if (choosingFlags[i]) {
                        oneComb[j++] = arr[i];
                    }
                }
                if (!callback(oneComb)) {
                    return false;
                }

                //如果最右不是true，从右边开始寻找第一个ture，将其右移一位
                if (!choosingFlags[arrLen - 1]) {
                    int i = arrLen - 2;
                    for (; i >= 0; i--) {
                        if (choosingFlags[i]) {
                            break;
                        }
                    }
                    choosingFlags[i + 1] = true;
                    choosingFlags[i] = false;
                }
                else {
                    //否则从右边开始尝试找到第一个不为true的
                    int i = arrLen - 2;
                    for (; i >= arrLen - n; i--) {
                        if (!choosingFlags[i]) {
                            break;
                        }
                    }
                    if (i < arrLen - n) { //所有true都在右边了
                        break;
                    }
                    //找到第一个不为true的了，再继续向左边寻找第一个为true的
                    for (; i >= 0; i--) {
                        if (choosingFlags[i]) {
                            break;
                        }
                    }
                    //将这个标志右移
                    choosingFlags[i + 1] = true;
                    choosingFlags[i] = false;
                    //将末尾的true全部靠到 i+2的位置
                    int left = i + 2;
                    int right = arrLen - 1;
                    while (left < right && !choosingFlags[left] && choosingFlags[right]) {
                        choosingFlags[left] = true;
                        choosingFlags[right] = false;
                        left++;
                        right--;
                    }
                }
            } while (true);
            return true;
        }

        /// <summary>
        /// 回溯递归方法
        /// </summary>
        /// <param name="arr">原始数组</param>
        /// <param name="n">组合几个数</param>
        /// <param name="start">从哪个开始</param>
        /// <param name="tempList">递归上一级组合的结果</param>
        /// <param name="callback">回调方法</param>
        private static bool _Backtrack<T>(T[] arr, int n, int start, List<T> tempList, Func<T[], bool> callback) {
            //终止条件，找到一对组合
            if (n == 0) {
                return callback(tempList.ToArray());
            }
            if (start < arr.Length - n) {
                //不选当前值，k不变
                if(!_Backtrack(arr, n, start + 1, tempList, callback)) {
                    return false;
                }
            }
            //选择当前值，k要减1
            tempList.Add(arr[start]);
            if(!_Backtrack(arr, n - 1, start + 1, tempList, callback)) {
                return false;
            }
            //因为是递归调用，跳到下一个分支的时候，要把这个分支选的值给移除
            tempList.RemoveAt(tempList.Count - 1);
            return true;
        }
    }
}
