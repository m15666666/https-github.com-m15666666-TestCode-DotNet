using System;
using System.Collections.Generic;

/*
给定长度分别为 m 和 n 的两个数组，其元素由 0-9 构成，表示两个自然数各位上的数字。现在从这两个数组中选出 k (k <= m + n) 个数字拼接成一个新的数，要求从同一个数组中取出的数字保持其在原数组中的相对顺序。

求满足该条件的最大数。结果返回一个表示该最大数的长度为 k 的数组。

说明: 请尽可能地优化你算法的时间和空间复杂度。

示例 1:

输入:
nums1 = [3, 4, 6, 5]
nums2 = [9, 1, 2, 5, 8, 3]
k = 5
输出:
[9, 8, 6, 5, 3]
示例 2:

输入:
nums1 = [6, 7]
nums2 = [6, 0, 4]
k = 5
输出:
[6, 7, 6, 0, 4]
示例 3:

输入:
nums1 = [3, 9]
nums2 = [8, 9]
k = 3
输出:
[9, 8, 9]
通过次数6,161提交次数18,944

*/

/// <summary>
/// https://leetcode-cn.com/problems/create-maximum-number/
/// 321. 拼接最大数
///
///
/// </summary>
internal class CreateMaximumNumberSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int[] MaxNumber(int[] nums1, int[] nums2, int k)
    {
        int m = nums1.Length;
        int n = nums2.Length;
        int[] ret = new int[k];
        if(m + n == k)
        {
            Merge(nums1, m, nums2, n, ret);
            return ret;
        }

        int[] mergeCache = new int[k];
        int[] cache1 = new int[k];
        int[] cache2 = new int[k];
        Stack<int> stack = new Stack<int>(Math.Max(m, n));

        for (int count1 = 0; count1 <= k; count1++)
        {
            int count2 = k - count1;
            if (count1 <= m && count2 <= n)
            {
                if(0 < count1) PickMax(nums1, count1, cache1);
                if(0 < count2) PickMax(nums2, count2, cache2);
                Merge(cache1, count1, cache2, count2, mergeCache);
                if (CompareTo(mergeCache, 0, ret, 0) == 1) (ret, mergeCache) = (mergeCache, ret);
            }
        }
        return ret;

        void PickMax(int[] nums, int remainCount, int[] cache)
        {
            var drop = nums.Length - remainCount;
            foreach (var num in nums)
            {
                while (0 < drop && 0 < stack.Count && stack.Peek() < num)
                {
                    stack.Pop();
                    drop -= 1;
                }
                stack.Push(num);
            }
            while (remainCount < stack.Count) stack.Pop();
            while (-1 < --remainCount) cache[remainCount] = stack.Pop();
        }

        static void Merge(int[] a, int aLen, int[] b, int bLen, int[] mergeCache)
        {
            int aIndex = 0;
            int bIndex = 0;
            int index = 0;
            while (aIndex < aLen && bIndex < bLen)
            {
                var va = a[aIndex];
                var vb = b[bIndex];
                if (va < vb)
                {
                    mergeCache[index++] = vb;
                    bIndex++;
                    continue;
                }
                if (vb < va)
                {
                    mergeCache[index++] = va;
                    aIndex++;
                    continue;
                }

                switch( CompareTo(a, aIndex, b, bIndex) )
                {
                    case 0:
                    case 1:
                        mergeCache[index++] = va;
                        aIndex++;
                        break;
                    case -1:
                        mergeCache[index++] = vb;
                        bIndex++;
                        break;
                }
            }
            while (aIndex < aLen) mergeCache[index++] = a[aIndex++];
            while (bIndex < bLen) mergeCache[index++] = b[bIndex++];
        }
        static int CompareTo(int[] a, int aIndex, int[] b, int bIndex)
        {
            int m = a.Length;
            int n = b.Length;
            int i = 0;
            while (aIndex < m && bIndex < n)
            {
                int va = a[aIndex++];
                int vb = b[bIndex++];
                if (va < vb) return -1;
                else if (vb < va) return 1;
            }
            if( aIndex == m && bIndex == n ) return 0;
            if( aIndex == m) return -1;
            return 1;
        }
        //static int CompareTo(int[] a, int[] b)
        //{
        //    for (int i = 0; i < a.Length; i++)
        //        if (a[i] < b[i]) return -1;
        //        else if (b[i] < a[i]) return 1;
        //    return 0;
        //}
    }
}

