using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个平衡括号字符串 S，按下述规则计算该字符串的分数：

() 得 1 分。
AB 得 A + B 分，其中 A 和 B 是平衡括号字符串。
(A) 得 2 * A 分，其中 A 是平衡括号字符串。
 

示例 1：

输入： "()"
输出： 1
示例 2：

输入： "(())"
输出： 2
示例 3：

输入： "()()"
输出： 2
示例 4：

输入： "(()(()))"
输出： 6
 

提示：

S 是平衡括号字符串，且只含有 ( 和 ) 。
2 <= S.length <= 50
*/
/// <summary>
/// https://leetcode-cn.com/problems/score-of-parentheses/
/// 856. 括号的分数
/// 
/// </summary>
class ScoreOfParenthesesSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int ScoreOfParentheses(string S)
    {
        var stack = new Stack<int>(); // 保存分数
        stack.Push(0); // The score of the current frame

        foreach (var c in S )
        {
            if (c == '(')
            {
                stack.Push(0);
                continue;
            }

            int currentScore = stack.Pop();
            int parentScore = stack.Pop();

            if (currentScore == 0) parentScore++;
            else parentScore += 2 * currentScore;
                
            stack.Push( parentScore );
        }

        return stack.Pop();
    }
}
/*
class Solution {

    public int scoreOfParentheses(String S) {
        return F(S, 0, S.length());
    }

    public int F(String S, int i, int j) {
        //Score of balanced string S[i:j]
        int ans = 0, bal = 0;

        // Split string into primitives
        for (int k = i; k < j; ++k) {
            bal += S.charAt(k) == '(' ? 1 : -1;
            if (bal == 0) {
                if (k - i == 1) ans++;
                else ans += 2 * F(S, i+1, k);
                i = k+1;
            }
        }

        return ans;
    }
}
方法二：栈
字符串 S 中的每一个位置都有一个“深度”，即该位置外侧嵌套的括号数目。例如，字符串 (()(.())) 中的 . 的深度为 2，因为它外侧嵌套了 2 层括号：(__(.__))。

我们用一个栈来维护当前所在的深度，以及每一层深度的得分。当我们遇到一个左括号 ( 时，我们将深度加一，并且新的深度的得分置为 0。当我们遇到一个右括号 ) 时，我们将当前深度的得分乘二并加到上一层的深度。这里有一种例外情况，如果遇到的是 ()，那么只将得分加一。

下面给出了字符串 (()(())) 每次对应的栈的情况：

[0, 0] (
[0, 0, 0] ((
[0, 1] (()
[0, 1, 0] (()(
[0, 1, 0, 0] (()((
[0, 1, 1] (()(()
[0, 3] (()(())
[6] (()(()))
JavaPython
public int scoreOfParentheses(String S) {
    Stack<Integer> stack = new Stack();
    stack.push(0); // The score of the current frame

    for (char c: S.toCharArray()) {
        if (c == '(')
            stack.push(0);
        else {
            int v = stack.pop();
            int w = stack.pop();
            stack.push(w + Math.max(2 * v, 1));
        }
    }

    return stack.pop();
}
复杂度分析

时间复杂度：O(N)O(N)，其中 NN 是字符串 S 的长度。

空间复杂度：O(N)O(N)，为栈的大小。

方法三：统计核心的数目
事实上，我们可以发现，只有 () 会对字符串 S 贡献实质的分数，其它的括号只会将分数乘二或者将分数累加。因此，我们可以找到每一个 () 对应的深度 x，那么答案就是 2^x 的累加和。

JavaPython
class Solution {

    public int scoreOfParentheses(String S) {
        int ans = 0, bal = 0;
        for (int i = 0; i < S.length(); ++i) {
            if (S.charAt(i) == '(') {
                bal++;
            } else {
                bal--;
                if (S.charAt(i-1) == '(')
                    ans += 1 << bal;
            }
        }

        return ans;
    }
}
复杂度分析

时间复杂度：O(N)O(N)，其中 NN 是字符串 S 的长度。

空间复杂度：O(1)O(1)。

public class Solution {
    public int ScoreOfParentheses(string S) {
        Stack<int> stack = new Stack<int>();
        int curr = 0;
        
        for(int i = 0;i<S.Length;i++){
            if(S[i] == '('){
                stack.Push(curr);
                curr = 0;
            }
            else{
                curr = stack.Pop()+Math.Max(curr*2,1);
            }
        }
        
        return curr;
    }
}

*/
