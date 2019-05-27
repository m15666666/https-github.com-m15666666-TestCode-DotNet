using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
在二维空间中有许多球形的气球。对于每个气球，提供的输入是水平方向上，气球直径的开始和结束坐标。由于它是水平的，所以y坐标并不重要，因此只要知道开始和结束的x坐标就足够了。开始坐标总是小于结束坐标。平面内最多存在104个气球。

一支弓箭可以沿着x轴从不同点完全垂直地射出。在坐标x处射出一支箭，若有一个气球的直径的开始和结束坐标为 xstart，xend， 且满足  xstart ≤ x ≤ xend，则该气球会被引爆。可以射出的弓箭的数量没有限制。 弓箭一旦被射出之后，可以无限地前进。我们想找到使得所有气球全部被引爆，所需的弓箭的最小数量。

Example:

输入:
[[10,16], [2,8], [1,6], [7,12]]

输出:
2

解释:
对于该样例，我们可以在x = 6（射爆[2,8],[1,6]两个气球）和 x = 11（射爆另外两个气球）。 
*/
/// <summary>
/// https://leetcode-cn.com/problems/minimum-number-of-arrows-to-burst-balloons/
/// 452. 用最少数量的箭引爆气球
/// https://blog.csdn.net/weixin_31866177/article/details/87949657
/// https://jelly54.github.io/2018/11/13/LeetCode-452-%E7%94%A8%E6%9C%80%E5%B0%91%E6%95%B0%E9%87%8F%E7%9A%84%E7%AE%AD%E5%BC%95%E7%88%86%E6%B0%94%E7%90%83/
/// </summary>
class MinimumNumberOfArrowsToBurstBalloonsSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int FindMinArrowShots(int[][] points)
    {
        if (points == null || points.Length == 0) return 0;
        Array.Sort( points, (x, y) => x[0].CompareTo(y[0]) );

        int ret = 1;
        int end = points[0][1];
        for (int i = 1; i < points.Length; i++)
        {
            var point = points[i];
            if (point[0] <= end)
            {
                end = Math.Min(end, point[1]);
                continue;
            }

            ret++;
            end = point[1];
        }
        return ret;
    }
}
/*
public class Solution
{
    public class comp : IComparer<int[]>
    {
        int IComparer<int[]>.Compare(int[] x, int[] y)
        {
            return x[1] - y[1];
        }
    }
    public int FindMinArrowShots(int[,] points)
    {
        int ln = points.GetLength(0);
        if(ln==0)
        {
            return 0;
        }
        List<int[]> ls = new List<int[]>();
        for(int i=0;i<ln;i++)
        {
            ls.Add(new int[] { points[i, 0], points[i, 1] });
        }
        ls.Sort(new comp());
        int result = 1, pre = 0;
        for(int i=1;i<ln;i++)
        {
            if(ls[i][0]>ls[pre][1])
            {
                pre = i;
                result += 1;
            }
        }
        return result;
    }
}
public class Solution {
    public int FindMinArrowShots(int[][] points) {
        if(points.Length==0) return 0;
        quicksort(points,0,points.Length-1);
        int end =points[0][1];
        int num =1;
        for(int i=0;i<points.Length-1;i++)
        {
            if(end >= points[i+1][0])
               continue;
            else
            {
                end=points[i+1][1];
                num++;
            }
        }
        return num;
    }
    public void  quicksort( int[][] points, int start,int end)
    {
        int [] x=new int[]{points[start][0],points[start][1]};
        int i=start,j=end;

        while(i<j)
        {
            while(points[j][1]>=x[1] && i<j)    j--;
            points[i][0]=points[j][0];
            points[i][1]=points[j][1];
           
            while(points[i][1]<=x[1] && i<j)   i++;
            points[j][0]=points[i][0];
            points[j][1]=points[i][1];
        }     
           points[i][0]=x[0];
           points[i][1]=x[1];
        
        if(start<i-1)    quicksort( points,  start, i-1);
        if(i+1<end)      quicksort( points,  i+1, end);
    }  
}
*/
