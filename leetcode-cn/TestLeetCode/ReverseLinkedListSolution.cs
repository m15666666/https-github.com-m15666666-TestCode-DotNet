/*
反转一个单链表。

示例:

输入: 1->2->3->4->5->NULL
输出: 5->4->3->2->1->NULL
进阶:
你可以迭代或递归地反转链表。你能否用两种方法解决这道题？

*/

/// <summary>
/// https://leetcode-cn.com/problems/reverse-linked-list/
///
///
/// </summary>
internal class ReverseLinkedListSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public ListNode ReverseList(ListNode head)
    {
        ListNode root = new ListNode(0);
        ListNode curr = head;
        while (curr != null)
        {
            ListNode next = curr.next;
            curr.next = root.next;
            root.next = curr;
            curr = next;
        }
        return root.next;
    }
}

/*

反转链表
力扣 (LeetCode)
发布于 2019-03-30
152.3k
方法一：迭代
假设存在链表 1 → 2 → 3 → Ø，我们想要把它改成 Ø ← 1 ← 2 ← 3。

在遍历列表时，将当前节点的 next 指针改为指向前一个元素。由于节点没有引用其上一个节点，因此必须事先存储其前一个元素。在更改引用之前，还需要另一个指针来存储下一个节点。不要忘记在最后返回新的头引用！


public ListNode reverseList(ListNode head) {
    ListNode prev = null;
    ListNode curr = head;
    while (curr != null) {
        ListNode nextTemp = curr.next;
        curr.next = prev;
        prev = curr;
        curr = nextTemp;
    }
    return prev;
}
复杂度分析

时间复杂度：O(n)O(n)，假设 nn 是列表的长度，时间复杂度是 O(n)O(n)。
空间复杂度：O(1)O(1)。
方法二：递归
递归版本稍微复杂一些，其关键在于反向工作。假设列表的其余部分已经被反转，现在我该如何反转它前面的部分？

假设列表为：

n_{1}\rightarrow ... \rightarrow n_{k-1} \rightarrow n_{k} \rightarrow n_{k+1} \rightarrow ... \rightarrow n_{m} \rightarrow \varnothing
n 
1
​	
 →...→n 
k−1
​	
 →n 
k
​	
 →n 
k+1
​	
 →...→n 
m
​	
 →∅

若从节点 n_{k+1}n 
k+1
​	
  到 n_{m}n 
m
​	
  已经被反转，而我们正处于 n_{k}n 
k
​	
 。

n_{1}\rightarrow ... \rightarrow n_{k-1} \rightarrow n_{k} \rightarrow n_{k+1} \leftarrow ... \leftarrow n_{m}
n 
1
​	
 →...→n 
k−1
​	
 →n 
k
​	
 →n 
k+1
​	
 ←...←n 
m
​	
 

我们希望 n_{k+1}n 
k+1
​	
  的下一个节点指向 n_{k}n 
k
​	
 。

所以，n_{k}n 
k
​	
 .next.next = n_{k}n 
k
​	
 。

要小心的是 n_{1}n 
1
​	
  的下一个必须指向 Ø 。如果你忽略了这一点，你的链表中可能会产生循环。如果使用大小为 2 的链表测试代码，则可能会捕获此错误。


public ListNode reverseList(ListNode head) {
    if (head == null || head.next == null) return head;
    ListNode p = reverseList(head.next);
    head.next.next = head;
    head.next = null;
    return p;
}
复杂度分析

时间复杂度：O(n)O(n)，假设 nn 是列表的长度，那么时间复杂度为 O(n)O(n)。
空间复杂度：O(n)O(n)，由于使用递归，将会使用隐式栈空间。递归深度可能会达到 nn 层。

public class Solution {
    public ListNode ReverseList(ListNode head) {
        if (head == null || head.next == null) {
            return head;
        }
        var cur = head;
        ListNode pre = null;
        while (cur != null) {
            var next = cur.next;
            cur.next = pre;
            pre = cur;
            cur = next;
        }
        return pre;
    }
}

public class Solution {
    public ListNode ReverseList(ListNode head) {
        ListNode p=head;
        if(p==null) return null;
        while(p.next!=null)
            p=p.next;
        ReverseOneByOne(head);
        head.next=null;
        return p;
    }
    private ListNode ReverseOneByOne(ListNode head){
        if(head.next==null) return head;
        ReverseOneByOne(head.next).next=head;
        return head;
    }
}


*/