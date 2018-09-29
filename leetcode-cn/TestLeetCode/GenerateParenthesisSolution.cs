using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// https://leetcode-cn.com/problems/generate-parentheses/
/// 括号生成
/// 给出 n 代表生成括号的对数，请你写出一个函数，使其能够生成所有可能的并且有效的括号组合。
/// https://leetcode-cn.com/problems/generate-parentheses/solution/
/// </summary>
class GenerateParenthesisSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    private static Dictionary<int, List<string>> _num2Parenthesises = new Dictionary<int, List<string>>() {
        { 0, new List<string>{ "" } },
    };
    public IList<string> GenerateParenthesis(int n)
    {
        if (_num2Parenthesises.ContainsKey(n)) return _num2Parenthesises[n];

        List<string> ans = new List<string>();
        if (n == 0)
        {
            ans.Add("");
        }
        else
        {
            for (int c = 0; c < n; ++c)
                foreach (var left in GenerateParenthesis(c))
                {
                    var part1 = $"({left})";
                    foreach (var right in GenerateParenthesis(n - 1 - c))
                    {
                        ans.Add(part1 + right);
                    }
                }
        }

        _num2Parenthesises[n] = ans;

        return ans;
    }


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