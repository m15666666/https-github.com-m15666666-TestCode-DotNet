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
蓄水池抽样算法
jackvin
发布于 2020-03-06
7.5k
前言
如果您觉得我的题解尚可，欢迎给我点赞
另外一个随机算法--洗牌算法

蓄水池抽样算法
最近经常能看到面经中出现在大数据流中的随机抽样问题

即：当内存无法加载全部数据时，如何从包含未知大小的数据流中随机选取k个数据，并且要保证每个数据被抽取到的概率相等。

当 k = 1 时，即此题的情况
也就是说，我们每次只能读一个数据。

假设数据流含有N个数，我们知道如果要保证所有的数被抽到的概率相等，那么每个数抽到的概率应该为 1/N

那如何保证呢？

先说方案：

每次只保留一个数，当遇到第 i 个数时，以 1/i的概率保留它，(i-1)/i的概率保留原来的数。

举例说明： 1 - 10

遇到1，概率为1，保留第一个数。
遇到2，概率为1/2，这个时候，1和2各1/2的概率被保留
遇到3，3被保留的概率为1/3，(之前剩下的数假设1被保留)，2/3的概率 1 被保留，(此时1被保留的总概率为 2/3 * 1/2 = 1/3)
遇到4，4被保留的概率为1/4，(之前剩下的数假设1被保留)，3/4的概率 1 被保留，(此时1被保留的总概率为 3/4 * 2/3 * 1/2 = 1/4)
以此类推，每个数被保留的概率都是1/N。

证明使用数学归纳法即可


import random
class Solution:

    def __init__(self, head: ListNode):
        self.head = head
        
    def getRandom(self) -> int:
        count = 0
        reserve = 0
        cur = self.head
        while cur:
            count += 1
            rand = random.randint(1,count)
            if rand == count:
                reserve = cur.val
            cur = cur.next
        return reserve
当 k = m 时
也就是说，我们每次能读m个数据。

和上面相同的道理，只不过概率在每次乘以了m而已

1.png

下一篇：【中规中矩】 水塘抽样 Reservoir Sampling 思想

【中规中矩】 水塘抽样 Reservoir Sampling 思想
卖码翁
发布于 2020-12-13
343
解题思路
水塘抽样 Reservoir Sampling。在大样本数据流中等概率随机抽样。需要以1/i的概率选择当前元素即可。因为(i-1)/i会替换前面保留下来的一个数,根据数学归纳法假设，前面的(i-1)个数可以被等概率的随机选择，机会均为1/(i-1)。因此选择上次留下数的概率为[1/(i-1)]*[(i-1)/i] = 1/i。

注意： 解法2中，getRandom每次都要重新从head最开始产生随机数，才能保证均匀分布且不会出现cur为空的情况。

代码

class Solution1 {
public:
    Solution1(ListNode* head) {
        len = getLen(head);
    }
    
    int getRandom() {
        int index = rand() % len;
        auto p = head;
        int i = 0;
        while (i != index) {
            i++;
            p = p->next;
        }

        assert(p && "unexpected nullptr!");
        return p->val;
    }

private:
    int len;
    ListNode* head;
    int getLen(ListNode* head) {
        if (!head) {
            return 0;
        }
        int count = 0;
        while (head) {
            count++;
            head = head->next;
        }

        return count;
    }
};
进阶解法2：



class Solution {
public:
    Solution(ListNode* head) {
        this->head = head;
    }
    
    int getRandom() {
        assert(head && "Unexpected nullptr!");
        int index = 2;
        int val = head->val;
        auto cur = head->next;
        while (cur) {
            if (rand() % index == 0) {
                val = cur->val; // 1/i proability replace last value
            }
            index++;
            cur = cur->next;
        }

        return val;
    }

private:
    int len;
    ListNode* head;
};
下一篇：lc 382 链表随机节点，我感 jio 讲得应该详细了

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