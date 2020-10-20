using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
给定一位研究者论文被引用次数的数组（被引用次数是非负整数）。编写一个方法，计算出研究者的 h 指数。

h 指数的定义: “h 代表“高引用次数”（high citations），
一名科研人员的 h 指数是指他（她）的 （N 篇论文中）至多有 h 篇论文分别被引用了至少 h 次。
（其余的 N - h 篇论文每篇被引用次数不多于 h 次。）”

 

示例:

输入: citations = [3,0,6,1,5]
输出: 3 
解释: 给定数组表示研究者总共有 5 篇论文，每篇论文相应的被引用了 3, 0, 6, 1, 5 次。
     由于研究者有 3 篇论文每篇至少被引用了 3 次，其余两篇论文每篇被引用不多于 3 次，所以她的 h 指数是 3。
 

说明: 如果 h 有多种可能的值，h 指数是其中最大的那个。
*/
/// <summary>
/// https://leetcode-cn.com/problems/h-index/
/// 274. H指数
/// https://blog.csdn.net/weixin_42326299/article/details/82659842
/// http://www.cnblogs.com/lightwindy/p/8655160.html
/// </summary>
class H_IndexSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int HIndex(int[] citations)
    {
        if (citations == null || citations.Length == 0) return 0;

        Array.Sort(citations);
        var length = citations.Length;
        for ( int index = 0; index < length; index++)
        {
            int hCount = length - index;
            if( hCount <= citations[index] ) return hCount;
        }

        return 0;
    }
}
/*
H 指数
力扣 (LeetCode)
发布于 2019-06-24
12.0k
方法一：排序
分析

我们想象一个直方图，其中 xx 轴表示文章，yy 轴表示每篇文章的引用次数。如果将这些文章按照引用次数降序排序并在直方图上进行表示，那么直方图上的最大的正方形的边长 hh 就是我们所要求的 hh。

h-index

算法

首先我们将引用次数降序排序，在排完序的数组 \mathrm{citations}citations 中，如果 \mathrm{citations}[i] \gt icitations[i]>i，那么说明第 0 到 ii 篇论文都有至少 i+1i+1 次引用。因此我们只要找到最大的 ii 满足 \mathrm{citations}[i] \gt icitations[i]>i，那么 hh 指数即为 i+1i+1。例如：

ii	0	1	2	3	4	5	6
引用次数	10	9	5	3	3	2	1
\mathrm{citations}[i] \gt icitations[i]>i	true	true	true	false	false	false	false
其中最大的满足 \mathrm{citations}[i] \gt icitations[i]>i 的 ii 值为 2，因此 h = i + 1 = 3h=i+1=3。

找到最大的 ii 的方法有很多，可以对数组进行线性扫描，也可以使用二分查找。由于排序的时间复杂度已经为 O(n \log n)O(nlogn)，因此无论是线性扫描 O(n)O(n) 还是二分查找 O(\log n)O(logn)，都不会改变算法的总复杂度。


public class Solution {
    public int hIndex(int[] citations) {
        // 排序（注意这里是升序排序，因此下面需要倒序扫描）
        Arrays.sort(citations);
        // 线性扫描找出最大的 i
        int i = 0;
        while (i < citations.length && citations[citations.length - 1 - i] > i) {
            i++;
        }
        return i;
    }
}
复杂度分析

时间复杂度：O(n\log n)O(nlogn)，即为排序的时间复杂度。
空间复杂度：O(1)O(1)。大部分语言的内置 sort 函数使用堆排序，它只需要 O(1)O(1) 的额外空间。
方法二：计数
分析

基于比较的排序算法存在时间复杂度下界 O(n\log n)O(nlogn)，如果要得到时间复杂度更低的算法，就必须考虑不基于比较的排序。

算法

方法一中，我们通过降序排序得到了 hh 指数，然而，所有基于比较的排序算法，例如堆排序，合并排序和快速排序，都存在时间复杂度下界 O(n\log n)O(nlogn)。要得到时间复杂度更低的算法. 可以考虑最常用的不基于比较的排序，计数排序。

然而，论文的引用次数可能会非常多，这个数值很可能会超过论文的总数 nn，因此使用计数排序是非常不合算的（会超出空间限制）。在这道题中，我们可以通过一个不难发现的结论来让计数排序变得有用，即：

如果一篇文章的引用次数超过论文的总数 nn，那么将它的引用次数降低为 nn 也不会改变 hh 指数的值。

由于 hh 指数一定小于等于 nn，因此这样做是正确的。在直方图中，将所有超过 yy 轴值大于 nn 的变为 nn 等价于去掉 y>ny>n 的整个区域。

h-index cut off

从直方图中可以更明显地看出结论的正确性，将 y>ny>n 的区域去除，并不会影响到最大的正方形，也就不会影响到 hh 指数。

我们用一个例子来说明如何使用计数排序得到 hh 指数。首先，引用次数如下所示：

\mathrm{citations} = [1, 3, 2, 3, 100]
citations=[1,3,2,3,100]

将所有大于 n=5n=5 的引用次数变为 nn，得到：

\mathrm{citations} = [1, 3, 2, 3, 5]
citations=[1,3,2,3,5]

计数排序得到的结果如下：

kk	0	1	2	3	4	5
count	0	1	1	2	0	1
s_ks 
k
​	
 	5	5	4	3	1	1
其中 s_ks 
k
​	
  表示至少有 kk 次引用的论文数量，在表中即为在它之后的列（包括本身）的 \mathrm{count}count 一行的和。根据定义，最大的满足 k \leq s_kk≤s 
k
​	
  的 kk 即为所求的 hh。在表中，这个 kk 为 3，因此 hh 指数为 3。


public class Solution {
    public int hIndex(int[] citations) {
        int n = citations.length;
        int[] papers = new int[n + 1];
        // 计数
        for (int c: citations)
            papers[Math.min(n, c)]++;
        // 找出最大的 k
        int k = n;
        for (int s = papers[n]; k > s; s += papers[k])
            k--;
        return k;
    }
}
复杂度分析

时间复杂度：O(n)O(n)。在计数时，我们仅需要遍历 \mathrm{citations}citations 数组一次，因此时间复杂度为 O(n)O(n)。在找出最大的 k 时，我们最多需要遍历计数的数组一次，而计数的数组的长度为 O(n)O(n)，因此这一步的时间复杂度为 O(n)O(n)，即总的时间复杂度为 O(n)O(n)。
空间复杂度：O(n)O(n)。我们需要使用 O(n)O(n) 的空间来存放计数的结果。
思考
可能会出现多个不同的 hh 指数吗？

答案是 否。从直方图中可以看出，由于 yy 轴已经降序排序，因此直线 y=xy=x 有且仅有穿过直方图一次。同时，也可以直接通过 hh 指数的定义证明出 hh 指数的唯一性。

public class Solution {
    public int HIndex(int[] citations) {
        if(citations.Length==0)
            return 0;
         Array.Sort(citations);
            for (var i = 0; i < citations.Length; i++) {
                if (citations.Length - i <= citations[i])
                    return citations.Length - i;
            }
            return 0;
    }
}
public class Solution {
    public int HIndex(int[] citations) {
        Array.Sort(citations, (a, b) => -a.CompareTo(b));
        for (int i = 0; i < citations.Length; ++i)
        {
            if(citations[i] < i + 1) return i;
            if(citations[i] == i + 1) return i + 1;
        }
        return citations.Length;
    }
}    
     
     
*/
