using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个字符串数组，将字母异位词组合在一起。字母异位词指字母相同，但排列不同的字符串。

示例:

输入: ["eat", "tea", "tan", "ate", "nat", "bat"]
输出:
[
  ["ate","eat","tea"],
  ["nat","tan"],
  ["bat"]
]
*/
/// <summary>
/// https://leetcode-cn.com/problems/group-anagrams/
/// 49.字母异位词分组
/// 
/// </summary>
class GroupAnagramsSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public IList<IList<string>> GroupAnagrams(string[] strs)
    {
        if (strs == null || strs.Length == 0) return new List<IList<string>>();

        Dictionary<string, List<string>> map = new Dictionary<string, List<string>>();
        int[] count = new int[26];
        foreach (var str in strs)
        {
            Array.Fill(count, 0);
            foreach (var c in str) count[c - 'a']++;

            var key = string.Join(",", count);
            if (!map.ContainsKey(key)) map[key] = new List<string>() { str };
            else map[key].Add(str);
        }

        return map.Values.ToArray();
    }


    //public IList<IList<string>> GroupAnagrams(string[] strs)
    //{
    //    //List<IList<string>> ret = new List<IList<string>>();

    //    if (strs == null || strs.Length == 0) return new List<IList<string>>();

    //    Dictionary<string, List<string>> map = new Dictionary<string, List<string>>();
    //    foreach( var str in strs)
    //    {
    //        var chars = str.ToArray();
    //        Array.Sort(chars);
    //        var key = string.Join("", chars);
    //        if (!map.ContainsKey(key)) map[key] = new List<string>();
    //        map[key].Add(str);
    //    }

    //    return map.Values.ToArray();
    //}

}
/*

字母异位词分组
力扣 (LeetCode)
发布于 2 年前
42.2k
方法一：排序数组分类
思路

当且仅当它们的排序字符串相等时，两个字符串是字母异位词。

算法

维护一个映射 ans : {String -> List}，其中每个键 \text{K}K 是一个排序字符串，每个值是初始输入的字符串列表，排序后等于 \text{K}K。

在 Java 中，我们将键存储为字符串，例如，code。 在 Python 中，我们将键存储为散列化元组，例如，('c', 'o', 'd', 'e')。

Anagrams

class Solution {
    public List<List<String>> groupAnagrams(String[] strs) {
        if (strs.length == 0) return new ArrayList();
        Map<String, List> ans = new HashMap<String, List>();
        for (String s : strs) {
            char[] ca = s.toCharArray();
            Arrays.sort(ca);
            String key = String.valueOf(ca);
            if (!ans.containsKey(key)) ans.put(key, new ArrayList());
            ans.get(key).add(s);
        }
        return new ArrayList(ans.values());
    }
}
复杂度分析

时间复杂度：O(NK \log K)O(NKlogK)，其中 NN 是 strs 的长度，而 KK 是 strs 中字符串的最大长度。当我们遍历每个字符串时，外部循环具有的复杂度为 O(N)O(N)。然后，我们在 O(K \log K)O(KlogK) 的时间内对每个字符串排序。

空间复杂度：O(NK)O(NK)，排序存储在 ans 中的全部信息内容。

方法二：按计数分类
思路

当且仅当它们的字符计数（每个字符的出现次数）相同时，两个字符串是字母异位词。

算法

我们可以将每个字符串 \text{s}s 转换为字符数 \text{count}count，由26个非负整数组成，表示 \text{a}a，\text{b}b，\text{c}c 的数量等。我们使用这些计数作为哈希映射的基础。

在 Java 中，我们的字符数 count 的散列化表示将是一个用 **＃** 字符分隔的字符串。 例如，abbccc 将表示为 ＃1＃2＃3＃0＃0＃0 ...＃0，其中总共有26个条目。 在 python 中，表示将是一个计数的元组。 例如，abbccc 将表示为 (1,2,3,0,0，...，0)，其中总共有 26 个条目。

Anagrams

class Solution {
    public List<List<String>> groupAnagrams(String[] strs) {
        if (strs.length == 0) return new ArrayList();
        Map<String, List> ans = new HashMap<String, List>();
        int[] count = new int[26];
        for (String s : strs) {
            Arrays.fill(count, 0);
            for (char c : s.toCharArray()) count[c - 'a']++;

            StringBuilder sb = new StringBuilder("");
            for (int i = 0; i < 26; i++) {
                sb.append('#');
                sb.append(count[i]);
            }
            String key = sb.toString();
            if (!ans.containsKey(key)) ans.put(key, new ArrayList());
            ans.get(key).add(s);
        }
        return new ArrayList(ans.values());
    }
}
复杂度分析

时间复杂度：O(NK)O(NK)，其中 NN 是 strs 的长度，而 KK 是 strs 中字符串的最大长度。计算每个字符串的字符串大小是线性的，我们统计每个字符串。

空间复杂度：O(NK)O(NK)，排序存储在 ans 中的全部信息内容。

下一篇：Python

自定义字符串的哈希规则，使用质数作为乘法因子（Java）
liweiwei1419
发布于 13 天前
596
别看名字起得这么玄乎（没有标题党的意思），只要有哈希表的基础知识，是很容易想到思路的。

这里「哈希」是 hash 的音译，意译是「散列」。

思路：

分入一组的字符串的特征：字符以及字符的个数相等，但是顺序不同；
这样的特征其实可以做一个映射，思想来源于哈希规则。这里要去除顺序的影响，那么我们就只关心每个字符以及它出现的次数；
每个字符对应一个 ASCII 值，用 ASCII 值乘以字符出现的次数的和感觉上就能表征一组字符串，但是很容易想到，这里面会有重复的值；
一个替代的做法是，把 ASCII 值 替换成为质数，于是这些数值一定不会有公约数，不在一组的数，它们的和一定不相等（也就是放在哈希表里，肯定不会被分在一个桶里）；
所有输入均为小写字母，因此只需要做 26 个映射，这种映射可以通过数组实现。
（限于本人水平有限，这里的思路没有办法说得很清楚，也没有办法证明给大家看，只能说是想法通过测试得到验证。）

评论有朋友提到，这样计算出来的「哈希值」是有可能整型越界的，这一点是我一开始没有想到的。但是仔细算了一下，这里「消耗」最大的值就是字母 z ，它对应的 ASCII 码是 25（已经减去了偏移），它对应的质数最大是 101，如果全部使用 z 是最消耗值的，运行下面这段代码：

System.out.println(Integer.MAX_VALUE / 101 / 25);
输出：850488。

因此，要产生溢出，输入字符至少长度要达到 850488 才可以。在这里认为不存在这种测试用例。

知识点复习：

哈希表的底层就是数组；
哈希函数；
哈希冲突的解决办法：1、链接法；2、开放地址法；
哈希表的扩容。
参考《算法导论》或者《算法 4》，初学的时候懂得意思，有个感性认知即可。

参考代码：

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.Objects;

public class Solution {


    public List<List<String>> groupAnagrams(String[] strs) {

        // 考察了哈希函数的基本知识，只要 26 个即可
        // （小写字母ACSII 码 - 97 ）以后和质数的对应规则，这个数组的元素顺序无所谓
        // key 是下标，value 就是数值
        int[] primes = {2, 3, 5, 7, 11, 13, 17, 19, 23, 29,
                31, 37, 41, 43, 47, 53, 59, 61, 67, 71,
                73, 79, 83, 89, 97, 101};

        // key 是字符串自定义规则下的哈希值
        Map<Integer, List<String>> hashMap = new HashMap<>();
        for (String s : strs) {
            int hashValue = 1;

            char[] charArray = s.toCharArray();
            for (char c : charArray) {
                hashValue *= primes[c - 'a'];
            }

            // 把单词添加到哈希值相同的分组
            if (hashMap.containsKey(hashValue)) {
                List<String> curList = hashMap.get(hashValue);
                curList.add(s);
            } else {
                List<String> newList = new ArrayList<>();
                newList.add(s);

                hashMap.put(hashValue, newList);
            }
        }
        return new ArrayList<>(hashMap.values());
    }

    public static void main(String[] args) {
        String[] strs = {"eat", "tea", "tan", "ate", "nat", "bat"};

        Solution solution = new Solution();
        List<List<String>> res = solution.groupAnagrams(strs);
        System.out.println(res);

        System.out.println((int) 'a');
    }
}
下一篇：C++ 引入hash表 时间：32ms 击败99.48% 内存：18.2MB 击败84.07%

public class Solution {
    public IList<IList<string>> GroupAnagrams (string[] strs)
    {
        IList<IList<string>> res = new List<IList<string>>();
        Dictionary<int, int> map = new Dictionary<int, int>();
        int[] PRIMES = new int[] { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 
            47, 53, 59, 61, 67, 71, 73, 79, 83, 89, 97, 101, 107 };
        foreach (string str in strs)
        {
            int key = 1;
            foreach (char c in str) key *= PRIMES[(c - 'a')];
            if (!map.ContainsKey(key))
            {
                map.Add(key, res.Count);
                res.Add(new List<string>());
            }
            res[map[key]].Add(str);
        }
        return res;
    }
}

public class Solution {
    public IList<IList<string>> GroupAnagrams(string[] strs) {
        List<IList<string>> list = new List<IList<string>>();
        if (strs.Length == 0)
            return list;
        int[] prime = { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 41, 43, 47, 53, 59, 61, 67, 71, 73, 79, 83, 89, 97, 101, 103 };
        Dictionary<int, List<string>> dict = new Dictionary<int, List<string>>();
        for(int i = 0; i < strs.Length; i ++) {
            int key = 1;
            for(int j = 0; j < strs[i].Length; j++) {
                key *= prime[strs[i][j] - 'a'];
            }
            if (!dict.ContainsKey(key))
                dict[key] = new List<string>();
            dict[key].Add(strs[i]);
        }

        return new List<IList<string>>(dict.Values);
    }
}

public class Solution {
    public IList<IList<string>> GroupAnagrams(string[] strs) {
        Dictionary<int, IList<string>> dictionary = new Dictionary<int, IList<string>>();
        int[] primes = {
            2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 41, 43, 47, 53, 59, 61, 67, 71, 73, 79, 83, 89, 97,
            101, 103
        };
        foreach (var str in strs) {
            int key = str.Aggregate(1, (cur, c) => cur * primes[c - 'a']);
            if (dictionary.ContainsKey(key))
                dictionary[key].Add(str);
            else
                dictionary.Add(key, new List<string> {str});
        }
        return dictionary.Values.ToList();
    }
}

public class Solution {
public IList<IList<string>> GroupAnagrams(string[] strs)
{
	var res = new Dictionary<string, IList<string>>();
	if (strs.Length == 0) return res.Values.ToList();

	for (var i = 0; i < strs.Length; i++) 
	{
		var xori = Xor(i, strs[i]);
		if (res.ContainsKey(xori)) res[xori].Add(strs[i]);
		else res.Add(xori, new List<string> {strs[i]});
	}
	
	return res.Values.ToList();
}

public string Xor(int index, string str) 
{
	var xor = new char[26];
	for (var i = 0; i < str.Length; i++)
		xor[str[i] - 'a']++;

	return new string(xor);
}
}

public class Solution {
    public IList<IList<string>> GroupAnagrams(string[] strs) {
            var res = new List<IList<string>>();
            var dict = new Dictionary<string, List<string>>();
            foreach (var str in strs)
            {
                char[] charArr = str.ToCharArray();
                Array.Sort(charArr);
                string sortedStr = new string(charArr);
                if (dict.ContainsKey(sortedStr))
                {
                    dict[sortedStr].Add(str);
                }
                else
                {
                    dict[sortedStr] = new List<string>(){str};
                }
            }

            foreach (var kv in dict)
            {
                res.Add(kv.Value);
            }
            return res;
    }
}

 
*/