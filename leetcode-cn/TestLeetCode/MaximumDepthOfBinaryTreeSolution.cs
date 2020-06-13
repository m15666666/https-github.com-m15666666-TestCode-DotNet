/*
给定一个二叉树，找出其最大深度。

二叉树的深度为根节点到最远叶子节点的最长路径上的节点数。

说明: 叶子节点是指没有子节点的节点。

示例：
给定二叉树 [3,9,20,null,null,15,7]，

    3
   / \
  9  20
    /  \
   15   7
返回它的最大深度 3 。

*/

/// <summary>
/// https://leetcode-cn.com/problems/maximum-depth-of-binary-tree/
/// 104. 二叉树的最大深度
///
///
///
///
/// </summary>
internal class MaximumDepthOfBinaryTreeSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int MaxDepth(TreeNode root)
    {
        if (root == null) return 0;
        int subHeight = root.left == null ? 0 : MaxDepth(root.left);
        if (root.right != null)
        {
            int rightHeight = MaxDepth(root.right);
            if (subHeight < rightHeight) subHeight = rightHeight;
        }
        return subHeight + 1;
    }
}

/*


二叉树的最大深度
力扣 (LeetCode)
发布于 2018-10-04
82.9k
树的定义

首先，给出我们将要使用的树的结点 TreeNode 的定义。


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



直观的方法是通过递归来解决问题。在这里，我们演示了 DFS（深度优先搜索）策略的示例。


class Solution {
  public int maxDepth(TreeNode root) {
    if (root == null) {
      return 0;
    } else {
      int left_height = maxDepth(root.left);
      int right_height = maxDepth(root.right);
      return java.lang.Math.max(left_height, right_height) + 1;
    }
  }
}
复杂度分析

时间复杂度：我们每个结点只访问一次，因此时间复杂度为 O(N)O(N)，
其中 NN 是结点的数量。
空间复杂度：在最糟糕的情况下，树是完全不平衡的，例如每个结点只剩下左子结点，递归将会被调用 NN 次（树的高度），因此保持调用栈的存储将是 O(N)O(N)。但在最好的情况下（树是完全平衡的），树的高度将是 \log(N)log(N)。因此，在这种情况下的空间复杂度将是 O(\log(N))O(log(N))。
方法二：迭代
我们还可以在栈的帮助下将上面的递归转换为迭代。

我们的想法是使用 DFS 策略访问每个结点，同时在每次访问时更新最大深度。

所以我们从包含根结点且相应深度为 1 的栈开始。然后我们继续迭代：将当前结点弹出栈并推入子结点。每一步都会更新深度。


import javafx.util.Pair;
import java.lang.Math;

class Solution {
  public int maxDepth(TreeNode root) {
    Queue<Pair<TreeNode, Integer>> stack = new LinkedList<>();
    if (root != null) {
      stack.add(new Pair(root, 1));
    }

    int depth = 0;
    while (!stack.isEmpty()) {
      Pair<TreeNode, Integer> current = stack.poll();
      root = current.getKey();
      int current_depth = current.getValue();
      if (root != null) {
        depth = Math.max(depth, current_depth);
        stack.add(new Pair(root.left, current_depth + 1));
        stack.add(new Pair(root.right, current_depth + 1));
      }
    }
    return depth;
  }
};
复杂度分析

时间复杂度：O(N)O(N)。
空间复杂度：O(N)O(N)。

public class Solution {
    public int MaxDepth(TreeNode root) {
		if(root==null) return 0;
		int depth = 0;
        var queue = new Queue<TreeNode>();
        queue.Enqueue(root);

        while(queue.Count > 0)
        {
            depth++;
            int size = queue.Count;
            for(int i = 0; i < size; i++)
            {
                var node = queue.Dequeue();
                if(node.left != null)
                {
                    queue.Enqueue(node.left);
                }
                if(node.right != null)
                {
                    queue.Enqueue(node.right);
                }
            }
        }

        return depth;
    }
}

public class Solution
{
	public int MaxDepth(TreeNode root)
	{
		if (root == null) return 0;
		int result=0;
		Df(root, ref result,0);
		return result;
	}
	private void Df(TreeNode node,ref int max ,int deep)
	{
		if (node == null)
			return;
		deep++;
		Df(node.left,ref max,deep);
		max = Math.Max(max,deep);
		Df(node.right,ref max,deep);
		max = Math.Max(max, deep);
	}
}

public class Solution {
    public int MaxDepth(TreeNode root) {
        if(root==null) return 0;
        return Math.Max(MaxDepth(root.left),MaxDepth(root.right))+1;
    }
}


*/
