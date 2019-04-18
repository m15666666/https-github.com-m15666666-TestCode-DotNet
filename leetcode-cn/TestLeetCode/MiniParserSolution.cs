using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/**
 * // This is the interface that allows for creating nested lists.
 * // You should not implement it, or speculate about its implementation
 * interface NestedInteger {
 *
 *     // Constructor initializes an empty nested list.
 *     public NestedInteger();
 *
 *     // Constructor initializes a single integer.
 *     public NestedInteger(int value);
 *
 *     // @return true if this NestedInteger holds a single integer, rather than a nested list.
 *     bool IsInteger();
 *
 *     // @return the single integer that this NestedInteger holds, if it holds a single integer
 *     // Return null if this NestedInteger holds a nested list
 *     int GetInteger();
 *
 *     // Set this NestedInteger to hold a single integer.
 *     public void SetInteger(int value);
 *
 *     // Set this NestedInteger to hold a nested list and adds a nested integer to it.
 *     public void Add(NestedInteger ni);
 *
 *     // @return the nested list that this NestedInteger holds, if it holds a nested list
 *     // Return null if this NestedInteger holds a single integer
 *     IList<NestedInteger> GetList();
 * }
 */
/*
给定一个用字符串表示的整数的嵌套列表，实现一个解析它的语法分析器。

列表中的每个元素只可能是整数或整数嵌套列表

提示：你可以假定这些字符串都是格式良好的：

字符串非空
字符串不包含空格
字符串只包含数字0-9, [, - ,, ]
 

示例 1：

给定 s = "324",

你应该返回一个 NestedInteger 对象，其中只包含整数值 324。
 

示例 2：

给定 s = "[123,[456,[789]]]",

返回一个 NestedInteger 对象包含一个有两个元素的嵌套列表：

1. 一个 integer 包含值 123
2. 一个包含两个元素的嵌套列表：
    i.  一个 integer 包含值 456
    ii. 一个包含一个元素的嵌套列表
         a. 一个 integer 包含值 789 
*/
/// <summary>
/// https://leetcode-cn.com/problems/mini-parser/
/// 385. 迷你语法分析器
/// </summary>
class MiniParserSolution
{
    public void Test()
    {
        var ret = Deserialize("324");
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public NestedInteger Deserialize(string s)
    {
        if (string.IsNullOrWhiteSpace(s)) return null;

        Stack<NestedInteger> stack = new Stack<NestedInteger>();
        bool isNegativeNum = false;
        bool isNumstart = false;
        int sum = 0;

        Action addNum = () => {
            if (isNumstart)
            {
                var ni = new NestedInteger(isNegativeNum ? -sum : sum);
                if (0 < stack.Count)
                {
                    var parent = stack.Pop();
                    parent.Add(ni);
                    stack.Push(parent);
                }
                else stack.Push(ni);

                isNegativeNum = false;
                isNumstart = false;
                sum = 0;
            }
        };
        foreach( var c in s )
        {
            switch (c)
            {
                case '[':
                    stack.Push(new NestedInteger());
                    break;

                case ']':
                    {
                        addNum();

                        if (1 < stack.Count)
                        {
                            var child = stack.Pop();
                            var parent = stack.Pop();
                            parent.Add(child);
                            stack.Push(parent);
                        }
                    }
                    break;
                case ',':
                    {
                        addNum();
                    }
                    break;

                case '-':
                    isNegativeNum = true;
                    break;

                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                    {
                        var num = c - '0';
                        if (!isNumstart) isNumstart = true;
                        sum = sum * 10 + num;
                    }
                    break;
            }
        }

        addNum();

        NestedInteger ret = null;
        if (0 < stack.Count) ret = stack.Pop();
        while( 0 < stack.Count)
        {
            var parent = stack.Pop();
            parent.Add(ret);
            ret = parent;
        }
        return ret;
    }

    
    // This is the interface that allows for creating nested lists.
    // You should not implement it, or speculate about its implementation
    public class NestedInteger
    {
        // Constructor initializes an empty nested list.
        public NestedInteger() { }
 
        // Constructor initializes a single integer.
        public NestedInteger(int value) { }

        // @return true if this NestedInteger holds a single integer, rather than a nested list.
        bool IsInteger() { return true; }

        // @return the single integer that this NestedInteger holds, if it holds a single integer
        // Return null if this NestedInteger holds a nested list
        int GetInteger() { return 0; }

        // Set this NestedInteger to hold a single integer.
        public void SetInteger(int value) { }

        // Set this NestedInteger to hold a nested list and adds a nested integer to it.
        public void Add(NestedInteger ni) { }

        // @return the nested list that this NestedInteger holds, if it holds a nested list
        // Return null if this NestedInteger holds a single integer
        IList<NestedInteger> GetList() { return null; }
    }
}