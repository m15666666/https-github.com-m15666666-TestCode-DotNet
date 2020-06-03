using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个二叉树，返回它的中序 遍历。

示例:

输入: [1,null,2,3]
   1
    \
     2
    /
   3

输出: [1,3,2]
进阶: 递归算法很简单，你可以通过迭代算法完成吗？
 
*/
/// <summary>
/// https://leetcode-cn.com/problems/binary-tree-inorder-traversal/
/// 94.二叉树的中序遍历
/// 
/// 
/// </summary>
class InorderTraversalSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    # region 迭代方式
    public IList<int> InorderTraversal(TreeNode root)
    {
        List<int> ret = new List<int>();
        if (root == null) return ret;

        Stack<TreeNode> stack = new Stack<TreeNode>(100);

        while( true )
        {
            if (root != null)
            {
                stack.Push(root);
                root = root.left;
                continue;
            }

            if (stack.Count == 0) break;

            root = stack.Pop();
            ret.Add(root.val);

            root = root.right;
        }
        
        return ret;
    }
    #endregion

    #region 递归方式
    //public IList<int> InorderTraversal(TreeNode root)
    //{
    //    List<int> ret = new List<int>();
    //    if (root == null) return ret;
    //    InorderTraversal(root, ret);
    //    return ret;
    //}

    //private void InorderTraversal( TreeNode root, List<int> ret )
    //{
    //    if (root.left != null) InorderTraversal(root.left, ret);
    //    ret.Add(root.val);
    //    if (root.right != null) InorderTraversal(root.right, ret);
    //}
    #endregion
}
/*

二叉树的中序遍历
力扣 (LeetCode)
发布于 2019-06-07
84.9k
方法一：递归
第一种解决方法是使用递归。这是经典的方法，直截了当。我们可以定义一个辅助函数来实现递归。


class Solution {
    public List < Integer > inorderTraversal(TreeNode root) {
        List < Integer > res = new ArrayList < > ();
        helper(root, res);
        return res;
    }

    public void helper(TreeNode root, List < Integer > res) {
        if (root != null) {
            if (root.left != null) {
                helper(root.left, res);
            }
            res.add(root.val);
            if (root.right != null) {
                helper(root.right, res);
            }
        }
    }
}
复杂度分析

时间复杂度：O(n)O(n)。递归函数 T(n) = 2 \cdot T(n/2)+1T(n)=2⋅T(n/2)+1。
空间复杂度：最坏情况下需要空间O(n)O(n)，平均情况为O(\log n)O(logn)。



方法二：基于栈的遍历
本方法的策略与上衣方法很相似，区别是使用了栈。

下面是示例:




public class Solution {
    public List < Integer > inorderTraversal(TreeNode root) {
        List < Integer > res = new ArrayList < > ();
        Stack < TreeNode > stack = new Stack < > ();
        TreeNode curr = root;
        while (curr != null || !stack.isEmpty()) {
            while (curr != null) {
                stack.push(curr);
                curr = curr.left;
            }
            curr = stack.pop();
            res.add(curr.val);
            curr = curr.right;
        }
        return res;
    }
}
复杂度分析

时间复杂度：O(n)O(n)。

空间复杂度：O(n)O(n)。




方法三：莫里斯遍历
本方法中，我们使用一种新的数据结构：线索二叉树。方法如下：

Step 1: 将当前节点current初始化为根节点

Step 2: While current不为空，

若current没有左子节点

    a. 将current添加到输出

    b. 进入右子树，亦即, current = current.right

否则

    a. 在current的左子树中，令current成为最右侧节点的右子节点

    b. 进入左子树，亦即，current = current.left
举例而言:



          1
        /   \
       2     3
      / \   /
     4   5 6

首先，1 是根节点，所以将 current 初始化为 1。1 有左子节点 2，current 的左子树是


         2
        / \
       4   5
在此左子树中最右侧的节点是 5，于是将 current(1) 作为 5 的右子节点。令 current = cuurent.left (current = 2)。
现在二叉树的形状为:


         2
        / \
       4   5
            \
             1
              \
               3
              /
             6
对于 current(2)，其左子节点为4，我们可以继续上述过程


        4
         \
          2
           \
            5
             \
              1
               \
                3
               /
              6
由于 4 没有左子节点，添加 4 为输出，接着依次添加 2, 5, 1, 3 。节点 3 有左子节点 6，故重复以上过程。
最终的结果是 [4,2,5,1,6,3]。

了解更多细节请查阅
线索二叉树 与
莫里斯方法解析


class Solution {
    public List < Integer > inorderTraversal(TreeNode root) {
        List < Integer > res = new ArrayList < > ();
        TreeNode curr = root;
        TreeNode pre;
        while (curr != null) {
            if (curr.left == null) {
                res.add(curr.val);
                curr = curr.right; // move to next right node
            } else { // has a left subtree
                pre = curr.left;
                while (pre.right != null) { // find rightmost
                    pre = pre.right;
                }
                pre.right = curr; // put cur after the pre node
                TreeNode temp = curr; // store cur node
                curr = curr.left; // move cur to the top of the new tree
                temp.left = null; // original cur left be null, avoid infinite loops
            }
        }
        return res;
    }
}
复杂度分析

时间复杂度：O(n)O(n)。 想要证明时间复杂度是O(n)O(n)，最大的问题是找到每个节点的前驱节点的时间复杂度。乍一想，找到每个节点的前驱节点的时间复杂度应该是 O(n\log n)O(nlogn)，因为找到一个节点的前驱节点和树的高度有关。
但事实上，找到所有节点的前驱节点只需要O(n)O(n) 时间。一棵 nn 个节点的二叉树只有 n-1n−1 条边，每条边只可能使用2次，一次是定位节点，一次是找前驱节点。
故复杂度为 O(n)O(n)。

空间复杂度：O(n)O(n)。使用了长度为 nn 的数组。

下一篇：颜色标记法-一种通用且简明的树遍历方法
© 著作权归作者所有
30
条评论

最热
精选评论(3)
lanmo1945lanmo1945
2019-09-27
下面才是正确的莫里斯中序遍历方式，不破坏原有树结构

// 莫里斯遍历利用叶子节点左右空域存储遍历前驱和后继
// 达到时间复杂度O(N)，空间复杂度O(1)
// 二叉树的串行遍历

// 莫里斯中序遍历
func MorrisTraverMid(root *TreeNode) []int {
	if root == nil {
		return nil
	}

	var result []int

	// 游标节点初始化为根节点
	cur := root

	// 定义前驱节点
	var pre *TreeNode

	// 当没有遍历到最后情况
	for cur != nil {

		// 当游标节点没有左孩子
		if cur.Left == nil {
			// 加游标节点值加入结果集(visit)
			result = append(result, cur.Val)

			// 因为没有左孩子，进入右孩子
			cur = cur.Right
			continue
		}

		// 当游标有左孩子
		// 1.找到左子树最右节点作为游标节点前驱

		// 得到左子树
		pre = cur.Left

		// 找到左子树最右叶子节点
		for pre.Right != nil && pre.Right != cur {
			pre = pre.Right
		}

		// 最右叶子节点
		if pre.Right == nil {
			// 最右叶子节点与cur连接
			pre.Right = cur

			// 进入左子树
			cur = cur.Left
			continue
		}

		// 最右节点与cur相等（成环情况）
		// visit cur
		result = append(result, cur.Val)

		// 破环
		pre.Right = nil

		// 进入右子树
		cur = cur.Right
	}

	return result
}

java版，不破坏原有结构

// 莫里斯中序遍历
class Solution {
    public List<Integer> inorderTraversal(TreeNode root) {
        List<Integer> ldr = new ArrayList<Integer>();
        TreeNode cur = root;
        TreeNode pre = null;
        while(cur!=null){
            if(cur.left==null){//左子树为空，输出当前节点，将其右孩子作为当前节点
                ldr.add(cur.val);
                cur = cur.right;
            }
            else{
                pre = cur.left;//左子树
                while(pre.right!=null&&pre.right!=cur){//找到前驱节点，即左子树中的最右节点
                    pre = pre.right;
                }
            //如果前驱节点的右孩子为空，将它的右孩子设置为当前节点。当前节点更新为当前节点的左孩子。
                if(pre.right==null){
                    pre.right = cur;
                    cur = cur.left;
                }
            //如果前驱节点的右孩子为当前节点，将它的右孩子重新设为空（恢复树的形状）。输出当前节点。当前节点更新为当前节点的右孩子。
                if(pre.right==cur){
                    pre.right = null;
                    ldr.add(cur.val);
                    cur = cur.right;
                }
            }
        }
        return ldr;
    }
}


颜色标记法-一种通用且简明的树遍历方法
henry
发布于 2019-09-04
23.8k
官方题解中介绍了三种方法来完成树的中序遍历，包括：

递归
借助栈的迭代方法
莫里斯遍历
在树的深度优先遍历中（包括前序、中序、后序遍历），递归方法最为直观易懂，但考虑到效率，我们通常不推荐使用递归。

栈迭代方法虽然提高了效率，但其嵌套循环却非常烧脑，不易理解，容易造成“一看就懂，一写就废”的窘况。而且对于不同的遍历顺序（前序、中序、后序），循环结构差异很大，更增加了记忆负担。

因此，我在这里介绍一种“颜色标记法”（瞎起的名字……），兼具栈迭代方法的高效，又像递归方法一样简洁易懂，更重要的是，这种方法对于前序、中序、后序遍历，能够写出完全一致的代码。

其核心思想如下：

使用颜色标记节点的状态，新节点为白色，已访问的节点为灰色。
如果遇到的节点为白色，则将其标记为灰色，然后将其右子节点、自身、左子节点依次入栈。
如果遇到的节点为灰色，则将节点的值输出。
使用这种方法实现的中序遍历如下：


class Solution:
    def inorderTraversal(self, root: TreeNode) -> List[int]:
        WHITE, GRAY = 0, 1
        res = []
        stack = [(WHITE, root)]
        while stack:
            color, node = stack.pop()
            if node is None: continue
            if color == WHITE:
                stack.append((WHITE, node.right))
                stack.append((GRAY, node))
                stack.append((WHITE, node.left))
            else:
                res.append(node.val)
        return res
如要实现前序、后序遍历，只需要调整左右子节点的入栈顺序即可。

二叉树的中序遍历 - 迭代法
jason
发布于 2019-10-14
18.4k
解题思路：
前序遍历迭代算法

后序遍历迭代算法

第一种方法
第二种方法
中序遍历迭代算法

前序遍历迭代算法：
二叉树的前序遍历

二叉树的遍历，整体上看都是好理解的。

三种遍历的迭代写法中，数前序遍历最容易理解。

递归思路：先树根，然后左子树，然后右子树。每棵子树递归。

在迭代算法中，思路演变成，每到一个节点 A，就应该立即访问它。

因为，每棵子树都先访问其根节点。对节点的左右子树来说，也一定是先访问根。

在 A 的两棵子树中，遍历完左子树后，再遍历右子树。

因此，在访问完根节点后，遍历左子树前，要将右子树压入栈。

思路：

栈S;
p= root;
while(p || S不空){
    while(p){
        访问p节点；
        p的右子树入S;
        p = p的左子树;
    }
    p = S栈顶弹出;
}
代码：

    vector<int> preorderTraversal(TreeNode* root) {
        stack<TreeNode*> S;
        vector<int> v;
        TreeNode* rt = root;
        while(rt || S.size()){
            while(rt){
                S.push(rt->right);
                v.push_back(rt->val);
                rt=rt->left;
            }
            rt=S.top();S.pop();
        }
        return v;        
    }
后序遍历迭代算法：
二叉树的后序遍历

有两种方法。第一种比第二种要容易理解，但多了个结果逆序的过程。

第一种方法：
我们可以用与前序遍历相似的方法完成后序遍历。

后序遍历与前序遍历相对称。

思路： 每到一个节点 A，就应该立即访问它。 然后将左子树压入栈，再次遍历右子树。

遍历完整棵树后，结果序列逆序即可。

思路：

栈S;
p= root;
while(p || S不空){
    while(p){
        访问p节点；
        p的左子树入S;
        p = p的右子树;
    }
    p = S栈顶弹出;
}
结果序列逆序;
代码：

    vector<int> postorderTraversal(TreeNode* root) {
        stack<TreeNode*> S;
        vector<int> v;
        TreeNode* rt = root;
        while(rt || S.size()){
            while(rt){
                S.push(rt->left);
                v.push_back(rt->val);
                rt=rt->right;
            }
            rt=S.top();S.pop();
        }
        reverse(v.begin(),v.end());
        return v;
    }
第二种方法：
按照左子树-根-右子树的方式，将其转换成迭代方式。

思路：每到一个节点 A，因为根要最后访问，将其入栈。然后遍历左子树，遍历右子树，最后返回到 A。

但是出现一个问题，无法区分是从左子树返回，还是从右子树返回。

因此，给 A 节点附加一个标记T。在访问其右子树前，T 置为 True。之后子树返回时，当 T 为 True表示从右子树返回，否则从左子树返回。

当 T 为 false 时，表示 A 的左子树遍历完，还要访问右子树。

同时，当 T 为 True 时，表示 A 的两棵子树都遍历过了，要访问 A 了。并且在 A 访问完后，A 这棵子树都访问完成了。

思路：

栈S;
p= root;
T<节点,True/False> : 节点标记;
while(p || S不空){
    while(p){
        p入S;
        p = p的左子树;
    }
    while(S不空 且 T[S.top] = True){
        访问S.top;
        S.top出S;
    }
    if(S不空){
        p = S.top 的右子树;
        T[S.top] = True;
    }
}
代码：

    vector<int> postorderTraversal(TreeNode* root) {
        stack<TreeNode*> S;
        unordered_map<TreeNode*,int> done;
        vector<int> v;
        TreeNode* rt = root;
        while(rt || S.size()){
            while(rt){
                S.push(rt);
                rt=rt->left;
            }
            while(S.size() && done[S.top()]){
                v.push_back(S.top()->val);
                S.pop();
            }
            if(S.size()){
                rt=S.top()->right;
                done[S.top()]=1;    
            }
        }
        return v;
    }
中序遍历迭代算法:
二叉树的中序遍历

思路：每到一个节点 A，因为根的访问在中间，将 A 入栈。然后遍历左子树，接着访问 A，最后遍历右子树。

在访问完 A 后，A 就可以出栈了。因为 A 和其左子树都已经访问完成。

思路：

栈S;
p= root;
while(p || S不空){
    while(p){
        p入S;
        p = p的左子树;
    }
    p = S.top 出栈;
    访问p;
    p = p的右子树;
}
代码：

    vector<int> inorderTraversal(TreeNode* root) {
        stack<TreeNode*> S;
        vector<int> v;
        TreeNode* rt = root;
        while(rt || S.size()){
            while(rt){
                S.push(rt);
                rt=rt->left;
            }
            rt=S.top();S.pop();
            v.push_back(rt->val);
            rt=rt->right;
        }
        return v;        
    }
下一篇：动画演示+三种实现 94. 二叉树的中序遍历

完全模仿递归，不变一行。秒杀全场，一劳永逸
PualKing
发布于 2020-05-16
527
如果你只想掌握其中某一种遍历大可去找那些奇技淫巧的题解（一会判断指针一会儿判断栈就问你怕不怕？面试的时候你能想起来？）；如果你想统一掌握三种遍历，并且希望思路清晰，我强烈建议你阅读下去！因为这里介绍的是递归转迭代的思路，而不仅仅是用迭代的形式完成题目。本题解也是双100%哦！

老思路链接
新思路

递归的本质就是压栈，了解递归本质后就完全可以按照递归的思路来迭代。
怎么压，压什么？压的当然是待执行的内容，后面的语句先进栈，所以进栈顺序就决定了前中后序。
我们需要一个标志区分每个递归调用栈，这里使用nullptr来表示。
具体直接看注释，可以参考文章最后“和递归写法的对比”。先序遍历看懂了，中序和后序也就秒懂。

先序遍历


class Solution {
public:
    vector<int> preorderTraversal(TreeNode* root) {
        vector<int> res;  //保存结果
        stack<TreeNode*> call;  //调用栈
        if(root!=nullptr) call.push(root);  //首先介入root节点
        while(!call.empty()){
            TreeNode *t = call.top();
            call.pop();  //访问过的节点弹出
            if(t!=nullptr){
                if(t->right) call.push(t->right);  //右节点先压栈，最后处理
                if(t->left) call.push(t->left);
                call.push(t);  //当前节点重新压栈（留着以后处理），因为先序遍历所以最后压栈
                call.push(nullptr);  //在当前节点之前加入一个空节点表示已经访问过了
            }else{  //空节点表示之前已经访问过了，现在需要处理除了递归之外的内容
                res.push_back(call.top()->val);  //call.top()是nullptr之前压栈的一个节点，也就是上面call.push(t)中的那个t
                call.pop();  //处理完了，第二次弹出节点（彻底从栈中移除）
            }
        }
        return res;
    }
};
后序遍历
你没看错，只有注释部分改变了顺序，父>右>左。


class Solution {
public:
    vector<int> postorderTraversal(TreeNode* root) {
        vector<int> res;
        stack<TreeNode*> call;
        if(root!=nullptr) call.push(root);
        while(!call.empty()){
            TreeNode *t = call.top();
            call.pop();
            if(t!=nullptr){
                call.push(t);  //在右节点之前重新插入该节点，以便在最后处理（访问值）
                call.push(nullptr); //nullptr跟随t插入，标识已经访问过，还没有被处理
                if(t->right) call.push(t->right);
                if(t->left) call.push(t->left);
            }else{
                res.push_back(call.top()->val);
                call.pop();
            }
        }
        return res;   
    }
};
中序遍历
你没看错，只有注释部分改变了顺序，右>父>左。其他和前序遍历“一 模 一 样”


class Solution {
public:
    vector<int> inorderTraversal(TreeNode* root) {
        vector<int> res;
        stack<TreeNode*> call;
        if(root!=nullptr) call.push(root);
        while(!call.empty()){
            TreeNode *t = call.top();
            call.pop();
            if(t!=nullptr){
                if(t->right) call.push(t->right);
                call.push(t);  //在左节点之前重新插入该节点，以便在左节点之后处理（访问值）
                call.push(nullptr); //nullptr跟随t插入，标识已经访问过，还没有被处理
                if(t->left) call.push(t->left);
            }else{
                res.push_back(call.top()->val);
                call.pop();
            }
        }
        return res;
    }
};
对比中序遍历的递归写法


void dfs(t){ //进入函数表示“访问过”，将t从栈中弹出

    dfs(t->left);   //因为要访问t->left, 所以我先把函数中下面的信息都存到栈里。
                //依次call.push(t->right), call.push(t)【t第二次入栈】, call.push(nullptr)【标识t二次入栈】, call.push(t->left)。
                //此时t并没有被处理（卖萌）。栈顶是t->left, 所以现在进入t->left的递归中。

    //res.push_back(t->val)
    t.卖萌();   //t->left 处理完了，t->left被彻底弹出栈。
                //此时栈顶是nullptr, 表示t是已经访问过的。那么我现在需要真正的处理t了（即，执行卖萌操作）。
                //卖萌结束后，t 就被彻底弹出栈了。
    

    dfs(t->right); 
}
image.png

下一篇：非递归，栈的中序遍历
 
 
 
*/