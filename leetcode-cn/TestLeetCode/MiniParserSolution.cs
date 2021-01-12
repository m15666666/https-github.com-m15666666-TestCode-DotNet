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
/*
class Solution {
public:
    NestedInteger deserialize(string s) {
        int n = s.size();
        if (n == 0)return NestedInteger();
        if (s[0] != '[')return NestedInteger(stoi(s));
        string num;
        stack<NestedInteger> st;
        for (int i = 0; i < n; i++) {
            //cout << i << " " << st.size() << "\n";
            if (s[i] == '[') {
                st.push(NestedInteger());
            }
            else if (s[i] == ',') {
                if(!num.empty())st.top().add(NestedInteger(stoi(num)));
                num.clear();
            }
            else if (s[i] == ']') {
                if (!num.empty()) {
                    st.top().add(NestedInteger(stoi(num)));
                    num.clear();
                }
                if (st.size() > 1) {
                    auto now = st.top();
                    st.pop();
                    st.top().add(now);
                }               
            }
            else num += s[i];
        }
        return st.top();
    }
};
java递归 2ms 100%
百道
发布于 2020-01-28
3.1k
设定一个getNest()函数用于返回一个列表类型的NestedInteger。

重要的思想是通过类的全局字符数组和一个下标值让每次调用递归函数都知道要处理哪个位置。


class Solution {
    //递归函数通过字符数组和cur下标确定要处理的位置
    char[] chars;
    int cur = 0;
    public NestedInteger deserialize(String s) {
        chars = s.toCharArray();
        //本身不是一个集合而是一个整数的情况
        if(chars[0]!='[') return new NestedInteger(Integer.valueOf(s));
        //调用递归函数返回根集合
        return getNest();
    }
    public NestedInteger getNest(){
        NestedInteger nest = new NestedInteger();
        int num = 0;//num用于缓存用逗号分割的整数类型的值
        boolean negative = false;//当前记录的整数是不是负数
        while(cur!=chars.length-1){
            cur ++;
            if(chars[cur]==',') continue;
            if(chars[cur]=='[') nest.add(getNest());//遇到[递归获取子集合
            else if(chars[cur]==']') return nest;
            else if(chars[cur]=='-') negative = true;
            else{//是数字的情况
                if(negative) num = 10*num - (chars[cur]-48);
                else num = 10*num + (chars[cur]-48);
                //如果下一个字符是,或者]说明当前数字已经记录完了，需要加入集合中
                if(chars[cur+1]==','||chars[cur+1]==']'){ 
                    nest.add(new NestedInteger(num));
                    num = 0;
                    negative = false;
                }
            }
        }
        return null;
    }
}
2020/06/08编辑：写这篇题解的时候刚开始用Java刷题不久，写的代码不是很优雅，这个代码有几处地方可以修改一下让代码更清晰规范一些：

这段代码中的48代表的就是字符'0'，建议使用'0'替换48。
negative是布尔型，其实可以优化成整型sign，后面就不需要用if-else进行判断，代码更加精炼。
优化后的代码如下：


class Solution {
    //递归函数通过字符数组和cur下标确定要处理的位置
    char[] chars;
    int cur = 0;
    public NestedInteger deserialize(String s) {
        chars = s.toCharArray();
        //本身不是一个集合而是一个整数的情况
        if(chars[0]!='[') return new NestedInteger(Integer.valueOf(s));
        //调用递归函数返回根集合
        return getNest();
    }
    public NestedInteger getNest(){
        NestedInteger nest = new NestedInteger();
        int num = 0;//num用于缓存用逗号分割的整数类型的值
        int sign = 1;//当前记录的整数的符号，1代表整数，-1代表负数
        while(cur!=chars.length-1){
            cur ++;
            if(chars[cur]==',') continue;
            if(chars[cur]=='[') nest.add(getNest());//遇到[递归获取子集合
            else if(chars[cur]==']') return nest;
            else if(chars[cur]=='-') sign = -1;
            else{//是数字的情况
                num = 10*num + sign * (chars[cur]-'0');
                //如果下一个字符是,或者]说明当前数字已经记录完了，需要加入集合中
                if(chars[cur+1]==','||chars[cur+1]==']'){ 
                    nest.add(new NestedInteger(num));
                    num = 0;
                    sign = 1;
                }
            }
        }
        return null;
    }
}

Python3：题目不难，只需理清逻辑
撒盐的Shelby
发布于 2019-10-15
2.7k
刚开始你可能会被NestedInteger搞懵，这是要实现NestedInteger嘛？
仔细阅读它的注释就会发现，这是一个创建嵌套列表的接口，你不需要去实现或者推测它是如何实现的.
换句话说，这是一个辅助类，我们在实现的时候只需要转换成它规定的方式即可.
我们维护一个栈stack用于存储嵌套列表，接下来我们对可能出现的情况分别进行处理：

Step1： 第一个字符不是'['，说明遇到了数字，那么我们就直接返回
注意要包装成NestedInteger对象：

    if s[0] != '[':
        return NestedInteger(int(s))
Step2： 第一个字符是'['，每一个字符可能的情况共有5种，我们分别讨论
- 数字：计算10进制数字大小即可
- 负号：设置符号位为-1
- 左括号：栈append一个空的NestedInteger对象
- 逗号：前面是数字，把栈顶的元素pop出来，然后append(前面的数字)，重新压入栈中
    其实，题目说明了这些字符串都是格式良好的，遇到逗号说明前面肯定有'['，此时栈一定是有元素的
- 右括号：处理同逗号；但还需对嵌套列表进行处理：
    把栈顶元素pop出来(即嵌套底层的list)，
    把新的栈顶(即嵌套的高层list)append刚才pop出来的底层的list，重新压入栈中
实现如下：


class Solution:
    def deserialize(self, s: str) -> NestedInteger:
        
        if s[0] != '[':
            return NestedInteger(int(s))
        
        stack = []
        # num为数字，sign为符号为，is_num为前一个是否为数字
        num, sign, is_num = 0, 1, False
        
        for c in s:
            if c.isdigit():
                num = num * 10 + int(c)
                is_num = True
            elif c == '-':
                sign = -1
            elif c == '[':
                stack.append(NestedInteger())
            elif c == ',' or c == ']':
                # 把刚才遇到的数字append进去
                if is_num:
                    cur_list = stack.pop()
                    cur_list.add(NestedInteger(sign * num))
                    stack.append(cur_list)
                num, sign, is_num = 0, 1, False

                # 此时为嵌套列表
                if c == ']' and len(stack) > 1:
                    cur_list = stack.pop()
                    stack[-1].add(cur_list)

        return stack[0]


[Cpp] 8ms 100% 逐个解析
夕舞雪薇

发布于 2020-01-15
1.0k
解题思路
见代码注释，设置个当前处理到的下标pos，逐个从pos位置开始解析

QQ截图20200111163526.jpg

代码

class Solution {
private:
    int pos = 0;

    NestedInteger parse(string &s) {
        // 如果当前位置是个"["则已更改返回list
        if (s[pos] == '[') return parseList(s);
        // 否则解析一个数
        return parseNum(s);
    }

    // 解析一个数字
    NestedInteger parseNum(string &s) {
        int num = 0;
        // 判断正负
        int sign = s[pos] == '-' ? -1 : 1;
        if (s[pos] == '-' || s[pos] == '+') pos++;

        for (; pos < s.size() && isdigit(s[pos]); pos++)
            num = num * 10 + s[pos] - '0';
        // 返回该数字
        return NestedInteger(sign * num);
    }

    // 解析一个List
    NestedInteger parseList(string &s) {
        NestedInteger ni;
        while (s[pos] != ']') {
            pos++;                    // 跳过[和,
            if (s[pos] == ']') break;
            // 对List中的每个元素进行解析，可能存在数字或者嵌套的List
            ni.add(parse(s));
        }
        pos++;                        // 跳过]
        // 返回解析的List
        return ni;
    }

public:
    NestedInteger deserialize(string s) {
        return parse(s);
    }
};
 
*/