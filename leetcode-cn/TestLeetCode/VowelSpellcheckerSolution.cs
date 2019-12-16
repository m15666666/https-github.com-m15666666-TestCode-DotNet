using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
在给定单词列表 wordlist 的情况下，我们希望实现一个拼写检查器，将查询单词转换为正确的单词。

对于给定的查询单词 query，拼写检查器将会处理两类拼写错误：

大小写：如果查询匹配单词列表中的某个单词（不区分大小写），则返回的正确单词与单词列表中的大小写相同。
例如：wordlist = ["yellow"], query = "YellOw": correct = "yellow"
例如：wordlist = ["Yellow"], query = "yellow": correct = "Yellow"
例如：wordlist = ["yellow"], query = "yellow": correct = "yellow"
元音错误：如果在将查询单词中的元音（‘a’、‘e’、‘i’、‘o’、‘u’）分别替换为任何元音后，能与单词列表中的单词匹配（不区分大小写），则返回的正确单词与单词列表中的匹配项大小写相同。
例如：wordlist = ["YellOw"], query = "yollow": correct = "YellOw"
例如：wordlist = ["YellOw"], query = "yeellow": correct = "" （无匹配项）
例如：wordlist = ["YellOw"], query = "yllw": correct = "" （无匹配项）
此外，拼写检查器还按照以下优先级规则操作：

当查询完全匹配单词列表中的某个单词（区分大小写）时，应返回相同的单词。
当查询匹配到大小写问题的单词时，您应该返回单词列表中的第一个这样的匹配项。
当查询匹配到元音错误的单词时，您应该返回单词列表中的第一个这样的匹配项。
如果该查询在单词列表中没有匹配项，则应返回空字符串。
给出一些查询 queries，返回一个单词列表 answer，其中 answer[i] 是由查询 query = queries[i] 得到的正确单词。

 

示例：

输入：wordlist = ["KiTe","kite","hare","Hare"], queries = ["kite","Kite","KiTe","Hare","HARE","Hear","hear","keti","keet","keto"]
输出：["kite","KiTe","KiTe","Hare","hare","","","KiTe","","KiTe"]
 

提示：

1 <= wordlist.length <= 5000
1 <= queries.length <= 5000
1 <= wordlist[i].length <= 7
1 <= queries[i].length <= 7
wordlist 和 queries 中的所有字符串仅由英文字母组成。
*/
/// <summary>
/// https://leetcode-cn.com/problems/vowel-spellchecker/
/// 966. 元音拼写检查器
/// 
/// </summary>
class VowelSpellcheckerSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public string[] Spellchecker(string[] wordlist, string[] queries)
    {
        wordsPerfect = new HashSet<string>(wordlist);
        wordsLower = new Dictionary<string, string>(queries.Length);
        wordsVowel = new Dictionary<string, string>(queries.Length);

        foreach (string word in wordlist)
        {
            var wordlow = word.ToLower();
            if(!wordsLower.ContainsKey(wordlow)) wordsLower.Add(wordlow, word);

            var wordlowDV = DeVowel(wordlow);
            if(!wordsVowel.ContainsKey(wordlowDV)) wordsVowel.Add(wordlowDV, word);
        }

        return queries.Select(item => Solve(item)).ToArray();
    }

    private string Solve(string query)
    {
        if (wordsPerfect.Contains(query))
            return query;

        var key = query.ToLower();
        if (wordsLower.ContainsKey(key)) return wordsLower[key];

        key = DeVowel(key);
        if (wordsVowel.ContainsKey(key)) return wordsVowel[key];

        return "";
    }

    private string DeVowel(string word)
    {
        _stringBuilder.Length = 0;
        foreach (char c in word )
            _stringBuilder.Append(IsVowel(c) ? '*' : c);
        return _stringBuilder.ToString();
    }

    private static bool IsVowel(char c)
    {
        return (c == 'a' || c == 'e' || c == 'i' || c == 'o' || c == 'u');
    }

    private readonly StringBuilder _stringBuilder = new StringBuilder();
    private HashSet<string> wordsPerfect;
    private Dictionary<string, string> wordsLower;
    private Dictionary<string, string> wordsVowel;
}
/*
方法：哈希映射（HashMap）
思路与算法

我们分析了算法需要考虑的 3 种情况: 当查询完全匹配时，当查询存在大小写不同的单词匹配时，当查询与出现元音错误的单词匹配时。

在所有 3 种情况下，我们都可以使用哈希表来查询答案。

对于第一种情况（完全匹配），我们使用集合存放单词以有效地测试查询单词是否在该组中。
对于第二种情况（大小写不同），我们使用一个哈希表，该哈希表将单词从其小写形式转换为原始单词（大小写正确的形式）。
对于第三种情况（元音错误），我们使用一个哈希表，将单词从其小写形式（忽略元音的情况下）转换为原始单词。
该算法仅剩的要求是认真规划和仔细阅读问题。

JavaPython
class Solution {
    Set<String> words_perfect;
    Map<String, String> words_cap;
    Map<String, String> words_vow;

    public String[] spellchecker(String[] wordlist, String[] queries) {
        words_perfect = new HashSet();
        words_cap = new HashMap();
        words_vow = new HashMap();

        for (String word: wordlist) {
            words_perfect.add(word);

            String wordlow = word.toLowerCase();
            words_cap.putIfAbsent(wordlow, word);

            String wordlowDV = devowel(wordlow);
            words_vow.putIfAbsent(wordlowDV, word);
        }

        String[] ans = new String[queries.length];
        int t = 0;
        for (String query: queries)
            ans[t++] = solve(query);
        return ans;
    }

    public String solve(String query) {
        if (words_perfect.contains(query))
            return query;

        String queryL = query.toLowerCase();
        if (words_cap.containsKey(queryL))
            return words_cap.get(queryL);

        String queryLV = devowel(queryL);
        if (words_vow.containsKey(queryLV))
            return words_vow.get(queryLV);

        return "";
    }

    public String devowel(String word) {
        StringBuilder ans = new StringBuilder();
        for (char c: word.toCharArray())
            ans.append(isVowel(c) ? '*' : c);
        return ans.toString();
    }

    public boolean isVowel(char c) {
        return (c == 'a' || c == 'e' || c == 'i' || c == 'o' || c == 'u');
    }
}
复杂度分析

时间复杂度：O(\mathcal{C})O(C)，其中 \mathcal{C}C 是 wordlist 和 queries 中内容的总数。

空间复杂度：O(\mathcal{C})O(C)。
 
*/
