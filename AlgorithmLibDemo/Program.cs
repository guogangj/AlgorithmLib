using AlgorithmLib;
using System;
using System.Diagnostics;
using System.Reflection;

namespace AlgorithmLibDemo;
class Program {
    delegate void DemoFun();

    class DemoItem {
        public string FunStr => Fun?.GetMethodInfo()?.Name ?? "(Unknown)";
        public DemoFun Fun { get; set; }
        public string Desc { get; set; }
    }


    static void Main(string[] args) {

        DemoItem[] demoItems = new DemoItem[] {
            new DemoItem {Fun = Demos.PermutationDemo, Desc = "排列DEMO"},
            new DemoItem {Fun = Demos.CombinationDemo, Desc = "组合DEMO"},
            new DemoItem {Fun = Demos.NumberDivideDemo, Desc = "数值比例分摊DEMO"},
            new DemoItem {Fun = Demos.FindRelationDemo, Desc = "找出关系DEMO"},
            new DemoItem {Fun = Demos.FindFragmentsDemo, Desc = "找出构成整体的碎片集DEMO"},
            new DemoItem {Fun = Demos.FindFragmentsDemo2, Desc = "找出构成整体的碎片集DEMO(对象处理)"},
            new DemoItem {Fun = Demos.ShuffleDemo, Desc = "洗牌，随机取值DEMO"},
            new DemoItem {Fun = Demos.ShiftArrayItemsDemo, Desc = "数组元素位移DEMO"}
        };

        void PrintMenu() {
            Console.WriteLine("Please choose the DEMO you want to run:");
            for (int i = 1; i <= demoItems.Length; i++) {
                Console.WriteLine("{0,5} - {1,-30}{2}", i, demoItems[i - 1].FunStr, demoItems[i - 1].Desc);
            }
            Console.Write("Input the No. or 'q' to quit: ");
        }

        Console.WriteLine("********************* AlgorithmLib DEMOs *********************");

        TryAgainFlag:
        PrintMenu();
        string actionStr = Console.ReadLine();
        if (actionStr?.Trim() == "q") {
            return;
        }
        if (int.TryParse(actionStr, out int actionId) && actionId >= 1 && actionId <= demoItems.Length) {
            DemoItem demoItem = demoItems[actionId - 1];
            Console.WriteLine("Execute: ...");
            demoItem.Fun();
            Console.Write("\nDone! Press any key to next try.");
            Console.ReadKey();
        }
        else {
            Console.WriteLine("Incorrect. Try again.");
        }
        Console.WriteLine();
        goto TryAgainFlag;
    }
}
