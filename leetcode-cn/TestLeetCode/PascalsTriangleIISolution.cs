using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个非负索引 k，其中 k ≤ 33，返回杨辉三角的第 k 行。



在杨辉三角中，每个数是它左上方和右上方的数的和。

示例:

输入: 3
输出: [1,3,3,1]
进阶：

你可以优化你的算法到 O(k) 空间复杂度吗？

*/
/// <summary>
/// https://leetcode-cn.com/problems/pascals-triangle-ii/
/// 119. 杨辉三角 II
/// 
/// 
/// 
/// </summary>
class PascalsTriangleIISolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public IList<int> GetRow(int rowIndex) {
        int rowCount = rowIndex + 1;
        int[] ret = new int[rowCount];
        ret[0] = 1;
        int preColumnCount = 1;
        for( int row = 1; row < rowCount; row++)
        {
            int prev_1 = 1;
            int prev;
            for (int col = 1; col < preColumnCount; col++)
            {
                prev = ret[col];
                ret[col] = prev_1  + prev;
                prev_1 = prev;
            }
            ret[preColumnCount] = 1;
            preColumnCount++;
        }

        return ret;
    }

}
/*
public class Solution {
    public IList<int> GetRow(int rowIndex) {
        IList<IList<int>> listAll = new List<IList<int>>();
		Test(listAll, rowIndex);
		return listAll[rowIndex];
    }
    public void Test(IList<IList<int>> listAll,int indexRow)
		{
			for (int i = 1; i <= indexRow+1; i++)
			{
				//当n>2时
				List<int> list = new List<int>();
				list.Add(1);
				//迭代一下
				for (int j = 0; j < i - 1; j++)
				{
					if (i - 2 == j) list.Add(1);
					else
					{
						//n的第二个值
						list.Add(listAll[i - 2][j] + listAll[i - 2][j + 1]);
					}
				}
				listAll.Add(list);
			}
		}
}

public class Solution {
    public IList<int> GetRow(int rowIndex) {
        int[] res = new int[++rowIndex]; 
        for (int i = 0; i < rowIndex; i++) {
            res[i] = 1;
            for (int j = i - 1; j > 0; j--)
                res[j] += res[j - 1];
        }
        return res.ToList();
    }
}

public class Solution {
    public IList<int> GetRow(int rowIndex) {
        IList<int> x=new List<int>();
        double c;
        for(int i=0,j,c1,c2;i<=rowIndex;i++){
            for(c=1,j=i;j>0;j--){
                c=c*(rowIndex-i+j)/j;
            }
            x.Add((int)Math.Round(c));
        }
        return x;
    }
}

public class Solution {
    public IList<int> GetRow(int rowIndex) {
        int numRows = 34;
        int[][] dp =new int[numRows][];
        for(int i = 0;i<numRows;i++)
        {
            dp[i] = new int[i+1];
            for(int j = 0;j<=i;j++)
            {
                if(j==0 || j ==i)
                {
                    dp[i][j] = 1;
                }
                else
                {
                    dp[i][j] = dp[i-1][j-1] + dp[i-1][j];
                }
            }
        }

        return dp[rowIndex].ToList();
    }
}


 
 
*/
