using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定整数数组 A，每次 move 操作将会选择任意 A[i]，并将其递增 1。

返回使 A 中的每个值都是唯一的最少操作次数。

示例 1:

输入：[1,2,2]
输出：1
解释：经过一次 move 操作，数组将变为 [1, 2, 3]。
示例 2:

输入：[3,2,1,2,1,7]
输出：6
解释：经过 6 次 move 操作，数组将变为 [3, 4, 1, 2, 5, 7]。
可以看出 5 次或 5 次以下的 move 操作是不能让数组的每个值唯一的。
提示：

0 <= A.length <= 40000
0 <= A[i] < 40000
*/
/// <summary>
/// https://leetcode-cn.com/problems/minimum-increment-to-make-array-unique/
/// 945. 使数组唯一的最小增量
/// 
/// </summary>
class MinimumIncrementToMakeArrayUniqueSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int MinIncrementForUnique(int[] A)
    {
        const int Step1 = 40000;
        ushort[] count = new ushort[Step1];
        foreach (int a in A) count[a]++;

        int ret = 0;
        int takenCount = 0;
        int index = 0;
        for (; index < Step1; ++index)
        {
            var c = count[index];
            if ( 1 < c )
            {
                int taken = c - 1;
                takenCount += taken;
                ret -= index * taken;
            }
            else if ( 0 < takenCount && c == 0)
            {
                takenCount--;
                ret += index;
            }
        }

        //for (; 0 < takenCount; ++index)
        //{
        //    takenCount--;
        //    ret += index;
        //}


        //if( 0 < takenCount ) ret += (index + index + takenCount - 1) * takenCount / 2;

        if (0 < takenCount)
        {
            ret += index * takenCount;
            ret += (takenCount - 1) * takenCount / 2;
        }

        return ret;
    }
}
/*
方法一：计数
分析

由于 A[i] 的范围为 [0, 40000)，我们可以用数组统计出每个范围中的数出现的次数，然后对于每个重复出现的数，我们暴力地将它递增，直到它增加到一个没有重复出现的数为止。但这样的方法的时间复杂度较大，可以达到 O(N^2)O(N 
2
 )，例如数组 A 中所有元素都是 1 的情况。

因此，我们不能过于暴力地对重复出现的数进行递增，而是用以下的做法：当我们找到一个没有出现过的数的时候，将之前某个重复出现的数增加到这个没有出现过的数的值。注意，这里“之前某个重复出现的数”是可以任意选择的，它并不会影响最终的答案，因为将 P 增加到 X 并且将 Q 增加到 Y 与将 P 增加到 Y 并且将 Q 增加到 X 都需要进行 (X + Y) - (P + Q) 次操作。

例如当数组 A 为 [1, 1, 1, 1, 3, 5] 时，我们发现有 3 个重复的 1，且没有出现过 2，4 和 6，因此一共需要进行 (2 + 4 + 6) - (1 + 1 + 1) = 9 次操作。

算法

首先统计出范围中每个数出现的次数，然后对于范围中的每个数 x：

如果 x 出现了两次以上，就将额外出现的数记录下来（例如保存到一个列表中）；

如果 x 没有出现过，那么在记录下来的数中选取一个 v，将它增加到 x，需要进行的操作次数为 x - v。

在下面的 Java 代码中，我们对该算法进行了优化，使得我们不需要将额外出现的数记录下来。还是以 [1, 1, 1, 1, 3, 5] 为例，当我们发现有 3 个重复的 1 时，我们先将操作次数减去 1 + 1 + 1。接下来，当我们发现 2，4 和 6 都没有出现过时，我们依次将操作次数增加 2，4 和 6。这种优化方法在方法二中也被使用。

JavaPython
class Solution {
    public int minIncrementForUnique(int[] A) {
        int[] count = new int[100000];
        for (int x: A) count[x]++;

        int ans = 0, taken = 0;

        for (int x = 0; x < 100000; ++x) {
            if (count[x] >= 2) {
                taken += count[x] - 1;
                ans -= x * (count[x] - 1);
            }
            else if (taken > 0 && count[x] == 0) {
                taken--;
                ans += x;
            }
        }

        return ans;
    }
}
复杂度分析

时间复杂度：O(L)O(L)，其中 LL 的数量级是数组 A 的长度加上其数据范围，因为在最坏情况下，数组 A 中的所有数都是数据范围的最大值。

空间复杂度：O(L)O(L)。

方法二：排序
分析

我们可以将数组先进行排序，再使用方法一中的优化方法。

算法

将数组排完序后，我们对数组进行线性扫描，会有两种情况：

如果 A[i - 1] == A[i]，我们将操作次数减去 A[i]，并将重复的数的个数增加 1；

如果 A[i - 1] < A[i]，我们就可以将之前重复的数放入区间 (A[i - 1], A[i]) 中。设当前重复的数的个数为 taken，我们可以放入 give = min(taken, A[i] - A[i - 1] - 1) 个数，它们的和为

A[i-1] * \mathrm{give} + (\sum_{k=1}^{\mathrm{give}})
A[i−1]∗give+( 
k=1
∑
give
​	
 )

JavaPython
class Solution {
    public int minIncrementForUnique(int[] A) {
        Arrays.sort(A);
        int ans = 0, taken = 0;

        for (int i = 1; i < A.length; ++i) {
            if (A[i-1] == A[i]) {
                taken++;
                ans -= A[i];
            } else {
                int give = Math.min(taken, A[i] - A[i-1] - 1);
                ans += give * (give + 1) / 2 + give * A[i-1];
                taken -= give;
            }
        }

        if (A.length > 0)
            ans += taken * (taken + 1) / 2 + taken * A[A.length - 1];

        return ans;
    }
}
复杂度分析

时间复杂度：O(N\log N)O(NlogN)，其中 NN 是数组 AA 的长度。

空间复杂度：O(N)O(N)。
 
*/
