using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
编写一个程序，找出第 n 个丑数。

丑数就是只包含质因数 2, 3, 5 的正整数。

示例:

输入: n = 10
输出: 12
解释: 1, 2, 3, 4, 5, 6, 8, 9, 10, 12 是前 10 个丑数。
说明:  

1 是丑数。
n 不超过1690。
*/
/// <summary>
/// https://leetcode-cn.com/problems/ugly-number-ii/
/// 264. 丑数 II
/// https://blog.csdn.net/qq_34364995/article/details/80680354
/// </summary>
class UglyNumberIISolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int NthUglyNumber(int n)
    {
        if (n < 1) return 0;

        int[] results = new int[n];
        int index = 0;
        var lastUgly = 1;
        results[index++] = 1;
        int index2 = 0, index3 = 0, index5 = 0;
        while( index < n)
        {
            var v2 = results[index2] * 2;
            var v3 = results[index3] * 3;
            var v5 = results[index5] * 5;

            lastUgly = Math.Min( Math.Min( v2, v3 ), v5 );
            results[index++] = lastUgly;

            if (v2 == lastUgly) ++index2;
            if (v3 == lastUgly) ++index3;
            if (v5 == lastUgly) ++index5;
        }
        return lastUgly;
    }
}
/*
//别人的算法
public class Solution {
    public int NthUglyNumber(int n) {
       int[] ugly = new int[n];
            int n2 = 1, i2 = 0;
            int n3 = 1, i3 = 0;
            int n5 = 1, i5 = 0;
            for (int i = 0; i < n; i++)
            {
                ugly[i] = Math.Min(Math.Min(n2, n3), n5);
                if (ugly[i] == n2) n2 = ugly[i2++] * 2;
                if (ugly[i] == n3) n3 = ugly[i3++] * 3;
                if (ugly[i] == n5) n5 = ugly[i5++] * 5;
            }
            return ugly[n - 1];
    }
}
public class Solution {
    public int NthUglyNumber(int n) {
        int[] num=new int[n];
        num[0]=1;
        int index2=0,index3=0,index5=0;
        for(int i=1;i<n;i++){
            num[i]=min(num[index2]*2,num[index3]*3,num[index5]*5);
            if(num[i]==num[index2]*2) index2++;
            if(num[i]==num[index3]*3) index3++;
            if(num[i]==num[index5]*5) index5++;
        }
        return num[n-1];
        
    }
    
    public int min(int a,int b,int c){
        return a>b?(b>c?c:b):(a>c?c:a);
    }
}
*/
