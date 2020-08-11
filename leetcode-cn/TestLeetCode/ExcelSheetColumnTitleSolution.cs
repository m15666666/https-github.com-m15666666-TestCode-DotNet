using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个正整数，返回它在 Excel 表中相对应的列名称。

例如，

    1 -> A
    2 -> B
    3 -> C
    ...
    26 -> Z
    27 -> AA
    28 -> AB 
    ...
示例 1:

输入: 1
输出: "A"
示例 2:

输入: 28
输出: "AB"
示例 3:

输入: 701
输出: "ZY"


*/
/// <summary>
/// https://leetcode-cn.com/problems/excel-sheet-column-title/
/// 168. Excel表列名称
/// 
/// 
/// 
/// </summary>
class ExcelSheetColumnTitleSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public string ConvertToTitle(int n) {
        const int Base = 26;
        StringBuilder builder = new StringBuilder();
        while(0 < n)
        {
            n--;
            char c = (char)('A' + (n % Base));
            builder.Insert(0, c);
            n /= Base;
        }
        return builder.ToString();
    }
}
/*

public class Solution {
    public string ConvertToTitle(int n) {
            var chars = new char[26];
            for (int i = 1; i < chars.Length; i++)
            {
                chars[i] = (char) ('A' + i - 1);
            }

            chars[0] = 'Z';
            var res = new StringBuilder(string.Empty);
            while (n != 0)
            {
                res.Insert(0, chars[n % 26]);
                if (n % 26 == 0)
                {
                    n /= 26;
                    n--;
                }
                else
                {
                    n /= 26;
                }
            }
            return res.ToString();
    }
}

public class Solution {
    public string ConvertToTitle(int n) {
        string columnStr = string.Empty;
        while(n >= 1) {
            n = n - 1;
            columnStr = NumToChar(n % 26) + columnStr;
            n = n / 26;
        }
        return columnStr;
    }

    private string NumToChar(int num) {
        switch(num) {
            case 0:
                return "A";
            case 1:
                return "B";
            case 2:
                return "C";
            case 3:
                return "D";
            case 4:
                return "E";
            case 5:
                return "F";
            case 6:
                return "G";
            case 7:
                return "H";
            case 8:
                return "I";
            case 9:
                return "J";
            case 10:
                return "K";
            case 11:
                return "L";
            case 12:
                return "M";
            case 13:
                return "N";
            case 14:
                return "O";
            case 15:
                return "P";
            case 16:
                return "Q";
            case 17:
                return "R";
            case 18:
                return "S";
            case 19:
                return "T";
            case 20:
                return "U";
            case 21:
                return "V";
            case 22:
                return "W";
            case 23:
                return "X";
            case 24:
                return "Y";
            case 25:
                return "Z";
        }
        return string.Empty;
    }
}

public class Solution
{
	public string ConvertToTitle(int n)
	{
		string s = "";
		while (n != 0)
		{
			s = (char)((n - 1) % 26 + 'A') + s;
			n = (n - 1) / 26;
		}
		return s;
	}
}

public class Solution {
    public string ConvertToTitle(int n) {
        string s="";
        while(n!=0){
            n--;
            int div=n%26;
            s=(char)('A'+div)+s;
            n=n/26;
        }

        return s;
    }
}

public class Solution {
    public string ConvertToTitle(int n) {
        string strExC="";
        while(n>0){
            n--;
            strExC= ((char)('A'+n%26)).ToString()+strExC;
            n/=26;
        }
        return strExC;
    }
}

public class Solution {
    public string ConvertToTitle(int n) {
        string ans = string.Empty;
        while (n != 0)
        {
            int a = (--n) % 26;
            ans = (char)(a + 'A') + ans;
            n = n / 26;
        }
        return ans;
    }
}

 
 
 
*/