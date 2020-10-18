/*
给定一个包含 [0, n] 中 n 个数的数组 nums ，找出 [0, n] 这个范围内没有出现在数组中的那个数。

 

进阶：

你能否实现线性时间复杂度、仅使用额外常数空间的算法解决此问题?
 

示例 1：

输入：nums = [3,0,1]
输出：2
解释：n = 3，因为有 3 个数字，所以所有的数字都在范围 [0,3] 内。2 是丢失的数字，因为它没有出现在 nums 中。
示例 2：

输入：nums = [0,1]
输出：2
解释：n = 2，因为有 2 个数字，所以所有的数字都在范围 [0,2] 内。2 是丢失的数字，因为它没有出现在 nums 中。
示例 3：

输入：nums = [9,6,4,2,3,5,7,0,1]
输出：8
解释：n = 9，因为有 9 个数字，所以所有的数字都在范围 [0,9] 内。8 是丢失的数字，因为它没有出现在 nums 中。
示例 4：

输入：nums = [0]
输出：1
解释：n = 1，因为有 1 个数字，所以所有的数字都在范围 [0,1] 内。1 是丢失的数字，因为它没有出现在 nums 中。
 

提示：

n == nums.length
1 <= n <= 104
0 <= nums[i] <= n
nums 中的所有数字都 独一无二

*/

/// <summary>
/// https://leetcode-cn.com/problems/missing-number/
/// 268. 丢失的数字
///
/// </summary>
internal class MissingNumberSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int MissingNumber(int[] nums)
    {
        int len = nums.Length;
        int ret = len;
        for (int i = 0; i < len; i++) ret ^= i ^ nums[i];
        return ret;
    }

    //public int MissingNumber(int[] nums)
    //{
    //    Array.Sort(nums);
    //    int index = 0;

    //    foreach( var v in nums)
    //    {
    //        if (index != v) return index;
    //        index++;
    //    }

    //    return index;
    //}
}

