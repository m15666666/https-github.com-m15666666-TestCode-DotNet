using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// https://leetcode-cn.com/problems/word-search/
/// 79.单词搜索
/// 给定一个二维网格和一个单词，找出该单词是否存在于网格中。
/// 单词必须按照字母顺序，通过相邻的单元格内的字母构成，
/// 其中“相邻”单元格是那些水平相邻或垂直相邻的单元格。同一个单元格内的字母不允许被重复使用。
/// </summary>
class WordsExistSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    public bool Exist(char[,] board, string word)
    {
        if (string.IsNullOrWhiteSpace(word)) return true;
        if (board == null || board.Length == 0) return false;

        HashSet<string> path = new HashSet<string>();
        int m = board.GetLength(0);
        int n = board.GetLength(1);
        var firstChar = word[0];
        for (var row = 0; row < m; row++)
            for (var column = 0; column < n; column++)
                if (board[row, column] == firstChar)
                {
                    var position = $"{row}-{column}";
                    path.Add(position);
                    if (BackTrade(board, m, n, word, row, column, 1, path)) return true;
                    path.Remove(position);
                }
        return false;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="board"></param>
    /// <param name="word"></param>
    /// <param name="startRow"></param>
    /// <param name="startColumn"></param>
    /// <param name="wordIndex"></param>
    /// <param name="path"></param>
    /// <returns>true: find complete word</returns>
    private bool BackTrade(char[,] board, int m, int n, string word, int startRow, int startColumn, int wordIndex, HashSet<string> path)
    {
        if (word.Length <= wordIndex) return true;

        int row, column;
        string position;

        // up
        row = startRow - 1;
        if ( -1 < row )
        {
            column = startColumn;
            if (word[wordIndex] == board[row, column] && !path.Contains((position = $"{row}-{column}")))
            {
                path.Add(position);
                if( BackTrade( board, m, n, word, row, column, wordIndex + 1, path) ) return true;
                path.Remove(position);
            }
        }

        // down
        row = startRow + 1;
        if (row < m)
        {
            column = startColumn;
            if (word[wordIndex] == board[row, column] && !path.Contains((position = $"{row}-{column}")))
            {
                path.Add(position);
                if (BackTrade(board, m, n, word, row, column, wordIndex + 1, path)) return true;
                path.Remove(position);
            }
        }

        // left
        column = startColumn - 1;
        if (-1 < column)
        {
            row = startRow;
            if (word[wordIndex] == board[row, column] && !path.Contains((position = $"{row}-{column}")))
            {
                path.Add(position);
                if (BackTrade(board, m, n, word, row, column, wordIndex + 1, path)) return true;
                path.Remove(position);
            }
        }

        // right
        column = startColumn + 1;
        if ( column < n )
        {
            row = startRow;
            if (word[wordIndex] == board[row, column] && !path.Contains((position = $"{row}-{column}")))
            {
                path.Add(position);
                if (BackTrade(board, m, n, word, row, column, wordIndex + 1, path)) return true;
                path.Remove(position);
            }
        }

        return false;
    }

}