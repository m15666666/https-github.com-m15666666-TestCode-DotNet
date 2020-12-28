/*
不使用运算符 + 和 - ​​​​​​​，计算两整数 ​​​​​​​a 、b ​​​​​​​之和。

示例 1:

输入: a = 1, b = 2
输出: 3
示例 2:

输入: a = -2, b = 3
输出: 1

*/

/// <summary>
/// https://leetcode-cn.com/problems/sum-of-two-integers/
/// 371. 两整数之和
///
/// </summary>
internal class SumOfTwoIntegersSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int GetSum(int a, int b)
    {
        while (b != 0)
        {
            int temp = a ^ b;
            b = (a & b) << 1;
            a = temp;
        }
        return a;
    }
}

/*
利用位操作实现两数求和
phoenixfei
发布于 2019-09-12
11.7k
利用位操作实现加法
首先看十进制是如何做的： 5+7=12，三步走

第一步：相加各位的值，不算进位，得到2。
第二步：计算进位值，得到10. 如果这一步的进位值为0，那么第一步得到的值就是最终结果。
第三步：重复上述两步，只是相加的值变成上述两步的得到的结果2和10，得到12。
同样我们可以用三步走的方式计算二进制值相加： 5---101，7---111

第一步：相加各位的值，不算进位，得到010，二进制每位相加就相当于各位做异或操作，101^111。
第二步：计算进位值，得到1010，相当于各位进行与操作得到101，再向左移一位得到1010，(101&111)<<1。
第三步重复上述两步，各位相加 010^1010=1000，进位值为100=(010 & 1010)<<1。
继续重复上述两步：1000^100 = 1100，进位值为0，跳出循环，1100为最终结果。
结束条件：进位为0，即a为最终的求和结果。

class Solution {
    public int getSum(int a, int b) {
        while(b != 0){
            int temp = a ^ b;
            b = (a & b) << 1;
            a = temp;
        }
        return a;
    }
}

位运算详解以及在 Python 中需要的特殊处理
江不知
发布于 2019-06-11
29.1k
题目说不能使用运算符 + 和 -，那么我们就要使用其他方式来替代这两个运算符的功能。

位运算中的加法
我们先来观察下位运算中的两数加法，其实来来回回就只有下面这四种：


0 + 0 = 0
0 + 1 = 1
1 + 0 = 1
1 + 1 = 0（进位 1）
仔细一看，这可不就是相同位为 0，不同位为 1 的异或运算结果嘛~

异或和与运算操作
我们知道，在位运算操作中，异或的一个重要特性是无进位加法。我们来看一个例子：


a = 5 = 0101
b = 4 = 0100

a ^ b 如下：

0 1 0 1
0 1 0 0
-------
0 0 0 1
a ^ b 得到了一个无进位加法结果，如果要得到 a + b 的最终值，我们还要找到进位的数，把这二者相加。在位运算中，我们可以使用与操作获得进位：


a = 5 = 0101
b = 4 = 0100

a & b 如下：

0 1 0 1
0 1 0 0
-------
0 1 0 0
由计算结果可见，0100 并不是我们想要的进位，1 + 1 所获得的进位应该要放置在它的更高位，即左侧位上，因此我们还要把 0100 左移一位，才是我们所要的进位结果。

那么问题就容易了，总结一下：

a + b 的问题拆分为 (a 和 b 的无进位结果) + (a 和 b 的进位结果)
无进位加法使用异或运算计算得出
进位结果使用与运算和移位运算计算得出
循环此过程，直到进位为 0
在 Python 中的特殊处理
在 Python 中，整数不是 32 位的，也就是说你一直循环左移并不会存在溢出的现象，这就需要我们手动对 Python 中的整数进行处理，手动模拟 32 位 INT 整型。

具体做法是将整数对 0x100000000 取模，保证该数从 32 位开始到最高位都是 0。

具体实现

class Solution(object):
    def getSum(self, a, b):
        """
        :type a: int
        :type b: int
        :rtype: int
        """
        # 2^32
        MASK = 0x100000000
        # 整型最大值
        MAX_INT = 0x7FFFFFFF
        MIN_INT = MAX_INT + 1
        while b != 0:
            # 计算进位
            carry = (a & b) << 1 
            # 取余范围限制在 [0, 2^32-1] 范围内
            a = (a ^ b) % MASK
            b = carry % MASK
        return a if a <= MAX_INT else ~((a % MIN_INT) ^ MAX_INT)   
当然，如果你在 Python 中想要偷懒也行，毕竟 life is short……


class Solution(object):
    def getSum(self, a, b):
        """
        :type a: int
        :type b: int
        :rtype: int
        """
        return sum([a, b])
		
public class Solution {
    public int GetSum(int a, int b) {
        while(b != 0)
        {
            int tmp = a ^ b;
            b = (a & b) << 1;
            a = tmp;
        }
        return a;
    }
}

public class Solution {
    public int GetSum(int a, int b) {
		var aBitArray = new BitArray(BitConverter.GetBytes(a));
		var bBitArray = new BitArray(BitConverter.GetBytes(b));
		var addBitArray = new BitArray(32);

		var temp = false;
		for(int i = 0; i < 32; i++)
		{
			addBitArray[i] = aBitArray[i] ^ bBitArray[i] ^ temp;
			temp = (aBitArray[i] && bBitArray[i]) || (aBitArray[i] && temp) || (temp && bBitArray[i]);
		}
		var result = new int[1];
		addBitArray.CopyTo(result, 0);
		return result[0];
    }
}

*/