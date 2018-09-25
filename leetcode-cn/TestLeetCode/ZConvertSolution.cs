using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// https://leetcode-cn.com/problems/zigzag-conversion/description/
/// Z字形变换
/// 将字符串 "PAYPALISHIRING" 以Z字形排列成给定的行数：
/// https://leetcode-cn.com/problems/zigzag-conversion/solution/
/// </summary>
class ZConvertSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    public string Convert(string s, int numRows)
    {
        if(numRows == 1) return s;

        StringBuilder ret = new StringBuilder();
        int n = s.Length;
        int cycleLen = 2 * numRows - 2;

        for (int i = 0; i < numRows; i++)
        {
            for (int j = 0; j + i < n; j += cycleLen)
            {
                var firstIndexInCycle = j + i;
                ret.Append(s[firstIndexInCycle]);

                var secondIndexInCycle = j + cycleLen - i;
                if (i != 0 && i != numRows - 1 && secondIndexInCycle < n)
                    ret.Append(s[secondIndexInCycle]);
            }
        }
        return ret.ToString();
    }

}