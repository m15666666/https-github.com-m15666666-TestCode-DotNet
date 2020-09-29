/*
请判断一个链表是否为回文链表。

示例 1:

输入: 1->2
输出: false
示例 2:

输入: 1->2->2->1
输出: true
进阶：
你能否用 O(n) 时间复杂度和 O(1) 空间复杂度解决此题？

*/

/// <summary>
/// https://leetcode-cn.com/problems/palindrome-linked-list/
/// 234. 回文链表
///
///
/// </summary>
internal class PalindromeLinkedListSolution
{
    public bool IsPalindrome(ListNode head)
    {
        if (head == null || head.next == null) return true;

        var firstHalfEnd = EndOfFirstHalf(head);
        var secondHalfStart = ReverseList(firstHalfEnd.next);

        var p1 = head;
        var p2 = secondHalfStart;
        bool ret = true;
        while (p2 != null)
        {
            if (p1.val != p2.val) { 
                ret = false;
                break;
            }
            p1 = p1.next;
            p2 = p2.next;
        }

        firstHalfEnd.next = ReverseList(secondHalfStart);
        return ret;

        static ListNode ReverseList(ListNode head)
        {
            ListNode prev = null;
            ListNode curr = head;
            while (curr != null)
            {
                ListNode nextTemp = curr.next;
                curr.next = prev;
                prev = curr;
                curr = nextTemp;
            }
            return prev;
        }

        // 与链表长度的奇偶有关，偶数时slow指向前一半的最后一个元素，奇数时slow指向中间的元素
        static ListNode EndOfFirstHalf(ListNode head)
        {
            ListNode fast = head;
            ListNode slow = head;
            while (fast.next != null && fast.next.next != null)
            {
                fast = fast.next.next;
                slow = slow.next;
            }
            return slow;
        }
    }
}

