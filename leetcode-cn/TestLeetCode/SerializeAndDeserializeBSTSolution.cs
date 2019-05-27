using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
序列化是将数据结构或对象转换为一系列位的过程，以便它可以存储在文件或内存缓冲区中，或通过网络连接链路传输，以便稍后在同一个或另一个计算机环境中重建。

设计一个算法来序列化和反序列化二叉搜索树。 对序列化/反序列化算法的工作方式没有限制。 您只需确保二叉搜索树可以序列化为字符串，并且可以将该字符串反序列化为最初的二叉搜索树。

编码的字符串应尽可能紧凑。

注意：不要使用类成员/全局/静态变量来存储状态。 你的序列化和反序列化算法应该是无状态的。 
*/
/// <summary>
/// https://leetcode-cn.com/problems/serialize-and-deserialize-bst/
/// 449. 序列化和反序列化二叉搜索树
/// https://www.cnblogs.com/TIMHY/p/9191122.html
/// </summary>
class SerializeAndDeserializeBSTSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    // Encodes a tree to a single string.
    public string serialize(TreeNode root)
    {
        StringBuilder builder = new StringBuilder();
        serialize(root, builder);
        return builder.ToString();
    }

    // Decodes your encoded data to tree.
    public TreeNode deserialize(string data)
    {
        if (string.IsNullOrWhiteSpace(data)) return null;
        int index = 0;
        return deserialize(data, ref index);
    }

    private static TreeNode deserialize(string data, ref int index )
    {
        if ( data.Length <= index ) return null;
        if(data[index] == EndChar)
        {
            index++;
            return null;
        }
        int val = 0;
        do
        {
            val = val * 10 + (data[index++] - '0');
        } while (data[index] != EndChar);

        index++;
        var ret = new TreeNode(val);
        ret.left = deserialize(data, ref index);
        ret.right = deserialize(data, ref index);
        return ret;
    }

    private const char EndChar = '|';
    private static void serialize(TreeNode root, StringBuilder builder)
    {
        if (root == null)
        {
            builder.Append(EndChar);
            return;
        }

        builder.Append(root.val);
        builder.Append(EndChar);

        serialize(root.left, builder);
        serialize(root.right, builder);
    }
}