using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个二叉搜索树的根节点 root 和一个值 key，删除二叉搜索树中的 key 对应的节点，并保证二叉搜索树的性质不变。返回二叉搜索树（有可能被更新）的根节点的引用。

一般来说，删除节点可分为两个步骤：

首先找到需要删除的节点；
如果找到了，删除它。
说明： 要求算法时间复杂度为 O(h)，h 为树的高度。

示例:

root = [5,3,6,2,4,null,7]
key = 3

    5
   / \
  3   6
 / \   \
2   4   7

给定需要删除的节点值是 3，所以我们首先找到 3 这个节点，然后删除它。

一个正确的答案是 [5,4,6,2,null,null,7], 如下图所示。

    5
   / \
  4   6
 /     \
2       7

另一个正确答案是 [5,2,6,null,4,null,7]。

    5
   / \
  2   6
   \   \
    4   7 
*/
/// <summary>
/// https://leetcode-cn.com/problems/delete-node-in-a-bst/
/// 450. 删除二叉搜索树中的节点
/// https://blog.csdn.net/qq_17550379/article/details/82344213
/// https://blog.csdn.net/my_clear_mind/article/details/82284346
/// </summary>
class DeleteNodeInABSTSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public TreeNode DeleteNode(TreeNode root, int key)
    {
        if (root == null) return null;

        if (root.val == key)
        {
            if (root.left != null && root.right != null)
            {
                TreeNode minRNode = root.right;
                while (minRNode.left != null)
                {
                    minRNode = minRNode.left;
                }
                root.val = minRNode.val;
                root.right = DeleteNode(root.right, minRNode.val);

                return root;
            }
            else
            {
                if (root.left != null)
                { //仅有左孩子
                    //if(parent == null) 
                    //root.val = root.left.val;
                    return root.left;
                }
                else if (root.right != null)
                {   //仅有右孩子
                    //root.val = root.right.val;
                    return root.right;
                }

                //无孩子
                return null;
            }
        }

        if (key > root.val) root.right = DeleteNode(root.right, key);
        else if (key < root.val) root.left = DeleteNode(root.left, key);

        return root;
    }
}
/*
public class Solution {
    public TreeNode DeleteNode(TreeNode root, int key) {
        if(root == null)
            return null;
        if(root.val == key){
            if(root.left == null && root.right == null)
                return null;
            else if(root.left == null)
                return root.right;
            else if(root.right == null)
                return root.left;
            else{
                TreeNode visit = root.right;
                while(visit.left != null)
                    visit = visit.left;
                root.val = visit.val;
                root.right = DeleteNode(root.right, root.val);
            }
        }
        else if(root.val > key)
            root.left = DeleteNode(root.left, key);
        else
            root.right = DeleteNode(root.right, key);
        return root;
    }
} 
public class Solution {
    public TreeNode DeleteNode(TreeNode root, int key) {
            if (root == null) return root;
            if (root.val == key)
            {
                // 如果删除的节点没有右孩子，那么就选择它的左孩子来代替原来的节点。
                if (root.right == null) return root.left;
                // 如果被删除节点的右孩子没有左孩子，那么这个右孩子被用来替换被删除节点。
                if (root.right.left == null)
                {
                    root.right.left = root.left;
                    return root.right;
                }
                // 如果被删除节点的右孩子有左孩子，
                // 就需要用被删除节点右孩子的左子树中的最下面的节点来替换它
                // https://images0.cnblogs.com/i/175043/201406/291214353511360.gif
                var leftTree = root.right.left;
                while (leftTree.left != null)
                {
                    leftTree = leftTree.left;
                }
                int min = leftTree.val;
                DeleteNode(root.right, min);
                root.val = min;
                return root;
            }
            else if (root.val > key) root.left = DeleteNode(root.left, key);
            else root.right = DeleteNode(root.right, key);
            return root;
    }
}
public class Solution
{
    private TreeNode FindMin(TreeNode root) =>
        root == null
            ? root
            : root.left == null
                ? root
                : FindMin(root.left);
    
    public TreeNode DeleteNode(TreeNode root, int key)
    {
        if (root == null) return root;
        else
        {
            if (key < root.val)
                root.left = DeleteNode(root.left, key);
            else if (key > root.val)
                root.right = DeleteNode(root.right, key);
            else // Find!
            {
                if (root.left != null && root.right != null)
                {
                    TreeNode temp = FindMin(root.right);
                    root.val = temp.val;
                    root.right = DeleteNode(root.right, root.val);
                }
                else
                {
                    TreeNode temp = root;
                    if (root.left == null)
                        root = root.right;
                    else
                        root = root.left;
                }
            }
        }
        
        return root;
    }
}
public class Solution {
    public  TreeNode DeleteNode(TreeNode root, int key) {
        if (root == null) {
			return root;
		}

		if (key < root.val) {
			root.left = DeleteNode(root.left, key);
		} else if (key > root.val) {
			root.right = DeleteNode(root.right, key);
		} else {
			TreeNode leftChild = root.left;
			TreeNode rightChild = root.right;

			if (rightChild == null) {
				return leftChild;
			}
			if (leftChild == null) {
				return rightChild;
			}
			TreeNode sNode = findNode(rightChild);
			sNode.right = deleteNode(rightChild);
			sNode.left = leftChild;

			root.left = null;
			root.right = null;
			return sNode;
		}

		return root;
	}

	private  TreeNode findNode(TreeNode root) {
		if (root.left == null) {
			return root;
		} else {
			return findNode(root.left);
		}
	}

	private  TreeNode deleteNode(TreeNode root) {
		if (root.left == null) {
			return root.right;
		} else {
			root.left = deleteNode(root.left);
		}
		return root;
	}
}
*/
