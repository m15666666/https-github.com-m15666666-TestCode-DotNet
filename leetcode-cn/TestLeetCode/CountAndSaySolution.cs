using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*「外观数列」是一个整数序列，从数字 1 开始，序列中的每一项都是对前一项的描述。前五项如下：

1.     1
2.     11
3.     21
4.     1211
5.     111221
1 被读作  "one 1"  ("一个一") , 即 11。
11 被读作 "two 1s" ("两个一"）, 即 21。
21 被读作 "one 2",  "one 1" （"一个二" ,  "一个一") , 即 1211。

给定一个正整数 n（1 ≤ n ≤ 30），输出外观数列的第 n 项。

注意：整数序列中的每一项将表示为一个字符串。

 

示例 1:

输入: 1
输出: "1"
解释：这是一个基本样例。
示例 2:

输入: 4
输出: "1211"
解释：当 n = 3 时，序列是 "21"，其中我们有 "2" 和 "1" 两组，"2" 可以读作 "12"，也就是出现频次 = 1 而 值 = 2；类似 "1" 可以读作 "11"。所以答案是 "12" 和 "11" 组合在一起，也就是 "1211"。
 
*/
/// <summary>
/// https://leetcode-cn.com/problems/count-and-say/
/// 38. 外观数列
/// 
/// </summary>
class CountAndSaySolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public string CountAndSay(int n)
    {
        if (n < 2) return "1";

        Queue<char> queue = new Queue<char>();
        queue.Enqueue('1');
        int size;
        int count;
        char firstChar;
        for( int i = 1; i < n; i++)
        {
            count = 1;
            firstChar = queue.Dequeue();
            size = queue.Count;
            while(0 < size--)
            {
                var c = queue.Dequeue();
                if (c == firstChar) count++;
                else
                {
                    foreach (var c1 in count.ToString()) queue.Enqueue(c1);
                    queue.Enqueue(firstChar);

                    firstChar = c;
                    count = 1;
                }
            }

            foreach (var c1 in count.ToString()) queue.Enqueue(c1);
            queue.Enqueue(firstChar);
        }
        return new string(queue.ToArray());
    }
}
/*

递归求解！打败98.60%的人数！
HCZM
发布于 5 天前
544
解题思路
使用StringBuilder来操作字符串，效率提高了将近9倍！附上图片说明！

image.png

代码
class Solution {
    public String countAndSay(int n) {
                StringBuilder s = new StringBuilder();
        int p1 = 0;
        int cur = 1;
        if ( n == 1 )
            return "1";
        String str = countAndSay(n - 1);
        for ( cur = 1; cur < str.length(); cur++ ) {
            if ( str.charAt(p1) != str.charAt(cur) ) {// 如果碰到当前字符与前面紧邻的字符不等则更新此次结果
                int count = cur - p1;
                s.append(count).append(str.charAt(p1));
                p1 = cur;
            }
        }
        if ( p1 != cur ){// 防止最后一段数相等，如果不等说明p1到cur-1这段字符串是相等的
            int count = cur - p1;
            s.append(count).append(str.charAt(p1));
        }
        return s.toString();
    }
}
使用String操作的代码
1.png

class Solution {
    public String countAndSay(int n) {
        String s = "";
        int p1 = 0;
        int cur = 1;
        if ( n == 1 )
            return "1";
        String str = countAndSay(n - 1);
        for ( cur = 1; cur < str.length(); cur++ ) {
            if ( str.charAt(p1) != str.charAt(cur) ) {
                int count = cur - p1;
                s = s + count + ""+str.charAt(p1);
                p1 = cur;
            }
        }
        if ( p1 != cur ){
            int count = cur - p1;
            s = s + count +""+ str.charAt(p1);
        }
        return s;
    }
}

public class Solution {
public string CountAndSay(int n)
{
	var res = "1";
	n--;	
	while (n > 0)
	{		
		res = CountAndSay(res);
		n--;
	}
	
	return res;
}

public string CountAndSay(string pre) 
{
	if (pre.Length == 1) return "11";
	
	var index = 1;
	var count = 1;
	var res = string.Empty;
	while (index < pre.Length)
	{
		if (pre[index] == pre[index - 1]) 
		{
			index++;
			count++;
		}
		else
		{
			res = res + count.ToString() + pre[index-1].ToString();
			count = 1;
			index++;
		}
	}
	
	return res + count.ToString() + pre[index-1].ToString();
}
}
*/
