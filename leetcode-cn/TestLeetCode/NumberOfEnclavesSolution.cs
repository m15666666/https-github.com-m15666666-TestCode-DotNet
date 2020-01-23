using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给出一个二维数组 A，每个单元格为 0（代表海）或 1（代表陆地）。

移动是指在陆地上从一个地方走到另一个地方（朝四个方向之一）或离开网格的边界。

返回网格中无法在任意次数的移动中离开网格边界的陆地单元格的数量。

 

示例 1：

输入：[[0,0,0,0],[1,0,1,0],[0,1,1,0],[0,0,0,0]]
输出：3
解释： 
有三个 1 被 0 包围。一个 1 没有被包围，因为它在边界上。
示例 2：

输入：[[0,1,1,0],[0,0,1,0],[0,0,1,0],[0,0,0,0]]
输出：0
解释：
所有 1 都在边界上或可以到达边界。
 

提示：

1 <= A.Length <= 500
1 <= A[i].Length <= 500
0 <= A[i][j] <= 1
所有行的大小都相同
在真实的面试中遇到过这道题？
*/
/// <summary>
/// https://leetcode-cn.com/problems/number-of-enclaves/
/// 1020. 飞地的数量
/// 
/// </summary>
class NumberOfEnclavesSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int NumEnclaves(int[][] A)
    {
        if (A == null || A.Length == 0) return 0;
        
        _A = A;
        _row = A.Length;
        _col = A[0].Length;

        // 淹没所有和边界相接的陆地
        for (int i = 0; i < _row; i++)
        {
            Dfs(i, 0);
            Dfs(i, _col - 1);
        }
        for (int i = 0; i < _col; i++)
        {
            Dfs(0, i);
            Dfs(_row - 1, i);
        }

        // 统计剩下的飞陆
        int ret = 0;
        for (int i = 0; i < _row; i++)
            for (int j = 0; j < _col; j++)
                if (A[i][j] == 1) ret++;
        return ret;
    }
    private int _row;
    private int _col;
    private int[][] _A;
    private void Dfs(int r, int c)
    {
        if (_A[r][c] == 0) return;

        _A[r][c] = 0;
        if (r > 0) { Dfs(r - 1, c); }
        if (c > 0) { Dfs(r, c - 1); }
        if (r < _row - 1) { Dfs(r + 1, c); }
        if (c < _col - 1) { Dfs(r, c + 1); }
    }
}
/*
C++ 多解法：深度优先搜索+并查集
大力王
406 阅读
解法一：
深度优先搜索DFS

class Solution {
public:
    int dirs[4][2] = {{-1, 0}, {1, 0}, {0, 1}, {0, -1}};
    bool isValid(int r, int c, int R, int C) {
        return r >= 0 && r < R && c >= 0 && c < C;
    }
    void dfs(vector<vector<int> >& A, int r, int c, int R, int C) {
        if (!isValid(r, c, R, C) || A[r][c] != 1)
            return;
        A[r][c] = -1;
        for (int i = 0; i < 4; ++i) {
            int nr = r + dirs[i][0];
            int nc = c + dirs[i][1];
            dfs(A, nr, nc, R, C);
        }
    }
    int numEnclaves(vector<vector<int>>& A) {
        if (A.empty())
            return 0;
        int R = A.size();
        int C = A[0].size();
        for (int i = 0; i < R; ++i) {
            dfs(A, i, 0, R, C);
            dfs(A, i, C - 1, R, C);
        }
        for (int i = 0; i < C; ++i) {
            dfs(A, 0, i, R, C);
            dfs(A, R - 1, i, R, C);
        }
        int res = 0;
        for (int i = 0; i < R; ++i) {
            for (int j = 0; j < C; ++j) {
                res += A[i][j] == 1;
            }
        }
        return res;
    }
};
image.png

解法二：
并查集

class Solution {
public:
    int dirs[2][2] = {{1, 0}, {0, 1}};
    vector<int> F;
    int father(int x) {
        if (x != F[x]) F[x] = father(F[x]);
        return F[x];
    }
    bool onEdge(int x, int R, int C) {
        return x / C == 0 || x / C == (R - 1) || (x % C) == 0 || (x % C) == (C - 1);
    }
    int numEnclaves(vector<vector<int>>& A) {
        if (A.empty()) return 0;
        int R = A.size();
        int C = A[0].size();
        F.resize(R * C);
        for (int i = 0; i < R * C; ++i) F[i] = i;
        for (int i = 0; i < R; ++i) {
            for (int j = 0; j < C; ++j) {
                if (A[i][j] != 1) continue;
                int n1 = i * C + j;
                for (int k = 0; k < 2; ++k) {
                    int r = i + dirs[k][0];
                    int c = j + dirs[k][1];
                    if (r >= 0 && r < R && c >=0 && c < C && A[r][c] == 1) {
                        int n2 = r * C + c;
                        int f1 = father(n1);
                        int f2 = father(n2);
                        if (f1 == f2) continue;
                        if (onEdge(f1, R, C)) {
                            F[f2] = f1;
                        } else {
                            F[f1] = f2;
                        }
                    }
                }
            }
        }
        int res = 0;
        for (int i = 1; i < R - 1; ++i) {
            for (int j = 1; j < C - 1; ++j) {
                if (A[i][j] != 1) continue;
                int f = father(i * C + j);
                if (!onEdge(f, R, C)) ++res;
            }
        }
        return res;
    }
};
image.png

下一篇：java 先把所有和边界相接的陆地淹没，剩下的就是飞陆了

java 先把所有和边界相接的陆地淹没，剩下的就是飞陆了
姚俊杰
179 阅读
class Solution {
    int row, col;
    int[][] A;
    public int numEnclaves(int[][] A) {
        if(A == null || A.length == 0) return 0;
        this.A = A;
        this.row = A.length;
        this.col = A[0].length;

        // 淹没所有和边界相接的陆地
        for (int i = 0; i < row; i++) {
            dfs(i, 0);
            dfs(i, col - 1);
        }
        for (int i = 0; i < col; i++) {
            dfs(0, i);
            dfs(row - 1, i);
        }
        // 统计剩下的飞陆
        int count = 0;
        for (int i = 0; i < row; i++) {
            for (int j = 0; j < col; j++) {
                if(A[i][j] == 1) count++;
            }
        }
        return count;
    }

    // 把此大陆淹没，即把 1 变成 0
    public void dfs(int r, int c){
        if(A[r][c] == 0) return;

        A[r][c] = 0;
        if(r > 0       ) { dfs(r - 1, c);       }
        if(c > 0       ) { dfs(r,     c - 1);   }
        if(r < row - 1 ) { dfs(r + 1, c);       }
        if(c < col - 1 ) { dfs(r,     c + 1);   }
    }
}
下一篇：java 深搜 
*/
