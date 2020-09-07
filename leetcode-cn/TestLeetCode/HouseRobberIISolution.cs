using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
你是一个专业的小偷，计划偷窃沿街的房屋，每间房内都藏有一定的现金。这个地方所有的房屋都围成一圈，这意味着第一个房屋和最后一个房屋是紧挨着的。同时，相邻的房屋装有相互连通的防盗系统，如果两间相邻的房屋在同一晚上被小偷闯入，系统会自动报警。

给定一个代表每个房屋存放金额的非负整数数组，计算你在不触动警报装置的情况下，能够偷窃到的最高金额。

示例 1:

输入: [2,3,2]
输出: 3
解释: 你不能先偷窃 1 号房屋（金额 = 2），然后偷窃 3 号房屋（金额 = 2）, 因为他们是相邻的。
示例 2:

输入: [1,2,3,1]
输出: 4
解释: 你可以先偷窃 1 号房屋（金额 = 1），然后偷窃 3 号房屋（金额 = 3）。
     偷窃到的最高金额 = 1 + 3 = 4 。

*/
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

打家劫舍 II（动态规划，结构化思路，清晰题解）
Krahets
发布于 2019-09-19
40.5k
解题思路：
总体思路：
此题是 198. 打家劫舍 的拓展版： 唯一的区别是此题中的房间是环状排列的（即首尾相接），而 198.198. 题中的房间是单排排列的；而这也是此题的难点。

环状排列意味着第一个房子和最后一个房子中只能选择一个偷窃，因此可以把此环状排列房间问题约化为两个单排排列房间子问题：

在不偷窃第一个房子的情况下（即 nums[1:]nums[1:]），最大金额是 p_1p 
1
​	
  ；
在不偷窃最后一个房子的情况下（即 nums[:n-1]nums[:n−1]），最大金额是 p_2p 
2
​	
  。
综合偷窃最大金额： 为以上两种情况的较大值，即 max(p1,p2)max(p1,p2) 。
下面的任务则是解决 单排排列房间（即 198. 打家劫舍） 问题。推荐可以先把 198.198. 做完再做这道题。

198. 解题思路：
典型的动态规划，以下按照标准流程解题。

状态定义：
设动态规划列表 dpdp ，dp[i]dp[i] 代表前 ii 个房子在满足条件下的能偷窃到的最高金额。
转移方程：
设： 有 nn 个房子，前 nn 间能偷窃到的最高金额是 dp[n]dp[n] ，前 n-1n−1 间能偷窃到的最高金额是 dp[n-1]dp[n−1] ，此时向这些房子后加一间房，此房间价值为 numnum ；
加一间房间后： 由于不能抢相邻的房子，意味着抢第 n+1n+1 间就不能抢第 nn 间；那么前 n+1n+1 间房能偷取到的最高金额 dp[n+1]dp[n+1] 一定是以下两种情况的 较大值 ：
不抢第 n+1n+1 个房间，因此等于前 nn 个房子的最高金额，即 dp[n+1] = dp[n]dp[n+1]=dp[n] ；
抢第 n+1n+1 个房间，此时不能抢第 nn 个房间；因此等于前 n-1n−1 个房子的最高金额加上当前房间价值，即 dp[n+1] = dp[n-1] + numdp[n+1]=dp[n−1]+num ；
细心的我们发现： 难道在前 nn 间的最高金额 dp[n]dp[n] 情况下，第 nn 间一定被偷了吗？假设没有被偷，那 n+1n+1 间的最大值应该也可能是 dp[n+1] = dp[n] + numdp[n+1]=dp[n]+num 吧？其实这种假设的情况可以被省略，这是因为：
假设第 nn 间没有被偷，那么此时 dp[n] = dp[n-1]dp[n]=dp[n−1] ，此时 dp[n+1] = dp[n] + num = dp[n-1] + numdp[n+1]=dp[n]+num=dp[n−1]+num ，即可以将 两种情况合并为一种情况 考虑；
假设第 nn 间被偷，那么此时 dp[n+1] = dp[n] + numdp[n+1]=dp[n]+num 不可取 ，因为偷了第 nn 间就不能偷第 n+1n+1 间。
最终的转移方程： dp[n+1] = max(dp[n],dp[n-1]+num)dp[n+1]=max(dp[n],dp[n−1]+num)
初始状态：
前 00 间房子的最大偷窃价值为 00 ，即 dp[0] = 0dp[0]=0 。
返回值：
返回 dpdp 列表最后一个元素值，即所有房间的最大偷窃价值。
简化空间复杂度：
我们发现 dp[n]dp[n] 只与 dp[n-1]dp[n−1] 和 dp[n-2]dp[n−2] 有关系，因此我们可以设两个变量 cur和 pre 交替记录，将空间复杂度降到 O(1)O(1) 。
复杂度分析：
时间复杂度 O(N)O(N) ： 两次遍历 nums 需要线性时间；
空间复杂度 O(1)O(1) ： cur和 pre 使用常数大小的额外空间。


代码：

class Solution {
    public int rob(int[] nums) {
        if(nums.length == 0) return 0;
        if(nums.length == 1) return nums[0];
        return Math.max(myRob(Arrays.copyOfRange(nums, 0, nums.length - 1)), 
                        myRob(Arrays.copyOfRange(nums, 1, nums.length)));
    }
    private int myRob(int[] nums) {
        int pre = 0, cur = 0, tmp;
        for(int num : nums) {
            tmp = cur;
            cur = Math.max(pre + num, cur);
            pre = tmp;
        }
        return cur;
    }
}

*/
