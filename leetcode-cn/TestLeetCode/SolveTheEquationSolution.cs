using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
求解一个给定的方程，将x以字符串"x=#value"的形式返回。该方程仅包含'+'，' - '操作，变量 x 和其对应系数。

如果方程没有解，请返回“No solution”。

如果方程有无限解，则返回“Infinite solutions”。

如果方程中只有一个解，要保证返回值 x 是一个整数。

示例 1：

输入: "x+5-3+x=6+x-2"
输出: "x=2"
示例 2:

输入: "x=x"
输出: "Infinite solutions"
示例 3:

输入: "2x=x"
输出: "x=0"
示例 4:

输入: "2x+3x-6x=x+2"
输出: "x=-1"
示例 5:

输入: "x=x+2"
输出: "No solution" 
*/
/// <summary>
/// https://leetcode-cn.com/problems/solve-the-equation/
/// 640. 求解方程
/// https://blog.csdn.net/zrh_CSDN/article/details/88022469
/// </summary>
class SolveTheEquationSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public string SolveEquation(string equation)
    {
        int a = 0, b = 0;
        int found = equation.IndexOf('=');
        Helper(equation.Substring(0, found), true, ref a, ref b);
        Helper(equation.Substring(found + 1), false, ref a, ref b );
        if (a == 0 && a == b) return "Infinite solutions";
        if (a == 0 && a != b) return "No solution";
        return $"x={b / a}";
    }
    private static void Helper(string e, bool isLeft, ref int a, ref int b)
    {
        int sign = 1, num = -1;
        e += "+";
        for (int i = 0; i < e.Length; ++i)
        {
            if (e[i] == '-' || e[i] == '+')
            {
                num = (num == -1) ? 0 : (num * sign);
                b += isLeft ? -num : num;
                num = -1;
                sign = (e[i] == '+') ? 1 : -1;
            }
            else if (e[i] >= '0' && e[i] <= '9')
            {
                if (num == -1) num = 0;
                num = num * 10 + e[i] - '0';
            }
            else if (e[i] == 'x')
            {
                num = (num == -1) ? sign : (num * sign);
                a += isLeft ? num : -num;
                num = -1;
            }
        }
    }
}
/*
public class Solution {
    public string SolveEquation(string equation) {
        string[] arr = equation.Split('=');
        string left = arr[0];
        string right = arr[1];
        
        Console.WriteLine(left);
        Console.WriteLine(right);
        
        int leftVal = GetVal(left);
        int leftXVal = GetXVal(left);
        
        int rightVal = GetVal(right);
        int rightXVal = GetXVal(right);
        
        Console.WriteLine("leftVal " + leftVal);
        Console.WriteLine("leftXVal " + leftXVal);
        Console.WriteLine("rightVal " + rightVal);
        Console.WriteLine("rightXVal " + rightXVal);
        
        if (leftXVal == rightXVal) {
            if (leftVal == rightVal) {
                return "Infinite solutions";
            } else {
                return "No solution";
            }
        } else {
            int val = (rightVal - leftVal)/(leftXVal - rightXVal);
            return "x=" + val;
        }
    }
    
    int GetVal(string str) {
        int n = str.Length;
        bool neg = false;
        int res = 0;
        int num = -1;
        for (int i = 0; i < n; i ++) {
            if (str[i] == '+' || str[i] == '-') {
                if (num == -1) {
                    num = 0;
                }
                
                if (neg) {
                    res -= num;
                } else {
                    res += num;
                }
                
                if (str[i] == '+') {
                    neg = false;
                } else {
                    neg = true;
                }
                num = -1;
            } else if (str[i] == 'x') {
                num = -1;
                neg = false;
            } else {
                if (num == -1) {
                    num = 0;
                }
                num = num * 10 + str[i] - '0';
            }
        }
        
        if (num != -1) {
            if (neg) {
                res -= num;
            } else {
                res += num;
            }
        }
        
        return res;
    }
    
    int GetXVal(string str) {
        int n = str.Length;
        bool neg = false;
        int res = 0;
        int num = -1;
        for (int i = 0; i < n; i ++) {
            if (str[i] == '+' || str[i] == '-') {
                if (str[i] == '+') {
                    neg = false;
                } else {
                    neg = true;
                }
                num = -1;
            } else if (str[i] == 'x') {
                if (num == -1) {
                    num = 1;
                }
                if (neg) {
                    res -= num;
                } else {
                    res += num;
                }
                Console.WriteLine("x res = " + res);
                num = -1;
            } else {
                if (num == -1) {
                    num = 0;
                }
                num = num * 10 + str[i] - '0';
            }
        }
        
        return res;
    }
} 
*/
