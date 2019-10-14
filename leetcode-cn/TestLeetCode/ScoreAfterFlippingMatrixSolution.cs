using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
有一个二维矩阵 A 其中每个元素的值为 0 或 1 。

移动是指选择任一行或列，并转换该行或列中的每一个值：将所有 0 都更改为 1，将所有 1 都更改为 0。

在做出任意次数的移动后，将该矩阵的每一行都按照二进制数来解释，矩阵的得分就是这些数字的总和。

返回尽可能高的分数。

 

示例：

输入：[[0,0,1,1],[1,0,1,0],[1,1,0,0]]
输出：39
解释：
转换为 [[1,1,1,1],[1,0,0,1],[1,1,1,1]]
0b1111 + 0b1001 + 0b1111 = 15 + 9 + 15 = 39
 

提示：

1 <= A.length <= 20
1 <= A[0].length <= 20
A[i][j] 是 0 或 1
在真实的面试中遇到过这道题？
*/
/// <summary>
/// https://leetcode-cn.com/problems/score-after-flipping-matrix/
/// 861. 翻转矩阵后的得分
/// 
/// </summary>
class ScoreAfterFlippingMatrixSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int MatrixScore(int[][] A)
    {
        int rowCount = A.Length;
        int columnCount = A[0].Length;
        int columnValue = (1 << (columnCount - 1)); // 初始值设为最高列 == Math.Pow(2, columnCount - 1)
        int ret = rowCount * columnValue; // 第一列全转换为1，sum初始值
        
        for (int r = 0; r < rowCount; ++r) 
            A[r][0] = A[r][0] == 0 ? 1 : 0; // 用于1到columnCount-1列是否翻转

        // 按列加
        for (int c = 1; c < columnCount; ++c)
        {
            columnValue >>= 1;
            int ones = 0; // 1的数量
            for (int r = 0; r < rowCount; ++r)
                ones += A[r][c] ^ A[r][0];

            ret += Math.Max(ones, rowCount - ones) * columnValue;// (1 << (columnCount - 1 - c));
        }
        return ret;
    }
}
/*
对于二进制数来说，我们只要保证最高位是1，就可以保证这个数是最大的，因为移动操作会使得它取反，因此我们进行行变化的时候只需要考虑首位即可。
对于后面的列处理，由于只影响的是该列，所以若要取得最大值，只需要保证该列1的个数 不少于0的个数即可。但是我们在进行列判断的时候，可以简化移动操作，因为这个过程我们会进行记录1的个数的计算，所以我们可以直接由1个个数去计算最后的总和即可。

class Solution {
public:
    int matrixScore(vector<vector<int>>& A) {
        if(A.empty()) return 0;
        int rowCount = A.size();//行
        int columnCount = A[0].size();//列
        //行移动
        for(int i=0; i<rowCount; i++) {
            if(A[i][0] == 0) {//行翻转条件
                int index = 0;
                while(index<columnCount) {
                    A[i][index] ^= 1;
                    index++;
                }
            }
        }
        
        int sum = rowCount * pow(2, columnCount-1);//第一列全为1，sum初始值、
        for(int i=1; i<columnCount; i++) {//按列计算
            int count = 0;
            for(int j=0; j<rowCount; j++) {
                if(A[j][i] == 1)//统计1的个数
                    count++;
            }
            if(count <= rowCount/2) {//列反转条件,不用进行翻转操作，直接计算
                count = rowCount - count;
            }
            sum += count * pow(2, columnCount - i -1);
        }
        
        return sum;
    }
}; 

public class Solution {
    public int MatrixScore(int[][] A) {
        int row = A.Length;
        int col = A[0].Length;
        
        for (int i = 0; i < row; i ++) {
            if (A[i][0] == 0) {
                for (int j = 0; j < col; j ++) {
                    A[i][j] = 1 - A[i][j];
                }
            }
        }
        
        int res = 0;
        for (int j = 0; j < col; j ++) {
            int count = 0;
            for (int i = 0; i < row; i ++) {
                if (A[i][j] == 1) {
                    count ++;
                }
            }
            
            count = Math.Max(count, row - count);
            
            res = res * 2 + count;
        }
        
        return res;
    }
}
*/
