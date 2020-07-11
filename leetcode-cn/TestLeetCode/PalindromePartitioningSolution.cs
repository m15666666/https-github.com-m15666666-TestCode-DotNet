using System.Collections.Generic;

/*
给定一个字符串 s，将 s 分割成一些子串，使每个子串都是回文串。

返回 s 所有可能的分割方案。

示例:

输入: "aab"
输出:
[
  ["aa","b"],
  ["a","a","b"]
]

*/

/// <summary>
/// https://leetcode-cn.com/problems/palindrome-partitioning/
/// 131.分隔回文串
/// 给定一个字符串 s，将 s 分割成一些子串，使每个子串都是回文串。
/// 返回 s 所有可能的分割方案。
/// 示例:
/// 输入: "aab"
/// 输出:
/// [
/// ["aa","b"],
/// ["a","a","b"]
/// ]
/// https://blog.csdn.net/Ding_xiaofei/article/details/81946621
/// </summary>
internal class PalindromePartitioningSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public IList<IList<string>> Partition(string s)
    {
        List<IList<string>> ret = new List<IList<string>>();
        if (string.IsNullOrEmpty(s)) return ret;

        int length = s.Length;
        var dp = new bool[length, length];
        for (int right = 0; right < length; right++)
            for (int left = 0; left <= right; left++)
                if (s[left] == s[right] && (right - left <= 2 || dp[left + 1, right - 1]))
                    dp[left, right] = true;

        Stack<string> stack = new Stack<string>();
        BackTrack(0);
        return ret;

        void BackTrack(int startIndex)
        {
            if (startIndex == length)
            {
                string[] texts = new string[stack.Count];
                int textIndex = texts.Length - 1;
                foreach (var text in stack)
                    texts[textIndex--] = text;
                ret.Add(texts);
                return;
            }

            for (int stopIndex = startIndex; stopIndex < length; stopIndex++)
            {
                if (!dp[startIndex, stopIndex]) continue;

                var subString = s.Substring(startIndex, stopIndex - startIndex + 1);
                stack.Push(subString);
                BackTrack(stopIndex + 1);
                stack.Pop();
            }
        }
    }

    //public IList<IList<string>> Partition(string s)
    //{
    //    List<IList<string>> ret = new List<IList<string>>();

    //    if (string.IsNullOrWhiteSpace(s)) return ret;

    //    Stack<string> stack = new Stack<string>();

    //    BackTrack(s, 0, 0, stack, ret);

    //    return ret;
    //}

    //private void BackTrack( string s, int startIndex, int totalLength, Stack<string> stack, List<IList<string>> ret )
    //{
    //    var length = s.Length;
    //    {
    //        //int totalLength = 0;
    //        if( totalLength == length )
    //        {
    //            string[] texts = new string[stack.Count];
    //            int textIndex = texts.Length - 1;
    //            foreach (var text in stack)
    //            {
    //                texts[textIndex--] = text;
    //                //totalLength += text.Length;
    //            }
    //            ret.Add(texts);
    //            return;
    //        }
    //        //if ( length <= startIndex ) return;
    //    }

    //    for( int stopIndex = startIndex; stopIndex < length; stopIndex++ )
    //    {
    //        var subStringCount = stopIndex - startIndex + 1;
    //        if (!IsPalindrome(s, startIndex, subStringCount )) continue;
    //        var subString = s.Substring(startIndex, subStringCount);

    //        stack.Push(subString);

    //        BackTrack(s, stopIndex + 1, totalLength + subStringCount, stack, ret);

    //        stack.Pop();
    //    }
    //}

    //private static bool IsPalindrome( string s, int startIndex, int count )
    //{
    //    int stopIndex = startIndex + count - 1;
    //    while( startIndex <= stopIndex)
    //    {
    //        if (s[startIndex++] != s[stopIndex--]) return false;
    //    }
    //    return true;
    //}
}
/*

回溯、优化（使用动态规划预处理数组）
liweiwei1419
发布于 2019-12-12
16.0k
搜索问题主要使用回溯法。

回溯法思考的步骤：

1、画递归树；

2、根据自己画的递归树编码。

image.png

思考如何根据这棵递归树编码：

1、每一个结点表示剩余没有扫描到的字符串，产生分支是截取了剩余字符串的前缀；

2、产生前缀字符串的时候，判断前缀字符串是否是回文。

如果前缀字符串是回文，则可以产生分支和结点；
如果前缀字符串不是回文，则不产生分支和结点，这一步是剪枝操作。
3、在叶子结点是空字符串的时候结算，此时从根结点到叶子结点的路径，就是结果集里的一个结果，使用深度优先遍历，记录下所有可能的结果。

采用一个路径变量 path 搜索，path 全局使用一个（注意结算的时候，需要生成一个拷贝），因此在递归执行方法结束以后需要回溯，即将递归之前添加进来的元素拿出去；
path 的操作只在列表的末端，因此合适的数据结构是栈。
方法一：回溯
参考代码 1：


import java.util.ArrayDeque;
import java.util.ArrayList;
import java.util.Deque;
import java.util.List;

public class Solution {

    public List<List<String>> partition(String s) {
        int len = s.length();
        List<List<String>> res = new ArrayList<>();
        if (len == 0) {
            return res;
        }

        // Stack 这个类 Java 的文档里推荐写成 Deque<Integer> stack = new ArrayDeque<Integer>();
        // 注意：只使用 stack 相关的接口
        Deque<String> stack = new ArrayDeque<>();
        backtracking(s, 0, len, stack, res);
        return res;
    }

    private void backtracking(String s, int start, int len, Deque<String> path, List<List<String>> res) {
        if (start == len) {
            res.add(new ArrayList<>(path));
            return;
        }

        for (int i = start; i < len; i++) {

            // 因为截取字符串是消耗性能的，因此，采用传子串索引的方式判断一个子串是否是回文子串
            // 不是的话，剪枝
            if (!checkPalindrome(s, start, i)) {
                continue;
            }

            path.addLast(s.substring(start, i + 1));
            backtracking(s, i + 1, len, path, res);
            path.removeLast();
        }
    }

    private boolean checkPalindrome(String str, int left, int right) {
        // 严格小于即可
        while (left < right) {
            if (str.charAt(left) != str.charAt(right)) {
                return false;
            }
            left++;
            right--;
        }
        return true;
    }
}
方法二：回溯的优化（加了动态规划）
在上一步，验证回文串那里，每一次都得使用“两边夹”的方式验证子串是否是回文子串。于是“用空间换时间”，利用「力扣」第 5 题：最长回文子串 的思路，利用动态规划把结果先算出来，这样就可以以 O(1)O(1) 的时间复杂度直接得到一个子串是否是回文。

参考代码 2：


import java.util.ArrayDeque;
import java.util.ArrayList;
import java.util.Deque;
import java.util.List;
import java.util.Stack;

public class Solution {

    public List<List<String>> partition(String s) {
        int len = s.length();
        List<List<String>> res = new ArrayList<>();
        if (len == 0) {
            return res;
        }

        // 预处理
        // 状态：dp[i][j] 表示 s[i][j] 是否是回文
        boolean[][] dp = new boolean[len][len];
        // 状态转移方程：在 s[i] == s[j] 的时候，dp[i][j] 参考 dp[i + 1][j - 1]
        for (int right = 0; right < len; right++) {
            // 注意：left <= right 取等号表示 1 个字符的时候也需要判断
            for (int left = 0; left <= right; left++) {
                if (s.charAt(left) == s.charAt(right) && (right - left <= 2 || dp[left + 1][right - 1])) {
                    dp[left][right] = true;
                }
            }
        }

        Deque<String> stack = new ArrayDeque<>();
        backtracking(s, 0, len, dp, stack, res);
        return res;
    }

    private void backtracking(String s,
                              int start,
                              int len,
                              boolean[][] dp,
                              Deque<String> path,
                              List<List<String>> res) {
        if (start == len) {
            res.add(new ArrayList<>(path));
            return;
        }

        for (int i = start; i < len; i++) {
            // 剪枝
            if (!dp[start][i]) {
                continue;
            }
            path.addLast(s.substring(start, i + 1));
            backtracking(s, i + 1, len, dp, path, res);
            path.removeLast();
        }
    }
}
下一篇：python3 递归 简单易懂

public class Solution {
public IList<IList<string>> Partition(string s)
{
	var res = new List<IList<string>>();
	
	if (string.IsNullOrEmpty(s)) return res;

	var dp = new bool[s.Length, s.Length];
	
	for (var i = 0; i < s.Length; i++)
	{
		dp[i, i] = true;
		
		for (var j = 0; j < i; j++)
		{
			if (s[i] == s[j] && (i - j < 2 || dp[i - 1, j + 1])) 
			{
				dp[i, j] = true;
			}
		}
	}
	
	BrackTrack(s, dp, new List<string>(), 0, res);
	return res;
}

public void BrackTrack(string s, bool[,] dp, List<string> current, int index, List<IList<string>> res) 
{
	if (index == s.Length) 
	{
		res.Add(current.ToList());
		return;
	}

	for (var i = index; i < s.Length; i++)
	{
		if (dp[i, index]) 
		{
			current.Add(s.Substring(index, i - index + 1));
			BrackTrack(s, dp, current, i + 1, res);
			current.RemoveAt(current.Count() - 1);
		}
	}
}
}

public class Solution {
    public IList<IList<string>> Partition(string s)
    {
        bool[,] dp = new bool[s.Length, s.Length];
        for (int i = 0; i < s.Length; i++)
        {
            InitializeDP(s, i, i, dp);
            InitializeDP(s, i, i + 1, dp);
        }
        List<IList<string>> ans = new List<IList<string>>();
        PartitionInternal(ans, s, 0, dp, new List<string>());
        return ans;
    }

    private void PartitionInternal(List<IList<string>> ans, string s, int index, bool[,] dp, List<string> path)
    {
        if (index == s.Length)
        {
            ans.Add(new List<string>(path));
            return;
        }
        for (int i = index; i < s.Length; i++)
        {
            if (dp[index, i])
            {
                path.Add(s.Substring(index, i - index + 1));
                PartitionInternal(ans, s, i + 1, dp, path);
                path.RemoveAt(path.Count - 1);
            }
        }
    }

    private void InitializeDP(string s, int left, int right, bool[,] dp)
    {
        while (left >= 0 && right < s.Length)
        {
            if (s[left] == s[right])
            {
                dp[left--, right++] = true;
            }
            else
            {
                break;
            }
        }
    }
}

public class Solution {
    public IList<IList<string>> Partition(string s)
    {
        var n = s.Length;
        var p = new List<int>[n + 1];
        for (var i = 0; i < n; i++)
        {
            p[i] = new List<int>();
            for (var j = 0; j <= i; j++)
            {
                if (isPalindrome(s, j, i))
                {
                    p[i].Add(j);
                }
            }
        }
        var result = new List<IList<string>>();
        Dfs(s, p, n - 1, new Stack<string>(), result);
        return result;
    }

    private void Dfs(string s, List<int>[] p, int cur, Stack<string> stack, List<IList<string>> result)
    {
        if (cur < 0)
        {
            result.Add(stack.ToList());
            return;
        }
        for (var i = 0; i < p[cur].Count; i++)
        {
            stack.Push(s.Substring(p[cur][i], cur - p[cur][i] + 1));
            Dfs(s, p, p[cur][i] - 1, stack, result);
            stack.Pop();
        }
    }

    private bool isPalindrome(string s, int start, int end)
    {
        while (start < end)
        {
            if (s[start++] != s[end--])
                return false;
        }
        return true;
    }
}

 
 
 
*/