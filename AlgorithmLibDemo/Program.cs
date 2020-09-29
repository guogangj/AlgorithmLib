using AlgorithmLib;
using System;

namespace AlgorithmLibDemo {
    class Program {

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

        static void Main(string[] args) {
            //PermutationDemo();
            CombinationDemo();
        }
    }
}
