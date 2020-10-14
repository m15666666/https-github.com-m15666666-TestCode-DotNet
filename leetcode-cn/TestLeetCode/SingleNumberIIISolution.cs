/*
给定一个整数数组 nums，其中恰好有两个元素只出现一次，
其余所有元素均出现两次。 找出只出现一次的那两个元素。

示例 :

输入: [1,2,1,3,2,5]
输出: [3,5]
注意：

结果输出的顺序并不重要，对于上面的例子， [5, 3] 也是正确答案。
你的算法应该具有线性时间复杂度。你能否仅使用常数空间复杂度来实现？

*/

/// <summary>
/// https://leetcode-cn.com/problems/single-number-iii/
/// 260. 只出现一次的数字 III
/// </summary>
internal class SingleNumberIIISolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int[] SingleNumber(int[] nums)
    {
        int diffBits = 0;
        foreach (int num in nums) diffBits ^= num;

        int lastDiffBit = diffBits & (-diffBits);

        int one = 0;
        foreach (int num in nums) if ((num & lastDiffBit) != 0) one ^= num;

        int two = diffBits ^ one;
        return new int[] { one, two };
    }

    //public int[] SingleNumber(int[] nums)
    //{
    //    if (nums == null || nums.Length == 0) return new int[0];

    //    List<int> ret = new List<int>();
    //    Array.Sort(nums);

    //    int count = 0;
    //    int prevV = nums[0];

    //    for( int index = 0; index < nums.Length; index++ )
    //    {
    //        var v = nums[index];
    //        if( count == 0)
    //        {
    //            prevV = v;
    //            ++count;
    //            continue;
    //        }

    //        if( prevV == v )
    //        {
    //            count = 0;
    //            continue;
    //        }

    //        ret.Add( prevV );
    //        prevV = v;
    //        count = 1;
    //    }

    //    if(count == 1) ret.Add(prevV);

    //    return ret.ToArray();
    //}
}

