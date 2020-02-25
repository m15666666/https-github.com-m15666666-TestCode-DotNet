using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给出二叉树的根节点 root，树上每个节点都有一个不同的值。

如果节点值在 to_delete 中出现，我们就把该节点从树上删去，最后得到一个森林（一些不相交的树构成的集合）。

返回森林中的每棵树。你可以按任意顺序组织答案。

 

示例：



输入：root = [1,2,3,4,5,6,7], to_delete = [3,5]
输出：[[1,2,null,4],[6],[7]]
 

提示：

树中的节点数最大为 1000。
每个节点都有一个介于 1 到 1000 之间的值，且各不相同。
to_delete.length <= 1000
to_delete 包含一些从 1 到 1000、各不相同的值。
*/
/// <summary>
/// https://leetcode-cn.com/problems/delete-nodes-and-return-forest/
/// 1110. 删点成林
/// 
/// </summary>
class DeleteNodesAndReturnForestSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public IList<TreeNode> DelNodes(TreeNode root, int[] to_delete)
    {
        if (root == null) return new TreeNode[0];

        _deleted = new HashSet<int>(to_delete);
        _list = new List<TreeNode>();

        root = Dfs(root);
        if (root != null) _list.Add(root);

        return _list;
    }

    private HashSet<int> _deleted;
    private List<TreeNode> _list;

    private TreeNode Dfs(TreeNode root)
    {
        if (root == null) return null;

        root.left = Dfs(root.left);
        root.right = Dfs(root.right);

        if (_deleted.Contains(root.val))
        {
            if (root.left != null) _list.Add(root.left);
            if (root.right != null) _list.Add(root.right);

            return null;
        }
        return root;
    }
}
/*
超简洁的后序遍历方法
hundanLi
发布于 3 个月前
402 阅读
class Solution {
    private Set<Integer> set = new HashSet<>();
    private List<TreeNode> list = new ArrayList<>();
    public List<TreeNode> delNodes(TreeNode root, int[] dels) {
        if (root == null) {
            return Collections.emptyList();
        }
        for (int del : dels) {
            set.add(del);
        }
        root = dfs(root);
        if (root != null) {
            list.add(root);
        }
        return list;
    }

    private TreeNode dfs(TreeNode root) {
        if (root == null) {
            return null;
        }
        root.left = dfs(root.left);
        root.right = dfs(root.right);

        if (set.contains(root.val)) {
            if (root.left != null) {
                list.add(root.left);
            }
            if (root.right != null) {
                list.add(root.right);
            }
            return null;
        }
        return root;
    }
}
下一篇：C++ dfs递归
 
*/
