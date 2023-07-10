using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AlgorithmLib {
    /// <summary>
    /// 关联关系帮助类
    /// </summary>
    public class RelationshipHelper {

        /// <summary>
        /// 根据index“旋转数组”并去掉第一个元素, 如[1,2,3,4], 按index为2, 就返回[4,1,2]
        /// </summary>
        private static int[] RollArrayAndRemoveFirstByIndex(int[] array, int index) {
            Debug.Assert(array != null && array.Length > 1 && index < array.Length && index >= 0);
            int[] newArray = new int[array.Length - 1];
            if (index < array.Length - 1) {
                Array.Copy(array, index + 1, newArray, 0, array.Length - 1 - index);
            }
            if (index > 0) {
                Array.Copy(array, 0, newArray, array.Length - index - 1, index);
            }
            return newArray;
        }

        /// <summary>
        /// 在数组头增加一个元素, 如[1,2,3,4]，加5，就返回[5,1,2,3,4]
        /// </summary>
        private static int[] PrependElementToArray(int[] array, int prependValue) {
            int[] newRes = new int[array.Length + 1];
            newRes[0] = prependValue;
            Array.Copy(array, 0, newRes, 1, array.Length);
            return newRes;
        }

        /// <summary>
        /// "原始"被拆分成若干的"碎片"，此方法尝试计算出"碎片"与"原始"的关系
        /// 例如给出"原始"：[8,13,2], 给出"碎片：[7,5,3,2,6]
        /// 返回的结果是：[5,3,6,7,2]
        /// 因为：[5+3=8, 6+7=13, 2]
        /// </summary>
        /// <param name="originals">原始数组</param>
        /// <param name="fragments">碎片数组</param>
        /// <returns>重排后的碎片数组，返回null表示无法建立起对应关系</returns>
        public static int[] FindRelationship(int[] originals, int[] fragments) {
            if (originals == null || fragments == null || originals.Sum() != fragments.Sum() || fragments.Length < originals.Length) {
                return null;
            }

            int[] SubFindRelationship(int currOriginalIdx, int matchedValForCurrOrgIdx, int[] subFragments) {
                Debug.Assert(currOriginalIdx < originals.Length && subFragments.Any());
                if (originals.Length - currOriginalIdx > subFragments.Length) {
                    return null;
                }
                if (originals.Length - currOriginalIdx == 1 && subFragments.Length == 1) {
                    return matchedValForCurrOrgIdx + subFragments[0] == originals[currOriginalIdx] ? subFragments : null;
                }
                HashSet<int> hash = new HashSet<int>();
                for (int i = 0; i < subFragments.Length; i++) {
                    if(hash.Contains(subFragments[i])) {
                        continue;
                    }
                    hash.Add(subFragments[i]);
                    if (matchedValForCurrOrgIdx + subFragments[i] == originals[currOriginalIdx]) {
                        int[] res = SubFindRelationship(currOriginalIdx + 1, 0, RollArrayAndRemoveFirstByIndex(subFragments, i));
                        if (res != null) {
                            return PrependElementToArray(res, subFragments[i]);
                        }
                    }
                    else if (matchedValForCurrOrgIdx + subFragments[i] < originals[currOriginalIdx]) {
                        int[] res = SubFindRelationship(currOriginalIdx, matchedValForCurrOrgIdx + subFragments[i], RollArrayAndRemoveFirstByIndex(subFragments, i));
                        if (res != null) {
                            return PrependElementToArray(res, subFragments[i]);
                        }
                    }
                }
                return null;
            }

            return SubFindRelationship(0, 0, fragments);
        }

        /// <summary>
        /// 找出能够合成整体的碎片集（这相当于是FindRelationship的“局部版”）
        /// </summary>
        /// <param name="whole">整体</param>
        /// <param name="fragmentgs">碎片源</param>
        /// <returns>返回null表示找不到合适的</returns>
        public static int[] FindFragments(int whole, int[] fragments) {
            int[] SubFindFragments(int subWhole, int[] subFragments) {
                if (subFragments.Length == 1) {
                    return subWhole == subFragments[0] ? subFragments : null;
                }
                HashSet<int> hash = new HashSet<int>();
                for(int i=0; i<subFragments.Length; i++) {
                    if (hash.Contains(subFragments[i])) {
                        continue;
                    }
                    hash.Add(subFragments[i]);
                    if (subWhole == subFragments[i]) {
                        return new int[] { subFragments[i] };
                    }
                    else if (subWhole > subFragments[i]) {
                        int[] res = SubFindFragments(subWhole - subFragments[i], RollArrayAndRemoveFirstByIndex(subFragments, i));
                        if(res != null) {
                            return PrependElementToArray(res, subFragments[i]);
                        }
                    }
                }
                return null;
            }
            return SubFindFragments(whole, fragments);
        }

        /// <summary>
        /// FindRelationship的泛化版
        /// 有一个整体对象列表和一个碎片对象列表
        /// </summary>
        /// <typeparam name="T1">整体对象的类型</typeparam>
        /// <typeparam name="T2">碎片对象的类型</typeparam>
        /// <typeparam name="T1Id">整体对象ID的类型</typeparam>
        /// <param name="orgList">整体对象列表</param>
        /// <param name="fragList">碎片对象列表</param>
        /// <param name="toGetT1Id">获取整体对象ID的方法</param>
        /// <param name="toGetT1Val">获取整体对象值的方法</param>
        /// <param name="toGetT2Val">获取碎片对象值的方法</param>
        /// <param name="toSetT2RefId">填写碎片对象关联到的整体对象的ID的方法(这是结果输出)</param>
        /// <exception cref="ArgumentException"></exception>
        public static void MakeRelationShip<T1, T2, T1Id>(
            IEnumerable<T1> orgList,
            IEnumerable<T2> fragList,
            Func<T1, T1Id> toGetT1Id,
            Func<T1, int> toGetT1Val,
            Func<T2, int> toGetT2Val,
            Action<T2, T1Id> toSetT2RefId) {

            int[] orgValues = orgList.Select(l => toGetT1Val(l)).ToArray();
            int[] fragValues = fragList.Select(l => toGetT2Val(l)).ToArray();
            Debug.Assert(orgValues.Length == orgList.Count());
            Debug.Assert(fragValues.Length == fragList.Count());

            int[] fragValuesDone = FindRelationship(orgValues, fragValues);
            if (fragValuesDone == null) {
                throw new ArgumentException("Unable to find relationship of those set.");
            }
            Debug.Assert(fragValues.Length == fragValuesDone.Length);

            bool[] matchFlags = new bool[fragValuesDone.Length];
            for (int i = 0; i < matchFlags.Length; i++) {
                matchFlags[i] = false;
            }

            T1[] arrOrgList = orgList.ToArray();
            T2[] arrFragList = fragList.ToArray();

            //用结果去匹配
            int orgPointer = 0;
            int fragPointer = 0;
            int currentOrgValue = 0;
            T1Id tid = toGetT1Id(arrOrgList[orgPointer]);
            while (true) {
                Debug.Assert(fragPointer<fragValues.Length);
                currentOrgValue += fragValuesDone[fragPointer];

                //在碎片中，找到这个值的ID：fragmentsDone[fragPointer]，并将ID关联至整体
                int fragmentIdx;
                for(fragmentIdx=0; fragmentIdx< fragValuesDone.Length; fragmentIdx++) {
                    if (fragValues[fragmentIdx] == fragValuesDone[fragPointer] && !matchFlags[fragmentIdx]) {
                        matchFlags[fragmentIdx] = true;
                        break;
                    }
                }
                Debug.Assert(fragmentIdx < fragValuesDone.Length);
                toSetT2RefId(arrFragList[fragmentIdx], tid);

                fragPointer++;
                Debug.Assert(currentOrgValue <= orgValues[orgPointer]);
                if (currentOrgValue== orgValues[orgPointer]) {
                    orgPointer++;
                    if(orgPointer >= orgValues.Length) {
                        break;
                    }
                    tid = toGetT1Id(arrOrgList[orgPointer]);
                    currentOrgValue = 0;
                }
            }
        }
    }
}
