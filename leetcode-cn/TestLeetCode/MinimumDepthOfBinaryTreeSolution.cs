using System;

/*
给定一个二叉树，找出其最小深度。

最小深度是从根节点到最近叶子节点的最短路径上的节点数量。

说明: 叶子节点是指没有子节点的节点。

示例:

给定二叉树 [3,9,20,null,null,15,7],

    3
   / \
  9  20
    /  \
   15   7
返回它的最小深度  2.

*/

/// <summary>
/// https://leetcode-cn.com/problems/minimum-depth-of-binary-tree/
/// 111. 二叉树的最小深度
///
///
///
///
/// </summary>
internal class MinimumDepthOfBinaryTreeSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int MinDepth(TreeNode root)
    {
        if (root == null) return 0;
        if ((root.left == null) && (root.right == null)) return 1;

        int minDepth = int.MaxValue;
        if (root.left != null) minDepth = Math.Min(MinDepth(root.left), minDepth);
        if (root.right != null) minDepth = Math.Min(MinDepth(root.right), minDepth);

        return minDepth + 1;
    }
}
/*

二叉树的最小深度
力扣 (LeetCode)
发布于 2019-06-10
37.9k
树的定义

首先，定义树节点结构 TreeNode 。


// Definition for a binary tree node.
public class TreeNode {
  int val;
  TreeNode left;
  TreeNode right;

  TreeNode(int x) {
    val = x;
  }
}
方法一：递归
算法

最直接的思路就是递归。

我们用深度优先搜索来解决这个问题。


class Solution {
  public int minDepth(TreeNode root) {
    if (root == null) {
      return 0;
    }

    if ((root.left == null) && (root.right == null)) {
      return 1;
    }

    int min_depth = Integer.MAX_VALUE;
    if (root.left != null) {
      min_depth = Math.min(minDepth(root.left), min_depth);
    }
    if (root.right != null) {
      min_depth = Math.min(minDepth(root.right), min_depth);
    }

    return min_depth + 1;
  }
}
复杂度分析

时间复杂度：我们访问每个节点一次，时间复杂度为 O(N)O(N) ，其中 NN 是节点个数。
空间复杂度：最坏情况下，整棵树是非平衡的，例如每个节点都只有一个孩子，递归会调用 NN （树的高度）次，因此栈的空间开销是 O(N)O(N) 。但在最好情况下，树是完全平衡的，高度只有 \log(N)log(N)，因此在这种情况下空间复杂度只有 O(\log(N))O(log(N)) 。
方法二：深度优先搜索迭代
我们可以利用栈将上述解法中的递归变成迭代。

想法是对于每个节点，按照深度优先搜索的策略访问，同时在访问到叶子节点时更新最小深度。

我们从一个包含根节点的栈开始，当前深度为 1 。

然后开始迭代：弹出当前栈顶元素，将它的孩子节点压入栈中。当遇到叶子节点时更新最小深度。


class Solution {
  public int minDepth(TreeNode root) {
    LinkedList<Pair<TreeNode, Integer>> stack = new LinkedList<>();
    if (root == null) {
      return 0;
    }
    else {
      stack.add(new Pair(root, 1));
    }

    int min_depth = Integer.MAX_VALUE;
    while (!stack.isEmpty()) {
      Pair<TreeNode, Integer> current = stack.pollLast();
      root = current.getKey();
      int current_depth = current.getValue();
      if ((root.left == null) && (root.right == null)) {
        min_depth = Math.min(min_depth, current_depth);
      }
      if (root.left != null) {
        stack.add(new Pair(root.left, current_depth + 1));
      }
      if (root.right != null) {
        stack.add(new Pair(root.right, current_depth + 1));
      }
    }
    return min_depth;
  }
}
复杂度分析

时间复杂度：每个节点恰好被访问一遍，复杂度为 O(N)O(N)。
空间复杂度：最坏情况下我们会在栈中保存整棵树，此时空间复杂度为 O(N)O(N)。
方法二：广度优先搜索迭代
深度优先搜索方法的缺陷是所有节点都必须访问到，以保证能够找到最小深度。因此复杂度是 O(N)O(N) 。

一个优化的方法是利用广度优先搜索，我们按照树的层去迭代，第一个访问到的叶子就是最小深度的节点，这样就不用遍历所有的节点了。


class Solution {
  public int minDepth(TreeNode root) {
    LinkedList<Pair<TreeNode, Integer>> stack = new LinkedList<>();
    if (root == null) {
      return 0;
    }
    else {
      stack.add(new Pair(root, 1));
    }

    int current_depth = 0;
    while (!stack.isEmpty()) {
      Pair<TreeNode, Integer> current = stack.poll();
      root = current.getKey();
      current_depth = current.getValue();
      if ((root.left == null) && (root.right == null)) {
        break;
      }
      if (root.left != null) {
        stack.add(new Pair(root.left, current_depth + 1));
      }
      if (root.right != null) {
        stack.add(new Pair(root.right, current_depth + 1));
      }
    }
    return current_depth;
  }
}
复杂度分析

时间复杂度：最坏情况下，这是一棵平衡树，我们需要按照树的层次一层一层的访问完所有节点，除去最后一层的节点。这样访问了 N/2N/2 个节点，因此复杂度是 O(N)O(N)。
空间复杂度：和时间复杂度相同，也是 O(N)O(N)。
下一篇：二叉树的最小深度-理解递归结束条件

public class Solution {
    public int MinDepth(TreeNode root) {
        if(root == null)
        {
            return 0;
        }

        Queue currentQueue = new Queue();
        Queue nextQueue = new Queue();
        int depth = 0;

        nextQueue.Enqueue(root);
        while(nextQueue.Count > 0)
        {
            depth++;
            currentQueue = nextQueue;
            nextQueue = new Queue();

            while(currentQueue.Count > 0)
            {
                TreeNode currentNode = currentQueue.Dequeue() as TreeNode;

                if(currentNode.left == null && currentNode.right == null)
                {
                    return depth;
                }

                if(currentNode.left != null)
                {
                    nextQueue.Enqueue(currentNode.left);
                }

                if(currentNode.right != null)
                {
                    nextQueue.Enqueue(currentNode.right);
                }
            }
        }

        return depth;
    }
}

*/
