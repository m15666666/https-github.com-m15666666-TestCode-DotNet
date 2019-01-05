using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// https://leetcode-cn.com/problems/basic-calculator-ii/
/// 227. 基本计算器 II
/// 实现一个基本的计算器来计算一个简单的字符串表达式的值。
/// 字符串表达式仅包含非负整数，+， - ，*，/ 四种运算符和空格  。 整数除法仅保留整数部分。
/// 示例 1:
/// 输入: "3+2*2"
/// 输出: 7
/// 示例 2:
/// 
/// 输入: " 3/2 "
/// 输出: 1
/// 示例 3:
/// 输入: " 3+5 / 2 "
/// 输出: 5
/// 说明：
/// 你可以假设所给定的表达式都是有效的。
/// 请不要使用内置的库函数 eval。
/// </summary>
class BasicCalculatorIISolution
{
    public static void Test()
    {
        var ret = Calculate(" 1+2+3*4/2 + 3*4/3");
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    public static int Calculate(string s)
    {
        if (string.IsNullOrWhiteSpace(s)) return 0;

        var tokens = StringToToken(s);
        Queue<CalcToken> input = new Queue<CalcToken>(tokens);
        Stack<CalcToken> stack = new Stack<CalcToken>();

        while( 0 < input.Count)
        {
            var a = input.Dequeue();
            if( a.TokenType == CalcTokenType.Multiply || a.TokenType == CalcTokenType.Divide)
            {
                int operand1 = int.Parse( stack.Pop().Value.ToString() );
                int operand2 = int.Parse( input.Dequeue().Value.ToString() );

                var result = a.TokenType == CalcTokenType.Multiply ? operand1 * operand2 : operand1 / operand2;
                stack.Push(new CalcToken { TokenType = CalcTokenType.StringOperand, Value = result });
                continue;
            }
            stack.Push(a);
        }

        input = new Queue<CalcToken>(stack.Reverse());
        int ret = int.Parse(input.Dequeue().Value.ToString());
        while (0 < input.Count)
        {
            var a = input.Dequeue();
            if (a.TokenType == CalcTokenType.Plus || a.TokenType == CalcTokenType.Subtract)
            {
                int operand2 = int.Parse(input.Dequeue().Value.ToString());

                ret = a.TokenType == CalcTokenType.Plus ? ret + operand2 : ret - operand2;
            }
        }
        return ret;
    }

    private static List<CalcToken> StringToToken(string s)
    {
        StringBuilder sb = new StringBuilder();
        List<CalcToken> ret = new List<CalcToken>();

        foreach( var c in s)
        {
            if( '0' <= c && c <= '9')
            {
                sb.Append(c);
                continue;
            }

            if (0 < sb.Length)
            {
                ret.Add(new CalcToken { TokenType = CalcTokenType.StringOperand, Value = sb.ToString() });
                sb.Clear();
            }

            if (c == ' ') continue;

            var token = new CalcToken { TokenType = CalcToken.CreateCalcTokenType(c) };
            if(token.TokenType != CalcTokenType.None) ret.Add(token);
        }

        if (0 < sb.Length)
        {
            ret.Add(new CalcToken { TokenType = CalcTokenType.StringOperand, Value = sb.ToString() });
        }

        return ret;
    }
}

class CalcToken
{
    public CalcTokenType TokenType { get; set; }
    public object Value { get; set; }

    private static readonly Dictionary<char, CalcTokenType> CToMap = new Dictionary<char, CalcTokenType> {
        { '+', CalcTokenType.Plus},
        { '-', CalcTokenType.Subtract},
        { '*', CalcTokenType.Multiply},
        { '/', CalcTokenType.Divide},
    };