/*
一招吃遍力扣四道题，妈妈再也不用担心我被套路啦～
lucifer
发布于 2020-06-21
5.1k
我花了几天时间，从力扣中精选了四道相同思想的题目，来帮助大家解套，如果觉得文章对你有用，记得点赞分享，让我看到你的认可，有动力继续做下去。

这就是接下来要给大家讲的四个题，其中 1081 和 316 题只是换了说法而已。

316. 去除重复字母(困难)
321. 拼接最大数(困难)
402. 移掉 K 位数字(中等)
1081. 不同字符的最小子序列（中等）
402. 移掉 K 位数字（中等）
我们从一个简单的问题入手，识别一下这种题的基本形式和套路，为之后的三道题打基础。

题目描述

给定一个以字符串表示的非负整数  num，移除这个数中的 k 位数字，使得剩下的数字最小。

注意:

num 的长度小于 10002 且  ≥ k。
num 不会包含任何前导零。


示例 1 :

输入: num = "1432219", k = 3
输出: "1219"
解释: 移除掉三个数字 4, 3, 和 2 形成一个新的最小的数字 1219。
示例 2 :

输入: num = "10200", k = 1
输出: "200"
解释: 移掉首位的 1 剩下的数字为 200. 注意输出不能有任何前导零。
示例 3 :

输入: num = "10", k = 2
输出: "0"
解释: 从原数字移除所有的数字，剩余为空就是 0。

前置知识
数学
思路
这道题让我们从一个字符串数字中删除 k 个数字，使得剩下的数最小。也就说，我们要保持原来的数字的相对位置不变。

以题目中的 num = 1432219， k = 3 为例，我们需要返回一个长度为 4 的字符串，问题在于： 我们怎么才能求出这四个位置依次是什么呢？



（图 1）

暴力法的话，我们需要枚举C_n^(n - k) 种序列（其中 n 为数字长度），并逐个比较最大。这个时间复杂度是指数级别的，必须进行优化。

一个思路是：

从左到右遍历
对于每一个遍历到的元素，我们决定是丢弃还是保留
问题的关键是：我们怎么知道，一个元素是应该保留还是丢弃呢？

这里有一个前置知识：对于两个数 123a456 和 123b456，如果 a > b， 那么数字 123a456 大于 数字 123b456，否则数字 123a456 小于等于数字 123b456。也就说，两个相同位数的数字大小关系取决于第一个不同的数的大小。

因此我们的思路就是：

从左到右遍历
对于遍历到的元素，我们选择保留。
但是我们可以选择性丢弃前面相邻的元素。
丢弃与否的依据如上面的前置知识中阐述中的方法。
以题目中的 num = 1432219， k = 3 为例的图解过程如下：



（图 2）

由于没有左侧相邻元素，因此没办法丢弃。



（图 3）

由于 4 比左侧相邻的 1 大。如果选择丢弃左侧的 1，那么会使得剩下的数字更大（开头的数从 1 变成了 4）。因此我们仍然选择不丢弃。



（图 4）

由于 3 比左侧相邻的 4 小。 如果选择丢弃左侧的 4，那么会使得剩下的数字更小（开头的数从 4 变成了 3）。因此我们选择丢弃。

。。。

后面的思路类似，我就不继续分析啦。

然而需要注意的是，如果给定的数字是一个单调递增的数字，那么我们的算法会永远选择不丢弃。这个题目中要求的，我们要永远确保丢弃 k 个矛盾。

一个简单的思路就是：

每次丢弃一次，k 减去 1。当 k 减到 0 ，我们可以提前终止遍历。
而当遍历完成，如果 k 仍然大于 0。不妨假设最终还剩下 x 个需要丢弃，那么我们需要选择删除末尾 x 个元素。
上面的思路可行，但是稍显复杂。


（图 5）

我们需要把思路逆转过来。刚才我的关注点一直是丢弃，题目要求我们丢弃 k 个。反过来说，不就是让我们保留 n - kn−k 个元素么？其中 n 为数字长度。 那么我们只需要按照上面的方法遍历完成之后，再截取前n - k个元素即可。

按照上面的思路，我们来选择数据结构。由于我们需要保留和丢弃相邻的元素，因此使用栈这种在一端进行添加和删除的数据结构是再合适不过了，我们来看下代码实现。

代码（Python）

class Solution(object):
    def removeKdigits(self, num, k):
        stack = []
        remain = len(num) - k
        for digit in num:
            while k and stack and stack[-1] > digit:
                stack.pop()
                k -= 1
            stack.append(digit)
        return ''.join(stack[:remain]).lstrip('0') or '0'
复杂度分析

时间复杂度：虽然内层还有一个 while 循环，但是由于每个数字最多仅会入栈出栈一次，因此时间复杂度仍然为 O(N)O(N)，其中 NN 为数字长度。
空间复杂度：我们使用了额外的栈来存储数字，因此空间复杂度为 O(N)O(N)，其中 NN 为数字长度。
提示： 如果题目改成求删除 k 个字符之后的最大数，我们只需要将 stack[-1] > digit 中的大于号改成小于号即可。

316. 去除重复字母（困难）
题目描述

给你一个仅包含小写字母的字符串，请你去除字符串中重复的字母，使得每个字母只出现一次。需保证返回结果的字典序最小（要求不能打乱其他字符的相对位置）。

示例 1:

输入: "bcabc"
输出: "abc"
示例 2:

输入: "cbacdcbc"
输出: "acdb"
前置知识
字典序
数学
思路
与上面题目不同，这道题没有一个全局的删除次数 k。而是对于每一个在字符串 s 中出现的字母 c 都有一个 k 值。这个 k 是 c 出现次数 - 1。

沿用上面的知识的话，我们首先要做的就是计算每一个字符的 k，可以用一个字典来描述这种关系，其中 key 为 字符 c，value 为其出现的次数。

具体算法：

建立一个字典。其中 key 为 字符 c，value 为其出现的剩余次数。
从左往右遍历字符串，每次遍历到一个字符，其剩余出现次数 - 1.
对于每一个字符，如果其对应的剩余出现次数大于 1，我们可以选择丢弃（也可以选择不丢弃），否则不可以丢弃。
是否丢弃的标准和上面题目类似。如果栈中相邻的元素字典序更大，那么我们选择丢弃相邻的栈中的元素。
还记得上面题目的边界条件么？如果栈中剩下的元素大于 n - kn−k，我们选择截取前 n - kn−k 个数字。然而本题中的 k 是分散在各个字符中的，因此这种思路不可行的。

不过不必担心。由于题目是要求只出现一次。我们可以在遍历的时候简单地判断其是否在栈上即可。

代码：


class Solution:
    def removeDuplicateLetters(self, s) -> int:
        stack = []
        remain_counter = collections.Counter(s)

        for c in s:
            if c not in stack:
                while stack and c < stack[-1] and  remain_counter[stack[-1]] > 0:
                    stack.pop()
                stack.append(c)
            remain_counter[c] -= 1
        return ''.join(stack)
复杂度分析

时间复杂度：由于判断当前字符是否在栈上存在需要 O(N)O(N) 的时间，因此总的时间复杂度就是 O(N ^ 2)O(N 
2
 )，其中 NN 为字符串长度。
空间复杂度：我们使用了额外的栈来存储数字，因此空间复杂度为 O(N)O(N)，其中 NN 为字符串长度。
查询给定字符是否在一个序列中存在的方法。根本上来说，有两种可能：

有序序列： 可以二分法，时间复杂度大致是 O(N)O(N)。
无序序列： 可以使用遍历的方式，最坏的情况下时间复杂度为 O(N)O(N)。我们也可以使用空间换时间的方式，使用 NN的空间 换取 O(1)O(1)的时间复杂度。
由于本题中的 stack 并不是有序的，因此我们的优化点考虑空间换时间。而由于每种字符仅可以出现一次，这里使用 hashset 即可。

代码（Python）

class Solution:
    def removeDuplicateLetters(self, s) -> int:
        stack = []
        seen = set()
        remain_counter = collections.Counter(s)

        for c in s:
            if c not in seen:
                while stack and c < stack[-1] and  remain_counter[stack[-1]] > 0:
                    seen.discard(stack.pop())
                seen.add(c)
                stack.append(c)
            remain_counter[c] -= 1
        return ''.join(stack)
复杂度分析

时间复杂度：O(N)O(N)，其中 NN 为字符串长度。
空间复杂度：我们使用了额外的栈和 hashset，因此空间复杂度为 O(N)O(N)，其中 NN 为字符串长度。
LeetCode 《1081. 不同字符的最小子序列》 和本题一样，不再赘述。

321. 拼接最大数（困难）
题目描述

给定长度分别为  m  和  n  的两个数组，其元素由  0-9  构成，表示两个自然数各位上的数字。现在从这两个数组中选出 k (k <= m + n)  个数字拼接成一个新的数，要求从同一个数组中取出的数字保持其在原数组中的相对顺序。

求满足该条件的最大数。结果返回一个表示该最大数的长度为  k  的数组。

说明: 请尽可能地优化你算法的时间和空间复杂度。

示例  1:

输入:
nums1 = [3, 4, 6, 5]
nums2 = [9, 1, 2, 5, 8, 3]
k = 5
输出:
[9, 8, 6, 5, 3]
示例 2:

输入:
nums1 = [6, 7]
nums2 = [6, 0, 4]
k = 5
输出:
[6, 7, 6, 0, 4]
示例 3:

输入:
nums1 = [3, 9]
nums2 = [8, 9]
k = 3
输出:
[9, 8, 9]
前置知识
分治
数学
思路
和第一道题类似，只不不过这一次是两个数组，而不是一个，并且是求最大数。

最大最小是无关紧要的，关键在于是两个数组，并且要求从两个数组选取的元素个数加起来一共是 k。

然而在一个数组中取 k 个数字，并保持其最小（或者最大），我们已经会了。但是如果问题扩展到两个，会有什么变化呢？

实际上，问题本质并没有发生变化。 假设我们从 nums1 中取了 k1 个，从 num2 中取了 k2 个，其中 k1 + k2 = k。而 k1 和 k2 这 两个子问题我们是会解决的。由于这两个子问题是相互独立的，因此我们只需要分别求解，然后将结果合并即可。

假如 k1 和 k2 个数字，已经取出来了。那么剩下要做的就是将这个长度分别为 k1 和 k2 的数字，合并成一个长度为 k 的数组合并成一个最大的数组。

以题目的 nums1 = [3, 4, 6, 5] nums2 = [9, 1, 2, 5, 8, 3] k = 5 为例。 假如我们从 num1 中取出 1 个数字，那么就要从 nums2 中取出 4 个数字。

运用第一题的方法，我们计算出应该取 nums1 的 [6]，并取 nums2 的 [9,5,8,3]。 如何将 [6] 和 [9,5,8,3]，使得数字尽可能大，并且保持相对位置不变呢？

实际上这个过程有点类似归并排序中的治，而上面我们分别计算 num1 和 num2 的最大数的过程类似归并排序中的分。


（图 6）

代码：

我们将从 num1 中挑选的 k1 个数组成的数组称之为 A，将从 num2 中挑选的 k2 个数组成的数组称之为 B，


def merge(A, B):
    ans = []
    while A or B:
        bigger = A if A > B else B
        ans.append(bigger[0])
        bigger.pop(0)
    return ans

这里需要说明一下。 在很多编程语言中：如果 A 和 B 是两个数组，当前仅当 A 的首个元素字典序大于 B 的首个元素，A > B 返回 true，否则返回 false。

比如：


A = [1,2]
B = [2]
A < B # True

A = [1,2]
B = [1,2,3]
A < B # False
以合并 [6] 和 [9,5,8,3] 为例，图解过程如下：


（图 7）

具体算法：

从 nums1 中 取 min(i, len(nums1))min(i,len(nums1)) 个数形成新的数组 A（取的逻辑同第一题），其中 i 等于 0,1,2, ... k。
从 nums2 中 对应取 min(j, len(nums2))min(j,len(nums2)) 个数形成新的数组 B（取的逻辑同第一题），其中 j 等于 k - i。
将 A 和 B 按照上面的 merge 方法合并
上面我们暴力了 k 种组合情况，我们只需要将 k 种情况取出最大值即可。
代码（Python）

class Solution:
    def maxNumber(self, nums1, nums2, k):

        def pick_max(nums, k):
            stack = []
            drop = len(nums) - k
            for num in nums:
                while drop and stack and stack[-1] < num:
                    stack.pop()
                    drop -= 1
                stack.append(num)
            return stack[:k]

        def merge(A, B):
            ans = []
            while A or B:
                bigger = A if A > B else B
                ans.append(bigger[0])
                bigger.pop(0)
            return ans

        return max(merge(pick_max(nums1, i), pick_max(nums2, k-i)) for i in range(k+1) if i <= len(nums1) and k-i <= len(nums2))
复杂度分析

时间复杂度：pick_max 的时间复杂度为 O(M + N)O(M+N) ，其中 MM 为 nums1 的长度，NN 为 nums2 的长度。 merge 的时间复杂度为 O(k)O(k)，再加上外层遍历所有的 k 中可能性。因此总的时间复杂度为 O(k^2 * (M + N))O(k 
2
 ∗(M+N))。
空间复杂度：我们使用了额外的 stack 和 ans 数组，因此空间复杂度为 O(max(M, N, k))O(max(M,N,k))，其中 MM 为 nums1 的长度，NN 为 nums2 的长度。
总结
这四道题都是删除或者保留若干个字符，使得剩下的数字最小（或最大）或者字典序最小（或最大）。而解决问题的前提是要有一定数学前提。而基于这个数学前提，我们贪心地删除栈中相邻的字符。如果你会了这个套路，那么这四个题目应该都可以轻松解决。

316. 去除重复字母（困难），我们使用 hashmap 代替了数组的遍历查找，属于典型的空间换时间方式，可以认识到数据结构的灵活使用是多么的重要。背后的思路是怎么样的？为什么想到空间换时间的方式，我在文中也进行了详细的说明，这都是值得大家思考的问题。然而实际上，这些题目中使用的栈也都是空间换时间的思想。大家下次碰到需要空间换取时间的场景，是否能够想到本文给大家介绍的栈和哈希表呢？

321. 拼接最大数（困难）则需要我们能够对问题进行分解，这绝对不是一件简单的事情。但是对难以解决的问题进行分解是一种很重要的技能，希望大家能够通过这道题加深这种分治思想的理解。 大家可以结合我之前写过的几个题解练习一下，它们分别是：

【简单易懂】归并排序（Python）
一文看懂《最大子序列和问题》
更多题解可以访问我的 LeetCode 题解仓库：https://github.com/azl397985856/leetcode 。 目前已经 30K star 啦。

public class Solution
{
    public int[] MaxNumber(int[] a, int[] b, int k)        
    {
        a = TrimArray(a, k);
        b = TrimArray(b, k);
        int n = a.Length;
        int m = b.Length;
        int d = n + m - k;
        if (d == 0) return MergeArray(a, b);

        var ans = new int[k];
        for(int i = 0; i <= d; ++i)
        {
            int j = d - i;
            if (n < i || m < j) continue;
            var na = TrimArray(a, n - i);
            var nb = TrimArray(b, m - j);

            ans = MaxArray(ans, MergeArray(na, nb));
        }
        return ans;
    }

    int[] MergeArray(int[] a, int[] b)
    {
        int n = a.Length;
        int m = b.Length;
        var c = new int[n + m];

        int i = 0;
        int j = 0;
        int k = 0;
        while(i < n && j < m)
        {
            if (a[i] > b[j]) c[k++] = a[i++];
            else if (a[i] < b[j]) c[k++] = b[j++];
            else
            {
                int p = i;
                int q = j;
                while(p < n && q < m && a[p] == b[q]) { p++; q++; }
                if (p < n && q < m) c[k++] = a[p] > b[q] ? a[i++] : b[j++];
                if (p == n) c[k++] = b[j++];
                if (q == m) c[k++] = a[i++];                                                               
            }
        }
        while (i < n) c[k++] = a[i++];
        while (j < m) c[k++] = b[j++];
        return c;
    }

    int[] TrimArray(int[] a, int k)
    {
        int n = a.Length;
        if (n <= k) return a;

        var st = new Stack<int>();
        int d = n - k;
        for (int i = 0; i < n; ++i)
        {
            if (st.Count == 0 || d == 0) st.Push(a[i]);
            else
            {
                while (st.Count > 0 && d > 0 && st.Peek() < a[i]) { st.Pop(); d--; }
                st.Push(a[i]);
            }
        }
        while (d > 0) { st.Pop(); d--; }

        return st.Reverse().ToArray();
    }

    int[] MaxArray(int[] a, int[] b)
    {
        for (int i = 0; i < a.Length; ++i)
        {
            if (a[i] > b[i]) return a;
            if (a[i] < b[i]) return b;
        }
        return a;
    }
}

public class Solution {
    public int[] MaxNumber(int[] nums1, int[] nums2, int k) {
        return PlanA(nums1, nums2, k);
    }

    public int[] PlanA(int[] nums1, int[] nums2, int k) {
        int len1 = nums1.Length;
        int len2 = nums2.Length;
        int [] ans = new int[k];
        System.Console.WriteLine($"len1 = {len1}, len2 = {len2},k = {k}");
        for(var i = Math.Max(0,k - len2); i <= len1 && i <= k ; ++i) {
            int[] res = Merge(MaxArray(nums1,i), MaxArray(nums2,k-i), k);
            if(Greater(res, 0, ans, 0)) {
                ans = res;
            }
        }
        return ans;
    }

    public int[] MaxArray(int [] nums, int k) {
        int [] ans = new int[k];
        int j = 0;
        for(var i = 0; i < nums.Length; ++i) {
            while(j > 0 && ans[j-1] < nums[i] && nums.Length + j - i > k) {
                j --;
            }
            if(j < k) {
                ans[j++] = nums[i];
            }
        }
        return ans;
    }

    public int[] Merge(int[] nums1, int[] nums2, int k) {
        int [] ans = new int[k];
        int index1 = 0;
        int index2 = 0;
        for(var i = 0; i < k; ++i) {
            ans[i] = Greater(nums1, index1, nums2,index2)? nums1[index1++]: nums2[index2 ++];
        }
        return ans;
    }

    public bool Greater(int[] nums1, int i, int[] nums2, int j) {
        while(i < nums1.Length && j < nums2.Length && nums1[i] == nums2[j]) {
            i ++;
            j ++;
        }
        return j == nums2.Length || (i < nums1.Length && nums1[i] > nums2[j]);
    }
}

public class Solution {
    public int[] MaxNumber(int[] nums1, int[] nums2, int k) {
        return PlanA(nums1, nums2, k);
    }

    public int[] PlanA(int[] nums1, int[] nums2, int k) {
        int len1 = nums1.Length;
        int len2 = nums2.Length;
        int [] ans = new int[k];
        System.Console.WriteLine($"len1 = {len1}, len2 = {len2},k = {k}");
        for(var i = Math.Max(0,k - len2); i <= len1 && i <= k ; ++i) {
            int[] res = Merge(MaxArray(nums1,i), MaxArray(nums2,k-i), k);
            if(Greater(res, 0, ans, 0)) {
                ans = res;
            }
        }
        return ans;
    }

    public int[] MaxArray(int [] nums, int k) {
        int [] ans = new int[k];
        int j = 0;
        for(var i = 0; i < nums.Length; ++i) {
            while(j > 0 && ans[j-1] < nums[i] && nums.Length + j - i > k) {
                j --;
            }
            if(j < k) {
                ans[j++] = nums[i];
            }
        }
        return ans;
    }

    public int[] Merge(int[] nums1, int[] nums2, int k) {
        int [] ans = new int[k];
        int index1 = 0;
        int index2 = 0;
        for(var i = 0; i < k; ++i) {
            ans[i] = Greater(nums1, index1, nums2,index2)? nums1[index1++]: nums2[index2 ++];
        }
        return ans;
    }

    public bool Greater(int[] nums1, int i, int[] nums2, int j) {
        while(i < nums1.Length && j < nums2.Length && nums1[i] == nums2[j]) {
            i ++;
            j ++;
        }
        return j == nums2.Length || (i < nums1.Length && nums1[i] > nums2[j]);
    }
}



*/