using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定圆的半径和圆心的 x、y 坐标，写一个在圆中产生均匀随机点的函数 randPoint 。

说明:

输入值和输出值都将是浮点数。
圆的半径和圆心的 x、y 坐标将作为参数传递给类的构造函数。
圆周上的点也认为是在圆中。
randPoint 返回一个包含随机点的x坐标和y坐标的大小为2的数组。
示例 1：

输入: 
["Solution","randPoint","randPoint","randPoint"]
[[1,0,0],[],[],[]]
输出: [null,[-0.72939,-0.65505],[-0.78502,-0.28626],[-0.83119,-0.19803]]
示例 2：

输入: 
["Solution","randPoint","randPoint","randPoint"]
[[10,5,-7.5],[],[],[]]
输出: [null,[11.52438,-8.33273],[2.46992,-16.21705],[11.13430,-12.42337]]
输入语法说明：

输入是两个列表：调用成员函数名和调用的参数。Solution 的构造函数有三个参数，圆的半径、圆心的 x 坐标、圆心的 y 坐标。randPoint 没有参数。输入参数是一个列表，即使参数为空，也会输入一个 [] 空列表。 
*/
/// <summary>
/// https://leetcode-cn.com/problems/generate-random-point-in-a-circle/
/// 478. 在圆内随机生成点
/// https://blog.csdn.net/qq_23523409/article/details/84953194
/// https://www.jianshu.com/p/c8ab26becbb4
/// https://www.cnblogs.com/yunlambert/p/10161339.html
/// </summary>
class GenerateRandomPointInACircleSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public GenerateRandomPointInACircleSolution(double radius, double x_center, double y_center)
    {
        _radius = radius;
        _x = x_center;
        _y = y_center;
        _d = 2 * _radius;
        _r2 = _radius * _radius;
    }

    private Random _random = new Random();
    private double _radius;
    private double _x;
    private double _y;
    private double _r2;
    private double _d;
    public double[] RandPoint()
    {
        double dx = 0, dy = 0;
        do
        {
            dx = _random.NextDouble() * _d - _radius;
            dy = _random.NextDouble() * _d - _radius;
        }
        while ((dx * dx + dy * dy) > _r2);

        return new double[] { _x + dx, _y + dy };
    }
}