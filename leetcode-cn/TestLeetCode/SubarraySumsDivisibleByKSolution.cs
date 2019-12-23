using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个整数数组 A，返回其中元素之和可被 K 整除的（连续、非空）子数组的数目。

 

示例：

输入：A = [4,5,0,-2,-3,1], K = 5
输出：7
解释：
有 7 个子数组满足其元素之和可被 K = 5 整除：
[4, 5, 0, -2, -3, 1], [5], [5, 0], [5, 0, -2, -3], [0], [0, -2, -3], [-2, -3]
 

提示：

1 <= A.length <= 30000
-10000 <= A[i] <= 10000
2 <= K <= 10000
*/
/// <summary>
/// https://leetcode-cn.com/problems/subarray-sums-divisible-by-k/
/// 974. 和可被 K 整除的子数组
/// 
/// </summary>
class SubarraySumsDivisibleByKSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int SubarraysDivByK(int[] A, int K)
    {
        int len = A.Length;
        int[] sums = new int[len + 1];
        sums[0] = 0;
        for (int i = 0; i < len; ++i) sums[i + 1] = sums[i] + A[i];

        int[] counts = new int[K];
        Array.Fill(counts, 0);

        foreach (int sum in sums) counts[(sum % K + K) % K]++;

        int ret = 0;
        foreach (int count in counts)
            ret += ( count - 1 + 1 ) * ( count - 1 ) / 2;
        return ret;
    }
}
/*
和可被 K 整除的子数组
力扣 (LeetCode)
发布于 1 年前
2.7k 阅读
方法：前缀和与计数
思路

通常，涉及连续子数组问题的时候，我们使用前缀和来解决它们。我们令 P[i+1] = A[0] + A[1] + ... + A[i]。那么，每个连续子数组就可以写成 P[j] - P[i] （其中 j > i） 的形式。因此，我们将 P[j] - P[i] 模 K 等于 0 等价于 P[i] 与 P[j] 在模 K 的意义下同余。

算法

计算所有的 P[i] 在模 K 意义下的值。如果说一共有 C_xC 
x
​	
  个 P[i] \equiv x \pmod{K}P[i]≡x(modK)。那么，就有 \sum_x \binom{C_x}{2}∑ 
x
​	
 ( 
2
C 
x
​	
 
​	
 ) 个可行的连续子数组。

举一个例子，给定数组为 A = [4,5,0,-2,-3,1]。那么 P = [0,4,9,9,7,4,5]，同时 C_0 = 2, C_2 = 1, C_4 = 4C 
0
​	
 =2,C 
2
​	
 =1,C 
4
​	
 =4：

对于 C_0 = 2C 
0
​	
 =2（P[0]P[0]、P[6]P[6]），这表示一共有 \binom{2}{2} = 1( 
2
2
​	
 )=1 的连续子数组的和能被 KK 整除，也就是 A[0:6] = [4, 5, 0, -2, -3, 1]A[0:6]=[4,5,0,−2,−3,1]。

对于 C_4 = 4C 
4
​	
 =4（P[1]P[1]、P[2]P[2]、P[3]P[3]、P[5]P[5]），这表示一共有 \binom{4}{2} = 6( 
2
4
​	
 )=6 个连续子数组的和能被 KK 整除，分别是 A[1:2]A[1:2]、A[1:3]A[1:3]、A[1:5]A[1:5]、A[2:3]A[2:3]、A[2:5]A[2:5]、A[3:5]A[3:5]。

class Solution {
    public int subarraysDivByK(int[] A, int K) {
        int N = A.length;
        int[] P = new int[N+1];
        for (int i = 0; i < N; ++i)
            P[i+1] = P[i] + A[i];

        int[] count = new int[K];
        for (int x: P)
            count[(x % K + K) % K]++;

        int ans = 0;
        for (int v: count)
            ans += v * (v - 1) / 2;
        return ans;
    }
}
复杂度分析

时间复杂度：O(N)O(N)，其中 NN 是数组 A 的长度。

空间复杂度：O(N)O(N)。（如果只存储 count 数组的话，那么解法的空间复杂度就可以变为 O(K)O(K)。）

public class Solution {
    public int SubarraysDivByK(int[] A, int K) {
        int N = A.Length, sum = 0, ans = 0;
        int[] map = new int[K];
        map[0] = 1;
        for (int i = 1; i <= N; i++) {
            sum = sum + A[i-1]; 
            int key = (sum % K + K) % K;
            ans += map[key];
            map[key]++;
        }
        return ans;
    }
} 
*/
