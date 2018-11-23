using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// https://leetcode-cn.com/problems/palindrome-partitioning/
/// 131.分隔回文串
/// 给定一个字符串 s，将 s 分割成一些子串，使每个子串都是回文串。
/// 返回 s 所有可能的分割方案。
/// 示例:
/// 输入: "aab"
/// 输出:
/// [
/// ["aa","b"],
/// ["a","a","b"]
/// ]
/// https://blog.csdn.net/Ding_xiaofei/article/details/81946621
/// </summary>
class PalindromePartitioningSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    public IList<IList<string>> Partition(string s)
    {
        List<IList<string>> ret = new List<IList<string>>();

        if (string.IsNullOrWhiteSpace(s)) return ret;

        Stack<string> stack = new Stack<string>();

        BackTrade(s, 0, 0, stack, ret);

        return ret;
    }

    private void BackTrade( string s, int startIndex, int totalLength, Stack<string> stack, List<IList<string>> ret )
    {
        var length = s.Length;
        {
            //int totalLength = 0;
            if( totalLength == length )
            {
                string[] texts = new string[stack.Count];
                int textIndex = texts.Length - 1;
                foreach (var text in stack)
                {
                    texts[textIndex--] = text;
                    //totalLength += text.Length;
                }
                ret.Add(texts);
                return;
            }
            //if ( length <= startIndex ) return;
        }

        for( int stopIndex = startIndex; stopIndex < length; stopIndex++ )
        {
            var subStringCount = stopIndex - startIndex + 1;
            if (!IsPalindrome(s, startIndex, subStringCount )) continue;
            var subString = s.Substring(startIndex, subStringCount);

            stack.Push(subString);

            BackTrade(s, stopIndex + 1, totalLength + subStringCount, stack, ret);

            stack.Pop();
        }
    }

    private static bool IsPalindrome( string s, int startIndex, int count )
    {
        int stopIndex = startIndex + count - 1;
        while( startIndex <= stopIndex)
        {
            if (s[startIndex++] != s[stopIndex--]) return false;
        }
        return true;
    }
}