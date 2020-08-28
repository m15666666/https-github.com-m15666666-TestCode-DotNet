using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
统计所有小于非负整数 n 的质数的数量。

示例:

输入: 10
输出: 4
解释: 小于 10 的质数一共有 4 个, 它们是 2, 3, 5, 7 。 
 
*/
/// <summary>
/// https://leetcode-cn.com/problems/count-primes/
/// 204. 计数质数
/// 统计所有小于非负整数 n 的质数的数量。
/// https://blog.csdn.net/qq_27093465/article/details/59576577
/// https://blog.csdn.net/jianchilu/article/details/79439868
/// </summary>
class CountPrimesSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    public int CountPrimes(int n)
    {
        var primesHelper = new PrimesHelper(n-1);
        return primesHelper.CountPrimes();
    }

    public class PrimesHelper
    {
        private int N { get; set; }

        private List<int> Primes { get; set; }  = new List<int>();
        private int _currentBiggestPrime = 7;
        private int _primeCount = 0;

        public PrimesHelper(int n)
        {
            N = n;

            Primes.AddRange(new []{2,3,5,7});

            Init();
        }

        private void Init()
        {
            int n = N;
            if (n < 3) return;

            int squareRoot = 2;
            int square = squareRoot * squareRoot;
            _primeCount = 1;
            int currentN = 3;

            while (true)
            {
                // 计算currentN是否是质数
                if (currentN <= square)
                {
                    // do ...
                    bool isPrime = true;
                    foreach (var prime in Primes)
                    {
                        if (squareRoot < prime) break;
                        if (currentN % prime == 0)
                        {
                            isPrime = false;
                            break;
                        }
                    }

                    if (isPrime)
                    {
                        _primeCount++;
                        if (_currentBiggestPrime < currentN)
                        {
                            _currentBiggestPrime = currentN;
                            Primes.Add(currentN);
                        }
                    }

                    currentN++;
                    if (n < currentN) return;
                }
                else
                {
                    squareRoot++;
                    square = squareRoot * squareRoot;
                    continue;
                }
            }
        }

        public int CountPrimes()
        {
            int n = N;
            if (n < 2) return 0;
            if (n == 2) return 1;

            return _primeCount;
        }
    }
}
/*

public class Solution {
    public int CountPrimes(int n) {
		int count = 0, s = 0;

		if (n == 10000)
			return 1229;
		if (n == 499979)
			return 41537;
		if (n == 999983)
			return 78497;
		if (n == 1500000)
			return 114155;

		for(int i = 0; i < n; i++)
		{
			count = 0;

			for(int j = 1; j <= i;j++ )
			{
				if(i % j == 0)
				{
					count++;
				}
			}

			if(count==2)
			{
				s++;
			}
		}

		return s;
    }
}

public class Solution {
    private List<int> primes = new List<int>();
    public int CountPrimes(int n) {
        if (n == 499979)
        return 41537;
        if (n == 999983)
        return 78497;
        if (n == 1500000)
        return 114155;
        if (n < 3) return 0;
        else if (n == 3) return 1;
        bool flag;
        primes.Add(2);
        for (int i = 3; i < n; i += 2) {
            flag = false;
            foreach (int prime in primes) {
                if (i % prime == 0) {
                    goto l1;
                }
                if (i / 2 < prime)
                    break;
            }
            primes.Add(i);
            l1:
            i = i;
        }
        return primes.Count;
    }
}

public class Solution {
    public int CountPrimes(int n) {
        int result = 0, sqrt_n = (int)Math.Sqrt(n);
            bool[] b = new bool[n];   // 初始化默认值都为 false，为质数标记
            b.Initialize();
            if (2 < n) result++; // 如果大于 2 则一定拥有 2 这个质数
            for (int i = 3; i < n; i += 2)
            {  // 从 3 开始遍历，且只遍历奇数
                if (!b[i])
                {  // 是质数
                    if (i <= sqrt_n)//不大于根号n
                        for (int j = i; i * j < n; j += 2)
                            b[i * j] = true;    // 将当前质数的奇数倍都设置成非质数标记 true
                    result++;   // 质数个数 +1
                }
            }
            return result;
    }
}

public class Solution {
    public int CountPrimes(int n) {

        if(n < 3) return 0;

        bool[] prime = new bool[n];
        Array.Fill(prime,true);

        for(int i = 2; i*i<n; i++){
            
            if(prime[i]){

                for(int j = i*i; j < n; j+=i){
                    prime[j] = false;
                }
            }

        }
        int ans = 0;  
        for(int i = 2; i<n; i++){
           if(prime[i]){
               ans++;
           }
        }

        return ans;
    }
}

public class Solution {
    public int CountPrimes(int n) {
        int sum=0;
        int[] nums=new int[n+1];
        for(int i=2;i<n;i++)
        {
            if(nums[i]==0)
            {
                for(int j=2;i*j<n;j++)
                {
                    nums[i*j]=1;
                }
                sum++;
            }
        }
        return sum;
    }
}
 
 
*/