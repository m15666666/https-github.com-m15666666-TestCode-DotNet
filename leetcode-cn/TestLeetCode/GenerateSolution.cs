using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// 帕斯卡三角形
/// </summary>
class GenerateSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    public IList<IList<int>> Generate(int numRows)
    {
        List<IList<int>> ret = new List<IList<int>>();

        if (numRows == 0) return ret;

        List<int> parent = new List<int>() { 1 };
        ret.Add(parent);

        if (numRows == 1) return ret;

        parent = new List<int>() { 1, 1 };
        ret.Add(parent);

        if (numRows == 2) return ret;

        while (true)
        {
            int nextCount = parent.Count + 1;
            List<int> next = new List<int>() { 1 };
            ret.Add(next);

            int index = -1;
            int lastValue = 1;
            foreach (var v in parent)
            {
                index++;
                if (0 < index)
                {
                    next.Add(lastValue + v);
                    lastValue = v;
                }
            }

            next.Add(1);
            if (next.Count == numRows) break;
            parent = next;
        }

        return ret;
    }
}