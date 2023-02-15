using AlgorithmLib;
using System;
using System.Diagnostics;

namespace AlgorithmLibDemo;

class Demos {

    /// <summary>
    /// 排列demo
    /// </summary>
    public static void PermutationDemo() {
        int[] arr1 = new int[] { 1, 2, 3, 4 };
        Utils.SimplePrintArray(arr1, "\n排列1: ");
        int count = 0;
        PermutationHelper.Permutation(arr1, arr1.Length, arr1.Length, res => {
            Utils.SimplePrintArray(res);
            count++;
            return true;
        }, true);
        Console.WriteLine("总数: " + count);

        int[] arr2 = new int[] { 1, 2, 2, 2 };
        Utils.SimplePrintArray(arr2, "\n排列2: ");
        count = 0;
        PermutationHelper.Permutation(arr1, arr1.Length, arr1.Length, res => {
            Utils.SimplePrintArray(res);
            count++;
            return true;
        }, true);
        Console.WriteLine("总数: " + count);

        int[] arr3 = new[] { 16, 11, 13, 28, 28, 28, 28, 28, 28, 28, 28 };
        Utils.SimplePrintArray(arr3, "\n排列3: ");
        count = 0;
        PermutationHelper.Permutation(arr3, arr3.Length, arr3.Length, res => {
            Utils.SimplePrintArray(res);
            count++;
            return true;
        }, true);
        Console.WriteLine("总数: " + count);
    }

    /// <summary>
    /// 组合DEMO
    /// </summary>
    public static void CombinationDemo() {
        char[] arr = new char[] { 'a', 'a', 'a', 'd', 'e', 'f', 'g' };

        CombinationHelper.Method = CombinationMethod.FlagDrift;
        Stopwatch sw = new Stopwatch();
        sw.Start();
        CombinationHelper.Combination(arr, 3, res => {
            Utils.SimplePrintArray(res);
            return true;
        });
        sw.Stop();
        Console.WriteLine($"标志位选择法耗时: {sw.ElapsedMilliseconds} ms");

        CombinationHelper.Method = CombinationMethod.Backtrack;
        sw.Restart();
        CombinationHelper.Combination(arr, 3, res => {
            Utils.SimplePrintArray(res);
            return true;
        });
        sw.Stop();
        Console.WriteLine($"回溯法耗时: {sw.ElapsedMilliseconds} ms");
    }

    /// <summary>
    /// 数值比例分摊DEMO
    /// </summary>
    public static void NumberDivideDemo() {
        
        decimal toDivide = 100;
        decimal[] ratioArray = new[] { 1m, 1.2m };
        int decimalNum = 2;

        void PrintDivInfo() {
            Utils.SimplePrintArray(ratioArray, $"\n以此比例分摊 [{toDivide}] (小数位{decimalNum}): ");
        }
        PrintDivInfo();
        Utils.SimplePrintArray(NumberDivideHelper.Divide(toDivide, ratioArray, decimalNum));

        toDivide = 10;
        ratioArray = new[] { 1m, 0m };
        decimalNum = 2;
        PrintDivInfo();
        Utils.SimplePrintArray(NumberDivideHelper.Divide(toDivide, ratioArray, decimalNum));

        toDivide = 10;
        ratioArray = new[] { 3m, 3m, 3m };
        decimalNum = 2;
        PrintDivInfo();
        Utils.SimplePrintArray(NumberDivideHelper.Divide(toDivide, ratioArray, decimalNum));

        toDivide = 10;
        ratioArray = new[] { 3m, 3m, 3m};
        decimalNum = 0;
        PrintDivInfo();
        Utils.SimplePrintArray(NumberDivideHelper.Divide(toDivide, ratioArray, decimalNum));

        toDivide = 10;
        ratioArray = new[] { 5m, 1m, 0.1m };
        decimalNum = 0;
        PrintDivInfo();
        Utils.SimplePrintArray(NumberDivideHelper.Divide(toDivide, ratioArray, decimalNum));


        toDivide = 10.1m;
        ratioArray = new[] { 5m, 1m, 0.1m };
        decimalNum = 0;
        PrintDivInfo();
        try {
            Utils.SimplePrintArray(NumberDivideHelper.Divide(toDivide, ratioArray, decimalNum));
        }
        catch(ArgumentException ex) { 
            Console.WriteLine("出错了: " + ex.Message);
        }
    }


    /// <summary>
    /// 找出关系DEMO
    /// </summary>
    public static void FindRelationDemo() {
        int[] originals = new[] { 8, 13, 2 };
        int[] fragments = new[] { 7, 5, 3, 2, 6 };
        int[] res = RelationshipHelper.FindRelationship(originals, fragments);
        void PrintTestResult() {
            Utils.SimplePrintArray(originals, $"\n原始组: ");
            Utils.SimplePrintArray(fragments, $"碎片组: ");
            if(res!= null) {
                Utils.SimplePrintArray(res, $"结果: ");
            }
            else {
                Console.WriteLine("结果: (无结果)");
            }
        }
        PrintTestResult();

        originals = new[] { 23, 16, 4, 8, 15, 7 };
        fragments = new[] { 3, 3, 1, 7, 14, 8, 17, 1, 7, 4, 3, 1, 3, 1 };
        res = RelationshipHelper.FindRelationship(originals, fragments);
        PrintTestResult();

        originals = new[] { 3, 5 };
        fragments = new[] { 2, 4, 1, 1 };
        res = RelationshipHelper.FindRelationship(originals, fragments);
        PrintTestResult();

        originals = new[] { 7, 13, 6, 99, 24, 16, 35 };
        fragments = new[] { 11, 9, 50, 13, 3,6, 30, 11, 7, 5, 4, 11, 2, 5, 9, 24};
        res = RelationshipHelper.FindRelationship(originals, fragments);
        PrintTestResult();

        originals = new[] { 500, 100 };
        fragments = new[] { 16, 11, 13, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28 };
        res = RelationshipHelper.FindRelationship(originals, fragments);
        PrintTestResult();

        originals = new[] { 8, 7, 12 };
        fragments = new[] { 6, 4, 9, 3, 5 };
        res = RelationshipHelper.FindRelationship(originals, fragments);
        PrintTestResult();
    }

