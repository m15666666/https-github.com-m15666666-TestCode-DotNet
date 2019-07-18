using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
设计实现双端队列。
你的实现需要支持以下操作：

MyCircularDeque(k)：构造函数,双端队列的大小为k。
insertFront()：将一个元素添加到双端队列头部。 如果操作成功返回 true。
insertLast()：将一个元素添加到双端队列尾部。如果操作成功返回 true。
deleteFront()：从双端队列头部删除一个元素。 如果操作成功返回 true。
deleteLast()：从双端队列尾部删除一个元素。如果操作成功返回 true。
getFront()：从双端队列头部获得一个元素。如果双端队列为空，返回 -1。
getRear()：获得双端队列的最后一个元素。 如果双端队列为空，返回 -1。
isEmpty()：检查双端队列是否为空。
isFull()：检查双端队列是否满了。
示例：

MyCircularDeque circularDeque = new MycircularDeque(3); // 设置容量大小为3
circularDeque.insertLast(1);			        // 返回 true
circularDeque.insertLast(2);			        // 返回 true
circularDeque.insertFront(3);			        // 返回 true
circularDeque.insertFront(4);			        // 已经满了，返回 false
circularDeque.getRear();  				// 返回 2
circularDeque.isFull();				        // 返回 true
circularDeque.deleteLast();			        // 返回 true
circularDeque.insertFront(4);			        // 返回 true
circularDeque.getFront();				// 返回 4
 
 * Your MyCircularDeque object will be instantiated and called as such:
 * MyCircularDeque obj = new MyCircularDeque(k);
 * bool param_1 = obj.InsertFront(value);
 * bool param_2 = obj.InsertLast(value);
 * bool param_3 = obj.DeleteFront();
 * bool param_4 = obj.DeleteLast();
 * int param_5 = obj.GetFront();
 * int param_6 = obj.GetRear();
 * bool param_7 = obj.IsEmpty();
 * bool param_8 = obj.IsFull();
 

提示：

所有值的范围为 [1, 1000]
操作次数的范围为 [1, 1000]
请不要使用内置的双端队列库。 
*/
/// <summary>
/// https://leetcode-cn.com/problems/design-circular-deque/
/// 641. 设计循环双端队列
/// 
/// </summary>
class DesignCircularDequeSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    /** Initialize your data structure here. Set the size of the deque to be k. */
    public DesignCircularDequeSolution(int k)
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
        Count = 0;
    }
    private int Count { get; set; }
    /** Adds an item at the front of Deque. Return true if the operation is successful. */
    public bool InsertFront(int value)
    {
        if (IsFull()) return false;

        if ( IsEmpty() )
        {
            _headIndex = _tailIndex = 0;
            _buffer[0] = value;
            Count = 1;
        }
        else
        {
            _buffer[DecreaseIndex(ref _headIndex)] = value;
            Count++;
        }
        return true;
    }

    /** Adds an item at the rear of Deque. Return true if the operation is successful. */
    public bool InsertLast(int value)
    {
        if (IsFull()) return false;

        if ( IsEmpty() )
        {
            _headIndex = _tailIndex = 0;
            _buffer[0] = value;
            Count = 1;
        }
        else
        {
            _buffer[IncreaseIndex(ref _tailIndex)] = value;
            Count++;
        }
        return true;
    }

    /** Deletes an item from the front of Deque. Return true if the operation is successful. */
    public bool DeleteFront()
    {
        if (IsEmpty()) return false;

        IncreaseIndex(ref _headIndex);
        Count--;

        return true;
    }

    /** Deletes an item from the rear of Deque. Return true if the operation is successful. */
    public bool DeleteLast()
    {
        if (IsEmpty()) return false;

        DecreaseIndex(ref _tailIndex);
        Count--;

        return true;
    }

    private int DecreaseIndex( ref int index )
    {
        var newIndex = index - 1;
        if (newIndex < 0) newIndex += _capacity;
        index = newIndex;
        return index;
    }
    private int IncreaseIndex(ref int index)
    {
        index = (index + 1) % _capacity;
        return index;
    }

    /** Get the front item from the deque. */
    public int GetFront()
    {
        return IsEmpty() ? -1 : _buffer[_headIndex];
    }

    /** Get the last item from the deque. */
    public int GetRear()
    {
        return IsEmpty() ? -1 : _buffer[_tailIndex];
    }

    /** Checks whether the circular deque is empty or not. */
    public bool IsEmpty()
    {
        return Count == 0;
    }

    /** Checks whether the circular deque is full or not. */
    public bool IsFull()
    {
        return Count == _capacity;
    }
}
/*
public class MyCircularDeque {

    int size;
    Stack<int> frontStack;
    Stack<int> lastStack;
    public MyCircularDeque(int k)
    {
        size = k;
        frontStack = new Stack<int>();
        lastStack = new Stack<int>();
    }

    public bool InsertFront(int value)
    {
        // Console.WriteLine("InsertFront");
        if (frontStack.Count() + lastStack.Count() >= size)
        {
            return false;
        }
        else
        {
            frontStack.Push(value);
            return true;
        }
    }

    public bool InsertLast(int value)
    {
        // Console.WriteLine("InsertLast");
        if (frontStack.Count() + lastStack.Count() >= size)
        {
            return false;
        }
        else
        {
            lastStack.Push(value);
            return true;
        }
    }

    public bool DeleteFront()
    {
        // Console.WriteLine("DeleteFront");
        if (frontStack.Count() == 0)
        {
            while (lastStack.Count() > 0)
            {
                frontStack.Push(lastStack.Pop());
            }
        }

        if (frontStack.Count() > 0)
        {
            frontStack.Pop();
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool DeleteLast()
    {
        // Console.WriteLine("DeleteLast");
        if (lastStack.Count() == 0)
        {
            while (frontStack.Count() > 0)
            {
                lastStack.Push(frontStack.Pop());
            }
        }

        if (lastStack.Count() > 0)
        {
            lastStack.Pop();
            return true;
        }
        else
        {
            return false;
        }
    }

    public int GetFront()
    {
        // Console.WriteLine("GetFront");
        if (frontStack.Count() == 0)
        {
            while (lastStack.Count() > 0)
            {
                frontStack.Push(lastStack.Pop());
            }
        }

        if (frontStack.Count() > 0)
            return frontStack.Peek();
        else
            return -1;
    }

    public int GetRear()
    {
        // Console.WriteLine("GetRear");
        if (lastStack.Count() == 0)
        {
            while (frontStack.Count() > 0)
            {
                lastStack.Push(frontStack.Pop());
            }
        }

        if (lastStack.Count() > 0)
            return lastStack.Peek();
        else
            return -1;
    }

    public bool IsEmpty()
    {
        // Console.WriteLine("IsEmpty");
        return frontStack.Count() == 0 && lastStack.Count() == 0;
    }

    public bool IsFull()
    {
        // Console.WriteLine("IsFull");
        int count = frontStack.Count() + lastStack.Count();
        return count == size;
    }
} 

*/