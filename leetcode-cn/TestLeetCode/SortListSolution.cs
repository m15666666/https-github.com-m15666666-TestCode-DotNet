using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


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

        ListNode root = new ListNode(int.MinValue);
        ListNode tail = root;
        ListNode middle = root;
        int count = 0;
        int midIndex = 0;

        var current = head;

        while (current != null)
        {
            count++;
            var v = current.val;
            var next = current.next;

            if( tail.val <= v)
            {
                tail.next = current;
                tail = current;
                tail.next = null;
                current = next;
                continue;
            }

            ListNode previous = null;
            var newCurrent = middle.val <= v ? middle : root;
            var newCurrentIndex = middle.val <= v ? midIndex : 0;
            while (newCurrent != null && newCurrent.val <= v)
            {
                if ( newCurrentIndex == count / 2 + 1 ) middle = newCurrent;
                previous = newCurrent;
                newCurrent = newCurrent.next;

                newCurrentIndex++;
            }

            previous.next = current;
            current.next = newCurrent;

            if (newCurrent == null) tail = current;
            else middle = current;

            current = next;
        }

        return root.next;
    }

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
