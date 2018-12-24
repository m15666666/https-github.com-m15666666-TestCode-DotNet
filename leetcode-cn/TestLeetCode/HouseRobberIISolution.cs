using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// https://leetcode-cn.com/problems/house-robber-ii/
/// 213. 打家劫舍 II
/// 你是一个专业的小偷，计划偷窃沿街的房屋，每间房内都藏有一定的现金。这个地方所有的房屋都围成一圈，
/// 这意味着第一个房屋和最后一个房屋是紧挨着的。同时，相邻的房屋装有相互连通的防盗系统，
/// 如果两间相邻的房屋在同一晚上被小偷闯入，系统会自动报警。
/// 给定一个代表每个房屋存放金额的非负整数数组，计算你在不触动警报装置的情况下，能够偷窃到的最高金额。
/// 示例 1:
/// 输入: [2,3,2]
/// 输出: 3
/// 解释: 你不能先偷窃 1 号房屋（金额 = 2），然后偷窃 3 号房屋（金额 = 2）, 因为他们是相邻的。
/// 示例 2:
/// 输入: [1,2,3,1]
/// 输出: 4
/// 解释: 你可以先偷窃 1 号房屋（金额 = 1），然后偷窃 3 号房屋（金额 = 3）。
/// 偷窃到的最高金额 = 1 + 3 = 4 。
/// https://blog.csdn.net/xuchonghao/article/details/80693778
/// </summary>
class HouseRobberIISolution
{
    public static void Test()
    {
        var ret = Rob(new int[] { 1, 2, 1, 1 });
        //int[] nums = new int[] {3, 2, 4};l
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    private static int RobCycle(int[] nums, int low, int high)
    {
        int len = high - low + 1;

        int s1 = 0;//not rob
        int s2 = 0;//rob
        for (int i = 0; i < len; i++)
        {
            int tmp = s1;
            s1 = Math.Max(s1, s2);//not rob
            s2 = nums[low + i] + tmp;//rob
        }

        return Math.Max(s1, s2);
    }
    public static int Rob(int[] nums)
    {
        if (nums == null || nums.Length == 0) return 0;
        var length = nums.Length;
        if (length == 1) return nums[0];
        int a = RobCycle(nums, 0, length - 2);
        int b = RobCycle(nums, 1, length - 1);
        return Math.Max(a, b);
    }
}

/*
// 自己的做法，没找到问题，但结果不对
输入
[1, 2, 3, 4, 5, 1, 2, 3, 4, 5]
输出
15
预期结果
16

    public static int Rob(int[] nums)
    {
        if (nums == null || nums.Length == 0 ) return 0;

        var length = nums.Length;
        if (length == 1) return nums[0];
        if (length == 2) return Math.Max(nums[0], nums[1]);
        if (length == 3) return Math.Max( Math.Max(nums[0], nums[1]), nums[2]);

        Queue<int> queue = new Queue<int>(3);
        queue.Enqueue(0);
        queue.Enqueue(0);
        queue.Enqueue(0);
        
        // 第一个
        for (int i = length - 2; 1 < i; i--)
        {
            var lastOne = queue.Dequeue();
            var lastTwo = queue.Peek();
            queue.Enqueue(nums[i] + Math.Max(lastOne, lastTwo));
        }
        queue.Dequeue();
        var firstMax = nums[0] + Math.Max(queue.Dequeue(), queue.Dequeue());


        // 第二个
        queue.Clear();
        queue.Enqueue(0);
        queue.Enqueue(0);
        queue.Enqueue(0);
        for (int i = length - 1; 2 < i; i--)
        {
            var lastOne = queue.Dequeue();
            var lastTwo = queue.Peek();
            queue.Enqueue(nums[i] + Math.Max(lastOne, lastTwo));
        }
        queue.Dequeue();
        var secondMax = nums[1] + Math.Max(queue.Dequeue(), queue.Dequeue());

        return Math.Max(firstMax, secondMax);
    }
*/
