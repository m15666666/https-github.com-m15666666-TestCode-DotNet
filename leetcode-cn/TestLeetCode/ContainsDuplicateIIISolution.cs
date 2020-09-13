using System;
using System.Collections.Generic;

/*
在整数数组 nums 中，是否存在两个下标 i 和 j，使得 nums [i] 和 nums [j] 的差的绝对值小于等于 t ，且满足 i 和 j 的差的绝对值也小于等于 ķ 。

如果存在则返回 true，不存在返回 false。

 

示例 1:

输入: nums = [1,2,3,1], k = 3, t = 0
输出: true
示例 2:

输入: nums = [1,0,1,1], k = 1, t = 2
输出: true
示例 3:

输入: nums = [1,5,9,1,5,9], k = 2, t = 3
输出: false

*/

/// <summary>
/// https://leetcode-cn.com/problems/contains-duplicate-iii/
/// 220. 存在重复元素 III
/// 给定一个整数数组，判断数组中是否有两个不同的索引 i 和 j，
/// 使得 nums [i] 和 nums [j] 的差的绝对值最大为 t，并且 i 和 j 之间的差的绝对值最大为 ķ。
/// 示例 1:
/// 输入: nums = [1,2,3,1], k = 3, t = 0
/// 输出: true
/// 示例 2:
/// 输入: nums = [1,0,1,1], k = 1, t = 2
/// 输出: true
/// 示例 3:
/// 输入: nums = [1,5,9,1,5,9], k = 2, t = 3
/// 输出: false
/// </summary>
internal class ContainsDuplicateIIISolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public bool ContainsNearbyAlmostDuplicate(int[] nums, int k, int t)
    {
        if (t < 0) return false;
        var d = new Dictionary<long, long>();
        long w = (long)t + 1;
        for (int i = 0; i < nums.Length; ++i)
        {
            long m = GetBucketID(nums[i], w);
            if (d.ContainsKey(m)) return true;
            if (d.ContainsKey(m - 1) && Math.Abs(nums[i] - d[m - 1]) < w)
                return true;
            if (d.ContainsKey(m + 1) && Math.Abs(nums[i] - d[m + 1]) < w)
                return true;
            d[m] = (long)nums[i];
            if (i >= k) d.Remove(GetBucketID(nums[i - k], w));
        }
        return false;

        static long GetBucketID(long x, long w)
        {
            return x < 0 ? ((x + 1) / w) - 1 : x / w;
        }
    }

    //public bool ContainsNearbyAlmostDuplicate(int[] nums, int k, int t)
    //{
    //    if (t == 0) return ContainsNearbyDuplicate(nums, k);
    //    int length = nums.Length;
    //    int lengthMinusOne = length - 1;
    //    for ( int index = 0; index < lengthMinusOne; index++)
    //    {
    //        int lastIndex = Math.Min(index + k, lengthMinusOne);
    //        long v = nums[index];
    //        for (int j = index + 1; j <= lastIndex; j++)
    //        {
    //            if (Math.Abs(v - nums[j]) <= t) return true;
    //        }
    //    }
    //    return false;
    //}

    //private static bool ContainsNearbyDuplicate(int[] nums, int k)
    //{
    //    if (nums == null || nums.Length < 2 || k < 1) return false;

    //    Dictionary<int, int> num2Index = new Dictionary<int, int>();
    //    int index = -1;
    //    foreach (var num in nums)
    //    {
    //        index++;
    //        if (!num2Index.ContainsKey(num))
    //        {
    //            num2Index.Add(num, index);
    //            continue;
    //        }
    //        if (index - num2Index[num] <= k) return true;
    //        num2Index[num] = index;
    //    }
    //    return false;
    //}
}

