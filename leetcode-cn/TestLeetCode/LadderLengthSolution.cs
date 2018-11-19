using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// https://leetcode-cn.com/problems/word-ladder/
/// 127.单词接龙
/// 给定两个单词（beginWord 和 endWord）和一个字典，找到从 beginWord 到 endWord 的最短转换序列的长度。转换需遵循如下规则：
/// 每次转换只能改变一个字母。
/// 转换过程中的中间单词必须是字典中的单词。
/// 说明:
/// 如果不存在这样的转换序列，返回 0。
/// 所有单词具有相同的长度。
/// 所有单词只由小写字母组成。
/// 字典中不存在重复的单词。
/// 你可以假设 beginWord 和 endWord 是非空的，且二者不相同。
/// https://blog.csdn.net/qq_41410799/article/details/82383616
/// </summary>
class LadderLengthSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    public int LadderLength(string beginWord, string endWord, IList<string> wordList)
    {
        if (string.IsNullOrWhiteSpace(beginWord) || string.IsNullOrWhiteSpace(endWord) || wordList == null || wordList.Count == 0) return 0;
        if (beginWord == endWord) return 1;

        HashSet<string> words = new HashSet<string>(wordList);
        if (!words.Contains(endWord)) return 0;
        if (words.Contains(beginWord)) words.Remove(beginWord);

        Queue<string> queue = new Queue<string>();
        Dictionary<string, int> text2PathCount = new Dictionary<string, int>();
        queue.Enqueue(beginWord);
        text2PathCount[beginWord] = 1;

        while( 0 < queue.Count && 0 < words.Count)
        {
            var word = queue.Dequeue();
            var pathCount = text2PathCount[word];

            var length = word.Length;
            StringBuilder sb = new StringBuilder( word, length );
            for (var index = 0; index < length; index++)
            {
                var old = sb[index];
                for (var c = 'a'; c <= 'z'; c++)
                {
                    if (c == old) continue;
                    sb.Replace(sb[index], c, index, 1);
                    var newWord = sb.ToString();

                    if (!words.Contains(newWord)) continue;

                    if (newWord == endWord) return pathCount + 1;

                    queue.Enqueue(newWord);
                    text2PathCount[newWord] = pathCount + 1;
                    words.Remove(newWord);
                }

                sb.Replace(sb[index], old, index, 1);
            }
        }

        return 0;
    }
}