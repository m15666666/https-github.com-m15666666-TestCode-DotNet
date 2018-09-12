using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// 罗马数字转整数
/// </summary>
class RomanToIntSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    public int RomanToInt(string s)
    {
        var roman2int = Roman2Int;
        int val = 0;
        for (int i = 0; i < s.Length; i++)
        {
            if (i == s.Length - 1 || roman2int[s[i + 1]] <= roman2int[s[i]])
                val += roman2int[s[i]];
            else
                val -= roman2int[s[i]];
        }
        return val;
    }

    private static readonly Dictionary<char, int> Roman2Int = new Dictionary<char, int> {
        { 'I', 1},
        { 'V', 5},
        { 'X', 10},
        { 'C', 100},
        { 'M', 1000},
        { 'L', 50},
        { 'D', 500},
    };
}