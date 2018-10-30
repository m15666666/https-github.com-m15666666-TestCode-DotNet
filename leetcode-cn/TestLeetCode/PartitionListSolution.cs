using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// https://leetcode-cn.com/problems/partition-list/
/// 86.分隔链表
/// 给定一个链表和一个特定值 x，对链表进行分隔，使得所有小于 x 的节点都在大于或等于 x 的节点之前。
/// 你应当保留两个分区中每个节点的初始相对位置。
/// </summary>
class PartitionListSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    public ListNode Partition(ListNode head, int x)
    {
        if (head == null) return null;

        List<ListNode> less = new List<ListNode>();
        List<ListNode> more = new List<ListNode>();

        var current = head;
        while ( current != null )
        {
            if (current.val < x) less.Add(current);
            else more.Add(current);

            current = current.next;
        }

        ListNode ret = null;
        current = null;
        foreach ( var n in less )
        {
            if( ret == null)
            {
                ret = current = n;
                continue;
            }

            current.next = n;
            current = current.next;
        }

        foreach (var n in more)
        {
            if (ret == null)
            {
                ret = current = n;
                continue;
            }

            current.next = n;
            current = current.next;
        }

        if (current != null) current.next = null;

        return ret;
    }
}