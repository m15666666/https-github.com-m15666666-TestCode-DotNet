using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个头结点为 root 的链表, 编写一个函数以将链表分隔为 k 个连续的部分。

每部分的长度应该尽可能的相等: 任意两部分的长度差距不能超过 1，也就是说可能有些部分为 null。

这k个部分应该按照在链表中出现的顺序进行输出，并且排在前面的部分的长度应该大于或等于后面的长度。

返回一个符合上述规则的链表的列表。

举例： 1->2->3->4, k = 5 // 5 结果 [ [1], [2], [3], [4], null ]

示例 1：

输入: 
root = [1, 2, 3], k = 5
输出: [[1],[2],[3],[],[]]
解释:
输入输出各部分都应该是链表，而不是数组。
例如, 输入的结点 root 的 val= 1, root.next.val = 2, \root.next.next.val = 3, 且 root.next.next.next = null。
第一个输出 output[0] 是 output[0].val = 1, output[0].next = null。
最后一个元素 output[4] 为 null, 它代表了最后一个部分为空链表。
示例 2：

输入: 
root = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10], k = 3
输出: [[1, 2, 3, 4], [5, 6, 7], [8, 9, 10]]
解释:
输入被分成了几个连续的部分，并且每部分的长度相差不超过1.前面部分的长度大于等于后面部分的长度。
 

提示:

root 的长度范围： [0, 1000].
输入的每个节点的大小范围：[0, 999].
k 的取值范围： [1, 50]. 
*/
/// <summary>
/// https://leetcode-cn.com/problems/split-linked-list-in-parts/
/// 725. 分隔链表
/// https://blog.csdn.net/Magge_Lin/article/details/82962001
/// https://blog.csdn.net/lgy54321/article/details/85040053
/// </summary>
class SplitLinkedListInPartsSolution
{
    public void Test()
    {
        ListNode root = new ListNode(1) { next = new ListNode(2) { next = new ListNode(3) { next = new ListNode(4)} } };
        int k = 5;
        var ret = SplitListToParts(root, k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public ListNode[] SplitListToParts(ListNode root, int k)
    {
        if (k == 1) return new ListNode[] { root };

        List<ListNode> list = new List<ListNode>();
        
        var current = root;
        while(current != null)
        {
            list.Add(current);
            current = current.next;
        }

        int length = list.Count;
        int average = length / k;
        int remain = length % k;

        List<ListNode> ret = new List<ListNode>();
        int index = 0;
        for (int j = 0; j < k; j++)
        {
            int size = average + (0 < remain ? 1 : 0);
            if(size == 0)
            {
                ret.Add(null);
                continue;
            }

            if (0 < remain) --remain;

            current = list[index];
            ret.Add(current);
            for (int i = index + 1, s = 1; s < size; i++,s++)
            {
                current.next = list[i];
                current = current.next;
            }
            current.next = null;

            index += size;
        }

        return ret.ToArray();
    }
}
/*
public class Solution {
    public ListNode[] SplitListToParts(ListNode root, int k) {
        ListNode[] output=new ListNode[k];
        ListNode p=root;
        int len=0,avg=0,extra=0;
        while(p!=null)
        {
            len++;
            p=p.next;
        }
        p=root;
        avg=len/k;
        extra=len%k;
        for(int i=0;i<k;i++)
        {
            int cnt=1;
            ListNode temp=p;
            ListNode copy=temp;
            if(i<extra)
            {
                while(cnt<avg+1)
                {
                    p=p.next;
                    temp=temp.next;
                    cnt++;
                }
                p=p.next;
                temp.next=null;
            }
            else
            {
                if(avg!=0)
                {
                    while(cnt<avg)
                    {
                        p=p.next;
                        temp=temp.next;
                        cnt++;
                    }
                    p=p.next;
                    temp.next=null;
                }
                else
                {
                    copy=null;
                }
            }
            output[i]=copy;
        }
         
        return output;
    }
} 
*/
