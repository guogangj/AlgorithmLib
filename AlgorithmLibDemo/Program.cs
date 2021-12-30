using AlgorithmLib;
using System;

namespace AlgorithmLibDemo {
    class Program {

        static void SimplePrintArray<T>(T[] arr) {
            Console.WriteLine(string.Join(", ", arr));
        }

        //组合demo
        static void CombinationDemo() {
            char[] arr = new char[] { 'a', 'b', 'c', 'd' };
            CombinationHelper.Combination1(arr, 2, 3, res => {
                foreach (char c in res) {
                    Console.Write(c + " ");
                }
                Console.WriteLine();
            });
        }

        //排列demo
        static void PermutationDemo() {
            char[] arr = new char[] { 'a', 'b', 'c', 'd' };
            PermutationHelper.Permutation1(arr, 2, 3, res => {
                foreach (char c in res) {
                    Console.Write(c + " ");
                }
                Console.WriteLine();
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
            int[] originals = new[] { 23, 16, 4, 8, 15, 7 };
            int[] fragments = new[] { 3, 3, 1, 7, 14, 8, 17, 1, 7, 4, 3, 1, 3, 1 };
            int[] res = RelationshipHelper.FindRelationship(originals, fragments);
            if (res != null) {
                foreach (int i in res) {
                    Console.Write(" " + i);
                }
            }
        }

        static void Main(string[] args) {
            //PermutationDemo();
            //CombinationDemo();
            //NumberDivideDemo();
            FindRelationDemo();
        }
    }
}
