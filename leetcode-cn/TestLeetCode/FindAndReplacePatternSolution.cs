using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
你有一个单词列表 words 和一个模式  pattern，你想知道 words 中的哪些单词与模式匹配。

如果存在字母的排列 p ，使得将模式中的每个字母 x 替换为 p(x) 之后，我们就得到了所需的单词，那么单词与模式是匹配的。

（回想一下，字母的排列是从字母到字母的双射：每个字母映射到另一个字母，没有两个字母映射到同一个字母。）

返回 words 中与给定模式匹配的单词列表。

你可以按任何顺序返回答案。

 

示例：

输入：words = ["abc","deq","mee","aqq","dkd","ccc"], pattern = "abb"
输出：["mee","aqq"]
解释：
"mee" 与模式匹配，因为存在排列 {a -> m, b -> e, ...}。
"ccc" 与模式不匹配，因为 {a -> c, b -> c, ...} 不是排列。
因为 a 和 b 映射到同一个字母。
 

提示：

1 <= words.length <= 50
1 <= pattern.length = words[i].length <= 20
*/
/// <summary>
/// https://leetcode-cn.com/problems/find-and-replace-pattern/
/// 890. 查找和替换模式
/// 
/// </summary>
class FindAndReplacePatternSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public IList<string> FindAndReplacePattern(string[] words, string pattern)
    {
        List<string> ret = new List<string>();
        foreach (string word in words)
            if (Match(word, pattern)) ret.Add(word);

        return ret;
    }

    Dictionary<char, char> Word2Pattern = new Dictionary<char, char>();
    Dictionary<char, char> Pattern2Word = new Dictionary<char, char>();

    private bool Match(string word, string pattern)
    {
        Word2Pattern.Clear();
        Pattern2Word.Clear();

        for (int i = 0; i < word.Length; ++i)
        {
            char w = word[i];
            char p = pattern[i];
            if (!Word2Pattern.ContainsKey(w)) Word2Pattern.Add(w, p);
            if (!Pattern2Word.ContainsKey(p)) Pattern2Word.Add(p, w);
            if (Word2Pattern[w] != p || Pattern2Word[p] != w) return false;
        }

        return true;
    }
}
/*
方法一：双映射表
我们可以用两个映射表（map）存储字母到字母的映射关系，第一个映射表保证一个字母不会映射到两个字母，第二个映射表保证不会有两个字母映射到同一个字母。例如 word 为 a，pattern 为 x，那么第一个映射表存储 a -> x，第二个映射表存储 x -> a。

JavaPython
class Solution {
    public List<String> findAndReplacePattern(String[] words, String pattern) {
        List<String> ans = new ArrayList();
        for (String word: words)
            if (match(word, pattern))
                ans.add(word);
        return ans;
    }

    public boolean match(String word, String pattern) {
        Map<Character, Character> m1 = new HashMap();
        Map<Character, Character> m2 = new HashMap();

        for (int i = 0; i < word.length(); ++i) {
            char w = word[i);
            char p = pattern[i);
            if (!m1.containsKey(w)) m1.Add(w, p);
            if (!m2.containsKey(p)) m2.Add(p, w);
            if (m1[w) != p || m2[p) != w)
                return false;
        }

        return true;
    }
}
复杂度分析

时间复杂度：O(N * K)O(N?K)，其中 NN 是数组 words 的长度，KK 是每个单词的长度。

空间复杂度：O(N * K)O(N?K)。

方法二：单映射表
我们可以删去方法一中的第二个映射表，改成在添加完所有映射关系后，遍历第一个映射表并使用一个数组记录每个值出现的次数。如果某个值出现了超过一次，就说明有两个字母映射到同一个字母，否则映射就是合法的。

JavaPython
class Solution {
    public List<String> findAndReplacePattern(String[] words, String pattern) {
        List<String> ans = new ArrayList();
        for (String word: words)
            if (match(word, pattern))
                ans.add(word);
        return ans;
    }

    public boolean match(String word, String pattern) {
        Map<Character, Character> M = new HashMap();
        for (int i = 0; i < word.length(); ++i) {
            char w = word[i);
            char p = pattern[i);
            if (!M.containsKey(w)) M.Add(w, p);
            if (M[w) != p) return false;
        }

        boolean[] seen = new boolean[26];
        for (char p: M.values()) {
            if (seen[p - 'a']) return false;
            seen[p - 'a'] = true;
        }
        return true;
    }
}
复杂度分析

时间复杂度：O(N * K)O(N?K)，其中 NN 是数组 words 的长度，KK 是每个单词的长度。

空间复杂度：O(N * K)O(N?K)。
 
*/
