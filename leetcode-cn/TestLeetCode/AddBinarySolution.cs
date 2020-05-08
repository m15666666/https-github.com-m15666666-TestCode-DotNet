using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给你两个二进制字符串，返回它们的和（用二进制表示）。

输入为 非空 字符串且只包含数字 1 和 0。

 

示例 1:

输入: a = "11", b = "1"
输出: "100"
示例 2:

输入: a = "1010", b = "1011"
输出: "10101"
 

提示：

每个字符串仅由字符 '0' 或 '1' 组成。
1 <= a.length, b.length <= 10^4
字符串如果不是 "0" ，就都不含前导零。
*/
/// <summary>
/// https://leetcode-cn.com/problems/add-binary/
/// 67. 二进制求和
/// 
/// </summary>
class AddBinarySolution
{
    public void Test()
    {
    }

    public string AddBinary(string a, string b) {

        const char One = '1';
        int n = a.Length, m = b.Length;
        if (n < m) return AddBinary(b, a);

        StringBuilder ret = new StringBuilder( n + 1 );
        int carry = 0, j = m - 1;
        for(int i = n - 1; -1 < i; --i) 
        {
          if (a[i] == '1') ++carry;
          if ( -1 < j && b[j--] == One) ++carry;

          if (carry % 2 == 1) ret.Insert(0,One);
          else ret.Insert(0,'0');

          carry /= 2;
        }
        if (carry == 1) ret.Insert(0,One);

        return ret.ToString();
    }
}
/*

二进制求和
力扣 (LeetCode)
发布于 3 个月前
14.3k
综述
使用内置函数的简单方法：

将 a 和 b 转换为十进制整数。

求和。

将求和结果转换为二进制整数。

class Solution:
    def addBinary(self, a, b) -> str:
        return '{0:b}'.format(int(a, 2) + int(b, 2))
class Solution {
  public String addBinary(String a, String b) {
    return Integer.toBinaryString(Integer.parseInt(a, 2) + Integer.parseInt(b, 2));
  }
}
算法的时间复杂度为 \mathcal{O}(N + M)O(N+M)，但是该方法存在两个问题。

在 Java 中，该方法受输入字符串 a 和 b 的长度限制。字符串长度太大时，不能将其转换为 Integer，Long 或者 BigInteger 类型。
33 位 1，不能转换为 Integer。

65 位 1，不能转换为 Long。

500000001 位 1，不能转换为 BigInteger。

因此，为了适用于长度较大的字符串计算，应该使用逐比特位的计算方法。

如果输入的数字很大，该方法的效率非常低。
使用位操作提高计算速度。

方法一：逐位计算
算法

这是一种古老的经典算法，无需把数字转换成十进制，直接逐位计算和与进位即可。

初始进位 carry = 0carry=0，如果数字 aa 的最低位是 1，则将 1 加到进位 carrycarry；同理如果数字 bb 的最低位是 1，则也将 1 加到进位。此时最低位有三种情况：(00)_2(00) 
2
​	
 ，(01)_2(01) 
2
​	
  或者 (10)_2(10) 
2
​	
 。

然后将 carrycarry 的最低位作为最低位的值，将 carrycarry 的最高位移至下一位继续计算。

重复上述步骤，直到数字 aa 和 bb 的每一位计算完毕。最后如果 carrycarry 的最高位不为 0，则将最高位添加到计算结果的末尾。最后翻转结果得到求和结果。



class Solution {
  public String addBinary(String a, String b) {
    int n = a.length(), m = b.length();
    if (n < m) return addBinary(b, a);
    int L = Math.max(n, m);

    StringBuilder sb = new StringBuilder();
    int carry = 0, j = m - 1;
    for(int i = L - 1; i > -1; --i) {
      if (a.charAt(i) == '1') ++carry;
      if (j > -1 && b.charAt(j--) == '1') ++carry;

      if (carry % 2 == 1) sb.append('1');
      else sb.append('0');

      carry /= 2;
    }
    if (carry == 1) sb.append('1');
    sb.reverse();

    return sb.toString();
  }
}
复杂度分析

时间复杂度：\mathcal{O}(\max(N, M))O(max(N,M))，其中 NN 和 MM 是输入字符串 aa 和 bb 的长度。

空间复杂度：\mathcal{O}(\max(N, M))O(max(N,M))，存储求和结果。

方法二：位操作
思路

如果不允许使用加法运算，则可以使用位操作。

如果不了解位操作，请先从计算输入数据的 XOR 开始学习，同时练习以下题目：
只出现一次的数字 II，
只出现一次的数字 III，
数组中两个数的最大异或值，
重复的DNA序列，
最大单词长度乘积，
等等。

XOR 操作得到两个数字无进位相加的结果。



进位和两个数字与操作结果左移一位对应。



现在问题被简化为：首先计算两个数字的无进位相加结果和进位，然后计算无进位相加结果与进位之和。同理求和问题又可以转换成上一步，直到进位为 0 结束。

算法

把 aa 和 bb 转换成整型数字 xx 和 yy，xx 保存结果，yy 保存进位。

当进位不为 0：y != 0：

计算当前 xx 和 yy 的无进位相加结果：answer = x^y。

计算当前 xx 和 yy 的进位：carry = (x & y) << 1。

完成本次循环，更新 x = answer，y = carry。

返回 xx 的二进制形式。

class Solution:
    def addBinary(self, a, b) -> str:
        x, y = int(a, 2), int(b, 2)
        while y:
            answer = x ^ y
            carry = (x & y) << 1
            x, y = answer, carry
        return bin(x)[2:]
此方法在 Python 中用 4 行代码就能实现。

class Solution:
    def addBinary(self, a, b) -> str:
        x, y = int(a, 2), int(b, 2)
        while y:
            x, y = x ^ y, (x & y) << 1
        return bin(x)[2:]
性能分析

如果输入数字大于 2^{100}2 
100
 ，必须使用效率较低的 BigInteger。
在 Java 中，应当首先考虑使用 Integer 或者 Long，而不是 BigInteger。

复杂度分析

时间复杂度：\mathcal{O}(N + M)O(N+M)，其中 NN 和 MM 是输入字符串 aa 和 bb 的长度。

空间复杂度：\mathcal{O}(\max(N, M))O(max(N,M))，存储计算结果。

下一篇：画解算法：67. 二进制求和
 
*/
