using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个区间的集合，找到需要移除区间的最小数量，使剩余区间互不重叠。

注意:

可以认为区间的终点总是大于它的起点。
区间 [1,2] 和 [2,3] 的边界相互“接触”，但没有相互重叠。
示例 1:

输入: [ [1,2], [2,3], [3,4], [1,3] ]

输出: 1

解释: 移除 [1,3] 后，剩下的区间没有重叠。
示例 2:

输入: [ [1,2], [1,2], [1,2] ]

输出: 2

解释: 你需要移除两个 [1,2] 来使剩下的区间没有重叠。
示例 3:

输入: [ [1,2], [2,3] ]

输出: 0

解释: 你不需要移除任何区间，因为它们已经是无重叠的了。 
*/
/// <summary>
/// https://leetcode-cn.com/problems/non-overlapping-intervals/
/// 435. 无重叠区间
/// https://blog.csdn.net/qq_36946274/article/details/81591172
/// 
/// </summary>
class NonOverlappingIntervalsSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int EraseOverlapIntervals(int[][] intervals)
    {
        if (intervals == null || intervals.Length < 2) return 0;

        const int EndIndex = 1;
        const int StartIndex = 0;
        Comparison<int[]> action = (x, y) => { return x[EndIndex].CompareTo(y[EndIndex]); };
        Array.Sort(intervals, action);

        int length = intervals.Length;
        int count = 1;
        int last = 0;
        for (int i = 1; i < length; i++)
        {
            if (intervals[last][EndIndex] <= intervals[i][StartIndex])
            {
                count++;
                last = i;
            }
        }
        return length - count;
    }
}
/*
无重叠区间
力扣官方题解
发布于 2020-12-30
22.7k
方法一：动态规划
思路与算法

题目的要求等价于「选出最多数量的区间，使得它们互不重叠」。由于选出的区间互不重叠，因此我们可以将它们按照端点从小到大的顺序进行排序，并且无论我们按照左端点还是右端点进行排序，得到的结果都是唯一的。

这样一来，我们可以先将所有的 nn 个区间按照左端点（或者右端点）从小到大进行排序，随后使用动态规划的方法求出区间数量的最大值。设排完序后这 nn 个区间的左右端点分别为 l_0, \cdots, l_{n-1}l 
0
​	
 ,⋯,l 
n−1
​	
  以及 r_0, \cdots, r_{n-1}r 
0
​	
 ,⋯,r 
n−1
​	
 ，那么我们令 f_if 
i
​	
  表示「以区间 ii 为最后一个区间，可以选出的区间数量的最大值」，状态转移方程即为：

f_i = \max_{j < i ~\wedge~ r_j \leq l_i} \{ f_j \} + 1
f 
i
​	
 = 
j<i ∧ r 
j
​	
 ≤l 
i
​	
 
max
​	
 {f 
j
​	
 }+1

即我们枚举倒数第二个区间的编号 jj，满足 j < ij<i，并且第 jj 个区间必须要与第 ii 个区间不重叠。由于我们已经按照左端点进行升序排序了，因此只要第 jj 个区间的右端点 r_jr 
j
​	
  没有越过第 ii 个区间的左端点 l_il 
i
​	
 ，即 r_j \leq l_ir 
j
​	
 ≤l 
i
​	
 ，那么第 jj 个区间就与第 ii 个区间不重叠。我们在所有满足要求的 jj 中，选择 f_jf 
j
​	
  最大的那一个进行状态转移，如果找不到满足要求的区间，那么状态转移方程中 \minmin 这一项就为 00，f_if 
i
​	
  就为 11。

最终的答案即为所有 f_if 
i
​	
  中的最大值。

代码

由于方法一的时间复杂度较高，因此在下面的 \texttt{Python}Python 代码中，我们尽量使用列表推导优化常数，使得其可以在时间限制内通过所有测试数据。


class Solution {
    public int eraseOverlapIntervals(int[][] intervals) {
        if (intervals.length == 0) {
            return 0;
        }
        
        Arrays.sort(intervals, new Comparator<int[]>() {
            public int compare(int[] interval1, int[] interval2) {
                return interval1[0] - interval2[0];
            }
        });

        int n = intervals.length;
        int[] f = new int[n];
        Arrays.fill(f, 1);
        for (int i = 1; i < n; ++i) {
            for (int j = 0; j < i; ++j) {
                if (intervals[j][1] <= intervals[i][0]) {
                    f[i] = Math.max(f[i], f[j] + 1);
                }
            }
        }
        return n - Arrays.stream(f).max().getAsInt();
    }
}
复杂度分析

时间复杂度：O(n^2)O(n 
2
 )，其中 nn 是区间的数量。我们需要 O(n \log n)O(nlogn) 的时间对所有的区间按照左端点进行升序排序，并且需要 O(n^2)O(n 
2
 ) 的时间进行动态规划。由于前者在渐进意义下小于后者，因此总时间复杂度为 O(n^2)O(n 
2
 )。

注意到方法一本质上是一个「最长上升子序列」问题，因此我们可以将时间复杂度优化至 O(n \log n)O(nlogn)，具体可以参考「300. 最长递增子序列的官方题解」。

空间复杂度：O(n)O(n)，即为存储所有状态 f_if 
i
​	
  需要的空间。

方法二：贪心
思路与算法

我们不妨想一想应该选择哪一个区间作为首个区间。

假设在某一种最优的选择方法中，[l_k, r_k][l 
k
​	
 ,r 
k
​	
 ] 是首个（即最左侧的）区间，那么它的左侧没有其它区间，右侧有若干个不重叠的区间。设想一下，如果此时存在一个区间 [l_j, r_j][l 
j
​	
 ,r 
j
​	
 ]，使得 r_j < r_kr 
j
​	
 <r 
k
​	
 ，即区间 jj 的右端点在区间 kk 的左侧，那么我们将区间 kk 替换为区间 jj，其与剩余右侧被选择的区间仍然是不重叠的。而当我们将区间 kk 替换为区间 jj 后，就得到了另一种最优的选择方法。

我们可以不断地寻找右端点在首个区间右端点左侧的新区间，将首个区间替换成该区间。那么当我们无法替换时，首个区间就是所有可以选择的区间中右端点最小的那个区间。因此我们将所有区间按照右端点从小到大进行排序，那么排完序之后的首个区间，就是我们选择的首个区间。

如果有多个区间的右端点都同样最小怎么办？由于我们选择的是首个区间，因此在左侧不会有其它的区间，那么左端点在何处是不重要的，我们只要任意选择一个右端点最小的区间即可。

当确定了首个区间之后，所有与首个区间不重合的区间就组成了一个规模更小的子问题。由于我们已经在初始时将所有区间按照右端点排好序了，因此对于这个子问题，我们无需再次进行排序，只要找出其中与首个区间不重合并且右端点最小的区间即可。用相同的方法，我们可以依次确定后续的所有区间。

在实际的代码编写中，我们对按照右端点排好序的区间进行遍历，并且实时维护上一个选择区间的右端点 \textit{right}right。如果当前遍历到的区间 [l_i, r_i][l 
i
​	
 ,r 
i
​	
 ] 与上一个区间不重合，即 l_i \geq \textit{right}l 
i
​	
 ≥right，那么我们就可以贪心地选择这个区间，并将 \textit{right}right 更新为 r_ir 
i
​	
 。

代码


class Solution {
    public int eraseOverlapIntervals(int[][] intervals) {
        if (intervals.length == 0) {
            return 0;
        }
        
        Arrays.sort(intervals, new Comparator<int[]>() {
            public int compare(int[] interval1, int[] interval2) {
                return interval1[1] - interval2[1];
            }
        });

        int n = intervals.length;
        int right = intervals[0][1];
        int ans = 1;
        for (int i = 1; i < n; ++i) {
            if (intervals[i][0] >= right) {
                ++ans;
                right = intervals[i][1];
            }
        }
        return n - ans;
    }
}
复杂度分析

时间复杂度：O(n \log n)O(nlogn)，其中 nn 是区间的数量。我们需要 O(n \log n)O(nlogn) 的时间对所有的区间按照右端点进行升序排序，并且需要 O(n)O(n) 的时间进行遍历。由于前者在渐进意义下大于后者，因此总时间复杂度为 O(n \log n)O(nlogn)。

空间复杂度：O(\log n)O(logn)，即为排序需要使用的栈空间。
 
public class Solution {
    public int EraseOverlapIntervals(int[][] intervals) {
        if(intervals==null||intervals.Length==0) return 0;
        Array.Sort(intervals,(x,y)=>{
            //按照end正序排序
            return x[1].CompareTo(y[1]);         
        });
        int end=intervals[0][1];
        int res=0;
        //end排序，正向遍历
        for(int i=1;i<intervals.Length;i++)
        {
            if(intervals[i][0]<end)
                res++;
            else
                end=intervals[i][1];
        }
        return res;
    }
    public int EraseOverlapIntervals222(int[][] intervals) {
        if(intervals==null||intervals.Length==0) return 0;
        Array.Sort(intervals,(x,y)=>{
            //按照start正序排序
            return x[0].CompareTo(y[0]);         
        });
        int len=intervals.Length;
        int start=intervals[len-1][0];
        int res=0;
        //start排序，反向遍历
        for(int i=len-2;i>=0;i--)
        {
            if(intervals[i][1]>start)
                res++;
            else
                start=intervals[i][0];
        }
        return res;
    }
}
*/