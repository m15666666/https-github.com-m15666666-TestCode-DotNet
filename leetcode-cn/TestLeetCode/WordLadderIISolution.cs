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

单词接龙 II
力扣官方题解
发布于 2020-06-06
18.4k
方法一：广度优先搜索
思路

本题要求的是最短转换序列，看到最短首先想到的就是 BFS。想到 BFS 自然而然的就能想到图，但是本题并没有直截了当的给出图的模型，因此我们需要把它抽象成图的模型。

我们可以把每个单词都抽象为一个点，如果两个单词可以只改变一个字母进行转换，那么说明他们之间有一条双向边。因此我们只需要把满足转换条件的点相连，就形成了一张图。根据示例 1 中的输入，我们可以建出下图：

fig1

基于该图，我们以 hit 为图的起点， 以cog 为终点进行广度优先搜索（BFS），寻找 hit 到 cog 的最短路径。下图即为答案中的一条路径。

fig2

最大的难点解决了，我们再考虑其他要求。本题要求输出所有的最短路径。那么我们在到达某个点的时候需要把它前面经过的点一起记录下来放到一起，当到达终点的时候一起输出到结果中。

算法

基于上面的思路我们考虑如何编程实现。

方便起见，我们先给每一个单词标号，即给每个单词分配一个 id。创建一个由单词 word到 id 对应的映射 wordId，并将 beginWord 与 wordList 中所有的单词都加入这个映射中。之后我们检查 endWord 是否在该映射内，若不存在，则输入无解。我们可以使用哈希表实现上面的映射关系。

同理我们可以创建一个由对应 id 到 word 的映射 idWord，方便最后输出结果。由于 id 实际上是整数且连续，所以这个映射用数组实现即可。

接下来我们将 idWord 中的单词两两匹配，检查它们是否可以通过改变一个字母进行互相转换。如果可以，则在这两个点之间建一条双向边。

为了保留相同长度的多条路径，我们采用 cost 数组，其中 cost[i] 表示 beginWord 对应的点到第 i 个点的代价（即转换次数）。初始情况下其所有元素初始化为无穷大。

接下来将起点加入队列开始广度优先搜索，队列的每一个节点中保存从起点开始的所有路径。

对于每次取出的节点 now，每个节点都是一个数组，数组中的最后一个元素为当前路径的最后节点 last :

若该节点为终点，则将其路径转换为对应的单词存入答案;
若该节点不为终点，则遍历和它连通的节点（假设为 to ）中满足 cost[to] >= cost[now] + 1cost[to]>=cost[now]+1 的加入队列，并更新 cost[to] = cost[now] + 1cost[to]=cost[now]+1。如果 cost[to] < cost[now] + 1cost[to]<cost[now]+1，说明这个节点已经被访问过，不需要再考虑。
代码


class Solution {
    private static final int INF = 1 << 20;
    private Map<String, Integer> wordId; // 单词到id的映射
    private ArrayList<String> idWord; // id到单词的映射
    private ArrayList<Integer>[] edges; // 图的边

    public Solution() {
        wordId = new HashMap<>();
        idWord = new ArrayList<>();
    }

