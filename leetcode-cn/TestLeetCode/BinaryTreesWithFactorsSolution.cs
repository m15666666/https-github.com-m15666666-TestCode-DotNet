using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给出一个含有不重复整数元素的数组，每个整数均大于 1。

我们用这些整数来构建二叉树，每个整数可以使用任意次数。

其中：每个非叶结点的值应等于它的两个子结点的值的乘积。

满足条件的二叉树一共有多少个？返回的结果应模除 10 ** 9 + 7。

 

示例 1:

输入: A = [2, 4]
输出: 3
解释: 我们可以得到这些二叉树: [2], [4], [4, 2, 2]
示例 2:

输入: A = [2, 4, 5, 10]
输出: 7
解释: 我们可以得到这些二叉树: [2], [4], [5], [10], [4, 2, 2], [10, 2, 5], [10, 5, 2].
 

提示:

1 <= A.length <= 1000.
2 <= A[i] <= 10 ^ 9.
*/
/// <summary>
/// https://leetcode-cn.com/problems/binary-trees-with-factors/
/// 823. 带因子的二叉树
/// https://www.cnblogs.com/shayue/articles/10186299.html
/// 
/// </summary>
class BinaryTreesWithFactorsSolution
{
    public void Test()
    {
        
        int[] nums = new int[] { 45, 42, 2, 18, 23, 1170, 12, 41, 40, 9, 47, 24, 33, 28, 10, 32, 29, 17, 46, 11, 759, 37, 6, 26, 21, 49, 31, 14, 19, 8, 13, 7, 27, 22, 3, 36, 34, 38, 39, 30, 43, 15, 4, 16, 35, 25, 20, 44, 5, 48 };
        var ret = NumFactoredBinaryTrees(nums);
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int NumFactoredBinaryTrees(int[] A)
    {
        const long Mod = 1000000000 + 7;
        Dictionary<int, long> V2Counts = new Dictionary<int, long>();
        int len = A.Length;
        Array.Sort(A);
        foreach (var v in A) V2Counts.Add(v, 1);

        for (int i = 0; i < len; i++)
        {
            int current = A[i];
            for (int j = 0; j < i; j++)
            {
                int vJ = A[j];
                int t;
                if (current % vJ == 0 && V2Counts.ContainsKey(t = (current / vJ)))
                    V2Counts[current] = (V2Counts[current] + V2Counts[vJ] * V2Counts[t]) % Mod;
            }
        }

        long ret = 0;
        foreach (var v in V2Counts.Values)
        {
            ret += v;
            ret %= Mod;
        }
        return (int)ret;
    }
}
/*
public class Solution {
    public int NumFactoredBinaryTrees(int[] A) {
        
        Array.Sort(A);
        
        int n = A.Length;
        
        Dictionary<int, int> val2Index = new Dictionary<int, int>();
        for (int i = 0; i < n; i ++) {
            val2Index[A[i]] = i;
        }
        
        long[] dp = new long[n];
        for (int i = 0; i < n; i ++) {
            long val = 0;
            for (int j = 0; j < i; j ++) {
                if (A[j] * A[j] > A[i]) {
                    break;
                }
                
                if (A[i]%A[j] == 0) {
                    int other = A[i]/A[j];
                    if (val2Index.ContainsKey(other)) {
                        int otherIndex = val2Index[other];
                        long temp = 0;
                        if (other == A[j]) {
                            temp = (dp[j] * dp[otherIndex]) % (1000000000 + 7);
                        } else {
                            temp = (((dp[j] * dp[otherIndex]) %(1000000000 + 7)) * 2) % (1000000000 + 7);
                        }
                        val = (val + temp) % (1000000000 + 7);
                    }
                }
                // Console.WriteLine(" i = " + i + " j = " + j + " val = " + val);
            }
            
            val += 1;
            val = val % (1000000000 + 7);
            dp[i] = (int)val;
        }
        
        int res = 0;
        for (int i = 0; i < n; i ++) {
            res = (int)(res + dp[i]);
            res = res % (1000000000 + 7);
        }
        
        return res;
    }
} 
*/
