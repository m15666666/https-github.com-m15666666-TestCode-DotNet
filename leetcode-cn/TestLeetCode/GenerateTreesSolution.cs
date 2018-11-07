using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// https://leetcode-cn.com/problems/unique-binary-search-trees-ii/
/// 95.不同的二叉搜索树II
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
        List<TreeNode>[,] cache = new List<TreeNode>[n + 2, n + 2];
        return GenerateTrees(1, n, cache);
    }

    private List<TreeNode> GenerateTrees(int start, int end, List<TreeNode>[,] cache )
    {
        List<TreeNode> ret = new List<TreeNode>();
        if (end < start)
        {
            ret.Add(null);
            return ret;
        }
        for (int i = start; i <= end; i++)
        {
            List<TreeNode> list1 = cache[start,i - 1];
            if (list1 == null)
            {
                //递归，并将结果保存下来
                list1 = GenerateTrees(start, i - 1, cache);
                cache[start,i - 1] = list1;
            }
            //（start,i-1）为左子树，遍历不同的左子树组合
            foreach (TreeNode left in list1)
            {
                List<TreeNode> list2 = cache[i + 1,end];//当 i = n 时，就需要求 cache[n+1,end] 的值
                if (list2 == null)
                {
                    //递归，并将结果保存下来
                    list2 = GenerateTrees(i + 1, end, cache);
                    cache[i + 1,end] = list2;
                }
                //（i+1,end）为右子树，遍历不同的右子树组合
                foreach (TreeNode right in list2)
                {
                    TreeNode root = new TreeNode(i);
                    root.left = left;
                    root.right = right;
                    ret.Add(root);
                }
            }
        }
        return ret;
    }
}