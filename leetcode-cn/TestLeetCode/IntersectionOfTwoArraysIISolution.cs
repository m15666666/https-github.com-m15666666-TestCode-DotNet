/*
给定两个数组，编写一个函数来计算它们的交集。

示例 1：

输入：nums1 = [1,2,2,1], nums2 = [2,2]
输出：[2,2]
示例 2:

输入：nums1 = [4,9,5], nums2 = [9,4,9,8,4]
输出：[4,9]

说明：

输出结果中每个元素出现的次数，应与元素在两个数组中出现次数的最小值一致。
我们可以不考虑输出结果的顺序。
进阶：

如果给定的数组已经排好序呢？你将如何优化你的算法？
如果 nums1 的大小比 nums2 小很多，哪种方法更优？
如果 nums2 的元素存储在磁盘上，内存是有限的，并且你不能一次加载所有的元素到内存中，你该怎么办？

*/

using System.Collections.Generic;

/// <summary>
/// https://leetcode-cn.com/problems/intersection-of-two-arrays-ii/
/// 350. 两个数组的交集II
///
///
///
/// </summary>
internal class IntersectionOfTwoArraysIISolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int[] Intersection(int[] nums1, int[] nums2)
    {
        if (nums2.Length < nums1.Length) (nums1, nums2) = (nums2, nums1);

        List<int> ret = new List<int>(nums1.Length);
        Dictionary<int, int> num2Count = new Dictionary<int, int>();
        foreach (var v in nums1)
            if (num2Count.ContainsKey(v)) ++num2Count[v];
            else num2Count[v] = 1;

        foreach (int v in nums2)
            if (num2Count.ContainsKey(v))
            {
                if (num2Count[v] == 1) num2Count.Remove(v);
                else --num2Count[v];

                ret.Add(v);
            }
        return ret.ToArray();
    }
}

/*

*/