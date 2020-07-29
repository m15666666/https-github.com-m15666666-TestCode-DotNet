using System;
using System.Collections.Generic;

/*
给定一个二维平面，平面上有 n 个点，求最多有多少个点在同一条直线上。

示例 1:

输入: [[1,1],[2,2],[3,3]]
输出: 3
解释:
^
|
|        o
|     o
|  o  
+------------->
0  1  2  3  4
示例 2:

输入: [[1,1],[3,2],[5,3],[4,1],[2,3],[1,4]]
输出: 4
解释:
^
|
|  o
|     o        o
|        o
|  o        o
+------------------->
0  1  2  3  4  5  6

*/

/// <summary>
/// https://leetcode-cn.com/problems/max-points-on-a-line/
/// 149. 直线上最多的点数
///
///
///
/// </summary>
internal class MaxPointsOnALineSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        var ret = MaxPoints(new int[][] { new[] { -4, 1 }, new[] { -7, 7 }, new[] { -1, 5 }, new[] { 9, -25 } });
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int MaxPoints(int[][] points)
    {
        var slope2Count = new Dictionary<decimal, int>();
        int horisontalCount;
        var n = points.Length;
        if (n < 3) return n;

        int ret = 1;
        for (int i = 0; i < n - 1; i++)
            ret = Math.Max(MaxPointsCrossI(i), ret);
        return ret;

        void TestLine(int i, int j, ref int count, ref int duplicates)
        {
            ///decimal a = 1.1M;

            int x1 = points[i][0];
            int y1 = points[i][1];
            int x2 = points[j][0];
            int y2 = points[j][1];
            if ((x1 == x2) && (y1 == y2)) duplicates++;
            else if (y1 == y2)
            {
                horisontalCount += 1;
                count = Math.Max(horisontalCount, count);
            }
            else
            {
                decimal slope = 1M * (x1 - x2) / (y1 - y2);
                slope2Count[slope] = (slope2Count.ContainsKey(slope) ? slope2Count[slope] : 1) + 1;
                count = Math.Max(slope2Count[slope], count);
            }
        }

        int MaxPointsCrossI(int i)
        {
            slope2Count.Clear();
            horisontalCount = 1;
            int count = 1;
            int duplicates = 0;

            for (int j = i + 1; j < n; j++)
                TestLine(i, j, ref count, ref duplicates);

            return count + duplicates;
        }
    }
}

