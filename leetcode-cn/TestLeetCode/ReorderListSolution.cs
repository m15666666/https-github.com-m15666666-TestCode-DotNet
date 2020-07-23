

/*
给定一个单链表 L：L0→L1→…→Ln-1→Ln ，
将其重新排列后变为： L0→Ln→L1→Ln-1→L2→Ln-2→…

你不能只是单纯的改变节点内部的值，而是需要实际的进行节点交换。

示例 1:

给定链表 1->2->3->4, 重新排列为 1->4->2->3.
示例 2:

给定链表 1->2->3->4->5, 重新排列为 1->5->2->4->3.

 
*/
/// <summary>
/// https://leetcode-cn.com/problems/reorder-list/
/// 143. 重排链表
/// 给定一个单链表 L：L0→L1→…→Ln-1→Ln ，
/// 将其重新排列后变为： L0→Ln→L1→Ln-1→L2→Ln-2→…
/// 你不能只是单纯的改变节点内部的值，而是需要实际的进行节点交换。
/// 示例 1:
/// 给定链表 1->2->3->4, 重新排列为 1->4->2->3.
/// 示例 2:
/// 给定链表 1->2->3->4->5, 重新排列为 1->5->2->4->3.
/// </summary>
internal class ReorderListSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public void ReorderList(ListNode head)
    {
        if (head == null || head.next == null) return;

        var mid = FindMiddle(head);
        var tail = Reverse(mid.next);
        mid.next = null;
        Merge(head, tail);

        ListNode FindMiddle(ListNode head)
        {
            var fast = head.next;
            var slow = head;
            while (fast != null && fast.next != null)
            {
                fast = fast.next.next;
                slow = slow.next;
            }
            return slow;
        }

        ListNode Reverse(ListNode head)
        {
            ListNode pre = null;
            var current = head;
            while (current != null)
            {
                var temp = current.next;
                current.next = pre;
                pre = current;
                current = temp;
            }
            return pre;
        }

        void Merge(ListNode head1, ListNode head2)
        {
            var head = new ListNode(0);
            var count = 0;
            while (head1 != null && head2 != null)
            {
                if (count % 2 == 0)
                {
                    head.next = head1;
                    head1 = head1.next;
                }
                else
                {
                    head.next = head2;
                    head2 = head2.next;
                }
                head = head.next;
                count++;
            }
            if (head1 != null) head.next = head1; 
            else head.next = head2;
        }
    }

    //public void ReorderList(ListNode head)
    //{
    //    if (head == null || head.next == null || head.next.next == null) return;

    //    var parent = head.next;
    //    var child = parent.next;

    //    while( child.next != null)
    //    {
    //        parent = child;
    //        child = child.next;
    //    }

    //    child.next = head.next;
    //    head.next = child;
    //    parent.next = null;

    //    ReorderList(head.next.next);
    //}
}

