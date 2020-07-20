using System;
using System.Collections.Generic;
using System.Linq;

/*
给定一个非空字符串 s 和一个包含非空单词列表的字典 wordDict，在字符串中增加空格来构建一个句子，使得句子中所有的单词都在词典中。返回所有这些可能的句子。

说明：

分隔时可以重复使用字典中的单词。
你可以假设字典中没有重复的单词。
示例 1：

输入:
s = "catsanddog"
wordDict = ["cat", "cats", "and", "sand", "dog"]
输出:
[
  "cats and dog",
  "cat sand dog"
]
示例 2：

输入:
s = "pineapplepenapple"
wordDict = ["apple", "pen", "applepen", "pine", "pineapple"]
输出:
[
  "pine apple pen apple",
  "pineapple pen apple",
  "pine applepen apple"
]
解释: 注意你可以重复使用字典中的单词。
示例 3：

输入:
s = "catsandog"
wordDict = ["cats", "dog", "sand", "and", "cat"]
输出:
[]

*/

/// <summary>
/// https://leetcode-cn.com/problems/word-break-ii/
/// 140. 单词拆分 II
///
///
///
///
/// </summary>
internal class WordBreakIISolution
{
    public void Test()
    {
        var ret = WordBreak("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaabaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", 
            new List<string> { "a", "aa", "aaa", "aaaa", "aaaaa", "aaaaaa", "aaaaaaa", "aaaaaaaa", "aaaaaaaaa", "aaaaaaaaaa" });
    }

    public class WordRef
    {
        public List<WordRef> Prev { get; set; }
        public int Index { get; set; }
        public int Length { get; set; }
    }

    public IList<string> WordBreak(string s, IList<string> wordDict)
    {
        if(wordDict.Count == 0) return new string[0];

        int len = s.Length;
        HashSet<string> set = new HashSet<string>(wordDict.Count);
        HashSet<int> lenSet = new HashSet<int>(wordDict.Count);
        foreach( var word in wordDict)
        {
            //word = string.Intern(word);
            set.Add(word);
            lenSet.Add(word.Length);
        }
        var lens = lenSet.ToArray();
        Array.Sort(lens);
        int maxLen = lens[lens.Length - 1];
        string[,] words = new string[len,len + 1];

        var dp = new List<WordRef>[len + 1];
        for (int i = 1; i <= len; i++)
        {
            var list = new List<WordRef>();
            dp[i] = list;

            if (i <= maxLen)
            {
                var sub = words[0,i] ?? (words[0,i] = s.Substring(0, i));
                if (set.Contains(sub)) list.Add(new WordRef { Index = 0, Length = i});
            }
            
            foreach( var wLen in lens) {
                var j = i - wLen;
                if (j < 1) break;
                if (0 == dp[j].Count) continue;

                var sub = words[j,wLen] ?? (words[j,wLen] = s.Substring(j, wLen));
                if (!set.Contains(sub)) continue;

                list.Add(new WordRef { Prev = dp[j], Index = j, Length = wLen });
                //foreach (var l in dp[j])
                    //list.Add(new WordRef { Prev = l, Index = j, Length = wLen });
            }
        }
        List<string> ret = new List<string>();
        var builder = new System.Text.StringBuilder();
        Stack<WordRef> stack = new Stack<WordRef>();
        foreach( var l in dp[len] )
            Print(l);
        return ret;

        void Print( WordRef word )
        {
            stack.Push(word);
            if(word.Prev != null)
            {
                foreach( var w in word.Prev)
                    Print(w);
            }
            else
            {
                builder.Length = 0;
                foreach( var w in stack)
                    builder.Append(words[w.Index, w.Length]).Append(' ');
                ret.Add( builder.ToString(0, builder.Length - 1) );
            }
            stack.Pop();
        }
    }
    //public IList<string> WordBreak(string s, IList<string> wordDict)
    //{
    //    if(wordDict.Count == 0) return new string[0];

    //    int len = s.Length;
    //    HashSet<string> set = new HashSet<string>(wordDict.Count);
    //    HashSet<int> lenSet = new HashSet<int>(wordDict.Count);
    //    foreach( var word in wordDict)
    //    {
    //        set.Add(word);
    //        lenSet.Add(word.Length);
    //    }
    //    var lens = lenSet.ToArray();
    //    Array.Sort(lens);
    //    int maxLen = lens[lens.Length - 1];

    //    var dp = new List<List<(int,int)>>[len + 1];
    //    for (int i = 1; i <= len; i++)
    //    {
    //        var list = new List<List<(int,int)>>();
    //        dp[i] = list;

