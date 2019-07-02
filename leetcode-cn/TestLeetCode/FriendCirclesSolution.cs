using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
班上有 N 名学生。其中有些人是朋友，有些则不是。他们的友谊具有是传递性。
如果已知 A 是 B 的朋友，B 是 C 的朋友，那么我们可以认为 A 也是 C 的朋友。所谓的朋友圈，是指所有朋友的集合。

给定一个 N * N 的矩阵 M，表示班级中学生之间的朋友关系。如果M[i][j] = 1，表示已知第 i 个和 j 个学生互为朋友关系，
否则为不知道。你必须输出所有学生中的已知的朋友圈总数。

示例 1:

输入: 
[[1,1,0],
 [1,1,0],
 [0,0,1]]
输出: 2 
说明：已知学生0和学生1互为朋友，他们在一个朋友圈。
第2个学生自己在一个朋友圈。所以返回2。
示例 2:

输入: 
[[1,1,0],
 [1,1,1],
 [0,1,1]]
输出: 1
说明：已知学生0和学生1互为朋友，学生1和学生2互为朋友，所以学生0和学生2也是朋友，所以他们三个在一个朋友圈，返回1。
注意：

N 在[1,200]的范围内。
对于所有学生，有M[i][i] = 1。
如果有M[i][j] = 1，则有M[j][i] = 1。 
*/
/// <summary>
/// https://leetcode-cn.com/problems/friend-circles/
/// 547. 朋友圈
/// https://blog.csdn.net/qq_41822647/article/details/88422310
/// 
/// https://leetcode-cn.com/submissions/detail/21714878/
/// </summary>
class FriendCirclesSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int FindCircleNum(int[][] M)
    {
        _P = new int[M.Length];
        for (int i = 0; i < M.Length; i++) _P[i] = i;
        for (int i = 0; i < M.Length; i++)
        {
            for (int j = 0; j < i; j++) //因为数组是对称的，所以只需要遍历它的左下角或右上角即可
            {
                if (M[i][j] == 0) continue; //表示没有关系
                Join(i, j); //拜大哥
            }
        }
        int ret = 0;
        for (int i = 0; i < _P.Length; i++)
            if (i == _P[i]) ret++;
        return ret;
    }
    private int[] _P;
    private int FindRoot(int x)
    {
        if (x == _P[x]) return x;
        return _P[x] = FindRoot(_P[x]);
    }
    private void Join(int x, int y)
    {
        int a = FindRoot(x);
        int b = FindRoot(y);
        if (a == b) return;
        _P[a] = b;
    }
}
/*
public class Solution {
    public int FindCircleNum(int[][] M) 
    {
        // approach 1
        bool[] visited = new bool[M.Length];
        int counter = 0;
        for(int i = 0; i < visited.Length; i++)
        {
            if(!visited[i])
            {
                counter++;
                DSF(M, i, visited);
            }
        }
        return counter;
    }
    
    private void DSF(int[][] M, int index, bool[] visited)
    {
        if(visited[index]) return;
        visited[index] = true;
        for(int i = 0; i < visited.Length; i++)
        {
            if(M[index][i] == 1 && !visited[i])
                DSF(M, i, visited);
        }
    }
}

*/
