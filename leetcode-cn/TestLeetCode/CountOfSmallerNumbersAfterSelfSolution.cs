using System.Collections.Generic;
using System;
using System.Linq;

/*
给定一个整数数组 nums，按要求返回一个新数组 counts。数组 counts 有该性质： counts[i] 的值是  nums[i] 右侧小于 nums[i] 的元素的数量。

 

示例：

输入：nums = [5,2,6,1]
输出：[2,1,1,0]
解释：
5 的右侧有 2 个更小的元素 (2 和 1)
2 的右侧仅有 1 个更小的元素 (1)
6 的右侧有 1 个更小的元素 (1)
1 的右侧有 0 个更小的元素
 

提示：

0 <= nums.length <= 10^5
-10^4 <= nums[i] <= 10^4

*/

/// <summary>
/// https://leetcode-cn.com/problems/count-of-smaller-numbers-after-self/
/// 315. 计算右侧小于当前元素的个数
///
///
/// </summary>
internal class CountOfSmallerNumbersAfterSelfSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public IList<int> CountSmaller(int[] nums)
    {
        int n = nums.Length;
        var index = new int[n];
        var temp = new int[n];
        var tempIndex = new int[n];
        var ret = new int[n];

        for (int i = 0; i < n; ++i) index[i] = i;

        MergeSort(nums, 0, n - 1);

        return ret;

        void MergeSort(int[] a, int l, int r)
        {
            if (r <= l) return;

            int mid = (l + r) / 2;
            MergeSort(a, l, mid);
            MergeSort(a, mid + 1, r);
            Merge(a, l, mid, r);
        }

        void Merge(int[] a, int l, int mid, int r)
        {
            int i = l, j = mid + 1, p = l;
            while (i <= mid && j <= r)
            {
                if (a[i] <= a[j])
                {
                    temp[p] = a[i];
                    tempIndex[p] = index[i];
                    ret[index[i]] += (j - mid - 1);
                    ++i;
                    ++p;
                }
                else
                {
                    temp[p] = a[j];
                    tempIndex[p] = index[j];
                    ++j;
                    ++p;
                }
            }

            while (i <= mid)
            {
                temp[p] = a[i];
                tempIndex[p] = index[i];
                ret[index[i]] += (j - mid - 1);
                ++i;
                ++p;
            }

            while (j <= r)
            {
                temp[p] = a[j];
                tempIndex[p] = index[j];
                ++j;
                ++p;
            }

            for (int k = l; k <= r; ++k)
            {
                index[k] = tempIndex[k];
                a[k] = temp[k];
            }
        }
    }
}

