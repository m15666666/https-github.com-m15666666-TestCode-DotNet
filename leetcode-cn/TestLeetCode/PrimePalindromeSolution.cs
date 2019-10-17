using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
求出大于或等于 N 的最小回文素数。

回顾一下，如果一个数大于 1，且其因数只有 1 和它自身，那么这个数是素数。

例如，2，3，5，7，11 以及 13 是素数。

回顾一下，如果一个数从左往右读与从右往左读是一样的，那么这个数是回文数。

例如，12321 是回文数。

 

示例 1：

输入：6
输出：7
示例 2：

输入：8
输出：11
示例 3：

输入：13
输出：101
 

提示：

1 <= N <= 10^8
答案肯定存在，且小于 2 * 10^8。
*/
/// <summary>
/// https://leetcode-cn.com/problems/prime-palindrome/
/// 866. 回文素数
/// 
/// </summary>
class PrimePalindromeSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int PrimePalindrome(int N)
    {
        while (true)
        {
            if (N == Reverse(N) && IsPrime(N)) return N;
            N++;

            // 如果当前数字长度为 8，可以跳过检查，因为不存在 8 长度的素数
            if (10_000_000 <= N && N < 100_000_000) N = 100_000_000;
        }
    }

    private static readonly HashSet<int> Primes = new HashSet<int> { 2,3,5,7,11,17,19};
    private static bool IsPrime(int N)
    {
        if (N < 2) return false;
        if (Primes.Contains(N)) return true;

        int upper = (int)Math.Sqrt(N);
        for (int d = 2; d <= upper; ++d)
            if (N % d == 0) return false;
        return true;
    }

    private static int Reverse(int N)
    {
        int ret = 0;
        while (0 < N)
        {
            ret = 10 * ret + (N % 10);
            N /= 10;
        }
        return ret;
    }
}
/*
方法一： 遍历回文串
思路

假设有一个回文串 XX，下一个回文串是什么？

每个 dd 长度的回文串都有一个 回文根，回文根为前 k = \frac{d+1}{2}k= 
2
d+1
?	
  个数字。下一个回文串一定是由下一个回文根组成的。

举个例子，如果 123123 是 1232112321 的回文根，那么下一个回文串 1242112421 的回文根就是 124124。

需要注意一个回文根可能对应两个回文串，例如 123321123321，1232112321 的回文根就都是 123123。

算法

对于每个 回文根，找对应的两个回文串（一个奇数长度，一个偶数长度）。对于 kk 长度的回文根，会产生长度为 2*k - 12?k?1 和 2*k - 12?k?1 的回文串。

当检查回文串的时候，需要先检查小的 2k - 12k?1 长度的，这里直接把数字变成字符串来检查是否对称。

至于检查素数，这里用的是常见的 O(\sqrt{N})O( 
N
?	
 ) 复杂度的算法来检查是不是素数，即检查小于 \sqrt{N} 
N
?	
  的数中有没有能整除 NN 的。

JavaPython
class Solution {
    public int primePalindrome(int N) {
        for (int L = 1; L <= 5; ++L) {
            //Check for odd-length palindromes
            for (int root = (int)Math.pow(10, L - 1); root < (int)Math.pow(10, L); ++root) {
                StringBuilder sb = new StringBuilder(Integer.toString(root));
                for (int k = L-2; k >= 0; --k)
                    sb.append(sb.charAt(k));
                int x = Integer.valueOf(sb.toString());
                if (x >= N && isPrime(x))
                    return x;
                    //If we didn't check for even-length palindromes:
                    //return N <= 11 ? min(x, 11) : x
            }

            //Check for even-length palindromes
            for (int root = (int)Math.pow(10, L - 1); root < (int)Math.pow(10, L); ++root) {
                StringBuilder sb = new StringBuilder(Integer.toString(root));
                for (int k = L-1; k >= 0; --k)
                    sb.append(sb.charAt(k));
                int x = Integer.valueOf(sb.toString());
                if (x >= N && isPrime(x))
                    return x;
            }
        }

        throw null;
    }

    public boolean isPrime(int N) {
        if (N < 2) return false;
        int R = (int) Math.sqrt(N);
        for (int d = 2; d <= R; ++d)
            if (N % d == 0) return false;
        return true;
    }
}
复杂度分析

时间复杂度： O(N)O(N)，其中大于最大 N 的素数为 100030001，这决定了时间复杂度的上限。

空间复杂度： O(\log N)O(logN)。

方法二： 数学法
算法

遍历所有数字，检查是不是回文串。如果是，检查是不是素数，如果当前数字长度为 8，可以跳过检查，因为不存在 8 长度的素数。

JavaPython
class Solution {
    public int primePalindrome(int N) {
        while (true) {
            if (N == reverse(N) && isPrime(N))
                return N;
            N++;
            if (10_000_000 < N && N < 100_000_000)
                N = 100_000_000;
        }
    }

    public boolean isPrime(int N) {
        if (N < 2) return false;
        int R = (int) Math.sqrt(N);
        for (int d = 2; d <= R; ++d)
            if (N % d == 0) return false;
        return true;
    }

    public int reverse(int N) {
        int ans = 0;
        while (N > 0) {
            ans = 10 * ans + (N % 10);
            N /= 10;
        }
        return ans;
    }
}

复杂度分析

时间复杂度： O(N)O(N)。

空间复杂度： O(1)O(1)。


public class Solution {
    public int PrimePalindrome(int N) {
        if (N == 1) {
            return 2;
        }
        
        int num = FirstHui(N);        
        while (num < 200000000) {
            // Console.WriteLine(num);
            bool prime = Prime(num);
            if (prime) {
                return num;
            } else {
                num = NextHui(num);
            }
        }
        return 0;
    }
    
    bool Prime(int num) {
        
        int spr = (int)Math.Sqrt(num);
        for (int i = 2; i <= spr; i ++) {
            if (num%i == 0) {
                return false;
            }
        }
        return true;
    }
    
    int FirstHui(int num) {
        int n = Wei(num);
        int[] arr = new int[n];
        
        int temp = num;
        for (int i = n - 1; i >= 0; i --) {
            arr[i] = temp%10;
            temp = temp/10;
        }
        
        // for (int i = 0; i < n; i ++) {
        //     Console.WriteLine(" " + arr[i]);
        // }
        
        int s = 0;
        int e = n - 1;
        while(s < e) {
            arr[e] = arr[s];
            s ++;
            e --;
        }
        
        int res = 0;
        for (int i = 0; i < n; i ++) {
            res = res * 10 + arr[i];
        }
        
        while(res < num) {
            res = NextHui(res);
        }
        
        return res;
    }
    
    int NextHui(int hui) {
        // return 0;
        int t = Wei(hui) - 1;
        
        int n = t + 1;
        
        int leftHalfNum = hui/(int)Math.Pow(10, n/2);
        // Console.WriteLine(" leftHalf " + leftHalfNum);
        int t_left = Wei(leftHalfNum);
        // Console.WriteLine(" t_left " + t_left);
        leftHalfNum += 1;
        // Console.WriteLine(" t_left2 " + (int)Math.Log(leftHalfNum, 10));
        
        if (Wei(leftHalfNum) > t_left) {
            return (int)Math.Pow(10, t+1) + 1;
        } else {
            int[] arr = new int[n];
            int l = t/2;
            int r;
            if (t %2 == 0) {
                r = l;
            } else {
                r = l + 1;
            }
            
            while(leftHalfNum > 0) {
                int mod = leftHalfNum%10;
                leftHalfNum /= 10;
                arr[l] = mod;
                arr[r] = mod;
                l--;
                r++;
            }
            
            int res = 0;
            for (int i = 0; i < n; i ++) {
                res = res * 10 + arr[i];
            }
            
            return res;
        }
    }
    
    int Wei(int num) {
        int res = 0;
        while(num > 0) {
            res ++;
            num /= 10;
        }
        return res;
    }
}
 
*/
