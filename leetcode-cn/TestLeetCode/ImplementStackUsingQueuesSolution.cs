using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
使用队列实现栈的下列操作：

push(x) -- 元素 x 入栈
pop() -- 移除栈顶元素
top() -- 获取栈顶元素
empty() -- 返回栈是否为空
注意:

你只能使用队列的基本操作-- 也就是 push to back, peek/pop from front, size, 和 is empty 这些操作是合法的。
你所使用的语言也许不支持队列。 你可以使用 list 或者 deque（双端队列）来模拟一个队列 , 只要是标准的队列操作即可。
你可以假设所有操作都是有效的（例如, 对一个空的栈不会调用 pop 或者 top 操作）。

*/
/// <summary>
/// https://leetcode-cn.com/problems/implement-stack-using-queues/
/// 225. 用队列实现栈
/// 
/// 
/// 
/// 
/// 
/// </summary>
class ImplementStackUsingQueuesSolution
{
    public ImplementStackUsingQueuesSolution() {

    }

    private List<int> _list = new List<int>();
    public void Push(int x) {
        _list.Insert(0, x);
    }
    
    public int Pop() {
        var ret = _list[0];
        _list.RemoveAt(0);
        return ret;
    }
    
    public int Top() {
        return _list[0];
    }
    
    public bool Empty() {
        return _list.Count == 0;
    }

}
/*
用队列实现栈
力扣 (LeetCode)
发布于 2019-06-24
50.3k
绪论
这篇文章是为初级读者准备的，文章中介绍栈和队列这两种数据结构。

方法一 （两个队列，压入 -O(1)O(1)， 弹出 -O(n)O(n)）
思路

栈是一种 后进先出（last in - first out， LIFO）的数据结构，栈内元素从顶端压入（push），从顶端弹出（pop）。一般我们用数组或者链表来实现栈，但是这篇文章会来介绍如何用队列来实现栈。队列是一种与栈相反的 先进先出（first in - first out， FIFO）的数据结构，队列中元素只能从 后端（rear）入队（push），然后从 前端（front）端出队（pop）。为了满足栈的特性，我们需要维护两个队列 q1 和 q2。同时，我们用一个额外的变量来保存栈顶元素。

算法

压入（push）

新元素永远从 q1 的后端入队，同时 q1 的后端也是栈的 栈顶（top）元素。

Push an element in stack

Figure 1. Push an element in stack


private Queue<Integer> q1 = new LinkedList<>();
private Queue<Integer> q2 = new LinkedList<>();
private int top;

// Push element x onto stack.
public void push(int x) {
    q1.add(x);
    top = x;
}
复杂度分析

时间复杂度：O(1)O(1)
队列是通过链表来实现的，入队（add）操作的时间复杂度为 O(1)O(1)。

空间复杂度：O(1)O(1)

弹出（pop）

我们需要把栈顶元素弹出，就是 q1 中最后入队的元素。

考虑到队列是一种 FIFO 的数据结构，最后入队的元素应该在最后被出队。因此我们需要维护另外一个队列 q2，这个队列用作临时存储 q1 中出队的元素。q2 中最后入队的元素将作为新的栈顶元素。接着将 q1 中最后剩下的元素出队。我们通过把 q1 和 q2 互相交换的方式来避免把 q2 中的元素往 q1 中拷贝。

Pop an element from stack

Figure 2. Pop an element from stack


// Removes the element on top of the stack.
public void pop() {
    while (q1.size() > 1) {
        top = q1.remove();
        q2.add(top);
    }
    q1.remove();
    Queue<Integer> temp = q1;
    q1 = q2;
    q2 = temp;
}
复杂度分析

时间复杂度：O(n)O(n)
算法让 q1 中的 nn 个元素出队，让 n - 1n−1 个元素从 q2 入队，在这里 nn 是栈的大小。这个过程总共产生了 2n - 12n−1 次操作，时间复杂度为 O(n)O(n)。
方法二 （两个队列， 压入 - O(n)O(n)， 弹出 - O(1)O(1)）
算法

压入（push)

接下来介绍的算法让每一个新元素从 q2 入队，同时把这个元素作为栈顶元素保存。当 q1 非空（也就是栈非空），我们让 q1 中所有的元素全部出队，再将出队的元素从 q2 入队。通过这样的方式，新元素（栈中的栈顶元素）将会在 q2 的前端。我们通过将 q1， q2 互相交换的方式来避免把 q2 中的元素往 q1 中拷贝。

Push an element in stack

Figure 3. Push an element in stack


public void push(int x) {
    q2.add(x);
    top = x;
    while (!q1.isEmpty()) {                
        q2.add(q1.remove());
    }
    Queue<Integer> temp = q1;
    q1 = q2;
    q2 = temp;
}
复杂度分析

时间复杂度：O(n)O(n)
算法会让 q1 出队 nn 个元素，同时入队 n + 1n+1 个元素到 q2。这个过程会产生 2n + 12n+1 步操作，同时链表中 插入 操作和 移除 操作的时间复杂度为 O(1)O(1)，因此时间复杂度为 O(n)O(n)。

空间复杂度：O(1)O(1)

弹出（pop）

直接让 q1 中元素出队，同时将出队后的 q1 中的队首元素作为栈顶元素保存。

Pop an element from stack

Figure 4. Pop an element from stack


// Removes the element on top of the stack.
public int pop() {
    q1.remove();
    int res = top;
    if (!q1.isEmpty()) {
        top = q1.peek();
    }
    return res;
}
复杂度分析

时间复杂度：O(1)O(1)
空间复杂度：O(1)O(1)
判断空（empty）和 取栈顶元素（top）是同样的实现方式。

判断空（empty）

q1 里包含了栈中所有的元素，所以只需要检查 q1 是否为空就可以了。


// Returns whether the stack is empty.
public boolean empty() {
    return q1.isEmpty();
}
时间复杂度：O(1)O(1)
空间复杂度：O(1)O(1)

取栈顶元素（top）

栈顶元素被保存在 top 变量里，每次我们 压入 或者 弹出 一个元素的时候都会随之更新这个变量。


// Get the top element.
public int top() {
    return top;
}
时间复杂度：O(1)O(1)
栈顶元素每次都是被提前计算出来的，同时只有 top 操作可以得到它的值。

空间复杂度：O(1)O(1)

方法三 （一个队列， 压入 - O(n)O(n)， 弹出 - O(1)O(1)）
上面介绍的两个方法都有一个缺点，它们都用到了两个队列。下面介绍的方法只需要使用一个队列。

算法

压入（push）

当我们将一个元素从队列入队的时候，根据队列的性质这个元素会存在队列的后端。

但当我们实现一个栈的时候，最后入队的元素应该在前端，而不是在后端。为了实现这个目的，每当入队一个新元素的时候，我们可以把队列的顺序反转过来。

Push an element in stack

Figure 5. Push an element in stack


private LinkedList<Integer> q1 = new LinkedList<>();

// Push element x onto stack.
public void push(int x) {
    q1.add(x);
    int sz = q1.size();
    while (sz > 1) {
        q1.add(q1.remove());
        sz--;
    }
}
复杂度分析

时间复杂度：O(n)O(n)
这个算法需要从 q1 中出队 nn 个元素，同时还需要入队 nn 个元素到 q1，其中 nn 是栈的大小。这个过程总共产生了 2n + 12n+1 步操作。链表中 插入 操作和 移除 操作的时间复杂度为 O(1)O(1)，因此时间复杂度为 O(n)O(n)。

空间复杂度：O(1)O(1)

弹出（pop）

最后一个压入的元素永远在 q1 的前端，这样的话我们就能在常数时间内让它 出队。


// Removes the element on top of the stack.
public void pop() {
    q1.remove();
}
复杂度分析

时间复杂度：O(1)O(1)
空间复杂度：O(1)O(1)
判断空（empty）

q1 中包含了栈中所有的元素，所以只需要检查 q1 是否为空就可以了。


// Return whether the stack is empty.
public boolean empty() {
    return q1.isEmpty();
}
时间复杂度：O(1)O(1)
空间复杂度：O(1)O(1)

取栈顶（top）

栈顶元素永远在 q1 的前端，直接返回就可以了。


// Get the top element.
public int top() {
    return q1.peek();
}
时间复杂度：O(1)O(1)
空间复杂度：O(1)O(1)

public class MyStack
{ 
	private Queue<int> queA;
	private Queue<int> queB; 
	public MyStack()
	{
		  queA = new Queue<int>(); 
		  queB = new Queue<int>();
	}

	public void Push(int x)
	{
		queA.Enqueue(x);
		queB = revQueA(queA);
	}

	public int Pop()
	{
		int res= queB.Dequeue();
		queA = revQueB(queB);
		return res;
	}


	public int Top()
	{
		return queB.Peek();
	}


	public bool Empty()
	{
		return queB.Count == 0 ? true : false;
	}

	private Queue<int>  revQueA(Queue<int> A)
	{
		int[] arr = A.ToArray(); 
		queB.Clear();
		for (int i = A.Count-1; i >=0 ; i--)
			queB.Enqueue(arr[i]);

		return queB;
	}

	private Queue<int> revQueB(Queue<int> B)
	{
		int[] arr = B.ToArray();
		queA.Clear();
		for (int i = B.Count - 1; i >= 0; i--)
			queA.Enqueue(arr[i]);

		return queA;
	}
}



*/
