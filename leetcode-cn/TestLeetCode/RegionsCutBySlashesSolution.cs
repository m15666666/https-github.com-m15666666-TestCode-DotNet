using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
在由 1 x 1 方格组成的 N x N 网格 grid 中，每个 1 x 1 方块由 /、\ 或空格构成。这些字符会将方块划分为一些共边的区域。

（请注意，反斜杠字符是转义的，因此 \ 用 "\\" 表示。）。

返回区域的数目。

 

示例 1：

输入：
[
  " /",
  "/ "
]
输出：2
解释：2x2 网格如下：

示例 2：

输入：
[
  " /",
  "  "
]
输出：1
解释：2x2 网格如下：

示例 3：

输入：
[
  "\\/",
  "/\\"
]
输出：4
解释：（回想一下，因为 \ 字符是转义的，所以 "\\/" 表示 \/，而 "/\\" 表示 /\。）
2x2 网格如下：

示例 4：

输入：
[
  "/\\",
  "\\/"
]
输出：5
解释：（回想一下，因为 \ 字符是转义的，所以 "/\\" 表示 /\，而 "\\/" 表示 \/。）
2x2 网格如下：

示例 5：

输入：
[
  "//",
  "/ "
]
输出：3
解释：2x2 网格如下：

 

提示：

