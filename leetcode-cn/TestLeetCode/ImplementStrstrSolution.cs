using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
实现 strStr() 函数。

给定一个 haystack 字符串和一个 needle 字符串，在 haystack 字符串中找出 needle 字符串出现的第一个位置 (从0开始)。如果不存在，则返回  -1。

示例 1:

输入: haystack = "hello", needle = "ll"
输出: 2
示例 2:

输入: haystack = "aaaaa", needle = "bba"
输出: -1
说明:

当 needle 是空字符串时，我们应当返回什么值呢？这是一个在面试中很好的问题。

对于本题而言，当 needle 是空字符串时我们应当返回 0 。这与C语言的 strstr() 以及 Java的 indexOf() 定义相符。
*/
/// <summary>
/// https://leetcode-cn.com/problems/implement-strstr/
/// 28. 实现 strStr()
/// 
/// 
/// </summary>
class ImplementStrstrSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    public int StrStr(string haystack, string needle)
    {
        int L = needle.Length;
        int n = haystack.Length;
        if (n < L) return -1;

        const int baseNumber = 26;
        const char a = 'a';
        long modulus = (long)Math.Pow(2, 31);

        long hashOfHay = 0;
        long hashOfNeedle = 0;
        for (int i = 0; i < L; ++i)
        {
            hashOfHay = (hashOfHay * baseNumber + (haystack[i] - a)) % modulus;
            hashOfNeedle = (hashOfNeedle * baseNumber + (needle[i] - a)) % modulus;
        }
        if (hashOfHay == hashOfNeedle) return 0;

        // 转换公式，使用同余的概念，预先计算同余的常数
        long maxOrder = 1; // 最高次的同余（26^L % modulus）
        for (int i = 1; i <= L; ++i) maxOrder = (maxOrder * baseNumber) % modulus;

        int upper = n - L + 1;
        for (int start = 1; start < upper; start++)
        {
            // 转换公式，使用同余的概念
            hashOfHay = (hashOfHay * baseNumber - (haystack[start - 1] - a) * maxOrder
                    + (haystack[start + L - 1] - a)) % modulus;
            if (hashOfHay == hashOfNeedle) return start;
        }
        return -1;
    }
}
/*
public class Solution { // 非常特别
    public int StrStr(string haystack, string needle) {
        if(needle.Length == 0)
        {
            return 0;
        }
        int[] next = new int[needle.Length];
        //上一个的next值
        int k = -1;
        next[0] = -1;
        int i = 0;
        while(i < next.Length - 1)
        {
            if(k  == -1 || needle[k] == needle[i])
            {
                next[++i] = ++k;
            }else{
                k = next[k];
            }
        }

        int needleIndex = 0;
        int hayIndex = 0;
        while(hayIndex < haystack.Length && needleIndex < needle.Length)
        {
            while(needleIndex >= 0 && haystack[hayIndex] != needle[needleIndex])
            {
                needleIndex = next[needleIndex];
            }
            hayIndex++;
            needleIndex++;
        }
        return needleIndex == needle.Length ? hayIndex - needle.Length : -1;
    }
}

using System;
public class Solution {
    public int StrStr(string haystack, string needle) {
        if(needle.Length == 0){
            return 0;
        }
        int i = 0;
        int j = 0;
        int[] next = getNext(needle);
        while( i < haystack.Length && j < needle.Length){
            if(j == -1 || haystack[i] == needle[j]){
                i++;
                j++;
            }else{
                j = next[j];
            }
        }

        if(j == needle.Length){
            return i - j;
        }else{
            return -1;
        }
    }

    public int[] getNext(string needle){
        int[] next = new int[needle.Length+1];
        next[0] = -1;
        int i = 0, j = -1;

        while(i < needle.Length){
            if(j == -1 || needle[i] == needle[j]){
                i++;
                j++;
                next[i] = j;
            }else{
                j = next[j];
            }
        }
        return next;
    }
}

实现 strStr()
力扣 (LeetCode)
发布于 18 天前
5.2k
概述
这道题是要在 haystack 字符串中找到 needle 字符串。下面会给出的三种解法，这三种解法都基于滑动窗口。

子串逐一比较的解法最简单，将长度为 L 的滑动窗口沿着 haystack 字符串逐步移动，并将窗口内的子串与 needle 字符串相比较，时间复杂度为 O((N - L)L)O((N−L)L)

显示上面这个方法是可以优化的。双指针方法虽然也是线性时间复杂度，不过它可以避免比较所有的子串，因此最优情况下的时间复杂度为 O(N)O(N)，但最坏情况下的时间复杂度依然为 O((N - L)L)O((N−L)L)。

有 O(N)O(N) 复杂度的解法嘛？答案是有的，有两种方法可以实现：

Rabin-Karp，通过哈希算法实现常数时间窗口内字符串比较。

比特位操作，通过比特掩码来实现常数时间窗口内字符串比较。

方法一：子串逐一比较 - 线性时间复杂度
最直接的方法 - 沿着字符换逐步移动滑动窗口，将窗口内的子串与 needle 字符串比较。

fig

实现

class Solution:
    def strStr(self, haystack: str, needle: str) -> int:
        L, n = len(needle), len(haystack)

        for start in range(n - L + 1):
            if haystack[start: start + L] == needle:
                return start
        return -1
复杂度分析

时间复杂度：O((N - L)L)O((N−L)L)，其中 N 为 haystack 字符串的长度，L 为 needle 字符串的长度。内循环中比较字符串的复杂度为 L，总共需要比较 (N - L) 次。

空间复杂度：O(1)O(1)。

方法二：双指针 - 线性时间复杂度
上一个方法的缺陷是会将 haystack 所有长度为 L 的子串都与 needle 字符串比较，实际上是不需要这么做的。

首先，只有子串的第一个字符跟 needle 字符串第一个字符相同的时候才需要比较。

fig

其次，可以一个字符一个字符比较，一旦不匹配了就立刻终止。

fig

如下图所示，比较到最后一位时发现不匹配，这时候开始回溯。需要注意的是，pn 指针是移动到 pn = pn - curr_len + 1 的位置，而 不是 pn = pn - curr_len 的位置。

fig

这时候再比较一次，就找到了完整匹配的子串，直接返回子串的开始位置 pn - L。

fig

算法

移动 pn 指针，直到 pn 所指向位置的字符与 needle 字符串第一个字符相等。

通过 pn，pL，curr_len 计算匹配长度。

如果完全匹配（即 curr_len == L），返回匹配子串的起始坐标（即 pn - L）。

如果不完全匹配，回溯。使 pn = pn - curr_len + 1， pL = 0， curr_len = 0。

实现

class Solution:
    def strStr(self, haystack: str, needle: str) -> int:
        L, n = len(needle), len(haystack)
        if L == 0:
            return 0

        pn = 0
        while pn < n - L + 1:
            # find the position of the first needle character
            # in the haystack string
            while pn < n - L + 1 and haystack[pn] != needle[0]:
                pn += 1
            
            # compute the max match string
            curr_len = pL = 0
            while pL < L and pn < n and haystack[pn] == needle[pL]:
                pn += 1
                pL += 1
                curr_len += 1
            
            # if the whole needle string is found,
            # return its start position
            if curr_len == L:
                return pn - L
            
            # otherwise, backtrack
            pn = pn - curr_len + 1
            
        return -1
复杂度分析

时间复杂度：最坏时间复杂度为 O((N - L)L)O((N−L)L)，最优时间复杂度为 O(N)O(N)。

空间复杂度：O(1)O(1)。

方法三： Rabin Karp - 常数复杂度
有一种最坏时间复杂度也为 O(N)O(N) 的算法。思路是这样的，先生成窗口内子串的哈希码，然后再跟 needle 字符串的哈希码做比较。

这个思路有一个问题需要解决，如何在常数时间生成子串的哈希码？

滚动哈希：常数时间生成哈希码

生成一个长度为 L 数组的哈希码，需要 O(L)O(L) 时间。

如何在常数时间生成滑动窗口数组的哈希码？利用滑动窗口的特性，每次滑动都有一个元素进，一个出。

由于只会出现小写的英文字母，因此可以将字符串转化成值为 0 到 25 的整数数组： arr[i] = (int)S.charAt(i) - (int)'a'。按照这种规则，abcd 整数数组形式就是 [0, 1, 2, 3]，转换公式如下所示。

h_0 = 0 \times 26^3 + 1 \times 26^2 + 2 \times 26^1 + 3 \times 26^0
h 
0
​	
 =0×26 
3
 +1×26 
2
 +2×26 
1
 +3×26 
0
 

可以将上面的公式写成通式，如下所示。其中 c_ic 
i
​	
  为整数数组中的元素，a = 26a=26，其为字符集的个数。

h_0 = c_0 a^{L - 1} + c_1 a^{L - 2} + ... + c_i a^{L - 1 - i} + ... + c_{L - 1} a^1 + c_L a^0
h 
0
​	
 =c 
0
​	
 a 
L−1
 +c 
1
​	
 a 
L−2
 +...+c 
i
​	
 a 
L−1−i
 +...+c 
L−1
​	
 a 
1
 +c 
L
​	
 a 
0
 

h_0 = \sum_{i = 0}^{L - 1}{c_i a^{L - 1 - i}}
h 
0
​	
 = 
i=0
∑
L−1
​	
 c 
i
​	
 a 
L−1−i
 

下面来考虑窗口从 abcd 滑动到 bcde 的情况。这时候整数形式数组从 [0, 1, 2, 3] 变成了 [1, 2, 3, 4]，数组最左边的 0 被移除，同时最右边新添了 4。滑动后数组的哈希值可以根据滑动前数组的哈希值来计算，计算公式如下所示。

h_1 = (h_0 - 0 \times 26^3) \times 26 + 4 \times 26^0
h 
1
​	
 =(h 
0
​	
 −0×26 
3
 )×26+4×26 
0
 

写成通式如下所示。

h_1 = (h_0 a - c_0 a^L) + c_{L + 1}
h 
1
​	
 =(h 
0
​	
 a−c 
0
​	
 a 
L
 )+c 
L+1
​	
 

如何避免溢出

a^La 
L
  可能是一个很大的数字，因此需要设置数值上限来避免溢出。设置数值上限可以用取模的方式，即用 h % modulus 来代替原本的哈希值。

理论上，modules 应该取一个很大数，但具体应该取多大的数呢? 详见这篇文章，对于这个问题来说 2^{31}2 
31
  就足够了。

算法

计算子字符串 haystack.substring(0, L) 和 needle.substring(0, L) 的哈希值。

从起始位置开始遍历：从第一个字符遍历到第 N - L 个字符。

根据前一个哈希值计算滚动哈希。

如果子字符串哈希值与 needle 字符串哈希值相等，返回滑动窗口起始位置。

返回 -1，这时候 haystack 字符串中不存在 needle 字符串。

实现

class Solution:
    def strStr(self, haystack: str, needle: str) -> int:
        L, n = len(needle), len(haystack)
        if L > n:
            return -1
        
        # base value for the rolling hash function
        a = 26
        # modulus value for the rolling hash function to avoid overflow
        modulus = 2**31
        
        # lambda-function to convert character to integer
        h_to_int = lambda i : ord(haystack[i]) - ord('a')
        needle_to_int = lambda i : ord(needle[i]) - ord('a')
        
        # compute the hash of strings haystack[:L], needle[:L]
        h = ref_h = 0
        for i in range(L):
            h = (h * a + h_to_int(i)) % modulus
            ref_h = (ref_h * a + needle_to_int(i)) % modulus
        if h == ref_h:
            return 0
              
        # const value to be used often : a**L % modulus
        aL = pow(a, L, modulus) 
        for start in range(1, n - L + 1):
            # compute rolling hash in O(1) time
            h = (h * a - h_to_int(start - 1) * aL + h_to_int(start + L - 1)) % modulus
            if h == ref_h:
                return start

        return -1
复杂度分析

时间复杂度：O(N)O(N)，计算 needle 字符串的哈希值需要 O(L)O(L) 时间，之后需要执行 (N - L)(N−L) 次循环，每次循环的计算复杂度为常数。

空间复杂度：O(1)O(1)。

实现

PythonJava
class Solution {
  // function to convert character to integer
  public int charToInt(int idx, String s) {
    return (int)s.charAt(idx) - (int)'a';
  }

  public int strStr(String haystack, String needle) {
    int L = needle.Length, n = haystack.Length;
    if (L > n) return -1;

    // base value for the rolling hash function
    int a = 26;
    // modulus value for the rolling hash function to avoid overflow
    long modulus = (long)Math.pow(2, 31);

    // compute the hash of strings haystack[:L], needle[:L]
    long h = 0, ref_h = 0;
    for (int i = 0; i < L; ++i) {
      h = (h * a + charToInt(i, haystack)) % modulus;
      ref_h = (ref_h * a + charToInt(i, needle)) % modulus;
    }
    if (h == ref_h) return 0;

    // const value to be used often : a**L % modulus
    long aL = 1;
    for (int i = 1; i <= L; ++i) aL = (aL * a) % modulus;

    for (int start = 1; start < n - L + 1; ++start) {
      // compute rolling hash in O(1) time
      h = (h * a - charToInt(start - 1, haystack) * aL
              + charToInt(start + L - 1, haystack)) % modulus;
      if (h == ref_h) return start;
    }
    return -1;
  }
}
复杂度分析

时间复杂度：O(N)O(N)，计算 needle 字符串的哈希值需要 O(L)O(L) 时间，之后需要执行 (N - L)(N−L) 次循环，每次循环的计算复杂度为常数。

空间复杂度：O(1)O(1)。


*/
