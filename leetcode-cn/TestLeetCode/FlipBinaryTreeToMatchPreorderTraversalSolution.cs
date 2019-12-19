using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个有 N 个节点的二叉树，每个节点都有一个不同于其他节点且处于 {1, ..., N} 中的值。

通过交换节点的左子节点和右子节点，可以翻转该二叉树中的节点。

考虑从根节点开始的先序遍历报告的 N 值序列。将这一 N 值序列称为树的行程。

（回想一下，节点的先序遍历意味着我们报告当前节点的值，然后先序遍历左子节点，再先序遍历右子节点。）

我们的目标是翻转最少的树中节点，以便树的行程与给定的行程 voyage 相匹配。 

如果可以，则返回翻转的所有节点的值的列表。你可以按任何顺序返回答案。

如果不能，则返回列表 [-1]。

 

示例 1：



输入：root = [1,2], voyage = [2,1]
输出：[-1]
示例 2：



输入：root = [1,2,3], voyage = [1,3,2]
输出：[1]
示例 3：



输入：root = [1,2,3], voyage = [1,2,3]
输出：[]
 

提示：

1 <= N <= 100

来源：力扣（LeetCode）
链接：https://leetcode-cn.com/problems/flip-binary-tree-to-match-preorder-traversal
著作权归领扣网络所有。商业转载请联系官方授权，非商业转载请注明出处。给定一个有 N 个节点的二叉树，每个节点都有一个不同于其他节点且处于 {1, ..., N} 中的值。

通过交换节点的左子节点和右子节点，可以翻转该二叉树中的节点。

考虑从根节点开始的先序遍历报告的 N 值序列。将这一 N 值序列称为树的行程。

（回想一下，节点的先序遍历意味着我们报告当前节点的值，然后先序遍历左子节点，再先序遍历右子节点。）

我们的目标是翻转最少的树中节点，以便树的行程与给定的行程 voyage 相匹配。 

如果可以，则返回翻转的所有节点的值的列表。你可以按任何顺序返回答案。

如果不能，则返回列表 [-1]。

 

示例 1：



输入：root = [1,2], voyage = [2,1]
输出：[-1]
示例 2：



输入：root = [1,2,3], voyage = [1,3,2]
输出：[1]
示例 3：



输入：root = [1,2,3], voyage = [1,2,3]
输出：[]
 

提示：

1 <= N <= 100
*/
/// <summary>
/// https://leetcode-cn.com/problems/flip-binary-tree-to-match-preorder-traversal/
/// 971. 翻转二叉树以匹配先序遍历
/// 
/// </summary>
class FlipBinaryTreeToMatchPreorderTraversalSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public IList<int> FlipMatchVoyage(TreeNode root, int[] voyage)
    {
        flipped = new List<int>();
        index = 0;
        this.voyage = voyage;
        isFailed = false;

        Dfs(root);

        if (isFailed) return new int[] { -1 };

        return flipped;
    }

    private List<int> flipped;
    private int index = 0;
    private int[] voyage;
    private bool isFailed;

    private void Dfs(TreeNode node)
    {
        if (isFailed || node == null) return;

        if (node.val != voyage[index++])
        {
            isFailed = true;
            return;
        }

        var left = node.left;
        var right = node.right;
        if (left == null)
        {
            if (right == null) return;
            Dfs(right);
        }
        else
        {
            if (left.val != voyage[index])
            {
                if (right == null)
                {
                    isFailed = true;
                    return;
                }
                flipped.Add(node.val);
                (left, right) = (right, left);
            }
            Dfs(left);
            Dfs(right);
        }


        //if (node.left != null && node.left.val != voyage[index])
        //{
        //    flipped.Add(node.val);
        //    Dfs(node.right);
        //    Dfs(node.left);
        //}
        //else
        //{
        //    Dfs(node.left);
        //    Dfs(node.right);
        //}
    }
}
/*
方法：深度优先搜索
思路

当做先序遍历的时候，我们可能会翻转某一个节点，尝试使我们当前的遍历序列与给定的行程序列相匹配。

如果我们希望先序遍历序列的下一个数字是 voyage[i] ，那么至多只有一种可行的遍历路径供我们选择，因为所有节点的值都不相同。

算法

进行深度优先遍历。如果遍历到某一个节点的时候，节点值不能与行程序列匹配，那么答案一定是 [-1]。

否则，当行程序列中的下一个期望数字 voyage[i] 与我们即将遍历的子节点的值不同的时候，我们就要翻转一下当前这个节点。

JavaPython
class Solution {
    List<Integer> flipped;
    int index;
    int[] voyage;

    public List<Integer> flipMatchVoyage(TreeNode root, int[] voyage) {
        flipped = new ArrayList();
        index = 0;
        this.voyage = voyage;

        dfs(root);
        if (!flipped.isEmpty() && flipped.get(0) == -1) {
            flipped.clear();
            flipped.add(-1);
        }

        return flipped;
    }

    public void dfs(TreeNode node) {
        if (node != null) {
            if (node.val != voyage[index++]) {
                flipped.clear();
                flipped.add(-1);
                return;
            }

            if (index < voyage.length && node.left != null &&
                    node.left.val != voyage[index]) {
                flipped.add(node.val);
                dfs(node.right);
                dfs(node.left);
            } else {
                dfs(node.left);
                dfs(node.right);
            }
        }
    }
}
复杂度分析

时间复杂度：O(N)O(N)，其中 NN 是给定树中节点的数量。

空间复杂度：O(N)O(N)。
 
*/
