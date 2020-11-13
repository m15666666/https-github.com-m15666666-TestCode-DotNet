using System;

/*
有 n 个气球，编号为0 到 n-1，每个气球上都标有一个数字，这些数字存在数组 nums 中。

现在要求你戳破所有的气球。如果你戳破气球 i ，就可以获得 nums[left] * nums[i] * nums[right] 个硬币。 这里的 left 和 right 代表和 i 相邻的两个气球的序号。注意当你戳破了气球 i 后，气球 left 和气球 right 就变成了相邻的气球。

求所能获得硬币的最大数量。

说明:

你可以假设 nums[-1] = nums[n] = 1，但注意它们不是真实存在的所以并不能被戳破。
0 ≤ n ≤ 500, 0 ≤ nums[i] ≤ 100
示例:

输入: [3,1,5,8]
输出: 167
解释: nums = [3,1,5,8] --> [3,5,8] -->   [3,8]   -->  [8]  --> []
     coins =  3*1*5      +  3*5*8    +  1*3*8      + 1*8*1   = 167

*/

/// <summary>
/// https://leetcode-cn.com/problems/burst-balloons/
/// 312. 戳气球
///
/// </summary>
internal class BurstBalloonsSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int MaxCoins(int[] nums)
    {
        int n = nums.Length;
        int[,] dp = new int[n + 2, n + 2]; // 默认(i,j)区间逐步添加气球的最大值为0
        int[] val = new int[n + 2];
        val[0] = val[n + 1] = 1;
        for (int i = 1; i <= n; i++) val[i] = nums[i - 1];

        for (int i = n - 1; -1 < i; i--)
            for (int j = i + 2; j <= n + 1; j++)
                for (int k = i + 1; k < j; k++)
                {
                    // (i,j)开区间添加第一个气球并将区间分成(i,k)和(k,j)两个开区间继续计算
                    // 在这里要使用之前计算的(i,k)和(k,j)两个开区间的结果
                    int sum = val[i] * val[k] * val[j]; 
                    sum += dp[i, k] + dp[k, j];
                    if(dp[i,j] < sum) dp[i, j] = sum;
                }

        return dp[0, n + 1];
    }
}