    public List<List<String>> findLadders(String beginWord, String endWord, List<String> wordList) {
        int id = 0;
        // 将wordList所有单词加入wordId中 相同的只保留一个 // 并为每一个单词分配一个id
        for (String word : wordList) {
            if (!wordId.containsKey(word)) { 
                wordId.put(word, id++);
                idWord.add(word);
            }
        }
        // 若endWord不在wordList中 则无解
        if (!wordId.containsKey(endWord)) {
            return new ArrayList<>();
        }
        // 把beginWord也加入wordId中
        if (!wordId.containsKey(beginWord)) {
            wordId.put(beginWord, id++);
            idWord.add(beginWord);
        }

        // 初始化存边用的数组
        edges = new ArrayList[idWord.size()];
        for (int i = 0; i < idWord.size(); i++) {
            edges[i] = new ArrayList<>();
        }
        // 添加边
        for (int i = 0; i < idWord.size(); i++) {
            for (int j = i + 1; j < idWord.size(); j++) {
                // 若两者可以通过转换得到 则在它们间建一条无向边
                if (transformCheck(idWord.get(i), idWord.get(j))) {
                    edges[i].add(j);
                    edges[j].add(i);
                }
            }
        }

        int dest = wordId.get(endWord); // 目的ID
        List<List<String>> res = new ArrayList<>(); // 存答案
        int[] cost = new int[id]; // 到每个点的代价
        for (int i = 0; i < id; i++) {
            cost[i] = INF; // 每个点的代价初始化为无穷大
        }

        // 将起点加入队列 并将其cost设为0
        Queue<ArrayList<Integer>> q = new LinkedList<>();
        ArrayList<Integer> tmpBegin = new ArrayList<>();
        tmpBegin.add(wordId.get(beginWord));
        q.add(tmpBegin);
        cost[wordId.get(beginWord)] = 0;

        // 开始广度优先搜索
        while (!q.isEmpty()) {
            ArrayList<Integer> now = q.poll();
            int last = now.get(now.size() - 1); // 最近访问的点
            if (last == dest) { // 若该点为终点则将其存入答案res中
                ArrayList<String> tmp = new ArrayList<>();
                for (int index : now) {
                    tmp.add(idWord.get(index)); // 转换为对应的word
                }
                res.add(tmp);
            } else { // 该点不为终点 继续搜索
                for (int i = 0; i < edges[last].size(); i++) {
                    int to = edges[last].get(i);
                    // 此处<=目的在于把代价相同的不同路径全部保留下来
                    if (cost[last] + 1 <= cost[to]) {
                        cost[to] = cost[last] + 1;
                        // 把to加入路径中
                        ArrayList<Integer> tmp = new ArrayList<>(now); tmp.add(to);
                        q.add(tmp); // 把这个路径加入队列
                    }
                }
            }
        }
        return res;
    }

    // 两个字符串是否可以通过改变一个字母后相等
    boolean transformCheck(String str1, String str2) {
        int differences = 0;
        for (int i = 0; i < str1.length() && differences < 2; i++) {
            if (str1.charAt(i) != str2.charAt(i)) {
                ++differences;
            }
        }
        return differences == 1;
    } 
}
复杂度分析

时间复杂度：O(N^2*C)O(N 
2
 ?C)。其中 NN 为 wordList 的长度，CC 为列表中单词的长度。构建映射关系的时间复杂度为 O(N)O(N)。建图首先两层遍历 wordList，复杂度为 O(N^2)O(N 
2
 )，里面比较两个单词是否可以转换的时间复杂度为 O(C)O(C)，总的时间复杂度为 O(N^2*C)O(N 
2
 ?C)。广度优先搜索的时间复杂度最坏情况下是 O(N^2)O(N 
2
 )，因此总时间复杂度为 O(N^2*C)O(N 
2
 ?C)。

空间复杂度：O(N^2)O(N 
2
 )。其中 NN 为 wordList 的大小。数组和哈希表的复杂度都为 O(N)O(N)，图 edges 的复杂度最坏为 O(N^2)O(N 
2
 )。广度优先搜索队列的复杂度最坏情况下，即每两个路径都有很多重叠的节点时，也是 O(N^2)O(N 
2
 )，因此总的空间复杂度为 O(N^2)O(N 
2
 )。

拓展

由于本题起点和终点固定，所以可以从起点和终点同时开始进行双向广度优先搜索，可以进一步降低时间复杂度。

下一篇：详细通俗的思路分析，多解法

