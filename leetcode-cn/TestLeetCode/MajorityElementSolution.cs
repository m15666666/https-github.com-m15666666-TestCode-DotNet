using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个大小为 n 的数组，找到其中的多数元素。多数元素是指在数组中出现次数大于 ⌊ n/2 ⌋ 的元素。

你可以假设数组是非空的，并且给定的数组总是存在多数元素。

 

示例 1:

输入: [3,2,3]
输出: 3
示例 2:

输入: [2,2,1,1,1,2,2]
输出: 2

*/
/// <summary>
/// https://leetcode-cn.com/problems/majority-element/
/// 169. 多数元素
/// 
/// 
/// </summary>
class MajorityElementSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    public int MajorityElement(int[] nums)
    {
        int count = 0;
        int ret = 0;

        foreach (int num in nums) {
            if (count == 0) ret = num;
            count += (num == ret) ? 1 : -1;
        }

        return ret;
    }
}
/*

value 的实际意义即为：到当前的这一步遍历为止，众数出现的次数比非众数多出了多少次。我们将 value 的值也写在下方：


nums:      [7, 7, 5, 7, 5, 1 | 5, 7 | 5, 5, 7, 7 | 7, 7, 7, 7]
value:      1  2  1  2  1  0  -1  0  -1 -2 -1  0   1  2  3  4
有没有发现什么？我们将 count 和 value 放在一起：


nums:      [7, 7, 5, 7, 5, 1 | 5, 7 | 5, 5, 7, 7 | 7, 7, 7, 7]
count:      1  2  1  2  1  0   1  0   1  2  1  0   1  2  3  4
value:      1  2  1  2  1  0  -1  0  -1 -2 -1  0   1  2  3  4
发现在每一步遍历中，count 和 value 要么相等，要么互为相反数！并且在候选众数 candidate 就是 maj 时，它们相等，candidate 是其它的数时，它们互为相反数！

为什么会有这么奇妙的性质呢？这并不难证明：我们将候选众数 candidate 保持不变的连续的遍历称为「一段」。在同一段中，count 的值是根据 candidate == x 的判断进行加减的。那么如果 candidate 恰好为 maj，那么在这一段中，count 和 value 的变化是同步的；如果 candidate 不为 maj，那么在这一段中 count 和 value 的变化是相反的。因此就有了这样一个奇妙的性质。

这样以来，由于：

我们证明了 count 的值一直为非负，在最后一步遍历结束后也是如此；

由于 value 的值与真正的众数 maj 绑定，并且它表示「众数出现的次数比非众数多出了多少次」，那么在最后一步遍历结束后，value 的值为正数；

在最后一步遍历结束后，count 非负，value 为正数，所以它们不可能互为相反数，只可能相等，即 count == value。因此在最后「一段」中，count 的 value 的变化是同步的，也就是说，candidate 中存储的候选众数就是真正的众数 maj。

JavaPythonC++

class Solution {
    public int majorityElement(int[] nums) {
        int count = 0;
        Integer candidate = null;

        for (int num : nums) {
            if (count == 0) {
                candidate = num;
            }
            count += (num == candidate) ? 1 : -1;
        }

        return candidate;
    }
}
复杂度分析

时间复杂度：O(n)O(n)。Boyer-Moore 算法只对数组进行了一次遍历。

空间复杂度：O(1)O(1)。Boyer-Moore 算法只需要常数级别的额外空间。

下一篇：Java-3种方法(计数法/排序法/摩尔投票法)


作者：LeetCode-Solution
链接：https://leetcode-cn.com/problems/majority-element/solution/duo-shu-yuan-su-by-leetcode-solution/
来源：力扣（LeetCode）
著作权归作者所有。商业转载请联系作者获得授权，非商业转载请注明出处。

public class Solution {
    public int MajorityElement(int[] nums) {
        int count = 0;
        int candidate = 0;
        for(int i = 0;i < nums.Length;i++)
        {
            if(count == 0)
                candidate = nums[i];
            count += candidate == nums[i] ? 1 : -1;
        }
        return candidate;
    }
}

public class Solution {
    public int MajorityElement(int[] nums) {
int last=nums[0];
int count = 1;
for     (int i =1; i <nums.Length;i++){
    if      (nums[i]==last){
        count++;
    }else{
count--;
    }
    if  (count==0){count=1;last=nums[i];}
}
return last;
    }
}

public class Solution
{
    public int MajorityElement(int[] nums)
    {
        int candidate = 0;
        int count = 0;
        for (int i = 0; i < nums.Length; i++)
        {
            if (count == 0)
                candidate = nums[i];
            
            if (nums[i] != candidate)
                count--;
            else
                count++;                    
        }
        return candidate;
    }
}

public class Solution {
    public int MajorityElement(int[] nums) {
        if (nums == null || nums.Length == 0) return 0;
        var candidate = 0;
        var num = 0;
        foreach(var item in nums) {
            if (candidate == 0) num = item;
            if (item == num) candidate++;
            else candidate--;
        }
        return num;
    }
}





*/
