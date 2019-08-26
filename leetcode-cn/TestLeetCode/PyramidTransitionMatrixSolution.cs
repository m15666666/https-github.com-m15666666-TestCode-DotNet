using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
现在，我们用一些方块来堆砌一个金字塔。 每个方块用仅包含一个字母的字符串表示，例如 “Z”。

使用三元组表示金字塔的堆砌规则如下：

(A, B, C) 表示，“C”为顶层方块，方块“A”、“B”分别作为方块“C”下一层的的左、右子块。当且仅当(A, B, C)是被允许的三元组，我们才可以将其堆砌上。

初始时，给定金字塔的基层 bottom，用一个字符串表示。一个允许的三元组列表 allowed，每个三元组用一个长度为 3 的字符串表示。

如果可以由基层一直堆到塔尖返回true，否则返回false。

示例 1:

输入: bottom = "XYZ", allowed = ["XYD", "YZE", "DEA", "FFF"]
输出: true
解析:
可以堆砌成这样的金字塔:
    A
   / \
  D   E
 / \ / \
X   Y   Z

因为符合('X', 'Y', 'D'), ('Y', 'Z', 'E') 和 ('D', 'E', 'A') 三种规则。
示例 2:

输入: bottom = "XXYX", allowed = ["XXX", "XXY", "XYX", "XYY", "YXZ"]
输出: false
解析:
无法一直堆到塔尖。
注意, 允许存在三元组(A, B, C)和 (A, B, D) ，其中 C != D.
注意：

bottom 的长度范围在 [2, 8]。
allowed 的长度范围在[0, 200]。
方块的标记字母范围为{'A', 'B', 'C', 'D', 'E', 'F', 'G'}。
*/
/// <summary>
/// https://leetcode-cn.com/problems/pyramid-transition-matrix/
/// 756. 金字塔转换矩阵
/// https://blog.csdn.net/koukehui0292/article/details/84580403
/// </summary>
class PyramidTransitionMatrixSolution
{
    public void Test()
    {
        var ret = PyramidTransition("DB", new string[] { "BDD", "ACA", "CBA", "BDC", "AAC", "DCB", "ABC", "DDA", "CCB" });
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public bool PyramidTransition(string bottom, IList<string> allowed)
    {
        const char A = 'A';
        const int Size = 'Z' - A + 1;
        lookup = new List<byte>[Size,Size];
        foreach( var allow in allowed)
        {
            int row = allow[0] - A;
            int column = allow[1] - A;
            byte code = (byte)(allow[2] - A);
            if (lookup[row,column] == null) lookup[row, column] = new List<byte>() { code };
            else lookup[row, column].Add(code);
        }

        return CheckBottom(bottom.Select(c => (byte)(c - A)).ToArray());
    }

    private bool CheckBottom(byte[] bottom)
    {
        int n = bottom.Length;
        if (n == 1) return true;
        for ( int i = n - 1; 0 < i; i--)
            if (lookup[bottom[i - 1], bottom[i]] == null) return false;

        // 找下一层
        List<byte[]> nextLevels = new List<byte[]>();
        Stack<byte> stack = new Stack<byte>();

        GetNextLevel(bottom, nextLevels, stack, n - 1);

        foreach (var nextLevel in nextLevels)
            if (CheckBottom(nextLevel)) return true;

        return false;
    }


    private void GetNextLevel(byte[] bottom, List<byte[]> nextLevels, Stack<byte> stack, int index )
    {
        foreach( var code in lookup[bottom[index-1], bottom[index]])
        {
            stack.Push(code);
            if (index == 1)
            {
                var copy = stack.ToArray();
                nextLevels.Add(copy);
            }
            else GetNextLevel(bottom, nextLevels, stack, index - 1);
            stack.Pop();
        }
    }

    private List<byte>[,] lookup;
}
/*
public class Solution {
    public bool PyramidTransition(string bottom, IList<string> allowed)
    {
            //*
            //* 验证金字塔是否能盖成
            //* 思路：
            //*  1.给了一层底，那么上一层的砖的可选项就是固定的了
            //*  2.若上一层的可选项都试验过了，然后还是不成，那么就得通知下一层换个方案了
            //*  3.若下一层是一开始约定好的，那么显然说明盖不成金字塔
            //*  4.若底层已有一块儿砖，说明已经到了塔尖，现有的方案是可行的
            //*  5.这是一种典型的回溯思路，即每一层都有很多种可选项，一个可选项不行，就接着试验另一种可选项
            //*  
            //* 时间复杂度：O(n^2)
            //* 空间复杂度：O(n^2)
            //*

        Dictionary<string, List<char>> allowedDic = new Dictionary<string, List<char>>();
        foreach (var allowItem in allowed)
        {
            var keyTemp = allowItem.Substring(0, 2);
            if (!allowedDic.ContainsKey(keyTemp)) allowedDic[keyTemp] = new List<char>();

            allowedDic[keyTemp].Add(allowItem.Substring(2, 1)[0]);
        }

        return BackTrace(bottom, allowedDic, 0, new List<char>());
    }

    private bool BackTrace(string bottom, Dictionary<string, List<char>> allowedDic, int pos, List<char> newBottom)
    {
        if (bottom.Length == 1) return true;
        if (bottom.Length - 1 == pos) return BackTrace(new string(newBottom.ToArray()), allowedDic, 0, new List<char>());

        var keyTemp = bottom.Substring(pos, 2);
        if (!allowedDic.ContainsKey(keyTemp)) return false;

        foreach (var optionItem in allowedDic[keyTemp])
        {
            newBottom.Add(optionItem);
            var resultTemp = BackTrace(bottom, allowedDic, pos + 1, newBottom);
            if (resultTemp) return true;

            newBottom.RemoveAt(newBottom.Count - 1);
        }

        return false;
    }
} 

*/