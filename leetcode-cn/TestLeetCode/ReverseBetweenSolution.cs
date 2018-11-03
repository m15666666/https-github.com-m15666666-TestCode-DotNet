using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// https://leetcode-cn.com/problems/reverse-linked-list-ii/
/// 92.反转链表II
/// 反转从位置 m 到 n 的链表。请使用一趟扫描完成反转。
/// </summary>
class ReverseBetweenSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    public ListNode ReverseBetween(ListNode head, int m, int n)
    {
        if (head == null || head.next == null || m < 1 || n < 1 || n <= m ) return head;

        ListNode prev = null;
        ListNode next = null;
        ListNode firstReverse = null;
        ListNode prevReverse = null;

        var current = head;
        var currentIndex = 1;

        if ( 1 == m )
        {
            prev = null;
            firstReverse = current;
            ReverseList(firstReverse, currentIndex, n, ref prevReverse, ref next);

            firstReverse.next = next;

            return prevReverse;
        }

        var mMinusOne = m - 1;
        while (current != null)
        {
            if (currentIndex < mMinusOne )
            {
                // do nothing
            }
            else if (currentIndex == mMinusOne )
            {
                prev = current;
                firstReverse = current.next;
                currentIndex++;

                ReverseList(firstReverse, currentIndex, n, ref prevReverse, ref next);

                break;
            }

            currentIndex++;
            current = current.next;
        }

        if ( prev == null || firstReverse == null ) return head;

        prev.next = prevReverse;
        firstReverse.next = next;

        return head;
    }

    private void ReverseList( ListNode firstReverse, int currentIndex, int n, ref ListNode prevReverse, ref ListNode next )
    {
        if (firstReverse == null) return;

        var reverse = firstReverse;
        while (reverse != null)
        {
            next = reverse.next;
            reverse.next = prevReverse;

            prevReverse = reverse;

            if (currentIndex == n) break;

            currentIndex++;
            reverse = next;
        }
    }
}