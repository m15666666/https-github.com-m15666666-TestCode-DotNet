using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给出集合 [1,2,3,…,n]，其所有元素共有 n! 种排列。

按大小顺序列出所有排列情况，并一一标记，当 n = 3 时, 所有排列如下：

"123"
"132"
"213"
"231"
"312"
"321"
给定 n 和 k，返回第 k 个排列。

说明：

给定 n 的范围是 [1, 9]。
给定 k 的范围是[1,  n!]。
示例 1:

输入: n = 3, k = 3
输出: "213"
示例 2:

输入: n = 4, k = 9
输出: "2314"
*/
/// <summary>
/// https://leetcode-cn.com/problems/permutation-sequence/description/
/// 60.第K个排列
/// 
/// </summary>
class GetKthPermutationSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public string GetPermutation(int n, int k)
    {
        int[] factorials = new int[n];
        List<int> nums = new List<int>(n) { 1 };

        factorials[0] = 1;
        for(int i = 1; i < n; ++i) {
          factorials[i] = factorials[i - 1] * i;
          nums.Add(i + 1);
        }

        --k;

        StringBuilder ret = new StringBuilder();
        for (int i = n - 1; i > -1; --i) {
            int idx = k / factorials[i];
            if( 0 < idx) k -= idx * factorials[i];

            ret.Append(nums[idx]);
            nums.RemoveAt(idx);
        }
        return ret.ToString();
    }

    //public string GetPermutation(int n, int k)
    //{
    //    if ( n < 1 ) return string.Empty;
    //    if ( n == 1 ) return "1";

    //    int n1 = 1;
    //    int count = 1;

    //    while( n1 < k && count < n )
    //    {
    //        count++;
    //        n1 *= count;
    //    }

    //    if (n1 < k) return string.Empty;

    //    List<int> high = new List<int>();
    //    List<int> low = new List<int>();

    //    int i = 0;
    //    for (; i < n - count; i++) high.Add(i + 1);
    //    for (; i < n; i++) low.Add(i + 1);

    //    BackTrack(high, low, k, n1, count);

    //    return string.Join("", high);
    //}

    //private static void BackTrack( List<int> high, List<int> low, int k, int n1, int count )
    //{
    //    if (low.Count == 0) return;
    //    if( low.Count == 1)
    //    {
    //        high.Add(low[0]);
    //        low.Clear();
    //        return;
    //    }

    //    int smallN1 = n1 / count;
    //    int remainder;
    //    int quotient = Math.DivRem( k, smallN1, out remainder );
    //    //int index = (int)Math.Ceiling( k / smallN1 ) - 1;
    //    int index = ( quotient - 1 ) + (0 < remainder ? 1 : 0);
    //    if (index < 0 || low.Count <= index ) return;

    //    high.Add(low[index]);
    //    low.RemoveAt(index);

    //    k -= smallN1 * index;
    //    BackTrack(high, low, k, smallN1, count - 1);
    //}
}

