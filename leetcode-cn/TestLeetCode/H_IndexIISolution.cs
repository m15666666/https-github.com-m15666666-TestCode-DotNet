/*
给定一位研究者论文被引用次数的数组（被引用次数是非负整数），数组已经按照升序排列。编写一个方法，计算出研究者的 h 指数。

h 指数的定义: “h 代表“高引用次数”（high citations），一名科研人员的 h 指数是指他（她）的 （N 篇论文中）至多有 h 篇论文分别被引用了至少 h 次。（其余的 N - h 篇论文每篇被引用次数不多于 h 次。）"

示例:

输入: citations = [0,1,3,5,6]
输出: 3
解释: 给定数组表示研究者总共有 5 篇论文，每篇论文相应的被引用了 0, 1, 3, 5, 6 次。
     由于研究者有 3 篇论文每篇至少被引用了 3 次，其余两篇论文每篇被引用不多于 3 次，所以她的 h 指数是 3。

说明:

如果 h 有多有种可能的值 ，h 指数是其中最大的那个。

进阶：

这是 H指数 的延伸题目，本题中的 citations 数组是保证有序的。
你可以优化你的算法到对数时间复杂度吗？

*/

/// <summary>
/// https://leetcode-cn.com/problems/h-index-ii/
/// 275. H指数 II
/// </summary>
internal class H_IndexIISolution
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
        int n = citations.Length;
        int left = 0, right = n - 1;
        while (left <= right)
        {
            var mid = left + (right - left) / 2;
            var factor = citations[mid];
            if (factor == n - mid) return n - mid;
            if (factor < n - mid) left = mid + 1;
            else right = mid - 1;
        }
        return n - left;

        //if (citations == null || citations.Length == 0) return 0;

        //var length = citations.Length;
        //for (int index = 0; index < length; index++)
        //{
        //    int hCount = length - index;
        //    if (hCount <= citations[index]) return hCount;
        //}

        //return 0;
    }
}

/*
H指数 II
力扣 (LeetCode)
发布于 2020-01-19
4.9k
方法一：线性搜索
算法：

由于引用次数列表是按升序排序的，因此我们可以再一次迭代过程中解决该问题。

让我们思考一篇引用次数为 c 的文章，它的索引是 i，即 c = citations[i]。我们可以知道，引用次数高于 c 的文章数量是 n-i-1。加上当前文章，有 n-i 个文章引用次数至少为 c 次。

根据 H 指数的定义，我们只需要找到第一篇文章 c = citation[i] 大于或等于 n - i，即 c >= n - i。我们知道在次之后的文章都引用次数至少 c 次，因此总共有 n-i 篇文章引用次数至少为 c 次。因此，根据定义，H 指数应该是 n-i。

在这里插入图片描述


class Solution {
  public int hIndex(int[] citations) {
    int idx = 0, n = citations.length;
    for(int c : citations) {
      if (c >= n - idx) return n - idx;
      else idx++;
    }
    return 0;
  }
}
复杂度分析

时间复杂度：\mathcal{O}(N)O(N)，其中 NN 指的是输入数组的长度，最坏的情况下我们要遍历整个数组。
空间复杂度：\mathcal{O}(1)O(1)，这是一个常数空间的解决方法。
方法二：二分搜索
根据我们在方法一中的阐述，问题实际上可以归结为：给定一个大小为 n 的升序的引用次数列表，要求找到满足 citations[i] >= n - i 的第一个 citations[i]。

通过问题的转换，我们可以用二分搜索来解决该问题。在二分搜索算法中，我们递归的将搜索范围减半，与线性搜索相比，时间复杂度更优为 \mathcal{O}(\log N)O(logN)。

在这里插入图片描述

算法：

首先，我们先获取列表的中间元素，即 citations[pivot]，它将原始列表分成了两个子列表：citations[0 : pivot - 1] 和 citations[pivot + 1 : n]。
然后比较 n - pivot 和 citations[pivot] 的值，二分搜索算法分为以下 3 种情况：
若 citations[pivot] == n - pivot：则我们找到了想要的元素。
若 citations[pivot] < n - pivot：由于我们想要的元素应该大于或等于 n - pivot，所以我们应该进一步搜索右边的子列表，即 citations[pivot + 1 : n]。
若 citations[pivot] > n - pivot：我们应该进一步搜索左边的子列表，即 citations[0 : pivot-1]。
与典型的二分搜索算法的一个小区别就是，我们返回的结果是 n - pivot，而不是所需元素的值。


class Solution {
  public int hIndex(int[] citations) {
    int idx = 0, n = citations.length;
    int pivot, left = 0, right = n - 1;
    while (left <= right) {
      pivot = left + (right - left) / 2;
      if (citations[pivot] == n - pivot) return n - pivot;
      else if (citations[pivot] < n - pivot) left = pivot + 1;
      else right = pivot - 1;
    }
    return n - left;
  }
}
复杂度分析

时间复杂度：\mathcal{O}(\log N)O(logN)，由于使用了二分搜索算法。
空间复杂度：\mathcal{O}(1)O(1)，这是一个常数空间的解决方法。

public class Solution {
    public int HIndex(int[] citations) {
        int n = citations.Length;
            for (int h = n; h > 0; h--) {
                if (citations[n-h] >= h) {
                    return h;
                }
            }
            return 0;
    }
}

public class Solution {
    public int HIndex(int[] citations) {
        if (citations.Length == 0) return 0;
        return this.BinarySearch(citations, 0, citations.Length-1);
    }

    private int BinarySearch(int[] citations, int start, int end) {
        if (start == end) {
            return Math.Min(citations.Length-start, citations[start]);
        }

        int mid = start + end >> 1;
        if (citations[mid] == citations.Length - mid) {
            return citations[mid];
        }

        if (citations[mid] < citations.Length - mid) {
            return this.BinarySearch(citations, mid+1, end);
        }

        return this.BinarySearch(citations, start, mid);
    }
}



public class Solution {
    public int HIndex(int[] citations) {
        int len = citations.Length;
        for (int i = 0; i < len; ++i)
        {
            if(citations[len - i - 1] < i + 1) return i;
            if(citations[len - i - 1] == i + 1) return i + 1;
        }
        return citations.Length;
    }
}
public class Solution {
    public int HIndex(int[] citations) {
           if (citations.Length == 0)
                return 0;
            var left = 0;
            var right = citations.Length - 1;
            while (left <= right) {
                var mid = (left + right) / 2;
                if (citations.Length - mid <= citations[mid])
                {
                    if (mid==0||!(citations.Length - (mid - 1) <= citations[mid - 1])) {
                        return citations.Length- mid;
                    }
                    right = mid - 1;
                }
                else {
                    left = mid + 1;
                }
            }
            return 0;
    }
}

*/