using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个字符串，请将字符串里的字符按照出现的频率降序排列。

示例 1:

输入:
"tree"

输出:
"eert"

解释:
'e'出现两次，'r'和't'都只出现一次。
因此'e'必须出现在'r'和't'之前。此外，"eetr"也是一个有效的答案。
示例 2:

输入:
"cccaaa"

输出:
"cccaaa"

解释:
'c'和'a'都出现三次。此外，"aaaccc"也是有效的答案。
注意"cacaca"是不正确的，因为相同的字母必须放在一起。
示例 3:

输入:
"Aabb"

输出:
"bbAa"

解释:
此外，"bbaA"也是一个有效的答案，但"Aabb"是不正确的。
注意'A'和'a'被认为是两种不同的字符。 
*/
/// <summary>
/// https://leetcode-cn.com/problems/sort-characters-by-frequency/
/// 451. 根据字符出现频率排序
/// </summary>
class SortCharactersByFrequencySolution
{
    public void Test()
    {
        var ret = FrequencySort("tree");
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public string FrequencySort(string s)
    {
        if (string.IsNullOrWhiteSpace(s)) return s;

        const int length = 256;
        int[] counts = new int[length];
        int[] indexs = new int[length];
        Array.Fill(counts, 0);
        for(int i = 0; i < indexs.Length; i++)
        {
            indexs[i] = i;
        }

        foreach( var c in s)
        {
            counts[(int)c]++;
        }

        Array.Sort(counts, indexs);

        StringBuilder ret = new StringBuilder();
        for( int i = indexs.Length - 1; -1 < i; i--)
        {
            var index = indexs[i];
            var count = counts[i];
            if (count == 0) break;
            
            char c = (char)(index);
            ret.Append(c, count);
        }

        return ret.ToString();
    }
}
/*
public class Solution {
    public string FrequencySort(string s)
    {
        int[] cache = new int[200];
        int length = s.Length;
        StringBuilder result = new StringBuilder();
        for (int i = 0; i < length; i++)
        {
            cache[s[i] - ' '] = cache[s[i] - ' '] + 1;
        }
        int index = MaxIndex(cache);
        while (index != -1)
        {
            int numlength = cache[index];
            for (int i = 0; i < numlength; i++)
            {
                result.Append(Convert.ToChar(' ' + index));
            }
            cache[index] = 0;
            index = MaxIndex(cache);
        }
        return result.ToString();
    }

    public int MaxIndex(int[] cache)
    {
        int length = cache.Length;
        int index = 0;
        int val = 0;
        for (int i = 0; i < length; i++)
        {
            if (cache[i] > val)
            {
                val = cache[i];
                index = i;
            }
        }
        if (val > 0) return index;
        else return -1;
    }
}
public class Solution {
    public struct ST
    {
        public int val;
        public int cnt;
    }
    public string FrequencySort(string s) {
        ST []cnt = new ST[200];
        
        for(int i=0;i<s.Length;i++)
        {
            if(cnt[(int)s[i]].val==0)cnt[(int)s[i]].val =(int)s[i];
            
            cnt[(int)s[i]].cnt++;
        }
        Array.Sort(cnt,(a, b) =>
            {
                if (a.cnt < b.cnt) return 1;
                else if (a.cnt > b.cnt) return -1;
                else return 0;

            });
        
        StringBuilder sb=new StringBuilder();
        for(int i=0;i<cnt.Length;i++)
        {
            for(int j=0;j<cnt[i].cnt;j++)
            {
                sb.Append((char)cnt[i].val);
            }            
        }
        
        return sb.ToString();
    }
}
public class Solution {
    public string FrequencySort(string s) {
        var dict=new Dictionary<char,int>();
        for(int i=0;i<s.Length;i++){
            if(dict.ContainsKey(s[i])){
                dict[s[i]]++;
            }else{
                dict[s[i]]=1;
            }
        }
        
        var builder=new StringBuilder();
        foreach(var kv in dict.OrderByDescending(kv=>kv.Value)){            
            builder.Append(new String(kv.Key,kv.Value));
        }
        return builder.ToString();
    }
}
public class Solution
{
	public string FrequencySort(string s)
	{
		var dic = new Dictionary<char, int>();
		foreach (var ch in s)
		{
			dic.TryGetValue(ch, out var n);
			dic[ch] = ++n;
		}
		return dic
			.OrderByDescending(e => e.Value)
			// 预指定大小, 减少resize
			// c.Append(char, int repeatCount) / new string(char, int repeatCount)
			.Aggregate(new StringBuilder(s.Length), (c, i) => c.Append(i.Key, i.Value))
			.ToString();
	}
}
public class Solution {
    public string FrequencySort(string s)
    {
        Dictionary<char, int> mDicts = new Dictionary<char, int>();
        foreach(char t in s)
        {
            if(!mDicts.ContainsKey(t))
            {
                mDicts.Add(t, 1);
            }
            else
            {
                mDicts[t] += 1;
            }
        }
        //对字典进行降序排序
        List<KeyValuePair<char, int>> mLists = new List<KeyValuePair<char, int>>(mDicts);
        mLists.Sort((x, y) => { return -x.Value.CompareTo(y.Value); });
        //这里使用string会超时
        StringBuilder ans = new StringBuilder() ;
        foreach(var t in mLists)
        {
           for(int i = 0; i < t.Value; i++)
                ans.Append(t.Key);
        }
        return ans.ToString();
    }
}

*/