public class Solution {
    public IList<IList<string>> FindLadders(string beginWord, string endWord, IList<string> wordList) {
            var result = new List<IList<string>>();
            if (!wordList.Contains(endWord))
            {
                return result;
            }

            var words = new HashSet<string>(wordList);
            var mapTree = new Dictionary<string, List<string>>();
            var begin = new HashSet<string> {beginWord};
            var end = new HashSet<string> {endWord};
            if (BuildTree(words, begin, end, mapTree, true))
            {
                ConnectTree(beginWord, endWord, new List<string>());
            }

            return result;


            bool BuildTree(HashSet<string> words, HashSet<string> begin, HashSet<string> end,
                Dictionary<string, List<string>> mapTree, bool isFront)
            {
                if (begin.Count == 0)
                {
                    return false;
                }

                if (begin.Count > end.Count)
                {
                    return BuildTree(words, end, begin, mapTree, !isFront);
                }

                words.ExceptWith(begin);
                bool isMeet = false;
                HashSet<string> nextLevel = new HashSet<string>();
                foreach (var word in begin)
                {
                    char[] chars = word.ToCharArray();

                    for (int i = 0; i < chars.Length; i++)
                    {
                        char temp = chars[i];
                        for (char j = 'a'; j <= 'z'; j++)
                        {
                            chars[i] = j;
                            string newWord = new string(chars);
                            if (words.Contains(newWord))
                            {
                                nextLevel.Add(newWord);
                                string key = isFront ? word : newWord;
                                string nextWord = isFront ? newWord : word;
                                if (end.Contains(newWord))
                                {
                                    isMeet = true;
                                }

                                if (!mapTree.ContainsKey(key))
                                {
                                    mapTree.Add(key, new List<string>());
                                }

                                mapTree[key].Add(nextWord);
                            }
                        }

                        chars[i] = temp;
                    }
                }

                if (isMeet)
                {
                    return true;
                }

                return BuildTree(words, nextLevel, end, mapTree, isFront);
            }

            void ConnectTree(string originWord, string targetWord, List<string> currentList)
            {
                List<string> newList = new List<string>(currentList) {originWord};
                if (originWord == targetWord)
                {
                    result.Add(newList);
                    return;
                }

                if (mapTree.ContainsKey(originWord))
                {
                    foreach (var word in mapTree[originWord])
                    {
                        ConnectTree(word, targetWord, newList);
                    }
                }
            }
    }
}

public class Solution {
    public IList<IList<string>> FindLadders(string beginWord, string endWord, IList<string> wordList) {
        // 先将 wordList 放到哈希表里，便于判断某个单词是否在 wordList 里
        IList<IList<string>> res = new List<IList<string>>();
        HashSet<string> wordSet = new HashSet<string>(wordList);
        if (wordSet.Count == 0 || !wordSet.Contains(endWord)) {
            return res;
        }
        // 第 1 步：使用双向广度优先遍历得到后继结点列表 successors
        // key：字符串，value：广度优先遍历过程中 key 的后继结点列表
        Dictionary<string, HashSet<string>> successors = new Dictionary<string, HashSet<string>>();
        bool found = bidirectionalBfs(beginWord, endWord, wordSet, successors);
        if (!found) {
            return res;
        }
        // 第 2 步：基于后继结点列表 successors ，使用回溯算法得到所有最短路径列表
        LinkedList<string> path = new LinkedList<string>();
        path.AddLast(beginWord);
        dfs(beginWord, endWord, successors, path, res);
        return res;
    }

