using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
如果一个数列至少有三个元素，并且任意两个相邻元素之差相同，则称该数列为等差数列。

例如，以下数列为等差数列:

1, 3, 5, 7, 9
7, 7, 7, 7
3, -1, -5, -9
以下数列不是等差数列。

1, 1, 2, 5, 7
 

数组 A 包含 N 个数，且索引从0开始。数组 A 的一个子数组划分为数组 (P, Q)，P 与 Q 是整数且满足 0<=P<Q<N 。

如果满足以下条件，则称子数组(P, Q)为等差数组：

元素 A[P], A[p + 1], ..., A[Q - 1], A[Q] 是等差的。并且 P + 1 < Q 。

函数要返回数组 A 中所有为等差数组的子数组个数。

 

示例:

A = [1, 2, 3, 4]

返回: 3, A 中有三个子等差数组: [1, 2, 3], [2, 3, 4] 以及自身 [1, 2, 3, 4]。 
*/
/// <summary>
/// https://leetcode-cn.com/problems/arithmetic-slices/
/// 413. 等差数列划分
/// https://blog.csdn.net/xuchonghao/article/details/80853595
/// </summary>
class ArithmeticSlicesSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int NumberOfArithmeticSlices(int[] A)
    {
        if (A == null || A.Length == 0) return 0;
        int sum = 0;
        int count = 0;
        for (int i = 2; i < A.Length; i++)
        {
            if (A[i] - A[i - 1] == A[i - 1] - A[i - 2])
            {
                count++;
                sum += count;
            }
            else
            {
                count = 0;
            }
        }

        return sum;
    }
}
/*
public class Solution {
    public int NumberOfArithmeticSlices(int[] A) {
        int n = A.Length;
        int p = 0;
        
        int result = 0;

        while(p < n - 2) {
                int dif = A[p+1] - A[p];
                int q;
                int end = p;
                for (q = p + 2; q < n; q ++) {
                    if (A[q] - A[q-1] == dif) {
                        end = q;
                    } else {
                        break;
                    }
                }
                
                int length = end - p + 1;
                if (length >=3 ) {
                    p = end;
                    
                    result += (length-1) * (length -2) /2;
                } else {
                    p = p + 1;
                }
        }
        
        return result;
    }
}
public class Solution {
    public int NumberOfArithmeticSlices(int[] A) {
        if(A.Length<3)
            return 0;
        int number = 0;
        int count = 0;
        int i = 1;
        while(i<A.Length-1)
        {
            if(A[i]-A[i-1]==A[i+1]-A[i])
                count++;
            else if(count>0)
            {
                number= number + (count+1)*count/2;
                count=0;
            }
            i++;
        }
        if(count>0)
            number= number + (count+1)*count/2;
        return number;
    }
}
public class Solution {
    public int NumberOfArithmeticSlices(int[] A) {
        if(A.Length<=2)return 0;
        int[] dp = new int[A.Length];
        dp[0]=0;
        dp[1]=0;
        int sum=0;
        for(int i = 2;i<A.Length;i++)
        {
            if(A[i]-A[i-1]==A[i-1]-A[i-2])
            {
                dp[i]=dp[i-1]+1;
                sum+=dp[i];
            }
        }
        return sum;
    }
}
*/
