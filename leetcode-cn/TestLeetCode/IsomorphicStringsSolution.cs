using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定两个字符串 s 和 t，判断它们是否是同构的。

如果 s 中的字符可以被替换得到 t ，那么这两个字符串是同构的。

所有出现的字符都必须用另一个字符替换，同时保留字符的顺序。两个字符不能映射到同一个字符上，但字符可以映射自己本身。

示例 1:

输入: s = "egg", t = "add"
输出: true
示例 2:

输入: s = "foo", t = "bar"
输出: false
示例 3:

输入: s = "paper", t = "title"
输出: true
说明:
你可以假设 s 和 t 具有相同的长度。
 
 
*/
/// <summary>
/// https://leetcode-cn.com/problems/isomorphic-strings/
/// 205. 同构字符串
/// 
/// 
/// 
/// </summary>
class IsomorphicStringsSolution
{

    public bool IsIsomorphic(string s, string t) {
        if (string.IsNullOrEmpty(s) && string.IsNullOrEmpty(t)) return true;
        if (s.Length != t.Length) return false;
        int len = s.Length;
        Dictionary<char, char> map = new Dictionary<char, char>();
        HashSet<char> used = new HashSet<char>();
        for( int i = 0; i < len; i++)
        {
            char c1 = s[i];
            char c2 = t[i];
            if (map.ContainsKey(c2))
            {
                if (c1 != map[c2]) return false;
            }
            else
            {
                if (used.Contains(c1)) return false;
                used.Add(c1);
                map[c2] = c1;
            }
        }
        return true;
        
    }
}
/*
public class Solution {
    public bool IsIsomorphic(string s, string t) {
       // 中间语言转换版本
        var len=s.Length;
        int[] mapS =new int[128];
        int[] mapT =new int[128];
        for(var i=0;i<len;i++)
        {
            if(mapS[s[i]]!=mapT[t[i]]) return false;
            
            if(mapS[s[i]]==0)
            {
                mapS[s[i]]=i+1;
                mapT[t[i]]=i+1;
            }
        }
        
        return true;
        
    }
}

public class Solution
{
	public string Isomorphic(string s)
	{
		Dictionary<char, char> charMap = new Dictionary<char, char>();
		StringBuilder builder = new StringBuilder();
		char ch = '0';
		foreach (char c in s)
		{
			if (!charMap.ContainsKey(c))
				charMap.Add(c, ch++);
			builder.Append(charMap[c]);
		}
		return builder.ToString();
	}
	public bool IsIsomorphic(string s, string t)
	{
		s = Isomorphic(s);
		t = Isomorphic(t);
		return string.Equals(t,s);
	}
}

public class Solution {
    public bool IsIsomorphic(string s, string t)
    {
        Hashtable tb = new Hashtable();
        char[] c1 = s.ToCharArray();//egg
        char[] c2 = t.ToCharArray();//add
        for(int i = 0; i < c1.Length; i++)
        {
            if (tb.ContainsKey(c1[i]))
            {
                //title    paper

                if((char)tb[c1[i]] != c2[i])
                {
                    return false;
                }
            }
            else
            {
                if (tb.ContainsValue(c2[i])) return false;
                tb.Add(c1[i],c2[i]);
            }

        }
        return true;
    }
}



 
 
 
*/