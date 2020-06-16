using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个二叉树，返回其节点值自底向上的层次遍历。 （即按从叶子节点所在层到根节点所在的层，逐层从左向右遍历）

例如：
给定二叉树 [3,9,20,null,null,15,7],

    3
   / \
  9  20
    /  \
   15   7
返回其自底向上的层次遍历为：

[
  [15,7],
  [9,20],
  [3]
]

*/
/// <summary>
/// https://leetcode-cn.com/problems/binary-tree-level-order-traversal-ii/
/// 107. 二叉树的层次遍历 II
/// 
/// 
/// 
/// 
/// </summary>
class BinaryTreeLevelOrderTraversalIISolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    public IList<IList<int>> LevelOrderBottom(TreeNode root)
    {
        List<IList<int>> ret = new List<IList<int>>();

        if (root == null) return ret;
        Queue<TreeNode> queue = new Queue<TreeNode>();
        queue.Enqueue(root);

        while(0 < queue.Count)
        {
            int size = queue.Count;
            List<int> set = new List<int>();
            ret.Add(set);
            for(int i = 0; i < size; i++)
            {
                var item = queue.Dequeue();
                set.Add(item.val);
                if (item.left != null) queue.Enqueue(item.left);
                if (item.right != null) queue.Enqueue(item.right);
            }
        }

        ret.Reverse();
        return ret;
    }
}
/*
public class Solution {
    public IList<IList<int>> levels = new List<IList<int>>();
    public IList<IList<int>> LevelOrderBottom(TreeNode root)
        {
            if (root == null) return levels;
            MyLevelOrderBottom(root, 0);
            return levels;
        }

        public void MyLevelOrderBottom(TreeNode root,int level)
        {
            //如果当前层等于列表个数，则添加一个列表
            if (levels.Count == level)
                levels.Insert(0,new List<int>());
            //将当前数据添加到列表中
            levels[levels.Count - level - 1].Add(root.val);
            //递归左子树
            if (root.left != null)
                MyLevelOrderBottom(root.left, level + 1);
            //递归右子树
            if (root.right != null)
                MyLevelOrderBottom(root.right, level + 1);
        }
}

public class Solution {
    public IList<IList<int>> LevelOrderBottom(TreeNode root) {
        IList<IList<int>> result = new List<IList<int>>();

        if(root == null)
        {
            return result;
        }

        Stack<List<int>> stack = new Stack<List<int>>();
        Queue<TreeNode> q = new Queue<TreeNode>();
        q.Enqueue(root);
        int rowSize = 1;
        List<int> items = new List<int>();
        while(q.Count>0)
        {
            var tmpNode = q.Dequeue();            
            rowSize--;
            items.Add(tmpNode.val);

            if(tmpNode.left != null)
            {
                q.Enqueue(tmpNode.left);
            }

            if(tmpNode.right != null)
            {
                q.Enqueue(tmpNode.right);
            }

            if(rowSize == 0)
            {
                rowSize = q.Count;
                stack.Push(items);
                items = new List<int>();
            }
        }

        while(stack.Count>0)
        {
            result.Add(stack.Pop());
        }

        return result;
    }
}

public class Solution {
    public IList<IList<int>> LevelOrderBottom(TreeNode root) {
        List<IList<int>> ans = new List<IList<int>>();

        if(root==null)
            return ans;

        Queue<TreeNode> q = new Queue<TreeNode>();
        q.Enqueue(root);
        
        while(q.Count>0){
            int count = q.Count;
            IList<int> list = new List<int>();
            for(int i =0; i<count ;i++){
                TreeNode curr = q.Dequeue();
                list.Add(curr.val);

                if(curr.left!=null){
                    q.Enqueue(curr.left);
                }
                if(curr.right!=null){
                    q.Enqueue(curr.right);
                }
            }
            ans.Add(list);
        }
        ans.Reverse();
        return ans;
    }
}
 
 
*/
