using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
在未排序的数组中找到第 k 个最大的元素。请注意，你需要找的是数组排序后的第 k 个最大的元素，而不是第 k 个不同的元素。

示例 1:

输入: [3,2,1,5,6,4] 和 k = 2
输出: 5
示例 2:

输入: [3,2,3,1,2,4,5,5,6] 和 k = 4
输出: 4
说明:

你可以假设 k 总是有效的，且 1 ≤ k ≤ 数组的长度。

*/
/// <summary>
/// https://leetcode-cn.com/problems/kth-largest-element-in-an-array/
/// 215. 数组中的第K个最大元素
/// 在未排序的数组中找到第 k 个最大的元素。请注意，你需要找的是数组排序后的第 k 个最大的元素，而不是第 k 个不同的元素。
/// 示例 1:
/// 输入: [3,2,1,5,6,4]
/// 和 k = 2
/// 输出: 5
/// 示例 2:
/// 输入: [3,2,3,1,2,4,5,5,6]
/// 和 k = 4
/// 输出: 4
/// 说明:
/// 你可以假设 k 总是有效的，且 1 ≤ k ≤ 数组的长度。
/// </summary>
class KthLargestElementInAnArraySolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    public int FindKthLargest(int[] nums, int k)
    {
        if ( k < 0 || nums == null || nums.Length == 0 || nums.Length < k ) return -1;
        Array.Sort(nums);
        return nums[nums.Length - k];
    }
}
/*
数组中的第K个最大元素
力扣官方题解
发布于 2020-06-28
49.7k
📺 视频题解

📖 文字题解
约定：假设这里数组的长度为 nn。

题目分析：本题希望我们返回数组排序之后的倒数第 kk 个位置。

方法一：基于快速排序的选择方法
思路和算法

我们可以用快速排序来解决这个问题，先对原数组排序，再返回倒数第 kk 个位置，这样平均时间复杂度是 O(n \log n)O(nlogn)，但其实我们可以做的更快。

首先我们来回顾一下快速排序，这是一个典型的分治算法。我们对数组 a[l \cdots r]a[l⋯r] 做快速排序的过程是（参考《算法导论》）：

分解： 将数组 a[l \cdots r]a[l⋯r] 「划分」成两个子数组 a[l \cdots q - 1]a[l⋯q−1]、a[q + 1 \cdots r]a[q+1⋯r]，使得 a[l \cdots q - 1]a[l⋯q−1] 中的每个元素小于等于 a[q]a[q]，且 a[q]a[q] 小于等于 a[q + 1 \cdots r]a[q+1⋯r] 中的每个元素。其中，计算下标 qq 也是「划分」过程的一部分。
解决： 通过递归调用快速排序，对子数组 a[l \cdots q - 1]a[l⋯q−1] 和 a[q + 1 \cdots r]a[q+1⋯r] 进行排序。
合并： 因为子数组都是原址排序的，所以不需要进行合并操作，a[l \cdots r]a[l⋯r] 已经有序。
上文中提到的 「划分」 过程是：从子数组 a[l \cdots r]a[l⋯r] 中选择任意一个元素 xx 作为主元，调整子数组的元素使得左边的元素都小于等于它，右边的元素都大于等于它， xx 的最终位置就是 qq。
由此可以发现每次经过「划分」操作后，我们一定可以确定一个元素的最终位置，即 xx 的最终位置为 qq，并且保证 a[l \cdots q - 1]a[l⋯q−1] 中的每个元素小于等于 a[q]a[q]，且 a[q]a[q] 小于等于 a[q + 1 \cdots r]a[q+1⋯r] 中的每个元素。所以只要某次划分的 qq 为倒数第 kk 个下标的时候，我们就已经找到了答案。 我们只关心这一点，至于 a[l \cdots q - 1]a[l⋯q−1] 和 a[q+1 \cdots r]a[q+1⋯r] 是否是有序的，我们不关心。

因此我们可以改进快速排序算法来解决这个问题：在分解的过程当中，我们会对子数组进行划分，如果划分得到的 qq 正好就是我们需要的下标，就直接返回 a[q]a[q]；否则，如果 qq 比目标下标小，就递归右子区间，否则递归左子区间。这样就可以把原来递归两个区间变成只递归一个区间，提高了时间效率。这就是「快速选择」算法。

我们知道快速排序的性能和「划分」出的子数组的长度密切相关。直观地理解如果每次规模为 nn 的问题我们都划分成 11 和 n - 1n−1，每次递归的时候又向 n - 1n−1 的集合中递归，这种情况是最坏的，时间代价是 O(n ^ 2)O(n 
2
 )。我们可以引入随机化来加速这个过程，它的时间代价的期望是 O(n)O(n)，证明过程可以参考「《算法导论》9.2：期望为线性的选择算法」。

代码


class Solution {
    Random random = new Random();

    public int findKthLargest(int[] nums, int k) {
        return quickSelect(nums, 0, nums.length - 1, nums.length - k);
    }

    public int quickSelect(int[] a, int l, int r, int index) {
        int q = randomPartition(a, l, r);
        if (q == index) {
            return a[q];
        } else {
            return q < index ? quickSelect(a, q + 1, r, index) : quickSelect(a, l, q - 1, index);
        }
    }

    public int randomPartition(int[] a, int l, int r) {
        int i = random.nextInt(r - l + 1) + l;
        swap(a, i, r);
        return partition(a, l, r);
    }

    public int partition(int[] a, int l, int r) {
        int x = a[r], i = l - 1;
        for (int j = l; j < r; ++j) {
            if (a[j] <= x) {
                swap(a, ++i, j);
            }
        }
        swap(a, i + 1, r);
        return i + 1;
    }

    public void swap(int[] a, int i, int j) {
        int temp = a[i];
        a[i] = a[j];
        a[j] = temp;
    }
}
复杂度分析

时间复杂度：O(n)O(n)，如上文所述，证明过程可以参考「《算法导论》9.2：期望为线性的选择算法」。
空间复杂度：O(\log n)O(logn)，递归使用栈空间的空间代价的期望为 O(\log n)O(logn)。
方法二：基于堆排序的选择方法
思路和算法

我们也可以使用堆排序来解决这个问题——建立一个大根堆，做 k - 1k−1 次删除操作后堆顶元素就是我们要找的答案。在很多语言中，都有优先队列或者堆的的容器可以直接使用，但是在面试中，面试官更倾向于让更面试者自己实现一个堆。所以建议读者掌握这里大根堆的实现方法，在这道题中尤其要搞懂「建堆」、「调整」和「删除」的过程。

友情提醒：「堆排」在很多大公司的面试中都很常见，不了解的同学建议参考《算法导论》或者大家的数据结构教材，一定要学会这个知识点哦! ^_^



代码


class Solution {
    public int findKthLargest(int[] nums, int k) {
        int heapSize = nums.length;
        buildMaxHeap(nums, heapSize);
        for (int i = nums.length - 1; i >= nums.length - k + 1; --i) {
            swap(nums, 0, i);
            --heapSize;
            maxHeapify(nums, 0, heapSize);
        }
        return nums[0];
    }

    public void buildMaxHeap(int[] a, int heapSize) {
        for (int i = heapSize / 2; i >= 0; --i) {
            maxHeapify(a, i, heapSize);
        } 
    }

    public void maxHeapify(int[] a, int i, int heapSize) {
        int l = i * 2 + 1, r = i * 2 + 2, largest = i;
        if (l < heapSize && a[l] > a[largest]) {
            largest = l;
        } 
        if (r < heapSize && a[r] > a[largest]) {
            largest = r;
        }
        if (largest != i) {
            swap(a, i, largest);
            maxHeapify(a, largest, heapSize);
        }
    }

    public void swap(int[] a, int i, int j) {
        int temp = a[i];
        a[i] = a[j];
        a[j] = temp;
    }
}
复杂度分析

时间复杂度：O(n \log n)O(nlogn)，建堆的时间代价是 O(n)O(n)，删除的总代价是 O(k \log n)O(klogn)，因为 k < nk<n，故渐进时间复杂为 O(n + k \log n) = O(n \log n)O(n+klogn)=O(nlogn)。
空间复杂度：O(\log n)O(logn)，即递归使用栈空间的空间代价。

public class Solution {
    public int FindKthLargest(int[] nums, int k) {
        QuickSort(nums,0, nums.Length - 1);
        return nums[nums.Length - k];
    }
         void QuickSort(int[] map, int left, int right)
        {
            do
            {
                int i = left;
                int j = right;
                int x = map[i + ((j - i) >> 1)];
                do
                {
                    //while (i < map.Length && CompareKeys(x, map[i]) > 0) i++;
                    //while (j >= 0 && CompareKeys(x, map[j]) < 0) j--;

                    while (i < map.Length && x> map[i]) i++;  
                    while (j >= 0 && x<map[j]) j--;
                    if (i > j) break;
                    if (i < j)
                    {
                        int temp = map[i];
                        map[i] = map[j];
                        map[j] = temp;
                    }
                    i++;
                    j--;
                } while (i <= j);
                if (j - left <= right - i)
                {
                    if (left < j) QuickSort(map, left, j);
                    left = i;
                }
                else
                {
                    if (i < right) QuickSort(map, i, right);
                    right = j;
                }
            } while (left < right);
        }
}
public class Solution {
    public int FindKthLargest(int[] nums, int k) {
        if (nums == null|| nums.Length < 1 || k > nums.Length)
            return 0;
        Array.Sort(nums);
        //QuickSort(nums, 0, nums.Length - 1);
        return nums[nums.Length - k];
    }
    void QuickSort(int[] nums, int low, int high) {
        if (low >= high)
            return;
        int preLow = low;
        int preHigh = high;
        
        int key = nums[low];
        while(low < high) {
            while(low < high && nums[high] >= key)
                high--;
            nums[low] = nums[high];
            while(low < high && nums[low] <= key)
                low++;
            nums[high] = nums[low];
        }
        nums[low] = key;
        
        if (preLow < low)
            QuickSort(nums, preLow, low - 1);
        if (low < preHigh)
            QuickSort(nums, low + 1, preHigh);
    }
}
public class Solution {
    public int FindKthLargest(int[] nums, int k) {
        if(nums.Length == 1){
            return nums[0] ;
        }
        int start = 0;
        int end = nums.Length;
        while(true){
            int poilt = QuickSort(nums,start,end);
            if(end - poilt - 1 < k){                
                if(end - poilt == k){
                    return nums[poilt];
                }
                k = k - end + poilt;
                end = poilt;
            }
            else if(k < end - poilt ) {
                start = poilt + 1;
            }
        }
    }
    private int QuickSort(int[] nums, int i, int j){
        if(i+1 == j){
            return i;
        }
        
        int t = new Random().Next(i,j);
        Swap(nums, i ,t);
        
        int start = i ;
        int temp = nums[i++];
        while(i<j){
            if(nums[i] <= temp){
                i++;
            }
            else{
                Swap(nums, --j ,i );
            }
        }
        Swap(nums , start ,j-1);
        return j-1;
    }

    private void Swap(int[] nums, int i, int j){
        if(i == j){
            return;
        }
        int temp = nums[i];
        nums[i] = nums[j];
        nums[j] = temp;
    }
}
public class Solution {
    public int FindKthLargest(int[] nums, int k) {
        var start = 0;
        var end = nums.Length - 1;
        return findKth(nums,start,end,nums.Length - k);    
    }
    
    private int findKth(int[] nums,int start,int end, int k){
        if(start>=end) return nums[k];
        int left = start,right = end;
        int pivot = nums[start + (end - start)/2];
        while(left<=right){
            while(left<=right && nums[left]<pivot) left++;
            while(left<=right && nums[right]>pivot) right--;
            if(left<=right){
                swap(nums,left,right);
                left++;right--;
            }
        }
        if(k<=right) return findKth(nums,start,right,k);
        if(k>=left) return findKth(nums,left,end,k);
        return nums[k];
    }
    
    private void swap(int[] nums,int left,int right){
        int temp = nums[left];
        nums[left] = nums[right];
        nums[right] = temp;
    }
    
}
public class Solution {
    public int FindKthLargest(int[] nums, int k) {
        if(nums == null || nums.Length == 0)return 0;
        //构建二叉堆,使用前k个元素构造
        for(int i = k / 2 - 1;i > -1;i--){
            Sink(nums,i,k);
        }
        int count = nums.Length;
        for(int i = k;i < count;i++){
            if(nums[i] > nums[0]){
                Swap(nums,0,i);
                Sink(nums,0,k);
            }
        }
        return nums[0];
    }
    
    public bool Less(int a,int b){
        return a - b <= 0;
    }
    
    public void Swap(int[] nums,int i,int j){
        int temp = nums[i];
        nums[i] = nums[j];
        nums[j] = temp;
    }
    
    public void Sink(int[] nums,int index,int count){
        int leftIdx = index * 2 + 1;        
        if(leftIdx >= count)return;
        int rightIdx = leftIdx + 1;
        int val = nums[index];
        int leftVal = nums[leftIdx];
        int rightVal = 0;
        int swapIdx = -1;
        if(rightIdx < count){
            rightVal = nums[rightIdx]; 
        }
        if(rightIdx < count && Less(rightVal,leftVal)){
            if(Less(rightVal,val)){
                Swap(nums,index,rightIdx);
                swapIdx = rightIdx;
            }
        }
        else{
            if(Less(leftVal,val)){
                Swap(nums,index,leftIdx);
                swapIdx = leftIdx;
            }
        }
        if(swapIdx > -1){
            Sink(nums,swapIdx,count);
        }
    }
}
public class Solution {
    public int FindKthLargest(int[] nums, int k) 
    {
        return Selection(nums, 0, nums.Length-1, k);
    }
    
    private int Selection(int[] nums, int start, int end, int k)
    {
        if(start == end)
            return nums[start];
        int index = Partion(nums, start, end);
        int rank = end - index + 1;
        
        if(rank > k)
        {
            return Selection(nums, index+1, end, k);
        }
        else if(rank < k)
        {
            return Selection(nums, start, index-1, k-rank);
        }
        else
        {
            return nums[index];
        }
    }
    
    private int Partion(int[] nums, int start, int end)
    {
        int pivot = nums[start];
        int i = start;
        for(int j = start + 1; j <= end; j++)
        {
            if(nums[j] < pivot)
            {
                i++;
                Swap(nums, i, j);
            }
        }
        Swap(nums, i, start);
        return i;
    }
    
    private void Swap(int[] nums, int i, int j)
    {
        if(i == j) return;
        int temp = nums[i];
        nums[i] = nums[j];
        nums[j] = temp;
    }
}
public class Solution {
    public int FindKthLargest(int[] nums, int k) 
    {
        return Selection(nums, 0, nums.Length-1, k);
    }
    
    private int Selection(int[] nums, int start, int end, int k)
    {
        if(start == end)
            return nums[start];
        int index = Partion(nums, start, end);
        int rank = end - index + 1;
        
        if(rank > k)
        {
            return Selection(nums, index+1, end, k);
        }
        else if(rank < k)
        {
            return Selection(nums, start, index-1, k-rank);
        }
        else
        {
            return nums[index];
        }
    }

    private int Partion(int[] nums, int start, int end)
    {
        int pivot = nums[end];
        int i = start - 1;
        for (int j = start; j <= end; j++)
        {
            if (nums[j] < pivot)
            {
                i++;
                Swap(nums, i, j);
            }
        }
        Swap(nums, i + 1, end);
        return i + 1;
    }

    private void Swap(int[] nums, int i, int j)
    {
        if (i == j) return;
        int temp = nums[i];
        nums[i] = nums[j];
        nums[j] = temp;
    }
}
*/
