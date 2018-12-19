using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// https://leetcode-cn.com/problems/implement-trie-prefix-tree/
/// 208. 实现 Trie (前缀树)
/// 实现一个 Trie (前缀树)，包含 insert, search, 和 startsWith 这三个操作。
/// 示例:
/// Trie trie = new Trie();
/// trie.insert("apple");
/// trie.search("apple");   // 返回 true
/// trie.search("app");     // 返回 false
/// trie.startsWith("app"); // 返回 true
/// trie.insert("app");   
/// trie.search("app");     // 返回 true
/// https://segmentfault.com/a/1190000015804960
/// </summary>
class Trie // ImplementTriePrefixTreeSolution
{
    public static void Test()
    {
        Trie trie = new Trie();
        trie.Insert("");
        trie.Insert("apple");
        var ret = trie.Search("apple");
        ret = trie.StartsWith("app");
        trie.Insert("app");
        ret = trie.Search("app");
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    /**
     * a-z有26个字母，需要访问时由于a的ASCII码为97，所以所有字母访问的对应下表皆为 字母的ASCII码-97
     */
    public Trie[] Children { get; } = new Trie[26];
    private const char Start_a = 'a';

    /**
     * 标识此节点是否为某个单词的结束节点
     */
    public bool EndAsWord { get; set; }

    /** Initialize your data structure here. */
    public Trie()
    {
    }

    /** Inserts a word into the trie. */
    public void Insert(string word)
    {
        if (string.IsNullOrEmpty(word)) return;

        Trie currentNode = this;
        foreach ( var c in word)
        {
            var leafIndex = c - Start_a;
            var leaf = currentNode.Children[leafIndex];
            if( leaf == null )
            {
                leaf = new Trie();
                //leaf.value = c;
                currentNode.Children[leafIndex] = leaf;
            }
            currentNode = leaf;
        }
        currentNode.EndAsWord = true;
    }

    /** Returns if the word is in the trie. */
    public bool Search(string word)
    {
        if (string.IsNullOrWhiteSpace(word)) return true;

        Trie currentNode = this;
        foreach (var c in word)
        {
            var leafIndex = c - Start_a;
            var leaf = currentNode.Children[leafIndex];
            if (leaf == null) return false;

            currentNode = leaf;
        }
        return currentNode.EndAsWord;
    }

    /** Returns if there is any word in the trie that starts with the given prefix. */
    public bool StartsWith(string prefix)
    {
        if (string.IsNullOrEmpty(prefix)) return true;

        Trie currentNode = this;
        foreach (var c in prefix)
        {
            var leafIndex = c - Start_a;
            var leaf = currentNode.Children[leafIndex];
            if (leaf == null) return false;

            currentNode = leaf;
        }
        return true;
    }
}