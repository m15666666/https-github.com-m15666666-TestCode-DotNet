using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



class FirstBadVersionSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int FirstBadVersion(int n)
    {
        if (n <= 1) return n;

        long first = 1;
        long last = n;

        while (first < last)
        {
            long half = (first + last) / 2;
            Console.WriteLine($"half:{half}");
            if (IsBadVersion((int)half))
            {
                last = half;
            }
            else
            {
                first = half + 1;
            }
        }
        return (int)last;
    }

    private bool IsBadVersion(int n)
    {
        return true;
    }
}