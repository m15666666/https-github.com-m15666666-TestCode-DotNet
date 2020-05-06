using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个由整数组成的非空数组所表示的非负整数，在该数的基础上加一。

最高位数字存放在数组的首位， 数组中每个元素只存储单个数字。

你可以假设除了整数 0 之外，这个整数不会以零开头。

示例 1:

输入: [1,2,3]
输出: [1,2,4]
解释: 输入数组表示数字 123。
示例 2:

输入: [4,3,2,1]
输出: [4,3,2,2]
解释: 输入数组表示数字 4321。
*/
/// <summary>
/// https://leetcode-cn.com/problems/plus-one/
/// 66. 加一
/// 
/// 
/// </summary>
class PlusOneSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int[] PlusOne(int[] digits) 
    {
        for (int i = digits.Length - 1; -1 < i; i--) 
        {
            if(digits[i] == 9)
            {
                digits[i] = 0;
                continue;
            }
            digits[i]++;
            return digits;
        }
        digits = new int[digits.Length + 1];
        digits[0] = 1;
        return digits;
    }
}
/*

Java 数学解题
YHHZW
发布于 1 年前
33.5k
根据题意加一，没错就是加一这很重要，因为它是只加一的所以有可能的情况就只有两种：

除 99 之外的数字加一；
数字 99。
加一得十进一位个位数为 00 加法运算如不出现进位就运算结束了且进位只会是一。

所以只需要判断有没有进位并模拟出它的进位方式，如十位数加 11 个位数置为 00，如此循环直到判断没有再进位就退出循环返回结果。

然后还有一些特殊情况就是当出现 9999、999999 之类的数字时，循环到最后也需要进位，出现这种情况时需要手动将它进一位。

class Solution {
    public int[] plusOne(int[] digits) {
        for (int i = digits.length - 1; i >= 0; i--) {
            digits[i]++;
            digits[i] = digits[i] % 10;
            if (digits[i] != 0) return digits;
        }
        digits = new int[digits.length + 1];
        digits[0] = 1;
        return digits;
    }
}
PS：本人并非大佬，这是第一次写思路解释，如有写的不好的地方请多多包涵，哈哈哈

下一篇：画解算法：66. 加一

public class Solution {
    public int[] PlusOne(int[] digits) {
        int n = digits.Length;
    for (int i = n - 1; i >= 0; i--) {//从尾部每位数字遍历 
        if (digits[i] < 9) {
            digits[i]++;
            return digits;
        }// 如果该位数字<9 则+1返回
        digits[i] = 0;// if = 9 则该位置零 继续循环上一位
    }
    int[] result= new int[n + 1];//如果全是9，则会在数字第一位加1
    result[0] = 1;
    return result;
    }
}

 
*/