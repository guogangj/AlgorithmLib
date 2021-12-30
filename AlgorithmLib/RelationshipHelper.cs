using System;
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

            int[] SubFindRelationship(int currOriginalIdx, int matchedValForCurrOrgIdx,  int[] subFragments) {
                Debug.Assert(currOriginalIdx < originals.Length && subFragments.Any());
                if (originals.Length-currOriginalIdx > subFragments.Length) {
                    return null;
                }
                if (originals.Length - currOriginalIdx == 1 && subFragments.Length == 1) {
                    return matchedValForCurrOrgIdx + subFragments[0]== originals[currOriginalIdx] ? subFragments : null;
                }

                for (int i = 0; i < subFragments.Length; i++) {
                    if (matchedValForCurrOrgIdx + subFragments[i] == originals[currOriginalIdx]) {
                        int[] res = SubFindRelationship(currOriginalIdx+1, 0, RollArrayAndRemoveFirstByIndex(subFragments, i));
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
    }
}
