using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// https://leetcode-cn.com/problems/surrounded-regions/
/// 130.被围绕的区域
/// 给定一个二维的矩阵，包含 'X' 和 'O'（字母 O）。
/// 找到所有被 'X' 围绕的区域，并将这些区域里所有的 'O' 用 'X' 填充。
/// 示例:
/// X X X X
/// X O O X
/// X X O X
/// X O X X
/// </summary>
class SolveSurroundedRegionsSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    public void Solve(char[,] board)
    {
        if (board == null || board.Length < 9) return;

        const char X = 'X';
        const char O = 'O';
        const char smallO = 'o';

        var m = board.GetLength(0);
        var n = board.GetLength(1);

        int left = 0;
        int right = n - 1;
        int top = 0;
        int bottom = m - 1;

        Queue<Position2D> queue = new Queue<Position2D>();

        while( left < right - 1 && top < bottom - 1 )
        {
            {
                int rowIndex = top + 1;
                for (int columnIndex = left + 1; columnIndex < right; columnIndex++)
                {
                    var v = board[rowIndex, columnIndex];
                    if (v == O)
                    {
                        if (board[rowIndex - 1, columnIndex] == O || (board[rowIndex, columnIndex - 1] == O) || (columnIndex == right - 1 && board[rowIndex, right] == O))
                        {
                            // do nothing
                            continue;
                        }
                        board[rowIndex, columnIndex] = smallO;
                        queue.Enqueue(new Position2D { X = rowIndex, Y = columnIndex });
                    }
                }
                top++;
                if (!(left < right - 1 && top < bottom - 1)) break;
            }

            {
                int rowIndex = bottom - 1;
                for (int columnIndex = left + 1; columnIndex < right; columnIndex++)
                {
                    var v = board[rowIndex, columnIndex];
                    if (v == O)
                    {
                        if (board[rowIndex + 1, columnIndex] == O || (board[rowIndex, columnIndex - 1] == O) || (columnIndex == right - 1 && board[rowIndex, right] == O))
                        {
                            // do nothing
                            continue;
                        }
                        board[rowIndex, columnIndex] = smallO;
                        queue.Enqueue(new Position2D { X = rowIndex, Y = columnIndex });
                    }
                }
                bottom--;
                if (!(left < right - 1 && top < bottom - 1)) break;
            }

            {
                int columnIndex = left + 1;
                for (int rowIndex = top + 1; rowIndex < bottom; rowIndex++)
                {
                    var v = board[rowIndex, columnIndex];
                    if (v == O)
                    {
                        if (board[rowIndex, columnIndex - 1] == O || (board[rowIndex - 1,columnIndex] == O) || (rowIndex == bottom - 1 && board[bottom,columnIndex] == O))
                        {
                            // do nothing
                            continue;
                        }
                        board[rowIndex, columnIndex] = smallO;
                        queue.Enqueue(new Position2D { X = rowIndex, Y = columnIndex });
                    }
                }
                left++;
                if (!(left < right - 1 && top < bottom - 1)) break;
            }

            {
                int columnIndex = right - 1;
                for (int rowIndex = top + 1; rowIndex < bottom; rowIndex++)
                {
                    var v = board[rowIndex, columnIndex];
                    if (v == O)
                    {
                        if (board[rowIndex, columnIndex + 1] == O || (board[rowIndex - 1, columnIndex] == O) || (rowIndex == bottom - 1 && board[bottom, columnIndex] == O))
                        {
                            // do nothing
                            continue;
                        }
                        board[rowIndex, columnIndex] = smallO;
                        queue.Enqueue(new Position2D { X = rowIndex, Y = columnIndex });
                    }
                }
                right--;
            }
        } // while( left < right - 1 && top < bottom - 1 )
        if (queue.Count == 0) return;

        int maxCount = queue.Count;
        int iterateCount = 0;
        while( iterateCount < maxCount )
        {
            var a = queue.Dequeue();
            var i = a.X;
            var j = a.Y;
            var leftV = board[i, j - 1];
            var rightV = board[i, j + 1];
            var topV = board[i - 1, j];
            var bottomV = board[i + 1, j];
            if (leftV == O ||  rightV == O || topV == O || bottomV  == O )
            {
                board[i, j] = O;

                iterateCount = 0;
                maxCount--;
                continue;
            }

            if (leftV == X && rightV == X && topV == X && bottomV == X)
            {
                board[i, j] = X;

                iterateCount = 0;
                maxCount--;
                continue;
            }

            //if (leftV == smallO || rightV == smallO || topV == smallO || bottomV == smallO )
            //{
                queue.Enqueue(a);

                iterateCount++;

                if (iterateCount == maxCount) break;
            //}
        } // while( iterateCount < maxCount )

        foreach ( var a in queue)
        {
            board[a.X, a.Y] = X;
        }
    }

    public class Position2D
    {
        public int X;
        public int Y;
    }
}