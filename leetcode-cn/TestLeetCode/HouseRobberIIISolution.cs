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
打家劫舍 III
力扣官方题解
发布于 2020-08-04
36.2k
方法一：动态规划
思路与算法

简化一下这个问题：一棵二叉树，树上的每个点都有对应的权值，每个点有两种状态（选中和不选中），问在不能同时选中有父子关系的点的情况下，能选中的点的最大权值和是多少。

我们可以用 f(o)f(o) 表示选择 oo 节点的情况下，oo 节点的子树上被选择的节点的最大权值和；g(o)g(o) 表示不选择 oo 节点的情况下，oo 节点的子树上被选择的节点的最大权值和；ll 和 rr 代表 oo 的左右孩子。

当 oo 被选中时，oo 的左右孩子都不能被选中，故 oo 被选中情况下子树上被选中点的最大权值和为 ll 和 rr 不被选中的最大权值和相加，即 f(o) = g(l) + g(r)f(o)=g(l)+g(r)。
当 oo 不被选中时，oo 的左右孩子可以被选中，也可以不被选中。对于 oo 的某个具体的孩子 xx，它对 oo 的贡献是 xx 被选中和不被选中情况下权值和的较大值。故 g(o) = \max \{ f(l) , g(l)\}+\max\{ f(r) , g(r) \}g(o)=max{f(l),g(l)}+max{f(r),g(r)}。
至此，我们可以用哈希映射来存 ff 和 gg 的函数值，用深度优先搜索的办法后序遍历这棵二叉树，我们就可以得到每一个节点的 ff 和 gg。根节点的 ff 和 gg 的最大值就是我们要找的答案。

我们不难给出这样的实现：


class Solution {
    Map<TreeNode, Integer> f = new HashMap<TreeNode, Integer>();
    Map<TreeNode, Integer> g = new HashMap<TreeNode, Integer>();

    public int rob(TreeNode root) {
        dfs(root);
        return Math.max(f.getOrDefault(root, 0), g.getOrDefault(root, 0));
    }

    public void dfs(TreeNode node) {
        if (node == null) {
            return;
        }
        dfs(node.left);
        dfs(node.right);
        f.put(node, node.val + g.getOrDefault(node.left, 0) + g.getOrDefault(node.right, 0));
        g.put(node, Math.max(f.getOrDefault(node.left, 0), g.getOrDefault(node.left, 0)) + Math.max(f.getOrDefault(node.right, 0), g.getOrDefault(node.right, 0)));
    }
}
假设二叉树的节点个数为 nn。

我们可以看出，以上的算法对二叉树做了一次后序遍历，时间复杂度是 O(n)O(n)；由于递归会使用到栈空间，空间代价是 O(n)O(n)，哈希映射的空间代价也是 O(n)O(n)，故空间复杂度也是 O(n)O(n)。

我们可以做一个小小的优化，我们发现无论是 f(o)f(o) 还是 g(o)g(o)，他们最终的值只和 f(l)f(l)、g(l)g(l)、f(r)f(r)、g(r)g(r) 有关，所以对于每个节点，我们只关心它的孩子节点们的 ff 和 gg 是多少。我们可以设计一个结构，表示某个节点的 ff 和 gg 值，在每次递归返回的时候，都把这个点对应的 ff 和 gg 返回给上一级调用，这样可以省去哈希映射的空间。

代码如下。

代码


class Solution {
    public int rob(TreeNode root) {
        int[] rootStatus = dfs(root);
        return Math.max(rootStatus[0], rootStatus[1]);
    }

    public int[] dfs(TreeNode node) {
        if (node == null) {
            return new int[]{0, 0};
        }
        int[] l = dfs(node.left);
        int[] r = dfs(node.right);
        int selected = node.val + l[1] + r[1];
        int notSelected = Math.max(l[0], l[1]) + Math.max(r[0], r[1]);
        return new int[]{selected, notSelected};
    }
}
复杂度分析

时间复杂度：O(n)O(n)。上文中已分析。
空间复杂度：O(n)O(n)。虽然优化过的版本省去了哈希映射的空间，但是栈空间的使用代价依旧是 O(n)O(n)，故空间复杂度不变。

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
