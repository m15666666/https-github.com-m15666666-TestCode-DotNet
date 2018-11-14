using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// https://leetcode-cn.com/problems/convert-sorted-list-to-binary-search-tree/
/// 109.有序链表转换二叉搜索树
/// 给定一个单链表，其中的元素按升序排序，将其转换为高度平衡的二叉搜索树。
/// 本题中，一个高度平衡二叉树是指一个二叉树每个节点 的左右两个子树的高度差的绝对值不超过 1。
/// https://www.cnblogs.com/wmx24/p/9449320.html，巧妙的构造了快指针始终是慢指针位置的2倍。但递归时每次对链表的遍历耗费了时间。
/// https://www.bbsmax.com/A/6pdDEW0Kzw/，代码清楚，思路与上面的类似。效率更慢。
/// https://blog.csdn.net/m0_37316917/article/details/79995668，思路与上面一致，有注释。
/// https://blog.csdn.net/weixin_42130471/article/details/80488873，思路与上面一致，但先将链表变为数组，提高后序处理的效率。但链表数据太大了就超时。
/// </summary>
class SortedListToBSTSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    public TreeNode SortedListToBST(ListNode head)
    {
        if (head == null) return null;
        if (head.next == null) return new TreeNode(head.val);

        var current = head;
        List<int> list = new List<int>(100);
        while( current != null)
        {
            list.Add(current.val);
            current = current.next;
        }

        return SortedListToBST(list, 0, list.Count - 1);
    }

    private TreeNode SortedListToBST( List<int> list, int startIndex, int stopIndex)
    {
        if (stopIndex < startIndex) return null;

        int midIndex = ( startIndex + stopIndex ) / 2;
        TreeNode ret = new TreeNode(list[midIndex]);
        ret.left = SortedListToBST(list, startIndex, midIndex - 1);
        ret.right = SortedListToBST(list, midIndex + 1, stopIndex);
        return ret;
    }

}