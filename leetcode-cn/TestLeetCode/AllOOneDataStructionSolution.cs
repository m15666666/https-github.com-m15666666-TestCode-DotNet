/*
请你实现一个数据结构支持以下操作：

Inc(key) - 插入一个新的值为 1 的 key。或者使一个存在的 key 增加一，保证 key 不为空字符串。
Dec(key) - 如果这个 key 的值是 1，那么把他从数据结构中移除掉。否则使一个存在的 key 值减一。如果这个 key 不存在，这个函数不做任何事情。key 保证不为空字符串。
GetMaxKey() - 返回 key 中值最大的任意一个。如果没有元素存在，返回一个空字符串"" 。
GetMinKey() - 返回 key 中值最小的任意一个。如果没有元素存在，返回一个空字符串""。
 

挑战：

你能够以 O(1) 的时间复杂度实现所有操作吗？

*/

using System.Collections.Generic;

/// <summary>
/// https://leetcode-cn.com/problems/all-oone-data-structure/
/// 432. 全 O(1) 的数据结构
///
///
/// </summary>
internal class AllOOneDataStructionSolution
{
    public void Test()
    {
        Dec("hello");
        GetMaxKey();
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public AllOOneDataStructionSolution()
    {
        head.next = tail;
        tail.prev = head;
    }

    public void Inc(string key)
    {
        if (!_key2Node.ContainsKey(key))
        { // 当前key不存在，插入到链表末尾
            ListNode node = new ListNode(key, 1);
            _key2Node[key] = node;
            insert(tail.prev, tail, node); // 插入
            //if (!_firstValue2Node.ContainsKey(1)) newFirst(1, node); // 更新first
            newLast(1, node); // 更新last
        }
        else
        {
            ListNode node = _key2Node[key]; // 当前节点
            int val = node.val; // 旧值
            int newVal = val + 1; // 新值
            ListNode firstNode = _firstValue2Node[val]; // 链表中第一个值为val的节点
            ListNode lastNode = _lastValue2Node[val]; // 链表中最后一个值为val的节点

            // 1. 找位置
            node.val = newVal;
            if (firstNode == lastNode)
            { // 当前节点是唯一一个值为val的节点
                _firstValue2Node.Remove(val); // 没有值为val的节点了
                _lastValue2Node.Remove(val); // 没有值为val的节点了
                newLast(newVal, node); // 更新last
            }
            else if (node == firstNode)
            { // 该节点是链表中第一个值为val的节点
                // 不动
                newLast(newVal, node);
                newFirst(val, node.next);
            }
            else
            {
                if (node == lastNode) newLast(val, node.prev); // 是最后一个值val的节点
                // 这个时候，节点应该移动到链表中第一个值为val的节点之前
                move(node, firstNode.prev, firstNode);
                newLast(newVal, node);
            }
        }
    }

    public void Dec(string key)
    {
        // 与inc类似，不过多了一个值为1删除的判断
        if (!_key2Node.ContainsKey(key)) return;
        ListNode node = _key2Node[key];

        int val = node.val;
        int newVal = val - 1;
        ListNode firstNode = _firstValue2Node[val];
        ListNode lastNode = _lastValue2Node[val];

        if (val == 1)
        { // 值为1，删除这个节点
            if (firstNode == lastNode)
            { // 没有值为1的节点了
                _firstValue2Node.Remove(1);
                _lastValue2Node.Remove(1);
            }
            else if (node == firstNode)
            { // 起始值右移
                _firstValue2Node[1] = node.next;
            }
            else if (node == lastNode)
            { // 终结值左移
                _lastValue2Node[1] = node.prev;
            }
            remove(node);
            _key2Node.Remove(key);
        }
        else
        {
            node.val = newVal;
            if (firstNode == lastNode)
            { // 唯一值为val的节点
                // 位置不变，成为newVal的首位
                _firstValue2Node.Remove(val);
                _lastValue2Node.Remove(val);
                newFirst(newVal, node);
            }
            else if (node == lastNode)
            { // 是最后一项val值的节点
                // 位置不变，成为newVal的首位，并且prev成为val的最后一位
                newFirst(newVal, node);
                newLast(val, node.prev);
            }
            else
            {
                if (node == firstNode) newFirst(val, node.next); // 是第一项val值的节点
                move(node, lastNode, lastNode.next); // 移动到lastNode之后
                newFirst(newVal, node);
            }
        }
    }
    public string GetMaxKey() => head.next == tail ? "" : head.next.key;
    public string GetMinKey() => tail.prev == head ? "" : tail.prev.key;

    /**
     * 将节点 [insert] 插入到 n1 与 n2 之间
     */

    private void insert(ListNode n1, ListNode n2, ListNode insert)
    {
        n1.next = insert;
        n2.prev = insert;
        insert.prev = n1;
        insert.next = n2;
    }

    /**
     * 删除链表节点[n]
     */

    private void remove(ListNode n)
    {
        ListNode prev = n.prev;
        ListNode next = n.next;
        prev.next = next;
        next.prev = prev;
        n.prev = null;
        n.next = null;
    }

    /**
     * 将节点node移动到prev与next之间
     */

    private void move(ListNode node, ListNode prev, ListNode next)
    {
        remove(node);
        insert(prev, next, node);
    }

    /**
     * 将[node]设置为新的val值起始点
     */

    private void newFirst(int val, ListNode node)
    {
        _firstValue2Node[val] = node;
        if (!_lastValue2Node.ContainsKey(val)) _lastValue2Node[val] = node;
    }

    /**
     * 将[node]设置为新的val值终止点
     */

    private void newLast(int val, ListNode node)
    {
        _lastValue2Node[val] = node;
        if (!_firstValue2Node.ContainsKey(val)) _firstValue2Node[val] = node;
    }

    public class ListNode
    {
        public ListNode prev;
        public ListNode next;
        public string key;
        public int val;

        public ListNode(string key, int val)
        {
            this.key = key;
            this.val = val;
        }
    }

    /**
     * k-v查找节点
     */
    private readonly Dictionary<string, ListNode> _key2Node = new Dictionary<string, ListNode>();
    /**
     * key - 节点的值；
     * value - 链表中第一个值为key的节点。
     */
    private readonly Dictionary<int, ListNode> _firstValue2Node = new Dictionary<int, ListNode>();
    /**
     * key - 节点的值；
     * value - 链表中最后一个值为key的节点。
     */
    private readonly Dictionary<int, ListNode> _lastValue2Node = new Dictionary<int, ListNode>();

    /**
     * 链表伪头节点
     */
    private readonly ListNode head = new ListNode(null, 0);
    /**
     * 链表伪尾节点
     */
    private readonly ListNode tail = new ListNode(null, 0);
}

/*
HashMap+链表实现全O(1)解法（思路+代码+超级详细注释） - 【432. 全 O(1) 的数据结构】
littlefogcat
发布于 2021-02-03
163
简单思路：
使用降序排列的双向链表实现有序；
使用 HashMap 实现查询，其中 value 为链表的节点；
由于链表中可能有重复值，所以额外使用两个map first和last来保存每个值在链表中的起始位置和结束位置。
结构示意图：
结构示意图

复杂度分析：
inc：
当节点不存在时，插入到链表末尾，O(1)；
当节点存在时，根据first得到链表中当前值的起始位置，将节点插入到起始位置之前，O(1)。

dec：
如果节点值为1，那么删除节点，O(1)；
如果节点值不为1，那么根据last得到链表中当前值的结束位置，将节点插入到结束位置之后，O(1)。

getMaxKey：
因为链表降序排列，所以第一个节点即是最大，O(1)。

getMinKey：
因为链表降序排列，所以最后一个节点即是最小，O(1)。

以上操作均满足O(1)。

完整代码+注释

public class AllOne {
    
    private final Map<String, ListNode> map = new HashMap<>();
    
    private final Map<Integer, ListNode> first = new HashMap<>();
    
    private final Map<Integer, ListNode> last = new HashMap<>();

    
    private final ListNode head = new ListNode(null, 0);
    
    private final ListNode tail = new ListNode(null, 0);

    AllOne() {
        head.next = tail;
        tail.prev = head;
    }

    private class ListNode { // 链表节点
        ListNode prev, next;
        String key;
        int val;

        public ListNode(String key, int val) {
            this.key = key;
            this.val = val;
        }
    }

    
    private void insert(ListNode n1, ListNode n2, ListNode insert) {
        n1.next = insert;
        n2.prev = insert;
        insert.prev = n1;
        insert.next = n2;
    }

    
    private void remove(ListNode n) {
        ListNode prev = n.prev;
        ListNode next = n.next;
        prev.next = next;
        next.prev = prev;
        n.prev = null;
        n.next = null;
    }


    private void move(ListNode node, ListNode prev, ListNode next) {
        remove(node);
        insert(prev, next, node);
    }

    private void newFirst(int val, ListNode node) {
        first.put(val, node);
        if (!last.containsKey(val)) last.put(val, node);
    }

    private void newLast(int val, ListNode node) {
        last.put(val, node);
        if (!first.containsKey(val)) first.put(val, node);
    }

 
    public void inc(String key) {
        if (!map.containsKey(key)) { // 当前key不存在，插入到链表末尾
            ListNode node = new ListNode(key, 1);
            map.put(key, node);
            insert(tail.prev, tail, node); // 插入
            if (!first.containsKey(1)) newFirst(1, node); // 更新first
            newLast(1, node); // 更新last
        } else {
            ListNode node = map.get(key); // 当前节点
            int val = node.val; // 旧值
            int newVal = val + 1; // 新值
            ListNode firstNode = first.get(val); // 链表中第一个值为val的节点
            ListNode lastNode = last.get(val); // 链表中最后一个值为val的节点

            // 1. 找位置
            node.val = newVal;
            if (firstNode == lastNode) { // 当前节点是唯一一个值为val的节点
                first.remove(val); // 没有值为val的节点了
                last.remove(val); // 没有值为val的节点了
                newLast(newVal, node); // 更新last
            } else if (node == firstNode) { // 该节点是链表中第一个值为val的节点
                // 不动
                newLast(newVal, node);
                newFirst(val, node.next);
            } else {
                if (node == lastNode) newLast(val, node.prev); // 是最后一个值val的节点
                // 这个时候，节点应该移动到链表中第一个值为val的节点之前
                move(node, firstNode.prev, firstNode);
                newLast(newVal, node);
            }
        }
    }

    
    public void dec(String key) {
        // 与inc类似，不过多了一个值为1删除的判断
        ListNode node = map.get(key);
        if (node == null) return;

        int val = node.val;
        int newVal = val - 1;
        ListNode firstNode = first.get(val);
        ListNode lastNode = last.get(val);

        if (val == 1) { // 值为1，删除这个节点
            if (firstNode == lastNode) { // 没有值为1的节点了
                first.remove(1);
                last.remove(1);
            } else if (node == firstNode) { // 起始值右移
                first.put(1, node.next);
            } else if (node == lastNode) { // 终结值左移
                last.put(1, node.prev);
            }
            remove(node);
            map.remove(key);
        } else {
            node.val = newVal;
            if (firstNode == lastNode) { // 唯一值为val的节点
                // 位置不变，成为newVal的首位
                first.remove(val);
                last.remove(val);
                newFirst(newVal, node);
            } else if (node == lastNode) { // 是最后一项val值的节点
                // 位置不变，成为newVal的首位，并且prev成为val的最后一位
                newFirst(newVal, node);
                newLast(val, node.prev);
            } else {
                if (node == firstNode) newFirst(val, node.next); // 是第一项val值的节点
                move(node, lastNode, lastNode.next); // 移动到lastNode之后
                newFirst(newVal, node);
            }
        }
    }

    
    public String getMaxKey() {
        return head.next == tail ? "" : head.next.key;
    }

    
    public String getMinKey() {
        return tail.prev == head ? "" : tail.prev.key;
    }
}


public class AllOne
{
    private class Item
    {
        public int Value { get; set; }

        public LinkedList<string> Keys { get; set; }

        public Item(int value, params string[] keys)
        {
            Value = value;
            Keys = new LinkedList<string>();
            foreach (var e in keys)
                Keys.AddFirst(e);
        }

        public Item(int value)
        {
            Value = value;
            Keys = new LinkedList<string>();
        }
    }

    private class Nodes
    {
        public LinkedListNode<Item> Outer { get; set; }

        public LinkedListNode<string> Inner { get; set; }

        public Nodes(LinkedListNode<Item> outer = null, LinkedListNode<string> inner = null)
        {
            Outer = outer;
            Inner = inner;
        }

        public void Deconstruct(out LinkedListNode<Item> outer, out LinkedListNode<string> inner)
        {
            outer = Outer;
            inner = Inner;
        }
    }

    private LinkedList<Item> List { get; set; } = new LinkedList<Item>();

    private Dictionary<string, Nodes> Data { get; set; } = new Dictionary<string, Nodes>();

    
    public AllOne()
    {

    }

    
    public void Inc(string key)
    {
        if (Data.TryAdd(key, new Nodes()))
        {
            if (List.Count == 0 || List.First.Value.Value != 1)
                List.AddFirst(new Item(1, key));
            else List.First.Value.Keys.AddFirst(key);
            Data[key] = new Nodes(List.First, List.First.Value.Keys.First);
        }
        else
        {
            var (opos, ipos) = Data[key];
            var onpos = opos.Next;
            LinkedListNode<Item> pos;
            int value = opos.Value.Value + 1;
            if (onpos != null && onpos.Value.Value == value)
                pos = onpos;
            else if (opos.Value.Keys.Count != 1)
                pos = List.AddAfter(opos, new Item(value));
            else
            {
                opos.Value = new Item(value) { Keys = opos.Value.Keys };
                return;
            }
            opos.Value.Keys.Remove(ipos);
            pos.Value.Keys.AddFirst(ipos);
            Data[key] = new Nodes(pos, pos.Value.Keys.First);
            if (opos.Value.Keys.Count == 0)
                List.Remove(opos);
        }
    }

    
    public void Dec(string key)
    {
        if (!Data.ContainsKey(key)) return;
        var (opos, ipos) = Data[key];
        int value = opos.Value.Value - 1;
        opos.Value.Keys.Remove(ipos);
        if (value == 0) Data.Remove(key);
        else
        {
            var onpos = opos.Previous;
            LinkedListNode<Item> pos;
            if (onpos != null && onpos.Value.Value == value)
                pos = onpos;
            else if (opos.Value.Keys.Count != 1)
                pos = List.AddBefore(opos, new Item(value));
            else
            {
                opos.Value = new Item(value) { Keys = opos.Value.Keys };
                return;
            }
            pos.Value.Keys.AddFirst(ipos);
            Data[key] = new Nodes(pos, pos.Value.Keys.First);
        }
        if (opos.Value.Keys.Count == 0)
            List.Remove(opos);
    }

    
    public string GetMaxKey()
    {
        return List.Count == 0 ? "" : List.Last.Value.Keys.First.Value;
    }

    
    public string GetMinKey()
    {
        return List.Count == 0 ? "" : List.First.Value.Keys.First.Value;
    }
}

*/