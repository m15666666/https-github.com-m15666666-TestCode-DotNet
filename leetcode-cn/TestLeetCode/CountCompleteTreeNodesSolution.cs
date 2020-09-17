using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给出一个完全二叉树，求出该树的节点个数。

说明：

完全二叉树的定义如下：在完全二叉树中，除了最底层节点可能没填满外，其余每层节点数都达到最大值，并且最下面一层的节点都集中在该层最左边的若干位置。若最底层为第 h 层，则该层包含 1~ 2h 个节点。

示例:

输入: 
    1
   / \
  2   3
 / \  /
4  5 6

输出: 6
 
*/
/// <summary>
/// https://leetcode-cn.com/problems/count-complete-tree-nodes/
/// 222. 完全二叉树的节点个数
/// 给出一个完全二叉树，求出该树的节点个数。
/// 说明：
/// 完全二叉树的定义如下：在完全二叉树中，除了最底层节点可能没填满外，其余每层节点数都达到最大值，
/// 并且最下面一层的节点都集中在该层最左边的若干位置。若最底层为第 h 层，则该层包含 1~ 2h 个节点。
/// 示例:
/// 输入: 
/// 1
/// / \
/// 2   3
/// / \  /
/// 4  5 6
/// 输出: 6
/// http://dongcoder.com/detail-1113988.html
/// </summary>
class CountCompleteTreeNodesSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    // 获取左子树的高度(其实是最左侧分支）
    public int GetLeftHeight(TreeNode root)
    {
        int count = 0;
        while (root != null)
        {
            count++;
            root = root.left;
        }
        return count;
    }

    // 获取右子树的高度（其实是最右侧分支的高度）
    public int GetRightHeight(TreeNode root)
    {
        int count = 0;
        while (root != null)
        {
            count++;
            root = root.right;
        }
        return count;
    }

    public int CountNodes(TreeNode root)
    {
        if (root == null) return 0;

        int leftHeight = GetLeftHeight(root);
        int rightHeight = GetRightHeight(root);

        if (leftHeight == rightHeight)
        {
            // 表示是满二叉树，二叉树的节点数直接由公式2^n-1得到
            // leftHeight即为层数， 1 << leftHeight使用位运算计算2^leftHeight，效率更高
            // 注意(1 << leftHeight) - 1 的括号必须有！！
            return (1 << leftHeight) - 1;
        }

        // 若该二叉树不是满二叉树，递归的调用该方法，计算左子树和右子树的节点数
        return CountNodes(root.left) + CountNodes(root.right) + 1;
    }
}
/*

完全二叉树的节点个数
力扣 (LeetCode)
发布于 2020-01-19
16.1k
方法一：线性时间
算法：

最简单的解决方法就是用递归一个一个的计算节点。


class Solution:
    def countNodes(self, root: TreeNode) -> int:
        return 1 + self.countNodes(root.right) + self.countNodes(root.left) if root else 0
复杂度分析

时间复杂度：\mathcal{O}(N)O(N)。
空间复杂度：\mathcal{O}(d) = \mathcal{O}(\log N)O(d)=O(logN)，其中 dd 指的是树的的高度，运行过程中堆栈所使用的空间。
方法二：二分搜索
方法一没有利用完全二叉树的特性。完全二叉树中，除了最后一层外，其余每层节点都是满的，并且最后一层的节点全部靠向左边。

这说明如果第 k 层不是最后一层，则在第 k 层中将有 2^k 个节点。由于最后一层可能没有完全填充，则节点数在 1 到 2^d 之间，其中 d 指的是树的高度。

在这里插入图片描述

我们可以直接计算除了最后一层以外的所有结点个数：\sum_{k = 0}^{k = d - 1}{2^k} = 2^d - 1∑ 
k=0
k=d−1
​	
 2 
k
 =2 
d
 −1。那么我们可以将问题简化为计算完全二叉树最后一层有多少个节点。

在这里插入图片描述

现在有两个问题：

最后一层我们需要检查多少个节点？
一次检查的最佳的时间性能是什么？
让我们从第一个问题开始思考。最后一层的叶子节点全部靠向左边，我们可以用二分搜索只检查 \log(2^d) = dlog(2 
d
 )=d 个叶子代替检查全部叶子。

在这里插入图片描述

让我们思考第二个问题，最后一层的叶子节点索引在 0 到 $2^d - 1$ 之间。如何检查第 idx 节点是否存在？让我们来用二分搜索来构造从根节点到 idx 的移动序列。如，idx = 4。idx 位于 0,1,2,3,4,5,6,7 的后半部分，因此第一步是向右移动；然后 idx 位于 4,5,6,7 的前半部分，因此第二部是向左移动；idx 位于 4,5 的前半部分，因此下一步是向左移动。一次检查的时间复杂度为 \mathcal{O}(d)O(d)。

在这里插入图片描述

我们需要 \mathcal{O}(d)O(d) 次检查，一次检查需要 \mathcal{O}(d)O(d)，所以总的时间复杂度为 \mathcal{O}(d^2)O(d 
2
 )。

算法：

如果树为空，返回 0。
计算树的高度 d。
如果 d == 0，返回 1。
除最后一层以外的所有节点数为 2^d-1。最后一层的节点数通过二分搜索，检查最后一层有多少个节点。使用函数 exists(idx, d, root) 检查第 idx 节点是否存在。
使用二分搜索实现 exists(idx, d, root)。
返回 2^d - 1 + 最后一层的节点数。

class Solution {
  // Return tree depth in O(d) time.
  public int computeDepth(TreeNode node) {
    int d = 0;
    while (node.left != null) {
      node = node.left;
      ++d;
    }
    return d;
  }

  // Last level nodes are enumerated from 0 to 2**d - 1 (left -> right).
  // Return True if last level node idx exists. 
  // Binary search with O(d) complexity.
  public boolean exists(int idx, int d, TreeNode node) {
    int left = 0, right = (int)Math.pow(2, d) - 1;
    int pivot;
    for(int i = 0; i < d; ++i) {
      pivot = left + (right - left) / 2;
      if (idx <= pivot) {
        node = node.left;
        right = pivot;
      }
      else {
        node = node.right;
        left = pivot + 1;
      }
    }
    return node != null;
  }

  public int countNodes(TreeNode root) {
    // if the tree is empty
    if (root == null) return 0;

    int d = computeDepth(root);
    // if the tree contains 1 node
    if (d == 0) return 1;

    // Last level nodes are enumerated from 0 to 2**d - 1 (left -> right).
    // Perform binary search to check how many nodes exist.
    int left = 1, right = (int)Math.pow(2, d) - 1;
    int pivot;
    while (left <= right) {
      pivot = left + (right - left) / 2;
      if (exists(pivot, d, root)) left = pivot + 1;
      else right = pivot - 1;
    }

    // The tree contains 2**d - 1 nodes on the first (d - 1) levels
    // and left nodes on the last level.
    return (int)Math.pow(2, d) - 1 + left;
  }
}
复杂度分析

时间复杂度：\mathcal{O}(d^2) = \mathcal{O}(\log^2 N)O(d 
2
 )=O(log 
2
 N)，其中 dd 指的是树的高度。
空间复杂度：\mathcal{O}(1)O(1)。

public class Solution {
    int count=0;
    public int CountNodes(TreeNode root) {
        if(root!=null){
            count++;
            CountNodes(root.left);
            CountNodes(root.right);
        }
        return count;
    }
}

public class Solution {
    public int CountNodes(TreeNode root) {

        // if(root==null)return 0;
        // return CountNodes(root.left)+CountNodes(root.right)+1;     

        if(root==null)return 0;

        int left = GetDepth(root.left);
        int right = GetDepth(root.right);

        if(left == right)return (1<<left)+CountNodes(root.right);
        else return (1<<right)+CountNodes(root.left);

    }

    int GetDepth(TreeNode root){
        int depth = 0;

        while(root!=null){
            depth++;
            root = root.left;
        }

        return depth;
    }
}

public class Solution {
    public int CountNodes(TreeNode root) {
        return root == null ? 0 : 1 + this.CountNodes(root.left) + this.CountNodes(root.right);
    }
}



public class Solution {
    public int CountNodes(TreeNode root) {
        if(root==null)return 0;//判空返回
        //获取左右子树高度
        int leftHeight=GetHeight(root.left);
        int rigthHeight=GetHeight(root.right);
        //相等说明左子树一定是满的，返回左子树节点数和右子树递归，否则先计算左子树递归
        if(leftHeight==rigthHeight)return (1<<leftHeight)+CountNodes(root.right);
        else return (1<<rigthHeight)+CountNodes(root.left);
    }
    public int GetHeight(TreeNode node){
        //左右子树获取高度都是取其左子树遍历
        //左右子树若相等，最后的叶节点一定在右子树，若不相等则右子树高度小于左子树
        int height = 0;
        while(node != null) {
            height++;
            node = node.left;
        }
        return height;
    }
}
*/
