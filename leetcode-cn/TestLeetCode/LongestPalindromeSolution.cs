using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个字符串 s，找到 s 中最长的回文子串。你可以假设 s 的最大长度为 1000。

示例 1：

输入: "babad"
输出: "bab"
注意: "aba" 也是一个有效答案。
示例 2：

输入: "cbbd"
输出: "bb"
*/
/// <summary>
/// https://leetcode-cn.com/problems/longest-palindromic-substring
/// 5. 最长回文子串
/// 
/// </summary>
class LongestPalindromeSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    
    public string LongestPalindrome(string s)
    {
        if (string.IsNullOrEmpty(s)) return string.Empty;
        int n = s.Length;

        int start = -1;
        int maxLength = -1;
        int len, len2;
        for (int i = 0; i < s.Length; i++)
        {
            len = ExpandAroundCenter(i, i);
            len2 = ExpandAroundCenter(i, i + 1);
            if(len < len2) len = len2;
            if (maxLength < len)
            {
                maxLength = len;
                start = i - (maxLength - 1) / 2;
            }
        }
        return s.Substring(start, maxLength);

        int ExpandAroundCenter(int left, int right)
        {
            while (-1 < left && right < n && s[left] == s[right])
            {
                left--;
                right++;
            }
            return right - left - 1;
        }
    }
}
/*

public class Solution {
        public string LongestPalindrome(string s)
        {
            if (string.IsNullOrEmpty(s)) return s;
            if (s.Length == 1) return s;

            var q = s.Length / 2;
            var p = s.Length % 2 == 1 ? q : q - 1;

            ReadOnlySpan<char> sspan = s.ToCharArray();
            var result = ReadOnlySpan<char>.Empty;

            while ((p + 1) * 2 > result.Length)
            {
                var palindrome = LongestPalindromeABBA(sspan, p);
                if (palindrome.Length > result.Length) result = palindrome;

                palindrome = LongestPalindromeABXBA(sspan, p);
                if (palindrome.Length > result.Length) result = palindrome;

                if (p != q)
                {
                    palindrome = LongestPalindromeABBA(sspan, q);
                    if (palindrome.Length > result.Length) result = palindrome;

                    palindrome = LongestPalindromeABXBA(sspan, q);
                    if (palindrome.Length > result.Length) result = palindrome;
                }

                p--;
                q++;
            }

            return result.ToString();
        }

        private ReadOnlySpan<char> LongestPalindromeABXBA(ReadOnlySpan<char> s, int i)
        {
            var p = i - 1;
            var q = i + 1;
            var len = 1;

            while (p >= 0 && q < s.Length && s[p] == s[q])
            {
                p--;
                q++;
                len += 2;
            }

            return s.Slice(p + 1, len);
        }

        private ReadOnlySpan<char> LongestPalindromeABBA(ReadOnlySpan<char> s, int i)
        {
            var p = i;
            var q = i + 1;
            var len = 0;

            while (p >= 0 && q < s.Length && s[p] == s[q])
            {
                p--;
                q++;
                len += 2;
            }

            return len == 0 ? ReadOnlySpan<char>.Empty : s.Slice(p + 1, len);
        }
}

  
class LongestPalindromeSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    
    public string LongestPalindrome(string s)
    {
        if (s == null || s.Length == 1) return s;
        string lastPalindrome = string.Empty;
        int lastPalindromeLength = lastPalindrome.Length;

        for ( int startIndex = 0; startIndex < s.Length; startIndex++)
        {
            int endIndex = s.Length - 1;
            if (endIndex - startIndex + 1 <= lastPalindromeLength) break;

            while (true)
            {
                var remainLength = endIndex - startIndex + 1;
                if (remainLength <= lastPalindromeLength) break;

                if (s[startIndex] == s[endIndex])
                {
                    var isPalin = IsPalindrome(s, startIndex, endIndex);
                    if (isPalin)
                    {
                        lastPalindrome = s.Substring(startIndex, remainLength);
                        lastPalindromeLength = lastPalindrome.Length;
                        break;
                    }
                }

                endIndex--;
                if (endIndex < startIndex) break;
            }
        }

        return lastPalindrome;
    }

    /// <summary>
    /// 是回文子串
    /// </summary>
    /// <param name="s"></param>
    /// <param name="startIndex"></param>
    /// <param name="endIndex"></param>
    /// <returns></returns>
    private static bool IsPalindrome( string s, int startIndex, int endIndex )
    {
        if (s == null || s.Length == 1) return true;
        while (startIndex < endIndex )
        {
            if (s[startIndex++] != s[endIndex--]) return false;
        }

        return true;
    }
}

最长回文子串
力扣 (LeetCode)
发布于 2 年前
244.9k
摘要
这篇文章是为中级读者而写的。它介绍了回文，动态规划以及字符串处理。请确保你理解什么是回文。回文是一个正读和反读都相同的字符串，例如，\textrm{“aba”}“aba” 是回文，而 \textrm{“abc”}“abc” 不是。

解决方案
方法一：最长公共子串
常见错误

有些人会忍不住提出一个快速的解决方案，不幸的是，这个解决方案有缺陷(但是可以很容易地纠正)：

反转 SS，使之变成 S'S 
′
 。找到 SS 和 S'S 
′
  之间最长的公共子串，这也必然是最长的回文子串。

这似乎是可行的，让我们看看下面的一些例子。

例如，S = \textrm{“caba”}S=“caba”, S' = \textrm{“abac”}S 
′
 =“abac”：

SS 以及 S'S 
′
  之间的最长公共子串为 \textrm{“aba”}“aba”，恰恰是答案。

让我们尝试一下这个例子：S = \textrm{“abacdfgdcaba”}S=“abacdfgdcaba”, S' = \textrm{“abacdgfdcaba”}S 
′
 =“abacdgfdcaba”：

SS 以及 S'S 
′
  之间的最长公共子串为 \textrm{“abacd”}“abacd”。显然，这不是回文。

算法

我们可以看到，当 SS 的其他部分中存在非回文子串的反向副本时，最长公共子串法就会失败。为了纠正这一点，每当我们找到最长的公共子串的候选项时，都需要检查子串的索引是否与反向子串的原始索引相同。如果相同，那么我们尝试更新目前为止找到的最长回文子串；如果不是，我们就跳过这个候选项并继续寻找下一个候选。

这给我们提供了一个复杂度为 O(n^2)O(n 
2
 ) 动态规划解法，它将占用 O(n^2)O(n 
2
 ) 的空间（可以改进为使用 O(n)O(n) 的空间）。请在 这里 阅读更多关于最长公共子串的内容。

方法二：暴力法
很明显，暴力法将选出所有子字符串可能的开始和结束位置，并检验它是不是回文。

复杂度分析

时间复杂度：O(n^3)O(n 
3
 )，假设 nn 是输入字符串的长度，则 \binom{n}{2} = \frac{n(n-1)}{2}( 
2
n
​	
 )= 
2
n(n−1)
​	
  为此类子字符串(不包括字符本身是回文的一般解法)的总数。因为验证每个子字符串需要 O(n)O(n) 的时间，所以运行时间复杂度是 O(n^3)O(n 
3
 )。

空间复杂度：O(1)O(1)。

方法三：动态规划
为了改进暴力法，我们首先观察如何避免在验证回文时进行不必要的重复计算。考虑 \textrm{“ababa”}“ababa” 这个示例。如果我们已经知道 \textrm{“bab”}“bab” 是回文，那么很明显，\textrm{“ababa”}“ababa” 一定是回文，因为它的左首字母和右尾字母是相同的。

我们给出 P(i,j)P(i,j) 的定义如下：

P(i,j) = \begin{cases} \text{true,} &\quad\text{如果子串} S_i \dots S_j \text{是回文子串}\\ \text{false,} &\quad\text{其它情况} \end{cases}
P(i,j)={ 
true,
false,
​	
  
如果子串S 
i
​	
 …S 
j
​	
 是回文子串
其它情况
​	
 

因此，

P(i, j) = ( P(i+1, j-1) \text{ and } S_i == S_j )
P(i,j)=(P(i+1,j−1) and S 
i
​	
 ==S 
j
​	
 )

基本示例如下：

P(i, i) = true
P(i,i)=true

P(i, i+1) = ( S_i == S_{i+1} )
P(i,i+1)=(S 
i
​	
 ==S 
i+1
​	
 )

这产生了一个直观的动态规划解法，我们首先初始化一字母和二字母的回文，然后找到所有三字母回文，并依此类推…

复杂度分析

时间复杂度：O(n^2)O(n 
2
 )，这里给出我们的运行时间复杂度为 O(n^2)O(n 
2
 ) 。

空间复杂度：O(n^2)O(n 
2
 )，该方法使用 O(n^2)O(n 
2
 ) 的空间来存储表。

补充练习

你能进一步优化上述解法的空间复杂度吗？

方法四：中心扩展算法
事实上，只需使用恒定的空间，我们就可以在 O(n^2)O(n 
2
 ) 的时间内解决这个问题。

我们观察到回文中心的两侧互为镜像。因此，回文可以从它的中心展开，并且只有 2n - 12n−1 个这样的中心。

你可能会问，为什么会是 2n - 12n−1 个，而不是 nn 个中心？原因在于所含字母数为偶数的回文的中心可以处于两字母之间（例如 \textrm{“abba”}“abba” 的中心在两个 \textrm{‘b’}‘b’ 之间）。

public String longestPalindrome(String s) {
    if (s == null || s.Length < 1) return "";
    int start = 0, end = 0;
    for (int i = 0; i < s.Length; i++) {
        int len1 = expandAroundCenter(s, i, i);
        int len2 = expandAroundCenter(s, i, i + 1);
        int len = Math.max(len1, len2);
        if (len > end - start) {
            start = i - (len - 1) / 2;
            end = i + len / 2;
        }
    }
    return s.substring(start, end + 1);
}

private int expandAroundCenter(String s, int left, int right) {
    int L = left, R = right;
    while (L >= 0 && R < s.Length && s.charAt(L) == s.charAt(R)) {
        L--;
        R++;
    }
    return R - L - 1;
}
复杂度分析

时间复杂度：O(n^2)O(n 
2
 )，由于围绕中心来扩展回文会耗去 O(n)O(n) 的时间，所以总的复杂度为 O(n^2)O(n 
2
 )。

空间复杂度：O(1)O(1)。

方法五：Manacher 算法
还有一个复杂度为 O(n)O(n) 的 Manacher 算法。然而，这是一个非同寻常的算法，在 45 分钟的编码时间内提出这个算法将会是一个不折不扣的挑战。理解它，我保证这将是非常有趣的。

下一篇：详细通俗的思路分析，多解法

*/
