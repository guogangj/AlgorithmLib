# AlgorithmLib
AlgorithmLib是一个包含了我的一些常用的算法的基于```.NET Standard 2.0```的帮助类库。目前包含的算法有：排列、组合、数值分摊、计算组合关系等。

## 使用
1. nuget包引用
```
Install-Package AlgorithmLib
```
2. 用之...
## 排列(Permutation)
一般地，从n个不同元素中取出m（m≤n）个元素，按照一定的顺序排成一列，叫做从n个元素中取出m个元素的一个排列。特别地，当m=n时，这个排列被称作全排列。
### 全排列(All Permutation)
```
int[] arr = new int[] { 1, 2, 3 };
PermutationHelper.Permutation(arr, res => {
    // ...
    return true;
});
```
res依次是：
```
1
2
3
1 2
1 3
2 1
2 3
3 2
3 1
1 2 3
1 3 2
2 1 3
2 3 1
3 2 1
3 1 2
```
### 指定排列结果元素个数
```
int[] arr = new int[] { 1, 2, 3 };
PermutationHelper.Permutation(arr, 2, res => {
    // ...
    return true;
});
```
res依次是：
```
1 2
1 3
2 1
2 3
3 2
3 1
```
### 指定排列结果元素个数范围
```
int[] arr = new int[] { 1, 2, 3, 4 };
PermutationHelper.Permutation(arr, 2, 4, res => {
    // ...
    return true;
});
```
res依次是：
```
1 2
1 3
...
4 3
4 1
1 2 3
1 2 4
...
4 1 3
4 1 2
1 2 3 4
1 2 4 3
...
4 1 3 2
4 1 2 3
```
### 中止
可以在回调函数中返回false来中止排列的执行：
```
int[] arr = new int[] { 1, 2, 3 };
PermutationHelper.Permutation(arr, res => {
    if(condition){
        return false;
    }
    return true;
});
```

## 组合(Combination)
从n个不同的元素中，任取m（m≤n）个元素为一组，叫作从n个不同元素中取出m个元素的一个组合。
### 指定取几个数
```
int[] arr = new int[] { 1, 2, 3 };
CombinationHelper.Combination(arr, 2, res => {
    // ...
    return true;
});
```
res依次是：
```
1, 2
1, 3
2, 3
```
### 指定取几个数到几个数
```
int[] arr = new int[] { 1, 2, 3, 4 };
CombinationHelper.Combination(arr, 2, 3, res => {
    SimplePrintArray(res);
    return true;
});
```
res依次是：
```
1, 2
1, 3
1, 4
2, 3
2, 4
3, 4
1, 2, 3
1, 2, 4
1, 3, 4
2, 3, 4
```
### 中止
与排列类似，在回调函数中返回false即可。
### 指定组合算法
目前提供了两种组合算法，分别是“标志选择法”和“回溯法”，默认是使用“标志选择法”，这种方法没用递归，而“回溯法'则用到了递归，两种方法效率差不多，在元素较多时，回溯法略快一些。下面是使用回溯法的例子：
```
CombinationHelper.Method = CombinationMethod.Backtrack;
int[] arr = new int[] { 1, 2, 3, 4 };
CombinationHelper.Combination(arr, 2, 3, res => {
    SimplePrintArray(res);
    return true;
});
```
res依次是：
```
3, 4
2, 4
2, 3
1, 4
1, 3
1, 2
2, 3, 4
1, 3, 4
1, 2, 4
1, 2, 3
```
请注意回溯法返回的组合的出现的先后次序与标志选择法正好是倒过来的。

## 数值比例分摊
就是把一个数字，按一定的比例，分成若干个数字，这若干个数字加起来，要严格等于原先的数字，这个算法主要是解决了小数运算的精度的问题，这在企业开发中用得非常多。下面是一些例子：
```
// 100按"1,1.2"比例分摊，保留两位小数
// 结果为：45.45, 54.55
NumberDivideHelper.Divide(100, new[] { 1m, 1.2m }, 2);

// 结果为：10, 0
NumberDivideHelper.Divide(10, new[] { 1m, 0m }, 2);

// 结果为：3.34, 3.33, 3.33
NumberDivideHelper.Divide(10, new[] { 3m, 3m, 3m }, 2);

// 结果为：4, 3, 3
NumberDivideHelper.Divide(10, new[] { 3m, 3m, 3m }, 0);

// 结果为：8, 2, 0
NumberDivideHelper.Divide(10, new[] { 5m, 1m, 0.1m }, 0);

// 结果为：8.3, 1.7, 0.2
NumberDivideHelper.Divide(10.1m, new[] { 5m, 1m, 0.1m }, 1);
```

## 组合关系

### 找出构成整体的碎片集

给出一个总数（整体），和一个数组（碎片），从这个数组中找到若干个数，使它们的和等于给出的总数。

