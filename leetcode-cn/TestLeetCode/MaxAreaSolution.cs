using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// https://leetcode-cn.com/problems/container-with-most-water/
/// 盛最多水的容器
/// </summary>
class MaxAreaSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    public int MaxArea(int[] height)
    {
        if (height == null || height.Length < 2) return 0;

        #region 方法一：暴力法

        //int maxArea = 0;
        //for(int i = 0; i < height.Length - 1; i++)
        //{
        //    for( int j = height.Length - 1; i < j; j--)
        //    {
        //        var area = (j - i) * Math.Min(height[i], height[j]);
        //        if (maxArea < area) maxArea = area;
        //    }
        //}

        //return maxArea;

        #endregion

        #region 方法二：双指针法

        int maxArea = 0;
        int startIndex = 0;
        int endIndex = height.Length - 1;
        while( startIndex < endIndex)
        {
            var startValue = height[startIndex];
            var endValue = height[endIndex];
            var area = (endIndex - startIndex) * Math.Min(startValue, endValue);
            if (maxArea < area) maxArea = area;

            if (startValue < endValue) startIndex++;
            else endIndex--;
        }

        return maxArea;

        #endregion
    }

}