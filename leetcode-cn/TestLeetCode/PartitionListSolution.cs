using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个链表和一个特定值 x，对链表进行分隔，使得所有小于 x 的节点都在大于或等于 x 的节点之前。

你应当保留两个分区中每个节点的初始相对位置。

示例:

输入: head = 1->4->3->2->5->2, x = 3
输出: 1->2->2->4->3->5
 
*/
/// <summary>
/// https://leetcode-cn.com/problems/partition-list/
/// 86.分隔链表
/// 
/// 
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
/*
分隔链表
力扣 (LeetCode)
发布于 1 年前
18.3k
本题要求我们改变链表结构，使得值小于 x的元素，位于值大于等于x元素的前面。这实质上意味着在改变后的链表中有某个点，在该点之前的元素全部小于x ，该点之后的元素全部 大于等于x。
我们将这个点记为JOINT。

image.png

对该问题的逆向工程告诉我们，如果我们在JOINT将改后链表拆分，我们会得到两个更小的链表，其中一个包括全部值小于x的元素，另一个包括全部值大于x的元素。在解法中，我们的主要目的是创建这两个链表，并将它们连接。

双指针法：
直觉

我们可以用两个指针before 和 after 来追踪上述的两个链表。两个指针可以用于分别创建两个链表，然后将这两个链表连接即可获得所需的链表。

算法

初始化两个指针 before 和 after。在实现中，我们将两个指针初始化为哑 ListNode。这有助于减少条件判断。（不信的话，你可以试着写一个不带哑结点的方法自己看看！）
image.png

利用head指针遍历原链表。
若head 指针指向的元素值 小于 x，该节点应当是 before 链表的一部分。因此我们将其移到 before 中。
image.png

否则，该节点应当是after 链表的一部分。因此我们将其移到 after 中。
image.png

遍历完原有链表的全部元素之后，我们得到了两个链表 before 和 after。原有链表的元素或者在before 中或者在 after 中，这取决于它们的值。
image.png

*`注意:` 由于我们从左到右遍历了原有链表，故两个链表中元素的相对顺序不会发生变化。另外值得注意的是，在图中我们完好地保留了原有链表。事实上，在算法实现中，我们将节点从原有链表中移除，并将它们添加到别的链表中。我们没有使用任何额外的空间，只是将原有的链表元素进行移动。*
现在，可以将 before 和 after 连接，组成所求的链表。
image.png

为了算法实现更容易，我们使用了哑结点初始化。不能让哑结点成为返回链表中的一部分，因此在组合两个链表时需要向前移动一个节点。

class Solution {
    public ListNode partition(ListNode head, int x) {

        // before and after are the two pointers used to create the two list
        // before_head and after_head are used to save the heads of the two lists.
        // All of these are initialized with the dummy nodes created.
        ListNode before_head = new ListNode(0);
        ListNode before = before_head;
        ListNode after_head = new ListNode(0);
        ListNode after = after_head;

        while (head != null) {

            // If the original list node is lesser than the given x,
            // assign it to the before list.
            if (head.val < x) {
                before.next = head;
                before = before.next;
            } else {
                // If the original list node is greater or equal to the given x,
                // assign it to the after list.
                after.next = head;
                after = after.next;
            }

            // move ahead in the original list
            head = head.next;
        }

        // Last node of "after" list would also be ending node of the reformed list
        after.next = null;

        // Once all the nodes are correctly assigned to the two lists,
        // combine them to form a single list which would be returned.
        before.next = after_head.next;

        return before_head.next;
    }
}
复杂度分析

时间复杂度: O(N)O(N)，其中NN是原链表的长度，我们对该链表进行了遍历。
空间复杂度: O(1)O(1)，我们没有申请任何新空间。值得注意的是，我们只移动了原有的结点，因此没有使用任何额外空间。
下一篇：GO 双百 两个哨兵节点
 
 
 
*/