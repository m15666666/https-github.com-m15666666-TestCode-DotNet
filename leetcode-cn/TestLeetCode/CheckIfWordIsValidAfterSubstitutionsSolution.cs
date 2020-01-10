using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定有效字符串 "abc"。

对于任何有效的字符串 V，我们可以将 V 分成两个部分 X 和 Y，使得 X + Y（X 与 Y 连接）等于 V。（X 或 Y 可以为空。）那么，X + "abc" + Y 也同样是有效的。

例如，如果 S = "abc"，则有效字符串的示例是："abc"，"aabcbc"，"abcabc"，"abcabcababcc"。无效字符串的示例是："abccba"，"ab"，"cababc"，"bac"。

如果给定字符串 S 有效，则返回 true；否则，返回 false。

 

示例 1：

输入："aabcbc"
输出：true
解释：
从有效字符串 "abc" 开始。
然后我们可以在 "a" 和 "bc" 之间插入另一个 "abc"，产生 "a" + "abc" + "bc"，即 "aabcbc"。
示例 2：

输入："abcabcababcc"
输出：true
解释：
"abcabcabc" 是有效的，它可以视作在原串后连续插入 "abc"。
然后我们可以在最后一个字母之前插入 "abc"，产生 "abcabcab" + "abc" + "c"，即 "abcabcababcc"。
示例 3：

输入："abccba"
输出：false
示例 4：

输入："cababc"
输出：false
 

提示：

1 <= S.length <= 20000
S[i] 为 'a'、'b'、或 'c'
*/
/// <summary>
/// https://leetcode-cn.com/problems/check-if-word-is-valid-after-substitutions/
/// 1003. 检查替换后的词是否有效
/// 
/// </summary>
class CheckIfWordIsValidAfterSubstitutionsSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public bool IsValid(string S)
    {
        Stack<char> stack = new Stack<char>();
        foreach (char c in S)
        {
            if (c != 'c')
            {
                stack.Push(c);
            }
            else
            {
                if (stack.Count == 0 || stack.Peek() != 'b') return false;
                stack.Pop();
                if (stack.Count == 0 || stack.Peek() != 'a') return false;
                stack.Pop();
            }
        }
        return stack.Count == 0;
    }
}
/*
C++ 使用 stack 解决
Crossing
发布于 2 个月前
205 阅读
Talk is cheap. Show me the code.

class Solution {
public:
    bool isValid(string S) {
        stack<char> stk;
        for (char c : S) {
            if (c != 'c') {
                stk.push(c);
            } else {
                if (stk.empty() || stk.top() != 'b') return false;
                stk.pop();
                if (stk.empty() || stk.top() != 'a') return false;
                stk.pop();
            }
        }
        return stk.empty();
    }
};
1107.png

下一篇：Java 反复删除“abc”，简单易懂

public class Solution {
    public bool IsValid(string S) {
            List<char> stack = new List<char>();
            if (S.Length%3 != 0 || S.Length == 0 || S[0] == 'b' || S[0] == 'c' || S[1] == 'c')
                return false;
            foreach (char c in S)
            {
                if (c == 'a')
                    stack.Add(c);
                else if (c == 'b')
                {
                    if (stack.Count < 1 || stack[stack.Count - 1] != 'a')
                        return false;
                    stack.Add(c);
                }
                else
                {
                    if (stack.Count < 2 || stack[stack.Count - 1] != 'b')
                        return false;
                    stack.RemoveAt(stack.Count - 1);
                    stack.RemoveAt(stack.Count - 1);
                }
            }
            return stack.Count == 0;
    }
}

 
*/
