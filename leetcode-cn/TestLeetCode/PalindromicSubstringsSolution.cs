using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个字符串，你的任务是计算这个字符串中有多少个回文子串。

具有不同开始位置或结束位置的子串，即使是由相同的字符组成，也会被计为是不同的子串。

示例 1:

输入: "abc"
输出: 3
解释: 三个回文子串: "a", "b", "c".
示例 2:

输入: "aaa"
输出: 6
说明: 6个回文子串: "a", "a", "a", "aa", "aa", "aaa".
注意:

输入的字符串长度不会超过1000。 
*/
/// <summary>
/// https://leetcode-cn.com/problems/palindromic-substrings/
/// 647. 回文子串
/// https://blog.csdn.net/OneDeveloper/article/details/79853156
/// </summary>
class PalindromicSubstringsSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int CountSubstrings(string s)
    {
        if (string.IsNullOrEmpty(s)) return 0;

        int ret = 0;
        int length = s.Length;
        for (int i = 0; i < length; i++)
        {
            ret += Count(s, i, i);//以 s(i) 为中心向两边扩散
            ret += Count(s, i, i + 1); //以 s(i) 与 s(i+1) 为中心向两边扩散
        }
        return ret;
    }
    private static int Count(string s, int beg, int end)
    {
        int count = 0;
        while (beg >= 0 && end < s.Length && s[beg] == s[end])
        {
            count++;
            beg--;
            end++;
        }
        return count;
    }
}
/*
public class Solution
{
	public int CountSubstrings(string s)
	{
		int count=0;
		for (int i=0;i < s.Length;i++)
		{
			count++;
			int j=1;
			while (i + j < s.Length && i - j >= 0)
			{
				if (s[i + j] == s[i - j])
				{
					count++;
					j++;
				}
				else
					break;
			}
			int t=i;
			int k=t + 1;
			while (t >= 0 && k < s.Length)
			{
				if (s[t] == s[k])
				{
					count++;
					t--;
					k++;
				}
				else
					break;
			}
		}
		return count;
	}
}
public class Solution {
    public int CountSubstrings(string s) {
        if(s.Length==0){
            return 0;
        }
        
        int n=s.Length;
        int count=0;
        for(int i=0;i<n;i++){
            int begin=i;
            int end=i;
            while(begin>=0 && end<n && s[begin]==s[end]){
                begin--;
                end++;
                count++;
            }
            
            int a=i;
            int b=i+1;
            while(a>=0 && b<n && s[a]==s[b]){
                a--;
                b++;
                count++;
            }
        }
        return count;
    }
}
public class Solution {
    public int CountSubstrings(string s) {
        int n = s.Length;
        int[,] dp = new int[n+1,n+1];
        
        for (int i = 0; i < n+1; i ++) {
            dp[0,i] = 1;
            dp[1,i] = 1;
        }
        
       // for (int j = 2; j < n+1; j ++) {
       //      for (int i = 0; i < n + 1; i ++) {
       //          if (s[i] == s[i+j-1] && dp[j-1][i+1] == 1) {
       //              dp[j][i]
       //          }
       //       }
       //  }
        
        for (int i = 2; i < n+1; i ++) {
            for (int j = 0; j + i <= n; j ++) {
                if (s[j] == s[i+j-1] && dp[i-2,j+1] == 1) {
                    dp[i,j] = 1;
                }
            }
        }
        
        // for (int i = 0; i < n + 1; i ++) {
        //     for (int j = 0; j < n + 1; j ++) {
        //         Console.Write(" " + dp[i, j]);
        //     }
        //     Console.WriteLine();
        // }
        
        int res = 0;
        for (int i = 1; i < n + 1; i ++) {
            for (int j = 0; j < n; j ++) {
                res += dp[i,j];
            }
        }
        return res;
    }
}
public class Solution {
    public int CountSubstrings(string s) {
       int count=0;
       for(int i=0;i<s.Length;i++)
       {
           //回文串是奇数
           count+= extendString(i,i,s);
           //回文串是偶数
           count+= extendString(i,i+1,s);
       }
        return count;
    }
    public int extendString(int start,int end,string s)
    {
        //这叫做马拉车，即start--，end++
        int count=0;
        while(start>=0&&end<s.Length&&s[start]==s[end])
        {
            start--;
            end++;
            count++;
        }
        return count;
    }
}
*/
