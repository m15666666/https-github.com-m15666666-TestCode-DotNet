/*
给定两个字符串 s 和 t，它们只包含小写字母。

字符串 t 由字符串 s 随机重排，然后在随机位置添加一个字母。

请找出在 t 中被添加的字母。
 

示例 1：

输入：s = "abcd", t = "abcde"
输出："e"
解释：'e' 是那个被添加的字母。
示例 2：

输入：s = "", t = "y"
输出："y"
示例 3：

输入：s = "a", t = "aa"
输出："a"
示例 4：

输入：s = "ae", t = "aea"
输出："a"
 

提示：

0 <= s.length <= 1000
t.length == s.length + 1
s 和 t 只包含小写字母

*/

/// <summary>
/// https://leetcode-cn.com/problems/find-the-difference/
/// 389. 找不同
///
///
/// </summary>
internal class FindTheDifferenceSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public char FindTheDifference(string s, string t)
    {
        int[] count = new int[26];
        foreach (var c in s) count[c - 'a']++;
        foreach (var c in t)
        {
            int index = c - 'a';
            count[index]--;
            if (count[index] < 0) return c;
        }
        return ' ';
    }
}

/*
找不同
力扣官方题解
发布于 2020-12-17
18.6k
方法一：计数
首先遍历字符串 ss，对其中的每个字符都将计数值加 11；然后遍历字符串 tt，对其中的每个字符都将计数值减 11。当发现某个字符计数值为负数时，说明该字符在字符串 tt 中出现的次数大于在字符串 ss 中出现的次数，因此该字符为被添加的字符。


class Solution {
    public char findTheDifference(String s, String t) {
        int[] cnt = new int[26];
        for (int i = 0; i < s.length(); ++i) {
            char ch = s.charAt(i);
            cnt[ch - 'a']++;
        }
        for (int i = 0; i < t.length(); ++i) {
            char ch = t.charAt(i);
            cnt[ch - 'a']--;
            if (cnt[ch - 'a'] < 0) {
                return ch;
            }
        }
        return ' ';
    }
}
复杂度分析

时间复杂度：O(N)O(N)，其中 NN 为字符串的长度。

空间复杂度：O(|\Sigma|)O(∣Σ∣)，其中 \SigmaΣ 是字符集，这道题中字符串只包含小写字母，|\Sigma|=26∣Σ∣=26。需要使用数组对每个字符计数。

方法二：求和
将字符串 ss 中每个字符的 ASCII 码的值求和，得到 A_sA 
s
​	
 ；对字符串 tt 同样的方法得到 A_tA 
t
​	
 。两者的差值 A_t-A_sA 
t
​	
 −A 
s
​	
  即代表了被添加的字符。


class Solution {
    public char findTheDifference(String s, String t) {
        int as = 0, at = 0;
        for (int i = 0; i < s.length(); ++i) {
            as += s.charAt(i);
        }
        for (int i = 0; i < t.length(); ++i) {
            at += t.charAt(i);
        }
        return (char) (at - as);
    }
}
复杂度分析

时间复杂度：O(N)O(N)。

空间复杂度：O(1)O(1)。

方法三：位运算
如果将两个字符串拼接成一个字符串，则问题转换成求字符串中出现奇数次的字符。类似于「136. 只出现一次的数字」，我们使用位运算的技巧解决本题。


class Solution {
    public char findTheDifference(String s, String t) {
        int ret = 0;
        for (int i = 0; i < s.length(); ++i) {
            ret ^= s.charAt(i);
        }
        for (int i = 0; i < t.length(); ++i) {
            ret ^= t.charAt(i);
        }
        return (char) ret;
    }
}
复杂度分析

时间复杂度：O(N)O(N)。

空间复杂度：O(1)O(1)。

*/