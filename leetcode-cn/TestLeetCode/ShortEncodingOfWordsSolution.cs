using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个单词列表，我们将这个列表编码成一个索引字符串 S 与一个索引列表 A。

例如，如果这个列表是 ["time", "me", "bell"]，我们就可以将其表示为 S = "time#bell#" 和 indexes = [0, 2, 5]。

对于每一个索引，我们可以通过从字符串 S 中索引的位置开始读取字符串，直到 "#" 结束，来恢复我们之前的单词列表。

那么成功对给定单词列表进行编码的最小字符串长度是多少呢？

示例：

输入: words = ["time", "me", "bell"]
输出: 10
说明: S = "time#bell#" ， indexes = [0, 2, 5] 。

提示：

1 <= words.length <= 2000
1 <= words[i].length <= 7
每个单词都是小写字母 。
*/
/// <summary>
/// https://leetcode-cn.com/problems/short-encoding-of-words/
/// 820. 单词的压缩编码
/// https://blog.csdn.net/qq_41855420/article/details/90406569
/// </summary>
class ShortEncodingOfWordsSolution
{
    public void Test()
    {
        var ret = this.MinimumLengthEncoding(new string[] { "time", "me", "bell" });
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int MinimumLengthEncoding(string[] words)
    {
        //第一步：首先利用words构造前缀树
        var root = new TrieNode();
        foreach (var word in words)
        {
            AddWord(word, root);
        }

        int ret = 0;
        //第二步：开始搜索树中每条路径的长度
        FindCount(root, 1, ref ret);//注意起始节点深度为1，"time#"中的字符'#'
        return ret;
    }

    private static void AddWord(string word, TrieNode root) {
        const char a = 'a';
		TrieNode node = root;//扫描这棵树，插入word
        for ( int i = word.Length - 1; -1 < i; i--){
            var index = word[i] - a;
            if (node.children[index] == null) {
                node.children[index] = new TrieNode();
            }
            node = node.children[index];
        }
        node.isWord = true;//标记为单词
    }

    /// <summary>
    /// 寻找以root为根的路径长度（深度）
    /// </summary>
    private static void FindCount(TrieNode trieRoot, int depth, ref int resCount)
    {
        bool flag = true;//如果当前trieRoot没有孩子
        for (int i = 0; i < 26; ++i)
        {
            if (trieRoot.children[i] == null) continue;
            flag = false;
            FindCount(trieRoot.children[i], depth + 1, ref resCount);
        }
        // 说明它数路径终点，比如"time#"中的字符't'
        if (flag) resCount += depth;
    }

    /// <summary>
    /// 前缀树的程序表示
    /// </summary>
    public class TrieNode
    {
        public bool isWord = false;//当前节点为结尾是否是单词
        public TrieNode[] children = new TrieNode[26];
    }
}
/*
public class Solution {
    public int MinimumLengthEncoding(string[] words) {
        
        int MinLength = 0;
        
        LetterTree Root = new LetterTree();
        Root.letters = new LetterTree[26];
        
        foreach(var word in words)
        {
            var current = Root;
            
            for (int i = word.Length - 1; i >= 0; i--)
            {
                int index = word[i] - 'a';
                if(current.letters == null)
                {
                    //路径存在，但需要延长
                    //加上当前单词剩余长度
                    MinLength++;
                    current.letters = new LetterTree [26];
                    current.letters[index] = new LetterTree();
                    current = current.letters[index];
                    
                }
                else 
                {
                    if(current.letters[index] == null)
                    {
                        //当前路径存在，但需要新的分支
                        //那么需要增加一个#号，以及当前完整单词
                        MinLength += word.Length - i + 1;
                        
                        current.letters[index] = new LetterTree();
                        current = current.letters[index];
                    }
                    else
                    {
                        //当前路径存在，且存在分支，接续向下查找
                        current = current.letters[index];
                    }
                }
            }
        }
        
        return MinLength;
    }
    
    private class LetterTree {
        public LetterTree [] letters;
    }
} 
*/
