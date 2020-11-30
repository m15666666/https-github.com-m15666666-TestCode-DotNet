/*
中位数是有序列表中间的数。如果列表长度是偶数，中位数则是中间两个数的平均值。

例如，

[2,3,4] 的中位数是 3

[2,3] 的中位数是 (2 + 3) / 2 = 2.5

设计一个支持以下两种操作的数据结构：

void addNum(int num) - 从数据流中添加一个整数到数据结构中。
double findMedian() - 返回目前所有元素的中位数。
示例：

addNum(1)
addNum(2)
findMedian() -> 1.5
addNum(3)
findMedian() -> 2
进阶:

如果数据流中所有整数都在 0 到 100 范围内，你将如何优化你的算法？
如果数据流中 99% 的整数都在 0 到 100 范围内，你将如何优化你的算法？

*/

/// <summary>
/// https://leetcode-cn.com/problems/find-median-from-data-stream/
/// 295. 数据流的中位数
///
///
///
///
/// </summary>
internal class FindMedianFromDataStreamSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public FindMedianFromDataStreamSolution()
    {
    }

    public void AddNum(int num)
    {
        _lowMaxHeap.Insert(num);

        _highMinHeap.Insert(_lowMaxHeap.Max);
        _lowMaxHeap.DelMax();

        if (_lowMaxHeap.Count < _highMinHeap.Count)
        {
            _lowMaxHeap.Insert(_highMinHeap.Max);
            _highMinHeap.DelMax();
        }
    }

    public double FindMedian()
    {
        return _highMinHeap.Count < _lowMaxHeap.Count ? _lowMaxHeap.Max : (_lowMaxHeap.Max + _highMinHeap.Max) * 0.5;
    }

    private const int Capacity = 10000;
    private MaxPQ<int> _lowMaxHeap = new MaxPQ<int>(Capacity, true);
    private MaxPQ<int> _highMinHeap = new MaxPQ<int>(Capacity, false);

    public class MaxPQ<Key> where Key : System.IComparable<Key>
    {
        private Key[] pq;

        private int N = 0;
        private readonly bool _maxOrMin = true;

        /// <summary>
        /// 最大堆或者最小堆
        /// </summary>
        /// <param name="cap">容量</param>
        /// <param name="maxOrMin">true:最大堆，false:最小堆</param>
        public MaxPQ(int cap, bool maxOrMin = true)
        {
            pq = new Key[cap + 1];

            _maxOrMin = maxOrMin;
        }

        private void Swim(int k)
        {
            while (k > 1 && Less(Parent(k), k))
            {
                Exchange(Parent(k), k);
                k = Parent(k);
            }
        }

        private void Sink(int k)
        {
            while (Left(k) <= N)
            {
                int older = Left(k);
                if (Right(k) <= N && Less(older, Right(k)))
                    older = Right(k);
                if (Less(older, k)) break;
                Exchange(k, older);
                k = older;
            }
        }

        private void Exchange(int i, int j)
        {
            Key temp = pq[i];
            pq[i] = pq[j];
            pq[j] = temp;
        }

        private bool Less(int i, int j) => (_maxOrMin ? pq[i].CompareTo(pq[j]) : pq[j].CompareTo(pq[i])) < 0;

        private int Parent(int i) => i / 2;

        private int Left(int i) => i * 2;

        private int Right(int i) => i * 2 + 1;

        public void Insert(Key e)
        {
            N++;
            pq[N] = e;
            Swim(N);
        }

        public Key DelMax()
        {
            Key max = pq[1];
            Exchange(1, N);
            pq[N] = default(Key);
            N--;
            Sink(1);
            return max;
        }

        public Key Max => pq[1];
        public int Count => N;
    }
}

