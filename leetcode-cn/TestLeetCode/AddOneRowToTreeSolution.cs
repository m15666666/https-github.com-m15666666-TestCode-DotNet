using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个二叉树，根节点为第1层，深度为 1。在其第 d 层追加一行值为 v 的节点。

添加规则：给定一个深度值 d （正整数），针对深度为 d-1 层的每一非空节点 N，为 N 创建两个值为 v 的左子树和右子树。

将 N 原先的左子树，连接为新节点 v 的左子树；将 N 原先的右子树，连接为新节点 v 的右子树。

如果 d 的值为 1，深度 d - 1 不存在，则创建一个新的根节点 v，原先的整棵树将作为 v 的左子树。

示例 1:

输入: 
二叉树如下所示:
       4
     /   \
    2     6
   / \   / 
  3   1 5   

v = 1

d = 2

输出: 
       4
      / \
     1   1
    /     \
   2       6
  / \     / 
 3   1   5   

示例 2:

输入: 
二叉树如下所示:
      4
     /   
    2    
   / \   
  3   1    

v = 1

d = 3

输出: 
      4
     /   
    2
   / \    
  1   1
 /     \  
3       1
注意:

输入的深度值 d 的范围是：[1，二叉树最大深度 + 1]。
输入的二叉树至少有一个节点。 
*/
/// <summary>
/// https://leetcode-cn.com/problems/add-one-row-to-tree/
/// 623. 在二叉树中增加一行
/// https://blog.csdn.net/zrh_CSDN/article/details/86063479
/// </summary>
class AddOneRowToTreeSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public TreeNode AddOneRow(TreeNode root, int v, int d)
    {
        if (d <= 1)
        {
            var newRoot = new TreeNode(v);

            if (d == 1) newRoot.left = root;
            else newRoot.right = root;

            return newRoot;
        }

        if(root != null)
        {
            root.left = AddOneRow(root.left, v, d > 2 ? d - 1 : 1);
            root.right = AddOneRow(root.right, v, d > 2 ? d - 1 : 0);
        }
        return root;
    }
}
/*
public class Solution {
    public TreeNode AddOneRow(TreeNode root, int v, int d) {
        if(d==1)
        {
            TreeNode n=new TreeNode(v);
            n.left=root;
            return n;
        }
        IList<TreeNode> dnode=
            new List<TreeNode>();
        FindNode(root,d,1,dnode);
        foreach(TreeNode n in dnode)
        {
            TreeNode n1=
                new TreeNode(v);
            TreeNode n2=
                new TreeNode(v);
            n1.left=n.left;
            n2.right=n.right;
            n.left=n1;
            n.right=n2;
        }
        return root;
    }
    public void FindNode(TreeNode n,
        int d,int curd,
        IList<TreeNode> dnode)
    {
        if(n==null)
            return;
        if(curd==d-1)
            dnode.Add(n);
        else
        {
            FindNode(n.left,
                     d,curd+1,dnode);
            FindNode(n.right,
                    d,curd+1,dnode);
        }
    }
}
public class Solution {
    public TreeNode AddOneRow(TreeNode root, int v, int d) {
        if(d==1)
        {   
            TreeNode t = new TreeNode(v);
            t.left = root;
            return t;
        }
        var queue = new Queue<TreeNode>();
        var flag = true;
        queue.Enqueue(root);
        int count;
        int level=1;
        TreeNode node= null;
        while(queue.Any() && flag)
        {
            count = queue.Count;
            for(int i =0 ;i<count;i++)
            { 
                node = queue.Dequeue();
                if(d-1==level)
                {
                    var tempLeft= node.left;
                    var tempRight = node.right;
                    node.left = new TreeNode(v);
                    node.right = new TreeNode(v);
                    node.left.left = tempLeft;
                    node.right.right =tempRight;
                    
                }
               if(node.left!=null)
                   queue.Enqueue(node.left);
                if(node.right!=null)
                   queue.Enqueue(node.right);
                
            }
            level++;
        }
        return root;
            
    }
}
public class Solution {
    public TreeNode AddOneRow(TreeNode root, int v, int d)
    {
        if (root == null)
        {
            return null;
        }

        if (d == 1)
        {
            TreeNode node = new TreeNode(v);
            node.left = root;

            return node;
        }

        if (d == 2)
        {
            TreeNode left = root.left;
            TreeNode right = root.right;

            TreeNode temp = new TreeNode(v);
            temp.left = left;
            root.left = temp;

            TreeNode node = new TreeNode(v);
            node.right = right;
            root.right = node;
        }
        else
        {
            root.left = AddOneRow(root.left, v, d - 1);
            root.right = AddOneRow(root.right, v, d - 1);
        }
        return root;
    }
}
*/
