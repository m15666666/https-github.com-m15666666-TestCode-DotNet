using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个整数 n，生成所有由 1 ... n 为节点所组成的 二叉搜索树 。

 

示例：

输入：3
输出：
[
  [1,null,3,2],
  [3,2,null,1],
  [3,1,null,null,2],
  [2,1,3],
  [1,null,2,null,3]
]
解释：
以上的输出对应以下 5 种不同结构的二叉搜索树：

   1         3     3      2      1
    \       /     /      / \      \
     3     2     1      1   3      2
    /     /       \                 \
   2     1         2                 3
 

提示：

0 <= n <= 8
 
*/
/// <summary>
/// https://leetcode-cn.com/problems/unique-binary-search-trees-ii/
/// 95.不同的二叉搜索树II
/// 
/// 给定一个整数 n，生成所有由 1 ... n 为节点所组成的二叉搜索树。
/// https://blog.csdn.net/happyaaaaaaaaaaa/article/details/51635367
/// https://blog.csdn.net/OneDeveloper/article/details/80169460
/// </summary>
class GenerateTreesSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public IList<TreeNode> GenerateTrees(int n)
    {
        if (n < 1) return new List<TreeNode>();
        List<TreeNode> nullTree = new List<TreeNode>() { null };
        List<TreeNode>[,] cache = new List<TreeNode>[n + 1, n + 1];
        return GenerateTrees(1, n);

        List<TreeNode> GenerateTrees(int start, int end )
        {
            if (end < start) return nullTree;
            List<TreeNode> ret = cache[start,end];
            if (ret != null) return ret;

            if (start == end)
            {
                ret = new List<TreeNode>() { new TreeNode(start) };
                cache[start, end] = ret;
                return ret;
            }

            ret = new List<TreeNode>();
            for (int i = start; i <= end; i++)
            {
                List<TreeNode> leftSubTrees = GenerateTrees(start,i - 1);
                List<TreeNode> rightSubTrees = GenerateTrees(i + 1,end);
                foreach (TreeNode left in leftSubTrees)
                    foreach (TreeNode right in rightSubTrees)
                        ret.Add(new TreeNode(i) { left = left, right = right });
            }

            cache[start, end] = ret;
            return ret;
        }
    }

    //public IList<TreeNode> GenerateTrees(int n)
    //{
    //    if (n < 1) return new List<TreeNode>();
    //    List<TreeNode>[,] cache = new List<TreeNode>[n + 2, n + 2];
    //    return GenerateTrees(1, n, cache);
    //}

    //private List<TreeNode> GenerateTrees(int start, int end, List<TreeNode>[,] cache )
    //{
    //    List<TreeNode> ret = new List<TreeNode>();
    //    if (end < start)
    //    {
    //        ret.Add(null);
    //        return ret;
    //    }
    //    for (int i = start; i <= end; i++)
    //    {
    //        List<TreeNode> list1 = cache[start,i - 1];
    //        if (list1 == null)
    //        {
    //            //递归，并将结果保存下来
    //            list1 = GenerateTrees(start, i - 1, cache);
    //            cache[start,i - 1] = list1;
    //        }
    //        //（start,i-1）为左子树，遍历不同的左子树组合
    //        foreach (TreeNode left in list1)
    //        {
    //            List<TreeNode> list2 = cache[i + 1,end];//当 i = n 时，就需要求 cache[n+1,end] 的值
    //            if (list2 == null)
    //            {
    //                //递归，并将结果保存下来
    //                list2 = GenerateTrees(i + 1, end, cache);
    //                cache[i + 1,end] = list2;
    //            }
    //            //（i+1,end）为右子树，遍历不同的右子树组合
    //            foreach (TreeNode right in list2)
    //            {
    //                TreeNode root = new TreeNode(i);
    //                root.left = left;
    //                root.right = right;
    //                ret.Add(root);
    //            }
    //        }
    //    }
    //    return ret;
    //}
}
/*

不同的二叉搜索树 II
力扣 (LeetCode)
发布于 2019-07-07
39.8k
树的定义

首先，给出 TreeNode 的定义，后面会用到。


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
首先来计数需要构建的二叉树数量。可能的二叉搜素数数量是一个 卡特兰数。

我们跟随上文的逻辑，只是这次是构建具体的树，而不是计数。

算法

我们从序列 1 ..n 中取出数字 i，作为当前树的树根。于是，剩余 i - 1 个元素可用于左子树，n - i 个元素用于右子树。
如 前文所述，这样会产生 G(i - 1) 种左子树 和 G(n - i) 种右子树，其中 G 是卡特兰数。

image.png

现在，我们对序列 1 ... i - 1 重复上述过程，以构建所有的左子树；然后对 i + 1 ... n 重复，以构建所有的右子树。

这样，我们就有了树根 i 和可能的左子树、右子树的列表。

最后一步，对两个列表循环，将左子树和右子树连接在根上。

Python

class Solution:
    def generateTrees(self, n):
        """
        :type n: int
        :rtype: List[TreeNode]
        """
        def generate_trees(start, end):
            if start > end:
                return [None,]
            
            all_trees = []
            for i in range(start, end + 1):  # pick up a root
                # all possible left subtrees if i is choosen to be a root
                left_trees = generate_trees(start, i - 1)
                
                # all possible right subtrees if i is choosen to be a root
                right_trees = generate_trees(i + 1, end)
                
                # connect left and right subtrees to the root i
                for l in left_trees:
                    for r in right_trees:
                        current_tree = TreeNode(i)
                        current_tree.left = l
                        current_tree.right = r
                        all_trees.append(current_tree)
            
            return all_trees
        
        return generate_trees(1, n) if n else []
复杂度分析

Java

class Solution {
  public LinkedList<TreeNode> generate_trees(int start, int end) {
    LinkedList<TreeNode> all_trees = new LinkedList<TreeNode>();
    if (start > end) {
      all_trees.add(null);
      return all_trees;
    }

    // pick up a root
    for (int i = start; i <= end; i++) {
      // all possible left subtrees if i is choosen to be a root
      LinkedList<TreeNode> left_trees = generate_trees(start, i - 1);

      // all possible right subtrees if i is choosen to be a root
      LinkedList<TreeNode> right_trees = generate_trees(i + 1, end);

      // connect left and right trees to the root i
      for (TreeNode l : left_trees) {
        for (TreeNode r : right_trees) {
          TreeNode current_tree = new TreeNode(i);
          current_tree.left = l;
          current_tree.right = r;
          all_trees.add(current_tree);
        }
      }
    }
    return all_trees;
  }

  public List<TreeNode> generateTrees(int n) {
    if (n == 0) {
      return new LinkedList<TreeNode>();
    }
    return generate_trees(1, n);
  }
}
复杂度分析

时间复杂度 : 主要的计算开销在于构建给定根的全部可能树，也就是卡特兰数 G_nG 
n
​	
 。该过程重复了 nn 次，也就是 nG_nnG 
n
​	
 。卡特兰数以 \frac{4^n}{n^{3/2}} 
n 
3/2
 
4 
n
 
​	
  增长。因此，总的时间复杂度为 O(\frac{4^n}{n^{1/2}})O( 
n 
1/2
 
4 
n
 
​	
 )。这看上去很大，但别忘了，我们可是要生成 G_n\sim\frac{4^n}{n^{3/2}}G 
n
​	
 ∼ 
n 
3/2
 
4 
n
 
​	
  棵树的。

空间复杂度 :G_nG 
n
​	
  棵树，每个有 n 个元素，共计 n G_nnG 
n
​	
 。也就是 O(\frac{4^n}{n^{1/2}})O( 
n 
1/2
 
4 
n
 
​	
 )。

下一篇：详细通俗的思路分析，多解法

public class Solution {
    public IList<TreeNode> GenerateTrees(int n) {
        IList<TreeNode> result = new List<TreeNode>();
        if (n == 0)
            return result;
        else
            return Reversion(1, n);
    }

    //递归：从序列 1 ..n 中取出数字 i，作为当前树的树根。于是，剩余 i - 1 个元素可用于左子树，n - i 个元素用于右子树
    //     对两个列表循环，将左子树和右子树连接在根上。
    public List<TreeNode> Reversion(int start, int end)
    {
        List<TreeNode> ans = new List<TreeNode>();

        //开始位置大于结束位置，没有结果，返回null
        if (start > end)
        {
            ans.Add(null);
            return ans;
        }

        //只有一个值，将该值作为节点返回
        if (start == end)
        {
            TreeNode node = new TreeNode(start);
            ans.Add(node);
            return ans;
        }

        for (int i = start; i <= end; i++)
        {
            //小于i的数全部为其左子树，大于的数为右子树
            List<TreeNode> leftTrees = Reversion(start, i - 1);
            List<TreeNode> rightTrees = Reversion(i + 1, end);

            //将所有可能的左子树和右子树都和根节点生成一棵树            
            foreach (TreeNode left in leftTrees)
                foreach (TreeNode right in rightTrees)
                {
                    TreeNode root = new TreeNode(i);
                    root.left = left;
                    root.right = right;

                    ans.Add(root);
                }
        }

        return ans;
    }
}

 
 
 
*/