/*
// 别人做的答案，包含：快慢指针找链表中点，双指针反转链表等技巧。
public class Solution
{
    public void ReorderList(ListNode head)
    {
        if (head == null || head.next == null) return;
        var mid = findMiddle(head);
        var tail = reverse(mid.next);
        mid.next = null;
        merge(head, tail);
    }

    private ListNode findMiddle(ListNode head)
    {
        var fast = head.next;
        var slow = head;
        while (fast != null && fast.next != null)
        {
            fast = fast.next.next;
            slow = slow.next;
        }
        return slow;
    }

    private ListNode reverse(ListNode head)
    {
        ListNode pre = null;
        var curt = head;
        while (curt != null)
        {
            var temp = curt.next;
            curt.next = pre;
            pre = curt;
            curt = temp;
        }
        return pre;
    }

    private void merge(ListNode head1, ListNode head2)
    {
        var head = new ListNode(0);
        var count = 0;
        while (head1 != null && head2 != null)
        {
            if (count % 2 == 0)
            {
                head.next = head1;
                head1 = head1.next;
            }
            else
            {
                head.next = head2;
                head2 = head2.next;
            }
            head = head.next;
            count++;
        }
        if (head1 != null)
        {
            head.next = head1;
        }
        else
        {
            head.next = head2;
        }
    }
}



public class Solution {
    public void ReorderList(ListNode head) {
        var nodeList = new List<ListNode>();
        ListNode i = head;
        while(i!=null)
        {
            nodeList.Add(i);
            i = i.next;
        }
        ListNode dummyHead = new ListNode(-1);
        ListNode cur = dummyHead;
        int start = 0;
        int end = nodeList.Count - 1;
        while(start<end)
        {
            cur.next = nodeList[start];
            cur = cur.next;
            cur.next = nodeList[end];
            cur = cur.next;
            start++;
            end--;
        }

        if(start == end)
        {
            cur.next = nodeList[start];
            cur = cur.next;
        }
        cur.next = null;
    }
}

public class Solution {
    public void ReorderList(ListNode head) {
        if (head?.next == null) { return ; }
        var lengthOfNode = 1;
        var currenNode = head;
        while (currenNode.next != null)
        {
            lengthOfNode += 1;
            currenNode = currenNode.next;
        }

        var middle = lengthOfNode / 2;
        var endNodeOfLeft = head;
        for (var i = 0; i < middle-1 ; i++)
        {
            endNodeOfLeft = endNodeOfLeft.next;
        }

        var startNodeOfRight = endNodeOfLeft.next;
        endNodeOfLeft.next = null;

        startNodeOfRight = ReverseNode(startNodeOfRight);
        head = MergeTwoNode(head, startNodeOfRight);
    }

    private ListNode MergeTwoNode(ListNode l1, ListNode l2)
    {
        if (l1 == null || l2 == null) { return l1 ?? l2; }

        var currentNodeOfL1 = l1;
        var currentNodeOfL2 = l2;
        var headOfResult = new ListNode(0);
        var currentNodeOfResult = headOfResult;
        while (currentNodeOfL1 != null && currentNodeOfL2 != null)
        {
            currentNodeOfResult.next = currentNodeOfL1;
            currentNodeOfL1 = currentNodeOfL1.next;
            currentNodeOfResult = currentNodeOfResult.next;
            currentNodeOfResult.next = currentNodeOfL2;
            currentNodeOfL2 = currentNodeOfL2.next;
            currentNodeOfResult = currentNodeOfResult.next;
        }

        currentNodeOfResult.next = currentNodeOfL1 ?? currentNodeOfL2;
        return headOfResult.next;
    }

    private ListNode ReverseNode(ListNode head)
    {
        if (head?.next == null) { return head; }

        ListNode previousNode = null;
        ListNode nextNode = null;
        var currentNode = head;
        while (currentNode != null)
        {
            nextNode = currentNode.next;
            currentNode.next = previousNode;
            previousNode = currentNode;
            currentNode = nextNode;
        }

        return previousNode;
    }
}

public class Solution {
    public void ReorderList(ListNode head)
        {
            ListNode newHead = new ListNode(0) { next = head };
            ListNode p1 = newHead, p2 = newHead;
            while (p2 != null && p2.next != null)
            {
                p2 = p2.next.next;
                p1 = p1.next;
            }

            p2 = Reverse(p1.next);
            p1.next = null;
            p1 = head;

            while(p2 != null)
            {
                ListNode t = p2;
                p2 = p2.next;
                t.next = p1.next;
                p1.next = t;
                p1 = t.next;
            }
        }

        public ListNode Reverse(ListNode node)
        {
            if (node == null)
                return null;
            ListNode head = node;
            while(node.next != null)
            {
                ListNode t = node.next;
                node.next = t.next;
                t.next = head;
                head = t;
            }
            return head;
        }
}

public class Solution {
    public void ReorderList(ListNode head) {
                    if (head == null) return;
            Stack<ListNode> s = new Stack<ListNode>();
            int cnt = 0;
            var node = head;
            while(node!=null)
            {
                s.Push(node);
                node = node.next;
                cnt++;
            }
            cnt /= 2;
            var h = head;
            var t = s.Pop();
            while(cnt > 0)
            {
                var tmp = h.next;
                h.next = t;
                t.next = tmp;
                h = tmp;
                t = s.Pop();
                cnt--;
            }
            h.next = null;
    }
}

*/
