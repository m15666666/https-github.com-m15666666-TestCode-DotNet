using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一棵二叉树，想象自己站在它的右侧，按照从顶部到底部的顺序，返回从右侧所能看到的节点值。

示例:

输入: [1,2,3,null,5,null,4]
输出: [1, 3, 4]
解释:

   1            <---
 /   \
2     3         <---
 \     \
  5     4       <---

 
*/
/// <summary>
/// https://leetcode-cn.com/problems/binary-tree-right-side-view/
/// 199. 二叉树的右视图
/// 给定一棵二叉树，想象自己站在它的右侧，按照从顶部到底部的顺序，返回从右侧所能看到的节点值。
/// </summary>
class BinaryTreeRightSideViewSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    public IList<int> RightSideView(TreeNode root)
    {
        List<int> ret = new List<int>();
        if( root != null ) RightSideView(root, 1, ret);
        return ret;
    }

    private void RightSideView( TreeNode root, int level, List<int> ret )
    {
        if( ret.Count < level ) ret.Add(root.val);
        if( root.right != null ) RightSideView( root.right, level + 1, ret );
        if (root.left != null) RightSideView(root.left, level + 1, ret);
    }
}

/*

二叉树的右视图
力扣官方题解
发布于 2020-04-21
26.8k
📺 视频题解

📖 文字题解
初步想法
由于树的形状无法提前知晓，不可能设计出优于 O(n)O(n) 的算法。因此，我们应该试着寻找线性时间解。带着这个想法，我们来考虑一些同等有效的方案。

方法一：深度优先搜索
思路

我们对树进行深度优先搜索，在搜索过程中，我们总是先访问右子树。那么对于每一层来说，我们在这层见到的第一个结点一定是最右边的结点。

算法

这样一来，我们可以存储在每个深度访问的第一个结点，一旦我们知道了树的层数，就可以得到最终的结果数组。

fig1

上图表示了问题的一个实例。红色结点自上而下组成答案，边缘以访问顺序标号。


class Solution {
    public List<Integer> rightSideView(TreeNode root) {
        Map<Integer, Integer> rightmostValueAtDepth = new HashMap<Integer, Integer>();
        int max_depth = -1;

        Stack<TreeNode> nodeStack = new Stack<TreeNode>();
        Stack<Integer> depthStack = new Stack<Integer>();
        nodeStack.push(root);
        depthStack.push(0);

        while (!nodeStack.isEmpty()) {
            TreeNode node = nodeStack.pop();
            int depth = depthStack.pop();

            if (node != null) {
            	// 维护二叉树的最大深度
                max_depth = Math.max(max_depth, depth);

                // 如果不存在对应深度的节点我们才插入
                if (!rightmostValueAtDepth.containsKey(depth)) {
                    rightmostValueAtDepth.put(depth, node.val);
                }

                nodeStack.push(node.left);
                nodeStack.push(node.right);
                depthStack.push(depth+1);
                depthStack.push(depth+1);
            }
        }

        List<Integer> rightView = new ArrayList<Integer>();
        for (int depth = 0; depth <= max_depth; depth++) {
            rightView.add(rightmostValueAtDepth.get(depth));
        }

        return rightView;
    }
}
复杂度分析

时间复杂度 : O(n)O(n)。深度优先搜索最多访问每个结点一次，因此是线性复杂度。

空间复杂度 : O(n)O(n)。最坏情况下，栈内会包含接近树高度的结点数量，占用 {O}(n)O(n) 的空间。

方法二：广度优先搜索
思路

我们可以对二叉树进行层次遍历，那么对于每层来说，最右边的结点一定是最后被遍历到的。二叉树的层次遍历可以用广度优先搜索实现。

算法

执行广度优先搜索，左结点排在右结点之前，这样，我们对每一层都从左到右访问。因此，只保留每个深度最后访问的结点，我们就可以在遍历完整棵树后得到每个深度最右的结点。除了将栈改成队列，并去除了rightmost_value_at_depth之前的检查外，算法没有别的改动。

fig2

上图表示了同一个示例，红色结点自上而下组成答案，边缘以访问顺序标号。


class Solution {
    public List<Integer> rightSideView(TreeNode root) {
        Map<Integer, Integer> rightmostValueAtDepth = new HashMap<Integer, Integer>();
        int max_depth = -1;

        Queue<TreeNode> nodeQueue = new LinkedList<TreeNode>();
        Queue<Integer> depthQueue = new LinkedList<Integer>();
        nodeQueue.add(root);
        depthQueue.add(0);

        while (!nodeQueue.isEmpty()) {
            TreeNode node = nodeQueue.remove();
            int depth = depthQueue.remove();

            if (node != null) {
            	// 维护二叉树的最大深度
                max_depth = Math.max(max_depth, depth);

                // 由于每一层最后一个访问到的节点才是我们要的答案，因此不断更新对应深度的信息即可
                rightmostValueAtDepth.put(depth, node.val);

                nodeQueue.add(node.left);
                nodeQueue.add(node.right);
                depthQueue.add(depth+1);
                depthQueue.add(depth+1);
            }
        }

        List<Integer> rightView = new ArrayList<Integer>();
        for (int depth = 0; depth <= max_depth; depth++) {
            rightView.add(rightmostValueAtDepth.get(depth));
        }

        return rightView;
    }
}
复杂度分析

时间复杂度 : {O}(n)O(n)。 每个节点最多进队列一次，出队列一次，因此广度优先搜索的复杂度为线性。

空间复杂度 : {O}(n)O(n)。每个节点最多进队列一次，所以队列长度最大不不超过 nn，所以这里的空间代价为 O(n)O(n)。

注释

deque 数据类型来自于collections 模块，支持从头和尾部的常数时间 append/pop 操作。若使用 Python 的 list，通过 list.pop(0) 去除头部会消耗 O(n)O(n) 的时间。



public class Solution {
    public IList<int> list = new List<int>();
    
    public IList<int> RightSideView(TreeNode root, int layer = 0) {
        if(root == null)
            return list;
        
        while(list.Count <= layer)
            list.Add(0);
        
        list[layer] = root.val;
        
        RightSideView(root.left, layer + 1);
        RightSideView(root.right, layer + 1);
        
        return list;
    }
}

public class Solution {
    List<int> ret;
    void dfs(TreeNode tn, int dep) {
        if(dep >= ret.Count) ret.Add(tn.val);
        else ret[dep] = tn.val;
        if(tn.left != null) dfs(tn.left, dep + 1);
        if(tn.right != null) dfs(tn.right, dep + 1);
    }
    public IList<int> RightSideView(TreeNode root) {
        ret = new List<int>();
        if(root != null) dfs(root, 0);
        return ret;
    }
}
*/
