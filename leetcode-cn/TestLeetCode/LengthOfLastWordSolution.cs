using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个仅包含大小写字母和空格 ' ' 的字符串 s，返回其最后一个单词的长度。如果字符串从左向右滚动显示，那么最后一个单词就是最后出现的单词。

如果不存在最后一个单词，请返回 0 。

说明：一个单词是指仅由字母组成、不包含任何空格字符的 最大子字符串。

 

示例:

输入: "Hello World"
输出: 5
*/
/// <summary>
/// https://leetcode-cn.com/problems/length-of-last-word/
/// 58. 最后一个单词的长度
/// 
/// </summary>
class LengthOfLastWordSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int LengthOfLastWord(string s) 
    {
        int end = s.Length - 1;
        while (-1 < end && s[end] == ' ') end--;
        if (end < 0) return 0;

        int begin = end;
        while (-1 < begin && s[begin] != ' ') begin--;
        return end - begin;
    }

}
/*

画解算法：58. 最后一个单词的长度
灵魂画手
发布于 10 个月前
19.7k
思路
标签：字符串遍历
从字符串末尾开始向前遍历，其中主要有两种情况
第一种情况，以字符串"Hello World"为例，从后向前遍历直到遍历到头或者遇到空格为止，即为最后一个单词"World"的长度5
第二种情况，以字符串"Hello World "为例，需要先将末尾的空格过滤掉，再进行第一种情况的操作，即认为最后一个单词为"World"，长度为5
所以完整过程为先从后过滤掉空格找到单词尾部，再从尾部向前遍历，找到单词头部，最后两者相减，即为单词的长度
时间复杂度：O(n)，n为结尾空格和结尾单词总体长度
代码
class Solution {
    public int lengthOfLastWord(String s) {
        int end = s.length() - 1;
        while(end >= 0 && s.charAt(end) == ' ') end--;
        if(end < 0) return 0;
        int start = end;
        while(start >= 0 && s.charAt(start) != ' ') start--;
        return end - start;
    }
}
画解


下一篇：PHP 解法

public class Solution {
    public int LengthOfLastWord(string s) {
        int len = 0;
        bool first = false;
        for(int i=s.Length-1;i>=0;i--) {
            if(s[i]!=' ') {
                first = true;
                len++;
            }else if(first) {
                break;
            }
        }
        return len;
    }
}

*/