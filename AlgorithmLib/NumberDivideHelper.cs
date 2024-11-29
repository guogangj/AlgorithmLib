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

            decimal[] result = new decimal[partNum]; //要返回的结果（可能需要舍入）


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

            decimal[] rawResult = new decimal[partNum]; //未经舍入的原始分摊结果

            //尝试执行分摊
            for (int i = 0; i < partNum; i++) {
                rawResult[i] = total * scale[i] / scaleSum;
                if (decimalNum != -1) {

                    result[i] = decimal.Round(rawResult[i], decimalNum);
                }
                else {
                    result[i] = rawResult[i];
                }
            }

            decimal totalRes = result.Sum();
            decimal totalDiff = total - totalRes;
            if (totalDiff == 0m) {
                return result;
            }

            if (decimalNum == -1) {
                //如果小数位不限，产生的误差就算有也极小，直接把误差值算到差距最大的一个值上即可
                int maxDiffIdx = 0;
                for (int i = 1; i < partNum; i++) {
                    if (Math.Abs(result[i] - rawResult[i]) > Math.Abs(result[maxDiffIdx] - rawResult[maxDiffIdx])) {
                        maxDiffIdx = i;
                    }
                }
                result[maxDiffIdx] += totalDiff;
            }
            else {
                //平掉由于舍入引起的误差
                //用差值来排个序，准备从差值大的开始调整
                var sortedDiffs = totalDiff > 0
                    ? rawResult.Select((r, i) => new { Index = i, Residual = r - result[i] }).ToArray().OrderByDescending(x => x.Residual).ToArray()
                    : rawResult.Select((r, i) => new { Index = i, Residual = r - result[i] }).ToArray().OrderBy(x => x.Residual).ToArray();

                //最小调整单元，根据小数位决定
                decimal unit = 1m / (decimalNum >= 0 ? (decimal)Math.Pow(10, decimalNum) : 1m);

                int idx = 0;
                while (totalDiff != 0) {
                    int index = sortedDiffs[idx].Index;
                    decimal adjustment = totalDiff > 0 ? unit : -unit;
                    if (result[index] + adjustment >= 0) {
                        result[index] += adjustment;
                        totalDiff -= adjustment;
                    }
                    idx = (idx + 1) % sortedDiffs.Length; //按道理应该不会出现idx比数组长度大的情形，这里的取模应该是不需要的
                }
            }
            return result;
        }
    }
}
