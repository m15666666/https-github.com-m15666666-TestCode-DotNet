using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
满二叉树是一类二叉树，其中每个结点恰好有 0 或 2 个子结点。

返回包含 N 个结点的所有可能满二叉树的列表。 答案的每个元素都是一个可能树的根结点。

答案中每个树的每个结点都必须有 node.val=0。

你可以按任何顺序返回树的最终列表。

示例：

输入：7
输出：[[0,0,0,null,null,0,0,null,null,0,0],[0,0,0,null,null,0,0,0,0],[0,0,0,0,0,0,0],[0,0,0,0,0,null,null,null,null,0,0],[0,0,0,0,0,null,null,0,0]]
解释：

提示：

1 <= N <= 20
*/
/// <summary>
/// https://leetcode-cn.com/problems/all-possible-full-binary-trees/
/// 894. 所有可能的满二叉树
/// 
/// </summary>
class AllPossibleFullBinaryTreesSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public IList<TreeNode> AllPossibleFBT(int N)
    {
        if (N == 1) return One;
        if ( N % 2 == 0 ) return Zero;

        if (_cache.ContainsKey(N)) return _cache[N];
        
        List<TreeNode> ret = new List<TreeNode>();
        for (int x = 0; x < N; x++)
        {
            int y = N - 1 - x;
            if (x % 2 == 0 || y % 2 == 0) continue;

            var xSubTree = AllPossibleFBT(x);
            var ySubTree = AllPossibleFBT(y);
            foreach (TreeNode left in xSubTree)
                foreach (TreeNode right in ySubTree)
                    ret.Add(new TreeNode(0)
                    {
                        left = left,
                        right = right
                    });
        }

        _cache.Add(N, ret);
        return ret;
    }

    private static readonly TreeNode[] Zero = new TreeNode[0];
    private static readonly TreeNode[] One = new TreeNode[1] { new TreeNode(0) };
    private readonly Dictionary<int, List<TreeNode>> _cache = new Dictionary<int, List<TreeNode>>();
}