1 <= grid.length == grid[0].length <= 30
grid[i][j] 是 '/'、'\'、或 ' '。
*/
/// <summary>
/// https://leetcode-cn.com/problems/regions-cut-by-slashes/
/// 959. 由斜杠划分区域
/// 
/// </summary>
class RegionsCutBySlashesSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    public int RegionsBySlashes(string[] grid)
    {
        int n = grid.Length;
        int fourN = 4 * n;
        const int NextBoxOffset = 4;
        _separatedArea = n * n * 4;
        _roots = new int[_separatedArea];
        Array.Fill(_roots, -1);

        for (int i = 0, rowCount = 0; i < n; i++, rowCount += fourN)
        {
            for (int j = 0, columnCount = 0; j < n; j++, columnCount += NextBoxOffset)   //每个格子中只可能是对角线有边来间隔，将每个格子向下和向右合并
            {
                int baseIndex = rowCount + columnCount;
                if (i + 1 < n)    //可以向下合并
                {
                    Union(baseIndex + 1, baseIndex + fourN);  //i行j列的下格子和i+1行j列的上格子进行合并
                }
                if (j + 1 < n)    //可以向右合并
                {
                    Union(baseIndex + 3, baseIndex + NextBoxOffset + 2); //i行j列的右格子和i行j+1列的左格子进行合并
                }

                int right = baseIndex + 3;
                switch (grid[i][j])
                {
                    case ' ':
                        Union(baseIndex, right);   //单个正方形内部的上和右进行合并
                        Union(baseIndex + 1, right); //单个正方形内部的下、右进行合并
                        Union(baseIndex + 2, right); //单个正方形内部的左、右进行合并
                        break;

                    case '/':
                        Union(baseIndex + 1, right);   //下和右合并
                        Union(baseIndex, baseIndex + 2);     //左和上合并
                        break;

                    case '\\':
                        Union(baseIndex, right);   //上和右合并
                        Union(baseIndex + 1, baseIndex + 2); //下和左合并
                        break;
                }
            }
        }
        return _separatedArea;
    }

    private int FindRoot(int x)
    {
        int root = _roots[x];
        if (root == -1) return x;  //找到根节点，返回根节点的下标，只有根节点的uset是负数，其他节点的uset是根节点的下标

        root = FindRoot(root);  //递归地找根节点，且可以将查找路径上的所有节点的uset都直接指向根节点，进行了路径压缩
        return _roots[x] = root;
    }

    private void Union(int x, int y)
    {
        x = FindRoot(x);
        y = FindRoot(y);
        if (x == y) return;  //两个节点已经在同一棵树上，已联通

        _roots[x] = y;
        _separatedArea--;    //每发生一次合并，不联通的部分个数减少一
    }

    private int[] _roots;
    private int _separatedArea;
}
/*
class Solution {
public:
    int find(int x){
        if(uset[x]<0) return x;  //找到根节点，返回根节点的下标，只有根节点的uset是负数，其他节点的uset是根节点的下标
        uset[x] = find(uset[x]);  //递归地找根节点，且可以将查找路径上的所有节点的uset都直接指向根节点，进行了路径压缩
        return uset[x];
    }
    void Union(int x,int y){
        if((x=find(x))==(y=find(y))) return ;  //两个节点已经在同一棵树上，已联通
        if(uset[x]<uset[y])
            uset[x]+=uset[y], uset[y]=x;
        else
            uset[y]+=uset[x], uset[x]=y;
        ans--;    //每发生一次合并，不联通的部分个数减少一
        return ;
    }
    int regionsBySlashes(vector<string>& grid) {
        int n=grid.size();  //n*n
        ans=n*n*4;  //一共n*n个正方形，每个正方形分成四个格子
        //每个正方形被分成上下左右四个小格子 下标是  i*4*n+j*4+0 ... i*4*n+j*4+3
        for(int i=0;i<n*n*4;i++) 
        {
            uset[i]=-1;  //并查集初始化，-1表示已自己为根结点构建树
        }
        for(int i=0;i<n;i++)
        {
            for(int j=0;j<n;j++)   //每个格子中只可能是对角线有边来间隔，将每个格子向下和向右合并
            {
				int root = i*4*n+j*4;
                if(i+1<n)    //可以向下合并
                {
                    Union(root+1, (root + 4 * n));  //i行j列的下格子和i+1行j列的上格子进行合并
                }
                if(j+1<n)    //可以向右合并
                {
                    Union(root+3, root + 4 + 2); //i行j列的右格子和i行j+1列的左格子进行合并
                }
                if(grid[i][j]==' ')
                {
                    Union(root, root+3);   //单个正方形内部的上和右进行合并
                    Union(root+1, root+2); //单个正方形内部的左和下进行合并
                    Union(root+1, root+3); //单个正方形内部的四个格子都合并了
                }
                else if(grid[i][j]=='/')
                {
                    Union(root+1, root+3);   //下和右合并
                    Union(root, root+2);     //左和上合并
                }
                else if(grid[i][j]=='\\')
                {
                    Union(root, root+3);   //上和右合并
                    Union(root+1, root+2); //下和左合并
                }
            }
        }
        return ans;
    }
    private:
    int uset[3605],ans;
};

作者：Kaya_lss
链接：https://leetcode-cn.com/problems/regions-cut-by-slashes/solution/mei-ge-zheng-fang-xing-fen-cheng-shang-xia-zuo-you/

public class Solution {
    private int[] p;
    public int RegionsBySlashes(string[] grid)
    {
        int n = grid.Length;
        p = new int[4 * n * n];
        for (int i = 0; i < 4 * n * n; ++i)
            p[i] = i;
        
        for (int i = 0; i < n; ++i)
        {
            for (int j = 0; j < n; ++j)
            {
                int index = 4 * (i * n + j);
                switch (grid[i][j])
                {
                    case '/':
                        Union(index + 0, index + 3);
                        Union(index + 1, index + 2);
                        break;
                    case '\\':
                        Union(index + 0, index + 1);
                        Union(index + 2, index + 3);
                        break;
                    case ' ':
                        Union(index + 0, index + 1);
                        Union(index + 1, index + 2);
                        Union(index + 2, index + 3);
                        break;
                }

                if (i < n - 1)
                    Union(index + 2, index + 4 * n + 0);
                if (j < n - 1)
                    Union(index + 1, index + 4 + 3);
            }
        }

        int res = 0;
        for (int i = 0; i < p.Length; ++i)
        {
            if (i == GetParent(p[i]))
                res++;
        }
        return res;
    }

    private int GetParent(int i)
    {
        while (p[i] != i)
        {
            p[i] = p[p[i]];
            i = p[i];
        }
        return p[i];
    }

    private void Union(int a, int b)
    {
        int pa = GetParent(a);
        int pb = GetParent(b);
        if(pa == pb) return;
        p[pa] = pb;
    }
}
 
*/
