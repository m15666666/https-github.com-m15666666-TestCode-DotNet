using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定四个包含整数的数组列表 A , B , C , D ,计算有多少个元组 (i, j, k, l) ，使得 A[i] + B[j] + C[k] + D[l] = 0。

为了使问题简单化，所有的 A, B, C, D 具有相同的长度 N，且 0 ≤ N ≤ 500 。所有整数的范围在 -228 到 228 - 1 之间，最终结果不会超过 231 - 1 。

例如:

输入:
A = [ 1, 2]
B = [-2,-1]
C = [-1, 2]
D = [ 0, 2]

输出:
2

解释:
两个元组如下:
1. (0, 0, 0, 1) -> A[0] + B[0] + C[0] + D[1] = 1 + (-2) + (-1) + 2 = 0
2. (1, 1, 0, 0) -> A[1] + B[1] + C[0] + D[0] = 2 + (-1) + (-1) + 0 = 0 
*/
/// <summary>
/// https://leetcode-cn.com/problems/4sum-ii/
/// 454. 四数相加 II
/// https://blog.csdn.net/ling_hun_pang_zi/article/details/81044289
/// </summary>
class FourSumIISolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int FourSumCount(int[] A, int[] B, int[] C, int[] D)
    {
        if (A == null || A.Length == 0) return 0;

        Dictionary<int, int> cdCounts = new Dictionary<int, int>();
        for (int i = 0; i < C.Length; i++)
        {
            var c = C[i];
            for (int j = 0; j < D.Length; j++)
            {
                var v = c + D[j];
                if (!cdCounts.ContainsKey(v)) cdCounts.Add(v, 1);
                else cdCounts[v]++;
            }
        }

        int ret = 0;
        for (int i = 0; i < A.Length; i++)
        {
            var a = A[i];
            for (int j = 0; j < B.Length; j++)
            {
                var v = -(a + B[j]);
                if ( cdCounts.ContainsKey(v) ) ret += cdCounts[v];
            }
        }

        return ret;
    }
}
/*
public class Solution {
    public int FourSumCount(int[] A, int[] B, int[] C, int[] D) {
        Dictionary<int, int> dic = new Dictionary<int, int>();
        foreach(int a in A){
            foreach(int b in B){
                if(dic.ContainsKey(a + b))
                    ++dic[a + b];
                else
                    dic[a + b] = 1;
            }
        }
        
        int count = 0;
        foreach(int c in C){
            foreach(int d in D){
                if(dic.ContainsKey(- c - d)){
                    count += dic[- c - d];
                }
            }
        }
        
        return count;
    }
}
public class Solution
{
	public int FourSumCount(int[] A, int[] B, int[] C, int[] D)
	{
		var dic = new Dictionary<int, int>();
		foreach (var d in D)
		{
			foreach (var c in C)
			{
				var key = d + c;
				dic.TryGetValue(key, out var n);
				dic[key] = ++n;
			}
		}

		var ans = 0;
		foreach (var b in B)
		{
			foreach (var a in A)
			{
				var key = 0 - b - a;
				dic.TryGetValue(key, out var n);
				ans += n;
			}
		}
		return ans;
	}
}
public class Solution {
    public int FourSumCount(int[] A, int[] B, int[] C, int[] D)
    {
        int rs = 0;
        var dict1 = ConstructDict(A, B);
        var dict2 = ConstructDict(C, D);
        foreach (var left in dict1.Keys)
        {
            if (dict2.ContainsKey(-left))
            {
                rs += dict1[left] * dict2[-left];
            }
        }

        return rs;
    }

    private Dictionary<int, int> ConstructDict(int[] A, int[] B)
    {
        Dictionary<int,int> rs = new Dictionary<int, int>();
        int len = A.Length;
        for (int i = 0; i < len; i++)
        {
            for (int j = 0; j < len; j++)
            {
                int sum = A[i] + B[j];
                if (rs.ContainsKey(sum))
                {
                    rs[sum]++;
                }
                else
                {
                    rs[sum] = 1;
                }
            }
        }

        return rs;
    }
}
*/
