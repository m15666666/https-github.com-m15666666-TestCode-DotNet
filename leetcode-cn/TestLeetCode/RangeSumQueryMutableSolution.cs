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
