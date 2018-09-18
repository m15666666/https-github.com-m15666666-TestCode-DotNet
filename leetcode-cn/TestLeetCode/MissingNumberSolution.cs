using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// 缺失数字
/// 给定一个包含 0, 1, 2, ..., n 中 n 个数的序列，找出 0 .. n 中没有出现在序列中的那个数。
/// </summary>
class MissingNumberSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int MissingNumber(int[] nums)
    {
        Array.Sort(nums);
        int index = 0;

        foreach( var v in nums)
        {
            if (index != v) return index;
            index++;
        }

        return index;
    }
}