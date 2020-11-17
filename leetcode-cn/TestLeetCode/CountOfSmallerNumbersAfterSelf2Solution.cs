using System.Collections.Generic;
using System;
using System.Linq;

/*
在数组中的两个数字，如果前面一个数字大于后面的数字，则这两个数字组成一个逆序对。输入一个数组，求出这个数组中的逆序对的总数。

 

示例 1:

输入: [7,5,6,4]
输出: 5

*/

/// <summary>
/// https://leetcode-cn.com/problems/count-of-smaller-numbers-after-self/
/// 315. 计算右侧小于当前元素的个数
/// https://leetcode-cn.com/problems/shu-zu-zhong-de-ni-xu-dui-lcof/
/// 剑指 Offer 51. 数组中的逆序对
///
///
/// </summary>
internal class CountOfSmallerNumbersAfterSelf2Solution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int ReversePairs(int[] nums)
    {
        int n = nums.Length;
        var temp = new int[n];
        int ret = 0;

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
                    ret += (j - mid - 1);
                    ++i;
                    ++p;
                }
                else
                {
                    temp[p] = a[j];
                    ++j;
                    ++p;
                }
            }

            while (i <= mid)
            {
                temp[p] = a[i];
                ret += (j - mid - 1);
                ++i;
                ++p;
            }

            while (j <= r)
            {
                temp[p] = a[j];
                ++j;
                ++p;
            }

            for (int k = l; k <= r; ++k) a[k] = temp[k];
        }
    }
}

