using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// https://leetcode-cn.com/problems/spiral-matrix/
/// 54. 螺旋矩阵
/// 给定一个包含 m x n 个元素的矩阵（m 行, n 列），请按照顺时针螺旋顺序，返回矩阵中的所有元素。
/// </summary>
class SpiralOrderSolution
{
    public static void Test()
    {
        int[,] a = new int[2, 1] { { 3},{ 2} };

        SpiralOrder(a);
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    public static IList<int> SpiralOrder(int[,] matrix)
    {
        if (matrix == null || matrix.Length == 0) return new List<int>();

        Direction direction = Direction.Right;
        int upIndexBorder = 0;
        int downIndexBorder = matrix.GetUpperBound(0);
        int leftIndexBorder = 0;
        int rightIndexBorder = matrix.GetUpperBound(1);
        int rowIndex = 0, columnIndex = 0;
        int count = 0;

        List<int> ret = new List<int>();
        while( count < matrix.Length )
        {
            ret.Add(matrix[rowIndex, columnIndex]);
            count++;

            switch ( direction)
            {
                case Direction.Right:
                    if( rightIndexBorder == columnIndex )
                    {
                        //columnIndex = rightIndexBorder;
                        direction = Direction.Down;
                        rowIndex++;
                        upIndexBorder++;
                    }
                    else columnIndex++;
                    break;

                case Direction.Down:
                    if( downIndexBorder == rowIndex )
                    {
                        //rowIndex = downIndexBorder;
                        direction = Direction.Left;
                        columnIndex--;
                        rightIndexBorder--;
                    }
                    else rowIndex++;
                    break;

                case Direction.Left:
                    if(columnIndex == leftIndexBorder)
                    {
                        //columnIndex = leftIndexBorder;
                        direction = Direction.Up;
                        rowIndex--;
                        downIndexBorder--;
                    }
                    else columnIndex--;
                    break;

                case Direction.Up:
                    if(rowIndex == upIndexBorder)
                    {
                        //rowIndex = upIndexBorder;
                        direction = Direction.Right;
                        columnIndex++;
                        leftIndexBorder++;
                    }
                    else rowIndex--;
                    break;
            }
        }

        return ret;
    }
    
    private enum Direction
    {
        Right = 0,
        Down = 1,
        Left = 2,
        Up = 3
    }
}