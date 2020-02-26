using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
有效括号字符串 仅由 "(" 和 ")" 构成，并符合下述几个条件之一：

空字符串
连接，可以记作 AB（A 与 B 连接），其中 A 和 B 都是有效括号字符串
嵌套，可以记作 (A)，其中 A 是有效括号字符串
类似地，我们可以定义任意有效括号字符串 s 的 嵌套深度 depth(S)：

s 为空时，depth("") = 0
s 为 A 与 B 连接时，depth(A + B) = max(depth(A), depth(B))，其中 A 和 B 都是有效括号字符串
s 为嵌套情况，depth("(" + A + ")") = 1 + depth(A)，其中 A 是有效括号字符串
例如：""，"()()"，和 "()(()())" 都是有效括号字符串，嵌套深度分别为 0，1，2，而 ")(" 和 "(()" 都不是有效括号字符串。

 

给你一个有效括号字符串 seq，将其分成两个不相交的子序列 A 和 B，且 A 和 B 满足有效括号字符串的定义
（注意：A.length + B.length = seq.length）。

现在，你需要从中选出 任意 一组有效括号字符串 A 和 B，使 max(depth(A), depth(B)) 的可能取值最小。

返回长度为 seq.length 答案数组 answer ，选择 A 还是 B 的编码规则是：如果 seq[i] 是 A 的一部分，
那么 answer[i] = 0。否则，answer[i] = 1。即便有多个满足要求的答案存在，你也只需返回 一个。

 

示例 1：

输入：seq = "(()())"
输出：[0,1,1,1,1,0]
示例 2：

输入：seq = "()(())()"
输出：[0,0,0,1,1,0,1,1]
 

提示：

1 <= text.size <= 10000
*/
/// <summary>
/// https://leetcode-cn.com/problems/maximum-nesting-depth-of-two-valid-parentheses-strings/
/// 1111. 有效括号的嵌套深度
/// 
/// </summary>
class MaximumNestingDepthOfTwoValidParenthesesStringsSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int[] MaxDepthAfterSplit(string seq)
    {
        int len = seq.Length;
        int[] ret = new int[len];
        ret[0] = 0;
        for (int i = 1; i < len; i++) {
            if (seq[i] == seq[i - 1]) ret[i] = 1 - ret[i - 1];
            else ret[i] = ret[i - 1];
        }
        return ret;
    }
}

/*


最简单的方法，不需要任何变量
scut_green
发布于 2 个月前
101 阅读
代码思路：只需要判断当前字符与前一个字符的关系
两个单括号组成的情况有四种:'((', '))', '()', ')('
前两种情况，这两个单括号肯定不能在同一子序列中，因为那样会加深字符串深度，所以分别分到A，B组
后两种情况，分到同一组不会加深字符串深度，所以可以分到同一组

class Solution:
    def maxDepthAfterSplit(self, seq: str) -> List[int]:
        res = [0]*len(seq)
        for i in range(1, len(seq)):
            if seq[i] == seq[i-1]: # 前后分到不同组
                res[i] = 1 - res[i-1]
            else:
                res[i] = res[i-1] # 前后同组
        return res
		
public class Solution {
    public int[] MaxDepthAfterSplit(string seq) {
        Stack<char> stackA = new Stack<char>();
        Stack<char> stackB = new Stack<char>();
        int[] result = new int[seq.Length];
        bool AIn = true;
        bool Aout = true;
        for(int i=0;i<seq.Length;i++)
        {
            if(seq[i]=='(')
            {
                if(AIn)
                {
                    stackA.Push(seq[i]);
                    result[i] = 0;
                    AIn = false;
                }
                else
                {
                    stackB.Push(seq[i]);
                    result[i] = 1;
                    AIn = true;
                }
            }
            else
            {
                if(Aout && stackA.Count>0)
                {
                    stackA.Pop();
                    result[i] = 0;
                    Aout = false;
                }
                else if(!Aout && stackB.Count>0)
                {
                    stackB.Pop();
                    result[i] = 1;
                    Aout = true;
                }
                else
                {
                    return new int[0];
                }
            }
        }
        return result;
    }
}
*/