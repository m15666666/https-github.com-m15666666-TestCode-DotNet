using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// https://leetcode-cn.com/problems/linked-list-cycle-ii/
/// 142. 环形链表 II
/// 给定一个链表，返回链表开始入环的第一个节点。 如果链表无环，则返回 null。
/// 说明：不允许修改给定的链表。
/// 进阶：
/// 你是否可以不用额外空间解决此题？
/// https://blog.csdn.net/huan_chen/article/details/80065238
/// </summary>
class LinkedListCycleIISolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    public ListNode DetectCycle(ListNode head)
    {
        if (head == null) return null;

        HashSet<ListNode> set = new HashSet<ListNode>();
        while( head != null)
        {
            if (set.Contains(head)) return head;
            set.Add(head);
            head = head.next;
        }

        return null;
    }

}