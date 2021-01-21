using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
UTF-8 中的一个字符可能的长度为 1 到 4 字节，遵循以下的规则：

对于 1 字节的字符，字节的第一位设为0，后面7位为这个符号的unicode码。
对于 n 字节的字符 (n > 1)，第一个字节的前 n 位都设为1，第 n+1 位设为0，后面字节的前两位一律设为10。剩下的没有提及的二进制位，全部为这个符号的unicode码。
这是 UTF-8 编码的工作方式：

   Char. number range  |        UTF-8 octet sequence
      (hexadecimal)    |              (binary)
   --------------------+---------------------------------------------
   0000 0000-0000 007F | 0xxxxxxx
   0000 0080-0000 07FF | 110xxxxx 10xxxxxx
   0000 0800-0000 FFFF | 1110xxxx 10xxxxxx 10xxxxxx
   0001 0000-0010 FFFF | 11110xxx 10xxxxxx 10xxxxxx 10xxxxxx
给定一个表示数据的整数数组，返回它是否为有效的 utf-8 编码。

注意:
输入是整数数组。只有每个整数的最低 8 个有效位用来存储数据。这意味着每个整数只表示 1 字节的数据。

示例 1:

data = [197, 130, 1], 表示 8 位的序列: 11000101 10000010 00000001.

返回 true 。
这是有效的 utf-8 编码，为一个2字节字符，跟着一个1字节字符。
示例 2:

data = [235, 140, 4], 表示 8 位的序列: 11101011 10001100 00000100.

