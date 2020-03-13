using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
罗马数字包含以下七种字符: I， V， X， L，C，D 和 M。

字符          数值
I             1
V             5
X             10
L             50
C             100
D             500
M             1000
例如， 罗马数字 2 写做 II ，即为两个并列的 1。12 写做 XII ，即为 X + II 。 27 写做  XXVII, 即为 XX + V + II 。

通常情况下，罗马数字中小的数字在大的数字的右边。但也存在特例，例如 4 不写做 IIII，而是 IV。数字 1 在数字 5 的左边，所表示的数等于大数 5 减小数 1 得到的数值 4 。同样地，数字 9 表示为 IX。这个特殊的规则只适用于以下六种情况：

I 可以放在 V (5) 和 X (10) 的左边，来表示 4 和 9。
X 可以放在 L (50) 和 C (100) 的左边，来表示 40 和 90。 
C 可以放在 D (500) 和 M (1000) 的左边，来表示 400 和 900。
给定一个罗马数字，将其转换成整数。输入确保在 1 到 3999 的范围内。

示例 1:

输入: "III"
输出: 3
示例 2:

输入: "IV"
输出: 4
示例 3:

输入: "IX"
输出: 9
示例 4:

输入: "LVIII"
输出: 58
解释: L = 50, V= 5, III = 3.
示例 5:

输入: "MCMXCIV"
输出: 1994
解释: M = 1000, CM = 900, XC = 90, IV = 4.
*/
/// <summary>
/// https://leetcode-cn.com/problems/roman-to-integer/
/// 13. 罗马数字转整数
/// 
/// </summary>
class RomanToIntSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    public int RomanToInt(string s)
    {
        var roman2int = Roman2Int;
        int val = 0;
        for (int i = 0; i < s.Length; i++)
        {
            if (i == s.Length - 1 || roman2int[s[i + 1]] <= roman2int[s[i]])
                val += roman2int[s[i]];
            else
                val -= roman2int[s[i]];
        }
        return val;
    }

    private static readonly Dictionary<char, int> Roman2Int = new Dictionary<char, int> {
        { 'I', 1},
        { 'V', 5},
        { 'X', 10},
        { 'C', 100},
        { 'M', 1000},
        { 'L', 50},
        { 'D', 500},
    };
}
/*


用时 99.93%，内存98.73%，简单解法
DoneSpeak
发布于 4 个月前
14.1k
执行用时 :4 ms, 在所有 java 提交中击败了99.93% 的用户
内存消耗 :36.1 MB, 在所有 java 提交中击败了98.73%的用户

按照题目的描述，可以总结如下规则：

罗马数字由 I,V,X,L,C,D,M 构成；
当小值在大值的左边，则减小值，如 IV=5-1=4；
当小值在大值的右边，则加小值，如 VI=5+1=6；
由上可知，右值永远为正，因此最后一位必然为正。
一言蔽之，把一个小值放在大值的左边，就是做减法，否则为加法。

Screen Shot 2020-02-17 at 21.14.47.png

在代码实现上，可以往后看多一位，对比当前位与后一位的大小关系，从而确定当前位是加还是减法。当没有下一位时，做加法即可。

也可保留当前位的值，当遍历到下一位的时，对比保留值与遍历位的大小关系，再确定保留值为加还是减。最后一位做加法即可。

import java.util.*;

class Solution {
    public int romanToInt(String s) {
        int sum = 0;
        int preNum = getValue(s.charAt(0));
        for(int i = 1;i < s.length(); i ++) {
            int num = getValue(s.charAt(i));
            if(preNum < num) {
                sum -= preNum;
            } else {
                sum += preNum;
            }
            preNum = num;
        }
        sum += preNum;
        return sum;
    }
    
    private int getValue(char ch) {
        switch(ch) {
            case 'I': return 1;
            case 'V': return 5;
            case 'X': return 10;
            case 'L': return 50;
            case 'C': return 100;
            case 'D': return 500;
            case 'M': return 1000;
            default: return 0;
        }
    }
}
下一篇：画解算法：13. 罗马数字转整数
 
*/
