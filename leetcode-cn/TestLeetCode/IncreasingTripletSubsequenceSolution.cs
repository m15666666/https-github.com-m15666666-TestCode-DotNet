using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
给定一个未排序的数组，判断这个数组中是否存在长度为 3 的递增子序列。

数学表达式如下:

如果存在这样的 i, j, k,  且满足 0 ≤ i < j < k ≤ n-1，
使得 arr[i] < arr[j] < arr[k] ，返回 true ; 否则返回 false 。
说明: 要求算法的时间复杂度为 O(n)，空间复杂度为 O(1) 。

示例 1:

输入: [1,2,3,4,5]
输出: true
示例 2:

输入: [5,4,3,2,1]
输出: false
*/
/// <summary>
/// https://leetcode-cn.com/problems/increasing-triplet-subsequence/
/// 334. 递增的三元子序列
/// </summary>
class IncreasingTripletSubsequenceSolution
{
    public void Test()
    {
        var ret = IncreasingTriplet(new int[] { 2, 4, -2, -3 });
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public bool IncreasingTriplet(int[] nums)
    {
        if (nums == null || nums.Length < 3) return false;

        int[] vs = new int[2];
        int upperIndex = 0;
        vs[0] = nums[0];
        for (int i = 1; i < nums.Length; i++)
        {
            var v = nums[i];
            if (vs[upperIndex] < v)
            {
                if (upperIndex == 1) return true;
                vs[++upperIndex] = v;
                continue;
            }
            for (int j = 0; j <= upperIndex; j++)
            {
                if (v <= vs[j])
                {
                    vs[j] = v;
                    break;
                }
            }
        }
        return false;
    }
}
/*
public class Solution {
    public bool IncreasingTriplet(int[] nums) {
        int first = int.MaxValue;
        int second = int.MaxValue;
        if (nums.Length < 3) return false;
        for (int i = 0; i < nums.Length; i++) {
            if (first > nums[i]) {
                first = nums[i];
            } else if (nums[i] > first && nums[i] < second) {
                second = nums[i];
            } else if (nums[i] > second) {
                return true;
            }
        }
        return false;
    }
}
public bool IncreasingTriplet(int[] nums)
{
    if (nums == null || nums.Length < 3) return false;
    Queue<int> queue = new Queue<int>();
    queue.Enqueue(0);

    int first = int.MaxValue;
    int second = int.MaxValue;
    int lastQueueV = int.MaxValue;
    while( 0 < queue.Count)
    {
        var index = queue.Dequeue();

        var firstIndex = index;
        var secondIndex = index;
        first = nums[firstIndex];
        for ( int i = index + 1; i < nums.Length; i++ )
        {
            var v = nums[i];
            if (v == first) continue;
            if ( v < first)
            {
                first = v;
                firstIndex = i;
                continue;
            }

            second = v;
            secondIndex = i;
            break;
        }

        if (secondIndex == index) return false;

        for (int i = secondIndex + 1; i < nums.Length; i++)
        {
            var v = nums[i];
            if (second < v) return true;

            if (second == v) continue;
                
            if (first < v)
            {
                second = v;
                continue;
            }

            if (first == v) continue;

            if (v < lastQueueV)
            {
                lastQueueV = v;
                queue.Enqueue(i);
            }
        }
    }

    return false;
} 
     
*/
