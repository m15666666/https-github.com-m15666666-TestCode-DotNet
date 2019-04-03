using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
给出一个由无重复的正整数组成的集合，找出其中最大的整除子集，子集中任意一对 (Si，Sj) 都要满足：Si % Sj = 0 或 Sj % Si = 0。

如果有多个目标子集，返回其中任何一个均可。

 

示例 1:

输入: [1,2,3]
输出: [1,2] (当然, [1,3] 也正确)
示例 2:

输入: [1,2,4,8]
输出: [1,2,4,8] 
*/
/// <summary>
/// https://leetcode-cn.com/problems/largest-divisible-subset/
/// 368. 最大整除子集
/// https://blog.csdn.net/qq_32805671/article/details/88060749
/// </summary>
class LargestDivisibleSubsetSolution
{
    public void Test()
    {
        int[] nums = new int[] { 3, 4, 16, 8};
        var ret = LargestDivisibleSubset(nums);
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public IList<int> LargestDivisibleSubset(int[] nums)
    {
        if (nums == null || nums.Length == 0) return new int[0];
        int[] maxCount = new int[nums.Length];
        for (int i = 0; i < maxCount.Length; i++) maxCount[i] = 1;
        int[] preIndex = new int[nums.Length];
        for (int i = 0; i < preIndex.Length; i++) preIndex[i] = -1;

        Array.Sort(nums);
        int maxre = 1;
        int maxi = 0;
        var len = nums.Length;
        for (var i = 1; i < len; ++i)
        {
            int maxtmp = 0;
            var half = nums[i] / 2;
            for (var j = 0; j < i && nums[j] <= half; j++)
            {
                if (nums[i] % nums[j] == 0 && maxtmp < maxCount[j])
                {
                    maxtmp = maxCount[j];
                    maxCount[i] = maxtmp + 1;
                    preIndex[i] = j;
                }
            }
            if (maxtmp + 1 > maxre)
            {
                maxre = maxtmp + 1;
                maxi = i;
            }
        }

        var res = new int[maxre];
        maxre--;
        for (var i = maxi; i >= 0; i = preIndex[i], maxre--) res[maxre] = nums[i];

        return res;
    }
}
/*
public class Solution {
    public IList<int> LargestDivisibleSubset(int[] nums) {
        IList<int> results = new List<int>();
        
        if(nums.Length == 0) 
        {
            return results;
        }
        
        //maxSubsetCounts[i]存放包含nums[i]的整除序列的数目
        int[] maxSubsetCounts = new int[nums.Length];
        //lastStartIndexs[i]存放nums[i]对应的最大整除序列的上一个节点的下标
        int[] lastStartIndexs = new int[nums.Length];
        
        for(int i = 0; i < nums.Length; ++i) {
            //初始化为1，表示所有节点能够被自己整除
            maxSubsetCounts[i] = 1;
            //初始化为自己的下标，表示还没有通过前面的那个节点，得到最大的整除序列
            lastStartIndexs[i] = i;
        }
        
        //先排序，默认排序顺序为升序
        Array.Sort(nums);
        
        int maxLength = 1;
        int maxIndex = 0;
        for(int i = 0; i < nums.Length; ++i) {
            for(int j = 0; j < i; ++j) {
                if(nums[i] % nums[j] == 0 && (maxSubsetCounts[j] + 1) > maxSubsetCounts[i]) {
                    maxSubsetCounts[i] = maxSubsetCounts[j] + 1;
                    lastStartIndexs[i] = j; 
                }
                
                if(maxSubsetCounts[i] > maxLength) {
                    maxLength = maxSubsetCounts[i];
                    maxIndex = i;
                }
            }
        }
        
        while(maxIndex != lastStartIndexs[maxIndex]) {
            results.Add(nums[maxIndex]);
            maxIndex = lastStartIndexs[maxIndex];
        }
        
        //添加最后一个数字
        results.Add(nums[maxIndex]);
        
        return results;
    }
} 
*/
