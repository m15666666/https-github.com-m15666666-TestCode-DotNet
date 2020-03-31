using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个字符串 s 和一些长度相同的单词 words。找出 s 中恰好可以由 words 中所有单词串联形成的子串的起始位置。

注意子串要与 words 中的单词完全匹配，中间不能有其他字符，但不需要考虑 words 中单词串联的顺序。

 

示例 1：

输入：
  s = "barfoothefoobarman",
  words = ["foo","bar"]
输出：[0,9]
解释：
从索引 0 和 9 开始的子串分别是 "barfoo" 和 "foobar" 。
输出的顺序不重要, [9,0] 也是有效答案。
示例 2：

输入：
  s = "wordgoodgoodgoodbestword",
  words = ["word","good","best","word"]
输出：[]
*/
/// <summary>
/// https://leetcode-cn.com/problems/substring-with-concatenation-of-all-words/
/// 30. 串联所有单词的子串
/// 
/// </summary>
class SubstringWithConcatenationOfAllWordsSolution
{
    public void Test()
    {
        //var ret = FindSubstring("wordgoodgoodgoodbestword", new string[]{"word", "good", "best", "word"});
        var ret = FindSubstring("aaaaaaaa", new string[] { "aa", "aa", "aa" });
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public IList<int> FindSubstring(string s, string[] words)
    {
        if (string.IsNullOrEmpty(s) || words == null || words.Length == 0) return new int[0];

        Dictionary<int,int> hash2Count = new Dictionary<int, int>();
        int hash;
        foreach (var word in words)
        {
            //hash = word.GetHashCode();
            hash = GetHashCode(word.AsSpan());
            if (!hash2Count.ContainsKey(hash)) hash2Count.Add(hash, 1);
            else hash2Count[hash]++;
        }

        int wordLen = words[0].Length;
        int wordCount = words.Length;
        int upper = s.Length - wordLen;
        var hashcodeCache = new int[upper + 1]; // 缓存right下标计算的hashcode结果
        
        Dictionary<int, int> hash2CountOfWindow = new Dictionary<int, int>(hash2Count.Count);

        List<int> ret = new List<int>();
        for (int i = 0; i < wordLen; i++)
        {
            int left = i, right = i, count = 0;
            hash2CountOfWindow.Clear();
            while (right <= upper)
            {
                //hash = string.GetHashCode(s.AsSpan(right, wordLen));
                hash = GetHashCode(s.AsSpan(right, wordLen));
                hashcodeCache[right] = hash;
                right += wordLen;

                if (!hash2Count.ContainsKey(hash))
                {
                    count = 0;
                    left = right;
                    hash2CountOfWindow.Clear();
                    continue;
                }

                if (!hash2CountOfWindow.ContainsKey(hash)) hash2CountOfWindow.Add(hash, 1);
                else hash2CountOfWindow[hash]++;
                count++;
                int hashCount = hash2Count[hash];
                while (hashCount < hash2CountOfWindow[hash])
                {
                    hash2CountOfWindow[hashcodeCache[left]]--; // 只能从窗口前端去除块
                    count--;
                    
                    left += wordLen;
                }
                if (count == wordCount) ret.Add(left);
            }
        }
        return ret;

        // 由于hashcode不支持string.GetHashCode,public static int GetHashCode(ReadOnlySpan<char> value);
        // 所以用ILSpan复制的.net 代码，参考：https://stackoverrun.com/cn/q/4114625
        int GetHashCode(ReadOnlySpan<char> value)
        {
            var src = value;
            int hash1 = 5381;
            int hash2 = hash1;

            int c;
            int index = 0;
            int len = src.Length;
            while (index < len && (c = src[index]) != 0)
            {
                hash1 = ((hash1 << 5) + hash1) ^ c;
                if (index + 1 < len)
                {
                    c = src[index + 1];
                    if (c == 0) break;
                }
                else break;

                hash2 = ((hash2 << 5) + hash2) ^ c;
                index += 2;
            }
            return hash1 + (hash2 * 1566083941);
        }
    }
}
/*

串联所有单词的子串
powcai
发布于 1 年前
24.0k
思路：
一开始，我的想法是，每次从 s 截取一定长度（固定）的字符串，看这段字符串出现单词个数是否和要匹配的单词个数相等!如下代码：

class Solution:
    def findSubstring(self, s: str, words: List[str]) -> List[int]:
        from collections import Counter
        if not s or not words:return []
        all_len = sum(map(len, words))
        n = len(s)
        words = Counter(words)
        res = []
        for i in range(0, n - all_len + 1):
            tmp = s[i:i+all_len]
            flag = True
            for key in words:
                if words[key] != tmp.count(key):
                    flag = False
                    break
            if flag:res.append(i)
        return res
但是比如：s = "ababaab", words = ["ab","ba","ba"] 就会报错！

错误原因：因为计算时候我们会从字符串中间计算，也就是说会出现单词截断的问题。

所以我想另一种方法：

思路一：

因为单词长度固定的，我们可以计算出截取字符串的单词个数是否和 words 里相等，所以我们可以借用哈希表。

一个是哈希表是 words，一个哈希表是截取的字符串，比较两个哈希是否相等！

因为遍历和比较都是线性的，所以时间复杂度：O(n^2)O(n 
2
 )

上面思路每次都要反复遍历 s；下面介绍滑动窗口。

思路二：

滑动窗口！

我们一直在 s 维护着所有单词长度总和的一个长度队列！

时间复杂度：O(n)O(n)

还可以再优化，只是加一些判断，详细看代码吧！

代码：
思路一：

class Solution {
    public List<Integer> findSubstring(String s, String[] words) {
        List<Integer> res = new ArrayList<>();
        if (s == null || s.length() == 0 || words == null || words.length == 0) return res;
        HashMap<String, Integer> map = new HashMap<>();
        int one_word = words[0].length();
        int word_num = words.length;
        int all_len = one_word * word_num;
        for (String word : words) {
            map.put(word, map.getOrDefault(word, 0) + 1);
        }
        for (int i = 0; i < s.length() - all_len + 1; i++) {
            String tmp = s.substring(i, i + all_len);
            HashMap<String, Integer> tmp_map = new HashMap<>();
            for (int j = 0; j < all_len; j += one_word) {
                String w = tmp.substring(j, j + one_word);
                tmp_map.put(w, tmp_map.getOrDefault(w, 0) + 1);
            }
            if (map.equals(tmp_map)) res.add(i);
        }
        return res;
    }
}
思路二：

class Solution {
    public List<Integer> findSubstring(String s, String[] words) {
        List<Integer> res = new ArrayList<>();
        if (s == null || s.length() == 0 || words == null || words.length == 0) return res;
        HashMap<String, Integer> map = new HashMap<>();
        int one_word = words[0].length();
        int word_num = words.length;
        int all_len = one_word * word_num;
        for (String word : words) {
            map.put(word, map.getOrDefault(word, 0) + 1);
        }
        for (int i = 0; i < one_word; i++) {
            int left = i, right = i, count = 0;
            HashMap<String, Integer> tmp_map = new HashMap<>();
            while (right + one_word <= s.length()) {
                String w = s.substring(right, right + one_word);
                tmp_map.put(w, tmp_map.getOrDefault(w, 0) + 1);
                right += one_word;
                count++;
                while (tmp_map.getOrDefault(w, 0) > map.getOrDefault(w, 0)) {
                    String t_w = s.substring(left, left + one_word);
                    count--;
                    tmp_map.put(t_w, tmp_map.getOrDefault(t_w, 0) - 1);
                    left += one_word;
                }
                if (count == word_num) res.add(left);

            }
        }

        return res;
    }
}
再优化：

class Solution {
    public List<Integer> findSubstring(String s, String[] words) {
        List<Integer> res = new ArrayList<>();
        if (s == null || s.length() == 0 || words == null || words.length == 0) return res;
        HashMap<String, Integer> map = new HashMap<>();
        int one_word = words[0].length();
        int word_num = words.length;
        int all_len = one_word * word_num;
        for (String word : words) {
            map.put(word, map.getOrDefault(word, 0) + 1);
        }
        for (int i = 0; i < one_word; i++) {
            int left = i, right = i, count = 0;
            HashMap<String, Integer> tmp_map = new HashMap<>();
            while (right + one_word <= s.length()) {
                String w = s.substring(right, right + one_word);
                right += one_word;
                if (!map.containsKey(w)) {
                    count = 0;
                    left = right;
                    tmp_map.clear();
                } else {
                    tmp_map.put(w, tmp_map.getOrDefault(w, 0) + 1);
                    count++;
                    while (tmp_map.getOrDefault(w, 0) > map.getOrDefault(w, 0)) {
                        String t_w = s.substring(left, left + one_word);
                        count--;
                        tmp_map.put(t_w, tmp_map.getOrDefault(t_w, 0) - 1);
                        left += one_word;
                    }
                    if (count == word_num) res.add(left);
                }
            }
        }
        return res;
    }
}
下一篇：详细通俗的思路分析，多解法
      
*/
