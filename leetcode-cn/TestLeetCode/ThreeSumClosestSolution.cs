using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个包括 n 个整数的数组 nums 和 一个目标值 target。找出 nums 中的三个整数，使得它们的和与 target 最接近。返回这三个数的和。假定每组输入只存在唯一答案。

例如，给定数组 nums = [-1，2，1，-4], 和 target = 1.

与 target 最接近的三个数的和为 2. (-1 + 2 + 1 = 2).
*/
/// <summary>
/// https://leetcode-cn.com/problems/3sum-closest/
/// 16. 最接近的三数之和
/// 
/// </summary>
class ThreeSumClosestSolution
{
    public void Test()
    {
        
        int[] nums = new int[] {-1,0,1,1,55};
        var ret = ThreeSumClosest(nums, 3);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    public int ThreeSumClosest(int[] nums, int target)
    {
        if (nums == null || nums.Length < 3) return 0;

        Array.Sort(nums);

        int lastClosest = nums[0] + nums[1] + nums[2];
        if (lastClosest == target) return lastClosest;

        int lastDiff = target < lastClosest ? lastClosest - target : target - lastClosest;

        int upper = nums.Length - 2;
        int end = nums.Length - 1;
        
        int sum;
        int difference;
        int left, right;
        for (int i = 0; i < upper; i++)
        {
            left = i + 1;
            right = end;
            while (left < right)
            {
                sum = nums[i] + nums[left] + nums[right];
                if (sum == target) return sum;

                difference = target < sum ? sum - target : target - sum;

                if (difference < lastDiff)
                {
                    lastClosest = sum;
                    lastDiff = difference;
                }

                if (sum < target) ++left;
                else --right;
            }
        }

        return lastClosest;
    }
}
/*

画解算法：16. 最接近的三数之和
灵魂画手
发布于 9 个月前
37.8k
解题方案
思路
标签：排序和双指针
本题目因为要计算三个数，如果靠暴力枚举的话时间复杂度会到 O(n^3)O(n 
3
 )，需要降低时间复杂度
首先进行数组排序，时间复杂度 O(nlogn)O(nlogn)
在数组 nums 中，进行遍历，每遍历一个值利用其下标i，形成一个固定值 nums[i]
再使用前指针指向 start = i + 1 处，后指针指向 end = nums.length - 1 处，也就是结尾处
根据 sum = nums[i] + nums[start] + nums[end] 的结果，判断 sum 与目标 target 的距离，如果更近则更新结果 ans
同时判断 sum 与 target 的大小关系，因为数组有序，如果 sum > target 则 end--，如果 sum < target 则 start++，如果 sum == target 则说明距离为 0 直接返回结果
整个遍历过程，固定值为 n 次，双指针为 n 次，时间复杂度为 O(n^2)O(n 
2
 )
总时间复杂度：O(nlogn) + O(n^2) = O(n^2)O(nlogn)+O(n 
2
 )=O(n 
2
 )
代码
class Solution {
    public int threeSumClosest(int[] nums, int target) {
        Arrays.sort(nums);
        int ans = nums[0] + nums[1] + nums[2];
        for(int i=0;i<nums.length;i++) {
            int start = i+1, end = nums.length - 1;
            while(start < end) {
                int sum = nums[start] + nums[end] + nums[i];
                if(Math.abs(target - sum) < Math.abs(target - ans))
                    ans = sum;
                if(sum > target)
                    end--;
                else if(sum < target)
                    start++;
                else
                    return ans;
            }
        }
        return ans;
    }
}
画解


点击我的头像加关注，和我一起打卡天天算法

下一篇：最接近的三数之和



最接近的三数之和
然然
发布于 13 天前
282
解题思路
采用双指针--哨兵模式优化遍历性能，将最小的距离差保留下来，便可求出最优解
此题测试用例很少，当数据量很大时，此程序性能堪忧

代码
class Solution:
    def threeSumClosest(self, nums, target):
        nums.sort()
        length = len(nums)
        min_dis = float('inf')
        for k in range(length - 2):
            # 哨兵i j
            i, j = k + 1, length - 1
            while i < j and 0 < abs(min_dis):
                sum_num = nums[i] + nums[j] + nums[k]
                if sum_num - target == 0:
                    return sum_num
                elif sum_num - target > 0:
                    # 更新最小距离
                    if abs(min_dis) > abs(sum_num - target):
                        min_dis = sum_num - target
                    j -= 1
                    while i < j and nums[j] == nums[j + 1]:
                        j -= 1
                else:
                     # 更新最小距离
                    if abs(min_dis) > abs(sum_num - target):
                        min_dis = sum_num - target
                    i += 1
                    while i < j and nums[i] == nums[i - 1]:
                        i += 1
        return target + min_dis
下一篇：对双指针法进行一点优化，达到2ms，击败100%

public class Solution {
    public int ThreeSumClosest(int[] nums, int target) {
            System.Array.Sort(nums);
            int minimumSum = 9999;
            int returnInt = 9999;
            for(int i = 0; i < nums.Length - 2; i++) {
                if(i > 0 && nums[i] == nums[i - 1]) continue;
                int L = i + 1;
                int R = nums.Length - 1;
                while(L < R) {
                    int sum = nums[i] + nums[L] + nums[R];
                    if(System.Math.Abs(sum - target) < minimumSum) {
                        minimumSum = System.Math.Abs(sum - target);
                        returnInt = sum;
                    }
                    if(sum == target) return target;
                    else if(sum > target) R--;
                    else if(sum < target) L++;
                }
            }

            return returnInt;
        }
} 
*/
