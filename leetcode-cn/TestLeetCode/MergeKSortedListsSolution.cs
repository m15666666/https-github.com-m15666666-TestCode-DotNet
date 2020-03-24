using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
合并 k 个排序链表，返回合并后的排序链表。请分析和描述算法的复杂度。

示例:

输入:
[
  1->4->5,
  1->3->4,
  2->6
]
输出: 1->1->2->3->4->4->5->6
*/
/// <summary>
/// https://leetcode-cn.com/problems/merge-k-sorted-lists/
/// 23. 合并K个排序链表
/// 
/// 
/// </summary>
class MergeKSortedListsSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    public ListNode MergeKLists(ListNode[] lists)
    {
        PriorityQueue<int, ListNode> priorityQueue = new PriorityQueue<int, ListNode>();
        foreach (var list in lists)
            if (list != null) priorityQueue.Enqueue(list.val, list);

        ListNode root = new ListNode(0);
        var current = root;
        ListNode min;
        ListNode minNext;
        while( 0 < priorityQueue.Count)
        {
            min = priorityQueue.Dequeue();

            current.next = min;
            current = current.next;

            minNext = min.next;
            if (minNext != null) priorityQueue.Enqueue(minNext.val, minNext);
        }
        
        return root.next;
    }

    // 优先队列
    public class PriorityQueue<KeyType, ValueType>
    {
        private readonly SortedDictionary<KeyType, Queue<ValueType>> _map = new SortedDictionary<KeyType, Queue<ValueType>>();
        public int Count { get; private set; } = 0;
        
        public void Enqueue(KeyType key, ValueType value)
        {
            if (!_map.ContainsKey(key)) _map.Add(key, new Queue<ValueType>());
            _map[key].Enqueue(value);
            Count++;
        }

        public ValueType Dequeue()
        {
            if (Count < 1) return default;

            var pair = _map.First();
            var queue = pair.Value;
            var ret = queue.Dequeue();
            if (queue.Count == 0) _map.Remove(pair.Key);
            Count--;
            return ret;
        }
    }
}
/*

合并K个排序链表
力扣 (LeetCode)
发布于 10 个月前
60.7k
方法 1：暴力
想法 & 算法

遍历所有链表，将所有节点的值放到一个数组中。
将这个数组排序，然后遍历所有元素得到正确顺序的值。
用遍历得到的值，创建一个新的有序链表。
关于排序，你可以参考 这里 获得更多关于排序算法的内容。

class Solution(object):
    def mergeKLists(self, lists):
        """
        :type lists: List[ListNode]
        :rtype: ListNode
        """
        self.nodes = []
        head = point = ListNode(0)
        for l in lists:
            while l:
                self.nodes.append(l.val)
                l = l.next
        for x in sorted(self.nodes):
            point.next = ListNode(x)
            point = point.next
        return head.next
复杂度分析

时间复杂度：O(N\log N)O(NlogN) ，其中 NN 是节点的总数目。

遍历所有的值需花费 O(N)O(N) 的时间。
一个稳定的排序算法花费 O(N\log N)O(NlogN) 的时间。
遍历同时创建新的有序链表花费 O(N)O(N) 的时间。
空间复杂度：O(N)O(N) 。

排序花费 O(N)O(N) 空间（这取决于你选择的算法）。
创建一个新的链表花费 O(N)O(N) 的空间。
方法 2：逐一比较
算法

比较 \text{k}k 个节点（每个链表的首节点），获得最小值的节点。
将选中的节点接在最终有序链表的后面。
复杂度分析

时间复杂度： O(kN)O(kN) ，其中 \text{k}k 是链表的数目。

几乎最终有序链表中每个节点的时间开销都为 O(k)O(k) （\text{k-1}k-1 次比较）。
总共有 NN 个节点在最后的链表中。
空间复杂度：

O(n)O(n) 。创建一个新的链表空间开销为 O(n)O(n) 。
O(1)O(1) 。重复利用原来的链表节点，每次选择节点时将它直接接在最后返回的链表后面，而不是创建一个新的节点。
方法 3：用优先队列优化方法 2
算法

几乎与上述方法一样，除了将 比较环节 用 优先队列 进行了优化。你可以参考 这里 获取更多信息。

from Queue import PriorityQueue

class Solution(object):
    def mergeKLists(self, lists):
        """
        :type lists: List[ListNode]
        :rtype: ListNode
        """
        head = point = ListNode(0)
        q = PriorityQueue()
        for l in lists:
            if l:
                q.put((l.val, l))
        while not q.empty():
            val, node = q.get()
            point.next = ListNode(val)
            point = point.next
            node = node.next
            if node:
                q.put((node.val, node))
        return head.next
复杂度分析

时间复杂度： O(N\log k)O(Nlogk) ，其中 \text{k}k 是链表的数目。

弹出操作时，比较操作的代价会被优化到 O(\log k)O(logk) 。同时，找到最小值节点的时间开销仅仅为 O(1)O(1)。
最后的链表中总共有 NN 个节点。
空间复杂度：

O(n)O(n) 。创造一个新的链表需要 O(n)O(n) 的开销。
O(k)O(k) 。以上代码采用了重复利用原有节点，所以只要 O(1)O(1) 的空间。同时优先队列（通常用堆实现）需要 O(k)O(k) 的空间（远比大多数情况的 NN要小）。
方法 4：逐一两两合并链表
算法

将合并 \text{k}k 个链表的问题转化成合并 2 个链表 \text{k-1}k-1 次。这里是 合并两个有序链表 的题目。

复杂度分析

时间复杂度： O(kN)O(kN) ，其中 \text{k}k 是链表的数目。

我们可以在 O(n)O(n) 的时间内合并两个有序链表，其中 nn 是两个链表的总长度。
把所有合并过程所需的时间加起来，我们可以得到： O(\sum_{i=1}^{k-1} (i*(\frac{N}{k}) + \frac{N}{k})) = O(kN)O(∑ 
i=1
k−1
​	
 (i∗( 
k
N
​	
 )+ 
k
N
​	
 ))=O(kN) 。
空间复杂度：O(1)O(1)

我们可以在 O(1)O(1) 空间内合并两个有序链表。
方法 5：分治
想法 & 算法

这个方法沿用了上面的解法，但是进行了较大的优化。我们不需要对大部分节点重复遍历多次。

将 \text{k}k 个链表配对并将同一对中的链表合并。
第一轮合并以后， \text{k}k 个链表被合并成了 \frac{k}{2} 
2
k
​	
  个链表，平均长度为 \frac{2N}{k} 
k
2N
​	
  ，然后是 \frac{k}{4} 
4
k
​	
  个链表， \frac{k}{8} 
8
k
​	
  个链表等等。
重复这一过程，直到我们得到了最终的有序链表。
因此，我们在每一次配对合并的过程中都会遍历几乎全部 NN 个节点，并重复这一过程 \log_2Klog 
2
​	
 K 次。

image.png

class Solution(object):
    def mergeKLists(self, lists):
        """
        :type lists: List[ListNode]
        :rtype: ListNode
        """
        amount = len(lists)
        interval = 1
        while interval < amount:
            for i in range(0, amount - interval, interval * 2):
                lists[i] = self.merge2Lists(lists[i], lists[i + interval])
            interval *= 2
        return lists[0] if amount > 0 else lists

    def merge2Lists(self, l1, l2):
        head = point = ListNode(0)
        while l1 and l2:
            if l1.val <= l2.val:
                point.next = l1
                l1 = l1.next
            else:
                point.next = l2
                l2 = l1
                l1 = point.next.next
            point = point.next
        if not l1:
            point.next=l2
        else:
            point.next=l1
        return head.next
复杂度分析

时间复杂度： O(N\log k)O(Nlogk) ，其中 \text{k}k 是链表的数目。

我们可以在 O(n)O(n) 的时间内合并两个有序链表，其中 nn 是两个链表中的总节点数。
将所有的合并进程加起来，我们可以得到：O\big(\sum_{i=1}^{log_2k}N \big)= O(N\log k)O(∑ 
i=1
log 
2
​	
 k
​	
 N)=O(Nlogk) 。
空间复杂度：O(1)O(1)

我们可以用 O(1)O(1) 的空间实现两个有序链表的合并。
下一篇：合并 K 个排序链表


public class Solution {
   public ListNode MergeKLists(ListNode[] lists) {
        if(lists==null||lists.Length==0)
        return null;
        return Merge(lists,0,lists.Length-1);
    }

    public ListNode Merge(ListNode[] lists,int start,int end)
    {
        if (start>=end)
        {
            return lists[start];
        }
 
        int mid = start+(end-start)/2;
        ListNode l1 = Merge(lists,start,mid);
        ListNode l2 = Merge(lists,mid+1,end);
        return mergeTwoLists(l1,l2);
    }
    public ListNode mergeTwoLists(ListNode l1, ListNode l2) {
        ListNode dummyNode = new ListNode(-1);
        ListNode pre = dummyNode;
        if(l1==null)
        {
            return l2;
        }
        if(l2==null)
        {
            return l1;
        }
        while(l1!=null&&l2!=null)
        {
            if(l1.val>=l2.val)
        {
            pre.next = l2;
            l2 = l2.next;
        }
        else
        {
            pre.next= l1;
            l1 = l1.next;
        }
        pre = pre.next;
        }
        
        if(l1!=null)
        {
            pre.next = l1;
        }
        if(l2!=null)
        {
            pre.next = l2;
        }
        return dummyNode.next;
    }
}

*/
