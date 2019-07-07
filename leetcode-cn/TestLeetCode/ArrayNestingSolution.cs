using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
索引从0开始长度为N的数组A，包含0到N - 1的所有整数。找到并返回最大的集合S，S[i] = {A[i], A[A[i]], A[A[A[i]]], ... }且遵守以下的规则。

假设选择索引为i的元素A[i]为S的第一个元素，S的下一个元素应该是A[A[i]]，之后是A[A[A[i]]]... 以此类推，不断添加直到S出现重复的元素。

示例 1:

输入: A = [5,4,0,3,1,6,2]
输出: 4
解释: 
A[0] = 5, A[1] = 4, A[2] = 0, A[3] = 3, A[4] = 1, A[5] = 6, A[6] = 2.

其中一种最长的 S[K]:
S[0] = {A[0], A[5], A[6], A[2]} = {5, 6, 2, 0}
注意:

N是[1, 20,000]之间的整数。
A中不含有重复的元素。
A中的元素大小在[0, N-1]之间。 
*/
/// <summary>
/// https://leetcode-cn.com/problems/array-nesting/
/// 565. 数组嵌套
/// https://blog.csdn.net/zrh_CSDN/article/details/85334445
/// </summary>
class ArrayNestingSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int ArrayNesting(int[] nums)
    {
        int n = nums.Length;
        int ret = 0;
        bool[] visited = new bool [n];
        Array.Fill(visited, false);
        for (int i = 0; i < n; ++i)
        {
            if (visited[nums[i]]) continue;
            int count = 0;
            int j = i;
            while (count == 0 || j != i)
            {
                visited[j] = true;
                j = nums[j];
                ++count;
            }
            if (ret < count) ret = count;
        }
        return ret;
    }
}