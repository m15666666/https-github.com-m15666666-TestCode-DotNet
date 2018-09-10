using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
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