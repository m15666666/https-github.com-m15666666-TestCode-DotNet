using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个排序链表，删除所有含有重复数字的节点，只保留原始链表中 没有重复出现 的数字。

示例 1:

输入: 1->2->3->3->4->4->5
输出: 1->2->5
示例 2:

输入: 1->1->1->2->3
输出: 2->3
     
*/
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
/*
java双指针解法
Kris
发布于 1 个月前
1.4k
因为这道题没有官方题解嘛，萌新小白也来写一篇题解嘿嘿，如果有不足之处欢迎提出噢！！！（本文没有任何排版！！）

话不多说，首先看题是删除所有链表中的所有重复节点！！

为了方便处理头结点，这里我们添加一个哨兵节点no1，让他指向head！（实不相瞒是看大佬题解学到的超级有用！！）
（所有链表题加一个哨兵节点都可以非常方便处理头部节点！）

然后就用一前一后的p1,p2指针来查看是否有相同的节点，这里我分两种情况
如果有相同的节点：（r=1)
就让p2后移直到一个新节点
直接用p1.next=p2.next 就可以删掉所有重复节点啦！！
如果没有相同的节点：(r=0)
就是寻常的p1,p2后移即可

class Solution {
    public ListNode deleteDuplicates(ListNode head) {
        ListNode no1=new ListNode(0);no1.next=head;
        ListNode p1=no1,p2=head;
        int r=0;
        while(p1.next!=null){
            while(p2.next!=null&&p2.next.val==p1.next.val){
                p2=p2.next;
                r=1;//如果有重复节点则r置为1
            }
            if(r==1){
                p1.next=p2.next;//删掉重复节点
                p2=p2.next;//再指针后移！！这时候p1不用动！
                r=0;
            }else{
                p1=p2;//指针后移!p1,p2都向后移动一下
                if(p2.next!=null)p2=p2.next;
            }
        }
        return no1.next;
    }
} 
     
*/