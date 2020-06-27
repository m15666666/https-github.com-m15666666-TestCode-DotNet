using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个非负整数 numRows，生成杨辉三角的前 numRows 行。



在杨辉三角中，每个数是它左上方和右上方的数的和。

示例:

输入: 5
输出:
[
     [1],
    [1,1],
   [1,2,1],
  [1,3,3,1],
 [1,4,6,4,1]
]


*/
/// <summary>
/// https://leetcode-cn.com/problems/pascals-triangle/
/// 118. 杨辉三角
/// 
/// 
/// 
/// </summary>
class PascalsTriangleSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public IList<IList<int>> Generate(int numRows) {
        if (numRows == 0) return new List<IList<int>>();

        int[][] ret = new int[numRows][];
        var num = new int[1] { 1 };
        ret[0] = num;
        for( int row = 1; row < numRows; row++)
        {
            num = new int[row + 1];
            ret[row] = num;

            num[0] = 1;
            num[num.Length - 1] = 1;
            var prevRow = ret[row - 1];
            for (int col = 1; col < prevRow.Length; col++) num[col] = prevRow[col-1] + prevRow[col]; 
        }

        return ret;
    }

}
/*

杨辉三角
力扣 (LeetCode)
发布于 2018-10-25
43.5k
方法：动态规划
思路

如果能够知道一行杨辉三角，我们就可以根据每对相邻的值轻松地计算出它的下一行。

算法

虽然这一算法非常简单，但用于构造杨辉三角的迭代方法可以归类为动态规划，因为我们需要基于前一行来构造每一行。

首先，我们会生成整个 triangle 列表，三角形的每一行都以子列表的形式存储。然后，我们会检查行数为 00 的特殊情况，否则我们会返回 [1][1]。如果 numRows > 0numRows>0，那么我们用 [1][1] 作为第一行来初始化 triangle with [1][1]，并按如下方式继续填充：




class Solution {
    public List<List<Integer>> generate(int numRows) {
        List<List<Integer>> triangle = new ArrayList<List<Integer>>();

        // First base case; if user requests zero rows, they get zero rows.
        if (numRows == 0) {
            return triangle;
        }

        // Second base case; first row is always [1].
        triangle.add(new ArrayList<>());
        triangle.get(0).add(1);

        for (int rowNum = 1; rowNum < numRows; rowNum++) {
            List<Integer> row = new ArrayList<>();
            List<Integer> prevRow = triangle.get(rowNum-1);

            // The first row element is always 1.
            row.add(1);

            // Each triangle element (other than the first and last of each row)
            // is equal to the sum of the elements above-and-to-the-left and
            // above-and-to-the-right.
            for (int j = 1; j < rowNum; j++) {
                row.add(prevRow.get(j-1) + prevRow.get(j));
            }

            // The last row element is always 1.
            row.add(1);

            triangle.add(row);
        }

        return triangle;
    }
}
复杂度分析

时间复杂度：O(numRows^2)O(numRows 
2
 )

虽然更新 triangle 中的每个值都是在常量时间内发生的，
但它会被执行 O(numRows^2)O(numRows 
2
 ) 次。想要了解原因，就需要考虑总共有多少
次循环迭代。很明显外层循环需要运行
numRowsnumRows 次，但在外层循环的每次迭代中，内层
循环要运行 rowNumrowNum 次。因此，triangle 发生的更新总数为
1 + 2 + 3 + \ldots + numRows1+2+3+…+numRows，根据高斯公式
有

\begin{aligned} \frac{numRows(numRows+1)}{2} &= \frac{numRows^2 + numRows}{2} \\ &= \frac{numRows^2}{2} + \frac{numRows}{2} \\ &= O(numRows^2) \end{aligned}
2
numRows(numRows+1)
​	
 
​	
  
= 
2
numRows 
2
 +numRows
​	
 
= 
2
numRows 
2
 
​	
 + 
2
numRows
​	
 
=O(numRows 
2
 )
​	
 

空间复杂度：O(numRows^2)O(numRows 
2
 )

因为我们需要存储我们在 triangle 中更新的每个数字，
所以空间需求与时间复杂度相同。

下一篇：提供一种递归方式的思路


public class Solution {
           public IList<IList<int>> Generate(int numRows)
        {
            IList<IList<int>> result = new List<IList<int>>();
            for (int i = 0; i < numRows; i++)
            {
                var list = new List<int>(i + 1);
                for (int j = 0; j < i + 1; j++)
                {
                    int element;
                    if (i == 0 || j == 0 || j == i)
                    {
                        element = 1;
                    }
                    else
                    {
                        element = result[i - 1][j - 1] + result[i - 1][j];
                    }
                    list.Add(element);
                }
                result.Add(list);
            }
            return result;
        }
}

public class Solution {
    public IList<IList<int>> Generate(int numRows) {
        
        int[][] ans=new int[numRows][];
        
        
        for(int i=0;i<numRows;i++)
        {
            ans[i] =new int[i+1];
            
            for(int j=0;j<=i;j++)
            {
                if(j==0 || j==i)
                {
                    ans[i][j]=1;    
                }
                else{
                    ans[i][j]=ans[i-1][j-1]+ans[i-1][j];
                }
                
                
                
                
            }
            
        }
        
        
        IList<IList<int>> res=new List<IList<int>>();
        
        for(int i=0;i<numRows;i++){
            
            res.Add(ans[i].ToList());
                      
        }
        return res;


    }
}

public class Solution {
    public IList<IList<int>> Generate(int numRows) 
    {
        IList<IList<int>> res = new List<IList<int>>();
        for(int i = 1;i<numRows+1;i++)
        {
            int[] row = new int[i];
            for(int j = 0;j<i;j++)
            {
                if(j==0||j==i-1)
                {
                    row[j]=1;
                }
                else
                {
                    row[j] = res[i-2][j] + res[i-2][j-1];
                }
            }
            res.Add(row.ToList());
        }
        
        return res;
    }
}

 
 
 
 
*/
