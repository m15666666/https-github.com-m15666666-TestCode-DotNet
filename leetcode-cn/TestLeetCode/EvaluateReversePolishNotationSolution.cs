using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// https://leetcode-cn.com/problems/evaluate-reverse-polish-notation/
/// 150. 逆波兰表达式求值
/// 根据逆波兰表示法，求表达式的值。
/// 有效的运算符包括 +, -, *, / 。每个运算对象可以是整数，也可以是另一个逆波兰表达式。
/// 说明：
/// 整数除法只保留整数部分。
/// 给定逆波兰表达式总是有效的。换句话说，表达式总会得出有效数值且不存在除数为 0 的情况。
/// </summary>
class EvaluateReversePolishNotationSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    public int EvalRPN(string[] tokens)
    {
        if (tokens == null || tokens.Length == 0) return 0;

        Stack<int> stack = new Stack<int>();
        foreach( var token in tokens)
        {
            switch( token )
            {
                case "+":
                case "-":
                case "*":
                case "/":
                    {
                        var item2 = stack.Pop();
                        var item1 = stack.Pop();
                        int result = 0;
                        switch (token)
                        {
                            case "+":
                                result = item1 + item2;
                                break;
                            case "-":
                                result = item1 - item2;
                                break;
                            case "*":
                                result = item1 * item2;
                                break;
                            case "/":
                                result = item1 / item2;
                                break;
                        }
                        stack.Push(result);
                    }
                    break;
                default:
                    stack.Push(int.Parse(token));
                    break;
            }
        }
        return stack.Pop();
    }

    #region 解法1
    //public int EvalRPN(string[] tokens)
    //{
    //    if (tokens == null || tokens.Length == 0) return 0;

    //    Stack<string> stack = new Stack<string>();
    //    foreach (var token in tokens)
    //    {
    //        switch (token)
    //        {
    //            case "+":
    //            case "-":
    //            case "*":
    //            case "/":
    //                {
    //                    var item2 = int.Parse(stack.Pop());
    //                    var item1 = int.Parse(stack.Pop());
    //                    int result = 0;
    //                    switch (token)
    //                    {
    //                        case "+":
    //                            result = item1 + item2;
    //                            break;
    //                        case "-":
    //                            result = item1 - item2;
    //                            break;
    //                        case "*":
    //                            result = item1 * item2;
    //                            break;
    //                        case "/":
    //                            result = item1 / item2;
    //                            break;
    //                    }
    //                    stack.Push(result.ToString());
    //                }
    //                break;
    //            default:
    //                stack.Push(token);
    //                break;
    //        }
    //    }
    //    return int.Parse(stack.Pop());
    //}
    #endregion

}