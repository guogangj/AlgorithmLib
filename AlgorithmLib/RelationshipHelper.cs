using System;
using System.Diagnostics;
using System.Linq;

namespace AlgorithmLib {
    /// <summary>
    /// 关联关系帮助类
    /// </summary>
    public class RelationshipHelper {

        /// <summary>
        /// 获取“旋转数组” [1,2,3,4]，按index为2旋转，就变成[3,4,1,2]
        /// </summary>
        private static int[] GetRotatedArray(int[] array, int index) {
            Debug.Assert(array != null && index < array.Length);
            int[] newArray = new int[array.Length];
            Array.Copy(array, index, newArray, 0, array.Length - index);
            Array.Copy(array, 0, newArray, array.Length - index, index);
            return newArray;
        }

        /// <summary>
        /// 获取“子数组” [1,2,3,4]，按index为1截取，就变成[2,3,4]
        /// </summary>
        private static int[] GetSubArray(int[] array, int index) {
            Debug.Assert(array != null && index < array.Length);
            int[] newArray = new int[array.Length - index];
            Array.Copy(array, index, newArray, 0, array.Length - index);
            return newArray;
        }

        private static int[] SubFindRelationship(int[] subOriginals, int[] subFragments) {
            Debug.Assert(subOriginals.Any() && subFragments.Any());
            if(subOriginals.Length > subFragments.Length) {
                return null;
            }
            if (subOriginals.Length == 1 && subFragments.Length == 1) {
                return subOriginals[0] == subFragments[0] ? subOriginals : null;
            }

            for (int i = 0; i < subFragments.Length; i++) {
                if (subFragments[i] == subOriginals[0]) {
                    int[] rotated = GetRotatedArray(subFragments, i);
                    int[] res = SubFindRelationship(GetSubArray(subOriginals, 1),
                        GetSubArray(rotated, 1));
                    if (res != null) {
                        int[] newRes = new int[res.Length+1];
                        newRes[0] = subFragments[i];
                        Array.Copy(res, 0, newRes, 1, res.Length);
                        return newRes;
                    }
                }
                else if(subFragments[i]<subOriginals[0]) {
                    int[] rotated = GetRotatedArray(subFragments, i);
                    int[] newSubOriginals = GetSubArray(subOriginals, 0);
                    newSubOriginals[0] -= subFragments[i];
                    int[] res = SubFindRelationship(newSubOriginals, GetSubArray(rotated, 1));
                    if(res!=null) {
                        int[] newRes = new int[res.Length + 1];
                        newRes[0] = subFragments[i];
                        Array.Copy(res, 0, newRes, 1, res.Length);
                        return newRes;
                    }
                }
            }
            return null;
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
            return SubFindRelationship(originals, fragments);
        }
    }
}
