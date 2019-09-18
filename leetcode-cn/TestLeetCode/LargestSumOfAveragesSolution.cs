using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
我们将给定的数组 A 分成 K 个相邻的非空子数组 ，我们的分数由每个子数组内的平均值的总和构成。计算我们所能得到的最大分数是多少。

注意我们必须使用 A 数组中的每一个数进行分组，并且分数不一定需要是整数。

示例:
输入: 
A = [9,1,2,3,9]
K = 3
输出: 20
解释: 
A 的最优分组是[9], [1, 2, 3], [9]. 得到的分数是 9 + (1 + 2 + 3) / 3 + 9 = 20.
我们也可以把 A 分成[9, 1], [2], [3, 9].
这样的分组得到的分数为 5 + 2 + 6 = 13, 但不是最大值.
说明:

1 <= A.length <= 100.
1 <= A[i] <= 10000.
1 <= K <= A.length.
答案误差在 10^-6 内被视为是正确的。
*/
/// <summary>
/// https://leetcode-cn.com/problems/largest-sum-of-averages/
/// 813. 最大平均值和的分组
/// https://blog.csdn.net/u010669349/article/details/98765310
/// </summary>
class LargestSumOfAveragesSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public double LargestSumOfAverages(int[] A, int K)
    {
        int len = A.Length;
        var sum = new double[len + 1];
        sum[0] = 0;
        for (int i = 0; i < len; i++)
        {
            sum[i + 1] = A[i] + sum[i];//前i个元素的和
        }
        var dp = new double[len + 1,K + 1];//dp[i,k]表示把A数组中前i个元素分成K段，最大值
        for(int i = 1;i <= len;i++){
            dp[i,1] = sum[i]/i;//把前i个元素分成一段
            for(int k = 2;k <= K && k <= i; k++){ //计算前i个元素分成[2, K]段（注意k不能超过i，因为i个元素最多分成i段
                for(int j=1;j<i;j++){////把前j个分成K - 1段，将(j, i]分成一段
                    //sum[i] - sum[j]计算的是[j, i）和，方程表示把前j个分成k - 1段，最后(j, i]单独看做一段计算的和
                    var v = dp[j, k - 1] + (sum[i] - sum[j]) / (i - j);
                    if (dp[i, k] < v) dp[i, k] = v;
                }
            }
        }
        return dp[len,K];
    }
}
/*
public class Solution {
    double [,] arr;
    public double LargestSumOfAverages(int[] A, int K) {
        int n = A.Length;
        arr = new double[n+1,n+1];
        
        double res = Fun(A, 0, K);
        
        return res;
    }
    
    double Fun(int[] A, int s, int k) {
        if (arr[s, k] != 0) {
            return arr[s, k];
        }
        int n = A.Length;
        double score = 0;
        if (k == 1) {
            int count = 0;
            int sum = 0;
            for (int i = s; i < n; i ++) {
                count ++;
                sum += A[i];
            }
            score = 1.0 * sum/count;
        } else {
            int sum = 0;
            for (int i = s; i <= n - k; i ++) {
                sum += A[i];
            
                double preScore = 1.0 * sum/(i - s + 1);
                double restScore = Fun(A, i+1, k - 1);
                score = Math.Max(score, preScore + restScore);
            }
        }
        
        arr[s,k] = score;
            
        // Console.WriteLine(" s = " + s + " k = " + k);
        // Console.WriteLine(" score = " + score);
        return score;
    }
}
public class Solution {
    public double LargestSumOfAverages(int[] A, int K) {
        if (A == null || A.Length == 0 || A.Length < K)
            throw new ArgumentException();
        double[] sum = new double[A.Length];
        sum[0] = A[0];
        for (int i = 1; i < A.Length; i++)
        {
            sum[i] = sum[i - 1] + A[i];
        }

        double[,] dp = new double[A.Length + 1, K + 1];
        for (int i = 1; i <= A.Length; i++)
        {
            dp[i, 1] = sum[i - 1] / i;
            for (int j = 2; j <= K && j <= i; j++)
            {
                for (int m = 1; m < i; m++)
                {
                    dp[i, j] = Math.Max(dp[i, j], (sum[i - 1] - sum[m - 1]) / (i - m) + dp[m, j - 1]);
                }
            }
        }
        return dp[A.Length, K];
    }
}
*/
