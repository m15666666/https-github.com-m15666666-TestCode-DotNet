using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



class MergeArraySolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public void Merge(int[] nums1, int m, int[] nums2, int n)
    {
        if (n == 0) return;
        if (m == 0 && 0 < n)
        {
            nums2.CopyTo(nums1,0);
            return;
        }

        int lastIndex = m + n - 1;
        int mLastIndex = m - 1;
        int nLastIndex = n - 1;

        while( -1< lastIndex && -1 < nLastIndex)
        {
            var nValue = nums2[nLastIndex];

            if (mLastIndex < 0)
            {
                nums1[lastIndex--] = nValue;
                nLastIndex--;
                continue;
            }

            var mValue = nums1[mLastIndex];
            if (mValue <= nValue)
            {
                nums1[lastIndex--] = nValue;
                nLastIndex--;
            }
            else
            {
                nums1[lastIndex--] = mValue;
                mLastIndex--;
            }
        }
    }
}