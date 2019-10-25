using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
在 R 行 C 列的矩阵上，我们从 (r0, c0) 面朝东面开始

这里，网格的西北角位于第一行第一列，网格的东南角位于最后一行最后一列。

现在，我们以顺时针按螺旋状行走，访问此网格中的每个位置。

每当我们移动到网格的边界之外时，我们会继续在网格之外行走（但稍后可能会返回到网格边界）。

最终，我们到过网格的所有 R * C 个空间。

按照访问顺序返回表示网格位置的坐标列表。

 

示例 1：

输入：R = 1, C = 4, r0 = 0, c0 = 0
输出：[[0,0],[0,1],[0,2],[0,3]]


 

示例 2：

输入：R = 5, C = 6, r0 = 1, c0 = 4
输出：[[1,4],[1,5],[2,5],[2,4],[2,3],[1,3],[0,3],[0,4],[0,5],[3,5],[3,4],[3,3],[3,2],[2,2],[1,2],[0,2],[4,5],[4,4],[4,3],[4,2],[4,1],[3,1],[2,1],[1,1],[0,1],[4,0],[3,0],[2,0],[1,0],[0,0]]


 

提示：

1 <= R <= 100
1 <= C <= 100
0 <= r0 < R
0 <= c0 < C
*/
/// <summary>
/// https://leetcode-cn.com/problems/spiral-matrix-iii/
/// 885. 螺旋矩阵 III
/// 
/// </summary>
class SpiralMatrixIIISolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int[][] SpiralMatrixIII(int R, int C, int r0, int c0)
    {
        int len = R * C;
        int[][] ret = new int[len][];
        int count = 0;

        ret[count++] = new int[] { r0, c0 };
        if (len == 1) return ret;

        int upper1 = 2 * Math.Max(R, C);
        for (int k = 1; k < upper1; k += 2)
            for (int i = 0; i < 4; ++i)
            {  // i: direction index
                int dk = k + (i / 2);  // number of steps in this direction
                for (int j = 0; j < dk; ++j)
                {
                    r0 += dr[i];
                    c0 += dc[i];
                    if (0 <= r0 && r0 < R && 0 <= c0 && c0 < C)
                    {
                        ret[count++] = new int[] { r0, c0 };
                        if (count == len) return ret;
                    }
                }
            }

        throw null;
    }

    private static readonly int[] dr = new int[] { 0, 1, 0, -1 };
    private static readonly int[] dc = new int[] { 1, 0, -1, 0 };
}
/*
public class Solution
{
    public int[][] SpiralMatrixIII(int R, int C, int r0, int c0)
    {

        int total = (int)(R * C);
        int[][] ans = new int[total][];
        int direction = 0; // 0 - E; 1 - S; 2 - W; 3 - N
        int[] dr = new int[4] { 0, 1, 0, -1 };
        int[] dc = new int[4] { 1, 0, -1, 0 };

        int r = r0; // current row index
        int c = c0; // current columne index
        int steps = 0;

        ans[0] = new int[2] { r, c };

        for (int i = 1; i < total;)
        {
            steps = direction % 2 == 0 ? steps + 1 : steps;

            for (int move = 0; move < steps; move++)
            {
                r += dr[direction];
                c += dc[direction];
                if (r >= 0 && r < R && c >= 0 && c < C)
                {
                    ans[i] = new int[2] { r, c };
                    i++;
                }
            }
            direction = (direction + 1) % 4;
        }
        return ans;
    }
}
    
public class Solution
{
    public int[][] SpiralMatrixIII(int R, int C, int r0, int c0)
    {

        int total = (int)(R * C);
        int[][] ans = new int[total][];
        int direction = 0; // 0 - E; 1 - S; 2 - W; 3 - N

        int r = r0; // current row index
        int c = c0; // current columne index
        int steps = 0;

        ans[0] = new int[2] { r, c };

        for (int i = 1; i < total;)
        {
            steps = direction % 2 == 0 ? steps + 1 : steps;

            for (int move = 0; move < steps; move++)
            {
                if (direction == 0) //E
                    c++;
                else if (direction == 1)    // S
                    r++;
                else if (direction == 2)    // W
                    c--;
                else    // N
                    r--;
                if (r >= 0 && r < R && c >= 0 && c < C)
                {
                    ans[i] = new int[2] { r, c };
                    i++;
                }
            }

            direction = direction == 3 ? 0 : direction + 1;
        }

        return ans;
    }
}
*/
