using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
给定一个含有数字和运算符的字符串，为表达式添加括号，
改变其运算优先级以求出不同的结果。你需要给出所有可能的组合的结果。
有效的运算符号包含 +, - 以及 * 。

示例 1:

输入: "2-1-1"
输出: [0, 2]
解释: 
((2-1)-1) = 0 
(2-(1-1)) = 2
示例 2:

输入: "2*3-4*5"
输出: [-34, -14, -10, -10, 10]
解释: 
(2*(3-(4*5))) = -34 
((2*3)-(4*5)) = -14 
((2*(3-4))*5) = -10 
(2*((3-4)*5)) = -10 
(((2*3)-4)*5) = 10
*/

/// <summary>
/// https://leetcode-cn.com/problems/different-ways-to-add-parentheses/
/// 241. 为运算表达式设计优先级
/// https://blog.csdn.net/w8253497062015/article/details/80732687
/// http://www.dongcoder.com/detail-1114093.html
/// </summary>
class DifferentgWaysToAddParenthesesSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    public IList<int> DiffWaysToCompute(string input)
    {
        if (string.IsNullOrWhiteSpace(input)) return new int[0];

        Dictionary<string, IList<int>> midresults = new Dictionary<string, IList<int>>();
        return DiffWaysToCompute(input, midresults);
    }

    private IList<int> DiffWaysToCompute(string input, Dictionary<string, IList<int>> midresults )
    {
        if (midresults.ContainsKey(input)) {
            Console.WriteLine($"{input} found cache.");
            return midresults[input];
        }
        int index = -1;
        List<int> ret = new List<int>();
        foreach( var c in input)
        {
            ++index;
            bool isPlus = false;
            bool isMinus = false;
            bool isMultiply = false;
            switch (c)
            {
                case '+':
                    isPlus = true;
                    break;
                case '-':
                    isMinus = true;
                    break;
                case '*':
                    isMultiply = true;
                    break;
            }
            if (!isPlus && !isMinus && !isMultiply) continue;
            var before = DiffWaysToCompute(input.Substring(0, index), midresults);
            var after = DiffWaysToCompute(input.Substring(index + 1), midresults);
            foreach (var nums1 in before)
                foreach (var num2 in after)
                    if (isPlus) ret.Add(nums1 + num2);
                    else if(isMinus) ret.Add(nums1 - num2);
                    else  ret.Add(nums1 * num2);
        }

        if (ret.Count == 0) ret.Add(int.Parse(input));

        midresults[input] = ret;

        return ret;
    }
}
/*
//别人的算法
public class Solution {
    Dictionary<Char,Func<int,int,int>> _dict=new Dictionary<char, Func<int, int, int>>{
        {'+',(x,y)=>(x+y)},
        {'-',(x,y)=>(x-y)},
        {'*',(x,y)=>(x*y)}
    };
    // List<int> _list;
    string _str;
    public IList<int> DiffWaysToCompute (string input) {
        _str=input;
        return Down(0,_str.Length);
    }
    private List<int> Down(int begin,int end){
        List<int> cur=new List<int>();
        for (int i = begin; i < end; i++)
        {
            if(_dict.ContainsKey(_str[i]))
                cur.AddRange(Caculate(i,begin,end));
        }
        if(cur.Count==0){
            int v=0;
            for (int i = begin; i < end; i++)
            {
                v=v*10+_str[i]-'0';
            }
            cur.Add(v);
        }
        return cur;
    }
    private List<int> Caculate(int index,int begin,int end){
        List<int> cur=new List<int>();
        Func<int,int,int> func=_dict[_str[index]];
        // if(_dict.ContainsKey(_str[index]))
        {
            List<int> l1=Down(begin,index);
            List<int> l2=Down(index+1,end);
            for (int i = 0; i < l1.Count; i++)
            {
                for (int j = 0; j < l2.Count; j++)
                {
                    cur.Add(func(l1[i],l2[j]));
                }
            }
        }
        return cur;
    }
}
public class Solution {
    public IList<int> DiffWaysToCompute(string input) {
          var list = new List<int>();
            var temp = false;
            for (var i = 0; i < input.Length; i++) {
                var op = input[i];
                if (op == '+' || op == '-' || op == '*') {
                    temp = true;
                    var nums1 = DiffWaysToCompute(input.Substring(0, i));
                    var nums2 = DiffWaysToCompute(input.Substring(i+1));
                    foreach (var n1 in nums1)
                        foreach (var n2 in nums2) {
                            if (op == '+')
                                list.Add(n1 + n2);
                            if (op == '-')
                                list.Add(n1 - n2);
                            if (op == '*')
                                list.Add(n1 * n2);
                        }
                }
            }
            if (!temp)
            {
                return new[] { int.Parse(input) };
            }
            return list;
    }
}     
*/
