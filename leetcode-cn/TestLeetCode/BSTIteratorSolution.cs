using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
实现一个二叉搜索树迭代器。你将使用二叉搜索树的根节点初始化迭代器。

调用 next() 将返回二叉搜索树中的下一个最小的数。

 

示例：



BSTIterator iterator = new BSTIterator(root);
iterator.next();    // 返回 3
iterator.next();    // 返回 7
iterator.hasNext(); // 返回 true
iterator.next();    // 返回 9
iterator.hasNext(); // 返回 true
iterator.next();    // 返回 15
iterator.hasNext(); // 返回 true
iterator.next();    // 返回 20
iterator.hasNext(); // 返回 false
 

提示：

next() 和 hasNext() 操作的时间复杂度是 O(1)，并使用 O(h) 内存，其中 h 是树的高度。
你可以假设 next() 调用总是有效的，也就是说，当调用 next() 时，BST 中至少存在一个下一个最小的数。
 
*/
/// <summary>
/// https://leetcode-cn.com/problems/binary-search-tree-iterator/
/// 173. 二叉搜索树迭代器
/// 实现一个二叉搜索树迭代器。你将使用二叉搜索树的根节点初始化迭代器。
/// 调用 next() 将返回二叉搜索树中的下一个最小的数。
/// 注意: next() 和hasNext() 操作的时间复杂度是O(1)，并使用 O(h) 内存，其中 h 是树的高度。
/// </summary>
public class BSTIterator
{

    public BSTIterator(TreeNode root) {

        var enumerable = CreateIterator(root);
        _enumerator = enumerable.GetEnumerator();
        _hasNext = _enumerator.MoveNext();
    }

    private IEnumerator<int> _enumerator;
    private bool _hasNext = true;
    private IEnumerable<int> CreateIterator(TreeNode root) {
        Stack<TreeNode> stack = new Stack<TreeNode>();
        PushNode(root);

        while(0 < stack.Count)
        {
            var n = stack.Pop();
            yield return n.val;
            PushNode(n.right);

        }

        void PushNode(TreeNode node)
        {
            while (node != null)
            {
                stack.Push(node);
                node = node.left;
            }
        }
    }
    
    public int Next() {
        var ret = _enumerator.Current;
        _hasNext = _enumerator.MoveNext();
        return ret;
    }
    
    public bool HasNext() {
        return _hasNext;
    }


    //public BSTIterator(TreeNode root)
    //{
    //    _stack.Clear();
    //    PushNode(root);
    //}

    //private Stack<TreeNode> _stack = new Stack<TreeNode>();
    //public bool HasNext()
    //{
    //    return 0 < _stack.Count;
    //}

    //public int Next()
    //{
    //    if(0 < _stack.Count)
    //    {
    //        var n = _stack.Pop();
    //        PushNode(n.right);
    //        return n.val;
    //    }
    //    return 0;
    //}

