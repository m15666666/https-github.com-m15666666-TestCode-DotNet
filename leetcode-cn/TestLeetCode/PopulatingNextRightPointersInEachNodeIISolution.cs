/*
给定一个二叉树

struct Node {
  int val;
  Node *left;
  Node *right;
  Node *next;
}
填充它的每个 next 指针，让这个指针指向其下一个右侧节点。如果找不到下一个右侧节点，则将 next 指针设置为 NULL。

初始状态下，所有 next 指针都被设置为 NULL。

 

进阶：

你只能使用常量级额外空间。
使用递归解题也符合要求，本题中递归程序占用的栈空间不算做额外的空间复杂度。
 

示例：



输入：root = [1,2,3,4,5,null,7]
输出：[1,#,2,3,#,4,5,7,#]
解释：给定二叉树如图 A 所示，你的函数应该填充它的每个 next 指针，以指向其下一个右侧节点，如图 B 所示。
 

提示：

树中的节点数小于 6000
-100 <= node.val <= 100

*/

/// <summary>
/// https://leetcode-cn.com/problems/populating-next-right-pointers-in-each-node-ii
/// 117. 填充每个节点的下一个右侧节点指针 II
///
///
///
/// </summary>
internal class PopulatingNextRightPointersInEachNodeIISolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public Node Connect(Node root)
    {
        if (root == null)  return root; 

        Node prev, leftmost = root;
        while (leftmost != null) 
        {
            var curr = leftmost;
            leftmost = null;
            prev = null;
            
            while (curr != null) 
            {
                if(curr.left != null) RecordNextLevel(curr.left);
                if(curr.right != null) RecordNextLevel(curr.right);
                
                curr = curr.next;
            }
        }
        return root ;

        void RecordNextLevel(Node childNode) 
        {
            if (prev != null) prev.next = childNode;
            else leftmost = childNode;    
            
            prev = childNode; 
        }
    }

    public class Node
    {
        public int val;
        public Node left;
        public Node right;
        public Node next;

        public Node()
        {
        }

        public Node(int _val)
        {
            val = _val;
        }

        public Node(int _val, Node _left, Node _right, Node _next)
        {
            val = _val;
            left = _left;
            right = _right;
            next = _next;
        }
    }
}
/*


填充每个节点的下一个右侧节点指针 II
力扣 (LeetCode)
发布于 2020-02-17
5.9k
方法一：层序遍历
思路

树和图有两种基本遍历方法。一种是深度优先遍历，例如：每次只遍历一个分支；另外一种是广度优先遍历，例如：先遍历完一层再进入下一层。树的深度优先遍历又可以分为先序遍历 preorder、中序遍历 inorder 和后序遍历 postorder。树的广度优先遍历基于节点的层级 level 概念。一个节点的层级取决于该节点的深度或者到根节点的距离。需要先遍历完同一层级的所有节点，才能进入下一层级。



很明显，此问题应该使用广度优先遍历。使用广度优先遍历，可以将同一层级的所有节点连接起来。

算法

创建一个辅助队列 Q，可以通过多种方式实现层序遍历，尤其是在在识别特定节点的时候。

在队列中以 (node, level)(node,level) 的形式存储节点，同时存储其子节点为 \text (node.left, \;\; parent\_level + 1)(node.left,parent_level+1) 和 (node.right,\;\; parent\_level + 1)(node.right,parent_level+1)。这种方法节点多了一个层级属性，需要创建一个新的数据结构，效率很低。


可以使用一个标记分离不同层级之间的节点。通常情况下，在队列中插入一个 NULL 元素，标记当前层级结束，下一层级开始。但是这种方法会创建与层级数量相同个数的 NULL 元素，造成过多内存消耗。


该方法使用嵌套循环结构。每一步都需要记录当前队列中 全部 元素数量，对应树中一个层级元素的数量。然后从队列中处理对应数量的元素。完成后，这一层级所有的节点都被访问，队列包含下一层级的 全部 节点。下面是对应的伪代码：


while (!Q.empty())
{
    size = Q.size()
    for i in range 0..size
    {
        node = Q.pop()
        Q.push(node.left)
        Q.push(node.right)
    }
}
首先在队列中加入根节点。因为第 0 层级只有一个节点，不需要建立连接，直接进入 while 循环即可。



伪代码中 while 循环迭代的是树的层级，内部的 for 循环迭代的是一个层级上所有的节点。由于可以访问同一层级的所有节点，因此能够建立 next 指针连接。

for 循环弹出一个节点时，同时把它的左孩子节点和右孩子节点依次加入队列。因此队列中每个层级的元素也是顺序存储的。可以通过已有的顺序建立 next 指针。




class Solution {
    public Node connect(Node root) {
        
        if (root == null) {
            return root;
        }
        
        // Initialize a queue data structure which contains
        // just the root of the tree
        Queue<Node> Q = new LinkedList<Node>(); 
        Q.add(root);
        
        // Outer while loop which iterates over 
        // each level
        while (Q.size() > 0) {
            
            // Note the size of the queue
            int size = Q.size();
            
            // Iterate over all the nodes on the current level
            for(int i = 0; i < size; i++) {
                
                // Pop a node from the front of the queue
                Node node = Q.poll();
                
                // This check is important. We don't want to
                // establish any wrong connections. The queue will
                // contain nodes from 2 levels at most at any
                // point in time. This check ensures we only 
                // don't establish next pointers beyond the end
                // of a level
                if (i < size - 1) {
                    node.next = Q.peek();
                }
                
                // Add the children, if any, to the back of
                // the queue
                if (node.left != null) {
                    Q.add(node.left);
                }
                if (node.right != null) {
                    Q.add(node.right);
                }
            }
        }
        
        // Since the tree has now been modified, return the root node
        return root;
    }
}
复杂度分析

时间复杂度：O(N)O(N)，每个节点被访问一次，即从队列中弹出，并建立 next 指针。

空间复杂度：O(N)O(N)。这是一棵完美二叉树，它的最后一个层级包含 N/2N/2 个节点。广度优先遍历的复杂度取决于一个层级上的最大元素数量。这种情况下空间复杂度为 O(N)O(N)。

方法二：使用已建立的 next 指针
思路

因为必须处理树上的所有节点，所以无法降低时间复杂度，但是可以尝试降低空间复杂度。因为对树的结构一无所知，所以使用队列保证有序访问同一层的所有节点，建立它们的连接。

一旦在某层的节点之间建立了 next 指针，那这层节点实际上形成了一个链表。基于该想法，提出降低空间复杂度的思路：

第 N 层节点之间建立 next 指针后，再建立第 N+1 层节点的 next 指针。因为可以通过 next 指针访问同一层的所有节点，所以使用第 N 层的 next 指针可以为第 N+1 层节点建立 next 指针。

算法

从根节点开始。因为第 0 层只有一个节点，不需要处理。可以在上一层为下一层建立 next 指针。该方法最重要的一点是：位于第 N-1N−1 层时为第 NN 层建立 next 指针。一旦完成这些连接操作，移至第 NN 层为第 N+1N+1 层建立 next 指针。

当遍历到某层节点时，该层节点的 next 指针已经建立。这样就不需要队列从而节省空间。每次只要知道下一层的最左边的节点，就可以从该节点开始，像遍历链表一样遍历该层的所有节点。

根据上面思路，得到下面伪代码：


leftmost = root
while (leftmost != null)
{
    curr = leftmost
    prev = NULL
    while (curr != null)
    {
        → process left child
        → process right child
        → set leftmost for the next level
        curr = curr.next
    }
}
解释伪代码中一些变量，帮助理解算法。

**leftmost：**每一层的最左节点。找到每层的最左节点作为链表首部，然后从该节点开始访问该层的所有节点。因为树的结构多样，实际上并不知道每一层的最左节点是哪个。观察几种树的结构和它们的最左节点。

如果你对该类型的问题感兴趣，可以尝试解决199. 二叉树的右视图。

**curr：**用来遍历当前层的所有节点。从该层的最左节点一直移动到该层最后一个节点。

**prev：**指向下一层的节点。每次更新 curr 指针时，需要令 prev.next 连接到 curr 的左子节点。同时更新 prev。下面例子重点介绍四种可能的 prev 指针更新情况。

第一种情况是 prev 指针初始化。开始时 prev 指针为空，找到下一层的最左节点时，将该节点赋值给 prev 指针。

第二种情况是当前节点没有左子节点，将 prev 指向当前节点的右子节点。例如下图中节点 2, 3, 5, 9 之间已经建立 next 指针。



第三种情况是下一个节点没有孩子节点。此时不需要更新 prev 指针。


第四种情况是下一个节点同时拥有左子节点和右子节点。首先将 prev 指向左子节点。在完成必要处理之后，令 prev 指针指向右子节点。


完成当前层操作后，进入下一层。最后一件事情就是更新最左节点，使用最左节点作为下一层的链表头部节点。每次使用 prev 指针指向第一个节点时，同时将它设为最左节点。例如下图中初始最左节点是 2，现在变成 4。



import javafx.util.Pair;
class Solution {
    
    Node prev, leftmost;
    
    public void processChild(Node childNode) {
        
        if (childNode != null) {
            
            // If the "prev" pointer is alread set i.e. if we
            // already found atleast one node on the next level,
            // setup its next pointer
            if (this.prev != null) {
                this.prev.next = childNode;
                    
            } else {
                
                // Else it means this child node is the first node
                // we have encountered on the next level, so, we
                // set the leftmost pointer
                this.leftmost = childNode;
            }    
                
            this.prev = childNode; 
        }
    }
        
    public Node connect(Node root) {
        
        if (root == null) {
            return root;
        }
        
        // The root node is the only node on the first level
        // and hence its the leftmost node for that level
        this.leftmost = root;
        
        // Variable to keep track of leading node on the "current" level
        Node curr = leftmost;
        
        // We have no idea about the structure of the tree,
        // so, we keep going until we do find the last level.
        // the nodes on the last level won't have any children
        while (this.leftmost != null) {
            
            // "prev" tracks the latest node on the "next" level
            // while "curr" tracks the latest node on the current
            // level.
            this.prev = null;
            curr = this.leftmost;
            
            // We reset this so that we can re-assign it to the leftmost
            // node of the next level. Also, if there isn't one, this
            // would help break us out of the outermost loop.
            this.leftmost = null;
            
            // Iterate on the nodes in the current level using
            // the next pointers already established.
            while (curr != null) {
                
                // Process both the children and update the prev
                // and leftmost pointers as necessary.
                this.processChild(curr.left);
                this.processChild(curr.right);
                
                // Move onto the next node.
                curr = curr.next;
            }
        }
                
        return root ;
    }
}
复杂度分析

时间复杂度：O(N)O(N)，每个节点只处理一次。

空间复杂度：O(1)O(1)，不使用额外空间。

下一篇：纯C，利用当前层的 next 指针构造下层的 next，【117.填充每个节点的下一个右侧节点II】【思路清晰，代码易读】

public class Solution {
    public Node Connect(Node root) {
        if(root == null)
            return null;
        Queue<Node> queue = new Queue<Node>();
        queue.Enqueue(root);
        while(queue.Count > 0){
            int count = queue.Count;
            Node node0 = queue.Dequeue();
            if(node0.left != null)
                queue.Enqueue(node0.left);
            if(node0.right != null)
                queue.Enqueue(node0.right);
            for(int i = 1; i < count; i++){
                Node node1 = queue.Dequeue();
                node0.next = node1;
                if(node1.left != null)
                    queue.Enqueue(node1.left);
                if(node1.right != null)
                    queue.Enqueue(node1.right);
                node0 = node1;
            }
        }
        return root;
    }
}

public class Solution {
    public Node Connect(Node root) {
        Node mostLeft = root;
            while (mostLeft != null)
            {
                Node cur = mostLeft;

                ConnectChild(cur);

                mostLeft = GetMostLeftNode(mostLeft);
            }
            return root;
    }

    //获取最左边节点
    private Node GetMostLeftNode(Node root)
        {
            while (root != null)
            {
                if (root.left != null)
                {
                    return root.left;
                }
                else if (root.right != null)
                {
                    return root.right;
                }
                root = root.next;
            }
            return null;
        }

        //给一个头节点，将头节点所属层的子节点全部连接
        private void ConnectChild(Node root)
        {
            Node pre = null;
            Queue<Node> queue = new Queue<Node>(2);//队列，大小为2，仅保存两个节点
            while(root != null)
            {
                if (root.left != null) queue.Enqueue(root.left);
                if(queue.Count == 2)//队列满了，则全部弹出，并将next指针连好
                {
                    Node left = queue.Dequeue();
                    Node right = queue.Dequeue();
                    left.next = right;
                    if (pre != null)
                        pre.next = left;                       
                    pre = right;
                }
                if (root.right != null) queue.Enqueue(root.right);
                if (queue.Count == 2)
                {
                    Node left = queue.Dequeue();
                    Node right = queue.Dequeue();
                    left.next = right;
                   if (pre != null)
                        pre.next = left;                       
                    pre = right;
                }
                root = root.next;
            }
            if(pre !=null && queue.Count > 0)//看队列中是否有值，有则将其连接
            {
                Node node = queue.Dequeue();
                pre.next = node;
            }
        }
}

public class Solution {
    public Node Connect(Node root) {
        if(root == null)
            return null;
        Node node = root.next;
        while(node != null)
        {
            if(node.left != null)
            {
                node = node.left;
                break;
            }
            else if(node.right != null)
            {
                node = node.right;
                break;
            }
            else
            {
                node = node.next;
            }
        }

        if(root.left != null)
            root.left.next = root.right == null ? node : root.right;
        
        if(root.right != null)
            root.right.next = node;
        
        Connect(root.right);
        Connect(root.left);

        return root;
    }
}

public class Solution {
    public Node Connect(Node root) {
         if (root == null) return null;
            Node cur = root;
            while (cur != null) //cur每次向next移动，直到为null退出
            {
                Node nextLevelHead = new Node();//每层头节点, dummy 指针在链表中经常用到，他只是为了处理头结点的情况，它并不属于当前链表。
                Node nextLevel = nextLevelHead;
                while (cur != null) //遍历 cur 的当前层
                {
                    if (cur.left != null)  //如果 cur 的孩子不为 null 就将它接到 tail 后边，然后更新tail。
                    {
                        nextLevel.next = cur.left;
                        nextLevel = nextLevel.next;
                    }
                    if (cur.right != null) //如果 cur 的孩子不为 null 就将它接到 tail 后边，然后更新tail。
                    {
                        nextLevel.next = cur.right;
                        nextLevel = nextLevel.next;
                    }

                    cur = cur.next; //cur每次向next移动，直到为null退出
                }

                //更新 cur 到下一层
                cur = nextLevelHead.next; //当 cur 为 null 的时候，再利用 dummy 指针得到新的一层的开始节点。
            }
            return root;
    }
}


 
 
 
 
 
 
*/
