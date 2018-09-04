using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// 你是一个专业的小偷，计划偷窃沿街的房屋。每间房内都藏有一定的现金，影响你偷窃的唯一制约因素就是相邻的房屋装有相互连通的防盗系统，如果两间相邻的房屋在同一晚上被小偷闯入，系统会自动报警。
/// https://blog.csdn.net/qq_26410101/article/details/80569811
/// </summary>
class RobSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    public int Rob(int[] nums)
    {
        if (nums == null || nums.Length == 0) return 0;
        if (nums.Length == 1) return nums[0];

        int max1 = nums[0];
        int max2 = Math.Max(max1, nums[1]); ;
        for (int i = 2; i < nums.Length; i++)
        {
            int tmp = max2;
            max2 = Math.Max(max1 + nums[i], max2);
            max1 = tmp;
        }
        return max2;
    }

}