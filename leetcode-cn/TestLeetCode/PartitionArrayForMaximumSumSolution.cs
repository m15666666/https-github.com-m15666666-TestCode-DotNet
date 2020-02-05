using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给出整数数组 A，将该数组分隔为长度最多为 K 的几个（连续）子数组。分隔完成后，每个子数组的中的值都会变为该子数组中的最大值。

返回给定数组完成分隔后的最大和。

 

示例：

输入：A = [1,15,7,9,2,5,10], K = 3
输出：84
解释：A 变为 [15,15,15,9,10,10,10]
 

提示：

1 <= K <= A.length <= 500
0 <= A[i] <= 10^6
 
*/
/// <summary>
/// https://leetcode-cn.com/problems/partition-array-for-maximum-sum/
/// 1043. 分隔数组以得到最大和
/// 
/// </summary>
class PartitionArrayForMaximumSumSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int MaxSumAfterPartitioning(int[] A, int K)
    {
        int n = A.Length;

        int[] dp = new int[n+1];
        Array.Fill(dp, 0);
        for (int dpIndex = 1; dpIndex <= n; dpIndex++)
        {
            int index = dpIndex - 1;
            int maxOfRange = A[index];
            for (int len = 1; len <= K && -1 < index; len++, index--) 
            {
                int v = A[index];
                if (maxOfRange < v) maxOfRange = v;
                int sum = dp[index] + maxOfRange * len;
                if (dp[dpIndex] < sum) dp[dpIndex] = sum;
            }
        }
        return dp[n];
    }
}
/*
dp[i]表示A[0]...A[i]的最大和
lss
331 阅读
class Solution {
public:
    int maxSumAfterPartitioning(vector<int>& A, int K) {
        int dp[505]={0};  //dp[i]表示A[0]...A[i]的最大和，一定要手动初始化为0
        int n=A.size(),maxtmp;
        for(int i=0;i<n;i++)
        {
            maxtmp=A[i];
            for(int len=1;len<=K && i-len+1>=0;len++) //找i前面最近的len个数字中的最大值
            {
                //A[i-len+1]...A[i]
                maxtmp=max(maxtmp,A[i-len+1]);  //随着len值的增加，每次向左扩充一位
                //i-len+1前面一位是i-len
                if(i-len>=0)
                    dp[i]=max(dp[i],dp[i-len]+maxtmp*len);
                else
                    dp[i]=max(dp[i],maxtmp*len);   //最近的len位都被替换成maxtmp的值，这样结果最大
            }
        }
        return dp[n-1];
    }
};

public int maxSumAfterPartitioning(int[] A, int K) {
    if (A == null || A.length == 0) return 0;
    int len = A.length;
    int[] dp = new int[len + 1];
    for (int i = 0; i <= len; i++) {
        int j = i-1;
        int max = dp[i];
        while ((i - j) <= K && j >= 0) {
            max = Math.max(max, A[j]);
            dp[i] = Math.max(dp[i], dp[j] + max * (i - j));
            j--;
        }
    }
    return dp[len];
}

public class Solution {
    public int MaxSumAfterPartitioning(int[] A, int K) {
        int[] dp = new int[A.Length];

        for(int i = 0;i<A.Length;i++){
            int currMax = 0;

            for(int k = 1; k<=K && i-k+1 >= 0 ;k++){
                currMax = Math.Max(currMax,A[i-k+1]);
                dp[i] = Math.Max(dp[i],(i-k >= 0 ? dp[i-k]:0)+currMax*k);
            }
        }
        return dp[A.Length-1];
    }
}
 
*/
