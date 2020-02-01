using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给出二叉搜索树的根节点，该二叉树的节点值各不相同，修改二叉树，
使每个节点 node 的新值等于原树中大于或等于 node.val 的值之和。

提醒一下，二叉搜索树满足下列约束条件：

节点的左子树仅包含键小于节点键的节点。
节点的右子树仅包含键大于节点键的节点。
左右子树也必须是二叉搜索树。
 

示例：



输入：[4,1,6,0,2,5,7,null,null,null,3,null,null,null,8]
输出：[30,36,21,36,35,26,15,null,null,null,33,null,null,null,8]
 

提示：

树中的节点数介于 1 和 100 之间。
每个节点的值介于 0 和 100 之间。
给定的树为二叉搜索树。
*/
/// <summary>
/// https://leetcode-cn.com/problems/binary-search-tree-to-greater-sum-tree/
/// 1038. 从二叉搜索树到更大和树
/// 
/// </summary>
class BinarySearchTreeToGreaterSumTreeSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public TreeNode BstToGst(TreeNode root)
    {
        if (root != null)
        {
            BstToGst(root.right);
            _rightTreeValueSum += root.val;
            root.val = _rightTreeValueSum;
            BstToGst(root.left);
        }
        return root;
    }

    private int _rightTreeValueSum = 0;
}
/*
g解题思路
一个节点需要加上比自己大的所有数
大于自己的  都在右子树
越往右的 比他大的就越少
比如
    4
1       6
    5     7
比7大的没有
比6大的是7
比5大的是6 7
能总结出规律  定义val = 0 我们只要从大到小 遍历二叉树 并让val+当前节点的值 并赋给当前节点
遍历顺序  右中左
代码
class Solution{
public:
    int val = 0;
    TreeNode* bstToGst(TreeNode* root) {
        if (root) {
            bstToGst(root->right);
            val += root->val;
            root->val = val;
            bstToGst(root->left);
        }
        return root;
    }
};


public class Solution {
    public TreeNode BstToGst(TreeNode root) {
        TreeNode btnRoot = root;
        Stack<TreeNode> stack = new Stack<TreeNode>();
                if (root == null)
                {
                    return root;
                }
                while (root.right != null)
                {
                    stack.Push(root);
                    root = root.right;
                }
                int sum = 0;
                stack.Push(root);
                while(stack.Count() > 0)
                {
                    root = stack.Pop();
                    sum += root.val;
                    root.val = sum;
                    if (root.left != null)
                    {
                        root = root.left;
                        while (root.right != null)
							{
                            stack.Push(root);
                            root = root.right;
                        }
                        stack.Push(root);
                    }
                }
                return btnRoot;
    }
}

public class Solution {
    public TreeNode BstToGst(TreeNode root) {
            var stack = new Stack<TreeNode>();
            
            var n = root;
            while (n.right != null)
            {
                stack.Push(n);
                n = n.right;
            }
            stack.Push(n);

            var sum = 0;
            while (stack.Count > 0)
            {
                n = stack.Pop();
                sum += n.val;
                n.val = sum;

                if(n.left !=null)
                {
                    n = n.left;
                    while (n.right != null)
                    {
                        stack.Push(n);
                        n = n.right;
                    }
                    stack.Push(n);
                }
            }
            return root;
    }
} 
*/
