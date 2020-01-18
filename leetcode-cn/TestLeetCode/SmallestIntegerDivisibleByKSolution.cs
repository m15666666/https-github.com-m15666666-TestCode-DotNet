using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定正整数 K，你需要找出可以被 K 整除的、仅包含数字 1 的最小正整数 N。

返回 N 的长度。如果不存在这样的 N，就返回 -1。

 

示例 1：

输入：1
输出：1
解释：最小的答案是 N = 1，其长度为 1。
示例 2：

输入：2
输出：-1
解释：不存在可被 2 整除的正整数 N 。
示例 3：

输入：3
输出：3
解释：最小的答案是 N = 111，其长度为 3。
 

提示：

1 <= K <= 10^5
*/
/// <summary>
/// https://leetcode-cn.com/problems/smallest-integer-divisible-by-k/
/// 1015. 可被 K 整除的最小整数
/// 
/// </summary>
class SmallestIntegerDivisibleByKSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int SmallestRepunitDivByK(int K)
    {
        if (K % 2 == 0 || K % 5 == 0) return -1;

        int v = 1;
        int len = 1;
        while (v % K != 0)
        {
            v %= K;
            v = v * 10 + 1;
            len += 1;
        }
        return len;
    }
}
/*
Java解法，以及证明的思路
凯凯😷
发布于 5 个月前
880 阅读
正常思路
通过以下代码，我们可以计算出的答案。首先我们检查x是否是K的倍数，如果不是，我们将其乘10加1。

while (x % K != 0) {
    x = x * 10 + 1;
}
优化方案
容易想到，反复乘10加1很快就会超出int能表示的范围。

对于任意整数x > 0x>0，存在非负整数pp和qq，使得x = pK + qx=pK+q。例如，x = 2x=2，K = 5K=5时，可以令p = 0p=0，q = 2q=2。又例如，x = 29x=29，K = 7K=7时，可以令p = 4p=4，q = 1q=1。

我们在计算答案时，使用了以下公式：

x_{i+1} = 10 x_i + 1
x 
i+1
​	
 =10x 
i
​	
 +1

请注意代码与公式的不同。代码中，我们把x_{i+1}x 
i+1
​	
 的值又保存在了x_ix 
i
​	
 的位置，覆盖了其原来的值。为了使得证明过程更加清晰明了，我们在公式中体现这种区别。

我们将前一条公式代入，因此有了：

10 x_i + 1 = 10 (pK + q) + 1 = 10 pK + 10 q + 1
10x 
i
​	
 +1=10(pK+q)+1=10pK+10q+1

由于等号两边相等，那么在两边分别对KK取余，其结果也应该相等。

(10 x_i + 1) \% K = (10 pK + 10 q + 1) \% K
(10x 
i
​	
 +1)%K=(10pK+10q+1)%K

观察等式右侧，有一项10 pK10pK，由于它是KK的倍数，因此无论pp为何值，这一项都不会影响最终结果，因此将它去掉，得到：

(10 x_i + 1) \% K = (10 q + 1) \% K
(10x 
i
​	
 +1)%K=(10q+1)%K

由于x = pK + qx=pK+q，我们能够推出x \% K = qx%K=q，带入上式：

(10 x_i + 1) \% K = (10 (x_i \% K) + 1) \% K
(10x 
i
​	
 +1)%K=(10(x 
i
​	
 %K)+1)%K

再看一眼上一节中的代码：

while (x % K != 0) {
    x = x * 10 + 1;
}
最后的公式告诉我们，x * 10 + 1和(x % K) * 10 + 1在后续判断x % K != 0时是没有任何区别的。因此我们可以将代码改成：

while (x % K != 0) {
    x = x % K;
    x = x * 10 + 1;
}
这样就可以避免x超范围了。

数学证明
上述算法有没有什么漏洞呢？我们来分析一下。

设上述程序执行完x = x % K这一句后，xx的值（或者称为状态）为SS。

那么一共有多少种SS呢？由于刚刚执行完x = x % K，所以xx是不可能大于等于KK的，因此SS种类数不会超过KK。

那么求解答案的过程中，可不可以重复经过某个状态S_iS 
i
​	
 呢？不可以，因为相同的xx值必然在后续的x = x * 10 + 1等语句中产生相同的结果，程序则必然陷入死循环。

那么，什么样的KK会使程序重复经过某个状态S_iS 
i
​	
 呢？

程序求解过程中，设程序经历了如下状态序列：S_1S 
1
​	
 ，S_2S 
2
​	
 ，S_3S 
3
​	
 ，\dots…，S_iS 
i
​	
 ，\dots…，S_jS 
j
​	
 ，\dots…，S_nS 
n
​	
 。

假设，存在S_i = S_jS 
i
​	
 =S 
j
​	
 ，也就是：

(10 S_{i-1} + 1) \% K = (10 S_{j-1} + 1) \% K
(10S 
i−1
​	
 +1)%K=(10S 
j−1
​	
 +1)%K

让我们把取余操作去掉，于是等式变成了：

10 S_{i-1} + 1 = 10 S_{j-1} + 1 + a K
10S 
i−1
​	
 +1=10S 
j−1
​	
 +1+aK

其中，aa是一个能使等式成立的值，类似上面的pp、qq。我们整理一下这个等式（假设S_{i-1} > S_{j-1}S 
i−1
​	
 >S 
j−1
​	
 ）：

S_{i-1} - S_{j-1} = \frac{aK}{10}
S 
i−1
​	
 −S 
j−1
​	
 = 
10
aK
​	
 

aa的取值范围是多少呢？S_{i-1}S 
i−1
​	
 和S_{j-1}S 
j−1
​	
 都是小于KK的（因为他们也刚经历了x = x % K语句），因此0 < 10(S_{i-1} - S_{j-1}) < 10K0<10(S 
i−1
​	
 −S 
j−1
​	
 )<10K，所以0 < a < 100<a<10。

使用一个大于0，小于10的整数，如何消除分母上的10，使得\frac{aK}{10} 
10
aK
​	
 成为一个整数呢？那就只有2 \times 5 = 102×5=10这条路了。

因此只有KK是2或5的倍数时，我们才能找到一个aa的值，使\frac{aK}{10} 
10
aK
​	
 成为一个整数，使S_{i-1} - S_{j-1} = \frac{aK}{10}S 
i−1
​	
 −S 
j−1
​	
 = 
10
aK
​	
 成立，使S_i = S_jS 
i
​	
 =S 
j
​	
 成立，最终程序会死循环。

所以，最终的程序如下所示：

public static int smallestRepunitDivByK(int K) {
    if (K % 2 == 0 || K % 5 == 0) {
        return -1;
    }
    int temp = 1;
    int len = 1;
    while (temp % K != 0) {
        temp = temp % K;
        temp = temp * 10 + 1;
        len += 1;
    }
    return len;
}
下一篇：C++ 数学法 

public class Solution {
    public int SmallestRepunitDivByK(int x) {
            if (x % 2 == 0 || x % 5 == 0) return -1;
            int n = 1, count = 1;
            while (true)
            {
                if ((n %= x) == 0) return count++;
                count++;
                n = n * 10 + 1;
            }
    }
}
*/
