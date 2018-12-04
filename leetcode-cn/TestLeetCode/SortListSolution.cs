using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// https://leetcode-cn.com/problems/sort-list/
/// 148. 排序链表
/// 在 O(n log n) 时间复杂度和常数级空间复杂度下，对链表进行排序。
/// </summary>
class SortListSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    public ListNode SortList(ListNode head)
    {
        if (head == null || head.next == null) return head;

        ListNode root = new ListNode(int.MinValue);
        ListNode tail = root;
        ListNode middle = root;
        int count = 0;
        int midIndex = 0;

        var current = head;

        while (current != null)
        {
            count++;
            var v = current.val;
            var next = current.next;

            if( tail.val <= v)
            {
                tail.next = current;
                tail = current;
                tail.next = null;
                current = next;
                continue;
            }

            ListNode previous = null;
            var newCurrent = middle.val <= v ? middle : root;
            var newCurrentIndex = middle.val <= v ? midIndex : 0;
            while (newCurrent != null && newCurrent.val <= v)
            {
                if ( newCurrentIndex == count / 2 + 1 ) middle = newCurrent;
                previous = newCurrent;
                newCurrent = newCurrent.next;

                newCurrentIndex++;
            }

            previous.next = current;
            current.next = newCurrent;

            if (newCurrent == null) tail = current;
            else middle = current;

            current = next;
        }

        return root.next;
    }

}