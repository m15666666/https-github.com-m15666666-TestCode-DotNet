using System;

/*
给定一些标记了宽度和高度的信封，宽度和高度以整数对形式 (w, h) 出现。当另一个信封的宽度和高度都比这个信封大的时候，这个信封就可以放进另一个信封里，如同俄罗斯套娃一样。

请计算最多能有多少个信封能组成一组“俄罗斯套娃”信封（即可以把一个信封放到另一个信封里面）。

说明:
不允许旋转信封。

示例:

输入: envelopes = [[5,4],[6,4],[6,7],[2,3]]
输出: 3
解释: 最多信封的个数为 3, 组合为: [2,3] => [5,4] => [6,7]。

*/

/// <summary>
/// https://leetcode-cn.com/problems/russian-doll-envelopes/
/// 354. 俄罗斯套娃信封问题
///
///
///
/// </summary>
internal class RussianDollEnvelopesSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int MaxEnvelopes(int[][] envelopes)
    {
        if (envelopes == null || envelopes.Length == 0 || envelopes[0] == null || envelopes[0].Length == 0) return 0;

        static int WidthAscThenHeightDesc(int[] a1, int[] a2) => a1[0] == a2[0] ? a2[1] - a1[1] : a1[0] - a2[0];
        Array.Sort(envelopes, WidthAscThenHeightDesc);

        int[] heights = new int[envelopes.Length];
        for (int i = 0; i < envelopes.Length; ++i) heights[i] = envelopes[i][1];
        return LengthOfLIS(heights);

        static int LengthOfLIS(int[] nums)
        {
            int n = nums.Length;

            // 持续添加、替换刷新的向量，与dp有不同，如果考虑节省内存，可以使用List<int>代替int[]，自己写二分查找算法部分。
            int[] vector = new int[n];
            
            // 从第二个元素开始查找，可读性好一些，同时避免不同库BinarySearch边界处理不一致导致问题。
            int currentLength = 1;
            vector[0] = nums[0];

            for (int i = 1; i < n; i++)
            {
                int num = nums[i];
                int insertIndex = Array.BinarySearch(vector, 0, currentLength, num);

                // 直接使用 ~ 替代 -(i+1) 可读性好一些。
                if (insertIndex < 0) insertIndex = ~insertIndex; //i = -(i + 1);

                vector[insertIndex] = num;
                if (insertIndex == currentLength) currentLength++;
            }
            return currentLength;
        }
    }
}