/*
缺失数字
力扣 (LeetCode)
发布于 2019-06-24
34.3k
方法一：排序
分析

如果数组是有序的，那么就很容易知道缺失的数字是哪个了。

算法

首先我们对数组进行排序，随后我们可以在常数时间内判断两种特殊情况：0 没有出现在数组的首位，以及 nn 没有出现在数组的末位。如果这两种特殊情况都不满足，那么缺失的数字一定在 0 和 nn 之间（不包括两者）。此时我们可以在线性时间内扫描这个数组，如果某一个数比它前面的那个数大了超过 1，那么这两个数之间的那个数即为缺失的数字。

class Solution {
    public int missingNumber(int[] nums) {
        Arrays.sort(nums);

        // 判断 n 是否出现在末位
        if (nums[nums.length-1] != nums.length) {
            return nums.length;
        }
        // 判断 0 是否出现在首位
        else if (nums[0] != 0) {
            return 0;
        }

        // 此时缺失的数字一定在 (0, n) 中
        for (int i = 1; i < nums.length; i++) {
            int expectedNum = nums[i-1] + 1;
            if (nums[i] != expectedNum) {
                return expectedNum;
            }
        }

        // 未缺失任何数字（保证函数有返回值）
        return -1;
    }
}
复杂度分析

时间复杂度：O(n\log n)O(nlogn)。由于排序的时间复杂度为 O(n\log n)O(nlogn)，扫描数组的时间复杂度为 O(n)O(n)，因此总的时间复杂度为 O(n\log n)O(nlogn)。
空间复杂度：O(1)O(1) 或 O(n)O(n)。空间复杂度取决于使用的排序算法，根据排序算法是否进行原地排序（即不使用额外的数组进行临时存储），空间复杂度为 O(1)O(1) 或 O(n)O(n)。
方法二：哈希表
分析

我们可以直接查询每个数是否在数组中出现过来找出缺失的数字。如果使用哈希表，那么每一次查询操作都是常数时间的。

算法

我们将数组中的所有数插入到一个集合中，这样每次查询操作的时间复杂度都是 O(1)O(1) 的。

class Solution {
    public int missingNumber(int[] nums) {
        Set<Integer> numSet = new HashSet<Integer>();
        for (int num : nums) numSet.add(num);

        int expectedNumCount = nums.length + 1;
        for (int number = 0; number < expectedNumCount; number++) {
            if (!numSet.contains(number)) {
                return number;
            }
        }
        return -1;
    }
}
复杂度分析

时间复杂度：O(n)O(n)。集合的插入操作的时间复杂度都是 O(1)O(1)，一共插入了 nn 个数，时间复杂度为 O(n)O(n)。集合的查询操作的时间复杂度同样是 O(1)O(1)，最多查询 n+1n+1 次，时间复杂度为 O(n)O(n)。因此总的时间复杂度为 O(n)O(n)。
空间复杂度：O(n)O(n)。集合中会存储 nn 个数，因此空间复杂度为 O(n)O(n)。
方法三：位运算
分析

由于异或运算（XOR）满足结合律，并且对一个数进行两次完全相同的异或运算会得到原来的数，因此我们可以通过异或运算找到缺失的数字。

算法

我们知道数组中有 nn 个数，并且缺失的数在 [0..n][0..n] 中。因此我们可以先得到 [0..n][0..n] 的异或值，再将结果对数组中的每一个数进行一次异或运算。未缺失的数在 [0..n][0..n] 和数组中各出现一次，因此异或后得到 0。而缺失的数字只在 [0..n][0..n] 中出现了一次，在数组中没有出现，因此最终的异或结果即为这个缺失的数字。

在编写代码时，由于 [0..n][0..n] 恰好是这个数组的下标加上 nn，因此可以用一次循环完成所有的异或运算，例如下面这个例子：

下标	0	1	2	3
数字	0	1	3	4
可以将结果的初始值设为 nn，再对数组中的每一个数以及它的下标进行一个异或运算，即：

\begin{aligned} \mathrm{missing} &= 4 \wedge (0 \wedge 0) \wedge (1 \wedge 1) \wedge (2 \wedge 3) \wedge (3 \wedge 4) \\ &= (4 \wedge 4) \wedge (0 \wedge 0) \wedge (1 \wedge 1) \wedge (3 \wedge 3) \wedge 2 \\ &= 0 \wedge 0 \wedge 0 \wedge 0 \wedge 2 \\ &= 2 \end{aligned}
missing
​

=4∧(0∧0)∧(1∧1)∧(2∧3)∧(3∧4)
=(4∧4)∧(0∧0)∧(1∧1)∧(3∧3)∧2
=0∧0∧0∧0∧2
=2
​

就得到了缺失的数字为 2。

class Solution {
    public int missingNumber(int[] nums) {
        int missing = nums.length;
        for (int i = 0; i < nums.length; i++) {
            missing ^= i ^ nums[i];
        }
        return missing;
    }
}
复杂度分析

时间复杂度：O(n)O(n)。这里假设异或运算的时间复杂度是常数的，总共会进行 O(n)O(n) 次异或运算，因此总的时间复杂度为 O(n)O(n)。
空间复杂度：O(1)O(1)。算法中只用到了 O(1)O(1) 的额外空间，用来存储答案。
方法四：数学
分析

我们可以用 高斯求和公式 求出 [0..n][0..n] 的和，减去数组中所有数的和，就得到了缺失的数字。高斯求和公式即

\sum_{i=0}^{n}i = \frac{n(n+1)}{2}
i=0
∑
n
​
 i=
2
n(n+1)
​

算法

我们在线性时间内可以求出数组中所有数的和，并在常数时间内求出前 n+1n+1 个自然数（包括 0）的和，将后者减去前者，就得到了缺失的数字。

class Solution {
    public int missingNumber(int[] nums) {
        int expectedSum = nums.length*(nums.length + 1)/2;
        int actualSum = 0;
        for (int num : nums) actualSum += num;
        return expectedSum - actualSum;
    }
}
复杂度分析

时间复杂度：O(n)O(n)。求出数组中所有数的和的时间复杂度为 O(n)O(n)，高斯求和公式的时间复杂度为 O(1)O(1)，因此总的时间复杂度为 O(n)O(n)。
空间复杂度：O(1)O(1)。算法中只用到了 O(1)O(1) 的额外空间，用来存储答案。

public class Solution {
    public int MissingNumber(int[] nums) {
        int n = nums.Length;
        var fullList = new List<int>();
        var check = new List<int>();
        for(int i = 0;i<=n;i++){
            fullList.Add(i);
        }
        foreach(var ele in nums){
            check.Add(ele);
        }
        
        int missing = fullList.Except(check).ElementAt(0);
        return missing;
    }
}

public class Solution {
    public int MissingNumber(int[] nums) {
        int n = nums.Length;
        var fullList = new List<int>();
        for(int i = 0;i<=n;i++){
            fullList.Add(i);
        }      
        int missing = fullList.Except(nums.ToList()).ElementAt(0);
        return missing;
    }
}

public class Solution {
    public int MissingNumber(int[] nums) {
        int n = nums.Length;
        var fullList = new List<int>();
        for(int i = 0;i<=n;i++){
            fullList.Add(i);
        }      
        int missing = fullList.Except(nums).ElementAt(0);
        return missing;
    }
}

public class Solution {
    public int MissingNumber(int[] nums) {
        return (0 + nums.Length)  * (nums.Length + 1)/ 2-nums.Sum();
    }
}



*/