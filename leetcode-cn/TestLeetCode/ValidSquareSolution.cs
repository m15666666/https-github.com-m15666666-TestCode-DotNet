using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定二维空间中四点的坐标，返回四点是否可以构造一个正方形。

一个点的坐标（x，y）由一个有两个整数的整数数组表示。

示例:

输入: p1 = [0,0], p2 = [1,1], p3 = [1,0], p4 = [0,1]
输出: True
 
注意:

所有输入整数都在 [-10000，10000] 范围内。
一个有效的正方形有四个等长的正长和四个等角（90度角）。
输入点没有顺序。 
*/
/// <summary>
/// https://leetcode-cn.com/problems/valid-square/
/// 593. 有效的正方形
/// https://leetcode-cn.com/problems/valid-square/solution/gen-ju-zheng-fang-xing-he-ling-xing-de-ding-yi-pan/
/// </summary>
class ValidSquareSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public bool ValidSquare(int[] p1, int[] p2, int[] p3, int[] p4)
    {
        //List<int[]> points = new List<int[]> {p1,p2,p3,p4};
        //points = points.OrderBy(p => p[0]).ThenBy(p => p[1]).ToList();

        //p1 = points[0];
        //p2 = points[1];
        //p3 = points[2];
        //p4 = points[3];

        //int side = p2[1] - p1[1];

        //return ((p1[0] == p2[0]) && (p3[0] == p4[0]) && (p1[1] == p3[1]) && (p2[1] == p4[1])
        //    && (side == p4[1] - p3[1]) && (side == p3[0] - p1[0]) && (side == p4[0] - p2[0])
        //    );
        int[][] points = new int[][] { p1, p2, p3, p4 };
        int[] distanceSquare = new int[6];
        int distanceSquareIndex = 0;
        for ( int i = 0; i < points.Length - 1; i++)
        {
            for( int j = i + 1; j < points.Length; j++)
            {
                distanceSquare[distanceSquareIndex++] = DistanceSquare(points[i], points[j]);
            }
        }
        Array.Sort(distanceSquare);

        // 边长
        int edge = distanceSquare[0];

        // 对角线
        int didiagonal = distanceSquare[5];
        return (edge == distanceSquare[1])
            && (edge == distanceSquare[2])
            && (edge == distanceSquare[3])
            && (distanceSquare[4] == didiagonal)
            && (edge < didiagonal);

        int DistanceSquare(int[] x, int[] y)
        {
            var deltaX = x[0] - y[0];
            var deltaY = x[1] - y[1];
            return deltaX * deltaX + deltaY * deltaY;
        }
    }
}
/*
public class Solution {
    public bool ValidSquare(int[] p1, int[] p2, int[] p3, int[] p4) {
        int dis12 = (p1[0] - p2[0]) * (p1[0] - p2[0]) + (p1[1] - p2[1]) * (p1[1] - p2[1]);
        int dis13 = (p1[0] - p3[0]) * (p1[0] - p3[0]) + (p1[1] - p3[1]) * (p1[1] - p3[1]);
        int dis14 = (p1[0] - p4[0]) * (p1[0] - p4[0]) + (p1[1] - p4[1]) * (p1[1] - p4[1]);
        int dis23 = (p2[0] - p3[0]) * (p2[0] - p3[0]) + (p2[1] - p3[1]) * (p2[1] - p3[1]);
        int dis24 = (p2[0] - p4[0]) * (p2[0] - p4[0]) + (p2[1] - p4[1]) * (p2[1] - p4[1]);
        int dis34 = (p3[0] - p4[0]) * (p3[0] - p4[0]) + (p3[1] - p4[1]) * (p3[1] - p4[1]);
        
        int[] arr = new int[6];
        arr[0] = dis12;
        arr[1] = dis13;
        arr[2] = dis14;
        arr[3] = dis23;
        arr[4] = dis24;
        arr[5] = dis34;
        
        Array.Sort(arr);
        
        if (arr[0] != 0 && arr[0] == arr[1] && arr[1] == arr[2] && arr[2] == arr[3] && arr[4] == arr[5] && arr[4] == 2 * arr[0]) {
            return true;
        } else {
            return false;
        }
    }
}
public class Solution {
    public bool ValidSquare(int[] p1, int[] p2, int[] p3, int[] p4)
    {
        HashSet<int> lengths = new HashSet<int>();
        lengths.Add(GetDis(p1, p2));
        lengths.Add(GetDis(p1, p3));
        lengths.Add(GetDis(p1, p4));
        lengths.Add(GetDis(p2, p3));
        lengths.Add(GetDis(p2, p4));
        lengths.Add(GetDis(p3, p4));

        foreach (int item in lengths)
        {
            if (item == 0)
            {
                return false;
            }
        }

        return lengths.Count == 2;
    }

    public int GetDis(int[] p1, int[] p2)
    {
        return (p1[1] - p2[1]) * (p1[1] - p2[1]) + (p1[0] - p2[0]) * (p1[0] - p2[0]);
    }
}
     
*/
