using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// https://leetcode-cn.com/problems/repeated-dna-sequences/
/// 187. 重复的DNA序列
/// 所有 DNA 由一系列缩写为 A，C，G 和 T 的核苷酸组成，例如：“ACGAATTCCG”。在研究 DNA 时，识别 DNA 中的重复序列有时会对研究非常有帮助。
/// 编写一个函数来查找 DNA 分子中所有出现超多一次的10个字母长的序列（子串）。
/// 示例:
/// 输入: s = "AAAAACCCCCAAAAACCCCCCAAAAAGGGTTT"
/// 输出: ["AAAAACCCCC", "CCCCCAAAAA"]
/// https://blog.csdn.net/w8253497062015/article/details/80672207
/// </summary>
class RepeatedDNASequencesSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    public IList<string> FindRepeatedDnaSequences(string s)
    {
        List<string> ret = new List<string>();
        if (s == null || s.Length < 10) return ret;

        Dictionary<string, int> dnaCount = new Dictionary<string, int>();
        int lastIndex = s.Length - 10;
        for( int i = 0; i <= lastIndex; i++)
        {
            var str = s.Substring(i, 10);
            if (dnaCount.ContainsKey(str)) dnaCount[str] = 2;
            else dnaCount[str] = 1;
        }
        foreach( var pair in dnaCount)
        {
            if (1 < pair.Value) ret.Add(pair.Key);
        }
        return ret;
    }
}