/*
计算右侧小于当前元素的个数
力扣官方题解
发布于 2020-07-10
32.0k
方法一：离散化树状数组
预备知识

「树状数组」是一种可以动态维护序列前缀和的数据结构，它的功能是：

单点更新 update(i, v)： 把序列 ii 位置的数加上一个值 vv，在该题中 v = 1v=1
区间查询 query(i)： 查询序列 [1 \cdots i][1⋯i] 区间的区间和，即 ii 位置的前缀和
修改和查询的时间代价都是 O(\log n)O(logn)，其中 nn 为需要维护前缀和的序列的长度。

思路与算法

记题目给定的序列为 aa，我们规定 a_ia 
i
​	
  的取值集合为 aa 的「值域」。我们用桶来表示值域中的每一个数，桶中记录这些数字出现的次数。假设a = \{5, 5, 2, 3, 6\}a={5,5,2,3,6}，那么遍历这个序列得到的桶是这样的：


index  ->  1 2 3 4 5 6 7 8 9
value  ->  0 1 1 0 2 1 0 0 0
转化为动态维护前缀和问题

记 value 序列为 vv，我们可以看出它第 i - 1i−1 位的前缀和表示「有多少个数比 ii 小」。那么我们可以从后往前遍历序列 aa，记当前遍历到的元素为 a_ia 
i
​	
 ，我们把 a_ia 
i
​	
  对应的桶的值自增 11，记 a_i = pa 
i
​	
 =p，把 vv 序列 p - 1p−1 位置的前缀和加入到答案中算贡献。为什么这么做是对的呢，因为我们在循环的过程中，我们把原序列分成了两部分，后半部部分已经遍历过（已入桶），前半部分是待遍历的（未入桶），那么我们求到的 p - 1p−1 位置的前缀和就是「已入桶」的元素中比 pp 小的元素的个数总和。这种动态维护前缀和的问题我们可以用「树状数组」来解决。

用离散化优化空间

我们显然可以用数组来实现这个桶，可问题是如果 a_ia 
i
​	
  中有很大的元素，比如 10^910 
9
 ，我们就要开一个大小为 10^910 
9
  的桶，内存中是存不下的。这个桶数组中很多位置是 00，有效位置是稀疏的，我们要想一个办法让有效的位置全聚集到一起，减少无效位置的出现，这个时候我们就需要用到一个方法——离散化。离散化的方法有很多，但是目的是一样的，即把原序列的值域映射到一个连续的整数区间，并保证它们的偏序关系不变。 这里我们将原数组去重后排序，原数组每个数映射到去重排序后这个数对应位置的下标，我们称这个下标为这个对应数字的 \rm idid。已知数字获取 \rm idid 可以在去重排序后的数组里面做二分查找，已知 \rm idid 获取数字可以直接把 \rm idid 作为下标访问去重排序数组的对应位置。大家可以参考代码和图来理解这个过程。



其实，计算每个数字右侧小于当前元素的个数，这个问题我们在 「剑指 Offer 51. 数组中的逆序对」 题解的「方法二：离散化树状数组」中遇到过，在统计逆序对的时候，只需要统计每个位置右侧小于当前元素的个数，再对它们求和，就可以得到逆序对的总数。这道逆序对的题可以作为本题的补充练习。

代码如下。

代码


public class Solution 
{
    private int[] c;

    private int[] a;

    private void Init(int length)
    {
        c = new int[length];
        Array.Fill(c, 0);
    }

    private int LowBit(int x)
    {
        return x & (-x);
    }

    private void Discretization(int[] nums)
    {
        a = (int[])nums.Clone();
        var hashSet = new HashSet<int>(a);
        a = hashSet.ToArray();
        Array.Sort(a);
    }

    private int GetId(int x)
    {
        return Array.BinarySearch(a, x) + 1;
    }

	private int Query(int pos)
    {
        int ret = 0;
        while (pos > 0)
        {
            ret += c[pos];
            pos -= LowBit(pos);
        }

        return ret;
    }
	
	private void Update(int pos)
    {
        while (pos < c.Length)
        {
            c[pos] += 1;
            pos += LowBit(pos);
        }
    }
	
    public IList<int> CountSmaller(int[] nums) 
    {
        var resultList = new List<int>(); 

        Discretization(nums);

        Init(nums.Length + 5);

        for (int i = nums.Length - 1; i >= 0; --i)
        {
            var id = GetId(nums[i]);
            resultList.Add(Query(id - 1));
            Update(id);
        }

        resultList.Reverse();

        return resultList;
    }
}
复杂度分析

假设题目给出的序列长度为 nn。

时间复杂度：我们梳理一下这个算法的流程，这里离散化使用哈希表去重，然后再对去重的数组进行排序，时间代价为 O(n \log n)O(nlogn)；初始化树状数组的时间代价是 O(n)O(n)；通过值获取离散化 \rm idid 的操作单次时间代价为 O(\log n)O(logn)；对于每个序列中的每个元素，都会做一次查询 \rm idid、单点修改和前缀和查询，总的时间代价为 O(n \log n)O(nlogn)。故渐进时间复杂度为 O(n \log n)O(nlogn)。
空间复杂度：这里用到的离散化数组、树状数组、哈希表的空间代价都是 O(n)O(n)，故渐进空间复杂度为 O(n)O(n)。
方法二：归并排序
预备知识

这里假设读者已经知道如何使用归并排序的方法计算序列的逆序对数，如果读者还不知道的话可以参考 「剑指 Offer 51. 数组中的逆序对」 的官方题解哦。

思路与算法

我们发现「离散化树状数组」的方法几乎于 「剑指 Offer 51. 数组中的逆序对」 中的完全相同，那么我们可不可以借鉴逆序对问题中的归并排序的方法呢？

我们还是要在「归并排序」的「并」中做文章。我们通过一个实例来看看。假设我们有两个已排序的序列等待合并，分别是 L = \{ 8, 12, 16, 22, 100 \}L={8,12,16,22,100} 和 R = \{ 7, 26, 55, 64, 91 \}R={7,26,55,64,91}。一开始我们用指针 lPtr = 0 指向 LL 的头部，rPtr = 0 指向 RR 的头部。记已经合并好的部分为 MM。


L = [8, 12, 16, 22, 100]   R = [7, 26, 55, 64, 91]  M = []
     |                          |
   lPtr                       rPtr
我们发现 lPtr 指向的元素大于 rPtr 指向的元素，于是把 rPtr 指向的元素放入答案，并把 rPtr 后移一位。


L = [8, 12, 16, 22, 100]   R = [7, 26, 55, 64, 91]  M = [7]
     |                              |
    lPtr                          rPtr
接着我们继续合并：


L = [8, 12, 16, 22, 100]   R = [7, 26, 55, 64, 91]  M = [8, 9]
        |                          |
       lPtr                       rPtr
此时 lPtr 比 rPtr 小，把 lPtr 对应的数加入答案。如果我们要统计 88 的右边比 88 小的元素，这里 77 对它做了一次贡献。如果带合并的序列 L = \{ 8, 12, 16, 22, 100 \}L={8,12,16,22,100}，R = \{ 7, 7, 7, 26, 55, 64, 91\}R={7,7,7,26,55,64,91}，那么一定有一个时刻，lPtr 和 rPtr 分别指向这些对应的位置：


L = [8, 12, 16, 22, 100]   R = [7, 7, 7, 26, 55, 64, 91]  M = [7, 7, 7]
     |                                   |
    lPtr                                rPtr
下一步我们就是把 88 加入 MM 中，此时三个 77 对 88 的右边比 88 小的元素的贡献为 33。以此类推，我们可以一边合并一边计算 RR 的头部到 rPtr 前一个数字对当前 lPtr 指向的数字的贡献。

我们发现用这种「算贡献」的思想在合并的过程中计算逆序对的数量的时候，只在 lPtr 右移的时候计算，是基于这样的事实：当前 lPtr 指向的数字比 rPtr 小，但是比 RR 中 [0 ... rPtr - 1] 的其他数字大，[0 ... rPtr - 1] 的数字是在 lPtr 右边但是比 lPtr 对应数小的数字，贡献为这些数字的个数。

但是我们又遇到了新的问题，在「并」的过程中 88 的位置一直在发生改变，我们应该把计算的贡献保存到哪里呢？这个时候我们引入一个新的数组，来记录每个数字对应的原数组中的下标，例如：


    a = [8, 9, 1, 5, 2]
index = [0, 1, 2, 3, 4]
排序的时候原数组和这个下标数组同时变化，则排序后我们得到这样的两个数组：


    a = [1, 2, 5, 8, 9]
index = [2, 4, 3, 0, 1]
我们用一个数组 ans 来记录贡献。我们对某个元素计算贡献的时候，如果它对应的下标为 p，我们只需要在 ans[p] 上加上贡献即可。

大家可以参考代码来理解这个过程。

代码


public class Solution {
    private int[] index;

    private int[] temp;

    private int[] tempIndex;

    private int[] ans;

    public void Merge(int[] a, int l, int mid, int r)
    {
        int i = l, j = mid + 1, p = l;
        while (i <= mid && j <= r)
        {
            if (a[i] <= a[j])
            {
                temp[p] = a[i];
                tempIndex[p] = index[i];
                ans[index[i]] += (j - mid - 1);
                ++i;
                ++p;
            }
            else 
            {
                temp[p] = a[j];
                tempIndex[p] = index[j];
                ++j;
                ++p;
            }
        }

        while (i <= mid) 
        {
            temp[p] = a[i];
            tempIndex[p] = index[i];
            ans[index[i]] += (j - mid - 1);
            ++i;
            ++p;
        }

        while (j <= r)
        {
            temp[p] = a[j];
            tempIndex[p] = index[j];
            ++j;
            ++p;
        }

        for (int k = l; k <= r; ++k)
        {
            index[k] = tempIndex[k];
            a[k] = temp[k];
        }
    }

    public void MergeSort(int[] a, int l, int r)
    {
        if (l >= r) 
        {
            return;
        }

        int mid = (l + r) >> 1;
        MergeSort(a, l, mid);
        MergeSort(a, mid + 1, r);
        Merge(a, l, mid, r);
    }

    public IList<int> CountSmaller(int[] nums) {
        this.index = new int[nums.Length];
        this.temp = new int[nums.Length];
        this.tempIndex = new int[nums.Length];
        this.ans = new int[nums.Length];

        for (int i = 0; i < nums.Length; ++i)
        {
            index[i] = i;
        }

        int l = 0, r = nums.Length - 1;
        MergeSort(nums, l, r);

        return new List<int>(ans);
    }
}
复杂度分析

时间复杂度：O(n \log n)O(nlogn)，即归并排序的时间复杂度。
空间复杂度：O(n)O(n)，这里归并排序的临时数组、下标映射数组以及答案数组的空间代价均为 O(n)O(n)。

public class Solution {
    int[] aux;
    int[] index;
    int[] tmpIndex;
    int[] ans;
    public IList<int> CountSmaller(int[] nums) {
        index = new int[nums.Length];
        for (int i = 0; i < nums.Length; i++) {
            index[i] = i;
        }
        aux = new int[nums.Length];
        ans = new int[nums.Length];
        tmpIndex = new int[nums.Length];
        MergeSort(nums, 0, nums.Length - 1);
        return ans;
    }

    // 逆序对参考https://leetcode-cn.com/problems/shu-zu-zhong-de-ni-xu-dui-lcof/
    void MergeSort(int[] nums, int left, int right) {
        if (left >= right) return;
        int mid = (left + right) / 2;
        MergeSort(nums, left, mid);
        MergeSort(nums, mid + 1, right);
        int i = mid, j = right, k = right;
        while (i >= left || j > mid) {
            if (i < left) {
                tmpIndex[k] = index[j];
                aux[k--] = nums[j--];
            } else if (j <= mid) {
                tmpIndex[k] = index[i];
                aux[k--] = nums[i--];
            } else {
                if (nums[i] > nums[j]) {
                    ans[index[i]] += j - mid;
                    tmpIndex[k] = index[i];
                    aux[k--] = nums[i--];
                } else {
                    tmpIndex[k] = index[j];
                    aux[k--] = nums[j--];
                }
            }
        }
        for (int n = left; n <= right; n++) {
            index[n] = tmpIndex[n];
            nums[n] = aux[n];
        }
    }
}

public class Solution {
    private static int[] c;
    private static int[] a;
    private void Init(int length)
    {
        c=new int[length];
        Array.Fill(c,0);
    }
    private int LowBit(int x)
    {
        return x & (-x);
    }
    private void Update(int pos)
    {
        while(pos<c.Length)
        {
            c[pos]+=1;
            pos+=LowBit(pos);
        }
    }
    private int Query(int pos)
    {
        int ret=0;
        while(pos > 0)
        {
            ret+=c[pos];
            pos-=LowBit(pos);
        }
        return ret;
    }
    private void Discretization(int[] nums)
    {
        a=(int[])nums.Clone();
        var hashSet = new HashSet<int>(a);
        a = hashSet.ToArray();
        Array.Sort(a);
    }
    private int GetId(int x)
    {
        return Array.BinarySearch(a, x) + 1;
    }

    public IList<int> CountSmaller(int[] nums) {
        var resultList = new List<int>();
        Discretization(nums);
        Init(nums.Length + 5);
        for(int i=nums.Length-1;i>=0;--i)
        {
            var id=GetId(nums[i]);
            resultList.Add(Query(id-1));
            Update(id);
        }
        resultList.Reverse();
        return resultList;
    }
}

public class Solution {
	class BSTNode
	{
		public int val;        // 节点值
		public int leftCount;  // 标记当前节点左端的节点数（即比val小的个数）
		public BSTNode left;   // 左节点
		public BSTNode right;  // 右节点
		public BSTNode(int val)
		{
			this.val = val;
		}
	}

	private BSTNode treeroot;
	private void BST_insert(BSTNode root, int val, ref int count)
	{
		if (val < root.val)
		{
			root.leftCount++;
			if (root.left != null)
			{
				BST_insert(root.left, val, ref count);
			}
			else
			{
				root.left = new BSTNode(val);
			}
		}
		else
		{
			count += (root.val == val) ? root.leftCount : root.leftCount + 1;
			if (root.right != null)
			{
				BST_insert(root.right, val, ref count);
			}
			else
			{
				root.right = new BSTNode(val);
			}
		}
	}

	public IList<int> CountSmaller(int[] nums)
	{
		if (nums.Length == 0)
		{
			return nums;
		}
		int length = nums.Length;
		int[] result = new int[length];
		treeroot = new BSTNode(nums[length - 1]);
		for (int i = length - 2; i >= 0; --i)
		{
			int count = 0;
			BST_insert(treeroot, nums[i], ref count);
			result[i] = count;
		}
		return result;
	}
}

public class Solution {
    private int[] BIT;
    private Dictionary<int, int> indices;

    public IList<int> CountSmaller(int[] nums) {
        var sorted_nums = new SortedSet<int>(nums).ToArray();
        this.BIT = new int[sorted_nums.Length+1];
        this.indices = new Dictionary<int, int>(sorted_nums.Length);
        for (int i = 0; i < sorted_nums.Length; ++i) {
            this.indices.Add(sorted_nums[i], i+1);
        }

        for (int i = nums.Length - 1; i >= 0; --i) {
            this.Add(nums[i]);
            nums[i] = this.CountSmaller(nums[i]);
        }
        return nums.ToList();
    }

    private void Add(int num) {
        int i = this.indices[num];
        while (i < this.BIT.Length) {
            ++this.BIT[i];
            i += i & -i;
        }
    }

    private int CountSmaller(int num) {
        int i = this.indices[num] - 1;
        int sum = 0;
        while (i > 0) {
            sum += this.BIT[i];
            i -= i & -i;
        }
        return sum;
    }
}

public class Solution {
    public IList<int> CountSmaller(int[] nums)
        {
            var result = new List<int>();
            var length = nums.Length;
            if (length == 0)
            {
                return result;
            }

            TreeNode root = null;
            for (var i = length - 1; i >= 0; i--)
            {
                root = InsertToBinarySearchTree(root, nums[i], 0, result);
            }

            return result;
        }

        private TreeNode InsertToBinarySearchTree(TreeNode node, int value, int smallerCount, List<int> result)
        {
            if (node == null)
            {
                result.Insert(0, smallerCount);
                return new TreeNode(value);
            }

            if (node.Value >= value)
            {
                node.LeftChildrenCount++;
                node.Left = InsertToBinarySearchTree(node.Left, value, smallerCount, result);
            }

            if (node.Value < value)
            {
                node.Right = InsertToBinarySearchTree(node.Right, value, smallerCount + node.LeftChildrenCount + 1, result);
            }

            return node;
        }

        public class TreeNode
        {
            public TreeNode(int value)
            {
                Value = value;
            }

            public TreeNode Left { get; set; }

            public int LeftChildrenCount { get; set; }

            public TreeNode Right { get; set; }

            public int Value { get; private set; }
        }
}

*/