using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
你的任务是计算 ab 对 1337 取模，a 是一个正整数，b 是一个非常大的正整数且会以数组形式给出。

示例 1:

输入: a = 2, b = [3]
输出: 8
示例 2:

输入: a = 2, b = [1,0]
输出: 1024
*/
/// <summary>
/// https://leetcode-cn.com/problems/super-pow/
/// 372. 超级次方
/// https://blog.csdn.net/zrh_CSDN/article/details/83932992
/// </summary>
class SuperPowSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int SuperPow(int a, int[] b)
    {
        const int Mod = 1337;
        if (a == 0) return 0;
        if (a == 1 || b == null || b.Length == 0 || b[0] == 0) return 1;

        var cache = new Dictionary<int,int>();
        int ret = 1;
        foreach (var p in b) ret = Pow(ret, 10, cache) * Pow(a, p, cache) % Mod;
        return ret;

        static int Pow(int x, int n, Dictionary<int,int> cache)
        {
            if (x == 1) return 1;
            if (n == 0) return 1;
            if (n == 1) return x % Mod;
            int key = n * Mod + x;
            if (cache.ContainsKey(key)) return cache[key];
            x %= Mod;
            int half = n / 2;
            var ret = Pow(x, half, cache) * Pow(x, n - half, cache) % Mod;
            cache[key] = ret;
            return ret;
        }
    }

}
/*
public class Solution {
    public int SuperPow(int a, int[] b)
	{
		if (a == 1) return 1;
		int[] dp = new int[b.Length];
		a %= 1337;
		dp[0] = a;
		for (int i = 1; i < b.Length; i++)
		{
			dp[i] = FastPower(dp[i - 1], 10);
		}
		int result = 1;
		for (int i = 0; i < b.Length; i++)
		{
			if (b[i] != 0)
			{
				result = result * FastPower(dp[b.Length - 1 - i], b[i]) % 1337;
			}
		}
		return result;
	}

	//快速幂
	public static int FastPower(int baseNum, int power)
	{
		int result = 1;
		while (power > 0)
		{
			if ((power & 1) == 1)
			{//此处等价于if(power%2==1)
				result = result * baseNum % 1337;
			}
			power >>= 1;//此处等价于power=power/2
			baseNum = (baseNum * baseNum) % 1337;
		}
		return result;
	}
}
*/