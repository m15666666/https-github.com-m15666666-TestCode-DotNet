using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
返回与给定先序遍历 preorder 相匹配的二叉搜索树（binary search tree）的根结点。

(回想一下，二叉搜索树是二叉树的一种，其每个节点都满足以下规则，对于 node.left 的任何后代，值总 < node.val，而 node.right 的任何后代，值总 > node.val。此外，先序遍历首先显示节点的值，然后遍历 node.left，接着遍历 node.right。）

 

示例：

输入：[8,5,1,7,10,12]
输出：[8,5,10,1,7,null,12]

 

提示：

1 <= preorder.length <= 100
先序 preorder 中的值是不同的。
*/
/// <summary>
/// https://leetcode-cn.com/problems/construct-binary-search-tree-from-preorder-traversal/
/// 1008. 先序遍历构造二叉树
/// 
/// </summary>
class ConstructBinarySearchTreeFromPreorderTraversalSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public TreeNode BstFromPreorder(int[] preorder)
    {
        int len = preorder.Length;
        if (len == 0) return null;

        TreeNode ret = new TreeNode(preorder[0]);
        Stack<TreeNode> stack = new Stack<TreeNode>();
        stack.Push(ret);

        int v;
        TreeNode child;
        for (int i = 1; i < len; i++)
        {
            v = preorder[i];

            TreeNode node = stack.Peek();
            while (0 < stack.Count && stack.Peek().val < v)
                node = stack.Pop();

            child = new TreeNode(v);
            if (node.val < v) node.right = child;
            else node.left = child;

            stack.Push(child);
        }
        return ret;
    }
}
/*
监控二叉树
力扣 (LeetCode)
发布于 1 年前
3.8k 阅读
方法一：使用先序遍历和中序遍历构造二叉树
分析

该方法基于两个结论：

根据二叉树的先序遍历和中序遍历，可以唯一确定并构造出这课二叉树。可以参考从前序与中序遍历序列构造二叉树。

如果二叉树是一颗二叉搜索树，那么它的中序遍历就是树中所有的元素的值的升序排序。

根据上述两个结论，我们可以将先序遍历进行生序排序，得到中序遍历，再使用两者构造出二叉搜索树。

算法

首先将先序遍历排序得到中序遍历，随后使用分治的方法从先序遍历和中序遍历构造出二叉搜索树。具体的方法是，我们先获取先序遍历中的第一个元素，它对应了二叉树的根节点，它在中序遍历中的位置为 x，那么中序遍历中 x 左边的所有元素都在根节点的左子树中，x 右边的所有元素都在根节点的右子树中，这样根节点的左子树和右子树中的节点个数就都确定了。我们回到先序遍历中，由于根节点的左子树和右子树在先序遍历中一定都是连续的一段，并且它们的个数已经确定，且左子树的先序遍历出现在右子树之前，那么我们就得到了根节点左子树和右子树对应的先序遍历。有了子树的先序遍历和中序遍历，我们就可以递归地构造子树了。下图中给出了这种方法的详细步骤。

bla

class Solution {
    // start from first preorder element
    int pre_idx = 0;
    int[] preorder;
    HashMap<Integer, Integer> idx_map = new HashMap<Integer, Integer>();

    public TreeNode helper(int in_left, int in_right) {
        // if there is no elements to construct subtrees
        if (in_left == in_right)
            return null;

        // pick up pre_idx element as a root
        int root_val = preorder[pre_idx];
        TreeNode root = new TreeNode(root_val);

        // root splits inorder list
        // into left and right subtrees
        int index = idx_map.get(root_val);

        // recursion 
        pre_idx++;
        // build left subtree
        root.left = helper(in_left, index);
        // build right subtree
        root.right = helper(index + 1, in_right);
        return root;
    }

    public TreeNode bstFromPreorder(int[] preorder) {
        this.preorder = preorder;
        int [] inorder = Arrays.copyOf(preorder, preorder.length);
        Arrays.sort(inorder);

        // build a hashmap value -> its index
        int idx = 0;
        for (Integer val : inorder)
            idx_map.put(val, idx++);
        return helper(0, inorder.length);
    }
}
复杂度分析

时间复杂度：O(N \log N)O(NlogN)。对先序遍历进行排序的时间复杂度为 O(N \log N)O(NlogN)，构造二叉搜索树的时间复杂度为 O(N)O(N)，因此总的时间复杂度为 O(N \log N)O(NlogN)。

空间复杂度：O(N)O(N)，中序遍历使用的数组的空间为 O(N)O(N)。

方法二：递归
分析

当我们将先序遍历排序得到中序遍历时，我们花费了 O(N \log N)O(NlogN) 的时间复杂度，但事实上并没有得到任何额外的信息。也就是说，我们可以直接跳过生成中序遍历的步骤，根据先序遍历直接构造出二叉树。注意，由于题目中的二叉树是二叉搜索树，因此根据先序遍历构造出的二叉树才是唯一的。

我们使用递归的方法，在扫描先序遍历的同时构造出二叉树。我们在递归时维护一个 (lower, upper) 二元组，表示当前位置可以插入的节点的值的上下界。如果此时先序遍历位置的值处于上下界中，就将这个值作为新的节点插入到当前位置，并递归地处理当前位置的左右孩子的两个位置。否则回溯到当前位置的父节点。

算法

将 lower 和 upper 的初始值分别设置为负无穷和正无穷，因为根节点的值可以为任意值。

从先序遍历的第一个元素 idx = 0 开始构造二叉树，构造使用的函数名为 helper(lower, upper)：

如果 idx = n，即先序遍历中的所有元素已经被添加到二叉树中，那么此时构造已经完成；

如果当前 idx 对应的先序遍历中的元素 val = preorder[idx] 的值不在 [lower, upper] 范围内，则进行回溯；

如果 idx 对应的先序遍历中的元素 val = preorder[idx] 的值在 [lower, upper] 范围内，则新建一个节点 root，并对其左孩子递归处理 helper(lower, val)，对其右孩子递归处理 helper(val, upper)。

bla

class Solution {
    int idx = 0;
    int[] preorder;
    int n;

    public TreeNode helper(int lower, int upper) {
        // if all elements from preorder are used
        // then the tree is constructed
        if (idx == n) return null;

        int val = preorder[idx];
        // if the current element 
        // couldn't be placed here to meet BST requirements
        if (val < lower || val > upper) return null;

        // place the current element
        // and recursively construct subtrees
        idx++;
        TreeNode root = new TreeNode(val);
        root.left = helper(lower, val);
        root.right = helper(val, upper);
        return root;
    }

    public TreeNode bstFromPreorder(int[] preorder) {
        this.preorder = preorder;
        n = preorder.length;
        return helper(Integer.MIN_VALUE, Integer.MAX_VALUE);
    }
}
复杂度分析

时间复杂度：O(N)O(N)，仅扫描前序遍历一次。

空间复杂度：O(N)O(N)，用来存储二叉树。

方法三：迭代
方法二中的递归可以通过添加一个栈变成迭代。

将先序遍历中的第一个元素作为二叉树的根节点，即 root = new TreeNode(preorder[0])，并将其放入栈中。

使用 for 循环迭代先序遍历中剩下的所有元素：

将栈顶的元素作为父节点，当前先序遍历中的元素作为子节点。如果栈顶的元素值小于子节点的元素值，则将栈顶的元素弹出并作为新的父节点，直到栈空或栈顶的元素值大于子节点的元素值。注意，这里作为父节点的是最后一个被弹出栈的元素，而不是此时栈顶的元素；

如果父节点的元素值小于子节点的元素值，则子节点为右孩子，否则为左孩子；

将子节点放入栈中。



class Solution {
  public TreeNode bstFromPreorder(int[] preorder) {
    int n = preorder.length;
    if (n == 0) return null;

    TreeNode root = new TreeNode(preorder[0]);
    Deque<TreeNode> deque = new ArrayDeque<TreeNode>();
    deque.push(root);

    for (int i = 1; i < n; i++) {
      // take the last element of the deque as a parent
      // and create a child from the next preorder element
      TreeNode node = deque.peek();
      TreeNode child = new TreeNode(preorder[i]);
      // adjust the parent 
      while (!deque.isEmpty() && deque.peek().val < child.val)
        node = deque.pop();

      // follow BST logic to create a parent-child link
      if (node.val < child.val) node.right = child;
      else node.left = child;
      // add the child into deque
      deque.push(child);
    }
    return root;
  }
}
复杂度分析

时间复杂度：O(N)O(N)，仅扫描前序遍历一次。

空间复杂度：O(N)O(N)，用来存储栈和二叉树。 

public class Solution {
    public TreeNode BstFromPreorder(int[] preorder) {
        if (preorder == null || preorder.Length == 0)
            return null;
        
        return BuildBST(preorder, 0, preorder.Length - 1);
    }

    private TreeNode BuildBST(int[] preorder, int start, int end)
    {
        if (start > end)
            return null;

        if (start == end)
            return new TreeNode(preorder[start]);

        TreeNode node = new TreeNode(preorder[start]);
        int splitter = start + 1;
        while (splitter <= end)
        {
            if (preorder[splitter] < preorder[start])
                ++splitter;
            else   
                break;
        }

        node.left = BuildBST(preorder, start + 1, splitter - 1);
        node.right = BuildBST(preorder, splitter, end);

        return node;
    }
}

public class Solution {    
    int i = 0;
    public TreeNode BstFromPreorder(int[] preorder) {
        return helper(preorder, int.MaxValue);
    }
    
    private TreeNode helper(int[] preorder, int upper)
    {
        if(i == preorder.Length || preorder[i] > upper)
            return null;
        
        TreeNode root = new TreeNode(preorder[i++]);
        
        root.left = helper(preorder, root.val);
        root.right = helper(preorder, upper);
        
        return root;
    }
}
*/
