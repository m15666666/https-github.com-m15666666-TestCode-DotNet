using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个二维网格和一个单词，找出该单词是否存在于网格中。

单词必须按照字母顺序，通过相邻的单元格内的字母构成，其中“相邻”单元格是那些水平相邻或垂直相邻的单元格。同一个单元格内的字母不允许被重复使用。

 

示例:

board =
[
  ['A','B','C','E'],
  ['S','F','C','S'],
  ['A','D','E','E']
]

给定 word = "ABCCED", 返回 true
给定 word = "SEE", 返回 true
给定 word = "ABCB", 返回 false
 

提示：

board 和 word 中只包含大写和小写英文字母。
1 <= board.length <= 200
1 <= board[i].length <= 200
1 <= word.length <= 10^3
     
*/
/// <summary>
/// https://leetcode-cn.com/problems/word-search/
/// 79.单词搜索
/// 
/// 
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


    public bool Exist(char[][] board, string word) {

        const int Width = 200;
        if (string.IsNullOrWhiteSpace(word)) return true;
        if (board == null || board.Length == 0) return false;

        var path = new HashSet<int>();
        int m = board.Length;
        int n = board[0].Length;
        var firstChar = word[0];
        for (var row = 0; row < m; row++)
            for (var column = 0; column < n; column++)
                if (board[row][column] == firstChar)
                {
                    var position = row * Width + column;
                    path.Add(position);
                    if (BackTrack(row, column, 1)) return true;
                    path.Remove(position);
                }
        return false;

        bool BackTrack(int startRow, int startColumn, int wordIndex)
        {
            if (word.Length <= wordIndex) return true;

            int row, column;
            // up
            row = startRow - 1;
            if (-1 < row)
            {
                column = startColumn;
                if (Check(row, column, wordIndex)) return true;
            }

            // down
            row = startRow + 1;
            if (row < m)
            {
                column = startColumn;
                if (Check(row, column, wordIndex)) return true;
            }

            // left
            column = startColumn - 1;
            if (-1 < column)
            {
                row = startRow;
                if (Check(row, column, wordIndex)) return true;
            }

            // right
            column = startColumn + 1;
            if (column < n)
            {
                row = startRow;
                if (Check(row, column, wordIndex)) return true;
            }

            return false;
        }
        bool Check( int row, int column, int wordIndex)
        {
            int position = row * Width + column;
            if (word[wordIndex] == board[row][column] && !path.Contains(position))
            {
                path.Add(position);
                if (BackTrack(row, column, wordIndex + 1)) return true;
                path.Remove(position);
            }
            return false;
        }
    }
    
    //public bool Exist(char[,] board, string word)
    //{
    //    if (string.IsNullOrWhiteSpace(word)) return true;
    //    if (board == null || board.Length == 0) return false;

    //    HashSet<string> path = new HashSet<string>();
    //    int m = board.GetLength(0);
    //    int n = board.GetLength(1);
    //    var firstChar = word[0];
    //    for (var row = 0; row < m; row++)
    //        for (var column = 0; column < n; column++)
    //            if (board[row, column] == firstChar)
    //            {
    //                var position = $"{row}-{column}";
    //                path.Add(position);
    //                if (BackTrack(board, m, n, word, row, column, 1, path)) return true;
    //                path.Remove(position);
    //            }
    //    return false;
    //}

    ///// <summary>
    ///// 
    ///// </summary>
    ///// <param name="board"></param>
    ///// <param name="word"></param>
    ///// <param name="startRow"></param>
    ///// <param name="startColumn"></param>
    ///// <param name="wordIndex"></param>
    ///// <param name="path"></param>
    ///// <returns>true: find complete word</returns>
    //private bool BackTrack(char[,] board, int m, int n, string word, int startRow, int startColumn, int wordIndex, HashSet<string> path)
    //{
    //    if (word.Length <= wordIndex) return true;

    //    int row, column;
    //    string position;

    //    // up
    //    row = startRow - 1;
    //    if ( -1 < row )
    //    {
    //        column = startColumn;
    //        if (word[wordIndex] == board[row, column] && !path.Contains((position = $"{row}-{column}")))
    //        {
    //            path.Add(position);
    //            if( BackTrack( board, m, n, word, row, column, wordIndex + 1, path) ) return true;
    //            path.Remove(position);
    //        }
    //    }

    //    // down
    //    row = startRow + 1;
    //    if (row < m)
    //    {
    //        column = startColumn;
    //        if (word[wordIndex] == board[row, column] && !path.Contains((position = $"{row}-{column}")))
    //        {
    //            path.Add(position);
    //            if (BackTrack(board, m, n, word, row, column, wordIndex + 1, path)) return true;
    //            path.Remove(position);
    //        }
    //    }

    //    // left
    //    column = startColumn - 1;
    //    if (-1 < column)
    //    {
    //        row = startRow;
    //        if (word[wordIndex] == board[row, column] && !path.Contains((position = $"{row}-{column}")))
    //        {
    //            path.Add(position);
    //            if (BackTrack(board, m, n, word, row, column, wordIndex + 1, path)) return true;
    //            path.Remove(position);
    //        }
    //    }

    //    // right
    //    column = startColumn + 1;
    //    if ( column < n )
    //    {
    //        row = startRow;
    //        if (word[wordIndex] == board[row, column] && !path.Contains((position = $"{row}-{column}")))
    //        {
    //            path.Add(position);
    //            if (BackTrack(board, m, n, word, row, column, wordIndex + 1, path)) return true;
    //            path.Remove(position);
    //        }
    //    }

    //    return false;
    //}

}
/*

在二维平面上使用回溯法（Python 代码、Java 代码）
liweiwei1419
发布于 1 年前
38.1k
这是一个使用回溯算法解决的问题，涉及的知识点有 DFS 和状态重置。



参考代码：

public class Solution {

    private boolean[][] marked;

    //        x-1,y
    // x,y-1  x,y    x,y+1
    //        x+1,y
    private int[][] direction = {{-1, 0}, {0, -1}, {0, 1}, {1, 0}};
    // 盘面上有多少行
    private int m;
    // 盘面上有多少列
    private int n;
    private String word;
    private char[][] board;

    public boolean exist(char[][] board, String word) {
        m = board.length;
        if (m == 0) {
            return false;
        }
        n = board[0].length;
        marked = new boolean[m][n];
        this.word = word;
        this.board = board;

        for (int i = 0; i < m; i++) {
            for (int j = 0; j < n; j++) {
                if (dfs(i, j, 0)) {
                    return true;
                }
            }
        }
        return false;
    }

    private boolean dfs(int i, int j, int start) {
        if (start == word.length() - 1) {
            return board[i][j] == word.charAt(start);
        }
        if (board[i][j] == word.charAt(start)) {
            marked[i][j] = true;
            for (int k = 0; k < 4; k++) {
                int newX = i + direction[k][0];
                int newY = j + direction[k][1];
                if (inArea(newX, newY) && !marked[newX][newY]) {
                    if (dfs(newX, newY, start + 1)) {
                        return true;
                    }
                }
            }
            marked[i][j] = false;
        }
        return false;
    }

    private boolean inArea(int x, int y) {
        return x >= 0 && x < m && y >= 0 && y < n;
    }

    public static void main(String[] args) {

//        char[][] board =
//                {
//                        {'A', 'B', 'C', 'E'},
//                        {'S', 'F', 'C', 'S'},
//                        {'A', 'D', 'E', 'E'}
//                };
//
//        String word = "ABCCED";


        char[][] board = {{'a', 'b'}};
        String word = "ba";
        Solution solution = new Solution();
        boolean exist = solution.exist(board, word);
        System.out.println(exist);
    }
}
说明：

1、偏移量数组在二维平面内是经常使用的，可以把它的设置当做一个技巧，并且在这个问题中，偏移量数组内的 4 个偏移的顺序无关紧要；

说明：类似使用这个技巧的问题还有：「力扣」第 130 题：被围绕的区域、「力扣」第 200 题：岛屿数量。

2、对于这种搜索算法，我认为理解 DFS 和状态重置并不难，代码编写也相对固定，难在代码的编写和细节的处理，建议多次编写，自己多总结多思考，把自己遇到的坑记下。

我自己在写

for i in range(m):
    for j in range(n):
        # 对每一个格子都从头开始搜索
        if self.__search_word(board, word, 0, i, j, marked, m, n):
            return True
这一段的时候，就傻乎乎地写成了：

# 这一段代码是错误的，不要模仿
for i in range(m):
    for j in range(n):
        # 对每一个格子都从头开始搜索
        return self.__search_word(board, word, 0, i, j, marked, m, n)
这样其实就变成只从坐标 (0,0) 开始搜索，搜索不到返回 False，但题目的意思是：只要你的搜索返回 True 才返回，如果全部的格子都搜索完了以后，都返回 False ，才返回 False。

下一篇：深度优先搜索与回溯详解

public class Solution {
    public bool Exist(char[][] board, string word) {
        for (int y = 0; y < board.Length; y++)
        {
            for (int x = 0; x < board[0].Length; x++)
            {
                if (Exist(board, x, y, word, 0))
                {
                    return true;
                }
            }
        }

        return false;
    }

    public bool Exist(char[][] board, int x, int y, string word, int wordIndex)
    {
        if (y < 0 || y >= board.Length || x < 0 || x >= board[0].Length) return false;

        var currentChar = word[wordIndex];
        if (board[y][x] != currentChar) return false;

        if (wordIndex == word.Length - 1) return true;

        board[y][x] = '-';
        var result = Exist(board, x - 1, y, word, wordIndex + 1) ||
                        Exist(board, x + 1, y, word, wordIndex + 1) ||
                        Exist(board, x, y - 1, word, wordIndex + 1) ||
                        Exist(board, x, y + 1, word, wordIndex + 1);
        board[y][x] = currentChar;

        return result;
    }
}

public class Solution {
    public bool Exist(char[][] board, string word) {
        for (var i = 0; i < board.Length; ++i) {
            for (var j = 0; j < board[i].Length; ++j) {
                if (board[i][j] == word[0]) {
                    if (TestWord(board, i, j, word, 0)) { return true; }
                }
            }
        }
        return false;
    }
    
    private bool TestWord(char[][] board, int i, int j, string word, int first) {
        if (first >= word.Length) {
            return true;
        }
        if (i < 0 || i >= board.Length || j < 0 || j >= board[0].Length) { return false; }
        if (board[i][j] != word[first]) {
            return false;
        }
        
        var diff = new int[] { -1, 1 };
        foreach (var d in diff) {
            var c = board[i][j];
            board[i][j] = ' ';
            if (TestWord(board, i+d, j, word, first+1)) {
                return true;
            }
            if (TestWord(board, i, j+d, word, first+1)) {
                return true;
            }
            board[i][j] = c;
        }
        return false;
    }
} 
     
     
*/