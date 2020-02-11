using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定由若干 0 和 1 组成的矩阵 matrix，从中选出任意数量的列并翻转其上的 每个 单元格。
翻转后，单元格的值从 0 变成 1，或者从 1 变为 0 。

返回经过一些翻转后，行上所有值都相等的最大行数。
 

示例 1：

输入：[[0,1],[1,1]]
输出：1
解释：不进行翻转，有 1 行所有值都相等。
示例 2：

输入：[[0,1],[1,0]]
输出：2
解释：翻转第一列的值之后，这两行都由相等的值组成。
示例 3：

输入：[[0,0,0],[0,0,1],[1,1,0]]
输出：2
解释：翻转前两列的值之后，后两行由相等的值组成。
 

提示：

1 <= matrix.length <= 300
1 <= matrix[i].length <= 300
所有 matrix[i].length 都相等
matrix[i][j] 为 0 或 1
*/
/// <summary>
/// https://leetcode-cn.com/problems/flip-columns-for-maximum-number-of-equal-rows/
/// 1072. 按列翻转得到最大值等行数
/// 
/// </summary>
class FlipColumnsForMaximumNumberOfEqualRowsSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int MaxEqualRowsAfterFlips(int[][] matrix)
    {
        int column = matrix[0].Length;
        byte[] buffer = new byte[column / 8 + 1];
        var bitArray = new System.Collections.BitArray(column);
        var bits2Count = new Dictionary<string, int>();
        foreach( var row in matrix)
        {
            int index = 0;
            bool reverse = row[0] == 0;
            if (reverse)
                foreach( var bit in row)
                    bitArray.Set(index++, bit == 0);
            else
                foreach (var bit in row)
                    bitArray.Set(index++, bit == 1);

            bitArray.CopyTo(buffer, 0);
            var bitString = BitConverter.ToString(buffer);
            if (!bits2Count.ContainsKey(bitString)) bits2Count.Add(bitString, 1);
            else bits2Count[bitString]++;
        }
        return bits2Count.Values.Max();
    }
}
/*
C++ 简单题解
大力王
发布于 3 个月前
270 阅读
行转成字符串，转的时候，如果行以0开头，则将行中所有数取反，如果是1开头，保持不变
然后看有多少个相同的字符串，选择其中个数最大的

class Solution {
public:
    int maxEqualRowsAfterFlips(vector<vector<int>>& matrix) {
        unordered_map<string, int> m;
        int res = 0;
        for (auto& r : matrix) {
            string s;
            int d = 1 ^ r[0];
            for (auto x : r) {
                s += (d ^ x) + '0';
            }
            ++m[s];
            res = max(res, m[s]);
        }
        return res;
    }
}; 
*/
