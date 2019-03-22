using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
在上次打劫完一条街道之后和一圈房屋后，小偷又发现了一个新的可行窃的地区。这个地区只有一个入口，我们称之为“根”。 除了“根”之外，每栋房子有且只有一个“父“房子与之相连。一番侦察之后，聪明的小偷意识到“这个地方的所有房屋的排列类似于一棵二叉树”。 如果两个直接相连的房子在同一天晚上被打劫，房屋将自动报警。

计算在不触动警报的情况下，小偷一晚能够盗取的最高金额。

示例 1:

输入: [3,2,3,null,3,null,1]

     3
    / \
   2   3
    \   \ 
     3   1

输出: 7 
解释: 小偷一晚能够盗取的最高金额 = 3 + 3 + 1 = 7.
示例 2:

输入: [3,4,5,1,3,null,1]

     3
    / \
   4   5
  / \   \ 
 1   3   1

输出: 9
解释: 小偷一晚能够盗取的最高金额 = 4 + 5 = 9. 
*/
/// <summary>
/// https://leetcode-cn.com/problems/house-robber-iii/
/// 337. 打家劫舍 III
/// https://blog.csdn.net/weixin_40673608/article/details/86668628
/// https://blog.csdn.net/xuchonghao/article/details/80700753
/// </summary>
class HouseRobberIIISolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int Rob(TreeNode root)
    {
        Dictionary<TreeNode, int> cache = new Dictionary<TreeNode, int>();
        return Rob(root, cache);
    }

    private int Rob(TreeNode root, Dictionary<TreeNode,int> cache )
    {
        if (root == null) return 0;
        if (cache.ContainsKey(root)) return cache[root];

        // 不包含根节点值
        int s0 = 0;

        // 包含根节点值，不包含左右子节点值，包含孙子节点值
        int s1 = root.val;

        var left = root.left;
        var right = root.right;

        s0 = Rob(left, cache) + Rob(right, cache);

        if (left != null) s1 += Rob(left.left, cache) + Rob(left.right, cache);
        if (right != null) s1 += Rob(right.left, cache) + Rob(right.right, cache);

        var ret = Math.Max(s0, s1);
        cache[root] = ret;
        return ret;
    }
}
/*
public class Solution {
    //返回当前节点为根节点的最大Rob钱数
    public int Helper(TreeNode root, ref int l, ref int r) {
        if(root == null) {
            return 0;    
        }
        
        int ll = 0, lr = 0, rl = 0, rr = 0;
        
        l = Helper(root.left, ref ll, ref lr);
        r = Helper(root.right, ref rl, ref rr);
        
        return Math.Max(l + r, root.val + ll + lr + rl + rr);
    }
    
    public int Rob(TreeNode root) {
        int l = 0, r = 0;
        
        return Helper(root, ref l, ref r);
    }
} 
*/
