using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个字符串，逐个翻转字符串中的每个单词。

 

示例 1：

输入: "the sky is blue"
输出: "blue is sky the"
示例 2：

输入: "  hello world!  "
输出: "world! hello"
解释: 输入字符串可以在前面或者后面包含多余的空格，但是反转后的字符不能包括。
示例 3：

输入: "a good   example"
输出: "example good a"
解释: 如果两个单词间有多余的空格，将反转后单词间的空格减少到只含一个。
 

说明：

无空格字符构成一个单词。
输入字符串可以在前面或者后面包含多余的空格，但是反转后的字符不能包括。
如果两个单词间有多余的空格，将反转后单词间的空格减少到只含一个。
 

进阶：

请选用 C 语言的用户尝试使用 O(1) 额外空间复杂度的原地解法。

*/

/// <summary>
/// https://leetcode-cn.com/problems/reverse-words-in-a-string/
/// 151. 翻转字符串里的单词
/// 给定一个字符串，逐个翻转字符串中的每个单词。
/// 示例:  
/// 输入: "the sky is blue",
/// 输出: "blue is sky the".
/// </summary>
class ReverseWordsOfStringSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    public string ReverseWords(string s)
    {
        if (string.IsNullOrWhiteSpace(s)) return string.Empty;

        var parts = s.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        Array.Reverse(parts);
        return string.Join(' ', parts);
    }

}
/*

翻转字符串里的单词
力扣官方题解
发布于 2020-04-09
26.2k
📺视频题解

📖文字题解
方法一：使用语言特性
思路和算法

很多语言对字符串提供了 split（拆分），reverse（翻转）和 join（连接）等方法，因此我们可以简单的调用内置的 API 完成操作：

使用 split 将字符串按空格分割成字符串数组；
使用 reverse 将字符串数组进行反转；
使用 join 方法将字符串数组拼成一个字符串。
fig


class Solution {
    public String reverseWords(String s) {
        // 除去开头和末尾的空白字符
        s = s.trim();
        // 正则匹配连续的空白字符作为分隔符分割
        List<String> wordList = Arrays.asList(s.split("\\s+"));
        Collections.reverse(wordList);
        return String.join(" ", wordList);
    }
}
复杂度分析

时间复杂度：O(N)O(N)，其中 N 为输入字符串的长度。

空间复杂度：O(N)O(N)，用来存储字符串分割之后的结果。

方法二：自行编写对应的函数
思路和算法

我们也可以不使用语言中的 API，而是自己编写对应的函数。在不同语言中，这些函数实现是不一样的，主要的差别是有些语言的字符串不可变（如 Java 和 Python)，有些语言的字符串可变（如 C++)。

对于字符串不可变的语言，首先得把字符串转化成其他可变的数据结构，同时还需要在转化的过程中去除空格。

fig

对于字符串可变的语言，就不需要再额外开辟空间了，直接在字符串上原地实现。在这种情况下，反转字符和去除空格可以一起完成。

fig


class Solution {
    public StringBuilder trimSpaces(String s) {
        int left = 0, right = s.length() - 1;
        // 去掉字符串开头的空白字符
        while (left <= right && s.charAt(left) == ' ') ++left;

        // 去掉字符串末尾的空白字符
        while (left <= right && s.charAt(right) == ' ') --right;

        // 将字符串间多余的空白字符去除
        StringBuilder sb = new StringBuilder();
        while (left <= right) {
            char c = s.charAt(left);

            if (c != ' ') sb.append(c);
            else if (sb.charAt(sb.length() - 1) != ' ') sb.append(c);

            ++left;
        }
        return sb;
    }

    public void reverse(StringBuilder sb, int left, int right) {
        while (left < right) {
            char tmp = sb.charAt(left);
            sb.setCharAt(left++, sb.charAt(right));
            sb.setCharAt(right--, tmp);
        }
    }

    public void reverseEachWord(StringBuilder sb) {
        int n = sb.length();
        int start = 0, end = 0;

        while (start < n) {
            // 循环至单词的末尾
            while (end < n && sb.charAt(end) != ' ') ++end;
            // 翻转单词
            reverse(sb, start, end - 1);
            // 更新start，去找下一个单词
            start = end + 1;
            ++end;
        }
    }

    public String reverseWords(String s) {
        StringBuilder sb = trimSpaces(s);

        // 翻转字符串
        reverse(sb, 0, sb.length() - 1);

        // 翻转每个单词
        reverseEachWord(sb);

        return sb.toString();
    }
}
复杂度分析

时间复杂度：O(N)O(N)，其中 N 为输入字符串的长度。

空间复杂度：Java 和 Python 的方法需要 O(N)O(N) 的空间来存储字符串，而 C++ 方法只需要 O(1) 的额外空间来存放若干变量。

方法三：双端队列
思路和算法

由于双端队列支持从队列头部插入的方法，因此我们可以沿着字符串一个一个单词处理，然后将单词压入队列的头部，再将队列转成字符串即可。

fig


class Solution {
    public String reverseWords(String s) {
        int left = 0, right = s.length() - 1;
        // 去掉字符串开头的空白字符
        while (left <= right && s.charAt(left) == ' ') ++left;

        // 去掉字符串末尾的空白字符
        while (left <= right && s.charAt(right) == ' ') --right;

        Deque<String> d = new ArrayDeque();
        StringBuilder word = new StringBuilder();
        
        while (left <= right) {
            char c = s.charAt(left);
            if ((word.length() != 0) && (c == ' ')) {
                // 将单词 push 到队列的头部
                d.offerFirst(word.toString());
                word.setLength(0);
            } else if (c != ' ') {
                word.append(c);
            }
            ++left;
        }
        d.offerFirst(word.toString());

        return String.join(" ", d);
    }
}
复杂度分析

时间复杂度：O(N)O(N)，其中 N 为输入字符串的长度。

空间复杂度：O(N)O(N)，双端队列存储单词需要 O(N)O(N) 的空间。

下一篇：151.翻转字符串里的单词 +186. 翻转字符串里的单词 II

 
 
 
*/