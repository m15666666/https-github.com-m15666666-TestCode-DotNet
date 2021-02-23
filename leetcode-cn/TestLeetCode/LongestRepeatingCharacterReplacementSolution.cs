using System;

/*
给你一个仅由大写英文字母组成的字符串，你可以将任意位置上的字符替换成另外的字符，总共可最多替换 k 次。在执行上述操作后，找到包含重复字母的最长子串的长度。

注意:
字符串长度 和 k 不会超过 104。

示例 1:

输入:
s = "ABAB", k = 2

输出:
4

解释:
用两个'A'替换为两个'B',反之亦然。
示例 2:

输入:
s = "AABABBA", k = 1

输出:
4

解释:
将中间的一个'A'替换为'B',字符串变为 "AABBBBA"。
子串 "BBBB" 有最长重复字母, 答案为 4。
*/

/// <summary>
/// https://leetcode-cn.com/problems/longest-repeating-character-replacement/
/// 424. 替换后的最长重复字符
/// https://www.cnblogs.com/kexinxin/p/10242222.html
/// </summary>
internal class LongestRepeatingCharacterReplacementSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int CharacterReplacement(string s, int k)
    {
        const char A = 'A';
        int[] num = new int[26];
        int n = s.Length;
        int maxCount = 0; // 动态窗口范围内，曾经出现过的单一字符最多次数
        int left = 0, right = 0;
        while (right < n)
        {
            var rightIndex = s[right] - A;
            num[rightIndex]++;
            maxCount = Math.Max(maxCount, num[rightIndex]);
            if (k + maxCount < right - left + 1) num[s[left++] - A]--;

            right++;
        }
        return right - left;
    }

    //public int CharacterReplacement(string s, int k)
    //{
    //    if (string.IsNullOrWhiteSpace(s)) return 0;
    //    int start = 0;
    //    int end = 0;
    //    int res = 0;
    //    int[] charNum = new int[26];
    //    charNum[s[0] - 'A']++;
    //    while (end < s.Length)
    //    {
    //        int maxChar = 0;
    //        for (int i = 0; i < 26; i++)
    //        {
    //            if (charNum[i] > maxChar)
    //                maxChar = charNum[i];
    //        }
    //        if (maxChar + k > end - start)
    //        {
    //            end++;
    //            if (end < s.Length)
    //                charNum[s[end] - 'A']++;
    //        }
    //        else
    //        {
    //            charNum[s[start] - 'A']--;
    //            start++;
    //        }
    //        if (maxChar + k > res)
    //            res = maxChar + k <= s.Length ? maxChar + k : s.Length;
    //    }
    //    return res;
    //}
}

/*
替换后的最长重复字符
力扣官方题解
发布于 2021-02-01
27.6k
方法一：双指针
我们可以枚举字符串中的每一个位置作为右端点，然后找到其最远的左端点的位置，满足该区间内除了出现次数最多的那一类字符之外，剩余的字符（即非最长重复字符）数量不超过 kk 个。

这样我们可以想到使用双指针维护这些区间，每次右指针右移，如果区间仍然满足条件，那么左指针不移动，否则左指针至多右移一格，保证区间长度不减小。

虽然这样的操作会导致部分区间不符合条件，即该区间内非最长重复字符超过了 kk 个。但是这样的区间也同样不可能对答案产生贡献。当我们右指针移动到尽头，左右指针对应的区间的长度必然对应一个长度最大的符合条件的区间。

实际代码中，由于字符串中仅包含大写字母，我们可以使用一个长度为 2626 的数组维护每一个字符的出现次数。每次区间右移，我们更新右移位置的字符出现的次数，然后尝试用它更新重复字符出现次数的历史最大值，最后我们使用该最大值计算出区间内非最长重复字符的数量，以此判断左指针是否需要右移即可。

代码


class Solution {
    public int characterReplacement(String s, int k) {
        int[] num = new int[26];
        int n = s.length();
        int maxn = 0;
        int left = 0, right = 0;
        while (right < n) {
            num[s.charAt(right) - 'A']++;
            maxn = Math.max(maxn, num[s.charAt(right) - 'A']);
            if (right - left + 1 - maxn > k) {
                num[s.charAt(left) - 'A']--;
                left++;
            }
            right++;
        }
        return right - left;
    }
}
时间复杂度

时间复杂度：O(n)O(n)，其中 nn 是字符串的长度。我们至多只需要遍历该字符串一次。

空间复杂度：O(|\Sigma|)O(∣Σ∣)，其中 |\Sigma|∣Σ∣ 是字符集的大小。我们需要存储每个大写英文字母的出现次数。

public class Solution {
    public int CharacterReplacement(string s, int k) {
        int n = s.Length;
        int maxn = 0;
        int left = 0;
        int right = 0;
        int[] num = new int[26];
        while(right < n) {
            num[s[right] - 'A']++;
            maxn = Max(maxn, num[s[right] - 'A']);
            if (right - left + 1 - maxn > k) {
                num[s[left]-'A']--;
                left++;
            }
            right++;
        }
        return right - left;
    }
    public int Max(int n1, int n2) {
        if (n1 > n2) {
            return n1;
        } 
        return n2;
    }
}
*/