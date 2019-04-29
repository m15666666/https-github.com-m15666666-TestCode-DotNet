using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
给定一个长度为 n 的整数数组 A 。

假设 Bk 是数组 A 顺时针旋转 k 个位置后的数组，我们定义 A 的“旋转函数” F 为：

F(k) = 0 * Bk[0] + 1 * Bk[1] + ... + (n-1) * Bk[n-1]。

计算F(0), F(1), ..., F(n-1)中的最大值。

注意:
可以认为 n 的值小于 105。

示例:

A = [4, 3, 2, 6]

F(0) = (0 * 4) + (1 * 3) + (2 * 2) + (3 * 6) = 0 + 3 + 4 + 18 = 25
F(1) = (0 * 6) + (1 * 4) + (2 * 3) + (3 * 2) = 0 + 4 + 6 + 6 = 16
F(2) = (0 * 2) + (1 * 6) + (2 * 4) + (3 * 3) = 0 + 6 + 8 + 9 = 23
F(3) = (0 * 3) + (1 * 2) + (2 * 6) + (3 * 4) = 0 + 2 + 12 + 12 = 26

所以 F(0), F(1), F(2), F(3) 中的最大值是 F(3) = 26 。 
*/
/// <summary>
/// https://leetcode-cn.com/problems/rotate-function/
/// 396. 旋转函数
/// </summary>
class RotateFunctionSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    /*
     *  通过观察，可以得出下面的规律：
        F(1) = F(0) + sum - 4D
        F(2) = F(1) + sum - 4C
        F(3) = F(2) + sum - 4B
        找到规律, F(i) = F(i-1) + sum - n*A[n-i]。
     */
    public int MaxRotateFunction(int[] A)
    {
        // F0...Fn-1
        int functionValue = 0;

        // 数组和
        int sum = 0;

        int n = A.Length;

        // 计算F0
        for (int i = 0; i < n; ++i)
        {
            sum += A[i];
            functionValue += i * A[i];
        }

        int ret = functionValue;

        // 计算F1...Fn-1
        for (int i = 1; i < n; ++i)
        {
            functionValue = functionValue + sum - n * A[n - i];
            if (ret < functionValue) ret = functionValue;
        }

        return ret;
    }
}
/*
public class Solution
{
    public int MaxRotateFunction(int[] A)
    {
        int sum = 0, f0 = 0;
        for (int i = 0; i < A.Length; i++)
        {
            sum += A[i];
            f0 += i * A[i];
        }
        int max = f0;
        for (int i = 1; i < A.Length; i++)
        {
            f0 = f0 + sum - A.Length * A[A.Length - i];
            max = max > f0 ? max : f0;
        }
        return max;
    }
}
*/
