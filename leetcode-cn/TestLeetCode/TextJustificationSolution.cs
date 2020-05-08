using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个单词数组和一个长度 maxWidth，重新排版单词，使其成为每行恰好有 maxWidth 个字符，且左右两端对齐的文本。

你应该使用“贪心算法”来放置给定的单词；也就是说，尽可能多地往每行中放置单词。必要时可用空格 ' ' 填充，使得每行恰好有 maxWidth 个字符。

要求尽可能均匀分配单词间的空格数量。如果某一行单词间的空格不能均匀分配，则左侧放置的空格数要多于右侧的空格数。

文本的最后一行应为左对齐，且单词之间不插入额外的空格。

说明:

单词是指由非空格字符组成的字符序列。
每个单词的长度大于 0，小于等于 maxWidth。
输入单词数组 words 至少包含一个单词。
示例:

输入:
words = ["This", "is", "an", "example", "of", "text", "justification."]
maxWidth = 16
输出:
[
   "This    is    an",
   "example  of text",
   "justification.  "
]
示例 2:

输入:
words = ["What","must","be","acknowledgment","shall","be"]
maxWidth = 16
输出:
[
  "What   must   be",
  "acknowledgment  ",
  "shall be        "
]
解释: 注意最后一行的格式应为 "shall be    " 而不是 "shall     be",
     因为最后一行应为左对齐，而不是左右两端对齐。       
     第二行同样为左对齐，这是因为这行只包含一个单词。
示例 3:

输入:
words = ["Science","is","what","we","understand","well","enough","to","explain",
         "to","a","computer.","Art","is","everything","else","we","do"]
maxWidth = 20
输出:
[
  "Science  is  what we",
  "understand      well",
  "enough to explain to",
  "a  computer.  Art is",
  "everything  else  we",
  "do                  "
]
*/
/// <summary>
/// https://leetcode-cn.com/problems/text-justification/
/// 68. 文本左右对齐
/// 
/// 
/// </summary>
class TextJustificationSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public IList<string> FullJustify(string[] words, int maxWidth) {

        StringBuilder builder = new StringBuilder(maxWidth);
        List<string> ret = new List<string>();
        if (words == null || words.Length == 0) return ret;

        int charCount = 0;
        int beginIndex = 0;
        int len = words.Length;
        for (int i = 0; i < len; i++)
        {
            charCount += words[i].Length + 1;

            if (i + 1 == len || charCount + words[i + 1].Length > maxWidth)
            {   
                ret.Add(FillWords(beginIndex, i, i + 1 == len));
                beginIndex = i + 1;
                charCount = 0;
            }
        }
        return ret;

