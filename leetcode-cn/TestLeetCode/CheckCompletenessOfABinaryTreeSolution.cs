using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个二叉树，确定它是否是一个完全二叉树。

百度百科中对完全二叉树的定义如下：

若设二叉树的深度为 h，除第 h 层外，其它各层 (1～h-1) 的结点数都达到最大个数，第 h 层所有的结点都连续集中在最左边，这就是完全二叉树。（注：第 h 层可能包含 1~ 2h 个节点。）

 

示例 1：



输入：[1,2,3,4,5,6]
输出：true
解释：最后一层前的每一层都是满的（即，结点值为 {1} 和 {2,3} 的两层），且最后一层中的所有结点（{4,5,6}）都尽可能地向左。
示例 2：



输入：[1,2,3,4,5,null,7]
输出：false
解释：值为 7 的结点没有尽可能靠向左侧。
 

提示：

树中将会有 1 到 100 个结点。
*/
/// <summary>
/// https://leetcode-cn.com/problems/check-completeness-of-a-binary-tree/
/// 958. 二叉树的完全性检验
/// 
/// </summary>
class CheckCompletenessOfABinaryTreeSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public bool IsCompleteTree(TreeNode root)
    {
        if (root == null) return true;

        Queue<TreeNode> queue = new Queue<TreeNode>();
        queue.Enqueue(root);
        bool isNull = false;
        while( 0 < queue.Count)
        {
            int count = queue.Count;
            for( int i = 0; i < count; i++ )
            {
                var node = queue.Dequeue();
                var left = node.left;
                var right = node.right;

                if (left == null && right != null) return false;
                
                if (isNull)
                {
                    if (left != null || right != null) return false;
                    continue;
                }

                if ( left == null ) isNull = true;
                else queue.Enqueue(left);

                if (right == null) isNull = true;
                else queue.Enqueue(right);
            }
        }
        return true;
    }
}
/*
方法 1：广度优先搜索
想法

这个问题可以简化成两个小问题：用 (depth, position) 元组表示每个节点的”位置“；确定如何定义所有节点都是在最左边的。

假如我们在深度为 3 的行有 4 个节点，位置为 0，1，2，3；那么就有 8 个深度为 4 的新节点位置在 0，1，2，3，4，5，6，7；所以我们可以找到规律：对于一个节点，它的左孩子为：(depth, position) -> (depth + 1, position * 2)，右孩子为 (depth, position) -> (depth + 1, position * 2 + 1)。所以，对于深度为 d 的行恰好含有 2^{d-1}2 
d−1
  个节点，所有节点都是靠左边排列的当他们的位置编号是 0, 1, ... 且没有间隙。

一个更简单的表示深度和位置的方法是：用 1 表示根节点，对于任意一个节点 v，它的左孩子为 2*v 右孩子为 2*v + 1。这就是我们用的规则，在这个规则下，一颗二叉树是完全二叉树当且仅当节点编号依次为 1, 2, 3, ... 且没有间隙。

算法

对于根节点，我们定义其编号为 1。然后，对于每个节点 v，我们将其左节点编号为 2 * v，将其右节点编号为 2 * v + 1。

我们可以发现，树中所有节点的编号按照广度优先搜索顺序正好是升序。（也可以使用深度优先搜索，之后对序列排序）。

然后，我们检测编号序列是否为无间隔的 1, 2, 3, …，事实上，我们只需要检查最后一个编号是否正确，因为最后一个编号的值最大。

JavaPython
class Solution {
    public boolean isCompleteTree(TreeNode root) {
        List<ANode> nodes = new ArrayList();
        nodes.add(new ANode(root, 1));
        int i = 0;
        while (i < nodes.size()) {
            ANode anode = nodes.get(i++);
            if (anode.node != null) {
                nodes.add(new ANode(anode.node.left, anode.code * 2));
                nodes.add(new ANode(anode.node.right, anode.code * 2 + 1));
            }
        }

        return nodes.get(i-1).code == nodes.size();
    }
}

class ANode {  // Annotated Node
    TreeNode node;
    int code;
    ANode(TreeNode node, int code) {
        this.node = node;
        this.code = code;
    }
}
复杂度分析

时间复杂度：O(N)O(N)，其中 NN 是树节点个数。
空间复杂度：O(N)O(N)。
 
public class Solution {
    public bool IsCompleteTree(TreeNode root) {
        if (root == null)
            return false;
        
        if (root.left == null && root.right == null)
            return true;
        
        List<TreeNode> level = new List<TreeNode>();
        level.Add(root);
        bool parentHasNull = false;
        bool isLastLevel = false;
        
        while (level.Any())
        {
            bool hasNull = false;
            List<TreeNode> children = new List<TreeNode>();
            
            foreach (var node in level)
            {
                if (hasNull && (node.left != null || node.right != null))
                    return false;
                    
                if (node.left != null)
                    children.Add(node.left);
                else
                    hasNull = true;
                
                if (node.right != null)
                {
                    if (hasNull)
                        return false;
                    else
                        children.Add(node.right);
                }                 
                else
                    hasNull = true;
            }
            
            if (isLastLevel && children.Any())
                return false;
            
            if (children.Any() && hasNull)
                isLastLevel = true;
            
            level = children;
        }
        
        return true;
    }
}

public class Solution {
    public bool IsCompleteTree(TreeNode root) {
        Queue<TreeNode> que = new Queue<TreeNode>();
        
        que.Enqueue(root);
        
        while(que.Count != 0){
            TreeNode curr = que.Dequeue();
            
            if(curr.left == null){
                if(curr.right != null)
                    return false;
                
                break;
            }
            que.Enqueue(curr.left);
            
            if(curr.right == null){
                break;
            }
            que.Enqueue(curr.right);
        }
        
        while(que.Count != 0){
            TreeNode curr = que.Dequeue();
            
            if(curr.left != null || curr.right != null)
                return false;
        }
        
        return true;
    }
}
*/
