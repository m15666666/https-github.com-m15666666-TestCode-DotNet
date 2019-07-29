using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个二叉树，编写一个函数来获取这个树的最大宽度。树的宽度是所有层中的最大宽度。这个二叉树与满二叉树（full binary tree）结构相同，但一些节点为空。

每一层的宽度被定义为两个端点（该层最左和最右的非空节点，两端点间的null节点也计入长度）之间的长度。

示例 1:

输入: 

           1
         /   \
        3     2
       / \     \  
      5   3     9 

输出: 4
解释: 最大值出现在树的第 3 层，宽度为 4 (5,3,null,9)。
示例 2:

输入: 

          1
         /  
        3    
       / \       
      5   3     

输出: 2
解释: 最大值出现在树的第 3 层，宽度为 2 (5,3)。
示例 3:

输入: 

          1
         / \
        3   2 
       /        
      5      

输出: 2
解释: 最大值出现在树的第 2 层，宽度为 2 (3,2)。
示例 4:

输入: 

          1
         / \
        3   2
       /     \  
      5       9 
     /         \
    6           7
输出: 8
解释: 最大值出现在树的第 4 层，宽度为 8 (6,null,null,null,null,null,null,7)。
注意: 答案在32位有符号整数的表示范围内。 
*/
/// <summary>
/// https://leetcode-cn.com/problems/maximum-width-of-binary-tree/
/// 662. 二叉树最大宽度
/// </summary>
class MaximumWidthOfBinaryTreeSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int WidthOfBinaryTree(TreeNode root)
    {
        if (root == null) return 0;

        Queue<TreeNode> queue = new Queue<TreeNode>(128);
        queue.Enqueue(root);

        int ret = 1;
        while( 0 < queue.Count )
        {
            int size = queue.Count;
            int width = 0;
            int nullCount = 0;
            for( int i = 0; i < size; i++)
            {
                var node = queue.Dequeue();
                if(node == null)
                {
                    if (0 < width)
                    {
                        nullCount++;
                        queue.Enqueue(null);
                        queue.Enqueue(null);
                    }
                    continue;
                }
                if (0 < width && 0 < nullCount)
                {
                    width += nullCount;
                    nullCount = 0;
                }
                width++;

                if (node.left != null) {
                    queue.Enqueue(node.left);
                }
                else queue.Enqueue(null);

                if (node.right != null) {
                    queue.Enqueue(node.right);
                }
                else queue.Enqueue(null);
            }
            if (ret < width) ret = width;
        }
        return ret;
    }
}
/*
public class Solution {
    public int WidthOfBinaryTree(TreeNode root) {
        if(root.left==null && root.right==null)
            return 1;
        if(root==null)
            return 0;
        var queue = new Queue<TreeNode>();
        var max=1;
        queue.Enqueue(root);
        int count;
        TreeNode node= null;
        List<int> list = new List<int>();
        list.Add(1);
        int size = 1;
        while(queue.Any())
        {
            node = queue.Dequeue();
            size--;
            int index = list[0];
            list.Remove(list[0]);
            if(node.left!=null)
            {
                queue.Enqueue(node.left);
                list.Add(2*index);
            }
            if(node.right!=null)
            {
                queue.Enqueue(node.right);
                list.Add(2*index+1);
            }
            if(size==0)
            {
                {
                    if(list.Count>=2)
                    max = Math.Max(list.Last()-list.First()+1,max);
                }
                size=queue.Count;
            }
            
           
           
        }
        return max;
    }
}

*/
