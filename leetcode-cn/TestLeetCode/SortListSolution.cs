using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
在 O(n log n) 时间复杂度和常数级空间复杂度下，对链表进行排序。

示例 1:

输入: 4->2->1->3
输出: 1->2->3->4
示例 2:

输入: -1->5->3->4->0
输出: -1->0->3->4->5
通过次数77,464提交次数116,770
 
*/
/// <summary>
/// https://leetcode-cn.com/problems/sort-list/
/// 148. 排序链表
/// 在 O(n log n) 时间复杂度和常数级空间复杂度下，对链表进行排序。
/// </summary>
class SortListSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public ListNode SortList(ListNode head)
    {
        if (head == null || head.next == null) return head;
        
        ListNode fast = head.next, slow = head;
        while (fast != null && fast.next != null) {
            slow = slow.next;
            fast = fast.next.next;
        }

        ListNode right = slow.next;
        slow.next = null;
        ListNode left = SortList(head);
        right = SortList(right);
        ListNode h = new ListNode(0);
        ListNode ret = h;
        while (left != null && right != null) {
            if (left.val < right.val) {
                h.next = left;
                left = left.next;
            } else {
                h.next = right;
                right = right.next;
            }
            h = h.next;
        }
        h.next = left ?? right;
        return ret.next;
    }

    //public ListNode SortList(ListNode head)
    //{
    //    if (head == null || head.next == null) return head;

    //    ListNode root = new ListNode(int.MinValue);
    //    ListNode tail = root;
    //    ListNode middle = root;
    //    int count = 0;
    //    int midIndex = 0;

    //    var current = head;

    //    while (current != null)
    //    {
    //        count++;
    //        var v = current.val;
    //        var next = current.next;

    //        if( tail.val <= v)
    //        {
    //            tail.next = current;
    //            tail = current;
    //            tail.next = null;
    //            current = next;
    //            continue;
    //        }

    //        ListNode previous = null;
    //        var newCurrent = middle.val <= v ? middle : root;
    //        var newCurrentIndex = middle.val <= v ? midIndex : 0;
    //        while (newCurrent != null && newCurrent.val <= v)
    //        {
    //            if ( newCurrentIndex == count / 2 + 1 ) middle = newCurrent;
    //            previous = newCurrent;
    //            newCurrent = newCurrent.next;

    //            newCurrentIndex++;
    //        }

    //        previous.next = current;
    //        current.next = newCurrent;

    //        if (newCurrent == null) tail = current;
    //        else middle = current;

    //        current = next;
    //    }

    //    return root.next;
    //}

}

/*
 * 别人的解法1

    public ListNode SortList(ListNode head) {
        if(head == null || head.next == null) {
           return head;
        }
        return FindMiddle(head);
    }
    
    private ListNode FindMiddle(ListNode startNode) {
        if(startNode == null || startNode.next == null) {
           return startNode;
        }
        ListNode slowNode = startNode;
        ListNode fastNode = startNode.next;
        while(fastNode != null) {
            fastNode = fastNode.next;
            if(fastNode != null) {
                slowNode = slowNode.next;
                fastNode = fastNode.next; 
            }
        }
        ListNode middleNode = slowNode.next;
        slowNode.next = null;
        startNode = FindMiddle(startNode);
        middleNode = FindMiddle(middleNode);
        return Merge(startNode, middleNode);
    }
    
    private ListNode Merge(ListNode pNode, ListNode qNode) {
        if(pNode == null) {
            return qNode;
        }
        if(qNode == null) {
            return pNode;
        }
        ListNode resHead;
        if(pNode.val > qNode.val) {
            resHead = qNode;
            resHead.next = Merge(pNode, qNode.next);
        }
        else {
            resHead = pNode;
            resHead.next = Merge(qNode, pNode.next);
        }
        return resHead;
    }
 */

