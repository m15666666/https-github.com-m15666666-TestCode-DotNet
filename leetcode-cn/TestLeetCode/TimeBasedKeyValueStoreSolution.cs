using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
创建一个基于时间的键值存储类 TimeMap，它支持下面两个操作：

1. set(string key, string value, int timestamp)

存储键 key、值 value，以及给定的时间戳 timestamp。
2. get(string key, int timestamp)

返回先前调用 set(key, value, timestamp_prev) 所存储的值，其中 timestamp_prev <= timestamp。
如果有多个这样的值，则返回对应最大的  timestamp_prev 的那个值。
如果没有值，则返回空字符串（""）。
 

示例 1：

输入：inputs = ["TimeMap","set","get","get","set","get","get"], inputs = [[],["foo","bar",1],["foo",1],["foo",3],["foo","bar2",4],["foo",4],["foo",5]]
输出：[null,null,"bar","bar",null,"bar2","bar2"]
解释：  
TimeMap kv;   
kv.set("foo", "bar", 1); // 存储键 "foo" 和值 "bar" 以及时间戳 timestamp = 1   
kv.get("foo", 1);  // 输出 "bar"   
kv.get("foo", 3); // 输出 "bar" 因为在时间戳 3 和时间戳 2 处没有对应 "foo" 的值，所以唯一的值位于时间戳 1 处（即 "bar"）   
kv.set("foo", "bar2", 4);   
kv.get("foo", 4); // 输出 "bar2"   
kv.get("foo", 5); // 输出 "bar2"   

示例 2：

输入：inputs = ["TimeMap","set","set","get","get","get","get","get"], inputs = [[],["love","high",10],["love","low",20],["love",5],["love",10],["love",15],["love",20],["love",25]]
输出：[null,null,null,"","high","high","low","low"]
 

提示：

所有的键/值字符串都是小写的。
所有的键/值字符串长度都在 [1, 100] 范围内。
所有 TimeMap.set 操作中的时间戳 timestamps 都是严格递增的。
1 <= timestamp <= 10^7
TimeMap.set 和 TimeMap.get 函数在每个测试用例中将（组合）调用总计 120000 次。
*/
/// <summary>
/// https://leetcode-cn.com/problems/time-based-key-value-store/
/// 981. 基于时间的键值存储
/// 
/// </summary>
class TimeBasedKeyValueStoreSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public TimeBasedKeyValueStoreSolution()
    {
    }

    
    public void Set(string key, string value, int timestamp)
    {
        if (!_map.ContainsKey(key)) _map[key] = new List<Tuple<string, int>>();
        _map[key].Add(new Tuple<string, int>(value, timestamp));
    }

    private readonly Dictionary<string, List<Tuple<string, int>>> _map = new Dictionary<string, List<Tuple<string, int>>>();
    public string Get(string key, int timestamp)
    {
        if (!_map.ContainsKey(key)) return "";

        var list = _map[key];
        if(list.Count == 0) return "";

        var first = list[0];
        if (timestamp < first.Item2) return "";

        var last = list[list.Count - 1];
        if (last.Item2 <= timestamp) return last.Item1;

        int left = 0;
        int right = list.Count - 1;
        do
        {
            int mid = (left + right) / 2;
            var m = list[mid];
            if (timestamp < m.Item2)
            {
                right = mid - 1;
            }
            else if (m.Item2 <= timestamp)
            {
                left = mid + 1;
            }
        } while (left <= right);
        return list[left - 1].Item1;
    }
}
/*
基于时间的键值存储
力扣 (LeetCode)
发布于 1 年前
1.5k 阅读
方法一：HashMap + 二分查找
思路与算法

对于每一个键值 key 的两种操作，我们只关注键值的时间戳与值信息。我们可以将这些信息存储在一个 HashMap 中。

对于每一个键值 key，我们可以在已经按照时间戳排序好的序列中进行二分检索，从而找到对应 key 相关的 value。

import javafx.util.Pair;

class TimeMap {
    Map<String, List<Pair<Integer, String>>> M;

    public TimeMap() {
        M = new HashMap();
    }

    public void set(String key, String value, int timestamp) {
        if (!M.containsKey(key))
            M.put(key, new ArrayList<Pair<Integer, String>>());

        M.get(key).add(new Pair(timestamp, value));
    }

    public String get(String key, int timestamp) {
        if (!M.containsKey(key)) return "";

        List<Pair<Integer, String>> A = M.get(key);
        int i = Collections.binarySearch(A, new Pair<Integer, String>(timestamp, "{"),
                (a, b) -> Integer.compare(a.getKey(), b.getKey()));

        if (i >= 0)
            return A.get(i).getValue();
        else if (i == -1)
            return "";
        else
            return A.get(-i-2).getValue();
    }
}
复杂度分析

时间复杂度：对于 set 操作，O(1)O(1) 。对于 get 操作，O(\log N)O(logN)。 其中，NN 是 TimeMap 中元素的数量。

空间复杂度：O(N)O(N)。

方法二：TreeMap
思路与算法

对于 Java 语言，我们可以使用 TreeMap.floorKey(timestamp) 来找到小于等于给定时间戳 timestamp 的最大时间戳。

我们使用与 方法一 相同的方法构建解法，仅仅替换这部分的功能。

class TimeMap {
    Map<String, TreeMap<Integer, String>> M;

    public TimeMap() {
        M = new HashMap();
    }

    public void set(String key, String value, int timestamp) {
        if (!M.containsKey(key))
            M.put(key, new TreeMap());

        M.get(key).put(timestamp, value);
    }

    public String get(String key, int timestamp) {
        if (!M.containsKey(key)) return "";

        TreeMap<Integer, String> tree = M.get(key);
        Integer t = tree.floorKey(timestamp);
        return t != null ? tree.get(t) : "";
    }
}
复杂度分析

时间复杂度：对于 set 操作，O(1)O(1)。对于 get 操作，O(\log N)O(logN)。其中，N​N​ 是 TimeMap 中元素的数量。

空间复杂度：O(N)O(N)。 
*/
