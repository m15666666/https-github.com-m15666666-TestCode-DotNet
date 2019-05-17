using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
您将获得一个双向链表，除了下一个和前一个指针之外，它还有一个子指针，可能指向单独的双向链表。这些子列表可能有一个或多个自己的子项，依此类推，生成多级数据结构，如下面的示例所示。

扁平化列表，使所有结点出现在单级双链表中。您将获得列表第一级的头部。

 

示例:

输入:
 1---2---3---4---5---6--NULL
         |
         7---8---9---10--NULL
             |
             11--12--NULL

输出:
1-2-3-7-8-11-12-9-10-4-5-6-NULL
 

以上示例的说明:

给出以下多级双向链表:


 

我们应该返回如下所示的扁平双向链表:
 
*/
/// <summary>
/// https://leetcode-cn.com/problems/flatten-a-multilevel-doubly-linked-list/
/// 430. 扁平化多级双向链表
/// https://www.cnblogs.com/hlk09/p/9565674.html
/// </summary>
class FlattenAMultilevelDoublyLinkedListSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public Node Flatten(Node head)
    {
        if (head == null || (head.next == null && head.child == null)) return head;

        if (head.child == null)
            head.next = Flatten(head.next);
        else
        {
            Node n = Flatten(head.next);
            Node c = head.child;
            head.child = null;
            c = Flatten(c);
            c.prev = head;
            head.next = c;
            Node p = c;
            while ( c != null && c.next != null )
                c = c.next;

            c.next = n;
            if ( n != null ) n.prev = c;
        }
        return head;


        //if (head == null || (head.next == null && head.child == null) ) return head;

        //Stack<Node> stack = new Stack<Node>();

        //Node current = head;

        //if (current.next != null) stack.Push(current.next);
        //if (current.child != null) stack.Push(current.child);

        //while( 0 < stack.Count )
        //{
        //    var n = stack.Pop();
        //    if (n.next != null) stack.Push(n.next);
        //    if (n.child != null) stack.Push(n.child);

        //    current.next = n;
        //    n.prev = current;
        //    n.next = null;
        //    n.child = null;
        //    current = n;
        //}

        //return head;
    }

    // Definition for a Node.
    public class Node
    {
        public int val;
        public Node prev;
        public Node next;
        public Node child;

        public Node() { }
        public Node(int _val, Node _prev, Node _next, Node _child)
        {
            val = _val;
            prev = _prev;
            next = _next;
            child = _child;
        }
    }
}
/*
public class Solution {
    Node t;
    Stack<Node> s= new Stack<Node>();
    public Node Flatten(Node head) {
        t=head;
        GetNode(head);
        while(s.Count>0)
        {
            Node n = s.Pop();
            t.next = n;
            n.prev = t;
            t=t.next;
            GetNode(n);
        }
        return head;
    }
    
    public void GetNode(Node head)
    {
        if(head==null)return;
        if(head.child!=null)
        {
            if(head.next!=null){s.Push(head.next);}
            head.next=head.child;
            head.child.prev=head;
            head.child=null;
        }
        if(t.next!=null)t=t.next;
        GetNode(head.next);
    }
}
public class Solution {
    public Node Flatten(Node head) {
        Stack<Node> stack=new Stack<Node>();
        Node cur=head;
        while(cur!=null || stack.Count!=0)
        {
            if(cur.child!=null){
                if(cur.next!=null)
                {
                    stack.Push(cur.next);
                }
                cur.next=cur.child;
                cur.child.prev=cur;
                cur.child=null;
                cur=cur.next;
            }
            else{
                if(cur.next!=null)
                    cur=cur.next;
                else if(stack.Count!=0)
                {
                    cur.next=stack.Pop();
                    cur.next.prev=cur;
                    cur=cur.next;
                }
                else
                    break;
            }
        }
        return head;
    }
} 
*/
