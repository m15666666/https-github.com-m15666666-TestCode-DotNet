using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个二叉树，在树的最后一行找到最左边的值。

示例 1:

输入:

    2
   / \
  1   3

输出:
1
 

示例 2:

输入:

        1
       / \
      2   3
     /   / \
    4   5   6
       /
      7

输出:
7
 

注意: 您可以假设树（即给定的根节点）不为 NULL。
*/
/// <summary>
/// https://leetcode-cn.com/problems/find-bottom-left-tree-value/
/// 513. 找树左下角的值
/// </summary>
class FindBottomLeftTreeValueSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int FindBottomLeftValue(TreeNode root)
    {
        if (root == null) return 0;

        int level = 0;
        return FindBottomLeftValue(root, ref level);
    }

    private int FindBottomLeftValue(TreeNode root, ref int level )
    {
        level++;
        if (root.left == null && root.right == null) return root.val;

        int leftLevel = level;
        int rightLevel = level;
        int leftV = 0;
        int rightV = 0;
        
        if (root.left != null) leftV = FindBottomLeftValue(root.left, ref leftLevel);
        if (root.right != null)  rightV = FindBottomLeftValue(root.right, ref rightLevel );

        if( leftLevel < rightLevel)
        {
            level = rightLevel;
            return rightV;
        }

        level = leftLevel;
        return leftV;
    }
}
/*
public class Solution {
    public int FindBottomLeftValue(TreeNode root) {
        Queue<TreeNode> queue = new Queue<TreeNode>();
        queue.Enqueue(root);
        int val = 0;
        while(queue.Count() > 0) {
            int size =queue.Count();
            
            bool hasSet = false;
            
            for(int i = 0; i < size; i ++) {
                TreeNode node = queue.Dequeue();
                if (node != null) {
                    if (hasSet == false) {
                        hasSet = true;
                        val = node.val;
                    }
                    queue.Enqueue(node.left);
                    queue.Enqueue(node.right);
                }
            }
        }
        return val;
    }
}
public class Solution {
    public int FindBottomLeftValue(TreeNode root) {
        int res = 0;
        Queue<TreeNode> queue = new Queue<TreeNode>();
        queue.Enqueue(root);
        while(queue.Count != 0){
            int len = queue.Count;
            for(int i = 0; i < len; ++i){
                TreeNode node = queue.Dequeue();
                if(i == 0)
                    res = node.val;
                if(node.left != null)
                    queue.Enqueue(node.left);
                if(node.right != null)
                    queue.Enqueue(node.right);
            }
        }
        return res;
    }
}

*/