/*

存在重复元素 III
力扣 (LeetCode)
发布于 2019-06-24
28.4k
概述
这篇文章是为中级读者准备的，文章中会介绍了以下几种方法：
二叉搜索树，散列表和桶。

方法一 （线性搜索） 【超时】
思路

将每个元素与它之前的 kk 个元素比较，查看它们的数值之差是不是在 tt 以内。

算法

解决这个问题需要找到一组满足以下条件的 ii 和 jj：

\bigl| i-j \bigr| \le k 
∣
∣
∣
​	
 i−j 
∣
∣
∣
​	
 ≤k
\bigl| \mathrm{nums}[i] - \mathrm{nums}[j] \bigr| \le t 
∣
∣
∣
​	
 nums[i]−nums[j] 
∣
∣
∣
​	
 ≤t
我们需要维护了一个kk大小的滑动窗口。这种情况下，第一个条件始终是满足的，只需要通过线性搜索来检查第二个条件是否满足就可以了。


public boolean containsNearbyAlmostDuplicate(int[] nums, int k, int t) {
    for (int i = 0; i < nums.length; ++i) {
        for (int j = Math.max(i - k, 0); j < i; ++j) {
            if (Math.abs(nums[i] - nums[j]) <= t) return true;
        }
    }
    return false;
}
// Time limit exceeded.
复杂度分析

时间复杂度：O(n \min(k,n))O(nmin(k,n))
每次搜索都将花费 O(\min(k, n))O(min(k,n)) 的时间，需要注意的是一次搜索中我们最多比较 nn 次，哪怕 kk 比 nn 大。

空间复杂度：O(1)O(1)
额外开辟的空间为常数个

方法二 （二叉搜索树） 【通过】
思路

如果窗口中维护的元素是有序的，只需要用二分搜索检查条件二是否是满足的就可以了。
利用自平衡二叉搜索树，可以在对数时间内通过 插入 和 删除 来对滑动窗口内元素排序。
算法

方法一真正的瓶颈在于检查第二个条件是否满足需要扫描滑动窗口中所有的元素。因此我们需要考虑的是有没有比全扫描更好的方法。

如果窗口内的元素是有序的，那么用两次二分搜索就可以找到 x+tx+t 和 x-tx−t 这两个边界值了。

然而不幸的是，窗口中的元素是无序的。这里有一个初学者非常容易犯的错误，那就是将滑动窗口维护成一个有序的数组。虽然在有序数组中 搜索 只需要花费对数时间，但是为了让数组保持有序，我们不得不做插入和删除的操作，而这些操作是非常不高效的。想象一下，如果你有一个kk大小的有序数组，当你插入一个新元素xx的时候。虽然可以在O(\log k)O(logk)时间内找到这个元素应该插入的位置，但最后还是需要O(k)O(k)的时间来将xx插入这个有序数组。因为必须得把当前元素应该插入的位置之后的所有元素往后移一位。当你要删除一个元素的时候也是同样的道理。在删除了下标为ii的元素之后，还需要把下标ii之后的所有元素往前移一位。因此，这种做法并不会比方法一更好。

为了能让算法的效率得到真正的提升，我们需要引入一个支持 插入，搜索，删除 操作的 动态 数据结构，那就是自平衡二叉搜索树。自平衡 这个词的意思是，这个树在随机进行插入,删除操作之后，它会自动保证树的高度最小。为什么一棵树需要自平衡呢？这是因为在二叉搜索树上的大部分操作需要花费的时间跟这颗树的高度直接相关。可以看一下下面这棵严重左偏的非平衡二叉搜索树。


            6
           /
          5
         /
        4
       /
      3
     /
    2
   /
  1
图 1. 一个严重左偏的非平衡二叉搜索树。

在上面这棵二叉搜索树上查找一个元素需要花费 线性 时间复杂度，这跟在链表中搜索的速度是一样的。现在我们来比较一下下面这棵平衡二叉搜索树。


          4
        /   \
       2     6
      / \   /
     1   3  5
图2. 一颗平衡的二叉搜索树

假设这棵树上节点总数为 nn，一个平衡树能把高度维持在 h = \log nh=logn。因此这棵树上支持在 O(h) = O(\log n)O(h)=O(logn) 时间内完成 插入，搜索，删除 操作。

下面给出整个算法的伪代码：

初始化一颗空的二叉搜索树 set
对于每个元素xx，遍历整个数组
在 set 上查找大于等于xx的最小的数，如果s - x \leq ts−x≤t则返回 true
在 set 上查找小于等于xx的最大的数，如果x - g \leq tx−g≤t则返回 true
在 set 中插入xx
如果树的大小超过了kk, 则移除最早加入树的那个数。
返回 false
我们把大于等于 xx 的最小的数 ss 当做 xx 在 BST 中的后继节点。同样的，我们能把小于等于 xx 最大的数 gg 当做 xx 在 BST 中的前继节点。ss 和 gg 这两个数是距离 xx 最近的数。因此只需要检查它们和 xx 的距离就能知道条件二是否满足了。


public boolean containsNearbyAlmostDuplicate(int[] nums, int k, int t) {
    TreeSet<Integer> set = new TreeSet<>();
    for (int i = 0; i < nums.length; ++i) {
        // Find the successor of current element
        Integer s = set.ceiling(nums[i]);
        if (s != null && s <= nums[i] + t) return true;

        // Find the predecessor of current element
        Integer g = set.floor(nums[i]);
        if (g != null && nums[i] <= g + t) return true;

        set.add(nums[i]);
        if (set.size() > k) {
            set.remove(nums[i - k]);
        }
    }
    return false;
}
复杂度分析

时间复杂度：O(n \log (\min(n,k)))O(nlog(min(n,k)))
我们需要遍历这个 nn 长度的数组。对于每次遍历，在 BST 中 搜索，插入 或者 删除 都需要花费 O(\log \min(k, n))O(logmin(k,n)) 的时间。

空间复杂度：O(\min(n,k))O(min(n,k))
空间复杂度由 BST 的大小决定，其大小的上限由 kk 和 nn 共同决定。

笔记

当数组中的元素非常大的时候，进行数学运算可能造成溢出。所以可以考虑使用支持大数的数据类型，例如 long。

C++ 中的 std::set，std::set::upper_bound 和 std::set::lower_bound 分别等价于 Java 中的 TreeSet，TreeSet::ceiling 和 TreeSet::floor。Python 标准库不提供自平衡 BST。

方法三 （桶） 【通过】
思路

受 桶排序 的启发，我们可以把 桶 当做窗口来实现一个线性复杂度的解法。

算法

桶排序是一种把元素分散到不同桶中的排序算法。接着把每个桶再独立地用不同的排序算法进行排序。桶排序的概览如下所示：

在上面的例子中，我们有 8 个未排序的整数。我们首先来创建五个桶，这五个桶分别包含 [0,9], [10,19], [20, 29], [30, 39], [40, 49][0,9],[10,19],[20,29],[30,39],[40,49] 这几个区间。这 8 个元素中的任何一个元素都在一个桶里面。对于值为 xx 的元素来说，它所属桶的标签为 x/wx/w，在这里我们让 w = 10w=10。对于每个桶我们单独用其他排序算法进行排序，最后按照桶的顺序收集所有的元素就可以得到一个有序的数组了。

回到这个问题，我们尝试去解决的最大的问题在于：

对于给定的元素 xx, 在窗口中是否有存在区间 [x-t, x+t][x−t,x+t] 内的元素？
我们能在常量时间内完成以上判断嘛？
我们不妨把把每个元素当做一个人的生日来考虑一下吧。假设你是班上新来的一位学生，你的生日在 三月 的某一天，你想知道班上是否有人生日跟你生日在 t=30t=30 天以内。在这里我们先假设每个月都是3030天，很明显，我们只需要检查所有生日在 二月，三月，四月 的同学就可以了。

之所以能这么做的原因在于，我们知道每个人的生日都属于一个桶，我们把这个桶称作月份！每个桶所包含的区间范围都是 tt，这能极大的简化我们的问题。很显然，任何不在同一个桶或相邻桶的两个元素之间的距离一定是大于 tt 的。

我们把上面提到的桶的思想应用到这个问题里面来，我们设计一些桶，让他们分别包含区间 ..., [0,t], [t+1, 2t+1], ......,[0,t],[t+1,2t+1],...。我们把桶来当做窗口，于是每次我们只需要检查 xx 所属的那个桶和相邻桶中的元素就可以了。终于，我们可以在常量时间解决在窗口中搜索的问题了。

还有一件值得注意的事，这个问题和桶排序的不同之处在于每次我们的桶里只需要包含最多一个元素就可以了，因为如果任意一个桶中包含了两个元素，那么这也就是意味着这两个元素是 足够接近的 了，这时候我们就直接得到答案了。因此，我们只需使用一个标签为桶序号的散列表就可以了。


public class Solution {
    // Get the ID of the bucket from element value x and bucket width w
    // In Java, `-3 / 5 = 0` and but we need `-3 / 5 = -1`.
    private long getID(long x, long w) {
        return x < 0 ? (x + 1) / w - 1 : x / w;
    }

    public boolean containsNearbyAlmostDuplicate(int[] nums, int k, int t) {
        if (t < 0) return false;
        Map<Long, Long> d = new HashMap<>();
        long w = (long)t + 1;
        for (int i = 0; i < nums.length; ++i) {
            long m = getID(nums[i], w);
            // check if bucket m is empty, each bucket may contain at most one element
            if (d.containsKey(m))
                return true;
            // check the nei***or buckets for almost duplicate
            if (d.containsKey(m - 1) && Math.abs(nums[i] - d.get(m - 1)) < w)
                return true;
            if (d.containsKey(m + 1) && Math.abs(nums[i] - d.get(m + 1)) < w)
                return true;
            // now bucket m is empty and no almost duplicate in nei***or buckets
            d.put(m, (long)nums[i]);
            if (i >= k) d.remove(getID(nums[i - k], w));
        }
        return false;
    }
}
复杂度分析

时间复杂度：O(n)O(n)
对于这 nn 个元素中的任意一个元素来说，我们最多只需要在散列表中做三次 搜索，一次 插入 和一次 删除。这些操作是常量时间复杂度的。因此，整个算法的时间复杂度为 O(n)O(n)。

空间复杂度：O(\min(n, k))O(min(n,k))
需要开辟的额外空间取决了散列表的大小，其大小跟它所包含的元素数量成线性关系。散列表的大小的上限同时由 nn 和 kk 决定。因此，空间复杂度为 O(\min(n, k))O(min(n,k))。

public class Solution {
    public bool ContainsNearbyAlmostDuplicate(int[] nums, int k, int t) {
            if (t < 0) return false;
            int length = nums.Length;
            if (length == 0 || length == 1) return false;
            Hashtable htb = new Hashtable();
            int head = 0;
            int tail = k < length - 1 ? k : length - 1;
            for (int i = head; i <= tail; i++)
            {
                if (IsContain(htb, nums[i], t)) return true;
                else { htb.Add(nums[i], 0); }
            }
            tail++;
            while (tail < length)
            {
                htb.Remove(nums[head]);
                if (IsContain(htb, nums[tail], t)) return true;
                else
                {
                    htb.Add(nums[tail], 0);
                }
                tail++; head++;
            }
            return false;
    }
            public bool IsContain(Hashtable htb,int tail,int t)
        {
            if (t == 0)
            {
                return htb.ContainsKey(tail);
            }
            foreach(int k in htb.Keys)
            {
                if ((long)tail - (long)k <= (long)t && (long)tail - (long)k >= (long)-t)
                {
                    return true;
                }
            }
            return false;
        }
}
public class Solution {
    public bool ContainsNearbyAlmostDuplicate(int[] nums, int k, int t) {
        if(nums == null || nums.Count()<2 || k < 1 || t<0)
            return false;
        var ss = new SortedSet<long>();
        for(int i = 0; i<nums.Count(); i++)
        {
            if(i>k)
            {
                ss.Remove(nums[i-k-1]);
            }
            if(ss.GetViewBetween((long)nums[i]-t, (long)nums[i]+t).Count()>0)
                return true;
            ss.Add(nums[i]);
        }
        return false;
    }
}
public class Solution {
    public bool ContainsNearbyAlmostDuplicate(int[] nums, int k, int t) {
        if(t < 0 || k < 1) return false;
        Dictionary<long,long> dict = new Dictionary<long,long>();

        for(int i = 0; i < nums.Length; ++i){
            int bucket = getBucket(nums[i], t);
            if(dict.ContainsKey(bucket)
              || (dict.ContainsKey(bucket - 1) && Math.Abs(dict[bucket - 1] - nums[i]) <= t)
              || (dict.ContainsKey(bucket + 1) && Math.Abs(dict[bucket + 1] - nums[i]) <= t)){
                return true;
            }

            dict[bucket] = nums[i];
            if(dict.Count >= k + 1){
                dict.Remove(getBucket(nums[i - (k)], t));
            }
        }

        return false;
    }

    private int getBucket(int val, int t) {
        int bucket = val / (t + 1);
        if(val < 0){
            bucket -= 1;
        }
        return bucket;
    }
}
*/