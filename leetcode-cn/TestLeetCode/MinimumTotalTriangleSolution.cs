using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// https://leetcode-cn.com/problems/triangle/
/// 120.三角形最小路径和
/// 给定一个三角形，找出自顶向下的最小路径和。每一步只能移动到下一行中相邻的结点上。
/// </summary>
class MinimumTotalTriangleSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    public int MinimumTotal(IList<IList<int>> triangle)
    {
        // 使用动态规划，从最后一行开始计算

        var rowCount = triangle.Count;
        var pathValues = triangle[rowCount - 1].ToArray();
        for( var index = rowCount - 2; 0 <= index; index--)
        {
            int columnIndex = 0;
            foreach( var v in triangle[index] )
            {
                pathValues[columnIndex] = v + Math.Min(pathValues[columnIndex], pathValues[columnIndex + 1]);
                columnIndex++;
            }
        }
        return pathValues[0];
    }
}