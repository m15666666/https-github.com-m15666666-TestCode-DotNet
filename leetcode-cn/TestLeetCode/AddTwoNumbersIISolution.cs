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
