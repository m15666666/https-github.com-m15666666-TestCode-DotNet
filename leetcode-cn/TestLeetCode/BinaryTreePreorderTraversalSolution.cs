using System.Collections.Generic;

/*
给定一个二叉树，返回它的 前序 遍历。

 示例:

输入: [1,null,2,3]
   1
    \
     2
    /
   3

输出: [1,2,3]
进阶: 递归算法很简单，你可以通过迭代算法完成吗？

*/

/// <summary>
/// https://leetcode-cn.com/problems/binary-tree-preorder-traversal/
/// 144. 二叉树的前序遍历
/// 给定一个二叉树，返回它的 前序 遍历。
/// </summary>
internal class BinaryTreePreorderTraversalSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public IList<int> PreorderTraversal(TreeNode root)
    {
        var ret = new List<int>();

        TreeNode node = root;
        while (node != null)
        {
            if (node.left == null)
            {
                ret.Add(node.val);
                node = node.right;
                continue;
            }
            TreeNode predecessor = node.left;
            while ((predecessor.right != null) && (predecessor.right != node))
                predecessor = predecessor.right;

            if (predecessor.right == null)
            {
                ret.Add(node.val);
                predecessor.right = node;
                node = node.left;
                continue;
            }
            predecessor.right = null;
            node = node.right;
        }
        return ret;
    }

    //public IList<int> PreorderTraversal(TreeNode root)
    //{
    //    List<int> ret = new List<int>();
    //    PreorderTraversal(root, ret);
    //    return ret;
    //}

    //private void PreorderTraversal( TreeNode root, IList<int> ret )
    //{
    //    if (root == null) return;
    //    ret.Add(root.val);

    //    PreorderTraversal(root.left, ret);
    //    PreorderTraversal(root.right, ret);
    //}
}

/*

二叉树的前序遍历
力扣 (LeetCode)
发布于 2019-06-24
49.1k
有两种通用的遍历树的策略：

深度优先搜索（DFS）

在这个策略中，我们采用深度作为优先级，以便从跟开始一直到达某个确定的叶子，然后再返回根到达另一个分支。

深度优先搜索策略又可以根据根节点、左孩子和右孩子的相对顺序被细分为前序遍历，中序遍历和后序遍历。

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
从根节点开始，每次迭代弹出当前栈顶元素，并将其孩子节点压入栈中，先压右孩子再压左孩子。

在这个算法中，输出到最终结果的顺序按照 Top->Bottom 和 Left->Right，符合前序遍历的顺序。


class Solution {
  public List<Integer> preorderTraversal(TreeNode root) {
    LinkedList<TreeNode> stack = new LinkedList<>();
    LinkedList<Integer> output = new LinkedList<>();
    if (root == null) {
      return output;
    }

    stack.add(root);
    while (!stack.isEmpty()) {
      TreeNode node = stack.pollLast();
      output.add(node.val);
      if (node.right != null) {
        stack.add(node.right);
      }
      if (node.left != null) {
        stack.add(node.left);
      }
    }
    return output;
  }
}
算法复杂度

时间复杂度：访问每个节点恰好一次，时间复杂度为 O(N)O(N) ，其中 NN 是节点的个数，也就是树的大小。
空间复杂度：取决于树的结构，最坏情况存储整棵树，因此空间复杂度是 O(N)O(N)。
方法 2：莫里斯遍历
方法基于 莫里斯的文章，可以优化空间复杂度。算法不会使用额外空间，只需要保存最终的输出结果。如果实时输出结果，那么空间复杂度是 O(1)O(1)。

算法

算法的思路是从当前节点向下访问先序遍历的前驱节点，每个前驱节点都恰好被访问两次。

首先从当前节点开始，向左孩子走一步然后沿着右孩子一直向下访问，直到到达一个叶子节点（当前节点的中序遍历前驱节点），所以我们更新输出并建立一条伪边 predecessor.right = root 更新这个前驱的下一个点。如果我们第二次访问到前驱节点，由于已经指向了当前节点，我们移除伪边并移动到下一个顶点。

如果第一步向左的移动不存在，就直接更新输出并向右移动。




class Solution {
  public List<Integer> preorderTraversal(TreeNode root) {
    LinkedList<Integer> output = new LinkedList<>();

    TreeNode node = root;
    while (node != null) {
      if (node.left == null) {
        output.add(node.val);
        node = node.right;
      }
      else {
        TreeNode predecessor = node.left;
        while ((predecessor.right != null) && (predecessor.right != node)) {
          predecessor = predecessor.right;
        }

        if (predecessor.right == null) {
          output.add(node.val);
          predecessor.right = node;
          node = node.left;
        }
        else{
          predecessor.right = null;
          node = node.right;
        }
      }
    }
    return output;
  }
}
算法复杂度

时间复杂度：每个前驱恰好访问两次，因此复杂度是 O(N)O(N)，其中 NN 是顶点的个数，也就是树的大小。
空间复杂度：我们在计算中不需要额外空间，但是输出需要包含 NN 个元素，因此空间复杂度为 O(N)O(N)。
下一篇：144. 二叉树的前序遍历（非递归）

public class Solution {
    public IList<int> PreorderTraversal(TreeNode root) {
        IList<int> res = new List<int>();
        if(root == null)
            return res;
        Traverse(root, res);
        return res;
    }
    
    private void Traverse(TreeNode node, IList<int> res){
        if(node == null)
            return;
        res.Add(node.val);
        Traverse(node.left, res);
        Traverse(node.right, res);
    }
}

public class Solution {
    public IList<int> PreorderTraversal(TreeNode root) {
        var result = new List<int>();
        if(root == null)return result;
        var stack = new Stack<TreeNode>();
        stack.Push(root);
        while(stack.Count > 0){
            var t = stack.Pop();
            result.Add(t.val);
            if(t.right != null)stack.Push(t.right);
            if(t.left != null)stack.Push(t.left);
        }
        return result;
    }
}


*/
