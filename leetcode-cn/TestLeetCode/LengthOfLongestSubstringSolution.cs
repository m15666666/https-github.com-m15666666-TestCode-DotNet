using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// https://leetcode-cn.com/problems/longest-substring-without-repeating-characters/description/
/// 无重复字符的最长子串
/// 给定一个字符串，找出不含有重复字符的最长子串的长度。
/// </summary>
class LengthOfLongestSubstringSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    public int LengthOfLongestSubstring(string s)
    {
        int maxCount = 0;
        StringSegment currentSegment = new StringSegment();
        foreach ( var c in s)
        {
            if ( currentSegment.CharSet.Contains(c) )
            {
                //Console.WriteLine(currentSegment.CharString);
                maxCount = Math.Max(currentSegment.Chars.Count, maxCount);
                currentSegment = currentSegment.GetRights(c);
            }

            currentSegment.CharSet.Add(c);
            currentSegment.Chars.Add(c);
        }

        //Console.WriteLine(currentSegment.CharString);
        return Math.Max(currentSegment.Chars.Count, maxCount);
    }

    public class StringSegment
    {
        public List<char> Chars { get; set; } = new List<char>();

        public HashSet<char> CharSet { get; set; } = new HashSet<char>();

        public string CharString
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                foreach (var c in Chars)
                {
                    sb.Append($"{c},");
                }

                return sb.ToString();
            }
        }

        /// <summary>
        /// 获得字符c后面(右面)的字符串列表
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public StringSegment GetRights( char c )
        {
            if (!CharSet.Contains(c)) return null;

            List<char> chars = new List<char>();
            HashSet<char> charSet = new HashSet<char>();

            bool add = false;
            foreach ( var ar in Chars )
            {
                if (add)
                {
                    chars.Add(ar);
                    charSet.Add(ar);
                    continue;
                }

                if( ar == c)
                {
                    add = true;
                    continue;
                }
            }

            return new StringSegment { Chars = chars, CharSet = charSet };
        }
    }
}