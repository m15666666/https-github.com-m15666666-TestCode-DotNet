using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
我们给出两个单词数组 A 和 B。每个单词都是一串小写字母。

现在，如果 b 中的每个字母都出现在 a 中，包括重复出现的字母，那么称单词 b 是单词 a 的子集。 例如，“wrr” 是 “warrior” 的子集，但不是 “world” 的子集。

如果对 B 中的每一个单词 b，b 都是 a 的子集，那么我们称 A 中的单词 a 是通用的。

你可以按任意顺序以列表形式返回 A 中所有的通用单词。

 

示例 1：

输入：A = ["amazon","apple","facebook","google","leetcode"], B = ["e","o"]
输出：["facebook","google","leetcode"]
示例 2：

输入：A = ["amazon","apple","facebook","google","leetcode"], B = ["l","e"]
输出：["apple","google","leetcode"]
示例 3：

输入：A = ["amazon","apple","facebook","google","leetcode"], B = ["e","oo"]
输出：["facebook","google"]
示例 4：

输入：A = ["amazon","apple","facebook","google","leetcode"], B = ["lo","eo"]
输出：["google","leetcode"]
示例 5：

输入：A = ["amazon","apple","facebook","google","leetcode"], B = ["ec","oc","ceo"]
输出：["facebook","leetcode"]
 

提示：

1 <= A.length, B.length <= 10000
1 <= A[i].length, B[i].length <= 10
A[i] 和 B[i] 只由小写字母组成。
A[i] 中所有的单词都是独一无二的，也就是说不存在 i != j 使得 A[i] == A[j]。
*/
/// <summary>
/// https://leetcode-cn.com/problems/word-subsets/
/// 916. 单词子集
/// 
/// </summary>
class WordSubsetsSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public IList<string> WordSubsets(string[] A, string[] B)
    {
        const int CharacterCount = 26;
        int[] bmax = new int[CharacterCount];
        Array.Fill(bmax, 0);
        int[] bCount = new int[CharacterCount];
        foreach (string b in B)
        {
            Count(b, bCount);
            for (int i = 0; i < CharacterCount; ++i)
                if(bmax[i] < bCount[i]) bmax[i] = bCount[i];
        }

        int[] aCount = bCount;
        var ret = new List<string>();
        foreach (string a in A)
        {
            Count(a, aCount);
            bool match = true;
            for (int i = 0; i < CharacterCount; ++i)
                if (aCount[i] < bmax[i])
                {
                    match = false;
                    break;
                }

            if(match) ret.Add(a);
        }

        return ret;
    }

    private static void Count(string s, int[] ret)
    {
        Array.Fill(ret, 0);
        foreach (char c in s)
            ret[c - 'a']++;
    }
}
/*
方法 1：将 B 合并成一个单词
想法

如果 b 是 a 的子集，那么就称 a 是 b 的超集。记录 N_{\text{"a"}}(\text{word})N 
"a"
?	
 (word) 是 word 中字母 \text{"a"}"a" 出现次数。

当我们检查 A 中的单词 wordA 是否是 wordB 的超集时，我们只需要单独检验每个字母个数：对于每个字母，有 N_{\text{letter}}(\text{wordA}) \geq N_{\text{letter}}(\text{wordB})N 
letter
?	
 (wordA)≥N 
letter
?	
 (wordB)。

现在，检验单词 wordA 是否是所有 \text{wordB}_iwordB 
i
?	
  的超集，我们需要检验所有 ii 是否满足 N_{\text{letter}}(\text{wordA}) \geq N_{\text{letter}}(\text{wordB}_i)N 
letter
?	
 (wordA)≥N 
letter
?	
 (wordB 
i
?	
 )，等价于检验 N_{\text{letter}}(\text{wordA}) \geq \max\limits_i(N_{\text{letter}}(\text{wordB}_i))N 
letter
?	
 (wordA)≥ 
i
max
?	
 (N 
letter
?	
 (wordB 
i
?	
 ))。

例如，当我们检验 "warrior" 是否是 B = ["wrr", "wa", "or"] 的超集时，我们可以按照字母出现的最多次数将 B 中所有单词合并成一个单词 "arrow"，然后判断一次即可。

算法

将 B 合并成一个单独的单词 bmax，然后比较 A 中的所有单词 a。

JavaPython
class Solution {
    public List<String> wordSubsets(String[] A, String[] B) {
        int[] bmax = count("");
        for (String b: B) {
            int[] bCount = count(b);
            for (int i = 0; i < 26; ++i)
                bmax[i] = Math.max(bmax[i], bCount[i]);
        }

        List<String> ans = new ArrayList();
        search: for (String a: A) {
            int[] aCount = count(a);
            for (int i = 0; i < 26; ++i)
                if (aCount[i] < bmax[i])
                    continue search;
            ans.add(a);
        }

        return ans;
    }

    public int[] count(String S) {
        int[] ans = new int[26];
        for (char c: S.toCharArray())
            ans[c - 'a']++;
        return ans;
    }
}
复杂度分析

时间复杂度：O(A+B)O(A+B)，其中 AA 和 BB 分别是 A 和 B 的单词个数。
空间复杂度：O(A\text{.length} + B\text{.length})O(A.length+B.length)。
 
*/