    //        if (i <= maxLen)
    //        {
    //            var sub = s.Substring(0, i);
    //            if (set.Contains(sub)) list.Add(new List<(int, int)> { (0, i) });
    //        }
            
    //        foreach( var wLen in lens) {
    //            var j = i - wLen;
    //            if (j < 1) break;
    //            if (0 == dp[j].Count) continue;

    //            var sub = s.Substring(j, wLen);
    //            if (!set.Contains(sub)) continue;

    //            foreach (var l in dp[j])
    //                list.Add(new List<(int, int)>(l)
    //                {
    //                    (j, wLen)
    //                });
    //        }
    //    }
    //    List<string> ret = new List<string>();
    //    var builder = new System.Text.StringBuilder();
    //    //string.Intern()
    //    foreach( var l in dp[len] )
    //    {
    //        builder.Length = 0;
    //        foreach (var pair in l)
    //            builder.Append(s.Substring(pair.Item1, pair.Item2)).Append(" ");
    //        builder.Remove(builder.Length - 1,1);
    //        ret.Add(builder.ToString());
    //    }
    //    return ret;
    //}
}

/*

单词拆分 II
力扣 (LeetCode)
发布于 2019-06-10
20.9k
方法 1：暴力
算法

解决此题最简单的方法是使用回溯。为了找到解，我们检查字符串（ss）的所有可能前缀是否在字典中，如果在（比方说 s1s1 ），那么调用回溯函数并检查剩余部分的字符串。
如果剩余部分可以形成有效拆分，这个函数返回前缀 s1s1 ，并将回溯调用的剩余结果（即 s-s1s−s1）跟在 s1s1 的后面。否则，返回空列表。


public class Solution {
    public List<String> wordBreak(String s, Set<String> wordDict) {
        return word_Break(s, wordDict, 0);
    }
    public List<String> word_Break(String s, Set<String> wordDict, int start) {
        LinkedList<String> res = new LinkedList<>();
        if (start == s.length()) {
            res.add("");
        }
        for (int end = start + 1; end <= s.length(); end++) {
            if (wordDict.contains(s.substring(start, end))) {
                List<String> list = word_Break(s, wordDict, end);
                for (String l : list) {
                    res.add(s.substring(start, end) + (l.equals("") ? "" : " ") + l);
                }
            }
        }
        return res;
    }
}
复杂度分析

时间复杂度：O(n^n)O(n 
n
 )，考虑最坏情况 ss = "\text{aaaaaaa}aaaaaaa"，ss 的每一个前缀都在字典中，回溯树的大小会达到 n^nn 
n
  。

空间复杂度：O(n^3)O(n 
3
 )，最坏情况下，回溯的深度可以达到 nn 层，每层可能包含 nn 个字符串，且每个字符串的长度都为 nn 。



方法 2：记忆化回溯
算法

在之前的方法中，我们可以看出许多子问题的求解都是冗余的，也就是我们对于相同的子串调用了函数多次。

为了避免这种情况，我们使用记忆化的方法，我们使用一个 key:valuekey:value 这样的哈希表来进行优化。在哈希表中， keykey 是当前考虑字符串的开始下标， valuevalue 包含了所有从头开始的所有可行句子。下次我们遇到相同位置开始的调用时，我们可以直接从哈希表里返回结果，而不需要重新计算结果。

通过记忆化的方法，许多冗余的子问题都可以被省去，回溯树得到了剪枝，所以极大程度降低了时间复杂度。


public class Solution {

    public List<String> wordBreak(String s, Set<String> wordDict) {
        return word_Break(s, wordDict, 0);
    }
    HashMap<Integer, List<String>> map = new HashMap<>();

    public List<String> word_Break(String s, Set<String> wordDict, int start) {
        if (map.containsKey(start)) {
            return map.get(start);
        }
        LinkedList<String> res = new LinkedList<>();
        if (start == s.length()) {
            res.add("");
        }
        for (int end = start + 1; end <= s.length(); end++) {
            if (wordDict.contains(s.substring(start, end))) {
                List<String> list = word_Break(s, wordDict, end);
                for (String l : list) {
                    res.add(s.substring(start, end) + (l.equals("") ? "" : " ") + l);
                }
            }
        }
        map.put(start, res);
        return res;
    }
}
复杂度分析

时间复杂度：O(n^3)O(n 
3
 ) 。回溯树的大小最多 n^2n 
2
  。创建列表需要 nn 的时间。


方法 3：使用动态规划
算法

这个方法背后的想法是对于给定的问题（ss），它可以被拆分成子问题 s1s1 和 s2s2 。如果这些子问题分别都能满足条件，那么整个文字 ss 也可以满足。比方说， "\text{catsanddog}catsanddog" 可以被拆分成子字符串 "\text{catsand}catsand" 和 "\text{dog}dog" 。子问题 "\text{catsand}catsand" 进一步可以被拆分成 "\text{cats}" 和 "\text{and}" ，它们分别都是字典的一部分，所以 "\text{catsand}catsand" 也是满足条件的。递归回来，因为 "\text{catsand}catsand" 和 "\text{dog}dog" 分别都满足要求，所以原字符串 "\text{catsanddog}catsanddog" 也符合要求。

现在，我们来考虑 \text{dp}dp 数组如何求出。我们使用长度为 n+1n+1 的数组 \text{dp}dp ，其中 nn 是给定字符串的长度。 \text{dp}[k]dp[k] 被用来存储用 s[0:k-1]s[0:k−1] 可被拆分成合法单词的句子。我们同事用两个指针 ii 和 jj ，其中 ii 表示子字符串 s's 
′
  的长度（s's 
′
  是 ss 的一个前缀）， jj 表示 s's 
′
  的拆分位置，即拆分成 s'(0,j)s 
′
 (0,j) 和 s'(j+1,i)s 
′
 (j+1,i) 。

为了求出 \text{dp}dp 数组，我们将 \text{dp}[0]dp[0] 初始化为空串。我们以 ii 为结尾表示的子字符串的所有前缀，通过指针 jj 将 ss 拆分成 s1's1 
′
  和 s2's2 
′
  。为了求出\text{dp}[i]dp[i] ，我们检查所有 \text{dp}[j]dp[j] 包含的所有非空字符串，也就是所有能形成 s1's1 
′
  的句子。如果存在，我们进一步检查 s2's2 
′
  是否在字典里，如果两个条件都满足，我们将子字符串 s2's2 
′
  添加到所有 s1's1 
′
  的句子的后面（这些句子已经保存在了 dp[j]dp[j] 里面），并将这些新形成的句子保存进（\text{dp}[i]dp[i]）。最终， \text{dp}[n]dp[n] （nn 是给定字符串 ss 的长度）里面保存了所有可以得到完整字符串 ss 的所有句子。


public class Solution {
   public List<String> wordBreak(String s, Set<String> wordDict) {
       LinkedList<String>[] dp = new LinkedList[s.length() + 1];
       LinkedList<String> initial = new LinkedList<>();
       initial.add("");
       dp[0] = initial;
       for (int i = 1; i <= s.length(); i++) {
           LinkedList<String> list = new LinkedList<>();
           for (int j = 0; j < i; j++) {
               if (dp[j].size() > 0 && wordDict.contains(s.substring(j, i))) {
                   for (String l : dp[j]) {
                       list.add(l + (l.equals("") ? "" : " ") + s.substring(j, i));
                   }
               }
           }
           dp[i] = list;
       }
       return dp[s.length()];
   }
}
复杂度分析

时间复杂度：O(n^3)O(n 
3
 )，求 \text{dp}dp 需要两重循环，添加一个新的列表需要额外一重循环。

空间复杂度：O(n^3)O(n 
3
 )，\text{dp}dp 数组的长度是nn ， \text{dp}dp 数组里保存了数组，数组里是一些字符串，也就是每个 \text{dp}dp 元素需要 n^2n 
2
  的空间。

下一篇：python3 动态规划

public class Solution {
    public IList<string> WordBreak(string s, IList<string> wordDict)
    {
        var n = s.Length;
        var split = new List<int>[n + 1];
        split[0] = new List<int>();
        for (var i = 0; i <= n; i++)
        {
            if (split[i] == null)
                continue;
            for (var j = 0; j < wordDict.Count; j++)
            {
                if (isSame(s, i, wordDict[j]))
                {
                    if (split[i + wordDict[j].Length] == null)
                        split[i + wordDict[j].Length] = new List<int>();
                    split[i + wordDict[j].Length].Add(i);
                }
                    
            }
        }
        var result = new List<string>();
        dfs(s, split, n, "", result);
        return result;
    }

    private void dfs(string s, List<int>[] split, int cur, string words, List<string> result)
    {
        if (cur == 0)
        {
            result.Add(words.Substring(0, words.Length - 1));
            return;
        }
        if (split[cur] == null)
            return;
        for (var i = 0; i < split[cur].Count; i++)
        {
            dfs(s, split, split[cur][i], s.Substring(split[cur][i], cur - split[cur][i]) + " " + words, result);
        }
    }

    private bool isSame(string s, int start, string t)
    {
        if (start + t.Length > s.Length)
            return false;
        for (var i = 0; i < t.Length; i++)
        {
            if (s[start + i] != t[i])
                return false;
        }
        return true;
    }
}

public class Solution {
    public IList<string> WordBreak(string s, IList<string> wordDict)
    {
        HashSet<int> length = new HashSet<int>(wordDict.Count);
        HashSet<string> words = new HashSet<string>(wordDict.Count);
        foreach (var w in wordDict)
        {
            length.Add(w.Length);
            words.Add(w);
        }
        var length2 = length.ToList();
        length2.Sort();
        Dictionary<string, List<string>> cache = new Dictionary<string, List<string>>();
        return WordBreak(cache, s, words, length2);
    }

    private List<string> WordBreak(Dictionary<string, List<string>> cache, string s, HashSet<string> words, List<int> length)
    {
        if (cache.TryGetValue(s, out var result))
        {
            return result;
        }
        result = new List<string>();
        foreach (var l in length)
        {
            if (l > s.Length)
            {
                break;
            }
            string temp = s.Substring(0, l);
            if (words.Contains(temp))
            {
                string next = s.Substring(l);
                if (l < s.Length)
                {
                    var nextResult = WordBreak(cache, next, words, length);
                    foreach (var r in nextResult)
                    {
                        result.Add($"{temp} {r}");
                    }
                }
                else
                {
                    result.Add(temp);
                }
            }
        }
        cache[s] = result;
        return result;
    }
}

public class Solution {
        //动态规划
        //举例：检查到字符串的第i位，判断字符串w长度为l
        ///////判断字符串从第0位到第i-l为止，是否可以匹配（0||1）
        ///////判断字符串从第i-l+1位到第i位是否与w匹配
        ///////如果匹配，获取第i-l位储存的所有可能组合形式，并加上w储存于第i位（list<string>）
        public IList<string> WordBreak(string s, IList<string> wordDict)
        {
            if (string.IsNullOrWhiteSpace(s) || !wordDict.Any()) return new List<string>();

            var dyn = new List<List<string>>();

            int maxLen = Int16.MinValue;
            var dic = new Dictionary<int, List<string>>();
            //找到字典中最长的字数
            foreach (var word in wordDict)
            {
                maxLen = maxLen > word.Length ? maxLen : word.Length;
                //根据字符串长度分组，可以减少对匹配字典的循环次数
                if (dic.ContainsKey(word.Length)) dic[word.Length].Add(word);
                else dic.Add(word.Length, new List<string> { word });
            }
            int noMatchLen = 0;//当前未匹配的长度
            for (var i = 0; i < s.Length; i++)
            {
                if (noMatchLen > maxLen) return new List<string>();//未匹配的长度已经超出字典最长字符串长度
                dyn.Add(new List<string>());//先赋值，延伸dyn长度
                foreach(var len in dic.Keys)
                {
                    //该长度没有过长，且第i - len位存在可匹配的组合;或刚好匹配至第0位
                    if ((i - len >= 0 && dyn[i - len].Any()) || (i - len == -1))
                    {
                        var subS = s.Substring(i - len + 1, len);
                        //循环匹配所有长度为len的单词
                        foreach (var word in dic[len])
                        {
                            if (word != subS) continue;
                            //对可以匹配上的，直接在该字符串对应的动态字典里装入可匹配的字符串
                            dyn[i].Add(word);
                        }
                    }
                }
                //根据该字符位置是否有可匹配的单词，来决定noMatchLen归零还是加一
                noMatchLen = dyn[i].Any() ? 0 : ++noMatchLen;
            }

            var result = new List<string>();
            if (!dyn[dyn.Count - 1].Any()) return result;
            //通过递归来组装所有可能的字符
            result = FormatResult(dyn, s.Length - 1);
            return result;
        }

        public List<string> FormatResult(List<List<string>> dyn, int end)
        {
            var result = new List<string>();
            if (end == -1) return result;
            
            //对第end字符对应的所有可匹配的单词进行循环
            foreach (var sub in dyn[end])
            {
                //一直往前走，直到找到第一个
                var subResult = FormatResult(dyn, end - sub.Length);
                if(!subResult.Any())
                {
                    result.Add(sub);
                }
                else
                {
                    foreach (var sr in subResult)
                    {
                        result.Add(string.Format("{0} {1}", sr, sub));
                    }
                }
            }
            return result;
        }

        
}
*/
