using System;
using System.Collections.Generic;
using System.Text;


/*
给定一个数组 nums，编写一个函数将所有 0 移动到数组的末尾，同时保持非零元素的相对顺序。

示例:

输入: [0,1,0,3,12]
输出: [1,3,12,0,0]
说明:

必须在原数组上操作，不能拷贝额外的数组。
尽量减少操作次数。

*/
/// <summary>
/// https://leetcode-cn.com/problems/move-zeroes/
/// 283. 移动零
/// 
/// </summary>
class MoveZeroesSolution
{
    public void Test()
    {
        int[] nums = new int[] { 0, 1, 0, 3, 12 };
        MoveZeroes(nums);
    }

    public void MoveZeroes(int[] nums)
    {
        int n = nums.Length;
        int zeroIndex = 0;
        for(; zeroIndex < n; ++zeroIndex)
            if(nums[zeroIndex] == 0)
                break;

        if (zeroIndex == n) return;

        int nonZeroIndex = zeroIndex + 1;
        for(; nonZeroIndex < n; ++nonZeroIndex)
            if(nums[nonZeroIndex] != 0)
            {
                nums[zeroIndex++] = nums[nonZeroIndex];
                nums[nonZeroIndex] = 0;
            }
    }
    //public void MoveZeroes(int[] nums)
    //{
    //    int zeroIndex = -1;

