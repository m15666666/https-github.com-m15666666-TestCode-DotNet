using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
给定一个整数数组 nums，其中恰好有两个元素只出现一次，
其余所有元素均出现两次。 找出只出现一次的那两个元素。

示例 :

输入: [1,2,1,3,2,5]
输出: [3,5]
注意：

结果输出的顺序并不重要，对于上面的例子， [5, 3] 也是正确答案。
你的算法应该具有线性时间复杂度。你能否仅使用常数空间复杂度来实现？
     
*/

/// <summary>
/// https://leetcode-cn.com/problems/single-number-iii/
/// 260. 只出现一次的数字 III
/// </summary>
class SingleNumberIIISolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int[] SingleNumber(int[] nums)
    {
        if (nums == null || nums.Length == 0) return new int[0];

        List<int> ret = new List<int>();
        Array.Sort(nums);

        int count = 0;
        int prevV = nums[0];

        for( int index = 0; index < nums.Length; index++ )
        {
            var v = nums[index];
            if( count == 0)
            {
                prevV = v;
                ++count;
                continue;
            }

            if( prevV == v )
            {
                count = 0;
                continue;
            }

            ret.Add( prevV );
            prevV = v;
            count = 1;
        }

        if(count == 1) ret.Add(prevV);

        return ret.ToArray();
    }
}
/*
//别人的算法
public class Solution {
    public int[] SingleNumber(int[] nums) {
        if (nums == null || nums.Length <2)
        {
            return null;
        }
        
        int temp = GetResult(nums, 0, nums.Length - 1);
       
        int n = 0;
        while ((temp & 1) == 0)
        {
            temp = temp >> 1;
            n++;
        }
        
        int one = 0;
        int two = 0;
        
        for (int i = 0; i < nums.Length; i++)
        {
            if (((nums[i]>> n) & 1) == 1)
            {
                one ^= nums[i];
            }
            else
            {
                two ^= nums[i];
            }
        }
        
        
        return new int[]{one, two};
    }
    
    int GetResult(int[] array, int start, int end)
    {
        int result = 0;
        for (int i = start; i <= end; i++)
        {
            result ^= array[i];
        }
        return result;
    }
}
     
     
     
*/
