using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// https://leetcode-cn.com/problems/add-and-search-word-data-structure-design/
/// 211. 添加与搜索单词 - 数据结构设计
/// 设计一个支持以下两种操作的数据结构：
/// void addWord(word)
/// bool search(word)
/// search(word) 可以搜索文字或正则表达式字符串，字符串只包含字母.或 a-z 。 . 可以表示任何一个字母。
/// 示例:
/// addWord("bad")
/// addWord("dad")
/// addWord("mad")
/// search("pad") -> false
/// search("bad") -> true
/// search(".ad") -> true
/// search("b..") -> true
/// https://blog.csdn.net/laputafallen/article/details/79998823
/// </summary>
public class WordDictionary
{
    private TrieNode RootNode { get; } = new TrieNode();

    /** Initialize your data structure here. */
    public WordDictionary()
    {

    }

    /** Adds a word into the data structure. */
    public void AddWord(string word)
    {
        if (string.IsNullOrWhiteSpace(word)) return;

        var currentNode = RootNode;
        foreach( var c in word)
        {
            var index = c - 'a';
            var child = currentNode.Children[index];
            if ( child == null)
            {
                currentNode.Children[index] = child = new TrieNode();
            }
            currentNode = child;
        }
        currentNode.IsWord = true;
    }

    /** Returns if the word is in the data structure. A word could contain the dot character '.' to represent any one letter. */
    public bool Search(string word)
    {
        return Find(word, RootNode, 0);
    }

    private bool Find(string word, TrieNode node, int index)
    {
        //若到达word末尾，判断字典树当前节点是否有对应字符串
        if (index == word.Length) return node.IsWord;
        var c = word[index];
        if (c == '.')
        {
            //回溯该节点所有分支
            foreach (TrieNode child in node.Children)
            {
                if (child != null && Find(word, child, index + 1)) return true;
            }
            return false;
        }
        {
            TrieNode child = node.Children[c - 'a'];
            return child != null && Find(word, child, index + 1);
        }
    }

    class TrieNode
    {
        public TrieNode[] Children { get;  } = new TrieNode[26];
        public bool IsWord { get; set; }
    }
}

/**
 * Your WordDictionary object will be instantiated and called as such:
 * WordDictionary obj = new WordDictionary();
 * obj.AddWord(word);
 * bool param_2 = obj.Search(word);
 */
