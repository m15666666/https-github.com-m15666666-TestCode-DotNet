using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
将一个按照升序排列的有序数组，转换为一棵高度平衡二叉搜索树。

本题中，一个高度平衡二叉树是指一个二叉树每个节点 的左右两个子树的高度差的绝对值不超过 1。

示例:

给定有序数组: [-10,-3,0,5,9],

一个可能的答案是：[0,-3,9,-10,null,5]，它可以表示下面这个高度平衡二叉搜索树：

      0
     / \
   -3   9
   /   /
 -10  5


*/
/// <summary>
/// https://leetcode-cn.com/problems/convert-sorted-array-to-binary-search-tree/
/// 108. 将有序数组转换为二叉搜索树
/// 
/// 
/// 
/// 
/// </summary>
class ConvertSortedArrayToBinarySearchTreeSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public TreeNode SortedArrayToBST(int[] nums) 
    {
        return SortedArrayToBST(0, nums.Length - 1);
    
        TreeNode SortedArrayToBST(int left, int right)
        {
            if (right < left) return null;

            int mid = (left + right) / 2;
            return new TreeNode(nums[mid])
            {
                left = SortedArrayToBST(left, mid - 1),
                right = SortedArrayToBST(mid + 1, right)
            };
        }
    }
}
/*

将有序数组转换为二叉搜索树
力扣 (LeetCode)
发布于 2020-02-10
22.5k
遍历树的方法。DFS：先序遍历，中序遍历，后序遍历；BFS。
遍历树的两种通用策略：

深度优先遍历（DFS）

这种方法以深度 depth 优先为策略，从根节点开始一直遍历到某个叶子节点，然后回到根节点，在遍历另外一个分支。
根据根节点，左孩子节点和右孩子节点的访问顺序又可以将 DFS 细分为先序遍历 preorder，中序遍历 inorder 和后序遍历 postorder。

广度优先遍历（BFS）

按照高度顺序，从上往下逐层遍历节点。
先遍历上层节点再遍历下层节点。

下图中按照不同的方法遍历对应子树，得到的遍历顺序都是 1-2-3-4-5。根据不同子树结构比较不同遍历方法的特点。



将有序数组转换为二叉搜索树的结果为什么 不唯一 ？
众所周知，二叉搜索树的中序遍历是一个升序序列。

将有序数组作为输入，可以把该问题看做 根据中序遍历序列创建二叉搜索树。

这个问题的答案唯一吗。例如：是否可以根据中序遍历序列和二叉搜索树之间是否一一对应，答案是 否定的。

下面是一些关于 BST 的知识。
中序遍历不能唯一确定一棵二叉搜索树。
先序和后序遍历不能唯一确定一棵二叉搜索树。
先序/后序遍历和中序遍历的关系：
inorder = sorted(postorder) = sorted(preorder)，
中序+后序、中序+先序可以唯一确定一棵二叉树。

因此，“有序数组 -> BST”有多种答案。



因此，添加一个附件条件：树的高度应该是平衡的、例如：每个节点的两棵子树高度差不超过 1。

这种情况下答案唯一吗？仍然没有。



高度平衡意味着每次必须选择中间数字作为根节点。这对于奇数个数的数组是有用的，但对偶数个数的数组没有预定义的选择方案。



对于偶数个数的数组，要么选择中间位置左边的元素作为根节点，要么选择中间位置右边的元素作为根节点，不同的选择方案会创建不同的平衡二叉搜索树。方法一始终选择中间位置左边的元素作为根节点，方法二始终选择中间位置右边的元素作为根节点。方法一和二会生成不同的二叉搜索树，这两种答案都是正确的。

方法一：中序遍历：始终选择中间位置左边元素作为根节点
算法



方法 helper(left, right) 使用数组 numsnums 中索引从 left 到 right 的元素创建 BST：

如果 left > right，子树中不存在元素，返回空。

找出中间元素：p = (left + right) // 2。

创建根节点：root = TreeNode(nums[p])。

递归创建左子树 root.left = helper(left, p - 1) 和右子树 root.right = helper(p + 1, right)。

返回 helper(0, len(nums) - 1)。


class Solution {
  int[] nums;

  public TreeNode helper(int left, int right) {
    if (left > right) return null;

    // always choose left middle node as a root
    int p = (left + right) / 2;

    // inorder traversal: left -> node -> right
    TreeNode root = new TreeNode(nums[p]);
    root.left = helper(left, p - 1);
    root.right = helper(p + 1, right);
    return root;
  }

  public TreeNode sortedArrayToBST(int[] nums) {
    this.nums = nums;
    return helper(0, nums.length - 1);
  }
}
复杂度分析

时间复杂度：\mathcal{O}(N)O(N)，每个元素只访问一次。

空间复杂度：\mathcal{O}(N)O(N)，二叉搜索树空间 \mathcal{O}(N)O(N)，递归栈深度 \mathcal{O}(\log N)O(logN)。

方法二：中序遍历：始终选择中间位置右边元素作为根节点
算法



方法 helper(left, right) 使用数组 numsnums 中索引从 left 到 right 的元素创建 BST：

如果 left > right，子树中不存在元素，返回空。

找出中间位置右边元素：

p = (left + right) // 2。

如果 left + right 是偶数，则 p + 1。

创建根节点：root = TreeNode(nums[p])。

递归创建左子树 root.left = helper(left, p - 1) 和右子树 root.right = helper(p + 1, right)。

返回 helper(0, len(nums) - 1)。


class Solution {
  int[] nums;

  public TreeNode helper(int left, int right) {
    if (left > right) return null;

    // always choose right middle node as a root
    int p = (left + right) / 2;
    if ((left + right) % 2 == 1) ++p;

    // inorder traversal: left -> node -> right
    TreeNode root = new TreeNode(nums[p]);
    root.left = helper(left, p - 1);
    root.right = helper(p + 1, right);
    return root;
  }

  public TreeNode sortedArrayToBST(int[] nums) {
    this.nums = nums;
    return helper(0, nums.length - 1);
  }
}
复杂度分析

时间复杂度：\mathcal{O}(N)O(N)，每个元素只访问一次。

空间复杂度：\mathcal{O}(N)O(N)，二叉搜索树空间 \mathcal{O}(N)O(N)，递归栈深度 \mathcal{O}(\log N)O(logN)。

方法三：中序遍历：选择任意一个中间位置元素作为根节点
不做预定义选择，每次随机选择中间位置左边或者右边元素作为根节点。每次运行的结果都不同，但都是正确的。



算法

方法 helper(left, right) 使用数组 numsnums 中索引从 left 到 right 的元素创建 BST：

如果 left > right，子树中不存在元素，返回空。

找出中间位置右边元素：

p = (left + right) // 2。

如果 left + right 是偶数，随机选择 p + 0 或者 p + 1。

创建根节点：root = TreeNode(nums[p])。

递归创建左子树 root.left = helper(left, p - 1) 和右子树 root.right = helper(p + 1, right)。

返回 helper(0, len(nums) - 1)。


class Solution {
    int[] nums;
    Random rand = new Random();
    
    public TreeNode helper(int left, int right) {
        if (left > right) return null;
        
        // choose random middle node as a root
        int p = (left + right) / 2; 
        if ((left + right) % 2 == 1) p += rand.nextInt(2);

        // inorder traversal: left -> node -> right
        TreeNode root = new TreeNode(nums[p]);
        root.left = helper(left, p - 1);
        root.right = helper(p + 1, right);
        return root;
    }
    
    public TreeNode sortedArrayToBST(int[] nums) {
        this.nums = nums;
        return helper(0, nums.length - 1);    
    }
}
复杂度分析

时间复杂度：\mathcal{O}(N)O(N)，每个元素只访问一次。

空间复杂度：\mathcal{O}(N)O(N)，二叉搜索树空间 \mathcal{O}(N)O(N)，递归栈深度 \mathcal{O}(\log N)O(logN)。

下一篇：图解二叉搜索树构造 | 递归 & Python & Go

public class Solution {
    public TreeNode SortedArrayToBST(int[] nums) {
        if(nums.Length == 0) return null;
        return helper(nums, 0, nums.Length - 1);
    }
    public TreeNode helper(int[] nums, int left, int right) {
        if(left > right) return null;
        int mid = (left + right)/2;
        if((left + right) % 2 != 0) {
            mid = mid + 1;
        }

        TreeNode root = new TreeNode(nums[mid]);
        root.left = helper(nums, left, mid -1);
        root.right = helper(nums, mid + 1, right);

        return root; 
    }
}

public class Solution {
    int[] nums;
    public TreeNode SortedArrayToBST(int[] nums) {
        this.nums=nums;
        return Divide(0,nums.Length-1);
    }
    private TreeNode Divide(int start,int end){
        if(start>end) return null;
        int middle=(start+end+1)/2;
        TreeNode temp;
        temp=new TreeNode();
        if(start==end){
            temp.val=nums[start];
            return temp;
        }
        temp.val=nums[middle];
        temp.left=Divide(start,middle-1);
        temp.right=Divide(middle+1,end);
        return temp;
    }
}

 
 
 
*/
