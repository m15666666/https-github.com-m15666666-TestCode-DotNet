using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个编码字符串 S。为了找出解码字符串并将其写入磁带，从编码字符串中每次读取一个字符，并采取以下步骤：

如果所读的字符是字母，则将该字母写在磁带上。
如果所读的字符是数字（例如 d），则整个当前磁带总共会被重复写 d-1 次。
现在，对于给定的编码字符串 S 和索引 K，查找并返回解码字符串中的第 K 个字母。

示例 1：

输入：S = "leet2code3", K = 10
输出："o"
解释：
解码后的字符串为 "leetleetcodeleetleetcodeleetleetcode"。
字符串中的第 10 个字母是 "o"。
示例 2：

输入：S = "ha22", K = 5
输出："h"
解释：
解码后的字符串为 "hahahaha"。第 5 个字母是 "h"。
示例 3：

输入：S = "a2345678999999999999999", K = 1
输出："a"
解释：
解码后的字符串为 "a" 重复 8301530446056247680 次。第 1 个字母是 "a"。

提示：

2 <= S.length <= 100
S 只包含小写字母与数字 2 到 9 。
S 以字母开头。
1 <= K <= 10^9
解码后的字符串保证少于 2^63 个字母。
*/
/// <summary>
/// https://leetcode-cn.com/problems/decoded-string-at-index/
/// 880. 索引处的解码字符串
/// 
/// </summary>
class DecodedStringAtIndexSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public string DecodeAtIndex(string S, int K)
    {
        if (string.IsNullOrEmpty(S)) return S;
        if (K == 1) return S[0].ToString();

        long size = 0;
        int len = S.Length;

        // Find length of decoded string
        foreach (var c in S )
        {
            if ('0' <= c && c <= '9') size *= c - '0';
            else size++;
        }

        for (int i = len - 1; i >= 0; --i)
        {
            var c = S[i];
            var isDigit = ('0' <= c && c <= '9');

            K = (int)(K % size);
            if (K == 0 && !isDigit) return c.ToString();

            if (isDigit) size /= (c - '0');
            else size--;
        }
        return string.Empty;
    }
}
/*
方法：逆向工作法
思路

如果我们有一个像 appleappleappleappleappleapple 这样的解码字符串和一个像 K=24 这样的索引，那么如果 K=4，答案是相同的。

一般来说，当解码的字符串等于某个长度为 size 的单词重复某些次数（例如 apple 与 size=5 组合重复6次）时，索引 K 的答案与索引 K % size 的答案相同。

我们可以通过逆向工作，跟踪解码字符串的大小来使用这种洞察力。每当解码的字符串等于某些单词 word 重复 d 次时，我们就可以将 k 减少到 K % (Word.Length)。

算法

首先，找出解码字符串的长度。之后，我们将逆向工作，跟踪 size：解析符号 S[0], S[1], ..., S[i] 后解码字符串的长度。

如果我们看到一个数字 S [i]，则表示在解析 S [0]，S [1]，...，S [i-1] 之后解码字符串的大小将是 size / Integer(S[i])。 否则，将是 size - 1。

C++JavaPython
class Solution {
public:
    string decodeAtIndex(string S, int K) {
        long size = 0;
        int N = S.size();

        // Find size = length of decoded string
        for (int i = 0; i < N; ++i) {
            if (isdigit(S[i]))
                size *= S[i] - '0';
            else
                size++;
        }

        for (int i = N-1; i >=0; --i) {
            K %= size;
            if (K == 0 && isalpha(S[i]))
                return (string) "" + S[i];

            if (isdigit(S[i]))
                size /= S[i] - '0';
            else
                size--;
        }
    }
};
复杂度分析

时间复杂度：O(N)O(N)，其中 NN 是 S 的长度。
空间复杂度：O(1)O(1)。
 
public class Solution {
    public string DecodeAtIndex(string S, int K) {

        long size = 0;
        long k = (long)K;
        int N = S.Length;
        for (int i = 0; i < N; i++)
        {
            if (char.IsDigit(S[i]))
            {
                size *= S[i] - '0';
            }
            else
                size++;
        }
        for (int i = N-1; i >=0; i--)
        {
            k %= size;
            if (k == 0 && !char.IsDigit(S[i]))
                return "" + S[i];

            if (char.IsDigit(S[i]))
                size /= S[i] - '0';
            else
                size--;

        }
        return "";
    }
}
*/