    //private void PushNode(TreeNode root)
    //{
    //    while (root != null)
    //    {
    //        _stack.Push(root);
    //        root = root.left;
    //    }
    //}
}
/*

二叉搜索树迭代器
力扣 (LeetCode)
发布于 2020-01-19
11.7k
概述
在研究这个问题的解决方案之前，让我们来总结以下问题的陈述中要求我们实现什么。我们有一个迭代器类，它有两个函数，即 next() 和 hasNext()。hasNext() 函数的作用是：返回一个布尔值，表示二叉搜索树中是否还有元素。next() 函数返回二叉搜索树中下一个最小元素。因此，我们第一次调用 next() 函数时，应返回二叉搜索树中的最小元素；同理，当我们最后一次调用 next() 时，应返回二叉搜索树中的最大元素。

你可能想知道迭代器的作用是什么。本质上，迭代器可以用于迭代任何容器的对象。就本题而言，容器是一个二叉搜索树。如果定义了这样的一个迭代器，那么遍历的逻辑就可以被抽象出来，我们可以使用迭代器按一定顺序处理元素。


1. new_iterator = BSTIterator(root);
2. while (new_iterator.hasNext())
3.     process(new_iterator.next());
现在我们知道了为数据结构设计一个迭代器类背后的原因，通常，迭代器只是逐个遍历容器的每个元素。对于二叉搜索树，我们希望迭代器以升序返回元素。

二叉搜索树的一个重要的特性是是二叉搜索树的中序序列是升序序列；因此，中序遍历是该解决方案的核心。

在这里插入图片描述
在这里插入图片描述
在这里插入图片描述
在这里插入图片描述
在这里插入图片描述
在这里插入图片描述
在这里插入图片描述
在这里插入图片描述
在这里插入图片描述
在这里插入图片描述
在这里插入图片描述
在这里插入图片描述
在这里插入图片描述
在这里插入图片描述
在这里插入图片描述
在这里插入图片描述
在这里插入图片描述
在这里插入图片描述
在这里插入图片描述

方法一：扁平化二叉搜索树
在计算机程序设计中，迭代器是使程序员能够遍历容器的对象。这是维基百科对迭代器的定义。当前，实现迭代器的最简单的方法是在类似数组的容器接口上。如果我们有一个数组，则我们只需要一个指针或者索引，就可以轻松的实现函数 next() 和 hasNext()。

因此，我们要研究的第一种方法就是基于这种思想。我们将使用额外的数组，并将二叉搜索树展开存放到里面。我们想要数组的元素按升序排序，则我们应该对二叉搜索树进行中序遍历，然后我们在数组中构建迭代器函数。

算法：

初始化一个空数组用来存放二叉搜索树的中序序列。
我们按中序遍历二叉搜索树，按照左中右的顺序处理节点。
一旦所有节点都在数组中，则我们只需要一个指针或索引来实现 next() 和 hasNext 这两个函数。每当调用 hasNext() 时，我们只需要检查索引是否达到数组末尾。每当调用 next() 时，我们只需要返回索引指向的元素，并向前移动一步，以模拟迭代器的进度。

class BSTIterator {

    ArrayList<Integer> nodesSorted;
    int index;

    public BSTIterator(TreeNode root) {

        // Array containing all the nodes in the sorted order
        this.nodesSorted = new ArrayList<Integer>();
        
        // Pointer to the next smallest element in the BST
        this.index = -1;
        
        // Call to flatten the input binary search tree
        this._inorder(root);
    }

    private void _inorder(TreeNode root) {

        if (root == null) {
            return;
        }

        this._inorder(root.left);
        this.nodesSorted.add(root.val);
        this._inorder(root.right);
    }

    public int next() {
        return this.nodesSorted.get(++this.index);
    }

    public boolean hasNext() {
        return this.index + 1 < this.nodesSorted.size();
    }
}
复杂度分析

时间复杂度：构造迭代器花费的时间为 O(N)O(N)，问题陈述只要求我们分析两个函数的复杂性，但是在实现类时，还要注意初始化类对象所需的时间；在这种情况下，时间复杂度与二叉搜索树中的节点数成线性关系。
next()：O(1)O(1)
hasNext()：O(1)O(1)
空间复杂度：O(N)O(N)，由于我们创建了一个数组来包含二叉搜索树中的所有节点值，这不符合问题陈述中的要求，任一函数的最大空间复杂度应为 O(h)O(h)，其中 hh 指的是树的高度，对于平衡的二叉搜索树，高度通常为 logNlogN。
方法二：受控递归
我们前面使用的方法在空间上与二叉搜索树中的节点数呈线性关系。然而，我们不得不使用这种方法的原因是我们可以在数组上迭代，且我们不能够暂停递归，然后在某个时候启动它。

但是，如果我们可以模拟中序遍历的受控递归，那么除了堆栈用于模拟递归的空间之外，实际上不需要使用任何其他空间。

因此，这种方法的本质上是使用自定义的栈来模拟中序遍历。也就是说，我们将采用迭代的方式来模拟中序遍历，而不是采用递归的方法；这样做的过程中，我们能够轻松的实现这两个函数的调用，而不是用其他额外的空间。

然而，就这两个函数的复杂性而言，会有点复杂，我们需要花一些时间来理解这种方法是否符合问题所说的渐近复杂性的要求。让我们更加具体的看一下这个算法。

算法：

初始化一个空栈 S，用于模拟二叉搜索树的中序遍历。中序遍历我们采用与之前相同的方法，只是我们现在使用的是自己的栈而不是系统的堆栈。由于我们使用自定义的数据结构，因此可以随时暂停和恢复递归。
我们还要实现一个帮助函数，在实现中将一次又一次的调用它。这个函数叫 _inorder_left，它将给定节点中的所有左子节点添加到栈中，直到节点没有左子节点为止。如下：

def inorder_left(root):
   while (root):
     S.append(root)
     root = root.left
第一次调用 next() 函数时，必须返回二叉搜索树的最小元素，然后我们模拟递归必须向前移动一步，即移动到二叉搜索树的下一个最小元素上。栈的顶部始终包含 next() 函数返回的元素。hasNext() 很容易实现，因为我们只需要检查栈是否为空。
首先，给定二叉搜索树的根节点，我们调用函数 _inorder_left，这确保了栈顶部始终包含了 next() 函数返回的元素。
假设我们调用 next()，我们需要返回二叉搜索树中的下一个最小元素，即栈的顶部元素。有两种可能性：
一个是栈顶部的节点是一个叶节点。这是最好的情况，因为我们什么都不用做，只需将节点从栈中弹出并返回其值。所以这是个常数时间的操作。
另一个情况是栈顶部的节点拥有右节点。我们不需要检查左节点，因为左节点已经添加到栈中了。栈顶元素要么没有左节点，要么左节点已经被处理了。如果栈顶部拥有右节点，那么我们需要对右节点上调用帮助函数。该时间复杂度取决于树的结构。
next() 函数调用中，获取下一个最小的元素不需要花太多时间，但是要保证栈顶元素是 next() 函数返回的元素方面花费了一些时间。

class BSTIterator {

    Stack<TreeNode> stack;

    public BSTIterator(TreeNode root) {
        
        // Stack for the recursion simulation
        this.stack = new Stack<TreeNode>();
        
        // Remember that the algorithm starts with a call to the helper function
        // with the root node as the input
        this._leftmostInorder(root);
    }

    private void _leftmostInorder(TreeNode root) {
      
        // For a given node, add all the elements in the leftmost branch of the tree
        // under it to the stack.
        while (root != null) {
            this.stack.push(root);
            root = root.left;
        }
    }

    public int next() {
        // Node at the top of the stack is the next smallest element
        TreeNode topmostNode = this.stack.pop();

        // Need to maintain the invariant. If the node has a right child, call the 
        // helper function for the right child
        if (topmostNode.right != null) {
            this._leftmostInorder(topmostNode.right);
        }

        return topmostNode.val;
    }

    public boolean hasNext() {
        return this.stack.size() > 0;
    }
}
复杂度分析

时间复杂度：
hasNext()：若栈中还有元素，则返回 true，反之返回 false。所以这是一个 O(1)O(1) 的操作。
next()：包含了两个主要步骤。一个是从栈中弹出一个元素，它是下一个最小的元素。这是一个 O(1)O(1) 的操作。然而，随后我们要调用帮助函数 _inorder_left ，它需要递归的，将左节点添加到栈上，是线性时间的操作，最坏的情况下为 O(N)O(N)。但是我们只对含有右节点的节点进行调用，它也不会总是处理 N 个节点。只有当我们有一个倾斜的树，才会有 N 个节点。因此该操作的平均时间复杂度仍然是 O(1)O(1)，符合问题中所要求的。
空间复杂度：O(h)O(h)，使用了一个栈来模拟递归。
下一篇：Morris莫里斯算法实现(Python3)


Morris莫里斯算法实现(Python3)
Lane
发布于 3 天前
24
使用 Morris 算法可以实现 O(1) 空间, 不过临时改变了树的结构, 可能不太符合题意. 但是依然值得参考一下.

关于 Morris 算法可以参看这篇题解: 一文掌握Morris遍历算法.




# Definition for a binary tree node.
# class TreeNode:
#     def __init__(self, x):
#         self.val = x
#         self.left = None
#         self.right = None

class BSTIterator:

    def __init__(self, root: TreeNode):
        self._curr = None
        self._head = root
        self._go_next()


    def _go_next(self):
        while self._head:
            if not self._head.left:
                self._curr = self._head
                self._head = self._head.right
                break
            else:
                self._pre = self._head.left
                while self._pre.right and self._pre.right != self._head:
                    self._pre = self._pre.right
                if self._pre.right == None:
                    self._pre.right = self._head
                    self._head = self._head.left
                else:  # self._pre.right == self._head
                    self._pre.right = None
                    self._curr = self._head
                    self._head = self._head.right
                    break


    def next(self) -> int:
        """
        @return the next smallest number
        """
        val = self._curr.val
        self._curr = None
        self._go_next()
        return val


    def hasNext(self) -> bool:
        """
        @return whether we have a next smallest number
        """
        return self._curr != None



# Your BSTIterator object will be instantiated and called as such:
# obj = BSTIterator(root)
# param_1 = obj.next()
# param_2 = obj.hasNext()
下一篇：栈模拟中序遍历
© 著作权归作者所有
0

public class BSTIterator {
    List<int> ans = new List<int>();
    int idx = 0;
    public BSTIterator(TreeNode root) {
        idx=0;
        ans=new List<int>();
        dfs(root);
    }
    private void dfs(TreeNode root){
        if(root==null) return;
        dfs(root.left);
        ans.Add(root.val);
        dfs(root.right);
    }
    
    public int Next() {
        return ans[idx++];
    }
    
    public bool HasNext() {
        return idx < ans.Count;
    }
}

public class BSTIterator {
    Stack<TreeNode> st;

    public BSTIterator(TreeNode root) {
        st = new Stack<TreeNode>();
        while(root!=null){
            st.Push(root);
            root = root.left;
        }
    }
    
    public int Next() {
        if(st.Count > 0){
            TreeNode pNode = st.Pop();
            TreeNode cur = pNode.right;
            while(cur!=null){
                st.Push(cur);
                cur = cur.left;
            }
            return pNode.val;
        }
        return -1;
    }
    
    public bool HasNext() {
        return st.Count > 0;
    }
}

public class BSTIterator {

    public BSTIterator(TreeNode root) {
        list = new Queue<int>();
        DFS(root);
    }
    void DFS(TreeNode node){
        if(node == null)
            return;
        DFS(node.left);
        list.Enqueue(node.val);
        DFS(node.right);
    }
    Queue<int> list;
    public int Next() {
        return list.Dequeue();
    }
    
    public bool HasNext() {
        return list.Any();
    }
}

 
 
 
*/