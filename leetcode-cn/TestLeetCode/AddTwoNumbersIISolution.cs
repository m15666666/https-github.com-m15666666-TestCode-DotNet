using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定两个非空链表来代表两个非负整数。数字最高位位于链表开始位置。它们的每个节点只存储单个数字。将这两数相加会返回一个新的链表。

 

你可以假设除了数字 0 之外，这两个数字都不会以零开头。

进阶:

如果输入链表不能修改该如何处理？换句话说，你不能对列表中的节点进行翻转。

示例:

输入: (7 -> 2 -> 4 -> 3) + (5 -> 6 -> 4)
输出: 7 -> 8 -> 0 -> 7 
*/
/// <summary>
/// https://leetcode-cn.com/problems/add-two-numbers-ii/
/// 445. 两数相加 II
/// </summary>
class AddTwoNumbersIISolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public ListNode AddTwoNumbers(ListNode l1, ListNode l2)
    {
        Stack<int> stack1 = new Stack<int>();
        Stack<int> stack2 = new Stack<int>();

        InitHandler(stack1, l1);
        InitHandler(stack2, l2);
        if( stack1.Count < stack2.Count)
        {
            var t1 = stack1;
            stack1 = stack2;
            stack2 = t1;
        }

        ListNode current = null;
        int carry = 0;
        while( 0 < stack1.Count )
        {
            var v1 = stack1.Pop();
            var v2 = 0 < stack2.Count ? stack2.Pop() : 0;
            var sum = v1 + v2 + carry;
            carry = sum / 10;
            current = new ListNode(sum % 10) { next = current };
        }

        if( 0 < carry ) current = new ListNode(carry) { next = current };

        return current;
    }

    private static readonly Action<Stack<int>, ListNode> InitHandler = (stack, list) => {
        if (list != null && list.val != 0)
        {
            while (list != null)
            {
                stack.Push(list.val);
                list = list.next;
            }
        }
        else stack.Push(0);
    };
}
/*

两数相加 II
力扣官方题解
发布于 2020-04-13
36.6k
📺 视频题解

📖 文字题解
方法一：栈
思路与算法

本题的主要难点在于链表中数位的顺序与我们做加法的顺序是相反的，为了逆序处理所有数位，我们可以使用栈：把所有数字压入栈中，再依次取出相加。计算过程中需要注意进位的情况。


class Solution {
    public ListNode addTwoNumbers(ListNode l1, ListNode l2) {
        Deque<Integer> stack1 = new LinkedList<Integer>();
        Deque<Integer> stack2 = new LinkedList<Integer>();
        while (l1 != null) {
            stack1.push(l1.val);
            l1 = l1.next;
        }
        while (l2 != null) {
            stack2.push(l2.val);
            l2 = l2.next;
        }
        int carry = 0;
        ListNode ans = null;
        while (!stack1.isEmpty() || !stack2.isEmpty() || carry != 0) {
            int a = stack1.isEmpty() ? 0 : stack1.pop();
            int b = stack2.isEmpty() ? 0 : stack2.pop();
            int cur = a + b + carry;
            carry = cur / 10;
            cur %= 10;
            ListNode curnode = new ListNode(cur);
            curnode.next = ans;
            ans = curnode;
        }
        return ans;
    }
}
复杂度分析

时间复杂度：O(max(m, n))O(max(m,n))，其中 mm 与 nn 分别为两个链表的长度。我们需要遍历每个链表。

空间复杂度：O(m + n)O(m+n)，其中 mm 与 nn 分别为两个链表的长度。这是我们把链表内容放入栈中所用的空间。

public class Solution {
    public ListNode AddTwoNumbers(ListNode l1, ListNode l2) {
        Stack<int> s1 = new Stack<int>();
        Stack<int> s2 = new Stack<int>();
        while (l1 != null)
        {
            s1.Push(l1.val);
            l1 = l1.next;
        }
        while (l2 != null)
        {
            s2.Push(l2.val);
            l2 = l2.next;
        }

        ListNode l = new ListNode(0);
        ListNode head = l;
        int carry = 0;
        while (s1.Count > 0 || s2.Count > 0)
        {
            int sum = carry;
            if (s1.Count > 0) sum += s1.Pop();
            if (s2.Count > 0) sum += s2.Pop();
            carry = sum / 10;
            ListNode node = new ListNode(sum % 10);
            node.next = l.next;
            l.next = node;
        }

        head.val = carry;
        return head.val == 0 ? head.next : head;
    }
} 
*/
