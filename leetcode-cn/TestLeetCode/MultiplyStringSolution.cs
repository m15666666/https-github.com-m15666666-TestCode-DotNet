using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// https://leetcode-cn.com/problems/multiply-strings/
/// 字符串相乘
/// 给定两个以字符串形式表示的非负整数 num1 和 num2，
/// 返回 num1 和 num2 的乘积，它们的乘积也表示为字符串形式。
/// https://blog.csdn.net/mijian1207mijian/article/details/51597479
/// </summary>
class MultiplyStringSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    public string Multiply(string num1, string num2)
    {
        if (string.IsNullOrEmpty(num1) || string.IsNullOrEmpty(num2) || num1[0] == '0' || num2[0] == '0') return "0";

        int len1 = num1.Length;
        int len2 = num2.Length;
        int[] num = new int[len1 + len2];

        int offset = -1;
        for ( var j = len2 - 1; -1 < j; j--)
        {
            offset++;
            var v2 = num2[j] - '0';
            
            var index = num.Length - 1 - offset;
            int carry = 0;
            for ( var i = len1 - 1; -1 < i; i--)
            {
                var v1 = num1[i] - '0';
                var multiplyV = num[index] + v1 * v2 + carry;
                carry = multiplyV / 10;
                num[index--] = multiplyV % 10;
            }
            if (0 < carry) num[index] = carry;
        }

        StringBuilder sb = new StringBuilder();
        bool firstNonzero = false;

        //将数组转换为字符串，如果第一位是0，去掉
        for (int i = 0; i < num.Length; i++)
        {
            //找出第一个非0的数字
            if (!firstNonzero && num[i] == 0)
                continue;
            else
            {
                sb.Append(num[i]);
                firstNonzero = true;
            }
        }

        return sb.ToString();
    }
}