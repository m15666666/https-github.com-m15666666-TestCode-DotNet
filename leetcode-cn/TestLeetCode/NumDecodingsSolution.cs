using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// https://leetcode-cn.com/problems/decode-ways/
/// 91.解码方法
/// 一条包含字母 A-Z 的消息通过以下方式进行了编码：
/// 'A' -> 1
/// 'B' -> 2
/// ...
/// 'Z' -> 26
/// 给定一个只包含数字的非空字符串，请计算解码方法的总数。
/// https://blog.csdn.net/zrh_CSDN/article/details/82495250
/// </summary>
class NumDecodingsSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    public int NumDecodings(string s)
    {
        if (string.IsNullOrWhiteSpace(s)) return 0;

        char? last1 = null;
        int last1Possibilities = 1;
        int last2Possibilities = 0;

        foreach( var c in s)
        {
            var p = c == '0' ? 0 : last1Possibilities;
            if( last1 != null && (last1 == '1' || (last1 == '2' && c < '7') ))
            {
                p += last2Possibilities;
            }

            last1 = c;
            last2Possibilities = last1Possibilities;
            last1Possibilities = p;
        }

        return last1Possibilities;
    }
}