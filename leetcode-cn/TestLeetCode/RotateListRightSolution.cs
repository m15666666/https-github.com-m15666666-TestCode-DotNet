using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
61. 旋转链表
给定一个链表，旋转链表，将链表每个节点向右移动 k 个位置，其中 k 是非负数。

示例 1:

输入: 1->2->3->4->5->NULL, k = 2
输出: 4->5->1->2->3->NULL
解释:
向右旋转 1 步: 5->1->2->3->4->NULL
向右旋转 2 步: 4->5->1->2->3->NULL
示例 2:

输入: 0->1->2->NULL, k = 4
输出: 2->0->1->NULL
解释:
向右旋转 1 步: 2->0->1->NULL
向右旋转 2 步: 1->2->0->NULL
向右旋转 3 步: 0->1->2->NULL
向右旋转 4 步: 2->0->1->NULL` 
*/
/// <summary>
/// https://leetcode-cn.com/problems/rotate-list/
/// 61.旋转链表
/// 
/// 
/// </summary>
class RotateListRightSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public ListNode RotateRight(ListNode head, int k)
    {
        if (k == 0 || head == null || head.next == null) return head;

        ListNode oldTail = head;
        int n;
        for(n = 1; oldTail.next != null; n++) oldTail = oldTail.next;

        k %= n;
        if (k == 0) return head;

        oldTail.next = head;

        ListNode newTail = head;
        for (int i = 0; i < n - k - 1; i++) newTail = newTail.next;

        ListNode ret = newTail.next;

        newTail.next = null;

        return ret;
    }

    //public ListNode RotateRight(ListNode head, int k)
    //{
    //    if (head == null || head.next == null || k < 1) return head;

    //    Dictionary<int, ListNode> index2Node = new Dictionary<int, ListNode>();
    //    var current = head;
    //    int count = 0;
    //    while( current != null)
    //    {
    //        index2Node[count++] = current;
    //        current = current.next;
    //    }

    //    k = k % count;
    //    if (k == 0) return head;

    //    var newLastNodeIndex = count - 1 - k;
    //    var newLastNode = index2Node[newLastNodeIndex];
    //    var newFirstNode = newLastNode.next;
    //    newLastNode.next = null;

    //    var oldLastNode = index2Node[count - 1];
    //    oldLastNode.next = head;

    //    return newFirstNode;
    //}

}
/*
 
*/