    private bool bidirectionalBfs(string beginWord,
                                     string endWord,
                                     HashSet<string> wordSet,
                                     Dictionary<string, HashSet<string>> successors) {
        // 记录访问过的单词
        HashSet<string> visited = new HashSet<string>();
        visited.Add(beginWord);
        visited.Add(endWord);

        HashSet<string> beginVisited = new HashSet<string>();
        beginVisited.Add(beginWord);
        HashSet<string> endVisited = new HashSet<string>();
        endVisited.Add(endWord);

        int wordLen = beginWord.Length;
        bool forward = true;
        bool found = false;
        // 在保证了 beginVisited 总是较小（可以等于）大小的集合前提下，&& !endVisited.isEmpty() 可以省略
        while (beginVisited.Count != 0 && endVisited.Count != 0) {
            // 一直保证 beginVisited 是相对较小的集合，方便后续编码
            if (beginVisited.Count > endVisited.Count) {
                HashSet<string> temp = beginVisited;
                beginVisited = endVisited;
                endVisited = temp;

                // 只要交换，就更改方向，以便维护 successors 的定义
                forward = !forward;
            }
            HashSet<string> nextLevelVisited = new HashSet<string>();
            // 默认 beginVisited 是小集合，因此从 beginVisited 出发
            foreach (string currentWord in beginVisited) {
                char[] charArray = currentWord.ToCharArray();
                for (int i = 0; i < wordLen; i++) {
                    char originChar = charArray[i];
                    for (char j = 'a'; j <= 'z'; j++) {
                        if (charArray[i] == j) {
                            continue;
                        }
                        charArray[i] = j;
                        string nextWord = new string(charArray);
                        if (wordSet.Contains(nextWord)) {
                            if (endVisited.Contains(nextWord)) {
                                found = true;
                                // 在另一侧找到单词以后，还需把这一层关系添加到「后继结点列表」
                                addToSuccessors(successors, forward, currentWord, nextWord);
                            }

                            if (!visited.Contains(nextWord)) {
                                nextLevelVisited.Add(nextWord);
                                addToSuccessors(successors, forward, currentWord, nextWord);
                            }
                        }
                    }
                    charArray[i] = originChar;
                }
            }
            beginVisited = nextLevelVisited;
            visited.UnionWith(nextLevelVisited);
            if (found) {
                break;
            }
        }
        return found;
    }

    private void dfs(string beginWord,
                     string endWord,
                     Dictionary<string, HashSet<string>> successors,
                     LinkedList<string> path,
                     IList<IList<string>> res) {

        if (beginWord.Equals(endWord)) {
            res.Add(new List<string>(path));
            return;
        }

        if (!successors.ContainsKey(beginWord)) {
            return;
        }

        HashSet<string> successorWords = successors[beginWord];
        foreach (string successor in successorWords) {
            path.AddLast(successor);
            dfs(successor, endWord, successors, path, res);
            path.RemoveLast();
        }
    }

    private void addToSuccessors(Dictionary<string, HashSet<string>> successors, bool forward,
                                 string currentWord, string nextWord) {
        if (!forward) {
            string temp = currentWord;
            currentWord = nextWord;
            nextWord = temp;
        }

        if(!successors.ContainsKey(currentWord)){
            successors.Add(currentWord, new HashSet<string>());
        }
        successors[currentWord].Add(nextWord);
    }

}

public class Solution {
    public IList<IList<string>> FindLadders(string beginWord, string endWord, IList<string> wordList)
    {
        List<IList<string>> ans = new List<IList<string>>();
        Bfs(ans, beginWord, endWord, wordList);
        return ans;
    }

