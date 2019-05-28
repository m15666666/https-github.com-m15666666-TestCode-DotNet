using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个整数序列：a1, a2, ..., an，一个132模式的子序列 ai, aj, ak 被定义为：当 i < j < k 时，ai < ak < aj。设计一个算法，当给定有 n 个数字的序列时，验证这个序列中是否含有132模式的子序列。

注意：n 的值小于15000。

示例1:

输入: [1, 2, 3, 4]

输出: False

解释: 序列中不存在132模式的子序列。
示例 2:

输入: [3, 1, 4, 2]

输出: True

解释: 序列中有 1 个132模式的子序列： [1, 4, 2].
示例 3:

输入: [-1, 3, 2, 0]

输出: True

解释: 序列中有 3 个132模式的的子序列: [-1, 3, 2], [-1, 3, 0] 和 [-1, 2, 0]. 
*/
/// <summary>
/// https://leetcode-cn.com/problems/132-pattern/
/// 456. 132模式
/// https://leetcode.com/problems/132-pattern/discuss/94089/Java-solutions-from-O(n3)-to-O(n)-for-%22132%22-pattern-(updated-with-one-pass-slution)
/// https://blog.csdn.net/LaputaFallen/article/details/80025638
/// </summary>
class The132PatternSolution
{
    public void Test()
    {
        var ret = Find132pattern(new int[] { 1, 0, 1, -4, -3 });
        //var ret = Find132pattern(new int[] { 3, 5, 0, 3, 4 });
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public bool Find132pattern(int[] nums)
    {
        if (nums == null || nums.Length < 3) return false;
        Stack<int> stack = new Stack<int>();
        int[] min = new int[nums.Length];
        min[0] = nums[0];

        for (int i = 1; i < nums.Length; i++) min[i] = Math.Min(min[i - 1], nums[i]);

        for (int j = nums.Length - 1; j >= 0; j--)
        {
            if (nums[j] > min[j])
            {
                while ( 0 < stack.Count && stack.Peek() <= min[j]) stack.Pop();
                if (0 < stack.Count && stack.Peek() < nums[j]) return true;
                stack.Push(nums[j]);
            }
        }

        return false;
    }
}
/*
public class Solution {
    public bool Find132pattern(int[] nums) {
        int[] minArray = new int[nums.Length];
        Stack<int> bolt = new Stack<int>();
        
        int min = int.MaxValue;
        for(int i = 0; i < nums.Length; i++)
        {
               
            if (nums[i] < min)
            {
                min = nums[i];
            }
            minArray[i] = min;
        }
        for(int i = nums.Length-1; i >=0; i--)
        {
            if (nums[i] > minArray[i])
            {
                int max = int.MinValue;
                while(bolt.Count!=0 && nums[i] > bolt.Peek())
                {
                    max = bolt.Peek();
                    bolt.Pop();
                }
                if (max > minArray[i])
                {
                    return true;
                }
            }
            bolt.Push(nums[i]);
        }
        return false;
    }
}
public class Solution {
    public bool Find132pattern(int[] nums) {
         int n = nums.Count();
        int mn = int.MaxValue;
        for (int i = 0; i < n; i++)
        {
            mn = Math.Min(mn, nums[i]);
            if (mn == nums[i]) continue;
            for (int j = n - 1; j > i; j--)
            {
                if (nums[j] > mn && nums[j] < nums[i]) return true;
            }
        }

        return false;
    }
} 
*/