/*

直线上最多的点数
力扣 (LeetCode)
发布于 2019-06-26
17.0k
方法：枚举
首先简化这个问题：找出一条通过点 i 的直线，使它经过最多的点。

我们会发现，其实只需要考虑当前点之后出现的点 i + 1 .. N - 1 即可，因为通过点 i-2 的直线已经在搜索点 i-2 的过程中考虑过了。

149-pic

思路非常简单：画一条通过点 i 和之后出现的点的直线，在哈希表中存储这条边并计数为 2 = 当前这条直线上有两个点。

假设现在 i < i + k < i + l 这三个点在同一条直线上，那当你画出一条通过 i 和 i+l 的直线后，就会发现它已经记录过了，因此更新这条边对应的计数：count++ 。

如何存储一条直线？

如果这条线是水平的，例如 y=c，我们可以利用常数 c 作为水平直线的哈希表的键。

注意到这里所有直线都是通过同一个点 i 的，因此并不需要记录 c 的值而只需要统计水平直线的个数。

剩下的直线可以被表示成 x = slope * y + c ，同样 c 也是不需要的，因为所有直线都通过同一个点 i 所以只需要用 slope 作为直线的键。

通过两个点 1 和 2 的直线方程可以 用坐标表示 为：

\frac{x - x_1}{x_1 - x_2} = \frac{y - y_1}{y_1 - y_2}
x 
1
?	
 ?x 
2
?	
 
x?x 
1
?	
 
?	
 = 
y 
1
?	
 ?y 
2
?	
 
y?y 
1
?	
 
?	
 

转换成 x = slope \times y + cx=slope×y+c 表示为：

slope = \frac{x_1 - x_2}{y_1 - y_2}
slope= 
y 
1
?	
 ?y 
2
?	
 
x 
1
?	
 ?x 
2
?	
 
?	
 

所以，最终的算法如下：

初始化最大点数 max_count = 1 。
迭代从 0 到 N - 2 所有的点 i 。
对于每个点 i 找出通过该点直线的最大点数 max_count_i：
初始化通过点 i 直线的最大点数：count = 1 。
迭代下一个顶点 j 从 i+1 到 N-1 。
如果 j 和 i 重合，更新点 i 相同点的个数。
否则：
保存通过 i 和 j 的直线。
每步更新 count 。
返回结果 max_count_i = count + duplicates 。
更新结果 max_count = max(max_count, max_count_i) 。



class Solution {
  int[][] points;
  int n;
  HashMap<Double, Integer> lines = new HashMap<Double, Integer>();
  int horisontal_lines;

  public Pair<Integer, Integer> add_line(int i, int j, int count, int duplicates) {
    // 
    // Add a line passing through i and j points.
    // Update max number of points on a line containing point i.
    // Update a number of duplicates of i point.
    // 
    // rewrite points as coordinates
    int x1 = points[i][0];
    int y1 = points[i][1];
    int x2 = points[j][0];
    int y2 = points[j][1];
    // add a duplicate point
    if ((x1 == x2) && (y1 == y2))
      duplicates++;
    // add a horisontal line : y = const
    else if (y1 == y2) {
      horisontal_lines += 1;
      count = Math.max(horisontal_lines, count);
    }
    // add a line : x = slope * y + c
    // only slope is needed for a hash-map
    // since we always start from the same point
    else {
      double slope = 1.0 * (x1 - x2) / (y1 - y2) + 0.0;
      lines.put(slope, lines.getOrDefault(slope, 1) + 1);
      count = Math.max(lines.get(slope), count);
    }
    return new Pair(count, duplicates);
  }

  public int max_points_on_a_line_containing_point_i(int i) {
    
    // Compute the max number of points
    // for a line containing point i.
    
    // init lines passing through point i
    lines.clear();
    horisontal_lines = 1;
    // One starts with just one point on a line : point i.
    int count = 1;
    // There is no duplicates of a point i so far.
    int duplicates = 0;

    // Compute lines passing through point i (fixed)
    // and point j (interation).
    // Update in a loop the number of points on a line
    // and the number of duplicates of point i.
    for (int j = i + 1; j < n; j++) {
      Pair<Integer, Integer> p = add_line(i, j, count, duplicates);
      count = p.getKey();
      duplicates = p.getValue();
    }
    return count + duplicates;
  }


  public int maxPoints(int[][] points) {

    this.points = points;
    // If the number of points is less than 3
    // they are all on the same line.
    n = points.length;
    if (n < 3)
      return n;

    int max_count = 1;
    // Compute in a loop a max number of points 
    // on a line containing point i.
    for (int i = 0; i < n - 1; i++)
      max_count = Math.max(max_points_on_a_line_containing_point_i(i), max_count);
    return max_count;
  }
}
复杂度分析

时间复杂度：O(N^2)O(N 
2
 ) ，因为通过点 0 最多有 N-1 条直线，通过点 1 最多有 N-2 条直线，通过点 N-2 只有 1 条直线，所以结果共有 (N - 1) + (N - 2) + .. + 1 = N(N - 1)/2 次操作，因此时间复杂度为 O(N^2)O(N 
2
 ) 。
空间复杂度：O(N)O(N) ，用来记录最多不超过 N-1 条直线。
下一篇：正确解法：直线上最多的点数，平面几何通用直线方程

public class Solution {

    public class Vector2 : IEqualityComparer<Vector2>
    {
        public int x;
        public int y;
        public Vector2()
        {
        }
        public Vector2(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public bool Equals(Vector2 x, Vector2 y)
        {
            return x.x == y.x && x.y == y.y;
        }

        public int GetHashCode(Vector2 obj)
        {
            return obj.x + obj.y;
        }
    }
    Dictionary<Vector2, int> lines = new Dictionary<Vector2, int>(new Vector2());
    public int MaxPoints(int[][] points) {
        int count = points.Length;
        if (count < 3)
            return count;
            
        List<Vector2> temp = new List<Vector2>();
        for (int i = 0; i < count; i++)
        {
            temp.Add(new Vector2(points[i][0], points[i][1]));
        }
        int maxCount = 1;
        int lineCount = 0;
        int duplicates = 0;
        int dx = 0;
        int dy = 0;
        int divisor = 0;
        Vector2 key;

        for (int i = 0; i < temp.Count - 1; i++)
        {
            lineCount = 0;
            duplicates = 0;
            lines.Clear();
            for (int j = i + 1; j < temp.Count; j++)
            {
                dx = temp[i].x - temp[j].x;
                dy = temp[i].y - temp[j].y;
                if (dx == 0 && dy == 0)
                {
                    duplicates++;
                    continue;
                }

                divisor = gcd(dx, dy);
                if (divisor != 0)
                {
                    dx /= divisor;
                    dy /= divisor;
                }
                key = new Vector2(dx, dy);

                if (lines.ContainsKey(key))
                    lines[key]++;
                else
                    lines.Add(key, 1);

                lineCount = Math.Max(lineCount, lines[key]);
            }
            maxCount = Math.Max(maxCount, lineCount + duplicates + 1);
        }

        return maxCount;
    }
    static int gcd(int x, int y)
    {
        if (y == 0)
            return x;
        else
            return gcd(y, x % y);
    }
}

public class Solution {
        // 字典解法, 以约分后的分数形式保存斜率为key
    public int MaxPoints(int[][] points)
    {
        if (points == null || points.Length == 0)
            return 0;

        if (points.Length <= 2)
            return points.Length;

        int res = 2;
        // 遍历计算每个点连接其他点组成的斜率,放入字典
        // 用Math.Max来比较出最多的
        for (int i = 0; i < points.Length - 1; i++)
        {
            // 注意以下定义在循环捏
            Dictionary<(int, int), int> dict = new Dictionary<(int, int), int>();
            // 重合点
            int dup      = 1;
            int maxCount = 0;
            for (int j = i + 1; j < points.Length; j++)
            {
                // 重合点数量
                if (points[i][0] == points[j][0] && points[i][1] == points[j][1])
                    dup++;
                else
                {
                    // 约分后的斜率
                    (int, int) slope = GetSlope(points[j], points[i]);

                    if (dict.ContainsKey(slope))
                        dict[slope]++;
                    else
                        dict.Add(slope, 1);
                    // 比较之后点的计数
                    maxCount = Math.Max(maxCount, dict[slope]);
                }
            }
            // 加上重复点
            res = Math.Max(res, maxCount + dup);
        }

        return res;
    }

    // 获得斜率,并约分
    public (int, int) GetSlope(int[] pb, int[] pa)
    {
        int dy = pb[1] - pa[1];
        int dx = pb[0] - pa[0];

        // vertical line
        if (dx == 0)
            return (0, pa[0]);

        // horizontal line
        if (dy == 0)
            return (pa[1], 0);
        // 获取公约数,约分斜率
        int d = gcd(dy, dx);
        return (dy / d, dx / d);
    }
    // public int MaxPoints(int[][] points) {   
    //     // if(points.Length == 0) return 0;
    //     // int result = 1;
    //     // Dictionary<string,int> hash = new Dictionary<string,int>();
    //     // for(int i = 0;i < points.Length;i++)
    //     // {
    //     //     int same = 0;
    //     //     for(int j = 0;j < points.Length;j++)
    //     //     {
    //     //         if(i == j) continue;
    //     //         int delx = points[i][0] - points[j][0];
    //     //         int dely = points[i][1] - points[j][1];
    //     //         if(delx == 0 && dely == 0)
    //     //         {
    //     //             same++;
    //     //             continue;
    //     //         }
    //     //         int ca = gcd(delx,dely);
    //     //         int yux = delx / ca;
    //     //         int yuy = dely / ca;
    //     //         string key = yux.ToString() + "," + yuy.ToString();
    //     //         if(hash.ContainsKey(key))
    //     //             hash[key]++;
    //     //         else
    //     //             hash.Add(key,2);

    //     //     }
    //     //     result = Math.Max(result,hash.Count > 0 ? hash.Max(p =>p.Value) + same : 1 + same);
    //     //     hash.Clear();
    //     // }
    //     // return result; 
    // }
    
    public int gcd(int a,int b)
    {
        return b == 0 ? a : gcd(b , a%b);
    }
}


*/