using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
我们有一个非负整数数组 A。

对于每个（连续的）子数组 B = [A[i], A[i+1], ..., A[j]] （ i <= j），我们对 B 中的每个元素进行按位或操作，获得结果 A[i] | A[i+1] | ... | A[j]。

返回可能结果的数量。 （多次出现的结果在最终答案中仅计算一次。）

 

示例 1：

输入：[0]
输出：1
解释：
只有一个可能的结果 0 。
示例 2：

输入：[1,1,2]
输出：3
解释：
可能的子数组为 [1]，[1]，[2]，[1, 1]，[1, 2]，[1, 1, 2]。
产生的结果为 1，1，2，1，3，3 。
有三个唯一值，所以答案是 3 。
示例 3：

输入：[1,2,4]
输出：6
解释：
可能的结果是 1，2，3，4，6，以及 7 。
 

提示：

1 <= A.length <= 50000
0 <= A[i] <= 10^9
在真实的面试中遇到过这道题？
*/
/// <summary>
/// https://leetcode-cn.com/problems/bitwise-ors-of-subarrays/
/// 898. 子数组按位或操作
/// 
/// </summary>
class BitwiseOrsOfSubarraysSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int SubarrayBitwiseORs(int[] A)
    {
        var ret = new HashSet<int>();
        var current = new HashSet<int>();
        var current2 = new HashSet<int>();

        foreach ( var v in A)
        {
            foreach ( var item in current)
            {
                var newV = item | v;
                if (!current2.Contains(newV)) current2.Add(newV);
            }
            if (!current2.Contains(v)) current2.Add(v);

            foreach (var item in current2) if(!ret.Contains(item)) ret.Add(item);

            (current, current2) = (current2,current);
            current2.Clear();
        }

        return ret.Count;
    }
}