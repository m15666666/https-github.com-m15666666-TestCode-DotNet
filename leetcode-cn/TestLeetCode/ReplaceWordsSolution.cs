using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
在英语中，我们有一个叫做 词根(root)的概念，它可以跟着其他一些词组成另一个较长的单词——我们称这个词为 继承词(successor)。例如，词根an，跟随着单词 other(其他)，可以形成新的单词 another(另一个)。

现在，给定一个由许多词根组成的词典和一个句子。你需要将句子中的所有继承词用词根替换掉。如果继承词有许多可以形成它的词根，则用最短的词根替换它。

你需要输出替换之后的句子。

示例 1:

输入: dict(词典) = ["cat", "bat", "rat"]
sentence(句子) = "the cattle was rattled by the battery"
输出: "the cat was rat by the bat"
注:

输入只包含小写字母。
1 <= 字典单词数 <=1000
1 <=  句中词语数 <= 1000
1 <= 词根长度 <= 100
1 <= 句中词语长度 <= 1000 
*/
/// <summary>
/// https://leetcode-cn.com/problems/replace-words/
/// 648. 单词替换
/// https://blog.csdn.net/weixin_31866177/article/details/84136107
/// </summary>
class ReplaceWordsSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public string ReplaceWords(IList<string> dict, string sentence)
    {
        Trie trie = new Trie();
        foreach (var dic in dict) trie.Add(dic);

        const char Split = ' ';
        string[] parts = sentence.Split(Split, StringSplitOptions.RemoveEmptyEntries);
        List<string> ret = new List<string>(parts.Length);
        foreach ( var part in parts ) ret.Add(trie.FindRoot(part));

        return string.Join(Split, ret);
    }
    
    class Trie
    {
        private Node _root = new Node();

        public string FindRoot(string s)
        {
            Node cur = _root;
            StringBuilder sb = new StringBuilder();
            foreach (var c in s)
            {
                sb.Append(c);
                if (!cur.Map.ContainsKey(c))
                    return s;
                else
                {
                    cur = cur.Map[c];

                    if (cur.IsWord)
                        return sb.ToString();
                }
            }
            return s;
        }

        public void Add(string s)
        {
            Node cur = _root;
            foreach(var c in s)
            {
                if (!cur.Map.ContainsKey(c))
                    cur.Map.Add(c, new Node());
                cur = cur.Map[c];
            }
            cur.IsWord = true;
        }
        public bool Contains(string s)
        {
            Node cur = _root;
            foreach (var c in s)
            {
                if (!cur.Map.ContainsKey(c))
                    return false;
                cur = cur.Map[c];
            }
            return cur.IsWord;
        }
    }
    class Node
    {
        public bool IsWord { get; set; }
        public Dictionary<char, Node> Map { get; private set; } = new Dictionary<char, Node>();
    }
}
/*
public class Solution {
    public string ReplaceWords(IList<string> dict, string sentence) {
        string[] arr = sentence.Split(" ");
        DicTree tree = new DicTree();
        foreach(string str in dict){
            tree.Insert(str);
        }
        for(int i = 0; i < arr.Length; ++i){
            string tmp = tree.BeginWith(arr[i]);
            if(tmp != null){
                arr[i] = tmp;
            }
        }
        //return arr.Join(" ");
        return String.Join(" ", arr);
    }
}
public class DicTree{
    private Node root;
    public DicTree(){
        root = new Node('.');
    }
    public void Insert(string s){
        Node tmpNode = root;
        foreach(char c in s){
            int pos = c - 'a';
            if(tmpNode.next[pos] == null){
                tmpNode.next[pos] = new Node(c);
            }
            tmpNode = tmpNode.next[pos];
        }
        tmpNode.isEnd = true;
    }
    public string BeginWith(string s){
        Node tmpNode = root;
        //foreach(char c in s){
        for(int i = 0; i<s.Length; ++i){
            char c = s[i];
            int pos = c - 'a';
            if(tmpNode.next[pos] == null){
                return null;
            }
            if(tmpNode.next[pos].isEnd){
                return s.Substring(0,i+1);
            }
            tmpNode = tmpNode.next[pos];
        }
        return null;
    }
}
public class Node
{
    private static int _SIZE = 26;
    public char val;
    public Node[] next;
    public bool isEnd;
    public Node(char val)
    {
        this.val = val;
        next = new Node[_SIZE];
        isEnd = false;
    }
}


public class Solution
{
    public string ReplaceWords(IList<string> dict, string sentence)
    {
        string[] strs = sentence.Split(" ");

        List<string> myDic = new List<string>();

        for (int i = 0; i < dict.Count(); i++)
        {
            myDic.Add(dict[i]);
        }

        myDic.Sort((a, b) => {
            if (a.Length < b.Length)
            {
                return -1;
            }
            else if (a.Length > b.Length)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        });


        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < strs.Length; i++)
        {
            for (int j = 0; j < myDic.Count(); j++)
            {
                if (strs[i].StartsWith(myDic[j]))
                {
                    strs[i] = myDic[j];
                    break;
                }
            }

            sb.Append(strs[i]);
            if (i != strs.Length - 1)
                sb.Append(" ");
        }


        return sb.ToString();
    }
}
*/