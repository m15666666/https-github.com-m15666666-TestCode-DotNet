using System.Collections.Generic;
using System.Linq;

/*
给定一个二维网格 board 和一个字典中的单词列表 words，找出所有同时在二维网格和字典中出现的单词。

单词必须按照字母顺序，通过相邻的单元格内的字母构成，其中“相邻”单元格是那些水平相邻或垂直相邻的单元格。同一个单元格内的字母在一个单词中不允许被重复使用。

示例:

输入:
words = ["oath","pea","eat","rain"] and board =
[
  ['o','a','a','n'],
  ['e','t','a','e'],
  ['i','h','k','r'],
  ['i','f','l','v']
]

输出: ["eat","oath"]
说明:
你可以假设所有输入都由小写字母 a-z 组成。

提示:

你需要优化回溯算法以通过更大数据量的测试。你能否早点停止回溯？
如果当前单词不存在于所有单词的前缀中，则可以立即停止回溯。什么样的数据结构可以有效地执行这样的操作？散列表是否可行？为什么？ 前缀树如何？如果你想学习如何实现一个基本的前缀树，请先查看这个问题： 实现Trie（前缀树）。

*/

/// <summary>
/// https://leetcode-cn.com/problems/word-search-ii/
/// 212. 单词搜索 II
///
///
///
/// </summary>
internal class WordSearchIISolution
{
    public void Test()
    {
        char[][] board = new char[][] { new char[]{ 'o', 'a', 'a', 'n' },
            new char[]{'e','t','a','e'},
            new char[]{'i','h','k','r'},
            new char[]{'i','f','l','v'}
        };
        string[] words = new string[]{"oath","pea","eat","rain"};
        var ret = FindWords(board, words);
    }

    public IList<string> FindWords(char[][] board, string[] words)
    {
        List<string> ret = new List<string>();
        TrieNode root = new TrieNode();
        foreach (var word in words) root.AddWord(word);

        int m = board.Length;
        int n = board[0].Length;
        for (int row = 0; row < m; ++row)
            for (int col = 0; col < n; ++col)
                if(!root.IsClosed) BackTracking(row, col, root);
        return ret;

        void BackTracking(int row, int col, TrieNode parent)
        {
            var letter = board[row][col];
            TrieNode node = parent.FindNode(letter);
            if (node == null || node.IsClosed) return;

            if (node.IsWord)
            {
                ret.Add(node.ToWord());
                node.IsWord = false;
                node.CheckAndClose();
            }
            if (node.IsClosed) return;

            board[row][col] = '#';

            int[] rowOffset = { -1, 0, 1, 0 };
            int[] colOffset = { 0, 1, 0, -1 };
            for (int i = 0; i < 4; ++i)
            {
                int newRow = row + rowOffset[i];
                int newCol = col + colOffset[i];
                if (newRow < 0 || newRow >= m || newCol < 0 || newCol >= n) continue;

                BackTracking(newRow, newCol, node);
            }

            board[row][col] = letter;
        }
    }

    public class TrieNode
    {
        private const char A = 'a';
        public static char Invalid = '#';
        public TrieNode[] Children { get; } = new TrieNode[26];
        public TrieNode Parent { get; set; }
        public bool IsWord { get; set; } = false;
        public bool IsClosed { get; set; } = false;
        public char Char { get; set; }
        public int Level { get; set; } = 0;

        public void AddWord(string word)
        {
            if (string.IsNullOrWhiteSpace(word)) return;

            var node = this;
            foreach (var c in word)
            {
                var index = c - A;
                var child = node.Children[index];
                if (child == null) node.Children[index] = child = new TrieNode()
                {
                    Parent = node,
                    Char = c,
                    Level = node.Level + 1,
                };
                node = child;
            }
            node.IsWord = true;
        }

        public string ToWord()
        {
            if (Level == 0) return string.Empty;
            var chars = new char[Level];
            int index = chars.Length - 1;
            var node = this;
            while (0 < node.Level)
            {
                chars[index--] = node.Char;
                node = node.Parent;
            }
            return new string(chars);
        }

        public void CheckAndClose()
        {
            if (!IsWord)
            {
                bool empty = true;
                for (int i = 0; i < Children.Length; i++)
                    if (Children[i] != null) empty = false;
                if (empty)
                {
                    IsClosed = true;
                    if (Parent != null)
                    {
                        Parent.Children[Char - A] = null;
                        Parent.CheckAndClose();
                    }
                }
            }
        }

