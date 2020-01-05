using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一颗根结点为 root 的二叉树，书中的每个结点都有一个从 0 到 25 的值，分别代表字母 'a' 到 'z'：值 0 代表 'a'，值 1 代表 'b'，依此类推。

找出按字典序最小的字符串，该字符串从这棵树的一个叶结点开始，到根结点结束。

（小贴士：字符串中任何较短的前缀在字典序上都是较小的：例如，在字典序上 "ab" 比 "aba" 要小。叶结点是指没有子结点的结点。）

 

示例 1：



输入：[0,1,2,3,4,3,4]
输出："dba"
示例 2：



输入：[25,1,3,1,3,0,2]
输出："adz"
示例 3：



输入：[2,2,1,null,1,0,null,0]
输出："abc"
 

提示：

给定树的结点数介于 1 和 8500 之间。
树中的每个结点都有一个介于 0 和 25 之间的值。
*/
/// <summary>
/// https://leetcode-cn.com/problems/smallest-string-starting-from-leaf/
/// 988. 从叶结点开始的最小字符串
/// 
/// </summary>
class SmallestStringStartingFromLeafSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    public string SmallestFromLeaf(TreeNode root)
    {
        Dfs(root, new StringBuilder());
        return _ret;
    }

    public void Dfs(TreeNode node, StringBuilder sb)
    {
        if (node == null) return;
        
        const int a = 'a';
        sb.Insert(0, (char)(a + node.val));

        if (node.left == null && node.right == null)
        {
            var compare = Compare(_ret, sb);
            if( 0 < compare)
            {
                _ret = sb.ToString();
                sb.Remove(0, 1);
                return;
            }
        }

        Dfs(node.left, sb);
        Dfs(node.right, sb);
        sb.Remove(0, 1);
    }

    private static int Compare(string s, StringBuilder sb)
    {
        int len1 = s.Length;
        int len2 = sb.Length;
        for (int i = 0; i < len1 && i < len2; i++)
        {
            var c1 = s[i];
            var c2 = sb[i];
            if (c2 < c1) return 1;
            if (c1 < c2) return -1;
        }
        if (len1 == len2) return 0;
        return len1 < len2 ? -1 : 1;
    }

    private string _ret = "~";
}
/*
从叶结点开始的最小字符串
力扣 (LeetCode)
发布于 1 年前
1.6k 阅读
方法：暴力法
思路

让我们创建出所有可能的字符串，然后逐一比较，输出字典序最小的那个。

算法

在我们深度优先搜索的过程中，我们不断调整 sb（或者 Python 代码中的 A），即根到这个节点的路径上的字符串内容。

当我们到达一个叶子节点的时候，我们翻转路径的字符串内容来创建一个候选答案。如果候选答案比当前答案要优秀，那么我们更新答案。

class Solution {
    String ans = "~";
    public String smallestFromLeaf(TreeNode root) {
        dfs(root, new StringBuilder());
        return ans;
    }

    public void dfs(TreeNode node, StringBuilder sb) {
        if (node == null) return;
        sb.append((char)('a' + node.val));

        if (node.left == null && node.right == null) {
            sb.reverse();
            String S = sb.toString();
            sb.reverse();

            if (S.compareTo(ans) < 0)
                ans = S;
        }

        dfs(node.left, sb);
        dfs(node.right, sb);
        sb.deleteCharAt(sb.length() - 1);
    }
}
复杂度分析

时间复杂度：我们用 O(N)O(N) 遍历这棵树，然后调整字符串内容 A [Python] 或者 sb。然后，翻转与比较的时间复杂度为 O(L)O(L)，其中 LL 是到达叶节点时候得到字符串的长度。例如，对于完全平衡的树，L = \log NL=logN 且时间复杂度为 O(N \log N)O(NlogN)。

空间复杂度：O(N)O(N)。 
*/
