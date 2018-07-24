using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class RotateArraySolution
{
    public void Test()
    {
        int[] nums = new int[] {1, 2, 3, 4, 5, 6, 7};
        int k = 4;
        Rotate_Method1((int[])nums.Clone(), k);

        Rotate_Method0((int[])nums.Clone(), k);
    }

    private void Rotate_Method0(int[] nums, int k)
    {
        if (nums == null || nums.Length == 1 || k < 1) return;

        k = k % nums.Length;
        if (k == 0) return;
        bool isMoveRight = k < (nums.Length - k);

        //WriteLine($"Rotate_Method0, k:{k}");
        //WriteLine(string.Join(",", nums.Select(v => v.ToString())));
        if (isMoveRight)
        {
            //WriteLine($"isMoveRight:{isMoveRight}");
            for (int index0 = 0; index0 < k; index0++)
            {
                int lastV = nums[nums.Length - 1];
                for (int index = nums.Length - 2; -1 < index; index--)
                {
                    nums[index + 1] = nums[index];
                    //WriteLine(string.Join(",", nums.Select(v => v.ToString())));
                }
                nums[0] = lastV;
                //WriteLine(string.Join(",", nums.Select(v => v.ToString())));
            }
        }
        else
        {
            int toLeft = nums.Length - k;
            for (int index0 = 0; index0 < toLeft; index0++)
            {
                int firstV = nums[0];
                for (int index = 1; index < nums.Length; index++)
                {
                    nums[index - 1] = nums[index];
                }
                nums[nums.Length - 1] = firstV;
                //WriteLine(string.Join(",", nums.Select(v => v.ToString())));
            }
        }
        WriteLine(string.Join(",", nums.Select(v => v.ToString())));
    }

    /// <summary>
    /// 给定一个数组，将数组中的元素向右移动 k 个位置，其中 k 是非负数。
    /// </summary>
    /// <param name="nums"></param>
    /// <param name="k"></param>
    public void Rotate_Method1(int[] nums, int k)
    {
        if (nums == null || nums.Length == 1 || k < 1) return;

        k = k % nums.Length;
        if (k == 0) return;

        int changeCount = 0;

        int lastI = nums.Length - 1;
        int lastV = nums[lastI];

        int index0 = lastI;
        while (changeCount < nums.Length)
        {
            int nextIndex = index0 - k;
            if (nextIndex < 0) nextIndex = nums.Length - 1 + nextIndex + 1;

            if (nextIndex != lastI)
            {
                //WriteLine($"{nextIndex}({nums[nextIndex]}) => {index0}");

                nums[index0] = nums[nextIndex];
                index0 = nextIndex;
            }
            else
            {
                nums[index0] = lastV;

                //WriteLine($"{nextIndex}({lastV}) => {index0}");

                lastI = lastI - 1;
                lastV = nums[lastI];

                index0 = lastI;
            }

            changeCount++;

            //WriteLine($"changeCount:{changeCount}, index0:{index0}");
            //WriteLine(string.Join(",", nums.Select(v => v.ToString())));
        }
    }

    private void WriteLine(string message)
    {
        Console.WriteLine(message);
    }

}