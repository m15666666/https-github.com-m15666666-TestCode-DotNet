using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
如果我们可以将小写字母插入模式串 pattern 得到待查询项 query，那么待查询项与给定模式串匹配。（我们可以在任何位置插入每个字符，也可以插入 0 个字符。）

给定待查询列表 queries，和模式串 pattern，返回由布尔值组成的答案列表 answer。只有在待查项 queries[i] 与模式串 pattern 匹配时， answer[i] 才为 true，否则为 false。

 

示例 1：

输入：queries = ["FooBar","FooBarTest","FootBall","FrameBuffer","ForceFeedBack"], pattern = "FB"
输出：[true,false,true,true,false]
示例：
"FooBar" 可以这样生成："F" + "oo" + "B" + "ar"。
"FootBall" 可以这样生成："F" + "oot" + "B" + "all".
"FrameBuffer" 可以这样生成："F" + "rame" + "B" + "uffer".
示例 2：

输入：queries = ["FooBar","FooBarTest","FootBall","FrameBuffer","ForceFeedBack"], pattern = "FoBa"
输出：[true,false,true,false,false]
解释：
"FooBar" 可以这样生成："Fo" + "o" + "Ba" + "r".
"FootBall" 可以这样生成："Fo" + "ot" + "Ba" + "ll".
示例 3：

输出：queries = ["FooBar","FooBarTest","FootBall","FrameBuffer","ForceFeedBack"], pattern = "FoBaT"
输入：[false,true,false,false,false]
解释： 
"FooBarTest" 可以这样生成："Fo" + "o" + "Ba" + "r" + "T" + "est".
 

提示：

1 <= queries.length <= 100
1 <= queries[i].length <= 100
1 <= pattern.length <= 100
所有字符串都仅由大写和小写英文字母组成。
*/
/// <summary>
/// https://leetcode-cn.com/problems/camelcase-matching/
/// 1023. 驼峰式匹配
/// 
/// </summary>
class CamelcaseMatchingSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public IList<bool> CamelMatch(string[] queries, string pattern)
    {
        string GetOtherChars(string query)
        {
            int index = 0;
            int count;
            var builder = new StringBuilder("a"); // 避免两个串相等时返回""
            foreach (var t in pattern)
            {
                int index2 = query.IndexOf(t, index);
                if (index2 < 0) return "";

                count = index2 - index;
                if (0 < count) builder.Append(query, index, count);

                index = index2 + 1;
            }

            builder.Append(query.Substring(index));
            return builder.ToString();
        }

        var ret = new List<bool>(queries.Length);
        foreach (var query in queries)
        {
            var others = GetOtherChars(query);
            if (string.IsNullOrEmpty( others )) ret.Add(false);
            else ret.Add(others.ToLower().Equals(others));
        }
        return ret;
    }
}
/*
从原串中去掉模式串，如果剩余的都是小写字母则为true
刷题er
61 阅读
解题思路
此处撰写解题思路

代码
class Solution {
    public List<Boolean> camelMatch(String[] queries, String pattern) {
        List<Boolean> res = new ArrayList<>(queries.length);
        for (String query : queries) {
            String other = getOther(query, pattern);
            if (other.equals("")) {
                res.add(false);
            } else {
                res.add(other.toLowerCase().equals(other));
            }
        }
        return res;
    }

    private static String getOther(String query, String pattern) {
        int index = 0;
        StringBuilder sb = new StringBuilder("a"); // 避免两个串相等时返回""
        for (int i = 0; i < pattern.length(); i++) {
            char t = pattern.charAt(i);
            int index2 = query.indexOf(t, index);
            if (index2 < 0) {
                return "";
            }
            sb.append(query, index, index2);
            index = index2 + 1;
        }
        sb.append(query.substring(index));
        return sb.toString();
    }
}
下一篇：驼峰式匹配
 
*/
