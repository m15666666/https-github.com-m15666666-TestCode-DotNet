using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// https://leetcode-cn.com/problems/copy-list-with-random-pointer/
/// 138.复制带随机指针的链表
/// 给定一个链表，每个节点包含一个额外增加的随机指针，该指针可以指向链表中的任何节点或空节点。
/// 要求返回这个链表的深度拷贝。 
/// </summary>
class CopyListWithRandomPointerSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    public RandomListNode CopyRandomList(RandomListNode head)
    {
        if (head == null) return null;
        var current = head;

        Dictionary<RandomListNode, RandomListNode> map = new Dictionary<RandomListNode, RandomListNode>();
        var ret = new RandomListNode(current.label);
        map[current] = ret;
        var retCurrent = ret;

        current = current.next;
        while ( current != null)
        {
            var copyNode = new RandomListNode(current.label);
            map[current] = copyNode;
            retCurrent.next = copyNode;

            current = current.next;
            retCurrent = copyNode;
        }

        current = head;
        retCurrent = ret;
        while (current != null)
        {
            if (current.random != null) retCurrent.random = map[current.random];
            current = current.next;
            retCurrent = retCurrent.next;
        }

        return ret;
    }

}

/**
 * Definition for singly-linked list with a random pointer.
 *  */
public class RandomListNode {
    public int label;
    public RandomListNode next, random;
    public RandomListNode(int x) { this.label = x; }
}
