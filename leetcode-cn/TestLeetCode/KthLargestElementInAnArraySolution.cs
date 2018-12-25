using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


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
//别人的算法
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
