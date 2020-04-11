using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给你一个未排序的整数数组，请你找出其中没有出现的最小的正整数。

示例 1:

输入: [1,2,0]
输出: 3
示例 2:

输入: [3,4,-1,1]
输出: 2
示例 3:

输入: [7,8,9,11,12]
输出: 1

提示：

你的算法的时间复杂度应为O(n)，并且只能使用常数级别的额外空间。
*/
/// <summary>
/// https://leetcode-cn.com/problems/first-missing-positive/
/// 41. 缺失的第一个正数
/// 
/// 
/// </summary>
class FirstMissingPositiveSolution
{
    public void Test()
    {
        int[] nums = new int[] {3,4,-1,1};
        var ret = FirstMissingPositive(nums);
    }


    public int FirstMissingPositive(int[] nums)
    {
        int n = nums.Length;

        bool containOne = false;
        for (int i = 0; i < n; i++)
            if (nums[i] == 1)
            {
                containOne = true;
                break;
            }

        if (!containOne) return 1;

        if (n == 1) return 2;

        for (int i = 0; i < n; i++)
            if ((nums[i] < 1) || (n < nums[i])) nums[i] = 1;

        for (int i = 0; i < n; i++)
        {
            int number = nums[i];
            if (number < 0) number = -number;
            nums[number - 1] = -Math.Abs(nums[number - 1]);
        }

        for (int i = 0; i < n; i++)
            if (0 < nums[i]) return i + 1;

        return n + 1;
    }
}
/*

缺失的第一个正数
力扣 (LeetCode)
发布于 1 年前
33.0k
方法 1：索引作为哈希表。
数据预处理

首先我们可以不考虑负数和零，因为不需要考虑。同样可以不考虑大于 n 的数字，
因为首次缺失的正数一定小于或等于 n + 1 。
缺失的正数为 n + 1 的情况会单独考虑。

41_max_possible_first.png

为了不考虑这些数，又要保证时间复杂度为 \mathcal{O}(N)O(N) ，因此
不能将这些元素弹出。我们可以将这些数用 1 替换。

41_replace.png

为了确保缺失的第一个正数不是 1，先要在这步操作前确定 1 是否存在。

如何实现就地算法

现在我们有一个只包含正数的数组，范围为 1 到 n，
现在的问题是在 \mathcal{O}(N)O(N) 的时间和常数空间内找出首次缺失的正数。

如果可以使用哈希表，且哈希表的映射是 正数 -> 是否存在 的话，这其实很简单。

41_missing.png

"脏工作环境" 的解决方法是将一个字符串 hash_str 分配 n 个 0，并且用类似于哈希表的方法，如果在数组中出现数字 i 则将字符串中 hash_str[i] 修改为 1 。

41_string.png

我们不使用这种方法，但是借鉴这种 使用索引作为哈希键值 的想法。

最终的想法是 使用索引作为哈希键 以及 元素的符号作为哈希值 来实现是否存在的检测。

例如，nums[2] 元素的负号意味着数字 2 出现在 nums 中。nums[3]元素的正号表示 3 没有出现在 nums 中。

为了完成此操作，我们遍历一遍数组（该操作在数据预处理使得数组中只有正数的操作后），检查每个元素值 elem 以及将nums[elem] 元素的符号变为符号来表示数字 elem 出现在 nums 中。注意，当数字出现多次时需要保证符号只会变化 1 次。

41_true_solution.png

算法

现在可以开始写算法了。

检查 1 是否存在于数组中。如果没有，则已经完成，1 即为答案。
如果 nums = [1]，答案即为 2 。
将负数，零，和大于 n 的数替换为 1 。
遍历数组。当读到数字 a 时，替换第 a 个元素的符号。
注意重复元素：只能改变一次符号。由于没有下标 n ，使用下标 0 的元素保存是否存在数字 n。
再次遍历数组。返回第一个正数元素的下标。
如果 nums[0] > 0，则返回 n 。
如果之前的步骤中没有发现 nums 中有正数元素，则返回n + 1。
代码

class Solution {
  public int firstMissingPositive(int[] nums) {
    int n = nums.length;

    // 基本情况
    int contains = 0;
    for (int i = 0; i < n; i++)
      if (nums[i] == 1) {
        contains++;
        break;
      }

    if (contains == 0)
      return 1;

    // nums = [1]
    if (n == 1)
      return 2;

    // 用 1 替换负数，0，
    // 和大于 n 的数
    // 在转换以后，nums 只会包含
    // 正数
    for (int i = 0; i < n; i++)
      if ((nums[i] <= 0) || (nums[i] > n))
        nums[i] = 1;

    // 使用索引和数字符号作为检查器
    // 例如，如果 nums[1] 是负数表示在数组中出现了数字 `1`
    // 如果 nums[2] 是正数 表示数字 2 没有出现
    for (int i = 0; i < n; i++) {
      int a = Math.abs(nums[i]);
      // 如果发现了一个数字 a - 改变第 a 个元素的符号
      // 注意重复元素只需操作一次
      if (a == n)
        nums[0] = - Math.abs(nums[0]);
      else
        nums[a] = - Math.abs(nums[a]);
    }

    // 现在第一个正数的下标
    // 就是第一个缺失的数
    for (int i = 1; i < n; i++) {
      if (nums[i] > 0)
        return i;
    }

    if (nums[0] > 0)
      return n;

    return n + 1;
  }
}
复杂性分析

时间复杂度： O(N)O(N) 由于所有的操作一共只会遍历长度为 N 的数组 4 次。
空间复杂度： O(1)O(1) 由于只使用了常数的空间。
下一篇：将数组视为哈希表，哈希函数为：f(nums[i]) = nums[i] - 1

public class Solution {
    public int FirstMissingPositive(int[] nums)
        {
            int length = nums.Length;
            int result = length + 1;
            bool[] temp = new bool[length];
            for (int i = 0; i < length; i++)
            {
                if (nums[i] > 0 && nums[i] <= length)
                {
                    temp[nums[i]-1] = true;
                }
            }
            for (int i = 0; i < length; i++)
            {
                if (!temp[i]) 
                {
                    result = i+1;
                    break;
                }
            }
            return result;
        }
}

public class Solution {
    public int FirstMissingPositive(int[] nums) {
        bool hasOne=false;
        foreach(int num in nums)
        {
            if(num==1)
                hasOne=true;
        }

        if(!hasOne)
        {
            return 1;
        }
        int length=nums.Length;
        bool hasN=false;
        if(length==1)
            return 2;
        for(int i=0;i<length;i++)
        {
            if(nums[i]<=0||nums[i]>length)
            {
                nums[i]=1;
            }
            if(nums[i]==length)
            {
                hasN=true;
                nums[i]=1;
            }
        }

        for(int i=0;i<length;i++)
        {
            int index=nums[i];
            if(index<0)
                index=-index;
            if(nums[index]>0)
            {
                nums[index]=-nums[index];
            }
        }

        for(int i=2;i<length;i++)
        {
            if(nums[i]>0)
                return i;
        }
        
        if(!hasN)
            return length;

        return length+1;
    }
}
*/