/*
戳气球
力扣官方题解
发布于 2020-07-18
30.4k
写在前面
为了方便处理，我们对 \textit{nums}nums 数组稍作处理，将其两边各加上题目中假设存在的 \textit{nums}[-1]nums[−1] 和 \textit{nums}[n]nums[n] ，并保存在 \textit{val}val 数组中，即 \textit{val}[i]=\textit{nums}[i-1]val[i]=nums[i−1] 。之所以这样处理是为了处理 \textit{nums}[-1]nums[−1] ，防止下标越界。

下文中的区间均指数组 \textit{val}val 上的区间。

方法一：记忆化搜索
思路及算法

我们观察戳气球的操作，发现这会导致两个气球从不相邻变成相邻，使得后续操作难以处理。于是我们倒过来看这些操作，将全过程看作是每次添加一个气球。

我们定义方法 \textit{solve}solve，令 \textit{solve}(i,j)solve(i,j) 表示将开区间 (i,j)(i,j) 内的位置全部填满气球能够得到的最多硬币数。由于是开区间，因此区间两端的气球的编号就是 ii 和 jj，对应着 \textit{val}[i]val[i] 和 \textit{val}[j]val[j]。

当 i \geq j-1i≥j−1 时，开区间中没有气球，\textit{solve}(i,j)solve(i,j) 的值为 00；

当 i < j-1i<j−1 时，我们枚举开区间 (i,j)(i,j) 内的全部位置 \textit{mid}mid，令 \textit{mid}mid 为当前区间第一个添加的气球，该操作能得到的硬币数为 \textit{val}[i] \times \textit{val}[\textit{mid}] \times val[j]val[i]×val[mid]×val[j]。同时我们递归地计算分割出的两区间对 \textit{solve}(i,j)solve(i,j) 的贡献，这三项之和的最大值，即为 \textit{solve}(i,j)solve(i,j) 的值。这样问题就转化为求 \textit{solve}(i,\textit{mid})solve(i,mid) 和 \textit{solve}(\textit{mid},j)solve(mid,j) ，可以写出方程：

\textit{solve}(i,j)= \begin{cases}{} \displaystyle \max_{\textit{mid} = i + 1}^{j - 1}val[i] \times \textit{val}[\textit{mid}] \times \textit{val}[j] + \textit{solve}(i, \textit{mid}) + \textit{solve}(\textit{mid}, j) ,&i < j - 1 \\ 0, & i \geq j - 1 \end{cases}
solve(i,j)= 
⎩
⎪
⎨
⎪
⎧
​	
  
mid=i+1
max
j−1
​	
 val[i]×val[mid]×val[j]+solve(i,mid)+solve(mid,j),
0,
​	
  
i<j−1
i≥j−1
​	
 

为了防止重复计算，我们存储 \textit{solve}solve 的结果，使用记忆化搜索的方法优化时间复杂度。



代码


class Solution {
    public int[][] rec;
    public int[] val;

    public int maxCoins(int[] nums) {
        int n = nums.length;
        val = new int[n + 2];
        for (int i = 1; i <= n; i++) {
            val[i] = nums[i - 1];
        }
        val[0] = val[n + 1] = 1;
        rec = new int[n + 2][n + 2];
        for (int i = 0; i <= n + 1; i++) {
            Arrays.fill(rec[i], -1);
        }
        return solve(0, n + 1);
    }

    public int solve(int left, int right) {
        if (left >= right - 1) {
            return 0;
        }
        if (rec[left][right] != -1) {
            return rec[left][right];
        }
        for (int i = left + 1; i < right; i++) {
            int sum = val[left] * val[i] * val[right];
            sum += solve(left, i) + solve(i, right);
            rec[left][right] = Math.max(rec[left][right], sum);
        }
        return rec[left][right];
    }
}
复杂度分析

时间复杂度：O(n^3)O(n 
3
 )，其中 nn 是气球数量。区间数为 n^2n 
2
 ，区间迭代复杂度为 O(n)O(n)，最终复杂度为 O(n^2 \times n) = O(n^3)O(n 
2
 ×n)=O(n 
3
 )。

空间复杂度：O(n^2)O(n 
2
 )，其中 nn 是气球数量。缓存大小为区间的个数。

方法二：动态规划
思路及算法

按照方法一的思路，我们发现我们可以通过变换计算顺序，从「自顶向下」的记忆化搜索变为「自底向上」的动态规划。

令 dp[i][j]表示填满开区间 (i,j) 能得到的最多硬币数，那么边界条件是 i \geq j-1i≥j−1，此时有 dp[i][j]=0。

可以写出状态转移方程：

dp[i][j]= \begin{cases}{} \displaystyle \max_{k = i + 1}^{j - 1}val[i] \times val[k] \times val[j] + dp[i][k] + dp[k][j] ,&i < j - 1 \\ 0, & i \geq j - 1 \end{cases}
dp[i][j]= 
⎩
⎪
⎨
⎪
⎧
​	
  
k=i+1
max
j−1
​	
 val[i]×val[k]×val[j]+dp[i][k]+dp[k][j],
0,
​	
  
i<j−1
i≥j−1
​	
 

最终答案即为 dp[0][n+1]dp[0][n+1]。实现时要注意到动态规划的次序。

代码


class Solution {
    public int maxCoins(int[] nums) {
        int n = nums.length;
        int[][] rec = new int[n + 2][n + 2];
        int[] val = new int[n + 2];
        val[0] = val[n + 1] = 1;
        for (int i = 1; i <= n; i++) {
            val[i] = nums[i - 1];
        }
        for (int i = n - 1; i >= 0; i--) {
            for (int j = i + 2; j <= n + 1; j++) {
                for (int k = i + 1; k < j; k++) {
                    int sum = val[i] * val[k] * val[j];
                    sum += rec[i][k] + rec[k][j];
                    rec[i][j] = Math.max(rec[i][j], sum);
                }
            }
        }
        return rec[0][n + 1];
    }
}
复杂度分析

时间复杂度：O(n^3)O(n 
3
 )，其中 nn 是气球数量。状态数为 n^2n 
2
 ，状态转移复杂度为 O(n)O(n)，最终复杂度为 O(n^2 \times n) = O(n^3)O(n 
2
 ×n)=O(n 
3
 )。

空间复杂度：O(n^2)O(n 
2
 )，其中 nn 是气球数量。

    public int MaxCoins(int[] nums) {
        int n = nums.Length;
        int[] temp = new int[n + 2];
        temp[0] = 1;
        temp[n + 1] = 1;
        for(int i = 1; i <= n; i++)
            temp[i] = nums[i - 1];
        nums = temp;

        int[][] dp = new int[n + 2][];
        for(int i = 0; i <= n + 1; i++)
            dp[i] = new int[n + 2];

        int ans = 0;
        for(int i = n + 1; i >= 0; i--)
        {
            for(int j = i + 2; j <= n + 1; j++)
            {
                for(int k = i + 1; k < j; k++)
                    dp[i][j] = Math.Max(dp[i][j], dp[i][k] + dp[k][j] + nums[i] * nums[k] * nums[j]);
            }
        }

        return dp[0][n + 1];
    }
}

public class Solution 
{
	int[][] dp;
	public  int MaxCoins(int[] nums)
	{
		if (nums.Length == 0)
			return 0;
		if (nums.Length == 1)
			return nums[0];
		if (nums.Length == 2)
		{
			return nums[0] * nums[1] + Math.Max(nums[0], nums[1]);
		}
		if (nums.Length == 3)
		{
			return nums[0] * nums[1] * nums[2] + MaxCoins(new int[] { nums[0], nums[2] });
		}
		int[] newnum = new int[nums.Length + 2];
		for (int i = 0; i < nums.Length; i++)
		{
			newnum[i + 1] = nums[i];
		}
		newnum[0] = 1;
		newnum[newnum.Length - 1] = 1;
		dp = new int[newnum.Length][];
		for (int i = 0; i < dp.Length; i++)
		{
			dp[i] = new int[newnum.Length];
			for (int j = 0; j < dp[i].Length; j++)
			{
				dp[i][j] = -1;
			}
		}
		return solve(newnum, 0, newnum.Length - 1);
	}

    int  solve(int[] nums, int m, int n)
	{
		if ( m >= n - 1)
		{
			return 0;
		}
		if (dp[m][n] != -1)
		{
			return dp[m][n];
		}
		for (int i = m + 1; i < n; i++)
		{
			int result = solve(nums, m, i) + solve(nums, i, n) + nums[m] * nums[i] * nums[n];
			if (dp[m][n] < result)
			{
				dp[m][n] = result;
			}
		}
		//Console.WriteLine(dp[m][n]);
		return dp[m][n];
	}
}


*/