using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个数组 A，将其划分为两个不相交（没有公共元素）的连续子数组 left 和 right， 使得：

left 中的每个元素都小于或等于 right 中的每个元素。
left 和 right 都是非空的。
left 要尽可能小。
在完成这样的分组后返回 left 的长度。可以保证存在这样的划分方法。

 

示例 1：

输入：[5,0,3,8,6]
输出：3
解释：left = [5,0,3]，right = [8,6]
示例 2：

输入：[1,1,1,0,6,12]
输出：4
解释：left = [1,1,1,0]，right = [6,12]
 

提示：

2 <= A.length <= 30000
0 <= A[i] <= 10^6
可以保证至少有一种方法能够按题目所描述的那样对 A 进行划分。
*/
/// <summary>
/// https://leetcode-cn.com/problems/partition-array-into-disjoint-intervals/
/// 915. 分割数组
/// 
/// </summary>
class PartitionArrayIntoDisjointIntervalsSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int PartitionDisjoint(int[] A)
    {
        int len = A.Length;
        int[] maxLeft = new int[len];
        int[] minRight = new int[len];

        {
            int max = A[0];
            maxLeft[0] = max;
            for (int i = 1; i < len; ++i)
            {
                var v = A[i];
                if (max < v) max = v;
                maxLeft[i] = max;
            }
        }
        {
            int min = A[len - 1];
            minRight[len - 1] = min;
            for (int i = len - 2; i >= 0; --i)
            {
                var v = A[i];
                if (v < min) min = v;
                minRight[i] = min;
            }
        }

        for (int i = 1; i < len; ++i)
            if (maxLeft[i - 1] <= minRight[i]) return i;

        return 0;
    }
}
/*
方法 1：辅助数组
想法

不检验 all(L <= R for L in left for R in right)，而是检验 max(left) <= min(right)。

算法

找出对于所有子集 left = A[:1], left = A[:2], left = A[:3], ... 的最大值 max(left)，也就是用 maxleft[i] 记录子集 A[:i] 的最大值。两两之间是相互关联的：max(A[:4]) = max(max(A[:3]), A[3]) 所以有 maxleft[4] = max(maxleft[3], A[3])。

同理，所有可能的 right 子集最小值 min(right) 也可以在线性时间内获得。

最后只需要快速扫描一遍 max(left) 和 min(right)，答案非常明显。

JavaPython
class Solution {
    public int partitionDisjoint(int[] A) {
        int N = A.length;
        int[] maxleft = new int[N];
        int[] minright = new int[N];

        int m = A[0];
        for (int i = 0; i < N; ++i) {
            m = Math.max(m, A[i]);
            maxleft[i] = m;
        }

        m = A[N-1];
        for (int i = N-1; i >= 0; --i) {
            m = Math.min(m, A[i]);
            minright[i] = m;
        }

        for (int i = 1; i < N; ++i)
            if (maxleft[i-1] <= minright[i])
                return i;

        throw null;
    }
}
复杂度分析

时间复杂度：O(N)O(N)，其中 NN 是 A 的长度。
空间复杂度：O(N)O(N)。
 
*/
