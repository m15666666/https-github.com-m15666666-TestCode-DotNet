using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个只包含 '(' 和 ')' 的字符串，找出最长的包含有效括号的子串的长度。

示例 1:

输入: "(()"
输出: 2
解释: 最长有效括号子串为 "()"
示例 2:

输入: ")()())"
输出: 4
解释: 最长有效括号子串为 "()()"
*/
/// <summary>
/// https://leetcode-cn.com/problems/longest-valid-parentheses/
/// 32. 最长有效括号
/// 
/// </summary>
class LongestValidParenthesesSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int LongestValidParentheses(string s)
    {
        if (string.IsNullOrEmpty(s)) return 0;

        int left = 0;
        int right = 0;
        int maxHalfLength = 0;
        int len = s.Length;
        for (int i = 0; i < len; i++)
        {
            if (s[i] == '(') left++;
            else right++;
            if (left == right && maxHalfLength < left) maxHalfLength = left;
            else if (left < right) left = right = 0;
        }

        left = right = 0;
        for (int i = len - 1; -1 < i; i--)
        {
            if (s[i] == '(') left++;
            else right++;
            if (left == right && maxHalfLength < left) maxHalfLength = left;
            else if (right < left) left = right = 0;
        }
        return maxHalfLength + maxHalfLength;
    }
}
/*

最长有效括号
力扣 (LeetCode)
发布于 10 个月前
57.6k
方法 1：暴力
算法

在这种方法中，我们考虑给定字符串中每种可能的非空偶数长度子字符串，检查它是否是一个有效括号字符串序列。为了检查有效性，我们使用栈的方法。

每当我们遇到一个 \text{‘(’}‘(’ ，我们把它放在栈顶。对于遇到的每个 \text{‘)’}‘)’ ，我们从栈中弹出一个 \text{‘(’}‘(’ ，如果栈顶没有 \text{‘(’}‘(’，或者遍历完整个子字符串后栈中仍然有元素，那么该子字符串是无效的。这种方法中，我们对每个偶数长度的子字符串都进行判断，并保存目前为止找到的最长的有效子字符串的长度。

例子:
"((())"

(( --> 无效
(( --> 无效
() --> 有效，长度为 2
)) --> 无效
((()--> 无效
(())--> 有效，长度为 4
最长长度为 4
public class Solution {
    public boolean isValid(String s) {
        Stack<Character> stack = new Stack<Character>();
        for (int i = 0; i < s.length(); i++) {
            if (s.charAt(i) == '(') {
                stack.push('(');
            } else if (!stack.empty() && stack.peek() == '(') {
                stack.pop();
            } else {
                return false;
            }
        }
        return stack.empty();
    }
    public int longestValidParentheses(String s) {
        int maxlen = 0;
        for (int i = 0; i < s.length(); i++) {
            for (int j = i + 2; j <= s.length(); j+=2) {
                if (isValid(s.substring(i, j))) {
                    maxlen = Math.max(maxlen, j - i);
                }
            }
        }
        return maxlen;
    }
}
复杂度分析

时间复杂度： O(n^2)O(n 
2
 ) 。从长度为 nn 的字符串产生所有可能的子字符串需要的时间复杂度为 O(n^2)O(n 
2
 ) 。验证一个长度为 nn 的子字符串需要的时间为 O(n)O(n) 。
空间复杂度： O(n)O(n) 。子字符串的长度最多会需要一个深度为 nn 的栈。
方法 2：动态规划
算法

这个问题可以通过动态规划解决。我们定义一个 \text{dp}dp 数组，其中第 ii 个元素表示以下标为 ii 的字符结尾的最长有效子字符串的长度。我们将 \text{dp}dp 数组全部初始化为 0 。现在，很明显有效的子字符串一定以 \text{‘)’}‘)’ 结尾。这进一步可以得出结论：以 \text{‘(’}‘(’ 结尾的子字符串对应的 \text{dp}dp 数组位置上的值必定为 0 。所以说我们只需要更新 \text{‘)’}‘)’ 在 \text{dp}dp 数组中对应位置的值。

为了求出 \text{dp}dp 数组，我们每两个字符检查一次，如果满足如下条件

\text{s}[i] = \text{‘)’}s[i]=‘)’ 且 \text{s}[i - 1] = \text{‘(’}s[i−1]=‘(’ ，也就是字符串形如``……()"‘‘……()"，我们可以推出：

\text{dp}[i]=\text{dp}[i-2]+2
dp[i]=dp[i−2]+2

我们可以进行这样的转移，是因为结束部分的 "()" 是一个有效子字符串，并且将之前有效子字符串的长度增加了 2 。

\text{s}[i] = \text{‘)’}s[i]=‘)’ 且 \text{s}[i - 1] = \text{‘)’}s[i−1]=‘)’，也就是字符串形如 ``.......))"‘‘.......))" ，我们可以推出：
如果 \text{s}[i - \text{dp}[i - 1] - 1] = \text{‘(’}s[i−dp[i−1]−1]=‘(’ ，那么

\text{dp}[i]=\text{dp}[i-1]+\text{dp}[i-\text{dp}[i-1]-2]+2
dp[i]=dp[i−1]+dp[i−dp[i−1]−2]+2

这背后的原因是如果倒数第二个 \text{‘)’}‘)’ 是一个有效子字符串的一部分（记为 sub_ssub 
s
​	
 ），对于最后一个 \text{‘)’}‘)’ ，如果它是一个更长子字符串的一部分，那么它一定有一个对应的 \text{‘(’}‘(’ ，它的位置在倒数第二个 \text{‘)’}‘)’ 所在的有效子字符串的前面（也就是 sub_ssub 
s
​	
  的前面）。因此，如果子字符串 sub_ssub 
s
​	
  的前面恰好是 \text{‘(’}‘(’ ，那么我们就用 22 加上 sub_ssub 
s
​	
  的长度（\text{dp}[i-1]dp[i−1]）去更新 \text{dp}[i]dp[i]。除此以外，我们也会把有效子字符串 ``(,sub_s,)"‘‘(,sub 
s
​	
 ,)"之前的有效子字符串的长度也加上，也就是加上 \text{dp}[i-\text{dp}[i-1]-2]dp[i−dp[i−1]−2] 。



public class Solution {
    public int longestValidParentheses(String s) {
        int maxans = 0;
        int dp[] = new int[s.length()];
        for (int i = 1; i < s.length(); i++) {
            if (s.charAt(i) == ')') {
                if (s.charAt(i - 1) == '(') {
                    dp[i] = (i >= 2 ? dp[i - 2] : 0) + 2;
                } else if (i - dp[i - 1] > 0 && s.charAt(i - dp[i - 1] - 1) == '(') {
                    dp[i] = dp[i - 1] + ((i - dp[i - 1]) >= 2 ? dp[i - dp[i - 1] - 2] : 0) + 2;
                }
                maxans = Math.max(maxans, dp[i]);
            }
        }
        return maxans;
    }
}
复杂度分析

时间复杂度： O(n)O(n) 。遍历整个字符串一次，就可以将 dpdp 数组求出来。
空间复杂度： O(n)O(n) 。需要一个大小为 nn 的 dpdp 数组。
方法 3：栈
算法

与找到每个可能的子字符串后再判断它的有效性不同，我们可以用栈在遍历给定字符串的过程中去判断到目前为止扫描的子字符串的有效性，同时能的都最长有效字符串的长度。我们首先将 -1−1 放入栈顶。

对于遇到的每个 \text{‘(’}‘(’ ，我们将它的下标放入栈中。
对于遇到的每个 \text{‘)’}‘)’ ，我们弹出栈顶的元素并将当前元素的下标与弹出元素下标作差，得出当前有效括号字符串的长度。通过这种方法，我们继续计算有效子字符串的长度，并最终返回最长有效子字符串的长度。



public class Solution {

    public int longestValidParentheses(String s) {
        int maxans = 0;
        Stack<Integer> stack = new Stack<>();
        stack.push(-1);
        for (int i = 0; i < s.length(); i++) {
            if (s.charAt(i) == '(') {
                stack.push(i);
            } else {
                stack.pop();
                if (stack.empty()) {
                    stack.push(i);
                } else {
                    maxans = Math.max(maxans, i - stack.peek());
                }
            }
        }
        return maxans;
    }
}
复杂度分析

时间复杂度： O(n)O(n) 。 nn 是给定字符串的长度。

空间复杂度： O(n)O(n) 。栈的大小最大达到 nn 。

方法 4：不需要额外的空间
算法

在这种方法中，我们利用两个计数器 leftleft 和 rightright 。首先，我们从左到右遍历字符串，对于遇到的每个 \text{‘(’}‘(’，我们增加 leftleft 计算器，对于遇到的每个 \text{‘)’}‘)’ ，我们增加 rightright 计数器。每当 leftleft 计数器与 rightright 计数器相等时，我们计算当前有效字符串的长度，并且记录目前为止找到的最长子字符串。如果 rightright 计数器比 leftleft 计数器大时，我们将 leftleft 和 rightright 计数器同时变回 00 。

接下来，我们从右到左做一遍类似的工作。



public class Solution {
    public int longestValidParentheses(String s) {
        int left = 0, right = 0, maxlength = 0;
        for (int i = 0; i < s.length(); i++) {
            if (s.charAt(i) == '(') {
                left++;
            } else {
                right++;
            }
            if (left == right) {
                maxlength = Math.max(maxlength, 2 * right);
            } else if (right >= left) {
                left = right = 0;
            }
        }
        left = right = 0;
        for (int i = s.length() - 1; i >= 0; i--) {
            if (s.charAt(i) == '(') {
                left++;
            } else {
                right++;
            }
            if (left == right) {
                maxlength = Math.max(maxlength, 2 * left);
            } else if (left >= right) {
                left = right = 0;
            }
        }
        return maxlength;
    }
}
复杂度分析

时间复杂度： O(n)O(n) 。遍历两遍字符串。
空间复杂度： O(1)O(1) 。仅有两个额外的变量 leftleft 和 rightright 。

遍历中心，双指针，中心扩展
lzhlyle
发布于 8 天前
274
思路
示例：))(()())(())((
遍历并寻找中心 c
以 left = c, right = c + 1 进行中心扩展，内部 递归寻找 最左边能到达的索引


代码
// (right, left)
private Map<Integer, Integer> map = new HashMap<>();
// key: 右括号的索引, value: 对应左括号的索引

public int longestValidParentheses(String s) {
    char[] arr = s.toCharArray();
    int max = 0;
    // 遍历中心
    for (int c = 0; c < arr.length - 1; c++) {
        if (arr[c] == '(' && arr[c + 1] == ')') {
            int r = expand(arr, c, c + 1);
            max = Math.max(max, r - map.get(r) + 1);
            c = r;
        }
    }
    return max;
}

// 中心扩展
private int expand(char[] arr, int l, int r) {
    while (l >= 0 && r < arr.length && arr[l] == '(' && arr[r] == ')') {
        l--; r++;
    }
    if (map.containsKey(l)) return expand(arr, map.get(l) - 1, r); // 继续递归扩展
    map.put(r - 1, l + 1); // (右, 左)
    return r - 1;
}
复杂度分析
空间复杂度 O(n)O(n)
不计递归占用的话，主要是 char[] arr 的内存空间
时间复杂度 O(n)O(n)
虽然看起来是 for 内再 while 又 expand 递归，但从看大局 c 也在不断右移
并没有浪费左移的重复时间
下一篇：巧妙的动态规划

*/
