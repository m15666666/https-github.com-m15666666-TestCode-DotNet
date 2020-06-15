using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
根据一棵树的中序遍历与后序遍历构造二叉树。

注意:
你可以假设树中没有重复的元素。

例如，给出

中序遍历 inorder = [9,3,15,20,7]
后序遍历 postorder = [9,15,7,20,3]
返回如下的二叉树：

    3
   / \
  9  20
    /  \
   15   7
 
*/
/// <summary>
/// https://leetcode-cn.com/problems/construct-binary-tree-from-inorder-and-postorder-traversal/
/// 106.从中序与后序遍历序列构造二叉树
/// 
/// 
/// 根据一棵树的中序遍历与后序遍历构造二叉树。
/// 注意:你可以假设树中没有重复的元素。
/// </summary>
class BuildTreeFromInAndPostSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    public TreeNode BuildTree(int[] inorder, int[] postorder)
    {
        if (postorder == null || postorder.Length == 0 || inorder == null || inorder.Length != postorder.Length) return null;
        return BuildTree(postorder, 0, postorder.Length - 1, inorder, 0, inorder.Length - 1);
    }

    private TreeNode BuildTree(int[] postorder, int pStart, int pStop, int[] inorder, int iStart, int iStop)
    {
        if (pStop < pStart || iStop < iStart) return null;

        var v = postorder[pStop];
        int index;
        for (index = iStart; index <= iStop; index++)
            if (v == inorder[index]) break;

        TreeNode ret = new TreeNode(v);
        var rightTreeNodeCount = iStop - index;
        var rightTreeStart = pStop - rightTreeNodeCount;
        ret.left = BuildTree(postorder, pStart, rightTreeStart - 1, inorder, iStart, index - 1);
        ret.right = BuildTree(postorder, rightTreeStart, pStop - 1, inorder, index + 1, iStop);
        return ret;
    }
}
/*

从中序与后序遍历序列构造二叉树
力扣 (LeetCode)
发布于 2020-02-10
9.5k
如何遍历树
遍历树有两种通用策略：

深度优先遍历（DFS）

这种方法以深度 depth 优先为策略，从根节点开始遍历直到某个叶子节点为止，然后回到根节点，再遍历另外一个分支。
根据根节点，左孩子节点和右孩子节点的访问顺序又可以将 DFS 细分为先序遍历 preorder，中序遍历 inorder 和后序遍历 postorder。

广度优先遍历（BFS）

按照高度顺序，从上往下逐层遍历节点。
先遍历上层节点再遍历下层节点。

下图中按照不同的方法遍历对应子树，得到的序列都是 1-2-3-4-5。根据不同子树结构比较不同遍历方法的特点。



本问题中使用中序和后序遍历。

方法一：递归
如何根据两种遍历序列构造树：中序，和先序/后序/等等。

这类问题在 Facebook 的面试中常常出现，它可以在 \mathcal{O}(N)O(N) 的时间内解决：

通常从先序序列或者后序序列开始，根据不同遍历方法的规律，选择合适的节点构造树。
例如：先序序列的 第一个 节点是根节点，然后是它的左孩子，右孩子等等。
后序序列的 最后一个 节点是根节点，然后是它的右孩子，左孩子等等。

从先序/后序序列中找到根节点，根据根节点将中序序列分为左子树和右子树。从中序序列中获得的信息是：如果当前子树为空（返回 None），否则继续构造子树。



算法

创建 hashmap 存储中序序列：value -> its index 。

方法 helper 的参数是中序序列中当前子树的左右边界，该方法仅用于检查子树是否为空。下面分析 helper(in_left = 0, in_right = n - 1) 的逻辑：

如果 in_left > in_right，说明子树为空，返回 None。

选择后序遍历的最后一个节点作为根节点。

假设根节点在中序遍历中索引为 index。从 in_left 到 index - 1 属于左子树，从 index + 1 到 in_right 属于右子树。

根据后序遍历逻辑，递归创建右子树 helper(index + 1, in_right) 和左子树 helper(in_left, index - 1)。

返回根节点 root。




class Solution {
  int post_idx;
  int[] postorder;
  int[] inorder;
  HashMap<Integer, Integer> idx_map = new HashMap<Integer, Integer>();

  public TreeNode helper(int in_left, int in_right) {
    // if there is no elements to construct subtrees
    if (in_left > in_right)
      return null;

    // pick up post_idx element as a root
    int root_val = postorder[post_idx];
    TreeNode root = new TreeNode(root_val);

    // root splits inorder list
    // into left and right subtrees
    int index = idx_map.get(root_val);

    // recursion 
    post_idx--;
    // build right subtree
    root.right = helper(index + 1, in_right);
    // build left subtree
    root.left = helper(in_left, index - 1);
    return root;
  }

  public TreeNode buildTree(int[] inorder, int[] postorder) {
    this.postorder = postorder;
    this.inorder = inorder;
    // start from the last postorder element
    post_idx = postorder.length - 1;

    // build a hashmap value -> its index
    int idx = 0;
    for (Integer val : inorder)
      idx_map.put(val, idx++);
    return helper(0, inorder.length - 1);
  }
}
复杂度分析

时间复杂度：\mathcal{O}(N)O(N)。
使用主定理计算时间复杂度。
T(N) = aT\left(\frac{N}{b}\right) + \Theta(N^d)T(N)=aT( 
b
N
​	
 )+Θ(N 
d
 )。
该公式表示花费 \Theta(N^d)Θ(N 
d
 ) 时间分解一个问题，得到 aa 个规模为 \frac{N}{b} 
b
N
​	
  的子问题。
这里把一个问题分解为两个子问题 a = 2，计算左右子树的规模为初始问题的一半 b = 2，每次分解花费恒定时间 d = 0，即 \log_b{a} = dlog 
b
​	
 a=d。
根据主定理，时间复杂度为 \mathcal{O}(N^{\log_b(a)}) = \mathcal{O}(N)O(N 
log 
b
​	
 (a)
 )=O(N)。

空间复杂度：\mathcal{O}(N)O(N)，存储整棵树。

public class Solution {
    public TreeNode BuildTree(int[] inorder, int[] postorder) {
        if (inorder == null || inorder.Length <= 0 || inorder.Length != postorder.Length) return null;
        int length = inorder.Length;
        Dictionary<int, int> valueToIndexMap = new Dictionary<int, int>(length);
        for (int i = 0; i < length; i++)
        {
            valueToIndexMap.Add(inorder[i], i);
        }
        return this.BuildTreeImpl(inorder, 0, length - 1, postorder, 0, length - 1, valueToIndexMap);
    }
    private TreeNode BuildTreeImpl(int[] inorder, int leftin, int rightin, 
        int[] postorder, int leftpost, int rightpost, Dictionary<int, int> valueToIndexMap)
    {
        if (leftin > rightin || leftpost > rightpost) return null;
        TreeNode root = new TreeNode(postorder[rightpost]);
        int inorderIndex = valueToIndexMap[postorder[rightpost]];
        root.left = this.BuildTreeImpl(inorder, leftin, inorderIndex - 1, 
            postorder, leftpost, leftpost + inorderIndex - leftin - 1, valueToIndexMap);
        root.right = this.BuildTreeImpl(inorder, inorderIndex + 1, rightin, 
            postorder, leftpost + inorderIndex - leftin, rightpost - 1, valueToIndexMap);
        return root;
    }
}

 
 
 
*/