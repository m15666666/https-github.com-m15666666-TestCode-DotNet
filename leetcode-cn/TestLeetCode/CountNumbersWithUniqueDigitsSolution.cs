using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
给定一个非负整数 n，计算各位数字都不同的数字 x 的个数，其中 0 ≤ x < 10n 。

示例:

输入: 2
输出: 91 
解释: 答案应为除去 11,22,33,44,55,66,77,88,99 外，在 [0,100) 区间内的所有数字。 
*/
/// <summary>
/// https://leetcode-cn.com/problems/count-numbers-with-unique-digits/
/// 357. 计算各个位数不同的数字个数
/// </summary>
class CountNumbersWithUniqueDigitsSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int CountNumbersWithUniqueDigits(int n)
    {
        if (n < 0) return 0;
        if (n == 0) return 1;
        if (n == 1) return 10;

        int ret = 9;
        int digitCount = n - 1;
        int choiceCount = 9;
        while (0 < digitCount--) ret *= choiceCount--;

        return ret + CountNumbersWithUniqueDigits(n - 1);
    }
}
/*
public class Solution {
    public int CountNumbersWithUniqueDigits(int n) {
        if (n >= 10)
        return 8877691;
        int sum = 1;
        for (int i = 1 ;i <= n ;i++){
            sum = sum + noreply(i);
        }
        return sum;
    }

    public int noreply(int num){
        int sum = 9;
        for (int i = 9;i > 10-num ;i--)
        sum = i * sum;
        return sum;
    }
}

public class Solution {
    //如果每一位都不一样的话，就可用排列组合来做了
    //
    //假设 n = 4, 这区间为[0, 9999]，
    //1000 ~ 9999的个数换算为排列组合为：9 * 9 * 8 * 7;
    //借此方法，可以求个n之前所有的不同的位数的个数，相加即可，注意，最多n = 10, n继续大的话，就不可能每位数都相同了。
    //
    public int CountNumbersWithUniqueDigits(int n) {
        if(n ==0)
            return 1;

        int sum = 10;//总个数记录
        int _cur = 9;//在当前位数之下，不同数字排列组合的个数
        int mod = 9;//当前剩余可组合的数组个数
        while(mod > 0 && n > 1)
        {
            _cur *= mod;
            sum += _cur;
            mod--;
            n--;
        }
        return sum;
    }
}

public class Solution {
    public int CountNumbersWithUniqueDigits(int n) {
        if (n == 0) return 1;
        if (n >= 10) n = 10;
        int[] dp = new int[n];
        int count = 9;
        if (n == 0) return 0;
        dp[0] = 10;
        for (int i=1;i<n;i++) {
            count = count * (10- i);
            dp[i] = dp[i-1] + count;
        }
        return dp[n-1];
    }
}

public class Solution {
    public int CountNumbersWithUniqueDigits(int n) {
            if (n == 0)
            {
                return 1;
            }
            else if (n == 1)
            {
                return 10;
            }
            else
            {
                int result = 9;
                for (int i = 0; i < n-1; i++)
                {
                    result *= (9 - i);
                }
                return result + CountNumbersWithUniqueDigits(n - 1);
            }
    }
}

public class Solution {
    public int CountNumbersWithUniqueDigits(int n)
    {
        if (n == 0)
        {
            return 1;
        }
        if (n == 1)
        {
            return 10;
        }
        int[] dp = new int[n +1];
        dp[0] = 1;
        dp[1] = 10;
        for (int i = 2; i <= Math.Min(n,10); i++)
        {
            dp[i] = dp[i - 1] + (dp[i - 1] - dp[i - 2]) * (11 - i);
        }
        if (n > 10)
        {
            return dp[10];
        }
        return dp[n];
    }
} 
public class Solution {
    public int CountNumbersWithUniqueDigits(int n)
        {
            if (n > 9 || n < 1)
                return 1;
            int[] arr = new int[n];
            arr[0] = 10;
            for (int i = 1; i<n ;i++)
            {
                int num = 9 ,sum = 1;
                for (int j = i; j > 0; j--)
                {
                    sum = num * sum;
                    num--;
                }
                sum *= 9;
                arr[i] = sum + arr[i - 1];
            }
            return arr[n-1];
        }
}
public class Solution {
    public int CountNumbersWithUniqueDigits(int n) {
        if(n==0)
            return 1;
          if (n > 10)
                n = 10;
            var res = 10;
            for (var i = 2; i <= n; i++) {
                var k = 9;
                for (var j = 0; j < i-1; j++) {
                    k *= (9 - j);
                }
                res += k;
            }
            return res;
    }
}
public class Solution {
    public int CountNumbersWithUniqueDigits(int n) {
        if(n == 0){
            return 1;
        }
        int r = 10;
        int s = 9;
        for(int i = 2; i <= n; i ++) {
            s *= (10 - i + 1);
            r += s;
        }
        return r;
    }
}
public class Solution {
    public int CountNumbersWithUniqueDigits(int n)
    {
        if (n < 0)
            return 0;
        if (n == 0)
            return 1;
        if (n == 1)
            return 10;
        if (n == 2)
            return 91;
        if (n == 3)
            return 739;
        if (n == 4)
            return 5275;
        if (n == 5)
            return 32491;
        if (n == 6)
            return 168571;
        if (n == 7)
            return 712891;
        if (n == 8)
            return 2345851;
        if (n == 9)
            return 4791702;
        else
            return 7419072;
    }
}
public class Solution {
    public int CountNumbersWithUniqueDigits (int n) {
        switch(n){
            case 0: return 1;
            case 1: return 10;
            case 2: return 91;
        }
        if(n>=10){
            return CountNumbersWithUniqueDigits(9);
        }
          
        return CountNumbersWithUniqueDigits(n-1)+9*Permutation(9,n-1);
    }
    private int Permutation(int n,int i){
        int s=1;
        for (int k = 0; k < i; k++)
        {
            s*=n-k;
        }
        return s;
    }
}
public class Solution {
    public int CountNumbersWithUniqueDigits(int n) {
        if (n > 10) n = 10;
        var arr = new int[11] { 1, 10, 91, 739, 5275, 32491, 168571, 712891, 2345851, 5611771, 8877691 };
        return arr[n];
    }
}
*/