    private void Bfs(List<IList<string>> ans, string beginWord, string endWord, IList<string> wordList)
    {
        if (!wordList.Remove(endWord))
        {
            return;
        }
        List<string> beginWordList = new List<string>(wordList);
        List<string> endWordList = new List<string>(wordList);
        Queue<List<string>> beginQueue = new Queue<List<string>>();
        Queue<List<string>> endQueue = new Queue<List<string>>();
        beginQueue.Enqueue(new List<string> { beginWord });
        endQueue.Enqueue(new List<string> { endWord });
        Dictionary<string, List<List<string>>> beginMap = new Dictionary<string, List<List<string>>> { { beginWord, new List<List<string>> { beginQueue.Peek() } } };
        Dictionary<string, List<List<string>>> endMap = new Dictionary<string, List<List<string>>> { { beginWord, new List<List<string>> { beginQueue.Peek() } } };

        while (beginQueue.Count > 0 && endQueue.Count > 0)
        {
            if (beginQueue.Count < endQueue.Count)//top bfs
            {
                var c = beginQueue.Count;
                bool found = false;
                beginMap.Clear();
                List<int> removeIdx = new List<int>();
                for (int i = 0; i < c; i++)
                {
                    var p = beginQueue.Dequeue();
                    var curWord = p.Last();
                    for (int j = 0; j < beginWordList.Count; j++)
                    {
                        if (IsNeighbor(curWord, beginWordList[j]))
                        {
                            if (endMap.TryGetValue(beginWordList[j], out var p2))
                            {
                                found = true;
                                foreach (var tempP2 in p2)
                                {
                                    var onePath = new List<string>(p);
                                    for (int k = tempP2.Count - 1; k >= 0; k--)
                                    {
                                        onePath.Add(tempP2[k]);
                                    }
                                    ans.Add(onePath);
                                }
                            }
                            if (!found)
                            {
                                var tempPath = new List<string>(p);
                                tempPath.Add(beginWordList[j]);
                                beginQueue.Enqueue(tempPath);
                                if (beginMap.TryGetValue(beginWordList[j], out var map))
                                {
                                    map.Add(tempPath);
                                }
                                else
                                {
                                    beginMap[beginWordList[j]] = new List<List<string>> { tempPath };
                                }
                            }
                            removeIdx.Add(j);
                        }
                    }
                }
                if (found)
                {
                    break;
                }
                removeIdx.Sort();
                int lastRemoveIdx = -1;
                for (int j = removeIdx.Count - 1; j >= 0; j--)
                {
                    if (lastRemoveIdx != removeIdx[j])
                    {
                        lastRemoveIdx = removeIdx[j];
                        beginWordList.RemoveAt(lastRemoveIdx);
                    }
                }
            }
            else//bottom bfs
            {
                var c = endQueue.Count;
                bool found = false;
                endMap.Clear();
                List<int> removeIdx = new List<int>();
                for (int i = 0; i < c; i++)
                {
                    var p = endQueue.Dequeue();
                    var curWord = p.Last();
                    for (int j = 0; j < endWordList.Count; j++)
                    {
                        if (IsNeighbor(curWord, endWordList[j]))
                        {
                            if (beginMap.TryGetValue(endWordList[j], out var p2))
                            {
                                found = true;
                                foreach (var tempP2 in p2)
                                {
                                    var onePath = new List<string>(tempP2);
                                    for (int k = p.Count - 1; k >= 0; k--)
                                    {
                                        onePath.Add(p[k]);
                                    }
                                    ans.Add(onePath);
                                }
                            }
                            if (!found)
                            {
                                var tempPath = new List<string>(p);
                                tempPath.Add(endWordList[j]);
                                endQueue.Enqueue(tempPath);
                                if (endMap.TryGetValue(endWordList[j], out var map))
                                {
                                    map.Add(tempPath);
                                }
                                else
                                {
                                    endMap[endWordList[j]] = new List<List<string>> { tempPath };
                                }
                            }
                            removeIdx.Add(j);
                        }
                    }
                }
                if (found)
                {
                    break;
                }
                removeIdx.Sort();
                int lastRemoveIdx = -1;
                for (int j = removeIdx.Count - 1; j >= 0; j--)
                {
                    if (lastRemoveIdx != removeIdx[j])
                    {
                        lastRemoveIdx = removeIdx[j];
                        endWordList.RemoveAt(lastRemoveIdx);
                    }
                }
            }
        }
    }

    private bool IsNeighbor(string str1, string str2)
    {
        int distance = 0;
        for (int i = 0; i < str1.Length; i++)
        {
            if (str1[i] != str2[i])
            {
                if (distance == 1)
                {
                    return false;
                }
                distance++;
            }
        }
        return distance == 1;
    }
}

