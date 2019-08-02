using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
实现一个带有buildDict, 以及 search方法的魔法字典。

对于buildDict方法，你将被给定一串不重复的单词来构建一个字典。

对于search方法，你将被给定一个单词，并且判定能否只将这个单词中一个字母换成另一个字母，使得所形成的新单词存在于你构建的字典中。

示例 1:

Input: buildDict(["hello", "leetcode"]), Output: Null
Input: search("hello"), Output: False
Input: search("hhllo"), Output: True
Input: search("hell"), Output: False
Input: search("leetcoded"), Output: False
注意:

你可以假设所有输入都是小写字母 a-z。
为了便于竞赛，测试所用的数据量很小。你可以在竞赛结束后，考虑更高效的算法。
请记住重置MagicDictionary类中声明的类变量，因为静态/类变量会在多个测试用例中保留。 请参阅这里了解更多详情。 
*/
/// <summary>
/// https://leetcode-cn.com/problems/implement-magic-dictionary/
/// 676. 实现一个魔法字典
/// 
/// </summary>
class ImplementMagicDictionarySolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    /** Initialize your data structure here. */
    public ImplementMagicDictionarySolution()
    {

    }

    /** Build a dictionary through a list of words */
    public void BuildDict(string[] dict)
    {
        _dict = new Dictionary<string, List<Tuple<int, char>>>();
        foreach (var s in dict)
        {
            StringBuilder sb = new StringBuilder(s);
            for (int i = 0; i < s.Length; i++)
            {
                var c = s[i];
                sb.Remove(i, 1);
                var key = sb.ToString();
                sb.Insert(i, c);
                Tuple<int, char> t = new Tuple<int, char>(i, c);

                if (!_dict.ContainsKey(key)) _dict.Add(key, new List<Tuple<int, char>> { t });
                else _dict[key].Add(t);
            }
        }
    }

    private Dictionary<string, List<Tuple<int, char>>> _dict;

    /** Returns if there is any word in the trie that equals to the given word after modifying exactly one character */
    public bool Search(string word)
    {
        StringBuilder sb = new StringBuilder(word);
        for (int i = 0; i < word.Length; i++)
        {
            var c = word[i];
            sb.Remove(i, 1);
            var key = sb.ToString();
            sb.Insert(i, c);
            if (_dict.ContainsKey(key))
            {
                foreach (var pair in _dict[key])
                {
                    if (pair.Item1 == i && pair.Item2 != c) return true;
                }
            }
        }
        return false;
    }
}
/*
public class MagicDictionary {
    List<string> list=new List<string>();
    public MagicDictionary()
    {

    }

    public void BuildDict(string[] dict)
    {
        for (int i = 0; i < dict.GetLength(0); i++)
        {
            list.Add(dict[i]);
        }
    }

    public bool Search(string word)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].Length != word.Length)
                continue;
            int butong = 0;
            for (int j = 0; j < word.Length; j++)
            {
                if (list[i][j] != word[j])
                {
                    butong++;
                    if (butong >= 2)
                        break;
                }
            }
            if (butong == 1)
                return true;
        }
        return false;
    }
}

*/