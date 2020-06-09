using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定两个二叉树，编写一个函数来检验它们是否相同。

如果两个树在结构上相同，并且节点具有相同的值，则认为它们是相同的。

示例 1:

输入:       1         1
          / \       / \
         2   3     2   3

        [1,2,3],   [1,2,3]

输出: true
示例 2:

输入:      1          1
          /           \
         2             2

        [1,2],     [1,null,2]

输出: false
示例 3:

输入:       1         1
          / \       / \
         2   1     1   2

        [1,2,1],   [1,1,2]

输出: false

*/
/// <summary>
/// https://leetcode-cn.com/problems/same-tree/
/// 100. 相同的树
/// 
/// 
/// 
/// 
/// </summary>
class SameTreeSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public bool IsSameTree(TreeNode p, TreeNode q) {
        if (p == null && q == null) return true;
        if (q == null || p == null) return false;
        if (p.val != q.val) return false;
        return IsSameTree(p.left, q.left) && IsSameTree(p.right, q.right);
    }
}
/*

相同的树
力扣 (LeetCode)
发布于 2019-07-08
37.6k
方法一：递归
直觉

最简单的策略是使用递归。首先判断 p 和 q 是不是 None，然后判断它们的值是否相等。
若以上判断通过，则递归对子结点做同样操作。

实现

class Solution {
  public boolean isSameTree(TreeNode p, TreeNode q) {
    // p and q are both null
    if (p == null && q == null) return true;
    // one of p and q is null
    if (q == null || p == null) return false;
    if (p.val != q.val) return false;
    return isSameTree(p.right, q.right) &&
            isSameTree(p.left, q.left);
  }
}
复杂度分析

时间复杂度 : O(N)O(N)，其中 N 是树的结点数，因为每个结点都访问一次。

空间复杂度 : 最优情况（完全平衡二叉树）时为 O(\log(N))O(log(N))，最坏情况下（完全不平衡二叉树）时为 {O}(N)O(N)，用于维护递归栈。




方法二：迭代
直觉

从根开始，每次迭代将当前结点从双向队列中弹出。然后，进行方法一中的判断：

p 和 q 不是 None,

p.val 等于 q.val,

若以上均满足，则压入子结点。

实现


class Solution {
  public boolean check(TreeNode p, TreeNode q) {
    // p and q are null
    if (p == null && q == null) return true;
    // one of p and q is null
    if (q == null || p == null) return false;
    if (p.val != q.val) return false;
    return true;
  }

  public boolean isSameTree(TreeNode p, TreeNode q) {
    if (p == null && q == null) return true;
    if (!check(p, q)) return false;

    // init deques
    ArrayDeque<TreeNode> deqP = new ArrayDeque<TreeNode>();
    ArrayDeque<TreeNode> deqQ = new ArrayDeque<TreeNode>();
    deqP.addLast(p);
    deqQ.addLast(q);

    while (!deqP.isEmpty()) {
      p = deqP.removeFirst();
      q = deqQ.removeFirst();

      if (!check(p, q)) return false;
      if (p != null) {
        // in Java nulls are not allowed in Deque
        if (!check(p.left, q.left)) return false;
        if (p.left != null) {
          deqP.addLast(p.left);
          deqQ.addLast(q.left);
        }
        if (!check(p.right, q.right)) return false;
        if (p.right != null) {
          deqP.addLast(p.right);
          deqQ.addLast(q.right);
        }
      }
    }
    return true;
  }
}
复杂度分析

时间复杂度 : O(N)O(N)，其中 N 是树的结点数，因为每个结点都访问一次。

空间复杂度 : 最优情况（完全平衡二叉树）时为 O(\log(N))O(log(N))，最坏情况下（完全不平衡二叉树）时为 {O}(N)O(N)，用于维护双向队列。

public class Solution 
{
    public bool IsSameTree(TreeNode p, TreeNode q) 
    {
        bool l;
        bool r;
        if(p!=null&&q!=null&&p.val==q.val)
        {
            l = IsSameTree(p.left,q.left);
            r = IsSameTree(p.right,q.right);
            if(l==true&&r==true)
            {
                return true ;
            }
            return false;
        }
        else if(p==null&&q==null)
        {
            return true;
        }
        else
        {
            return false;
        }    
    }
}

 
 
 
 
 
*/
