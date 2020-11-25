/*
给定一个整数数组 nums，返回区间和在 [lower, upper] 之间的个数，包含 lower 和 upper。
区间和 S(i, j) 表示在 nums 中，位置从 i 到 j 的元素之和，包含 i 和 j (i ≤ j)。

说明:
最直观的算法复杂度是 O(n2) ，请在此基础上优化你的算法。

示例:

输入: nums = [-2,5,-1], lower = -2, upper = 2,
输出: 3
解释: 3个区间分别是: [0,0], [2,2], [0,2]，它们表示的和分别为: -2, -1, 2。
通过次数18,869提交次数44,137

*/

/// <summary>
/// https://leetcode-cn.com/problems/count-of-range-sum/
/// 327. 区间和的个数
///
///
/// </summary>
internal class CountOfRangeSumSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int CountRangeSum(int[] nums, int lower, int upper)
    {
        long sum = 0;
        long[] sums = new long[nums.Length + 1];
        sums[0] = 0;
        long[] sorted = new long[sums.Length];
        for (int i = 0; i < nums.Length; ++i)
        {
            sum += nums[i];
            sums[i + 1] = sum;
        }
        return CountRangeSumRecursive(0, sums.Length - 1);

        int CountRangeSumRecursive(int left, int right)
        {
            if (left == right) return 0;

            int mid = (left + right) / 2;
            int n1 = CountRangeSumRecursive(left, mid);
            int n2 = CountRangeSumRecursive(mid + 1, right);
            int ret = n1 + n2;

            // 统计下标对的数量
            int i = left;
            int l = mid + 1;
            int r = mid + 1;
            while (i <= mid)
            {
                while (l <= right && sums[l] - sums[i] < lower) l++;
                while (r <= right && sums[r] - sums[i] <= upper) r++;
                ret += r - l;
                i++;
            }

            // 合并两个排序数组
            int count = right - left + 1;
            int p1 = left, p2 = mid + 1;
            int p = 0;
            while (p1 <= mid || p2 <= right)
            {
                if (mid < p1) sorted[p++] = sums[p2++];
                else if (right < p2) sorted[p++] = sums[p1++];
                else
                {
                    if (sums[p1] < sums[p2]) sorted[p++] = sums[p1++];
                    else sorted[p++] = sums[p2++];
                }
            }
            for (int j = 0; j < count; j++) sums[left + j] = sorted[j];

            return ret;
        }
    }
}

