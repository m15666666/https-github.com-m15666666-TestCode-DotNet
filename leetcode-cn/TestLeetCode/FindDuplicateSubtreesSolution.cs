using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一棵二叉树，返回所有重复的子树。对于同一类的重复子树，你只需要返回其中任意一棵的根结点即可。

两棵树重复是指它们具有相同的结构以及相同的结点值。

示例 1：

        1
       / \
      2   3
     /   / \
    4   2   4
       /
      4
下面是两个重复的子树：

      2
     /
    4
和

    4
因此，你需要以列表的形式返回上述重复子树的根结点。 
*/
/// <summary>
/// https://leetcode-cn.com/problems/find-duplicate-subtrees/
/// 652. 寻找重复的子树
/// https://www.cnblogs.com/grandyang/p/7500082.html
/// </summary>
class FindDuplicateSubtreesSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public IList<TreeNode> FindDuplicateSubtrees(TreeNode root)
    {
        List<TreeNode> ret = new List<TreeNode>();
        Dictionary<string, int> counts = new Dictionary<string, int>();
        GetKey(root, ret, counts);
        return ret;
    }

    private static string GetKey(TreeNode root, List<TreeNode> ret, Dictionary<string,int> counts )
    {
        if (root == null) return "#";
        string key = $"{root.val}-{GetKey(root.left,ret,counts)}-{GetKey(root.right, ret, counts)}";
        if (!counts.ContainsKey(key)) counts.Add(key, 1);
        else
        {
            if(counts[key] == 1)
            {
                ret.Add(root);
            }
            counts[key]++;
        }
        return key;
    }
}