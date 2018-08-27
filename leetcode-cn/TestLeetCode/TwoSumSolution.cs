using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


class TwoSumSolution
{
    public void Test()
    {
        int[] nums = new int[] {3, 2, 4};
        int k = 6;
        var ret = TwoSum((int[]) nums.Clone(), k);

        Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int[] TwoSum(int[] nums, int target)
    {
        for (int index0 = 0; index0 < nums.Length - 1; index0++)
        {
            var num0 = nums[index0];
            
            for (int index1 = index0 + 1; index1 < nums.Length; index1++)
            {
                var num1 = nums[index1];
                
                if (target == num0 + num1) return new int[] {index0, index1};
            }
        }
        return new int[] {-1, -1};
    }
}