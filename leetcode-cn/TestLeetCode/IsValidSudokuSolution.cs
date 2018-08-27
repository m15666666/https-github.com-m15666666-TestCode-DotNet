using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class IsValidSudokuSolution
{
    public void Test()
    {
        int[] nums = new int[] {1, 2, 3, 4, 5, 6, 7};
        int k = 4;
     //   Rotate_Method1((int[])nums.Clone(), k);

       // Rotate_Method0((int[])nums.Clone(), k);
    }

    public bool IsValidSudoku(char[,] board)
    {
        HashSet<char> set = new HashSet<char>();
        for (int r = 0; r < 9; r++)
        {
            for (int c = 0; c < 9; c++)
            {
                var v = board[r, c];
                if( v == '.') continue;

                if (set.Contains(v)) return false;
                set.Add(v);
            }
            set.Clear();
        }

        for (int c = 0; c < 9; c++)
        {
            for (int r = 0; r < 9; r++)
            {
                var v = board[r, c];
                if (v == '.') continue;

                if (set.Contains(v)) return false;
                set.Add(v);
            }
            set.Clear();
        }

        for (int r = 0; r < 9; r += 3)
        {
            for (int c = 0; c < 9; c += 3)
            {
                for (int r1 = 0; r1 < 3; r1++)
                {
                    for (int c1 = 0; c1 < 3; c1++)
                    {
                        var v = board[r+r1, c+c1];
                        if (v == '.') continue;

                        if (set.Contains(v)) return false;
                        set.Add(v);
                    }
                }
                set.Clear();
            }
        }

        return true;
    }

}