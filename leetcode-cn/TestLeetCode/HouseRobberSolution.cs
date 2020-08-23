using System;

/*
你是一个专业的小偷，计划偷窃沿街的房屋。每间房内都藏有一定的现金，影响你偷窃的唯一制约因素就是相邻的房屋装有相互连通的防盗系统，如果两间相邻的房屋在同一晚上被小偷闯入，系统会自动报警。

给定一个代表每个房屋存放金额的非负整数数组，计算你 不触动警报装置的情况下 ，一夜之内能够偷窃到的最高金额。

 

示例 1：

输入：[1,2,3,1]
输出：4
解释：偷窃 1 号房屋 (金额 = 1) ，然后偷窃 3 号房屋 (金额 = 3)。
     偷窃到的最高金额 = 1 + 3 = 4 。
示例 2：

输入：[2,7,9,3,1]
输出：12
解释：偷窃 1 号房屋 (金额 = 2), 偷窃 3 号房屋 (金额 = 9)，接着偷窃 5 号房屋 (金额 = 1)。
     偷窃到的最高金额 = 2 + 9 + 1 = 12 。
 

提示：

0 <= nums.length <= 100
0 <= nums[i] <= 400

*/

/// <summary>
/// https://leetcode-cn.com/problems/house-robber/
/// 198. 打家劫舍
///
///
/// </summary>
internal class HouseRobberSolution
{
    public static void Test()
    {
        var ret = Rob(new int[] { 1, 2, 1, 1 });
        //int[] nums = new int[] {3, 2, 4};l
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public static int Rob(int[] nums)
    {
        if (nums == null || nums.Length == 0) return 0;

        int len = nums.Length;
        if (len == 1) return nums[0];

        int first = nums[0];
        int second = Math.Max(first, nums[1]);
        int current;
        for (int i = 2; i < len; i++)
        {
            current = Math.Max(first + nums[i], second);
            first = second;
            second = current;
        }
        return second;
    }
}

/*
打家劫舍
力扣官方题解
发布于 2020-05-27
37.8k
📺 视频题解

📖 文字题解
方法一：动态规划 + 滚动数组
首先考虑最简单的情况。如果只有一间房屋，则偷窃该房屋，可以偷窃到最高总金额。如果只有两间房屋，则由于两间房屋相邻，不能同时偷窃，只能偷窃其中的一间房屋，因此选择其中金额较高的房屋进行偷窃，可以偷窃到最高总金额。

如果房屋数量大于两间，应该如何计算能够偷窃到的最高总金额呢？对于第 k~(k>2)k (k>2) 间房屋，有两个选项：

偷窃第 kk 间房屋，那么就不能偷窃第 k-1k−1 间房屋，偷窃总金额为前 k-2k−2 间房屋的最高总金额与第 kk 间房屋的金额之和。

不偷窃第 kk 间房屋，偷窃总金额为前 k-1k−1 间房屋的最高总金额。

在两个选项中选择偷窃总金额较大的选项，该选项对应的偷窃总金额即为前 kk 间房屋能偷窃到的最高总金额。

用 dp[i]dp[i] 表示前 ii 间房屋能偷窃到的最高总金额，那么就有如下的状态转移方程：

\textit{dp}[i] = \max(\textit{dp}[i-2]+\textit{nums}[i], \textit{dp}[i-1])
dp[i]=max(dp[i−2]+nums[i],dp[i−1])

边界条件为：

\begin{cases} \textit{dp}[0] = \textit{nums}[0] & 只有一间房屋，则偷窃该房屋 \\ \textit{dp}[1] = \max(\textit{nums}[0], \textit{nums}[1]) & 只有两间房屋，选择其中金额较高的房屋进行偷窃 \end{cases}
{ 
dp[0]=nums[0]
dp[1]=max(nums[0],nums[1])
​	
  
只有一间房屋，则偷窃该房屋
只有两间房屋，选择其中金额较高的房屋进行偷窃
​	
 

最终的答案即为 \textit{dp}[n-1]dp[n−1]，其中 nn 是数组的长度。




class Solution {
    public int rob(int[] nums) {
        if (nums == null || nums.length == 0) {
            return 0;
        }
        int length = nums.length;
        if (length == 1) {
            return nums[0];
        }
        int[] dp = new int[length];
        dp[0] = nums[0];
        dp[1] = Math.max(nums[0], nums[1]);
        for (int i = 2; i < length; i++) {
            dp[i] = Math.max(dp[i - 2] + nums[i], dp[i - 1]);
        }
        return dp[length - 1];
    }
}
上述方法使用了数组存储结果。考虑到每间房屋的最高总金额只和该房屋的前两间房屋的最高总金额相关，因此可以使用滚动数组，在每个时刻只需要存储前两间房屋的最高总金额。


class Solution {
    public int rob(int[] nums) {
        if (nums == null || nums.length == 0) {
            return 0;
        }
        int length = nums.length;
        if (length == 1) {
            return nums[0];
        }
        int first = nums[0], second = Math.max(nums[0], nums[1]);
        for (int i = 2; i < length; i++) {
            int temp = second;
            second = Math.max(first + nums[i], second);
            first = temp;
        }
        return second;
    }
}
复杂度分析

时间复杂度：O(n)O(n)，其中 nn 是数组长度。只需要对数组遍历一次。

空间复杂度：O(1)O(1)。使用滚动数组，可以只存储前两间房屋的最高总金额，而不需要存储整个数组的结果，因此空间复杂度是 O(1)O(1)。

下一篇：图解动态规划的解题四步骤（C++/Java/Python）

public class Solution {
    public int Rob(int[] nums) {
        int n = nums.Length;
        if(n==0){return 0;}
        if(n==1){return nums[0];}

        int nonExcept = nums[0];
        int except = Math.Max(nums[1],nonExcept);
        for(int i = 2; i < n; i++){
            int temp = except;
            except = Math.Max(except,nonExcept+nums[i]);
            nonExcept = temp;
        }
        return except;
    }
}

public class Solution {//动态规划
    public int Rob(int[] nums) {
        if(nums.Length==0)
        return 0;

        int[,]  dp=new int[nums.Length,2];//记录两条路线  1.第一天偷  2.第二天偷
        dp[0,1]=nums[0];//第一天偷的情况
        dp[0,0]=0; //第一天不偷
        for(int i=1;i<nums.Length;i++)
        {
            dp[i,0]=Math.Max(dp[i-1,0],dp[i-1,1]);//选择没偷，最高金额有两种情况。那昨天也没有偷，或者昨天偷了
            dp[i,1]=dp[i-1,0]+nums[i];//选择偷了，昨天一定没有偷
        }
        return Math.Max(dp[nums.Length-1,0],dp[nums.Length-1,1]);
    }
}

public class Solution {
    public int Rob(int[] nums) {
        if(nums==null || nums.Length==0)
            return 0;
        
        int n=nums.Length;
        int[] dp=new int[n+1];
        dp[0]=0;
        dp[1]=nums[0];

        for(int k=2; k<=n;k++)
            dp[k]=Math.Max(dp[k-1], nums[k-1]+dp[k-2]);
        
        return dp[n];
    }
}


*/