/*
对链表进行插入排序。

插入排序的动画演示如上。从第一个元素开始，该链表可以被认为已经部分排序（用黑色表示）。
每次迭代时，从输入数据中移除一个元素（用红色表示），并原地将其插入到已排好序的链表中。

 

插入排序算法：

插入排序是迭代的，每次只移动一个元素，直到所有元素可以形成一个有序的输出列表。
每次迭代中，插入排序只从输入数据中移除一个待排序的元素，找到它在序列中适当的位置，并将其插入。
重复直到所有输入数据插入完为止。
 

示例 1：

输入: 4->2->1->3
输出: 1->2->3->4
示例 2：

输入: -1->5->3->4->0
输出: -1->0->3->4->5
通过次数35,684提交次数55,095
在真实的面试中遇到过这道题？

*/

/// <summary>
/// https://leetcode-cn.com/problems/insertion-sort-list/
/// 147. 对链表进行插入排序 Insertion Sort List
/// 对链表进行插入排序。
/// </summary>
internal class InsertionSortListSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public ListNode InsertionSortList(ListNode head)
    {
        if (head == null || head.next == null) return head;

        ListNode root = new ListNode(0)
        {
            next = head
        };
        var prev = head;
        var cur = head.next;

        while (cur != null)
        {
            if (prev.val <= cur.val)
            {
                prev = cur;
                cur = cur.next;
                continue;
            }

            prev.next = cur.next;
            cur.next = null;
            var v = cur.val;
            ListNode p = root;
            while (p.next.val <= v) p = p.next;

            cur.next = p.next;
            p.next = cur;

            cur = prev.next;
        }
        return root.next;
    }

    //public ListNode InsertionSortList(ListNode head)
    //{
    //    if (head == null || head.next == null) return head;

    //    ListNode root = new ListNode(int.MinValue);
    //    var current = head;

    //    while ( current != null )
    //    {
    //        var v = current.val;
    //        var next = current.next;

    //        ListNode previous = null;
    //        var newCurrent = root;
    //        while( newCurrent != null && newCurrent.val <= v )
    //        {
    //            previous = newCurrent;
    //            newCurrent = newCurrent.next;
    //        }

    //        previous.next = current;
    //        current.next = newCurrent;

    //        current = next;
    //    }

    //    return root.next;
    //}
}

/*

    public ListNode InsertionSortList(ListNode head) {
        ListNode node = head;
        ListNode lastNode = head;
        while (node != null)
        {
            ListNode compare = head;
            ListNode lastCompare = head;
            while (compare != node)
            {
                if (node.val < compare.val)
                {
                    //insert
                    ListNode tmp = node.next;
                    node.next = compare;
                    lastNode.next = tmp;
                    if (compare != lastCompare)
                        lastCompare.next = node;
                    if (compare == head)
                        head = node;
                    node = lastNode;
                    break;
                }
                lastCompare = compare;
                compare = compare.next;
            }
            lastNode = node;
            node = node.next;
        }
        return head;
    }

C++ 98.69% 三种解法 简洁易懂
MrEthan
发布于 2020-07-02
1.0k
方法0：原地排序。
思路如下：
1.跳过已经排序的节点；
2.找到未排序的节点，将其从链表上摘下；
3.从头开始查找合适的插入点，未建立哨兵节点，需处理head更新的情况；

方法1：
方法0的改进版，建立哨兵节点dummy_head，便于从头开始查找合适的插入位置；

方法2：
建立新链表dummy_head，一个一个从原链表摘下节点，按顺序插入到新链表中。

效率：方法1>方法0>方法2
image.png


class Solution {
public:
    ListNode* insertionSortList0(ListNode* head) {
        if (!head || !head->next) {
            return head;
        }
        
        ListNode* work = NULL;
        ListNode* prev = head;
        ListNode* p = head->next;

        //p为未排序节点
        while (p) {
            //跳过本身已经有序节点
            if (p->val >= prev->val) {
                prev = p;
                p = p->next;
            }
            else {
                //p摘链
                prev->next = p->next;
                work = p;
                p = p->next;
                //从头开始找比p小的节点，插入到它的前面
                if (head->val > work->val) {
                    //插入到头节点之前
                    work->next = head;
                    head = work;
                }
                else {
                    //插入到头节点后
                    ListNode* temp = head;
                    while (temp->next->val <= work->val) {
                        temp = temp->next;
                    }
                    work->next = temp->next;
                    temp->next = work;
                }
            }
        }

        return head;
    }

    //使用哨兵节点，无需特殊处理头结点改变的情况
    ListNode* insertionSortList1(ListNode* head) {
        if (!head || !head->next) {
            return head;
        }
        ListNode* dummy_head = new ListNode(0);
        ListNode* cur = NULL, *prev = NULL, *ans = NULL;
        
        dummy_head->next = head;
        prev = head; //prev指向当前已排序的最大节点
        cur = head->next;

        while (cur) {
            if (cur->val >= prev->val) {
                //跳过已排序部分
                prev = cur;
                cur = cur->next;
                continue;
            }
            //cur节点摘链
            prev->next = cur->next;
            ListNode* p = dummy_head;
            //从头开始查找合适位置插入
            while (p->next->val <= cur->val) {
                p = p->next;
            }
            //找到比cur大的节点为p->next，把cur插入到p节点后面
            cur->next = p->next;
            p->next = cur;
            //继续遍历
            cur = prev->next;
        }
        ans = dummy_head->next;

        delete dummy_head;

        return ans;
    }

    //新建链表，从原链表取节点插入新链表
    ListNode* insertionSortList2(ListNode* head) {
        if (!head || !head->next) {
            return head;
        }

        ListNode* oldNode = NULL;
        ListNode* dummy_head = new ListNode(0);
        ListNode* ans = NULL;

        dummy_head->next = head;
        head = head->next;
        dummy_head->next->next = NULL;

        while (head) {
            //从原链表取节点
            oldNode = head;
            head = head->next;
            //断开摘下的节点与原链表的连接
            oldNode->next = NULL;

            //把节点按顺序插入新链表
            ListNode* p = dummy_head->next;
            ListNode* prev = dummy_head;
            while (p != NULL && p->val <= oldNode->val) {
                prev = p;
                p = p->next;
            }
            oldNode->next = prev->next;
            prev->next = oldNode;
        }
        ans = dummy_head->next;

        delete dummy_head;

        return ans;
    }
};
下一篇：完美通过

public class Solution {
    public ListNode InsertionSortList(ListNode head) {
        if (head == null || head.next == null)
            {
                return head;
            }

            ListNode dummy = new ListNode(-1);
            dummy.next = head;
            ListNode prev = head;
            ListNode cur = head.next;

            while (cur != null)
            {
                if (cur.val < prev.val)
                {
                    //cur的值小于prev的值 就从头开始比对
                    ListNode temp = dummy;
                    while (temp.next.val < cur.val)
                    {
                        temp = temp.next;
                    }
                    //从头开始比对找到第一个值小于cur的节点temp 
                    //把cur插入到temp后边
                    prev.next = cur.next;
                    cur.next = temp.next;
                    temp.next = cur;
                    cur = prev.next;
                }
                else
                {
                    //移动双指针
                    prev = prev.next;
                    cur = cur.next;
                }

               
            }

            return dummy.next;
    }
}


*/
