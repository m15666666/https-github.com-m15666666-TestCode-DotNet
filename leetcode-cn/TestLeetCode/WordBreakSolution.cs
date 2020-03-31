using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// https://leetcode-cn.com/problems/word-break/
/// 139.单词拆分
/// 给定一个非空字符串 s 和一个包含非空单词列表的字典 wordDict，判定 s 是否可以被空格拆分为一个或多个在字典中出现的单词。
/// 说明：
/// 拆分时可以重复使用字典中的单词。
/// 你可以假设字典中没有重复的单词。
/// </summary>
class WordBreakSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    public bool WordBreak(string s, IList<string> wordDict)
    {
        HashSet<string> set = new HashSet<string>(wordDict);
        Dictionary<int, bool> startIndex2CanWordBreak = new Dictionary<int, bool>();

        return BackTrack(s, 0, 0, set, startIndex2CanWordBreak);
    }

    private bool BackTrack( string s, int startIndex, int charCount, HashSet<string> wordDict, Dictionary<int, bool> startIndex2CanWordBreak )
    {
        if (startIndex2CanWordBreak.ContainsKey(startIndex)) return startIndex2CanWordBreak[startIndex];

        var length = s.Length;
        if (length == charCount) return startIndex2CanWordBreak[startIndex] = true;

        for( int stopIndex = startIndex; stopIndex < length; stopIndex++)
        {
            var count = stopIndex - startIndex + 1;
            var subString = s.Substring(startIndex, count);
            if (!wordDict.Contains(subString)) continue;

            if (BackTrack(s, stopIndex + 1, charCount + count, wordDict, startIndex2CanWordBreak)) return startIndex2CanWordBreak[startIndex] = true;
        }

        return startIndex2CanWordBreak[startIndex] = false;
    }

}