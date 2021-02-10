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
等差数列划分
力扣 (LeetCode)

发布于 2019-07-01
15.5k
方法一： 暴力 【通过】
最简单直白的方法就是考虑每一对元素（之间至少隔着一个元素），对两个元素之间的所有元素来判断是不是等差数列。接下来，只需遍历每个元素来检查相邻元素之间的差值是不是都是相等的就可以了。对于每个满足等差数列的区间，增加 countcount 来记录等差数列的总数。


public class Solution {
    public int numberOfArithmeticSlices(int[] A) {
        int count = 0;
        for (int s = 0; s < A.length - 2; s++) {
            int d = A[s + 1] - A[s];
            for (int e = s + 2; e < A.length; e++) {
                int i = 0;
                for (i = s + 1; i <= e; i++)
                    if (A[i] - A[i - 1] != d)
                        break;
                if (i > e)
                    count++;
            }
        }
        return count;
    }
}
复杂度分析

时间复杂度： O(n^3)O(n 
3
 )
对于每一对元素，都需要遍历它们之间的所有元素。其中 nn 为数组 AA 的大小。

空间复杂度： O(1)O(1)
只需额外开辟常数个空间。

方法二 优雅的暴力 【通过】
算法

在上一个方法中，我们考虑每一对元素和它们对应的区间，然后遍历这一段区间来检查相邻元素之间的差值是不是都是相等的。通过简单地观察，可以发现这个方法其实是可以优化的。

如果当前在处理一个区间 (s,e)(s,e)，其中 A[s]A[s]（区间头）和 A[e]A[e] （区间尾），我们需要检查这个区间内所有相邻元素的差值是不是都相等。现在，把这个区间扩大一点，变成从 ss 到 e+1e+1，我们就需要再一次对于区间 s:es:e 中的所有元素做一遍等差判断，然后再额外判断 A[e+1]A[e+1] 和 A[e]A[e] 的差值是不是跟之前的相等。其实是可以不用重复的判断区间 s:es:e 的，只需要判断最后一对元素的差值是不是跟之前区间中的差值相等就可以了。（固定 ss，不断增加 ee)。

需要注意的是，一旦当前区间不满足等差数列了，那就不需要继续判断了。


public class Solution {
    public int numberOfArithmeticSlices(int[] A) {
        int count = 0;
        for (int s = 0; s < A.length - 2; s++) {
            int d = A[s + 1] - A[s];
            for (int e = s + 2; e < A.length; e++) {
                if (A[e] - A[e - 1] == d)
                    count++;
                else
                    break;
            }
        }
        return count;
    }
}
复杂度分析

时间复杂度： O(n^2)O(n 
2
 )
算法有两层循环

空间复杂度： O(1)O(1)
只需额外开辟常数个空间。

方法三 递归 【通过】
算法

通过上一个方法我们归纳出来一个规律，如果区间 (i, j)(i,j) 是等差数列，那么当 A[j+1]A[j+1] 和 A[j]A[j] 的差值和之前的差值相等的情况下，区间 (i,j+1)(i,j+1) 也构成一个等差数列。此外，如果区间 (i,j)(i,j) 就不是一个等差数列，那么之后再向右拓展也不可能是一个等差数列了。

根据这个规律，我们可以设计一个递归算法。首先，定义变量 sumsum 来记录数组 AA 中所有等差数列的个数。接着，定义一个递归方法 slice(A,i) 来求在区间 (k,i)(k,i) 中，而不在区间 (k,j)(k,j) 中等差数列的个数，其中 j < ij<i。每次递归也都会更新 sumsum 值。

现在，假设我们知道了 slice(A,i-1)slice(A,i−1) 的值为 xx，同时这个区间内元素用 [a_0,a_1,a_2,...a_(i-1)][a 
0
​	
 ,a 
1
​	
 ,a 
2
​	
 ,...a 
(
​	
 i−1)] 来表示。如果这个区间本身就是一个等差数列，这么这里面所有相邻元素之间的差值都是相等的。现在要加入一个新的元素 a_ia 
i
​	
  将区间拓展成 (0,i)(0,i)，如果拓展之后的区间还是一个等差数列，那么一定存在 a_i-a_(i-1)=a_(i-1)-a_(i-2)a 
i
​	
 −a 
(
​	
 i−1)=a 
(
​	
 i−1)−a 
(
​	
 i−2)。因此每加入一个新元素，就会多出 apap 个等差序列。其中新增等差数列的区间为 (0,i), (1,i), ... (i-2,i)(0,i),(1,i),...(i−2,i)，这些区间总数为 x+1x+1。这是因为除了区间 (0,i)(0,i) 以外，其余的区间如 (1,i), (2,i),...(i-2,i)(1,i),(2,i),...(i−2,i) 这些都可以对应到之前的区间 (0,i-1), (1,i-1),...(i-3,i-1)(0,i−1),(1,i−1),...(i−3,i−1) 上去，其值为 xx。

因此，每次调用 slices，如果第 i个i个 元素与前一个元素的差值正好等于之前的差值，我们直接就可以算出新增的等差数组的个数 apap，同时可以更新 sumsum。但是，如果新元素跟前一个元素的差值不等于之前的差值，也就不会增加等差数列的个数。


public class Solution {
    int sum = 0;
    public int numberOfArithmeticSlices(int[] A) {
        slices(A, A.length - 1);
        return sum;
    }
    public int slices(int[] A, int i) {
        if (i < 2)
            return 0;
        int ap = 0;
        if (A[i] - A[i - 1] == A[i - 1] - A[i - 2]) {
            ap = 1 + slices(A, i - 1);
            sum += ap;
        } else
            slices(A, i - 1);
        return ap;
    }
}
复杂度分析