public class Solution {
    public IList<IList<string>> FindLadders(string beginWord, string endWord, IList<string> wordList) {
            IList<IList<string>> ans = new List<IList<string>>();
            if (!wordList.Contains(endWord))
            {
                return ans;
            }
            // 利用 BFS 得到所有的邻居节点
            Dictionary<string, List<string>> map = new Dictionary<string, List<string>>();
            bfs(beginWord, endWord, wordList, map);
            List<string> temp = new List<string>();
            // temp 用来保存当前的路径
            temp.Add(beginWord);
            findLaddersHelper(beginWord, endWord, map, temp, ans);
            return ans;
        }
        private void findLaddersHelper(string beginWord, string endWord,
            Dictionary<string, List<string>> map, List<string> temp, IList<IList<string>> ans)
        {
            if (beginWord.Equals(endWord))
            {
                ans.Add(new List<string>(temp));
                return;
            }
            // 得到所有的下一个的节点
            map.TryGetValue(beginWord,out var neighbors);
            neighbors = neighbors ?? new List<string>(); 
            foreach (string neighbor in neighbors)
            {
                temp.Add(neighbor);
                findLaddersHelper(neighbor, endWord, map, temp, ans);
                temp.RemoveAt(temp.Count - 1);
            }
        }
        //利用递归实现了双向搜索
        private void bfs(string beginWord, string endWord, IList<string> wordList, Dictionary<string, List<string>> mapDict)
        {
            HashSet<string> set1 = new HashSet<string> {beginWord};
            HashSet<string> set2 = new HashSet<string> {endWord};
            HashSet<string> wordSet = new HashSet<string>(wordList);
            bfsHelper(set1, set2, wordSet, true, mapDict);
        }
        // direction 为 true 代表向下扩展，false 代表向上扩展
        private bool bfsHelper(HashSet<string> set1, HashSet<string> set2, HashSet<string> wordSet, bool direction,
                                  Dictionary<string, List<string>> mapDict)
        {
            //set1 为空了，就直接结束
            //比如下边的例子就会造成 set1 为空
            //	"hot"
                "dog"
                ["hot","dog"]//
            if (set1.Count == 0)
            {
                return false;
            }
            // set1 的数量多，就反向扩展
            if (set1.Count > set2.Count)
            {
                return bfsHelper(set2, set1, wordSet, !direction, mapDict);
            }
            // 将已经访问过单词删除
            wordSet.RemoveWhere(set1.Contains);
            wordSet.RemoveWhere(set2.Contains);

            bool done = false;

            // 保存新扩展得到的节点
            HashSet<string> set = new HashSet<string>();

            foreach (string str in set1)
            {
                //遍历每一位
                for (int i = 0; i < str.Length; i++)
                {
                    char[] chars = str.ToCharArray();

                    // 尝试所有字母
                    for (char ch = 'a'; ch <= 'z'; ch++)
                    {
                        if (chars[i] == ch)
                        {
                            continue;
                        }
                        chars[i] = ch;

                        string word = new string(chars);

                        // 根据方向得到 map 的 key 和 val
                        string key = direction ? str : word;
                        string val = direction ? word : str;

                        List<string> list = mapDict.ContainsKey(key) ? mapDict[key] : new List<string>();

                        //如果相遇了就保存结果
                        if (set2.Contains(word))
                        {
                            done = true;
                            list.Add(val);
                            if(!mapDict.ContainsKey(key))
                                mapDict.Add(key, list);
                        }

                        //如果还没有相遇，并且新的单词在 word 中，那么就加到 set 中
                        if (!done && wordSet.Contains(word))
                        {
                            set.Add(word);
                            list.Add(val);
                            if(!mapDict.ContainsKey(key))
                                mapDict.Add(key, list);
                        }
                    }
                }
            }

            //一般情况下新扩展的元素会多一些，所以我们下次反方向扩展  set2
            return done || bfsHelper(set2, set, wordSet, !direction, mapDict);

        }
}

public class Solution {
    public IList<IList<string>> FindLadders(string beginWord, string endWord, IList<string> wordList)
    {
        List<IList<string>> ans = new List<IList<string>>();
        Bfs(ans, beginWord, endWord, wordList);
        return ans;
    }

