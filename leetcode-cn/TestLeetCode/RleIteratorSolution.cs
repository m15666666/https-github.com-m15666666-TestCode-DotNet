using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
编写一个遍历游程编码序列的迭代器。

迭代器由 RLEIterator(int[] A) 初始化，其中 A 是某个序列的游程编码。更具体地，对于所有偶数 i，A[i] 告诉我们在序列中重复非负整数值 A[i + 1] 的次数。

迭代器支持一个函数：next(int n)，它耗尽接下来的  n 个元素（n >= 1）并返回以这种方式耗去的最后一个元素。如果没有剩余的元素可供耗尽，则  next 返回 -1 。

例如，我们以 A = [3,8,0,9,2,5] 开始，这是序列 [8,8,8,5,5] 的游程编码。这是因为该序列可以读作 “三个八，零个九，两个五”。

 

示例：

输入：["RLEIterator","next","next","next","next"], [[[3,8,0,9,2,5]],[2],[1],[1],[2]]
输出：[null,8,8,5,-1]
解释：
RLEIterator 由 RLEIterator([3,8,0,9,2,5]) 初始化。
这映射到序列 [8,8,8,5,5]。
然后调用 RLEIterator.next 4次。

.next(2) 耗去序列的 2 个项，返回 8。现在剩下的序列是 [8, 5, 5]。

.next(1) 耗去序列的 1 个项，返回 8。现在剩下的序列是 [5, 5]。

.next(1) 耗去序列的 1 个项，返回 5。现在剩下的序列是 [5]。

.next(2) 耗去序列的 2 个项，返回 -1。 这是由于第一个被耗去的项是 5，
但第二个项并不存在。由于最后一个要耗去的项不存在，我们返回 -1。
 

提示：

0 <= A.length <= 1000
A.length 是偶数。
0 <= A[i] <= 10^9
每个测试用例最多调用 1000 次 RLEIterator.next(int n)。
每次调用 RLEIterator.next(int n) 都有 1 <= n <= 10^9 。
*/
/// <summary>
/// https://leetcode-cn.com/problems/rle-iterator/
/// 900. RLE 迭代器
/// 
/// </summary>
class RleIteratorSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public RleIteratorSolution(int[] A)
    {
        _A = A;
        _index = _usedCount = 0;
    }

    private int[] _A;
    private int _index;
    private int _usedCount;
    public int Next(int n)
    {
        while (_index < _A.Length)
        {
            var count = _A[_index];
            if (count < _usedCount + n)
            {
                n -= (count - _usedCount);
                _usedCount = 0;
                _index += 2;
                continue;
            }
            _usedCount += n;
            return _A[_index + 1];
        }

        return -1;
    }
}
/*
方法一：维护下一个元素的位置和删除次数
分析

在调用 next() 函数时，我们不会真正删除剩余的元素（或者说改变数组 A 的值），而是维护两个变量 i 和 q，其中 i 表示迭代器当前指向的是元素 A[i + 1]，q 表示它已经被删除的次数，q 的值不会大于 A[i]。

例如，当数组 A 为 [1,2,3,4] 时，它表示的序列为 [2,4,4,4]。当 i 和 q 的值分别为 0 和 0 时，表示没有任何元素被删除；当 i 和 q 的值分别为 0 和 1 时，表示元素 A[0 + 1] = 2 被删除了 1 次；当 i 和 q 的值分别为 2 和 1 时，表示元素 A[2 + 1] = 4 被删除了 1 次，且之前的元素被全部删除。

算法

如果我们调用 next(n)，即删除 n 个元素，那么对于当前的元素 A[i + 1]，我们还可以删除的次数为 D = A[i] - q。

如果 n > D，那么我们会删除所有的 A[i + 1]，并迭代到下一个元素，即 n -= D; i += 2; q = 0。

如果 n <= D，那么我们删除的最后一个元素就为 A[i + 1]，即 q += D; return A[i + 1]。

JavaPython
class RLEIterator {
    int[] A;
    int i, q;

    public RLEIterator(int[] A) {
        this.A = A;
        i = q = 0;
    }

    public int next(int n) {
        while (i < A.length) {
            if (q + n > A[i]) {
                n -= A[i] - q;
                q = 0;
                i += 2;
            } else {
                q += n;
                return A[i+1];
            }
        }

        return -1;
    }
}

复杂度分析

时间复杂度：O(N + Q)O(N+Q)，其中 NN 是数组 A 的长度，QQ 是调用函数 next() 的次数。

空间复杂度：O(N)O(N)。
 

public class RLEIterator {

    List<int> numsList;
    List<int> countsList;
    
    int num_index = 0;
    int num_count = -1;
    int len = 0;
    
    public RLEIterator(int[] A) {
        numsList = new List<int>();
        countsList = new List<int>();
        
        for (int i = 0; i < A.Length; i += 2) {
            if (A[i] == 0) {
                continue;
            } else {
                countsList.Add(A[i]);
                numsList.Add(A[i+1]);
            }
        }
        
        len = countsList.Count;
    }
    
    public int Next(int n) {
        // Console.WriteLine(" next " + n);
        if (num_index >= len) {
            return -1;
        }
        
        int val = n;
        while(val > 0) {
            if (num_count + val < countsList[num_index]) {
                num_count += val;
                val = 0;
            } else {
                val -= countsList[num_index] - num_count;
                num_index += 1;
                num_count = 0;
            }
            
            // Console.WriteLine("num_index " + num_index);
            // Console.WriteLine("num_count = " + num_count);
            // Console.WriteLine("val = " + val);
            
            if (num_index >= len) {
                return -1;
            }
        }
        
        // Console.WriteLine("num_index " + num_index);
        // Console.WriteLine("num_count = " + num_count);
        
        return numsList[num_index];
    }
}
*/
