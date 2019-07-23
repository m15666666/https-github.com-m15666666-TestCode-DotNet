using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
最初在一个记事本上只有一个字符 'A'。你每次可以对这个记事本进行两种操作：

Copy All (复制全部) : 你可以复制这个记事本中的所有字符(部分的复制是不允许的)。
Paste (粘贴) : 你可以粘贴你上一次复制的字符。
给定一个数字 n 。你需要使用最少的操作次数，在记事本中打印出恰好 n 个 'A'。输出能够打印出 n 个 'A' 的最少操作次数。

示例 1:

输入: 3
输出: 3
解释:
最初, 我们只有一个字符 'A'。
第 1 步, 我们使用 Copy All 操作。
第 2 步, 我们使用 Paste 操作来获得 'AA'。
第 3 步, 我们使用 Paste 操作来获得 'AAA'。
说明:

n 的取值范围是 [1, 1000] 。 
*/
/// <summary>
/// https://leetcode-cn.com/problems/2-keys-keyboard/
/// 650. 只有两个键的键盘
/// https://blog.csdn.net/libliuis/article/details/84851098
/// https://www.cnblogs.com/0xcafe/p/10087802.html
/// </summary>
class TwoKeysKeyboardSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int MinSteps(int n)
    {
        int ret = 0;
        int i = 2;
        while (1 < n)
        {
            while (n % i == 0)
            {
                n /= i;
                ret += i;
            }
            i++;
        }
        return ret;
    }
}
/*
public class Solution {
    public int MinSteps(int n) {
      int res = 0;
        for (int i = 2; i <= n; i++) {
            while (n % i == 0) {
                res += i;
                n /= i;
            }
        }
        return res;
    }
}
public class Solution {
    public int MinSteps(int n)
    {
        if (n == 1)
        {
            return 0;
        }
        for (int i = 2; i * i <= n; i++)
        {
            if (n % i == 0)
            {
                return i + MinSteps(n / i);
            }
        }
        return n;
    }
}
public class Solution {
    public int MinSteps(int n) {
        int sum = 0;
        int div = 2;
        while(n >= 2){
            if(n % div == 0){
                sum += div;
                n = n / div;
            } else {
                div++;
            }
        }
        return sum;
    }
}
public class Solution {
    public int MinSteps(int n) {
        if (n<=1) return 0;
        var dp = new int[n+1];
        for (int i=2;i<=n;i++) {
            dp[i] = 1;
        }
        for(int i=2;i<=n;i++){
            for(int j=i-1;j>=1;j--){
                if(i % j==0){
                    dp[i]=dp[j]+i/j;
                    break;
                }
            }     
            //dp[i]=Math.Min(dp[i],i);
        }
        return dp[n];   
    }
}

*/