    private void Bfs(List<IList<string>> ans, string beginWord, string endWord, IList<string> wordList)
    {
        if (!wordList.Remove(endWord))
        {
            return;
        }
        List<string> beginWordList = new List<string>(wordList);
        List<string> endWordList = new List<string>(wordList);
        Queue<List<string>> beginQueue = new Queue<List<string>>();
        Queue<List<string>> endQueue = new Queue<List<string>>();
        beginQueue.Enqueue(new List<string> { beginWord });
        endQueue.Enqueue(new List<string> { endWord });
        Dictionary<string, List<List<string>>> beginMap = new Dictionary<string, List<List<string>>> { { beginWord, new List<List<string>> { beginQueue.Peek() } } };
        Dictionary<string, List<List<string>>> endMap = new Dictionary<string, List<List<string>>> { { beginWord, new List<List<string>> { beginQueue.Peek() } } };

        while (beginQueue.Count > 0 && endQueue.Count > 0)
        {
            if (beginQueue.Count < endQueue.Count)//top bfs
            {
                var c = beginQueue.Count;
                bool found = false;
                beginMap.Clear();
                List<int> removeIdx = new List<int>();
                for (int i = 0; i < c; i++)
                {
                    var p = beginQueue.Dequeue();
                    var curWord = p.Last();
                    for (int j = 0; j < beginWordList.Count; j++)
                    {
                        if (IsNeighbor(curWord, beginWordList[j]))
                        {
                            if (endMap.TryGetValue(beginWordList[j], out var p2))
                            {
                                found = true;
                                foreach (var tempP2 in p2)
                                {
                                    var onePath = new List<string>(p);
                                    for (int k = tempP2.Count - 1; k >= 0; k--)
                                    {
                                        onePath.Add(tempP2[k]);
                                    }
                                    ans.Add(onePath);
                                }
                            }
                            if (!found)
                            {
                                var tempPath = new List<string>(p);
                                tempPath.Add(beginWordList[j]);
                                beginQueue.Enqueue(tempPath);
                                if (beginMap.TryGetValue(beginWordList[j], out var map))
                                {
                                    map.Add(tempPath);
                                }
                                else
                                {
                                    beginMap[beginWordList[j]] = new List<List<string>> { tempPath };
                                }
                            }
                            removeIdx.Add(j);
                        }
                    }
                }
                if (found)
                {
                    break;
                }
                removeIdx.Sort();
                int lastRemoveIdx = -1;
                for (int j = removeIdx.Count - 1; j >= 0; j--)
                {
                    if (lastRemoveIdx != removeIdx[j])
                    {
                        lastRemoveIdx = removeIdx[j];
                        beginWordList.RemoveAt(lastRemoveIdx);
                    }
                }
            }
            else//bottom bfs
            {
                var c = endQueue.Count;
                bool found = false;
                endMap.Clear();
                List<int> removeIdx = new List<int>();
                for (int i = 0; i < c; i++)
                {
                    var p = endQueue.Dequeue();
                    var curWord = p.Last();
                    for (int j = 0; j < endWordList.Count; j++)
                    {
                        if (IsNeighbor(curWord, endWordList[j]))
                        {
                            if (beginMap.TryGetValue(endWordList[j], out var p2))
                            {
                                found = true;
                                foreach (var tempP2 in p2)
                                {
                                    var onePath = new List<string>(tempP2);
                                    for (int k = p.Count - 1; k >= 0; k--)
                                    {
                                        onePath.Add(p[k]);
                                    }
                                    ans.Add(onePath);
                                }
                            }
                            if (!found)
                            {
                                var tempPath = new List<string>(p);
                                tempPath.Add(endWordList[j]);
                                endQueue.Enqueue(tempPath);
                                if (endMap.TryGetValue(endWordList[j], out var map))
                                {
                                    map.Add(tempPath);
                                }
                                else
                                {
                                    endMap[endWordList[j]] = new List<List<string>> { tempPath };
                                }
                            }
                            removeIdx.Add(j);
                        }
                    }
                }
                if (found)
                {
                    break;
                }
                removeIdx.Sort();
                int lastRemoveIdx = -1;
                for (int j = removeIdx.Count - 1; j >= 0; j--)
                {
                    if (lastRemoveIdx != removeIdx[j])
                    {
                        lastRemoveIdx = removeIdx[j];
                        endWordList.RemoveAt(lastRemoveIdx);
                    }
                }
            }
        }
    }

    private bool IsNeighbor(string str1, string str2)
    {
        int distance = 0;
        for (int i = 0; i < str1.Length; i++)
        {
            if (str1[i] != str2[i])
            {
                if (distance == 1)
                {
                    return false;
                }
                distance++;
            }
        }
        return distance == 1;
    }
}
 
*/
