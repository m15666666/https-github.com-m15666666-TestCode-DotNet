/*
给定一个已排序的正整数数组 nums，和一个正整数 n 。从 [1, n] 区间内选取任意个数字补充到 nums 中，使得 [1, n] 区间内的任何数字都可以用 nums 中某几个数字的和来表示。请输出满足上述要求的最少需要补充的数字个数。

示例 1:

输入: nums = [1,3], n = 6
输出: 1
解释:
根据 nums 里现有的组合 [1], [3], [1,3]，可以得出 1, 3, 4。
现在如果我们将 2 添加到 nums 中， 组合变为: [1], [2], [3], [1,3], [2,3], [1,2,3]。
其和可以表示数字 1, 2, 3, 4, 5, 6，能够覆盖 [1, 6] 区间里所有的数。
所以我们最少需要添加一个数字。
示例 2:

输入: nums = [1,5,10], n = 20
输出: 2
解释: 我们需要添加 [2, 4]。
示例 3:

输入: nums = [1,2,2], n = 5
输出: 0

*/

/// <summary>
/// https://leetcode-cn.com/problems/patching-array/
/// 330. 按要求补齐数组
///
///
///
///
/// </summary>
internal class PatchingArraySolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int MinPatches(int[] nums, int n)
    {
        long miss = 1;
        int ret = 0;
        int i = 0;
        int len = nums.Length;
        while (miss <= n)
        {
            if (i < len && nums[i] <= miss)
            {
                miss += nums[i++];
                continue;
            }
            miss += miss;
            ret++;
        }
        return ret;
    }
}

/*
按要求补齐数组
力扣 (LeetCode)
发布于 2019-07-10
5.2k
直觉

对于任何缺少的数字，如果我们想要让和能覆盖到它，我们必须添加至少一个小于或等于该数字的数字。否则，将无法覆盖到。想想象你要给一个人 xx 分钱的零钱，但你没有足够的硬币。你肯定会需要面额小于或者等于 xx 的硬币。

算法

假设 miss 是缺少的数字中最小的，则区间 [1, miss) (左闭右开) 已经被完全覆盖。为了覆盖 miss，我们需要添加某些小于等于 miss 的数字。否则将不可能覆盖到。

例如，数组 nums = [1,2,3,8]， n = 16。已经覆盖到的数字有区间 [1, 6] 和 [8, 14]。换而言之，7, 15, 16 没有覆盖到。如果你加的数字大于 7，则 7 依然覆盖不到。

假设我们添加的数字是 xx，则区间 [1, miss) 和 [x, x + miss) 均被覆盖到。由于我们知道 x <= miss，这两个区间必然覆盖了区间 [1, x + miss)。我们希望能够尽可能选择大的 xx，这样覆盖的范围就可以尽可能大。因此，最好的选择是 x = miss。

在覆盖到 miss 后，我们可以重新计算覆盖范围，查看新的最小的缺少数字。然后加上该数字。重复操作直到全部数字都被堵盖到。

下面是这个贪心算法的流程：

初始化区间 [1, miss) = [1, 1) = 空
每当 n 没有被覆盖
若当前元素 nums[i] 小于等于 miss
将范围扩展到 [1, miss + nums[i])
将 i 增加 1
否则
将 miss 添加到数组，将范围扩展到 [1, miss + miss)
增加数字的计数
返回增加数字的数目
示例:

nums = [1,2,3,8]，n = 80

迭代次数	miss	覆盖范围	i	nums[i]	增加的数字	备注
0	1	[1, 1)	0	1	0	
1	2	[1, 2)	1	2	0	
2	4	[1, 4)	2	3	0	
3	7	[1, 7)	3	8	0	
4	14	[1, 14)	3	8	1	patch 7
5	22	[1, 22)	4	none	1	
6	44	[1, 44)	4	none	2	patch 22
7	88	[1, 88)	4	none	3	patch 44
正确性

你可能会问，你怎么知道这样做是对的？在本节，我们将证明本贪心算法总会给出正确答案：

引理

若贪婪算法需要增加 kk 个数字来覆盖 [1, n][1,n]，不可能使用少于 kk 个数字来实现。

反证法

对于给定的数字 nn 和数组 \mathrm{nums}nums，假设贪心算法找到的 kk 个补充数字是 X_1 \lt X_2 \lt \ldots \lt X_k \leq nX 
1
​	
 <X 
2
​	
 <…<X 
k
​	
 ≤n。若存在另一个补充数字集合 Y_1 \leq Y_2 \leq \ldots \leq Y_{k'} \leq nY 
1
​	
 ≤Y 
2
​	
 ≤…≤Y 
k 
′
 
​	
 ≤n，其中 k' \lt kk 
′
 <k，则有Y_1 \leq X_1Y 
1
​	
 ≤X 
1
​	
 ，否则 X_1X 
1
​	
  就会覆盖不到。又因为增加 X_1X 
1
​	
  不能覆盖 X_2X 
2
​	
 ，这意味着全部元素的和，包括X_1X 
1
​	
 ， 小于X_2X 
2
​	
 。 因此增加 Y_1Y 
1
​	
  也不能覆盖 X_2X 
2
​	
 。于是有:

Y_2 \leq X_2
Y 
2
​	
 ≤X 
2
​	
 

否则 X_2X 
2
​	
  无法被覆盖，以此类推。

Y_i \leq X_i, i = 1, 2, \ldots k'
Y 
i
​	
 ≤X 
i
​	
 ,i=1,2,…k 
′
 

最终，可以看到由于X_1, X_2, \ldots X_{k'}X 
1
​	
 ,X 
2
​	
 ,…X 
k 
′
 
​	
  不足以覆盖 X_kX 
k
​	
 ，因此 Y_1, Y_2, \ldots, Y_{k'}Y 
1
​	
 ,Y 
2
​	
 ,…,Y 
k 
′
 
​	
  也不足以覆盖 X_k \leq nX 
k
​	
 ≤n。这与Y_1 \leq Y_2 \leq \ldots \leq Y_{k'} \leq nY 
1
​	
 ≤Y 
2
​	
 ≤…≤Y 
k 
′
 
​	
 ≤n 能覆盖 [1, n][1,n]矛盾。

Q.E.D. 证毕。

因此，贪婪算法将始终返回尽可能少的补充个数。即使对于特定案例，可能有许多不同的方案。但是，没有一个解法的所用个数会比贪婪算法少。


public class Solution {
    public int minPatches(int[] nums, int n) {
        int patches = 0, i = 0;
        long miss = 1; // use long to avoid integer overflow error
        while (miss <= n) {
            if (i < nums.length && nums[i] <= miss) // miss is covered
                miss += nums[i++];
            else { // patch miss to the array
                miss += miss;
                patches++; // increase the answer
            }
        }
        return patches;
    }
}
复杂度分析

时间复杂度 : O(m + \log n)O(m+logn)。在每次迭代中，我们或者增加 i ，或者将 miss 加倍。 i增加的总数为 mm， miss 加倍的总数为 \log nlogn。

空间复杂度 : O(1)O(1)。 只需要三个变量：patches，i 和 miss。



*/