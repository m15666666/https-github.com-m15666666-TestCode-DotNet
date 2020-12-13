using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
给定一个非空的整数数组，返回其中出现频率前 k 高的元素。

示例 1:

输入: nums = [1,1,1,2,2,3], k = 2
输出: [1,2]
示例 2:

输入: nums = [1], k = 1
输出: [1]

提示：

你可以假设给定的 k 总是合理的，且 1 ≤ k ≤ 数组中不相同的元素的个数。
你的算法的时间复杂度必须优于 O(n log n) , n 是数组的大小。
题目数据保证答案唯一，换句话说，数组中前 k 个高频元素的集合是唯一的。
你可以按任意顺序返回答案。

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
前 K 个高频元素
力扣官方题解
发布于 2020-09-05
44.6k
方法一：堆
思路与算法

首先遍历整个数组，并使用哈希表记录每个数字出现的次数，并形成一个「出现次数数组」。找出原数组的前 kk 个高频元素，就相当于找出「出现次数数组」的前 kk 大的值。

最简单的做法是给「出现次数数组」排序。但由于可能有 O(N)O(N) 个不同的出现次数（其中 NN 为原数组长度），故总的算法复杂度会达到 O(N\log N)O(NlogN)，不满足题目的要求。

在这里，我们可以利用堆的思想：建立一个小顶堆，然后遍历「出现次数数组」：

如果堆的元素个数小于 kk，就可以直接插入堆中。
如果堆的元素个数等于 kk，则检查堆顶与当前出现次数的大小。如果堆顶更大，说明至少有 kk 个数字的出现次数比当前值大，故舍弃当前值；否则，就弹出堆顶，并将当前值插入堆中。
遍历完成后，堆中的元素就代表了「出现次数数组」中前 kk 大的值。

代码


class Solution {
    public int[] topKFrequent(int[] nums, int k) {
        Map<Integer, Integer> occurrences = new HashMap<Integer, Integer>();
        for (int num : nums) {
            occurrences.put(num, occurrences.getOrDefault(num, 0) + 1);
        }

        // int[] 的第一个元素代表数组的值，第二个元素代表了该值出现的次数
        PriorityQueue<int[]> queue = new PriorityQueue<int[]>(new Comparator<int[]>() {
            public int compare(int[] m, int[] n) {
                return m[1] - n[1];
            }
        });
        for (Map.Entry<Integer, Integer> entry : occurrences.entrySet()) {
            int num = entry.getKey(), count = entry.getValue();
            if (queue.size() == k) {
                if (queue.peek()[1] < count) {
                    queue.poll();
                    queue.offer(new int[]{num, count});
                }
            } else {
                queue.offer(new int[]{num, count});
            }
        }
        int[] ret = new int[k];
        for (int i = 0; i < k; ++i) {
            ret[i] = queue.poll()[0];
        }
        return ret;
    }
}
复杂度分析

时间复杂度：O(N\log k)O(Nlogk)，其中 NN 为数组的长度。我们首先遍历原数组，并使用哈希表记录出现次数，每个元素需要 O(1)O(1) 的时间，共需 O(N)O(N) 的时间。随后，我们遍历「出现次数数组」，由于堆的大小至多为 kk，因此每次堆操作需要 O(\log k)O(logk) 的时间，共需 O(N\log k)O(Nlogk) 的时间。二者之和为 O(N\log k)O(Nlogk)。
空间复杂度：O(N)O(N)。哈希表的大小为 O(N)O(N)，而堆的大小为 O(k)O(k)，共计为 O(N)O(N)。
方法二：基于快速排序
思路与算法

我们可以使用基于快速排序的方法，求出「出现次数数组」的前 kk 大的值。

在对数组 \textit{arr}[l \ldots r]arr[l…r] 做快速排序的过程中，我们首先将数组划分为两个部分 \textit{arr}[i \ldots q-1]arr[i…q−1] 与 \textit{arr}[q+1 \ldots j]arr[q+1…j]，并使得 \textit{arr}[i \ldots q-1]arr[i…q−1] 中的每一个值都不超过 \textit{arr}[q]arr[q]，且 \textit{arr}[q+1 \ldots j]arr[q+1…j] 中的每一个值都大于 \textit{arr}[q]arr[q]。

于是，我们根据 kk 与左侧子数组 \textit{arr}[i \ldots q-1]arr[i…q−1] 的长度（为 q-iq−i）的大小关系：

如果 k \le q-ik≤q−i，则数组 \textit{arr}[l \ldots r]arr[l…r] 前 kk 大的值，就等于子数组 \textit{arr}[i \ldots q-1]arr[i…q−1] 前 kk 大的值。
否则，数组 \textit{arr}[l \ldots r]arr[l…r] 前 kk 大的值，就等于左侧子数组全部元素，加上右侧子数组 \textit{arr}[q+1 \ldots j]arr[q+1…j] 中前 k - (q - i)k−(q−i) 大的值。
原版的快速排序算法的平均时间复杂度为 O(N\log N)O(NlogN)。我们的算法中，每次只需在其中的一个分支递归即可，因此算法的平均时间复杂度降为 O(N)O(N)。

代码


class Solution {
    public int[] topKFrequent(int[] nums, int k) {
        Map<Integer, Integer> occurrences = new HashMap<Integer, Integer>();
        for (int num : nums) {
            occurrences.put(num, occurrences.getOrDefault(num, 0) + 1);
        }

        List<int[]> values = new ArrayList<int[]>();
        for (Map.Entry<Integer, Integer> entry : occurrences.entrySet()) {
            int num = entry.getKey(), count = entry.getValue();
            values.add(new int[]{num, count});
        }
        int[] ret = new int[k];
        qsort(values, 0, values.size() - 1, ret, 0, k);
        return ret;
    }

    public void qsort(List<int[]> values, int start, int end, int[] ret, int retIndex, int k) {
        int picked = (int) (Math.random() * (end - start + 1)) + start;
        Collections.swap(values, picked, start);
        
        int pivot = values.get(start)[1];
        int index = start;
        for (int i = start + 1; i <= end; i++) {
            if (values.get(i)[1] >= pivot) {
                Collections.swap(values, index + 1, i);
                index++;
            }
        }
        Collections.swap(values, start, index);

        if (k <= index - start) {
            qsort(values, start, index - 1, ret, retIndex, k);
        } else {
            for (int i = start; i <= index; i++) {
                ret[retIndex++] = values.get(i)[0];
            }
            if (k > index - start + 1) {
                qsort(values, index + 1, end, ret, retIndex, k - (index - start + 1));
            }
        }
    }
}
复杂度分析

时间复杂度：O(N^2)O(N 
2
 )，其中 NN 为数组的长度。
设处理长度为 NN 的数组的时间复杂度为 f(N)f(N)。由于处理的过程包括一次遍历和一次子分支的递归，最好情况下，有 f(N) = O(N) + f(N/2)f(N)=O(N)+f(N/2)，根据 主定理，能够得到 f(N) = O(N)f(N)=O(N)。
最坏情况下，每次取的中枢数组的元素都位于数组的两端，时间复杂度退化为 O(N^2)O(N 
2
 )。但由于我们在每次递归的开始会先随机选取中枢元素，故出现最坏情况的概率很低。
平均情况下，时间复杂度为 O(N)O(N)。
空间复杂度：O(N)O(N)。哈希表的大小为 O(N)O(N)，用于排序的数组的大小也为 O(N)O(N)，快速排序的空间复杂度最好情况为 O(\log N)O(logN)，最坏情况为 O(N)O(N)。
引申

本题与 215. 数组中的第K个最大元素 具有诸多相似之处。

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
