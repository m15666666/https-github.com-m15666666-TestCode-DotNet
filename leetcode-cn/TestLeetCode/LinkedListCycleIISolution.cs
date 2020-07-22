/*
给定一个链表，返回链表开始入环的第一个节点。 如果链表无环，则返回 null。

为了表示给定链表中的环，我们使用整数 pos 来表示链表尾连接到链表中的位置（索引从 0 开始）。 如果 pos 是 -1，则在该链表中没有环。

说明：不允许修改给定的链表。

 

示例 1：

输入：head = [3,2,0,-4], pos = 1
输出：tail connects to node index 1
解释：链表中有一个环，其尾部连接到第二个节点。

示例 2：

输入：head = [1,2], pos = 0
输出：tail connects to node index 0
解释：链表中有一个环，其尾部连接到第一个节点。

示例 3：

输入：head = [1], pos = -1
输出：no cycle
解释：链表中没有环。

 

进阶：
你是否可以不用额外空间解决此题？

*/

/// <summary>
/// https://leetcode-cn.com/problems/linked-list-cycle-ii/
/// 142. 环形链表 II
/// 给定一个链表，返回链表开始入环的第一个节点。 如果链表无环，则返回 null。
/// 说明：不允许修改给定的链表。
/// 进阶：
/// 你是否可以不用额外空间解决此题？
/// https://blog.csdn.net/huan_chen/article/details/80065238
/// </summary>
internal class LinkedListCycleIISolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public ListNode DetectCycle(ListNode head)
    {
        if (head == null) return null;

        ListNode intersect = GetIntersect(head);
        if (intersect == null) return null;

        ListNode ptr1 = head;
        ListNode ptr2 = intersect;
        while (ptr1 != ptr2)
        {
            ptr1 = ptr1.next;
            ptr2 = ptr2.next;
        }

        return ptr1;

        ListNode GetIntersect(ListNode head)
        {
            ListNode tortoise = head;
            ListNode hare = head;

            while (hare != null && hare.next != null)
            {
                tortoise = tortoise.next;
                hare = hare.next.next;
                if (tortoise == hare) return tortoise;
            }

            return null;
        }
    }

    //public ListNode DetectCycle(ListNode head)
    //{
    //    if (head == null) return null;

    //    HashSet<ListNode> set = new HashSet<ListNode>();
    //    while( head != null)
    //    {
    //        if (set.Contains(head)) return head;
    //        set.Add(head);
    //        head = head.next;
    //    }

    //    return null;
    //}
}
/*

环形链表 II
力扣 (LeetCode)
发布于 2019-06-10
64.6k
方法 1：哈希表
想法

如果我们用一个 Set 保存已经访问过的节点，我们可以遍历整个列表并返回第一个出现重复的节点。

算法

首先，我们分配一个 Set 去保存所有的列表节点。我们逐一遍历列表，检查当前节点是否出现过，如果节点已经出现过，那么一定形成了环且它是环的入口。否则如果有其他点是环的入口，我们应该先访问到其他节点而不是这个节点。其他情况，没有成环则直接返回 null 。

算法会在遍历有限个节点后终止，这是因为输入列表会被分成两类：成环的和不成环的。一个不成欢的列表在遍历完所有节点后会到达 null - 即链表的最后一个元素后停止。一个成环列表可以想象成是一个不成环列表将最后一个 null 元素换成环的入口。

如果 while 循环终止，我们返回 null 因为我们已经将所有的节点遍历了一遍且没有遇到重复的节点，这种情况下，列表是不成环的。对于循环列表， while 循环永远不会停止，但在某个节点上， if 条件会被满足并导致函数的退出。


public class Solution {
    public ListNode detectCycle(ListNode head) {
        Set<ListNode> visited = new HashSet<ListNode>();

        ListNode node = head;
        while (node != null) {
            if (visited.contains(node)) {
                return node;
            }
            visited.add(node);
            node = node.next;
        }

        return null;
    }
}
复杂度分析

时间复杂度：O(n)O(n)

不管是成环还是不成环的输入，算法肯定都只会访问每个节点一次。对于非成环列表这是显而易见的，因为第 nn 个节点指向 null ，这会让循环退出。对于循环列表， if 条件满足时会导致函数的退出，因为它指向了某个已经访问过的节点。两种情况下，访问的节点数最多都是 nn 个，所以运行时间跟节点数目成线性关系。

空间复杂度：O(n)O(n)

不管成环或者不成欢的输入，我们都需要将每个节点插入 Set 中一次。两者唯一的区别是最后访问的节点后是 null 还是一个已经访问过的节点。因此，由于 Set 包含 nn 个不同的节点，所需空间与节点数目也是线性关系的。



方法 2：Floyd 算法
想法

当然一个跑得快的人和一个跑得慢的人在一个圆形的赛道上赛跑，会发生什么？在某一个时刻，跑得快的人一定会从后面赶上跑得慢的人。

算法

Floyd 的算法被划分成两个不同的 阶段 。在第一阶段，找出列表中是否有环，如果没有环，可以直接返回 null 并退出。否则，用 相遇节点 来找到环的入口。

阶段 1

这里我们初始化两个指针 - 快指针和慢指针。我们每次移动慢指针一步、快指针两步，直到快指针无法继续往前移动。如果在某次移动后，快慢指针指向了同一个节点，我们就返回它。否则，我们继续，直到 while 循环终止且没有返回任何节点，这种情况说明没有成环，我们返回 null 。

下图说明了这个算法的工作方式：

image.png

环中的节点从 0 到 C-1C−1 编号，其中 CC 是环的长度。非环节点从 -F−F 到 -1−1 编号，其中 FF 是环以外节点的数目。 FF 次迭代以后，慢指针指向了 0 且快指针指向某个节点 hh ，其中 F \equiv h \pmod CF≡h(modC) 。这是因为快指针在 FF 次迭代中遍历了 2F2F 个节点，且恰好有 FF 个在环中。继续迭代 C-hC−h 次，慢指针显然指向第 C-hC−h 号节点，而快指针也会指向相同的节点。原因在于，快指针从 hh 号节点出发遍历了 2(C-h)2(C−h) 个节点。

\begin{aligned} h + 2(C-h) &= 2C - h \\ &\equiv C-h \pmod C \end{aligned}
h+2(C−h)
​	
  
=2C−h
≡C−h(modC)
​	
 

因此，如果列表是有环的，快指针和慢指针最后会同时指向同一个节点，因此被称为 相遇 。

阶段 2

给定阶段 1 找到的相遇点，阶段 2 将找到环的入口。首先我们初始化额外的两个指针： ptr1 ，指向链表的头， ptr2 指向相遇点。然后，我们每次将它们往前移动一步，直到它们相遇，它们相遇的点就是环的入口，返回这个节点。

下面的图将更好的帮助理解和证明这个方法的正确性。

image.png

我们利用已知的条件：慢指针移动 1 步，快指针移动 2 步，来说明它们相遇在环的入口处。（下面证明中的 tortoise 表示慢指针，hare 表示快指针）

\begin{aligned} 2 \cdot distance(tortoise) &= distance(hare) \\ 2(F+a) &= F+a+b+a \\ 2F+2a &= F+2a+b \\ F &= b \\ \end{aligned}
2⋅distance(tortoise)
2(F+a)
2F+2a
F
​	
  
=distance(hare)
=F+a+b+a
=F+2a+b
=b
​	
 

因为 F=bF=b ，指针从 hh 点出发和从链表的头出发，最后会遍历相同数目的节点后在环的入口处相遇。

下面的动画中动态地演示了整个算法过程：




public class Solution {
    private ListNode getIntersect(ListNode head) {
        ListNode tortoise = head;
        ListNode hare = head;

        // A fast pointer will either loop around a cycle and meet the slow
        // pointer or reach the `null` at the end of a non-cyclic list.
        while (hare != null && hare.next != null) {
            tortoise = tortoise.next;
            hare = hare.next.next;
            if (tortoise == hare) {
                return tortoise;
            }
        }

        return null;
}

    public ListNode detectCycle(ListNode head) {
        if (head == null) {
            return null;
        }

        // If there is a cycle, the fast/slow pointers will intersect at some
        // node. Otherwise, there is no cycle, so we cannot find an e***ance to
        // a cycle.
        ListNode intersect = getIntersect(head);
        if (intersect == null) {
            return null;
        }

        // To find the e***ance to the cycle, we have two pointers traverse at
        // the same speed -- one from the front of the list, and the other from
        // the point of intersection.
        ListNode ptr1 = head;
        ListNode ptr2 = intersect;
        while (ptr1 != ptr2) {
            ptr1 = ptr1.next;
            ptr2 = ptr2.next;
        }

        return ptr1;
    }
}
复杂度分析

时间复杂度：O(n)O(n)

对有环列表，快指针和慢指针在 F+C-hF+C−h 次迭代以后会指向同一个节点，正如上面正确性证明所示， F+C-h \leq F+C = nF+C−h≤F+C=n ，所以阶段 1 运行时间在 O(n)O(n) 时间以内，阶段 2 运行 F < nF<n 次迭代，所以它运行时间也在 O(n)O(n) 以内。

对于无环链表，快指针大约需要迭代 \dfrac{n}{2} 
2
n
​	
  次会抵达链表的尾部，这样不会进入阶段 2 就直接退出。

因此，不管是哪一类链表，都会在与节点数成线性关系的时间内运行完。

空间复杂度：O(1)O(1)

Floyd 的快慢指针算法仅需要几个指针，所以只需常数级别的额外空间。

下一篇：环形链表 II（双指针法，清晰图解）

public class Solution {
    public ListNode DetectCycle(ListNode head)
    {
        if (head == null) return null;

        bool hasCycle = false;
        ListNode fast = head;
        ListNode slow = head;
        while (fast != null && fast.next != null)
        {
            fast = fast.next.next;
            slow = slow.next;
            if (fast == slow)
            {
                fast = head;
                hasCycle = true;
                break;
            }
        }

        if (!hasCycle) return null;

        while (fast != slow)
        {
            fast = fast.next;
            slow = slow.next;
        }

        return fast;
    }
}

 
 
*/