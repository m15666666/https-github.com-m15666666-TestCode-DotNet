using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
给定一个整数数组  nums，求出数组从索引 i 到 j  (i ≤ j) 范围内元素的总和，包含 i,  j 两点。

update(i, val) 函数可以通过将下标为 i 的数值更新为 val，从而对数列进行修改。

示例:

Given nums = [1, 3, 5]

sumRange(0, 2) -> 9
update(1, 2)
sumRange(0, 2) -> 8
说明:

数组仅可以在 update 函数下进行修改。
你可以假设 update 函数与 sumRange 函数的调用次数是均匀分布的。 
*/
/// <summary>
/// https://leetcode-cn.com/problems/range-sum-query-mutable/
/// 307. 区域和检索 - 数组可修改
/// 像连续区间动态更新和查询这种问题，常常用线段树，树状数组等方法。很强大！！值得记录！
/// https://www.cnblogs.com/zhizhiyu/p/10181635.html
/// </summary>
class RangeSumQueryMutableSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

/**
 * Your NumArray object will be instantiated and called as such:
 * NumArray obj = new NumArray(nums);
 * obj.Update(i,val);
 * int param_2 = obj.SumRange(i,j);
 */
    public RangeSumQueryMutableSolution(int[] nums)
    {
        _originArray = nums;
        if (nums == null)
        {
            _sumArray = null;
            return;
        }

        _sumArray = new long[nums.Length];

        long sum = 0;
        int index = 0;
        foreach( var v in nums )
        {
            sum += v;
            _sumArray[index++] = sum;
        }
    }

    private int[] _originArray = null;
    private long[] _sumArray = null;

    public void Update(int i, int val)
    {
        if (_originArray.Length <= i || i < 0) return;
        var diff = val - _originArray[i];
        _originArray[i] = val;
        for (int index = i; index < _sumArray.Length; index++)
            _sumArray[index] += diff;
    }

    public int SumRange(int i, int j)
    {
        if ( i < 0 || _originArray.Length <= j || j < i) return 0;
        return (int)( 0 < i  ? _sumArray[j] - _sumArray[i - 1] : _sumArray[j]);
    }
}
/*
区域和检索 - 数组可修改
力扣 (LeetCode)
发布于 2019-06-30
12.3k
摘要：
本文面向中级读者。它引入了以下概念：区域和检索、sqrt 分解、线段树。

解决方法：
方法一：
区域和检索的一个简单的解决方案 - RSQ(i, j) 是将数组从索引 ii 迭代到 jj 并对每个元素求和。


private int[] nums;
public int sumRange(int i, int j) {
    int sum = 0;
    for (int l = i; l <= j; l++) {
        sum += data[l];
    }
    return sum;
}

public int update(int i, int val) {
    nums[i] = val;
}
复杂度分析

时间复杂度：O(n)O(n)。区域和检索 O(1)O(1) 的更新查询
对于区域和检索，我们从数组中访问每个元素的时间是固定的，在最坏的情况下，我们访问 nn 元素。因此，时间复杂度为 O(n)O(n)。更新查询的时间复杂度为 O(1)O(1)。
空间复杂度：O(1)O(1)
方法二：sqrt 分解
其思想是将数组分割成块，块的长度为 \sqrt n 
n
​	
 。然后我们计算每个块的和，并将其存储在辅助存储器 b 中。要查询 RSQ(i, j)，我们将添加位于内部的所有块和部分在范围 [i\ldots j][i…j] 重叠的块的总和。

算法：

在这里插入图片描述

在上面的示例中，数组 nums 的长度为 9，它被拆分为大小为 \sqrt 9 
9
​	
  的块。为了得到 RSQ(1, 7)，我们添加 b[1]。它存储范围 [3，5] 的和，以及 块0 和 块2 的部分和，它们是重叠的边界块。


private int[] b;
private int len;
private int[] nums;

public NumArray(int[] nums) {
    this.nums = nums;
    double l = Math.sqrt(nums.length);
    len = (int) Math.ceil(nums.length/l);
    b = new int [len];
    for (int i = 0; i < nums.length; i++)
        b[i / len] += nums[i];
}

public int sumRange(int i, int j) {
    int sum = 0;
    int startBlock = i / len;
    int endBlock = j / len;
    if (startBlock == endBlock) {
        for (int k = i; k <= j; k++)
            sum += nums[k];
    } else {
        for (int k = i; k <= (startBlock + 1) * len - 1; k++)
            sum += nums[k];
        for (int k = startBlock + 1; k <= endBlock - 1; k++)
            sum += b[k];
        for (int k = endBlock * len; k <= j; k++)
            sum += nums[k];
    }
    return sum;
}

public void update(int i, int val) {
    int b_l = i / len;
    b[b_l] = b[b_l] - nums[i] + val;
    nums[i] = val;
}
// Accepted
复杂度分析

时间复杂度：O(n)O(n) 预处理，O(\sqrt n)O( 
n
​	
 ) 区域和检索，O(1)O(1) 更新查询
空间复杂度：O(\sqrt{n})O( 
n
​	
 )，我们需要额外的 \sqrt {n} 
n
​	
  内存来存储所有块和。
方法三：线段树
算法：
线段树是一种非常灵活的数据结构，它可以用于解决多种范围查询问题，比如在对数时间内从数组中找到最小值、最大值、总和、最大公约数、最小公倍数等。

线段树图示

数组 A[0,1,\ldots,n-1]A[0,1,…,n−1] 的线段树是一个二叉树，其中每个节点都包含数组的一个子范围 [i\ldots j][i…j] 上的聚合信息（最小值、最大值、总和等），其左、右子节点分别包含范围 [i \ldots \frac{i+j}{2}][i… 
2
i+j
​	
 ] 和 [\frac{i + j}{2} + 1, j][ 
2
i+j
​	
 +1,j] 上的信息。

线段树既可以用数组也可以用树来实现。对于数组实现，如果索引 ii 处的元素不是一个叶节点，那么其左子节点和右子节点分别存储在索引为 2i2i 和 2i+12i+1 的元素处。

在上图所给出的示例中，每个叶节点都包含初始的数组元素 {2,4,5,7,8,9}。内部节点包含范围内相应元素的总和 - (11) 是从索引 0 到索引 2 的元素之和。而根节点 (35) 是它的两个子节点 (6) 和 (29) 的和,也是整个数组的和。

线段树可以分为以下三个步骤：

从给定数组构建线段树的预处理步骤。
修改元素时更新线段树。
使用线段树进行区域和检索。
构建线段树 :
我们将使用一种非常有效的自下而上的方法来构建线段树。从上面我们已经知道，如果某个节点 pp 包含范围 [i\ldots j][i…j] 的和，那么其左、右子节点分别包含范围 [i \ldots \frac{i + j}{2}][i… 
2
i+j
​	
 ] 和 [\frac{i + j}{2} + 1, j][ 
2
i+j
​	
 +1,j] 上的和。

因此，为了找到节点 pp 的和，我们需要提前计算其左、右子节点的和。

我们从叶节点开始，用输入数组的元素 a[0,1,\ldots,n-1]a[0,1,…,n−1] 初始化它们。然后我们逐步向上移动到更高一层来计算父节点的和，直到最后到达线段树的根节点。


int[] tree;
int n;
public NumArray(int[] nums) {
    if (nums.length > 0) {
        n = nums.length;
        tree = new int[n * 2];
        buildTree(nums);
    }
}
private void buildTree(int[] nums) {
    for (int i = n, j = 0;  i < 2 * n; i++,  j++)
        tree[i] = nums[j];
    for (int i = n - 1; i > 0; --i)
        tree[i] = tree[i * 2] + tree[i * 2 + 1];
}
复杂度分析

时间复杂度：O(n)O(n)。
因为我们在 for 循环的每次迭代中计算一个节点的和。而一个线段树中大约有 2n2n 个节点。
这可以通过以下方式证明：具有 nn 个元素的数组线段树有 nn 个叶节点（数组元素本身）。每一层中的节点数是下面一层中节点数的一半。
因此，如果我们按层对节点数求和，我们将得到：
n + n/2 + n/4 + n/8 + \ldots + 1 \approx 2n
n+n/2+n/4+n/8+…+1≈2n

空间复杂度：O(n)O(n)，我们用了 2n2n 的额外空间来存储整个线段树。
更新线段树 :
当我们更新数组中某个索引 ii 处的元素时，我们需要重建线段树，因为一些树节点上的和值也会随之产生变化。我们将再次使用自下而上的方法。首先更新存储 a[i]a[i] 元素的叶节点。从那里我们将一路向上，直到根节点，并用其子节点值的总和来更新每个父节点的值。


void update(int pos, int val) {
    pos += n;
    tree[pos] = val;
    while (pos > 0) {
        int left = pos;
        int right = pos;
        if (pos % 2 == 0) {
            right = pos + 1;
        } else {
            left = pos - 1;
        }
        // parent is updated after child is updated
        tree[pos / 2] = tree[left] + tree[right];
        pos /= 2;
    }
}
复杂度分析

时间复杂度：O(\log n)O(logn)。算法的时间复杂度为 O(\log n)O(logn)，因为有几个树节点的范围包括第 ii 个数组元素，每个层上都有一个。共有 \log(n)log(n) 层。
空间复杂度：O(1)O(1)。
区域和检索：

我们可以通过以下方式使用线段树进行区域和检索 [L, R][L,R]：算法保持循环不变：l \le rl≤r 以及已经算出 [L \ldots l][L…l] 和 [r \ldots R][r…R] 的总和，其中 ll 和 rr 分别是计算总和时的左边界和右边界。每次迭代的范围 [l,r][l,r] 都会缩小，直到在算法的大约 log nlogn 次迭代后两个边界相遇为止。


public int sumRange(int l, int r) {
    // get leaf with value 'l'
    l += n;
    // get leaf with value 'r'
    r += n;
    int sum = 0;
    while (l <= r) {
        if ((l % 2) == 1) {
           sum += tree[l];
           l++;
        }
        if ((r % 2) == 0) {
           sum += tree[r];
           r--;
        }
        l /= 2;
        r /= 2;
    }
    return sum;
}
复杂度分析

时间复杂度：O(\log n)O(logn)。因为在算法的每次迭代中，我们会向上移动一层，要么移动到当前节点的父节点，要么移动到父节点的左侧或者右侧的兄弟节点，直到两个边界相交为止。在最坏的情况下，这种情况会在算法进行 \log nlogn 次迭代后发生在根节点。
空间复杂度：O(1)O(1)。



public struct TreeNode
{
    public int sum;
}
public class NumArray
{
    private int[] nums = null;
    private TreeNode[] nodes = null;
    public NumArray(int[] nums)
    {
        this.nums = nums;
        int maxnodes = 1;
        while(maxnodes<nums.Length)
        {
            maxnodes <<= 1;
        }
        maxnodes *= 2;
        nodes = new TreeNode[maxnodes];
        this.buildtree(1, 0, this.nums.Length - 1);
    }
    private void buildtree(int p,int l,int r)
    {
        if(l==r)
        {
            this.nodes[p].sum = this.nums[l];
            return;
        }
        if(l<r)
        {
            int mid = (l + r) >> 1;
            buildtree(p << 1, l, mid);
            buildtree(p << 1 | 1, mid + 1, r);
            nodes[p].sum = nodes[p << 1].sum + nodes[p << 1 | 1].sum;
        }
    }

    public void Update(int i, int val)
    {
        updata(0, this.nums.Length - 1, i,1, val);
    }
    private void updata(int l,int r,int i,int p,int val)
    {
        if(l == r)
        {
            nodes[p].sum = val;
            return;
        }
        if(l<r)
        {
            int mid = (l + r) >> 1;
            if(i<=mid)
            {
                updata(l, mid, i,p<<1 ,val);
            }
            else
            {
                updata(mid + 1, r, i, p << 1 | 1, val);
            }
            nodes[p].sum = nodes[p << 1].sum + nodes[p << 1 | 1].sum;
        }
    }

    public int SumRange(int i, int j)
    {
        int sum = 0;
        query(i, j, 0, this.nums.Length - 1, 1, ref sum);
        return sum;
    }
    private void query(int i,int j,int l,int r,int p,ref int sum)
    {
        if(i>r||j<l)
        {
            return;
        }
        if(l>=i && j>=r)
        {
            sum += this.nodes[p].sum;
            return;
        }
        int mid = (l + r) >> 1;
        query(i, j, l, mid, p << 1,ref sum);
        query(i, j, mid + 1, r, p << 1 | 1, ref sum);
    }
} 
public class NumArray
{
    private int[] _nums;
    private int[] _sums;
    private int[] _tree;
    public NumArray(int[] nums)
    {
        _nums = nums;
        _sums = new int[nums.Length + 1];
        _tree = new int[nums.Length + 1];
        for (var i = 1; i < _sums.Length; i++)
        {
            _sums[i] = _sums[i - 1] + _nums[i - 1];
        }
        for (var i = 1; i < _tree.Length; i++)
        {
            _tree[i] = _sums[i] - _sums[i-_lowbit(i)];
        }
    }
    private int _lowbit(int x)
    {
        return (-x) & x;
    }
    public void Update(int i, int val)
    {
        var temp = _nums[i];
        _nums[i] = val;
        i = i + 1;
        for (; i < _tree.Length; i += _lowbit(i))
        {
            _tree[i] = _tree[i] - temp + val;
        }
    }

    public int SumRange(int i, int j)
    {
        return _getSum(j + 1) - _getSum(i);
    }
    private int _getSum(int index)
    {
        var res = 0;
        for (; index > 0; index -= _lowbit(index))
        {
            res += _tree[index];
        }
        return res;
    }
}
     
*/
