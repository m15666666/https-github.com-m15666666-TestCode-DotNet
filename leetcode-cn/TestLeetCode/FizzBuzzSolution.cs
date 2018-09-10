using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// 写一个程序，输出从 1 到 n 数字的字符串表示。
/// </summary>
class FizzBuzzSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    public IList<string> FizzBuzz(int n)
    {
        List<string> ret = new List<string>();

        for (int i = 1; i <= n; i++)
        {
            if (i % 3 == 0 && i % 5 == 0)
            {
                ret.Add("FizzBuzz");
            }
            else if (i % 3 == 0)
            {
                ret.Add("Fizz");
            }
            else if (i % 5 == 0)
            {
                ret.Add("Buzz");
            }
            else
            {
                ret.Add($"{i}");
            }
        }

        return ret;
    }
}