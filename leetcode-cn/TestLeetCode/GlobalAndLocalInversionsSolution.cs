using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
数组 A 是 [0, 1, ..., N - 1] 的一种排列，N 是数组 A 的长度。全局倒置指的是 i,j 满足 0 <= i < j < N 并且 A[i] > A[j] ，局部倒置指的是 i 满足 0 <= i < N 并且 A[i] > A[i+1] 。

当数组 A 中全局倒置的数量等于局部倒置的数量时，返回 true 。

 

示例 1:

输入: A = [1,0,2]
输出: true
解释: 有 1 个全局倒置，和 1 个局部倒置。
示例 2:

输入: A = [1,2,0]
输出: false
解释: 有 2 个全局倒置，和 1 个局部倒置。
注意:

A 是 [0, 1, ..., A.length - 1] 的一种排列
A 的长度在 [1, 5000]之间
这个问题的时间限制已经减少了。
*/
/// <summary>
/// https://leetcode-cn.com/problems/global-and-local-inversions/
/// 775. 全局倒置与局部倒置
/// https://blog.csdn.net/kongqingxin12/article/details/84748711
/// </summary>
class GlobalAndLocalInversionsSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public bool IsIdealPermutation(int[] A)
    {
        int localCount = 0;
        int globalCount = 0;

        var upper = A.Length;
        for ( int i = 1; i < upper; i++)
        {
            var v = A[i];
            if (v < A[i - 1])
            {
                localCount++;
                globalCount++;

                // [2 1 0] 这种情况
                if (v < i) globalCount += (i - v - 1);
            }
            else if(v < i) globalCount += (i - v);// [1 2 0] 这种情况
        }

        return globalCount == localCount;
    }
}
/*
class Solution {
public:
	bool isIdealPermutation(vector<int>& A) 
	{
		for (int i = 0; i < A.size(); i++)
		{
			if (abs(A[i] - i) > 1)
				return false;
		}
		return true;
	}
} 
*/
