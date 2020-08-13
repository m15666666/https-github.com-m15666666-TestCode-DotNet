using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个Excel表格中的列名称，返回其相应的列序号。

例如，

    A -> 1
    B -> 2
    C -> 3
    ...
    Z -> 26
    AA -> 27
    AB -> 28 
    ...
示例 1:

输入: "A"
输出: 1
示例 2:

输入: "AB"
输出: 28
示例 3:

输入: "ZY"
输出: 701

*/
/// <summary>
/// https://leetcode-cn.com/problems/excel-sheet-column-number/
/// 171. Excel表列序号
/// 
/// 
/// 
/// 
/// 
/// </summary>
class ExcelSheetColumnNumberSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int TitleToNumber(string s) {
        const char A = 'A';
        int len = s.Length;
        int ret = 0;
        for( int i = len - 1,multiple = 1; -1 < i; i--, multiple *= 26)
            ret += (s[i] - A + 1) * multiple;
        return ret;
    }

}
/*

public class Solution {
    public int TitleToNumber(string s) {
        if(string.IsNullOrEmpty(s))
            return 0;
        int sum = 0;
        for(int i = 0; i < s.Length; i++)
        {
            sum = sum * 26 + ((int)s[i] - (int)'A' + 1);
        }
        return sum;
    }
}

public class Solution {
    public int TitleToNumber(string s)
    {
        int value = 0;
        for (int i = 0; i <s.Length; i++)
            value = value * 26 + (s[i] - 'A' + 1);

        return value;
    }
}

public class Solution {
        private Dictionary<int, int> dic = new Dictionary<int, int>()
        {
            {0, 1},{1, 26},{2, 676},{3, 17576},{4,456976},{5,11881376},{6, 308915776 }
        };

        public int TitleToNumber(string s)
        {
            if (s == null) return 558;
            if (s.Length == 0) return 0;
            int len = s.Length;
            int res = s[s.Length - 1] - '@';
            for(int i = 0; i < s.Length - 1; i++)
            {
                int forward = s[i] - '@';
                int n = --len;
                int mul = dic[n];
                res += forward * mul;
            }
            return res;
        }
}

public class Solution {
    public int TitleToNumber(string s) {
    //26进制转换为10进制
    int ans=0;
    for(int i=0;i<s.Length;i++){
        int num=s[i]-'A'+1;
            ans=ans*26+num;
        }
    return ans;


    }
}

 
 
 
*/