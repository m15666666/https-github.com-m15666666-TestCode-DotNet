using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一组区间，对于每一个区间 i，检查是否存在一个区间 j，它的起始点大于或等于区间 i 的终点，这可以称为 j 在 i 的“右侧”。

对于任何区间，你需要存储的满足条件的区间 j 的最小索引，这意味着区间 j 有最小的起始点可以使其成为“右侧”区间。如果区间 j 不存在，则将区间 i 存储为 -1。最后，你需要输出一个值为存储的区间值的数组。

注意:

你可以假设区间的终点总是大于它的起始点。
你可以假定这些区间都不具有相同的起始点。
示例 1:

输入: [ [1,2] ]
输出: [-1]

解释:集合中只有一个区间，所以输出-1。
示例 2:

输入: [ [3,4], [2,3], [1,2] ]
输出: [-1, 0, 1]

解释:对于[3,4]，没有满足条件的“右侧”区间。
对于[2,3]，区间[3,4]具有最小的“右”起点;
对于[1,2]，区间[2,3]具有最小的“右”起点。
示例 3:

输入: [ [1,4], [2,3], [3,4] ]
输出: [-1, 2, -1]

解释:对于区间[1,4]和[3,4]，没有满足条件的“右侧”区间。
对于[2,3]，区间[3,4]有最小的“右”起点。 
*/
/// <summary>
/// https://leetcode-cn.com/problems/find-right-interval/
/// 436. 寻找右区间
/// https://blog.csdn.net/lv1224/article/details/81751189
/// </summary>
class FindRightIntervalSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int[] FindRightInterval(int[][] intervals)
    {
        if (intervals == null || intervals.Length == 0) return new int[0];

        int n = intervals.Length;
        int[] starts = new int[n];
        int[] ret = new int[n];

        Array.Fill(starts, 0);
        Array.Fill(ret, -1);

        Dictionary<int, int> start2Index = new Dictionary<int, int>();
        for( int i = 0; i < n; i++)
        {
            var start = intervals[i][0];
            starts[i] = start;
            start2Index[start] = i;
        }

        Array.Sort(starts);
        for( int i = 0;i < n; i++)
        {
            var end = intervals[i][1];
            ret[i] = Find(start2Index, starts, end);
        }
        return ret;
    }

    private int Find(Dictionary<int, int> start2Index, int[] starts, int end)
    {
        int left = 0;
        int right = starts.Length - 1;

        if (end < starts[left] || starts[right] < end) return -1;

        while( left < right)
        {
            var mid = (left + right) / 2;
            var v = starts[mid];
            if (end <= v) right = mid;
            else left = mid + 1;
        }
        return start2Index[starts[right]];
    }


}
/*
寻找右区间
力扣 (LeetCode)

发布于 2019-07-27
5.0k
方法一：暴力法
算法：

最简单的解决方案是对于集合中的每个区间，我们扫描所有区间找到其起点大于当前区间的终点的区间（具有最先差值）。在扫描的过程中，我们跟踪满足给定条件区间的索引。每个区间的结果存储在 res 数组中。


class Solution {
    public int[] findRightInterval(int[][] intervals) {
        int[] res = new int[intervals.length];
        for (int i = 0; i < intervals.length; i++) {
            int min = Integer.MAX_VALUE;
            int minindex = -1;
            for (int j = 0; j < intervals.length; j++) {
                if (intervals[j][0] >= intervals[i][1] && intervals[j][0] < min) {
                    min = intervals[j][0];
                    minindex = j;
                }
            }
            res[i] = minindex;
        }
        return res;
    }
}
复杂度分析

时间复杂度：\mathcal{O}(n^2)O(n 
2
 )。找到每个区间的答案需要扫描整个区间集合。
空间复杂度：\mathcal{O}(n)O(n)，数组 \text{res}res 具有 nn 个元素。
方法二：排序 + 扫描
算法：

我们使用一个哈希表 \text{hash}hash，存储的数据形式的键值对。在这里，\text{Key}Key 对应区间，而 \text{Value}Value 对应在 \text{intervals}intervals 数组中特定区间的索引。我们将 \text{intervals}intervals 中的每个元素存储在哈希表中。

我们根据区间的起点对 \text{intervals}intervals 数组进行排序。我们需要将数组的索引存储在哈希表中，以便排序后也能获得对应的索引。

然后，依次遍历数组中的区间，并找到在该区间结束位置后的一个区间。怎么找？由于 \text{intervals}intervals 数组是基于起点排序的，并且对于给定的区间，结束点总是大于起始点。因此我们只需要使用索引 jj 搜索区间，i+1< j < ni+1<j<n，这样按升序扫描时遇到第一个区间就是所需的结果。

然后，我们可以在哈希表中获取该区间对应的索引，将该索引存储到 resres 数组中。


class Solution {
    public int[] findRightInterval(int[][] intervals) {
        int[] res = new int[intervals.length];
        Map<int[], Integer> hash = new HashMap<>();
        for (int i = 0; i < intervals.length; i++) {
            hash.put(intervals[i], i);
        }
        Arrays.sort(intervals, (a, b) -> a[0] - b[0]);
        for (int i = 0; i < intervals.length; i++) {
            int min = Integer.MAX_VALUE;
            int minindex = -1;
            for (int j = i + 1; j < intervals.length; j++) {
                if (intervals[j][0] >= intervals[i][1] && intervals[j][0] < min) {
                    min = intervals[j][0];
                    minindex = hash.get(intervals[j]);
                }
            }
            res[hash.get(intervals[i])] = minindex;
        }
        return res;
    }
}
复杂度分析

时间复杂度：O(n^2)O(n 
2
 )。
排序使用了 O\big(nlog(n)\big)O(nlog(n)) 的时间。
对于第一个区间，我们需要在 n-1n−1 个元素中搜索。
对于第二个元素，我们需要在 n-2n−2 个元素中搜索，等等总共是 (n-1) + (n-2) + ... + 1 = \frac{n.(n-1)}{2} = O(n^2)(n−1)+(n−2)+...+1= 
2
n.(n−1)
​	
 =O(n 
2
 )。
空间复杂度：O(n)O(n)，\text{res}res 和 \text{hash}hash 均存储了 nn 个元素。
方法三：排序 + 二分查找
算法：

我们可以在一定程度上优化上述方法，因为 \text{intervals}intervals 有序，则我们不必用线性的方法来搜索所需的区间，而是可以利用二分查找来找到我们所需的区间。

如果找到所需的区间，则从哈希表中获取对用的所有添加到 \text{res}res 中，反之则添加 \text{-1}-1。

复杂度分析

时间复杂度：O\big((n.log(n)\big)O((n.log(n))。排序花费了 O\big(n.log(n)\big)O(n.log(n)) 的时间，二分查找花费了 O\big(log(n)\big)O(log(n)) 的时间。
空间复杂度：O(n)O(n)，\text{res}res 和 \text{hash}hash 均存储了 nn 个元素。
方法四：使用 TreeMap
算法：

在该方法中，我们不使用 hashmap，而是使用 TreeMap，底层是由红黑树（一种平衡的二叉搜索树）实现的。TreeMap 以 \text{(Key, Value)}(Key, Value) 的形式存储数据，并始终根据键值排序。这样我们将数组中的区间存储到 TreeMap 中，这样就可以获得排序的序列。

现在，我们遍历 \text{intervals}intervals 中的每个区间，并使用函数 TreeMap.ceilingEntry(end_point)，若 \text{Key}Key 刚刚好大于所选区间 \text{end\_point}end_point，则 返回 Key。反之，返回 null。

如果是非空值返回，则我们从 \text{(Key, Value)}(Key, Value) 对中获得 \text{Value}Value。然后添加到 resres 数组中。反之添加 \text{-1}-1 到 resres 数组中。

复杂度分析

时间复杂度：O\big(N \cdot \log{N}\big)O(N⋅logN)。TreeMap 的插入操作需要 O\big(\log{N}\big)O(logN) 的时间。TreeMap 的 ceilingEntry 操作需要 O\big(\log{N}\big)O(logN) 的时间。
空间复杂度：O(n)O(n)，\text{res}res 和 \text{hash}hash 均存储了 nn 个元素。
方法五：使用两个数组
算法：

我们保持两个数组：

\text{intervals}intervals，基于起始点排序。
\text{endIntervals}endIntervals，基于结束点排序
我们从 \text{endIntervals}endIntervals 数组中取出 i^{th}i 
th
  个区间，就可以从左到右扫描 \text{intervals}intervals 数组中的区间来找到满足右区间条件的区间。因为 \text{intervals}intervals 是基于起始点排序的。比如，从 \text{intervals}intervals 数组中选择的区间索引是 jj。

现在，当我们从 \text{endIntervals}endIntervals 数组中获取下一个区间时（即 (i+1)^{th}(i+1) 
th
  个区间），我们不需要从第一个索引开始扫描 \text{intervals}intervals 数组。相反，我们可以直接从 j^{th}j 
th
  索引开始，上一次搜索在 \text{intervals}intervals 数组中停止在这个索引。

我们还是用了 hashmap \text{hash}hash 保留了最初的区间和索引对应关系。

我们通过看图来了解该算法是如何工作的：



复杂度分析

时间复杂度：O\big(N \cdot \log{N}\big)O(N⋅logN)。排序花费了 O\big(N \cdot \log{N}\big)O(N⋅logN) 的时间。
空间复杂度：O(n)O(n)，\text{intervals}intervals，\text{endIntervals}endIntervals，\text{res}res 和 \text{hash}hash 均存储了 nn 个元素。

public class Solution {
     public int[] FindRightInterval(int[][] intervals)
     {
         var n = intervals.Length;
         var ans = new int[n];
         var starts = intervals.Select((x, i) => (value: x[0], index: i))
                 .OrderBy(x => x.value).ToList();
         var ends = intervals.Select((x, i) => (value: x[1], index: i))
                 .OrderBy(x => x.value).ToList();

         for(int i = 0, j = 0; i < n; ++i)
         {
             while (j < n && starts[j].value < ends[i].value)
                 ++j;

             if (j == n)
                 ans[ends[i].index] = -1;
             else
                 ans[ends[i].index] = starts[j].index;
         }

         return ans;
     }
} 
*/
