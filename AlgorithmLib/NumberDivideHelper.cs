using System;
using System.Linq;

namespace AlgorithmLib {
    /// <summary>
    /// 数值分摊算法类
    /// 提供数值分摊的方法
    /// </summary>
    public static class NumberDivideHelper {
        /// <summary>
        /// 分摊
        /// </summary>
        /// <param name="total">总值</param>
        /// <param name="scale">比例</param>
        /// <param name="decimalNum">小数位数(0-6)，默认-1表示不限</param>
        /// <returns></returns>
        public static decimal[] Divide(decimal total, decimal[] scale, int decimalNum = -1) {
            if (decimalNum != -1 && decimal.Round(total, decimalNum) != total) {
                throw new ArgumentException("Cannot divide the number due to its decimal number is more than specified value");
            }
            int partNum = scale.Length;
            decimal[] result = new decimal[partNum];
            if (partNum == 0) {
                return result;
            }

            if (scale.Any(d => d < 0m)) {
                throw new ArgumentException("Divide scale cannot be less than 0");
            }
            decimal scaleSum = scale.Sum();
            if (scaleSum == 0m) {
                throw new ArgumentException("Divide total scale cannot be equal to 0");
            }

            if (partNum == 1) {
                result[0] = total;
                return result;
            }
            if (decimalNum > 6) {
                decimalNum = 6;
            }
            if (decimalNum < -1) {
                decimalNum = -1;
            }

            //尝试执行分摊
            for (int i = 0; i < partNum; i++) {
                result[i] = total * scale[i] / scaleSum;

                if (decimalNum != -1) {

                    result[i] = decimal.Round(result[i], decimalNum);
                }
            }

            decimal totalTest = result.Sum();
            decimal remainder = total - totalTest;
            if (remainder == 0m) {
                return result;
            }

            //摊到最大的上面
            int maxIdx = 0;
            for (int i = 1; i < result.Length; i++) {
                if (result[i] > result[maxIdx]) {
                    maxIdx = i;
                }
            }
            result[maxIdx] += remainder;

            return result;
        }
    }
}
