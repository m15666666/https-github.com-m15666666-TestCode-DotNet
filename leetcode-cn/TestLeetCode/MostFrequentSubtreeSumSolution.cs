using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给出二叉树的根，找出出现次数最多的子树元素和。一个结点的子树元素和定义为以该结点为根的二叉树上所有结点的元素之和（包括结点本身）。然后求出出现次数最多的子树元素和。如果有多个元素出现的次数相同，返回所有出现次数最多的元素（不限顺序）。

 

示例 1
输入:

  5
 /  \
2   -3
返回 [2, -3, 4]，所有的值均只出现一次，以任意顺序返回所有值。

示例 2
输入:

  5
 /  \
2   -5
返回 [2]，只有 2 出现两次，-5 只出现 1 次。

 

提示： 假设任意子树元素和均可以用 32 位有符号整数表示。 
*/
/// <summary>
/// https://leetcode-cn.com/problems/most-frequent-subtree-sum/
/// 508. 出现次数最多的子树元素和
/// </summary>
class MostFrequentSubtreeSumSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int[] FindFrequentTreeSum(TreeNode root)
    {
        if (root == null) return new int[0];
        return new MostFrequentSubtreeSumHelper(root).FindFrequentTreeSum();
    }

    class MostFrequentSubtreeSumHelper
    {
        public MostFrequentSubtreeSumHelper(TreeNode root)
        {
            GetSum(root);
        }

        private int GetSum(TreeNode root)
        {
            if (root == null) return 0;
            if (_node2Sum.ContainsKey(root)) return _node2Sum[root];

            int ret = root.val + GetSum(root.left) + GetSum(root.right);
            _node2Sum.Add(root, ret);

            var count = 1;
            if (!_sum2count.ContainsKey(ret)) _sum2count.Add(ret, 1);
            else {
                count = _sum2count[ret] + 1;
                _sum2count[ret] = count;

                if (_maxCount < count) _maxCount = count;
            }
            
            return ret;
        }

        private Dictionary<int, int> _sum2count = new Dictionary<int, int>();
        private Dictionary<TreeNode, int> _node2Sum = new Dictionary<TreeNode, int>();
        private int _maxCount = 1;

        public int[] FindFrequentTreeSum()
        {
            return _sum2count.Where(item => item.Value == _maxCount).Select(item => item.Key).ToArray();
        }
    }
}
/*
public class Solution {
    Dictionary<int,int> treeSums;
    public int[] FindFrequentTreeSum(TreeNode root) {
        treeSums = new Dictionary<int, int>();
        List<int> result = new List<int>();
        if(root == null)
            return result.ToArray();
        TreeSum(root);
        int max = 0;
        foreach(int key in treeSums.Keys)
            if(treeSums[key]>max)
            {
                result.Clear();
                max = treeSums[key];
                result.Add(key);
            }
            else if(treeSums[key]==max)
                result.Add(key);
        return result.ToArray();
    }

    public int TreeSum(TreeNode root)
    {
        int res = root.val;
        if(root.left!=null)
            res+=TreeSum(root.left);
        if(root.right!=null)
            res+=TreeSum(root.right);
        if(treeSums.ContainsKey(res))
            treeSums[res]++;
        else treeSums.Add(res,1);
        return res;
    }
}

*/
