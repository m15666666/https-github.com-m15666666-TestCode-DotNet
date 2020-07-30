using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
根据 逆波兰表示法，求表达式的值。

有效的运算符包括 +, -, *, / 。每个运算对象可以是整数，也可以是另一个逆波兰表达式。

 

说明：

整数除法只保留整数部分。
给定逆波兰表达式总是有效的。换句话说，表达式总会得出有效数值且不存在除数为 0 的情况。
 

示例 1：

输入: ["2", "1", "+", "3", "*"]
输出: 9
解释: 该算式转化为常见的中缀算术表达式为：((2 + 1) * 3) = 9
示例 2：

输入: ["4", "13", "5", "/", "+"]
输出: 6
解释: 该算式转化为常见的中缀算术表达式为：(4 + (13 / 5)) = 6
示例 3：

输入: ["10", "6", "9", "3", "+", "-11", "*", "/", "*", "17", "+", "5", "+"]
输出: 22
解释: 
该算式转化为常见的中缀算术表达式为：
  ((10 * (6 / ((9 + 3) * -11))) + 17) + 5
= ((10 * (6 / (12 * -11))) + 17) + 5
= ((10 * (6 / -132)) + 17) + 5
= ((10 * 0) + 17) + 5
= (0 + 17) + 5
= 17 + 5
= 22
 

逆波兰表达式：

逆波兰表达式是一种后缀表达式，所谓后缀就是指算符写在后面。

平常使用的算式则是一种中缀表达式，如 ( 1 + 2 ) * ( 3 + 4 ) 。
该算式的逆波兰表达式写法为 ( ( 1 2 + ) ( 3 4 + ) * ) 。
逆波兰表达式主要有以下两个优点：

去掉括号后表达式无歧义，上式即便写成 1 2 + 3 4 + * 也可以依据次序计算出正确结果。
适合用栈操作运算：遇到数字则入栈；遇到算符则取出栈顶两个数字进行计算，并将结果压入栈中。
 
 
*/
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
/*

public class Solution {
    public int EvalRPN(string[] tokens) {
        Stack<int> stack = new Stack<int>();
        for (int i = 0; i < tokens.Length; i++)
        {
            if (tokens[i] != "+" && tokens[i] != "-" && tokens[i] != "*" && tokens[i] != "/")
            {
                stack.Push(int.Parse(tokens[i]));
            }
            else
            {
                stack.Push(compute(stack.Pop(), stack.Pop(), tokens[i]));
            }
        }
        return stack.Pop();
    }
      public int compute(int num1, int num2, string str)
    {
        switch(str)
        {
            case "+": return num1 + num2;
            case "-": return num2 - num1;
            case "*": return num1 * num2;
            case "/": return num2 / num1;
        }
        return 0;
    }
}

public class Solution {
    public int EvalRPN(string[] tokens) {
        Stack<int> stack = new Stack<int>();
        int num1 = 0;
        int num2 = 0;

        for(int i = 0; i < tokens.Length; i++){
            switch(tokens[i]){
                case "+":
                    num1 = stack.Pop();
                    num2 = stack.Pop();
                    stack.Push(num1 + num2);
                    break;
                case "-":
                    num1 = stack.Pop();
                    num2 = stack.Pop();
                    stack.Push(num2 - num1);
                    break;
                case "*":
                    num1 = stack.Pop();
                    num2 = stack.Pop();
                    stack.Push(num1 * num2);
                    break;
                case "/":
                    num1 = stack.Pop();
                    num2 = stack.Pop();
                    stack.Push(num2 / num1);
                    break;
                default:
                    stack.Push(Convert.ToInt32(tokens[i]));
                    break;
            }
        }
        return stack.Pop();
    }
}

 
 
 
 
*/