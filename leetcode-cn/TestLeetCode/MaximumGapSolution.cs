using System;
using System.Linq;

/*
给定一个无序的数组，找出数组在排序之后，相邻元素之间最大的差值。

如果数组元素个数小于 2，则返回 0。

示例 1:

输入: [3,6,9,1]
输出: 3
解释: 排序后的数组是 [1,3,6,9], 其中相邻元素 (3,6) 和 (6,9) 之间都存在最大差值 3。
示例 2:

输入: [10]
输出: 0
解释: 数组元素个数小于 2，因此返回 0。
说明:

你可以假设数组中所有元素都是非负整数，且数值在 32 位有符号整数范围内。
请尝试在线性时间复杂度和空间复杂度的条件下解决此问题。

*/

/// <summary>
/// https://leetcode-cn.com/problems/maximum-gap/
/// 164. 最大间距
///
///
/// </summary>
internal class MaximumGapSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int MaximumGap(int[] nums)
    {
        int len = nums.Length;
        if (nums.Length < 2) return 0;

        int mini = nums.Min(),
            maxi = nums.Max();

        int bucketSize = Math.Max(1, (maxi - mini) / (len - 1));
        int bucketNum = (maxi - mini) / bucketSize + 1;
        Bucket[] buckets = new Bucket[bucketNum];

        foreach (var num in nums)
        {
            int bucketIdx = (num - mini) / bucketSize;
            var b = buckets[bucketIdx];
            if (b == null) buckets[bucketIdx] = b = new Bucket();
            b.minval = Math.Min(num, b.minval);
            b.maxval = Math.Max(num, b.maxval);
        }

        int prevBucketMax = mini, maxGap = 0;
        foreach (var bucket in buckets)
        {
            if (bucket == null) continue;

            maxGap = Math.Max(maxGap, bucket.minval - prevBucketMax);
            prevBucketMax = bucket.maxval;
        }

        return maxGap;
    }

    public class Bucket
    {
        public int minval = int.MaxValue;
        public int maxval = int.MinValue;
    };
}

