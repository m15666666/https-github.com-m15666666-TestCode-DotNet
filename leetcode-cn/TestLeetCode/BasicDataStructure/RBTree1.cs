namespace TestLeetCode.BasicDataStructure
{
    /// <summary>
    /// 红黑树
    /// 参考：https://www.cnblogs.com/abatei/archive/2008/12/17/1356565.html
    /// </summary>
    public class RBTree1
    {
        private Node Root; //头指针
        private Node[] path = new Node[32]; //记录访问路径上的结点
        private int p; //表示当前访问到的结点在_path上的索引

        public Node Head => Root;

        public bool Add(int value) //添加一个元素
        {
            //如果是空树，则新结点成为二叉排序树的根
            if (Root == null)
            {
                Root = new Node { Value = value, IsRed = false };
                return true;
            }
            p = 0;
            //parent为上一次访问的结点，current为当前访问结点
            Node parent = null, current = Root;
            while (current != null)
            {
                path[p++] = current; //将路径上的结点插入数组
                //如果插入值已存在，则插入失败
                if (current.Value == value) return false;
                parent = current;
                //当插入值小于当前结点，则继续访问左子树，否则访问右子树
                current = (value < parent.Value) ? parent.Left : parent.Right;
            }
            current = new Node { Value = value, IsRed = true }; //创建新结点
            //如果插入值小于双亲结点的值
            if (value < parent.Value) parent.Left = current; //成为左孩子
            //如果插入值大于双亲结点的值
            else parent.Right = current; //成为右孩子
            if (!parent.IsRed) return true;
            path[p] = current;
            //回溯并旋转
            while ((p -= 2) >= 0) //现在p指向插入点的祖父结点
            {
                Node grandParent = path[p];
                parent = path[p + 1];
                if (!parent.IsRed) break;
                Node uncle = grandParent.Left == parent ? grandParent.Right : grandParent.Left;
                current = path[p + 2];
                // 叔父存在并且为红色的情况
                if (IsRed(uncle))
                {
                    parent.IsRed = false;
                    uncle.IsRed = false;
                    // 如果祖父不是根结点，则将其染成红色
                    if (0 < p) grandParent.IsRed = true;
                }
                //叔父不存在或为黑的情况需要旋转
                else
                {
                    Node newRoot;
                    //如果当前结点及父结点同为左孩子或右孩子
                    if (grandParent.Left == parent) newRoot = (parent.Left == current) ? LL(grandParent) : LR(grandParent);
                    else newRoot = (parent.Right == current) ? RR(grandParent) : RL(grandParent);

                    grandParent.IsRed = true; //祖父染成红色
                    newRoot.IsRed = false; //新根染成黑色
                    //将新根同曾祖父连接
                    ReplaceChildOfNodeOrRoot((p > 0) ? path[p - 1] : null, grandParent, newRoot);
                    return true; //旋转后不需要继续回溯，添加成功，直接退出
                }
            }
            return true;
        }

        //旋转根旋转后换新根
        private void ReplaceChildOfNodeOrRoot(Node parent, Node child, Node newChild)
        {
            if (parent != null)
            {
                if (parent.Left == child) parent.Left = newChild;
                else parent.Right = newChild;
                return;
            }
            Root = newChild;
        }

        private static bool IsBlack(Node node) => (node != null) && !node.IsRed;

        private static bool IsNullOrBlack(Node node) => node != null ? !node.IsRed : true;

        private static bool IsRed(Node node) => (node != null) && node.IsRed;

        //删除指定值
        public bool Remove(int value)
        {
            p = -1;
            //parent表示双亲结点，node表示当前结点
            Node node = Root;
            //寻找指定值所在的结点
            while (node != null)
            {
                path[++p] = node;
                //如果找到，则调用RemoveNode方法删除结点
                if (value == node.Value)
                {
                    RemoveNode(node);//现在p指向被删除结点
                    return true; //返回true表示删除成功
                }
                //如果删除值小于当前结点，则向左子树继续寻找
                if (value < node.Value) node = node.Left;
                //如果删除值大于当前结点，则向右子树继续寻找
                else node = node.Right;
            }
            return false; //返回false表示删除失败
        }

        //删除指定结点
        private void RemoveNode(Node node)
        {
            Node tmp = null; //tmp最终指向实际被删除的结点
            //当被删除结点存在左右子树时
            if (node.Left != null && node.Right != null)
            {
                tmp = node.Left; //获取左子树
                path[++p] = tmp;
                while (tmp.Right != null) //获取node的中序遍历前驱结点，并存放于tmp中
                {   //找到左子树中的最右下结点
                    tmp = tmp.Right;
                    path[++p] = tmp;
                }
                //用中序遍历前驱结点的值代替被删除结点的值
                node.Value = tmp.Value;
            }
            else tmp = node;

            //当只有左子树或右子树或为叶子结点时
            //首先找到惟一的孩子结点
            Node newTmp = tmp.Left;
            //如果只有右孩子或没孩子
            if (newTmp == null) newTmp = tmp.Right;
            if (p > 0)
            {
                Node parent = path[p - 1];
                //如果被删结点是左孩子
                if (parent.Left == tmp) parent.Left = newTmp;
                //如果被删结点是右孩子
                else parent.Right = newTmp;
                if (!tmp.IsRed && IsRed(newTmp))
                {
                    newTmp.IsRed = false;
                    return;
                }
            }
            else  //当删除的是根结点时
            {
                Root = newTmp;
                if (Root != null) Root.IsRed = false;
                return;
            }
            path[p] = newTmp;

            //如果被删除的是红色结点，则它必定是叶子结点，删除成功，返回
            if (IsRed(tmp)) return;

            //删除完后进行旋转，现在p指向实际被删除的位置,这个位置可能为空,tmp指向被删除的旧点，
            while (0 < p)
            {   //剩下的是双黑的情况
                //首先找到兄弟结点
                Node current = path[p];
                Node parent = path[p - 1];
                bool currentIsLeft = (parent.Left == current);
                Node sibling = currentIsLeft ? parent.Right : parent.Left;
                //红兄的情况，需要LL旋转或RR旋转
                if (IsRed(sibling))
                {
                    Node newRoot;
                    if (currentIsLeft) newRoot = RR(parent);
                    else newRoot = LL(parent);

                    ReplaceChildOfNodeOrRoot(p > 1 ? path[p - 2] : null, parent, newRoot);
                    sibling.IsRed = false;
                    parent.IsRed = true;
                    //回溯点降低
                    path[p - 1] = newRoot;
                    path[p] = parent;
                    path[++p] = current;
                    continue;
                }
                //黑兄的情况
                //黑兄二黑侄
                if (IsNullOrBlack(sibling.Left) && IsNullOrBlack(sibling.Right))
                {  //红父的情况
                    if (parent.IsRed)
                    {
                        parent.IsRed = false;
                        sibling.IsRed = true;
                        if (current != null)
                        {
                            current.IsRed = false;
                        }
                        break; //删除成功
                    }
                    else //黑父的情况
                    {
                        parent.IsRed = IsRed(current);
                        if (current != null)
                        {
                            current.IsRed = false;
                        }
                        sibling.IsRed = true;
                        p--; //需要继续回溯
                    }
                }
                else //黑兄红侄
                {
                    Node newRoot;
                    if (currentIsLeft) //兄弟在右边
                    {
                        if (IsRed(sibling.Right)) //红侄在右边
                        {  //RR型旋转
                            newRoot = RR(parent);
                            sibling.Right.IsRed = false;
                        }
                        else
                        {  //RL型旋转
                            newRoot = RL(parent);
                        }
                    }
                    else //兄弟在左边
                    {
                        if (IsRed(sibling.Left))
                        {  //LL型旋转
                            newRoot = LL(parent);
                            sibling.Left.IsRed = false;
                        }
                        else
                        {  //LR型旋转
                            newRoot = LR(parent);
                        }
                    }
                    if (current != null)
                    {
                        current.IsRed = false;
                    }
                    newRoot.IsRed = parent.IsRed;
                    parent.IsRed = false;
                    ReplaceChildOfNodeOrRoot(p > 1 ? path[p - 2] : null, parent, newRoot);
                    break; //不需要回溯，删除成功
                }
            }
        }

        //root为旋转根，rootPrev为旋转根双亲结点
        private Node LL(Node root) //LL型旋转，返回旋转后的新子树根
        {
            Node left = root.Left;
            root.Left = left.Right;
            left.Right = root;
            return left;
        }

        private Node LR(Node root) //LR型旋转，返回旋转后的新子树根
        {
            Node left = root.Left;
            Node right = left.Right;
            root.Left = right.Right;
            right.Right = root;
            left.Right = right.Left;
            right.Left = left;
            return right;
        }

        private Node RR(Node root) //RR型旋转，返回旋转后的新子树根
        {
            Node right = root.Right;
            root.Right = right.Left;
            right.Left = root;
            return right;
        }

        private Node RL(Node root) //RL型旋转，返回旋转后的新子树根
        {
            Node right = root.Right;
            Node left = right.Left;
            root.Right = left.Left;
            left.Left = root;
            right.Left = left.Right;
            left.Right = right;
            return left;
        }

        public Node LowerBound( int lowerBound ) => Root?.LowerBound(lowerBound);

        public class Node
        {
            public int Value { get; set; }
            public Node Left { get; set; }
            public Node Right { get; set; }
            public bool IsRed { get; set; }

            public Node LowerBound(int lowerBound)
            {
                if (lowerBound == Value) return this;

                if(lowerBound < Value)
                {
                    var ret = Left?.LowerBound(lowerBound);
                    return ret ?? this;
                }
                return (Right?.LowerBound(lowerBound));
            }

            public override string ToString() => $"Node: [value = {Value}]";
        }
    }
}