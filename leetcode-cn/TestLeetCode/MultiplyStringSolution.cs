using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定两个以字符串形式表示的非负整数 num1 和 num2，返回 num1 和 num2 的乘积，它们的乘积也表示为字符串形式。

示例 1:

输入: num1 = "2", num2 = "3"
输出: "6"
示例 2:

输入: num1 = "123", num2 = "456"
输出: "56088"
说明：

num1 和 num2 的长度小于110。
num1 和 num2 只包含数字 0-9。
num1 和 num2 均不以零开头，除非是数字 0 本身。
不能使用任何标准库的大数类型（比如 BigInteger）或直接将输入转换为整数来处理。
*/
/// <summary>
/// https://leetcode-cn.com/problems/multiply-strings/
/// 43. 字符串相乘
/// 
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
/*

优化版竖式(打败99.4%)
breezean
发布于 7 个月前
24.7k
方法一：普通竖式
思路

竖式运算思想，以 num1 为 123，num2 为 456 为例分析：



遍历 num2 每一位与 num1 进行相乘，将每一步的结果进行累加。

注意：

num2 除了第一位的其他位与 num1 运算的结果需要 补0
计算字符串数字累加其实就是 415. 字符串相加
实现

class Solution {
public String multiply(String num1, String num2)
{
    if (num1.equals("0") || num2.equals("0"))
    {
        return "0";
    }
    // 保存计算结果
    String res = "0";

    // num2 逐位与 num1 相乘
    for (int i = num2.length() - 1; i >= 0; i--)
    {
        int carry = 0;
        // 保存 num2 第i位数字与 num1 相乘的结果
        StringBuilder temp = new StringBuilder();
        // 补 0 
        for (int j = 0; j < num2.length() - 1 - i; j++)
        {
            temp.append(0);
        }
        int n2 = num2.charAt(i) - '0';

        // num2 的第 i 位数字 n2 与 num1 相乘
        for (int j = num1.length() - 1; j >= 0 || carry != 0; j--)
        {
            int n1 = j < 0 ? 0 : num1.charAt(j) - '0';
            int product = (n1 * n2 + carry) % 10;
            temp.append(product);
            carry = (n1 * n2 + carry) / 10;
        }
        // 将当前结果与新计算的结果求和作为新的结果
        res = addStrings(res, temp.reverse().toString());
    }
    return res;
}

public String addStrings(String num1, String num2)
{
    StringBuilder builder = new StringBuilder();
    int carry = 0;
    for (int i = num1.length() - 1, j = num2.length() - 1;
         i >= 0 || j >= 0 || carry != 0;
         i--, j--)
    {
        int x = i < 0 ? 0 : num1.charAt(i) - '0';
        int y = j < 0 ? 0 : num2.charAt(j) - '0';
        int sum = (x + y + carry) % 10;
        builder.append(sum);
        carry = (x + y + carry) / 10;
    }
    return builder.reverse().toString();
}
}
复杂度分析

时间复杂度：O(M N)。M,NM,N 分别为 num1 和 num2 的长度。
空间复杂度：O(M+N)。用于存储计算结果。
方法二：优化竖式
该算法是通过两数相乘时，乘数某位与被乘数某位相乘，与产生结果的位置的规律来完成。具体规律如下：

乘数 num1 位数为 MM，被乘数 num2 位数为 NN， num1 x num2 结果 res 最大总位数为 M+N
num1[i] x num2[j] 的结果为 tmp(位数为两位，"0x","xy"的形式)，其第一位位于 res[i + j]，第二位位于 res[i + j + 1]。
结合下图更容易理解



class Solution
{
    public String multiply(String num1, String num2)
    {
        if (num1.equals("0") || num2.equals("0"))
        {
            return "0";
        }
        int[] res = new int[num1.length() + num2.length()];
        for (int i = num1.length() - 1; i >= 0; i--)
        {
            int n1 = num1.charAt(i) - '0';
            for (int j = num2.length() - 1; j >= 0; j--)
            {
                int n2 = num2.charAt(j) - '0';
                int sum = (res[i + j + 1] + n1 * n2);
                res[i + j + 1] = sum % 10;
                res[i + j] += sum / 10;
            }
        }

        StringBuilder result = new StringBuilder();
        for (int i = 0; i < res.length; i++)
        {
            if (i == 0 && res[i] == 0) continue;
            result.append(res[i]);
        }
        return result.toString();
    }
}
复杂度分析

时间复杂度：O* (* M N*)O∗(∗MN∗)。M,NM,N 分别为 num1 和 num2 的长度。
空间复杂度：O* (M+N)O∗(M+N)。用于存储计算结果。
算法比较
算法  提交结果 执行用时    内存消耗 语言
普通竖式 通过  48 ms	43.8 MB Java
优化竖式 通过  4 ms	36.6 MB Java
虽然两者时间复杂度和空间复杂度相同，但优化竖式执行速度提高很明显，普通竖式耗时主要还是对每步计算的字符串相加这个过程。

下一篇：Python 字符串暴力模拟竖式乘法计算过程

击败100%
诺。
发布于 2 个月前
3.7k
解题思路
模拟乘法，将所有数据不进位直接存入数组，最后统一进位即可。

QQ截图20200218205851.png

代码
class Solution
{
    public:
    string multiply(string num1, string num2)
    {
        if (num1 == "0" || num2 == "0")
        {
            return "0";
        }
        reverse(num1.begin(), num1.end());
        reverse(num2.begin(), num2.end());

        int s1 = num1.size(), s2 = num2.size();

        int ans[s1 + s2] = { 0 };
        for (int i = 0; i < s1; i++)
        {
            for (int j = 0; j < s2; j++)
            {
                ans[i + j] += (num1[i] - '0') * (num2[j] - '0');
            }
        }

        for (int i = 0; i < s1 + s2; i++)
        {
            if (ans[i] > 9)
            {
                int t = ans[i];
                ans[i] = t % 10;
                ans[i + 1] += (t / 10);
            }
        }

        int pos = (ans[s1 + s2 - 1] == 0 ? s1 + s2 - 2 : s1 + s2 - 1);

        string aa = "";
        for (; pos >= 0; --pos)
        {
            aa += (ans[pos] + '0');
        }
        return aa;
    }
};

下一篇：参考C大神的思路写的，大神的思路很棒

*/