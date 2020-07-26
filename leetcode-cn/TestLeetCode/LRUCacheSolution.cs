using System.Collections.Generic;

/*
运用你所掌握的数据结构，设计和实现一个  LRU (最近最少使用) 缓存机制。它应该支持以下操作： 获取数据 get 和 写入数据 put 。

获取数据 get(key) - 如果关键字 (key) 存在于缓存中，则获取关键字的值（总是正数），否则返回 -1。
写入数据 put(key, value) - 如果关键字已经存在，则变更其数据值；如果关键字不存在，则插入该组「关键字/值」。当缓存容量达到上限时，它应该在写入新数据之前删除最久未使用的数据值，从而为新的数据值留出空间。

 

进阶:

你是否可以在 O(1) 时间复杂度内完成这两种操作？

 

示例:

LRUCache cache = new LRUCache( 2  );

cache.put(1, 1);
cache.put(2, 2);
cache.get(1);       // 返回  1
cache.put(3, 3);    // 该操作会使得关键字 2 作废
cache.get(2);       // 返回 -1 (未找到)
cache.put(4, 4);    // 该操作会使得关键字 1 作废
cache.get(1);       // 返回 -1 (未找到)
cache.get(3);       // 返回  3
cache.get(4);       // 返回  4
*/

/// <summary>
/// https://leetcode-cn.com/problems/lru-cache/
/// 146. LRU缓存机制
///
///
///
/// </summary>
internal class LRUCacheSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public LRUCacheSolution(int capacity)
    {
        _size = 0;
        _capacity = capacity;
        _head = new DLinkedNode();
        _tail = new DLinkedNode();
        _head.Next = _tail;
        _tail.Prev = _head;
    }

    public int Get(int key)
    {
        DLinkedNode node = _cache.ContainsKey(key) ? _cache[key] : null;
        if (node == null) return -1;

        MoveToHead(node);
        return node.Value;
    }

    public void Put(int key, int value)
    {
        DLinkedNode node = _cache.ContainsKey(key) ? _cache[key] : null;
        if (node == null)
        {
            DLinkedNode newNode = new DLinkedNode(key, value);
            _cache[key] = newNode;
            AddToHead(newNode);
            ++_size;
            if (_capacity < _size)
            {
                DLinkedNode tail = RemoveTail();
                _cache.Remove(tail.Key);
                --_size;
            }
            return;
        }
        node.Value = value;
        MoveToHead(node);
    }

    private void AddToHead(DLinkedNode node)
    {
        node.Prev = _head;
        node.Next = _head.Next;
        _head.Next.Prev = node;
        _head.Next = node;
    }

    private void RemoveNode(DLinkedNode node)
    {
        node.Prev.Next = node.Next;
        node.Next.Prev = node.Prev;
    }

    private void MoveToHead(DLinkedNode node)
    {
        RemoveNode(node);
        AddToHead(node);
    }

    private DLinkedNode RemoveTail()
    {
        DLinkedNode res = _tail.Prev;
        RemoveNode(res);
        return res;
    }

    private Dictionary<int, DLinkedNode> _cache = new Dictionary<int, DLinkedNode>();
    private int _size;
    private int _capacity;
    private DLinkedNode _head, _tail;

    public class DLinkedNode
    {
        public int Key;
        public int Value;
        public DLinkedNode Prev;
        public DLinkedNode Next;

        public DLinkedNode() { }

        public DLinkedNode(int key, int value)
        {
            Key = key; Value = value;
        }
    }
}
/*

LRU缓存机制
力扣官方题解
发布于 2020-05-24
35.5k
📺视频题解

📖文字题解
前言
实现本题的两种操作，需要用到一个哈希表和一个双向链表。在面试中，面试官一般会期望读者能够自己实现一个简单的双向链表，而不是使用语言自带的、封装好的数据结构。在 Python 语言中，有一种结合了哈希表与双向链表的数据结构 OrderedDict，只需要短短的几行代码就可以完成本题。在 Java 语言中，同样有类似的数据结构 LinkedHashMap。这些做法都不会符合面试官的要求，因此下面只给出使用封装好的数据结构实现的代码，而不多做任何阐述。


class LRUCache extends LinkedHashMap<Integer, Integer>{
    private int capacity;
    
    public LRUCache(int capacity) {
        super(capacity, 0.75F, true);
        this.capacity = capacity;
    }

    public int get(int key) {
        return super.getOrDefault(key, -1);
    }

    public void put(int key, int value) {
        super.put(key, value);
    }

    @Override
    protected boolean removeEldestEntry(Map.Entry<Integer, Integer> eldest) {
        return size() > capacity; 
    }
}
方法一：哈希表 + 双向链表
算法

LRU 缓存机制可以通过哈希表辅以双向链表实现，我们用一个哈希表和一个双向链表维护所有在缓存中的键值对。

双向链表按照被使用的顺序存储了这些键值对，靠近头部的键值对是最近使用的，而靠近尾部的键值对是最久未使用的。

哈希表即为普通的哈希映射（HashMap），通过缓存数据的键映射到其在双向链表中的位置。

这样以来，我们首先使用哈希表进行定位，找出缓存项在双向链表中的位置，随后将其移动到双向链表的头部，即可在 O(1)O(1) 的时间内完成 get 或者 put 操作。具体的方法如下：

对于 get 操作，首先判断 key 是否存在：

如果 key 不存在，则返回 -1−1；

如果 key 存在，则 key 对应的节点是最近被使用的节点。通过哈希表定位到该节点在双向链表中的位置，并将其移动到双向链表的头部，最后返回该节点的值。

对于 put 操作，首先判断 key 是否存在：

如果 key 不存在，使用 key 和 value 创建一个新的节点，在双向链表的头部添加该节点，并将 key 和该节点添加进哈希表中。然后判断双向链表的节点数是否超出容量，如果超出容量，则删除双向链表的尾部节点，并删除哈希表中对应的项；

如果 key 存在，则与 get 操作类似，先通过哈希表定位，再将对应的节点的值更新为 value，并将该节点移到双向链表的头部。

上述各项操作中，访问哈希表的时间复杂度为 O(1)O(1)，在双向链表的头部添加节点、在双向链表的尾部删除节点的复杂度也为 O(1)O(1)。而将一个节点移到双向链表的头部，可以分成「删除该节点」和「在双向链表的头部添加节点」两步操作，都可以在 O(1)O(1) 时间内完成。

小贴士

在双向链表的实现中，使用一个伪头部（dummy head）和伪尾部（dummy tail）标记界限，这样在添加节点和删除节点的时候就不需要检查相邻的节点是否存在。




public class LRUCache {
    class DLinkedNode {
        int key;
        int value;
        DLinkedNode prev;
        DLinkedNode next;
        public DLinkedNode() {}
        public DLinkedNode(int _key, int _value) {key = _key; value = _value;}
    }

    private Map<Integer, DLinkedNode> cache = new HashMap<Integer, DLinkedNode>();
    private int size;
    private int capacity;
    private DLinkedNode head, tail;

    public LRUCache(int capacity) {
        this.size = 0;
        this.capacity = capacity;
        // 使用伪头部和伪尾部节点
        head = new DLinkedNode();
        tail = new DLinkedNode();
        head.next = tail;
        tail.prev = head;
    }

    public int get(int key) {
        DLinkedNode node = cache.get(key);
        if (node == null) {
            return -1;
        }
        // 如果 key 存在，先通过哈希表定位，再移到头部
        moveToHead(node);
        return node.value;
    }

    public void put(int key, int value) {
        DLinkedNode node = cache.get(key);
        if (node == null) {
            // 如果 key 不存在，创建一个新的节点
            DLinkedNode newNode = new DLinkedNode(key, value);
            // 添加进哈希表
            cache.put(key, newNode);
            // 添加至双向链表的头部
            addToHead(newNode);
            ++size;
            if (size > capacity) {
                // 如果超出容量，删除双向链表的尾部节点
                DLinkedNode tail = removeTail();
                // 删除哈希表中对应的项
                cache.remove(tail.key);
                --size;
            }
        }
        else {
            // 如果 key 存在，先通过哈希表定位，再修改 value，并移到头部
            node.value = value;
            moveToHead(node);
        }
    }

    private void addToHead(DLinkedNode node) {
        node.prev = head;
        node.next = head.next;
        head.next.prev = node;
        head.next = node;
    }

    private void removeNode(DLinkedNode node) {
        node.prev.next = node.next;
        node.next.prev = node.prev;
    }

    private void moveToHead(DLinkedNode node) {
        removeNode(node);
        addToHead(node);
    }

    private DLinkedNode removeTail() {
        DLinkedNode res = tail.prev;
        removeNode(res);
        return res;
    }
}
复杂度分析

时间复杂度：对于 put 和 get 都是 O(1)O(1)。

空间复杂度：O(\text{capacity})O(capacity)，因为哈希表和双向链表最多存储 \text{capacity} + 1capacity+1 个元素。

下一篇：源于 LinkedHashMap源码

public class LRUCache
    {
        private DoubleList list;
        private Dictionary<int, Node> dict;
        private int capacity;
        public LRUCache(int capacity)
        {
            this.capacity = capacity;
            list = new DoubleList();
            dict = new Dictionary<int, Node>();
        }

        public int Get(int key)
        {
            if (!dict.ContainsKey(key))
                return -1;

            var node = dict[key];
            var res = node.Val;

            list.Remove(node);
            list.AddFirst(node);

            return res;
        }

        public void Put(int key, int value)
        {
            if (dict.ContainsKey(key))
            {
                var node = dict[key];
                node.Val = value;
                list.Remove(node);
                list.AddFirst(node);
            }
            else 
            {
                var node = new Node { Key = key, Val = value };
                if (list.Size() == capacity)
                {
                    var tail = list.RemoveLast();
                    dict.Remove(tail.Key);
                    list.AddFirst(node);
                }
                else 
                {
                    list.AddFirst(node);
                }
                dict[key] = node;
            }
        }
    }

    public class Node 
    {
        public int Key { get; set; }
        public int Val { get; set; }
        public Node Pre { get; set; }
        public Node Next { get; set; }
    }

    public class DoubleList 
    {
        private Node head;
        private Node tail;
        private int size;

        public void AddFirst(Node x) 
        {
            if (head == null) 
            {
                head = x;
                tail = x;
            }
            else
            {
                head.Pre = x;
                x.Next = head;
                head = x;
            }
            size++;
        }

        public Node RemoveLast() 
        {
            var node = tail;
            if (node == head)
            {
                head = null;
                tail = null;
            }
            else 
            {
                tail.Pre.Next = null;
                tail = tail.Pre;
            }

            size--;

            return node;
        }

        public void Remove(Node x) 
        {
            if (x == head)
            {
                head = x.Next;
            }
            else if (x == tail)
            {
                tail = x.Pre;
            }
            else 
            {
                x.Pre.Next = x.Next;
                x.Next.Pre = x.Pre;
            }
            size--;
        }

        public int Size() 
        {
            return size;
        }
    }


public class LRUCache {

        class DLinkedNode
        {
            internal int _key;
            internal int _value;
            internal DLinkedNode _prev;
            internal DLinkedNode _next;

            public DLinkedNode()
            {
            }

            public DLinkedNode(int key, int value)
            {
                _key = key;
                _value = value;
            }
        }

        private Dictionary<int, DLinkedNode> _cache
            = new Dictionary<int, DLinkedNode>();

        private int _size;
        private int _capacity;
        private DLinkedNode _head, _tail;

        public LRUCache(int capacity)
        {
            _size = 0;
            _capacity = capacity;
            // 使用伪头部和伪尾部节点
            _head = new DLinkedNode();
            _tail = new DLinkedNode();
            _head._next = _tail;
            _tail._prev = _head;
        }

        public int Get(int key)
        {
            if (_cache.TryGetValue(key, out var node))
            {
                // 如果 key 存在，先通过哈希表定位，再移动到头部
                MoveToHead(node);
                return node._value;
            }

            return -1;
        }

        public void Put(int key, int value)
        {
            if (_cache.TryGetValue(key, out var node))
            {
                // 如果 Key 存在，先通过哈希表定位，再修改 value，并移动到头部
                node._value = value;
                MoveToHead(node);
            }
            else
            {
                ++_size;
                // 如果 Key 不存在，创建一个新的节点
                var newNode = new DLinkedNode(key, value);
                // 添加进哈希表
                _cache.Add(key, newNode);
                // 添加至双向链表头部
                AddToHead(newNode);
                if (_size > _capacity)
                {
                    // 如果超出容量，删除双向链表的尾部节点
                    var tail = RemoveTail();
                    // 删除哈希表中对应的项
                    _cache.Remove(tail._key);
                    --_size;
                }
            }
        }


        private void AddToHead(DLinkedNode newNode)
        {
            newNode._prev = _head;
            newNode._next = _head._next;
            _head._next._prev = newNode;
            _head._next = newNode;
        }

        private void RemoveNode(DLinkedNode node)
        {
            node._prev._next = node._next;
            node._next._prev = node._prev;
        }

        private void MoveToHead(DLinkedNode node)
        {
            RemoveNode(node);
            AddToHead(node);
        }

        private DLinkedNode RemoveTail()
        {
            var node = _tail._prev;
            RemoveNode(node);
            return node;
        }
}


public class LRUCache {

    int capacity;
    Node head, tail;
    Dictionary<int, Node> LRUDic;
    public class Node
    {
        public int key;
        public int value;
        public Node pre;
        public Node next;
    }

    public LRUCache(int capacity) {
        this.capacity = capacity;
        LRUDic = new Dictionary<int, Node>();
        head = new Node();
        tail = new Node();
        head.next = tail;
        tail.pre = head;
    }
    
    public int Get(int key)
    {
        if (LRUDic.ContainsKey(key))
        {
            int value = LRUDic[key].value;
            DelletNode(LRUDic[key]);
            AddNodeAtHead(LRUDic[key]);
            return value;
        }
        else
        {
            return -1;
        }
    }
    
    public void Put(int key, int value)
    {
        if (LRUDic.ContainsKey(key))
        {
            Node node = LRUDic[key];
            DelletNode(node);
            node.value = value;
            AddNodeAtHead(node);
            return;
        }
        if (capacity > LRUDic.Count)
        {
            Node node = new Node();
            node.key = key;
            node.value = value;
            LRUDic.Add(key, node);
            AddNodeAtHead(node);
        }
        else
        {
            Node node = LRUDic[tail.pre.key];
            LRUDic.Remove(DelletTail());
            node.key = key;
            node.value = value;
            AddNodeAtHead(node);
            LRUDic.Add(key, node);
        }
    }

    public void DelletNode(Node node)
    {
        node.pre.next = node.next;
        node.next.pre = node.pre;
    }

    public void AddNodeAtHead(Node node)
    {
        node.next = head.next;
        head.next.pre = node;
        head.next = node;
        node.pre = head;

    }

    public int DelletTail()
    {
        int key = tail.pre.key;
        Node node = tail.pre;
        node.pre.next = tail;
        tail.pre = node.pre;
        return key;
    }
}


*/