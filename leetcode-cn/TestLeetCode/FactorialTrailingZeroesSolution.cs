/*
给定一个整数 n，返回 n! 结果尾数中零的数量。

示例 1:

输入: 3
输出: 0
解释: 3! = 6, 尾数中没有零。
示例 2:

输入: 5
输出: 1
解释: 5! = 120, 尾数中有 1 个零.
说明: 你算法的时间复杂度应为 O(log n) 。

*/

/// <summary>
/// https://leetcode-cn.com/problems/factorial-trailing-zeroes
/// 172. 阶乘后的零
///
///
///
///
/// </summary>
internal class FactorialTrailingZeroesSolution
{
    public int TrailingZeroes(int n)
    {
        long N = n;
        long zeroCount = 0;
        long currentMultiple = 5;
        while (currentMultiple <= N)
        {
            zeroCount += (N / currentMultiple);
            currentMultiple *= 5;
        }
        return (int)zeroCount;
    }
}

/*

阶乘后的零
力扣 (LeetCode)
发布于 2020-05-13
8.3k
方法一：计算阶乘
这种方法速度太慢了，但却是一个好的起点。虽然不会在面试中实现它，但是你可以简单的描述它是个解决问题的办法之一。

解决这个问题的最简单的办法就是计算 n!n!，然后计算它的末尾数 0 个数。阶乘是通过将所有在 11 和 nn 之间的数字相乘计算的。例如，10! = 10 \cdot 9 \cdot 8 \cdot 7 \cdot 6 \cdot 5 \cdot 4 \cdot 3 \cdot 2 \cdot 1 = 3,628,80010!=10⋅9⋅8⋅7⋅6⋅5⋅4⋅3⋅2⋅1=3,628,800。因此，可以使用以下算法迭代计算阶乘。


define function factorial(n):
    n_factorial = 1
    for i from 1 to n (inclusive):
        n_factorial = n_factorial * i
    return n_factorial
如果一个数字末尾有零，那么它可以被 1010 整除。除以 1010 将删除该零，并将所有其他数字右移一位。因此，我们可以通过反复检查数字是否可以被 1010 整除来计算末尾 0 的个数。


define function zero_count(x):
    zero_count = 0
    while x is divisible by 10: 
        zero_count += 1
        x = x / 10
    return zero_count
通过将这两个函数放到一起，我们可以计算阶乘后的零个数。

算法：

在 Java 中，我们需要使用 BigInteger，防止在计算阶乘的过程中溢出。


def trailingZeroes(self, n: int) -> int:
        
    # Calculate n!
    n_factorial = 1
    for i in range(2, n + 1):
        n_factorial *= i
    
    # Count how many 0's are on the end.
    zero_count = 0
    while n_factorial % 10 == 0:
        zero_count += 1
        n_factorial //= 10
        
    return zero_count
复杂度分析

时间复杂度：低于 O(n ^ 2)O(n 
2
 )。
计算阶乘是重复的乘法。通常，当我们知道乘法是固定大小的数字上（例如 32 位或 64 位整数）时，我们可以视为 O(1)O(1) 运算。但是，这里要乘以的数字会随着 nn 大小而增长，所以这里不能这么做。

因此，这里的第一步是考虑乘法的成本，因为我们不能假设它是 O(1)O(1)。把两个大数字相乘的流行方法它的成本是 O((\log x) \cdot (\log y))O((logx)⋅(logy))。我们将在近似值中使用它。

接下来，我们考虑以下在计算 n!n! 时，我们做了什么乘法运算。前几个乘法如下：

1 \cdot 2 = 21⋅2=2
2 \cdot 3 = 62⋅3=6
6 \cdot 4 = 246⋅4=24
24 \cdot 5 = 12024⋅5=120
120 \cdot 6 = 720120⋅6=720
......

这些乘法的成本：

\log 1 \cdot \log 2log1⋅log2
\log 2 \cdot \log 3log2⋅log3
\log 6 \cdot \log 4log6⋅log4
\log 24 \cdot \log 5log24⋅log5
\log 120 \cdot \log 6log120⋅log6
......

我们可以改写为：

\log , 1! \cdot \log , 2log,1!⋅log,2
\log , 2! \cdot \log , 3log,2!⋅log,3
\log , 3! \cdot \log , 4log,3!⋅log,4
\log , 4! \cdot \log , 5log,4!⋅log,5
\log , 5! \cdot \log , 6log,5!⋅log,6
......

发现了吗？每行的格式为 (\log k!) \cdot (\log k + 1)(logk!)⋅(logk+1)，最后一行是什么？计算阶乘的最后一步是乘以 nn。因此，最后一行必须是：

看到图案了吗？每行的格式为（\log，k！）\cdot（\log，k+1）（log，k！）⋅（log，k+1）。最后一行是什么？计算阶乘的最后一步是乘以n$。因此，最后一行必须是：

\log ((n - 1)!) \cdot \log (n)log((n−1)!)⋅log(n)

因为我们一个接一个地做这些乘法运算，所以我们应该把它们相加，得到总的时间复杂度。得到：

\log 1! \cdot \log 2 + \log 2! \cdot \log 3 + \log 3! \cdot \log 4 + \cdots + \log ((n - 2)!) \cdot \log (n - 1) + \log ((n - 1)!) \cdot \log nlog1!⋅log2+log2!⋅log3+log3!⋅log4+⋯+log((n−2)!)⋅log(n−1)+log((n−1)!)⋅logn

这个序列相加起来相当复杂，我们不是要找到一个确切的答案，而是通过扔掉不太重要的项，找到粗略的下界近似。

在这一点上，你会发现算法比 O(n)O(n) 差，因为我们添加了 nn 项。考虑到这个问题要求我们提出一个不低于 O(\log n)O(logn) 的算法。我们将进一步探讨，但是如果你已经理解到这一点，已经十分棒了。

注意 \log ((n - 1)!)log((n−1)!) 比 \log nlogn 大的多。因此，我们将删除这部分，留下 \log ((n - 1)!)log((n−1)!)。得到：

\log 1! + \log 2! + \log 3! + \cdots + \log ((n - 2)!) + \log ((n - 1)!)log1!+log2!+log3!+⋯+log((n−2)!)+log((n−1)!)

下一部分涉及到一个 log 原则，你可能听说过，也可能没有。如果你还没听说过，那么绝对指的记住，因为它非常有用。

O(\log n!) = O(n \log n)O(logn!)=O(nlogn)

我们根据这个原则重写序列：

1 \cdot \log 1 + 2 \cdot \log 2 + 3 \cdot \log 3 + \cdots + (n - 2) \cdot \log (n - 2) + (n - 1) \cdot \log (n - 1)1⋅log1+2⋅log2+3⋅log3+⋯+(n−2)⋅log(n−2)+(n−1)⋅log(n−1)

像以前一样，我们把较小的项去掉，看看剩下什么：

1 + 2 + 3 + ... + (n - 2) + (n - 1)1+2+3+...+(n−2)+(n−1)

这是个非常常见的序列，它的成本是 O(n^2)O(n 
2
 )。

那么，我们能得出什么结论呢？丢弃了项以后会使我们的时间复杂度低于真实的时间复杂度。换句话说，这个阶乘算法复杂度小于 O(n^2)O(n 
2
 )。

但是 O(n^2)O(n 
2
 ) 绝对不够好！。

尽管这种丢弃项的方法看起来有点奇怪，但快速做出早期决策非常有用，而不必费心与高等数学。只有当我们决定进一步研究改算法时，才会尝试得出更加精确的时间复杂度。在这种情况下，我们的下限足够让我们相信它绝对不值得一看！

第二部分，在结尾数零，与第一部分相比微不足道。

空间复杂度：O(\log n!) = O(n \log n)O(logn!)=O(nlogn)，为了存储 n!n!，我们需要 O(\log n!)O(logn!) 位，而它等于 O(n \log n)O(nlogn)。
方法二：计算因子 5
这种方法也太慢了，但是在解决问题的过程中，它很可能是提出对数方法的一个步骤。

与方法 1 中那样计算阶乘不同，我们可以认识到阶乘末尾的每个 00 表示乘以 1010。

那么，我们在计算 n!n! 时乘以 1010 的次数是多少？两个数字 aa 和 bb 相乘。例如，要执行 42 \cdot 75 = 315042⋅75=3150，可以重写如下：

42 = 2 \cdot 3 \cdot 742=2⋅3⋅7
75 = 3 \cdot 5 \cdot 575=3⋅5⋅5
42 \cdot 75 = 2 \cdot 3 \cdot 7 \cdot 3 \cdot 5 \cdot 542⋅75=2⋅3⋅7⋅3⋅5⋅5

现在，为了确定最后有多少个零，我们应该看有多少对 22 和 55 的因子。在上面的例子中，我们有一对 22 和 55 的因子。

那么，这和阶乘有什么关系呢？在一个阶乘中，我们把所有 11 和 nn 之间的数相乘，这和把所有 11 和 nn 之间所有数字的因子相乘是一样的。

例如，如果 n=16n=16，我们需要查看 11 到 1616 之间所有数字的因子。我们只对 22 和 55 有兴趣。包含 55 因子的数字是 {5，10，15}5，10，15，包含因子 22 的数字是 {2、4、6、8、10、12、14、16}2、4、6、8、10、12、14、16。因为只三个完整的对，因此 16!16! 后有三个零。

把这个放到一个算法中，我们得到：


twos = 0
for i from 1 to n inclusive:
    if i is divisible by 2:
        twos += 1

fives = 0
for i from 1 to n inclusive:
    if i is divisible by 5:
        fives += 1

tens = min(fives, twos)
这可以解决大部分情况，但是有的数字存在一个以上的因子。例如，若 i = 25，那么我们只做了 fives += 1。但是我们应该 fives += 2，因为 2525 有两个因子 5。

因此，我们需要计算每个数字中的因子 55。我们可以使用一个循环而不是 if 语句，我们若有因子 55 将数字除以 55。如果还有剩余的因子 55，则将重复步骤。

我们可以这样做：


twos = 0
for i from 1 to n inclusive:
    remaining_i = i
    while remaining_i is divisible by 2:
        twos += 1
        remaining_i = remaining_i / 2

fives = 0
for i from 1 to n inclusive:
    remaining_i = i
    while remaining_i is divisible by 5:
        fives += 1
        remaining_i = remaining_i / 5

tens = min(twos, fives)
这样我们就得到了正确答案，但是我们仍然可以做一些改进。

首先，我们可以注意到因子 22 数总是比因子 55 大。为什么？因为每四个数字算作额外的因子 22，但是只有每 2525 个数字算作额外的因子 55。下图我们可以清晰的看见：

在这里插入图片描述

因此我们可以删除计算因子 22 的过程，留下：


fives = 0
for i from 1 to n inclusive:
    remaining_i = i
    while remaining_i is divisible by 5:
        fives += 1
        remaining_i = remaining_i / 5

tens = fives
我们可以做最后一个优化。在上面的算法中，我们分析了从 11 到 nn 的每个数字。但是只有 5, 10, 15, 20, 25, 30, ... 等等5,10,15,20,25,30,...等等 至少有一个因子 55。所以，ww偶们不必一步一步的往上迭代，可以五步的往上迭代：因此可以修改为：


fives = 0
for i from 5 to n inclusive in steps of 5:
    remaining_i = i
    while remaining_i is divisible by 5:
        fives += 1
        remaining_i = remaining_i / 5

tens = fives
算法：

这是我们设计的算法


public int trailingZeroes(int n) {
        
    int zeroCount = 0;
    for (int i = 5; i <= n; i += 5) {
        int currentFactor = i;
        while (currentFactor % 5 == 0) {
            zeroCount++;
            currentFactor /= 5;
        }
    }
    return zeroCount;
}
或者，我们可以检查 55 的幂，而不是每次除以 55 来计算因子数。这是通过检查 i 是否可以被 55，2525，125125 等整除来实现的。


public int trailingZeroes(int n) {
    
    int zeroCount = 0;
    for (int i = 5; i <= n; i += 5) {
        int powerOf5 = 5;
        while (i % powerOf5 == 0) {
            zeroCount += 1;
            powerOf5 *= 5;
        }
    }
    return zeroCount;
}
复杂度分析

时间复杂度：O(n)O(n)。
在方法一中，我们不能将出发看作 O(1)O(1)，不过在此方法中，我们保持在它的范围内，因此可以将除法和乘法看作 O(1)O(1)。

为了计算零计数，我们循环从 55 到 nn 的每五个数字处理一次，这里是 O(n)O(n) 步（将 \frac{1}{5} 
5
1
​	
  作为常量处理）。

在每个步骤中，虽然看起来像是执行 O(\log n)O(logn) 操作来计算 5 的因子数，但实际上它只消耗 O(1)O(1)，因为绝大部分的数字只包含一个因子 55。可以证明，因子 55 的总数小于 \frac{2 \cdot n}{5} 
5
2⋅n
​	
 。

所以我们得到 O(n) \cdot O(1) = O(n)O(n)⋅O(1)=O(n)。

空间复杂度：O(1)O(1)，只是用了一个整数变量。
方法三：高效的计算因子 5
在方法 2 中，我们找到了一种计算阶乘后的零的个数的方法，而不需要实际计算阶乘。这是通过在 55 的每个倍数上循环，从 55 到 nn，并计算 55 的每个倍数中有多少个因子。我们把所有这些数字加起来得到最终结果。

然而，无论是实际上还是对问题的要求来说，方法 2 仍然太慢。为了得出一个足够快的算法，我们需要做进一步改进，这个改进能使我们在对数时间内计算出我们的答案。

思考我们之前简化的算法（但不正确），它不正确是因为对于有多个因子 55 时会计算出错，例如 2525。


fives = 0
for i from 1 to n inclusive:
    if i is divisible by 5:
        fives += 1
你会发现这是执行 \frac{n}{5} 
5
n
​	
  的低效方法。我们只对 55 的倍数感兴趣，不是 55 的倍数可以忽略,因此可以简化成:


fives = n / 5
tens = fives
那么，如何解决有多重因子的数字呢？所有包含两个及以上的因子 55 的数字都是 2525 的倍数。所以我们可以简单的除以 2525 来计算 2525 的倍数是多少。另外，我们在 \frac{n}{5} 
5
n
​	
  已经计算了 2525 一次，所以我们只需要再计算额外因子一次 \frac{n}{25} 
25
n
​	
  （而不是 2\cdot\frac{n}{25}2⋅ 
25
n
​	
 ）

所以结合起来我们得到：


fives = n / 5 + n / 25
tens = fives
但是如果有三个因子 55 呢！为了得到最终的结果，我们需要将所有的 \dfrac{n}{5} 
5
n
​	
 、\dfrac{n}{25} 
25
n
​	
 、\dfrac{n}{125} 
125
n
​	
 、\dfrac{n}{625} 
625
n
​	
  等相加。得到：

fives=\dfrac{n}{5}+\dfrac{n}{25}+\dfrac{n}{125}+\dfrac{n}{625}+\dfrac{n}{3125}+\cdotsfives= 
5
n
​	
 + 
25
n
​	
 + 
125
n
​	
 + 
625
n
​	
 + 
3125
n
​	
 +⋯

这样看起来会一直计算下去，但是并非如此！我们使用整数除法，最终，分母将大于 nn，因此当项等于 0 时，就可以停止我们的计算。

例如，如果 n=12345n=12345，我们得到：

fives=\dfrac{12345}{5}+\dfrac{12345}{25}+\dfrac{12345}{125}+\dfrac{12345}{625}+\dfrac{12345}{3125}+\dfrac{12345}{16075}+\dfrac{12345}{80375}+\cdotsfives= 
5
12345
​	
 + 
25
12345
​	
 + 
125
12345
​	
 + 
625
12345
​	
 + 
3125
12345
​	
 + 
16075
12345
​	
 + 
80375
12345
​	
 +⋯

等于：

fives = 2469 + 493 + 98 + 19 + 3 + 0 + 0 + \cdots = 3082fives=2469+493+98+19+3+0+0+⋯=3082

在代码中，我们可以通过循环 55 的幂来计算。


fives = 0
power_of_5 = 5
while n >= power_of_5:
    fives += n / power_of_5
    power_of_5 *= 5

tens = fives
算法：


public int trailingZeroes(int n) {
    int zeroCount = 0;
    // We need to use long because currentMultiple can potentially become
    // larger than an int.
    long currentMultiple = 5;
    while (n >= currentMultiple) {
        zeroCount += (n / currentMultiple);
        currentMultiple *= 5;
    }
    return zeroCount;
}
编写此算法的另一种方法是，我们不必每次尝试 55 的幂，而是每次将 nn 本身除以 55。这也是一样的，因为我们最终得到的序列是：

fives=\dfrac{n}{5}+\dfrac{（\dfrac{n}{5}）}{5}+\dfrac{（\dfrac{（\frac{n}{5}）}{5}）}{5}+\cdotsfives= 
5
n
​	
 + 
5
（ 
5
n
​	
 ）
​	
 + 
5
（ 
5
（ 
5
n
​	
 ）
​	
 ）
​	
 +⋯

注意，在第二步中，我们有 \dfrac{（\frac{n}{5}）}{5} 
5
（ 
5
n
​	
 ）
​	
 。这是因为前一步将 nn 本身除以 55。等等。

如果你熟悉分数规则，你会发现 \dfrac{（\frac{n}{5}）}{5} 
5
（ 
5
n
​	
 ）
​	
 和 \dfrac{n}{5\cdot 5}=\frac{n}{25} 
5⋅5
n
​	
 = 
25
n
​	
  是一样的。这意味着序列与：

\dfrac{n}{5}+\dfrac{n}{25}+\dfrac{n}{125}+\cdots 
5
n
​	
 + 
25
n
​	
 + 
125
n
​	
 +⋯

因此，这种编写算法的替代方法是等价的。


public int trailingZeroes(int n) {
    int zeroCount = 0;
    long currentMultiple = 5;
    while (n > 0) {
        n /= 5;
        zeroCount += n;
    }
    return zeroCount;
}
复杂度分析

时间复杂度：O(\log n)O(logn)。在这种方法中，我们将 nn 除以 55 的每个幂。根据定义，55 的 \log_5nlog 
5
​	
 n 幂小于或等于 nn。由于乘法和除法在 32 位整数范围内，我们将这些计算视为 O(1)O(1)。因此，我们正在执行 \log_5 n\cdot O（1）=\log nlog 
5
​	
 n⋅O（1）=logn 操作
空间复杂度：O(1)O(1)，只是用了常数空间。
下一篇：详细通俗的思路分析


public class Solution {
    public int TrailingZeroes(int n)
    {
        int res = 0;
        while (n>=5)
        {
            res += n / 5;
            n /= 5;
        }
        return res;
    }
}

public class Solution {
    public int TrailingZeroes(int n) {
        n=n/5;
        if(n<5)
        {
            return n;
        }
        return n+TrailingZeroes(n);     

    }
}

public class Solution {
    public int TrailingZeroes(int n) {
      int count=0;
      long mod=5;
      while(n>=mod)
      {
          count+=(int) (n/mod);
          mod=mod*5;
      }
      return count;
    }
}
*/