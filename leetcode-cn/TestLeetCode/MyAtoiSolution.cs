using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// https://leetcode-cn.com/problems/string-to-integer-atoi/
/// 字符串转整数 (atoi)
/// 实现 atoi，将字符串转为整数。
/// </summary>
class MyAtoiSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    public int MyAtoi(string str)
    {
        const int Zero = (int)'0';
        const int Nine = (int)'9';

        if (string.IsNullOrWhiteSpace(str)) return 0;

        str = str.TrimStart();

        long sum = 0;
        int startIndex = 0;
        bool isNegative = false;
        switch (str[0])
        {
            case '-':
                isNegative = true;
                startIndex++;
                break;

            case '+':
                startIndex++;
                break;
        }

        for( int i = startIndex; i < str.Length; i++)
        {
            int asc = (int)str[i];
            if( Zero <= asc && asc <= Nine)
            {
                sum = 10 * sum + (asc - Zero);

                long s = isNegative ? -sum : sum;

                if (s < int.MinValue) return int.MinValue;
                if (int.MaxValue < s) return int.MaxValue;

                continue;
            }

            break;
        }

        return (int)(isNegative ? -sum : sum);
    }
}