时间复杂度： O(n)O(n)
递归方法最多被调用 n-2n−2 次。

空间复杂度： O(n)O(n)
递归树的深度最多为 n-2n−2。

方法五： 动态规划 【通过】
算法

在上一个方法中，我们开始是从最大区间 (0,n-1)(0,n−1) 开始的，其中 nn 为数组 AA 中元素的个数。我们可以观察到区间 (0,i)(0,i) 中等差数列的个数只和这个区间中的元素有关。因此，这个问题可以用动态规划来解决。

首先创建一个大小为 nn 的一维数组 dpdp。dp[i]dp[i] 用来存储在区间 (k,i)(k,i)， 而不在区间 (k,j)(k,j) 中等差数列的个数，其中 j<ij<i。

与递归方法中后向推导不同，我们前向推导 dpdp 中的值。其余的思路跟上一个方法几乎一样。对于第 ii 个元素，判断这个元素跟前一个元素的差值是否和等差数列中的差值相等。如果相等，那么新区间中等差数列的个数即为 1+dp[i-1]1+dp[i−1]。sumsum 同时也要加上这个值来更新全局的等差数列总数。

下面的动画描述了 dpdp 的推导过程。




public class Solution {
    public int numberOfArithmeticSlices(int[] A) {
        int[] dp = new int[A.length];
        int sum = 0;
        for (int i = 2; i < dp.length; i++) {
            if (A[i] - A[i - 1] == A[i - 1] - A[i - 2]) {
                dp[i] = 1 + dp[i - 1];
                sum += dp[i];
            }
        }
        return sum;
    }
}

复杂度分析

时间复杂度： O(n)O(n)
只需遍历数组 AA 一次，其大小为 nn。

空间复杂度： O(n)O(n)
一维数组 dpdp 大小为 nn。

方法五 常数空间动态规划 【通过】
算法

在上一个方法中，可以观察到我们其实只需要 dp[i-1]dp[i−1] 来决定 dp[i]dp[i] 的值。因此，相对于整个 dpdp 数组，我们只需要保存一个最近一个 dpdp 值就可以了。


public class Solution {
    public int numberOfArithmeticSlices(int[] A) {
        int dp = 0;
        int sum = 0;
        for (int i = 2; i < A.length; i++) {
            if (A[i] - A[i - 1] == A[i - 1] - A[i - 2]) {
                dp = 1 + dp;
                sum += dp;
            } else
                dp = 0;
        }
        return sum;
    }
}
复杂度分析

时间复杂度： O(n)O(n)
只需遍历数组 AA 一次，其大小为 nn。

空间复杂度： O(1)O(1)
只需常数个额外空间。

方法六 公式计算 【通过】
算法

通过 dpdp 方法，我们观察到对于 kk 个连续且满足等差条件的元素，每次 sumsum 值分别增加 1, 2, 3, ..., k1,2,3,...,k。因此，与其每次更新 sumsum 值，只需要用变量 countcount 来记录有多少个满足等差条件的连续元素，之后直接把 sumsum 增加 count*(count+1)/2count∗(count+1)/2 就可以了。


public class Solution {
    public int numberOfArithmeticSlices(int[] A) {
        int count = 0;
        int sum = 0;
        for (int i = 2; i < A.length; i++) {
            if (A[i] - A[i - 1] == A[i - 1] - A[i - 2]) {
                count++;
            } else {
                sum += (count + 1) * (count) / 2;
                count = 0;
            }
        }
        return sum += count * (count + 1) / 2;
    }
}
复杂度分析

时间复杂度： O(n)O(n)
只需遍历数组 AA 一次，其大小为 nn。

空间复杂度： O(1)O(1)
只需额外开辟常数个空间。

提供一种官方没有写的想法，不需要 dp，O(n)
Jing Yang
发布于 2019-08-04
3.8k
首先遍历原数组 nums，用数组 diffs 存储相邻两个元素之间的差值。

然后遍历 diffs，用数组 cons 存储其中连续相同的差值的数目，比如有 33 个 33 连在一起，意味着原数组中这个位置存在一个最大长度为 44 的等差数列。

然后遍历 cons，对于长度为 n 的等差数列，其所有的长度大于等于 33 的子数列都是等差数列，则一共有 (n-2)(n-1)/2 个等差数列。
全部相加得到结果。

比如：


nums = [1,2,3,4,5,6,12,14,16]
diffs = [1,1,1,1,1,6,2,2]
cons = [5,1,2]
# 将 1 舍去，nums 中有长度为 5+1 和 2+1 的等差数列
result = (6-2)(6-1)/2 + (3-2)(3-1)/2
代码如下：


class Solution:
    def numberOfArithmeticSlices(self, nums: List[int]) -> int:
        
        # 第一次遍历
        diffs = []
        for i in range(len(nums) - 1):
            diffs.append(nums[i + 1] - nums[i])
            
        # 第二次遍历
        cons = []
        a = 1
        for i in range(1, len(diffs)):
            if diffs[i] == diffs[i - 1]:
                a += 1
            else:
                cons.append(a)
                a = 1
        cons.append(a)
        
        # 第三次遍历
        res = 0
        for num in cons:
            res += int(self.calc(num))
        return res
        
    # 用于计算cons内每个数代表的等差数列个数
    def calc(self, n):
        if n == 1:
            return 0
        n += 1
        return (n-2)*(n-1)/2

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
