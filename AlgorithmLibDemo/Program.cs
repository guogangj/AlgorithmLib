using AlgorithmLib;
using System;
using System.Diagnostics;

namespace AlgorithmLibDemo {
    class Program {

        static void SimplePrintArray<T>(T[] arr) {
            Console.WriteLine(string.Join(", ", arr));
        }

        //组合demo
        static void CombinationDemo() {
            char[] arr = new char[] {'a', 'b', 'c', 'd', 'e', 'f', 'g'};

            CombinationHelper.Method = CombinationMethod.FlagDrift;
            Stopwatch sw = new Stopwatch();
            sw.Start();
            CombinationHelper.Combination(arr, 3, res => {
                SimplePrintArray(res);
                return true;
            });
            sw.Stop();
            Console.WriteLine($"标志位选择法耗时: {sw.ElapsedMilliseconds} ms");

            CombinationHelper.Method = CombinationMethod.Backtrack;
            sw.Restart();
            CombinationHelper.Combination(arr, 3, res => {
                SimplePrintArray(res);
                return true;
            });
            sw.Stop();
            Console.WriteLine($"回溯法耗时: {sw.ElapsedMilliseconds} ms");
        }

        //排列demo
        static void PermutationDemo() {
            int[] arr = new int[] { 1, 2, 3, 4 };
            PermutationHelper.Permutation(arr, 2, 4, res => {
                SimplePrintArray(res);
                return true;
            });
        }

        //数值分摊demo
        static void NumberDivideDemo() {
            SimplePrintArray(NumberDivideHelper.Divide(100, new[] { 1m, 1.2m }, 2));
            SimplePrintArray(NumberDivideHelper.Divide(10, new[] { 1m, 0m }, 2));
            SimplePrintArray(NumberDivideHelper.Divide(10, new[] { 3m, 3m, 3m }, 2));
            SimplePrintArray(NumberDivideHelper.Divide(10, new[] { 3m, 3m, 3m }, 0));
            SimplePrintArray(NumberDivideHelper.Divide(10, new[] { 5m, 1m, 0.1m }, 0));
            SimplePrintArray(NumberDivideHelper.Divide(10.1m, new[] { 5m, 1m, 0.1m }, 1));
        }

        //查找关系DEMO
        static void FindRelationDemo() {
            //int[] originals = new[] { 8, 13, 2 };
            //int[] fragments = new[] { 7, 5, 3, 2, 6 };
            //int[] originals = new[] { 23, 16, 4, 8, 15, 7 };
            //int[] fragments = new[] { 3, 3, 1, 7, 14, 8, 17, 1, 7, 4, 3, 1, 3, 1 };
            int[] originals = { 3, 5 };
            int[] fragments = { 2, 4, 1, 1 };
            //int[] originals = new[] { 7, 13, 6, 99, 24, 16, 35 };
            //int[] fragments = new[] { 11, 9, 50, 13, 3,6, 30, 11, 7, 5, 4, 11, 2, 5, 9, 24};
            int[] res = RelationshipHelper.FindRelationship(originals, fragments);
            if (res != null) {
                SimplePrintArray(res);
            }
        }

        //洗牌，随机取值demo
        static void ShuffleDemo() {
            //洗牌10次
            for(int i=0; i<10; i++) {
                char[] arr = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
                ShuffleHelper.Shuffle(arr);
                SimplePrintArray(arr);
            }

            //随机取下标
            for (int i = 0; i < 10; i++) {
                int[] arr = ShuffleHelper.RandomGet(10, 5);
                SimplePrintArray(arr);
            }

            //随机取值
            char[] arrAlpha = { 'a', 'b', 'c', 'd', 'e', 'f', 'g' };
            for (int i = 0; i < 10; i++) {
                char[] arr = ShuffleHelper.RandomGet(arrAlpha, 3);
                SimplePrintArray(arr);
            }

            //边界测试
            char[] res = ShuffleHelper.RandomGet(arrAlpha, 0);
            Debug.Assert(res.Length==0);
            res = ShuffleHelper.RandomGet(arrAlpha, 7);
            SimplePrintArray(res);
        }

        static void Main(string[] args) {
            int a = 1;
            int b = 2;
            (a, b) = (b, a);
            Console.WriteLine(a + ", " + b);
            //PermutationDemo();
            //CombinationDemo();
            //NumberDivideDemo();
            //FindRelationDemo();
            ShuffleDemo();
        }
    }
}