/*
只出现一次的数字 III
力扣 (LeetCode)
发布于 2020-01-03
18.7k
概述
使用哈希表可以在 \mathcal{O}(N)O(N) 的时间复杂度和 \mathcal{O}(N)O(N) 的空间复杂度中解决该问题。

这个问题在常数的空间复杂度中解决有点困难，但可以借助两个位掩码来实现。

在这里插入图片描述

方法一：哈希表
建立一个值到频率的映射关系的哈希表，返回频率为 1 的数字。

算法：


class Solution {
  public int[] singleNumber(int[] nums) {
    Map<Integer, Integer> hashmap = new HashMap();
    for (int n : nums)
      hashmap.put(n, hashmap.getOrDefault(n, 0) + 1);

    int[] output = new int[2];
    int idx = 0;
    for (Map.Entry<Integer, Integer> item : hashmap.entrySet())
      if (item.getValue() == 1) output[idx++] = item.getKey();

    return output;
  }
}
复杂度分析

时间复杂度：\mathcal{O}(N)O(N)。
空间复杂度：\mathcal{O}(N)O(N)，哈希表所使用的空间。
方法二：两个掩码
本文将使用两个按位技巧：

使用异或运算可以帮助我们消除出现两次的数字；我们计算 bitmask ^= x，则 bitmask 留下的就是出现奇数次的位。
在这里插入图片描述

x & (-x) 是保留位中最右边 1 ，且将其余的 1 设位 0 的方法。
在这里插入图片描述

算法：

首先计算 bitmask ^= x，则 bitmask 不会保留出现两次数字的值，因为相同数字的异或值为 0。

但是 bitmask 会保留只出现一次的两个数字（x 和 y）之间的差异。

在这里插入图片描述

我们可以直接从 bitmask 中提取 x 和 y 吗？不能，但是我们可以用 bitmask 作为标记来分离 x 和 y。

我们通过 bitmask & (-bitmask) 保留 bitmask 最右边的 1，这个 1 要么来自 x，要么来自 y。

在这里插入图片描述
当我们找到了 x，那么 y = bitmask^x。


class Solution {
  public int[] singleNumber(int[] nums) {
    // difference between two numbers (x and y) which were seen only once
    int bitmask = 0;
    for (int num : nums) bitmask ^= num;

    // rightmost 1-bit diff between x and y
    int diff = bitmask & (-bitmask);

    int x = 0;
    // bitmask which will contain only x
    for (int num : nums) if ((num & diff) != 0) x ^= num;

    return new int[]{x, bitmask^x};
  }
}
复杂度分析

时间复杂度：\mathcal{O}(N)O(N) 的时间遍历输入数组。
空间复杂度：\mathcal{O}(1)O(1)。

采用分治的思想将问题降维
王勇
发布于 2019-05-30
4.5k
解题思路
第一步：
把所有的元素进行异或操作，最终得到一个异或值。因为是不同的两个数字，所以这个值必定不为 0；


       int xor = 0;
        for (int num : nums) {
            xor ^= num;
        } 
第二步：
取异或值最后一个二进制位为 1 的数字作为 mask，如果是 1 则表示两个数字在这一位上不同。


  int mask = xor & (-xor);
第三步：
通过与这个 mask 进行与操作，如果为 0 的分为一个数组，为 1 的分为另一个数组。这样就把问题降低成了：“有一个数组每个数字都出现两次，有一个数字只出现了一次，求出该数字”。对这两个子问题分别进行全异或就可以得到两个解。也就是最终的数组了。


        int[] ans = new int[2];
        for (int num : nums) {
            if ( (num & mask) == 0) {
                ans[0] ^= num;
            } else {
                ans[1] ^= num;
            }
        }
复杂度分析：
时间复杂度 O(N)O(N)，空间复杂度 O(1)O(1)

public class Solution {
    public int[] SingleNumber(int[] nums) {
            int v = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                v = v ^ nums[i];
            }
            int[] res = new int[2];
            v = v & (-v);
            for (int i =0; i < nums.Length; i++)
            {
                if ((nums[i] & v) == 0) res[0] ^= nums[i];
                else res[1] ^= nums[i];
            }
            return res;
    }
}



public class Solution {
    public int[] SingleNumber(int[] nums) {
        int x_xor_y = 0;
        foreach (var num in nums) {
            x_xor_y ^= num;
        }

        int x_diff_y = x_xor_y & -x_xor_y;
        int x = 0;
        foreach (var num in nums) {
            if ((num & x_diff_y) != 0) {
                x ^= num;
            }
        }

        return new int[] { x, x_xor_y ^ x };
    }
}



public class Solution {
    public int[] SingleNumber(int[] nums) {
            //1. find xor of these two X1
            //2. pick a random bit b which bit is 1, then that bit of n1 != n2
            //3. X1 xor with all nums that bit b is 1   -> n1
            //4. X1 xor with all nums that bit b is 0   -> n2
            var x1 = 0;
            foreach (var num in nums)
            {
                x1 ^= num;
            }

            int mask = 1;
            for (int i = 0; i < 32; i++)
            {
                mask = mask << 1;
                if ((x1 & mask) == mask)
                {
                    break;
                }
            }

            int n1 = x1;
            int n2 = x1;

            foreach (var num in nums)
            {
                if ((num & mask) == mask)
                {
                    n1 ^= num;
                }
                else
                {
                    n2 ^= num;
                }
            }

            return new[] {n1, n2};        
    }
}

public class Solution {
    public int[] SingleNumber(int[] nums)
    {
        HashSet<int> numbers = new HashSet<int>();
    foreach (int number in nums)
    {
        //在 C# 中，通过 HashSet 实例的 Add 方法添加元素时
        //如果添加元素已存在哈希集中，方法返回 False
        //注意逻辑非(!)操作符
        if (!numbers.Add(number))
            numbers.Remove(number); ;//重复便移除
    }
    return numbers.ToArray();
    }
}

public class Solution {
    public int[] SingleNumber(int[] nums) {
        if (nums == null || nums.Length <2)
        {
            return null;
        }

        int temp = GetResult(nums, 0, nums.Length - 1);

        int n = 0;
        while ((temp & 1) == 0)
        {
            temp = temp >> 1;
            n++;
        }

        int one = 0;
        int two = 0;

        for (int i = 0; i < nums.Length; i++)
        {
            if (((nums[i]>> n) & 1) == 1)
            {
                one ^= nums[i];
            }
            else
            {
                two ^= nums[i];
            }
        }

        return new int[]{one, two};
    }

    int GetResult(int[] array, int start, int end)
    {
        int result = 0;
        for (int i = start; i <= end; i++)
        {
            result ^= array[i];
        }
        return result;
    }
}

*/