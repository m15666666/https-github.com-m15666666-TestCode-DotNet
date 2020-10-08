using System;

/*
给定一个数组 nums，有一个大小为 k 的滑动窗口从数组的最左侧移动到数组的最右侧。你只可以看到在滑动窗口内的 k 个数字。滑动窗口每次只向右移动一位。

返回滑动窗口中的最大值。

 

进阶：

你能在线性时间复杂度内解决此题吗？

 

示例:

输入: nums = [1,3,-1,-3,5,3,6,7], 和 k = 3
输出: [3,3,5,5,6,7]
解释:

  滑动窗口的位置                最大值
---------------               -----
[1  3  -1] -3  5  3  6  7       3
 1 [3  -1  -3] 5  3  6  7       3
 1  3 [-1  -3  5] 3  6  7       5
 1  3  -1 [-3  5  3] 6  7       5
 1  3  -1  -3 [5  3  6] 7       6
 1  3  -1  -3  5 [3  6  7]      7
 

提示：

1 <= nums.length <= 10^5
-10^4 <= nums[i] <= 10^4
1 <= k <= nums.length
通过次数79,688提交次数162,142

*/

/// <summary>
/// https://leetcode-cn.com/problems/sliding-window-maximum/
/// 239. 滑动窗口最大值
///
///
///
///
///
///
///
/// </summary>
internal class SlidingWindowMaximumSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int[] MaxSlidingWindow(int[] nums, int k)
    {
        int n = nums.Length;
        if (n == 0 || k == 0) return new int[0];
        if (k == 1) return nums;

        int[] left = new int[n];
        left[0] = nums[0];
        int[] right = new int[n];
        right[n - 1] = nums[n - 1];
        for (int i = 1; i < n; i++)
        {
            if (i % k == 0) left[i] = nums[i];
            else left[i] = Math.Max(left[i - 1], nums[i]);

            int j = n - i - 1;
            if ((j + 1) % k == 0) right[j] = nums[j];
            else right[j] = Math.Max(right[j + 1], nums[j]);
        }

        int[] ret = new int[n - k + 1];
        for (int i = 0; i < ret.Length; i++)
            ret[i] = Math.Max(left[i + k - 1], right[i]);

        return ret;
    }
}

