using System.Collections.Generic;
using System.Text;

/*
给定一个二叉树，返回所有从根节点到叶子节点的路径。

说明: 叶子节点是指没有子节点的节点。

示例:

输入:

   1
 /   \
2     3
 \
  5

输出: ["1->2->5", "1->3"]

解释: 所有根节点到叶子节点的路径为: 1->2->5, 1->3


*/

/// <summary>
/// https://leetcode-cn.com/problems/binary-tree-paths/
/// 257. 二叉树的所有路径
/// 
///
///
///
/// </summary>
internal class BinaryTreePathsSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public IList<string> BinaryTreePaths(TreeNode root) {
        List<string> ret = new List<string>();
        if (root == null) return ret;

        StringBuilder builder = new StringBuilder(1024);
        Stack<int> stack = new Stack<int>();
        Dfs(stack, root);
        return ret;

        void Dfs(Stack<int> stack, TreeNode node)
        {
            if(node.left == null && node.right == null)
            {
                builder.Length = 0;
                foreach( var v in stack)
                    builder.Insert(0, "->").Insert(0, v);
                builder.Append(node.val);
                ret.Add(builder.ToString());
                return;
            }
            stack.Push(node.val);

            if(node.left != null) Dfs(stack, node.left);
            if(node.right != null) Dfs(stack, node.right);

            stack.Pop();
        }

    }

}

/*
二叉树的所有路径
力扣官方题解
发布于 2020-09-03
20.6k
方法一：深度优先搜索
思路与算法

最直观的方法是使用深度优先搜索。在深度优先搜索遍历二叉树时，我们需要考虑当前的节点以及它的孩子节点。

如果当前节点不是叶子节点，则在当前的路径末尾添加该节点，并继续递归遍历该节点的每一个孩子节点。
如果当前节点是叶子节点，则在当前路径末尾添加该节点后我们就得到了一条从根节点到叶子节点的路径，将该路径加入到答案即可。
如此，当遍历完整棵二叉树以后我们就得到了所有从根节点到叶子节点的路径。当然，深度优先搜索也可以使用非递归的方式实现，这里不再赘述。

代码


class Solution {
    public List<String> binaryTreePaths(TreeNode root) {
        List<String> paths = new ArrayList<String>();
        constructPaths(root, "", paths);
        return paths;
    }

    public void constructPaths(TreeNode root, String path, List<String> paths) {
        if (root != null) {
            StringBuffer pathSB = new StringBuffer(path);
            pathSB.append(Integer.toString(root.val));
            if (root.left == null && root.right == null) {  // 当前节点是叶子节点
                paths.add(pathSB.toString());  // 把路径加入到答案中
            } else {
                pathSB.append("->");  // 当前节点不是叶子节点，继续递归遍历
                constructPaths(root.left, pathSB.toString(), paths);
                constructPaths(root.right, pathSB.toString(), paths);
            }
        }
    }
}
复杂度分析

时间复杂度：O(N^2)O(N 
2
 )，其中 NN 表示节点数目。在深度优先搜索中每个节点会被访问一次且只会被访问一次，每一次会对 path 变量进行拷贝构造，时间代价为 O(N)O(N)，故时间复杂度为 O(N^2)O(N 
2
 )。

空间复杂度：O(N^2)O(N 
2
 )，其中 NN 表示节点数目。除答案数组外我们需要考虑递归调用的栈空间。在最坏情况下，当二叉树中每个节点只有一个孩子节点时，即整棵二叉树呈一个链状，此时递归的层数为 NN，此时每一层的 path 变量的空间代价的总和为 O(\sum_{i = 1}^{N} i) = O(N^2)O(∑ 
i=1
N
​	
 i)=O(N 
2
 ) 空间复杂度为 O(N^2)O(N 
2
 )。最好情况下，当二叉树为平衡二叉树时，它的高度为 \log NlogN，此时空间复杂度为 O((\log {N})^2)O((logN) 
2
 )。

方法二：广度优先搜索
思路与算法

我们也可以用广度优先搜索来实现。我们维护一个队列，存储节点以及根到该节点的路径。一开始这个队列里只有根节点。在每一步迭代中，我们取出队列中的首节点，如果它是叶子节点，则将它对应的路径加入到答案中。如果它不是叶子节点，则将它的所有孩子节点加入到队列的末尾。当队列为空时广度优先搜索结束，我们即能得到答案。



代码


class Solution {
    public List<String> binaryTreePaths(TreeNode root) {
        List<String> paths = new ArrayList<String>();
        if (root == null) {
            return paths;
        }
        Queue<TreeNode> nodeQueue = new LinkedList<TreeNode>();
        Queue<String> pathQueue = new LinkedList<String>();

        nodeQueue.offer(root);
        pathQueue.offer(Integer.toString(root.val));

        while (!nodeQueue.isEmpty()) {
            TreeNode node = nodeQueue.poll(); 
            String path = pathQueue.poll();

            if (node.left == null && node.right == null) {
                paths.add(path);
            } else {
                if (node.left != null) {
                    nodeQueue.offer(node.left);
                    pathQueue.offer(new StringBuffer(path).append("->").append(node.left.val).toString());
                }

                if (node.right != null) {
                    nodeQueue.offer(node.right);
                    pathQueue.offer(new StringBuffer(path).append("->").append(node.right.val).toString());
                }
            }
        }
        return paths;
    }
}
复杂度分析

时间复杂度：O(N^2)O(N 
2
 )，其中 NN 表示节点数目。分析同方法一。
空间复杂度：O(N^2)O(N 
2
 )，其中 NN 表示节点数目。在最坏情况下，队列中会存在 NN 个节点，保存字符串的队列中每个节点的最大长度为 NN，故空间复杂度为 O(N^2)O(N 
2
 )。
 
 public class Solution {
    public IList<string> BinaryTreePaths(TreeNode root) {
       IList<string> list=new List<string>();
       CallBack(root,list,"");
       return list;
    }  
    public void CallBack(TreeNode root,IList<string> list,string s)
    {
        if(root==null)return;
        else
        {
            StringBuilder sb=new StringBuilder();
            sb.Append(s);
            sb.Append(root.val.ToString());
            if(root.left==null&&root.right==null)
            {
                list.Add(sb.ToString());
            }
            else
            {
                sb.Append("->");
                CallBack(root.left,list,sb.ToString());
                CallBack(root.right,list,sb.ToString());
            }
        }
    }
}

public class Solution {
    public IList<string> BinaryTreePaths(TreeNode root)
    {
        List<string> res = new List<string>();

        if (root == null)
        {
            return res;
        }

        Stack<TreeNode> stack = new Stack<TreeNode>();
        Stack<string> prefix = new Stack<string>();
        stack.Push(root);
        prefix.Push(root.val.ToString());

        while (stack.Count > 0)
        {
            TreeNode node = stack.Pop();
            string current = prefix.Pop();

            if (node.left == null && node.right == null)
            {
                res.Add(current);
            }
            else
            {
                if (node.left != null)
                {
                    stack.Push(node.left);
                    prefix.Push(current + "->" + node.left.val.ToString());
                }
                if (node.right != null)
                {
                    stack.Push(node.right);
                    prefix.Push(current + "->" + node.right.val.ToString());
                }
            }
        }

        return res;
    }
}




*/