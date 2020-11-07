using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*





*/
/// <summary>
/// https://leetcode-cn.com/problems/range-sum-query-immutable/
/// 303. 区域和检索 - 数组不可变
/// 
/// 
/// </summary>
class RangeSumQueryImmutableSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public RangeSumQueryImmutableSolution(int[] nums)
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

    public int SumRange(int i, int j)
    {
        if ( i < 0 || _originArray.Length <= j || j < i) return 0;
        return (int)( 0 < i  ? _sumArray[j] - _sumArray[i - 1] : _sumArray[j]);
    }
}
/*
     
区域和检索 - 数组不可变
力扣 (LeetCode)
发布于 2019-06-19
28.5k
方法一：暴力法[超过时间限制]
每次调用 sumrange 时，我们都使用for循环将索引 ii 到 jj 之间的每个元素相加。


private int[] data;

public NumArray(int[] nums) {
    data = nums;
}

public int sumRange(int i, int j) {
    int sum = 0;
    for (int k = i; k <= j; k++) {
        sum += data[k];
    }
    return sum;
}
复杂度分析

时间复杂度：每次查询的时间 O(n)O(n)，每个 sumrange 查询需要 O(n)O(n) 时间。
空间复杂度：O(1)O(1)，请注意，data 是对 nums 的引用，不是它的副本。
方法二：缓存
假设 sumrange 被调用 1000次，其参数完全相同。我们怎么能加快速度？
我们可以用额外的空间换取速度。通过预先计算所有的范围和可能性并将其结果存储在哈希表中，我们可以将查询加速到常量时间。


private Map<Pair<Integer, Integer>, Integer> map = new HashMap<>();

public NumArray(int[] nums) {
    for (int i = 0; i < nums.length; i++) {
        int sum = 0;
        for (int j = i; j < nums.length; j++) {
            sum += nums[j];
            map.put(Pair.create(i, j), sum);
        }
    }
}

public int sumRange(int i, int j) {
    return map.get(Pair.create(i, j));
}
复杂度分析

时间复杂度：每次查询的时间 O(1)O(1)，O(n^2)O(n 
2
 ) 时间用来预计算。在构造函数中完成的预计算需要 O(n^2)O(n 
2
 ) 时间。每个 sumrange 查询的时间复杂性是 O(1)O(1) 因为哈希表的查找操作是常量时间。
空间复杂度：O(n^2)O(n 
2
 )，所需的额外空间为 O(n^2)O(n 
2
 ) 因为 ii 和 jj 都有 nn 个候选空间。
方法三：缓存
上面的方法需要很大的空间，我们可以优化它吗？
假设我们预先计算了从数字 00 到 kk 的累积和。我们可以用这个信息得出 sum(i，j)sum(i，j) 吗？
让我们将 sum[k]sum[k] 定义为 nums[0\cdots k-1]nums[0⋯k−1] 的累积和（包括这两个值）：
sum[k] = \left\{ \begin{array}{rl} \sum_{i=0}^{k-1}nums[i] & , k > 0 \\ 0 &, k = 0 \end{array} \right.
sum[k]={ 
∑ 
i=0
k−1
​	
 nums[i]
0
​	
  
,k>0
,k=0
​	
 

现在，我们可以计算 sumrange 如下：
sumrange（i，j）=sum[j+1]-sum[i]
sumrange（i，j）=sum[j+1]−sum[i]


private int[] sum;

public NumArray(int[] nums) {
    sum = new int[nums.length + 1];
    for (int i = 0; i < nums.length; i++) {
        sum[i + 1] = sum[i] + nums[i];
    }
}

public int sumRange(int i, int j) {
    return sum[j + 1] - sum[i];
}
注意，在上面的代码中，我们插入了一个虚拟 0 作为 sum 数组中的第一个元素。这个技巧可以避免在 sumrange 函数中进行额外的条件检查。
复杂度分析

时间复杂度：每次查询的时间 O(1)O(1)，O(N)O(N) 预计算时间。由于累积和被缓存，每个sumrange查询都可以用 O(1)O(1) 时间计算。
空间复杂度：O(n)O(n).

public class NumArray {
    int[] dp;
    public NumArray(int[] nums) {
        if(nums==null || nums.Length==0)
            return;
        
        int len=nums.Length;
        dp=new int[len+1];
        for(int i=1; i<=len; i++){
            dp[i]=nums[i-1]+dp[i-1];
        }
    }
    
    public int SumRange(int i, int j) {
        return dp[j+1]-dp[i];
    }
}

public class NumArray {
    private int[] nums;

    public NumArray(params int[] nums)
    {
        int length = nums.Length;
        this.nums = new int[length];
        if (length == 0)
            return;

        this.nums[0] = nums[0];
        for (int i = 1; i < length; i++)
        {
            this.nums[i] += nums[i] + this.nums[i-1];
        }
    }

    public int SumRange(int i, int j)
    {
        if (this.nums.Length == 0)
            return 0;

        if (i == 0)
            return this.nums[j];
        else
            return this.nums[j] - this.nums[i-1];
    }
}

*/