    /// <summary>
    /// 找出构成整体的碎片集DEMO
    /// </summary>
    public static void FindFragmentsDemo() {
        int whole = 4;
        int[] fragments = new int[] { 5, 2, 7, 1, 2, 3 };
        int[] res = RelationshipHelper.FindFragments(whole, fragments);
        void PrintTestResult() {
            Console.WriteLine($"\n整体: {whole}");
            Utils.SimplePrintArray(fragments, $"碎片: ");
            if (res != null) {
                Utils.SimplePrintArray(res, $"结果: ");
            }
            else {
                Console.WriteLine("结果: (无结果)");
            }
        }
        PrintTestResult();

        whole = 17;
        fragments = new int[] { 4, 4, 5, 5, 8, 2, 1, 9 };
        res = RelationshipHelper.FindFragments(whole, fragments);
        PrintTestResult();

        whole = 12;
        fragments = new int[] { 7, 6, 2, 9, 88, 7 };
        res = RelationshipHelper.FindFragments(whole, fragments);
        PrintTestResult();
    }

    /// <summary>
    /// 洗牌，随机取值DEMO
    /// </summary>
    public static void ShuffleDemo() {
        //洗牌10次
        Console.WriteLine("\n对[1,2,3,4,5,6,7,8,9]数组进行洗牌10次");
        for (int i = 0; i < 10; i++) {
            char[] arr = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            ShuffleHelper.Shuffle(arr);
            Utils.SimplePrintArray(arr, "结果: ");
        }


        //随机取下标
        Console.WriteLine("\n随机取数组下标10次,只需要告知数组长度(10)和要取的下标数(5),无需提供数组对象");
        for (int i = 0; i < 10; i++) {
            int[] arr = ShuffleHelper.RandomGet(10, 5);
            Utils.SimplePrintArray(arr, "下标: ");
        }

        //随机取值
        char[] arrAlpha = { 'a', 'b', 'c', 'd', 'e', 'f', 'g' };
        Utils.SimplePrintArray(arrAlpha, "\n随机取数组中的3个元素,10次: ");
        for (int i = 0; i < 10; i++) {
            char[] arr = ShuffleHelper.RandomGet(arrAlpha, 3);
            Utils.SimplePrintArray(arr, "结果: ");
        }
    }

    /// <summary>
    /// 数组元素位移DEMO
    /// </summary>
    public static void ShiftArrayItemsDemo() {

        char[] arr = new char[10];
        void ResetArray() {
            for (int i = 0; i < 10; i++) {
                arr[i] = (char)('0' + i);
            }
        }

        ResetArray();
        Utils.SimplePrintArray(arr, "针对此数组进行一些位移操作(会改变数组本身): ");

        int index = 0;
        int length = 0;
        int offset = 0;

        void PrintTestInfo(string info) {
            Console.WriteLine($"\n子数组起始位置[{index}], 子数组长度[{length}], 子数组位移量[{offset}]");
            Utils.SimplePrintArray(arr, info);
        }

        ResetArray();
        ArrayHelper.ShiftArrayItems(arr, index, length, offset);
        PrintTestInfo("什么都不变：");

        index = 0;
        length = 1;
        offset = 1;
        ResetArray();
        ArrayHelper.ShiftArrayItems(arr, index, length, offset);
        PrintTestInfo("0元素右移1：");

        index = 1;
        length = 3;
        offset = 2;
        ResetArray();
        ArrayHelper.ShiftArrayItems(arr, index, length, offset);
        PrintTestInfo("123元素右移2：");


        index = 7;
        length = 3;
        offset = -5;
        ResetArray();
        ArrayHelper.ShiftArrayItems(arr, index, length, offset);
        PrintTestInfo("1789元素左移5：");

        index = 0;
        length = 9;
        offset = 1;
        ResetArray();
        ArrayHelper.ShiftArrayItems(arr, index, length, offset);
        PrintTestInfo("0-8元素右移1：");

        //越界测试1
        try {
            ResetArray();
            ArrayHelper.ShiftArrayItems(arr, 7, 3, -8);
        }
        catch (IndexOutOfRangeException e) {
            Console.WriteLine("\n左移越界测试：" + e.Message);
        }

        //越界测试2
        try {
            ResetArray();
            ArrayHelper.ShiftArrayItems(arr, 7, 4, -2);
        }
        catch (IndexOutOfRangeException e) {
            Console.WriteLine("\n子数组长度越界：" + e.Message);
        }

        //越界测试3
        try {
            ResetArray();
            ArrayHelper.ShiftArrayItems(arr, -1, 3, 2);
        }
        catch (IndexOutOfRangeException e) {
            Console.WriteLine("\n子数组起始位置越界：" + e.Message);
        }
    }
}

