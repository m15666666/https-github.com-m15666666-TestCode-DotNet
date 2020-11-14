using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
编写一段程序来查找第 n 个超级丑数。

超级丑数是指其所有质因数都是长度为 k 的质数列表 primes 中的正整数。

示例:

输入: n = 12, primes = [2,7,13,19]
输出: 32 
解释: 给定长度为 4 的质数列表 primes = [2,7,13,19]，前 12 个超级丑数序列为：[1,2,4,7,8,13,14,16,19,26,28,32] 。
说明:

1 是任何给定 primes 的超级丑数。
 给定 primes 中的数字以升序排列。
0 < k ≤ 100, 0 < n ≤ 106, 0 < primes[i] < 1000 。
第 n 个超级丑数确保在 32 位有符整数范围内。
 */
/// <summary>
/// https://leetcode-cn.com/problems/super-ugly-number/
/// 313. 超级丑数
/// https://my.oschina.net/Tsybius2014/blog/547766
/// https://www.cnblogs.com/lightwindy/p/9758211.html，未读
/// </summary>
class SuperUglyNumberSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int NthSuperUglyNumber(int n, int[] primes)
    {
        int[] superUglyNumbers = new int[n];
        superUglyNumbers[0] = 1;
        int[] idxPrimes = new int[primes.Length];
        //for (int i = 0; i < idxPrimes.Length; i++) idxPrimes[i] = 0;

        int counter = 1;
        while (counter < n)
        {
            int min = int.MaxValue;
            for (int i = 0; i < primes.Length; i++)
            {
                int temp = superUglyNumbers[idxPrimes[i]] * primes[i];
                if( temp < min ) min = temp;
            }
            for (int i = 0; i < primes.Length; i++)
                if (min == superUglyNumbers[idxPrimes[i]] * primes[i])
                {
                    idxPrimes[i]++;
                }

            superUglyNumbers[counter++] = min;
        }

        return superUglyNumbers[n - 1];
    }
}
/*
public class Solution {
    public int NthSuperUglyNumber(int n, int[] primes) {
            var array = new int[n];
            var counts = new int[primes.Length];
            array[0] = 1;
            for (int i = 1; i < array.Length; i++)
            {
                var min = GetMin(array, primes, counts);
                array[i] = min;
                CountPlus(min, array, counts, primes);
            }
            return array[n - 1];
    }

	private int GetMin(int[] array, int[] primes, int[] counts)
	{
		var min = int.MaxValue;
		for (int i = 0; i < primes.Length; i++)
		{
			var num = array[counts[i]] * primes[i];
			if (num < min) min = num;
		}
		return min;
	}

	private void CountPlus(int min, int[] array, int[] counts, int[] primes)
	{
		for (int i = 0; i < counts.Length; i++)
		{
			if (min == array[counts[i]] * primes[i]) {
				counts[i]++;

			}
		}
	}
}

public class Solution {
    public int NthSuperUglyNumber(int n, int[] primes)
    {
        int len = primes.Length;
        int[] dp = new int[n];
        dp[0] = 1;
        int[] index = new int[len];
        for (int i = 1; i < n; i++)
        {
            int min = int.MaxValue;
            for (int j = 0; j < len; j++)
            {
                var num = primes[j] * dp[index[j]];
                min = Math.Min(min, num);
            }

            dp[i] = min;

            for (int j = 0; j < len; j++)
            {
                if (min == primes[j] * dp[index[j]])
                {
                    index[j]++;
                }
            }
        }

        return dp[n - 1];
    }
}

public class Solution {
    public int NthSuperUglyNumber(int n, int[] primes) {
        int[] dp = new int[n + 1];
        int m = primes.Length;
        int[] idx = new int[m];
        Array.Fill(idx, 1);

        dp[1] = 1;
        for (int k = 2; k <= n; k++) {
            int min = Int32.MaxValue;
            for (int i = 0; i < m; i++) {
                int x = dp[idx[i]] * primes[i];
                min = Math.Min(min, x);
            }
            dp[k] = min;
            for (int i = 0; i < m; i++) {
                if (dp[idx[i]] * primes[i] == min) {
                    idx[i]++;
                }
            }
        }

        return dp[n];
    }
}

*/
