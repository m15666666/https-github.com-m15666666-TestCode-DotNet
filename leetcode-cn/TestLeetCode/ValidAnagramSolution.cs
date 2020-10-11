/*
给定两个字符串 s 和 t ，编写一个函数来判断 t 是否是 s 的字母异位词。

示例 1:

输入: s = "anagram", t = "nagaram"
输出: true
示例 2:

输入: s = "rat", t = "car"
输出: false
说明:
你可以假设字符串只包含小写字母。

进阶:
如果输入字符串包含 unicode 字符怎么办？你能否调整你的解法来应对这种情况？

*/

using System;
/// <summary>
/// https://leetcode-cn.com/problems/valid-anagram/
/// 242. 有效的字母异位词
///
///
///
///
/// </summary>
internal class ValidAnagramSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public bool IsAnagram(string s, string t)
    {
        if (s.Length != t.Length) return false;

        int[] counter = new int[26];
        Array.Fill(counter, 0);
        for (int i = 0; i < s.Length; i++)
        {
            counter[s[i] - 'a']++;
            counter[t[i] - 'a']--;
        }
        foreach (int count in counter)
            if (count != 0) return false;
        return true;
    }
}

/*
有效的字母异位词
力扣 (LeetCode)
发布于 2019-06-19
48.5k
方法一：排序
算法：
通过将 ss 的字母重新排列成 tt 来生成变位词。因此，如果 TT 是 SS 的变位词，对两个字符串进行排序将产生两个相同的字符串。此外，如果 ss 和 tt 的长度不同，tt 不能是 ss 的变位词，我们可以提前返回。


public boolean isAnagram(String s, String t) {
    if (s.length() != t.length()) {
        return false;
    }
    char[] str1 = s.toCharArray();
    char[] str2 = t.toCharArray();
    Arrays.sort(str1);
    Arrays.sort(str2);
    return Arrays.equals(str1, str2);
}
复杂度分析

时间复杂度：O(n \log n)O(nlogn)，假设 nn 是 ss 的长度，排序成本 O(n\log n)O(nlogn) 和比较两个字符串的成本 O(n)O(n)。排序时间占主导地位，总体时间复杂度为 O(n \log n)O(nlogn)。
空间复杂度：O(1)O(1)，空间取决于排序实现，如果使用 heapsort，通常需要 O(1)O(1) 辅助空间。注意，在 Java 中，toCharArray() 制作了一个字符串的拷贝，所以它花费 O(n)O(n) 额外的空间，但是我们忽略了这一复杂性分析，因为：
这依赖于语言的细节。
这取决于函数的设计方式。例如，可以将函数参数类型更改为 char[]。
方法二：哈希表
算法：

为了检查 tt 是否是 ss 的重新排列，我们可以计算两个字符串中每个字母的出现次数并进行比较。因为 SS 和 TT 都只包含 A-ZA−Z 的字母，所以一个简单的 26 位计数器表就足够了。
我们需要两个计数器数表进行比较吗？实际上不是，因为我们可以用一个计数器表计算 ss 字母的频率，用 tt 减少计数器表中的每个字母的计数器，然后检查计数器是否回到零。

public boolean isAnagram(String s, String t) {
    if (s.length() != t.length()) {
        return false;
    }
    int[] counter = new int[26];
    for (int i = 0; i < s.length(); i++) {
        counter[s.charAt(i) - 'a']++;
        counter[t.charAt(i) - 'a']--;
    }
    for (int count : counter) {
        if (count != 0) {
            return false;
        }
    }
    return true;
}
或者我们可以先用计数器表计算 ss，然后用 tt 减少计数器表中的每个字母的计数器。如果在任何时候计数器低于零，我们知道 tt 包含一个不在 ss 中的额外字母，并立即返回 FALSE。

public boolean isAnagram(String s, String t) {
    if (s.length() != t.length()) {
        return false;
    }
    int[] table = new int[26];
    for (int i = 0; i < s.length(); i++) {
        table[s.charAt(i) - 'a']++;
    }
    for (int i = 0; i < t.length(); i++) {
        table[t.charAt(i) - 'a']--;
        if (table[t.charAt(i) - 'a'] < 0) {
            return false;
        }
    }
    return true;
}
复杂度分析

时间复杂度：O(n)O(n)。时间复杂度为 O(n)O(n) 因为访问计数器表是一个固定的时间操作。
空间复杂度：O(1)O(1)。尽管我们使用了额外的空间，但是空间的复杂性是 O(1)O(1)，因为无论 NN 有多大，表的大小都保持不变。
进阶：
如果输入字符串包含 unicode 字符怎么办？你能否调整你的解法来应对这种情况？

解答：
使用哈希表而不是固定大小的计数器。想象一下，分配一个大的数组来适应整个 Unicode 字符范围，这个范围可能超过 100万。哈希表是一种更通用的解决方案，可以适应任何字符范围。

public class Solution {
    public bool IsAnagram(string s, string t) {
        if (s.Length != t.Length)
            {
                return false;
            }
            int[] nums = new int[26];
            int[] nums2 = new int[26];
            for (int i = 0; i < s.Length; i++)
            {
                nums[s[i] - 'a'] += 1;
                nums2[t[i] - 'a'] += 1;
            }
            for (int i = 0; i < 26; i++)
            {
                if(nums[i]!=nums2[i])
                {
                    return false;
                }
            }
            return true;

    }
}


*/