using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个由 '(' 和 ')' 括号组成的字符串 S，我们需要添加最少的括号（ '(' 或是 ')'，可以在任何位置），以使得到的括号字符串有效。

从形式上讲，只有满足下面几点之一，括号字符串才是有效的：

它是一个空字符串，或者
它可以被写成 AB （A 与 B 连接）, 其中 A 和 B 都是有效字符串，或者
它可以被写作 (A)，其中 A 是有效字符串。
给定一个括号字符串，返回为使结果字符串有效而必须添加的最少括号数。

 

示例 1：

输入："())"
输出：1
示例 2：

输入："((("
输出：3
示例 3：

输入："()"
输出：0
示例 4：

输入："()))(("
输出：4
 

提示：

S.length <= 1000
S 只包含 '(' 和 ')' 字符。
*/
/// <summary>
/// https://leetcode-cn.com/problems/minimum-add-to-make-parentheses-valid/
/// 921. 使括号有效的最少添加
/// 
/// </summary>
class MinimumAddToMakeParenthesesValidSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int MinAddToMakeValid(string S)
    {
        int ret = 0;
        int balance = 0;
        foreach (var c in S)
        {
            balance += c == '(' ? 1 : -1;
            if (balance == -1)
            {
                ret++;
                balance++;
            }
        }

        return ret + balance;
    }
}
/*
方法一： 平衡法
思路和算法

保证左右括号数量的 平衡： 计算 '(' 出现的次数减去 ')' 出现的次数。如果值为 0，那就是平衡的，如果小于 0，就要在前面补上缺少的 '('。

计算 S 每个前缀子数组的 平衡度。如果值是负数（比如说，-1），那就得在前面加上一个 '('。同样的，如果值是正数（比如说，+B），那就得在末尾处加上 B 个 ')' 。

JavaPython
class Solution {
    public int minAddToMakeValid(String S) {
        int ans = 0, bal = 0;
        for (int i = 0; i < S.length(); ++i) {
            bal += S.charAt(i) == '(' ? 1 : -1;
            // It is guaranteed bal >= -1
            if (bal == -1) {
                ans++;
                bal++;
            }
        }

        return ans + bal;
    }
}
复杂度分析

时间复杂度： O(N)O(N)，其中 NN 是 S 的长度。

空间复杂度： O(1)O(1)。


public class Solution {
    public int MinAddToMakeValid(string S) {
        var stack = new Stack<char>();
        int right = 0;
        foreach(char item in S){
            if(item == '(')
                stack.Push(item);
            else {
                if(stack.Count != 0)
                    stack.Pop();
                else
                    right++;
            }
        }        
            
        return right + stack.Count;
    }
} 
*/
