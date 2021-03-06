﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
有一个由小写字母组成的字符串 S，和一个整数数组 shifts。

我们将字母表中的下一个字母称为原字母的 移位（由于字母表是环绕的， 'z' 将会变成 'a'）。

例如·，shift('a') = 'b'， shift('t') = 'u',， 以及 shift('z') = 'a'。

对于每个 shifts[i] = x ， 我们会将 S 中的前 i+1 个字母移位 x 次。

返回将所有这些移位都应用到 S 后最终得到的字符串。

示例：

输入：S = "abc", shifts = [3,5,9]
输出："rpl"
解释： 
我们以 "abc" 开始。
将 S 中的第 1 个字母移位 3 次后，我们得到 "dbc"。
再将 S 中的前 2 个字母移位 5 次后，我们得到 "igc"。
最后将 S 中的这 3 个字母移位 9 次后，我们得到答案 "rpl"。
提示：

1 <= S.length = shifts.length <= 20000
0 <= shifts[i] <= 10 ^ 9
*/
/// <summary>
/// https://leetcode-cn.com/problems/shifting-letters/
/// 848. 字母移位
/// 
/// </summary>
class ShiftingLettersSolution
{
    public void Test()
    {
        var ret = ShiftingLetters("abc", new int[] { 3, 5, 9 });
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public string ShiftingLetters(string S, int[] shifts)
    {
        if (string.IsNullOrEmpty(S) || shifts == null || shifts.Length == 0) return S;

        const char a = 'a';
        int sum = 0;
        char[] ret = new char[S.Length];
        for (int i = ret.Length - 1; -1 < i; i--)
        {
            sum = (sum + shifts[i]) % 26;
            ret[i] = (char)(((S[i] - a + sum) % 26) + a);
        }
        return new string(ret);

        //int[] sums = new int[shifts.Length];
        
        //for (int i = sums.Length - 1; -1 < i; i--)
        //{
        //    sum = (sum + shifts[i]) % 26;
        //    sums[i] = sum;
        //}
        ////char[] ret = S.ToCharArray();
        //char[] ret = new char[S.Length];
        //for (int i = 0; i < ret.Length; i++)
        //    ret[i] = (char)(((S[i] - a + sums[i]) % 26) + a);
        //return new string(ret);
    }
}