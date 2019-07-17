using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
设计你的循环队列实现。 循环队列是一种线性数据结构，其操作表现基于 FIFO（先进先出）原则并且队尾被连接在队首之后以形成一个循环。它也被称为“环形缓冲器”。

循环队列的一个好处是我们可以利用这个队列之前用过的空间。在一个普通队列里，一旦一个队列满了，
我们就不能插入下一个元素，即使在队列前面仍有空间。但是使用循环队列，我们能使用这些空间去存储新的值。

你的实现应该支持如下操作：

MyCircularQueue(k): 构造器，设置队列长度为 k 。
Front: 从队首获取元素。如果队列为空，返回 -1 。
Rear: 获取队尾元素。如果队列为空，返回 -1 。
enQueue(value): 向循环队列插入一个元素。如果成功插入则返回真。
deQueue(): 从循环队列中删除一个元素。如果成功删除则返回真。
isEmpty(): 检查循环队列是否为空。
isFull(): 检查循环队列是否已满。
 

示例：

MyCircularQueue circularQueue = new MycircularQueue(3); // 设置长度为 3

circularQueue.enQueue(1);  // 返回 true

circularQueue.enQueue(2);  // 返回 true

circularQueue.enQueue(3);  // 返回 true

circularQueue.enQueue(4);  // 返回 false，队列已满

circularQueue.Rear();  // 返回 3

circularQueue.isFull();  // 返回 true

circularQueue.deQueue();  // 返回 true

circularQueue.enQueue(4);  // 返回 true

circularQueue.Rear();  // 返回 4
 
提示：

所有的值都在 0 至 1000 的范围内；
操作数将在 1 至 1000 的范围内；
请不要使用内置的队列库。
*/
/// <summary>
/// https://leetcode-cn.com/problems/design-circular-queue/
/// 622. 设计循环队列
/// 
/// </summary>
class DesignCircularQueueSolution
{

    /**
     * Your MyCircularQueue object will be instantiated and called as such:
     * MyCircularQueue obj = new MyCircularQueue(k);
     * bool param_1 = obj.EnQueue(value);
     * bool param_2 = obj.DeQueue();
     * int param_3 = obj.Front();
     * int param_4 = obj.Rear();
     * bool param_5 = obj.IsEmpty();
     * bool param_6 = obj.IsFull();
     */
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    /** Initialize your data structure here. Set the size of the queue to be k. */
    public DesignCircularQueueSolution(int k)
    {
        _buffer = new int[k];
        _capacity = k;
        ResetIndex();
    }

    private int[] _buffer;
    private int _headIndex;
    private int _tailIndex;
    private int _capacity;
    private void ResetIndex()
    {
        _headIndex = _tailIndex = -1;
    }
    private int Count => _headIndex == -1 ? 0 : _tailIndex - _headIndex + 1;
    
    /** Insert an element into the circular queue. Return true if the operation is successful. */
    public bool EnQueue(int value)
    {
        if (IsFull()) return false;

        _buffer[(++_tailIndex) % _capacity] = value;
        if(_headIndex == -1)
        {
            _headIndex = _tailIndex;
        }
        else if( _capacity <= _headIndex && _capacity <= _tailIndex )
        {
            _headIndex -= _capacity;
            _tailIndex -= _capacity;
        }
        return true;
    }

    /** Delete an element from the circular queue. Return true if the operation is successful. */
    public bool DeQueue()
    {
        if (IsEmpty()) return false;

        ++_headIndex;
        if (IsEmpty()) ResetIndex();

        return true;
    }

    /** Get the front item from the queue. */
    public int Front()
    {
        return IsEmpty() ? -1 : _buffer[_headIndex % _capacity];
    }

    /** Get the last item from the queue. */
    public int Rear()
    {
        return IsEmpty() ? -1 : _buffer[_tailIndex % _capacity];
    }

    /** Checks whether the circular queue is empty or not. */
    public bool IsEmpty()
    {
        return Count == 0;
    }

    /** Checks whether the circular queue is full or not. */
    public bool IsFull()
    {
        return Count == _capacity;
    }
}
/*
public class MyCircularQueue {

    private int[] data;
    private int p_start, p_end, p_size;
    public MyCircularQueue(int k)
    {
        data = new int[k];
        p_start = -1;
        p_end = -1;
        p_size = k;
    }

    public bool EnQueue(int value)
    {
        if (IsFull())
        {
            return false;
        }
        if (IsEmpty())
        {
            p_start = 0;
        }
        p_end = (p_end + 1) % p_size;
        data[p_end] = value;
        return true;
    }

    public bool DeQueue()
    {
        if (IsEmpty())
            return false;
        if (p_start == p_end)
        {
            p_start = -1;
            p_end = -1;
            return true;
        }
        p_start = (p_start + 1) % p_size;
        return true;
    }

    public int Front()
    {
        if (IsEmpty())
        {
            return -1;
        }
        else
            return data[p_start];
    }

    public int Rear()
    {
        if (IsEmpty())
        {
            return -1;
        }
        else
            return data[p_end];
    }

    public bool IsEmpty()
    {
        return p_start == -1;
    }

    public bool IsFull()
    {
        return (p_end + 1) % p_size == p_start;
    }
} 


*/