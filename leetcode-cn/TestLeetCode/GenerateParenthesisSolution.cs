using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给出 n 代表生成括号的对数，请你写出一个函数，使其能够生成所有可能的并且有效的括号组合。

例如，给出 n = 3，生成结果为：

[
  "((()))",
  "(()())",
  "(())()",
  "()(())",
  "()()()"
]
*/
/// <summary>
/// https://leetcode-cn.com/problems/generate-parentheses/
/// 22. 括号生成
/// 
/// https://leetcode-cn.com/problems/generate-parentheses/solution/
/// </summary>
class GenerateParenthesisSolution
{
    public void Test()
    {
        var ret = GenerateParenthesis(3);
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public IList<string> GenerateParenthesis(int n)
    {
        if (n < 1) return new string[] { string.Empty};
        if (n == 1) return new string[] { "()" };

        _parenthesises = new Parentthesises[n + 1];
        var one = _parenthesises[1] = new Parentthesises(1);
        one.Parenthesis[1].Add(new char[] { Parentthesises.Left, Parentthesises.Right });

        // 生成2到n-1的组合
        for( int i = 2; i < n; i++) GetParenthesises(i);

        List<string> ret = new List<string>();
        var parentthesises = GetParenthesises(n);
        foreach (var list in parentthesises.Parenthesis)
        {
            if (list == null) continue;
            foreach (var l in list)
            {
                ret.Add(new string(l));
            }
        }

        return ret;
    }

    private Parentthesises[] _parenthesises;
    private Parentthesises GetParenthesises(int n)
    {
        if (_parenthesises[n] != null) return _parenthesises[n];

        var ret = new Parentthesises(n);
        _parenthesises[n] = ret;

        // 从1到n-1个括号开头的分类组合
        for ( int i = 1; i < n; i++ )
        {
            var left = _parenthesises[i];
            var right = _parenthesises[n - i];

            left.MergeRight(right, ret.Parenthesis[i]);
        }

        // 从第n-1个前后加括号生成第n个分类
        _parenthesises[n - 1].MergeLeft(ret.Parenthesis[n]);
        return ret;
    }

    public class Parentthesises
    {
        public const char Left = '(';
        public const char Right = ')';

        public int N;

        public Parentthesises(int n)
        {
            //if (n < 1) return;

            N = n;
            Parenthesis = new List<char[]>[n + 1];

            for (int i = 1; i <= n; i++)
                Parenthesis[i] = new List<char[]>();

            //if (n == 1) Parentthesis[1].Add(new char[] { Left, Right });
        }

        /// <summary>
        /// 从1到n个括号开头的分类组合
        /// </summary>
        public List<char[]>[] Parenthesis { get; set; }

        public void MergeRight(Parentthesises right, List<char[]> output)
        {
            int len = 2 * ( N + right.N );
            foreach( var l in Parenthesis[N] )
                foreach (var list in right.Parenthesis)
                {
                    if (list == null) continue;
                    foreach (var r in list)
                    {
                        var o = new char[len];
                        l.CopyTo(o, 0);
                        r.CopyTo(o, 2 * N);
                        output.Add(o);
                    }
                }
        }

        public void MergeLeft(List<char[]> output)
        {
            int len = 2 * N + 2;
            foreach (var list in Parenthesis)
            {
                if (list == null) continue;
                foreach (var l in list)
                {
                    var o = new char[len];
                    o[0] = Left;
                    o[len - 1] = Right;
                    l.CopyTo(o, 1);
                    output.Add(o);
                }
            }
        }
    }

    //private static Dictionary<int, List<string>> _num2Parenthesises = new Dictionary<int, List<string>>() {
    //    { 0, new List<string>{ "" } },
    //};
    //public IList<string> GenerateParenthesis(int n)
    //{
    //    //var a = new String()
    //    if (_num2Parenthesises.ContainsKey(n)) return _num2Parenthesises[n];

    //    List<string> ans = new List<string>();
    //    if (n == 0)
    //    {
    //        ans.Add("");
    //    }
    //    else
    //    {
    //        for (int c = 0; c < n; ++c)
    //            foreach (var left in GenerateParenthesis(c))
    //            {
    //                var part1 = $"({left})";
    //                foreach (var right in GenerateParenthesis(n - 1 - c))
    //                {
    //                    ans.Add(part1 + right);
    //                }
    //            }
    //    }

    //    _num2Parenthesises[n] = ans;

    //    return ans;
    //}


    //public IList<string> GenerateParenthesis(int n)
    //{
    //    return Parenthesis.GenerateParenthesis(n);
    //}

    //public class Parenthesis
    //{
    //    public List<Parenthesis> Children { get; } = new List<Parenthesis>();

    //    public string ToParenthesisString()
    //    {
    //        StringBuilder sb = new StringBuilder();
    //        sb.Append("(");

    //        foreach( var child in Children)
    //        {
    //            sb.Append( child.ToParenthesisString() );
    //        }

    //        sb.Append(")");

    //        return sb.ToString();
    //    }

    //    private static object _lock = new object();
    //    private static HashSet<string> _parenthesisSet = new HashSet<string>() { "()", };
    //    private static Dictionary<int, List<string>> _num2ParentthesisList = 
    //        new Dictionary<int, List<string>>() {
    //            { 0, new List<string> { "()" } },
    //        };

    //    public static IList<string> GenerateParenthesis( int n )
    //    {
    //        lock (_lock)
    //        {
    //            for (int i = 1; i < n; i++)
    //            {
    //                if (_num2ParentthesisList.ContainsKey(i)) continue;

    //                List<string> list = new List<string>();
    //                foreach ( var subParenthesis in _num2ParentthesisList[i - 1])
    //                {
    //                    var key = "()" + subParenthesis;
    //                    if (!_parenthesisSet.Contains(key))
    //                    {
    //                        _parenthesisSet.Add(key);
    //                        list.Insert(0,key);
    //                    }

    //                    key = subParenthesis + "()";
    //                    if (!_parenthesisSet.Contains(key))
    //                    {
    //                        _parenthesisSet.Add(key);
    //                        list.Insert(0, key);
    //                    }

    //                    key = "(" + subParenthesis + ")";
    //                    if (!_parenthesisSet.Contains(key))
    //                    {
    //                        _parenthesisSet.Add(key);
    //                        list.Insert(0, key);
    //                    }

    //                    int parenthesisStartIndex = 0;
    //                    while (parenthesisStartIndex < subParenthesis.Length)
    //                    {
    //                        var parenthesisIndex = subParenthesis.IndexOf("()", parenthesisStartIndex);
    //                        if (parenthesisIndex == -1) break;

    //                        key = subParenthesis.Insert(parenthesisIndex + 1, "()");
    //                        if (!_parenthesisSet.Contains(key))
    //                        {
    //                            _parenthesisSet.Add(key);
    //                            list.Insert(0, key);
    //                        }

    //                        parenthesisStartIndex += 2;
    //                    }

    //                    parenthesisStartIndex = 0;
    //                    while (parenthesisStartIndex < subParenthesis.Length)
    //                    {
    //                        var parenthesisIndex = subParenthesis.IndexOf("((", parenthesisStartIndex);
    //                        if (parenthesisIndex == -1) break;

    //                        key = subParenthesis.Insert(parenthesisIndex + 1, "()");
    //                        if (!_parenthesisSet.Contains(key))
    //                        {
    //                            _parenthesisSet.Add(key);
    //                            list.Insert(0, key);
    //                        }

    //                        parenthesisStartIndex += 2;
    //                    }

    //                    parenthesisStartIndex = 0;
    //                    while (parenthesisStartIndex < subParenthesis.Length)
    //                    {
    //                        var parenthesisIndex = subParenthesis.IndexOf("))", parenthesisStartIndex);
    //                        if (parenthesisIndex == -1) break;

    //                        key = subParenthesis.Insert(parenthesisIndex + 1, "()");
    //                        if (!_parenthesisSet.Contains(key))
    //                        {
    //                            _parenthesisSet.Add(key);
    //                            list.Insert(0, key);
    //                        }

    //                        parenthesisStartIndex += 2;
    //                    }
    //                }

    //                _num2ParentthesisList[i] = list;
    //            }

    //            return _num2ParentthesisList[n - 1];
    //        } // lock (_lock)
    //    }
    //}
}
/*
public class Solution {
    private void Recurr(char[] record, int pos, int left, int right, IList<string> ans) {
        if (left > right) {
            return;
        }
        if (left == 0 && right == 0) {
            ans.Add(new string(record));
            return;
        }
        if (left == right) {
            record[pos] = '('; Recurr(record, pos + 1, left - 1, right, ans);
        } else {
            if (left >= 1) {
                record[pos] = '('; Recurr(record, pos + 1, left - 1, right, ans);
            }
            if (right >= 1) {
                record[pos] = ')'; Recurr(record, pos + 1, left, right - 1, ans);
            }
        }
    }
    public IList<string> GenerateParenthesis(int n) {
        IList<string> ans = new List<string>();
        char[] record = new char[n << 1];
        Recurr(record, 0, n, n, ans);
        return ans;
    }
}

public class Solution {
    public IList<string> GenerateParenthesis(int n) {
        IList<string> list = new List<string>();
        if (n <= 0)
            return list;
        gen(list, 0, 0, n, "");
        return list;
    }

    void gen(IList<string> list, int left, int right, int n, string res) {
        if (left == n && right == n)
        {
            list.Add(res);
            return;
        }

        if (left < n)
        {
            // 注意：不能用++left 这样会修改left值影响后续的递归
            gen(list, left + 1, right, n, res + "(");
        }
        if (left > right && right < n)
        {
            gen(list, left, right + 1, n, res + ")");
        }
    }
}

public class Solution {
    public IList<string> GenerateParenthesis(int n)
    {
        IList<string>[] re = new IList<string>[n+1];
        re[0] = new List<string> { "" };
        for (int i = 1; i <= n; i++)
        {
            re[i] = new List<string>();
            for (int ii = 0; ii < i; ii++)
            {
                foreach (var inner in re[ii])
                {
                    foreach (var right in re[i-ii-1])
                    {
                        re[i].Add("(" + inner + ")" + right);
                    }
                }
            }
        }
        return re.Last();
    }

}

括号生成
力扣 (LeetCode)
发布于 2 年前
85.3k
方法一：暴力法
思路

我们可以生成所有 2^{2n}2 
2n
  个 '(' 和 ')' 字符构成的序列。然后，我们将检查每一个是否有效。

算法

为了生成所有序列，我们使用递归。长度为 n 的序列就是 '(' 加上所有长度为 n-1 的序列，以及 ')' 加上所有长度为 n-1 的序列。

为了检查序列是否为有效的，我们会跟踪 平衡，也就是左括号的数量减去右括号的数量的净值。如果这个值始终小于零或者不以零结束，该序列就是无效的，否则它是有效的。

class Solution {
    public List<String> generateParenthesis(int n) {
        List<String> combinations = new ArrayList();
        generateAll(new char[2 * n], 0, combinations);
        return combinations;
    }

    public void generateAll(char[] current, int pos, List<String> result) {
        if (pos == current.length) {
            if (valid(current))
                result.add(new String(current));
        } else {
            current[pos] = '(';
            generateAll(current, pos+1, result);
            current[pos] = ')';
            generateAll(current, pos+1, result);
        }
    }

    public boolean valid(char[] current) {
        int balance = 0;
        for (char c: current) {
            if (c == '(') balance++;
            else balance--;
            if (balance < 0) return false;
        }
        return (balance == 0);
    }
}

复杂度分析

时间复杂度：O(2^{2n}n)O(2 
2n
 n)，对于 2^{2n}2 
2n
  个序列中的每一个，我们用于建立和验证该序列的复杂度为 O(n)O(n)。

空间复杂度：O(2^{2n}n)O(2 
2n
 n)，简单地，每个序列都视作是有效的。请参见 方法三 以获得更严格的渐近界限。

方法二：回溯法
思路和算法

只有在我们知道序列仍然保持有效时才添加 '(' or ')'，而不是像 方法一 那样每次添加。我们可以通过跟踪到目前为止放置的左括号和右括号的数目来做到这一点，

如果我们还剩一个位置，我们可以开始放一个左括号。 如果它不超过左括号的数量，我们可以放一个右括号。

class Solution {
    public List<String> generateParenthesis(int n) {
        List<String> ans = new ArrayList();
        backtrack(ans, "", 0, 0, n);
        return ans;
    }

    public void backtrack(List<String> ans, String cur, int open, int close, int max){
        if (cur.length() == max * 2) {
            ans.add(cur);
            return;
        }

        if (open < max)
            backtrack(ans, cur+"(", open+1, close, max);
        if (close < open)
            backtrack(ans, cur+")", open, close+1, max);
    }
}
复杂度分析

我们的复杂度分析依赖于理解 generateParenthesis(n) 中有多少个元素。这个分析超出了本文的范畴，但事实证明这是第 n 个卡塔兰数 \dfrac{1}{n+1}\binom{2n}{n} 
n+1
1
​	
 ( 
n
2n
​	
 )，这是由 \dfrac{4^n}{n\sqrt{n}} 
n 
n
​	
 
4 
n
 
​	
  渐近界定的。

时间复杂度：O(\dfrac{4^n}{\sqrt{n}})O( 
n
​	
 
4 
n
 
​	
 )，在回溯过程中，每个有效序列最多需要 n 步。

空间复杂度：O(\dfrac{4^n}{\sqrt{n}})O( 
n
​	
 
4 
n
 
​	
 )，如上所述，并使用 O(n)O(n) 的空间来存储序列。

方法三：闭合数
思路

为了枚举某些内容，我们通常希望将其表示为更容易计算的不相交子集的总和。

考虑有效括号序列 S 的 闭包数：至少存在 index >= 0，使得 S[0], S[1], ..., S[2*index+1]是有效的。 显然，每个括号序列都有一个唯一的闭包号。 我们可以尝试单独列举它们。

算法

对于每个闭合数 c，我们知道起始和结束括号必定位于索引 0 和 2*c + 1。然后两者间的 2*c 个元素一定是有效序列，其余元素一定是有效序列。

class Solution {
    public List<String> generateParenthesis(int n) {
        List<String> ans = new ArrayList();
        if (n == 0) {
            ans.add("");
        } else {
            for (int c = 0; c < n; ++c)
                for (String left: generateParenthesis(c))
                    for (String right: generateParenthesis(n-1-c))
                        ans.add("(" + left + ")" + right);
        }
        return ans;
    }
}
复杂度分析

时间和空间复杂度：O(\dfrac{4^n}{\sqrt{n}})O( 
n
​	
 
4 
n
 
​	
 )，该分析与 方法二 类似。
下一篇：回溯算法（深度优先遍历）+ 广度优先遍历 + 动态规划

*/