    //    for (int index = 0; index < nums.Length; index++)
    //    {
    //        var v = nums[index];
    //        if (v == 0)
    //        {
    //            if (zeroIndex == -1)
    //            {
    //                zeroIndex = index;
    //            }
    //        }
    //        else
    //        {
    //            if (zeroIndex != -1)
    //            {
    //                nums[zeroIndex] = v;
    //                nums[index] = 0;
    //                zeroIndex++;
    //            }
    //        }
    //    }
    //}
}
/*
移动零
力扣 (LeetCode)
发布于 2019-06-19
66.7k
这个问题属于 “数组变换” 的一个广泛范畴。这一类是技术面试的重点。主要是因为数组是如此简单和易于使用的数据结构。遍历或表示不需要任何样板代码，而且大多数代码将看起来像伪代码本身。

问题的两个要求是：

将所有 0 移动到数组末尾。
所有非零元素必须保持其原始顺序。
这里很好地认识到这两个需求是相互排斥的，也就是说，你可以解决单独的子问题，然后将它们组合在一起以得到最终的解决方案。

方法一：空间局部优化

void moveZeroes(vector<int>& nums) {
    int n = nums.size();

    // Count the zeroes
    int numZeroes = 0;
    for (int i = 0; i < n; i++) {
        numZeroes += (nums[i] == 0);
    }

    // Make all the non-zero elements retain their original order.
    vector<int> ans;
    for (int i = 0; i < n; i++) {
        if (nums[i] != 0) {
            ans.push_back(nums[i]);
        }
    }

    // Move all zeroes to the end
    while (numZeroes--) {
        ans.push_back(0);
    }

    // Combine the result
    for (int i = 0; i < n; i++) {
        nums[i] = ans[i];
    }
}
复杂度分析

时间复杂度：O(n)O(n)。但是，操作总数是局部优化的。我们可以在更少的操作中实现相同的结果。
空间复杂度：O(n)O(n)，我们创建 “ans” 数组来存储结果。
方法二：空间最优，操作局部优化（双指针）
这种方法与上面的工作方式相同，即先满足一个需求，然后满足另一个需求。它以一种巧妙的方式做到了这一点。上述问题也可以用另一种方式描述，“将所有非 0 元素置于数组前面，保持它们的相对顺序相同”。
这是双指针的方法。由变量 “cur” 表示的快速指针负责处理新元素。如果新找到的元素不是 0，我们就在最后找到的非 0 元素之后记录它。最后找到的非 0 元素的位置由慢指针 “lastnonzerofoundat” 变量表示。当我们不断发现新的非 0 元素时，我们只是在 “lastnonzerofoundat+1” 第个索引处覆盖它们。此覆盖不会导致任何数据丢失，因为我们已经处理了其中的内容（如果它是非 0 的，则它现在已经写入了相应的索引，或者如果它是 0，则稍后将进行处理）。
在 “cur” 索引到达数组的末尾之后，我们现在知道所有非 0 元素都已按原始顺序移动到数组的开头。现在是时候满足其他要求了，“将所有 0 移动到末尾”。我们现在只需要在 “lastnonzerofoundat” 索引之后用 0 填充所有索引。

void moveZeroes(vector<int>& nums) {
    int lastNonZeroFoundAt = 0;
    // If the current element is not 0, then we need to
    // append it just in front of last non 0 element we found. 
    for (int i = 0; i < nums.size(); i++) {
        if (nums[i] != 0) {
            nums[lastNonZeroFoundAt++] = nums[i];
        }
    }
    // After we have finished processing new elements,
    // all the non-zero elements are already at beginning of array.
    // We just need to fill remaining array with 0's.
    for (int i = lastNonZeroFoundAt; i < nums.size(); i++) {
        nums[i] = 0;
    }
}
复杂度分析

时间复杂度：O(n)O(n)。但是，操作仍然是局部优化的。代码执行的总操作（数组写入）为 nn（元素总数）。
空间复杂度：O(1)O(1)，只使用常量空间。
方法三：最优解
前一种方法的操作是局部优化的。例如，所有（除最后一个）前导零的数组：[0，0，0，…，0，1]。对数组执行多少写操作？对于前面的方法，它写 0 n-1n−1 次，这是不必要的。我们本可以只写一次。怎么用？… 只需固定非 0 元素。

最优方法也是上述解决方案的一个细微扩展。一个简单的实现是，如果当前元素是非 0 的，那么它的正确位置最多可以是当前位置或者更早的位置。如果是后者，则当前位置最终将被非 0 或 0 占据，该非 0 或 0 位于大于 “cur” 索引的索引处。我们马上用 0 填充当前位置，这样不像以前的解决方案，我们不需要在下一个迭代中回到这里。

换句话说，代码将保持以下不变：

慢指针（lastnonzerofoundat）之前的所有元素都是非零的。
当前指针和慢速指针之间的所有元素都是零。
因此，当我们遇到一个非零元素时，我们需要交换当前指针和慢速指针指向的元素，然后前进两个指针。如果它是零元素，我们只前进当前指针。

void moveZeroes(vector<int>& nums) {
    for (int lastNonZeroFoundAt = 0, cur = 0; cur < nums.size(); cur++) {
        if (nums[cur] != 0) {
            swap(nums[lastNonZeroFoundAt++], nums[cur]);
        }
    }
}
复杂度分析

时间复杂度：O(n)O(n)。但是，操作是最优的。代码执行的总操作（数组写入）是非 0 元素的数量。这比上一个解决方案的复杂性（当大多数元素为 0 时）要好得多。但是，两种算法的最坏情况（当所有元素都为非 0 时）复杂性是相同的。
空间复杂度：O(1)O(1)，只使用了常量空间。

public class Solution {
    public void MoveZeroes(int[] nums) {
        var exceptZero = nums.ToList().Where(x=>x!=0);
        var zeroCount = nums.Length - exceptZero.Count();
        var result = new List<int>();
        foreach(var num in exceptZero){
           result.Add(num);
        }
        for(int i = 0;i<zeroCount;i++){
            result.Add(0);
        }
        Array.Clear(nums,0,nums.Length);
        Array.Copy(result.ToArray(),nums,exceptZero.Count());
    }
}

public class Solution 
{
    public void MoveZeroes(int[] nums) 
    {
        int j = 0;
        
        for (int i = 0; i < nums.Length; ++i)
        {
            if (nums[i] != 0)
            {
                nums[j] = nums[i];
                if (i > j) nums[i] = 0;
                ++j;
            }
        }
    }
}

public class Solution 
{
    public void MoveZeroes(int[] nums) 
    {
        int j = 0;
        
        for (int i = 0; i < nums.Length; ++i)
        {
            if (nums[i] != 0)
            {
                if (i > j) 
                {
                    nums[j] = nums[i];
                    nums[i] = 0;
                }

                ++j;
            }
        }
    }
}

public class Solution {
    public void MoveZeroes(int[] nums) {
        int j = 0;
        for ( int i = 0; i < nums.Length; i++ ) {
            if (nums[i] != 0)
                nums[j++] = nums[i];
        }
        while (j < nums.Length) {
            nums[j++] = 0;
        }
    }
}

*/