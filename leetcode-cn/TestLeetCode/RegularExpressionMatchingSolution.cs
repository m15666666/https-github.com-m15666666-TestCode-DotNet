using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给你一个字符串 s 和一个字符规律 p，请你来实现一个支持 '.' 和 '*' 的正则表达式匹配。

'.' 匹配任意单个字符
'*' 匹配零个或多个前面的那一个元素
所谓匹配，是要涵盖 整个 字符串 s的，而不是部分字符串。

说明:

s 可能为空，且只包含从 a-z 的小写字母。
p 可能为空，且只包含从 a-z 的小写字母，以及字符 . 和 *。
示例 1:

输入:
s = "aa"
p = "a"
输出: false
解释: "a" 无法匹配 "aa" 整个字符串。
示例 2:

输入:
s = "aa"
p = "a*"
输出: true
解释: 因为 '*' 代表可以匹配零个或多个前面的那一个元素, 在这里前面的元素就是 'a'。因此，字符串 "aa" 可被视为 'a' 重复了一次。
示例 3:

输入:
s = "ab"
p = ".*"
输出: true
解释: ".*" 表示可匹配零个或多个（'*'）任意字符（'.'）。
示例 4:

输入:
s = "aab"
p = "c*a*b"
输出: true
解释: 因为 '*' 表示零个或多个，这里 'c' 为 0 个, 'a' 被重复一次。因此可以匹配字符串 "aab"。
示例 5:

