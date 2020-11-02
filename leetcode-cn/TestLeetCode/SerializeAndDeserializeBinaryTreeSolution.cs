/*
序列化是将一个数据结构或者对象转换为连续的比特位的操作，进而可以将转换后的数据存储在一个文件或者内存中，同时也可以通过网络传输到另一个计算机环境，采取相反方式重构得到原数据。

请设计一个算法来实现二叉树的序列化与反序列化。这里不限定你的序列 / 反序列化算法执行逻辑，你只需要保证一个二叉树可以被序列化为一个字符串并且将这个字符串反序列化为原始的树结构。

示例: 

你可以将以下二叉树：

    1
   / \
  2   3
     / \
    4   5

序列化为 "[1,2,3,null,null,4,5]"
提示: 这与 LeetCode 目前使用的方式一致，详情请参阅 LeetCode 序列化二叉树的格式。你并非必须采取这种方式，你也可以采用其他的方法解决这个问题。

说明: 不要使用类的成员 / 全局 / 静态变量来存储状态，你的序列化和反序列化算法应该是无状态的。

*/

/// <summary>
/// https://leetcode-cn.com/problems/serialize-and-deserialize-binary-tree/
/// 297. 二叉树的序列化与反序列化
///
/// </summary>
internal class SerializeAndDeserializeBinaryTreeSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public string serialize(TreeNode root)
    {
        return root == null ? "X" : $"({serialize(root.left)}){root.val}({serialize(root.right)})";
    }

    public TreeNode deserialize(string data)
    {
        if (string.IsNullOrEmpty(data)) return null;
        int index = 0;
        return Parse(data, ref index);

        static TreeNode Parse(string data, ref int index)
        {
            if (data[index] == 'X')
            {
                ++index;
                return null;
            }

            return new TreeNode(0)
            {
                left = ParseSubtree(data, ref index),
                val = ParseValue(data, ref index),
                right = ParseSubtree(data, ref index)
            };
        }

        static TreeNode ParseSubtree(string data, ref int index)
        {
            ++index; // 跳过左括号
            TreeNode subtree = Parse(data, ref index);
            ++index; // 跳过右括号
            return subtree;
        }

        static int ParseValue(string data, ref int index)
        {
            int ret = 0;
            bool minus = false;
            if (data[index] == '-')
            {
                minus = true;
                ++index;
            }

            char c;
            while ((c = data[index]) != '(')
            {
                ret = ret * 10 + (c - '0');
                ++index;
            }
            return minus ? -ret : ret;
        }
    }
}
/*
二叉树的序列化与反序列化
力扣官方题解
发布于 2020-06-15
32.5k
方法一：深度优先搜索
思路和算法

二叉树的序列化本质上是对其值进行编码，更重要的是对其结构进行编码。可以遍历树来完成上述任务。众所周知，我们一般有两个策略：BFS / DFS。

BFS 可以按照层次的顺序从上到下遍历所有的节点
DFS 可以从一个根开始，一直延伸到某个叶，然后回到根，到达另一个分支。根据根节点、左节点和右节点之间的相对顺序，可以进一步将DFS策略区分为：
先序遍历
中序遍历
后序遍历
这里，我们选择先序遍历的编码方式，我们可以通过这样一个例子简单理解：



我们从根节点 1 开始，序列化字符串是 1,。然后我们跳到根节点 2 的左子树，序列化字符串变成 1,2,。现在从节点 2 开始，我们访问它的左节点 3（1,2,3,None,None,）和右节点 4

(1,2,3,None,None,4,None,None)。None,None, 是用来标记缺少左、右子节点，这就是我们在序列化期间保存树结构的方式。最后，我们回到根节点 1 并访问它的右子树，它恰好是叶节点 5。最后，序列化字符串是 1,2,3,None,None,4,None,None,5,None,None,。

即我们可以先序遍历这颗二叉树，遇到空子树的时候序列化成 None，否则继续递归序列化。那么我们如何反序列化呢？首先我们需要根据 , 把原先的序列分割开来得到先序遍历的元素列表，然后从左向右遍历这个序列：

如果当前的元素为 None，则当前为空树
否则先解析这棵树的左子树，再解析它的右子树
具体请参考下面的代码。

代码


public class Codec {
    public String rserialize(TreeNode root, String str) {
        if (root == null) {
            str += "None,";
        } else {
            str += str.valueOf(root.val) + ",";
            str = rserialize(root.left, str);
            str = rserialize(root.right, str);
        }
        return str;
    }
  
    public String serialize(TreeNode root) {
        return rserialize(root, "");
    }
  
    public TreeNode rdeserialize(List<String> l) {
        if (l.get(0).equals("None")) {
            l.remove(0);
            return null;
        }
  
        TreeNode root = new TreeNode(Integer.valueOf(l.get(0)));
        l.remove(0);
        root.left = rdeserialize(l);
        root.right = rdeserialize(l);
    
        return root;
    }
  
    public TreeNode deserialize(String data) {
        String[] data_array = data.split(",");
        List<String> data_list = new LinkedList<String>(Arrays.asList(data_array));
        return rdeserialize(data_list);
    }
}
复杂度分析

时间复杂度：在序列化和反序列化函数中，我们只访问每个节点一次，因此时间复杂度为 O(n)O(n)，其中 nn 是节点数，即树的大小。
空间复杂度：在序列化和反序列化函数中，我们递归会使用栈空间，故渐进空间复杂度为 O(n)O(n)。
方法二：括号表示编码 + 递归下降解码
思路和算法

我们也可以这样表示一颗二叉树：

如果当前的树为空，则表示为 X
如果当前的树不为空，则表示为 (<LEFT_SUB_TREE>)CUR_NUM(RIGHT_SUB_TREE)，其中：
<LEFT_SUB_TREE> 是左子树序列化之后的结果
<RIGHT_SUB_TREE> 是右子树序列化之后的结果
CUR_NUM 是当前节点的值
根据这样的定义，我们很好写出序列化的过程，后序遍历这颗二叉树即可，那如何反序列化呢？根据定义，我们可以推导出这样的巴科斯范式（BNF）：


T -> (T) num (T) | X
它的意义是：用 T 代表一棵树序列化之后的结果，| 表示 T 的构成为 (T) num (T) 或者 X，| 左边是对 T 的递归定义，右边规定了递归终止的边界条件。

因为：

T 的定义中，序列中的第一个字符要么是 X，要么是 (，所以这个定义是不含左递归的
当我们开始解析一个字符串的时候，如果开头是 X，我们就知道这一定是解析一个「空树」的结构，如果开头是 (，我们就知道需要解析 (T) num (T) 的结构，因此这里两种开头和两种解析方法一一对应，可以确定这是一个无二义性的文法
我们只需要通过开头的第一个字母是 X 还是 ( 来判断使用哪一种解析方法
所以这个文法是 LL(1) 型文法，如果你不知道什么是 LL(1) 型文法也没有关系，你只需要知道它定义了一种递归的方法来反序列化，也保证了这个方法的正确性——我们可以设计一个递归函数：

这个递归函数传入两个参数，带解析的字符串和当前当解析的位置 ptr，ptr 之前的位置是已经解析的，ptr 和 ptr 后面的字符串是待解析的
如果当前位置为 X 说明解析到了一棵空树，直接返回
否则当前位置一定是 (，对括号内部按照 (T) num (T) 的模式解析
具体请参考下面的代码。

代码


public class Codec {
    public String serialize(TreeNode root) {
        if (root == null) {
            return "X";
        }
        String l = "(" + serialize(root.left) + ")";
        String r = "(" + serialize(root.right) + ")";
        return  l + root.val + r;
    }

    public TreeNode deserialize(String data) {
        int[] ptr = {0};
        return parse(data, ptr);
    }

    public TreeNode parse(String data, int[] ptr) {
        if (data.charAt(ptr[0]) == 'X') {
            ++ptr[0];
            return null;
        }
        TreeNode cur = new TreeNode(0);
        cur.left = parseSubtree(data, ptr);
        cur.val = parseInt(data, ptr);
        cur.right = parseSubtree(data, ptr);
        return cur;
    }

    public TreeNode parseSubtree(String data, int[] ptr) {
        ++ptr[0]; // 跳过左括号
        TreeNode subtree = parse(data, ptr);
        ++ptr[0]; // 跳过右括号
        return subtree;
    }

    public int parseInt(String data, int[] ptr) {
        int x = 0, sgn = 1;
        if (!Character.isDigit(data.charAt(ptr[0]))) {
            sgn = -1;
            ++ptr[0];
        }
        while (Character.isDigit(data.charAt(ptr[0]))) {
            x = x * 10 + data.charAt(ptr[0]++) - '0';
        }
        return x * sgn;
    }
}
复杂度分析

时间复杂度：序列化时做了一次遍历，渐进时间复杂度为 O(n)O(n)。反序列化时，在解析字符串的时候 ptr 指针对字符串做了一次顺序遍历，字符串长度为 O(n)O(n)，故这里的渐进时间复杂度为 O(n)O(n)。
空间复杂度：考虑递归使用的栈空间的大小，这里栈空间的使用和递归深度有关，递归深度又和二叉树的深度有关，在最差情况下，二叉树退化成一条链，故这里的渐进空间复杂度为 O(n)O(n)。

using System.Runtime.InteropServices;

public class Codec {

    const short FixNull = -0x8000;
    const short FixIntMin = -0x7ffe;
    const short FixIntMax = 0x7fff;
    const short FixInt2 = -0x7fff;

    // Encodes a tree to a single string.
    public string serialize(TreeNode root) {
        var length = GetLength(root);

        var str = new string('\0', length);

        var span = MemoryMarshal.CreateSpan(
            ref MemoryMarshal.GetReference(str.AsSpan()),
            length);

        int offset = 0;

        Write(MemoryMarshal.Cast<char, short>(span), ref offset, root);

        // Assert(offset == length);

        return str;
    }

    // Decodes your encoded data to tree.
    public TreeNode deserialize(string data) {
        // Assert data.Length > 0

        int offset = 0;

        var node = Read(MemoryMarshal.Cast<char, short>(data.AsSpan()), ref offset);

        // Assert offset == data.Length

        return node;
    }

    public static int GetLength(TreeNode node){
        if(node is null) return 1;

        return (node.val >= FixIntMin && node.val <= FixIntMax ? 1 : 3) + GetLength(node.left) + GetLength(node.right);
    }

    public void Write(Span<short> values, ref int offset, TreeNode node){
        if(node is null){
            values[offset] = FixNull; ++offset;

            return;
        }
        
        if(node.val >= FixIntMin && node.val <= FixIntMax){
            values[offset] = (short)node.val; ++offset;
        }else{
            values[offset] = FixInt2; ++offset;
            values[offset] = (short)(((uint)node.val) >> 16); ++offset;
            values[offset] = (short)(((uint)node.val) & 0xffff); ++offset;
        }

        Write(values, ref offset, node.left);
        Write(values, ref offset, node.right);
    }

    public TreeNode Read(ReadOnlySpan<short> values, ref int offset){
        if(values[offset] == FixNull){
            ++offset;
            return null;
        }

        int value;

        if(values[offset] == FixInt2){
            ++offset;
            value = (int)((((uint)values[offset++]) << 16) | ((uint)values[offset++]));
        }else{
            value = values[offset++];
        }

        var node = new TreeNode(value);

        node.left = Read(values, ref offset);
        node.right = Read(values, ref offset);

        return node;
    }
}

public class Codec {

    // Encodes a tree to a single string.
    public string serialize(TreeNode root) {
        if (root == null) return string.Empty;

        var res = new List<string>();

        var queue = new Queue<TreeNode>();
        queue.Enqueue(root);

        while (queue.Count != 0){
            var node = queue.Dequeue();

            if (node == null){
                res.Add("null");
                continue;
            }

            res.Add(node.val.ToString());

            queue.Enqueue(node.left);
            queue.Enqueue(node.right);
        }

        return string.Join(",", res);
    }

    // Decodes your encoded data to tree.
    public TreeNode deserialize(string data) {
        //Console.WriteLine(data);
        if (string.IsNullOrEmpty(data)) return null;

        var nodes = data.Split(',');

        var root = new TreeNode(int.Parse(nodes[0]));
        var queue  = new Queue<TreeNode>();
        queue.Enqueue(root);

        int index = 1;

        while (queue.Count != 0){
            var node = queue.Dequeue();
            
            node.left = nodes[index] != "null" ? new TreeNode(int.Parse(nodes[index])) : null;
            ++index;
            if (node.left != null){
                queue.Enqueue(node.left);
            }

            node.right = nodes[index] != "null" ? new TreeNode(int.Parse(nodes[index])) : null;
            ++index;
            if (node.right != null){
                queue.Enqueue(node.right);
            }
        }
        return root;
    }
}

public class Codec {

    // Encodes a tree to a single string.
    public string serialize(TreeNode root)
    {
        var result = serialize2(root);
        return result.Substring(0, result.Length - 1);
    }

    private string serialize2(TreeNode root)
    {
        if (root == null)
        {
            return ",";
        }
        return root.val.ToString() + "," + serialize2(root.left) + serialize2(root.right);
    }

    // Decodes your encoded data to tree.
    public TreeNode deserialize(string data)
    {
        if(String.IsNullOrEmpty(data))
        {
            return null;
        }
        var arr = data.Split(',');
        int index = 0;
        return deserialize2(arr, ref index);
    }

    public TreeNode deserialize2(string[] arr, ref int index)
    {
        if(index >= arr.Length)
        {
            return null;
        }

        if(arr[index] == "")
        {
            index++;
            return null;
        }
        else
        {
            var node = new TreeNode(int.Parse(arr[index]));
            index++;
            node.left = deserialize2(arr, ref index);
            node.right = deserialize2(arr,ref index);
            return node;
        }
    }
}


 
 
*/