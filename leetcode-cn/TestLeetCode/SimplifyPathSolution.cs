using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// https://leetcode-cn.com/problems/simplify-path/
/// 71.简化路径
/// 给定一个文档 (Unix-style) 的完全路径，请进行路径简化。
/// https://blog.csdn.net/MebiuW/article/details/51399770
/// </summary>
class SimplifyPathSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    public string SimplifyPath(string path)
    {
        if (string.IsNullOrWhiteSpace(path)) return path;

        var parts = path.Split('/');

        Stack<string> stack = new Stack<string>();

        foreach( var part in parts)
        {
            if (string.IsNullOrWhiteSpace(part)) continue;
            if (part == ".") continue;
            if(part == "..")
            {
                if ( 0 < stack.Count) stack.Pop();
                continue;
            }

            stack.Push( part );
        }

        if (0 == stack.Count) return "/";

        StringBuilder sb = new StringBuilder();
        foreach( var a in stack.Reverse())
        {
            sb.Append($"/{a}");
        }

        return sb.ToString();
    }
}