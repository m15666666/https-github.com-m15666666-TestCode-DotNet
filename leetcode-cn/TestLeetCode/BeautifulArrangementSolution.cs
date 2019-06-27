using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
假设有从 1 到 N 的 N 个整数，如果从这 N 个数字中成功构造出一个数组，使得数组的第 i 位 (1 <= i <= N) 满足如下两个条件中的一个，我们就称这个数组为一个优美的排列。条件：

第 i 位的数字能被 i 整除
i 能被第 i 位上的数字整除
现在给定一个整数 N，请问可以构造多少个优美的排列？

示例1:

输入: 2
输出: 2
解释: 

第 1 个优美的排列是 [1, 2]:
  第 1 个位置（i=1）上的数字是1，1能被 i（i=1）整除
  第 2 个位置（i=2）上的数字是2，2能被 i（i=2）整除

第 2 个优美的排列是 [2, 1]:
  第 1 个位置（i=1）上的数字是2，2能被 i（i=1）整除
  第 2 个位置（i=2）上的数字是1，i（i=2）能被 1 整除
说明:

N 是一个正整数，并且不会超过15。
*/
/// <summary>
/// https://leetcode-cn.com/problems/beautiful-arrangement/
/// 526. 优美的排列
/// http://www.manongjc.com/article/34142.html
/// https://blog.csdn.net/laputafallen/article/details/79982441
/// </summary>
class BeautifulArrangementSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int CountArrangement(int N)
    {
        if (N < 3) return N;

        int[] nums = new int[N + 1];
        for (int i = 0; i <= N; i++) nums[i] = i;

        helper(nums, N);

        return count;
    }

    private int count = 0;
    private void swap(int[] nums, int i, int j)
    {
        int tmp = nums[i];
        nums[i] = nums[j];
        nums[j] = tmp;
    }
    private void helper(int[] nums, int start)
    {
        if (start == 0)
        {
            count++;
            return;
        }
        for (int i = start; i > 0; i--)
        {
            swap(nums, start, i);
            if (nums[start] % start == 0 || start % nums[start] == 0) helper(nums, start - 1);
            swap(nums, i, start);
        }
    }
}