using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个非重叠轴对齐矩形的列表 rects，写一个函数 pick 随机均匀地选取矩形覆盖的空间中的整数点。

提示：

整数点是具有整数坐标的点。
矩形周边上的点包含在矩形覆盖的空间中。
第 i 个矩形 rects [i] = [x1，y1，x2，y2]，其中 [x1，y1] 是左下角的整数坐标，[x2，y2] 是右上角的整数坐标。
每个矩形的长度和宽度不超过 2000。
1 <= rects.length <= 100
pick 以整数坐标数组 [p_x, p_y] 的形式返回一个点。
pick 最多被调用10000次。
 

示例 1：

输入: 
["Solution","pick","pick","pick"]
[[[[1,1,5,5]]],[],[],[]]
输出: 
[null,[4,1],[4,1],[3,3]]
示例 2：

输入: 
["Solution","pick","pick","pick","pick","pick"]
[[[[-2,-2,-1,-1],[1,0,3,0]]],[],[],[],[],[]]
输出: 
[null,[-1,-2],[2,0],[-2,-1],[3,0],[-2,-2]]
 

输入语法的说明：

输入是两个列表：调用的子例程及其参数。Solution 的构造函数有一个参数，即矩形数组 rects。pick 没有参数。参数总是用列表包装的，即使没有也是如此。 
*/
/// <summary>
/// https://leetcode-cn.com/problems/random-point-in-non-overlapping-rectangles/
/// 497. 非重叠矩形中的随机点
/// https://blog.csdn.net/zrh_CSDN/article/details/85016742
/// https://www.jianshu.com/p/aea1016435a2
/// </summary>
class RandomPointInNonoverlappingRectanglesSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public RandomPointInNonoverlappingRectanglesSolution(int[][] rects)
    {
        _rects = rects;
        _areas = new int[rects.Length];

        int sumArea = 0;
        int index = 0;
        foreach( var rect in rects)
        {
            int area = (rect[2] - rect[0] + 1) * (rect[3] - rect[1] + 1);
            sumArea += area;
            _areas[index++] = sumArea;
        }
    }

    private int[][] _rects = null;
    private int[] _areas;
    private Random _random = new Random();
    private Random _random2 = new Random();
    public int[] Pick()
    {
        var randomSumArea = _random.Next(_areas[_areas.Length - 1]+1);

        int index = 0;
        for( int i = 0; i < _areas.Length; i++)
        {
            if(randomSumArea <= _areas[i])
            {
                index = i;
                break;
            }
        }

        var rect = _rects[index];
        var x = rect[0] + _random2.Next(rect[2] - rect[0]+1);
        var y = rect[1] + _random2.Next(rect[3] - rect[1]+1);
        return new int[]{ x,y};
    }
}