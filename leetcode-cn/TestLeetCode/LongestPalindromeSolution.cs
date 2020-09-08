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
最长回文子串
力扣官方题解
发布于 2020-05-20
143.3k
📺 视频题解

📖 文字题解
方法一：动态规划
思路与算法

对于一个子串而言，如果它是回文串，并且长度大于 22，那么将它首尾的两个字母去除之后，它仍然是个回文串。例如对于字符串 \textrm{``ababa''}“ababa”，如果我们已经知道 \textrm{``bab''}“bab” 是回文串，那么 \textrm{``ababa''}“ababa” 一定是回文串，这是因为它的首尾两个字母都是 \textrm{``a''}“a”。

根据这样的思路，我们就可以用动态规划的方法解决本题。我们用 P(i,j)P(i,j) 表示字符串 ss 的第 ii 到 jj 个字母组成的串（下文表示成 s[i:j]s[i:j]）是否为回文串：

P(i,j) = \begin{cases} \text{true,} &\quad\text{如果子串~} S_i \dots S_j \text{~是回文串}\\ \text{false,} &\quad\text{其它情况} \end{cases}
P(i,j)={ 
true,
false,
​	
  
如果子串 S 
i
​	
 …S 
j
​	
  是回文串
其它情况
​	
 

这里的「其它情况」包含两种可能性：

s[i, j]s[i,j] 本身不是一个回文串；

i > ji>j，此时 s[i, j]s[i,j] 本身不合法。

那么我们就可以写出动态规划的状态转移方程：

P(i, j) = P(i+1, j-1) \wedge (S_i == S_j)
P(i,j)=P(i+1,j−1)∧(S 
i
​	
 ==S 
j
​	
 )

也就是说，只有 s[i+1:j-1]s[i+1:j−1] 是回文串，并且 ss 的第 ii 和 jj 个字母相同时，s[i:j]s[i:j] 才会是回文串。

上文的所有讨论是建立在子串长度大于 22 的前提之上的，我们还需要考虑动态规划中的边界条件，即子串的长度为 11 或 22。对于长度为 11 的子串，它显然是个回文串；对于长度为 22 的子串，只要它的两个字母相同，它就是一个回文串。因此我们就可以写出动态规划的边界条件：

\begin{cases} P(i, i) = \text{true} \\ P(i, i+1) = ( S_i == S_{i+1} ) \end{cases}
{ 
P(i,i)=true
P(i,i+1)=(S 
i
​	
 ==S 
i+1
​	
 )
​	
 

根据这个思路，我们就可以完成动态规划了，最终的答案即为所有 P(i, j) = \text{true}P(i,j)=true 中 j-i+1j−i+1（即子串长度）的最大值。注意：在状态转移方程中，我们是从长度较短的字符串向长度较长的字符串进行转移的，因此一定要注意动态规划的循环顺序。


class Solution:
    def longestPalindrome(self, s: str) -> str:
        n = len(s)
        dp = [[False] * n for _ in range(n)]
        ans = ""
        # 枚举子串的长度 l+1
        for l in range(n):
            # 枚举子串的起始位置 i，这样可以通过 j=i+l 得到子串的结束位置
            for i in range(n):
                j = i + l
                if j >= len(s):
                    break
                if l == 0:
                    dp[i][j] = True
                elif l == 1:
                    dp[i][j] = (s[i] == s[j])
                else:
                    dp[i][j] = (dp[i + 1][j - 1] and s[i] == s[j])
                if dp[i][j] and l + 1 > len(ans):
                    ans = s[i:j+1]
        return ans
复杂度分析

时间复杂度：O(n^2)O(n 
2
 )，其中 nn 是字符串的长度。动态规划的状态总数为 O(n^2)O(n 
2
 )，对于每个状态，我们需要转移的时间为 O(1)O(1)。

空间复杂度：O(n^2)O(n 
2
 )，即存储动态规划状态需要的空间。

方法二：中心扩展算法
思路与算法

我们仔细观察一下方法一中的状态转移方程：

\begin{cases} P(i, i) &=\quad \text{true} \\ P(i, i+1) &=\quad ( S_i == S_{i+1} ) \\ P(i, j) &=\quad P(i+1, j-1) \wedge (S_i == S_j) \end{cases}
⎩
⎪
⎪
⎨
⎪
⎪
⎧
​	
  
P(i,i)
P(i,i+1)
P(i,j)
​	
  
=true
=(S 
i
​	
 ==S 
i+1
​	
 )
=P(i+1,j−1)∧(S 
i
​	
 ==S 
j
​	
 )
​	
 

找出其中的状态转移链：

P(i, j) \leftarrow P(i+1, j-1) \leftarrow P(i+2, j-2) \leftarrow \cdots \leftarrow \text{某一边界情况}
P(i,j)←P(i+1,j−1)←P(i+2,j−2)←⋯←某一边界情况

可以发现，所有的状态在转移的时候的可能性都是唯一的。也就是说，我们可以从每一种边界情况开始「扩展」，也可以得出所有的状态对应的答案。

边界情况即为子串长度为 11 或 22 的情况。我们枚举每一种边界情况，并从对应的子串开始不断地向两边扩展。如果两边的字母相同，我们就可以继续扩展，例如从 P(i+1,j-1)P(i+1,j−1) 扩展到 P(i,j)P(i,j)；如果两边的字母不同，我们就可以停止扩展，因为在这之后的子串都不能是回文串了。

聪明的读者此时应该可以发现，「边界情况」对应的子串实际上就是我们「扩展」出的回文串的「回文中心」。方法二的本质即为：我们枚举所有的「回文中心」并尝试「扩展」，直到无法扩展为止，此时的回文串长度即为此「回文中心」下的最长回文串长度。我们对所有的长度求出最大值，即可得到最终的答案。


class Solution {
    public String longestPalindrome(String s) {
        if (s == null || s.length() < 1) return "";
        int start = 0, end = 0;
        for (int i = 0; i < s.length(); i++) {
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
        while (L >= 0 && R < s.length() && s.charAt(L) == s.charAt(R)) {
            L--;
            R++;
        }
        return R - L - 1;
    }
}
复杂度分析

时间复杂度：O(n^2)O(n 
2
 )，其中 nn 是字符串的长度。长度为 11 和 22 的回文中心分别有 nn 和 n-1n−1 个，每个回文中心最多会向外扩展 O(n)O(n) 次。

空间复杂度：O(1)O(1)。

方法三：Manacher 算法
还有一个复杂度为 O(n)O(n) 的 Manacher 算法。然而本算法十分复杂，一般不作为面试内容。这里给出，仅供有兴趣的同学挑战自己。

为了表述方便，我们定义一个新概念臂长，表示中心扩展算法向外扩展的长度。如果一个位置的最大回文字符串长度为 2 * length + 1 ，其臂长为 length。

下面的讨论只涉及长度为奇数的回文字符串。长度为偶数的回文字符串我们将会在最后与长度为奇数的情况统一起来。

思路与算法

在中心扩展算法的过程中，我们能够得出每个位置的臂长。那么当我们要得出以下一个位置 i 的臂长时，能不能利用之前得到的信息呢？

答案是肯定的。具体来说，如果位置 j 的臂长为 length，并且有 j + length > i，如下图所示：

fig1

当在位置 i 开始进行中心拓展时，我们可以先找到 i 关于 j 的对称点 2 * j - i。那么如果点 2 * j - i 的臂长等于 n，我们就可以知道，点 i 的臂长至少为 min(j + length - i, n)。那么我们就可以直接跳过 i 到 i + min(j + length - i, n) 这部分，从 i + min(j + length - i, n) + 1 开始拓展。

我们只需要在中心扩展法的过程中记录右臂在最右边的回文字符串，将其中心作为 j，在计算过程中就能最大限度地避免重复计算。

那么现在还有一个问题：如何处理长度为偶数的回文字符串呢？

我们可以通过一个特别的操作将奇偶数的情况统一起来：我们向字符串的头尾以及每两个字符中间添加一个特殊字符 #，比如字符串 aaba 处理后会变成 #a#a#b#a#。那么原先长度为偶数的回文字符串 aa 会变成长度为奇数的回文字符串 #a#a#，而长度为奇数的回文字符串 aba 会变成长度仍然为奇数的回文字符串 #a#b#a#，我们就不需要再考虑长度为偶数的回文字符串了。

注意这里的特殊字符不需要是没有出现过的字母，我们可以使用任何一个字符来作为这个特殊字符。这是因为，当我们只考虑长度为奇数的回文字符串时，每次我们比较的两个字符奇偶性一定是相同的，所以原来字符串中的字符不会与插入的特殊字符互相比较，不会因此产生问题。


class Solution:
    def expand(self, s, left, right):
        while left >= 0 and right < len(s) and s[left] == s[right]:
            left -= 1
            right += 1
        return (right - left - 2) // 2

    def longestPalindrome(self, s: str) -> str:
        end, start = -1, 0
        s = '#' + '#'.join(list(s)) + '#'
        arm_len = []
        right = -1
        j = -1
        for i in range(len(s)):
            if right >= i:
                i_sym = 2 * j - i
                min_arm_len = min(arm_len[i_sym], right - i)
                cur_arm_len = self.expand(s, i - min_arm_len, i + min_arm_len)
            else:
                cur_arm_len = self.expand(s, i, i)
            arm_len.append(cur_arm_len)
            if i + cur_arm_len > right:
                j = i
                right = i + cur_arm_len
            if 2 * cur_arm_len + 1 > end - start:
                start = i - cur_arm_len
                end = i + cur_arm_len
        return s[start+1:end+1:2]
复杂度分析

时间复杂度：O(n)O(n)，其中 nn 是字符串的长度。由于对于每个位置，扩展要么从当前的最右侧臂长 right 开始，要么只会进行一步，而 right 最多向前走 O(n)O(n) 步，因此算法的复杂度为 O(n)O(n)。

空间复杂度：O(n)O(n)，我们需要 O(n)O(n) 的空间记录每个位置的臂长。

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
