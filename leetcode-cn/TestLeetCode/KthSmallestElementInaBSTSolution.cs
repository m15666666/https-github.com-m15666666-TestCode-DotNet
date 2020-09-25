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
二叉搜索树中第K小的元素
力扣 (LeetCode)
发布于 2020-01-19
24.0k
概述：
怎么遍历树：

深度优先搜索（DFS）
在这个策略中，我们从根延伸到某一片叶子，然后再返回另一个分支。根据根节点，左节点，右节点的相对顺序，DFS 还可以分为前序，中序，后序。

广度优先搜索（BFS）
在这个策略中，我们逐层，从上到下扫描整个树。

下图展示了不同的遍历策略：
在这里插入图片描述

为了解决这个问题，可以使用 BST 的特性：BST 的中序遍历是升序序列。

方法一：递归
算法：

通过构造 BST 的中序遍历序列，则第 k-1 个元素就是第 k 小的元素。

在这里插入图片描述


class Solution {
  public ArrayList<Integer> inorder(TreeNode root, ArrayList<Integer> arr) {
    if (root == null) return arr;
    inorder(root.left, arr);
    arr.add(root.val);
    inorder(root.right, arr);
    return arr;
  }

  public int kthSmallest(TreeNode root, int k) {
    ArrayList<Integer> nums = inorder(root, new ArrayList<Integer>());
    return nums.get(k - 1);
  }
}
复杂度分析

时间复杂度：O(N)O(N)，遍历了整个树。
空间复杂度：O(N)O(N)，用了一个数组存储中序序列。
方法二：迭代
算法：

在栈的帮助下，可以将方法一的递归转换为迭代，这样可以加快速度，因为这样可以不用遍历整个树，可以在找到答案后停止。

在这里插入图片描述


class Solution {
  public int kthSmallest(TreeNode root, int k) {
    LinkedList<TreeNode> stack = new LinkedList<TreeNode>();

    while (true) {
      while (root != null) {
        stack.add(root);
        root = root.left;
      }
      root = stack.removeLast();
      if (--k == 0) return root.val;
      root = root.right;
    }
  }
}
复杂度分析

时间复杂度：\mathcal{O}(H + k)O(H+k)，其中 HH 指的是树的高度，由于我们开始遍历之前，要先向下达到叶，当树是一个平衡树时：复杂度为 \mathcal{O}(\log N + k)O(logN+k)。当树是一个不平衡树时：复杂度为 \mathcal{O}(N + k)O(N+k)，此时所有的节点都在左子树。
空间复杂度：\mathcal{O}(H + k)O(H+k)。当树是一个平衡树时：\mathcal{O}(\log N + k)O(logN+k)。当树是一个非平衡树时：\mathcal{O}(N + k)O(N+k)。

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
