# AlgorithmLib
这个是我的一些常用的算法的帮助类库。目前的算法有：排列、组合、数值分摊、计算组合关系等。

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

## 数值分摊
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

## 计算组合关系
在企业开发中，常常遇到这样的问题，把原先的一些记录(原始)拆分成若干更多的记录(碎片)，而我们要建立碎片和原始之间的对应关系，例如给出"原始"：```8,13,2```, 给出"碎片：```7,5,3,2,6```, 我们需要获得的结果是```5,3,6,7,2```, 因为```5+3=8, 6+7=13, 2=2```

```
int[] originals = new[] { 3, 5 };
int[] fragments = new[] { 2, 4, 1, 1 };
int[] res = RelationshipHelper.FindRelationship(originals, fragments);
// res为 [2, 1, 1, 4]
```
