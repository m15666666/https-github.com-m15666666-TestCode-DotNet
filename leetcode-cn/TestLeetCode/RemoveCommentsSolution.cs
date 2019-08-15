using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
*/
/// <summary>
/// https://leetcode-cn.com/problems/remove-comments/
/// 722. 删除注释
/// https://blog.csdn.net/w8253497062015/article/details/80732789
/// </summary>
class RemoveCommentsSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public IList<string> RemoveComments(string[] source)
    {
        List<string> ret = new List<string>();
        if (source == null) return ret;

        StringBuilder s = new StringBuilder();
        bool comment = false;
        foreach (var line in source)
        {
            for (int j = 0; j < line.Length; j++)
            {
                var c = line[j];
                if (!comment && j + 1 < line.Length && c == '/' && line[j + 1] == '/')
                {        // meet "//"  
                    break;  //如果没有注释，并且遇到了//的话，那么s相当于没有内容
                }
                else if (!comment && j + 1 < line.Length && c == '/' && line[j + 1] == '*')
                {   // meet "/*"  
                    comment = true;  //如果没有注释，并且遇到了/*就说明有注释，直到下一个*/为止
                    ++j;
                }
                else if (comment && j + 1 < line.Length && c == '*' && line[j + 1] == '/')
                {    // meet "*/"  
                    comment = false;  //如果还在注释的情况下，遇到*/，那么就说明结束注释
                    ++j;
                }
                else if (!comment)
                {
                    s.Append(c);  //没有注释的字符输入进s中
                }
            }

            if (!comment && 0 < s.Length)
            {  //没有注释且s中有值
                ret.Add(s.ToString());
                s.Clear();
            }
        }
        return ret;
    }
}
/*
public class Solution {
    public IList<string> RemoveComments(string[] source) {
        List<string> ans = new List<string>();
        bool isBlock = false;
        
        StringBuilder sb = new StringBuilder();
        foreach(string line in source){
            //smart
            if(!isBlock)
                sb.Clear();
            
            int i = 0;
            
            while(i < line.Length){
                if(!isBlock && i+1 < line.Length && line[i] == '/' && line[i+1] == '*'){
                    isBlock = true;
                    i++;
                }
                else if(isBlock && i+1 < line.Length && line[i] == '*' && line[i+1] == '/'){
                    isBlock = false;
                    i++;
                }
                else if(!isBlock && i+1 < line.Length && line[i] == '/' && line[i+1] == '/')
                    break;
                else if(!isBlock)
                    sb.Append(line[i]);
                //smart    
                i++;
            }
            if(!isBlock && sb.Length > 0)
                ans.Add(sb.ToString());
        }
        
        return ans.Cast<string>().ToList();
    }
}

*/
