using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
给定一个二叉搜索树，编写一个函数 kthSmallest 来查找其中第 k 个最小的元素。

说明：
你可以假设 k 总是有效的，1 ≤ k ≤ 二叉搜索树元素个数。

示例 1:

输入: root = [3,1,4,null,2], k = 1
   3
  / \
 1   4
  \
   2
输出: 1
示例 2:

输入: root = [5,3,6,2,4,null,null,1], k = 3
       5
      / \
     3   6
    / \
   2   4
  /
 1
输出: 3
进阶：
如果二叉搜索树经常被修改（插入/删除操作）并且你需要频繁地查找第 k 小的值，你将如何优化 kthSmallest 函数？
 */

/// <summary>
/// https://leetcode-cn.com/problems/kth-smallest-element-in-a-bst/
/// 230. 二叉搜索树中第K小的元素
/// 给定一个二叉搜索树，编写一个函数 kthSmallest 来查找其中第 k 个最小的元素。
/// https://blog.csdn.net/xuchonghao/article/details/80770490
/// https://www.cnblogs.com/MrSaver/p/9956180.html
/// </summary>
class KthSmallestElementInaBSTSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int KthSmallest(TreeNode root, int k)
    {
        Stack<TreeNode> st = new Stack<TreeNode>();

        while (root != null)
        {
            st.Push(root);
            root = root.left;
        }

        while (k != 0)
        {
            TreeNode n = st.Pop();
            k--;
            if (k == 0) return n.val;
            TreeNode right = n.right;
            while (right != null)
            {
                st.Push(right);
                right = right.left;
            }
        }

        return -1; // never hit if k is valid
    }
}
/*
//别人的算法
public class Solution {
    public int KthSmallest(TreeNode root, int k) {
        var stack = new Stack<TreeNode>();
        var node = root;
        var result = 0;
        while(node != null)
        {
            stack.Push(node);
            node = node.left;
        }
        
        int i = 1;
        while(stack.Any())
        {
            node = stack.Pop();
            if (i++ == k)
            {
                result = node.val;
                break;
            }
            
            if (node.right != null)
            {
                node = node.right;
                while(node != null)
                {
                    stack.Push(node);
                    node = node.left;
                }
            }
        }
        return result;
    }
}
public class Solution {
    public int KthSmallest(TreeNode root, int k) {
        List<int> list=new List<int>();
        PreOrder(list,root);
        return list[k-1];
    }
    public void PreOrder(List<int> list,TreeNode node){
        if(node==null)return;
        PreOrder(list,node.left);
        list.Add(node.val);
        PreOrder(list,node.right);
    }
}
public class Solution {
    public int KthSmallest(TreeNode root, int k) 
    {
        int count = CalculateTreeNodeCount(root.left);
        if(k <= count)
        {
            return KthSmallest(root.left,k);
        }
        else if(k > count + 1)
        {
            return KthSmallest(root.right,k - count - 1);
        }
        else
        {
            return root.val;
        }
    }
    
    public int CalculateTreeNodeCount(TreeNode root)
    {
        if(root == null) return 0;
        return 1 + CalculateTreeNodeCount(root.left)+CalculateTreeNodeCount(root.right);
    }
}
     
     
*/
