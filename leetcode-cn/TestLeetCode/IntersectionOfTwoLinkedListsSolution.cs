/*
编写一个程序，找到两个单链表相交的起始节点。

如下面的两个链表：



在节点 c1 开始相交。

 

示例 1：



输入：intersectVal = 8, listA = [4,1,8,4,5], listB = [5,0,1,8,4,5], skipA = 2, skipB = 3
输出：Reference of the node with value = 8
输入解释：相交节点的值为 8 （注意，如果两个链表相交则不能为 0）。从各自的表头开始算起，链表 A 为 [4,1,8,4,5]，链表 B 为 [5,0,1,8,4,5]。在 A 中，相交节点前有 2 个节点；在 B 中，相交节点前有 3 个节点。
 

示例 2：



输入：intersectVal = 2, listA = [0,9,1,2,4], listB = [3,2,4], skipA = 3, skipB = 1
输出：Reference of the node with value = 2
输入解释：相交节点的值为 2 （注意，如果两个链表相交则不能为 0）。从各自的表头开始算起，链表 A 为 [0,9,1,2,4]，链表 B 为 [3,2,4]。在 A 中，相交节点前有 3 个节点；在 B 中，相交节点前有 1 个节点。
 

示例 3：



输入：intersectVal = 0, listA = [2,6,4], listB = [1,5], skipA = 3, skipB = 2
输出：null
输入解释：从各自的表头开始算起，链表 A 为 [2,6,4]，链表 B 为 [1,5]。由于这两个链表不相交，所以 intersectVal 必须为 0，而 skipA 和 skipB 可以是任意值。
解释：这两个链表不相交，因此返回 null。
 

注意：

如果两个链表没有交点，返回 null.
在返回结果后，两个链表仍须保持原有的结构。
可假定整个链表结构中没有循环。
程序尽量满足 O(n) 时间复杂度，且仅用 O(1) 内存。


*/

/// <summary>
/// https://leetcode-cn.com/problems/intersection-of-two-linked-lists/
/// 160. 相交链表
/// 
///
/// </summary>
internal class IntersectionOfTwoLinkedListsSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public ListNode GetIntersectionNode(ListNode headA, ListNode headB) {
        if (headA == null || headB == null) return null;

        var pA = headA;
        var pB = headB;
        if (pA == pB) return pA;

        int countA = 0;
        int countB = 0;
        while( true)
        {
            pA = pA.next;
            if(pA == null)
            {
                if (countA == 0)
                {
                    countA++;
                    pA = headB;
                }
                else break;
            }
            pB = pB.next;
            if(pB == null)
            {
                if (countB == 0)
                {
                    countB++;
                    pB = headA;
                }
                else break;
            }
            if (pA == pB) return pA;
        }
        return null;
    }

}

/*
相交链表
力扣 (LeetCode)
发布于 2019-07-08
64.7k
方法一: 暴力法
对链表A中的每一个结点 a_ia 
i
​	
 ，遍历整个链表 B 并检查链表 B 中是否存在结点和 a_ia 
i
​	
  相同。

复杂度分析

时间复杂度 : (mn)(mn)。
空间复杂度 : O(1)O(1)。



方法二: 哈希表法
遍历链表 A 并将每个结点的地址/引用存储在哈希表中。然后检查链表 B 中的每一个结点 b_ib 
i
​	
  是否在哈希表中。若在，则 b_ib 
i
​	
  为相交结点。

复杂度分析

时间复杂度 : O(m+n)O(m+n)。
空间复杂度 : O(m)O(m) 或 O(n)O(n)。



方法三：双指针法
创建两个指针 pApA 和 pBpB，分别初始化为链表 A 和 B 的头结点。然后让它们向后逐结点遍历。
当 pApA 到达链表的尾部时，将它重定位到链表 B 的头结点 (你没看错，就是链表 B); 类似的，当 pBpB 到达链表的尾部时，将它重定位到链表 A 的头结点。
若在某一时刻 pApA 和 pBpB 相遇，则 pApA/pBpB 为相交结点。
想弄清楚为什么这样可行, 可以考虑以下两个链表: A={1,3,5,7,9,11} 和 B={2,4,9,11}，相交于结点 9。 由于 B.length (=4) < A.length (=6)，pBpB 比 pApA 少经过 22 个结点，会先到达尾部。将 pBpB 重定向到 A 的头结点，pApA 重定向到 B 的头结点后，pBpB 要比 pApA 多走 2 个结点。因此，它们会同时到达交点。
如果两个链表存在相交，它们末尾的结点必然相同。因此当 pApA/pBpB 到达链表结尾时，记录下链表 A/B 对应的元素。若最后元素不相同，则两个链表不相交。
复杂度分析

时间复杂度 : O(m+n)O(m+n)。
空间复杂度 : O(1)O(1)。
下一篇：图解相交链表

public class Solution {
    public ListNode GetIntersectionNode(ListNode headA, ListNode headB) {
        if(headA == null || headB == null) return null;

        ListNode pA = headA; ListNode pB = headB;

        while(pA != pB){
            pA = pA == null ? headB : pA.next;
            pB = pB == null ? headA : pB.next;
        }

        return pA;
    }
}

public class Solution {
    public ListNode GetIntersectionNode(ListNode headA, ListNode headB) {
        //假设链表a和链表b相交，没相交的部分为a1,b1,相交的部分为c，可知a1+c+b1=b1+c+a1；也就是说相交的两个链表分别按照这个等式两边的路线去走，走完刚好是来到相交的结点。这时候可以通过判断他们是否在相同的结点，跳出循环

        if(headA==null||headB==null)
        {
            return null;
        }
        ListNode routeA=headA;
        ListNode routeB=headB;
        while(routeA!=routeB)
        {
            if(routeA==null)
            {
                routeA=headB;
            }
            else
            {
                routeA=routeA.next;
            }
            if(routeB==null)
            {
                routeB=headA;
            }
            else{
                routeB=routeB.next;
            }
        }
        return routeA;

        
    }
}

public class Solution {
    public ListNode GetIntersectionNode(ListNode headA, ListNode headB) {
        if(headA==null||headB==null){
            return null;
        }
        ListNode p1,p2;
        p1=headA;
        p2=headB;
        bool flag1=true;
        bool flag2=true;
        while(p1!=null&&p2!=null){
            if(p1==p2){
                return p1;
            }
            p1=p1.next;
            p2=p2.next;
            if(p1==null&&flag1){
                flag1=false;
                p1=headB;
            }
            if(p2==null&&flag2){
                flag2=false;
                p2=headA;
            }
        }
        return null;
    }
}




*/