/*


Sort List （归并排序链表）
Krahets
发布于 2019-07-11
83.1k
解答一：归并排序（递归法）
题目要求时间空间复杂度分别为O(nlogn)O(nlogn)和O(1)O(1)，根据时间复杂度我们自然想到二分法，从而联想到归并排序；

对数组做归并排序的空间复杂度为 O(n)O(n)，分别由新开辟数组O(n)O(n)和递归函数调用O(logn)O(logn)组成，而根据链表特性：

数组额外空间：链表可以通过修改引用来更改节点顺序，无需像数组一样开辟额外空间；
递归额外空间：递归调用函数将带来O(logn)O(logn)的空间复杂度，因此若希望达到O(1)O(1)空间复杂度，则不能使用递归。
通过递归实现链表归并排序，有以下两个环节：

分割 cut 环节： 找到当前链表中点，并从中点将链表断开（以便在下次递归 cut 时，链表片段拥有正确边界）；
我们使用 fast,slow 快慢双指针法，奇数个节点找到中点，偶数个节点找到中心左边的节点。
找到中点 slow 后，执行 slow.next = None 将链表切断。
递归分割时，输入当前链表左端点 head 和中心节点 slow 的下一个节点 tmp(因为链表是从 slow 切断的)。
cut 递归终止条件： 当head.next == None时，说明只有一个节点了，直接返回此节点。
合并 merge 环节： 将两个排序链表合并，转化为一个排序链表。
双指针法合并，建立辅助ListNode h 作为头部。
设置两指针 left, right 分别指向两链表头部，比较两指针处节点值大小，由小到大加入合并链表头部，指针交替前进，直至添加完两个链表。
返回辅助ListNode h 作为头部的下个节点 h.next。
时间复杂度 O(l + r)，l, r 分别代表两个链表长度。
当题目输入的 head == None 时，直接返回None。
Picture2.png

class Solution {
    public ListNode sortList(ListNode head) {
        if (head == null || head.next == null)
            return head;
        ListNode fast = head.next, slow = head;
        while (fast != null && fast.next != null) {
            slow = slow.next;
            fast = fast.next.next;
        }
        ListNode tmp = slow.next;
        slow.next = null;
        ListNode left = sortList(head);
        ListNode right = sortList(tmp);
        ListNode h = new ListNode(0);
        ListNode res = h;
        while (left != null && right != null) {
            if (left.val < right.val) {
                h.next = left;
                left = left.next;
            } else {
                h.next = right;
                right = right.next;
            }
            h = h.next;
        }
        h.next = left != null ? left : right;
        return res.next;
    }
}


class Solution:
    def sortList(self, head: ListNode) -> ListNode:
        if not head or not head.next: return head # termination.
        # cut the LinkedList at the mid index.
        slow, fast = head, head.next
        while fast and fast.next:
            fast, slow = fast.next.next, slow.next
        mid, slow.next = slow.next, None # save and cut.
        # recursive for cutting.
        left, right = self.sortList(head), self.sortList(mid)
        # merge `left` and `right` linked list and return it.
        h = res = ListNode(0)
        while left and right:
            if left.val < right.val: h.next, left = left, left.next
            else: h.next, right = right, right.next
            h = h.next
        h.next = left if left else right
        return res.next
解答二：归并排序（从底至顶直接合并）
对于非递归的归并排序，需要使用迭代的方式替换cut环节：
我们知道，cut环节本质上是通过二分法得到链表最小节点单元，再通过多轮合并得到排序结果。
每一轮合并merge操作针对的单元都有固定长度intv，例如：
第一轮合并时intv = 1，即将整个链表切分为多个长度为1的单元，并按顺序两两排序合并，合并完成的已排序单元长度为2。
第二轮合并时intv = 2，即将整个链表切分为多个长度为2的单元，并按顺序两两排序合并，合并完成已排序单元长度为4。
以此类推，直到单元长度intv >= 链表长度，代表已经排序完成。
根据以上推论，我们可以仅根据intv计算每个单元边界，并完成链表的每轮排序合并，例如:
当intv = 1时，将链表第1和第2节点排序合并，第3和第4节点排序合并，……。
当intv = 2时，将链表第1-2和第3-4节点排序合并，第5-6和第7-8节点排序合并，……。
当intv = 4时，将链表第1-4和第5-8节点排序合并，第9-12和第13-16节点排序合并，……。
此方法时间复杂度O(nlogn)O(nlogn)，空间复杂度O(1)O(1)。
Picture1.png

模拟上述的多轮排序合并：
统计链表长度length，用于通过判断intv < length判定是否完成排序；
额外声明一个节点res，作为头部后面接整个链表，用于：
intv *= 2即切换到下一轮合并时，可通过res.next找到链表头部h；
执行排序合并时，需要一个辅助节点作为头部，而res则作为链表头部排序合并时的辅助头部pre；后面的合并排序可以将上次合并排序的尾部tail用做辅助节点。
在每轮intv下的合并流程：
根据intv找到合并单元1和单元2的头部h1, h2。由于链表长度可能不是2^n，需要考虑边界条件：
在找h2过程中，如果链表剩余元素个数少于intv，则无需合并环节，直接break，执行下一轮合并；
若h2存在，但以h2为头部的剩余元素个数少于intv，也执行合并环节，h2单元的长度为c2 = intv - i。
合并长度为c1, c2的h1, h2链表，其中：
合并完后，需要修改新的合并单元的尾部pre指针指向下一个合并单元头部h。（在寻找h1, h2环节中，h指针已经被移动到下一个单元头部）
合并单元尾部同时也作为下次合并的辅助头部pre。
当h == None，代表此轮intv合并完成，跳出。
每轮合并完成后将单元长度×2，切换到下轮合并：intv *= 2。

class Solution:
    def sortList(self, head: ListNode) -> ListNode:
        h, length, intv = head, 0, 1
        while h: h, length = h.next, length + 1
        res = ListNode(0)
        res.next = head
        # merge the list in different intv.
        while intv < length:
            pre, h = res, res.next
            while h:
                # get the two merge head `h1`, `h2`
                h1, i = h, intv
                while i and h: h, i = h.next, i - 1
                if i: break # no need to merge because the `h2` is None.
                h2, i = h, intv
                while i and h: h, i = h.next, i - 1
                c1, c2 = intv, intv - i # the `c2`: length of `h2` can be small than the `intv`.
                # merge the `h1` and `h2`.
                while c1 and c2:
                    if h1.val < h2.val: pre.next, h1, c1 = h1, h1.next, c1 - 1
                    else: pre.next, h2, c2 = h2, h2.next, c2 - 1
                    pre = pre.next
                pre.next = h1 if c1 else h2
                while c1 > 0 or c2 > 0: pre, c1, c2 = pre.next, c1 - 1, c2 - 1
                pre.next = h 
            intv *= 2
        return res.next
下一篇：Java代码详细注释 易懂 归并排序 自底向上非递归形式


public class Solution {
 public ListNode SortList(ListNode head)
        {
            if (head == null || head.next == null)
            {
                return head;
            }

            int len = ListNodeLength(head);

            ListNode dummy = new ListNode(-1);
            dummy.next = head;

            for (int i = 1; i < len; i *= 2)
            {
                ListNode tail = dummy;  //上一次完成合并后的链表的尾部
                ListNode cur = dummy.next;

                while (cur != null)
                {
                    ListNode left = cur;  
                    ListNode right = Split(left, i);// left->@->@ right->@->@->@...
                    cur = Split(right, i);  // left->@->@ right->@->@  cur->@->...
                    tail.next = MergeTwoLists(left, right);  //将tail与合并后的链表的头节点连接
                    while (tail.next != null)
                    {
                        //保持tail为尾部
                        tail = tail.next;
                    }
                }
            }

            return dummy.next;
        }

        /// <summary>
        /// 获取链表的长度
        /// </summary>
        private int ListNodeLength(ListNode head)
        {
            int length = 0;
            ListNode curr = head;

            while (curr != null)
            {
                length++;
                curr = curr.next;
            }

            return length;
        }

        /// <summary>
        /// 将head链表的前step个节点与后续节点分割开 返回分割后后续部分的链表头
        /// </summary>
        private ListNode Split(ListNode head, int step)
        {
            if (head == null) return null;

            for (int i = 1; head.next != null && i < step; i++)
            {
                head = head.next;
            }

            ListNode right = head.next;
            head.next = null;
            return right;
        }

        private ListNode MergeTwoLists(ListNode l1, ListNode l2)
        {
            ListNode prehead = new ListNode(-1);

            ListNode prev = prehead;
            while (l1 != null && l2 != null)
            {
                //两条链表谁的当前元素值小就让prev的next指向谁
                if (l1.val <= l2.val)
                {
                    prev.next = l1;
                    l1 = l1.next;
                }
                else
                {
                    prev.next = l2;
                    l2 = l2.next;
                }
                prev = prev.next;
            }

            // 合并后 l1 和 l2 最多只有一个还未被合并完，我们直接将链表末尾指向未合并完的链表即可
            prev.next = l1 == null ? l2 : l1;

            return prehead.next;
        }
}

public class Solution {
    public ListNode SortList(ListNode head) {
        if(head == null) return null;
        if(head.next == null) return head;
        ListNode dumHead = new ListNode(0), fast = dumHead, slow = dumHead;
        dumHead.next = head;
        while(fast != null && fast.next != null)
        {
            fast = fast.next.next;
            slow = slow.next;
        }
        ListNode head2 = slow.next;
        slow.next = null;
        return MergeSorted(SortList(dumHead.next), SortList(head2));
    }

    public ListNode MergeSorted(ListNode l1, ListNode l2)
    {
        if(l1 == null) return l2;
        ListNode head = new ListNode(0), cur = head;
        while(l1 != null && l2 != null)
        {
            if(l1.val <= l2.val)
            {
                cur.next = l1;
                l1 = l1.next;
            }
            else
            {
                cur.next = l2;
                l2 = l2.next;
            }
            cur = cur.next;
        }
        cur.next = l1 == null ? l2 : l1;
        return head.next;
    }
}

public class Solution {
	public ListNode SortList(ListNode head)
		{
			if (head != null && head.next != null)
			{
				ListNode newNode = Cut(head);
				ListNode n1 = SortList(newNode);
				ListNode n2 = SortList(head);
				return Merge(n1, n2);
			}
			return head;
		}

		ListNode Cut(ListNode head)
		{
			if (head.next == null)
				return head;
			ListNode dummy = new ListNode(0);
			dummy.next = head;
			ListNode pre = dummy;
			ListNode node = head;
			ListNode fast = head;
			while (fast != null && fast.next != null)
			{
				pre = pre.next;
				node = node.next;
				fast = fast.next.next;
			}
			pre.next = null;
			return node;
		}

		ListNode Merge(ListNode head, ListNode head2)
		{		
			ListNode dummy = new ListNode(0);	
			ListNode node = dummy;
			while (head != null || head2 != null)
			{
				if (head != null && head2 != null)
				{
					if (head.val < head2.val)
					{
						node.next = head;
						head = head.next;
					}
					else
					{
						node.next = head2;
						head2 = head2.next;
					}
				}
				else if (head == null)
				{
					node.next = head2;
					head2 = head2.next;

				}
				else
				{
					node.next = head;
					head = head.next;
				}
				node = node.next;
			}
			return dummy.next;
		}

    }

* 别人的解法2

    public ListNode SortList(ListNode head) {
            ListNode node = head;
            if (head == null)
            {
                return null;
            }
            return SortList(head, GetSortListLen(head));
    }
    
    public ListNode SortList(ListNode head, int len)
    {
        if (len == 1) //单节点
        {
            return head;
        }
        int mid = (len - 1) >> 1;
        ListNode left = head;
        ListNode right = left.next;
        for (int i = 0; i < mid - 1; i++)
        {
            left = left.next;
            right = right.next;
        }
        left.next = null;
        ListNode leftSortList = SortList(head, GetSortListLen(head));
        ListNode rightSortList = SortList(right, GetSortListLen(right));
        return MergeSortList(leftSortList, rightSortList);
    }

    /// <summary>
    /// 合并排序链表
    /// </summary>
    /// <returns></returns>
    public ListNode MergeSortList(ListNode leftSortList, ListNode rightSortList)
    {
        ListNode mergeNode, head;
        if (leftSortList.val <= rightSortList.val)
        {
            mergeNode = new ListNode(leftSortList.val);
            leftSortList = leftSortList.next;
        }
        else
        {
            mergeNode = new ListNode(rightSortList.val);
            rightSortList = rightSortList.next;
        }
        head = mergeNode;
        while (leftSortList != null && rightSortList != null)
        {
            if (leftSortList.val <= rightSortList.val)
            {
                mergeNode.next = new ListNode(leftSortList.val);
                leftSortList = leftSortList.next;
            }
            else
            {
                mergeNode.next = new ListNode(rightSortList.val);
                rightSortList = rightSortList.next;
            }
            mergeNode = mergeNode.next;
        }
        if (leftSortList == null)
        {
            mergeNode.next = rightSortList;
        }
        else
        {
            mergeNode.next = leftSortList;
        }
        return head;
    }

    /// <summary>
    /// 获取排序列表长度
    /// </summary>
    /// <param name="head"></param>
    /// <returns></returns>
    public int GetSortListLen(ListNode head)
    {
        int k = 0;
        if (head == null)
        {
            return 0;
        }
        ListNode node = head;
        while (node != null)
        {
            k++;
            node = node.next;
        }
        return k;
    }
  
*/
