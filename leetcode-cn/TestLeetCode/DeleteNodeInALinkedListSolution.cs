using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
请编写一个函数，使其可以删除某个链表中给定的（非末尾）节点。传入函数的唯一参数为 要被删除的节点 。

 

现有一个链表 -- head = [4,5,1,9]，它可以表示为:



 

示例 1：

输入：head = [4,5,1,9], node = 5
输出：[4,1,9]
解释：给定你链表中值为 5 的第二个节点，那么在调用了你的函数之后，该链表应变为 4 -> 1 -> 9.
示例 2：

输入：head = [4,5,1,9], node = 1
输出：[4,5,9]
解释：给定你链表中值为 1 的第三个节点，那么在调用了你的函数之后，该链表应变为 4 -> 5 -> 9.
 

提示：

链表至少包含两个节点。
链表中所有节点的值都是唯一的。
给定的节点为非末尾节点并且一定是链表中的一个有效节点。
不要从你的函数中返回任何结果。


*/
/// <summary>
/// https://leetcode-cn.com/problems/delete-node-in-a-linked-list/
/// 237. 删除链表中的节点
/// 
/// 
/// </summary>
class DeleteNodeInALinkedListSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public void DeleteNode(ListNode node) {
        node.val = node.next.val;
        node.next = node.next.next;
    }

}
/*
删除链表中的节点
力扣 (LeetCode)
发布于 2018-11-14
61.1k
方法：与下一个节点交换
从链表里删除一个节点 node 的最常见方法是修改之前节点的 next 指针，使其指向之后的节点。



因为，我们无法访问我们想要删除的节点 之前 的节点，我们始终不能修改该节点的 next 指针。相反，我们必须将想要删除的节点的值替换为它后面节点中的值，然后删除它之后的节点。







因为我们知道要删除的节点不是列表的末尾，所以我们可以保证这种方法是可行的。


public void deleteNode(ListNode node) {
    node.val = node.next.val;
    node.next = node.next.next;
}
复杂度分析

时间和空间复杂度都是：O(1)O(1)。

public class Solution {
    public void DeleteNode(ListNode node) {
        while(node.next.next != null)
        {
            node.val = node.next.val;
            node = node.next;
        }
        node.val = node.next.val;
        node.next = null;
    }
}



*/