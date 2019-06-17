using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
您需要在二叉树的每一行中找到最大的值。

示例：

输入: 

          1
         / \
        3   2
       / \   \  
      5   3   9 

输出: [1, 3, 9]
*/
/// <summary>
/// https://leetcode-cn.com/problems/find-largest-value-in-each-tree-row/
/// 515. 在每个树行中找最大值
/// </summary>
class FindLargestValueInEachTreeRowSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public IList<int> LargestValues(TreeNode root)
    {
        if (root == null) return new int[0];
        List<int> ret = new List<int>();
        Queue<TreeNode> queue = new Queue<TreeNode>();
        queue.Enqueue(root);
        while (0 < queue.Count )
        {
            int size = queue.Count;
            int max = int.MinValue;
            for (int i = 0; i < size; i++)
            {
                TreeNode node = queue.Dequeue();
                if (max < node.val) max = node.val;
                if(node.left != null) queue.Enqueue(node.left);
                if (node.right != null) queue.Enqueue(node.right);
            }
            ret.Add(max);
        }
        return ret;
    }
}
/*
public class Solution {
    public IList<int> LargestValues(TreeNode root) {
        List<int> output = new List<int>();
        Queue<TreeNode> q = new Queue<TreeNode>();
        if(root == null)
            return output;
        q.Enqueue(root);
        while(q.Count>0)
        {
            int maxVal = Int32.MinValue;
            int len = q.Count;
            for(int i = 0; i < len; i++)
            {
                TreeNode tNode = q.Dequeue();
                maxVal = maxVal < tNode.val? tNode.val : maxVal;
                if(tNode.left!=null)
                    q.Enqueue(tNode.left);
                if(tNode.right!=null)
                    q.Enqueue(tNode.right);
            }
            output.Add(maxVal);
        }
        return output;
    }
}
*/
