using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个整数数组 A，以及一个整数 target 作为目标值，返回满足 i < j < k 且 A[i] + A[j] + A[k] == target 的元组 i, j, k 的数量。

由于结果会非常大，请返回 结果除以 10^9 + 7 的余数。

示例 1：

输入：A = [1,1,2,2,3,3,4,4,5,5], target = 8
输出：20
解释：
按值枚举（A[i]，A[j]，A[k]）：
(1, 2, 5) 出现 8 次；
(1, 3, 4) 出现 8 次；
(2, 2, 4) 出现 2 次；
(2, 3, 3) 出现 2 次。
示例 2：

输入：A = [1,1,2,2,2,2], target = 5
输出：12
解释：
A[i] = 1，A[j] = A[k] = 2 出现 12 次：
我们从 [1,1] 中选择一个 1，有 2 种情况，
从 [2,2,2,2] 中选出两个 2，有 6 种情况。
 

提示：

3 <= A.length <= 3000
0 <= A[i] <= 100
0 <= target <= 300
在真实的面试中遇到过这道题？
*/
/// <summary>
/// https://leetcode-cn.com/problems/3sum-with-multiplicity/
/// 923. 三数之和的多种可能
/// 
/// </summary>
class ThreesumWithMultiplicitySolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int ThreeSumMulti(int[] A, int target)
    {
        Array.Sort(A);

        const long MOD = 1_000_000_007;
        long ret = 0;
        for (int i = 0; i < A.Length; ++i)
        {
            int T = target - A[i];
            int j = i + 1;
            int k = A.Length - 1;

            while (j < k)
            {
                if (A[j] + A[k] < T) j++;
                else if (A[j] + A[k] > T) k--;
                else if (A[j] != A[k])
                {  
                    int left = 1, right = 1;
                    while (j + 1 < k && A[j] == A[j + 1])
                    {
                        left++;
                        j++;
                    }
                    while (k - 1 > j && A[k] == A[k - 1])
                    {
                        right++;
                        k--;
                    }

                    ret += left * right;
                    ret %= MOD;
                    j++;
                    k--;
                }
                else
                {
                    ret += (k - j + 1) * (k - j) / 2;
                    ret %= MOD;
                    break;
                }
            } // while (j < k)
        }

        return (int)ret;
    }
}
/*
方法须知
下面讲的方法跟用双指针来做 "两数之和" 有异曲同工之妙，先来看一下 "两数之和" 这道题。

假设有一个有序数组，同时这个数组中元素唯一，想知道有多少对 i，j，满足 i < j 且 A[i] + A[j] == target。

"两数之和" 可以在线性时间解决的，定义两个指针 i，j，初始分别指向数组的头尾，i 逐渐递增，j 逐渐递减，来找出所有满足 A[i] + A[j] == target 的组合。

Python
def solve(A, target):
    # Assume A already sorted
    i, j = 0, len(A) - 1
    ans = 0
    while i < j:
        if A[i] + A[j] < target:
            i += 1
        elif A[i] + A[j] > target:
            j -= 1
        else:
            ans += 1
            i += 1
            j -= 1
    return ans
方法一： 三指针
思路和算法

先将数组进行排序，遍历数组下标，对于每个 i，设 T = target - A[i] 作为剩余要凑成的目标数。接着用双指针来完成 A[j] + A[k] == T 的子任务。

考虑到有些元素是重复的，需要小心处理边界条件。在特殊的情况下，比如说 target = 8，数组为 [2,2,2,2,3,3,4,4,4,5,5,5,6,6]，这个数组就有大量的重复元素可以组成 target，下面来分析一下这种情况该怎么处理。

只要 A[j] + A[k] == T，就要算上这一对 j, k 组合。在这个例子里面，当 A[j] == 2，A[k] == 6，有 4 * 2 = 8 种组合方式。

在特殊情况下，如果 A[j] == A[k]，比如最后剩下的 [4,4,4]，这里有 3 对。一般情况下，如果 A[j] == A[k]，我们有 \binom{M}{2} = \frac{M*(M-1)}{2}( 
2
M
​	
 )= 
2
M∗(M−1)
​	
  对 (j,k)（满足 j < k 且 A[j] + A[k] == T）。

JavaPython
class Solution {
    public int threeSumMulti(int[] A, int target) {
        int MOD = 1_000_000_007;
        long ans = 0;
        Arrays.sort(A);

        for (int i = 0; i < A.length; ++i) {
            // We'll try to find the number of i < j < k
            // with A[j] + A[k] == T, where T = target - A[i].

            // The below is a "two sum with multiplicity".
            int T = target - A[i];
            int j = i+1, k = A.length - 1;

            while (j < k) {
                // These steps proceed as in a typical two-sum.
                if (A[j] + A[k] < T)
                    j++;
                else if (A[j] + A[k] > T)
                    k--;
                else if (A[j] != A[k]) {  // We have A[j] + A[k] == T.
                    // Let's count "left": the number of A[j] == A[j+1] == A[j+2] == ...
                    // And similarly for "right".
                    int left = 1, right = 1;
                    while (j+1 < k && A[j] == A[j+1]) {
                        left++;
                        j++;
                    }
                    while (k-1 > j && A[k] == A[k-1]) {
                        right++;
                        k--;
                    }

                    ans += left * right;
                    ans %= MOD;
                    j++;
                    k--;
                } else {
                    // M = k - j + 1
                    // We contributed M * (M-1) / 2 pairs.
                    ans += (k-j+1) * (k-j) / 2;
                    ans %= MOD;
                    break;
                }
            }
        }

        return (int) ans;
    }
}
复杂度分析

时间复杂度： O(N^2)O(N 
2
 )，其中 NN 为 A 的长度。

空间复杂度： O(1)O(1)。

方法二： 数学法
思路和算法

设 count[x] 为数组 A 中 x 出现的次数。对于每种 x+y+z == target，可以数一下有多少种可能的组合，这里可以看几个例子：

如果 x，y，z 各不相同，有 count[x] * count[y] * count[z] 中组合。

如果 x == y != z，有 \binom{\text{count[x]}}{2} * \text{count[z]}( 
2
count[x]
​	
 )∗count[z] 种组合。

如果 x != y == z，有 \text{count[x]} * \binom{\text{count[y]}}{2}count[x]∗( 
2
count[y]
​	
 ) 种组合。

如果 x == y == z，有 \binom{\text{count[x]}}{3}( 
3
count[x]
​	
 ) 中组合。

(\binom{n}{k}( 
k
n
​	
 ) 表示二项式系数 \frac{n!}{(n-k)!k!} 
(n−k)!k!
n!
​	
 .)

JavaPython
class Solution {
    public int threeSumMulti(int[] A, int target) {
        int MOD = 1_000_000_007;
        long[] count = new long[101];
        for (int x: A)
            count[x]++;

        long ans = 0;

        // All different
        for (int x = 0; x <= 100; ++x)
            for (int y = x+1; y <= 100; ++y) {
                int z = target - x - y;
                if (y < z && z <= 100) {
                    ans += count[x] * count[y] * count[z];
                    ans %= MOD;
                }
            }

        // x == y != z
        for (int x = 0; x <= 100; ++x) {
            int z = target - 2*x;
            if (x < z && z <= 100) {
                ans += count[x] * (count[x] - 1) / 2 * count[z];
                ans %= MOD;
            }
        }

        // x != y == z
        for (int x = 0; x <= 100; ++x) {
            if (target % 2 == x % 2) {
                int y = (target - x) / 2;
                if (x < y && y <= 100) {
                    ans += count[x] * count[y] * (count[y] - 1) / 2;
                    ans %= MOD;
                }
            }
        }

        // x == y == z
        if (target % 3 == 0) {
            int x = target / 3;
            if (0 <= x && x <= 100) {
                ans += count[x] * (count[x] - 1) * (count[x] - 2) / 6;
                ans %= MOD;
            }
        }

        return (int) ans;
    }
}
复杂度分析

时间复杂度： O(N + W^2)O(N+W 
2
 )，其中 NN 为 A 的长度，WW 为 A[i] 中最大的数。

空间复杂度： O(W)O(W)。

方法三： 变种的三数之和
思路和算法那

在 方法二 中，count[x] 为 A 中 x 出现的次数。同时，让 keys 为数组 A 中所有元素只出现一次的有序数组。接着用三数之和的方法来处理 keys。

举个例子，如果 A = [1,1,2,2,3,3,4,4,5,5]，target = 8，得到 keys = [1,2,3,4,5]。当对 keys 做三数之和的时候，会遇到一些组合使得三数相加为 target，比如 (x,y,z) = (1,2,5), (1,3,4), (2,2,4), (2,3,3)。接着用 count 来算每种组合有多少次。

JavaPython
class Solution {
    public int threeSumMulti(int[] A, int target) {
        int MOD = 1_000_000_007;

        // Initializing as long saves us the trouble of
        // managing count[x] * count[y] * count[z] overflowing later.
        long[] count = new long[101];
        int uniq = 0;
        for (int x: A) {
            count[x]++;
            if (count[x] == 1)
                uniq++;
        }

        int[] keys = new int[uniq];
        int t = 0;
        for (int i = 0; i <= 100; ++i)
            if (count[i] > 0)
                keys[t++] = i;

        long ans = 0;
        // Now, let's do a 3sum on "keys", for i <= j <= k.
        // We will use count to add the correct contribution to ans.

        for (int i = 0; i < keys.length; ++i) {
            int x = keys[i];
            int T = target - x;
            int j = i, k = keys.length - 1;
            while (j <= k) {
                int y = keys[j], z = keys[k];
                if (y + z < T) {
                    j++;
                } else if (y + z > T) {
                    k--;
                } else {  // # x+y+z == T, now calc the size of the contribution
                    if (i < j && j < k) {
                        ans += count[x] * count[y] * count[z];
                    } else if (i == j && j < k) {
                        ans += count[x] * (count[x] - 1) / 2 * count[z];
                    } else if (i < j && j == k) {
                        ans += count[x] * count[y] * (count[y] - 1) / 2;
                    } else {  // i == j == k
                        ans += count[x] * (count[x] - 1) * (count[x] - 2) / 6;
                    }

                    ans %= MOD;
                    j++;
                    k--;
                }
            }
        }

        return (int) ans;
    }
}
复杂度分析

时间复杂度： O(N^2)O(N 
2
 )，其中 NN 是 A 的长度。

空间复杂度： O(N)O(N)。
 
*/
