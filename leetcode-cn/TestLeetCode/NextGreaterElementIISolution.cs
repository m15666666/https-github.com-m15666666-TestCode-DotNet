using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个循环数组（最后一个元素的下一个元素是数组的第一个元素），输出每个元素的下一个更大元素。数字 x 的下一个更大的元素是按数组遍历顺序，这个数字之后的第一个比它更大的数，这意味着你应该循环地搜索它的下一个更大的数。如果不存在，则输出 -1。

示例 1:

输入: [1,2,1]
输出: [2,-1,2]
解释: 第一个 1 的下一个更大的数是 2；
数字 2 找不到下一个更大的数； 
第二个 1 的下一个最大的数需要循环搜索，结果也是 2。
注意: 输入数组的长度不会超过 10000。 
*/
/// <summary>
/// https://leetcode-cn.com/problems/next-greater-element-ii/
/// 503. 下一个更大元素 II
/// https://blog.csdn.net/zy0707ok/article/details/82011671
/// </summary>
class NextGreaterElementIISolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int[] NextGreaterElements(int[] nums)
    {
        if ( nums == null || nums.Length == 0 ) return new int[0];
        if ( nums.Length == 1 ) return new int[] { -1 };

        Stack<int> stackValue = new Stack<int>();
        Stack<int> stackIndex = new Stack<int>();

        int n = nums.Length;
        int[] ret = new int[n];
        Array.Fill(ret, -1);
        for ( int i = 0; i < 2; i++)
        {
            for( int j = 0; j < n; j++)
            {
                var v = nums[j];
                while( (0 < stackValue.Count) && (stackValue.Peek() < v))
                {
                    stackValue.Pop();
                    ret[stackIndex.Pop()] = v;
                }

                if (i == 0)
                {
                    stackIndex.Push(j);
                    stackValue.Push(v);
                }
                else if (0 == stackValue.Count)
                    break;
            }
        }
        return ret;
    }
}
/*
public class Solution {
    public int[] NextGreaterElements(int[] nums)
    {
            //
            // 思路：
            // 1.为了让所有元素都能遍历它后面所有的元素，数组至少需要被遍历2次
            // 2.用栈来记录，目前未能找到下一个更大元素的索引位置，栈中最多存储N个元素的索引
            // 
            // 时间复杂度：入栈遍历一次，寻找结果的过程中，又遍历一次，所以是：O(n)
            // 空间复杂度：栈占用空间，O(n)
            //

        int arrLength = nums.Length;
        int[] forReturn = new int[arrLength];
        for (int j = 0; j<arrLength; j++) forReturn[j] = -1;

        Stack<int> needSearchItems = new Stack<int>();
        for (int i = 0; i< 2 * arrLength - 1; i++)
        {
            while (needSearchItems.Any() && nums[i % arrLength] > nums[needSearchItems.Peek()])
            {
                forReturn[needSearchItems.Peek()] = nums[i % arrLength];
                needSearchItems.Pop();
            }

            if (i<arrLength) needSearchItems.Push(i);
        }

        return forReturn;
    }
} 
public class Solution {
    public int[] NextGreaterElements(int[] nums) {
       var length = nums.Length;
            var result = new int[length];
            for (var i = 0; i < length; i++)
                result[i] = -1;
            var stack = new Stack<int>();
            for (var i = 0; i < 2 * length; i++)
            {
                var index = i % length;
                while (stack.Count > 0 && nums[stack.Peek()] < nums[index])
                {
                    result[stack.Peek()] = nums[index];
                    stack.Pop();
                }            

                stack.Push(index);
            }

            return result;
    }
}
public class Solution {
    public int[] NextGreaterElements(int[] nums) {
        int[] result = new int[nums.Length] ;
        for(int i=0;i<nums.Length;i++)
        {
            result[i] = -1;
            bool hasBigger = false;
            for(int j=i;j<nums.Length;j++)
            {
                if(nums[j]> nums[i])
                {
                    result[i] = nums[j];
                    hasBigger = true;
                    break;
                }
            }

            if(!hasBigger)
            {
                for (int j = 0; j < i; j++)
                {
                    if (nums[j] > nums[i])
                    {
                        result[i] = nums[j];
                        break;
                    }
                }
            }
        }
        return result;
    }
}
public class Solution {
    public int[] NextGreaterElements(int[] nums)
    {
        int[] re=new int[nums.Length];

        for (int i = 0; i < nums.Length; i++)
        {

            re[i] = FindGreaterIndex(nums, i);
        }
        return re;
    }

    int FindGreaterIndex(int[] nums,int curIndex)
    {
        int index = curIndex;
        while (true)
        {
            index++;
            if (index == nums.Length)
            {
                index = 0;
            }

            if (index == curIndex)
            {
                return -1;
            }
            else
            {
                if (nums[index] > nums[curIndex])
                {
                    return nums[index];
                }
            }
        }
    }
}
public class Solution {
    public int[] NextGreaterElements(int[] nums) {
        int[] result = new int[nums.Length];
        for (int i = 0; i < nums.Length; i++)
        {
            result[i] = -1;
            for (int j = 1; j < nums.Length; j++)
            {
                var index = (i + j) % nums.Length;
                if (nums[index] > nums[i])
                {
                    result[i] = nums[index];
                    break;
                }
            }
        }

        return result;
    }
}
*/