/*

区间和的个数
力扣官方题解
发布于 2020-11-07
26.0k
前言
本题目的方法二至方法五均用到了较高级的数据结构，读者一般只需掌握方法一即可，感兴趣的读者可以学习其他四种解法。

在某些方法的 C++ 代码中，我们没有将开辟的堆空间进行释放。维护树型结构的动态内存较为困难，而这些方法的重点在于算法和数据结构本身。

方法一：归并排序
思路与算法

设前缀和数组为 \textit{preSum}preSum，则问题等价于求所有的下标对 (i,j)(i,j)，满足

\textit{preSum}[j] - \textit{preSum}[i] \in [\textit{lower}, \textit{upper}]
preSum[j]−preSum[i]∈[lower,upper]

我们先考虑如下的问题：给定两个升序排列的数组 n_1, n_2n 
1
​	
 ,n 
2
​	
 ，试找出所有的下标对 (i,j)(i,j)，满足

n_2[j] - n_1[i] \in [\textit{lower}, \textit{upper}]
n 
2
​	
 [j]−n 
1
​	
 [i]∈[lower,upper]

在已知两个数组均为升序的情况下，这一问题是相对简单的：我们在 n_2n 
2
​	
  中维护两个指针 l,rl,r。起初，它们都指向 n_2n 
2
​	
  的起始位置。

随后，我们考察 n_1n 
1
​	
  的第一个元素。首先，不断地将指针 ll 向右移动，直到 n_2[l] \ge n_1[0] + \textit{lower}n 
2
​	
 [l]≥n 
1
​	
 [0]+lower 为止，此时， ll 及其右边的元素均大于或等于 n_1[0] + \textit{lower}n 
1
​	
 [0]+lower；随后，再不断地将指针 rr 向右移动，直到 n_2[r] > n_1[0] + \textit{upper}n 
2
​	
 [r]>n 
1
​	
 [0]+upper 为止，则 rr 左边的元素均小于或等于 n_1[0] + \textit{upper}n 
1
​	
 [0]+upper。故区间 [l,r)[l,r) 中的所有下标 jj，都满足

n_2[j] - n_1[0] \in [\textit{lower}, \textit{upper}]
n 
2
​	
 [j]−n 
1
​	
 [0]∈[lower,upper]

接下来，我们考察 n_1n 
1
​	
  的第二个元素。由于 n_1n 
1
​	
  是递增的，不难发现 l,rl,r 只可能向右移动。因此，我们不断地进行上述过程，并对于 n_1n 
1
​	
  中的每一个下标，都记录相应的区间 [l,r)[l,r) 的大小。最终，我们就统计得到了满足条件的下标对 (i,j)(i,j) 的数量。

在解决这一问题后，原问题就迎刃而解了：我们采用归并排序的方式，能够得到左右两个数组排序后的形式，以及对应的下标对数量。对于原数组而言，若要找出全部的下标对数量，只需要再额外找出左端点在左侧数组，同时右端点在右侧数组的下标对数量，而这正是我们此前讨论的问题。

代码


class Solution {
    public int countRangeSum(int[] nums, int lower, int upper) {
        long s = 0;
        long[] sum = new long[nums.length + 1];
        for (int i = 0; i < nums.length; ++i) {
            s += nums[i];
            sum[i + 1] = s;
        }
        return countRangeSumRecursive(sum, lower, upper, 0, sum.length - 1);
    }

    public int countRangeSumRecursive(long[] sum, int lower, int upper, int left, int right) {
        if (left == right) {
            return 0;
        } else {
            int mid = (left + right) / 2;
            int n1 = countRangeSumRecursive(sum, lower, upper, left, mid);
            int n2 = countRangeSumRecursive(sum, lower, upper, mid + 1, right);
            int ret = n1 + n2;

            // 首先统计下标对的数量
            int i = left;
            int l = mid + 1;
            int r = mid + 1;
            while (i <= mid) {
                while (l <= right && sum[l] - sum[i] < lower) {
                    l++;
                }
                while (r <= right && sum[r] - sum[i] <= upper) {
                    r++;
                }
                ret += r - l;
                i++;
            }

            // 随后合并两个排序数组
            int[] sorted = new int[right - left + 1];
            int p1 = left, p2 = mid + 1;
            int p = 0;
            while (p1 <= mid || p2 <= right) {
                if (p1 > mid) {
                    sorted[p++] = (int) sum[p2++];
                } else if (p2 > right) {
                    sorted[p++] = (int) sum[p1++];
                } else {
                    if (sum[p1] < sum[p2]) {
                        sorted[p++] = (int) sum[p1++];
                    } else {
                        sorted[p++] = (int) sum[p2++];
                    }
                }
            }
            for (int j = 0; j < sorted.length; j++) {
                sum[left + j] = sorted[j];
            }
            return ret;
        }
    }
}
复杂度分析

时间复杂度：O(N\log N)O(NlogN)，其中 NN 为数组的长度。设执行时间为 T(N)T(N)，则两次递归调用的时间均为 T(N/2)T(N/2)，最后需要 O(N)O(N) 的时间求出下标对数量以及合并数组，故有

T(N) = 2 \cdot T(N/2) + O(N)
T(N)=2⋅T(N/2)+O(N)

根据主定理，有 T(N) = O(N\log N)T(N)=O(NlogN)。

空间复杂度：O(N)O(N)。设空间占用为 M(N)M(N)，递归调用所需空间为 M(N/2)M(N/2)，而合并数组所需空间为 O(N)O(N)，故

M(N) = \max\big\{M(N/2), O(N)\big\} = M(N/2) + O(N)
M(N)=max{M(N/2),O(N)}=M(N/2)+O(N)

根据主定理，有 M(N) = O(N)M(N)=O(N)。

方法二：线段树
思路与算法

依然考虑前缀和数组 \textit{preSum}preSum。

对于每个下标 jj，以 jj 为右端点的下标对的数量，就等于数组 \textit{preSum}[0..j-1]preSum[0..j−1] 中的所有整数，出现在区间 [\textit{preSum}[j]-\textit{upper}, \textit{preSum}[j]-\textit{lower}][preSum[j]−upper,preSum[j]−lower] 的次数。故很容易想到基于线段树的解法。

我们从左到右扫描前缀和数组。每遇到一个数 \textit{preSum}[j]preSum[j]，我们就在线段树中查询区间 [\textit{preSum}[j]-\textit{upper}, \textit{preSum}[j]-\textit{lower}][preSum[j]−upper,preSum[j]−lower] 内的整数数量，随后，将 \textit{preSum}[j]preSum[j] 插入到线段树当中。

注意到整数的范围可能很大，故需要利用哈希表将所有可能出现的整数，映射到连续的整数区间内。

代码


class Solution {
    public int countRangeSum(int[] nums, int lower, int upper) {
        long sum = 0;
        long[] preSum = new long[nums.length + 1];
        for (int i = 0; i < nums.length; ++i) {
            sum += nums[i];
            preSum[i + 1] = sum;
        }
        
        Set<Long> allNumbers = new TreeSet<Long>();
        for (long x : preSum) {
            allNumbers.add(x);
            allNumbers.add(x - lower);
            allNumbers.add(x - upper);
        }
        // 利用哈希表进行离散化
        Map<Long, Integer> values = new HashMap<Long, Integer>();
        int idx = 0;
        for (long x : allNumbers) {
            values.put(x, idx);
            idx++;
        }

        SegNode root = build(0, values.size() - 1);
        int ret = 0;
        for (long x : preSum) {
            int left = values.get(x - upper), right = values.get(x - lower);
            ret += count(root, left, right);
            insert(root, values.get(x));
        }
        return ret;
    }

    public SegNode build(int left, int right) {
        SegNode node = new SegNode(left, right);
        if (left == right) {
            return node;
        }
        int mid = (left + right) / 2;
        node.lchild = build(left, mid);
        node.rchild = build(mid + 1, right);
        return node;
    }

    public int count(SegNode root, int left, int right) {
        if (left > root.hi || right < root.lo) {
            return 0;
        }
        if (left <= root.lo && root.hi <= right) {
            return root.add;
        }
        return count(root.lchild, left, right) + count(root.rchild, left, right);
    }

    public void insert(SegNode root, int val) {
        root.add++;
        if (root.lo == root.hi) {
            return;
        }
        int mid = (root.lo + root.hi) / 2;
        if (val <= mid) {
            insert(root.lchild, val);
        } else {
            insert(root.rchild, val);
        }
    }
}

class SegNode {
    int lo, hi, add;
    SegNode lchild, rchild;

    public SegNode(int left, int right) {
        lo = left;
        hi = right;
        add = 0;
        lchild = null;
        rchild = null;
    }
}
复杂度分析

时间复杂度：O(N\log N)O(NlogN)。使用哈希离散化之后，线段树维护的区间大小为 O(N)O(N)，故其深度、单次查询或插入的时间复杂度均为 O(\log N)O(logN)。而离散化本身的复杂度也为 O(N\log N)O(NlogN)。

空间复杂度：O(N)O(N)。线段树的深度为 O(N)O(N)，而第 ii 层拥有的节点数量为 2^{i-1}2 
i−1
 ，故线段树总的节点数量为 2^{O(\log N)} = O(N)2 
O(logN)
 =O(N)。

方法三：动态增加节点的线段树
思路与算法

与方法二类似，但我们可以不实用哈希表进行映射，而是只在线段树的插入操作过程中动态地增加树中的节点。而当我们进行查询操作时，如果到达一个空节点，那么说明对应的区间中暂时还没有值，就可以直接返回 00。

代码


class Solution {
    public int countRangeSum(int[] nums, int lower, int upper) {
        long sum = 0;
        long[] preSum = new long[nums.length + 1];
        for (int i = 0; i < nums.length; ++i) {
            sum += nums[i];
            preSum[i + 1] = sum;
        }
        
        long lbound = Long.MAX_VALUE, rbound = Long.MIN_VALUE;
        for (long x : preSum) {
            lbound = Math.min(Math.min(lbound, x), Math.min(x - lower, x - upper));
            rbound = Math.max(Math.max(rbound, x), Math.max(x - lower, x - upper));
        }
        
        SegNode root = new SegNode(lbound, rbound);
        int ret = 0;
        for (long x : preSum) {
            ret += count(root, x - upper, x - lower);
            insert(root, x);
        }
        return ret;
    }

    public int count(SegNode root, long left, long right) {
        if (root == null) {
            return 0;
        }
        if (left > root.hi || right < root.lo) {
            return 0;
        }
        if (left <= root.lo && root.hi <= right) {
            return root.add;
        }
        return count(root.lchild, left, right) + count(root.rchild, left, right);
    }

    public void insert(SegNode root, long val) {
        root.add++;
        if (root.lo == root.hi) {
            return;
        }
        long mid = (root.lo + root.hi) >> 1;
        if (val <= mid) {
            if (root.lchild == null) {
                root.lchild = new SegNode(root.lo, mid);
            }
            insert(root.lchild, val);
        } else {
            if (root.rchild == null) {
                root.rchild = new SegNode(mid + 1, root.hi);
            }
            insert(root.rchild, val);
        }
    }
}

class SegNode {
    long lo, hi;
    int add;
    SegNode lchild, rchild;

    public SegNode(long left, long right) {
        lo = left;
        hi = right;
        add = 0;
        lchild = null;
        rchild = null;
    }
}
复杂度分析

时间复杂度：O(N \log C)O(NlogC)，其中 CC 是线段树根节点对应的区间长度。由于我们使用 6464 位整数类型进行存储，因此 \log ClogC 不会超过 6464。使用动态增加节点的线段树，单次查询或插入的时间复杂度均为 O(\log C)O(logC)。
空间复杂度：O(N \log C)O(NlogC)。需要进行 NN 次线段树的插入操作，每次会添加不超过 \log ClogC 个新节点。
方法四：树状数组
思路与算法

树状数组与线段树基于类似的思想，不过树状数组支持的基本查询为求出 [0, \textit{val}][0,val] 之间的整数数量。为了查询区间 [\textit{preSum}[j]-\textit{upper}, \textit{preSum}[j]-\textit{lower}][preSum[j]−upper,preSum[j]−lower] 内的整数数量，需要执行两次查询，即分别查询 [0, \textit{preSum}[j]-\textit{upper}-1][0,preSum[j]−upper−1] 区间的整数数量 LL 和[0,\textit{preSum}[j]-\textit{lower}][0,preSum[j]−lower] 区间的整数数量 RR，答案即为两者作差 R-LR−L。

代码


class Solution {
    public int countRangeSum(int[] nums, int lower, int upper) {
        long sum = 0;
        long[] preSum = new long[nums.length + 1];
        for (int i = 0; i < nums.length; ++i) {
            sum += nums[i];
            preSum[i + 1] = sum;
        }
        
        Set<Long> allNumbers = new TreeSet<Long>();
        for (long x : preSum) {
            allNumbers.add(x);
            allNumbers.add(x - lower);
            allNumbers.add(x - upper);
        }
        // 利用哈希表进行离散化
        Map<Long, Integer> values = new HashMap<Long, Integer>();
        int idx = 0;
        for (long x: allNumbers) {
            values.put(x, idx);
            idx++;
        }

        int ret = 0;
        BIT bit = new BIT(values.size());
        for (int i = 0; i < preSum.length; i++) {
            int left = values.get(preSum[i] - upper), right = values.get(preSum[i] - lower);
            ret += bit.query(right + 1) - bit.query(left);
            bit.update(values.get(preSum[i]) + 1, 1);
        }
        return ret;
    }
}

class BIT {
    int[] tree;
    int n;

    public BIT(int n) {
        this.n = n;
        this.tree = new int[n + 1];
    }

    public static int lowbit(int x) {
        return x & (-x);
    }

    public void update(int x, int d) {
        while (x <= n) {
            tree[x] += d;
            x += lowbit(x);
        }
    }

    public int query(int x) {
        int ans = 0;
        while (x != 0) {
            ans += tree[x];
            x -= lowbit(x);
        }
        return ans;
    }
}
复杂度分析

时间复杂度：O(N\log N)O(NlogN)。离散化本身的复杂度为 O(N\log N)O(NlogN)，而树状数组单次更新或查询的复杂度为 O(\log N)O(logN)。

空间复杂度：O(N)O(N)。

方法五：平衡二叉搜索树
思路与算法

考虑一棵平衡二叉搜索树。若其节点数量为 NN，则深度为 O(\log N)O(logN)。二叉搜索树能够在 O(\log N)O(logN) 的时间内，对任意给定的值 \textit{val}val，查询树中所有小于或等于该值的数量。

因此，我们可以从左到右扫描前缀和数组。对于 \textit{preSum}[j]preSum[j] 而言，首先进行两次查询，得到区间 [\textit{preSum}[j]-\textit{upper}, \textit{preSum}[j]-\textit{lower}][preSum[j]−upper,preSum[j]−lower] 内的整数数量；随后再将 \textit{preSum}[j]preSum[j] 插入到平衡树中。

平衡二叉搜索树有多种不同的实现，最经典的为 AVL 树与红黑树。此外，在算法竞赛中，还包括 Treap、SBT 等数据结构。

下面给出基于 Treap 的实现。

代码


class Solution {
    public int countRangeSum(int[] nums, int lower, int upper) {
        long sum = 0;
        long[] preSum = new long[nums.length + 1];
        for (int i = 0; i < nums.length; ++i) {
            sum += nums[i];
            preSum[i + 1] = sum;
        }
        
        BalancedTree treap = new BalancedTree();
        int ret = 0;
        for (long x : preSum) {
            long numLeft = treap.lowerBound(x - upper);
            int rankLeft = (numLeft == Long.MAX_VALUE ? (int) (treap.getSize() + 1) : treap.rank(numLeft)[0]);
            long numRight = treap.upperBound(x - lower);
            int rankRight = (numRight == Long.MAX_VALUE ? (int) treap.getSize() : treap.rank(numRight)[0] - 1);
            ret += rankRight - rankLeft + 1;
            treap.insert(x);
        }
        return ret;
    }
}

class BalancedTree {
    private class BalancedNode {
        long val;
        long seed;
        int count;
        int size;
        BalancedNode left;
        BalancedNode right;

        BalancedNode(long val, long seed) {
            this.val = val;
            this.seed = seed;
            this.count = 1;
            this.size = 1;
            this.left = null;
            this.right = null;
        }

        BalancedNode leftRotate() {
            int prevSize = size;
            int currSize = (left != null ? left.size : 0) + (right.left != null ? right.left.size : 0) + count;
            BalancedNode root = right;
            right = root.left;
            root.left = this;
            root.size = prevSize;
            size = currSize;
            return root;
        }

        BalancedNode rightRotate() {
            int prevSize = size;
            int currSize = (right != null ? right.size : 0) + (left.right != null ? left.right.size : 0) + count;
            BalancedNode root = left;
            left = root.right;
            root.right = this;
            root.size = prevSize;
            size = currSize;
            return root;
        }
    }

    private BalancedNode root;
    private int size;
    private Random rand;

    public BalancedTree() {
        this.root = null;
        this.size = 0;
        this.rand = new Random();
    }

    public long getSize() {
        return size;
    }

    public void insert(long x) {
        ++size;
        root = insert(root, x);
    }

    public long lowerBound(long x) {
        BalancedNode node = root;
        long ans = Long.MAX_VALUE;
        while (node != null) {
            if (x == node.val) {
                return x;
            }
            if (x < node.val) {
                ans = node.val;
                node = node.left;
            } else {
                node = node.right;
            }
        }
        return ans;
    }

    public long upperBound(long x) {
        BalancedNode node = root;
        long ans = Long.MAX_VALUE;
        while (node != null) {
            if (x < node.val) {
                ans = node.val;
                node = node.left;
            } else {
                node = node.right;
            }
        }
        return ans;
    }

    public int[] rank(long x) {
        BalancedNode node = root;
        int ans = 0;
        while (node != null) {
            if (x < node.val) {
                node = node.left;
            } else {
                ans += (node.left != null ? node.left.size : 0) + node.count;
                if (x == node.val) {
                    return new int[]{ans - node.count + 1, ans};
                }
                node = node.right;
            }
        }
        return new int[]{Integer.MIN_VALUE, Integer.MAX_VALUE};
    }

    private BalancedNode insert(BalancedNode node, long x) {
        if (node == null) {
            return new BalancedNode(x, rand.nextInt());
        }
        ++node.size;
        if (x < node.val) {
            node.left = insert(node.left, x);
            if (node.left.seed > node.seed) {
                node = node.rightRotate();
            }
        } else if (x > node.val) {
            node.right = insert(node.right, x);
            if (node.right.seed > node.seed) {
                node = node.leftRotate();
            }
        } else {
            ++node.count;
        }
        return node;
    }
}
复杂度分析

时间复杂度：O(N\log N)O(NlogN)。

空间复杂度：O(N)O(N)。


*/