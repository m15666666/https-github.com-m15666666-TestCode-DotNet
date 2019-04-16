using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个单链表，随机选择链表的一个节点，并返回相应的节点值。保证每个节点被选的概率一样。

进阶:
如果链表十分大且长度未知，如何解决这个问题？你能否使用常数级空间复杂度实现？

示例:

// 初始化一个单链表 [1,2,3].
ListNode head = new ListNode(1);
head.next = new ListNode(2);
head.next.next = new ListNode(3);
Solution solution = new Solution(head);

// getRandom()方法应随机返回1,2,3中的一个，保证每个元素被返回的概率相等。
solution.getRandom(); 
*/
/// <summary>
/// https://leetcode-cn.com/problems/linked-list-random-node/
/// 382. 链表随机节点
/// </summary>
class LinkedListRandomNodeSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public LinkedListRandomNodeSolution(ListNode head)
    {
        while(head != null)
        {
            _nodes.Add(head);
            head = head.next;
        }
    }

    private List<ListNode> _nodes = new List<ListNode>();
    private Random _random = new Random();
    // Returns a random node's value.
    public int GetRandom()
    {
        int count = _nodes.Count;
        if (count == 0) return 0;
        int index = _random.Next() % count;
        return _nodes[index].val;
    }
}
/*
public class Solution
{
    ListNode _head;
    ListNode _cur;
    Random rd;
    public Solution(ListNode head)
    {
        if (head == null)
        {
            head = new ListNode(0);
        }
        _head = head;
        _cur = _head;
        rd = new Random();
        int init = rd.Next(5);
        while (init > 0)
        {
            _cur = _cur.next;
            if (_cur == null)
            {
                _cur = _head;
            }
            init--;
        }
    }

    public int GetRandom()
    {
        int init = rd.Next(5);
        while (init > 0)
        {
            _cur = _cur.next;
            if (_cur == null)
            {
                _cur = _head;
            }
            init--;
        }
        return _cur.val;
    }
} 
*/