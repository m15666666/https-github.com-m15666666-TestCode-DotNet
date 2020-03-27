using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个排序数组，你需要在 原地 删除重复出现的元素，使得每个元素只出现一次，返回移除后数组的新长度。

不要使用额外的数组空间，你必须在 原地 修改输入数组 并在使用 O(1) 额外空间的条件下完成。
 

示例 1:

给定数组 nums = [1,1,2], 

函数应该返回新的长度 2, 并且原数组 nums 的前两个元素被修改为 1, 2。 

你不需要考虑数组中超出新长度后面的元素。
示例 2:

给定 nums = [0,0,1,1,1,2,2,3,3,4],

函数应该返回新的长度 5, 并且原数组 nums 的前五个元素被修改为 0, 1, 2, 3, 4。

你不需要考虑数组中超出新长度后面的元素。
 

说明:

为什么返回数值是整数，但输出的答案是数组呢?

请注意，输入数组是以「引用」方式传递的，这意味着在函数里修改输入数组对于调用者是可见的。

你可以想象内部操作如下:

// nums 是以“引用”方式传递的。也就是说，不对实参做任何拷贝
int len = removeDuplicates(nums);

// 在函数里修改输入数组对于调用者是可见的。
// 根据你的函数返回的长度, 它会打印出数组中该长度范围内的所有元素。
for (int i = 0; i < len; i++) {
    print(nums[i]);
}
*/
/// <summary>
/// https://leetcode-cn.com/problems/remove-duplicates-from-sorted-array/
/// 26. 删除排序数组中的重复项
/// 
/// </summary>
class RemoveDuplicatesFromSortedArraySolution
{
    public void Test()
    {
        int[] nums = new int[] {1,2,2};
        var ret = RemoveDuplicates(nums);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    public int RemoveDuplicates(int[] nums)
    {
        if (nums == null) return 0;
        if (nums.Length < 2) return nums.Length;

        int lastV = nums[0];
        int len = nums.Length;
        int v;
        const int InvalidIndex = -1;
        int removeCount = 0;
        for( int i = 1, slotIndex = InvalidIndex; i < len; i++)
        {
            v = nums[i];
            if( lastV == v)
            {
                removeCount++;
                if (slotIndex == InvalidIndex) slotIndex = i;
                continue;
            }
            if( slotIndex != InvalidIndex ) nums[slotIndex++] = v;
            lastV = v;
        }

        return len - removeCount;
    }
}
/*

删除排序数组中的重复项
力扣 (LeetCode)
发布于 2 年前
117.5k
方法：双指针法
算法

数组完成排序后，我们可以放置两个指针 ii 和 jj，其中 ii 是慢指针，而 jj 是快指针。只要 nums[i] = nums[j]nums[i]=nums[j]，我们就增加 jj 以跳过重复项。

当我们遇到 nums[j] \neq nums[i]nums[j] 

​	
 =nums[i] 时，跳过重复项的运行已经结束，因此我们必须把它（nums[j]nums[j]）的值复制到 nums[i + 1]nums[i+1]。然后递增 ii，接着我们将再次重复相同的过程，直到 jj 到达数组的末尾为止。

public int removeDuplicates(int[] nums) {
    if (nums.length == 0) return 0;
    int i = 0;
    for (int j = 1; j < nums.length; j++) {
        if (nums[j] != nums[i]) {
            i++;
            nums[i] = nums[j];
        }
    }
    return i + 1;
}
复杂度分析

时间复杂度：O(n)O(n)，假设数组的长度是 nn，那么 ii 和 jj 分别最多遍历 nn 步。

空间复杂度：O(1)O(1)。

public class Solution {
    public int RemoveDuplicates(int[] nums) {
            int i = 0, j = 1;
            if(nums.Length==1)
            {
                return 1;
            }
            else if(nums.Length==0)
            {
                return 0;
            }
            else
            {
                while (j < nums.Length&&nums[i] != nums[j])
                {
                    i++;
                    j++;
                }
                while (j < nums.Length)
                {
                    if (nums[i] == nums[j])
                    {
                        j++;
                        continue;
                    }
                    nums[i + 1] = nums[j];
                    i++;
                }
                return i+1;
            }
    }
}

public class Solution {
    public int RemoveDuplicates(int[] nums) {
         if (nums == null || nums.Length == 0)
                return 0;
            int p = 0;
            for (int q = 1; q < nums.Length; q++)
            {
                if (nums[p] != nums[q])
                    nums[++p] = nums[q];
            }

            return p + 1;
    }
}

public class Solution {
    public int RemoveDuplicates (int[] nums) {
        if (nums.Length < 2) return nums.Length;
        var tag = 0;
        var i = 0;
        do {
            i++;
            if (nums[i] == nums[i - 1]) continue;
            tag++;
            nums[tag] = nums[i];
        } while (i < nums.Length - 1);

        return tag + 1;
    }
}

public class Solution {
    public int RemoveDuplicates(int[] nums) {
            if (nums.Length == 0) return 0;
            int index = 0;
            for (int i = 1; i < nums.Length; i++)
            {
                if (nums[index] != nums[i])
                {
                    nums[++index] = nums[i];
                }
            }
            index += 1;
            Array.Resize(ref nums,index);
            return index;
    }
}

public class Solution {
    public int RemoveDuplicates(int[] nums) 
    {
        if (nums.Length <1) return(0);
        int n = 1;

        for (int i=1; i < nums.Length; i ++)
        {
            if (nums[i] != nums[i-1])
            {
                nums[n]=nums[i];
                n++;
            }
        }
        return(n);
    }
} 
*/
