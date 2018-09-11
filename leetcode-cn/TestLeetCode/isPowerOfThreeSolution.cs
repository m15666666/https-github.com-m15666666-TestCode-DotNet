using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// 给定一个整数，写一个函数来判断它是否是 3 的幂次方。
/// </summary>
class IsPowerOfThreeSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    public bool IsPowerOfThree(int n)
    {
        if (n == 1) return true;
        if (n < 3) return false;
        switch (n % 10)
        {
            case 3:
            case 9:
            case 7:
            case 1:
                break;
            default:
                return false;
        }
        if (n == 3 || n == 9 || n == 27 || n == 81) return true;
        long current = 81 * 3;
        while (true)
        {
            if (current == n) return true;
            if (n < current) return false;
            current *= 3;
        }
        return false;


        //if (n == 1) return true;
        //if (n < 3) return false;

        //switch (n % 10)
        //{
        //    case 3:
        //    case 9:
        //    case 7:
        //    case 1:
        //        break;
        //    default:
        //        return false;
        //}
        //if (n % 7 == 0) return false;
        //if (n %11 == 0) return false;
        //if (n % 13 == 0) return false;
        //if (n % 17 == 0) return false;
        //if (n % 19 == 0) return false;
        ////if (n % 7 == 0) return false;

        //if (81 < n && n % 81 == 0) return true;
        //return n == 3 || n == 9 || n == 27 || n == 81;
    }
}