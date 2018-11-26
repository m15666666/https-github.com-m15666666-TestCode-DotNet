using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// https://leetcode-cn.com/problems/gas-station/
/// 134.加油站
/// 在一条环路上有 N 个加油站，其中第 i 个加油站有汽油 gas[i] 升。
/// 你有一辆油箱容量无限的的汽车，从第 i 个加油站开往第 i+1 个加油站需要消耗汽油 cost[i] 升。你从其中的一个加油站出发，开始时油箱为空。
/// 如果你可以绕环路行驶一周，则返回出发时加油站的编号，否则返回 -1。
/// 说明: 
/// 如果题目有解，该答案即为唯一答案。
/// 输入数组均为非空数组，且长度相同。
/// 输入数组中的元素均为非负数。
/// </summary>
class GasStationSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    public int CanCompleteCircuit(int[] gas, int[] cost)
    {
        if ( gas == null || cost == null || gas.Length == 0 || gas.Length != cost.Length ) return -1;

        var length = gas.Length;
        for( var startIndex = 0; startIndex < length; startIndex++ )
        {
            int currentIndex = startIndex;
            int gasSum = 0;

            while( true )
            {
                var currentCost = cost[currentIndex];
                gasSum += gas[currentIndex];

                if (gasSum < currentCost) break;

                gasSum -= currentCost;

                int nextIndex = (currentIndex + 1) % length;
                if (nextIndex == startIndex) return startIndex;

                currentIndex = nextIndex;
            }
        }
        return -1;
    }

}