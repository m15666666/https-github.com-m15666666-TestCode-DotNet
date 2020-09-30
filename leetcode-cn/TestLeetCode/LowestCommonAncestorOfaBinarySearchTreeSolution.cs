using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个二叉搜索树, 找到该树中两个指定节点的最近公共祖先。

百度百科中最近公共祖先的定义为：“对于有根树 T 的两个结点 p、q，最近公共祖先表示为一个结点 x，满足 x 是 p、q 的祖先且 x 的深度尽可能大（一个节点也可以是它自己的祖先）。”

例如，给定如下二叉搜索树:  root = [6,2,8,0,4,7,9,null,null,3,5]



 

示例 1:

输入: root = [6,2,8,0,4,7,9,null,null,3,5], p = 2, q = 8
输出: 6 
解释: 节点 2 和节点 8 的最近公共祖先是 6。
示例 2:

输入: root = [6,2,8,0,4,7,9,null,null,3,5], p = 2, q = 4
输出: 2
解释: 节点 2 和节点 4 的最近公共祖先是 2, 因为根据定义最近公共祖先节点可以为节点本身。
 

说明:

所有节点的值都是唯一的。
p、q 为不同节点且均存在于给定的二叉搜索树中。

 
*/
/// <summary>
/// https://leetcode-cn.com/problems/lowest-common-ancestor-of-a-binary-search-tree/
/// 235. 二叉搜索树的最近公共祖先
/// 
/// </summary>
class LowestCommonAncestorOfaBinarySearchTreeSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    public TreeNode LowestCommonAncestor(TreeNode root, TreeNode p, TreeNode q)
    {
        if (root == null || root.val == p.val || root.val == q.val) return root;

        TreeNode ancestor = root;
        while (true)
            if (p.val < ancestor.val && q.val < ancestor.val) ancestor = ancestor.left; 
            else if (p.val > ancestor.val && q.val > ancestor.val) ancestor = ancestor.right; 
            else break;
       
        return ancestor;
    }
}
/*
     
二叉搜索树的最近公共祖先
力扣官方题解
发布于 3 天前
14.9k
方法一：两次遍历
思路与算法

注意到题目中给出的是一棵「二叉搜索树」，因此我们可以快速地找出树中的某个节点以及从根节点到该节点的路径，例如我们需要找到节点 pp：

我们从根节点开始遍历；

如果当前节点就是 pp，那么成功地找到了节点；

如果当前节点的值大于 pp 的值，说明 pp 应该在当前节点的左子树，因此将当前节点移动到它的左子节点；

如果当前节点的值小于 pp 的值，说明 pp 应该在当前节点的右子树，因此将当前节点移动到它的右子节点。

对于节点 qq 同理。在寻找节点的过程中，我们可以顺便记录经过的节点，这样就得到了从根节点到被寻找节点的路径。

当我们分别得到了从根节点到 pp 和 qq 的路径之后，我们就可以很方便地找到它们的最近公共祖先了。显然，pp 和 qq 的最近公共祖先就是从根节点到它们路径上的「分岔点」，也就是最后一个相同的节点。因此，如果我们设从根节点到 pp 的路径为数组 \textit{path\_p}[]path_p[]，从根节点到 qq 的路径为数组 \textit{path\_q}[]path_q[]，那么只要找出最大的编号 ii，其满足

\textit{path\_p}[i] = \textit{path\_q}[i]
path_p[i]=path_q[i]

那么对应的节点就是「分岔点」，即 pp 和 qq 的最近公共祖先就是 \textit{path\_p}[i]path_p[i]（或 \textit{path\_q}[i]path_q[i]）。

代码


class Solution {
    public TreeNode lowestCommonAncestor(TreeNode root, TreeNode p, TreeNode q) {
        List<TreeNode> path_p = getPath(root, p);
        List<TreeNode> path_q = getPath(root, q);
        TreeNode ancestor = null;
        for (int i = 0; i < path_p.size() && i < path_q.size(); ++i) {
            if (path_p.get(i) == path_q.get(i)) {
                ancestor = path_p.get(i);
            } else {
                break;
            }
        }
        return ancestor;
    }

    public List<TreeNode> getPath(TreeNode root, TreeNode target) {
        List<TreeNode> path = new ArrayList<TreeNode>();
        TreeNode node = root;
        while (node != target) {
            path.add(node);
            if (target.val < node.val) {
                node = node.left;
            } else {
                node = node.right;
            }
        }
        path.add(node);
        return path;
    }
}
复杂度分析

时间复杂度：O(n)O(n)，其中 nn 是给定的二叉搜索树中的节点个数。上述代码需要的时间与节点 pp 和 qq 在树中的深度线性相关，而在最坏的情况下，树呈现链式结构，pp 和 qq 一个是树的唯一叶子结点，一个是该叶子结点的父节点，此时时间复杂度为 \Theta(n)Θ(n)。

空间复杂度：O(n)O(n)，我们需要存储根节点到 pp 和 qq 的路径。和上面的分析方法相同，在最坏的情况下，路径的长度为 \Theta(n)Θ(n)，因此需要 \Theta(n)Θ(n) 的空间。

方法二：一次遍历
思路与算法

在方法一中，我们对从根节点开始，通过遍历找出到达节点 pp 和 qq 的路径，一共需要两次遍历。我们也可以考虑将这两个节点放在一起遍历。

整体的遍历过程与方法一中的类似：

我们从根节点开始遍历；

如果当前节点的值大于 pp 和 qq 的值，说明 pp 和 qq 应该在当前节点的左子树，因此将当前节点移动到它的左子节点；

如果当前节点的值小于 pp 和 qq 的值，说明 pp 和 qq 应该在当前节点的右子树，因此将当前节点移动到它的右子节点；

如果当前节点的值不满足上述两条要求，那么说明当前节点就是「分岔点」。此时，pp 和 qq 要么在当前节点的不同的子树中，要么其中一个就是当前节点。

可以发现，如果我们将这两个节点放在一起遍历，我们就省去了存储路径需要的空间。

代码


class Solution {
    public TreeNode lowestCommonAncestor(TreeNode root, TreeNode p, TreeNode q) {
        TreeNode ancestor = root;
        while (true) {
            if (p.val < ancestor.val && q.val < ancestor.val) {
                ancestor = ancestor.left;
            } else if (p.val > ancestor.val && q.val > ancestor.val) {
                ancestor = ancestor.right;
            } else {
                break;
            }
        }
        return ancestor;
    }
}
复杂度分析

时间复杂度：O(n)O(n)，其中 nn 是给定的二叉搜索树中的节点个数。分析思路与方法一相同。

空间复杂度：O(1)O(1)。

public class Solution {
    public TreeNode LowestCommonAncestor(TreeNode root, TreeNode p, TreeNode q) {
        if(p.val < q.val){
            return GetNode(root, p, q);
        }

        return GetNode(root, q, p);
    }

    private TreeNode GetNode(TreeNode root, TreeNode p, TreeNode q){
        if(root == null){
            return null;
        }

        if(root.left == null || root.right == null){
            return root;
        }

        if(p.val <= root.val && q.val >= root.val){
            return root;
        }

        if(p.val < root.val && q.val < root.val){
            return GetNode(root.left, p, q);
        }

        return GetNode(root.right, p, q);
    }
}

public class Solution {
    public TreeNode LowestCommonAncestor(TreeNode root, TreeNode p, TreeNode q) {
        if (root == null || p == null || q == null) return null;

        var queue = new Queue<TreeNode>();
        queue.Enqueue(root);

        while (queue.Count != 0){
            var node = queue.Dequeue();

            if (node.val > p.val && node.val > q.val){
                //left
                if (node.left != null){
                    queue.Enqueue(node.left);
                }
            }
            else if (node.val < p.val && node.val < q.val){
                //right
                if (node.right != null){
                    queue.Enqueue(node.right);
                }
            }else{
                return node;
            }
        }

        return null;
    }
}


public class Solution {
    public TreeNode LowestCommonAncestor(TreeNode root, TreeNode p, TreeNode q) {
        if(p.val < root.val && q.val < root.val) return LowestCommonAncestor(root.left, p, q);
        if(p.val > root.val && q.val > root.val) return LowestCommonAncestor(root.right, p, q);
        return root;
    }
}

public class Solution {
public TreeNode LowestCommonAncestor(TreeNode root, TreeNode p, TreeNode q) {
    if ((root.val <= p.val && root.val >= q.val) || (root.val >= p.val && root.val <= q.val) )
    {
        return root;
    }
    else if (p.val < root.val && q.val < root.val)
    {
        return LowestCommonAncestor(root.left,p,q);
    }else
    {
        return LowestCommonAncestor(root.right,p,q);
    }
        
    }
}
     
*/
