using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
我们在两条独立的水平线上按给定的顺序写下 A 和 B 中的整数。

现在，我们可以绘制一些连接两个数字 A[i] 和 B[j] 的直线，只要 A[i] == B[j]，
且我们绘制的直线不与任何其他连线（非水平线）相交。

以这种方法绘制线条，并返回我们可以绘制的最大连线数。

 

示例 1：

输入：A = [1,4,2], B = [1,2,4]
输出：2
解释：
我们可以画出两条不交叉的线，如上图所示。
我们无法画出第三条不相交的直线，因为从 A[1]=4 到 B[2]=4 的直线将与从 A[2]=2 到 B[1]=2 的直线相交。
示例 2：

输入：A = [2,5,1,2,5], B = [10,5,2,1,5,2]
输出：3
示例 3：

输入：A = [1,3,7,1,7,5], B = [1,9,2,5,1]
输出：2
 

提示：

1 <= A.length <= 500
1 <= B.length <= 500
1 <= A[i], B[i] <= 2000
 
*/
/// <summary>
/// https://leetcode-cn.com/problems/uncrossed-lines/
/// 1035. 不相交的线
/// 
/// </summary>
class UncrossedLinesSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int MaxUncrossedLines(int[] A, int[] B)
    {
        int n1 = A.Length;
        int n2 = B.Length;
        
        var dp = new int[n1 + 1,n2 + 1];
        for (int i = 0; i <= n2; i++) dp[0, i] = 0;
        for (int i = 0; i <= n1; i++) dp[i, 0] = 0;

        for (int i = 0; i < n1; i++)
            for (int j = 0; j < n2; j++)
                if (A[i] == B[j]) dp[i + 1,j + 1] = dp[i,j] + 1;
                else dp[i + 1,j + 1] = Math.Max(dp[i + 1,j], dp[i,j + 1]);

        return dp[n1,n2];
    }
}
/*
同【最长公共子序列】
joseph_liujian
896 阅读
此题同 最长公共子序列

public class Solution {
    public int maxUncrossedLines(int[] A, int[] B) {
        int n1 = A.length;
        int n2 = B.length;
        int[][] dp = new int[n1+1][n2+1];
        for(int i=0;i<n1;i++){
            for(int j=0;j<n2;j++){
                if(A[i]==B[j]){
                    dp[i+1][j+1] = dp[i][j]+1;
                }else{
                    dp[i+1][j+1] = Math.max(dp[i+1][j],dp[i][j+1]);
                }
            }
        }
        return dp[n1][n2];
    }
}

public class Solution {
    public int MaxUncrossedLines(int[] A, int[] B) {
        int[][] dp = new int[A.Length+1][];
        for(int i=0;i<A.Length+1;i++)
        {
            dp[i] = new int[B.Length+1];
            if(i == 0)
                continue;
            for(int j=0;j<B.Length+1;j++)
            {
                if(j==0)
                    continue;
                if(A[i-1] == B[j-1])
                    dp[i][j] = dp[i-1][j-1]+1;
                else
                    dp[i][j] = Math.Max(dp[i-1][j],dp[i][j-1]);
            }
        }
        return dp[A.Length][B.Length];
    }
}
*/
