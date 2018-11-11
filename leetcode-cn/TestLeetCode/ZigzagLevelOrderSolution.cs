using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// https://leetcode-cn.com/problems/binary-tree-zigzag-level-order-traversal/
/// 103.二叉树的锯齿形层次遍历
/// 给定一个二叉树，返回其节点值的锯齿形层次遍历。（即先从左往右，再从右往左进行下一层遍历，以此类推，层与层之间交替进行）。
/// </summary>
class ZigzagLevelOrderSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    public IList<IList<int>> ZigzagLevelOrder(TreeNode root)
    {
        List<IList<int>> ret = new List<IList<int>>();
        if (root == null) return ret;

        List<TreeNode> list = new List<TreeNode> { root };
        bool left2right = true;

        while( 0 < list.Count)
        {
            var nodes = list.ToList();

            list.Clear();

            List<int> values = new List<int>();
            foreach( var node in nodes )
            {
                if (left2right) values.Add(node.val); else values.Insert(0, node.val);
                if (node.left != null) list.Add(node.left);
                if (node.right != null) list.Add(node.right);
            }

            ret.Add(values);

            left2right = !left2right;
        }

        return ret;
    }
}