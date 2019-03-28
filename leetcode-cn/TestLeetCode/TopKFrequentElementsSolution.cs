using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
示例 1:

输入: nums = [1,1,1,2,2,3], k = 2
输出: [1,2]
示例 2:

输入: nums = [1], k = 1
输出: [1]
说明：

你可以假设给定的 k 总是合理的，且 1 ≤ k ≤ 数组中不相同的元素的个数。
你的算法的时间复杂度必须优于 O(n log n) , n 是数组的大小。 
*/
/// <summary>
/// https://leetcode-cn.com/problems/top-k-frequent-elements/
/// 347. 前K个高频元素
/// </summary>
class TopKFrequentElementsSolution
{
    public void Test()
    {
        var ret = TopKFrequent(new int[] { 3, 2, 3, 1, 2, 4, 5, 5, 6, 7, 7, 8, 2, 3, 1, 1, 1, 10, 11, 5, 6, 2, 4, 7, 8, 5, 6 }, 10);
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public IList<int> TopKFrequent(int[] nums, int k)
    {
        if (nums == null) return new int[0];
        Dictionary<int, int> num2Index = new Dictionary<int, int>(nums.Length);

        int[] index2Count = new int[nums.Length];

        int index = -1;
        foreach (var n in nums)
        {
            index++;

            if ( num2Index.ContainsKey(n) ) { index2Count[num2Index[n]]++; }
            else { num2Index.Add(n,index); index2Count[index] = 1; }
        } // foreach (var n in nums)

        return num2Index.OrderByDescending(item => index2Count[item.Value]).Take(k).Select(item => item.Key).ToList();
    }
}
/*
public class Solution {
    public IList<int> TopKFrequent(int[] nums, int k) {
        var result = new List<int>();
        if(nums == null || nums.Length == 0 || k < 1)
            return result;
        
        var maxCount = 1;
        var map = new Dictionary<int, int>();
        foreach(var num in nums)
        {
            if(map.ContainsKey(num))
            {
                map[num]++;
                if(map[num] > maxCount)
                    maxCount = map[num];
            }
            else
            {
                map.Add(num, 1);
            }
        }
        
        var listByCount = new List<int>[maxCount+1];
        foreach(var entry in map)
        {
            if(listByCount[entry.Value] == null)
                listByCount[entry.Value] = new List<int>();
            listByCount[entry.Value].Add(entry.Key);
        }
        
        var index = maxCount;
        while(result.Count < k)
        {
            if(listByCount[index] != null)
                result.AddRange(listByCount[index]);
            index--;
        }
        return result;
    }
}
public class Solution {
    public IList<int> TopKFrequent(int[] nums, int k) {
        IList<int> resList = new List<int>();
        if(nums == null || nums.Length < 1)
            return resList;
        Dictionary<int, int> wordFreqDict = new Dictionary<int, int>();
        for (int i = 0; i < nums.Length; i++) {
            if (!wordFreqDict.ContainsKey(nums[i])) {
                wordFreqDict[nums[i]] = 1;
            }else {
                wordFreqDict[nums[i]] += 1;
            }
        }
        Dictionary<int, IList<int>> freqWordDict = new Dictionary<int, IList<int>>();
        IList<int> tmpList = new List<int>();
        foreach (KeyValuePair<int, int> kvp in wordFreqDict) {
            if (!freqWordDict.ContainsKey(kvp.Value)) {
                freqWordDict[kvp.Value] = new List<int>();
            }
            freqWordDict[kvp.Value].Add(kvp.Key);
        }
        int preMax = 0;
        int tmpMax = 0;
        for(int i = 0; i < k && i < freqWordDict.Count; i++){
            foreach (int freq in freqWordDict.Keys) {
                if (freq > tmpMax) {
                    if (i == 0) {
                        tmpMax = freq;
                    }else if (freq < preMax) {
                        tmpMax = freq;
                    }
                }
            }
            
            for (int j = 0; j < freqWordDict[tmpMax].Count; j++) {
                resList.Add(freqWordDict[tmpMax][j]);
                if (resList.Count == k) {
                    return resList;
                }
            }
            
            preMax = tmpMax;
			tmpMax = 0;
        }
        return resList;
    }
}
public class Solution {
        public IList<int> TopKFrequent(int[] nums, int k) {
 List<int> list = new List<int>();
            Dictionary<int, int> dic = new Dictionary<int, int>();
            int s = 0;
            for(int i=0;i<nums.Length;i++)
            {
                if(!dic.ContainsKey(nums[i]))
                {
                    dic.Add(nums[i], 1);
                }
                else
                {
                    s = dic[nums[i]];
                    s++;
                    dic[nums[i]] = s;
                }
            }

            Dictionary<int, int> dic1_SortedByValue = dic.OrderByDescending(p => p.Value).ToDictionary(o => o.Key, p => p.Value);
            
            foreach(var di in dic1_SortedByValue )
            {
                if(k==0)
                {
                    break;
                }
                list.Add(di.Key);
                k--;
                
            }
            return list;
        }
}
public class Solution {
    public IList<int> TopKFrequent(int[] nums, int k) {
        Dictionary<int, int> data = new Dictionary<int, int>();
            for (int i = 0; i < nums.Length; i++)
            {
                if (!data.ContainsKey(nums[i]))
                {
                    data.Add(nums[i], 1);
                }
                else
                {
                    data[nums[i]] += 1;
                }
            }
            var dicsort = from obj in data
                          orderby obj.Value descending
                          select obj;
            IList<int> result = dicsort.Take(k).Select(p => p.Key).ToArray();
            return result;
    }
}
public class Solution {
    public IList<int> TopKFrequent(int[] nums, int k) 
    {
        Dictionary<int,int> dict = new Dictionary<int,int>();
        foreach(int i in nums)
        {
            if(!dict.ContainsKey(i))
            {
              dict.Add(i,1);
            }
            else
            {
                dict[i] += 1;
            }
        }
        List<int> Ilist = new List<int>();
                for (int j = 0; j < k; j++)
                {
                    int key = (from d in dict orderby d.Value descending select d.Key).First();
                    Ilist.Add(key);
                    dict.Remove(key);
                }
                return Ilist;
    }
}
*/
