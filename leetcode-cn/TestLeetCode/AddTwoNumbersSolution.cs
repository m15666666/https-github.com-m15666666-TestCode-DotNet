using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// https://leetcode-cn.com/problems/add-two-numbers/description/
/// 给定两个非空链表来表示两个非负整数。位数按照逆序方式存储，它们的每个节点只存储单个数字。将两数相加返回一个新的链表。
/// 你可以假设除了数字 0 之外，这两个数字都不会以零开头。
/// </summary>
class AddTwoNumbersSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public ListNode AddTwoNumbers(ListNode l1, ListNode l2)
    {
        if (l1 == null) return l2;
        if (l2 == null) return l1;

        List<int> result = new List<int>();

        int more = 0;

        while (true)
        {
            if (l1 == null && l2 == null) break;

            int v1 = 0;
            if (l1 != null)
            {
                v1 = l1.val;
                l1 = l1.next;
            }

            int v2 = 0;
            if (l2 != null)
            {
                v2 = l2.val;
                l2 = l2.next;
            }

            var sum = v1 + v2 + more;

            if( 9 < sum )
            {
                more = 1;
            }
            else
            {
                more = 0;
            }

            result.Insert(0, sum % 10);
        }

        if( 0 < more ) result.Insert(0, more );

        while (result[0] == 0 && 1 < result.Count) result.RemoveAt(0);

        result.Reverse();

        ListNode ret = null;
        ListNode current = null;

        foreach ( var v in result)
        {
            var a = new ListNode( v );

            if (ret == null) {
                ret = a;
                current = ret;
            }
            else {
                current.next = a;
                current = current.next;
            }
        }

        return ret;
    }
}


/// <summary>
/// Definition for singly-linked list.
/// </summary>
public class ListNode
{
    public int val;
    public ListNode next;
    public ListNode(int x) { val = x; }
}