/*
滑动窗口最大值
力扣 (LeetCode)
发布于 2019-07-09
77.6k
方法一：暴力法
直觉

最简单直接的方法是遍历每个滑动窗口，找到每个窗口的最大值。一共有 N - k + 1 个滑动窗口，每个有 k 个元素，于是算法的时间复杂度为 {O}(N k)O(Nk)，表现较差。

实现


class Solution {
    public int[] maxSlidingWindow(int[] nums, int k) {
        int n = nums.length;
        if (n * k == 0) return new int[0];
        
        int [] output = new int[n - k + 1];
        for (int i = 0; i < n - k + 1; i++) {
            int max = Integer.MIN_VALUE;
            for(int j = i; j < i + k; j++) 
                max = Math.max(max, nums[j]);
            output[i] = max;
        }
        return output;
    }
}
复杂度分析

时间复杂度：{O}(N k)O(Nk)。其中 N 为数组中元素个数。

空间复杂度：{O}(N - k + 1)O(N−k+1)，用于输出数组。




方法二：双向队列
直觉

如何优化时间复杂度呢？首先想到的是使用堆，因为在最大堆中 heap[0] 永远是最大的元素。在大小为 k 的堆中插入一个元素消耗 \log(k)log(k) 时间，因此算法的时间复杂度为 {O}(N \log(k))O(Nlog(k))。

能否得到只要 {O}(N)O(N) 的算法？

我们可以使用双向队列，该数据结构可以从两端以常数时间压入/弹出元素。

存储双向队列的索引比存储元素更方便，因为两者都能在数组解析中使用。

算法

算法非常直截了当：

处理前 k 个元素，初始化双向队列。

遍历整个数组。在每一步 :

清理双向队列 :

  - 只保留当前滑动窗口中有的元素的索引。

  - 移除比当前元素小的所有元素，它们不可能是最大的。
将当前元素添加到双向队列中。
将 deque[0] 添加到输出中。
返回输出数组。
实现


class Solution {
  ArrayDeque<Integer> deq = new ArrayDeque<Integer>();
  int [] nums;

  public void clean_deque(int i, int k) {
    // remove indexes of elements not from sliding window
    if (!deq.isEmpty() && deq.getFirst() == i - k)
      deq.removeFirst();

    // remove from deq indexes of all elements 
    // which are smaller than current element nums[i]
    while (!deq.isEmpty() && nums[i] > nums[deq.getLast()]) deq.removeLast();
  }

  public int[] maxSlidingWindow(int[] nums, int k) {
    int n = nums.length;
    if (n * k == 0) return new int[0];
    if (k == 1) return nums;

    // init deque and output
    this.nums = nums;
    int max_idx = 0;
    for (int i = 0; i < k; i++) {
      clean_deque(i, k);
      deq.addLast(i);
      // compute max in nums[:k]
      if (nums[i] > nums[max_idx]) max_idx = i;
    }
    int [] output = new int[n - k + 1];
    output[0] = nums[max_idx];

    // build output
    for (int i  = k; i < n; i++) {
      clean_deque(i, k);
      deq.addLast(i);
      output[i - k + 1] = nums[deq.getFirst()];
    }
    return output;
  }
}
复杂度分析

时间复杂度：{O}(N)O(N)，每个元素被处理两次- 其索引被添加到双向队列中和被双向队列删除。

空间复杂度：{O}(N)O(N)，输出数组使用了 {O}(N - k + 1)O(N−k+1) 空间，双向队列使用了 {O}(k)O(k)。




方法三: 动态规划
直觉

这是另一个 {O}(N)O(N) 的算法。本算法的优点是不需要使用 数组 / 列表 之外的任何数据结构。

算法的思想是将输入数组分割成有 k 个元素的块。
若 n % k != 0，则最后一块的元素个数可能更少。

image.png

开头元素为 i ，结尾元素为 j 的当前滑动窗口可能在一个块内，也可能在两个块中。

image.png

情况 1 比较简单。 建立数组 left， 其中 left[j] 是从块的开始到下标 j 最大的元素，方向 左->右。

image.png

为了处理更复杂的情况 2，我们需要数组 right，其中 right[j] 是从块的结尾到下标 j 最大的元素，方向 右->左。right 数组和 left 除了方向不同以外基本一致。

image.png

两数组一起可以提供两个块内元素的全部信息。考虑从下标 i 到下标 j的滑动窗口。 根据定义，right[i] 是左侧块内的最大元素， left[j] 是右侧块内的最大元素。因此滑动窗口中的最大元素为 max(right[i], left[j])。

image.png

算法

算法十分直截了当：

从左到右遍历数组，建立数组 left。

从右到左遍历数组，建立数组 right。

建立输出数组 max(right[i], left[i + k - 1])，其中 i 取值范围为 (0, n - k + 1)。

实现




class Solution {
  public int[] maxSlidingWindow(int[] nums, int k) {
    int n = nums.length;
    if (n * k == 0) return new int[0];
    if (k == 1) return nums;

    int [] left = new int[n];
    left[0] = nums[0];
    int [] right = new int[n];
    right[n - 1] = nums[n - 1];
    for (int i = 1; i < n; i++) {
      // from left to right
      if (i % k == 0) left[i] = nums[i];  // block_start
      else left[i] = Math.max(left[i - 1], nums[i]);

      // from right to left
      int j = n - i - 1;
      if ((j + 1) % k == 0) right[j] = nums[j];  // block_end
      else right[j] = Math.max(right[j + 1], nums[j]);
    }

    int [] output = new int[n - k + 1];
    for (int i = 0; i < n - k + 1; i++)
      output[i] = Math.max(left[i + k - 1], right[i]);

    return output;
  }
}
复杂度分析

时间复杂度：{O}(N)O(N)，我们对长度为 N 的数组处理了 3次。

空间复杂度：{O}(N)O(N)，用于存储长度为 N 的 left 和 right 数组，以及长度为 N - k + 1的输出数组。


public int[] MaxSlidingWindow(int[] nums, int k) 
{
	
	int n = nums.Length;
	int[] ans = new int[n - k + 1];
	int index = 0;
	LinkedList<int> deque = new LinkedList<int>();
	
	for (int i = 0; i < n; ++ i) 
	{
		if (deque.Count > 0 && deque.First.Value < i - k + 1)
		{
			deque.RemoveFirst();
		}
		
		while (deque.Count > 0 && nums[deque.Last.Value] < nums[i])
		{
			deque.RemoveLast();
		}
		
		deque.AddLast(i);
		
		if (i - k + 1 >= 0)
		{
			ans[index ++] = nums[deque.First.Value];
		}
	}
	
	return ans;
}

public class Solution {
    public int[] MaxSlidingWindow(int[] nums, int k) {
        int size = nums.Length;
        int returnLength = size -k + 1;
        int[] result = new int[returnLength];

        int[] left = new int[size];
        int[] right = new int[size];

        for (int i=0;i<size;i++) {
            if (i%k == 0) left[i] = nums[i];
            else left[i] = nums[i] > left[i-1]? nums[i]:left[i-1];
        }

        for (int i=size-1;i>-1;i--) {
            if ((i+1)%k == 0 || i==size-1) right[i] = nums[i];
            else right[i] = nums[i] > right[i+1]? nums[i]:right[i+1];
        }

        for (int i=0;i<size-k+1;i++) {
            result[i] = left[i+k-1] > right[i]?left[i+k-1]:right[i];
        }
        return result;
    }
}

public class Solution {
    public int[] MaxSlidingWindow(int[] nums, int k) {
        // Time-O(n*logk), Space-O(k)
        List<int> ans = new List<int>();
        List<int> data = new List<int>();

        for (int i=0;i<nums.Length;i++)
        {
            // Enqueue - O(logk)
            int l=0, r=data.Count-1;
            while (l<=r)
            {
                int mid=(l+r)/2;
                if (nums[data[mid]]==nums[i])
                {
                    l = mid;
                    break;
                }
                else if (nums[data[mid]]<nums[i])
                    r = mid-1;
                else if (nums[data[mid]]>nums[i])
                    l = mid+1;
            }
            data.RemoveRange(l, data.Count-l);
            data.Add(i);
            // Dequeue if first data is old - O(1)
            if (data[0] == i-k)
                data.RemoveAt(0);
            // Add to answer - O(1)
            if (i>=k-1) 
                ans.Add(nums[data[0]]);
            // debug
            //displayList(data);
        }
        return ans.ToArray();
    }

    private void displayList<T>(List<T> list)
    {
        string s = "";
        foreach (T element in list)
        {
            s += element.ToString() + ',';
        }
        s = string.Format("[{0}]", s.TrimEnd(','));
        Console.WriteLine(s);
    }    
}

public class MaxQueue 
{
	private List<int> data;

	public MaxQueue() 
    {
		this.data = new List<int>();
	}
	public void Enqueue(int item) 
    {
        int l=0, r=data.Count-1;
        //Console.Write("[l,r]= [{0},{1}]->", l,r);
        while (l<=r)
        {
            int mid=(l+r)/2;
            if (data[mid]==item)
            {
                l = mid;
                break;
            }
            else if (data[mid]<item)
                r = mid-1;
            else if (data[mid]>item)
                l = mid+1;
            //Console.Write("[{0},{1}]->", l,r);
        }
        //Console.Write("[{0},{1}]:", l,r);
        data.RemoveRange(l, data.Count-l);
        data.Add(item);
	}
	public void Dequeue(int item) 
    {
        data.Remove(item);
	}
    public int Peek() 
    {
		return data[0];
	}
	public int Count() 
    {
		return data.Count;
	}
	public override string ToString() 
    {
		string s = "";
		for (int i = 0; i < data.Count; ++i)
			s += data[i].ToString() + " ";
		s += "count = " + data.Count;
		return s;
	}
}

public class Solution {
    public int[] MaxSlidingWindow(int[] nums, int k) {
        if(nums.Length == 0 || k == 0)
            return new int[0];
        List<int> res = new List<int>();//当做队列使用
        List<int> result = new List<int>();//存储结果
        for(int i = 0;i<nums.Length;i++){
            if(res.Count!=0&&res[0]<=i-k){
                res.RemoveAt(0);
            }
            while(res.Count!=0&&nums[res[res.Count-1]]<nums[i]){
                res.RemoveAt(res.Count-1);
            }
            res.Add(i);
            if(i>=k-1){
                result.Add(nums[res[0]]);
            }
        }
        return result.ToArray();
    }
}

public class Solution {
    public int[] MaxSlidingWindow(int[] nums, int k) {
        int [] result = new int[nums.Length - k + 1];
            //创建窗口
            LinkedList<int> maxSliderWindow = new LinkedList<int>();
            for (int i = 0; i < nums.Length; i++)
            {
                //在窗口达到限制长度后移除队首（第一个元素）以便继续添加窗口内元素
                if(maxSliderWindow.Count>0 && maxSliderWindow.First.Value < i-k + 1)
                {
                    maxSliderWindow.RemoveFirst();
                }
                //循环将当前需要入队的元素与队尾作比较（直到当前元素小于队尾元素）否则移除队尾
                while(maxSliderWindow.Count > 0 && nums[i] >=nums[maxSliderWindow.Last.Value])
                {
                    maxSliderWindow.RemoveLast();
                }
                //入队
                maxSliderWindow.AddLast(i);
                if (i >= k - 1)
                {
                    result[i - k + 1] = nums[maxSliderWindow.First.Value];
                }
            }
            return result;

    }
}


*/