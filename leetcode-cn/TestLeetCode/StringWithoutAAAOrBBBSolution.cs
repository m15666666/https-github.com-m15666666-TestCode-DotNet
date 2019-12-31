using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定两个整数 A 和 B，返回任意字符串 S，要求满足：

S 的长度为 A + B，且正好包含 A 个 'a' 字母与 B 个 'b' 字母；
子串 'aaa' 没有出现在 S 中；
子串 'bbb' 没有出现在 S 中。

示例 1：

输入：A = 1, B = 2
输出："abb"
解释："abb", "bab" 和 "bba" 都是正确答案。
示例 2：

输入：A = 4, B = 1
输出："aabaa"

提示：

0 <= A <= 100
0 <= B <= 100
对于给定的 A 和 B，保证存在满足要求的 S。
*/
/// <summary>
/// https://leetcode-cn.com/problems/string-without-aaa-or-bbb/
/// 984. 不含 AAA 或 BBB 的字符串
/// 
/// </summary>
class StringWithoutAAAOrBBBSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public string StrWithout3a3b(int A, int B)
    {
        const char b = 'b';
        const char a = 'a';
        StringBuilder ret = new StringBuilder(A + B);

        while (A > 0 || B > 0)
        {
            bool writeA = false;
            int len = ret.Length;
            if ( 2 <= len && ret[len - 1] == ret[len - 2])
            {
                if (ret[len - 1] == b) writeA = true;
            }
            else if ( B <= A )
            {
                 writeA = true;
            }

            if (writeA)
            {
                A--;
                ret.Append(a);
            }
            else
            {
                B--;
                ret.Append(b);
            }
        }

        return ret.ToString();
    }
}
/*
不含 AAA 或 BBB 的字符串
力扣 (LeetCode)
发布于 1 年前
3.4k 阅读
方法：贪心
思路

直观感觉，我们应该先选择当前所剩最多的待写字母写入字符串中。举一个例子，如果 A = 6, B = 2，那么我们期望写出 'aabaabaa'。进一步说，设当前所剩最多的待写字母为 x，只有前两个已经写下的字母都是 x 的时候，下一个写入字符串中的字母才不应该选择它。

算法

我们定义 A, B：待写的 'a' 与 'b' 的数量。

设当前还需要写入字符串的 'a' 与 'b' 中较多的那一个为 x，如果我们已经连续写了两个 x 了，下一次我们应该写另一个字母。否则，我们应该继续写 x。

class Solution {
    public String strWithout3a3b(int A, int B) {
        StringBuilder ans = new StringBuilder();

        while (A > 0 || B > 0) {
            boolean writeA = false;
            int L = ans.length();
            if (L >= 2 && ans.charAt(L-1) == ans.charAt(L-2)) {
                if (ans.charAt(L-1) == 'b')
                    writeA = true;
            } else {
                if (A >= B)
                    writeA = true;
            }

            if (writeA) {
                A--;
                ans.append('a');
            } else {
                B--;
                ans.append('b');
            }
        }

        return ans.toString();
    }
}
复杂度分析

时间复杂度：O(A+B)O(A+B)。

空间复杂度：O(A+B)O(A+B)。

public class Solution {
    public string StrWithout3a3b(int A, int B) {
        StringBuilder sb = new StringBuilder();
        if((A >= 2*B + 3) || (B >= 2*A + 3) )
        {
            return "";
        }
        else if(A >= 2*B)
        {
            for(int i=0;i<B;i++)
            {
                sb.Append("aab");
            }
            sb.Append(new string('a',A-2*B));
            return sb.ToString();
        }
        else if(A >= B && A < 2*B)
        {
            for(int i=0;i<A-B;i++)
            {
                sb.Append("aab");
            }
            for(int i=0;i<2*B-A;i++)
            {
                sb.Append("ab");
            }
            return sb.ToString();
        }
        else if(B > A && B < 2*A)
        {
            for(int i=0;i<B-A;i++)
            {
                sb.Append("bba");
            }
            for(int i=0;i<2*A-B;i++)
            {
                sb.Append("ba");
            }
            return sb.ToString();
        }
        else
        {
            for(int i=0;i<A;i++)
            {
                sb.Append("bba");
            }
            sb.Append(new string('b',B-2*A));
            return sb.ToString();
        }
        return "";
    }
} 
*/