    public static CalcTokenType CreateCalcTokenType( char c)
    {
        return CToMap.ContainsKey(c) ? CToMap[c] : CalcTokenType.None;
    }
}

enum CalcTokenType
{
    None,
    StringOperand,
    Plus,
    Subtract,
    Multiply,
    Divide,
}

/*
//别人的算法
public class Solution {
    public int Calculate(string s) {
      int res = 0, curRes = 0, num = 0, n = s.Length ;
            char op = '+';
            for (int i = 0; i < n; ++i)
            {
                char c = s[i];
                if (c >= '0' && c <= '9')
                {
                    num = num * 10 + c - '0';
                }
                if (c == '+' || c == '-' || c == '*' || c == '/' || i == n - 1)
                {
                    switch (op)
                    {
                        case '+': curRes += num; break;
                        case '-': curRes -= num; break;
                        case '*': curRes *= num; break;
                        case '/': curRes /= num; break;
                    }
                    if (c == '+' || c == '-' || i == n - 1)
                    {
                        res += curRes;
                        curRes = 0;
                    }
                    op = c;
                    num = 0;
                }
            }
            return res;
    }
    enum Expression { OPERAND, PLUS, MINUS, MULTIPLY, DIVIDE };
}
public class Solution {
    public int Calculate(string s) {
         char[] sa = s.ToCharArray();
            Expression[] expressions = new Expression[4];
            int[] operands = new int[4];
            int size = 0;
            for (int i = 0; i < sa.Length; i++)
            {
                if (sa[i] == ' ') continue;
                if (sa[i] >= '0' && sa[i] <= '9')
                {
                    // 遇到数字
                    int num = sa[i] - '0';
                    // 完整读取数字
                    while (i < sa.Length - 1 && sa[i + 1] >= '0' && sa[i + 1] <= '9') num = num * 10 + (sa[++i] - '0');
                    if (size > 0 && expressions[size - 1] == Expression.MULTIPLY)
                    {
                        // 如果当前的运算符为乘法，则立即完成运算
                        operands[size - 2] *= num;
                        size--;
                    }
                    else if (size > 0 && expressions[size - 1] == Expression.DIVIDE)
                    {
                        // 如果当前的运算符为除法，则立即完成运算
                        operands[size - 2] /= num;
                        size--;
                    }
                    else
                    {
                        // 当前运算符为加法或减法，要等到下一个运算符才能决定
                        expressions[size] = Expression.OPERAND;
                        operands[size] = num;
                        size++;
                    }
                }
                else if (sa[i] == '+' || sa[i] == '-')
                {
                    if (size > 2)
                    {
                        // 当前运算符为加法、减法，可以检查前面是否有为完成运算的加减法
                        if (expressions[size - 2] == Expression.PLUS)
                        {
                            operands[size - 3] += operands[size - 1];
                            size -= 2;
                        }
                        else if (expressions[size - 2] == Expression.MINUS)
                        {
                            operands[size - 3] -= operands[size - 1];
                            size -= 2;
                        }
                    }
                    // 加减号入栈
                    if (sa[i] == '+')
                    {
                        expressions[size++] = Expression.PLUS;
                    }
                    else
                    {
                        expressions[size++] = Expression.MINUS;
                    }
                }
                else
                {
                    // sa[i] == '*' || sa[i] == '/'
                    // 乘除号入栈
                    if (sa[i] == '*') expressions[size++] = Expression.MULTIPLY;
                    else expressions[size++] = Expression.DIVIDE;
                }
            }
            if (size > 1)
            {
                if (expressions[1] == Expression.PLUS)
                {
                    operands[0] += operands[2];
                }
                else
                {
                    operands[0] -= operands[2];
                }
                size = 1;
            }
            return operands[0];
    }
    enum Expression { OPERAND, PLUS, MINUS, MULTIPLY, DIVIDE };
}
public class Solution {
    public int Calculate(string s) {
        int cur = 0;
            Stack<int> leftOperands = new Stack<int>();
            Stack<char> operators = new Stack<char>();
            for (int i = 0; i < s.Length; i++)
            {
                char c = s[i];
                if (c>='0'&& c <= '9')
                {
                    cur = cur * 10 + c-'0';
                }else if(c=='+' || c == '-' || c == '*' || c == '/')
                {
                    if (operators.Count>0 && (operators.Peek() == '*' || operators.Peek() == '/'))
                    {
                        int left = leftOperands.Pop();
                        char ope = operators.Pop();

                        if (ope == '*')
                        {
                            cur = left * cur;
                        }
                        else
                        {
                            cur = left / cur;
                        }
                    }
                    leftOperands.Push(cur);
                    operators.Push(c);
                    cur = 0;
                }
            }

            if (operators.Count > 0 &&(operators.Peek()=='*'||operators.Peek()=='/'))
            {
                int left = leftOperands.Pop();
                char ope = operators.Pop();

                if (ope == '*')
                {
                    cur = left * cur;
                }
                else
                {
                    cur = left / cur;
                }
            }

            int ret = 0;
            while (operators.Count > 0)
            {
                int left = leftOperands.Pop();
                char ope = operators.Pop();

                if (ope == '+')
                {
                    ret = ret + cur;
                }
                else
                {
                    ret = ret - cur;
                }
                cur = left;
            }
        ret +=cur;
            return ret;
    }
}
public class Solution {
     public  int Calculate(string s)
        {
            var numStack = new Stack<int>();
            var opStack = new Stack<char>();
            var currentNum = string.Empty;
            foreach (var v in s.Reverse())
            {
                if (v == ' ')
                {
                    if (currentNum.Length > 0)
                    {
                        numStack.Push(int.Parse(currentNum));
                        currentNum = string.Empty;
                        continue;
                    }
                }
                else if (v >= '0' && v <= '9')
                {
                    currentNum = v+ currentNum;
                }
                else
                {
                    if (currentNum.Length > 0)
                    {
                        numStack.Push(int.Parse(currentNum));
                        currentNum = string.Empty;
                    }
                    while (opStack.Count>0&&_compare_op(v, opStack.Peek()) == -1)
                    {
                        numStack.Push(_cal(numStack.Pop(), numStack.Pop(), opStack.Pop()));                      
                    }
                    opStack.Push(v);
                }
            }
            if (currentNum.Length > 0)
                numStack.Push(int.Parse(currentNum));
            foreach (var op in opStack) {
                numStack.Push(_cal(numStack.Pop(), numStack.Pop(), op));
            }
            return numStack.Pop();
        }
        private  int _compare_op(char c1, char c2)
        {
            var n1 = 0;
            var n2 = 0;
            if (c1 == '*' || c1 == '/')
            {
                n1 = 1;
            }
            if (c2 == '*' || c2 == '/')
            {
                n2 = 1;
            }
            if (n1 > n2)
                return 1;
            else if (n1 < n2)
                return -1;
            else
                return 0;
        }
        private  int _cal(int n1, int n2, char op)
        {
            switch (op)
            {
                case '+': return n1 + n2;
                case '-': return n1 - n2;
                case '*': return n1 * n2;
                case '/': return n1 / n2;
                default: return 0;
            }
        }
}
public  class Solution
{
    LinkedList<int> _nums;
    LinkedList<char> _ops;
    public int Calculate(string s)
    {
        int i = 0;
        int len = s.Length;
        _nums = new LinkedList<int>();
        _ops = new LinkedList<char>();
        char tag = ' ';
        int num = 0;
        while (i < len)
        {
            if (s[i] == ' ')
            {
                i++;
                continue;
            }
            switch (s[i])
            {
                case '+':
                case '-':
                    if (tag == '*')
                    {
                        _ops.RemoveLast();
                        int a = _nums.Last();
                        _nums.RemoveLast();
                        _nums.AddLast(a * num);
                    }
                    else if (tag == '/')
                    {
                        _ops.RemoveLast();
                        int a = _nums.Last();
                        _nums.RemoveLast();
                        _nums.AddLast(a / num);
                    }
                    else
                    {
                        _nums.AddLast(num);
                    }
                    num = 0;
                    tag = s[i];
                    AddOrSub();
                    _ops.AddLast(s[i]);
                    break;
                case '*':
                case '/':
                    if (tag == '*')
                    {
                        _ops.RemoveLast();
                        int a = _nums.Last();
                        _nums.RemoveLast();
                        _nums.AddLast(a * num);
                    }
                    else if (tag == '/')
                    {
                        _ops.RemoveLast();
                        int a = _nums.Last();
                        _nums.RemoveLast();
                        _nums.AddLast(a / num);
                    }
                    else
                    {
                        _nums.AddLast(num);
                    }
                    num = 0;
                    tag = s[i];
                    _ops.AddLast(s[i]);
                    break;
                default:
                    num = num * 10 + (int)(s[i] - '0');
                    break;
            }
            i++;
        }
        if (tag == '*')
        {
            _ops.RemoveLast();
            int a = _nums.Last();
            _nums.RemoveLast();
            _nums.AddLast(a * num);
        }
        else if (tag == '/')
        {
            _ops.RemoveLast();
            int a = _nums.Last();
            _nums.RemoveLast();
            _nums.AddLast(a / num);
        }
        else
        {
            _nums.AddLast(num);
        }
        AddOrSub();
        return _nums.Last();
    }
    private void AddOrSub()
    {
        while (_ops.Count > 0)
        {
            char c = _ops.First();
            _ops.RemoveFirst();
            int a = _nums.First();
            _nums.RemoveFirst();
            int b = _nums.First();
            _nums.RemoveFirst();
            if (c == '+')
            {
                _nums.AddFirst(a + b);
            }
            else if (c == '-')
            {
                _nums.AddFirst(a - b);
            }
        }
    }
}
public class Solution {
    public int Calculate(string s) {
       int res = 0, num = 0, n = s.Length ;
            char op = '+';
            Stack<int> st=new Stack<int>();
            for (int i = 0; i < n; ++i)
            {
                if (s[i] >= '0')
                {
                    num = num * 10 + s[i] - '0';
                }
                if ((s[i] < '0' && s[i] != ' ') || i == n - 1)
                {
                    if (op == '+') st.Push(num);
                    if (op == '-') st.Push(-num);
                    if (op == '*' || op == '/')
                    {
                        int tmp = (op == '*') ? st.Peek() * num : st.Peek () / num;
                        st.Pop();
                        st.Push(tmp);
                    }
                    op = s[i];
                    num = 0;
                }
            }
            while (st.Count!=0)
            {
                res += st.Peek ();
                st.Pop();
            }
            return res;
    }
    enum Expression { OPERAND, PLUS, MINUS, MULTIPLY, DIVIDE };
}
*/
