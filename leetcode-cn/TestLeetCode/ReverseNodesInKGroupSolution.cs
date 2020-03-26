using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给你一个链表，每 k 个节点一组进行翻转，请你返回翻转后的链表。

k 是一个正整数，它的值小于或等于链表的长度。

如果节点总数不是 k 的整数倍，那么请将最后剩余的节点保持原有顺序。

 

示例：

给你这个链表：1->2->3->4->5

当 k = 2 时，应当返回: 2->1->4->3->5

当 k = 3 时，应当返回: 3->2->1->4->5

说明：

你的算法只能使用常数的额外空间。
你不能只是单纯的改变节点内部的值，而是需要实际进行节点交换。
*/
/// <summary>
/// https://leetcode-cn.com/problems/reverse-nodes-in-k-group/
/// 25. K 个一组翻转链表
/// 
/// 
/// </summary>
class ReverseNodesInKGroupSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public ListNode ReverseKGroup(ListNode head, int k)
    {
        if (head == null || head.next == null || k < 2) return head;

        var root = new ListNode(0)
        {
            next = head
        };
        var previous = root;
        while (true) 
        {
            var tail = previous;
            int count = k;
            while (0 < count && tail != null) 
            {
                count--;
                tail = tail.next;
            }
            if (tail == null) break;

            var groupHead = previous.next;
            ListNode currentHead;
            //while (previous.next != tail) 
            for( int i = 1; i < k; i++) // 移动k-1个节点
            {
                currentHead = previous.next;
                previous.next = currentHead.next;
                currentHead.next = tail.next;
                tail.next = currentHead;
            }
            previous = groupHead;
        }
        return root.next;
    }
}
/*

k 个一组翻转链表
powcai
发布于 1 年前
14.4k
解题思路：
思路一：
用栈，我们把 k 个数压入栈中，然后弹出来的顺序就是翻转的！

这里要注意几个问题：

第一，剩下的链表个数够不够 k 个（因为不够 k 个不用翻转）；

第二，已经翻转的部分要与剩下链表连接起来。

思路二：
尾插法。

直接举个例子：k = 3。

pre
tail    head
dummy    1     2     3     4     5
# 我们用tail 移到要翻转的部分最后一个元素
pre     head       tail
dummy    1     2     3     4     5
	       cur
# 我们尾插法的意思就是,依次把cur移到tail后面
pre          tail  head
dummy    2     3    1     4     5
	       cur
# 依次类推
pre     tail      head
dummy    3     2    1     4     5
		cur
....
思路三：
递归

代码 1：
栈

# Definition for singly-linked list.
# class ListNode:
#     def __init__(self, x):
#         self.val = x
#         self.next = None

class Solution:
    def reverseKGroup(self, head: ListNode, k: int) -> ListNode:
        dummy = ListNode(0)
        p = dummy
        while True:
            count = k 
            stack = []
            tmp = head
            while count and tmp:
                stack.append(tmp)
                tmp = tmp.next
                count -= 1
            # 注意,目前tmp所在k+1位置
            # 说明剩下的链表不够k个,跳出循环
            if count : 
                p.next = head
                break
            # 翻转操作
            while stack:
                p.next = stack.pop()
                p = p.next
            #与剩下链表连接起来 
            p.next = tmp
            head = tmp
        
        return dummy.next
代码 2：
尾插法

# Definition for singly-linked list.
# class ListNode:
#     def __init__(self, x):
#         self.val = x
#         self.next = None

class Solution:
    def reverseKGroup(self, head: ListNode, k: int) -> ListNode:
        dummy = ListNode(0)
        dummy.next = head
        pre = dummy
        tail = dummy
        while True:
            count = k
            while count and tail:
                count -= 1
                tail = tail.next
            if not tail: break
            head = pre.next
            while pre.next != tail:
                cur = pre.next # 获取下一个元素
                # pre与cur.next连接起来,此时cur(孤单)掉了出来
                pre.next = cur.next 
                cur.next = tail.next # 和剩余的链表连接起来
                tail.next = cur #插在tail后面
            # 改变 pre tail 的值
            pre = head 
            tail = head
        return dummy.next
代码 3：
递归

# Definition for singly-linked list.
# class ListNode:
#     def __init__(self, x):
#         self.val = x
#         self.next = None

class Solution:
    def reverseKGroup(self, head: ListNode, k: int) -> ListNode:
        cur = head
        count = 0
        while cur and count!= k:
            cur = cur.next
            count += 1
        if count == k:
            cur = self.reverseKGroup(cur, k)
            while count:
                tmp = head.next
                head.next = cur
                cur = head
                head = tmp
                count -= 1
            head = cur   
        return head
下一篇：递归是神！迭代是人！

public ListNode reverseKGroup(ListNode head, int k) {
    ListNode dummy = new ListNode(0);
    dummy.next = head;

    ListNode pre = dummy;
    ListNode end = dummy;

    while (end.next != null) {
        for (int i = 0; i < k && end != null; i++) end = end.next;
        if (end == null) break;
        ListNode start = pre.next;
        ListNode next = end.next;
        end.next = null;
        pre.next = reverse(start);
        start.next = next;
        pre = start;

        end = pre;
    }
    return dummy.next;
}

private ListNode reverse(ListNode head) {
    ListNode pre = null;
    ListNode curr = head;
    while (curr != null) {
        ListNode next = curr.next;
        curr.next = pre;
        pre = curr;
        curr = next;
    }
    return pre;
}

public class Solution {
    public ListNode ReverseKGroup(ListNode head, int k) {
        
        if(k>1)
        {
            ListNode p=head;        
			ListNode[] list=new ListNode[k];
		   
			if(TryReverseK(p,k,list))
			{
				head=list[k-1];
				ListNode before=list[0];
				p=before.next;
			
				while(TryReverseK(p,k,list))
				{
				   before.next=list[k-1];
				   before=list[0];
				   p=before.next;
				}
			}
        }
        return head;
    }

    public bool TryReverseK(ListNode p, int k,ListNode[] list)
    {
        int count=0;
        while(p!=null&&count<k)
        {
            list[count++]=p;
            p=p.next;
        }
        
        if(count==k)
        {
            p=list[k-1];
            ListNode q=p.next;
            for(int i=k-2;i>=0;i--)
            {
                p.next=list[i];
                p=p.next;
            }
            p.next=q;
            return true;
        }
        return false;
    }
}

public class Solution {
    public ListNode ReverseKGroup(ListNode head, int k) {
		if (head.next == null || head == null || k == 1)
                return head;

            int length = 0;
            ListNode r = head;
            while(r != null)
            {
                length++;
                r = r.next;
            }
            int steps = length / k;


            ListNode result = null;

            ListNode prev = null;
            ListNode curr = head;
            ListNode next = head.next;
            ListNode subHead = null;
            ListNode subHail = null;
            ListNode temp = null;

            for(int step = 0; step < steps; step++)
            {
                
                temp = subHail;
                subHail = curr;
                for (int i = 0; i < k; i++)
                {
                    next = curr.next;
                    curr.next = prev;
                    prev = curr;
                    curr = next;
                }
                subHead = prev;
                if (step == 0)
                {
                    result = subHead;
                }
                else
                {
                    temp.next = subHead;
                }
                
            }
            subHail.next = curr;

            return result;
    }
}

public class Solution {
    public ListNode ReverseKGroup(ListNode head, int k) {
        if (k == 1) {
            return head;
        }
        Stack<ListNode> stack = new Stack<ListNode>();
        ListNode pre = new ListNode(-1);
        ListNode node = pre;
        ListNode cur = head, first = null;
        while (cur != null) {
            // 找到翻转的起始点
            if (stack.Count == 0) {
                first = cur;
            }
            stack.Push(cur);
            cur = cur.next;
            if (stack.Count == k) {
                // -1->3->2->1->2  node = 1
                while (stack.Count != 0) {
                    node.next = stack.Pop();
                    node = node.next;
                }
                first = null;
            }
        }
        // first: 4->5 || null
        node.next = first;
        return pre.next;
    }
}

public class Solution {
        public ListNode ReverseKGroup(ListNode head, int k)
        {
            if (head == null)
            {
                return null;
            }

            var a = head;
            var b = head;

            for (int i = 0; i < k; i++)
            {
                if (b == null)
                {
                    return head;
                }
                b = b.next;
            }

            var dummy = Reverse(a, b);

            a.next = ReverseKGroup(b, k);

            return dummy;
        }
        public ListNode Reverse(ListNode a, ListNode b)
        {
            ListNode previous = null;
            var current = a;
            var next = a;

            while (current != b)
            {
                next = current.next;

                current.next = previous;
                previous = current;
                current = next;
            }

            return previous;
        }
}

public class Solution {
    public ListNode ReverseKGroup(ListNode head, int k)
    {
        var dummy = new ListNode(0);
        dummy.next = head;
        var prev = dummy;
        var next = prev;
        while(prev != null)
        {
            var start = prev.next;
            var end = start;
            for (int i = 1; i < k && end != null; i++)
            {
                end = end.next;
            }
            if(end == null)
            {
                break;
            }
            next = end.next;
            end.next = null;
            Reverse(start);
            start.next = next;
            prev.next = end;
            prev = start;
        }
        return dummy.next;
    }

    private void Reverse(ListNode node)
    {
        ListNode prev = null;
        while(node != null)
        {
            var temp = node.next;
            node.next = prev;
            prev = node;
            node = temp;
        }
    }
}

public class Solution {
    public ListNode ReverseKGroup(ListNode head, int k)
    {
        List<ListNode> list = new List<ListNode>();
        ListNode p = head;
        for (int i = 0; i < k; i++)
        {
            if (head == null) { return p; }
            list.Add(head);
            head = head.next;
           
        }
        for (int i = 1; i < k; i++)
        {
            list[i].next = list[i - 1];
        }
        list[0].next = ReverseKGroup(head, k);
        return list[k - 1];
    }
}  

 
*/
