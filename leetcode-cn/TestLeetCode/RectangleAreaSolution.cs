using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// https://leetcode-cn.com/problems/rectangle-area/
/// 223. 矩形面积
/// 在二维平面上计算出两个由直线构成的矩形重叠后形成的总面积。
/// 每个矩形由其左下顶点和右上顶点坐标表示，如图所示。
/// 示例:
/// 输入: -3, 0, 3, 4, 0, -1, 9, 2
/// 输出: 45
/// 说明: 假设矩形面积不会超出 int 的范围。
/// https://blog.csdn.net/w8253497062015/article/details/80118683
/// https://blog.csdn.net/cc_fys/article/details/80601190
/// </summary>
class RectangleAreaSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    public int ComputeArea(int A, int B, int C, int D, int E, int F, int G, int H)
    {
        int total = (C - A) * (D - B) + (G - E) * (H - F);

        if (C <= E || A >= G || B >= H || D <= F) return total;
        int[] xs = new int[] { A, C, E, G};
        int[] ys = new int[] { B, D, F, H };
        Array.Sort(xs);
        Array.Sort(ys);

        return total - (xs[2] - xs[1]) * (ys[2] - ys[1]);
    }
}