using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// https://leetcode-cn.com/problems/remove-duplicates-from-sorted-list-ii/
/// 82.删除排序链表中的重复元素II
/// 给定一个排序链表，删除所有含有重复数字的节点，只保留原始链表中 没有重复出现 的数字。
/// </summary>
class DeleteDuplicatesSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    public ListNode DeleteDuplicates(ListNode head)
    {
        if (head == null) return null;

        ListNode retFirst = null;
        ListNode retLast = null;

        var current = head;
        var currentFirst = current;
        int currentCount = 1;
        var v = current.val;
        var next = current.next;
        while ( next != null)
        {
            if( v == next.val )
            {
                currentCount++;
                current = next;
                next = current.next;
                continue;
            }

            if( currentCount == 1 )
            {
                // 添加
                if ( retFirst == null )
                    retLast = retFirst = currentFirst;
                else
                {
                    retLast.next = currentFirst;
                    retLast = retLast.next;
                }
            }
            else
            {
                // 抛弃
            }

            currentFirst = current = next;
            currentCount = 1;
            v = current.val;
            next = current.next;
        }

        if (currentCount == 1)
        {
            // 添加
            if (retFirst == null)
                retLast = retFirst = currentFirst;
            else
            {
                retLast.next = currentFirst;
                retLast = retLast.next;
            }
        }

        if (retLast != null) retLast.next = null;

        return retFirst;
    }
}