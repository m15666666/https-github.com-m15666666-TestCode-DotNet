using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
验证给定的字符串是否可以解释为十进制数字。

例如:

"0" => true
" 0.1 " => true
"abc" => false
"1 a" => false
"2e10" => true
" -90e3   " => true
" 1e" => false
"e3" => false
" 6e-1" => true
" 99e2.5 " => false
"53.5e93" => true
" --6 " => false
"-+3" => false
"95a54e53" => false

说明: 我们有意将问题陈述地比较模糊。在实现代码之前，你应当事先思考所有可能的情况。这里给出一份可能存在于有效十进制数字中的字符列表：

数字 0-9
指数 - "e"
正/负号 - "+"/"-"
小数点 - "."
当然，在输入中，这些字符的上下文也很重要。

更新于 2015-02-10:
C++函数的形式已经更新了。如果你仍然看见你的函数接收 const char * 类型的参数，请点击重载按钮重置你的代码。
*/
/// <summary>
/// https://leetcode-cn.com/problems/valid-number/
/// 65. 有效数字
/// 
/// </summary>
class ValidNumberSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public bool IsNumber(string s) 
    {
        int state = 0;
        foreach( var c in s ) 
        {
            int id = GetColumnId(c);
            if (id < 0) return false;
            state = _transfer[state,id];
            if (state < 0) return false;
        }
        return _finals[state];
    }
    private static int GetColumnId(char c) 
    {
        switch(c) 
        {
            case ' ': return 0;
            case '+':
            case '-': return 1;
            case '.': return 3;
            case 'e': return 4;
        }
        return '0' <= c && c <= '9' ? 2 : -1;
    }
    private readonly static int[,] _transfer = new int[,]{{ 0, 1, 6, 2,-1},
                                       {-1,-1, 6, 2,-1},
                                       {-1,-1, 3,-1,-1},
                                       { 8,-1, 3,-1, 4},
                                       {-1, 7, 5,-1,-1},
                                       { 8,-1, 5,-1,-1},
                                       { 8,-1, 6, 3, 4},
                                       {-1,-1, 5,-1,-1},
                                       { 8,-1,-1,-1,-1}};
    private readonly static bool[] _finals = new bool[] {
        false,false,false
        ,true, false, true
        ,true,false,true };
}
/*

表驱动法
while(1)
发布于 1 年前
11.6k
本题可以采用《编译原理》里面的确定的有限状态机（DFA）解决。构造一个DFA并实现，构造方法可以先写正则表达式，然后转为 DFA，也可以直接写，我就是直接写的，虽然大概率不会是最简结构（具体请参考《编译器原理》图灵出版社），不过不影响解题。DFA 作为确定的有限状态机，比 NFA 更加实用，因为对于每一个状态接收的下一个字符，DFA 能确定唯一一条转换路径，所以使用简单的表驱动的一些方法就可以实现，并且只需要读一遍输入流，比起 NFA 需要回读在速度上会有所提升。

构建出来的状态机如封面图片所示（红色为 终止状态，蓝色为 中间状态）。根据《编译原理》的解释，DFA 从状态 0 接受串 s 作为输入。当s耗尽的时候如果当前状态处于中间状态，则拒绝；如果到达终止状态，则接受。

然后，根据 DFA 列出如下的状态跳转表，之后我们就可以采用 表驱动法 进行编程实现了。需要注意的是，这里面多了一个状态 8，是用于处理串后面的若干个多余空格的。所以，所有的终止态都要跟上一个状态 8。其中，有一些状态标识为-1，是表示遇到了一些意外的字符，可以直接停止后续的计算。状态跳转表如下：

state	blank	+/-	0-9	.	e	other
0	0	1	6	2	-1	-1
1	-1	-1	6	2	-1	-1
2	-1	-1	3	-1	-1	-1
3	8	-1	3	-1	4	-1
4	-1	7	5	-1	-1	-1
5	8	-1	5	-1	-1	-1
6	8	-1	6	3	4	-1
7	-1	-1	5	-1	-1	-1
8	8	-1	-1	-1	-1	-1
状态图：
DFA.jpg

class Solution {
    public int make(char c) {
        switch(c) {
            case ' ': return 0;
            case '+':
            case '-': return 1;
            case '.': return 3;
            case 'e': return 4;
            default:
                if(c >= 48 && c <= 57) return 2;
        }
        return -1;
    }
    
    public boolean isNumber(String s) {
        int state = 0;
        int finals = 0b101101000;
        int[][] transfer = new int[][]{{ 0, 1, 6, 2,-1},
                                       {-1,-1, 6, 2,-1},
                                       {-1,-1, 3,-1,-1},
                                       { 8,-1, 3,-1, 4},
                                       {-1, 7, 5,-1,-1},
                                       { 8,-1, 5,-1,-1},
                                       { 8,-1, 6, 3, 4},
                                       {-1,-1, 5,-1,-1},
                                       { 8,-1,-1,-1,-1}};
        char[] ss = s.toCharArray();
        for(int i=0; i < ss.length; ++i) {
            int id = make(ss[i]);
            if (id < 0) return false;
            state = transfer[state][id];
            if (state < 0) return false;
        }
        return (finals & (1 << state)) > 0;
    }
}

javascript
var isNumber = function(s) {
    let state = 0, 
        finals = [0,0,0,1,0,1,1,0,1],
        transfer = [[ 0, 1, 6, 2,-1,-1],
                    [-1,-1, 6, 2,-1,-1],
                    [-1,-1, 3,-1,-1,-1],
                    [ 8,-1, 3,-1, 4,-1],
                    [-1, 7, 5,-1,-1,-1],
                    [ 8,-1, 5,-1,-1,-1],
                    [ 8,-1, 6, 3, 4,-1],
                    [-1,-1, 5,-1,-1,-1],
                    [ 8,-1,-1,-1,-1,-1]], 
        make = (c) => {
            switch(c) {
                case " ": return 0;
                case "+":
                case "-": return 1;
                case ".": return 3;
                case "e": return 4;
                default:
                    let code = c.charCodeAt();
                    if(code >= 48 && code <= 57) {
                        return 2;
                    } else {
                        return 5;
                    }
            }
        };
    for(let i=0; i < s.length; ++i) {
        state = transfer[state][make(s[i])];
        if (state < 0) return false;
    }
    return finals[state];
};

public class Solution {
    private bool IsInteger(string s) {
        byte valid = 0;
        for (var i = 0; i < s.Length; i++) {
            if (valid == 0) {
                if (s[i] == '+' || s[i] == '-') {
                    valid = 1;
                    if (i == s.Length - 1) {
                        return false;
                    }
                } else if (s[i] >= '0' && s[i] <= '9') {
                    valid = 1;
                } else {
                    return false;
                }
            } else {
                if (s[i] < '0' || s[i] > '9') {
                    return false;
                }
            }
        }

        return true;
    }

    private bool IsNature(string s, bool digitValid = true) {
        for (var i = 0; i < s.Length; i++) {
            if (s[i] == 'e') {
                if (i == s.Length - 1 || !digitValid) return false;
                return IsInteger(s.Substring(i + 1));
            }
            if (s[i] < '0' || s[i] > '9') {
                return false;
            }
            digitValid = true;
        }
        return true;
    }

    public bool IsNumber(string s) {
        if (string.IsNullOrEmpty(s)) return false;
        s = s.Trim(' ');
        byte valid = 0;
        byte digitValid = 0;
        for (var i = 0; i < s.Length; i++) {
            if (s[i] == '.') {
                if (i == s.Length - 1) {
                    return digitValid == 1;
                }
                return IsNature(s.Substring(i + 1), digitValid == 1);  
            }
            if (valid == 0) {
                if (s[i] >= '0' && s[i] <= '9') {
                    valid = 1;
                    digitValid = 1;
                } else if (s[i] == '-' || s[i] == '+') {
                    valid = 1;
                    if (i == s.Length - 1) {
                        return false;
                    }
                } else {
                    return false;
                }
            } else {
                if (s[i] == 'e') {
                    if (digitValid == 0) {
                        return false;
                    }

                    if (i == s.Length - 1) return false;
                    return IsInteger(s.Substring(i + 1));
                } 
                if (s[i] >= '0' && s[i] <= '9') {
                    digitValid = 1;
                } else {
                    return false;
                }
            }
        }

        return valid == 1;
    }
}

public class Solution {

    protected enum CharType {Number, Symbol, Point, Exponent, Space, Other};
    protected enum Status {Begin, Symbol, Integer, Point, Decimal, End, Error};

    public bool IsNumber(string s) {
        Status status = Status.Begin;
        bool exponent = false;
        bool containNumber = false;
        for (int i = 0; i < s.Length; i++) {
            CharType charType = GetCharType(s[i]);
            status = TransferStatus(status, charType, ref exponent);
            if (status == Status.Error) {
                return false;
            }
            if (charType == CharType.Number && !exponent) {
                containNumber = true;
            }
        }
        return containNumber && TransferStatus(status, CharType.Space, ref exponent) == Status.End;
    }

    protected Status TransferStatus(Status status, CharType charType, ref bool isExponent) {
        switch (status) {
            case Status.Begin:
                if (charType == CharType.Symbol) {
                    return Status.Symbol;
                } else if (charType == CharType.Number) {
                    return Status.Integer;
                } else if (!isExponent) {
                    if (charType == CharType.Space) {
                        return status;
                    } else if (charType == CharType.Point) {
                        return Status.Point;
                    }
                }
                break;
            case Status.Symbol:
                if (charType == CharType.Number) {
                    return Status.Integer;
                } else if (charType == CharType.Point && !isExponent) {
                    return Status.Point;
                }
                break;
            case Status.Integer:
            case Status.Decimal:
                if (charType == CharType.Number) {
                    return status;
                } else if (charType == CharType.Space) {
                    return Status.End;
                } else if (!isExponent) {
                    if (charType == CharType.Exponent) {
                        isExponent = true;
                        return Status.Begin;
                    } else if (charType == CharType.Point && status == Status.Integer) {
                        return Status.Point;
                    }
                }
                break;
            case Status.Point:
                if (charType == CharType.Number) {
                    return Status.Decimal;
                } else if (charType == CharType.Space) {
                    return Status.End;
                } else if (charType == CharType.Exponent) {
                    isExponent = true;
                    return Status.Begin;
                }
                break;
            case Status.End:
                if (charType == CharType.Space) {
                    return status;
                }
                break;
        }
        return Status.Error;
    }

    protected CharType GetCharType(char c) {
        if ("0123456789".IndexOf(c) > -1) {
            return CharType.Number;
        } else if (c == '+' || c == '-') {
            return CharType.Symbol;
        } else if (c == ' ') {
            return CharType.Space;
        } else if (c == '.') {
            return CharType.Point;
        } else if (c == 'e') {
            return CharType.Exponent;
        } else {
            return CharType.Other;
        }
    }
}

public class Solution {
    public bool IsNumber(string s) {
        if(string.IsNullOrEmpty(s)) return false;
        string str = s.Trim();
        if(string.IsNullOrEmpty(str)) return false;

        int left = 0;
        if(str[left] == '+' || str[left] == '-')
        {
            left++;
        }

        bool hasDigit = false;
        bool hasE = false;
        bool hasDot = false;
        int len = str.Length;
        for(; left < len; left++)
        {
            if(!char.IsDigit(str[left]))
            {
                if(str[left] == 'e' && !hasE)
                {
                    if(!hasDigit) return false;
                    if(left == len - 1) return false;
                    if(str[left + 1] == '+' || str[left + 1] == '-')
                    {
                        left++;
                        if(left == len - 1) return false;
                    }
                    hasE = true;
                }
                else if(str[left] == '.' && !hasDot)
                {
                    if(hasE) return false;
                    hasDot = true;
                }
                else
                {
                    return false;
                }
            }
            else
            {aa
                hasDigit = true;
            }
        }
        return hasDigit;
    }
}


 
*/
