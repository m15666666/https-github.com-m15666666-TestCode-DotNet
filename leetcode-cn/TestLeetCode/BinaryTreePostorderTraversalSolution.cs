using System.Collections.Generic;

/*
给定一个二叉树，返回它的 后序 遍历。

示例:

输入: [1,null,2,3]
   1
    \
     2
    /
   3

输出: [3,2,1]
进阶: 递归算法很简单，你可以通过迭代算法完成吗？

*/

/// <summary>
/// https://leetcode-cn.com/problems/binary-tree-postorder-traversal/
/// 145. 二叉树的后序遍历
///
///
///
/// </summary>
internal class BinaryTreePostorderTraversalSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public IList<int> PostorderTraversal(TreeNode root)
    {
        var output = new List<int>();
        if (root == null) return output;

        var stack = new Stack<TreeNode>();
        stack.Push(root);
        while (0 < stack.Count)
        {
            TreeNode node = stack.Pop();
            output.Insert(0, node.val);

            if (node.left != null) stack.Push(node.left);
            if (node.right != null) stack.Push(node.right);
        }
        return output;
    }
}

/*

二叉树的后序遍历
力扣 (LeetCode)
发布于 2019-06-25
37.5k
如何遍历一棵树
有两种通用的遍历树的策略：

深度优先搜索（DFS）

在这个策略中，我们采用 深度 作为优先级，以便从跟开始一直到达某个确定的叶子，然后再返回到达另一个分支。

深度优先搜索策略又可以根据根节点、左孩子和右孩子的相对顺序被细分为先序遍历，中序遍历和后序遍历。

宽度优先搜索（BFS）

我们按照高度顺序一层一层的访问整棵树，高层次的节点将会比低层次的节点先被访问到。

下图中的顶点按照访问的顺序编号，按照 1-2-3-4-5 的顺序来比较不同的策略。

102.png

本问题就是用宽度优先搜索遍历来划分层次：[[1], [2, 3], [4, 5]]。

方法 1：迭代
算法

首先，定义树的存储结构 TreeNode。


public class TreeNode {
  int val;
  TreeNode left;
  TreeNode right;

  TreeNode(int x) {
    val = x;
  }
}
从根节点开始依次迭代，弹出栈顶元素输出到输出列表中，然后依次压入它的所有孩子节点，按照从上到下、从左至右的顺序依次压入栈中。

因为深度优先搜索后序遍历的顺序是从下到上、从左至右，所以需要将输出列表逆序输出。


class Solution {
  public List<Integer> postorderTraversal(TreeNode root) {
    LinkedList<TreeNode> stack = new LinkedList<>();
    LinkedList<Integer> output = new LinkedList<>();
    if (root == null) {
      return output;
    }

    stack.add(root);
    while (!stack.isEmpty()) {
      TreeNode node = stack.pollLast();
      output.addFirst(node.val);
      if (node.left != null) {
        stack.add(node.left);
      }
      if (node.right != null) {
        stack.add(node.right);
      }
    }
    return output;
  }
}
复杂度分析

时间复杂度：访问每个节点恰好一次，因此时间复杂度为 O(N)O(N)，其中 NN 是节点的个数，也就是树的大小。
空间复杂度：取决于树的结构，最坏情况需要保存整棵树，因此空间复杂度为 O(N)O(N)。
下一篇：迭代解法，时间复杂度 O(n)，空间复杂度 O(n)

public class Solution {
    public IList<int> PostorderTraversal(TreeNode root) {
        List<int> result = new List<int>();
        Stack<TreeNode> stack1 = new Stack<TreeNode>();
        Stack<TreeNode> stack2 = new Stack<TreeNode>();
        
        if (root == null){
            return result;
        }
        stack1.Push(root);
        while (stack1.Count != 0){
            root = stack1.Pop();
            stack2.Push(root);
            if (root.left != null){
                stack1.Push(root.left);
            }
            if (root.right != null){
                stack1.Push(root.right);
            }
            }
            while (stack2.Count != 0 && stack1.Count == 0){
                root = stack2.Pop();
                result.Add(root.val);
            }
        return result;
      
    }
}


*/