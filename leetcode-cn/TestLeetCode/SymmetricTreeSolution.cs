/*
给定一个二叉树，检查它是否是镜像对称的。

 

例如，二叉树 [1,2,2,3,4,4,3] 是对称的。

    1
   / \
  2   2
 / \ / \
3  4 4  3
 

但是下面这个 [1,2,2,null,3,null,3] 则不是镜像对称的:

    1
   / \
  2   2
   \   \
   3    3

*/

/// <summary>
/// https://leetcode-cn.com/problems/symmetric-tree/
/// 101. 对称二叉树
///
///
///
/// </summary>
internal class SymmetricTreeSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public bool IsSymmetric(TreeNode root)
    {
        if (root == null) return true;
        return IsSymmetric(root.left, root.right);

        bool IsSymmetric(TreeNode left, TreeNode right)
        {
            if (left == null && right == null) return true;
            if (left == null || right == null || left.val != right.val) return false;

            return IsSymmetric(left.left, right.right) && IsSymmetric(left.right, right.left);
        }
    }
}

/*

对称二叉树
力扣官方题解
发布于 2020-05-30
12.2k
📺视频题解

📖文字题解
方法一：递归
思路和算法

如果一个树的左子树与右子树镜像对称，那么这个树是对称的。

fig1

因此，该问题可以转化为：两个树在什么情况下互为镜像？

如果同时满足下面的条件，两个树互为镜像：

它们的两个根结点具有相同的值
每个树的右子树都与另一个树的左子树镜像对称
fig2

我们可以实现这样一个递归函数，通过「同步移动」两个指针的方法来遍历这棵树，pp 指针和 qq 指针一开始都指向这棵树的根，随后 pp 右移时，qq 左移，pp 左移时，qq 右移。每次检查当前 pp 和 qq 节点的值是否相等，如果相等再判断左右子树是否对称。

代码如下。


class Solution {
    public boolean isSymmetric(TreeNode root) {
        return check(root, root);
    }

    public boolean check(TreeNode p, TreeNode q) {
        if (p == null && q == null) {
            return true;
        }
        if (p == null || q == null) {
            return false;
        }
        return p.val == q.val && check(p.left, q.right) && check(p.right, q.left);
    }
}
复杂度分析

假设树上一共 nn 个节点。

时间复杂度：这里遍历了这棵树，渐进时间复杂度为 O(n)O(n)。
空间复杂度：这里的空间复杂度和递归使用的栈空间有关，这里递归层数不超过 nn，故渐进空间复杂度为 O(n)O(n)。
方法二：迭代
思路和算法

「方法一」中我们用递归的方法实现了对称性的判断，那么如何用迭代的方法实现呢？首先我们引入一个队列，这是把递归程序改写成迭代程序的常用方法。初始化时我们把根节点入队两次。每次提取两个结点并比较它们的值（队列中每两个连续的结点应该是相等的，而且它们的子树互为镜像），然后将两个结点的左右子结点按相反的顺序插入队列中。当队列为空时，或者我们检测到树不对称（即从队列中取出两个不相等的连续结点）时，该算法结束。


class Solution {
    public boolean isSymmetric(TreeNode root) {
        return check(root, root);
    }

    public boolean check(TreeNode u, TreeNode v) {
        Queue<TreeNode> q = new LinkedList<TreeNode>();
        q.offer(u);
        q.offer(v);
        while (!q.isEmpty()) {
            u = q.poll();
            v = q.poll();
            if (u == null && v == null) {
                continue;
            }
            if ((u == null || v == null) || (u.val != v.val)) {
                return false;
            }

            q.offer(u.left);
            q.offer(v.right);

            q.offer(u.right);
            q.offer(v.left);
        }
        return true;
    }
}
复杂度分析

时间复杂度：O(n)O(n)，同「方法一」。
空间复杂度：这里需要用一个队列来维护节点，每个节点最多进队一次，出队一次，队列中最多不会超过 nn 个点，故渐进空间复杂度为 O(n)O(n)。

动画演示+多种实现 101. 对称二叉树
王尼玛
发布于 2019-11-23
6.1k
递归实现
乍一看无从下手，但用递归其实很好解决。
根据题目的描述，镜像对称，就是左右两边相等，也就是左子树和右子树是相当的。
注意这句话，左子树和右子相等，也就是说要递归的比较左子树和右子树。
我们将根节点的左子树记做 left，右子树记做 right。比较 left 是否等于 right，不等的话直接返回就可以了。
如果相当，比较 left 的左节点和 right 的右节点，再比较 left 的右节点和 right 的左节点
比如看下面这两个子树(他们分别是根节点的左子树和右子树)，能观察到这么一个规律：
左子树 22 的左孩子 == 右子树 22 的右孩子
左子树 22 的右孩子 == 右子树 22 的左孩子


    2         2
   / \       / \
  3   4     4   3
 / \ / \   / \ / \
8  7 6  5 5  6 7  8
根据上面信息可以总结出递归函数的两个条件：
终止条件：

left 和 right 不等，或者 left 和 right 都为空
递归的比较 left，left 和 right.right，递归比较 left，right 和 right.left
动态图如下：
2.gif

算法的时间复杂度是 O(n)O(n)，因为要遍历 nn 个节点
空间复杂度是 O(n)O(n)，空间复杂度是递归的深度，也就是跟树高度有关，最坏情况下树变成一个链表结构，高度是nn。
代码实现：


class Solution {
	public boolean isSymmetric(TreeNode root) {
		if(root==null) {
			return true;
		}
		//调用递归函数，比较左节点，右节点
		return dfs(root.left,root.right);
	}
	
	boolean dfs(TreeNode left, TreeNode right) {
		//递归的终止条件是两个节点都为空
		//或者两个节点中有一个为空
		//或者两个节点的值不相等
		if(left==null && right==null) {
			return true;
		}
		if(left==null || right==null) {
			return false;
		}
		if(left.val!=right.val) {
			return false;
		}
		//再递归的比较 左节点的左孩子 和 右节点的右孩子
		//以及比较  左节点的右孩子 和 右节点的左孩子
		return dfs(left.left,right.right) && dfs(left.right,right.left);
	}
}
队列实现
回想下递归的实现：
当两个子树的根节点相等时，就比较:
左子树的 left 和 右子树的 right，这个比较是用递归实现的。
现在我们改用队列来实现，思路如下：
首先从队列中拿出两个节点(left 和 right)比较
将 left 的 left 节点和 right 的 right 节点放入队列
将 left 的 right 节点和 right 的 left 节点放入队列
时间复杂度是 O(n)O(n)，空间复杂度是 O(n)O(n)
动画演示如下：
1.gif

代码实现：


class Solution {
	public boolean isSymmetric(TreeNode root) {
		if(root==null || (root.left==null && root.right==null)) {
			return true;
		}
		//用队列保存节点
		LinkedList<TreeNode> queue = new LinkedList<TreeNode>();
		//将根节点的左右孩子放到队列中
		queue.add(root.left);
		queue.add(root.right);
		while(queue.size()>0) {
			//从队列中取出两个节点，再比较这两个节点
			TreeNode left = queue.removeFirst();
			TreeNode right = queue.removeFirst();
			//如果两个节点都为空就继续循环，两者有一个为空就返回false
			if(left==null && right==null) {
				continue;
			}
			if(left==null || right==null) {
				return false;
			}
			if(left.val!=right.val) {
				return false;
			}
			//将左节点的左孩子， 右节点的右孩子放入队列
			queue.add(left.left);
			queue.add(right.right);
			//将左节点的右孩子，右节点的左孩子放入队列
			queue.add(left.right);
			queue.add(right.left);
		}
		
		return true;
	}
}
下一篇：画解算法：101. 对称二叉树

画解算法：101. 对称二叉树
灵魂画手
发布于 2019-05-31
20.5k
解题方案
思路
递归结束条件：

都为空指针则返回 true
只有一个为空则返回 false
递归过程：

判断两个指针当前节点值是否相等
判断 A 的右子树与 B 的左子树是否对称
判断 A 的左子树与 B 的右子树是否对称
短路：

在递归判断过程中存在短路现象，也就是做 与 操作时，如果前面的值返回 false 则后面的不再进行计算

时间复杂度：O(n)O(n)

代码

class Solution {
    public boolean isSymmetric(TreeNode root) {
        return isMirror(root, root);
    }

    public boolean isMirror(TreeNode t1, TreeNode t2) {
        if (t1 == null && t2 == null) return true;
        if (t1 == null || t2 == null) return false;
        return (t1.val == t2.val)
            && isMirror(t1.right, t2.left)
            && isMirror(t1.left, t2.right);
    }
}

public class Solution {
    public bool IsSymmetric(TreeNode root) 
    {
        Stack<TreeNode> st = new Stack<TreeNode>();
        st.Push(root);
        st.Push(root);
        while(st.Count>0)
        {
            TreeNode node1 = st.Pop();
            TreeNode node2 = st.Pop();
            if(node1==null&&node2==null)
            {
                continue;
            }
            if(node1==null||node2==null)
            {
                return false;
            }
            
            if(node1.val == node2.val)
            {
                st.Push(node1.left);
                st.Push(node2.right);
                st.Push(node1.right);
                st.Push(node2.left);
            }
            else
            {
                return false;
            }
        }
        return true;
    }
}

public class Solution {
    public bool IsSymmetric(TreeNode root) {
        if(root==null){
            return true;
        }
        return Helper(new List<TreeNode>{root.left,root.right});
    }

    public bool Helper(List<TreeNode> nodes){
        if(nodes.Count==0){
            return true;
        }
        
        int left=0,right=nodes.Count-1;

        while(left<right){
            if(nodes[left]?.val!=nodes[right]?.val){
                return false;
            }
            left++;
            right--;
        }
        List<TreeNode> nextLevel=new List<TreeNode>();
        foreach(var item in nodes){
            if(item!=null){
            nextLevel.Add(item.left);
            nextLevel.Add(item.right);
            }
        }
        return Helper(nextLevel);
    }
}


*/
