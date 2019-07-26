using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个不含重复元素的整数数组。一个以此数组构建的最大二叉树定义如下：

二叉树的根是数组中的最大元素。
左子树是通过数组中最大值左边部分构造出的最大二叉树。
右子树是通过数组中最大值右边部分构造出的最大二叉树。
通过给定的数组构建最大二叉树，并且输出这个树的根节点。

示例 ：

输入：[3,2,1,6,0,5]
输出：返回下面这棵树的根节点：

      6
    /   \
   3     5
    \    / 
     2  0   
       \
        1
 

提示：

给定的数组的大小在 [1, 1000] 之间。 
*/
/// <summary>
/// https://leetcode-cn.com/problems/maximum-binary-tree/
/// 654. 最大二叉树
/// https://blog.csdn.net/romeo12334/article/details/81304442
/// </summary>
class MaximumBinaryTreeSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public TreeNode ConstructMaximumBinaryTree(int[] nums)
    {
        if (nums == null || nums.Length == 0) return null;
        return ConstructMaximumBinaryTree(nums, 0, nums.Length - 1);
    }

    private static TreeNode ConstructMaximumBinaryTree(int[] nums, int startIndex, int endIndex )
    {
        var index = startIndex;
        int max = nums[index];
        for( int i = startIndex + 1; i <= endIndex; i++)
        {
            var v = nums[i];
            if ( max < v)
            {
                index = i;
                max = v;
            }
        }

        TreeNode ret = new TreeNode(max);
        if (startIndex <= index - 1)  ret.left = ConstructMaximumBinaryTree(nums, startIndex, index - 1);
        if(index + 1 <= endIndex) ret.right = ConstructMaximumBinaryTree(nums, index + 1, endIndex);
        return ret;
    }
}
/*
public class Solution {
    public TreeNode ConstructMaximumBinaryTree(int[] nums) {
        if(nums.Length<1)
            return null;
        return Gen(nums,0,nums.Length);
    }
    public TreeNode Gen(int[] nums,
                       int l,int r)
    {
        if(r-l==1)
        {
            TreeNode n1=new 
                TreeNode(nums[l]);
            return n1;
        }
        int max=nums[l];
        int index=l;
        for(int i=l+1;i<r;++i)
        {
            if(nums[i]>max)
            {
                max=nums[i];
                index=i;
            }
        }
        TreeNode n=new TreeNode(
            nums[index]);
        if(index>l)
            n.left=Gen(nums,l,index);
        if(index<r-1)
            n.right=
            Gen(nums,index+1,r);
        return n;
    }
}

*/