/*
回文链表
力扣 (LeetCode)
发布于 2020-01-14
61.0k
方法一：将值复制到数组中后用双指针法
如果你还不太熟悉链表，下面有列表的概要。

有两种常用的列表实现，一种是数组列表和链表。如果我们想在列表中存储值，那么它们是如何保存的呢？

数组列表底层是使用数组存储值，我们可以通过索引在 O(1)O(1) 的时间访问列表任何位置的值，这是由于内存寻址的方式。
链表存储的是称为节点的对象，每个节点保存一个值和指向下一个节点的指针。访问某个特定索引的节点需要 O(n)O(n) 的时间，因为要通过指针获取到下一个位置的节点。
确定数组列表是否为回文很简单，我们可以使用双指针法来比较两端的元素，并向中间移动。一个指针从起点向中间移动，另一个指针从终点向中间移动。这需要 O(n)O(n) 的时间，因为访问每个元素的时间是 O(1)O(1)，而有 nn 个元素要访问。

然后，直接在链表上操作并不简单，因为不论是正向访问还是反向访问都不是 O(1)O(1)。而将链表的值复制到数组列表中是 O(n)O(n)，因此最简单的方法就是将链表的值复制到数组列表中，再使用双指针法判断。

算法：

我们可以分为两个步骤：

复制链表值到数组列表中。
使用双指针法判断是否为回文。
第一步，我们需要遍历链表将值复制到数组列表中。我们用 currentNode 指向当前节点。每次迭代向数组添加 currentNode.val，并更新 currentNode = currentNode.next，当 currentNode = null 则停止循环。

执行第二部的最佳方法取决于你使用的变成语言。在 Python 中，很容易构造一个列表的反向副本，也很容易比较两个列表。在其他语言中，就没有那么简单。因此最好使用双指针法来检查是否为回文。我们在起点放置一个指针，在结尾放置一个指针，每一次迭代判断两个指针指向的元素是否相同，若不同，返回 false；相同则将两个指针向内移动，并继续判断，直到相遇。

在编码的过程中，注意我们比较的是节点值的大小，而不是节点本身。正确的比较方式是：node_1.val==node_2.val。而 node_1==node_2 是错误的。

class Solution {
    public boolean isPalindrome(ListNode head) {
        List<Integer> vals = new ArrayList<>();

        // Convert LinkedList into ArrayList.
        ListNode currentNode = head;
        while (currentNode != null) {
            vals.add(currentNode.val);
            currentNode = currentNode.next;
        }

        // Use two-pointer technique to check for palindrome.
        int front = 0;
        int back = vals.size() - 1;
        while (front < back) {
            // Note that we must use ! .equals instead of !=
            // because we are comparing Integer, not int.
            if (!vals.get(front).equals(vals.get(back))) {
                return false;
            }
            front++;
            back--;
        }
        return true;
    }
}
复杂度分析

时间复杂度：O(n)O(n)，其中 nn 指的是链表的元素个数。
第一步： 遍历链表并将值复制到数组中，O(n)O(n)。
第二步：双指针判断是否为回文，执行了 O(n/2)O(n/2) 次的判断，即 O(n)O(n)。
总的时间复杂度：O(2n) = O(n)O(2n)=O(n)。
空间复杂度：O(n)O(n)，其中 nn 指的是链表的元素个数，我们使用了一个数组列表存放链表的元素值。
方法二：递归
为了想出使用空间复杂度为 O(1)O(1) 的解决方案，你可能想过使用递归来解决，但是这仍然是 O(n)O(n) 的空间复杂度。让我们来看看为什么不是 O(1)O(1) 的空间复杂度。

递归为我们提供了一种优雅的方式来方向遍历节点。

function print_values_in_reverse(ListNode head)
    if head is NOT null
        print_values_in_reverse(head.next)
        print head.val
如果使用递归反向迭代节点，同时使用递归函数外的变量向前迭代，就可以判断链表是否为回文。

算法：
currentNode 指针是先到尾节点，由于递归的特性再从后往前进行比较。frontPointer 是递归函数外的指针。若 currentNode.val != frontPointer.val 则返回 false。反之，frontPointer 向前移动并返回 true。

之所以起作用的原因是递归处理节点的顺序是相反的（记住上面打印的算法）。由于递归，从本质上，我们同时在正向和逆向迭代。

下面的动画展示了算法的工作原理。每个节点都被赋予了标识符如 $1 和 $4，以便更好的解释它们。计算机在递归的过程将使用堆栈的空间，这就是为什么递归并不是 O(1)O(1) 的空间复杂度。

在这里插入图片描述
在这里插入图片描述
在这里插入图片描述
在这里插入图片描述
在这里插入图片描述
在这里插入图片描述
在这里插入图片描述
在这里插入图片描述
在这里插入图片描述
在这里插入图片描述
在这里插入图片描述
在这里插入图片描述
在这里插入图片描述
在这里插入图片描述
在这里插入图片描述
在这里插入图片描述
在这里插入图片描述
在这里插入图片描述
在这里插入图片描述
在这里插入图片描述
在这里插入图片描述
在这里插入图片描述
在这里插入图片描述
在这里插入图片描述
在这里插入图片描述
在这里插入图片描述
在这里插入图片描述
在这里插入图片描述
在这里插入图片描述
在这里插入图片描述
在这里插入图片描述
在这里插入图片描述
在这里插入图片描述
在这里插入图片描述

class Solution {
    private ListNode frontPointer;

    private boolean recursivelyCheck(ListNode currentNode) {
        if (currentNode != null) {
            if (!recursivelyCheck(currentNode.next)) return false;
            if (currentNode.val != frontPointer.val) return false;
            frontPointer = frontPointer.next;
        }
        return true;
    }

    public boolean isPalindrome(ListNode head) {
        frontPointer = head;
        return recursivelyCheck(head);
    }
}
复杂度分析

时间复杂度：O(n)O(n)，其中 nn 指的是链表的大小。
空间复杂度：O(n)O(n)，其中 nn 指的是链表的大小。我们要理解计算机如何运行递归函数，在一个函数中调用一个函数时，计算机需要在进入被调用函数之前跟踪它在当前函数中的位置（以及任何局部变量的值），通过运行时存放在堆栈中来实现（堆栈帧）。在堆栈中存放好了数据后就可以进入被调用的函数。在完成被调用函数之后，他会弹出堆栈顶部元素，以恢复在进行函数调用之前所在的函数。在进行回文检查之前，递归函数将在堆栈中创建 nn 个堆栈帧，计算机会逐个弹出进行处理。所以在使用递归时要考虑堆栈的使用情况。
这种方法不仅使用了 O(n)O(n) 的空间，且比第一种方法更差，因为在许多语言中，堆栈帧很大（如 Python），并且最大的运行时堆栈深度为 1000（可以增加，但是有可能导致底层解释程序内存出错）。为每个节点创建堆栈帧极大的限制了算法能够处理的最大链表大小。

方法三：
避免使用 O(n)O(n) 额外空间的方法就是改变输入。

我们可以将链表的后半部分反转（修改链表结构），然后将前半部分和后半部分进行比较。比较完成后我们应该将链表恢复原样。虽然不需要恢复也能通过测试用例，因为使用该函数的人不希望链表结构被更改。

算法：

我们可以分为以下几个步骤：

找到前半部分链表的尾节点。
反转后半部分链表。
判断是否为回文。
恢复链表。
返回结果。
执行步骤一，我们可以计算链表节点的数量，然后遍历链表找到前半部分的尾节点。

或者可以使用快慢指针在一次遍历中找到：慢指针一次走一步，快指针一次走两步，快慢指针同时出发。当快指针移动到链表的末尾时，慢指针到链表的中间。通过慢指针将链表分为两部分。

若链表有奇数个节点，则中间的节点应该看作是前半部分。

步骤二可以使用在反向链表问题中找到解决方法来反转链表的后半部分。

步骤三比较两个部分的值，当后半部分到达末尾则比较完成，可以忽略计数情况中的中间节点。

步骤四与步骤二使用的函数相同，再反转一次恢复链表本身。

class Solution {
    public boolean isPalindrome(ListNode head) {
        if (head == null) return true;

        // Find the end of first half and reverse second half.
        ListNode firstHalfEnd = endOfFirstHalf(head);
        ListNode secondHalfStart = reverseList(firstHalfEnd.next);

        // Check whether or not there is a palindrome.
        ListNode p1 = head;
        ListNode p2 = secondHalfStart;
        boolean result = true;
        while (result && p2 != null) {
            if (p1.val != p2.val) result = false;
            p1 = p1.next;
            p2 = p2.next;
        }

        // Restore the list and return the result.
        firstHalfEnd.next = reverseList(secondHalfStart);
        return result;
    }

    // Taken from https://leetcode.com/problems/reverse-linked-list/solution/
    private ListNode reverseList(ListNode head) {
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

    private ListNode endOfFirstHalf(ListNode head) {
        ListNode fast = head;
        ListNode slow = head;
        while (fast.next != null && fast.next.next != null) {
            fast = fast.next.next;
            slow = slow.next;
        }
        return slow;
    }
}
复杂度分析

时间复杂度：O(n)O(n)，其中 nn 指的是链表的大小。
空间复杂度：O(1)O(1)，我们是一个接着一个的改变指针，我们在堆栈上的堆栈帧不超过 O(1)O(1)。
该方法的缺点是，在并发环境下，函数运行时需要锁定其他线程或进程对链表的访问，因为在函数执执行过程中链表暂时断开。

下一篇：JAVA 快慢指针 链表反转 步骤详细 易理解

public class Solution {
	 ListNode _tempListNode;
	public  bool IsPalindrome(ListNode head)
	{
		_tempListNode = head;
		return Recursive(head);
	}

	public  bool Recursive(ListNode node)
	{
		if(node != null)           
		{
			if (!Recursive(node.next)) return false;
			if(node.val != _tempListNode.val) return false;
			_tempListNode = _tempListNode.next;
		}
		return true;
	}
}

*/