        public TrieNode FindNode(string word)
        {
            if (string.IsNullOrEmpty(word)) return null;
            var node = this;
            foreach (var c in word)
            {
                TrieNode child = node.Children[c - A];
                if (child == null) return null;
                node = child;
            }
            return node;
        }

        public TrieNode FindNode(char letter)
        {
            if (letter == Invalid) return null;
            return this.Children[letter - A];
        }
    }
}

/*

单词搜索 II
力扣 (LeetCode)
发布于 2020-03-23
9.8k
方法一：使用前缀树的回溯
这个问题实际上是一个简化的纵横填字游戏，在这个游戏中，单词的解已经被嵌入了一些无关字母。我们要做的就是把它们划掉。

直观地说，为了划掉所有潜在的单词，总体策略是一个接一个地迭代单元格，然后从每个单元格沿着它的四个潜在方向的走，找到匹配的单词。

当我们在黑板上徘徊时，若我们知道这不会发现新单词时，我们会停止探索。

有人可能已经猜到了我们用来解决这个问题的方法。是的，它是回溯，这将是解决方案的主干。构造一个回溯的解决方案是相当简单的。

解决这个问题的关键在于我们如何从字典中找到单词的匹配项。直观地说，可以使用 hashset 数据结构（例如Python 中的 set()）。

然而，在回溯过程中，人们会更经常地遇到这样的问题：是否存在任何包含特定前缀的单词，而不是是否有一个字符串作为单词存在于字典中。因为如果我们知道给定前缀的字典中不存在任何单词匹配，那么我们就不需要进一步探索某个方向。而这，将大大减少探测空间，从而提高回溯算法的性能。

能够查找前缀的数据结构叫 Trie，于 hashset 比较。Trie 不仅可以检查一个单词，还可以立即找到共享给定前缀的单词。事实证明，数据结构的选择（Trie 与 hashset）可能以排名前 5% 或后 5% 的解决方案结束。

这里我们展示了一个由单词列表构建的 Trie 示例。如下图所示，在所表示的节点处，我们将知道字典中至少有两个前缀为 d 的单词。

在这里插入图片描述

我们在实现 Trie 数据结构时遇到麻烦。我们可以从Trie 问题开始作为热身，然后再回来这个问题。

算法：

该算法的整个工作流程是直观的，包括在二维网格中的每个单元上循环和从单元开始的递归函数调用。这是算法的框架。

我们根据字典中的单词构建一个 Trie，稍后将用于匹配过程。
从每个单元格开始，如果字典中存在以单元格中的字母开头的单词,则我们开始回溯探索（即 backtracking(cell)）。
在递归函数 backtracking(cell) 调用过程中，我们探索当前单元格周围的相邻单元格（即 neighborCell）以进行下一个递归调用 backtracking(neighborCell)。在每次调用时，我们都会检查到目前为止遍历的字母序列是否与字典中的任何单词匹配，这需要借助于我们在开始时构建的 Trie 数据结构。
下面是算法的工作原理的。基于上述思想，我们给出了一些示例实现。之后，我们详细介绍了一些可以进一步应用于该算法的优化。

在这里插入图片描述


class TrieNode {
  HashMap<Character, TrieNode> children = new HashMap<Character, TrieNode>();
  String word = null;
  public TrieNode() {}
}

class Solution {
  char[][] _board = null;
  ArrayList<String> _result = new ArrayList<String>();

  public List<String> findWords(char[][] board, String[] words) {

    // Step 1). Construct the Trie
    TrieNode root = new TrieNode();
    for (String word : words) {
      TrieNode node = root;

      for (Character letter : word.toCharArray()) {
        if (node.children.containsKey(letter)) {
          node = node.children.get(letter);
        } else {
          TrieNode newNode = new TrieNode();
          node.children.put(letter, newNode);
          node = newNode;
        }
      }
      node.word = word;  // store words in Trie
    }

    this._board = board;
    // Step 2). Backtracking starting for each cell in the board
    for (int row = 0; row < board.length; ++row) {
      for (int col = 0; col < board[row].length; ++col) {
        if (root.children.containsKey(board[row][col])) {
          backtracking(row, col, root);
        }
      }
    }

    return this._result;
  }
  
  private void backtracking(int row, int col, TrieNode parent) {
    Character letter = this._board[row][col];
    TrieNode currNode = parent.children.get(letter);

    // check if there is any match
    if (currNode.word != null) {
      this._result.add(currNode.word);
      currNode.word = null;
    }

    // mark the current letter before the EXPLORATION
    this._board[row][col] = '#';

    // explore neighbor cells in around-clock directions: up, right, down, left
    int[] rowOffset = {-1, 0, 1, 0};
    int[] colOffset = {0, 1, 0, -1};
    for (int i = 0; i < 4; ++i) {
      int newRow = row + rowOffset[i];
      int newCol = col + colOffset[i];
      if (newRow < 0 || newRow >= this._board.length || newCol < 0
          || newCol >= this._board[0].length) {
        continue;
      }
      if (currNode.children.containsKey(this._board[newRow][newCol])) {
        backtracking(newRow, newCol, currNode);
      }
    }

    // End of EXPLORATION, restore the original letter in the board.
    this._board[row][col] = letter;

    // Optimization: incrementally remove the leaf nodes
    if (currNode.children.isEmpty()) {
      parent.children.remove(letter);
    }
  }
}
为了更好地理解回溯过程，我们将在下面的动画中演示如何在 Trie 中找到 dog。



优化：

在上述实现中，除了应用 Trie 数据结构外，我们还应用了一些技巧来进一步加快运行时间。特别是，Python 实现的运行速度可以超过 98% 的提交。我们按其重要性将这些技巧详述如下。

沿着 Trie 的节点回溯。
人们可以简单地使用 Trie 作为字典来快速找到单词和前缀的匹配，即在回溯的每一步，我们都从Trie 的根开始。

然而，更有效的方法是将 Trie 与回溯过程一起遍历，即每一步 backtracking(TrieNode)，TrieNode 的深度对应于我们到目前为止匹配的前缀的长度。这项措施可以将您的解决方案从提交的最低 5%5 中脱颖出来。

在回溯过程中逐渐剪除 Trie 中的节点（剪枝）。
这个想法的动机是整个算法的时间复杂度取决于 Trie 的大小。对于 Trie 中的叶节点，一旦遍历它（即找到匹配的单词），就不需要再遍历它了。结果，我们可以把它从树上剪下来。

逐渐地，这些非叶节点可以成为叶节点以后，因为我们修剪他们的孩子叶节点。在极端情况下，一旦我们找到字典中所有单词的匹配项，Trie 就会变成空的。这个剪枝措施可以减少在线测试用例 50% 的运行时间。

在这里插入图片描述

从 Trie 中删除匹配的单词。
在这个问题中，我们被要求返回所有匹配的单词，而不是潜在匹配的数量。因此，一旦到达包含单词匹配的特定 Trie 节点，我们就可以从 Trie 中删除匹配单词。

作为附带好处，我们不需要检查结果集中是否有任何重复项。因此，我们可以简单地使用一个列表而不是集合来保存结果，这样可以加快解决方案的速度。

复杂度分析

时间复杂度：\mathcal{O}(M(4\cdot3^{L-1}))O(M(4⋅3 
L−1
 ))，其中MM 是二维网格中的单元格数，LL 是单词的最大长度。
计算回溯算法将执行的确切步数是一个棘手的问题。我们为这个问题的最坏情况提供了该步骤的上限。该算法循环遍历二维网格中的所有单元，因此在复杂度公式中我们有 MM 作为因子。然后将其归结为每个启动单元所需的最大步骤数（即 4\cdot3^{L-1}4⋅3 
L−1
 ）。
假设单词的最大长度是 LL，从一个单元格开始，最初我们最多可以探索 4 个方向。假设每个方向都是有效的（即最坏情况），在接下来的探索中，我们最多有 3 个相邻的单元（不包括我们来的单元）要探索。因此，在回溯探索期间，我们最多遍历 4\cdot3^{L-1}4⋅3 
L−1
  个单元格。
你可能会想最坏的情况是什么样子。这里有一个例子。想象一下，二维网格中的每个单元都包含字母 a，单词词典包含一个单词 ['aaaa']。这是算法将遇到的最坏的情况之一。
在这里插入图片描述

注意，上述时间复杂性是在 Trie 数据结构一旦构建就不会改变的假设下估计的。如果采用优化策略逐步删除 Trie 中的节点，则可以大大提高时间复杂度，因为一旦匹配词典中的所有单词，即 Trie 变为空，回溯的成本就会降低到零。

空间复杂度：\mathcal{O}(N)O(N)，其中 NN 是字典中的字母总数。
算法消耗的主要空间是我们构建的 Trie 数据结构。在最坏的情况下，如果单词之间没有前缀重叠，则 Trie 将拥有与所有单词的字母一样多的节点。也可以选择在 Trie 中保留单词的副本。因此，我们可能需要 2N2N 的空间用于 Trie。

public class Solution {
    public class Trie
        {
            public Trie[] Next;
            public bool IsEnd;
            public string Word;
            public Trie()
            {
                Next = new Trie[26];
            }

            public void Insert(string word)
            {
                Trie cur = this;
                foreach (char c in word)
                {
                    if (cur.Next[c - 'a'] == null)
                    {
                        cur.Next[c - 'a'] = new Trie();
                    }
                    cur = cur.Next[c - 'a'];
                }
                cur.IsEnd = true;
                cur.Word = word;
            }

            public bool IsLeaf()
            {
                foreach(Trie sub in this.Next)
                {
                    if(sub != null)
                    {
                        return false;
                    }
                }
                return true;
            }
        }
        char[][] _board = null;
        List<string> result = new List<string>();
        int[] rowOffset = new int[] { -1, 0, 1, 0 };
        int[] colOffset = new int[] { 0, 1, 0, -1 };

        public IList<string> FindWords(char [][] board , String[] words)
        {
            Trie root = new Trie();
            foreach(string word in words)
            {
                root.Insert(word);
            }

            this._board = board;
            
            for(int row =0;row<board.Length;row++)
            {
                for(int col=0;col<board[row].Length;col++)
                {
                    if(root.Next[board[row][col] -'a'] != null)
                    {
                        BackTracking(row, col, root);
                    }
                }
            }
            return this.result;
        }

        private void BackTracking(int row, int col, Trie parent)
        {
            char letter = this._board[row][col];
            Trie cur = parent.Next[letter-'a'];
            if(cur.IsEnd)
            {
                this.result.Add(cur.Word);
                cur.Word = null;
                cur.IsEnd = false;
            }
            this._board[row][col] = '#';
            for(int i=0;i<4;i++)
            {
                int newRow = row + rowOffset[i];
                int newCol = col + colOffset[i];
                if(newRow<0 || newCol<0 || newRow >= _board.Length || newCol >= _board[0].Length)
                {
                    continue;
                }
                if(_board[newRow][newCol] != '#' && cur.Next[_board[newRow][newCol] -'a'] != null)
                {
                    BackTracking(newRow, newCol, cur);
                }
            }
            this._board[row][col] = letter;
            if(cur.IsLeaf())
            {
                parent.Next[letter-'a'] = null;
            }
        }
}

public class Solution {
    public IList<string> FindWords(char[][] board, string[] words)
    {
        List<string> output = new List<string>();

        TrieNode root = new TrieNode();
        foreach (string word in words)
        {
            TrieNode node = root;
            foreach (char c in word)
            {
                if (!node.Childrens.ContainsKey(c))
                    node.Childrens.Add(c, new TrieNode());
                node = node.Childrens[c];
            }
            node.word = word;
        }

        for (int i = 0; i < board.Length; i++)
        {
            for (int j = 0, len = board[i].Length; j < len; j++)
            {
                if (root.Childrens.ContainsKey(board[i][j]))
                    output.AddRange(Backtracking(root, board, i, j));
            }
        }

        return output;
    }

    private List<string> Backtracking(TrieNode parent, char[][] board, int row, int col)
    {
        List<string> result = new List<string>();
        char c = board[row][col];
        TrieNode node = parent.Childrens[c];

        if (node.word != null)
        {
            result.Add(node.word);
            node.word = null;
        }

        board[row][col] = '#';
        int[] rowOffset = { -1, 0, 1, 0 }, colOffset = { 0, 1, 0, -1 };
        for (int i = 0; i < 4; i++)
        {
            int newRow = row + rowOffset[i];
            int newCol = col + colOffset[i];
            if (newRow < 0 || newRow >= board.Length || newCol < 0 || newCol >= board[0].Length)
                continue;

            if (node.Childrens.ContainsKey(board[newRow][newCol]))
                result.AddRange(Backtracking(node, board, newRow, newCol));
        }
        board[row][col] = c;

        if (node.Childrens.Count == 0)
            parent.Childrens.Remove(c);

        return result;
    }

    class TrieNode
    {
        public Dictionary<char, TrieNode> Childrens;
        public string word = null;
        public TrieNode()
        {
            Childrens = new Dictionary<char, TrieNode>();
        }
    }
}


*/