using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个整数数组 a，其中1 ≤ a[i] ≤ n （n为数组长度）, 其中有些元素出现两次而其他元素出现一次。

找到所有出现两次的元素。

你可以不用到任何额外空间并在O(n)时间复杂度内解决这个问题吗？

示例：

输入:
[4,3,2,7,8,2,3,1]

输出:
[2,3] 
*/
/// <summary>
/// https://leetcode-cn.com/problems/find-all-duplicates-in-an-array/
/// 442. 数组中重复的数据
/// https://blog.csdn.net/weixin_42690125/article/details/81041119
/// </summary>
class FindAllDuplicatesInAnArraySolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public IList<int> FindDuplicates(int[] nums)
    {
        List<int> ret = new List<int>();
        for (int i = 0; i < nums.Length; ++i)
        {
            int idx = Math.Abs(nums[i]) - 1;
            if (nums[idx] < 0) ret.Add(idx + 1);
            else nums[idx] = -nums[idx];
        }
        return ret;
    }
}
/*
public class Solution {
    public IList<int> FindDuplicates(int[] nums) {
        int sum = 0;
        IList<int> result = new List<int>();
        for (int i = 0; i<nums.Length; i++)
        {
            int index = Math.Abs(nums[i])-1;
            if(nums[index]<0)
            {
                result.Add(Math.Abs(nums[i]));
            }
            else
            {
                nums[index] = (-1)*nums[index];
            }
        }
        return result;
        
    }
}
public class Solution {
    public IList<int> FindDuplicates(int[] nums) {
        HashSet<int> result = new HashSet<int>();
        for(int i = 0; i < nums.Length; i++)
        {
            while(nums[i]!=i+1)
            {
                if(nums[nums[i]-1] == nums[i])
                {
                    result.Add(nums[i]);
                    break;
                }
                int temp = nums[i]-1;
                nums[i]^=nums[temp];
                nums[temp]^=nums[i];
                nums[i]^=nums[temp];
            }
        }
        return new List<int>(result);
    }
}
public class Solution {
    public IList<int> FindDuplicates(int[] nums) {
        var ans = new List<int>();
        int ai;
        for (int i = 0; i < nums.Length; i++) {
            ai = Math.Abs(nums[i]);
            if (nums[ai - 1] > 0)
                nums[ai - 1] *= -1;
            else
                ans.Add(ai);
        }
        return ans;
    }
}
*/
