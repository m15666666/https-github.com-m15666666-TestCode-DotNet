using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个二叉树（具有根结点 root）， 一个目标结点 target ，和一个整数值 K 。

返回到目标结点 target 距离为 K 的所有结点的值的列表。 答案可以以任何顺序返回。

 

示例 1：

输入：root = [3,5,1,6,2,0,8,null,null,7,4], target = 5, K = 2

输出：[7,4,1]

解释：
所求结点为与目标结点（值为 5）距离为 2 的结点，
值分别为 7，4，以及 1



注意，输入的 "root" 和 "target" 实际上是树上的结点。
上面的输入仅仅是对这些对象进行了序列化描述。
 

提示：

给定的树是非空的，且最多有 K 个结点。
树上的每个结点都具有唯一的值 0 <= node.val <= 500 。
目标结点 target 是树上的结点。
0 <= K <= 1000.
*/
/// <summary>
/// https://leetcode-cn.com/problems/all-nodes-distance-k-in-binary-tree/
/// 863. 二叉树中所有距离为 K 的结点
/// 
/// </summary>
class AllNodesDistanceKInBinaryTreeSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    public IList<int> DistanceK(TreeNode root, TreeNode target, int K)
    {
        _parents = new Dictionary<TreeNode, TreeNode>();
        Dfs(root, null);

        Queue<TreeNode> queue = new Queue<TreeNode>();
        queue.Enqueue(target);

        var visited = new HashSet<TreeNode>();
        visited.Add(target);

        int dist = 0;
        while (0 < queue.Count)
        {
            if (dist++ == K) return queue.Select(item => item.val).ToArray();

            int n = queue.Count;
            for (int i = 0; i < n; i++) {
                TreeNode node = queue.Dequeue();

                var left = node.left;
                if (left != null && !visited.Contains(left))
                {
                    visited.Add(left);
                    queue.Enqueue(left);
                }

                var right = node.right;
                if (right != null && !visited.Contains(right))
                {
                    visited.Add(right);
                    queue.Enqueue(right);
                }
                TreeNode parent = _parents[node];
                if (parent != null && !visited.Contains(parent))
                {
                    visited.Add(parent);
                    queue.Enqueue(parent);
                }
            }
        }

        return new int[0];
    }

    private Dictionary<TreeNode, TreeNode> _parents;
    private void Dfs(TreeNode node, TreeNode parent)
    {
        if (node != null)
        {
            _parents.Add(node, parent);
            Dfs(node.left, node);
            Dfs(node.right, node);
        }
    }
}
/*
方法一： 深度优先搜索
思路

如果节点有指向父节点的引用，也就知道了距离该节点 1 距离的所有节点。之后就可以从 target 节点开始进行深度优先搜索了。

算法
对所有节点添加一个指向父节点的引用，之后做深度优先搜索，找到所有距离 target 节点 K 距离的节点。

JavaPython
class Solution {
    Map<TreeNode, TreeNode> parent;
    public List<Integer> distanceK(TreeNode root, TreeNode target, int K) {
        parent = new HashMap();
        dfs(root, null);

        Queue<TreeNode> queue = new LinkedList();
        queue.add(null);
        queue.add(target);

        Set<TreeNode> seen = new HashSet();
        seen.add(target);
        seen.add(null);

        int dist = 0;
        while (!queue.isEmpty()) {
            TreeNode node = queue.poll();
            if (node == null) {
                if (dist == K) {
                    List<Integer> ans = new ArrayList();
                    for (TreeNode n: queue)
                        ans.add(n.val);
                    return ans;
                }
                queue.offer(null);
                dist++;
            } else {
                if (!seen.contains(node.left)) {
                    seen.add(node.left);
                    queue.offer(node.left);
                }
                if (!seen.contains(node.right)) {
                    seen.add(node.right);
                    queue.offer(node.right);
                }
                TreeNode par = parent.get(node);
                if (!seen.contains(par)) {
                    seen.add(par);
                    queue.offer(par);
                }
            }
        }

        return new ArrayList<Integer>();
    }

    public void dfs(TreeNode node, TreeNode par) {
        if (node != null) {
            parent.put(node, par);
            dfs(node.left, node);
            dfs(node.right, node);
        }
    }
}
复杂度分析

时间复杂度： O(N)O(N)，其中 NN 是树中节点个数。

空间复杂度： O(N)O(N)。

方法二： 计算节点之间距离
思路

如果 target 节点在 root 节点的左子树中，且 target 节点深度为 3，那所有 root 节点右子树中深度为 K - 3 的节点到 target 的距离就都是 K。

算法

深度优先遍历所有节点。定义方法 dfs(node)，这个函数会返回 node 到 target 的距离。在 dfs(node) 中处理下面四种情况：

如果 node == target，把子树中距离 target 节点距离为 K 的所有节点加入答案。

如果 target 在 node 左子树中，假设 target 距离 node 的距离为 L+1，找出右子树中距离 target 节点 K - L - 1 距离的所有节点加入答案。

如果 target 在 node 右子树中，跟在左子树中一样的处理方法。

如果 target 不在节点的子树中，不用处理。

实现的算法中，还会用到一个辅助方法 subtree_add(node, dist)，这个方法会将子树中距离节点 node K - dist 距离的节点加入答案。

JavaPython
class Solution {
    List<Integer> ans;
    TreeNode target;
    int K;
    public List<Integer> distanceK(TreeNode root, TreeNode target, int K) {
        ans = new LinkedList();
        this.target = target;
        this.K = K;
        dfs(root);
        return ans;
    }

    // Return vertex distance from node to target if exists, else -1
    // Vertex distance: the number of vertices on the path from node to target
    public int dfs(TreeNode node) {
        if (node == null)
            return -1;
        else if (node == target) {
            subtree_add(node, 0);
            return 1;
        } else {
            int L = dfs(node.left), R = dfs(node.right);
            if (L != -1) {
                if (L == K) ans.add(node.val);
                subtree_add(node.right, L + 1);
                return L + 1;
            } else if (R != -1) {
                if (R == K) ans.add(node.val);
                subtree_add(node.left, R + 1);
                return R + 1;
            } else {
                return -1;
            }
        }
    }

    // Add all nodes 'K - dist' from the node to answer.
    public void subtree_add(TreeNode node, int dist) {
        if (node == null) return;
        if (dist == K)
            ans.add(node.val);
        else {
            subtree_add(node.left, dist + 1);
            subtree_add(node.right, dist + 1);
        }
    }
}
复杂度分析

时间复杂度： O(N)O(N)，其中 NN 树的大小。

空间复杂度： O(N)O(N)。


public class Solution {
    Dictionary<int,int> dict = new Dictionary<int,int>();
    public IList<int> DistanceK(TreeNode root, TreeNode target, int K) {
        
        List<int> ret= new List<int>();
        dfs(root,target);
        find(ret,root,K,dict[root.val]);
        return ret;
    }
    
    private int dfs(TreeNode r, TreeNode tar)
    {
        if(r==null)
            return -1;
        if(r==tar)
        {
            dict[tar.val]=0;
            return 0;
        }
        
        int left = dfs(r.left,tar);
        int right=dfs(r.right,tar);
        if(left!=-1)
        {
            dict[r.val]=left+1;
            return left+1;
        }
        if(right != -1)
        {
            dict[r.val]=right+1;
            return right+1;
        }
        return -1;
    }
    
    private void find(List<int> ret, TreeNode r,int K, int d)
    {
        if(r==null)
            return;
        if(dict.ContainsKey(r.val))
            d=dict[r.val];
        if(d==K)
            ret.Add(r.val);
        find(ret,r.left,K,d+1);
        find(ret,r.right,K,d+1);
    }
} 
*/
