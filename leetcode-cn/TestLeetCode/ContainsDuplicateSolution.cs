using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class ContainsDuplicateSolution
{
    public void Test()
    {
        int[] nums = new int[] { 1, 2, 3, 1 };

        var assert = ContainsDuplicate((int[])nums.Clone()) == true;

        Console.WriteLine($"ContainsDuplicateSolution, assert:{assert}");
    }

    /// <summary>
    /// 如果任何值在数组中出现至少两次，函数返回 true。如果数组中每个元素都不相同，则返回 false。
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public bool ContainsDuplicate(int[] nums)
    {
        if (nums == null || nums.Length == 1) return false;

        //Dictionary<int, int> d = new Dictionary<int, int>();
        //d.ContainsKey()

        HashSet<int> set = new HashSet<int>();
        foreach (var v in nums)
        {
            if (set.Contains(v)) return true;
            set.Add(v);
        }
        return false;
    }
}