返回 false 。
前 3 位都是 1 ，第 4 位为 0 表示它是一个3字节字符。
下一个字节是开头为 10 的延续字节，这是正确的。
但第二个延续字节不以 10 开头，所以是不符合规则的。 
*/
/// <summary>
/// https://leetcode-cn.com/problems/utf-8-validation/
/// 393. UTF-8 编码验证
/// </summary>
class UTF8ValidationSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public bool ValidUtf8(int[] data)
    {
        const byte Mask_10 = 0b11000000;
        const byte Compare_10 = 0b10000000;
        const byte Mask_110 = 0b11100000;
        const byte Compare_110 = 0b11000000;
        const byte Mask_1110 = 0b11110000;
        const byte Compare_1110 = 0b11100000;
        const byte Mask_11110 = 0b11111000;
        const byte Compare_11110 = 0b11110000;
        if (data == null || data.Length == 0) return true;
        
        for (int index0 = 0; index0 < data.Length; index0++) {
            int v = data[index0];
            if ((v & 0x80) == 0) { continue; }
            if ((v & Mask_110) == Compare_110) {

                int count = 1;
                while (0 < count--)
                {
                    index0++;
                    if (index0 == data.Length) return false;
                    if ((data[index0] & Mask_10) != Compare_10) return false;
                }
                continue;
            }
            if ((v & Mask_1110) == Compare_1110)
            {
                int count = 2;
                while (0 < count--)
                {
                    index0++;
                    if (index0 == data.Length) return false;
                    if ((data[index0] & Mask_10) != Compare_10) return false;
                }
                continue;
            }
            if ((v & Mask_11110) == Compare_11110)
            {
                int count = 3;
                while (0 < count--)
                {
                    index0++;
                    if (index0 == data.Length) return false;
                    if ((data[index0] & Mask_10) != Compare_10) return false;
                }
                continue;
            }
            return false;
        }
        return true;
    }
}
/*
UTF-8 编码验证
力扣 (LeetCode)

发布于 2019-07-01
6.5k
思路

这个问题非常有趣，因为它本身并不是特别难，但是确实是需要花一些功夫去处理一些细节。很多人解这道题的时候都会忽略一些细节，最后总有那么一到两个 Test Case 过不去。

说明： 下面的章节会给出三个例子来解释题目，如果你很清楚这道题了，那你可以直接跳过。

题述中给了两个例子来帮助你理解 UTF-8 字符集的规则。对于很多人来说这两个例子是不够的，所以接下来做的第一件事就是来理解题述中给的规则，同时再来看一个例子来帮助我们更好地理解这道题。那就开始吧。

一个合法的 UTF-8 字符的长度为 1-4 字节。
对于 1 字节的字符，字节的第一位设为 0，后面 7 位为这个符号的 unicode 码。
对于 n 字节的字符 (n > 1)，第一个字节的前 n 位都为 1，第 n+1 位为 0，后面字节的前两位一律都为 10。
输入是代表字符的整数数组。我们要判断数组中的数能不能表示成数个合法的 UTF-8 字符。在第一个的例子里面可以看到，数组上的可以表示称数个合法的 UTF-8 字符，因此这个数组是合法的。
有一个点需要注意的是，数组中的整数是有可能大于255的。我们都知道 8 比特能表示的最大整数为255。假如数组中有一个数为 476，那我们该怎么处理呢？ 根据题述例子中的说明，我们只需要考虑每个整数中 最高8位 就可以了。
现在规则已经搞清楚了，让我们先来看题述中例子，接下里再看一些可能造成困惑的其他例子。

例一


data = [197, 130, 1]
首先把这个数组表示成二进制的形式


11000101 10000010 00000001
记住，对于一个 n 字节长的 UTF-8 字符来说，第一个字节的前 n 位都为 1，第 n+1 位为 0，后面字节的前两位一律都为 10。


[1 1 0] 0 0 1 0 1
 ↑   ↑
第一个字节的前两个比特值都是 1，1 后面紧接着一个 0。这是一个合法 UTF-8 字符的开始。同时从中可知这是一个 2 字节长的 UTF-8 字符。这也就意味着序列中接下来的字节一定是 10xxxxxx 这样的形式。


[1 1 0] 0 0 1 0 1    [1 0] 0 0 0 0 1 0
 ↑   ↑                ↑ ↑
是的，确实是遵循了规则。因此数组中 197 130 这两个数共同组成了一个合法的 2 字节 UTF-8 字符。既然数组中还有元素，继续以同样的方式检查。接下来的整数是 1，先把它变成二进制表示。


00000001
在这里最高比特位是 0，那么它只能满足 1 字节 UTF-8 字符的规则了。让我们来回顾一下这个规则：

对于 1 字节的字符，字节的第一位设为 0，后面 7 位为这个符号的 unicode 码。


[0] 0 0 0 0 0 0 1
 ↑
显然，整数 1 本身就是一个合法的 1 字节 UTF-8 字符。这时候数组已经没有剩余数要处理了，从前面的分析可知整个数组表示的是两个合法的 UTF-8 字符，因此需要返回 True。

例子二


[235, 140, 4]
这是题述中的第二个例子。照样先把这个整数数组的转成二进制表示。


11101011 10001100 00000100
从数组的第一个整数开始。第一个字节会告诉我们这个 UTF-8 字符的长度，同时会告诉我们接下里该处理多少个字节来完全解析当前的 UTF-8 字符。


[1 1 1 0] 1 0 1 1
 ↑     ↑
可以看到前四个比特为 1110。这意味着这个 UTF-8 字符总共有 3 字节。记住，我们可以从第一个字节中知晓这个潜在 UTF-8 字符的长度。

对于 n 字节的字符 (n > 1)，第一个字节的前 n 位都为 1，第 n+1 位为 0。

根据这个规则我们知道了第一个 UTF-8 字符是长度为 3 字节。第一个字节已经处理过了，还需要处理剩下的两个字节。我们来看一下这个数组中剩下的两个字节。


[1 0] 0 0 1 1 0 0       0 0 0 0 0 1 0 0
 ↑ ↑                    ↑ (WRONG!)
第一个字节遵循了 10xxxxxx 规则，但是第二个字节违反了这个规则。根据第一个字节 11101011，这只能是一个 3 字节 UTF-8 字符。但是最后一个字节违反了规则。因此，这是一个非法字节，这也就意味着可以直接返回 False了。

例三

在看题解之前我们来看最后一个例子。在讨论区可以看到这个例子对很多人造成了困惑。

下面就是这个例子了：


[250,145,145,145,145]
照旧把数组中所有的整数转成二进制表示。


11111010 10010001 10010001 10010001 10010001
和前两个例子一样，首先看第一个字节来获取当前这个 UTF-8 字符的长度。从第一个字节中可以知道这个 UTF-8 字符的长度是 5 字节。


[1 1 1 1 1 0] 1 0
 ↑         ↑  
如果这是一个合法的 UTF-8 字符，那么接下来的四个字节必须遵守 10xxxxxx 规则。我们来看一下接下来的四个字节吧。


1. [1 0] 0 1 0 0 0 1
2. [1 0] 0 1 0 0 0 1
3. [1 0] 0 1 0 0 0 1
4. [1 0] 0 1 0 0 0 1
可以看到，这四个字节都是遵守 10xxxxxx 规则。那为什么这个例子需要返回 False 呢？ 这是因为你忽略了题目给的第一个规则。

题述中的第一个规则，很清楚地说了 “一个合法的 UTF-8 字符的长度为 1-4 字节。”

从第一个字节就知道了这个 UTF-8 字符将包含 5 字节 了，这也就意味着这个字符绝对是非法的。这也就是为什么对于这个例子应该返回 False了。

但愿上面我们一起看过的三个例子中能解答你心中大部分的疑惑。接下来我们来看题解吧。

方法一： 字符串操作
这个问题本身并不复杂。只要我们遵循题述中的规则，就不会做错了。下面将直接给出算法。

算法

对数组中每个整数进行处理。
对于每个整数，获取当前整数 字符串形式 的二进制表示。考虑到整数可能非常大，我们只需要只保留 最高 8 比特 就可以了。完成这个步骤之后，我们会获得一个 8 比特的二进制字符串数组。我们把这个数组叫做 bin_rep。
在接下来的步骤中有两个情况需要处理。
第一个情况是，我们正在处理某个 UTF-8 字符之中。在这个情况下，我们只需要检查前两个比特是不是 10 就可以了。bin_rep[:2] == "10"
第二个情况是刚开始处理一个新的 UTF-8字符。在这个情况下，我们需要查看这个字节的前缀，计算第一个 0 之前 1 的个数。这会告诉我们当前 UTF-8 字符的大小。
对于数组中的每个整数都这样处理，直到数组为空或者发现非法字符。
下面是算法的具体实现。


class Solution {
  public boolean validUtf8(int[] data) {

    // Number of bytes in the current UTF-8 character
    int numberOfBytesToProcess = 0;

    // For each integer in the data array.
    for (int i = 0; i < data.length; i++) {

      // Get the binary representation. We only need the least significant 8 bits
      // for any given number.
      String binRep = Integer.toBinaryString(data[i]);
      binRep =
          binRep.length() >= 8
              ? binRep.substring(binRep.length() - 8)
              : "00000000".substring(binRep.length() % 8) + binRep;

      // If this is the case then we are to start processing a new UTF-8 character.
      if (numberOfBytesToProcess == 0) {

        // Get the number of 1s in the beginning of the string.
        for (int j = 0; j < binRep.length(); j++) {
          if (binRep.charAt(j) == '0') {
            break;
          }

          numberOfBytesToProcess += 1;
        }

        // 1 byte characters
        if (numberOfBytesToProcess == 0) {
          continue;
        }

        // Invalid scenarios according to the rules of the problem.
        if (numberOfBytesToProcess > 4 || numberOfBytesToProcess == 1) {
          return false;
        }

      } else {

        // Else, we are processing integers which represent bytes which are a part of
        // a UTF-8 character. So, they must adhere to the pattern `10xxxxxx`.
        if (!(binRep.charAt(0) == '1' && binRep.charAt(1) == '0')) {
          return false;
        }
      }

      // We reduce the number of bytes to process by 1 after each integer.
      numberOfBytesToProcess -= 1;
    }

    // This is for the case where we might not have the complete data for
    // a particular UTF-8 character.
    return numberOfBytesToProcess == 0;
  }
}
复杂度分析

时间复杂度： O(N)O(N)
我们需要处理数组中的每一个整数，对每个整数提取它的二进制表示字符串。这个过程的复杂度为 O(N)O(N)，其中 NN 是数组大小。

空间复杂度： O(N)O(N)
对于每一个整数都需要创建一个二进制表示字符串。

方法二： 位操作
前一个方法就可以很好的解决问题了，但是字符串转换的一些操作会花很多时间，我们有更优雅的做法，那就是用位操作。

首先我们看一下一个整数表示的字节中有哪些部分是需要处理的。

如果在处理一个 UTF-8 字符的开始，我们需要提取一个字节的前 NN 比特，其中 NN 不会超过 4，之后的比特就不需要处理了。
如果是在处理一个 UTF-8 字符的过程中，我们只需要检查前两位是不是 10 就可以了。
我们来看一下怎么用位操作来完成以上两个任务。


mask = 1 << 7
while mask & num:
    n_bytes += 1
    mask = mask >> 1
首先我们创建了一个掩码，其值为 10000000。接下来会用这个掩码和整数做 逻辑与 操作，这样就可以知道有多少个 1 了。（记住，整数可能非常大，但我们只需要处理前 8 比特）

检查前两个比特是不是 10，我们可以用下面这两个掩码。


mask1 = 1 << 7
mask2 = 1 << 6

if not (num & mask1 and not (num & mask2)):
    return False
上面的代码可以高效地判断前两个比特是不是 10。如果不是，直接返回 False。

来看一下具体实现吧。


class Solution {
    public boolean validUtf8(int[] data) {

        // Number of bytes in the current UTF-8 character
        int numberOfBytesToProcess = 0;

        // Masks to check two most significant bits in a byte.
        int mask1 = 1 << 7;
        int mask2 = 1 << 6;

        // For each integer in the data array.
        for(int i = 0; i < data.length; i++) {
            // If this is the case then we are to start processing a new UTF-8 character.
            if (numberOfBytesToProcess == 0) {
                int mask = 1 << 7;
                 while ((mask & data[i]) != 0) {
                    numberOfBytesToProcess += 1;
                    mask = mask >> 1;
                 }

                // 1 byte characters
                if (numberOfBytesToProcess == 0) {
                    continue;
                }

                // Invalid scenarios according to the rules of the problem.
                if (numberOfBytesToProcess > 4 || numberOfBytesToProcess == 1) {
                    return false;
                }

            } else {

                // data[i] should have most significant bit set and
                // second most significant bit unset. So, we use the two masks
                // to make sure this is the case.
                if (!((data[i] & mask1) != 0 && (mask2 & data[i]) == 0)) {
                    return false;
                }
            }

            // We reduce the number of bytes to process by 1 after each integer.
            numberOfBytesToProcess -= 1;
        }

        // This is for the case where we might not have the complete data for
        // a particular UTF-8 character.
        return numberOfBytesToProcess == 0;
    }
}
复杂度分析

时间复杂度： O(N)O(N)
空间复杂度： O(1)O(1)

public class Solution {
    public bool ValidUtf8(int[] data) {
        int cnt = 0;
        foreach(int i in data)
        {
            if(cnt == 0)
            {
                if(i < 128) 
                {
                    continue;
                }
                else if(i >= 192 && i < 224)
                {
                    cnt = 1;
                }
                else if(i >= 224 && i < 240)
                {
                    cnt = 2;
                }
                else if(i >= 240 && i < 248)
                {
                    cnt = 3;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if(i >= 128 && i< 192 && cnt> 0) 
                {
                    cnt--;
                }
                else
                {
                    return false;
                }
            }
        }
        
        return cnt == 0 ? true : false;
    }
} 
*/