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