/*
数据流的中位数
力扣 (LeetCode)
发布于 2019-06-30
21.5k
方法一：简单排序
照问题说的做。

算法：
将数字存储在可调整大小的容器中。每次需要输出中间值时，对容器进行排序并输出中间值。


class MedianFinder {
    vector<double> store;

public:
    // Adds a number into the data structure.
    void addNum(int num)
    {
        store.push_back(num);
    }

    // Returns the median of current data stream
    double findMedian()
    {
        sort(store.begin(), store.end());

        int n = store.size();
        return (n & 1 ? store[n / 2] : (store[n / 2 - 1] + store[n / 2]) * 0.5);
    }
};
复杂度分析

时间复杂度：O(n\log n) + O(1) \simeq O(n\log n)O(nlogn)+O(1)≃O(nlogn)。
添加一个数字对于一个有效调整大小方案的容器来说需要花费 O(1)O(1) 的时间。
找到中间值主要取决于发生的排序。对于标准比较排序，这需要 O(n \log n)O(nlogn) 时间。
空间复杂度：O(n)O(n) 线性空间，用于在容器中保存输入。除了需要的空间之外没有其他空间（因为通常可以在适当的位置进行排序）。
方法二： 插入排序
保持输入容器始终排序

算法：
哪种算法允许将一个数字添加到已排序的数字列表中，但仍保持整个列表的排序状态？插入排序！

我们假设当前列表已经排序。当一个新的数字出现时，我们必须将它添加到列表中，同时保持列表的排序性质。这可以通过使用二分搜索找到插入传入号码的正确位置来轻松实现。
（记住，列表总是排序的）。一旦找到位置，我们需要将所有较高的元素移动一个空间，以便为传入的数字腾出空间。

当插入查询的数量较少或者中间查找查询的数量大致相同。 此方法会很好地工作。


class MedianFinder {
    vector<int> store; // resize-able container

public:
    // Adds a number into the data structure.
    void addNum(int num)
    {
        if (store.empty())
            store.push_back(num);
        else
            store.insert(lower_bound(store.begin(), store.end(), num), num);     // binary search and insertion combined
    }

    // Returns the median of current data stream
    double findMedian()
    {
        int n = store.size();
        return n & 1 ? store[n / 2] : (store[n / 2 - 1] + store[n / 2]) * 0.5;
    }
};
复杂度分析

时间复杂度：O(n) + O(\log n) \approx O(n)O(n)+O(logn)≈O(n).
二分搜索需要花费 O(\log n)O(logn) 时间才能找到正确的插入位置。
插入可能需要花费 O(n)O(n) 的时间，因为必须在容器中移动元素为新元素腾出空间。
空间复杂度：O(n)O(n) 线性空间，用于在容器中保存输入。
方法三：两个堆
以上两种方法对如何解决这个问题提供了一些有价值的见解。具体来说，我们可以推断出两件事：

如果我们可以一直直接访问中值元素，那么找到中值将需要一个恒定的时间。
如果我们能找到一种相当快速的方法来增加容器的数量，那么所产生的额外操作可能会减少。
但也许最重要的洞察是我们只需要一种一致的方式来访问中值元素，这是不容易观察到的。不需要对整个输入进行排序。

事实证明，有两种数据结构符合：

堆（或优先级队列）
自平衡二进制搜索树（我们将在方法4中详细讨论它们）
堆是这道题的天然原料！向元素添加元素需要对数时间复杂度。它们还可以直接访问组中的最大/最小元素。

如果我们可以用以下方式维护两个堆：

用于存储输入数字中较小一半的最大堆
用于存储输入数字的较大一半的最小堆
这样就可以访问输入中的中值：它们组成堆的顶部！

如果满足以下条件：

两个堆都是平衡的（或接近平衡的）
最大堆包含所有较小的数字，而最小堆包含所有较大的数字
那么我们可以这样说：

最大堆中的所有数字都小于或等于最大堆的top元素（我们称之为 xx）
最小堆中的所有数字都大于或等于最小堆的顶部元素（我们称之为 yy）
那么 xx 和 yy 几乎小于（或等于）元素的一半，大于（或等于）另一半。这就是中值元素的定义。

这使我们在这种方法中遇到了一个巨大的难题：平衡这两个堆！

算法：

两个优先级队列：
用于存储较小一半数字的最大堆 lo
用于存储较大一半数字的最小堆 hi
最大堆 lo 允许存储的元素最多比最小堆 hi 多一个。因此，如果我们处理了 kk 元素：
如果 k=2*n+1 \quad(\forall,n \in \mathbb z)k=2∗n+1(∀,n∈z) 则允许 lo 持有 n+1n+1 元素，而 hi 可以持有 nn 元素。
如果 k=2*n\quad(\forall,n\in\mathbb z)k=2∗n(∀,n∈z)，那么两个堆都是平衡的，并且每个堆都包含 nn 个元素。
这给了我们一个很好的特性，即当堆完全平衡时，中间值可以从两个堆的顶部派生。否则，最大堆 lo 的顶部保留合法的中间值。

添加一个数 num：
将 num 添加到最大堆 lo。因为 lo 收到了一个新元素，所以我们必须为 hi 做一个平衡步骤。因此，从 lo 中移除最大的元素并将其提供给 hi。
在上一个操作之后，最小堆 hi 可能会比最大堆 lo 保留更多的元素。我们通过从 hi 中去掉最小的元素并将其提供给 lo 来解决这个问题。
上面的步骤确保两个堆能够平衡
举个小例子就可以解决这个问题了！假设我们从流中获取输入 [41、35、62、5、97、108]。算法的运行过程如下：


Adding number 41
MaxHeap lo: [41]           // MaxHeap stores the largest value at the top (index 0)
MinHeap hi: []             // MinHeap stores the smallest value at the top (index 0)
Median is 41
=======================
Adding number 35
MaxHeap lo: [35]
MinHeap hi: [41]
Median is 38
=======================
Adding number 62
MaxHeap lo: [41, 35]
MinHeap hi: [62]
Median is 41
=======================
Adding number 4
MaxHeap lo: [35, 4]
MinHeap hi: [41, 62]
Median is 38
=======================
Adding number 97
MaxHeap lo: [41, 35, 4]
MinHeap hi: [62, 97]
Median is 41
=======================
Adding number 108
MaxHeap lo: [41, 35, 4]
MinHeap hi: [62, 97, 108]
Median is 51.5

class MedianFinder {
    priority_queue<int> lo;                              // max heap
    priority_queue<int, vector<int>, greater<int>> hi;   // min heap

public:
    // Adds a number into the data structure.
    void addNum(int num)
    {
        lo.push(num);                                    // Add to max heap

        hi.push(lo.top());                               // balancing step
        lo.pop();

        if (lo.size() < hi.size()) {                     // maintain size property
            lo.push(hi.top());
            hi.pop();
        }
    }

    // Returns the median of current data stream
    double findMedian()
    {
        return lo.size() > hi.size() ? (double) lo.top() : (lo.top() + hi.top()) * 0.5;
    }
};
复杂度分析

时间复杂度： O(5 \cdot \log n) + O(1) \approx O(\log n)O(5⋅logn)+O(1)≈O(logn).。
最坏情况下，从顶部有三个堆插入和两个堆删除。每一个都需要花费 O(\log n)O(logn) 时间。
找到平均值需要持续的 O(1)O(1) 时间，因为可以直接访问堆的顶部。
空间复杂度：O(n)O(n) 用于在容器中保存输入的线性空间。
方法四：Multiset 和双指针
自平衡二进制搜索树（如AVL树）具有一些非常有趣的特性。它们将树的高度保持在对数范围内。因此，插入新元素具有相当好的时间性能。中值总是缠绕在树根或它的一个子树上。使用与方法 3 相同的方法解决这个问题，但是使用自平衡二叉树似乎是一个不错的选择。但是，实现这样一个树并不是简单的，而且容易出错。

大多数语言实现模拟这种行为的是 multiset 类。唯一的问题是跟踪中值元素。这很容易用指针解决！

我们保持两个指针：一个用于中位数较低的元素，另一个用于中位数较高的元素。当元素总数为奇数时，两个指针都指向同一个中值元素（因为在本例中只有一个中值）。当元素数为偶数时，指针指向两个连续的元素，其平均值是输入的代表中位数。

算法：

两个迭代器/指针 lo_median 和 hi_median，它们在 multiset上迭代 data。
添加数字 num 时，会出现三种情况：
容器当前为空。因此，我们只需插入 num 并设置两个指针指向这个元素。
容器当前包含奇数个元素。这意味着两个指针当前都指向同一个元素。
如果 num 不等于当前的中位数元素，则 num 将位于元素的任一侧。无论哪一边，该部分的大小都会增加，因此相应的指针会更新。例如，如果 num 小于中位数元素，则在插入 num 时，输入的较小半部分的大小将增加 11。
如果 num 等于当前的中位数元素，那么所采取的操作取决于 num 是如何插入数据的。
容器当前包含偶数个元素。这意味着指针当前指向连续的元素。
如果 num 是两个中值元素之间的数字，则 num 将成为新的中值。两个指针都必须指向它。
否则，num 会增加较小或较高一半的大小。我们相应地更新指针。必须记住，两个指针现在必须指向同一个元素。
找到中间值很容易！它只是两个指针 lo_median 和 hi_median 所指元素的平均值。

class MedianFinder {
    multiset<int> data;
    multiset<int>::iterator lo_median, hi_median;

public:
    MedianFinder()
        : lo_median(data.end())
        , hi_median(data.end())
    {
    }

    void addNum(int num)
    {
        const size_t n = data.size();   // store previous size

        data.insert(num);               // insert into multiset

        if (!n) {
            // no elements before, one element now
            lo_median = hi_median = data.begin();
        }
        else if (n & 1) {
            // odd size before (i.e. lo == hi), even size now (i.e. hi = lo + 1)

            if (num < *lo_median)       // num < lo
                lo_median--;
            else                        // num >= hi
                hi_median++;            // insertion at end of equal range
        }
        else {
            // even size before (i.e. hi = lo + 1), odd size now (i.e. lo == hi)

            if (num > *lo_median && num < *hi_median) {
                lo_median++;                    // num in between lo and hi
                hi_median--;
            }
            else if (num >= *hi_median)         // num inserted after hi
                lo_median++;
            else                                // num <= lo < hi
                lo_median = --hi_median;        // insertion at end of equal range spoils lo
        }
    }

    double findMedian()
    {
        return (*lo_median + *hi_median) * 0.5;
    }
};
此解决方案的单指针版本更短（但更难理解），如下所示：


class MedianFinder {
    multiset<int> data;
    multiset<int>::iterator mid;

public:
    MedianFinder()
        : mid(data.end())
    {
    }

    void addNum(int num)
    {
        const int n = data.size();
        data.insert(num);

        if (!n)                                 // first element inserted
            mid = data.begin();
        else if (num < *mid)                    // median is decreased
            mid = (n & 1 ? mid : prev(mid));
        else                                    // median is increased
            mid = (n & 1 ? next(mid) : mid);
    }

    double findMedian()
    {
        const int n = data.size();
        return (*mid + *next(mid, n % 2 - 1)) * 0.5;
    }
};
复杂度分析

时间复杂度：O(\log n) + O(1) \approx O(\log n)O(logn)+O(1)≈O(logn)。
对于标准 multiset 方案，插入数字需要花费 O(\log n)O(logn) 时间。
找到平均值需要固定的 O(1)O(1) 时间，因为中位数元素可以直接从两个指针访问。
空间复杂度：O(n)O(n) 用于在容器中保存输入的线性空间。

public class MedianFinder
{
    Heap maxHeap = new Heap(10000, true);
    Heap minHeap = new Heap(10000, false);

    public MedianFinder()
    {

    }

    public void AddNum(int num)
    {
        if (maxHeap.heapSize == 0)
        {
            maxHeap.Push(num);
            return;
        }

        if (num <= maxHeap.topNum)
            maxHeap.Push(num);
        else
            minHeap.Push(num);
        if (maxHeap.heapSize - minHeap.heapSize > 1)
        {
            int maxTop = maxHeap.Pop();
            minHeap.Push(maxTop);
        }
        else if (minHeap.heapSize - maxHeap.heapSize > 0)
        {
            int minTop = minHeap.Pop();
            maxHeap.Push(minTop);
        }

    }

    public double FindMedian()
    {
        int count = maxHeap.heapSize + minHeap.heapSize;
        if ((count & 1) == 1)//奇数
            return maxHeap.topNum;
        else
            return (minHeap.topNum + maxHeap.topNum) / 2.0f;
    }

    class Heap
    {
        int[] nums;
        public int heapSize { get; private set; }
        public int capacity { get; private set; }
        public int topNum => nums[1];
        bool max;

        public Heap(int capacity, bool max)
        {
            this.capacity = capacity;
            nums = new int[capacity];
            this.max = max;
        }

        public int Pop()
        {
            if (heapSize <= 0)
                throw new System.Exception("堆为空！");

            int res = nums[1];
            int curNum = nums[heapSize--];
            nums[1] = curNum;
            int parent = 1, childL = parent * 2, childR;
            while (childL <= heapSize)
            {

                childR = childL + 1;
                if (max)
                {
                    int maxChild;
                    if (childR <= heapSize)
                        maxChild = nums[childL] >= nums[childR] ? childL : childR;
                    else
                        maxChild = childL;
                    if (nums[parent] >= nums[maxChild])
                        break;
                    int temp = nums[parent];
                    nums[parent] = nums[maxChild];
                    nums[maxChild] = temp;
                    parent = maxChild;
                }
                else
                {
                    int minChild;
                    if (childR <= heapSize)
                        minChild = nums[childL] <= nums[childR] ? childL : childR;
                    else
                        minChild = childL;
                    if (nums[parent] <= nums[minChild])
                        break;
                    int temp = nums[parent];
                    nums[parent] = nums[minChild];
                    nums[minChild] = temp;
                    parent = minChild;
                }

                childL = parent * 2;
            }
            return res;
        }

        public void Push(int num)
        {
            nums[++heapSize] = num;
            int curChild = heapSize, parent = curChild / 2;
            while (parent > 0)
            {
                if (max)
                {
                    if (nums[parent] >= nums[curChild])
                        break;
                }
                else
                {
                    if (nums[parent] <= nums[curChild])
                        break;
                }
                int temp = nums[parent];
                nums[parent] = nums[curChild];
                nums[curChild] = temp;
                curChild = parent;
                parent = curChild / 2;
            }
        }
    }
}

public class TreeNode
{
	public int val;
	public int size;
	public TreeNode left;
	public TreeNode right;

	public TreeNode(int val)
	{
		this.val = val;
		this.size = 1;
	}
}

public class MedianFinder
{
	private TreeNode root;
	public MedianFinder()
	{

	}

	public void AddNum(int num)
	{
		if (this.root == null)
		{
			root = new TreeNode(num);
		}
		else
		{
			this.AddNum(num, this.root);
		}
	}

	public double FindMedian()
	{
		if (this.root.size % 2 == 1)
		{
			return Search(this.root.size / 2 + 1, this.root);
		}
		else
		{
			int left = Search(this.root.size / 2, this.root);
			int right = Search(this.root.size / 2 + 1, this.root);
			return (left + right) / 2.0;
		}
	}

	private void AddNum(int num, TreeNode node)
	{
		if (node.val >= num)
		{
			if (node.left == null)
			{
				node.left = new TreeNode(num);
			}
			else
			{
				AddNum(num, node.left);
			}
		}
		else
		{
			if (node.right == null)
			{
				node.right = new TreeNode(num);
			}
			else
			{
				AddNum(num, node.right);
			}
		}
		node.size++;
	}

	private int Search(int index, TreeNode node)
	{
		if (node.size == 1)
		{
			return node.val;
		}
		int leftSize = node.left != null ? node.left.size : 0;
		if (leftSize >= index)
		{
			return Search(index, node.left);
		}
		else if (leftSize + 1 == index)
		{
			return node.val;
		}
		else
		{
			return Search(index - leftSize - 1, node.right);
		}
	}
}

public class MedianFinder {
	List<int> myList;
	public MedianFinder() 
	{
		myList = new List<int>();
	}
	
	//二分插入,使列表始终有序
	public void AddNum(int num)
	{
		int result = 0;
		int left = 0;
		int right = myList.Count-1;
		while(left<=right)
		{
			int mid = left + (right - left)/2;
			int value = myList[mid];
			// 目标值刚好等于中间值
			if (num == value)
			{
				result = mid;
				break;
			}else if (num<value) //目标值小于中间值，则目标值在中间值左边
			{
				right = mid - 1;
			}
			else
			{
				left = mid + 1;
				result = left;
			}
		}
		myList.Insert(result,num);
	}
	
	public double FindMedian() 
	{
		double result = 0.0;
		int length = myList.Count;
		if (length%2 == 1)
		{
			result = myList[length/2];
		}else
		{
			int left = myList[length/2 - 1];
			int right = myList[length/2];
			result = (left+right)/2.0;
		}
		return result;
	}
}

public class MedianFinder
{
	private Node root;

	public int Count { get; set; }
	// initialize your data structure here. 
	public MedianFinder()
	{
		root = new Node(null);
	}

	public void AddNum(int num)
	{
		//二叉树查找一个节点
		Node node = Search(num);
		//如果为叶节点则将其设为转为非叶节点
		if (node.IsLeaf == true)
		{
			node.ToNoNull(num);
		}
		//如果不为叶节点说明二叉树中存在该num，所以Size++（当前节点中num的个数）
		else
		{
			node.Size++;
		}
		//从该节点向上递归到根
		while (node != null)
		{
			//更新该节点左右子树的个数以及当前Size
			node.Update();
			node = node.P;
		}
		Count++;
	}

	public double FindMedian()
	{
		//按Count查找中位数在树中的索引
		return Count % 2 == 0 ? (ElementAt(Count / 2 - 1) + ElementAt(Count / 2)) / 2.0 : ElementAt(Count / 2);
	}

	private int ElementAt(int index)
	{
		return ElementAt(root, index);
	}
	//根据索引在树中递归查找相应索引的num值
	private int ElementAt(Node node,int index)
	{
		//判断index位于当前节点，还是左子树或者右子树
		if (index >= node.L && index < node.L + node.Size)
			return node.num;
		else if (node.L > index)
			return ElementAt(node.LL, index);
		else
			return ElementAt(node.RR, index - node.L - node.Size);
	}
	//在树中查找num相应的节点
	private Node Search(int num)
	{
		Node p = root;
		while (p.IsLeaf == false)
		{
			if (num < p.num)
				p = p.LL;
			else if (num > p.num)
				p = p.RR;
			else
				return p;
		}
		return p;
	}

	public class Node
	{
		public int num;     //值
		public int Size;    //num出现的个数
		public int L;       //左子树节点个数
		public int R;       //右子树节点个数
		public bool IsLeaf; //是否为叶子节点（空节点）
		public Node LL;     //左子节点
		public Node RR;     //右子节点
		public Node P;      //父节点
		//创建一个叶子节点（空节点）
		public Node(Node node)
		{
			IsLeaf = true;
			P = node;
		}
		//创建一个节点
		public Node(int num)
		{
			this.num = num;
			Size = 1;
			LL = new Node(this);
			RR = new Node(this);
		}
		//更新节点数据
		public void Update()
		{
			L = LL.L + LL.R + LL.Size;
			R = RR.L + RR.R + RR.Size;
		}
		//将该叶子节点（空节点）转为非空节点
		public void ToNoNull(int num)
		{
			this.num = num;
			Size = 1;
			IsLeaf = false;
			LL = new Node(this);
			RR = new Node(this);
		}
	}
}


*/