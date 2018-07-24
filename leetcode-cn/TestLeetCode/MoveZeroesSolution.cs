using System;
using System.Collections.Generic;
using System.Text;


class MoveZeroesSolution
{
    public void Test()
    {
        int[] nums = new int[] { 0, 1, 0, 3, 12 };
        MoveZeroes(nums);
    }

    public void MoveZeroes(int[] nums)
    {
        int zeroIndex = -1;

        for (int index = 0; index < nums.Length; index++)
        {
            var v = nums[index];
            if (v == 0)
            {
                if (zeroIndex == -1)
                {
                    zeroIndex = index;
                }
            }
            else
            {
                if (zeroIndex != -1)
                {
                    nums[zeroIndex] = v;
                    nums[index] = 0;
                    zeroIndex++;
                }
            }
        }
    }
}