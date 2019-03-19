using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
序列化二叉树的一种方法是使用前序遍历。当我们遇到一个非空节点时，我们可以记录下这个节点的值。如果它是一个空节点，我们可以使用一个标记值记录，例如 #。

     _9_
    /   \
   3     2
  / \   / \
 4   1  #  6
/ \ / \   / \
# # # #   # #
例如，上面的二叉树可以被序列化为字符串 "9,3,4,#,#,1,#,#,2,#,6,#,#"，其中 # 代表一个空节点。

给定一串以逗号分隔的序列，验证它是否是正确的二叉树的前序序列化。编写一个在不重构树的条件下的可行算法。

每个以逗号分隔的字符或为一个整数或为一个表示 null 指针的 '#' 。

你可以认为输入格式总是有效的，例如它永远不会包含两个连续的逗号，比如 "1,,3" 。

示例 1:

输入: "9,3,4,#,#,1,#,#,2,#,6,#,#"
输出: true
示例 2:

输入: "1,#"
输出: false
示例 3:

输入: "9,#,#,1"
输出: false 
*/
/// <summary>
/// https://leetcode-cn.com/problems/verify-preorder-serialization-of-a-binary-tree/
/// 331. 验证二叉树的前序序列化
/// </summary>
class VerifyPreorderSerializationOfBinaryTreeSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    
    public bool IsValidSerialization(string preorder)
    {
        if (string.IsNullOrWhiteSpace(preorder)) return false;
        string[] nodes = preorder.Split(',');
        _index = 0;
        return IsValidSerialization(nodes) && _index == nodes.Length;
    }

    private int _index;
    private bool IsValidSerialization( string[] nodes )
    {
        var index = _index++;
        if (nodes.Length <= index) return false;

        var v = nodes[index];
        if ( v == "#" ) return true;

        if (!IsValidSerialization(nodes)) return false;
        if (!IsValidSerialization(nodes)) return false;
        return true;
    }
}
/*
public class Solution {
    public bool IsValidSerialization(string preorder) {
        Stack<string> ss = new Stack<string>();
        string[] str = preorder.Split(',');
        for(int i=0;i<str.Length;i++)
        {
            if(str[i]!="#")
            {
                ss.Push(str[i]);
            }
            else
            {
                while(ss.Count!=0&&ss.Peek()=="#")
                {
                    ss.Pop();
                    if(ss.Count==0)
                    {
                        return false;
                    }
                    ss.Pop();
                }
                ss.Push("#");
            }
        }

        return ss.Count == 1 && ss.Peek() == "#";
    }
}
public class Solution {
    public bool IsValidSerialization(string preorder) {
        string[] arr = preorder.Split(',');
        Stack<string> stack = new Stack<string>();
        for (int i = 0; i < arr.Length; i++)
        {
            stack.Push(arr[i]);
            while (stack.Any() && stack.Peek() == "#")
            {
                Stack<string> _stackBak = new Stack<string>();
                _stackBak.Push(stack.Pop());
                if(stack.Any() && stack.Peek()=="#")
                {
                    _stackBak.Push(stack.Pop());
                    if (stack.Any())
                    {                         
                        if(stack.Pop()!="#")
                        {
                            stack.Push("#");
                            continue;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    stack.Push(_stackBak.Pop());
                    break;
                }
            }
        }
        if(stack.Any())
        {
            var s = stack.Pop();
            return s == "#" && !stack.Any();
        }
        else
        {
            return false;
        }
    }
}
*/
