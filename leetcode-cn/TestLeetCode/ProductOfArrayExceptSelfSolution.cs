using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// https://leetcode-cn.com/problems/product-of-array-except-self/
/// 238. 除自身以外数组的乘积
/// 给定长度为 n 的整数数组 nums，其中 n > 1，返回输出数组 output ，
/// 其中 output[i] 等于 nums 中除 nums[i] 之外其余各元素的乘积。
/// http://www.cnblogs.com/wmx24/p/9577751.html
/// https://www.cnblogs.com/mr-stn/p/8951354.html
/// </summary>
class ProductOfArrayExceptSelfSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    public int[] ProductExceptSelf(int[] nums)
    {
        if (nums == null || nums.Length == 0) return new int[0];
        int[] ret = new int[nums.Length];
        ret[ret.Length - 1] = 1;

        for( int index = ret.Length - 2; -1 < index; index--)
        {
            ret[index] = nums[index + 1] * ret[index + 1];
        }
        for (int index = 1; index < nums.Length; index++)
        {
            var v = nums[index - 1];
            ret[index] *= v;
            nums[index] *= v;
        }
        return ret;
    }
}