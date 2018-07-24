using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


class AddOneSolution
{
    public void Test()
    {
        int[] nums = new int[] { 1, 2, 3 };

        var ret = PlusOne((int[])nums.Clone());

        Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int[] PlusOne(int[] digits)
    {
        List<int> ret = new List<int>();

        int remain = 1;
        for (int index = digits.Length - 1; -1 < index; index--)
        {
            var value = digits[index] + remain;

            remain = 0;
            if (9 < value)
            {
                remain = 1;
                value -= 10;
            }

            ret.Insert(0, value);
        }
        if(remain == 1) ret.Insert(0, 1);

        return ret.ToArray();
    }
}