```
int whole = 17;
int[] fragments = new int[] { 4, 4, 5, 5, 8, 2, 1, 9 };
int[] res = RelationshipHelper.FindFragments(whole, fragments);
// res为 [4, 4, 8, 1]
```

### 找出构成整体的碎片集（对象处理）

这是**找出构成整体的碎片集**的附加功能版本。在企业开发中，我们常常需要建立对应关系，即若干个碎片加起来等于整体，找到这种关系后把碎片对象的一个字段设置为整体的ID，以此建立对应关系。

```
/// <summary>
/// 明细对象（碎片）
/// </summary>
class BillLine {
    /// <summary>
    /// 明细ID
    /// </summary>
    public int LineId { get; set; }
    /// <summary>
    /// 明细值
    /// </summary>
    public int LineValue { get; set; }
    /// <summary>
    /// 所属主档ID（这是想要的结果）
    /// </summary>
    public int MasterId { get; set; }
}

/// <summary>
/// 主档对象（整体）
/// </summary>
class MasterBill {
    /// <summary>
    /// 主档ID
    /// </summary>
    public int MasterId { get; set; }
    /// <summary>
    /// 主档值
    /// </summary>
    public int MasterValue { get; set; }
}

BillLine[] detailList = new BillLine[] {
    new BillLine{LineId = 1001, LineValue = 16},
    new BillLine{LineId = 1002, LineValue = 11},
    new BillLine{LineId = 1003, LineValue = 13},
    new BillLine{LineId = 1008, LineValue = 28},
    new BillLine{LineId = 1009, LineValue = 28},
    new BillLine{LineId = 1010, LineValue = 28},
    new BillLine{LineId = 1011, LineValue = 28},
    new BillLine{LineId = 1012, LineValue = 28},
};

MasterBill[] masterList = new MasterBill[] {
    new MasterBill{MasterId = 2001, MasterValue = 80},
    new MasterBill{MasterId = 2002, MasterValue = 100}
};

RelationshipHelper.MakeRelationShip(
    masterList,                    //整体列表
    detailList,                    //碎片列表
    t1 => t1.MasterId,             //获取整体ID的方法
    t1 => t1.MasterValue,          //获取整体的值的方法
    t2 => t2.LineValue,            //获取碎片的值的方法
    (t2, id) => t2.MasterId = id); //将结果ID
```
结果：

整体
| ID | 值 |
|----|----|
|2001|80  |
|2002|100 |

碎片
| ID |值|关联整体ID|
|----|--|----|
|1001|16|2002|
|1002|11|2001|
|1003|13|2001|
|1008|28|2001|
|1009|28|2001|
|1010|28|2002|
|1011|28|2002|
|1012|28|2002|

### 找出多值组合关系

这其实是**找出构成整体的碎片集**的变化版本。在企业开发中，常常遇到这样的问题，把原先的一些记录(原始)拆分成若干更多的记录(碎片)，而我们要建立碎片和原始之间的对应关系，例如给出"原始"：```8,13,2```, 给出"碎片：```7,5,3,2,6```, 我们需要获得的结果是```5,3,6,7,2```, 因为```5+3=8, 6+7=13, 2=2```。

```
int[] originals = new[] { 3, 5 };
int[] fragments = new[] { 2, 4, 1, 1 };
int[] res = RelationshipHelper.FindRelationship(originals, fragments);
// res为 [2, 1, 1, 4]
```

如果组合关系匹配失败，将会返回null
## 洗牌和随机取值

### 洗牌（随机打乱一个数组）

```
char[] arr = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
ShuffleHelper.Shuffle(arr);
// 数组arr被随机打乱，如：'3', '0', '9', '5', '8', '6', '2', '1', '4', '7'
```

### 随机从一个数组中取出N个下标
这种方法无需提供数组对象本身
```
int[] arr = ShuffleHelper.RandomGet(10, 5);
// arr为0-9的随机下标，如：3, 6, 4, 9, 1
```


### 随机从一个数组中取出N个值
```
char[] arrAlpha = { 'a', 'b', 'c', 'd', 'e', 'f', 'g' };
char[] arr = ShuffleHelper.RandomGet(arrAlpha, 3);
// arr为arrAlpha中的随机的3个元素，如：'d', 'f', 'b'
```

## 数组位移

数组的位移就是直接根据需求挪动数组的元素。如数组```[1,2,3,4,5]```的```[2,3]```子数组向右挪1个位置，数组就变成了```[1,4,2,3,5]```。

注： 如果越界，会抛出```IndexOutOfRangeException```异常。
```
int[] arr = new int[]{1,2,3,4,5,6,7,8,9};
ArrayHelper.ShiftArrayItems(arr, 7, 3, -5); //-5表示左移5位
// arr为 [0, 1, 7, 8, 9, 2, 3, 4, 5, 6]
```