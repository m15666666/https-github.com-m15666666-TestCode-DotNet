using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定两个表示复数的字符串。

返回表示它们乘积的字符串。注意，根据定义 i2 = -1 。

示例 1:

输入: "1+1i", "1+1i"
输出: "0+2i"
解释: (1 + i) * (1 + i) = 1 + i2 + 2 * i = 2i ，你需要将它转换为 0+2i 的形式。
示例 2:

输入: "1+-1i", "1+-1i"
输出: "0+-2i"
解释: (1 - i) * (1 - i) = 1 + i2 - 2 * i = -2i ，你需要将它转换为 0+-2i 的形式。 
注意:

输入字符串不包含额外的空格。
输入字符串将以 a+bi 的形式给出，其中整数 a 和 b 的范围均在 [-100, 100] 之间。输出也应当符合这种形式。
*/
/// <summary>
/// https://leetcode-cn.com/problems/complex-number-multiplication/
/// 537. 复数乘法
/// </summary>
class ComplexNumberMultiplicationSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public string ComplexNumberMultiply(string a, string b)
    {
        var cA = ToComplexInt(a);
        var cB = ToComplexInt(b);
        return $"{cA.Real* cB.Real - cA.Imag* cB.Imag}+{cA.Real* cB.Imag + cA.Imag* cB.Real}i";
    }

    private static readonly char[] SplitChars = new char[] { '+', 'i' };
    private static ComplexInt ToComplexInt( string s )
    {
        if (string.IsNullOrEmpty(s)) return new ComplexInt();

        var parts = s.Split(SplitChars, StringSplitOptions.RemoveEmptyEntries);
        if (parts == null || parts.Length < 2) return new ComplexInt();
        return new ComplexInt(int.Parse(parts[0]), int.Parse(parts[1]));
    }
    public struct ComplexInt
    {
        public ComplexInt(int r, int i)
        {
            Real = r;
            Imag = i;
        }
        public int Real;
        public int Imag;
    }
}
/*
public class Solution
{
    public string ComplexNumberMultiply(string a, string b)
    {
        int a1 = Convert.ToInt32(a.Split('+')[0]);
        int a2 = Convert.ToInt32(a.Split('+')[1].Replace("i", ""));
        int b1 = Convert.ToInt32(b.Split('+')[0]);
        int b2 = Convert.ToInt32(b.Split('+')[1].Replace("i", ""));
        return $"{a1 * b1 - a2 * b2}+{a1 * b2 + a2 * b1}i";
    }
}
public class Solution {
    public string ComplexNumberMultiply(string a, string b) {
        string[] left = a.Split('+');
        string[] right = b.Split('+');
        
        left[1] = left[1].Substring(0, left[1].Length - 1);
        right[1] = right[1].Substring(0, right[1].Length - 1);
        
        int w, x, y, z;
        w = int.Parse(left[0]);
        x = int.Parse(left[1]);
        y = int.Parse(right[0]);
        z = int.Parse(right[1]);
        
        int im, re;
        re = w * y - x * z;
        im = x * y + w * z;
        
        return $"{re}+{im}i";
    }
}
public class Solution {
    public string ComplexNumberMultiply(string strA, string strB) {
            string[] str1 = strA.Split("+");
            string[] str2 = strB.Split("+");

            string[] str3 = str1[1].Split("i");
            string[] str4 = str2[1].Split("i");

            //str1[0]*str2[0] +str1[0]*str2[1] + str1[1]* str2[0] + str1[1]* str2[1]
            int a = Convert.ToInt32(str1[0]);
            int x = Convert.ToInt32(str2[0]);
            int b = Convert.ToInt32(str3[0]);
            int y = Convert.ToInt32(str4[0]);
            //(a+ib)×(x+iy)== ax−by + i(bx + ay)      1+i   1-i   1+1
    
            return (a * x - b * y) + "+" +(b*x+a*y)+"i";
    }
}
*/
