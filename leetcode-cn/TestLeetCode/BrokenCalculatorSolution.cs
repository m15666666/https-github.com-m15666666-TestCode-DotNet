using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
在显示着数字的坏计算器上，我们可以执行以下两种操作：

双倍（Double）：将显示屏上的数字乘 2；
递减（Decrement）：将显示屏上的数字减 1 。
最初，计算器显示数字 X。

返回显示数字 Y 所需的最小操作数。

 

示例 1：

输入：X = 2, Y = 3
输出：2
解释：先进行双倍运算，然后再进行递减运算 {2 -> 4 -> 3}.
示例 2：

输入：X = 5, Y = 8
输出：2
解释：先递减，再双倍 {5 -> 4 -> 8}.
示例 3：

输入：X = 3, Y = 10
输出：3
解释：先双倍，然后递减，再双倍 {3 -> 6 -> 5 -> 10}.
示例 4：

输入：X = 1024, Y = 1
输出：1023
解释：执行递减运算 1023 次
 

提示：

1 <= X <= 10^9
1 <= Y <= 10^9
*/
/// <summary>
/// https://leetcode-cn.com/problems/broken-calculator/
/// 991. 坏了的计算器
/// 
/// </summary>
class BrokenCalculatorSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int BrokenCalc(int X, int Y)
    {
        int ret = 0;
        while (X < Y)
        {
            ret++;
            if (Y % 2 == 1) Y++;
            else Y /= 2;
        }

        return ret + X - Y;
    }
}
/*
坏了的计算器
力扣 (LeetCode)
发布于 1 年前
3.3k 阅读
方法：逆向思维
思路

除了对 X 执行乘 2 或 减 1 操作之外，我们也可以对 Y 执行除 2（当 Y 是偶数时）或者加 1 操作。

这样做的动机是我们可以总是贪心地执行除 2 操作：

当 Y 是偶数，如果先执行 2 次加法操作，再执行 1 次除法操作，我们可以通过先执行 1 次除法操作，再执行 1 次加法操作以使用更少的操作次数得到相同的结果 [(Y+2) / 2 vs Y/2 + 1]。

当 Y 是奇数，如果先执行 3 次加法操作，再执行 1 次除法操作，我们可以将其替代为顺次执行加法、除法、加法操作以使用更少的操作次数得到相同的结果 [(Y+3) / 2 vs (Y+1) / 2 + 1]。

算法

当 Y 大于 X 时，如果它是奇数，我们执行加法操作，否则执行除法操作。之后，我们需要执行 X - Y 次加法操作以得到 X。

class Solution {
    public int brokenCalc(int X, int Y) {
        int ans = 0;
        while (Y > X) {
            ans++;
            if (Y % 2 == 1)
                Y++;
            else
                Y /= 2;
        }

        return ans + X - Y;
    }
}
复杂度分析

时间复杂度： O(\log Y)O(logY)。

空间复杂度： O(1)O(1)。

执行用时 : 0 ms , 在所有 java 提交中击败了 100.00% 的用户 内存消耗 : 33.2 MB , 在所有 java 提交中击败了 77.42% 的用户；首先考虑两种情况，X>Y的时候，肯定不能使用乘法，只能使用减法，操作数就是X-Y；若X<Y时，可以考虑逆推，最后一步要么是*2，要么是-1，若是偶数，最后一步肯定是*2的步数最小，可以自己验证下，这样的话就可以使用递归处理；
老菜鸟
发布于 20 天前
43 阅读
解题思路
此处撰写解题思路

代码
class Solution {
    public int brokenCalc(int X, int Y) {
        if (X >= Y) {
            return X - Y;
        }

        if (Y % 2 == 1) {
            return 2 + brokenCalc(X, (Y + 1) / 2);
        } else {
            return 1 + brokenCalc(X, Y / 2);
        }
    }
}
*/
