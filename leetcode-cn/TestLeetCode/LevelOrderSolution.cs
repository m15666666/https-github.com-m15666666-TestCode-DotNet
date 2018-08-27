using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class TreeNode
{
     public int val;
     public TreeNode left;
     public TreeNode right;
     public TreeNode(int x) { val = x; }
}


class LevelOrderSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public IList<IList<int>> LevelOrder(TreeNode root)
    {
        InnerLevelOrderSolution innerLevelOrderSolution = new InnerLevelOrderSolution();
        innerLevelOrderSolution.LevelOrder( root, 1);
        return innerLevelOrderSolution.List;
    }

    class InnerLevelOrderSolution
    {
        public List<IList<int>> List = new List<IList<int>>();

        public void LevelOrder(TreeNode root, int level)
        {
            if (root == null) return;

            if (List.Count < level)
            {
                List.Add(new List<int>());
            }
            List[level - 1].Add(root.val);

            if (root.left != null) LevelOrder(root.left, level + 1);
            if (root.right != null) LevelOrder(root.right, level + 1);
        }
    }
}

