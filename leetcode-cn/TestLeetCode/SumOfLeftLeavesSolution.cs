/*
计算给定二叉树的所有左叶子之和。

示例：

    3
   / \
  9  20
    /  \
   15   7

在这个二叉树中，有两个左叶子，分别是 9 和 15，所以返回 24

*/

/// <summary>
/// https://leetcode-cn.com/problems/sum-of-left-leaves/
/// 404. 左叶子之和
///
///
/// </summary>
internal class SumOfLeftLeavesSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int SumOfLeftLeaves(TreeNode root)
    {
        return root != null ? Dfs(root) : 0;

        static int Dfs(TreeNode node)
        {
            int ret = 0;
            if (node.left != null) ret += IsLeaf(node.left) ? node.left.val : Dfs(node.left);
            if (node.right != null && !IsLeaf(node.right)) ret += Dfs(node.right);
            return ret;
        }

        static bool IsLeaf(TreeNode node) => node.left == null && node.right == null;
    }
}

/*
左叶子之和
力扣官方题解
发布于 2020-09-19
18.8k
前言
一个节点为「左叶子」节点，当且仅当它是某个节点的左子节点，并且它是一个叶子结点。因此我们可以考虑对整棵树进行遍历，当我们遍历到节点 \textit{node}node 时，如果它的左子节点是一个叶子结点，那么就将它的左子节点的值累加计入答案。

遍历整棵树的方法有深度优先搜索和广度优先搜索，下面分别给出了实现代码。

方法一：深度优先搜索

class Solution {
    public int sumOfLeftLeaves(TreeNode root) {
        return root != null ? dfs(root) : 0;
    }

    public int dfs(TreeNode node) {
        int ans = 0;
        if (node.left != null) {
            ans += isLeafNode(node.left) ? node.left.val : dfs(node.left);
        }
        if (node.right != null && !isLeafNode(node.right)) {
            ans += dfs(node.right);
        }
        return ans;
    }

    public boolean isLeafNode(TreeNode node) {
        return node.left == null && node.right == null;
    }
}
复杂度分析

时间复杂度：O(n)O(n)，其中 nn 是树中的节点个数。

空间复杂度：O(n)O(n)。空间复杂度与深度优先搜索使用的栈的最大深度相关。在最坏的情况下，树呈现链式结构，深度为 O(n)O(n)，对应的空间复杂度也为 O(n)O(n)。

方法二：广度优先搜索

class Solution {
    public int sumOfLeftLeaves(TreeNode root) {
        if (root == null) {
            return 0;
        }

        Queue<TreeNode> queue = new LinkedList<TreeNode>();
        queue.offer(root);
        int ans = 0;
        while (!queue.isEmpty()) {
            TreeNode node = queue.poll();
            if (node.left != null) {
                if (isLeafNode(node.left)) {
                    ans += node.left.val;
                } else {
                    queue.offer(node.left);
                }
            }
            if (node.right != null) {
                if (!isLeafNode(node.right)) {
                    queue.offer(node.right);
                }
            }
        }
        return ans;
    }

    public boolean isLeafNode(TreeNode node) {
        return node.left == null && node.right == null;
    }
}
复杂度分析

时间复杂度：O(n)O(n)，其中 nn 是树中的节点个数。

空间复杂度：O(n)O(n)。空间复杂度与广度优先搜索使用的队列需要的容量相关，为 O(n)O(n)。

public class Solution {
    public int SumOfLeftLeaves(TreeNode root) {
          List<int> list = new List<int>();

            GetValue(root, list);

            int sum = 0;
            for (var i = 0; i < list.Count; i++)
            {
                sum = sum + list[i];
            }

            return sum;
    }
     public int GetValue(TreeNode root,List<int> list)
        {
           
            if (root == null) return 0;

            if (root.left != null&&(root.left.left==null&&root.left.right==null))
            {
                list.Add(root.left.val);
            }

            GetValue(root.left, list);
            GetValue(root.right, list);

            return 0;
        }
}

public class Solution {

   
    public int SumOfLeftLeaves(TreeNode root) {
         List<int> nums=new List<int>();
        int sum=0;
        dfs(root,nums,0);
        foreach(int x in nums)
        {
           sum+=x;
        }
        return sum;

    }
    private void dfs(TreeNode node,List<int> nums,int a)
    {
        if(node==null)return;
        dfs(node.left,nums,1);
        if(node.left==null&&node.right==null&&a==1)
        nums.Add(node.val);
        dfs(node.right,nums,2);
    }
}

public class Solution {
    public int SumOfLeftLeaves(TreeNode root) {
        if (root == null) return 0;
        int leftSum = 0;
        if (root.left != null && root.left.left == null && root.left.right == null)
            leftSum += root.left.val;
        return leftSum + SumOfLeftLeaves(root.left) + SumOfLeftLeaves(root.right);
    }
}

public class Solution {
    public int SumOfLeftLeaves(TreeNode root) {
        int sum = 0;
        if(root != null){
            if(root.left != null && root.left.left == null && root.left.right == null)
                sum += root.left.val;
            sum += SumOfLeftLeaves(root.left) + SumOfLeftLeaves(root.right);
        }
        return sum;
    }
}


*/