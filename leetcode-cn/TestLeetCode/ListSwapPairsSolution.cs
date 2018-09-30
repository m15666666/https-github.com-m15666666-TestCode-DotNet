using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// https://leetcode-cn.com/problems/swap-nodes-in-pairs/description/
/// 两两交换链表中的节点
/// 给定一个链表，两两交换其中相邻的节点，并返回交换后的链表。
/// </summary>
class ListSwapPairsSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    public ListNode SwapPairs(ListNode head)
    {
        if ( head == null ) return head;

        var ret = head.next != null ? head.next : head;

        var first = head;
        ListNode parent = null;

        while ( first != null )
        {
            var second = first.next;
            if (second == null) break;

            var third = second.next;

            first.next = third;
            second.next = first;

            if ( parent != null ) parent.next = second;

            parent = first;
            first = third;
        }

        return ret;
    }
}