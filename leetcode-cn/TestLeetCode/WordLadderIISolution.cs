using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定两个单词（beginWord 和 endWord）和一个字典 wordList，找出所有从 beginWord 到 endWord 的最短转换序列。转换需遵循如下规则：

每次转换只能改变一个字母。
转换后得到的单词必须是字典中的单词。
说明:

如果不存在这样的转换序列，返回一个空列表。
所有单词具有相同的长度。
所有单词只由小写字母组成。
字典中不存在重复的单词。
你可以假设 beginWord 和 endWord 是非空的，且二者不相同。
示例 1:

输入:
beginWord = "hit",
endWord = "cog",
wordList = ["hot","dot","dog","lot","log","cog"]

输出:
[
  ["hit","hot","dot","dog","cog"],
  ["hit","hot","lot","log","cog"]
]
示例 2:

输入:
beginWord = "hit"
endWord = "cog"
wordList = ["hot","dot","dog","lot","log"]

输出: []

解释: endWord "cog" 不在字典中，所以不存在符合要求的转换序列。
 
 
*/
/// <summary>
/// https://leetcode-cn.com/problems/word-ladder-ii/
/// 126. 单词接龙 II
/// 
/// 
/// 
/// </summary>
class WordLadderIISolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public IList<IList<string>> FindLadders(string beginWord, string endWord, IList<string> wordList) {

        Dictionary<string, int> wordId = new Dictionary<string, int>(wordList.Count + 2);
        List<string> idWord = new List<string>(wordList.Count + 2);
        {
            int id = 0;
            if (!wordId.ContainsKey(beginWord))
            {
                wordId.Add(beginWord, id++);
                idWord.Add(beginWord);
            }

            foreach (var word in wordList)
            {
                if (!wordId.ContainsKey(word))
                {
                    wordId.Add(word, id++);
                    idWord.Add(word);
                }
            }
        }
        if (!wordId.ContainsKey(endWord)) return new  List<IList<string>>();

        var id2Word = idWord.ToArray();
        int wordCount = idWord.Count;
        List<int>[] edges = new List<int>[wordCount];
        for (int i = 0; i < wordCount; i++) {
            edges[i] = new List<int>();
        }

        for (int i = 0; i < wordCount; i++) 
            for (int j = i + 1; j < wordCount; j++) 
                if (RecordEdges(idWord[i], idWord[j])) {
                    edges[i].Add(j);
                    edges[j].Add(i);
                }

        Queue<List<int>> cache = new Queue<List<int>>();
        Func<List<int>> getFromCache = () => {
            if (0 < cache.Count) return cache.Dequeue();
            return new List<int>();
        };
        Action<List<int>> putToCache = list => {
            list.Clear();
            cache.Enqueue(list);
        };

        int dest = wordId[endWord];
        List<IList<string>> ret = new List<IList<string>>();
        int[] cost = new int[wordCount];
        Array.Fill(cost, int.MaxValue);

        Queue<List<int>> queue = new Queue<List<int>>();
        var begin = getFromCache();
        begin.Add(0/*wordId[beginWord]*/);
        queue.Enqueue(begin);
        cost[0/*wordId[beginWord]*/] = 0;

        while (0 < queue.Count) {
            var now = queue.Dequeue();
            int last = now[now.Count - 1];
            if (last == dest) {
                string[] ladder = new string[now.Count];
                int index = 0;
                foreach (int id in now) ladder[index++] = id2Word[id]; 
                ret.Add(ladder);
                putToCache(now);
                continue;
            }

            int nextLen = cost[last] + 1;
            foreach (int to in edges[last]) 
            {
                if (nextLen <= cost[to]) 
                {
                    cost[to] = nextLen;
                    var nextSeries = getFromCache();
                    nextSeries.AddRange(now);
                    nextSeries.Add(to);
                    queue.Enqueue(nextSeries);
                }
            }
            putToCache(now);
        }
        return ret;

        bool RecordEdges(string str1, string str2) {
            int differences = 0;
            int len = str1.Length;
            for (int i = 0; i < len && differences < 2; i++) 
                if (str1[i] != str2[i]) ++differences; 
            
            return differences == 1;
        } 
    }

}
/*
 
 
 
 
 
*/
