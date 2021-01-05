using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
给定一个由正整数组成且不存在重复数字的数组，找出和为给定目标正整数的组合的个数。

示例:

nums = [1, 2, 3]
target = 4

所有可能的组合为：
(1, 1, 1, 1)
(1, 1, 2)
(1, 2, 1)
(1, 3)
(2, 1, 1)
(2, 2)
(3, 1)

请注意，顺序不同的序列被视作不同的组合。

因此输出为 7。
进阶：
如果给定的数组中含有负数会怎么样？
问题会产生什么变化？
我们需要在题目中添加什么限制来允许负数的出现？
*/
/// <summary>
/// https://leetcode-cn.com/problems/combination-sum-iv/
/// 377. 组合总和 Ⅳ
/// https://blog.csdn.net/abc15766228491/article/details/82949256
/// </summary>
class CombinationSumIVSolution
{
    public void Test()
    {
        var ret = CombinationSum4(new int[] { 4,3,2 }, 32);
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int CombinationSum4(int[] nums, int target)
    {
        if (nums == null || nums.Length == 0) return 0;

        Array.Sort(nums);

        int[] dp = new int[target+1];
        dp[0] = 1;
        for (int i = 1; i <= target; i++)
        {
            // 这里遍历从i到target的所有值
            foreach( var v in nums)
            {
                // 这一步叠加生成dp[i]
                if (v <= i) dp[i] += dp[i - v];
                else break;
            }
        }
        return dp[target];
    }
}
/*
public class Solution
{
    Dictionary<int, int> _dict;
    public int CombinationSum4(int[] nums, int target)
    {
        _dict = new Dictionary<int, int>();
        Array.Sort(nums);
        return Down(nums, target);
    }
    private int Down(int[] nums, int target)
    {
        if (target <= 0) return 0;
        if (_dict.ContainsKey(target)) return _dict[target];
        int _len = nums.Length;
        int _res = 0;
        for (int i = 0; i < _len; i++)
        {
            if (nums[i] > target) break;
            else if (target == nums[i]) _res++;
            else _res += Down(nums, target - nums[i]);
        }
        _dict[target]=_res;
        return _res;
    }
}

public class Solution {
    public int CombinationSum4(int[] nums, int target) {
        Array.Sort(nums);
        if(target==0) return 1;
        if(nums.Length==0) return 0;
        int[] result=new int[target+1];
        result[0]=1;
        for(int i=1;i<=target;i++)
        {
            for(int j=0;j<nums.Length;j++)
            {
                if(nums[j]>i) break;
                
                int preResultIndex=i-nums[j];
                if(result[preResultIndex]!=0)
                {
                    result[i]+=result[preResultIndex];
                }
            }
        }
        return result[target];
    }
}


*/
