using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个非空字符串，其中包含字母顺序打乱的英文单词表示的数字0-9。按升序输出原始的数字。

注意:

输入只包含小写英文字母。
输入保证合法并可以转换为原始的数字，这意味着像 "abc" 或 "zerone" 的输入是不允许的。
输入字符串的长度小于 50,000。
示例 1:

输入: "owoztneoer"

输出: "012" (zeroonetwo)
示例 2:

输入: "fviefuro"

输出: "45" (fourfive) 
*/
/// <summary>
/// https://leetcode-cn.com/problems/reconstruct-original-digits-from-english/
/// 423. 从英文中重建数字
/// https://blog.csdn.net/qq_36946274/article/details/81587007
/// </summary>
class ReconstructOriginalDigitsFromEnglishSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public string OriginalDigits(string s)
    {
        if (string.IsNullOrWhiteSpace(s)) return string.Empty;
        Dictionary<char, int> char2Count = new Dictionary<char, int>();
        foreach( var c in s)
        {
            if (char2Count.ContainsKey(c)) char2Count[c]++;
            else char2Count.Add(c, 1);
        }

        Dictionary<int, int> num2Count = new Dictionary<int, int>();
        // 观察英文字母结果可知，0，2，4，6，8，这五个英文字母zero,two,four,six,eight，分别对应一个独一无二的字母：z,w,u,x,g 
        CountNumber(char2Count, num2Count, 0, 'z');
        CountNumber(char2Count, num2Count, 2, 'w');
        CountNumber(char2Count, num2Count, 4, 'u');
        CountNumber(char2Count, num2Count, 6, 'x');
        CountNumber(char2Count, num2Count, 8, 'g');
        // 观察剩下的英文字母可知，1，3，5，7，9，这五个英文字母one,three,five,seven,nine，也分别对应一个独一无二的字母：o,r,f,s,e（为什么说nine对应一个e呢，应为处理完前面所有的字符，剩下的e的数量也只能代表nine的数量了）。 
        CountNumber(char2Count, num2Count, 1, 'o');
        CountNumber(char2Count, num2Count, 3, 'r');
        CountNumber(char2Count, num2Count, 5, 'f');
        CountNumber(char2Count, num2Count, 7, 's');
        CountNumber(char2Count, num2Count, 9, 'e');

        StringBuilder sb = new StringBuilder();
        foreach( var n in Enumerable.Range(0, 10))
        {
            if (!num2Count.ContainsKey(n)) continue;
            for (int i = num2Count[n]; 0 < i; i--) sb.Append(n);
        }
        return sb.ToString();
    }

    private static readonly Dictionary<int, string> Num2Name = new Dictionary<int, string> {
        {0, "zero" },
        {1, "one" },
        {2, "two" },
        {3, "three" },
        {4, "four" },
        {5, "five" },
        {6, "six" },
        {7, "seven" },
        {8, "eight" },
        {9, "nine" },
    };

    private void CountNumber( Dictionary<char, int> char2Count, Dictionary<int, int> num2Count, int num, char identifier )
    {
        IEnumerable<char> removeChars = Num2Name[num];
        while (char2Count.ContainsKey(identifier))
        {
            if (num2Count.ContainsKey(num)) num2Count[num]++;
            else num2Count.Add(num, 1);

            foreach( var c in removeChars)
            {
                if (char2Count[c] == 1) char2Count.Remove(c);
                else char2Count[c]--;
            }
        }
    }
}
/*
public class Solution
{
    public string OriginalDigits(string s)
    {
        Dictionary<char, int> dictionary = new Dictionary<char, int>();
        for (int i = 0; i < 26; ++i)
        {
            dictionary[(char)('a' + i)] = 0;
        }
        for (int i = 0; i < s.Length; ++i)
        {
            ++dictionary[s[i]];
        }
        StringBuilder builder = new StringBuilder();
        int[] count = new int[10];
        
        Calc(count, dictionary, 2, 'w', "two");
        Calc(count, dictionary, 4, 'u', "four");
        Calc(count, dictionary, 6, 'x', "six");
        Calc(count, dictionary, 8, 'g', "eight");
        
        Calc(count, dictionary, 3, 'h', "three");
        Calc(count, dictionary, 5, 'f', "five");
        Calc(count, dictionary, 7, 's', "seven");

        Calc(count, dictionary, 0, 'r', "zero");
        Calc(count, dictionary, 9, 'i', "nine");

        Calc(count, dictionary, 1, 'n', "one");

        for (int i = 0; i < 10; ++i)
        {
            builder.Append((char)('0' + i), count[i]);
        }
        return builder.ToString();
    }

    public void Calc(
        int[] count,
        Dictionary<char, int> dict,
        int target,
        char targetChar,
        string word)
    {
        count[target] = dict[targetChar];
        for (int i = 0; i < word.Length; ++i)
        {
            dict[word[i]] -= count[target];
        }
    }
} 
*/
