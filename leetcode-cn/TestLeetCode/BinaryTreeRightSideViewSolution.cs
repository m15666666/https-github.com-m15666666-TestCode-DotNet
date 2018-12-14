using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


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
 // 别人的算法

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