/*

俄罗斯套娃信封问题
力扣 (LeetCode)
发布于 2020-01-03
18.1k
方法：排序 + 最长递增子序列
该问题为最长递增子序列的二维问题。

我们要找到最长的序列，且满足 seq[i+1] 中的元素大于 seq[i] 中的元素。

该问题是输入是按任意顺序排列的——我们不能直接套用标准的 LIS 算法，需要先对数据进行排序。我们如何对数据进行排序，以便我们的 LIS 算法总能找到最佳答案？

我们可以在这里找到最长递增子序列的解决方法。如果您不熟悉该算法，请先理解该算法，因为它是解决此问题的前提条件。

算法：
假设我们知道了信封套娃顺序，那么从里向外的顺序必须是按 w 升序排序的子序列。

在对信封按 w 进行排序以后，我们可以找到 h 上最长递增子序列的长度。、

我们考虑输入 [[1，3]，[1，4]，[1，5]，[2，3]]，如果我们直接对 h 进行 LIS 算法，我们将会得到 [3，4，5]，显然这不是我们想要的答案，因为 w 相同的信封是不能够套娃的。

为了解决这个问题。我们可以按 w 进行升序排序，若 w 相同则按 h 降序排序。则上述输入排序后为 [[1，5]，[1，4]，[1，3]，[2，3]]，再对 h 进行 LIS 算法可以得到 [5]，长度为 1，是正确的答案。这个例子可能不明显。

我们将输入改为 [[1，5]，[1，4]，[1，2]，[2，3]]。则提取 h 为 [5，4，2，3]。我们对 h 进行 LIS 算法将得到 [2，3]，是正确的答案。


class Solution {

    public int lengthOfLIS(int[] nums) {
        int[] dp = new int[nums.length];
        int len = 0;
        for (int num : nums) {
            int i = Arrays.binarySearch(dp, 0, len, num);
            if (i < 0) {
                i = -(i + 1);
            }
            dp[i] = num;
            if (i == len) {
                len++;
            }
        }
        return len;
    }

    public int maxEnvelopes(int[][] envelopes) {
        // sort on increasing in first dimension and decreasing in second
        Arrays.sort(envelopes, new Comparator<int[]>() {
            public int compare(int[] arr1, int[] arr2) {
                if (arr1[0] == arr2[0]) {
                    return arr2[1] - arr1[1];
                } else {
                    return arr1[0] - arr2[0];
                }
           }
        });
        // extract the second dimension and run LIS
        int[] secondDim = new int[envelopes.length];
        for (int i = 0; i < envelopes.length; ++i) secondDim[i] = envelopes[i][1];
        return lengthOfLIS(secondDim);
    }
}
复杂度分析

时间复杂度：O(N \log N)O(NlogN)。其中 NN 是输入数组的长度。排序和 LIS 算法都是 O(N \log N)O(NlogN)。
空间复杂度：O(N)O(N)，lis 函数需要一个数组 dp，它的大小可达 NN。另外，我们使用的排序算法也可能需要额外的空间。

public class Solution {
    public int MaxEnvelopes(int[][] envelopes) {
        int n = envelopes.Length;
        int[] arr = new int[n];
        Array.Sort(envelopes, (x1,x2)=>{
              
              if(x1[0] == x2[0]){
                  return x2[1] - x1[1];
              }else{
                  return x1[0] - x2[0];
              }

        } );

      for(int i = 0; i<n; i++){
          arr[i] = envelopes[i][1];
      }

      return GetLIS(arr);
    }

    int GetLIS(int[] nums){
	   int n = nums.Length;
	   int[] dp = new int[n];
	   int len = 0;
	   for(int i = 0; i<n; i++){
		   int idx = BinarySearch(dp, 0, len, nums[i]);
		   if(idx < 0){
			   idx = 0;
		   }
		   dp[idx] = nums[i];
		   if(idx == len){
			   len++;
		   }
	   }
	   return len;
    }

    int BinarySearch(int[] nums, int start, int end, int target){
          while(start < end){
             int mid = start + (end - start)/2;
             if(nums[mid] >= target){
                 end = mid;
             }else{
                 start = mid+ 1;
             }
          }

          return start;
    }
}

public class Solution {
    public int MaxEnvelopes(int[][] envelopes) {
		Array.Sort(envelopes, new IntComparer());
		int[] secondDim = new int[envelopes.Length];
		for (int i = 0; i < envelopes.Length; ++i) secondDim[i] = envelopes[i][1];
		return LengthOfLIS(secondDim);
    }
    public int LengthOfLIS(int[] nums)
	{
		int[] dp = new int[nums.Length];
		int len = 0;
		foreach (int num in nums)
		{
			int i = Array.BinarySearch(dp, 0, len, num);
			if (i < 0)
			{
				i = -(i + 1);
			}
			dp[i] = num;
			if (i == len)
			{
				len++;
			}
		}
		return len;
	}
    
    public class IntComparer : IComparer<int[]>
	{
		public int Compare(int[] arr1, int[] arr2)
		{
		
			if (arr1[0] == arr2[0])
			{
				return arr2[1] - arr1[1];
			}
			else
			{
				return arr1[0] - arr2[0];
			}
		}
	}
}

public class Solution 
{
    public int MaxEnvelopes(int[][] envelopes) 
    {
        Array.Sort(envelopes, (x, y) => x[0] == y[0] ? y[1].CompareTo(x[1]) : x[0].CompareTo(y[0]));        
        var arr = new int[envelopes.Length];
        int length = 0;
        for(int i = 0; i < envelopes.Length; i++)
        {
            var index = Array.BinarySearch(arr, 0, length, envelopes[i][1]);
            if(index < 0) index = ~index;
            arr[index] = envelopes[i][1];
            if(index == length) length++;
        }
        
        return length;
    }
}

public class Solution {
    public int MaxEnvelopes(int[][] envelopes) {
        int n = envelopes.Length;
        int[] arr = new int[n];
        Array.Sort(envelopes, (x1,x2)=>{
              
              if(x1[0] == x2[0]){
                  return x2[1] - x1[1];
              }else{
                  return x1[0] - x2[0];
              }

        } );

      for(int i = 0; i<n; i++){
          arr[i] = envelopes[i][1];
      }


      return GetLIS(arr);

    }

    int GetLIS(int[] nums){
           int n = nums.Length;
           // int[] dp = new int[n];
           int len = 0;

           for(int i = 0; i<n; i++){
               int idx = BinarySearch(nums, 0, len, nums[i]);

               if(idx < 0){
                   idx = 0;
               }

               nums[idx] = nums[i];

               if(idx == len){
                   len++;
               }
 

           }

           return len;
    }

    int BinarySearch(int[] nums, int start, int end, int target){
	  while(start < end){
		 int mid = start + (end - start)/2;

		 if(nums[mid] >= target){
			 end = mid;
		 }else{
			 start = mid+ 1;
		 }
	  }
	  return start;
    }
}
*/