/*

最大间距
力扣 (LeetCode)
发布于 2019-06-28
15.0k
方法 1：比较排序
想法

按照题意实现。

算法

将整个数组排序后，遍历数组找到相邻元素间的最大间距。


int maximumGap(vector<int>& nums)
{
    if (nums.empty() || nums.size() < 2)            // check if array is empty or small sized
        return 0;

    sort(nums.begin(), nums.end());                 // sort the array

    int maxGap = 0;

    for (int i = 0; i < nums.size() - 1; i++)
        maxGap = max(nums[i + 1] - nums[i], maxGap);

    return maxGap;
}
复杂度分析

时间复杂度：O(n\log n)O(nlogn)。排序的复杂度是 O(n\log n)O(nlogn)，遍历的复杂度是 O(n)O(n)，总复杂度是 O(n \log n)O(nlogn)。

空间复杂度：除去输入数组之外，不需要额外空间（因为大多数都是原地排序）。

方法 2：基数排序
算法

这个方法与第一种方法相似，不过我们基于基数排序而非传统的比较排序。


int maximumGap(vector<int>& nums)
{
    if (nums.empty() || nums.size() < 2)
        return 0;

    int maxVal = *max_element(nums.begin(), nums.end());

    int exp = 1;                                 // 1, 10, 100, 1000 ...
    int radix = 10;                              // base 10 system

    vector<int> aux(nums.size());

    while (maxVal / exp > 0) {                   // Go through all digits from LSD to MSD
        vector<int> count(radix, 0);

        for (int i = 0; i < nums.size(); i++)    // Counting sort
            count[(nums[i] / exp) % 10]++;

        for (int i = 1; i < count.size(); i++)   // you could also use partial_sum()
            count[i] += count[i - 1];

        for (int i = nums.size() - 1; i >= 0; i--)
            aux[--count[(nums[i] / exp) % 10]] = nums[i];

        for (int i = 0; i < nums.size(); i++)
            nums[i] = aux[i];

        exp *= 10;
    }

    int maxGap = 0;

    for (int i = 0; i < nums.size() - 1; i++)
        maxGap = max(nums[i + 1] - nums[i], maxGap);

    return maxGap;
}
复杂度分析

时间复杂度：O(d \cdot (n + k)) \approx O(n)O(d?(n+k))≈O(n)

由于在数组上的线性迭代是接近线性复杂度，所以方法的时间性能瓶颈只要是基数排序。

基数排序以计数排序为基础。

计数排序时间复杂度是 O(n+k)O(n+k)，其中 kk 是数组 nn 个元素的基数（数字个数）。如果 k \leq O(n)k≤O(n)，计数排序可以在线性时间内完成。在我们的例子中，基数是固定的（比如，k = 10k=10），因此计数排序运行时间是线性的 O(n)O(n)。
基数排序运行 dd 轮计数排序（其中每个元素由最多 dd 个数字组成）。因此有效运行时间是 O(d \cdot (n + k))O(d?(n+k))，但在我们的例子中，最大可能的 32 位有符号是 2147483647，因此 d \leq 10d≤10 是常数。
因此基数排序的时间效率是 O(n)O(n)。

空间复杂度：O(n + k) \approx O(n)O(n+k)≈O(n)额外空间。

计数排序需要额外 O(k)O(k) 空间，基数排序需要一个和输入数组相同大小的辅助数组。然而给定的 kk 是一个固定小常数，所以在大输入情况下计数排序的额外空间是可以被忽略的。

方法 3：桶和鸽笼原理
想法

对整个数组排序的代价很大，最坏情况下需要让每个元素都和其他所有元素比较。

如果我们不需要比较所有元素对呢？如果我们将元素分类，比如说用桶，这个想法将是可能的。我们只需要比较这些桶即可。

题外话：鸽笼原理

鸽笼原理描述说，nn 个物品放入 mm 个容器中，如果 n > mn>m 那么一定有一个容器装有至少两个物品。

假设对于数组中的任意一个元素都有一个桶，那么每个元素恰好占据一个桶。现在减少桶的个数，必然会有一些桶包含超过一个元素。

现在讨论元素之间的间距。考虑最好情况，假设元素排好序且两两之间间距相同。这意味着任意相邻元素都有恒定的差值。所以 nn 个元素有 n-1n?1 个间距，假设为 tt，显然可以得到 t = (max - min)/(n-1)t=(max?min)/(n?1)，其中 maxmax 和 minmin 是数组中最大和最小的元素。这个间距就是相邻元素间最大间距，也就是我们要的答案。

显然，tt 是具有相同数量（nn）和相同区间（max - minmax?min）的数组中，都可以满足的最小值。证明：假设从一个相等间距的数组出发，改变相邻量元素的间距，假设将 arr[i-1]arr[i?1] 和 arr[i]arr[i] 之间的间距变成 t - pt?p，那么 arr[i]arr[i] 和 arr[i+1]arr[i+1] 之间的间距就增长为 t+pt+p。因此最大间距就从 tt 变成了 t+pt+p，因此最大间距 tt 只会增加。

桶！

回到我们的问题，我们已经了解了鸽笼原理的应用，那么如果我们用桶来代替单独元素作比较，比较的次数会减小，因为桶中可能有多个元素。这并不能马上解决完这个问题。如果在桶中比较元素？那问题将会得到很好解决。

所以现在的想法是：如果我们只需要在桶之间相互比较，而不用比较桶内的元素，看起来会非常理想。这也将解决排序问题：只需要将元素分配到合适的桶中，因为桶已经有序，所以我们只需要比较桶，不需要将所有元素排序并比较了。

说明

以下是一些说明：

桶的大小是相同的嘛？

是的，他们大小都为 bb。

那么桶之间的间距也是固定的嘛？

是的，桶之间的间距是 1。这意味着两个大小为 3 的相邻桶分别代表的区间是 3 - 63?6 和 4 - 74?7。不会出现重叠。

为什么说两个相邻桶之间可能出现最大间距？

桶的大小也就是桶的容积，是桶可以容纳的最大区间范围。然而桶内的区间范围取决于桶内最大元素和最小元素的差值。例如一个大小为 55 的桶包含值域 6-106?10，它保存了元素 7,8,97,8,9 那么实际容积就是 (9 - 7) + 1 = 3(9?7)+1=3 与桶的大小不相等。

如何比较相邻两个桶？

我们比较实际范围，也就是前一个桶的最大元素和后一个桶的最小元素。比如说，两个大小为 55 的桶，分别保存元素 [1,2,3][1,2,3] 和 [9,10][9,10]，那么桶之间的间距就是 9-3=69?3=6（大于任意一个桶的大小）。

是否还要再比较一次元素？！

是的，需要！但只需要比较两倍桶个数的元素（每个桶的最大最小元素）。如果按照上面的做法，你会发现当选择了合适的桶大小时，比较次数远远小于数组中实际元素个数。

算法

选择合适的桶大小 bb 满足 1 < b \leq (max - min)/(n-1)1<b≤(max?min)/(n?1)。设 b = \lfloor (max - min)/(n-1) \rfloorb=?(max?min)/(n?1)?。
所有 nn 个元素被分为 k = \lceil (max - min)/b \rceilk=?(max?min)/b? 个桶。
因此第 ii 个桶保存的值区间为：\bigg [min + (i-1) * b, \space min + i*b \bigg )[min+(i?1)?b, min+i?b)（下标从 1 开始）。
显然很容易计算出每个元素属于哪个桶，\lfloor (num - min)/b \rfloor?(num?min)/b?（下标从 0 开始）其中 numnum 是元素的值。
当所有 nn 个元素都遍历过后，比较 k-1k?1 个相邻桶找到最大间距。

class Bucket {
public:
    bool used = false;
    int minval = numeric_limits<int>::max();        // same as INT_MAX
    int maxval = numeric_limits<int>::min();        // same as INT_MIN
};

int maximumGap(vector<int>& nums)
{
    if (nums.empty() || nums.size() < 2)
        return 0;

    int mini = *min_element(nums.begin(), nums.end()),
        maxi = *max_element(nums.begin(), nums.end());

    int bucketSize = max(1, (maxi - mini) / ((int)nums.size() - 1));        // bucket size or capacity
    int bucketNum = (maxi - mini) / bucketSize + 1;                         // number of buckets
    vector<Bucket> buckets(bucketNum);

    for (auto&& num : nums) {
        int bucketIdx = (num - mini) / bucketSize;                          // locating correct bucket
        buckets[bucketIdx].used = true;
        buckets[bucketIdx].minval = min(num, buckets[bucketIdx].minval);
        buckets[bucketIdx].maxval = max(num, buckets[bucketIdx].maxval);
    }

    int prevBucketMax = mini, maxGap = 0;
    for (auto&& bucket : buckets) {
        if (!bucket.used)
            continue;

        maxGap = max(maxGap, bucket.minval - prevBucketMax);
        prevBucketMax = bucket.maxval;
    }

    return maxGap;
}
复杂度分析

时间复杂度：O(n + b) \approx O(n)O(n+b)≈O(n)。

线性遍历一遍数组中的元素，复杂度为 O(n)O(n)。找到桶之间的最大间距需要线性遍历一遍所有的桶，复杂度为 O(b)O(b)。所以总复杂度是线性的。

空间复杂度：O(2 \cdot b) \approx O(b)O(2?b)≈O(b) 的额外空间。

每个桶只需要存储最大和最小元素，因此额外空间和桶个数线性相关。

下一篇：Java从 比较排序 到 计数排序 再到 桶排序！详细思路+注解！

public class Solution {
    public int MaximumGap(int[] nums) {
        int _len = nums.Length;
        if (_len <= 1){
            return 0;
        } else {
            Array.Sort(nums);
            int res = 0;
            for (int i=0;i<_len-1;i++){
                int tp = nums[i+1] - nums[i];
                if (tp > res){
                    res = tp;
                }
            }
            return res;
        }

    }
}


*/