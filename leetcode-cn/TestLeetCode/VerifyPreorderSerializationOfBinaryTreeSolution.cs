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
internal class VerifyPreorderSerializationOfBinaryTreeSolution
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
        const char NULL = '#';
        int slots = 1;
        int n = preorder.Length;
        for (int i = 0; i < n; ++i)
        {
            if (preorder[i] == ',')
            {
                --slots;

                if (slots < 0) return false;

                if (preorder[i - 1] != NULL) slots += 2;
            }
        }
        --slots;

        return (preorder[n - 1] == NULL ? slots : slots + 2) == 0;
    }

    //public bool IsValidSerialization(string preorder)
    //{
    //    if (string.IsNullOrWhiteSpace(preorder)) return false;
    //    string[] nodes = preorder.Split(',');
    //    _index = 0;
    //    return IsValidSerialization(nodes) && _index == nodes.Length;
    //}

    //private int _index;
    //private bool IsValidSerialization( string[] nodes )
    //{
    //    var index = _index++;
    //    if (nodes.Length <= index) return false;

    //    var v = nodes[index];
    //    if ( v == "#" ) return true;

    //    if (!IsValidSerialization(nodes)) return false;
    //    if (!IsValidSerialization(nodes)) return false;
    //    return true;
    //}
}

/*
验证二叉树的前序序列化
力扣 (LeetCode)
发布于 2020-03-11
5.6k
方法一：迭代
思路

首先不考虑最优性，从最简单的解法来讨论这个问题。

我们可以定义一个概念，叫做槽位，二叉树中任意一个节点或者空孩子节点都要占据一个槽位。二叉树的建立也伴随着槽位数量的变化。开始时只有一个槽位，如果根节点是空节点，就只消耗掉一个槽位，如果根节点不是空节点，除了消耗一个槽位，还要为孩子节点增加两个新的槽位。之后的节点也是同理。

有了上面的讨论，方法就很简单了。依次遍历前序序列化，根据节点是否为空，按照规则消耗/增加槽位。如果最后可以将所有的槽位消耗完，那么这个前序序列化就是合法的。

开始时只有一个可用槽位。

空节点和非空节点都消耗一个槽位。

空节点不增加槽位，非空节点增加两个槽位。

fig

算法

初始化可用槽位：slots = 1。

根据逗号分隔前序序列化，将结果数组存储，随后遍历该数组：

空节点和非空节点都消耗一个槽位：slots = slot - 1.

如果当前的可用槽位是负数，那么这个前序序列化是非法的，返回 False。

非空节点（node != '#'）新增两个可用槽位：slots = slots + 2.

如果所有的槽位都消耗完，那么这个前序序列化就是合法的：返回 slots == 0。

实现


class Solution {
  public boolean isValidSerialization(String preorder) {
    // number of available slots
    int slots = 1;

    for(String node : preorder.split(",")) {
      // one node takes one slot
      --slots;

      // no more slots available
      if (slots < 0) return false;

      // non-empty node creates two children slots
      if (!node.equals("#")) slots += 2;
    }

    // all slots should be used up
    return slots == 0;
  }
}
复杂度分析

时间复杂度：O(N)O(N)，其中 N 为字符串的长度。

空间复杂度：O(N)O(N)。

方法二：一遍过
思路

方法一需要用到 O(N)O(N) 的空间来存储前序序列化分割之后的结果数组，但我们可以直接遍历前序序列化字符串，这样就不用开辟额外空间了。

在遍历过程中，每遇到逗号字符就更新可用槽位的数量。首先，将槽位减一（空节点和非空节点都要消耗一个槽位）。其次，如果当前节点是非空节点（即逗号字符前不是 #），新增两个槽位。

需要注意的是，最后一个节点需要单独处理，因为最后一个节点后面没有逗号字符。



算法

初始化可用槽位为 1：slots = 1。

遍历前序序列化字符串，每遍历到逗号字符：

空节点和非空节点都消耗一个槽位：slots = slot - 1。

如果当前可用槽位是负数，那么这个先序序列就是非法的，返回 False。

非空节点（即逗号字符前不是 #），新增两个可用槽位：slots = slots + 2`。

最后一个节点需要单独处理，因为最后一个节点后面是没有逗号的。

如果可用槽位全部被消耗完，那么该前序序列化就是合法的：返回 slots == 0。

实现


class Solution {
  public boolean isValidSerialization(String preorder) {
    // number of available slots
    int slots = 1;

    int n = preorder.length();
    for(int i = 0; i < n; ++i) {
      if (preorder.charAt(i) == ',') {
        // one node takes one slot
        --slots;

        // no more slots available
        if (slots < 0) return false;

        // non-empty node creates two children slots
        if (preorder.charAt(i - 1) != '#') slots += 2;
      }
    }

    // the last node
    slots = (preorder.charAt(n - 1) == '#') ? slots - 1 : slots + 1;
    // all slots should be used up
    return slots == 0;
  }
}
复杂度分析

时间复杂度：O(N)O(N)，其中 N 为字符串的长度。

空间复杂度：O(1)O(1)，只占用常数空间。

public class Solution {
    public bool IsValidSerialization(string preorder) {
        int left=0;
        int noleft=0;
        bool number=true;
        for(int i=0;i<preorder.Length;i++)
        {
            if(left>noleft) return false;
            if(preorder[i]==',')
            {
                number=true;
                continue;
            }
            else if(preorder[i]=='#')
            {
                left++;
                number=false;
            }
            else
            {
                if(number==true)
                {
                    noleft++;
                    number=false;
                }
            }
        }
        return left-noleft==1;
    }
}

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