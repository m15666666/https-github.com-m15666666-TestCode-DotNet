using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// https://leetcode-cn.com/problems/binary-search-tree-iterator/
/// 173. 二叉搜索树迭代器
/// 实现一个二叉搜索树迭代器。你将使用二叉搜索树的根节点初始化迭代器。
/// 调用 next() 将返回二叉搜索树中的下一个最小的数。
/// 注意: next() 和hasNext() 操作的时间复杂度是O(1)，并使用 O(h) 内存，其中 h 是树的高度。
/// </summary>
public class BSTIterator
{
    private Stack<TreeNode> _stack = new Stack<TreeNode>();

    public BSTIterator(TreeNode root)
    {
        _stack.Clear();
        PushNode(root);
    }

    /** @return whether we have a next smallest number */
    public bool HasNext()
    {
        return 0 < _stack.Count;
    }

    /** @return the next smallest number */
    public int Next()
    {
        if(0 < _stack.Count)
        {
            var n = _stack.Pop();
            PushNode(n.right);
            return n.val;
        }
        return 0;
    }

    private void PushNode(TreeNode root)
    {
        while (root != null)
        {
            _stack.Push(root);
            root = root.left;
        }
    }
}