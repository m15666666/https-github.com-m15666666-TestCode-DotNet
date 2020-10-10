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
        var ret = DiffWaysToCompute("2-1-1");
    }

    public IList<int> DiffWaysToCompute(string input)
    {
        if (string.IsNullOrWhiteSpace(input)) return new int[0];

        Dictionary<(int,int), IList<int>> midresults = new Dictionary<(int,int), IList<int>>();
        return DiffWaysToCompute(input, 0, input.Length - 1);
        IList<int> DiffWaysToCompute(string input, int start, int stop)
        {
            var key = (start, stop);
            if (midresults.ContainsKey(key)) return midresults[key];

            List<int> ret = new List<int>();
            for( int index = start; index <= stop; ++index)
            {
                var c = input[index];
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
                var before = DiffWaysToCompute(input, start, index - 1);
                var after = DiffWaysToCompute(input, index + 1, stop);
                foreach (var nums1 in before)
                    foreach (var num2 in after)
                        if (isPlus) ret.Add(nums1 + num2);
                        else if(isMinus) ret.Add(nums1 - num2);
                        else  ret.Add(nums1 * num2);
            }

            if (ret.Count == 0)
            {
                int number = 0;
                for (int index = start; index <= stop; ++index)
                {
                    var c = input[index];
                    number = number * 10 + (c - '0');
                }

                ret.Add(number);
            }
            midresults[key] = ret;
            return ret;
        }
    }


    //public IList<int> DiffWaysToCompute(string input)
    //{
    //    if (string.IsNullOrWhiteSpace(input)) return new int[0];

    //    Dictionary<string, IList<int>> midresults = new Dictionary<string, IList<int>>();
    //    return DiffWaysToCompute(input, midresults);
    //}

    //private IList<int> DiffWaysToCompute(string input, Dictionary<string, IList<int>> midresults )
    //{
    //    if (midresults.ContainsKey(input)) {
    //        Console.WriteLine($"{input} found cache.");
    //        return midresults[input];
    //    }
    //    int index = -1;
    //    List<int> ret = new List<int>();
    //    foreach( var c in input)
    //    {
    //        ++index;
    //        bool isPlus = false;
    //        bool isMinus = false;
    //        bool isMultiply = false;
    //        switch (c)
    //        {
    //            case '+':
    //                isPlus = true;
    //                break;
    //            case '-':
    //                isMinus = true;
    //                break;
    //            case '*':
    //                isMultiply = true;
    //                break;
    //        }
    //        if (!isPlus && !isMinus && !isMultiply) continue;
    //        var before = DiffWaysToCompute(input.Substring(0, index), midresults);
    //        var after = DiffWaysToCompute(input.Substring(index + 1), midresults);
    //        foreach (var nums1 in before)
    //            foreach (var num2 in after)
    //                if (isPlus) ret.Add(nums1 + num2);
    //                else if(isMinus) ret.Add(nums1 - num2);
    //                else  ret.Add(nums1 * num2);
    //    }

    //    if (ret.Count == 0) ret.Add(int.Parse(input));

    //    midresults[input] = ret;

    //    return ret;
    //}
}
/*
public class Solution {
    IList<int> mark_indices = new List<int>();

    public IList<int> DiffWaysToCompute(string input) {
        for (int i = 0; i < input.Length; ++i) {
            if (input[i] == '+' || input[i] == '-' || input[i] == '*') {
                mark_indices.Add(i);
            }
        }
        return this.DiffWaysToCompute(input, 0, input.Length, 0, this.mark_indices.Count);
    }

    private IList<int> DiffWaysToCompute(string input, int start, int end, int mark_start, int mark_end) {
        if (mark_start == mark_end) {
            return new List<int> { int.Parse(input.Substring(start, end-start)) };
        }

        var ret = new List<int>();
        for (int mark_index = mark_start; mark_index < mark_end; ++mark_index) {
            var left_nums = this.DiffWaysToCompute(input, start, this.mark_indices[mark_index], mark_start, mark_index);
            var right_nums = this.DiffWaysToCompute(input, this.mark_indices[mark_index]+1, end, mark_index+1, mark_end);
            foreach (var left_num in left_nums) {
                foreach (var right_num in right_nums) {
                    switch (input[this.mark_indices[mark_index]]) {
                        case '+': ret.Add(left_num + right_num); break;
                        case '-': ret.Add(left_num - right_num); break;
                        default: ret.Add(left_num * right_num); break;
                    }
                }
            }
        }
        return ret;
    }
}

public class Solution {
    //Dictionary<string,>
    public IList<int> DiffWaysToCompute(string input) {
        IList<int> res = new List<int>();
        
        for (int i = 0; i < input.Length; i++) {
            
            char ch = input[i];
            if (ch == '+' || ch == '-' || ch == '*') {
                
                IList<int> left = DiffWaysToCompute(input.Substring(0, i));
                IList<int> right = DiffWaysToCompute(input.Substring(i + 1));
                
                foreach(int l in left) {
                    
                    foreach(int r in right) {
                        
                        if (ch == '+')
                            res.Add(l + r);
                        else if (ch == '-')
                            res.Add(l - r);
                        else
                            res.Add(l * r);
                    }
                }
            }
        }
        if (res.Count == 0)
            res.Add(Int32.Parse(input));
        return res;
    }
    int[] get_num(string s){
        string[]temp=s.Split(new char[]{'-','+','*'});
        int[]rst=new int[temp.Length];
        for(int i=0;i<rst.Length;i++)rst[i]=Int32.Parse(temp[i]);
        return rst;
    }
    char[]get_op(string s,int l){
        char[]rst= new char[l];
        int i=0;
        foreach(char c in s){
           if(c-'0'<0){ rst[i]=c;
            i++;}
        }
        return rst;
    }
    int Cal(int a, int b, char op){
        switch(op){
            case '+':return a+b;
            case '-':return a-b;
            case '*':return a*b;
            default:return -1;
        }
    }
    void print(List<int>nums,List<char>ops){
        for(int i=0;i<nums.Count;i++){
            Console.Write(nums[i]);
            if(i!=ops.Count)Console.Write(ops[i]);
        }
        Console.WriteLine();
    }
    void run(List<int>nums,List<char>ops,HashSet<int>rst){
        //print(nums,ops);
        if(nums.Count==1){rst.Add(nums[0]);return;}
        
        for(int i=1;i<nums.Count;i++){
            int tempa=nums[i];
            int tempb=nums[i-1];
            nums[i-1]=Cal(nums[i-1],nums[i],ops[i-1]);
            char ctemp=ops[i-1];
            nums.RemoveAt(i);
            ops.RemoveAt(i-1);
            run(nums,ops,rst);

            nums.Insert(i,tempa);
            nums[i-1]=tempb;
            ops.Insert(i-1,ctemp);
        }
        //for(int i=1;i<)

    }
}

public class Solution {
    public IList<int> DiffWaysToCompute(string input) {
            List<string> parts = new List<string>();
            string s = "";
            foreach (char c in input)
            {
                if (Char.IsDigit(c))
                    s += c;
                else
                {
                    if (s != "")
                    {
                        parts.Add(s);
                        s = "";
                    }
                    parts.Add(c.ToString());
                }
            }
            if (s != "")
                parts.Add(s);
            List<int> list = new List<int>();
            if (parts.Count == 1)
                list.Add(System.Convert.ToInt32(parts[0]));
            else if (parts.Count == 3)
            {
                int x = System.Convert.ToInt32(parts[0]);
                int y = System.Convert.ToInt32(parts[2]);
                switch (parts[1])
                {
                    case "+":
                        list.Add(x + y);
                        break;
                    case "-":
                        list.Add(x - y);
                        break;
                    case "*":
                        list.Add(x * y);
                        break;
                }
            }
            else if (parts.Count >= 5)
            {
                for (int i = 0; i < parts.Count - 1; i++)
                {
                    list.AddRange(Calc_(DiffWaysToCompute(ListToString(parts, 0, i)), DiffWaysToCompute(ListToString(parts, i + 2, parts.Count - 1)), parts[i + 1]));
                    i++;
                }
            }
            return list;
        }


        private List<int> Calc_(IList<int> list1, IList<int> list2, string op)
        {
            List<int> list = new List<int>();
            foreach(int x in list1)
                foreach (int y in list2)
                {
                    switch (op)
                    {
                        case "+":
                            list.Add(x + y);
                            break;
                        case "-":
                            list.Add(x - y);
                            break;
                        case "*":
                            list.Add(x * y);
                            break;
                    }
                }
            return list;
        }

        private string ListToString(List<string> list, int i, int j)
        {
            string s = "";
            for (int x = i; x <= j; x++)
                s += list[x];
            return s;
        }
}

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
