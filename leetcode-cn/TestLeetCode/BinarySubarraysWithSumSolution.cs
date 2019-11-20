using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
在由若干 0 和 1  组成的数组 A 中，有多少个和为 S 的非空子数组。

示例：

输入：A = [1,0,1,0,1], S = 2
输出：4
解释：
如下面黑体所示，有 4 个满足题目要求的子数组：
[1,0,1,0,1]
[1,0,1,0,1]
[1,0,1,0,1]
[1,0,1,0,1]
 

提示：

A.length <= 30000
0 <= S <= A.length
A[i] 为 0 或 1
*/
/// <summary>
/// https://leetcode-cn.com/problems/binary-subarrays-with-sum/
/// 930. 和相同的二元子数组
/// 
/// </summary>
class BinarySubarraysWithSumSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int NumSubarraysWithSum(int[] A, int S)
    {
        int sum = A.Sum();

        int[] indexes = new int[sum + 2];
        int t = 0;
        indexes[t++] = -1;
        for (int i = 0; i < A.Length; ++i)
            if (A[i] == 1)
                indexes[t++] = i;
        indexes[t] = A.Length;

        int ret = 0;
        if (S == 0)
        {
            for (int i = 0; i < indexes.Length - 1; ++i)
            {
                // w: number of zeros between consecutive ones
                int w = indexes[i + 1] - indexes[i] - 1;
                ret += w * (w + 1) / 2;
            }
            return ret;
        }

        int upper = indexes.Length - S;
        for (int i = 1; i < upper; i++)
        {
            int j = i + S - 1;
            int left = indexes[i] - indexes[i - 1];
            int right = indexes[j + 1] - indexes[j];
            ret += left * right;
        }

        return ret;
    }
}
/*
Approach 1: 枚举子数组中 1 的位置
我们设数组 A 中有 k 个 1，它们的位置从小到大分别为 x_1, x_2, \cdots, x_kx 
1
?	
 ,x 
2
?	
 ,?,x 
k
?	
 。对于一个和为 S 的子数组，它包含的 1 一定是 x_1, x_2, \cdots, x_kx 
1
?	
 ,x 
2
?	
 ,?,x 
k
?	
  中的某一个连续的段 x_i, x_{i+1}, \cdots, x_{i+S-1}x 
i
?	
 ,x 
i+1
?	
 ,?,x 
i+S?1
?	
 。因此我们可以枚举这个连续段的起始位置 i，并计算出包含该连续段的所有和为 S 的非空子数组。

对于某一个连续的段 x_i, x_{i+1}, \cdots, x_{i+S-1}x 
i
?	
 ,x 
i+1
?	
 ,?,x 
i+S?1
?	
 ，包含该连续段的所有和为 S 的非空子数组个数为：

(x_i - x_{i-1}) * (x_{i+S} - x_{i+S-1})
(x 
i
?	
 ?x 
i?1
?	
 )?(x 
i+S
?	
 ?x 
i+S?1
?	
 )

形象地说，x_ix 
i
?	
  左侧有 x_i - x_{i-1} - 1x 
i
?	
 ?x 
i?1
?	
 ?1 个 0，选择任意数量的 0 都不会改变子数组的和（也可以不选择任何 0)，因此左侧有 x_i - x_{i-1}x 
i
?	
 ?x 
i?1
?	
  种选择方法。同理，右侧有 x_{i+S} - x_{i+S-1}x 
i+S
?	
 ?x 
i+S?1
?	
  种选择方法。

例如当 S 的值为 2，A 为 [1,0,1,0,1,0,0,1] 时，我们尝试计算包含中间（即位置 2 和 4）两个 1 的子数组数目。子数组的左侧有 1 个 0，因此有 2 种选择（即位置 1 和 2）；右侧有 2 个 0，因此有 3 种选择（即位置 4，5 和 6）。总共有 2 * 3 = 6 种选择。

此外，需要注意考虑边界情况和特殊情况，例如 S = 0，i = 1 以及 i + S - 1 = k。

JavaPython
class Solution {
    public int numSubarraysWithSum(int[] A, int S) {
        int su = 0;
        for (int x: A) su += x;

        // indexes[i] = location of i-th one (1 indexed)
        int[] indexes = new int[su + 2];
        int t = 0;
        indexes[t++] = -1;
        for (int i = 0; i < A.length; ++i)
            if (A[i] == 1)
                indexes[t++] = i;
        indexes[t] = A.length;

        int ans = 0;
        if (S == 0) {
            for (int i = 0; i < indexes.length - 1; ++i) {
                // w: number of zeros between consecutive ones
                int w = indexes[i+1] - indexes[i] - 1;
                ans += w * (w + 1) / 2;
            }
            return ans;
        }

        for (int i = 1; i < indexes.length - S; ++i) {
            int j = i + S - 1;
            int left = indexes[i] - indexes[i-1];
            int right = indexes[j+1] - indexes[j];
            ans += left * right;
        }

        return ans;
    }
}
复杂度分析

时间复杂度：O(N)O(N)，其中 NN 是数组 A 的长度。

空间复杂度：O(N)O(N)。

方法二：前缀和
分析

设数组 P 为数组 A 的前缀和，即：

P[i] = A[0] + A[1] + ... + A[i - 1]

这样就可以通过 P[j + 1] - P[i] = A[i] + A[i + 1] + ... + A[j] 快速计算出 A[i..j] 的和。

我们可以对数组 P 进行一次线性扫描，当扫描到 P[j] 时，我们需要得到的是满足 P[j] = P[i] + S 且 i < j 的 i 的数目，使用计数器（map 或 dict）可以满足要求。

JavaPython
class Solution {
    public int numSubarraysWithSum(int[] A, int S) {
        int N = A.length;
        int[] P = new int[N + 1];
        for (int i = 0; i < N; ++i)
            P[i+1] = P[i] + A[i];

        Map<Integer, Integer> count = new HashMap();
        int ans = 0;
        for (int x: P) {
            ans += count.getOrDefault(x, 0);
            count.put(x+S, count.getOrDefault(x+S, 0) + 1);
        }

        return ans;
    }
}
复杂度分析

时间复杂度：O(N)O(N)，其中 NN 是数组 A 的长度。

空间复杂度：O(N)O(N)。

方法三：三指针
在方法二中，我们在固定区间的右端点 j 时，用计数器求出满足要求的左端点 i 的数目。而由于此题的特殊性，前缀和数组 P 是单调不降的，因此左端点的位置一定是连续的，即可以用 [i_lo, i_hi] 表示，并且随着 j 的增加，i_lo 和 i_hi 也单调不降，因此可以用类似双指针的方法，使用三个指针维护左端点的区间。

我们遍历区间的右端点 j，同时维护四个变量：

sum_lo：A[i_lo..j] 的值；

sum_hi：A[i_hi..j] 的值；

i_lo：最小的满足 sum_lo <= S 的 i；

i_hi：最大的满足 sum_hi <= S 的 i。

例如，当数组 A 为 [1,0,0,1,0,1]，S 的值为 2 。当 j = 5 时，i_lo 的值为 1，i_hi 的值为 3。对于每一个 j，和为 S 的非空子数组的数目为 i_hi - i_lo + 1。

JavaPython
class Solution {
    public int numSubarraysWithSum(int[] A, int S) {
        int iLo = 0, iHi = 0;
        int sumLo = 0, sumHi = 0;
        int ans = 0;

        for (int j = 0; j < A.length; ++j) {
            // While sumLo is too big, iLo++
            sumLo += A[j];
            while (iLo < j && sumLo > S)
                sumLo -= A[iLo++];

            // While sumHi is too big, or equal and we can move, iHi++
            sumHi += A[j];
            while (iHi < j && (sumHi > S || sumHi == S && A[iHi] == 0))
                sumHi -= A[iHi++];

            if (sumLo == S)
                ans += iHi - iLo + 1;
        }

        return ans;
    }
}
复杂度分析

时间复杂度：O(N)O(N)，其中 NN 是数组 A 的长度。

空间复杂度：O(1)O(1)。 

 
public class Solution {
    public int NumSubarraysWithSum(int[] A, int S)
    {
        int m = A.Length;
        Dictionary<int, int> dic = new Dictionary<int, int>();
        int ptr = 0;
        int count = 0;
        int sum = 0;
        dic[0] = 1;
        while (ptr!=m)
        {
            sum += A[ptr];
            if (dic.ContainsKey(sum - S))
                count += dic[sum - S];
            if (dic.ContainsKey(sum))
                dic[sum]++;
            else
                dic[sum] = 1;
            ptr++;
        }
        return count;
    }
}     
*/
