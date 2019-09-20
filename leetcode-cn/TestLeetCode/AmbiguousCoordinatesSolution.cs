using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
我们有一些二维坐标，如 "(1, 3)" 或 "(2, 0.5)"，然后我们移除所有逗号，小数点和空格，得到一个字符串S。返回所有可能的原始字符串到一个列表中。

原始的坐标表示法不会存在多余的零，所以不会出现类似于"00", "0.0", "0.00", "1.0", "001", "00.01"或一些其他更小的数来表示坐标。此外，一个小数点前至少存在一个数，所以也不会出现“.1”形式的数字。

最后返回的列表可以是任意顺序的。而且注意返回的两个数字中间（逗号之后）都有一个空格。

示例 1:
输入: "(123)"
输出: ["(1, 23)", "(12, 3)", "(1.2, 3)", "(1, 2.3)"]
示例 2:
输入: "(00011)"
输出:  ["(0.001, 1)", "(0, 0.011)"]
解释: 
0.0, 00, 0001 或 00.01 是不被允许的。
示例 3:
输入: "(0123)"
输出: ["(0, 123)", "(0, 12.3)", "(0, 1.23)", "(0.1, 23)", "(0.1, 2.3)", "(0.12, 3)"]
示例 4:
输入: "(100)"
输出: [(10, 0)]
解释: 
1.0 是不被允许的。

提示:

4 <= S.length <= 12.
S[0] = "(", S[S.length - 1] = ")", 且字符串 S 中的其他元素都是数字。
*/
/// <summary>
/// https://leetcode-cn.com/problems/ambiguous-coordinates/
/// 816. 模糊坐标
/// https://zhuanlan.zhihu.com/p/55485986
/// </summary>
class AmbiguousCoordinatesSolution
{
    public void Test()
    {
        var ret = AmbiguousCoordinates("(00011)");
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public IList<string> AmbiguousCoordinates(string S)
    {
        List<string> ret = new List<string>();
        if (string.IsNullOrEmpty(S) || S.Length == 2) return ret;

        int len = S.Length - 2;
        List<string> list1 = new List<string>();
        List<string> list2 = new List<string>();
        const int startIndex = 1;
        int stopIndex = S.Length - 2;
        StringBuilder sb = new StringBuilder();
        sb.Append("(");
        int mark1 = sb.Length;
        for (int count = 1; count < len; count++)
        {
            F(S, startIndex, startIndex + count - 1, list1);
            if (0 < list1.Count)
            {
                F(S, startIndex + count, stopIndex, list2);

                if (0 < list2.Count)
                {
                    foreach (var item1 in list1)
                    {
                        sb.Length = mark1;

                        sb.Append(item1);
                        sb.Append(", ");
                        int mark2 = sb.Length;
                        foreach (var item2 in list2)
                        {
                            sb.Length = mark2;

                            sb.Append(item2);
                            sb.Append(")");
                            ret.Add(sb.ToString());
                        }
                    }

                    list2.Clear();
                }
                list1.Clear();
            }
        }
        return ret;
    }

    private const char Zero = '0';
    private void F(string S, int startIndex, int stopIndex, List<string> list)
    {
        int len = stopIndex - startIndex + 1;
        if(len == 1)
        {
            list.Add(S[startIndex].ToString());
            return;
        }

        if (S[startIndex] == Zero && S[stopIndex] == Zero) return;

        if (S[startIndex] == Zero) {
            list.Add($"0.{S.Substring(startIndex+1, len-1)}");
            return;
        }

        var s = S.Substring(startIndex, len);
        
        if (S[stopIndex] == Zero)
        {
            list.Add(s);
            return;
        }

        StringBuilder sb = new StringBuilder();
        for ( int count = 1; count < len; count++)
        {
            sb.Append(S, startIndex, count);
            sb.Append('.');
            sb.Append(S, startIndex + count, len - count);
            list.Add(sb.ToString());
            sb.Clear();
        }
        list.Add(s);
    }
}
/*
public class Solution {
    public IList<string> AmbiguousCoordinates(string S) {
        int n = S.Length;
        string str = S.Substring(1, n-2);
        
        StringBuilder sb = new StringBuilder();
        
        IList<string> res = new List<string>();
        
        int len = n-2;
        for (int i = 1; i < len; i ++) {
            string str1 = str.Substring(0, i);
            string str2 = str.Substring(i, len - i);
            
            // Console.WriteLine(str1);
            // Console.WriteLine(str2);
            
            List<string> list1 = Strs(str1);
            List<string> list2 = Strs(str2);
            if (list1.Count() > 0 && list2.Count() > 0) {
                for (int i1= 0; i1 < list1.Count(); i1 ++) {
                    for (int i2 = 0; i2 < list2.Count(); i2 ++) {
                        sb.Clear();
                        sb.Append('(');
                        sb.Append(list1[i1]);
                        sb.Append(", ");
                        sb.Append(list2[i2]);
                        sb.Append(')');
                        res.Add(sb.ToString());
                    }
                }
            }
        }
        
        return res;
    }
    
    List<string> Strs(string str) {
        int n = str.Length;
        List<string> res = new List<string>();
        if (n == 1) {
            res.Add(str);
            return res;
        }
        
        if (str[0] == '0' && str[n-1] == '0') {
            return res;
        }
        
        if (str[n-1] == '0') {
            res.Add(str);
            return res;
        }
        
        if (str[0] == '0') {
            StringBuilder sb = new StringBuilder();
            sb.Append(str[0]);
            sb.Append('.');
            sb.Append(str.Substring(1, n-1));
            res.Add(sb.ToString());
            return res;
        }
        
        StringBuilder sb1 = new StringBuilder();
        for (int i = 1; i <= n; i ++) {
            sb1.Clear();
            sb1.Append(str.Substring(0,i));
            
            if (i != n) {
                sb1.Append('.');
                sb1.Append(str.Substring(i,n-i));
            }
            res.Add(sb1.ToString());
        }
        
        return res;
    }
} 
*/
