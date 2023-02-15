using System;

namespace AlgorithmLibDemo;

class Utils {
    /// <summary>
    /// 数组内容的简单打印
    /// </summary>
    /// <param name="arr">数组</param>
    /// <param name="title">标题</param>
    public static void SimplePrintArray<T>(T[] arr, string title = null) {
        Console.Write(title);
        Console.WriteLine(string.Join(", ", arr));
    }
}
