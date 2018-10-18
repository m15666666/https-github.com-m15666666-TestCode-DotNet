using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// https://leetcode-cn.com/problems/rotate-list/
/// 61.旋转链表
/// 给定一个链表，旋转链表，将链表每个节点向右移动 k 个位置，其中 k 是非负数。
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
        if (head == null || head.next == null || k < 1) return head;

        Dictionary<int, ListNode> index2Node = new Dictionary<int, ListNode>();
        var current = head;
        int count = 0;
        while( current != null)
        {
            index2Node[count++] = current;
            current = current.next;
        }

        k = k % count;
        if (k == 0) return head;

        var newLastNodeIndex = count - 1 - k;
        var newLastNode = index2Node[newLastNodeIndex];
        var newFirstNode = newLastNode.next;
        newLastNode.next = null;

        var oldLastNode = index2Node[count - 1];
        oldLastNode.next = head;

        return newFirstNode;
    }

}