using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



class SortedArrayToBSTSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public TreeNode SortedArrayToBST(int[] nums)
    {
        if (nums == null || nums.Length == 0) return null;
        return SortedArrayToBST(nums, 0, nums.Length);
    }

    private TreeNode SortedArrayToBST(int[] nums, int index, int length)
    {
        if ( length < 1) return null;

        int lastIndex = index + length - 1;

        if (length == 1) return new TreeNode(nums[index]);
        if (length == 2) return new TreeNode(nums[index + 1]) { left = new TreeNode(nums[index]) };
        if (length == 3) return new TreeNode(nums[index + 1]) { left = new TreeNode(nums[index]), right = new TreeNode(nums[index+2]) };

        int midIndex = index + length / 2;
        var ret = new TreeNode(nums[midIndex]);
        ret.left = SortedArrayToBST(nums, index, midIndex - 1 - index + 1);
        ret.right = SortedArrayToBST(nums, midIndex + 1, lastIndex - (midIndex + 1) + 1);
        return ret;
    }
}