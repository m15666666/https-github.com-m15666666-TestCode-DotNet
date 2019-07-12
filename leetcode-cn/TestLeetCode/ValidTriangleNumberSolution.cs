using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个包含非负整数的数组，你的任务是统计其中可以组成三角形三条边的三元组个数。

示例 1:

输入: [2,2,3,4]
输出: 3
解释:
有效的组合是: 
2,3,4 (使用第一个 2)
2,3,4 (使用第二个 2)
2,2,3
注意:

数组长度不超过1000。
数组里整数的范围为 [0, 1000]。 
*/
/// <summary>
/// https://leetcode-cn.com/problems/valid-triangle-number/
/// 611. 有效三角形的个数
/// 
/// </summary>
class ValidTriangleNumberSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int TriangleNumber(int[] nums)
    {
        if (nums == null || nums.Length < 3) return 0;

        int ret = 0;
        Array.Sort(nums);
        for( int i = nums.Length - 1; 1 < i; i--)
        {
            int left = 0;
            int right = i - 1;
            while( left < right)
            {
                if (nums[i] < nums[left] + nums[right])
                {
                    ret += (right - left);
                    --right;
                }
                else ++left;
            }
        }

        return ret;
    }
}
/*
public class Solution {
    public int TriangleNumber(int[] nums) {
         Array.Sort(nums);
            if (nums.Length < 3)
                return 0;
            int s = 0;
            for (int i = nums.Length-1; i >=2 ; i--)
            {
                int r = 0, l = i-1;
                while (r<l)
                {
                    if (nums[r] + nums[l] > nums[i])
                    {
                        s += l - r;
                        l--;
                    }
                    else
                    {
                        r++;
                    }
                }
               
            }
            return s;
    }
}

*/
