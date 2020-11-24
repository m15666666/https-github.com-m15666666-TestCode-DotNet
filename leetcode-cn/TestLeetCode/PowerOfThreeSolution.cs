using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个整数，写一个函数来判断它是否是 3 的幂次方。如果是，返回 true ；否则，返回 false 。

整数 n 是 3 的幂次方需满足：存在整数 x 使得 n == 3x

示例 1：

输入：n = 27
输出：true
示例 2：

输入：n = 0
输出：false
示例 3：

输入：n = 9
输出：true
示例 4：

输入：n = 45
输出：false
 

提示：

-231 <= n <= 231 - 1
 

进阶：

你能不使用循环或者递归来完成本题吗？

*/
/// <summary>
/// https://leetcode-cn.com/problems/power-of-three/
/// 326. 3的幂
/// 
/// </summary>
class PowerOfThreeSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public bool IsPowerOfThree(int n) {
        // 1162261467 = Math.Pow(3, 19)
        return 0 < n && 1162261467 % n == 0;
    }

}
/*
3 的幂
力扣 (LeetCode)
发布于 2019-06-20
26.1k
解决方法：
在本文中，我们将探讨加速简单计算的方法，以及为什么在实践中有用。

方法一：循环迭代
找出数字 n 是否是数字 b 的幂的一个简单方法是，n%3 只要余数为 0，就一直将 n 除以 b。

\begin{aligned} n &= b^x \ n &= b \times b \times \ldots \times b \end{aligned}
n
​	
  
=b 
x
  n
​	
  
=b×b×…×b
​	
 

因此，应该可以将 n 除以 b x 次，每次都有 0 的余数，最终结果是 1。


public class Solution {
    public boolean isPowerOfThree(int n) {
        if (n < 1) {
            return false;
        }

        while (n % 3 == 0) {
            n /= 3;
        }

        return n == 1;
    }
}

注意我们需要一个警卫来检查那个 n！=0，否则 while 循环将永远不会结束。对于负数，该算法没有意义，因此我们也将包括该保护。

复杂度分析

时间复杂度：O(\log_b(n))O(log 
b
​	
 (n))，在我们的例子中是 O(\log n)O(logn)。除数是用对数表示的。
空间复杂度：O(1)O(1)，没有使用额外的空间。
方法二：基准转换
在基数 10 中，10 的所有幂都从数字 1 开始，然后只跟 0（例如10、100、1000）。其他基地及其各自的权力也是如此。例如，在基数 2 中，10 _210 
2
​	
 、100 _2100 
2
​	
  和 1000 _21000 
2
​	
  分别表示为 2_{10}2 
10
​	
 , 4_{10}4 
10
​	
  和 8_{10}8 
10
​	
 。因此，如果我们把我们的数转换成基3，并且表示形式是 100…0，那么这个数就是3的幂。

证明 ：
给定以 3 为底的数字表示为数组 s，第 0 位开始为有效数字，从 3 为底转换为 10 为底的公式为：

\sum_{i=0}^{len(s) - 1} s[i] * 3^{i}
i=0
∑
len(s)−1
​	
 s[i]∗3 
i
 

因此，只有一个数字 1，其余的都是 0，这意味着这个数字是 3 的幂。

实现：

我们所要做的就是将数字转换为以3为底的基数 ，并检查它是否为前导1，后跟所有 0。
两个内置的Java函数将帮助我们前进。

String baseChange = Integer.toString(number, base);
上面的代码将 number 转换以 base 为底的基数，并以字符串形式返回结果。例如，integer.toString（5，2）=“101” 和 integer.toString（5，3）=“12”。

boolean matches = myString.matches("123");
上面的代码检查字符串中是否存在特定的正则表达式。例如，如果字符串 mystring 中存在子字符串 “123”，上面的内容将返回 true。


boolean powerOfThree = baseChange.matches("^10*$")
我们将使用上面的正则表达式来检查字符串是否以1 ^1 开头，后跟 0 或 多个 0 0* 并且不包含任何其他值 $。


public class Solution {
    public boolean isPowerOfThree(int n) {
        return Integer.toString(n, 3).matches("^10*$");
    }
}
复杂度分析

时间复杂度：O(\log_3n)O(log 
3
​	
 n)。
假设：
Integer.toString() - 基转换通常是作为一个重复的除法来实现的。复杂性应该类似于我们的方法 1:O（\ log_3n）O（ log 
3
​	
 n）的复杂性。
String.matches() - 方法迭代整个字符串。n 以 3 为基数表示的位数是O（\log_3n）O（log 
3
​	
 n）。
空间复杂度：O(\log_3n)O(log 
3
​	
 n)。我们使用两个附加变量。
以 3 为基数表示数字的字符串（大小为 \log_3nlog 
3
​	
 n）
正则表达式的字符串（常量大小）
方法三：运算法
我们可以用下面的数学公式

n = 3^i \ i = \ log_3(n) \ i = \frac{\ log_b(n)}{\ log_b(3)}
n=3 
i
  i= log 
3
​	
 (n) i= 
 log 
b
​	
 (3)
 log 
b
​	
 (n)
​	
 

若 n 是 3 的幂则 i 是整数。在 Java 中，我们通过取小数部分（利用 % 1）来检查数字是否是整数，并检查它是否是 0。


public class Solution {
    public boolean isPowerOfThree(int n) {
        return (Math.log10(n) / Math.log10(3)) % 1 == 0;
    }
}
常见的陷阱 :
这个解决方案是有问题的，因为我们开始使用 double s，这意味着我们会遇到精度错误。说明在比较双精度数时不应使用 ==。这是因为 Math.log10(n)/Math.log10(3) 的结果可能是 5.0000001 或 4.9999999。使用 Math.log() 函数而不是Math.log10() 可以观察到这种效果。

为了解决这个问题，我们需要将结果与 epsilon 进行比较。


return (Math.log(n) / Math.log(3) + epsilon) % 1 <= 2 * epsilon;
复杂度分析

时间复杂度：UnknownUnknown。这里主要消耗时间的运算是 Math.log，它限制了我们算法的时间复杂性。实现依赖于我们使用的语言和编译器 。
空间复杂度： O(1)O(1)，我们没有使用任何额外的内存。epsilon 变量可以是内联的。
方法四：整数限制
一个重要的信息可以从函数名中推导出来。


public boolean isPowerOfThree(int n)
我们可以看出 n 的类型是 int。在 Java 中说明了该变量是四个字节，他的最大值为 2147483647。有三种方法可以计算出该最大值。

Google
System.out.println(Integer.MAX_VALUE);
MaxInt = \frac{ 2^{32} }{2} - 1 
2
2 
32
 
​	
 −1 ,因为我们使用 32 位来表示数字，所以范围的一半用于负数，0 是正数的一部分。
知道了 n 的限制，我们现在可以推断出 n 的最大值，也就是 3 的幂，是 1162261467。我们计算如下：

3^{\lfloor{}\log_3{MaxInt}\rfloor{}} = 3^{\lfloor{}19.56\rfloor{}} = 3^{19} = 1162261467
3 
⌊log 
3
​	
 MaxInt⌋
 =3 
⌊19.56⌋
 =3 
19
 =1162261467

因此，我们应该返回 true 的 n 的可能值是 3^03 
0
 ，3^13 
1
 …3 ^ {19}3 
19
 。因为 3 是质数，所以 3^{19}3 
19
  的除数只有 3^03 
0
 ，3^13 
1
 …3 ^{19}3 
19
 ，因此我们只需要将 3^{19}3 
19
  除以 n。若余数为 0 意味着 n 是 3^{19}3 
19
  的除数，因此是 3 的幂。


public class Solution {
    public boolean isPowerOfThree(int n) {
        return n > 0 && 1162261467 % n == 0;
    }
}
复杂度分析

时间复杂度：O(1)O(1)。我们只做了一次操作。
空间复杂度： O(1)O(1)，没有使用额外空间。
性能测量 ：
函数的单次运行使得难以测量两种解的差别。在 leetcode 上的 AcceptedAccepted SolutionsSolutions RuntimeRuntime DistributionDistribution 页面上，所有解决方案都在 15 ms 到 20 ms 之间。为了完整性，我们提出了以下基准，以了解这两个解决方案的区别。

Java基准码 :


public static void main(String[] args) {
    Solution sol = new Solution();
    int iterations = 1; // See table header for this value
    for (int i = 0; i < iterations; i++) {
        sol.isPowerOfThree(i);
    }
}
在下表中，以秒为单位。

Iterations	10^610 
6
 	10^710 
7
 	10^810 
8
 	10^910 
9
 	MaxintMaxint
Java Approach 1: (Naive)	0.04	0.07	0.30	2.47	5.26
Java Approach 2: (Strings)	0.68	4.02	38.90	409.16	893.89
Java Approach 3: (Logarithms)	0.09	0.50	4.59	45.53	97.50
Java Approach 4: (Fast)	0.04	0.06	0.08	0.41	0.78
正如我们所看到的，对于小的 N 值，差异并不明显，但是随着我们进行更多的迭代，并且传递给 isPowerOfThree() 的 n 的值增长，我们看到方法 4 的性能显著提高。

结论 :
像这样的简单优化似乎可以忽略不计，但历史上，当计算能力成为一个问题时，它允许某些计算机程序（如Quake 3）成为可能。

public class Solution {
    public bool IsPowerOfThree(int n) {
        if(n==0)return false;
        if(n==1)return true;
        while(n%3==0){
            n/=3;
        }
        return n==1;
    }
}

public class Solution {
    public bool IsPowerOfThree(int n) {
        int k = 1;
        int n1 = n;
        while(n > 2)
        {
            k *= 3;
            n /= 3;
        }
        return k == n1;
    }
}

public class Solution {
    public bool IsPowerOfThree(int n) {
        long i = 1;
        while (i <= n) {
            if(i == n) return true;
            i *= 3;
        }
        return false;
    }
}

public class Solution {
   public bool IsPowerOfThree(int n)
	{
		int[] nums = new int[] { 1, 3, 9, 27, 81, 243, 729, 2187, 6561, 19683, 59049, 177147, 531441, 1594323, 4782969, 14348907, 43046721, 129140163, 387420489, 1162261467 };
		return Array.IndexOf(nums, n)>=0;
	}        
}

public class Solution {
    public bool IsPowerOfThree(int n) {
		switch (n) {
		case 1:return true;
		case 3:return true;
		case 9:return true;
		case 27:return true;
		case 81:return true;
		case 243:return true;
		case 729:return true;
		case 2187:return true;
		case 6561:return true;
		case 19683:return true;
		case 59049:return true;
		case 177147:return true;
		case 531441:return true;
		case 1594323:return true;
		case 4782969:return true;
		case 14348907:return true;
		case 43046721:return true;
		case 129140163:return true;
		case 387420489:return true;
		case 1162261467:return true;
		default: return false;}
    }
}
 
*/