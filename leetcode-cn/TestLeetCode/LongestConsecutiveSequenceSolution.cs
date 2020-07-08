using System.Collections.Generic;

/*
给定一个未排序的整数数组，找出最长连续序列的长度。

要求算法的时间复杂度为 O(n)。

示例:

输入: [100, 4, 200, 1, 3, 2]
输出: 4
解释: 最长连续序列是 [1, 2, 3, 4]。它的长度为 4。

*/

/// <summary>
/// https://leetcode-cn.com/problems/longest-consecutive-sequence/
/// 128. 最长连续序列
///
///
///
/// </summary>
internal class LongestConsecutiveSequenceSolution
{
    public void Test()
    {
        int[] nums = new int[] {100,4,200,1,3,2};
        var ret = LongestConsecutive(nums);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int LongestConsecutive(int[] nums)
    {
        if (nums == null || nums.Length == 0) return 0;

        HashSet<int> set = new HashSet<int>(nums);
        int ret = 1;
        foreach (int num in nums)
        {
            if (set.Contains(num - 1)) continue;

            int count = 1;
            while (set.Contains(num + count)) count += 1;

            if (ret < count) ret = count;
        }
        return ret;
    }
}

/*

最长连续序列
力扣官方题解
发布于 2020-06-05
22.6k
方法一：哈希表
思路和算法

我们考虑枚举数组中的每个数 xx，考虑以其为起点，不断尝试匹配 x+1, x+2, \cdotsx+1,x+2,⋯ 是否存在，假设最长匹配到了 x+yx+y，那么以 xx 为起点的最长连续序列即为 x, x+1, x+2, \cdots, x+yx,x+1,x+2,⋯,x+y，其长度为 y+1y+1，我们不断枚举并更新答案即可。

对于匹配的过程，暴力的方法是 O(n)O(n) 遍历数组去看是否存在这个数，但其实更高效的方法是用一个哈希表存储数组中的数，这样查看一个数是否存在即能优化至 O(1)O(1) 的时间复杂度。

仅仅是这样我们的算法时间复杂度最坏情况下还是会达到 O(n^2)O(n 
2
 )（即外层需要枚举 O(n)O(n) 个数，内层需要暴力匹配 O(n)O(n) 次），无法满足题目的要求。但仔细分析这个过程，我们会发现其中执行了很多不必要的枚举，如果已知有一个 x, x+1, x+2, \cdots, x+yx,x+1,x+2,⋯,x+y 的连续序列，而我们却重新从 x+1x+1，x+2x+2 或者是 x+yx+y 处开始尝试匹配，那么得到的结果肯定不会优于枚举 xx 为起点的答案，因此我们在外层循环的时候碰到这种情况跳过即可。

那么怎么判断是否跳过呢？由于我们要枚举的数 xx 一定是在数组中不存在前驱数 x-1x−1 的，不然按照上面的分析我们会从 x-1x−1 开始尝试匹配，因此我们每次在哈希表中检查是否存在 x-1x−1 即能判断是否需要跳过了。



增加了判断跳过的逻辑之后，时间复杂度是多少呢？外层循环需要 O(n)O(n) 的时间复杂度，只有当一个数是连续序列的第一个数的情况下才会进入内层循环，然后在内层循环中匹配连续序列中的数，因此数组中的每个数只会进入内层循环一次。根据上述分析可知，总时间复杂度为 O(n)O(n)，符合题目要求。


class Solution {
    public int longestConsecutive(int[] nums) {
        Set<Integer> num_set = new HashSet<Integer>();
        for (int num : nums) {
            num_set.add(num);
        }

        int longestStreak = 0;

        for (int num : num_set) {
            if (!num_set.contains(num - 1)) {
                int currentNum = num;
                int currentStreak = 1;

                while (num_set.contains(currentNum + 1)) {
                    currentNum += 1;
                    currentStreak += 1;
                }

                longestStreak = Math.max(longestStreak, currentStreak);
            }
        }

        return longestStreak;
    }
}
复杂度分析

时间复杂度：O(n)O(n)，其中 nn 为数组的长度。具体分析已在上面正文中给出。

空间复杂度：O(n)O(n)。哈希表存储数组中所有的数需要 O(n)O(n) 的空间。

下一篇：【动态规划】Python 题解

public class Solution {
    public int LongestConsecutive(int[] nums) {
		HashSet<int> hashSet = new HashSet<int>();
		for (int i = 0; i < nums.Length; i++)
		{
			if (hashSet.Contains(nums[i]))
			{
				continue;
			}

			hashSet.Add(nums[i]);
		}

		int result = 0;
					for (int i = 0; i < nums.Length; i++)
		{
			var temp = nums[i];
			int resultTemp = 0;
			if (!hashSet.Contains(temp + 1))
			{
				while (hashSet.Contains(temp))
				{
					temp--;
					resultTemp++;
				}
			}

			if (result < resultTemp)
			{
				result = resultTemp;
			}
		}

		return result;
	}
}

public partial class Solution
{
    public int LongestConsecutive(int[] nums)
    {
        Dictionary<int, bool> numsDic = new Dictionary<int, bool>();
        int maxLength = 0;

        for (int i = 0; i < nums.Length; i++)
        {
            if (!numsDic.ContainsKey(nums[i]))
            {
                numsDic.Add(nums[i], false);
            }
        }

        for (int i = 0; i < nums.Length; i++)
        {
            //Skip when calculated
            if (numsDic[nums[i]]) continue;

            int num = nums[i];
            int length = 1;
            bool loop = true;

            //负方向
            while (loop)
            {
                if (numsDic.ContainsKey(num - 1))
                {
                    length++;
                    num--;
                    numsDic[num] = true;
                }
                else
                    loop = false;
            }

            num = nums[i];
            loop = true;

            //正方向
            while (loop)
            {
                if (numsDic.ContainsKey(num + 1))
                {
                    length++;
                    num++;
                    numsDic[num] = true;
                }
                else loop = false;
            }

            if (length > maxLength) maxLength = length;
        }

        return maxLength;
    }
}

动态规划】Python 题解
江不知
发布于 2019-05-07
6.5k
解题思路：
题目要求 O(n)O(n) 复杂度。

用哈希表存储每个端点值对应连续区间的长度
若数已在哈希表中：跳过不做处理
若是新数加入：
取出其左右相邻数已有的连续区间长度 left 和 right
计算当前数的区间长度为：cur_length = left + right + 1
根据 cur_length 更新最大长度 max_length 的值
更新区间两端点的长度值

class Solution(object):
    def longestConsecutive(self, nums):
        hash_dict = dict()
        
        max_length = 0
        for num in nums:
            if num not in hash_dict:
                left = hash_dict.get(num - 1, 0)
                right = hash_dict.get(num + 1, 0)
                
                cur_length = 1 + left + right
                if cur_length > max_length:
                    max_length = cur_length
                
                hash_dict[num] = cur_length
                hash_dict[num - left] = cur_length
                hash_dict[num + right] = cur_length
                
        return max_length

*/