/*
数组中的逆序对
力扣官方题解
发布于 2020-04-23
70.1k
📺 视频题解

📖 文字题解
方法一：归并排序
预备知识

「归并排序」是分治思想的典型应用，它包含这样三个步骤：

分解： 待排序的区间为 [l, r][l,r]，令 m = \lfloor \frac{l + r}{2} \rfloorm=⌊ 
2
l+r
​	
 ⌋，我们把 [l, r][l,r] 分成 [l, m][l,m] 和 [m + 1, r][m+1,r]
解决： 使用归并排序递归地排序两个子序列
合并： 把两个已经排好序的子序列 [l, m][l,m] 和 [m + 1, r][m+1,r] 合并起来
在待排序序列长度为 11 的时候，递归开始「回升」，因为我们默认长度为 11 的序列是排好序的。

思路

那么求逆序对和归并排序又有什么关系呢？关键就在于「归并」当中「并」的过程。我们通过一个实例来看看。假设我们有两个已排序的序列等待合并，分别是 L = \{ 8, 12, 16, 22, 100 \}L={8,12,16,22,100} 和 R = \{ 9, 26, 55, 64, 91 \}R={9,26,55,64,91}。一开始我们用指针 lPtr = 0 指向 LL 的首部，rPtr = 0 指向 RR 的头部。记已经合并好的部分为 MM。


L = [8, 12, 16, 22, 100]   R = [9, 26, 55, 64, 91]  M = []
     |                          |
   lPtr                       rPtr
我们发现 lPtr 指向的元素小于 rPtr 指向的元素，于是把 lPtr 指向的元素放入答案，并把 lPtr 后移一位。


L = [8, 12, 16, 22, 100]   R = [9, 26, 55, 64, 91]  M = [8]
        |                       |
      lPtr                     rPtr
这个时候我们把左边的 88 加入了答案，我们发现右边没有数比 88 小，所以 88 对逆序对总数的「贡献」为 00。

接着我们继续合并，把 99 加入了答案，此时 lPtr 指向 1212，rPtr 指向 2626。


L = [8, 12, 16, 22, 100]   R = [9, 26, 55, 64, 91]  M = [8, 9]
        |                          |
       lPtr                       rPtr
此时 lPtr 比 rPtr 小，把 lPtr 对应的数加入答案，并考虑它对逆序对总数的贡献为 rPtr 相对 RR 首位置的偏移 11（即右边只有一个数比 1212 小，所以只有它和 1212 构成逆序对），以此类推。

我们发现用这种「算贡献」的思想在合并的过程中计算逆序对的数量的时候，只在 lPtr 右移的时候计算，是基于这样的事实：当前 lPtr 指向的数字比 rPtr 小，但是比 RR 中 [0 ... rPtr - 1] 的其他数字大，[0 ... rPtr - 1] 的其他数字本应当排在 lPtr 对应数字的左边，但是它排在了右边，所以这里就贡献了 rPtr 个逆序对。

利用这个思路，我们可以写出如下代码。

代码


public class Solution {
    public int reversePairs(int[] nums) {
        int len = nums.length;

        if (len < 2) {
            return 0;
        }

        int[] copy = new int[len];
        for (int i = 0; i < len; i++) {
            copy[i] = nums[i];
        }

        int[] temp = new int[len];
        return reversePairs(copy, 0, len - 1, temp);
    }

    private int reversePairs(int[] nums, int left, int right, int[] temp) {
        if (left == right) {
            return 0;
        }

        int mid = left + (right - left) / 2;
        int leftPairs = reversePairs(nums, left, mid, temp);
        int rightPairs = reversePairs(nums, mid + 1, right, temp);

        if (nums[mid] <= nums[mid + 1]) {
            return leftPairs + rightPairs;
        }

        int crossPairs = mergeAndCount(nums, left, mid, right, temp);
        return leftPairs + rightPairs + crossPairs;
    }

    private int mergeAndCount(int[] nums, int left, int mid, int right, int[] temp) {
        for (int i = left; i <= right; i++) {
            temp[i] = nums[i];
        }

        int i = left;
        int j = mid + 1;

        int count = 0;
        for (int k = left; k <= right; k++) {

            if (i == mid + 1) {
                nums[k] = temp[j];
                j++;
            } else if (j == right + 1) {
                nums[k] = temp[i];
                i++;
            } else if (temp[i] <= temp[j]) {
                nums[k] = temp[i];
                i++;
            } else {
                nums[k] = temp[j];
                j++;
                count += (mid - i + 1);
            }
        }
        return count;
    }
}
复杂度

记序列长度为 nn。

时间复杂度：同归并排序 O(n \log n)O(nlogn)。
空间复杂度：同归并排序 O(n)O(n)，因为归并排序需要用到一个临时数组。
方法二：离散化树状数组
预备知识

「树状数组」是一种可以动态维护序列前缀和的数据结构，它的功能是：

单点更新 update(i, v)： 把序列 ii 位置的数加上一个值 vv，这题 v = 1v=1
区间查询 query(i)： 查询序列 [1 \cdots i][1⋯i] 区间的区间和，即 ii 位置的前缀和
修改和查询的时间代价都是 O(\log n)O(logn)，其中 nn 为需要维护前缀和的序列的长度。

思路

记题目给定的序列为 aa，我们规定 a_ia 
i
​	
  的取值集合为 aa 的「值域」。我们用桶来表示值域中的每一个数，桶中记录这些数字出现的次数。假设a = \{5, 5, 2, 3, 6\}a={5,5,2,3,6}，那么遍历这个序列得到的桶是这样的：


index  ->  1 2 3 4 5 6 7 8 9
value  ->  0 1 1 0 2 1 0 0 0
我们可以看出它第 i - 1i−1 位的前缀和表示「有多少个数比 ii 小」。那么我们可以从后往前遍历序列 aa，记当前遍历到的元素为 a_ia 
i
​	
 ，我们把 a_ia 
i
​	
  对应的桶的值自增 11，把 i - 1i−1 位置的前缀和加入到答案中算贡献。为什么这么做是对的呢，因为我们在循环的过程中，我们把原序列分成了两部分，后半部部分已经遍历过（已入桶），前半部分是待遍历的（未入桶），那么我们求到的 i - 1i−1 位置的前缀和就是「已入桶」的元素中比 a_ia 
i
​	
  大的元素的总和，而这些元素在原序列中排在 a_ia 
i
​	
  的后面，但它们本应该排在 a_ia 
i
​	
  的前面，这样就形成了逆序对。

我们显然可以用数组来实现这个桶，可问题是如果 a_ia 
i
​	
  中有很大的元素，比如 10^910 
9
 ，我们就要开一个大小为 10^910 
9
  的桶，内存中是存不下的。这个桶数组中很多位置是 00，有效位置是稀疏的，我们要想一个办法让有效的位置全聚集到一起，减少无效位置的出现，这个时候我们就需要用到一个方法——离散化。

离散化一个序列的前提是我们只关心这个序列里面元素的相对大小，而不关心绝对大小（即只关心元素在序列中的排名）；离散化的目的是让原来分布零散的值聚集到一起，减少空间浪费。那么如何获得元素排名呢，我们可以对原序列排序后去重，对于每一个 a_ia 
i
​	
  通过二分查找的方式计算排名作为离散化之后的值。当然这里也可以不去重，不影响排名。

代码


class Solution {
    public int reversePairs(int[] nums) {
        int n = nums.length;
        int[] tmp = new int[n];
        System.arraycopy(nums, 0, tmp, 0, n);
        // 离散化
        Arrays.sort(tmp);
        for (int i = 0; i < n; ++i) {
            nums[i] = Arrays.binarySearch(tmp, nums[i]) + 1;
        }
        // 树状数组统计逆序对
        BIT bit = new BIT(n);
        int ans = 0;
        for (int i = n - 1; i >= 0; --i) {
            ans += bit.query(nums[i] - 1);
            bit.update(nums[i]);
        }
        return ans;
    }
}

class BIT {
    private int[] tree;
    private int n;

    public BIT(int n) {
        this.n = n;
        this.tree = new int[n + 1];
    }

    public static int lowbit(int x) {
        return x & (-x);
    }

    public int query(int x) {
        int ret = 0;
        while (x != 0) {
            ret += tree[x];
            x -= lowbit(x);
        }
        return ret;
    }

    public void update(int x) {
        while (x <= n) {
            ++tree[x];
            x += lowbit(x);
        }
    }
}
复杂度

时间复杂度：离散化的过程中使用了时间代价为 O(n \log n)O(nlogn) 的排序，单次二分的时间代价为 O(\log n)O(logn)，一共有 nn 次，总时间代价为 O(n \log n)O(nlogn)；循环执行 nn 次，每次进行 O(\log n)O(logn) 的修改和 O(\log n)O(logn) 的查找，总时间代价为 O(n \log n)O(nlogn)。故渐进时间复杂度为 O(n \log n)O(nlogn)。
空间复杂度：树状数组需要使用长度为 nn 的数组作为辅助空间，故渐进空间复杂度为 O(n)O(n)。

public class Solution {

    public void swap(int[] nums,int a ,int b){
        var tmp = nums[a];
        nums[a] = nums[b];
        nums[b] = tmp;
    }

    public int ReversePairs(int[] nums) {
        if(nums == null || nums.Length<=1){
            return 0;
        }

        var result = 0;
        // 归并排序
        var tmp = new int[nums.Length];
        var baseCount = 1;
        
        while(baseCount<nums.Length){
            int i = 0;
            while(i<nums.Length){
                int startIndex = i;
                int firstStart = i;
                int firstEnd = i+baseCount;
                firstEnd = firstEnd>nums.Length?nums.Length:firstEnd;
                int secondStart = i+baseCount;
                secondStart = secondStart>nums.Length?nums.Length:secondStart;
                int secondEnd = i+baseCount+baseCount;
                secondEnd = secondEnd>nums.Length?nums.Length:secondEnd;

                while(firstStart<firstEnd && secondStart<secondEnd){
                    if(nums[firstStart]>nums[secondStart]){
                        result += secondEnd-secondStart;
                        tmp[startIndex] = nums[firstStart];
                        startIndex++;
                        firstStart++;
                    }else{
                        tmp[startIndex] = nums[secondStart];
                        startIndex++;
                        secondStart++;
                    }
                }

                while(firstStart<firstEnd){
                    tmp[startIndex] = nums[firstStart];
                    startIndex++;
                    firstStart++;
                }

                while(secondStart<secondEnd){
                    tmp[startIndex] = nums[secondStart];
                    startIndex++;
                    secondStart++;
                }

                i+=2*baseCount;
            }
            
            var t = tmp;
            tmp = nums;
            nums = t;

            baseCount = baseCount*2;
        }
        return result;
    }
}

public class Solution {
    int[] aux;
    int ans = 0;
    public int ReversePairs(int[] nums) {
        aux = new int[nums.Length];
        MergeSort(nums, 0, nums.Length - 1);
        return ans;
    }

    void MergeSort(int[] nums, int left, int right) {
        if (left >= right) return;
        int mid = (left + right) / 2;
        MergeSort(nums, left, mid);
        MergeSort(nums, mid + 1, right);
        int i = mid, j = right, k = right;
        while (i >= left || j > mid) {
            if (i < left) {
                aux[k--] = nums[j--];
            } else if (j <= mid) {
                aux[k--] = nums[i--];
            } else {
                if (nums[i] > nums[j]) {
                    ans += j - mid;
                    aux[k--] = nums[i--];
                } else {
                    // ans += i - left + 1; // 前面的数字大于后面的数字才能组成逆序对
                    aux[k--] = nums[j--];
                }
            }
        }
        for (int n = left; n <= right; n++) {
            nums[n] = aux[n];
        }
    }
}



*/