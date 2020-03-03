using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给出两个 非空 的链表用来表示两个非负的整数。其中，它们各自的位数是按照 逆序 的方式存储的，并且它们的每个节点只能存储 一位 数字。

如果，我们将这两个数相加起来，则会返回一个新的链表来表示它们的和。

您可以假设除了数字 0 之外，这两个数都不会以 0 开头。

示例：

输入：(2 -> 4 -> 3) + (5 -> 6 -> 4)
输出：7 -> 0 -> 8
原因：342 + 465 = 807
*/
/// <summary>
/// https://leetcode-cn.com/problems/add-two-numbers/
/// 2. 两数相加
/// 
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