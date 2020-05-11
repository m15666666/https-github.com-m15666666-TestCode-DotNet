using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
以 Unix 风格给出一个文件的绝对路径，你需要简化它。或者换句话说，将其转换为规范路径。

在 Unix 风格的文件系统中，一个点（.）表示当前目录本身；此外，两个点 （..） 表示将目录切换到上一级（指向父目录）；两者都可以是复杂相对路径的组成部分。更多信息请参阅：Linux / Unix中的绝对路径 vs 相对路径

请注意，返回的规范路径必须始终以斜杠 / 开头，并且两个目录名之间必须只有一个斜杠 /。最后一个目录名（如果存在）不能以 / 结尾。此外，规范路径必须是表示绝对路径的最短字符串。

 

示例 1：

输入："/home/"
输出："/home"
解释：注意，最后一个目录名后面没有斜杠。
示例 2：

输入："/../"
输出："/"
解释：从根目录向上一级是不可行的，因为根是你可以到达的最高级。
示例 3：

输入："/home//foo/"
输出："/home/foo"
解释：在规范路径中，多个连续斜杠需要用一个斜杠替换。
示例 4：

输入："/a/./b/../../c/"
输出："/c"
示例 5：

输入："/a/../../b/../c//.//"
输出："/c"
示例 6：

输入："/a//b////c/d//././/.."
输出："/a/b/c"

*/
/// <summary>
/// https://leetcode-cn.com/problems/simplify-path/
/// 71.简化路径
/// 
/// 给定一个文档 (Unix-style) 的完全路径，请进行路径简化。
/// https://blog.csdn.net/MebiuW/article/details/51399770
/// </summary>
class SimplifyPathSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public string SimplifyPath(string path)
    {
        if (string.IsNullOrWhiteSpace(path)) return path;

        var parts = path.Split('/', StringSplitOptions.RemoveEmptyEntries);
        List<string> stack = new List<string>();
        foreach( var part in parts)
        {
            if (part == ".") continue;
            if(part == "..")
            {
                if ( 0 < stack.Count) stack.RemoveAt(stack.Count-1);
                continue;
            }

            stack.Add( part );
        }

        if (0 == stack.Count) return "/";

        StringBuilder builder = new StringBuilder(path.Length);
        foreach( var part in stack)
            builder.Append('/').Append(part);

        return builder.ToString();
    }

    //public string SimplifyPath(string path)
    //{
    //    if (string.IsNullOrWhiteSpace(path)) return path;

    //    var parts = path.Split('/');

    //    Stack<string> stack = new Stack<string>();

    //    foreach( var part in parts)
    //    {
    //        if (string.IsNullOrWhiteSpace(part)) continue;
    //        if (part == ".") continue;
    //        if(part == "..")
    //        {
    //            if ( 0 < stack.Count) stack.Pop();
    //            continue;
    //        }

    //        stack.Push( part );
    //    }

    //    if (0 == stack.Count) return "/";

    //    StringBuilder sb = new StringBuilder();
    //    foreach( var a in stack.Reverse())
    //    {
    //        sb.Append($"/{a}");
    //    }

    //    return sb.ToString();
    //}
}
/*

反向遍历，容易理解
Hooollin
发布于 1 个月前
1.3k
因为只涉及到对路径的简化，并没有cd somedir等从上往下切换目录的操作，如果从前往后遍历那么很显然需要记录已经访问到的路径名，还需要对".."进行返回上一级的操作，感觉很麻烦。

那么转换一下思路，如果从后往前遍历的话，如果碰到".."，那么对于接下来碰到的有效路径名，我们首先消除掉".."的影响，直接跳过；在没有碰到".."之前或是碰到的".."已经被处理完了，那么我们访问到的路径名就一定在最后的有效路径名里面；

思路比较简单，代码如下。

public String simplifyPath(String path) {
        String[] dirs = path.split("/");
        List<String> res = new ArrayList<>();
        int jmpCount = 0;
        for(int i = dirs.length - 1; i >= 0; i--){
            // 当前目录和空目录的情况跳过
            if(dirs[i].equals("") || dirs[i].equals(".")){
                continue;
            }
            // 如果要跳到父目录，记录一下需要跳过非" "、"."、".."的数量
            if(dirs[i].equals("..")){
                jmpCount += 1;
                continue;
            }
            // 如果之前累计的跳到父目录还有没处理完，那么当前的目录需要跳过
            if(jmpCount > 0){
                jmpCount -= 1;
                continue;
            }
            res.add(0, dirs[i]);
        }
        return "/" + String.join("/", res);
    }
	
Java 易懂,易解,效率高
StackOverflow
发布于 9 个月前
7.5k
1.此题主要考察的是栈,所以定义一个辅助栈;
2.先把字符串以"/"为分隔符分割成数组,此时数组有"路径"、""、"."、".."这四种情况;
3.遍历数组,当s[i].equals("..")并且栈不空时pop,当!s[i].equals("") && !s[i].equals(".") && !s[i].equals(".."),即s[i]是路径入栈;
4.栈空,返回"/",栈非空,用StringBuffer做一个连接返回即可;
5完结。

    public String simplifyPath(String path) {
        String[] s = path.split("/");
        Stack<String> stack = new Stack<>();
        
        for (int i = 0; i < s.length; i++) {
            if (!stack.isEmpty() && s[i].equals(".."))
                stack.pop();
            else if (!s[i].equals("") && !s[i].equals(".") && !s[i].equals(".."))
                stack.push(s[i]);
        }
        if (stack.isEmpty())
            return "/";

        StringBuffer res = new StringBuffer();
        for (int i = 0; i < stack.size(); i++) {
            res.append("/" + stack.get(i));
        }
        return res.toString();
    }
	
public class Solution {
    public string SimplifyPath(string path) {
        Stack<string> s = new Stack<string>();

            string[] spiltArr = path.Split('/');
            for (int i = 0; i < spiltArr.Length; i++)
            {
                if (string.IsNullOrEmpty(spiltArr[i]))
                {
                    continue;
                }

                if (spiltArr[i] == "..")
                {
                    if (s.Count > 0)
                    {
                        s.Pop();
                    }
                }
                else if (spiltArr[i] != ".")
                {
                    s.Push(spiltArr[i]);
                }
            }

            if (s.Count != 0)
            {
                StringBuilder sb = new StringBuilder();
                while (s.Count != 0)
                {
                    sb.Insert(0, s.Pop());
                    sb.Insert(0, "/");
                }
                return sb.ToString();
            }
            else
            {
                return "/";
            }
    }
}
 
*/