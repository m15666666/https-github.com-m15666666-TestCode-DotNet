using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个三角形，找出自顶向下的最小路径和。每一步只能移动到下一行中相邻的结点上。

相邻的结点 在这里指的是 下标 与 上一层结点下标 相同或者等于 上一层结点下标 + 1 的两个结点。

 

例如，给定三角形：

[
     [2],
    [3,4],
   [6,5,7],
  [4,1,8,3]
]
自顶向下的最小路径和为 11（即，2 + 3 + 5 + 1 = 11）。

 

说明：

如果你可以只使用 O(n) 的额外空间（n 为三角形的总行数）来解决这个问题，那么你的算法会很加分。
 
 
*/
/// <summary>
/// https://leetcode-cn.com/problems/triangle/
/// 120.三角形最小路径和
/// 
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
/*
自底向上动态规划，类似于从起点到目的地之间的最短路径
秋天回来了
发布于 2020-03-30
6.0k

class Solution(object):
    def minimumTotal(self, triangle):
        """
        :type triangle: List[List[int]]
        :rtype: int
        """
        n=len(triangle)
        for row in range(n-2,-1,-1):
            for col in range(len(triangle[row])):
                triangle[row][col]+=min(triangle[row+1][col],triangle[row+1][col+1])
        return triangle[0][0]
以
[
[2],
[3,4],
[6,5,7],
[4,1,8,3]
]
为例：

第一次对倒数第二行操作，6变成6+min(4,1)=7，5变成5+min(1,8)=6，7变成7+min(8,3)=10；这应该很好理解，当你选择的路径让你到达6时，接下来你可以选择4或者1，那么你自然会选择更小的1；当你选择的路径让你到达5时，接下来你可以选择1或者8，那么你自然会选择更小的1；当你选择的路径让你到达7时，接下来你可以选择8或者3，那么你自然会选择更小的3。

因此，可以把原来的lists变成
[
[2],
[3,4],
[7,6,10],
[4,1,8,3]
]
注意，因为我们是在原数组上进行操作，所以仍然保留了最后一行，但这不影响结果，因为我们最后会返回顶部的那个数，

继续向上操作，即操作倒数第三行，可以把lists变成
[
[2],
[9,10],
[7,6,10],
[4,1,8,3]
]

继续向上操作，即操作倒数第四行，可以把lists变成
[
[11],
[9,10],
[7,6,10],
[4,1,8,3]
]

最后，返回顶部的值。
 
public class Solution {
    public int MinimumTotal(IList<IList<int>> triangle) {
        int count = triangle.Count;
        int[,] result = new int[count, count];
        for(int i = 0;i < count;i++)
        {
            result[count - 1, i] = triangle[count - 1][i];
        }
        for(int i = count - 2;i >= 0;i--)
        {
            for(int j = 0;j < triangle[i].Count;j++)
            {
                result[i, j] = Math.Min(result[i + 1, j], result[i + 1, j + 1]) + triangle[i][j];
            }
        }
        return result[0, 0];
    }
} 

public class Solution {
    public int MinimumTotal(IList<IList<int>> triangle) {
        if( triangle.Count == 0 )
            return 0;
        
        for( int row = triangle.Count - 2 ; row >= 0 ; row-- )
        {
            for( int i = 0 ; i < triangle[row].Count; i++  )
                triangle[row][i] += Math.Min( triangle[row+1][i] , triangle[row+1][i+1] );
        }
        return triangle[0][0];
    }
} 
*/