输入:
s = "mississippi"
p = "mis*is*p*."
输出: false
*/
/// <summary>
/// https://leetcode-cn.com/problems/regular-expression-matching/
/// 10. 正则表达式匹配
/// 
/// </summary>
class RegularExpressionMatchingSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public bool IsMatch(string s, string p)
    {
        int sLen = s.Length;
        int pLen = p.Length;
        bool[,] dp = new bool[sLen + 1,pLen + 1];
        dp[sLen,pLen] = true;

        for (int j = pLen - 2; -1 < j; j--) // 开始匹配: 末尾的空字符串
            if (j + 1 < pLen && p[j + 1] == '*') dp[sLen, j] = dp[sLen, j + 2];

        for (int i = sLen - 1; -1 < i; i--) 
            for (int j = pLen - 1; -1 < j; j--)
            {
                var c = p[j];
                bool firstMatch = (c == s[i] || c == '.');

                if (j + 1 < pLen && p[j + 1] == '*') dp[i,j] = dp[i,j + 2] || (firstMatch && dp[i + 1,j]);
                else dp[i,j] = firstMatch && dp[i + 1,j + 1];
            }

        return dp[0,0];
    }
}
/*
正则表达式匹配
力扣 (LeetCode)
发布于 10 个月前
78.2k
方法 1：回溯
想法

如果没有星号（正则表达式中的 * ），问题会很简单——我们只需要从左到右检查匹配串 s 是否能匹配模式串 p 的每一个字符。

当模式串中有星号时，我们需要检查匹配串 s 中的不同后缀，以判断它们是否能匹配模式串剩余的部分。一个直观的解法就是用回溯的方法来体现这种关系。

算法

如果没有星号，我们的代码会像这样：

def match(text, pattern):
    if not pattern: return not text
    first_match = bool(text) and pattern[0] in {text[0], '.'}
    return first_match and match(text[1:], pattern[1:])
如果模式串中有星号，它会出现在第二个位置，即 \text{pattern[1]}pattern[1] 。这种情况下，我们可以直接忽略模式串中这一部分，或者删除匹配串的第一个字符，前提是它能够匹配模式串当前位置字符，即 \text{pattern[0]}pattern[0] 。如果两种操作中有任何一种使得剩下的字符串能匹配，那么初始时，匹配串和模式串就可以被匹配。

class Solution {
    public boolean isMatch(String text, String pattern) {
        if (pattern.isEmpty()) return text.isEmpty();
        boolean first_match = (!text.isEmpty() &&
                               (pattern.charAt(0) == text.charAt(0) || pattern.charAt(0) == '.'));

        if (pattern.length() >= 2 && pattern.charAt(1) == '*'){
            return (isMatch(text, pattern.substring(2)) ||
                    (first_match && isMatch(text.substring(1), pattern)));
        } else {
            return first_match && isMatch(text.substring(1), pattern.substring(1));
        }
    }
}
复杂度分析

时间复杂度：用 TT 和 PP 分别表示匹配串和模式串的长度。在最坏情况下，函数 match(text[i:], pattern[2j:]) 会被调用 \binom{i+j}{i}( 
i
i+j
​	
 ) 次，并留下长度为 O(T - i)O(T−i) 和 O(P - 2*j)O(P−2∗j) 长度的字符串。因此，总时间为 \sum_{i = 0}^T \sum_{j = 0}^{P/2} \binom{i+j}{i} O(T+P-i-2j)∑ 
i=0
T
​	
 ∑ 
j=0
P/2
​	
 ( 
i
i+j
​	
 )O(T+P−i−2j) 。通过本文以外的一些知识，我们可以证明它的时间复杂度为 O\big((T+P)2^{T + \frac{P}{2}}\big)O((T+P)2 
T+ 
2
P
​	
 
 ) 。

空间复杂度：对于 match 函数的每一次调用，我们都会产生如上所述的字符串，可能还会产生重复的字符串。如果内存没有被重复利用，那么即使只有总量为 O(T^2 + P^2)O(T 
2
 +P 
2
 ) 个不同的后缀，也会花费总共 O\big((T+P)2^{T + \frac{P}{2}}\big)O((T+P)2 
T+ 
2
P
​	
 
 ) 的空间。

方法 2: 动态规划
想法

因为题目拥有 最优子结构 ，一个自然的想法是将中间结果保存起来。我们通过用 \text{dp(i,j)}dp(i,j) 表示 \text{text[i:]}text[i:] 和 \text{pattern[j:]}pattern[j:] 是否能匹配。我们可以用更短的字符串匹配问题来表示原本的问题。

算法

我们用 [方法 1] 中同样的回溯方法，除此之外，因为函数 match(text[i:], pattern[j:]) 只会被调用一次，我们用 \text{dp(i, j)}dp(i, j) 来应对剩余相同参数的函数调用，这帮助我们节省了字符串建立操作所需要的时间，也让我们可以将中间结果进行保存。

自顶向下的方法

enum Result {
    TRUE, FALSE
}

class Solution {
    Result[][] memo;

    public boolean isMatch(String text, String pattern) {
        memo = new Result[text.length() + 1][pattern.length() + 1];
        return dp(0, 0, text, pattern);
    }

    public boolean dp(int i, int j, String text, String pattern) {
        if (memo[i][j] != null) {
            return memo[i][j] == Result.TRUE;
        }
        boolean ans;
        if (j == pattern.length()){
            ans = i == text.length();
        } else{
            boolean first_match = (i < text.length() &&
                                   (pattern.charAt(j) == text.charAt(i) ||
                                    pattern.charAt(j) == '.'));

            if (j + 1 < pattern.length() && pattern.charAt(j+1) == '*'){
                ans = (dp(i, j+2, text, pattern) ||
                       first_match && dp(i+1, j, text, pattern));
            } else {
                ans = first_match && dp(i+1, j+1, text, pattern);
            }
        }
        memo[i][j] = ans ? Result.TRUE : Result.FALSE;
        return ans;
    }
}
自底向上的方法

class Solution {
    public boolean isMatch(String text, String pattern) {
        boolean[][] dp = new boolean[text.length() + 1][pattern.length() + 1];
        dp[text.length()][pattern.length()] = true;

        for (int i = text.length(); i >= 0; i--){
            for (int j = pattern.length() - 1; j >= 0; j--){
                boolean first_match = (i < text.length() &&
                                       (pattern.charAt(j) == text.charAt(i) ||
                                        pattern.charAt(j) == '.'));
                if (j + 1 < pattern.length() && pattern.charAt(j+1) == '*'){
                    dp[i][j] = dp[i][j+2] || first_match && dp[i+1][j];
                } else {
                    dp[i][j] = first_match && dp[i+1][j+1];
                }
            }
        }
        return dp[0][0];
    }
}
复杂度分析

时间复杂度：用 TT 和 PP 分别表示匹配串和模式串的长度。对于i=0, ... , Ti=0,...,T 和 j=0, ... , Pj=0,...,P 每一个 dp(i, j)只会被计算一次，所以后面每次调用都是 O(1)O(1) 的时间。因此，总时间复杂度为 O(TP)O(TP) 。

空间复杂度：我们用到的空间仅有 O(TP)O(TP) 个 boolean 类型的缓存变量。所以，空间复杂度为 O(TP)O(TP) 。

class Solution
{
    public bool IsMatch(string s, string p)
    {
        // 先对p做一个格式化，形成等价的p，格式化过程中做以下处理：
        // 1、遇到"c*c*"这样的情形，忽略后一个c*
        // 2、遇到"c*c"这样的的情形将后面的c前移，形成"cc*"
        StringBuilder builder = new StringBuilder(), builderTmp = new StringBuilder();
        char c = '\0';

        // 对p的格式化，i不进行回撤，因此，复杂度为O(n)级
        for (int i = 0; i < p.Length;)
        {
            // 判断下一个字符字符，上述的特殊情况，第二个字符都是*，如果下一个字符会越界，则也不会出现特殊情况
            if (i + 1 == p.Length || p[i + 1] != '*')
            {
                builder.Append(p[i]);
                ++i;
                continue;
            }

            // 如果是"*"，则要判断是否是上述情况中的一种
            c = p[i];
            // c*模式
            if (i + 2 == p.Length)
            {
                // c*是最后一个了，不用再管
                builder.Append(c);      // i
                builder.Append('*');    // i+1
                i += 2;
                break;
            }

            for (i = i + 2; i + 1 < p.Length;)
            {
                if (p[i + 1] == '*')
                {
                    // 连续的c*
                    if (c == '.' || p[i] == c)
                    {
                        // .*后跟着其他的c*或者连续相同的c*，忽略c*
                        i += 2;
                        continue;
                    }

                    if (p[i] == '.')
                    {
                        // c*后跟着.*，将c改为.，将builderTmp清空，并将c置为.
                        builderTmp.Clear();
                        c = '.';
                        i += 2;
                        continue;
                    }

                    // 如果是两对都没有.*，那么将前一对加到builderTmp中，然后c记为后一对
                    builderTmp.Append(c);
                    builderTmp.Append('*');
                    c = p[i];
                    i += 2;
                    // continue;
                }
                else
                {
                    // 不是连续的c*，那么先将builderTmp中的内容append到builder中
                    builder.Append(builderTmp.ToString());
                    builderTmp.Clear();

                    if (p[i] == c)
                    {
                        // c*c的模式，将c添加到builder中
                        builder.Append(c);
                        ++i;
                        continue;
                    }

                    // 其他情况就跳出当前循环，循环外会将当前的c*添加到builder中
                    break;
                }
            }

            // 如果builderTmp中有内容，则先添加builderTmp，再添加c*
            builder.Append(builderTmp.ToString());
            builderTmp.Clear();

            // 把c*添加到字符串
            builder.Append(c);
            builder.Append('*');

            // continue;
        }

        // p格式化完成后，开始匹配
        return this.IsMatch(s, builder.ToString(), 0, 0, false, '\0');
    }

    private bool IsMatch(string s, string p, int sBegin, int pBegin, bool canSkip, char skipChar)
    {
        if (sBegin == s.Length && pBegin == p.Length)
        {
            // s和p都是末尾了
            return true;
        }

        if (pBegin == p.Length)
        {
            // p已经到尾部了，那就要看是否能跳过s的头部以及skipChar的情况
            if (canSkip)
            {
                if (skipChar == '.')
                {
                    // 可以跳过任意字符，直接返回true
                    return true;
                }

                for (; sBegin < s.Length; ++sBegin)
                {
                    if (s[sBegin] != skipChar)
                    {
                        return false;
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        int[] skipDistance = new int[26];

        // 找到maxSkip的值，pBegin之前的都不算
        int maxSkip = p.IndexOf('*', pBegin) - 1 - pBegin;
        if (maxSkip < 0)
        {
            maxSkip = p.Length - pBegin;
        }

        if (canSkip && skipChar == '.')
        {
            // 如果可以跳过头部并且是任意字符的跳过，则采用sunday算法优化比较，最大跳跃只能到*的前两个字符为止，即c*是不参与比较的，如果p中有.，那么跳跃的时候p只能跳到.为止
            int dotIndex = -1;
            for (int i = pBegin; i < p.Length; ++i)
            {
                if (i + 1 < p.Length && p[i + 1] == '*')
                {
                    // 如果下一个是*，则跳出，此次递归中需要比较的字符已经搜集完了
                    break;
                }
                if (p[i] == '.')
                {
                    dotIndex = i - pBegin;
                    continue;
                }
                skipDistance[p[i] - 'a'] = maxSkip - i + pBegin;
            }

            for (int i = 0; i < skipDistance.Length; ++i)
            {
                if (skipDistance[i] > maxSkip - dotIndex || skipDistance[i] == 0)
                {
                    skipDistance[i] = maxSkip - dotIndex;
                }
            }
        }

        int pIndex = pBegin;
        while (sBegin < s.Length)
        {
            // 先判断下一个是否是*
            if (pIndex + 1 < p.Length && p[pIndex + 1] == '*')
            {
                // 遇到c*，进行递归，s从当前字符开始（c*最少跳过0个字符），p从*的下一个字符开始
                if (this.IsMatch(s, p, sBegin, pIndex + 2, true, p[pIndex]))
                {
                    return true;
                }
            }
            else if (pIndex < p.Length && (s[sBegin] == p[pIndex] || p[pIndex] == '.'))
            {
                // 相同或者p为"."，过
                ++sBegin;
                ++pIndex;
                continue;
            }

            // 如果递归没有匹配上，或者当前字符不匹配，或者p已经结束了，那么就需要判断是否能够跳过，因为此时s还没有结束
            if (!canSkip)
            {
                // 不能跳过字符，直接返回false
                return false;
            }

            // 如果能够跳过，则看是否能够跳过任意字符
            if (skipChar == '.')
            {
                // 按照sunday算法跳跃，pIndex重置为pBegin
                if (sBegin - pIndex + pBegin + maxSkip >= s.Length || sBegin - pIndex + pBegin + skipDistance[s[sBegin - pIndex + pBegin + maxSkip] - 'a'] >= s.Length)
                {
                    // 跳过后越界，直接返回false
                    return false;
                }
                sBegin += skipDistance[s[sBegin - pIndex + pBegin + maxSkip] - 'a'] - pIndex + pBegin;
                pIndex = pBegin;
                continue;
            }

            // 如果只能跳过特定字符，那么就判断s和skipChar是否一致，一致就跳一个字符后后重新从头匹配
            if (s[sBegin - pIndex + pBegin] == skipChar)
            {
                sBegin = sBegin - pIndex + pBegin + 1;
                pIndex = pBegin;
                continue;
            }

            // 能跳过的特定字符与s不匹配，则返回false
            return false;
        }

        // 最后判断一下begin和pIndex的情况
        if (sBegin == s.Length && pIndex == p.Length)
        {
            // 都正好匹配结束，返回true
            return true;
        }
        else if (sBegin < s.Length)
        {
            // 如果s没有走完，那就是没有匹配到最后，返回false
            return false;
        }
        else if (pIndex < p.Length - 1)
        {
            // p没有走完，看看其后面的偶数个位置是否都是*(即a*b*c*类似模式)，只要有一个不是，那就返回false
            if ((p.Length - pIndex) % 2 == 0)
            {
                // 必须得是偶数个，*前的那个字符没有意义，直接看*的位置是否符合预期
                for (pIndex += 1; pIndex < p.Length; pIndex += 2)
                {
                    if (p[pIndex] != '*')
                    {
                        return false;
                    }
                }

                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            // 其他情况
            return false;
        }
    }
}

public class Solution {
    public bool IsMatch(string s, string p) {
        if (s == null || p == null)
        {
            return false;
        }
        bool[,] dp = new bool[s.Length + 1,p.Length + 1];
        dp[0,0] = true;//dp[i][j] 表示 s 的前 i 个是否能被 p 的前 j 个匹配
        for (int i = 0; i < p.Length; i++)
        { // here's the p's length, not s's
            if (p[i] == '*' && dp[0,i - 1])
            {
                dp[0,i + 1] = true; // here's y axis should be i+1
            }
        }
        for (int i = 0; i < s.Length; i++)
        {
            for (int j = 0; j < p.Length; j++)
            {
                if (p[j] == '.' || p[j] == s[i])
                {//如果是任意元素 或者是对于元素匹配
                    dp[i + 1,j + 1] = dp[i,j];
                }
                if (p[j] == '*')
                {
                    if (p[j - 1] != s[i] && p[j - 1] != '.')
                    {//如果前一个元素不匹配 且不为任意元素
                        dp[i + 1,j + 1] = dp[i + 1,j - 1];
                    }
                    else
                    {
                        dp[i + 1,j + 1] = (dp[i + 1,j] || dp[i,j + 1] || dp[i + 1,j - 1]);
                        //dp[i][j] = dp[i-1][j] // 多个字符匹配的情况	
                        //or dp[i][j] = dp[i][j-1] // 单个字符匹配的情况
                        //or dp[i][j] = dp[i][j-2] // 没有匹配的情况


                    }
                }
            }
        }
        return dp[s.Length, p.Length];

    }
}
*/