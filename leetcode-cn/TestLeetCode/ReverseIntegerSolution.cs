using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给出一个 32 位的有符号整数，你需要将这个整数中每位上的数字进行反转。

示例 1:

输入: 123
输出: 321
 示例 2:

输入: -123
输出: -321
示例 3:

输入: 120
输出: 21
注意:

假设我们的环境只能存储得下 32 位的有符号整数，则其数值范围为 [−231,  231 − 1]。
请根据这个假设，如果反转后整数溢出那么就返回 0。
     
*/
/// <summary>
/// https://leetcode-cn.com/problems/reverse-integer/
/// 7. 整数反转
/// 
/// 
/// https://blog.csdn.net/hy971216/article/details/80524704
/// </summary>
class ReverseIntegerSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int Reverse(int x)
    {
        long ret = 0;
        while (x != 0)
        {
            int remain = x % 10;
            x /= 10;
            ret = ret * 10 + remain;
        }

        if (int.MaxValue < ret || ret < int.MinValue ) return 0;
        return (int)ret;
    }
}
/*
整数反转
力扣 (LeetCode)
发布于 2 年前
210.8k
方法：弹出和推入数字 & 溢出前进行检查
思路

我们可以一次构建反转整数的一位数字。在这样做的时候，我们可以预先检查向原整数附加另一位数字是否会导致溢出。

算法

反转整数的方法可以与反转字符串进行类比。

我们想重复“弹出” xx 的最后一位数字，并将它“推入”到 \text{rev}rev 的后面。最后，\text{rev}rev 将与 xx 相反。

要在没有辅助堆栈 / 数组的帮助下 “弹出” 和 “推入” 数字，我们可以使用数学方法。

//pop operation:
pop = x % 10;
x /= 10;

//push operation:
temp = rev * 10 + pop;
rev = temp;
但是，这种方法很危险，因为当 \text{temp} = \text{rev} \cdot 10 + \text{pop}temp=rev⋅10+pop 时会导致溢出。

幸运的是，事先检查这个语句是否会导致溢出很容易。

为了便于解释，我们假设 \text{rev}rev 是正数。

如果 temp = \text{rev} \cdot 10 + \text{pop}temp=rev⋅10+pop 导致溢出，那么一定有 \text{rev} \geq \frac{INTMAX}{10}rev≥ 
10
INTMAX
​	
 。
如果 \text{rev} > \frac{INTMAX}{10}rev> 
10
INTMAX
​	
 ，那么 temp = \text{rev} \cdot 10 + \text{pop}temp=rev⋅10+pop 一定会溢出。
如果 \text{rev} == \frac{INTMAX}{10}rev== 
10
INTMAX
​	
 ，那么只要 \text{pop} > 7pop>7，temp = \text{rev} \cdot 10 + \text{pop}temp=rev⋅10+pop 就会溢出。
当 \text{rev}rev 为负时可以应用类似的逻辑。

class Solution {
    public int reverse(int x) {
        int rev = 0;
        while (x != 0) {
            int pop = x % 10;
            x /= 10;
            if (rev > int.MaxValue/10 || (rev == int.MaxValue / 10 && pop > 7)) return 0;
            if (rev < int.MinValue/10 || (rev == int.MinValue / 10 && pop < -8)) return 0;
            rev = rev * 10 + pop;
        }
        return rev;
    }
}
复杂度分析

时间复杂度：O(\log(x))O(log(x))，xx 中大约有 \log_{10}(x)log 
10
​	
 (x) 位数字。
空间复杂度：O(1)O(1)。 

public class Solution {
    public int Reverse(int x) {
            int res = 0;

            while (x != 0)
            {
                if (res * 10 / 10 != res)
                {
                    res = 0;
                    break;
                }

                res = res * 10 + x % 10;
                x = x / 10;
            }

            return res;
    }
}

public class Solution {
    public int Reverse(int x) {
        int y;
        int re = 0;
        int flag;
        if (x > 0)
        {
            flag = 1;
            y = x;
        }
        else
        {
            flag = -1;
            y = -x;
        }

        do
        {
            int mod = y % 10;
            y = y / 10;

            checked
            {
                try
                {
                    re = re * 10 + mod;
                }
                catch (Exception)
                {
                    return 0;
                }
            }
            
            if (y == 0)
            {
                return re * flag;
            }
        } while (true);
    }
}

public class Solution {
    public int Reverse(int x) {
         int rst = 0;
            int pop = 0;
            while (x >= 10 ||  x <= -10)
            {
                pop = x % 10;
                rst = rst * 10 + pop;
                x /= 10;
            }
            if (rst > int.MaxValue / 10 || (rst == int.MaxValue / 10 && pop > 7))
                return 0;
            if (rst < int.MinValue / 10 || (rst == int.MinValue && pop < -8))
                return 0;
            return rst * 10 + x;
    }
}

public class Solution {
    public int Reverse(int x) {
            int result = 0;
            int maxValue = int.MaxValue / 10;

            while (x != 0)
            {
                if (result > maxValue || result < -maxValue)
                {
                    return 0;
                }
                result = result * 10 + x % 10;
                x = x / 10;
            }
            return result;
    }
}

public class Solution {
    public int Reverse(int x) {
        bool isNegaTive = x < 0 ? true : false;
        if (isNegaTive) x = -x;
        int val = 0;
        while (x > 0) {
            try {
                val = checked(val * 10 + x % 10);
            }
            catch (System.OverflowException)
            {
                return 0;
            }
            x /= 10;
        }
        return isNegaTive ? (int)-val : (int)val;
    }
}

*/