        string FillWords(int beginIndex, int endIndex, bool lastLine)
        {
            const char Space = ' ';
            int wordCount = endIndex - beginIndex + 1;
            int spaceCount = maxWidth + 1 - wordCount;
            for (int i = beginIndex; i <= endIndex; i++)
                spaceCount -= words[i].Length;

            builder.Length = 0;
            if (lastLine)
            {
                for (int i = beginIndex; i < endIndex; i++)
                {
                    builder.Append(words[i]);
                    builder.Append(Space);
                }
            }
            else
            {
                if (1 < wordCount )
                {
                    int spaceSuffix = 1;
                    int spaceAvg = spaceCount / (wordCount - 1);
                    int spaceExtra = spaceCount % (wordCount - 1);
                    for (int i = beginIndex; i < endIndex; i++)
                    {
                        builder.Append(words[i]);
                        builder.Append(Space, spaceSuffix + spaceAvg + ((i - beginIndex) < spaceExtra ? 1 : 0));
                    }
                }
            }
            builder.Append(words[endIndex]);
            if(builder.Length < maxWidth) builder.Append(Space, maxWidth - builder.Length);
            return builder.ToString();
        }
    }
}
/*

【文本左右对齐】平均分布额外空格
Ikaruga
发布于 1 个月前
874
思路
先取出一行能够容纳的单词，将这些单词根据规则填入一行

计算出额外空格的数量 spaceCount，额外空格就是正常书写用不到的空格
21. 由总长度算起
22. 除去每个单词末尾必须的空格，为了统一处理可以在结尾虚拟加上一个长度
23. 除去所有单词的长度

图片.png

按照单词的间隙数量 wordCount - 1，对额外空格平均分布
平均分布可查看 【另一篇题解】，简单来说就是商和余数的计算

对于每个词填充之后，需要填充的空格数量等于 spaceSuffix + spaceAvg + ((i - bg) < spaceExtra)
spaceSuffix 【单词尾部固定的空格】
spaceAvg 【额外空格的平均值，每个间隙都要填入 spaceAvg 个空格】
((i - bg) < spaceExtra) 【额外空格的余数，前 spaceExtra 个间隙需要多 1 个空格】
图片.png

特殊处理
41. 一行只有一个单词，单词左对齐，右侧填满空格
42. 最后一行，所有单词左对齐，中间只有一个空格，最后一个单词右侧填满空格
答题
    string fillWords(vector<string>& words, int bg, int ed, int maxWidth, bool lastLine = false)
    {
        int wordCount = ed - bg + 1;
        int spaceCount = maxWidth + 1 - wordCount;  // 除去每个单词尾部空格， + 1 是最后一个单词的尾部空格的特殊处理
        for (int i = bg; i <= ed; i++)
        {
            spaceCount -= words[i].size();  // 除去所有单词的长度
        }

        int spaceSuffix = 1;    // 词尾空格
        int spaceAvg = (wordCount == 1) ? 1 : spaceCount / (wordCount - 1);     // 额外空格的平均值
        int spaceExtra = (wordCount == 1) ? 0 : spaceCount % (wordCount - 1);   // 额外空格的余数

        string ans;
        for (int i = bg; i < ed; i++)
        {
            ans += words[i];    // 填入单词
            if (lastLine)   // 特殊处理最后一行
            {
                fill_n(back_inserter(ans), 1, ' ');
                continue;
            }
            fill_n(back_inserter(ans), spaceSuffix + spaceAvg + ((i - bg) < spaceExtra), ' ');  // 根据计算结果补上空格
        }
        ans += words[ed];   // 填入最后一个单词
        fill_n(back_inserter(ans), maxWidth - ans.size(), ' '); // 补上这一行最后的空格
        return ans;
    }

    vector<string> fullJustify(vector<string>& words, int maxWidth) 
    {
        vector<string> ans;
        int cnt = 0;
        int bg = 0;
        for (int i = 0; i < words.size(); i++)
        {
            cnt += words[i].size() + 1;

            if (i + 1 == words.size() || cnt + words[i + 1].size() > maxWidth)
            {   // 如果是最后一个单词，或者加上下一个词就超过长度了，即可凑成一行
                ans.push_back(fillWords(words, bg, i, maxWidth, i + 1 == words.size()));
                bg = i + 1;
                cnt = 0;
            }
        }
        return ans;
    }

public class Solution {
    public IList<string> FullJustify(string[] words, int maxWidth)
    {
        List<string> show_lines(List<List<string>> lines)
        {
            return lines.Select((ws, idx) =>
            {
                var emptys = Math.Max(0, maxWidth - ws.Select(p => p.Length).Sum());
                var sb = new System.Text.StringBuilder(maxWidth);
                for (int i = 0; i < ws.Count; i++)
                {
                    bool last_line = idx == lines.Count - 1, last_word = i == ws.Count - 1;
                    var d = last_word ? 1 : ws.Count - i - 1;
                    var pad = emptys / d;
                    if (emptys % d != 0) pad++;
                    if (last_line && !last_word) pad = 1;
                    emptys -= pad;
                    sb.Append(ws[i]);
                    sb.Append(' ', pad);
                }
                return sb.ToString();
            }).ToList();
        }
        IEnumerable<List<string>> split_lines()
        {
            List<string> k = new List<string>();
            int w = 0;
            foreach (var item in words)
            {
                var w1 = w + item.Length + 1;
                if (w == 0) w1--;
                if (w == 0 || w1 <= maxWidth)
                {
                    k.Add(item);
                    w = w1;
                }
                else
                {
                    yield return k;
                    w = item.Length;
                    k = new List<string>{item};
                }
            }
            if (k.Count != 0)
            {
                yield return k;
            }
        }
        return show_lines(split_lines().ToList());
    }
} 
*/
