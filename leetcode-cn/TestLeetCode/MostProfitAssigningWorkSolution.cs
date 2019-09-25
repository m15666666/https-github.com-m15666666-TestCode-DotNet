using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
有一些工作：difficulty[i] 表示第i个工作的难度，profit[i]表示第i个工作的收益。

现在我们有一些工人。worker[i]是第i个工人的能力，即该工人只能完成难度小于等于worker[i]的工作。

每一个工人都最多只能安排一个工作，但是一个工作可以完成多次。

举个例子，如果3个工人都尝试完成一份报酬为1的同样工作，那么总收益为 $3。如果一个工人不能完成任何工作，他的收益为 $0 。

我们能得到的最大收益是多少？

示例：

输入: difficulty = [2,4,6,8,10], profit = [10,20,30,40,50], worker = [4,5,6,7]
输出: 100 
解释: 工人被分配的工作难度是 [4,4,6,6] ，分别获得 [20,20,30,30] 的收益。
提示:

1 <= difficulty.length = profit.length <= 10000
1 <= worker.length <= 10000
difficulty[i], profit[i], worker[i]  的范围是 [1, 10^5]
*/
/// <summary>
/// https://leetcode-cn.com/problems/most-profit-assigning-work/
/// 826. 安排工作以达到最大收益
/// 
/// </summary>
class MostProfitAssigningWorkSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int MaxProfitAssignment(int[] difficulty, int[] profit, int[] worker)
    {
        Array.Sort(difficulty, profit);
        Array.Sort(worker);

        int len = difficulty.Length;
        int ret = 0, difficultyIndex = 0, bestProfixOfLessDifficulty = 0;
        foreach (int skill in worker)
        {
            while (difficultyIndex < len && difficulty[difficultyIndex] <= skill)
            {
                var p = profit[difficultyIndex++];
                if (bestProfixOfLessDifficulty < p) bestProfixOfLessDifficulty = p;
            }

            ret += bestProfixOfLessDifficulty;
        }

        return ret;
    }
}