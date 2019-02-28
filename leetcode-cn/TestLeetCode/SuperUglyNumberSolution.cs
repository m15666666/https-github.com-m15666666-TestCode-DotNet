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
          var arr = new int[n];
            arr[0] = 1;
            var indexs = new int[primes.Length];
            for (var i = 1; i < n; i++)
            {
                var index = 0;
                var min = arr[indexs[0]] * primes[0];
                for (var j = 0; j < primes.Length; j++)
                {
                    var temp = arr[indexs[j]] * primes[j];
                    if (min > temp)
                    {
                        min = temp;
                        index = j;
                    }
                }
                for (var j = 0; j < primes.Length; j++)
                {
                    if (min == arr[indexs[j]] * primes[j])
                        indexs[j]++;
                }
                arr[i] = min;
            }
            return arr[n - 1];
    }
}
public class Solution {
    public int NthSuperUglyNumber(int n, int[] primes) {
            int[] pointers = new int[primes.Length];
            int[] values = new int[primes.Length];
            int[] uglyBag = new int[n];
            uglyBag[0] = 1;

            for(int i = 1; i < n; i++)
            {
                for(int j = 0; j < primes.Length; j++)
                {
                    values[j] = uglyBag[pointers[j]] * primes[j];
                }
                int min = values.Min();
                for(int j = 0; j < primes.Length; j++)
                {
                    if(min == values[j])
                    {
                        pointers[j]++;
                    }
                }
                uglyBag[i] = min;
            }
            return uglyBag[n - 1];
    }
}
*/
