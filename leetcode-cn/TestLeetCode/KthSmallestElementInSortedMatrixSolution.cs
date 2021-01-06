using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
给定一个 n x n 矩阵，其中每行和每列元素均按升序排序，找到矩阵中第k小的元素。
请注意，它是排序后的第k小元素，而不是第k个元素。

示例:

matrix = [
   [ 1,  5,  9],
   [10, 11, 13],
   [12, 13, 15]
],
k = 8,

返回 13。
说明: 
你可以假设 k 的值永远是有效的, 1 ≤ k ≤ n2 。 
*/
/// <summary>
/// https://leetcode-cn.com/problems/kth-smallest-element-in-a-sorted-matrix/
/// 378. 有序矩阵中第K小的元素
/// https://blog.csdn.net/fisherming/article/details/79908207
/// https://zhuanlan.zhihu.com/p/49264055
/// </summary>
class KthSmallestElementInSortedMatrixSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int KthSmallest(int[][] matrix, int k)
    {
        int n = matrix.Length;
        int L = matrix[0][0];
        int R = matrix[n - 1][n - 1];
        int ans = 0;
        while (L <= R)
        {
            int mid = L + (R - L) / 2;
            int midCount = 0;
            var lessCount = guess(matrix, mid, n, k, ref midCount);
            if ( lessCount < k )
            {
                if (k <= midCount + lessCount) return mid;

                ans = mid;
                L = mid + 1;
            }
            else
            {
                R = mid - 1;
            }
        }
        return ans;
    }

    private static int guess(int[][] matrix, int g, int n, int k, ref int gCount )
    {
        int sum = 0;
        for (int i = 0; i < n; i++)
        {
            int L = 0;
            int R = n - 1;
            int ans = 0;
            int foundIndex = -1;
            while (L <= R)
            {
                int mid = L + (R - L) / 2;
                //若某一行值小于g，则应该是最后一个元素的下标 + 1.
                if (matrix[i][mid] < g)
                {
                    foundIndex = mid;
                    ans = mid + 1;
                    L = mid + 1;
                }
                else
                {
                    R = mid - 1;
                }
            }
            if (foundIndex != -1 && (foundIndex + 1) < n && matrix[i][foundIndex + 1] == g) gCount++;
            sum += ans;
        }
        return sum;
    }
}
/*
有序矩阵中第K小的元素
力扣官方题解
发布于 2020-07-01
55.0k
方法一：直接排序
思路及算法

最直接的做法是将这个二维数组另存为为一维数组，并对该一维数组进行排序。最后这个一维数组中的第 kk 个数即为答案。

代码


class Solution {
    public int kthSmallest(int[][] matrix, int k) {
        int rows = matrix.length, columns = matrix[0].length;
        int[] sorted = new int[rows * columns];
        int index = 0;
        for (int[] row : matrix) {
            for (int num : row) {
                sorted[index++] = num;
            }
        }
        Arrays.sort(sorted);
        return sorted[k - 1];
    }
}
复杂度分析

时间复杂度：O(n^2\log{n})O(n 
2
 logn)，对 n^2n 
2
  个数排序。

空间复杂度：O(n^2)O(n 
2
 )，一维数组需要存储这 n^2n 
2
  个数。

方法二：归并排序
思路及算法

由题目给出的性质可知，这个矩阵的每一行均为一个有序数组。问题即转化为从这 nn 个有序数组中找第 kk 大的数，可以想到利用归并排序的做法，归并到第 kk 个数即可停止。

一般归并排序是两个数组归并，而本题是 nn 个数组归并，所以需要用小根堆维护，以优化时间复杂度。

具体如何归并，可以参考力扣 23. 合并K个排序链表。

代码


class Solution {
    public int kthSmallest(int[][] matrix, int k) {
        PriorityQueue<int[]> pq = new PriorityQueue<int[]>(new Comparator<int[]>() {
            public int compare(int[] a, int[] b) {
                return a[0] - b[0];
            }
        });
        int n = matrix.length;
        for (int i = 0; i < n; i++) {
            pq.offer(new int[]{matrix[i][0], i, 0});
        }
        for (int i = 0; i < k - 1; i++) {
            int[] now = pq.poll();
            if (now[2] != n - 1) {
                pq.offer(new int[]{matrix[now[1]][now[2] + 1], now[1], now[2] + 1});
            }
        }
        return pq.poll()[0];
    }
}
复杂度分析

时间复杂度：O(k\log{n})O(klogn)，归并 kk 次，每次堆中插入和弹出的操作时间复杂度均为 \log{n}logn。

空间复杂度：O(n)O(n)，堆的大小始终为 nn。

需要注意的是，kk 在最坏情况下是 n^2n 
2
 ，因此该解法最坏时间复杂度为 O(n^2\log{n})O(n 
2
 logn)。

方法三：二分查找
思路及算法

由题目给出的性质可知，这个矩阵内的元素是从左上到右下递增的（假设矩阵左上角为 matrix[0][0]matrix[0][0]）。以下图为例：

fig1

我们知道整个二维数组中 matrix[0][0]matrix[0][0] 为最小值，matrix[n - 1][n - 1]matrix[n−1][n−1] 为最大值，现在我们将其分别记作 ll 和 rr。

可以发现一个性质：任取一个数 midmid 满足 l\leq mid \leq rl≤mid≤r，那么矩阵中不大于 midmid 的数，肯定全部分布在矩阵的左上角。

例如下图，取 mid=8mid=8：

fig2

我们可以看到，矩阵中大于 midmid 的数就和不大于 midmid 的数分别形成了两个板块，沿着一条锯齿线将这个矩形分开。其中左上角板块的大小即为矩阵中不大于 midmid 的数的数量。

读者也可以自己取一些 midmid 值，通过画图以加深理解。

我们只要沿着这条锯齿线走一遍即可计算出这两个板块的大小，也自然就统计出了这个矩阵中不大于 midmid 的数的个数了。

走法演示如下，依然取 mid=8mid=8：

fig3

可以这样描述走法：

初始位置在 matrix[n - 1][0]matrix[n−1][0]（即左下角）；

设当前位置为 matrix[i][j]matrix[i][j]。若 matrix[i][j] \leq midmatrix[i][j]≤mid，则将当前所在列的不大于 midmid 的数的数量（即 i + 1i+1）累加到答案中，并向右移动，否则向上移动；

不断移动直到走出格子为止。

我们发现这样的走法时间复杂度为 O(n)O(n)，即我们可以线性计算对于任意一个 midmid，矩阵中有多少数不大于它。这满足了二分查找的性质。

不妨假设答案为 xx，那么可以知道 l\leq x\leq rl≤x≤r，这样就确定了二分查找的上下界。

每次对于「猜测」的答案 midmid，计算矩阵中有多少数不大于 midmid ：

如果数量不少于 kk，那么说明最终答案 xx 不大于 midmid；
如果数量少于 kk，那么说明最终答案 xx 大于 midmid。
这样我们就可以计算出最终的结果 xx 了。

代码


class Solution {
    public int kthSmallest(int[][] matrix, int k) {
        int n = matrix.length;
        int left = matrix[0][0];
        int right = matrix[n - 1][n - 1];
        while (left < right) {
            int mid = left + ((right - left) >> 1);
            if (check(matrix, mid, k, n)) {
                right = mid;
            } else {
                left = mid + 1;
            }
        }
        return left;
    }

    public boolean check(int[][] matrix, int mid, int k, int n) {
        int i = n - 1;
        int j = 0;
        int num = 0;
        while (i >= 0 && j < n) {
            if (matrix[i][j] <= mid) {
                num += i + 1;
                j++;
            } else {
                i--;
            }
        }
        return num >= k;
    }
}
复杂度分析

时间复杂度：O(n\log(r-l))O(nlog(r−l))，二分查找进行次数为 O(\log(r-l))O(log(r−l))，每次操作时间复杂度为 O(n)O(n)。

空间复杂度：O(1)O(1)。

写在最后
上述三种解法，第一种没有利用矩阵的性质，所以时间复杂度最差；第二种解法只利用了一部分性质（每一行是一个有序数列，而忽视了列之间的关系）；第三种解法则利用了全部性质，所以时间复杂度最佳。

这也启示我们要认真把握题目中的条件与性质，更有利于我们解题。

// tc o(n log(max - min)) sc o(1)
// binary search mid 找小于等于 mid 有多少个
// s = matrix[0][0]
// e = matrix[n - 1][n - 1]
// 这道题的二分法, 必须是 s < e, 不能是 s + 1 < e , 因为要让 e 无限制的 接近要求出来的数
// 所以 当 total >= k 是, e = mid, total < k 时 s = mid + 1
// tc o(klogn) sc o(1)
public class Solution {
    public int KthSmallest(int[][] matrix, int k) {
        int s = matrix[0][0];
        int e = matrix[^1][^1];
        while(s < e){
            int mid = (e - s) / 2 + s;
            if(count(mid, matrix) < k){
                s = mid + 1;
            } else {
                e = mid;
            }
        }
        return s;
    }
    
    public int count(int mid, int[][] matrix){
        int n = matrix.Length; 
        int res = 0;
        int i = n - 1;
        int j = 0;
        while(i >= 0 && j < n){
            if(matrix[i][j] > mid){
                i --;
            } else {
                res += i + 1;
                j ++;
            }
        }
        return res;
    }
}

public class Solution
{
    int [,] _matrix;
    int _n;
    public int KthSmallest(int[,] matrix, int k)
    {
        _matrix=matrix;
        _n=_matrix.GetLength(0);
        int left=_matrix[0,0];
        int right=_matrix[_n-1,_n-1];
        while(left<right){
        int mid=(left+right)/2;
        int c=NotBiggerThan(mid);
        if(c<k){
            left=mid+1;
        }
        else{
            right=mid;
        }
        }
        return left;
    }
    private int NotBiggerThan(int mid){
        int iBegin=_n-1;
        int jBegin=0;
        int count=0;
        while(jBegin<=_n&&iBegin>=0){
        if(jBegin<_n&&_matrix[iBegin,jBegin]<=mid){
            jBegin++;
        }
        else{
            count+=jBegin;
            iBegin--;
        }
        }
        // Console.WriteLine(count);
        return count;
    }
}
public class Solution {
    public int KthSmallest(int[,] matrix, int k) {
        int lo = matrix[0,0];
        int hi = matrix[matrix.GetLength(0)-1,matrix.GetLength(0)-1];
        
        while(lo<hi)
        {
            int mid = lo +(hi-lo)/2;
            int count = 0;
            int j = matrix.GetLength(1)-1;
            for(int i = 0; i < matrix.GetLength(0); i++)
            {
                while(j>=0 && matrix[i,j] > mid)
                    j--;
                count+=(j+1);
            }
            if(count < k)
                lo = mid+1;
            else
                hi = mid;
        }
    return hi;
    }
}
public class Solution {
    public int KthSmallest(int[,] matrix, int k) {
         var Ann=matrix;
         var testNums = new int[Ann.Length];
         var row=(int)Math.Sqrt(Ann.Length);
            for(var i=0;i<row;i++)
            {
                for(var j=0;j<row;j++)
                {
                    testNums[row * i + j] = Ann[i, j];
                }
            }
            Array.Sort(testNums);
        return testNums[k-1];
    }
}
public class Solution
{
    public int KthSmallest(int[,] matrix, int k)
    {
        //  int[,] _matrix;
        int _n;
        //  _matrix = matrix;
        _n = matrix.GetLength(0);
        SortedDictionary<int, int> _dict;
        _dict = new SortedDictionary<int, int>();
        int max = int.MinValue;
        for (int i = 0; i < _n; i++)
        {
        for (int j = 0; j < _n; j++)
        {
            int t = matrix[i, j];
            if (max < t)
            {
                if (_dict.Count >= k)
                {
                    break;
                }
                max = t;
            }
            _dict[t] = _dict.GetValueOrDefault(t, 0) + 1;
        }
        }
        foreach (var item in _dict)
        {
        k -= item.Value;
        if(k<=0){
            return item.Key;
        }
        }
        return 0;
    }
}
*/
