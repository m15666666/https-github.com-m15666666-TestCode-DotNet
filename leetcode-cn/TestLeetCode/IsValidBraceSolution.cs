using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// 有效的括号
/// 给定一个只包括 '('，')'，'{'，'}'，'['，']' 的字符串，判断字符串是否有效。
/// 有效字符串需满足：
/// 左括号必须用相同类型的右括号闭合。
/// 左括号必须以正确的顺序闭合。
/// </summary>
class IsValidBraceSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    public bool IsValid(string s)
    {
        if (string.IsNullOrWhiteSpace(s)) return true;

        Stack<char> braces = new Stack<char>();
        foreach( var c in s)
        {
            switch (c)
            {
                case '(':
                case '[':
                case '{':
                    braces.Push(c);
                    continue;

                case ')':
                    if (0 < braces.Count && braces.Pop() == '(') continue;
                    return false;

                case ']':
                    if (0 < braces.Count && braces.Pop() == '[') continue;
                    return false;

                case '}':
                    if (0 < braces.Count && braces.Pop() == '{') continue;
                    return false;

                default:
                    continue;
            }
        }

        return braces.Count == 0;
    }
}