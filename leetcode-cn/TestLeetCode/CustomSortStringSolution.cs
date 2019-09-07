using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
字符串S和 T 只包含小写字符。在S中，所有字符只会出现一次。

S 已经根据某种规则进行了排序。我们要根据S中的字符顺序对T进行排序。更具体地说，如果S中x在y之前出现，那么返回的字符串中x也应出现在y之前。

返回任意一种符合条件的字符串T。

示例:
输入:
S = "cba"
T = "abcd"
输出: "cbad"
解释: 
S中出现了字符 "a", "b", "c", 所以 "a", "b", "c" 的顺序应该是 "c", "b", "a". 
由于 "d" 没有在S中出现, 它可以放在T的任意位置. "dcba", "cdba", "cbda" 都是合法的输出。
注意:

S的最大长度为26，其中没有重复的字符。
T的最大长度为200。
S和T只包含小写字符。
*/
/// <summary>
/// https://leetcode-cn.com/problems/custom-sort-string/
/// 791. 自定义字符串排序
/// https://blog.csdn.net/start_lie/article/details/89486506
/// </summary>
class CustomSortStringSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public string CustomSortString(string S, string T)
    {
        byte[] charIndex = new byte['z' + 1];
        Array.Fill<byte>(charIndex, 0);
        for (byte i = 1; i <= S.Length; i++)
        {
            charIndex[S[i-1]] = i;
        }

        char[] ret = new char[T.Length];
        int extraCharIndex = ret.Length - 1;
        byte[] charCounts = new byte[S.Length + 1];
        Array.Fill<byte>(charCounts, 0);

        for (int i = 0; i < ret.Length; i++)
        {
            var c = T[i];
            int index = charIndex[c];
            if (index == 0)
            {
                ret[extraCharIndex--] = c;
                continue;
            }
            charCounts[index]++;
        }

        for (int i = 1, x = 0; i < charCounts.Length; i++)
        {
            int count = charCounts[i];
            if (0 == count) break;

            var c = S[i - 1];
            while ( 0 < count-- )
            {
                ret[x++] = c;
            }
        }
        return new string(ret);
    }
}
/*
public class Solution {
    public string CustomSortString(string S, string T) {
        
        int m = S.Length,n = T.Length;
        int[] map = new int[26];      //记录T中的字符个数
        int[] map1 = new int[26];   //记录S是否包含某个字符
        for(int i = 0;i < m;i++){
            char c = S[i];
            map1[c-'a']++;
        }
        StringBuilder sb = new StringBuilder();
        for(int i = 0;i < n;i++){
            char c = T[i];
            int t = c -'a';
            map[t]++;
            if(map1[t] == 0) sb.Append(c);            //如果S中不包含该字符，直接加上
        } 
       //按照S中的顺序，包含几次加几次
        for(int i = 0;i < m;i++){
            char c = S[i];
            int cnt = map[c-'a'];
            if(cnt > 0){
                for(int k = 0;k < cnt;k++)
                    sb.Append(c);
            }
        }
        return sb.ToString();
    }
} 
*/
