﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// https://leetcode-cn.com/problems/insertion-sort-list/
/// 147. 对链表进行插入排序 Insertion Sort List
/// 对链表进行插入排序。
/// </summary>
class InsertionSortListSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    public ListNode InsertionSortList(ListNode head)
    {
        if (head == null || head.next == null) return head;

        ListNode root = new ListNode(int.MinValue);
        var current = head;

        while ( current != null )
        {
            var v = current.val;
            var next = current.next;

            ListNode previous = null;
            var newCurrent = root;
            while( newCurrent != null && newCurrent.val <= v )
            {
                previous = newCurrent;
                newCurrent = newCurrent.next;
            }

            previous.next = current;
            current.next = newCurrent;

            current = next;
        }

        return root.next;
    }
}