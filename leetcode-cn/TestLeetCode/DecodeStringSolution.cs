using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个经过编码的字符串，返回它解码后的字符串。

编码规则为: k[encoded_string]，表示其中方括号内部的 encoded_string 正好重复 k 次。注意 k 保证为正整数。

你可以认为输入字符串总是有效的；输入字符串中没有额外的空格，且输入的方括号总是符合格式要求的。

此外，你可以认为原始数据不包含数字，所有的数字只表示重复的次数 k ，例如不会出现像 3a 或 2[4] 的输入。

示例:

s = "3[a]2[bc]", 返回 "aaabcbc".
s = "3[a2[c]]", 返回 "accaccacc".
s = "2[abc]3[cd]ef", 返回 "abcabccdcdcdef". 
*/
/// <summary>
/// https://leetcode-cn.com/problems/decode-string/
/// 394. 字符串解码
/// </summary>
class DecodeStringSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    //var groups = s.Split(']', StringSplitOptions.RemoveEmptyEntries);
    //foreach( var group in groups)
    //{
    //    var parts = group.Split('[');
    //    if (parts.Length != 2) continue;
    //    int count = Convert.ToInt32(parts[0]);
    //    for (int i = 0; i < count; i++)
    //        ret.Append(parts[1]);
    //}
    public string DecodeString(string s)
    {
        // "3[z]2[2[y]pq4[2[jk]e1[f]]]ef"
        if (string.IsNullOrWhiteSpace(s)) return s;
        int index = 0;
        return DecodeString(s, ref index);
    }

    private string DecodeString(string s, ref int index)
    {
        // "3[z]2[2[y]pq4[2[jk]e1[f]]]ef"
        StringBuilder ret = new StringBuilder();
        bool isInNumber = false;
        int count = 0;
        for (; index < s.Length;)
        {
            var c = s[index];
            index++;
            if ('0' <= c && c <= '9')
            {
                if (!isInNumber)
                {
                    count = 0;
                    isInNumber = true;
                }
                count = count * 10 + (c - '0');

                continue;
            }
            else
            {
                if (isInNumber)
                {
                    isInNumber = false;
                }

                switch (c)
                {
                    case '[':
                        var subString = DecodeString(s, ref index);
                        while (0 < count--) ret.Append(subString);
                        break;

                    case ']':
                        return ret.ToString();

                    default:
                        ret.Append(c);
                        break;
                }
            }
        }
        return ret.ToString();
    }
}
/*
public class Solution {
    public string DecodeString(string s) {
        int left;
        int right;
        while(GetInnerPos(s, out left, out right) == true) {
            
            int numRight = left - 1;
            int numLeft = left -1;
            for (int i = left -1; i >=0; i --) {
                if (s[i] >= '0' && s[i] <= '9') {
                    numLeft = i;
                } else {
                    break;
                }
            }
            
            // Console.WriteLine("numLeft = " + numLeft + " numRight = " + numRight);
            
            int num = 0;
            for (int i = numLeft; i <= numRight; i ++) {
                num = num * 10 + (s[i] - '0');
            }
            
            StringBuilder sb = new StringBuilder();
            sb.Append(s.Substring(0, numLeft));
            for (int i = 0; i < num; i ++) {
                sb.Append(s.Substring(left + 1, right - left - 1));
            }
            sb.Append(s.Substring(right + 1));
            
            s = sb.ToString();
            // Console.WriteLine("s = " + s);
        }
        
        return s;
    }
    
    bool GetInnerPos(string s, out int left,out int right) {
        
        left = -1;
        right = -1;
        for (int i = 0; i < s.Length; i ++) {
            
            if (s[i] =='[') {
                left = i;
            } 
            
            if (s[i] == ']') {
                right = i;
                return true;
            }
        }
        
        return false;
    }
}
public class Solution {
    public string DecodeString(string s) {
        Stack<char> stack = new Stack<char>();
        string decodeStr = ""; 
        for (int i = 0; i < s.Length; i++)
        {
            stack.Push(s[i]);
            //string tempCur = ""; 
            if (s[i].Equals(']'))
            {
                string cur = "";
                stack.Pop();
                int count = 0;//个数
                while (stack.Count > 0)
                {
                    char c = stack.Pop();
                    if (!c.Equals('['))
                    {
                        cur = c + cur;
                    }
                    else
                    {
                        int k = 0;
                        while (stack.Count > 0)
                        {
                            char n = stack.Pop();
                            if (n < 48 || n > 57)//不是数字
                            {
                                stack.Push(n);
                                break;
                            }
                            else
                            {
                                k++;
                                int num = int.Parse(n.ToString());
                                for (int _k = 1; _k <= k - 1; _k++)
                                {
                                    num *= 10;
                                }
                                count = num + count;
                            }
                        }
                        string temp = "";
                        for (int _k = 0; _k < count; _k++)
                        {
                            temp += cur;
                        }
                        cur = temp;
                        //将解码后的字符串压入栈中
                        foreach (var item in cur)
                        {
                            stack.Push(item);
                        }
                        count = 0;
                        break;
                    }
                }
            }
        }
        while (stack.Count > 0)
        {
            decodeStr = stack.Pop() + decodeStr;
        }
        return decodeStr;
    }
}
public class Solution {
   
    public string DecodeString(string s) {
        var stack = new Stack<string>();//字母和符号
        var num = string.Empty;
        var numStack = new Stack<int>();
        foreach (var v in s)
        {
            if (v >= '0' && v <= '9')
            {
                num += v;
            }
            else if (v == '[')
            {
                stack.Push(v.ToString());
                numStack.Push(int.Parse(num));
                num = string.Empty;
            }
            else if (v == ']')
            {
                var temp = stack.Pop();
                var flag = string.Empty;
                while (temp != "[")
                {
                    flag = temp + flag;
                    temp = stack.Pop();
                }
                var str = string.Empty;
                var n = numStack.Pop();
                for (var i = 0; i < n; i++)
                    str += flag;
                stack.Push(str);
            }
            else
                stack.Push(v.ToString());
        }
        var res = string.Empty;
        foreach (var v in stack)
            res = v + res;
        return res;
    }
}
public class Solution {
    public string DecodeString(string s)
    {
        Stack<string> stack = new Stack<string>();
        Stack<int> repeatStack = new Stack<int>();
        string str = "",num="";            
        for (int i = 0; i < s.Length; i++)
        {
            if (s[i] <= '9' && s[i] >= '0')
            {
                num += s[i];
                continue;
            }
            else if (s[i] == '[')
            {
                repeatStack.Push(Int32.Parse(num));
                num = "";
                stack.Push(str);
                str = "";
                continue;
            }
            else if (s[i] == ']')
            {
                int repeatCount = repeatStack.Pop();
                String Top = stack.Count > 0 ? stack.Pop() : "";
                string newStr = "";
                while (repeatCount-- > 0)
                    newStr += str;
                str = (Top + newStr);
            }
            else
            {
                str += s[i];
            }
        }
        Console.WriteLine(s+":"+str);
        return str;
    }
}
public class Solution {
    public string DecodeString(string s) {
        Stack<int> counts = new Stack<int>();
        Stack<string> strings = new Stack<string>();
        int count = 0;
        string result = "";
        string ss = "";
        for (int i = 0; i < s.Length; i++)
        {
            if (s[i] >= '0' && s[i] <= '9')
            {
                count = count * 10;
                count = count + s[i] - '0';
            }
            else if (s[i] == '[')
            {
                counts.Push(count);
                strings.Push(ss);
                count = 0;
                ss = "";
            }
            else if (s[i] == ']')
            {
                int times = counts.Pop();
                if (counts.Count == 0)
                {
                    for (int j = 0; j < times; j++)
                    {
                        result += ss;
                    }
                    ss = strings.Pop();
                }
                else
                {
                    string temp = "";
                    for (int k = 0; k < times; k++)
                    {
                        temp += ss;
                    }
                    ss = strings.Pop() + temp;
                }
            }
            else
            {
                if (counts.Count != 0)
                {
                    ss += s[i];
                }
                else
                {
                    result += s[i];
                }

            }

        }
        return result.ToString();
    }
}
*/
