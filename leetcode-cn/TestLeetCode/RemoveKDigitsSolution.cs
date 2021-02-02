using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
给定一个以字符串表示的非负整数 num，移除这个数中的 k 位数字，使得剩下的数字最小。

注意:

num 的长度小于 10002 且 ≥ k。
num 不会包含任何前导零。
示例 1 :

输入: num = "1432219", k = 3
输出: "1219"
解释: 移除掉三个数字 4, 3, 和 2 形成一个新的最小的数字 1219。
示例 2 :

输入: num = "10200", k = 1
输出: "200"
解释: 移掉首位的 1 剩下的数字为 200. 注意输出不能有任何前导零。
示例 3 :

输入: num = "10", k = 2
输出: "0"
解释: 从原数字移除所有的数字，剩余为空就是0。 
*/
/// <summary>
/// https://leetcode-cn.com/problems/remove-k-digits/
/// 402. 移掉K位数字
/// https://www.cnblogs.com/randyniu/p/9334144.html
/// </summary>
class RemoveKDigitsSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public string RemoveKdigits(string num, int k)
    {
        if (string.IsNullOrWhiteSpace(num) || num.Length == k) return "0";
        if (k < 1) return num;

        Stack<int> stack = new Stack<int>();//为了遍历，因此可以用vector来进行模拟。
        for (int i = 0; i < num.Length; ++i) //开始遍历整个string
        {
            int number = num[i] - '0';// 算出当前的值
            while (stack.Count > 0 && number < stack.Peek() && k > 0)// 开始进行贪心判断
            {
                stack.Pop();
                --k;
            }// 否则，如果number是0 并且S中有元素照样可以放置进去
            if (number != 0 || 0 < stack.Count )
            {
                stack.Push(number);
            }
        }//已经遍历完了所有的字符序列 这个时候如果k>0 仍需要进行弹出，这个时候，栈中的元素就是按照升序进行存储的了。
        while ( 0 < stack.Count && k > 0)
        {
            stack.Pop();
            --k;
        }

        var result = new StringBuilder();// 保存最后的结果
        foreach (var c in stack.Reverse())
            result.Append(c);
        return result.Length == 0 ? "0" : result.ToString();

        //int ret = 0;
        //int time = 1;
        //while( 0 < stack.Count )
        //{
        //    ret = ret + stack.Pop() * time;
        //    time *= 10;
        //}
        //return ret.ToString();

        //for (int i = 0; i < S.Count; ++i)
        //{
        //    result.Append(1, '0' + S[i]);
        //}

        //if (result == "")
        //    result = "0";

        //return result;
    }
}
/*
移掉K位数字
力扣官方题解
发布于 2020-11-15
29.2k
方法一：贪心 + 单调栈
对于两个相同长度的数字序列，最左边不同的数字决定了这两个数字的大小，例如，对于 A = 1axxxA=1axxx，B = 1bxxxB=1bxxx，如果 a > ba>b 则 A > BA>B。

基于此，我们可以知道，若要使得剩下的数字最小，需要保证靠前的数字尽可能小。

fig1

让我们从一个简单的例子开始。给定一个数字序列，例如 425425，如果要求我们只删除一个数字，那么从左到右，我们有 44、22 和 55 三个选择。我们将每一个数字和它的左邻居进行比较。从 22 开始，22 小于它的左邻居 44。假设我们保留数字 44，那么所有可能的组合都是以数字 44（即 4242，4545）开头的。相反，如果移掉 44，留下 22，我们得到的是以 22 开头的组合（即 2525），这明显小于任何留下数字 44 的组合。因此我们应该移掉数字 44。如果不移掉数字 44，则之后无论移掉什么数字，都不会得到最小数。

基于上述分析，我们可以得出「删除一个数字」的贪心策略：

给定一个长度为 nn 的数字序列 [D_0D_1D_2D_3\ldots D_{n-1}][D 
0
​	
 D 
1
​	
 D 
2
​	
 D 
3
​	
 …D 
n−1
​	
 ]，从左往右找到第一个位置 ii（i>0i>0）使得 D_i<D_{i-1}D 
i
​	
 <D 
i−1
​	
 ，并删去 D_{i-1}D 
i−1
​	
 ；如果不存在，说明整个数字序列单调不降，删去最后一个数字即可。

基于此，我们可以每次对整个数字序列执行一次这个策略；删去一个字符后，剩下的 n-1n−1 长度的数字序列就形成了新的子问题，可以继续使用同样的策略，直至删除 kk 次。

然而暴力的实现复杂度最差会达到 O(nk)O(nk)（考虑整个数字序列是单调不降的），因此我们需要加速这个过程。

考虑从左往右增量的构造最后的答案。我们可以用一个栈维护当前的答案序列，栈中的元素代表截止到当前位置，删除不超过 kk 次个数字后，所能得到的最小整数。根据之前的讨论：在使用 kk 个删除次数之前，栈中的序列从栈底到栈顶单调不降。

因此，对于每个数字，如果该数字小于栈顶元素，我们就不断地弹出栈顶元素，直到

栈为空
或者新的栈顶元素不大于当前数字
或者我们已经删除了 kk 位数字


上述步骤结束后我们还需要针对一些情况做额外的处理：

如果我们删除了 mm 个数字且 m<km<k，这种情况下我们需要从序列尾部删除额外的 k-mk−m 个数字。
如果最终的数字序列存在前导零，我们要删去前导零。
如果最终数字序列为空，我们应该返回 00。
最终，从栈底到栈顶的答案序列即为最小数。

考虑到栈的特点是后进先出，如果通过栈实现，则需要将栈内元素依次弹出然后进行翻转才能得到最小数。为了避免翻转操作，可以使用双端队列代替栈的实现。


class Solution {
    public String removeKdigits(String num, int k) {
        Deque<Character> deque = new LinkedList<Character>();
        int length = num.length();
        for (int i = 0; i < length; ++i) {
            char digit = num.charAt(i);
            while (!deque.isEmpty() && k > 0 && deque.peekLast() > digit) {
                deque.pollLast();
                k--;
            }
            deque.offerLast(digit);
        }
        
        for (int i = 0; i < k; ++i) {
            deque.pollLast();
        }
        
        StringBuilder ret = new StringBuilder();
        boolean leadingZero = true;
        while (!deque.isEmpty()) {
            char digit = deque.pollFirst();
            if (leadingZero && digit == '0') {
                continue;
            }
            leadingZero = false;
            ret.append(digit);
        }
        return ret.length() == 0 ? "0" : ret.toString();
    }
}
复杂度分析

时间复杂度：O(n)O(n)，其中 nn 为字符串的长度。尽管存在嵌套循环，但内部循环最多运行 kk 次。由于 0 < k \le n0<k≤n，主循环的时间复杂度被限制在 2n2n 以内。对于主循环之外的逻辑，它们的时间复杂度是 O(n)O(n)，因此总时间复杂度为 O(n)O(n)。

空间复杂度：O(n)O(n)。栈存储数字需要线性的空间

public class Solution {
    public string RemoveKdigits(string num, int k) {
        int n = num.Count();
        if (k >= n) return "0";
        Stack<char> stack = new Stack<char>();
        for (int i = 0; i < n; i++)
        {
            var c = stack.FirstOrDefault();
            while (c > num[i] && k > 0)
            {
                stack.Pop();
                c = stack.FirstOrDefault();
                k--;
            }
            if (stack.Count == 0 && num[i] == '0') continue;
            stack.Push(num[i]);
        }
        while (k > 0)
        {
            stack.Pop();
            k--;
        }
        var results = stack.Reverse();
        return results.Count() == 0 ? "0" : string.Join("", results.ToArray());
    }
}
public class Solution {
    public string RemoveKdigits(string num, int k) {
        Stack<char> stack = new Stack<char>();
        
        for (int i = 0; i < num.Length; i ++) {
            char c = num[i];
            while(stack.Count() > 0 && stack.Peek() > c && k > 0) {
                stack.Pop();
                k--;
            } 
            stack.Push(c);
        }
        
        while(k>0) {
            stack.Pop();
            k--;
        }
        
        Stack<char> tempStack = new Stack<char>();
        while(stack.Count() > 0) {
            tempStack.Push(stack.Pop());
        }
        
        StringBuilder sb = new StringBuilder();
        bool firstNo0 = false;
        while(tempStack.Count() > 0) {
            char c = tempStack.Pop();
            if (firstNo0 == false) {
                if (c != '0') {
                    firstNo0 = true;
                    sb.Append(c);
                }
            } else {
                sb.Append(c);
            }
            
        }
        
        string result = sb.ToString();
        if (result.Length == 0) {
            return "0";
        } else {
            return result;
        }
    }
}
*/
