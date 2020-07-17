using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个非空整数数组，除了某个元素只出现一次以外，其余每个元素均出现了三次。找出那个只出现了一次的元素。

说明：

你的算法应该具有线性时间复杂度。 你可以不使用额外空间来实现吗？

示例 1:

输入: [2,2,3,2]
输出: 3
示例 2:

输入: [0,1,0,1,0,1,99]
输出: 99
 
*/
/// <summary>
/// https://leetcode-cn.com/problems/single-number-ii/
/// 137.只出现一次的数字II
/// 给定一个非空整数数组，除了某个元素只出现一次以外，其余每个元素均出现了三次。找出那个只出现了一次的元素。
/// 说明：
/// 你的算法应该具有线性时间复杂度。 你可以不使用额外空间来实现吗？
/// </summary>
class SingleNumberIISolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int SingleNumber(int[] nums) {
        int ones = 0, twos = 0;
        foreach(int num in nums)
        {
            ones = (ones ^ num) & (~twos);
            twos = (twos ^ num) & (~ones);
        }
        return ones;
    }

    //public int SingleNumber(int[] nums)
    //{
    //    if (nums == null || nums.Length == 0 || (nums.Length % 3 ) != 1 ) return -1;

    //    Array.Sort(nums);

    //    var length = nums.Length;
    //    int index = 0;

    //    while( index + 2 < length )
    //    {
    //        if (nums[index] != nums[index + 1]) return nums[index];
    //        index += 3;
    //    }

    //    return nums[index];
    //}

}
/*

只出现一次的数字 II
力扣 (LeetCode)
发布于 2020-02-17
21.4k
综述
该问题看起来很简单，使用 Set 或 HashMap 可以在 \mathcal{O}(N)O(N) 的时间和 \mathcal{O}(N)O(N) 的空间内解决。

真正的挑战在于 Google 面试官要求使用常数空间解决该问题（最近 6 个月该问题在 Google 上非常流行），测试应聘者是否熟练位操作。



方法一：HashSet
将输入数组存储到 HashSet，然后使用 HashSet 中数字和的三倍与数组之和比较。

3 \times (a + b + c) - (a + a + a + b + b + b + c) = 2 c
3×(a+b+c)−(a+a+a+b+b+b+c)=2c


class Solution:
    def singleNumber(self, nums):
        return (3 * sum(set(nums)) - sum(nums)) // 2
复杂度分析

时间复杂度：\mathcal{O}(N)O(N)，遍历输入数组。

空间复杂度：\mathcal{O}(N)O(N)，存储 N/3N/3 个元素的集合。

方法二：HashMap
遍历输入数组，统计每个数字出现的次数，最后返回出现次数为 1 的数字。


from collections import Counter
class Solution:
    def singleNumber(self, nums):
        hashmap = Counter(nums)
            
        for k in hashmap.keys():
            if hashmap[k] == 1:
                return k
复杂度分析

时间复杂度：\mathcal{O}(N)O(N)，遍历输入数组。

空间复杂度：\mathcal{O}(N)O(N)，存储 N/3N/3 个元素的 Set。

方法三：位运算符：NOT，AND 和 XOR
思路

使用位运算符可以实现 \mathcal{O}(1)O(1) 的空间复杂度。

\sim x \qquad \textrm{表示} \qquad \textrm{位运算 NOT}
∼x表示位运算 NOT

x \& y \qquad \textrm{表示} \qquad \textrm{位运算 AND}
x&y表示位运算 AND

x \oplus y \qquad \textrm{表示} \qquad \textrm{位运算 XOR}
x⊕y表示位运算 XOR

XOR

该运算符用于检测出现奇数次的位：1、3、5 等。

0 与任何数 XOR 结果为该数。

0 \oplus x = x
0⊕x=x

两个相同的数 XOR 结果为 0。

x \oplus x = 0
x⊕x=0

以此类推，只有某个位置的数字出现奇数次时，该位的掩码才不为 0。



因此，可以检测出出现一次的位和出现三次的位，但是要注意区分这两种情况。

AND 和 NOT

为了区分出现一次的数字和出现三次的数字，使用两个位掩码：seen_once 和 seen_twice。

思路是：

仅当 seen_twice 未变时，改变 seen_once。

仅当 seen_once 未变时，改变seen_twice。



位掩码 seen_once 仅保留出现一次的数字，不保留出现三次的数字。


class Solution:
    def singleNumber(self, nums: List[int]) -> int:
        seen_once = seen_twice = 0
        
        for num in nums:
            # first appearance: 
            # add num to seen_once 
            # don't add to seen_twice because of presence in seen_once
            
            # second appearance: 
            # remove num from seen_once 
            # add num to seen_twice
            
            # third appearance: 
            # don't add to seen_once because of presence in seen_twice
            # remove num from seen_twice
            seen_once = ~seen_twice & (seen_once ^ num)
            seen_twice = ~seen_once & (seen_twice ^ num)

        return seen_once
复杂度分析

时间复杂度：\mathcal{O}(N)O(N)，遍历输入数组。

空间复杂度：\mathcal{O}(1)O(1)，不使用额外空间。

下一篇：137. 只出现一次的数字 II（有限状态自动机 + 位运算，清晰图解）


137. 只出现一次的数字 II（有限状态自动机 + 位运算，清晰图解）
Krahets
发布于 2019-06-14
27.1k
解题思路：
如下图所示，考虑数字的二进制形式，对于出现三次的数字，各 二进制位 出现的次数都是 33 的倍数。
因此，统计所有数字的各二进制位中 11 的出现次数，并对 33 求余，结果则为只出现一次的数字。

Picture1.png

方法一：有限状态自动机
各二进制位的 位运算规则相同 ，因此只需考虑一位即可。如下图所示，对于所有数字中的某二进制位 11 的个数，存在 3 种状态，即对 3 余数为 0, 1, 20,1,2 。

若输入二进制位 11 ，则状态按照以下顺序转换；
若输入二进制位 00 ，则状态不变。
0 \rightarrow 1 \rightarrow 2 \rightarrow 0 \rightarrow \cdots
0→1→2→0→⋯

Picture2.png

如下图所示，由于二进制只能表示 0, 10,1 ，因此需要使用两个二进制位来表示 33 个状态。设此两位分别为 twotwo , oneone ，则状态转换变为：

00 \rightarrow 01 \rightarrow 10 \rightarrow 00 \rightarrow \cdots
00→01→10→00→⋯

Picture3.png

接下来，需要通过 状态转换表 导出 状态转换的计算公式 。首先回忆一下位运算特点，对于任意二进制位 xx ，有：

异或运算：x ^ 0 = x​ ， x ^ 1 = ~x
与运算：x & 0 = 0 ， x & 1 = x
计算 oneone 方法：

设当前状态为 twotwo oneone ，此时输入二进制位 nn 。如下图所示，通过对状态表的情况拆分，可推出 oneone 的计算方法为：


if two == 0:
  if n == 0:
    one = one
  if n == 1:
    one = ~one
if two == 1:
    one = 0
引入 异或运算 ，可将以上拆分简化为：


if two == 0:
    one = one ^ n
if two == 1:
    one = 0
引入 与运算 ，可继续简化为：


one = one ^ n & ~two
Picture4.png

计算 twotwo 方法：

由于是先计算 oneone ，因此应在新 oneone 的基础上计算 twotwo 。
如下图所示，修改为新 oneone 后，得到了新的状态图。观察发现，可以使用同样的方法计算 twotwo ，即：


two = two ^ n & ~one
Picture5.png

返回值：

以上是对数字的二进制中 “一位” 的分析，而 int 类型的其他 31 位具有相同的运算规则，因此可将以上公式直接套用在 32 位数上。

遍历完所有数字后，各二进制位都处于状态 0000 和状态 0101 （取决于 “只出现一次的数字” 的各二进制位是 11 还是 00 ），而此两状态是由 oneone 来记录的（此两状态下 twostwos 恒为 00 ），因此返回 onesones 即可。

复杂度分析：
时间复杂度 O(N)O(N) ： 其中 NN 位数组 numsnums 的长度；遍历数组占用 O(N)O(N) ，每轮中的常数个位运算操作占用 O(32 \times3 \times 2) = O(1)O(32×3×2)=O(1) 。
空间复杂度 O(1)O(1) ： 变量 onesones , twostwos 使用常数大小的额外空间。


代码：

class Solution {
    public int singleNumber(int[] nums) {
        int ones = 0, twos = 0;
        for(int num : nums){
            ones = ones ^ num & ~twos;
            twos = twos ^ num & ~ones;
        }
        return ones;
    }
}
方法二：遍历统计
此方法相对容易理解，但效率较低，总体推荐方法一。

使用 与运算 ，可获取二进制数字 numnum 的最右一位 n_1n 
1
​	
  ：

n_1 = num \& i
n 
1
​	
 =num&i

配合 无符号右移操作 ，可获取 numnum 所有位的值（即 n_1n 
1
​	
  ~ n_{32}n 
32
​	
  ）：

num = num >>> 1
num=num>>>1

建立一个长度为 32 的数组 countscounts ，通过以上方法可记录所有数字的各二进制位的 11 的出现次数。


int[] counts = new int[32];
for(int i = 0; i < nums.length; i++) {
    for(int j = 0; j < 32; j++) {
        counts[j] += nums[i] & 1; // 更新第 j 位
        nums[i] >>>= 1; // 第 j 位 --> 第 j + 1 位
    }
}
将 countscounts 各元素对 33 求余，则结果为 “只出现一次的数字” 的各二进制位。


for(int i = 0; i < 32; i++) {
    counts[i] %= 3; // 得到 只出现一次的数字 的第 (31 - i) 位 
}
利用 左移操作 和 或运算 ，可将 countscounts 数组中各二进位的值恢复到数字 resres 上（循环区间是 i \in [0, 31]i∈[0,31] ）。


for(int i = 0; i < counts.length; i++) {
    res <<= 1; // 左移 1 位
    res |= counts[31 - i]; // 恢复第 i 位的值到 res
}
最终返回 resres 即可。

由于 Python 的存储负数的特殊性，需要先将 00 - 3232 位取反（即 res ^ 0xffffffff ），再将所有位取反（即 ~ ）。
两个组合操作实质上是将数字 3232 以上位取反， 00 - 3232 位不变。

复杂度分析：
时间复杂度 O(N)O(N) ： 其中 NN 位数组 numsnums 的长度；遍历数组占用 O(N)O(N) ，每轮中的常数个位运算操作占用 O(1)O(1) 。
空间复杂度 O(1)O(1) ： 数组 countscounts 长度恒为 3232 ，占用常数大小的额外空间。
代码：
实际上，只需要修改求余数值 mm ，即可实现解决 除了一个数字以外，其余数字都出现 mm 次 的通用问题。


class Solution {
    public int singleNumber(int[] nums) {
        int[] counts = new int[32];
        for(int num : nums) {
            for(int j = 0; j < 32; j++) {
                counts[j] += num & 1;
                num >>>= 1;
            }
        }
        int res = 0, m = 3;
        for(int i = 0; i < 32; i++) {
            res <<= 1;
            res |= counts[31 - i] % m;
        }
        return res;
    }
}
下一篇：逻辑电路角度详细分析该题思路，可推广至通解

public class Solution {
    public int SingleNumber(int[] nums) {
		int seenOnce = 0, seenTwice = 0;
            foreach (int num in nums)
            {
                // first appearence: 
                // add num to seen_once 
                // don't add to seen_twice because of presence in seen_once

                // second appearance: 
                // remove num from seen_once 
                // add num to seen_twice

                // third appearance: 
                // don't add to seen_once because of presence in seen_twice
                // remove num from seen_twice
                seenOnce = ~seenTwice & (seenOnce ^ num);
                seenTwice = ~seenOnce & (seenTwice ^ num);
            }

            return seenOnce;
    }
}

public class Solution {
    public int SingleNumber(int[] nums) {
        int f = 0, s = 0;
        for(int idx = 0; idx < nums.Length; idx++)
        {
            f = f ^ nums[idx] & ~s;
            s = s ^ nums[idx] & ~f;
        }
        return f;
    }
}

public class Solution {
    public int SingleNumber(int[] nums) {
        HashSet<int> h = new HashSet<int>();
        Dictionary<int,int> d = new Dictionary<int,int>();
        for(int i=0;i<nums.Length;++i)
        {
            if(!d.ContainsKey(nums[i]))
            {
                d.Add(nums[i],1);
                h.Add(nums[i]);
            }
            else
            {
                if(d[nums[i]]==1)
                {
                    d.Remove(nums[i]);
                    d.Add(nums[i],2);
                }
                else if(d[nums[i]]==2)
                {
                    d.Remove(nums[i]);
                    h.Remove(nums[i]);
                }
            }
        }
        return h.First();
    }
}

public class Solution {
    public int SingleNumber(int[] nums) {
        long hashnums=0,tempnums=0;
        HashSet<int> hs=new HashSet<int>();
        foreach(int i in nums){
            tempnums+=i;
            hs.Add(i);
        }
        foreach(int j in hs){
            hashnums+=j;
        }
        long res=(hashnums*3-tempnums)/2;
        return (int)res;
    }
}

public class Solution {
    public int SingleNumber(int[] nums) {
Dictionary<int, int> dic = new Dictionary<int, int>();
            for (int i = 0; i < nums.Length; i++)
            {
                if (!dic.ContainsKey(nums[i]))
                    dic.Add(nums[i], 1);
                else
                {
                    dic[nums[i]] = -1;
                }
            }
            int ret = 0;
            foreach (int item in dic.Keys)
            {
                if (dic[item] == 1)
                    ret = item;
            }
            return ret;
    }
}

public class Solution {
    public int SingleNumber(int[] nums) {
        HashSet<int> h = new HashSet<int>();
        Dictionary<int,int> d = new Dictionary<int,int>();
        for(int i=0;i<nums.Length;++i)
        {
            if(!d.ContainsKey(nums[i]))
            {
                d.Add(nums[i],1);
                h.Add(nums[i]);
            }
            else
            {
                if(d[nums[i]]==1)
                {
                    d.Remove(nums[i]);
                    d.Add(nums[i],2);
                }
                else if(d[nums[i]]==2)
                {
                    d.Remove(nums[i]);
                    h.Remove(nums[i]);
                }
            }
        }
        return h.First();
    }
}

public class Solution {
    public int SingleNumber(int[] nums) {
      Dictionary<int,int> dict=new Dictionary<int,int>();
      for(int i=0;i<nums.Length;i++)//
      {
        if(dict.ContainsKey(nums[i]))
        {
            dict[nums[i]]++;
        }
        else
        {
            dict.Add(nums[i],1);
        }
      }
      return dict.Single(a=>a.Value==1).Key;
    }
}

 
 
*/