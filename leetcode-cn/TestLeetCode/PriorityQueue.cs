using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace TestLeetCode
{
    /// <summary>
    /// 优先队列
    /// https://stackoverflow.com/questions/102398/priority-queue-in-net
    /// </summary>
    class PriorityQueue
    {
        int total_size;
        SortedDictionary<int, Queue> storage;

        public PriorityQueue()
        {
            this.storage = new SortedDictionary<int, Queue>();
            this.total_size = 0;
        }

        public bool IsEmpty()
        {
            return (total_size == 0);
        }

        public object Dequeue()
        {
            if (IsEmpty())
            {
                throw new Exception("Please check that priorityQueue is not empty before dequeing");
            }
            else
                foreach (Queue q in storage.Values)
                {
                    // we use a sorted dictionary
                    if (q.Count > 0)
                    {
                        total_size--;
                        return q.Dequeue();
                    }
                }

            Debug.Assert(false, "not supposed to reach here. problem with changing total_size");

            return null; // not supposed to reach here.
        }

        // same as above, except for peek.

        public object Peek()
        {
            if (IsEmpty())
                throw new Exception("Please check that priorityQueue is not empty before peeking");
            else
                foreach (Queue q in storage.Values)
                {
                    if (q.Count > 0)
                        return q.Peek();
                }

            Debug.Assert(false, "not supposed to reach here. problem with changing total_size");

            return null; // not supposed to reach here.
        }

        public object Dequeue(int prio)
        {
            total_size--;
            return storage[prio].Dequeue();
        }

        public void Enqueue(object item, int prio)
        {
            if (!storage.ContainsKey(prio))
            {
                storage.Add(prio, new Queue());
            }
            storage[prio].Enqueue(item);
            total_size++;
        }
    }
}
/*
Ask Question
Asked 11 years, 6 months ago
Active 10 months ago
Viewed 176k times

213


63
Closed. This question is off-topic. It is not currently accepting answers.
Want to improve this question? Update the question so it's on-topic for Stack Overflow.

Closed 3 years ago.

I am looking for a .NET implementation of a priority queue or heap data structure

Priority queues are data structures that provide more flexibility than simple sorting, because they allow new elements to enter a system at arbitrary intervals. It is much more cost-effective to insert a new job into a priority queue than to re-sort everything on each such arrival.

The basic priority queue supports three primary operations:

Insert(Q,x). Given an item x with key k, insert it into the priority queue Q.
Find-Minimum(Q). Return a pointer to the item whose key value is smaller than any other key in the priority queue Q.
Delete-Minimum(Q). Remove the item from the priority queue Q whose key is minimum
Unless I am looking in the wrong place, there isn't one in the framework. Is anyone aware of a good one, or should I roll my own?

c# .net data-structures heap priority-queue
shareimprove this question
edited Nov 4 '15 at 15:46

Colonel Panic
107k6868 gold badges337337 silver badges412412 bronze badges
asked Sep 19 '08 at 14:43

Doug McClean
13.2k44 gold badges4141 silver badges6464 bronze badges
33
FYI I've developed an easy-to-use, highly optimized C# priority-queue, which can be found here. It was developed specifically for pathfinding applications (A*, etc.), but should work perfectly for any other application as well. I would post this as an answer, but the question has recently been closed... – BlueRaja - Danny Pflughoeft Jul 25 '13 at 20:32 
1
ParallelExtensionsExtras has a ConcurrentPriorityQueue code.msdn.microsoft.com/ParExtSamples – VoteCoffee Sep 18 '14 at 18:04
Shamelessly introduce PriorityQueue, as part of effort to port java concurrent API to .net for Spring.Net. It's both a heap and queue with full generic support. Binary can be downloaded here. – Kenneth Xu Dec 14 '14 at 1:08 
@BlueRaja-DannyPflughoeft Could you add that an an answer? – mafu Nov 20 '16 at 2:33
1
Just to summarize. There're no heap data structure in .net, neither in .net core now. Though Array.Sort users it for large numbers. Internal implementations exist. – Artyom Oct 17 '17 at 9:48
show 2 more comments
14 Answers
Active
Oldest
Votes

42

I like using the OrderedBag and OrderedSet classes in PowerCollections as priority queues.

shareimprove this answer
edited May 27 '19 at 20:10

BartoszKP
29.9k1212 gold badges8282 silver badges117117 bronze badges
answered Sep 19 '08 at 14:48

Ben Hoffstein
94.9k88 gold badges9797 silver badges117117 bronze badges
60
OrderedBag/OrderedSet do more work than necessary, they use a red-black tree instead of a heap. – Dan Berindei Nov 20 '09 at 14:08
3
@DanBerindei: not necessary work if you need make running calculation (delete old items), heap only support deleting min or max – Svisstack Jul 27 '14 at 12:09 
add a comment

69

You might like IntervalHeap from the C5 Generic Collection Library. To quote the user guide

Class IntervalHeap<T> implements interface IPriorityQueue<T> using an interval heap stored as an array of pairs. The FindMin and FindMax operations, and the indexer’s get-accessor, take time O(1). The DeleteMin, DeleteMax, Add and Update operations, and the indexer’s set-accessor, take time O(log n). In contrast to an ordinary priority queue, an interval heap offers both minimum and maximum operations with the same efficiency.

The API is simple enough

> var heap = new C5.IntervalHeap<int>();
> heap.Add(10);
> heap.Add(5);
> heap.FindMin();
5
Install from Nuget https://www.nuget.org/packages/C5 or GitHub https://github.com/sestoft/C5/

shareimprove this answer
edited Nov 4 '15 at 10:12

Colonel Panic
107k6868 gold badges337337 silver badges412412 bronze badges
answered Jul 11 '09 at 18:17

jaras
1,18322 gold badges1010 silver badges1212 bronze badges
3
This looks to be a very solid library and it comes with 1400 unit tests. – ECC-Dan Mar 26 '13 at 14:16
1
I tried to use it but it has serious flaws. IntervalHeap does not have a built-in concept of priority and forces you to implement IComparable or IComparer making it a sorted collection not a "Priority". Even worse there is no direct way to update priority of some previous entry!!! – morteza khosravi Mar 9 '18 at 19:21
add a comment

51

Here's my attempt at a .NET heap

public abstract class Heap<T> : IEnumerable<T>
{
    private const int InitialCapacity = 0;
    private const int GrowFactor = 2;
    private const int MinGrow = 1;

    private int _capacity = InitialCapacity;
    private T[] _heap = new T[InitialCapacity];
    private int _tail = 0;

    public int Count { get { return _tail; } }
    public int Capacity { get { return _capacity; } }

    protected Comparer<T> Comparer { get; private set; }
    protected abstract bool Dominates(T x, T y);

    protected Heap() : this(Comparer<T>.Default)
    {
    }

    protected Heap(Comparer<T> comparer) : this(Enumerable.Empty<T>(), comparer)
    {
    }

    protected Heap(IEnumerable<T> collection)
        : this(collection, Comparer<T>.Default)
    {
    }

    protected Heap(IEnumerable<T> collection, Comparer<T> comparer)
    {
        if (collection == null) throw new ArgumentNullException("collection");
        if (comparer == null) throw new ArgumentNullException("comparer");

        Comparer = comparer;

        foreach (var item in collection)
        {
            if (Count == Capacity)
                Grow();

            _heap[_tail++] = item;
        }

        for (int i = Parent(_tail - 1); i >= 0; i--)
            BubbleDown(i);
    }

    public void Add(T item)
    {
        if (Count == Capacity)
            Grow();

        _heap[_tail++] = item;
        BubbleUp(_tail - 1);
    }

    private void BubbleUp(int i)
    {
        if (i == 0 || Dominates(_heap[Parent(i)], _heap[i])) 
            return; //correct domination (or root)

        Swap(i, Parent(i));
        BubbleUp(Parent(i));
    }

    public T GetMin()
    {
        if (Count == 0) throw new InvalidOperationException("Heap is empty");
        return _heap[0];
    }

    public T ExtractDominating()
    {
        if (Count == 0) throw new InvalidOperationException("Heap is empty");
        T ret = _heap[0];
        _tail--;
        Swap(_tail, 0);
        BubbleDown(0);
        return ret;
    }

    private void BubbleDown(int i)
    {
        int dominatingNode = Dominating(i);
        if (dominatingNode == i) return;
        Swap(i, dominatingNode);
        BubbleDown(dominatingNode);
    }

    private int Dominating(int i)
    {
        int dominatingNode = i;
        dominatingNode = GetDominating(YoungChild(i), dominatingNode);
        dominatingNode = GetDominating(OldChild(i), dominatingNode);

        return dominatingNode;
    }

    private int GetDominating(int newNode, int dominatingNode)
    {
        if (newNode < _tail && !Dominates(_heap[dominatingNode], _heap[newNode]))
            return newNode;
        else
            return dominatingNode;
    }

    private void Swap(int i, int j)
    {
        T tmp = _heap[i];
        _heap[i] = _heap[j];
        _heap[j] = tmp;
    }

    private static int Parent(int i)
    {
        return (i + 1)/2 - 1;
    }

    private static int YoungChild(int i)
    {
        return (i + 1)*2 - 1;
    }

    private static int OldChild(int i)
    {
        return YoungChild(i) + 1;
    }

    private void Grow()
    {
        int newCapacity = _capacity*GrowFactor + MinGrow;
        var newHeap = new T[newCapacity];
        Array.Copy(_heap, newHeap, _capacity);
        _heap = newHeap;
        _capacity = newCapacity;
    }

    public IEnumerator<T> GetEnumerator()
    {
        return _heap.Take(Count).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

public class MaxHeap<T> : Heap<T>
{
    public MaxHeap()
        : this(Comparer<T>.Default)
    {
    }

    public MaxHeap(Comparer<T> comparer)
        : base(comparer)
    {
    }

    public MaxHeap(IEnumerable<T> collection, Comparer<T> comparer)
        : base(collection, comparer)
    {
    }

    public MaxHeap(IEnumerable<T> collection) : base(collection)
    {
    }

    protected override bool Dominates(T x, T y)
    {
        return Comparer.Compare(x, y) >= 0;
    }
}

public class MinHeap<T> : Heap<T>
{
    public MinHeap()
        : this(Comparer<T>.Default)
    {
    }

    public MinHeap(Comparer<T> comparer)
        : base(comparer)
    {
    }

    public MinHeap(IEnumerable<T> collection) : base(collection)
    {
    }

    public MinHeap(IEnumerable<T> collection, Comparer<T> comparer)
        : base(collection, comparer)
    {
    }

    protected override bool Dominates(T x, T y)
    {
        return Comparer.Compare(x, y) <= 0;
    }
}
Some tests:

[TestClass]
public class HeapTests
{
    [TestMethod]
    public void TestHeapBySorting()
    {
        var minHeap = new MinHeap<int>(new[] {9, 8, 4, 1, 6, 2, 7, 4, 1, 2});
        AssertHeapSort(minHeap, minHeap.OrderBy(i => i).ToArray());

        minHeap = new MinHeap<int> { 7, 5, 1, 6, 3, 2, 4, 1, 2, 1, 3, 4, 7 };
        AssertHeapSort(minHeap, minHeap.OrderBy(i => i).ToArray());

        var maxHeap = new MaxHeap<int>(new[] {1, 5, 3, 2, 7, 56, 3, 1, 23, 5, 2, 1});
        AssertHeapSort(maxHeap, maxHeap.OrderBy(d => -d).ToArray());

        maxHeap = new MaxHeap<int> {2, 6, 1, 3, 56, 1, 4, 7, 8, 23, 4, 5, 7, 34, 1, 4};
        AssertHeapSort(maxHeap, maxHeap.OrderBy(d => -d).ToArray());
    }

    private static void AssertHeapSort(Heap<int> heap, IEnumerable<int> expected)
    {
        var sorted = new List<int>();
        while (heap.Count > 0)
            sorted.Add(heap.ExtractDominating());

        Assert.IsTrue(sorted.SequenceEqual(expected));
    }
}
shareimprove this answer
answered Dec 8 '12 at 10:32

Ohad Schneider
30.1k1010 gold badges137137 silver badges181181 bronze badges
2
I would recommend clearing the heap value in ExtractDominating, so it doesn't hold on to the referenced object longer than necessary (potential memory leak). For value types it obviously is of no concern. – Wout Aug 3 '15 at 11:03
5
Nice but you can't remove items from it? That's an important operation for priority queues. – Tom Larkworthy Aug 5 '16 at 22:05
It looks like the underlying object is an array. Wouldn't this be better as a binary tree? – Grunion Shaftoe Jun 26 '18 at 17:03
1
@OhadSchneider very very cool, I was just looking into min heap and tried to do what you did making it generic and min or max heap! great work – Gilad Nov 24 '18 at 21:02
1
@Gilad IEqualityComparer<T> wouldn't be enough, as that would only tell you whether two items are equal, whereas you need to know the relation between them (who's smaller / larger). It's true that I could have used IComparer<T> though... – Ohad Schneider Sep 9 '19 at 0:03
show 3 more comments

23

here's one i just wrote, maybe it's not as optimized (just uses a sorted dictionary) but simple to understand. you can insert objects of different kinds, so no generic queues.

using System;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;

namespace PrioQueue
{
    public class PrioQueue
    {
        int total_size;
        SortedDictionary<int, Queue> storage;

        public PrioQueue ()
        {
            this.storage = new SortedDictionary<int, Queue> ();
            this.total_size = 0;
        }

        public bool IsEmpty ()
        {
            return (total_size == 0);
        }

        public object Dequeue ()
        {
            if (IsEmpty ()) {
                throw new Exception ("Please check that priorityQueue is not empty before dequeing");
            } else
                foreach (Queue q in storage.Values) {
                    // we use a sorted dictionary
                    if (q.Count > 0) {
                        total_size--;
                        return q.Dequeue ();
                    }
                }

                Debug.Assert(false,"not supposed to reach here. problem with changing total_size");

                return null; // not supposed to reach here.
        }

        // same as above, except for peek.

        public object Peek ()
        {
            if (IsEmpty ())
                throw new Exception ("Please check that priorityQueue is not empty before peeking");
            else
                foreach (Queue q in storage.Values) {
                    if (q.Count > 0)
                        return q.Peek ();
                }

                Debug.Assert(false,"not supposed to reach here. problem with changing total_size");

                return null; // not supposed to reach here.
        }

        public object Dequeue (int prio)
        {
            total_size--;
            return storage[prio].Dequeue ();
        }

        public void Enqueue (object item, int prio)
        {
            if (!storage.ContainsKey (prio)) {
                storage.Add (prio, new Queue ());
              }
            storage[prio].Enqueue (item);
            total_size++;

        }
    }
}
shareimprove this answer
edited Jan 24 '14 at 18:36
answered Feb 14 '11 at 17:03

kobi7
90911 gold badge66 silver badges1414 bronze badges
this doesnt allow for multiple entries with the same priority though? – Letseatlunch Apr 16 '11 at 16:39
2
it does. when you invoke the Enqueue method, it will add the item to the queue of that priority. (the part in else in the enqueue method.) – kobi7 Apr 19 '11 at 17:06 
5
What do you mean by "it's not really a priority queue in the computer science meaning"? What about it makes you believe that this isn't a priority queue? – Mark Byers Feb 23 '12 at 12:30 
13
-1 for not using generics. – cdiggins Jan 1 '13 at 20:49
2
One of the biggest benefits of Heap/PriorityQueue is the O(1) complexity of min/max extraction, i.e. the Peek operation. And here it involves enumerator setup, for-loop, etc. Why!? Also, the "Enqueue" operation rather than being O(logN) - another key feature of the heap, has one O(longN) swipe because of "ContainsKey", a second one (again O(longN)) to add the Queue entry (if needed), a third one to actually retrieve the Queue (the storage[prio] line), and finally a linear adding to that queue. This is truly insane in the light of core algorithm implementation. – Jonan Georgiev Nov 23 '16 at 10:09
show 6 more comments

10

I found one by Julian Bucknall on his blog here - http://www.boyet.com/Articles/PriorityQueueCSharp3.html

We modified it slightly so that low-priority items on the queue would eventually 'bubble-up' to the top over time, so they wouldn't suffer starvation.

shareimprove this answer
answered Oct 14 '08 at 13:36

Duncan
9,5801010 gold badges5858 silver badges9494 bronze badges
add a comment

8

As mentioned in Microsoft Collections for .NET, Microsoft has written (and shared online) 2 internal PriorityQueue classes within the .NET Framework. Their code is available to try out.

EDIT: As @mathusum-mut commented, there is a bug in one of Microsoft's internal PriorityQueue classes (the SO community has, of course, provided fixes for it): Bug in Microsoft's internal PriorityQueue<T>?

shareimprove this answer
edited May 30 '17 at 17:44
answered Mar 2 '17 at 18:11

weir
3,67522 gold badges2020 silver badges3131 bronze badges
9
A bug was found in one of the implementations here: stackoverflow.com/questions/44221454/… – MathuSum Mut May 27 '17 at 21:11 
ohh! I can see that all these classes PriorityQueue<T> in shared source of Microsoft are marked with internal access specifier. So they are used only by the internal functionalities of the framework. They aren't available for general consumption just by referring windowsbase.dll in a C# project. Only way is to copy the shared source into project itself inside a class file. – RBT Jan 2 '18 at 11:56
add a comment

7

You may find useful this implementation: http://www.codeproject.com/Articles/126751/Priority-queue-in-Csharp-with-help-of-heap-data-st.aspx

it is generic and based on heap data structure

shareimprove this answer
answered Nov 11 '10 at 21:05

Alexey
7911 silver badge11 bronze badge
add a comment

6

class PriorityQueue<T>
{
    IComparer<T> comparer;
    T[] heap;
    public int Count { get; private set; }
    public PriorityQueue() : this(null) { }
    public PriorityQueue(int capacity) : this(capacity, null) { }
    public PriorityQueue(IComparer<T> comparer) : this(16, comparer) { }
    public PriorityQueue(int capacity, IComparer<T> comparer)
    {
        this.comparer = (comparer == null) ? Comparer<T>.Default : comparer;
        this.heap = new T[capacity];
    }
    public void push(T v)
    {
        if (Count >= heap.Length) Array.Resize(ref heap, Count * 2);
        heap[Count] = v;
        SiftUp(Count++);
    }
    public T pop()
    {
        var v = top();
        heap[0] = heap[--Count];
        if (Count > 0) SiftDown(0);
        return v;
    }
    public T top()
    {
        if (Count > 0) return heap[0];
        throw new InvalidOperationException("优先队列为空");
    }
    void SiftUp(int n)
    {
        var v = heap[n];
        for (var n2 = n / 2; n > 0 && comparer.Compare(v, heap[n2]) > 0; n = n2, n2 /= 2) heap[n] = heap[n2];
        heap[n] = v;
    }
    void SiftDown(int n)
    {
        var v = heap[n];
        for (var n2 = n * 2; n2 < Count; n = n2, n2 *= 2)
        {
            if (n2 + 1 < Count && comparer.Compare(heap[n2 + 1], heap[n2]) > 0) n2++;
            if (comparer.Compare(v, heap[n2]) >= 0) break;
            heap[n] = heap[n2];
        }
        heap[n] = v;
    }
}
easy.

shareimprove this answer
answered Nov 24 '15 at 8:16

Shimou Dong
7911 silver badge33 bronze badges
13
Sometimes I see stuff like for (var n2 = n / 2; n > 0 && comparer.Compare(v, heap[n2]) > 0; n = n2, n2 /= 2) heap[n] = heap[n2]; and wonder if it was worth one-lining – user2761580 May 15 '17 at 12:43 
1
@DustinBreakey personal style :) – Shimou Dong May 16 '17 at 2:09
3
but definitely not readable to others. Consider writing code which does not leave a question mark floating on top of the developer's head. – alzaimar Mar 18 '18 at 9:30
add a comment

3

Use a Java to C# translator on the Java implementation (java.util.PriorityQueue) in the Java Collections framework, or more intelligently use the algorithm and core code and plug it into a C# class of your own making that adheres to the C# Collections framework API for Queues, or at least Collections.

shareimprove this answer
answered Sep 19 '08 at 14:49

JeeBee
16.7k44 gold badges4545 silver badges5959 bronze badges
This works, but unfortunately IKVM doesn't support Java generics, so you lose type safety. – Mechanical snail Nov 8 '12 at 4:34 
8
There is no such thing as "Java generics" when you're dealing with compiled Java bytecode. IKVM can't support it. – Mark Dec 7 '12 at 21:57
add a comment

3

AlgoKit
I wrote an open source library called AlgoKit, available via NuGet. It contains:

Implicit d-ary heaps (ArrayHeap),
Binomial heaps,
Pairing heaps.
The code has been extensively tested. I definitely recommend you to give it a try.

Example
var comparer = Comparer<int>.Default;
var heap = new PairingHeap<int, string>(comparer);

heap.Add(3, "your");
heap.Add(5, "of");
heap.Add(7, "disturbing.");
heap.Add(2, "find");
heap.Add(1, "I");
heap.Add(6, "faith");
heap.Add(4, "lack");

while (!heap.IsEmpty)
    Console.WriteLine(heap.Pop().Value);
Why those three heaps?
The optimal choice of implementation is strongly input-dependent — as Larkin, Sen, and Tarjan show in A back-to-basics empirical study of priority queues, arXiv:1403.0252v1 [cs.DS]. They tested implicit d-ary heaps, pairing heaps, Fibonacci heaps, binomial heaps, explicit d-ary heaps, rank-pairing heaps, quake heaps, violation heaps, rank-relaxed weak heaps, and strict Fibonacci heaps.

AlgoKit features three types of heaps that appeared to be most efficient among those tested.

Hint on choice
For a relatively small number of elements, you would likely be interested in using implicit heaps, especially quaternary heaps (implicit 4-ary). In case of operating on larger heap sizes, amortized structures like binomial heaps and pairing heaps should perform better.

shareimprove this answer
answered Aug 24 '16 at 19:20

Patryk Gołębiowski
3,51933 gold badges2121 silver badges4343 bronze badges
add a comment

2

Here is the another implementation from NGenerics team:

NGenerics PriorityQueue

shareimprove this answer
answered Jul 8 '11 at 14:45

husayt
11.3k77 gold badges4343 silver badges6969 bronze badges
add a comment

1

I had the same issue recently and ended up creating a NuGet package for this.

This implements a standard heap-based priority queue. It also has all the usual niceties of the BCL collections: ICollection<T> and IReadOnlyCollection<T> implementation, custom IComparer<T> support, ability to specify an initial capacity, and a DebuggerTypeProxy to make the collection easier to work with in the debugger.

There is also an Inline version of the package which just installs a single .cs file into your project (useful if you want to avoid taking externally-visible dependencies).

More information is available on the github page.

shareimprove this answer
answered Jul 29 '16 at 11:05

ChaseMedallion
17.7k1111 gold badges6767 silver badges127127 bronze badges
add a comment

1

A Simple Max Heap Implementation.

https://github.com/bharathkumarms/AlgorithmsMadeEasy/blob/master/AlgorithmsMadeEasy/MaxHeap.cs

using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgorithmsMadeEasy
{
    class MaxHeap
    {
        private static int capacity = 10;
        private int size = 0;
        int[] items = new int[capacity];

        private int getLeftChildIndex(int parentIndex) { return 2 * parentIndex + 1; }
        private int getRightChildIndex(int parentIndex) { return 2 * parentIndex + 2; }
        private int getParentIndex(int childIndex) { return (childIndex - 1) / 2; }

        private int getLeftChild(int parentIndex) { return this.items[getLeftChildIndex(parentIndex)]; }
        private int getRightChild(int parentIndex) { return this.items[getRightChildIndex(parentIndex)]; }
        private int getParent(int childIndex) { return this.items[getParentIndex(childIndex)]; }

        private bool hasLeftChild(int parentIndex) { return getLeftChildIndex(parentIndex) < size; }
        private bool hasRightChild(int parentIndex) { return getRightChildIndex(parentIndex) < size; }
        private bool hasParent(int childIndex) { return getLeftChildIndex(childIndex) > 0; }

        private void swap(int indexOne, int indexTwo)
        {
            int temp = this.items[indexOne];
            this.items[indexOne] = this.items[indexTwo];
            this.items[indexTwo] = temp;
        }

        private void hasEnoughCapacity()
        {
            if (this.size == capacity)
            {
                Array.Resize(ref this.items,capacity*2);
                capacity *= 2;
            }
        }

        public void Add(int item)
        {
            this.hasEnoughCapacity();
            this.items[size] = item;
            this.size++;
            heapifyUp();
        }

        public int Remove()
        {
            int item = this.items[0];
            this.items[0] = this.items[size-1];
            this.items[this.size - 1] = 0;
            size--;
            heapifyDown();
            return item;
        }

        private void heapifyUp()
        {
            int index = this.size - 1;
            while (hasParent(index) && this.items[index] > getParent(index))
            {
                swap(index, getParentIndex(index));
                index = getParentIndex(index);
            }
        }

        private void heapifyDown()
        {
            int index = 0;
            while (hasLeftChild(index))
            {
                int bigChildIndex = getLeftChildIndex(index);
                if (hasRightChild(index) && getLeftChild(index) < getRightChild(index))
                {
                    bigChildIndex = getRightChildIndex(index);
                }

                if (this.items[bigChildIndex] < this.items[index])
                {
                    break;
                }
                else
                {
                    swap(bigChildIndex,index);
                    index = bigChildIndex;
                }
            }
        }
    }
}


Calling Code:
    MaxHeap mh = new MaxHeap();
    mh.Add(10);
    mh.Add(5);
    mh.Add(2);
    mh.Add(1);
    mh.Add(50);
    int maxVal  = mh.Remove();
    int newMaxVal = mh.Remove();

shareimprove this answer
answered Feb 1 '17 at 8:02

Bharathkumar V
12111 silver badge1111 bronze badges
add a comment

-3

The following implementation of a PriorityQueue uses SortedSet from the System library.

using System;
using System.Collections.Generic;

namespace CDiggins
{
    interface IPriorityQueue<T, K> where K : IComparable<K>
    {
        bool Empty { get; }
        void Enqueue(T x, K key);
        void Dequeue();
        T Top { get; }
    }

    class PriorityQueue<T, K> : IPriorityQueue<T, K> where K : IComparable<K>
    {
        SortedSet<Tuple<T, K>> set;

        class Comparer : IComparer<Tuple<T, K>>
        {
            public int Compare(Tuple<T, K> x, Tuple<T, K> y)
            {
                return x.Item2.CompareTo(y.Item2);
            }
        }

        PriorityQueue() { set = new SortedSet<Tuple<T, K>>(new Comparer()); }
        public bool Empty { get { return set.Count == 0; } }
        public void Enqueue(T x, K key) { set.Add(Tuple.Create(x, key)); }
        public void Dequeue() { set.Remove(set.Max); }
        public T Top { get { return set.Max.Item1; } }
    }
}
shareimprove this answer
edited Jan 1 '13 at 21:31
answered Jan 1 '13 at 20:47

cdiggins
14k44 gold badges8585 silver badges8989 bronze badges
6
SortedSet.Add will fail (and return false) if you already have an item in the set with the same "priority" as the item you are trying to add. So...if A.Compare(B) == 0 and A is already in the list, your PriorityQueue.Enqueue function will silently fail. – Joseph Mar 4 '13 at 21:22 
Mind to explain what are T x and K key ? I'm guessing this is a trick to allowing duplicate T x, and I need to generate an unique key (e.g. UUID) ? – Thariq Nugrohotomo Jan 29 '18 at 1:20
add a comment
Not the answer you're looking for? Browse other questions tagged c# .net data-structures heap priority-queue or ask your own question. 
*/