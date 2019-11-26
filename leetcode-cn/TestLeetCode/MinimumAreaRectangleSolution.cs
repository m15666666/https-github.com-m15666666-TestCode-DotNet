using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定在 xy 平面上的一组点，确定由这些点组成的矩形的最小面积，其中矩形的边平行于 x 轴和 y 轴。

如果没有任何矩形，就返回 0。

示例 1：

输入：[[1,1],[1,3],[3,1],[3,3],[2,2]]
输出：4
示例 2：

输入：[[1,1],[1,3],[3,1],[3,3],[4,1],[4,3]]
输出：2
 

提示：

1 <= points.Length <= 500
0 <= points[i][0] <= 40000
0 <= points[i][1] <= 40000
所有的点都是不同的。
*/
/// <summary>
/// https://leetcode-cn.com/problems/minimum-area-rectangle/
/// 939. 最小面积矩形
/// 
/// </summary>
class MinimumAreaRectangleSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int MinAreaRect(int[][] points)
    {
        const int mod = 40001;
        var pointSet = new HashSet<int>();
        foreach (int[] point in points)
            pointSet.Add(mod * point[0] + point[1]);

        int len = points.Length;
        int ret = int.MaxValue;
        for (int i = 0; i < len; ++i)
            for (int j = i + 1; j < len; ++j)
            {
                if (points[i][0] != points[j][0] && points[i][1] != points[j][1])
                {
                    if (pointSet.Contains(mod * points[i][0] + points[j][1]) &&
                            pointSet.Contains(mod * points[j][0] + points[i][1]))
                        ret = Math.Min(ret, Math.Abs(points[j][0] - points[i][0]) *
                                            Math.Abs(points[j][1] - points[i][1]));
                }
            }

        return ret < int.MaxValue ? ret : 0;
    }
}
/*
方法一：按列排序
我们将这些点按照 x 轴（即列）排序，那么对于同一列的两个点 (x, y1) 和 (x, y2)，我们找出它作为右边界的最小的矩形。这可以通过记录下我们之前遇到的所有 (y1, y2) 点对来做到。

JavaPython
class Solution {
    public int minAreaRect(int[][] points) {
        Map<Integer, List<Integer>> rows = new TreeMap();
        for (int[] point: points) {
            int x = point[0], y = point[1];
            rows.computeIfAbsent(x, z-> new ArrayList()).add(y);
        }

        int ans = Integer.MAX_VALUE;
        Map<Integer, Integer> lastx = new HashMap();
        for (int x: rows.keySet()) {
            List<Integer> row = rows.get(x);
            Collections.sort(row);
            for (int i = 0; i < row.size(); ++i)
                for (int j = i+1; j < row.size(); ++j) {
                    int y1 = row.get(i), y2 = row.get(j);
                    int code = 40001 * y1 + y2;
                    if (lastx.containsKey(code))
                        ans = Math.min(ans, (x - lastx.get(code)) * (y2-y1));
                    lastx.put(code, x);
                }
        }

        return ans < Integer.MAX_VALUE ? ans : 0;
    }
}
复杂度分析

时间复杂度：O(N^2)O(N 
2
 )，其中 NN 是数组 points 的长度。

空间复杂度：O(N)O(N)。

方法二：枚举对角线
我们将所有点放入集合中，并枚举矩形对角线上的两个点，并判断另外两个点是否出现在集合中。例如我们在枚举到 (1, 1) 和 (5, 5) 时，我们需要判断 (1, 5) 和 (5, 1) 是否也出现在集合中。

JavaPython
class Solution {
    public int minAreaRect(int[][] points) {
        Set<Integer> pointSet = new HashSet();
        for (int[] point: points)
            pointSet.add(40001 * point[0] + point[1]);

        int ans = Integer.MAX_VALUE;
        for (int i = 0; i < points.length; ++i)
            for (int j = i+1; j < points.length; ++j) {
                if (points[i][0] != points[j][0] && points[i][1] != points[j][1]) {
                    if (pointSet.contains(40001 * points[i][0] + points[j][1]) &&
                            pointSet.contains(40001 * points[j][0] + points[i][1])) {
                        ans = Math.min(ans, Math.abs(points[j][0] - points[i][0]) *
                                            Math.abs(points[j][1] - points[i][1]));
                    }
                }
            }

        return ans < Integer.MAX_VALUE ? ans : 0;
    }
}
时间复杂度：O(N^2)O(N 
2
 )，其中 NN 是数组 points 的长度。

空间复杂度：O(N)O(N)。
 

public class Solution {
    public int MinAreaRect(int[][] points) {
        if(points == null || points.Length == 0)
            return 0;

        Dictionary<int, List<int>> rows = new Dictionary<int,List<int>>();

        Array.Sort(points,(a,b) => a[0] == b[0] ? a[1]-b[1] : a[0]-b[0]);

        foreach(int[] point in points){
            int x= point[0];
            int y = point[1];

            if(!rows.ContainsKey(x))
                rows.Add(x,new List<int>());
            
            rows[x].Add(y);
        }

        Dictionary<int,int> lastX = new Dictionary<int,int>();
        int ans = Int32.MaxValue;
        foreach(int key in rows.Keys){
            List<int> row = rows[key];

            for(int i = 0;i<row.Count;i++){
                for(int j = i+1;j<row.Count;j++){
                    int y1 = row[i];
                    int y2 = row[j];
                    int code = 40001*y1+y2;

                    if(lastX.ContainsKey(code)){
                        ans = Math.Min(ans,(key-lastX[code])*(y2-y1));
                    }
                    else{
                        lastX.Add(code,key);
                    }
                    lastX[code] = key;
                }
            }
        }
        return ans == Int32.MaxValue? 0:ans;
    }
}l
*/
