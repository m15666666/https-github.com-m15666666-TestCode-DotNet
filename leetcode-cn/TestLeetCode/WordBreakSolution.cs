using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个非空字符串 s 和一个包含非空单词列表的字典 wordDict，判定 s 是否可以被空格拆分为一个或多个在字典中出现的单词。

说明：

拆分时可以重复使用字典中的单词。
你可以假设字典中没有重复的单词。
示例 1：

输入: s = "leetcode", wordDict = ["leet", "code"]
输出: true
解释: 返回 true 因为 "leetcode" 可以被拆分成 "leet code"。
示例 2：

输入: s = "applepenapple", wordDict = ["apple", "pen"]
输出: true
解释: 返回 true 因为 "applepenapple" 可以被拆分成 "apple pen apple"。
     注意你可以重复使用字典中的单词。
示例 3：

输入: s = "catsandog", wordDict = ["cats", "dog", "sand", "and", "cat"]
输出: false
 
 
*/
/// <summary>
/// https://leetcode-cn.com/problems/word-break/
/// 139.单词拆分
/// 给定一个非空字符串 s 和一个包含非空单词列表的字典 wordDict，判定 s 是否可以被空格拆分为一个或多个在字典中出现的单词。
/// 说明：
/// 拆分时可以重复使用字典中的单词。
/// 你可以假设字典中没有重复的单词。
/// </summary>
class WordBreakSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    public bool WordBreak(string s, IList<string> wordDict)
    { 
        var wordDictSet = new HashSet<string>(wordDict);
        var dp = new bool[s.Length + 1];
        dp[0] = true;
        for (int i = 1; i <= s.Length; i++) {
            for (int j = 0; j < i; j++) {
                if (dp[j] && wordDictSet.Contains(s.Substring(j, i - j))) {
                    dp[i] = true;
                    break;
                }
            }
        }
        return dp[s.Length];
    }
    //public bool WordBreak(string s, IList<string> wordDict)
    //{
    //    HashSet<string> set = new HashSet<string>(wordDict);
    //    Dictionary<int, bool> startIndex2CanWordBreak = new Dictionary<int, bool>();

    //    return BackTrack(s, 0, 0, set, startIndex2CanWordBreak);
    //}

    //private bool BackTrack( string s, int startIndex, int charCount, HashSet<string> wordDict, Dictionary<int, bool> startIndex2CanWordBreak )
    //{
    //    if (startIndex2CanWordBreak.ContainsKey(startIndex)) return startIndex2CanWordBreak[startIndex];

    //    var length = s.Length;
    //    if (length == charCount) return startIndex2CanWordBreak[startIndex] = true;

    //    for( int stopIndex = startIndex; stopIndex < length; stopIndex++)
    //    {
    //        var count = stopIndex - startIndex + 1;
    //        var subString = s.Substring(startIndex, count);
    //        if (!wordDict.Contains(subString)) continue;

    //        if (BackTrack(s, stopIndex + 1, charCount + count, wordDict, startIndex2CanWordBreak)) return startIndex2CanWordBreak[startIndex] = true;
    //    }

    //    return startIndex2CanWordBreak[startIndex] = false;
    //}

}
/*

单词拆分
力扣官方题解
发布于 2020-06-24
19.0k
方法一：动态规划
思路和算法

我们定义 \textit{dp}[i]dp[i] 表示字符串 ss 前 ii 个字符组成的字符串 s[0..i-1]s[0..i−1] 是否能被空格拆分成若干个字典中出现的单词。从前往后计算考虑转移方程，每次转移的时候我们需要枚举包含位置 i-1i−1 的最后一个单词，看它是否出现在字典中以及除去这部分的字符串是否合法即可。公式化来说，我们需要枚举 s[0..i-1]s[0..i−1] 中的分割点 jj ，看 s[0..j-1]s[0..j−1] 组成的字符串 s_1s 
1
​	
 （默认 j = 0j=0 时 s_1s 
1
​	
  为空串）和 s[j..i-1]s[j..i−1] 组成的字符串 s_2s 
2
​	
  是否都合法，如果两个字符串均合法，那么按照定义 s_1s 
1
​	
  和 s_2s 
2
​	
  拼接成的字符串也同样合法。由于计算到 \textit{dp}[i]dp[i] 时我们已经计算出了 \textit{dp}[0..i-1]dp[0..i−1] 的值，因此字符串 s_1s 
1
​	
  是否合法可以直接由 dp[j]dp[j] 得知，剩下的我们只需要看 s_2s 
2
​	
  是否合法即可，因此我们可以得出如下转移方程：

\textit{dp}[i]=\textit{dp}[j]\ \&\&\ \textit{check}(s[j..i-1])
dp[i]=dp[j] && check(s[j..i−1])

其中 \textit{check}(s[j..i-1])check(s[j..i−1]) 表示子串 s[j..i-1]s[j..i−1] 是否出现在字典中。

对于检查一个字符串是否出现在给定的字符串列表里一般可以考虑哈希表来快速判断，同时也可以做一些简单的剪枝，枚举分割点的时候倒着枚举，如果分割点 jj 到 ii 的长度已经大于字典列表里最长的单词的长度，那么就结束枚举，但是需要注意的是下面的代码给出的是不带剪枝的写法。

对于边界条件，我们定义 \textit{dp}[0]=truedp[0]=true 表示空串且合法。

有能力的读者也可以考虑怎么结合字典树 \textit{Trie}Trie 来实现，这里不再展开。


public class Solution {
    public boolean wordBreak(String s, List<String> wordDict) {
        Set<String> wordDictSet = new HashSet(wordDict);
        boolean[] dp = new boolean[s.length() + 1];
        dp[0] = true;
        for (int i = 1; i <= s.length(); i++) {
            for (int j = 0; j < i; j++) {
                if (dp[j] && wordDictSet.contains(s.substring(j, i))) {
                    dp[i] = true;
                    break;
                }
            }
        }
        return dp[s.length()];
    }
}
复杂度分析

时间复杂度：O(n^2)O(n 
2
 ) ，其中 nn 为字符串 ss 的长度。我们一共有 O(n)O(n) 个状态需要计算，每次计算需要枚举 O(n)O(n) 个分割点，哈希表判断一个字符串是否出现在给定的字符串列表需要 O(1)O(1) 的时间，因此总时间复杂度为 O(n^2)O(n 
2
 )。

空间复杂度：O(n)O(n) ，其中 nn 为字符串 ss 的长度。我们需要 O(n)O(n) 的空间存放 \textit{dp}dp 值以及哈希表亦需要 O(n)O(n) 的空间复杂度，因此总空间复杂度为 O(n)O(n)。

动态规划+记忆化回溯 逐行解释 python3
吴彦祖
发布于 2019-12-06
12.5k
题解
方法一：动态规划
在这里插入图片描述

初始化 dp=[False,\cdots,False]dp=[False,⋯,False]，长度为 n+1n+1。nn 为字符串长度。dp[i]dp[i] 表示 ss 的前 ii 位是否可以用 wordDictwordDict 中的单词表示。

初始化 dp[0]=Truedp[0]=True，空字符可以被表示。

遍历字符串的所有子串，遍历开始索引 ii，遍历区间 [0,n)[0,n)：

遍历结束索引 jj，遍历区间 [i+1,n+1)[i+1,n+1)：
若 dp[i]=Truedp[i]=True 且 s[i,\cdots,j)s[i,⋯,j) 在 wordlistwordlist 中：dp[j]=Truedp[j]=True。解释：dp[i]=Truedp[i]=True 说明 ss 的前 ii 位可以用 wordDictwordDict 表示，则 s[i,\cdots,j)s[i,⋯,j) 出现在 wordDictwordDict 中，说明 ss 的前 jj 位可以表示。
返回 dp[n]dp[n]

复杂度分析
时间复杂度：O(n^{2})O(n 
2
 )
空间复杂度：O(n)O(n)

class Solution:
    def wordBreak(self, s: str, wordDict: List[str]) -> bool:       
        n=len(s)
        dp=[False]*(n+1)
        dp[0]=True
        for i in range(n):
            for j in range(i+1,n+1):
                if(dp[i] and (s[i:j] in wordDict)):
                    dp[j]=True
        return dp[-1]



在这里插入图片描述

方法二：记忆化回溯
使用记忆化函数，保存出现过的 backtrack(s)backtrack(s)，避免重复计算。
定义回溯函数 backtrack(s)backtrack(s)
若 ss 长度为 00，则返回 TrueTrue，表示已经使用 wordDictwordDict 中的单词分割完。
初试化当前字符串是否可以被分割 res=Falseres=False
遍历结束索引 ii，遍历区间 [1,n+1)[1,n+1)：
若 s[0,\cdots,i-1]s[0,⋯,i−1] 在 wordDictwordDict 中：res=backtrack(s[i,\cdots,n-1])\ or\ resres=backtrack(s[i,⋯,n−1]) or res。解释：保存遍历结束索引中，可以使字符串切割完成的情况。
返回 resres
返回 backtrack(s)backtrack(s)

class Solution:
    def wordBreak(self, s: str, wordDict: List[str]) -> bool:
        import functools
        @functools.lru_cache(None)
        def back_track(s):
            if(not s):
                return True
            res=False
            for i in range(1,len(s)+1):
                if(s[:i] in wordDict):
                    res=back_track(s[i:]) or res
            return res
        return back_track(s)


public class Solution {
    public bool WordBreak(string s, IList<string> wordDict)
    {
        int max = 0;
        HashSet<string> set = new HashSet<string>();
        foreach (var word in wordDict)
        {
            set.Add(word);
            if (word.Length > max)
            {
                max = word.Length;
            }
        }
        bool[] dp = new bool[s.Length + 1];
        dp[0] = true;
        for (int i = 1; i <= s.Length; i++)
        {
            int start = Math.Max(0, i - max);
            for(int j = start; j < i; j++)
            {
                if(dp[j] && set.Contains(s.Substring(j, i - j)))
                {
                    dp[i] = true;
                    break;
                }
            }
        }
        return dp[s.Length];
    }
}

public class Solution {
    public bool WordBreak(string s, IList<string> wordDict) {
         bool[] dp = new bool[s.Length];

            for (int i = 0; i < s.Length; i++)
            {
                foreach (var word in wordDict)
                {
                    var len = word.Length;
                    int j = i + 1 - len;
                    if (j>=0)
                    {
                        var sub = s.Substring(j, len);
                        if (sub == word && (j == 0 || dp[j - 1]))
                        {
                            dp[i] = true;
                            break;
                        }
                    }
                }
            }

            return dp[s.Length - 1];
    }
}

public class Solution {
    public bool WordBreak(string s, IList<string> wordDict) {
        HashSet<string> dict = new HashSet<string>(wordDict.Count);

        int len = s.Length, min = int.MaxValue, max = 0;

        foreach (string word in wordDict)
        {
            min = Math.Min(min, word.Length);
            max = Math.Max(max, word.Length);
            dict.Add(word);
        }

        bool[] dp = new bool[len + 1];

        dp[0] = true;

        for (int i = 1; i <= len; ++i)
        {
            for (int j = min; j <= max && j <= i; ++j)
            {
                if (dp[i - j] && dict.Contains(s.Substring(i - j, j)))
                {
                    dp[i] = true;
                    break;
                }
            }
        }

        return dp[len];
    }
}

public class Solution {
    public bool WordBreak(string s, IList<string> wordDict) {
        if (wordDict == null || !wordDict.Any()) return false;
        if (string.IsNullOrEmpty(s)) return true;
        
        var wordsMatcher = new HashSet<string>(wordDict);
        var maxLength = wordDict.OrderByDescending(e => e.Length).First().Length;

        var DP = new bool[s.Length + 1];
        DP[0] = true;

        for (int i = 1; i <= s.Length; i++) for (int j = 0; j < i; j++) {
            if (i - j > maxLength) continue;

            if (DP[j] && wordsMatcher.Contains(s.Substring(j, i - j))) {
                DP[i] = true;
                break;
            }
        }

        return DP[s.Length];
    }
}

 
 
*/