/// <summary>
/// 60.第K个排列,别人的答案，试一下
/// </summary>
class GetKthPermutationSolution_Others_1
{
    public static void Test()
    {
        var ret = GetPermutation(3, 2);
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public static string GetPermutation(int n, int k)
    {
        if (n == 1)
            return "1";
        int count = GetCount(n - 1);
        Console.WriteLine(count + " " + k);
        int x = (k - 1) / count + 1;
        return replace(GetPermutation(n - 1, k - x * count + count), x);
    }

    private static int GetCount(int n)
    {
        int ret = 1;
        for (int i = 1; i <= n; i++)
            ret *= i;

        return ret;
    }

    private static string replace(string s, int x)
    {
        //Console.WriteLine(s);
        Console.WriteLine(x);
        string ret = "";
        ret += (char)('0' + x);
        foreach (var n in s)
            ret += n - '0' >= x ? (char)(n + 1) : n;

        return ret;
    }
}
/*

第 k 个排列
力扣 (LeetCode)
发布于 2 个月前
8.3k
解决方案
面试中主要有三种类型的排列问题：

全排列
下一个排列
第 k 个排列（当前问题）
如果排列的顺序不重要，可以使用“交换”的思想回溯写出全排列。生成 N!N! 个全排列需要时间 \mathcal{O}(N \times N!)O(N×N!)。该算法可以解决第一类问题。

D.E. Knuth 算法按照字典顺序生成全排列，在 \mathcal{O}(N)O(N) 时间内完成。该算法可以解决第二类问题。

但是这两个算法不能解决第三类问题：

良好的时间复杂度，即无回溯。

先前排列未知，即不能使用 D.E. Knuth 算法。

为了解决这两个问题，可以使用映射的思路，因为生成数字的排列更容易。

使用数字生成排列，然后映射到组合/子集/排列中。

这种方法也广泛用于密码破解算法。

方法一：阶乘数系统
为什么需要阶乘数系统

排列的每种情况都可以使用十进制或二进制数表示：

k = \sum\limits_{m = 0}^{N - 1}{k_m 2^m}, \qquad 0 \le k_m \le 1
k= 
m=0
∑
N−1
​	
 k 
m
​	
 2 
m
 ,0≤k 
m
​	
 ≤1

原理如下：



排列的问题在于排列的所有情况可能比数字表示的范围更大。NN 个元素的全排列数量为 N!N!，NN 位二进制包含 2^N2 
N
  个不同的数字。简单使用二进制数作为解空间不能包含排列的所有情况。

因此使用阶乘数系统，它具有非恒定基数：

k = \sum\limits_{m = 0}^{N - 1}{k_m m!}, \qquad 0 \le k_m \le m
k= 
m=0
∑
N−1
​	
 k 
m
​	
 m!,0≤k 
m
​	
 ≤m

注意：权重的大小不恒定，而是取决于基数：当 0 \le k_m \le m0≤k 
m
​	
 ≤m 时基数为 m!m!。例如：k_0 = 0k 
0
​	
 =0，0 \le k_1 \le 10≤k 
1
​	
 ≤1，0 \le k_2 \le 20≤k 
2
​	
 ≤2 等等。

映射方式如下：



现在映射全部排列情况。从排列数 k = 0 = \sum\limits_{m = 0}^{N - 1}{0 \times m!}k=0= 
m=0
∑
N−1
​	
 0×m! 到排列数 N! - 1N!−1：k = N! - 1 = \sum\limits_{m = 0}^{N - 1}{m \times m!}k=N!−1= 
m=0
∑
N−1
​	
 m×m!。

现在使用这些阶乘数构造全部的排列。

如何从阶乘构造排列

N = 3N=3 时，输入数组为 nums = [1, 2, 3]，k = 3k=3。但是排列的编号为从 00 到 N! - 1N!−1，而不是从 11 到 N!N!。因此 N = 3N=3 时，k = 2k=2。

首先构造 k = 2k=2 的阶乘数：

k = 2 = 1 \times 2! + 0 \times 1! + 0 \times 0! = (1, 0, 0)
k=2=1×2!+0×1!+0×0!=(1,0,0)

阶乘中的系数表示输入数组中，除去已使用元素的索引。这符合每个元素只能在排列中出现一次的要求。



第一个数字是 1，即排列中的第一个元素是 nums[1] = 2。由于每个元素只能使用一次，则从 nums 中删除该元素。



阶乘中下一个系数为 0，即排列中 nums[0] = 1，然后从 nums 中删除该元素。



阶乘中下一个系数也是 0，即排列中 nums[0] = 3，然后从 nums 中删除该元素。



算法

生成输入数组，存储从 11 到 NN 的数字。

计算从 00 到 (N - 1)!(N−1)! 的所有阶乘数。

在 (0, N! - 1)(0,N!−1) 区间内，kk 重复减 11。

计算 kk 的阶乘，使用阶乘系数构造排列。

返回排列字符串。

java
class Solution {
  public String getPermutation(int n, int k) {
    int[] factorials = new int[n];
    List<Integer> nums = new ArrayList() {{add(1);}};

    factorials[0] = 1;
    for(int i = 1; i < n; ++i) {
      // generate factorial system bases 0!, 1!, ..., (n - 1)!
      factorials[i] = factorials[i - 1] * i;
      // generate nums 1, 2, ..., n
      nums.add(i + 1);
    }

    // fit k in the interval 0 ... (n! - 1)
    --k;

    // compute factorial representation of k
    StringBuilder sb = new StringBuilder();
    for (int i = n - 1; i > -1; --i) {
      int idx = k / factorials[i];
      k -= idx * factorials[i];

      sb.append(nums.get(idx));
      nums.remove(idx);
    }
    return sb.toString();
  }
}

python
class Solution:
    def getPermutation(self, n: int, k: int) -> str:
        factorials, nums = [1], ['1']
        for i in range(1, n):
            # generate factorial system bases 0!, 1!, ..., (n - 1)!
            factorials.append(factorials[i - 1] * i)
            # generate nums 1, 2, ..., n
            nums.append(str(i + 1))
        
        # fit k in the interval 0 ... (n! - 1)
        k -= 1
        
        # compute factorial representation of k
        output = []
        for i in range(n - 1, -1, -1):
            idx = k // factorials[i]
            k -= idx * factorials[i]
            
            output.append(nums[idx])
            del nums[idx]
        
        return ''.join(output)
复杂度分析

时间复杂度：\mathcal{O}(N^2)O(N 
2
 )，从列表中删除元素，共执行操作次数：
N + (N - 1) + ... + 1 = N(N - 1)/2
N+(N−1)+...+1=N(N−1)/2

空间复杂度：\mathcal{O}(N)O(N)。
下一篇：逆康托展开详解 Python

public class Solution {
    public string GetPermutation(int n, int k) {
        var candindates = new List<int>();
        int nj = GetJ(n, candindates);
        var result = new List<int>();
        //k是从1开始计数，换成重0开始
        Detect(k-1, nj, n, candindates, result);
        string resultStr = "";
        for(int i = 0; i<result.Count; i++)
        {
            resultStr = resultStr + result[i];
        }
        return resultStr;
    }

    //参数 n!, n
    private void Detect(int k, int nj, int n,  List<int> candindates, List<int> result)
    {
        if(candindates.Count == 1)
        {
            result.Add(candindates[0]);
            return;
        }
        //用(n-1)!求商取模， (n-1)! = n!/n
        int index = k/(nj/n);
        int left = k%(nj/n);
        //选取并去除选中的数字
        result.Add(candindates[index]);
        candindates.RemoveAt(index);
        //递归调用(n-1)!, n-1
        Detect(left, nj/n, n-1, candindates, result);
    }

    private int GetJ(int n, List<int> candindates)
    {
        int t = 1;
        for(int i=1; i<=n; i++)
        {
            candindates.Add(i);
            t*=i;
        }
        return t;
    }
}


 
*/