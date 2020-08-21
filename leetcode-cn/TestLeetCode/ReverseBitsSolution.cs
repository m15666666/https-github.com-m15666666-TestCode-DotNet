using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
颠倒给定的 32 位无符号整数的二进制位。

示例 1：

输入: 00000010100101000001111010011100
输出: 00111001011110000010100101000000
解释: 输入的二进制串 00000010100101000001111010011100 表示无符号整数 43261596，
     因此返回 964176192，其二进制表示形式为 00111001011110000010100101000000。
示例 2：

输入：11111111111111111111111111111101
输出：10111111111111111111111111111111
解释：输入的二进制串 11111111111111111111111111111101 表示无符号整数 4294967293，
     因此返回 3221225471 其二进制表示形式为 10111111111111111111111111111111 。
 

提示：

请注意，在某些语言（如 Java）中，没有无符号整数类型。在这种情况下，输入和输出都将被指定为有符号整数类型，并且不应影响您的实现，因为无论整数是有符号的还是无符号的，其内部的二进制表示形式都是相同的。
在 Java 中，编译器使用二进制补码记法来表示有符号整数。因此，在上面的 示例 2 中，输入表示有符号整数 -3，输出表示有符号整数 -1073741825。
 
 
 
*/
/// <summary>
/// https://leetcode-cn.com/problems/reverse-bits/
/// 190. 颠倒二进制位
/// 
/// 
/// 颠倒给定的 32 位无符号整数的二进制位。
/// Input: 43261596
/// Output: 964176192 
/// Explanation: 43261596 represented in binary as 00000010100101000001111010011100, 
/// return 964176192 represented in binary as 00111001011110000010100101000000.
/// 
/// https://blog.csdn.net/hy971216/article/details/80524704
/// </summary>
class ReverseBitsSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public uint reverseBits(uint n) {
        n = (n >> 16) | (n << 16);
        n = ((n & 0xff00ff00) >> 8) | ((n & 0x00ff00ff) << 8);
        n = ((n & 0xf0f0f0f0) >> 4) | ((n & 0x0f0f0f0f) << 4);
        n = ((n & 0xcccccccc) >> 2) | ((n & 0x33333333) << 2);
        n = ((n & 0xaaaaaaaa) >> 1) | ((n & 0x55555555) << 1);
        return n;
    }

    //public uint reverseBits(uint n)
    //{
    //    uint reverse_num = 0;
    //    const int bitCount = 32;
    //    for ( int i = 0; i < bitCount; i++)
    //    {
    //       uint temp = (n & (uint)(1 << i));
    //        if (temp != 0)
    //            reverse_num |= (uint)(1 << ((bitCount - 1) - i));
    //    }

    //    return reverse_num;
    //}
}
/*


颠倒二进制位
力扣 (LeetCode)
发布于 2020-04-02
15.0k
方法一：逐位颠倒
虽然这个问题并不难，但它常常是面试开始时的一个热身问题。重点是测试一个人对数据类型和位操作的基本知识。

在面试的时候逐位颠倒作为最直接的解决方案。

在这里插入图片描述

尽管听起来很简单，但上述逻辑的不同实现产生不同的解决方案。例如，要检索整数 n 中最右边的位，可以应用模运算（即 n%2）或与运算（即 n &1）。另一个例子是，为了组合反转位（例如 2^a，2^b2 
a
 ，2 
b
 ）的结果，可以使用加法运算（即 2^a+2^b2 
a
 +2 
b
 ）或再次使用位或运算（即 2^a | 2^b2 
a
 ∣2 
b
 ）。

算法：
在这里，我们将展示基于上述逻辑的实现示例。

在这里插入图片描述
关键思想是，对于位于索引 i 处的位，在反转之后，其位置应为 31-i（注：索引从零开始）。

我们从右到左遍历输入整数的位字符串（即 n=n>>1）。要检索整数的最右边的位，我们应用与运算（n&1）。
对于每个位，我们将其反转到正确的位置（即（n&1）<<power）。然后添加到最终结果。
当 n==0 时，我们终止迭代。

class Solution:
    # @param n, an integer
    # @return an integer
    def reverseBits(self, n):
        ret, power = 0, 31
        while n:
            ret += (n & 1) << power
            n = n >> 1
            power -= 1
        return ret
复杂度分析

时间复杂度：\mathcal{O}(\log_2{N})O(log 
2
​	
 N)。在算法中，我们有一个循环来迭代输入的最高非零位，即\log_2{N}log 
2
​	
 N。
空间复杂度：\mathcal{O}(1)O(1)，因为不管输入是什么，内存的消耗是固定的。
方法二：带记忆化的按字节颠倒
有人可能会说，每字节（8 位的比特位）反转可能更有效。由于该题的输入是固定的 32 位整数，所以在本题中不一定是这样的。但是在处理长字节流时，它会变得更有效。

在这里插入图片描述

使用字节作为迭代单位的另一个隐含优点是，我们可以使用记忆化技术，可以缓存先前计算的值，以避免重新计算。

记忆化的后续问题是：如果该函数多次被调用，你该如何优化。

若要按自己为单位反转位，可以应用上述所示的算法。在这里，我们展示一种完全基于算术和位操作，不基于任何循环语句，如下所示：


def reverseByte(byte):
    return (byte * 0x0202020202 & 0x010884422010) % 1023
这个算法为用 3 个操作反转一个字节中的位，在 Sean Eron Anderson 的在线电子书 Bit Twiddling Hacks 中可以看到更多的细节。

算法：

我们按字节遍历整数。为了检索整数中最右边的字节，我们应用位掩码为 11111111 的与操作（即 n&0xff）。
对于每个字节，首先我们通过一个名为 reverseByte(byte) 的函数来反转字节中的位。然后将反转的结果添加到答案中。
对于函数 reverseByte(byte)，我们使用记忆化技术，它缓存函数的结果并直接返回结果，以便将来遇到相同的输入。
注意，可以选择更小的单位而不是字节，例如 4 位的单位，这将需要更多的计算来交换更少的缓存空间。记忆化技术是空间和计算时间之间的权衡。


class Solution:
    # @param n, an integer
    # @return an integer
    def reverseBits(self, n):
        ret, power = 0, 24
        cache = dict()
        while n:
            ret += self.reverseByte(n & 0xff, cache) << power
            n = n >> 8
            power -= 8
        return ret

    def reverseByte(self, byte, cache):
        if byte not in cache:
            cache[byte] = (byte * 0x0202020202 & 0x010884422010) % 1023 
        return cache[byte]
复杂度分析

时间复杂度：\mathcal{O}(1)O(1)。尽管我们在算法中有一个循环，但是无论输入是什么，迭代次数都是固定的，因为在我们的问题中整数是固定大小（32 位）的。
空间复杂度：\mathcal{O}(1)O(1)，同样，尽管我们使用了缓存来保留反转字节的结果，但缓存中的大小总数限制为 2^8=2562 
8
 =256。
方法三：
在方法 2 中，我们展示了一个关于如何在不使用循环语句的情况下反转字节中的位的示例。在面试过程中，你可能会被要求在不使用循环的情况下反转整个 32 位。在这里，我们提出了一种只使用位操作的解决方案。

这种思想可以看作是一种分治的策略，我们通过掩码将 32 位整数划分成具有较少位的块，然后通过将美俄个块反转，最后将每个块的结果合并得到最终结果。

在下图中，我们演示如何使用上述思想反转两个位。同样的，这个想法可以应用到比特块上。

在这里插入图片描述

算法：

我们可以通过以下步骤实现该算法：

首先，我们将原来的 32 位分为 2 个 16 位的块。
然后我们将 16 位块分成 2 个 8 位的块。
然后我们继续将这些块分成更小的块，直到达到 1 位的块。
在上述每个步骤中，我们将中间结果合并为一个整数，作为下一步的输入。

class Solution:
    # @param n, an integer
    # @return an integer
    def reverseBits(self, n):
        n = (n >> 16) | (n << 16)
        n = ((n & 0xff00ff00) >> 8) | ((n & 0x00ff00ff) << 8)
        n = ((n & 0xf0f0f0f0) >> 4) | ((n & 0x0f0f0f0f) << 4)
        n = ((n & 0xcccccccc) >> 2) | ((n & 0x33333333) << 2)
        n = ((n & 0xaaaaaaaa) >> 1) | ((n & 0x55555555) << 1)
        return n
复杂度分析

时间复杂度：\mathcal{O}(1)O(1)，没有使用循环。
空间复杂度：\mathcal{O}(1)O(1)，没有使用变量。
下一篇：一行代码，思路简单，性能双100

public class Solution {
	public uint reverseBits(uint n) 
	{
		uint result = 0;
		for(int i = 0; i < 32;i++)    
		{
			result += ((n & (1U << (31 - i))) != 0 ? 1U << i : 0);
		}
		return result;
	}
}

public class Solution {
    public uint reverseBits(uint n) {
        uint num1 = n & 0xAAAAAAAA;
            uint num2 = n & 0x55555555;
            n = (num1 >> 1) | (num2 << 1);
              num1 = n & 0xCCCCCCCC;
            num2 = n & 0x33333333;
            n = (num1 >> 2) | (num2 << 2);
            num1 = n & 0xF0F0F0F0 ;
            num2 = n & 0x0F0F0F0F;
            n = (num1 >> 4) | (num2 << 4);
            num1 = n & 0xFF00FF00;
            num2 = n & 0x00FF00FF;
            n = (num1 >> 8) | (num2 << 8);
            num1 = n & 0xFFFF0000;
            num2 = n & 0x0000FFFF;
            n = (num1 >> 16) | (num2 << 16);
            return n;
    }
}

public class Solution 
{
    public uint reverseBits(uint n) 
    {
        uint ret=0;
        int power=31;
        while(n!=0)
        {
            ret+=(n&1)<<power;
            n=n>>1;
            power--;
        }
        return ret;
    }
}

public class Solution {
    public uint reverseBits(uint n) {
        n = (n >> 16) | (n << 16);
        n = ((n & 0xff00ff00) >> 8) | ((n & 0x00ff00ff) << 8);
        n = ((n & 0xf0f0f0f0) >> 4) | ((n & 0x0f0f0f0f) << 4);
        n = ((n & 0xcccccccc) >> 2) | ((n & 0x33333333) << 2);
        n = ((n & 0xaaaaaaaa) >> 1) | ((n & 0x55555555) << 1);
        return n;
    }
}

public class Solution {
    public uint reverseBits(uint n) 
    {
        uint result=0;
        int pow=31;
        while(n>0)
        {
            result+=(n%2)<<pow;
            pow--;
            n = n>>1;
        }
        return result;
    }
}

public class Solution
{
	public uint reverseBits(uint n)
	{
		uint res = 0u, rp = 1u, lp = 1u << 31;
		while (lp != 0u)
		{
			res |= ((rp & n) != 0u) ? lp : 0u;
			rp <<= 1; lp >>= 1;
		}
		return res;
	}
}

 
 
 
*/