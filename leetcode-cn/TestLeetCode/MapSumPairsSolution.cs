using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
实现一个 MapSum 类里的两个方法，insert 和 sum。

对于方法 insert，你将得到一对（字符串，整数）的键值对。字符串表示键，整数表示值。
如果键已经存在，那么原来的键值对将被替代成新的键值对。

对于方法 sum，你将得到一个表示前缀的字符串，你需要返回所有以该前缀开头的键的值的总和。

示例 1:

输入: insert("apple", 3), 输出: Null
输入: sum("ap"), 输出: 3
输入: insert("app", 2), 输出: Null
输入: sum("ap"), 输出: 5 
*/
/// <summary>
/// https://leetcode-cn.com/problems/map-sum-pairs/
/// 677. 键值映射
/// 
/// </summary>
class MapSumPairsSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    /** Initialize your data structure here. */
    public MapSumPairsSolution()
    {

    }

    private Dictionary<string, int> _map = new Dictionary<string, int>();
    public void Insert(string key, int val)
    {
        if (!_map.ContainsKey(key)) _map.Add(key, val);
        else _map[key] = val;
    }

    public int Sum(string prefix)
    {
        int ret = 0;
        foreach(var pair in _map)
        {
            if (pair.Key.StartsWith(prefix)) ret += pair.Value;
        }
        return ret;
    }
}
/*
public class MapSum {
    private MapSumNode root = new MapSumNode();
    public MapSum() { }

    public void Insert(string key, int val)
    {
        var cur = root;
        foreach (var c in key)
        {
            if (cur.Children[c - 'a'] == null)
            {
                cur.Children[c - 'a'] = new MapSumNode();
            }
            cur = cur.Children[c - 'a'];
        }
        cur.Value = val;
        cur.IsWord = true;
    }

    public int Sum(string prefix)
    {
        var cur = root;
        foreach (var c in prefix)
        {
            if (cur.Children[c - 'a'] == null)
            {
                return 0;
            }
            cur = cur.Children[c - 'a'];
        }
        return dfs(cur);
    }

    public int dfs(MapSumNode root)
    {
        var sum = root.Value;
        foreach (var child in root.Children)
        {
            if (child != null)
            {
                sum += dfs(child);
            }
        }
        return sum;
    }
}

public class MapSumNode
{
    public int Value { get; set; }
    public bool IsWord { get; set; }
    public MapSumNode[] Children { get; set; }

    public MapSumNode()
    {
        this.IsWord = false;
        this.Children = new MapSumNode[26];
    }

    public MapSumNode(int value)
    {
        this.Value = value;
        this.IsWord = false;
        this.Children = new MapSumNode[26];
    }
}
*/