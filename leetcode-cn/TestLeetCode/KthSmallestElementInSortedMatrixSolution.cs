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
