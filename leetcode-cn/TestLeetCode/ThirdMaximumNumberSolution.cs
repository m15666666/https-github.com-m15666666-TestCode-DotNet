/*
给你一个非空数组，返回此数组中 第三大的数 。如果不存在，则返回数组中最大的数。

 

示例 1：

输入：[3, 2, 1]
输出：1
解释：第三大的数是 1 。
示例 2：

输入：[1, 2]
输出：2
解释：第三大的数不存在, 所以返回最大的数 2 。
示例 3：

输入：[2, 2, 3, 1]
输出：1
解释：注意，要求返回第三大的数，是指第三大且唯一出现的数。
存在两个值为2的数，它们都排第二。
 

提示：

1 <= nums.length <= 104
-231 <= nums[i] <= 231 - 1
 

进阶：你能设计一个时间复杂度 O(n) 的解决方案吗？

通过次数44,458提交次数124,636

*/

/// <summary>
/// https://leetcode-cn.com/problems/third-maximum-number/
/// 414. 第三大的数
///
///
/// </summary>
internal class ThirdMaximumNumberSolution
{
    public int ThirdMax(int[] nums)
    {
        int max1 = int.MinValue;// 第一大
        int max2 = max1, max3 = max1; // 第二、第三大
        int min = int.MaxValue;

        foreach (int num in nums)
        {
            if (max3 < num && num != max2 && num != max1)
            {
                if (max1 < num)
                {
                    max3 = max2;
                    max2 = max1;
                    max1 = num;
                }
                else if (max2 < num)
                {
                    max3 = max2;
                    max2 = num;
                }
                else max3 = num;
            }
            if (num < min) min = num;
        }

        if (max2 == int.MinValue) return max1;

        // min的作用是用于排除第三大就是Integer.MIN_VALUE的情况
        if (max3 == int.MinValue && min != int.MinValue) return max1;

        return max3;
    }
}

/*
class Solution {
    public int thirdMax(int[] nums) {
        int max1 = Integer.MIN_VALUE;
        int max2 = max1,max3 = max1;
        int min = Integer.MAX_VALUE;

        for(int i=0;i<nums.length;i++){
            if(nums[i] > max3 && nums[i] != max2 && nums[i] != max1){
                if(nums[i] > max1){
                    max3 = max2;
                    max2 = max1;
                    max1 = nums[i];
                }else if(nums[i] > max2){
                    max3 = max2;
                    max2 = nums[i];
                }else  
                    max3 = nums[i];
            }
            min = Math.min(min,nums[i]);
        }

        //没有第二大，就返回第一大。
        if(max2 == Integer.MIN_VALUE) return max1;

        //不存在第三大的数，可能是存在多个相同的第二大的数。
        //min的作用是用于排除第三大就是Integer.MIN_VALUE的情况
        if(max3 == Integer.MIN_VALUE && min != Integer.MIN_VALUE) return max1;
        
        return max3; //存在第三大
    }
}

public class Solution {
    public int ThirdMax(int[] nums) {
		long f, s, t;
        int count = 0;
        f = long.MinValue;
        s = long.MinValue;
        t = long.MinValue;
        for (int i = 0; i < nums.Length; i++)
        {
            if (nums[i] > f)
            {
                count++;
                t = s;
                s = f;
                f = nums[i];
            }
            else if(nums[i] > s && nums[i] < f)
            {
                count++;
                t = s;
                s = nums[i];
            }
            else if(nums[i] > t && nums[i] < s)
            {
                count++;
                t = nums[i];
            }
        }
        if (count < 3) return (int)f;
        else return (int)t;

    }
}
*/