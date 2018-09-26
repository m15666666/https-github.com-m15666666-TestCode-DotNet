using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// https://leetcode-cn.com/problems/integer-to-roman/
/// 整数转罗马数字
/// https://blog.csdn.net/net_wolf_007/article/details/51770112
/// </summary>
class IntToRomanSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    private static readonly int[] _number = { 1000, 900, 500, 400, 100, 90, 50, 40, 10, 9, 5, 4, 1 };
    private static readonly string[] _flags = { "M", "CM", "D", "CD", "C", "XC", "L", "XL", "X", "IX", "V", "IV", "I" };
    public string IntToRoman(int num)
    {
        if( num <= 0 ) return string.Empty;

        StringBuilder ret = new StringBuilder();
        
        for (int i = 0; i < _number.Length && 0 < num; i++)
        {
            var threshold = _number[i];
            if ( num < threshold ) continue;

            while ( threshold <= num )
            {
                num -= threshold;

                ret.Append(_flags[i]);
            }
        }
        return ret.ToString(); 
    }

}