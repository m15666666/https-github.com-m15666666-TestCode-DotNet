using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定两个由一些闭区间组成的列表，每个区间列表都是成对不相交的，并且已经排序。

返回这两个区间列表的交集。

（形式上，闭区间 [a, b]（其中 a <= b）表示实数 x 的集合，而 a <= x <= b。两个闭区间的交集是一组实数，要么为空集，要么为闭区间。例如，[1, 3] 和 [2, 4] 的交集为 [2, 3]。）

示例：

输入：A = [[0,2],[5,10],[13,23],[24,25]], B = [[1,5],[8,12],[15,24],[25,26]]
输出：[[1,2],[5,5],[8,10],[15,23],[24,24],[25,25]]
注意：输入和所需的输出都是区间对象组成的列表，而不是数组或列表。

提示：

0 <= A.length < 1000
0 <= B.length < 1000
0 <= A[i][0], A[i][1], B[i][0], B[i][1] < 10^9
*/
/// <summary>
/// https://leetcode-cn.com/problems/interval-list-intersections/
/// 986. 区间列表的交集
/// 
/// </summary>
class IntervalListIntersectionsSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int[][] IntervalIntersection(int[][] A, int[][] B)
    {
        List<int[]> ret = new List<int[]>();
        int indexA = 0;
        int indexB = 0;

        while (indexA < A.Length && indexB < B.Length)
        {
            int lo = Math.Max(A[indexA][0], B[indexB][0]);
            int hi = Math.Min(A[indexA][1], B[indexB][1]);
            if (lo <= hi) ret.Add(new int[] { lo, hi });

            if (A[indexA][1] < B[indexB][1]) indexA++;
            else indexB++;
        }

        return ret.ToArray();
    }
}
/*
区间列表的交集
力扣 (LeetCode)
发布于 1 年前
2.2k 阅读
方法：归并区间
思路

我们称 b 为区间 [a, b] 的末端点。

在两个数组给定的所有区间中，考虑拥有最小末端点的区间 A[0]。（为了不失一般性，假设 A[0]出现在数组 A 中)

然后，在数组 B 的区间中， A[0] 只可能与数组 B 中的至多一个区间相交。（如果 B 中存在两个区间均与 A[0] 相交，那么它们将共同包含 A[0] 的末端点，但是 B 中的区间应该是不相交的，所以导出矛盾）

算法

如果 A[0] 拥有最小的末端点，那么它只可能与 B[0] 相交。然后我们就可以删除区间 A[0] 了，因为它不能与其他任何区间再相交了。

相似的，如果 B[0] 拥有最小的末端点，那么它只可能与区间 A[0] 相交，然后我们就可以将 B[0] 删除了，因为它无法再与其他区间相交了。

我们用两个指针 i 与 j 来虚拟地完成删除 A[0] 或 B[0] 的操作。

class Solution {
    public Interval[] intervalIntersection(Interval[] A, Interval[] B) {
        List<Interval> ans = new ArrayList();
        int i = 0, j = 0;

        while (i < A.length && j < B.length) {
            // Let's check if A[i] intersects B[j].
            // lo - the startpoint of the intersection
            // hi - the endpoint of the intersection
            int lo = Math.max(A[i].start, B[j].start);
            int hi = Math.min(A[i].end, B[j].end);
            if (lo <= hi)
                ans.add(new Interval(lo, hi));

            // Remove the interval with the smallest endpoint
            if (A[i].end < B[j].end)
                i++;
            else
                j++;
        }

        return ans.toArray(new Interval[ans.size()]);
    }
}
复杂度分析

时间复杂度：O(M + N)O(M+N)，其中 M, NM,N 分别是数组 A 与 B 的长度。

空间复杂度：O(M + N)O(M+N)，也是答案区间数量的上限。

public class Solution {
    public int[][] IntervalIntersection(int[][] A, int[][] B) {
        int[] x = new int[A.Length + B.Length];
        int[] y = new int[A.Length + B.Length];
        int aIndex = 0, bIndex = 0, insertIndex = 0;
        while (aIndex < A.Length && bIndex < B.Length) {
            if (A[aIndex][1] < B[bIndex][1]) {
                if (B[bIndex][0] > A[aIndex][1]) {

                } else {
                    int pos = Math.Max(A[aIndex][0], B[bIndex][0]);
                    x[insertIndex] = pos;
                    y[insertIndex++] = A[aIndex][1];
                }
                ++aIndex;
            } else if (A[aIndex][1] > B[bIndex][1]) {
                if (A[aIndex][0] > B[bIndex][1]) {

                } else {
                    int pos = Math.Max(A[aIndex][0], B[bIndex][0]);
                    x[insertIndex] = pos;
                    y[insertIndex++] = B[bIndex][1];
                }
                ++bIndex;
            } else {
                int pos = Math.Max(B[bIndex][0], A[aIndex][0]);
                x[insertIndex] = pos;
                y[insertIndex++] = A[aIndex][1];
                ++aIndex; ++bIndex;
            }
        }
        int[][] ans = new int[insertIndex][];
        for (int i = 0; i < insertIndex; ++i) {
            ans[i] = new int[2];
            ans[i][0] = x[i];
            ans[i][1] = y[i];
        }
        return ans;
    }
} 
*/
