using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
给定一个二叉树，判断它是否是高度平衡的二叉树。

本题中，一棵高度平衡二叉树定义为：

一个二叉树每个节点 的左右两个子树的高度差的绝对值不超过1。

示例 1:

给定二叉树 [3,9,20,null,null,15,7]

    3
   / \
  9  20
    /  \
   15   7
返回 true 。

示例 2:

给定二叉树 [1,2,2,3,3,null,null,4,4]

       1
      / \
     2   2
    / \
   3   3
  / \
 4   4
返回 false 。

*/
/// <summary>
/// https://leetcode-cn.com/problems/balanced-binary-tree/
/// 110. 平衡二叉树
/// 
/// 
/// </summary>
class BalancedBinaryTreeSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    public bool IsBalanced(TreeNode root) 
    {
        const int NotBalanced = -1;
        return NotBalanced < GetHeight(root);

        int GetHeight(TreeNode root)
        {
            if( root == null )return 0; 

            int left = GetHeight(root.left);
            if( left == NotBalanced) return NotBalanced;

            int right = GetHeight(root.right);
            if( right == NotBalanced) return NotBalanced;

            if (left < right) (left, right) = (right,left);
            if( 1 < (left-right)) return NotBalanced;

            return left + 1;
        }

    }

}
/*

平衡二叉树
力扣 (LeetCode)
发布于 2020-02-10
25.0k
根据定义，一棵二叉树 TT 存在节点 p\in Tp∈T，满足 |\texttt{height}(p.left) - \texttt{height}(p.right)| > 1∣height(p.left)−height(p.right)∣>1 时，它是不平衡的。下图中每个节点的高度都被标记出来，高亮区域是一棵不平衡子树。



平衡子树暗示了一个事实，每棵子树也是一个子问题。
现在的问题是：按照什么顺序处理这些子问题？

方法一：自顶向下的递归
算法

定义方法 \texttt{height}height，用于计算任意一个节点 p\in Tp∈T 的高度：

\texttt{height}(p) = \begin{cases} -1 & p \text{ is an empty subtree i.e. } \texttt{null}\\ 1 + \max(\texttt{height}(p.left), \texttt{height}(p.right)) & \text{ otherwise} \end{cases}
height(p)={ 
−1
1+max(height(p.left),height(p.right))
​	
  
p is an empty subtree i.e. null
 otherwise
​	
 

接下来就是比较每个节点左右子树的高度。在一棵以 rr 为根节点的树
TT 中，只有每个节点左右子树高度差不大于 1 时，该树才是平衡的。因此可以比较每个节点左右两棵子树的高度差，然后向上递归。


class Solution {
  // Recursively obtain the height of a tree. An empty tree has -1 height
  private int height(TreeNode root) {
    // An empty tree has height -1
    if (root == null) {
      return -1;
    }
    return 1 + Math.max(height(root.left), height(root.right));
  }

  public boolean isBalanced(TreeNode root) {
    // An empty tree satisfies the definition of a balanced tree
    if (root == null) {
      return true;
    }

    // Check if subtrees have height within 1. If they do, check if the
    // subtrees are balanced
    return Math.abs(height(root.left) - height(root.right)) < 2
        && isBalanced(root.left)
        && isBalanced(root.right);
  }
};


复杂度分析

时间复杂度：\mathcal{O}(n\log n)O(nlogn)。

对于每个深度为 dd 的节点 pp，\texttt{height}(p)height(p) 被调用 pp 次。

首先，需要知道一棵平衡二叉树可以拥有的节点数量。令 f(h)f(h) 表示一棵高度为 hh 的平衡二叉树需要的最少节点数量。

f(h) = f(h - 1) + f(h - 2) + 1
f(h)=f(h−1)+f(h−2)+1

这与斐波那契数列的递归关系几乎相同。实际上，它的复杂度分析方法也和斐波那契数列一样。f(h)f(h) 的下界是 f(h) = \Omega\left(\left(\frac{3}{2}\right)^h\right)f(h)=Ω(( 
2
3
​	
 ) 
h
 )。

\begin{align} f(h+1) &= f(h) + f(h-1) + 1 \\ &> f(h) + f(h-1) & \qquad\qquad \text{This is the fibonacci sequence}\\ &\geq \left(\frac{3}{2}\right)^{h} + \left(\frac{3}{2}\right)^{h-1} & \text{via our claim} \\ &= \frac{5}{2} \left(\frac{3}{2}\right)^{h-1}\\ &> \frac{9}{4} \left(\frac{3}{2}\right)^{h-1} & \frac{9}{4} < \frac{5}{2}\\ &> \left(\frac{3}{2}\right)^{h+1} \end{align}

因此，平衡二叉树的高度 hh 不大于 \mathcal{O}(\log_{1.5}(n))O(log 
1.5
​	
 (n))。有了这个限制，可以保证方法 \texttt{height}height 在每个节点上调用不超过 \mathcal{O}(\log n)O(logn) 次。

如果树是倾斜的，高度达到 \mathcal{O}(n)$，算法没有尽早结束，最终会达到 \mathcal{O}(n^2)O(n 
2
 ) 的复杂度。
但是请注意：只要有子节点的两棵子树高度差大于 1，就会停止递归。实际上，如果树是完全倾斜的，仅需要检查最开始的两棵子树。
空间复杂度：\mathcal{O}(n)O(n)。如果树完全倾斜，递归栈可能包含所有节点。

一个有趣的事实：f(n) = f(n-1) + f(n-2) + 1f(n)=f(n−1)+f(n−2)+1 被称为斐波那契数列。

方法二：自底向上的递归
思路

方法一计算 \texttt{height}height 存在大量冗余。每次调用 \texttt{height}height 时，要同时计算其子树高度。但是自底向上计算，每个子树的高度只会计算一次。可以递归先计算当前节点的子节点高度，然后再通过子节点高度判断当前节点是否平衡，从而消除冗余。

算法

使用与方法一中定义的 \texttt{height}height 方法。自底向上与自顶向下的逻辑相反，首先判断子树是否平衡，然后比较子树高度判断父节点是否平衡。算法如下：

检查子树是否平衡。如果平衡，则使用它们的高度判断父节点是否平衡，并计算父节点的高度。


// Utility class to store information from recursive calls
final class TreeInfo {
  public final int height;
  public final boolean balanced;

  public TreeInfo(int height, boolean balanced) {
    this.height = height;
    this.balanced = balanced;
  }
}

class Solution {
  // Return whether or not the tree at root is balanced while also storing
  // the tree's height in a reference variable.
  private TreeInfo isBalancedTreeHelper(TreeNode root) {
    // An empty tree is balanced and has height = -1
    if (root == null) {
      return new TreeInfo(-1, true);
    }

    // Check subtrees to see if they are balanced.
    TreeInfo left = isBalancedTreeHelper(root.left);
    if (!left.balanced) {
      return new TreeInfo(-1, false);
    }
    TreeInfo right = isBalancedTreeHelper(root.right);
    if (!right.balanced) {
      return new TreeInfo(-1, false);
    }

    // Use the height obtained from the recursive calls to
    // determine if the current node is also balanced.
    if (Math.abs(left.height - right.height) < 2) {
      return new TreeInfo(Math.max(left.height, right.height) + 1, true);
    }
    return new TreeInfo(-1, false);
  }

  public boolean isBalanced(TreeNode root) {
    return isBalancedTreeHelper(root).balanced;
  }
};


复杂度分析

时间复杂度：\mathcal{O}(n)O(n)，计算每棵子树的高度和判断平衡操作都在恒定时间内完成。

空间复杂度：\mathcal{O}(n)O(n)，如果树不平衡，递归栈可能达到 \mathcal{O}(n)O(n)。

下一篇：110. 平衡二叉树（从底至顶，从顶至底）
© 著作权归作者所有
23
条评论

最热
精选评论(3)
dan-ge-bu-xiang-dong蛋哥不想动
2020-04-24
自底向上的简便写法

public boolean isBalanced(TreeNode root) {
        return helper(root)>=0;
    }

    public int helper(TreeNode root){
        if(root == null){
            return 0;
        }
        int l = helper(root.left);
        int r = helper(root.right);
        if(l==-1 || r==-1 || Math.abs(l-r)>1) return -1;
        return Math.max(l,r) +1;
    }
	
class ResultType{
    public bool isBalanced;
    public int height;
    public ResultType(bool isBalanced, int height) {
        this.isBalanced = isBalanced;
        this.height = height;
    }
    
}
public class Solution {   
    public bool IsBalanced(TreeNode root) {
        return helper(root).isBalanced;
    }
    private ResultType helper(TreeNode root){
        if (root == null){
            return new ResultType(true, -1);
        }
        ResultType left = helper(root.left);
        ResultType right = helper(root.right);
        if (Math.Abs(left.height - right.height) > 1){
            return new ResultType(false, -1);
        }
        if (!left.isBalanced || !right.isBalanced){
            return new ResultType(false, -1);
        }
        return new ResultType(true, Math.Max(left.height, right.height) + 1);
    }
}

public class Solution {
    public bool IsBalanced(TreeNode root) {
        if(root==null)return true;
        int a=Depth(root.left);
        int b=Depth(root.right);
        if(a==-1||b==-1||Math.Abs(a-b)>1)
            return false;
        else 
            return true;
    }
    public int Depth(TreeNode root)
    {
        if(root==null)return 0;
        int a=Depth(root.left);
        int b=Depth(root.right);
        if(a==-1||b==-1||Math.Abs(a-b)>1)
            return -1;
        else 
            return Math.Max(a,b)+1;
    }
}

public class Solution
{
    public bool IsBalanced(TreeNode root)
    {
        return Height(root) != -1;
    }

    private int Height(TreeNode root)
    {
        if (root == null) return 0;
        int left = Height(root.left);
        if (left == -1) return -1;
        int right = Height(root.right);
        if (right == -1) return -1;
        if (Math.Abs(left - right) > 1)
            return -1;
        return Math.Max(left, right) + 1;
    }
}

	 
 
 
 
*/
