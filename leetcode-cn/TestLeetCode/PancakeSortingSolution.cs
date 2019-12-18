using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定数组 A，我们可以对其进行煎饼翻转：我们选择一些正整数 k <= A.length，然后反转 A 的前 k 个元素的顺序。我们要执行零次或多次煎饼翻转（按顺序一次接一次地进行）以完成对数组 A 的排序。

返回能使 A 排序的煎饼翻转操作所对应的 k 值序列。任何将数组排序且翻转次数在 10 * A.length 范围内的有效答案都将被判断为正确。

示例 1：

输入：[3,2,4,1]
输出：[4,2,4,3]
解释：
我们执行 4 次煎饼翻转，k 值分别为 4，2，4，和 3。
初始状态 A = [3, 2, 4, 1]
第一次翻转后 (k=4): A = [1, 4, 2, 3]
第二次翻转后 (k=2): A = [4, 1, 2, 3]
第三次翻转后 (k=4): A = [3, 2, 1, 4]
第四次翻转后 (k=3): A = [1, 2, 3, 4]，此时已完成排序。 
示例 2：

输入：[1,2,3]
输出：[]
解释：
输入已经排序，因此不需要翻转任何内容。
请注意，其他可能的答案，如[3，3]，也将被接受。
 

提示：

1 <= A.length <= 100
A[i] 是 [1, 2, ..., A.length] 的排列
*/
/// <summary>
/// https://leetcode-cn.com/problems/pancake-sorting/
/// 969. 煎饼排序
/// 
/// </summary>
class PancakeSortingSolution
{
    public void Test()
    {
        int[] nums = new int[] {3, 2, 4,1};
        var ret = PancakeSort(nums);
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public IList<int> PancakeSort(int[] A)
    {
        int len = A.Length;

        var indexes = Enumerable.Range(1, len).ToArray();
        Comparison<int> comparison = (i1, i2) => A[i2-1].CompareTo(A[i1-1]); // 按照数值倒序排列
        Array.Sort( indexes, comparison);

        List<int> ret = new List<int>();
        foreach (int i in indexes)
        {
            int index = i;
            foreach (int windows in ret)
                if (index <= windows) index = windows + 1 - index;

            ret.Add(index); ;
            ret.Add(len--);
        }

        return ret;
    }
}
/*
方法：从大到小排序
思路

我们可以将最大的元素（在位置 i，下标从 1 开始）进行煎饼翻转 i 操作将它移动到序列的最前面，然后再使用煎饼翻转 A.length 操作将它移动到序列的最后面。 此时，最大的元素已经移动到正确的位置上了，所以之后我们就不需要再使用 k 值大于等于 A.length 的煎饼翻转操作了。

我们可以重复这个过程直到序列被排好序为止。 每一步，我们只需要花费两次煎饼翻转操作。

算法

我们从数组 A 中的最大值向最小值依次进行枚举，每一次将枚举的元素放到正确的位置上。

每一步，对于在位置 i 的（从大到小枚举的）元素，我们会使用思路中提到的煎饼翻转组合操作将它移动到正确的位置上。 值得注意的是，执行一次煎饼翻转操作 f，会将位置在 i, i <= f 的元素翻转到位置 f+1 - i 上。

JavaPython
class Solution {
    public List<Integer> pancakeSort(int[] A) {
        List<Integer> ans = new ArrayList();
        int N = A.length;

        Integer[] B = new Integer[N];
        for (int i = 0; i < N; ++i)
            B[i] = i+1;
        Arrays.sort(B, (i, j) -> A[j-1] - A[i-1]);

        for (int i: B) {
            for (int f: ans)
                if (i <= f)
                    i = f+1 - i;
            ans.add(i);
            ans.add(N--);
        }

        return ans;
    }
}
复杂度分析

时间复杂度：O(N^2)O(N 
2
 )，其中 NN 是数组 A 的长度。

空间复杂度：O(N)O(N)。


public class Solution {
    public IList<int> PancakeSort(int[] A) {
        List<int> ans = new List<int>();
        
        for(int i = A.Length;i>0;i--){
            int j = 0;
            while(A[j] != i)
                j++;
            reverse(A,j+1);
            ans.Add(j+1);
            reverse(A,i);
            ans.Add(i);
        }
        return ans;
    }
    
    public void reverse(int[] A, int k){
        int l = 0;
        int r = k-1;
        
        while(l <r){
            int temp = A[l];
            A[l] = A[r];
            A[r] = temp;
            l++;
            r--;
        }
    }
} 
*/
