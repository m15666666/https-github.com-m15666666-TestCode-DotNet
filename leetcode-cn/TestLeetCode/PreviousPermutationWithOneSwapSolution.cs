using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给你一个正整数的数组 A（其中的元素不一定完全不同），请你返回可在 一次交换（交换两数字 A[i] 和 A[j] 的位置）后得到的、按字典序排列小于 A 的最大可能排列。

如果无法这么操作，就请返回原数组。

 

示例 1：

输入：[3,2,1]
输出：[3,1,2]
解释：
交换 2 和 1
 

示例 2：

输入：[1,1,5]
输出：[1,1,5]
解释： 
这已经是最小排列
 

示例 3：

输入：[1,9,4,6,7]
输出：[1,7,4,6,9]
解释：
交换 9 和 7
 

示例 4：

输入：[3,1,1,3]
输出：[1,3,1,3]
解释：
交换 1 和 3
 

提示：

1 <= A.length <= 10000
1 <= A[i] <= 10000
*/
/// <summary>
/// https://leetcode-cn.com/problems/previous-permutation-with-one-swap/
/// 1053. 交换一次的先前排列
/// 
/// </summary>
class PreviousPermutationWithOneSwapSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int[] PrevPermOpt1(int[] A)
    {
        int len = A.Length;
        int max = -1;
        int maxIndex = -1;
        for (int i = len - 2; -1 < i; i--)
        {
            var v = A[i];
            if (A[i + 1] < v)
            {
                for (int j = i + 1; j < len; j++)
                {
                    if (A[j] < v)
                    {
                        if (max < A[j])
                        {
                            max = A[j];
                            maxIndex = j;
                        }
                    }
                }
                if (-1 < maxIndex)
                {
                    (A[i], A[maxIndex]) = (max, A[i]);
                    return A;
                }
            }
        }
        return A;
    }
}
/*
【含详细分析】 思路简单题目有趣
张佳晨
1.7k 阅读
解题思路：
这道题目的关键是 按字典序排列小于 A 的最大可能排列， 那么有

对当前序列进行逆序查找，找到第一个降序的位置 i，使得 A[i]>A[i+1]A[i]>A[i+1]

由于 A[i]>A[i+1]A[i]>A[i+1]，必能构造比当前字典序小的序列
由于逆序查找，交换 A[i] 为最优解
寻找在 A[i] 最左边且小于 A[i] 的最大的数字 A[j]

由于 A[j] < A[]i]A[j]<A[]i], 交换 A[i] 与 A[j] 后的序列字典序一定小于当前字典序
由于 A[j] 是满足关系的最大的最左的，因此一定是满足小于关系的交换后字典序最大的
这里的两条，最大和最左边缺一不可，可以结合样例

[3, 1, 1, 3] => [1, 3, 1, 3]

[3, 1, 2, 3] => [2, 1, 3, 3]

[4, 1, 2, 3] => [3, 1, 2, 4]

理解

解题方案：【字典序】 ( 1ms)
执行用时 : 1 ms，在 Previous Permutation With One Swap 的 Java 提交中击败了 90.41% 的用户。

内存消耗 : 50.9 MB，在 Previous Permutation With One Swap 的 Java 提交中击败了 100.00% 的用户。

class Solution {
    public int[] prevPermOpt1(int[] A) {
        int len = A.length;
        int curMax = -1;
        int index = -1;
        boolean hasResult = false;
        for (int i = len - 2; i >= 0; i--) {
            if (A[i+1] < A[i]) {                    // 此处逆序，需要移动A[i]
                for (int j = i + 1; j < len; j++) { // 寻找与 A[i] 交换的位置
                   if (A[i] > A[j]) {               // 必须满足 A[i] > A[j]，否则不能满足交换后的字典序小于原始字典序
                        hasResult = true;
                        if (A[j] > curMax) {        
                            curMax = A[j];
                            index = j;
                        }
                   } 
                }
                if (hasResult) {
                    int tmp = A[i];
                    A[i] = A[index];
                    A[index] = tmp;
                    return A;
                }
            }
        }
        return A;
    }
}
复杂度分析:
时间：O(N)O(N)

空间：O(1)O(1) 
*/
