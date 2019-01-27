using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
给定一个包含 n + 1 个整数的数组 nums，其数字都在 1 到 n 之间（包括 1 和 n），
可知至少存在一个重复的整数。假设只有一个重复的整数，找出这个重复的数。

示例 1:

输入: [1,3,4,2,2]
输出: 2
示例 2:

输入: [3,1,3,4,2]
输出: 3
说明：

不能更改原数组（假设数组是只读的）。
只能使用额外的 O(1) 的空间。
时间复杂度小于 O(n2) 。
数组中只有一个重复的数字，但它可能不止重复出现一次。 
     
     
     
*/
/// <summary>
/// https://leetcode-cn.com/problems/find-the-duplicate-number/
/// 287. 寻找重复数
/// https://blog.csdn.net/qq_39745284/article/details/82419207
/// https://blog.csdn.net/weixin_39789689/article/details/82420653
/// </summary>
class FindTheDuplicateNumberSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int FindDuplicate(int[] nums)
    {
        int speedX1 = 0;
        int speedX2 = 0;

        do
        {
            speedX1 = nums[speedX1];
            speedX2 = nums[nums[speedX2]];
        } while (speedX1 != speedX2);

        speedX1 = 0;
        while (speedX1 != speedX2)
        {
            speedX1 = nums[speedX1];
            speedX2 = nums[speedX2];
        }

        return speedX1;
    }
}
/*
//别人的算法 
public class Solution {
    public int FindDuplicate(int[] nums) {
    int slow = 0;
    int fast = 0;
    do{
        slow = nums[slow];
        fast = nums[nums[fast]];
    }while(slow != fast);
    
    fast = 0;
    
    while(slow != fast){
        slow = nums[slow];
        fast = nums[fast];
        
    }
    
    return slow;
    }
}
public class Solution {
    public int FindDuplicate(int[] nums) {
        if(nums == null || nums.Length == 0)
            return 0;
        
        var start = 1;
        var end = nums.Length - 1;
        while(start < end)
        {
            var middle = start + (end-start)/2;
            var count = 0;
            for(var i=0; i<nums.Length; i++)
            {
                if( nums[i] <= middle)
                {
                    count++;
                }
            }
            
            if(count > middle)
            {
                end = middle;
            }
            else
            {
                start=middle+1;
            }
        }
        return start;
    }
}
public class Solution {
    public int FindDuplicate(int[] nums) {
       for (int i = 0; i < nums.Length; i++)
            {
                var useIndex = Math.Abs(nums[i]);

                if (nums[useIndex] < 0)
                {
                    return useIndex;
                }

                nums[useIndex] = - nums[useIndex];
           
            }

            return -1;
    }
}
     
*/
