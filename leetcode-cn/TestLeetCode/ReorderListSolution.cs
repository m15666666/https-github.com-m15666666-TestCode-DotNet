using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// https://leetcode-cn.com/problems/reorder-list/
/// 143. 重排链表
/// 给定一个单链表 L：L0→L1→…→Ln-1→Ln ，
/// 将其重新排列后变为： L0→Ln→L1→Ln-1→L2→Ln-2→…
/// 你不能只是单纯的改变节点内部的值，而是需要实际的进行节点交换。
/// 示例 1:
/// 给定链表 1->2->3->4, 重新排列为 1->4->2->3.
/// 示例 2:
/// 给定链表 1->2->3->4->5, 重新排列为 1->5->2->4->3.
/// </summary>
class ReorderListSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    public void ReorderList(ListNode head)
    {
        if (head == null || head.next == null || head.next.next == null) return;

        var parent = head.next;
        var child = parent.next;

        while( child.next != null)
        {
            parent = child;
            child = child.next;
        }

        child.next = head.next;
        head.next = child;
        parent.next = null;

        ReorderList(head.next.next);
    }

}
/*
// 别人做的答案，包含：快慢指针找链表中点，双指针反转链表等技巧。
public class Solution
{
    public void ReorderList(ListNode head)
    {
        if (head == null || head.next == null) return;
        var mid = findMiddle(head);
        var tail = reverse(mid.next);
        mid.next = null;
        merge(head, tail);
    }

    private ListNode findMiddle(ListNode head)
    {
        var fast = head.next;
        var slow = head;
        while (fast != null && fast.next != null)
        {
            fast = fast.next.next;
            slow = slow.next;
        }
        return slow;
    }

    private ListNode reverse(ListNode head)
    {
        ListNode pre = null;
        var curt = head;
        while (curt != null)
        {
            var temp = curt.next;
            curt.next = pre;
            pre = curt;
            curt = temp;
        }
        return pre;
    }

    private void merge(ListNode head1, ListNode head2)
    {
        var head = new ListNode(0);
        var count = 0;
        while (head1 != null && head2 != null)
        {
            if (count % 2 == 0)
            {
                head.next = head1;
                head1 = head1.next;
            }
            else
            {
                head.next = head2;
                head2 = head2.next;
            }
            head = head.next;
            count++;
        }
        if (head1 != null)
        {
            head.next = head1;
        }
        else
        {
            head.next = head2;
        }
    }
}

*/