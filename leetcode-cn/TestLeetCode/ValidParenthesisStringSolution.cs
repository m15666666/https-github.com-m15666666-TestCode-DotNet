using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个只包含三种字符的字符串：（ ，） 和 *，写一个函数来检验这个字符串是否为有效字符串。
有效字符串具有如下规则：

任何左括号 ( 必须有相应的右括号 )。
任何右括号 ) 必须有相应的左括号 ( 。
左括号 ( 必须在对应的右括号之前 )。
* 可以被视为单个右括号 ) ，或单个左括号 ( ，或一个空字符串。
一个空字符串也被视为有效字符串。
示例 1:

输入: "()"
输出: True
示例 2:

输入: "(*)"
输出: True
示例 3:

输入: "(*))"
输出: True
注意:

字符串大小将在 [1，100] 范围内。 
*/
/// <summary>
/// https://leetcode-cn.com/problems/valid-parenthesis-string/
/// 678. 有效的括号字符串
/// https://blog.csdn.net/curry3030/article/details/80879375
/// </summary>
class ValidParenthesisStringSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public bool CheckValidString(string s)
    {
        if (string.IsNullOrEmpty(s)) return true;

        var stack1 = new Stack<int>();
        var stack2 = new Stack<int>();

        for (int i = 0; i < s.Length; i++)
        {
            switch (s[i])
            {
                case '(':
                    stack1.Push(i);
                    break;
                    
                case '*':
                    stack2.Push(i);
                    break;

                default:
                    {
                        if (0 < stack1.Count)
                            stack1.Pop();
                        else if (0 < stack2.Count)
                            stack2.Pop();
                        else return false;
                    }
                    break;
            }
        }

        if (stack2.Count < stack1.Count) return false;

        while(0 < stack1.Count)
            if ( stack2.Pop() < stack1.Pop() ) return false;

        return true;
    }
}
/*
public class Solution {
    public bool CheckValidString(string s) {
        if(s == null || s.Length == 0)
            return true;
        
        LinkedList<char> list = new LinkedList<char>();
        for(int i = 0; i < s.Length; i++){
            switch(s[i]){
                case '(':
                    list.AddLast(s[i]);
                    break;
                case'*':
                    list.AddLast(s[i]);
                    break;    
                case ')':
                    if(list.Count != 0){
                        LinkedListNode<char> node = list.FindLast('(');
                        if(node == null)
                            node = list.FindLast('*');
                        list.Remove(node);
                    }
                    else
                        return false;
                    break;
                default:
                    break;
            }         
        }
        
        var stack = new Stack<char>();
        while(list.Count != 0){
            char c = list.First.Value;
            if(c == '('){
                stack.Push(c);
            }
            else {
                if(stack.Count != 0)
                    stack.Pop();
            }
            list.RemoveFirst();
        }        
        return stack.Count == 0;
    }
}
public class Solution {
    public bool CheckValidString(string s) {
        if(string.IsNullOrEmpty(s))
            return true;
      
     List<int> left = new List<int>();
     List<int> star = new List<int>();
        for(int i = 0; i< s.Length ; i++)
        {
            if(s[i] == '(')
               left.Add(i);
            if(s[i] == '*')
            {
               star.Add(i); 
            }
            if(s[i] == ')')
            {
                if(left.Count > 0)
                {
                    left.RemoveAt(left.Count-1);
                }
                else if(star.Count > 0)
                {
                   star.RemoveAt(star.Count-1);
                }
                else
                    return false;
            }
        }
          if(left.Count > 0)
          {
              if(star.Count < left.Count)
                  return false;
              int index = star.Count - 1;
              for(int i = left.Count -1 ;i>=0;i--)
              {
                  for(int j = index;j>=0;j--)
                  {
                    if(left[i]>star[j])
                       return false;
                      else
                      {
                          index = j-1;
                          break;
                      }
                  }
              }
          }
             
        return true;
    }
}
*/
