using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
一个机器人位于一个 m x n 网格的左上角 （起始点在下图中标记为“Start” ）。

机器人每次只能向下或者向右移动一步。机器人试图达到网格的右下角（在下图中标记为“Finish”）。

问总共有多少条不同的路径？

例如，上图是一个7 x 3 的网格。有多少可能的路径？

示例 1:

输入: m = 3, n = 2
输出: 3
解释:
从左上角开始，总共有 3 条路径可以到达右下角。
1. 向右 -> 向右 -> 向下
2. 向右 -> 向下 -> 向右
3. 向下 -> 向右 -> 向右
示例 2:

输入: m = 7, n = 3
输出: 28
 

提示：

1 <= m, n <= 100
题目数据保证答案小于等于 2 * 10 ^ 9
*/
/// <summary>
/// https://leetcode-cn.com/problems/unique-paths/
/// 62.不同路径
/// 
/// </summary>
class UniquePathsSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    public int UniquePaths(int m, int n)
    {
        if (m < 1 || n < 1) return 0;
        if (m == 1 || n == 1) return 1;

        int[] vertical = new int[m - 1];

        // 最下面一行，和最右面一列的路径都是1.
        for (int i = 0; i < vertical.Length; i++) vertical[i] = 1;

        for( int c = 0; c < n - 1; c++)
        {
            int horizontalValue = 1;
            for (int i = 0; i < vertical.Length; i++)
            {
                horizontalValue += vertical[i];
                vertical[i] = horizontalValue;
            }
        }

        return vertical[vertical.Length - 1];
    }

}
/*
动态规划
powcai
发布于 1 年前
52.3k
思路
思路一：排列组合

因为机器到底右下角，向下几步，向右几步都是固定的，

比如，m=3, n=2，我们只要向下 1 步，向右 2 步就一定能到达终点。

所以有 C_{m+n-2}^{m-1}C 
m+n−2
m−1
​	
 

def uniquePaths(self, m: int, n: int) -> int:
        return int(math.factorial(m+n-2)/math.factorial(m-1)/math.factorial(n-1))
思路二：动态规划

我们令 dp[i][j] 是到达 i, j 最多路径

动态方程：dp[i][j] = dp[i-1][j] + dp[i][j-1]

注意，对于第一行 dp[0][j]，或者第一列 dp[i][0]，由于都是在边界，所以只能为 1

时间复杂度：O(m*n)O(m∗n)

空间复杂度：O(m * n)O(m∗n)

优化：因为我们每次只需要 dp[i-1][j],dp[i][j-1]

所以我们只要记录这两个数，直接看代码吧！

代码
思路二：

class Solution {
    public int uniquePaths(int m, int n) {
        int[][] dp = new int[m][n];
        for (int i = 0; i < n; i++) dp[0][i] = 1;
        for (int i = 0; i < m; i++) dp[i][0] = 1;
        for (int i = 1; i < m; i++) {
            for (int j = 1; j < n; j++) {
                dp[i][j] = dp[i - 1][j] + dp[i][j - 1];
            }
        }
        return dp[m - 1][n - 1];  
    }
}
优化1：空间复杂度 O(2n)O(2n)

class Solution {
    public int uniquePaths(int m, int n) {
        int[] pre = new int[n];
        int[] cur = new int[n];
        Arrays.fill(pre, 1);
        Arrays.fill(cur,1);
        for (int i = 1; i < m;i++){
            for (int j = 1; j < n; j++){
                cur[j] = cur[j-1] + pre[j];
            }
            pre = cur.clone();
        }
        return pre[n-1]; 
    }
}
优化2：空间复杂度 O(n)O(n)

class Solution {
    public int uniquePaths(int m, int n) {
        int[] cur = new int[n];
        Arrays.fill(cur,1);
        for (int i = 1; i < m;i++){
            for (int j = 1; j < n; j++){
                cur[j] += cur[j-1] ;
            }
        }
        return cur[n-1];
    }
}
下一篇：C++ 的递归求解

*/