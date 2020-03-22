using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// 假设你正在爬楼梯。需要 n 阶你才能到达楼顶。每次你可以爬 1 或 2 个台阶。你有多少种不同的方法可以爬到楼顶呢？
/// </summary>
class ClimbStairsSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    
    public int ClimbStairs(int n)
    {
        Dictionary<int, int> n2PathCount = new Dictionary<int, int>();
        return Climb(n);

        int Climb(int number)
        {
            if (number <= 1) return 1;
            if (number == 2) return 2;

            if (n2PathCount.ContainsKey(number)) return n2PathCount[number];

            var ret = Climb(number - 1) + Climb(number - 2);

            if (!n2PathCount.ContainsKey(number)) n2PathCount[number] = ret;

            return ret;
        }
    }
}