using System.Collections.Generic;

/*
给定一个二叉树，判断其是否是一个有效的二叉搜索树。

假设一个二叉搜索树具有如下特征：

节点的左子树只包含小于当前节点的数。
节点的右子树只包含大于当前节点的数。
所有左子树和右子树自身必须也是二叉搜索树。
示例 1:

输入:
    2
   / \
  1   3
输出: true
示例 2:

输入:
    5
   / \
  1   4
     / \
    3   6
输出: false
解释: 输入为: [5,1,4,null,null,3,6]。
     根节点的值为 5 ，但是其右子节点值为 4 。

*/

/// <summary>
/// https://leetcode-cn.com/problems/validate-binary-search-tree/
/// 98. 验证二叉搜索树
///
///
/// </summary>
internal class ValidateBinarySerachTreeSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public bool IsValidBST(TreeNode root)
    {
        var stack = new Stack<TreeNode>();

        while (root != null)
        {
            stack.Push(root);
            root = root.left;
        }

        int inorder;
        if (0 < stack.Count)
        {
            root = stack.Pop();
            inorder = root.val;

            root = root.right;
            while (root != null)
            {
                stack.Push(root);
                root = root.left;
            }
        }
        else return true;

        while (0 < stack.Count)
        {
            root = stack.Pop();
            if (root.val <= inorder) return false;
            inorder = root.val;

            root = root.right;
            while (root != null)
            {
                stack.Push(root);
                root = root.left;
            }
        }
        return true;
    }
}

/*

验证二叉搜索树
力扣官方题解
发布于 2020-05-03
24.0k
📺视频题解

📖文字题解
方法一: 递归
思路和算法

要解决这道题首先我们要了解二叉搜索树有什么性质可以给我们利用，由题目给出的信息我们可以知道：如果该二叉树的左子树不为空，则左子树上所有节点的值均小于它的根节点的值； 若它的右子树不空，则右子树上所有节点的值均大于它的根节点的值；它的左右子树也为二叉搜索树。

这启示我们设计一个递归函数 helper(root, lower, upper) 来递归判断，函数表示考虑以 root 为根的子树，判断子树中所有节点的值是否都在 (l,r)(l,r) 的范围内（注意是开区间）。如果 root 节点的值 val 不在 (l,r)(l,r) 的范围内说明不满足条件直接返回，否则我们要继续递归调用检查它的左右子树是否满足，如果都满足才说明这是一棵二叉搜索树。

那么根据二叉搜索树的性质，在递归调用左子树时，我们需要把上界 upper 改为 root.val，即调用 helper(root.left, lower, root.val)，因为左子树里所有节点的值均小于它的根节点的值。同理递归调用右子树时，我们需要把下界 lower 改为 root.val，即调用 helper(root.right, root.val, upper)。

函数递归调用的入口为 helper(root, -inf, +inf)， inf 表示一个无穷大的值。

下图展示了算法如何应用在示例 2 上：




class Solution {
  public boolean helper(TreeNode node, Integer lower, Integer upper) {
    if (node == null) return true;

    int val = node.val;
    if (lower != null && val <= lower) return false;
    if (upper != null && val >= upper) return false;

    if (! helper(node.right, val, upper)) return false;
    if (! helper(node.left, lower, val)) return false;
    return true;
  }

  public boolean isValidBST(TreeNode root) {
    return helper(root, null, null);
  }
}
复杂度分析

时间复杂度 : O(n)O(n)，其中 nn 为二叉树的节点个数。在递归调用的时候二叉树的每个节点最多被访问一次，因此时间复杂度为 O(n)O(n)。

空间复杂度 : O(n)O(n)，其中 nn 为二叉树的节点个数。递归函数在递归过程中需要为每一层递归函数分配栈空间，所以这里需要额外的空间且该空间取决于递归的深度，即二叉树的高度。最坏情况下二叉树为一条链，树的高度为 nn ，递归最深达到 nn 层，故最坏情况下空间复杂度为 O(n)O(n) 。

方法二：中序遍历
思路和算法

基于方法一中提及的性质，我们可以进一步知道二叉搜索树「中序遍历」得到的值构成的序列一定是升序的，这启示我们在中序遍历的时候实时检查当前节点的值是否大于前一个中序遍历到的节点的值即可。如果均大于说明这个序列是升序的，整棵树是二叉搜索树，否则不是，下面的代码我们使用栈来模拟中序遍历的过程。

可能由读者不知道中序遍历是什么，我们这里简单提及一下，中序遍历是二叉树的一种遍历方式，它先遍历左子树，再遍历根节点，最后遍历右子树。而我们二叉搜索树保证了左子树的节点的值均小于根节点的值，根节点的值均小于右子树的值，因此中序遍历以后得到的序列一定是升序序列。

fig1


class Solution {
  public boolean isValidBST(TreeNode root) {
    Stack<TreeNode> stack = new Stack();
    double inorder = - Double.MAX_VALUE;

    while (!stack.isEmpty() || root != null) {
      while (root != null) {
        stack.push(root);
        root = root.left;
      }
      root = stack.pop();
      // 如果中序遍历得到的节点的值小于等于前一个 inorder，说明不是二叉搜索树
      if (root.val <= inorder) return false;
      inorder = root.val;
      root = root.right;
    }
    return true;
  }
}
复杂度分析

时间复杂度 : O(n)O(n)，其中 nn 为二叉树的节点个数。二叉树的每个节点最多被访问一次，因此时间复杂度为 O(n)O(n)。

空间复杂度 : O(n)O(n)，其中 nn 为二叉树的节点个数。栈最多存储 nn 个节点，因此需要额外的 O(n)O(n) 的空间。

下一篇：中序遍历轻松拿下，🤷‍♀️必

public class Solution {
    public bool IsValidBST(TreeNode root)
    {
        List<int> ans = new List<int>();
        helper(ans, root);
        
        for(int i = 1; i < ans.Count; i++)
        {
            if(ans[i - 1] >= ans[i]) return false;
        }
        return true;
    }

    private void helper(List<int> ans, TreeNode root)
    {
        if(root == null) return;
        if(root.left != null)
            helper(ans, root.left);
        
        ans.Add(root.val);
        
        if(root.right != null)
            helper(ans,root.right);
    }
}

*/
