﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
给定一个单链表，把所有的奇数节点和偶数节点分别排在一起。请注意，这里的奇数节点和偶数节点指的是节点编号的奇偶性，而不是节点的值的奇偶性。

请尝试使用原地算法完成。你的算法的空间复杂度应为 O(1)，时间复杂度应为 O(nodes)，nodes 为节点总数。

示例 1:

输入: 1->2->3->4->5->NULL
输出: 1->3->5->2->4->NULL
示例 2:

输入: 2->1->3->5->6->4->7->NULL 
输出: 2->3->6->7->1->5->4->NULL
说明:

应当保持奇数节点和偶数节点的相对顺序。
链表的第一个节点视为奇数节点，第二个节点视为偶数节点，以此类推。
*/
/// <summary>
/// https://leetcode-cn.com/problems/odd-even-linked-list/
/// 328. 奇偶链表
/// </summary>
class OddEvenLinkedListSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public ListNode OddEvenList(ListNode head)
    {
        if (head == null) return head;

        var odd = head;
        var even = odd.next;
        var firstEven = even;
        while( true )
        {
            if (even == null || even.next == null) break;

            odd = odd.next = even.next;
            even = even.next = odd.next;
        }
        odd.next = firstEven;

        return head;
    }
}
/*
public class Solution {
    public ListNode OddEvenList(ListNode head) {
        if(head == null || head.next == null || head.next.next == null)
            return head;
        ListNode odd = head;
        ListNode even = head.next;
        ListNode evenHead = even;
        while(even != null && even.next != null){
            odd.next = even.next;
            odd = odd.next;
            even.next = odd.next;
            even = even.next;
        }
        odd.next = evenHead;
        return head;
    }
} 
*/
