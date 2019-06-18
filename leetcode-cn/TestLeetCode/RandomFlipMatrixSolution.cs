using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
题中给出一个 n 行 n 列的二维矩阵 (n_rows,n_cols)，且所有值被初始化为 0。要求编写一个 flip 函数，均匀随机的将矩阵中的 0 变为 1，并返回该值的位置下标 [row_id,col_id]；同样编写一个 reset 函数，将所有的值都重新置为 0。尽量最少调用随机函数 Math.random()，并且优化时间和空间复杂度。

注意:

1.1 <= n_rows, n_cols <= 10000

2. 0 <= row.id < n_rows 并且 0 <= col.id < n_cols

3.当矩阵中没有值为 0 时，不可以调用 flip 函数

4.调用 flip 和 reset 函数的次数加起来不会超过 1000 次

示例 1：

输入: 
["Solution","flip","flip","flip","flip"]
[[2,3],[],[],[],[]]
输出: [null,[0,1],[1,2],[1,0],[1,1]]
示例 2：

输入: 
["Solution","flip","flip","reset","flip"]
[[1,2],[],[],[],[]]
输出: [null,[0,0],[0,1],null,[0,0]]
输入语法解释：

输入包含两个列表：被调用的子程序和他们的参数。Solution 的构造函数有两个参数，分别为 n_rows 和 n_cols。flip 和 reset 没有参数，参数总会以列表形式给出，哪怕该列表为空
*/
/// <summary>
/// https://leetcode-cn.com/problems/random-flip-matrix/
/// 519. 随机翻转矩阵
/// https://www.e-learn.cn/content/qita/1781465
/// </summary>
class RandomFlipMatrixSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public RandomFlipMatrixSolution(int n_rows, int n_cols)
    {
        _rows = n_rows;
        _cols = n_cols;
        _total = _rows * _cols;
    }

    private int _rows = 0;
    private int _cols = 0;
    private int _total = 0;
    private Dictionary<int, int> _map = new Dictionary<int, int>();
    private Random _random = new Random();

    public int[] Flip()
    {
        if (_total < 1) return new int[0];
        int r = _random.Next(_total--);
        int x = _map.ContainsKey(r) ? _map[r] : r;
        _map[r] = _map.ContainsKey(_total) ? _map[_total] : _total;
        return new int[]{ x / _cols, x % _cols };
    }

    public void Reset()
    {
        _map.Clear();
        _total = _rows * _cols;
    }
}