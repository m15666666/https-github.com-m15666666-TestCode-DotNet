using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个正整数数组 w ，其中 w[i] 代表位置 i 的权重，请写一个函数 pickIndex ，它可以随机地获取位置 i，选取位置 i 的概率与 w[i] 成正比。

说明:

1 <= w.length <= 10000
1 <= w[i] <= 10^5
pickIndex 将被调用不超过 10000 次
示例1:

输入: 
["Solution","pickIndex"]
[[[1]],[]]
输出: [null,0]
示例2:

输入: 
["Solution","pickIndex","pickIndex","pickIndex","pickIndex","pickIndex"]
[[[1,3]],[],[],[],[],[]]
输出: [null,0,1,1,1,0]
输入语法说明：

输入是两个列表：调用成员函数名和调用的参数。Solution 的构造函数有一个参数，即数组 w。pickIndex 没有参数。输入参数是一个列表，即使参数为空，也会输入一个 [] 空列表。
*/
/// <summary>
/// https://leetcode-cn.com/problems/random-pick-with-weight/
/// 528. 按权重随机选择
/// https://blog.csdn.net/qq_35923749/article/details/89214611
/// </summary>
class RandomPickWithWeightSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public RandomPickWithWeightSolution(int[] w)
    {
        _sums = null;
        if (w == null || w.Length == 0) return;

        _sums = new int[w.Length];
        _sums[0] = w[0];
        for (int i = 1; i < w.Length; i++)
        {
            _sums[i] = _sums[i-1] + w[i];
        }
    }

    private Random _random = new Random();
    private int[] _sums;
    public int PickIndex()
    {
        if (_sums == null) return -1;
        int value = _random.Next(_sums[_sums.Length-1] + 1);
        for (int i = 0; i < _sums.Length; i++)
            if (value <= _sums[i]) return i;
        return _sums.Length - 1;

        //int left = 0;
        //int right = _sums.Length - 1;
        //while (left < right)
        //{
        //    int mid = (left + right) / 2;
        //    if (_sums[mid] == value) return mid;
        //    else if (_sums[mid] > value) right = mid;
        //    else left = mid + 1;
        //}
        //return left;
    }
}