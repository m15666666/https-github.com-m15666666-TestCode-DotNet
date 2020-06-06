/*
给定三个字符串 s1, s2, s3, 验证 s3 是否是由 s1 和 s2 交错组成的。

示例 1:

输入: s1 = "aabcc", s2 = "dbbca", s3 = "aadbbcbcac"
输出: true
示例 2:

输入: s1 = "aabcc", s2 = "dbbca", s3 = "aadbbbaccc"
输出: false

*/

/// <summary>
/// https://leetcode-cn.com/problems/interleaving-string/
/// 97. 交错字符串
///
/// </summary>
internal class InterleavingStringSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public bool IsInterleave(string s1, string s2, string s3)
    {
        int len1 = s1.Length;
        int len2 = s2.Length;
        if (s3.Length != len1 + len2) return false;

        bool[] dp = new bool[len2 + 1];
        dp[0] = true;
        for (int j = 1; j <= len2; j++)
            dp[j] = dp[j - 1] && s2[j - 1] == s3[j - 1];

        for (int i = 1; i <= len1; i++)
        {
            dp[0] = dp[0] && s1[i - 1] == s3[i - 1];
            for (int j = 1; j <= len2; j++)
            {
                var c = s3[i + j - 1];
                dp[j] = (dp[j] && s1[i - 1] == c) || (dp[j - 1] && s2[j - 1] == c);
            }
        }
        return dp[len2];
    }
}

/*

交错字符串
力扣 (LeetCode)
发布于 2019-06-09
13.9k
方法 1：暴力
最基本的想法就就是找到所有 s1s1 和 s2s2 能够形成的交错字符串。为了实现这样的想法，我们使用回溯。我们首先将 s1s1 中的第一个字符作为开始字符，然后将 s1s1 字符串剩余部分和 s2s2 字符串所有可能情况添加在这个字符后面，每次用完所有字符后检查字符串与 s3s3 是否一致。类似的，我们可以选择 s2s2 第一个字符作为开始字符，然后将 s2s2 剩余字符串和 s1s1 字符串回溯地添加到该字符后面，看是否能形成 s3s3 。

为了实现回溯函数，我们回溯地调用函数 is\_Interleave(s1,i+1,s2,j,res+s1.charAt(i),s3)is_Interleave(s1,i+1,s2,j,res+s1.charAt(i),s3) ，表示我们选择了 s1s1 当前字符作为下一个字符，然后又调用函数 is\_Interleave(s1,i,s2,j+1,res+s2.charAt(j),s3)is_Interleave(s1,i,s2,j+1,res+s2.charAt(j),s3) ，这表示选择了 s2s2 的当前字符作为下一个字符。这里， resres 表示 s1s1 和 s2s2 已经添加到结果字符串里的部分。如果某一次调用，返回的结果是 TrueTrue ，这表示至少有一个交错字符串是符合要求 s3s3 的。当所有情况都被考虑后，过程结束并返回 FalseFalse 。

让我们来看一个小例子：


s1="abc"
s2="bcd"
s3="abcbdc"
首先，我们选择 s1s1 字符串的第一个字母 'a' ，所以递归进去后， s1s1 变为 "bc" ， s2s2 保持不变为 "bcd" ，s3s3 为 "abcbdc" 。当函数返回结果的时候，我们再次调用递归函数，但是这次 3 个字符串分别变为 s1s1="abc" ， s2s2="cd" ，s3s3="abcbdc" 。


public class Solution {
    public boolean is_Interleave(String s1,int i,String s2,int j,String res,String s3)
    {
        if(res.equals(s3) && i==s1.length() && j==s2.length())
            return true;
        boolean ans=false;
        if(i<s1.length())
            ans|=is_Interleave(s1,i+1,s2,j,res+s1.charAt(i),s3);
        if(j<s2.length())
            ans|=is_Interleave(s1,i,s2,j+1,res+s2.charAt(j),s3);
        return ans;

    }
    public boolean isInterleave(String s1, String s2, String s3) {
        return is_Interleave(s1,0,s2,0,"",s3);
    }
}
复杂度分析

时间复杂度：O(2^{m+n})O(2 
m+n
 ) 。 mm 是 s1s1 的长度， nn 是 s2s2 的长度。

空间复杂度：O(m+n)O(m+n) 。递归栈的深度最多为 m+nm+n 。



方法 2：记忆化回溯
算法

在上面提到的回溯方法中，我们只考虑了两个字符串的所有可能交错字符串情况。但有可能 s1s1 和 s2s2 的相同部分在不同顺序处理情况下，已经被计算过了。不管处理的顺序是如何的，如果已经产生的字符串与要求字符串 s3s3 是匹配的，那么剩余的结果字符串只与 s1s1 和 s2s2 剩余的部分有关系，而与之前已处理过的部分没有关系了，所以回溯方法导致了冗余的计算。

这种冗余可以通过在回溯的过程中使用记忆化去除。为了达到这个目的，我们维护 3 个指针 i, j, ki,j,k ，分别指向 s1, s2, s3s1,s2,s3 当前位置。同时，我们维护一个 2D 的记忆数组，记录目前已经处理过的子字符串。 memo[i][j]memo[i][j] 保存的值是 1/0 或者 -1 ，取决于状态，也就是 s1s1 下标为 i^{th}i 
th
  且 s2s2 下标为 j^{th}j 
th
  是否已经被处理过。与方法 1 类似，我们通过判断 s1s1 的当前字符（通过指针 ii 表示）与 s3s3 的当前字符（通过指针 kk 来表示），如果相等，我们可以将它放到暂存的结果串中，并同样递归调用函数：

is\_Interleave(s1, i+1, s2, j, s3, k+1,memo)
is_Interleave(s1,i+1,s2,j,s3,k+1,memo)

所以，我们要增加 ii 和 kk ，因为这两个指针之前的字符串都已经被处理过了。类似的，我们从第二个字符串选择当前字符，然后继续回溯调用。回溯函数停止的条件是 s1s1 或者 s2s2 有一个已经被完全处理完了。比方说， s1s1 处理完时，我们直接将 s2s2剩余部分和 s3s3 剩余部分进行比较。当从当前回溯调用返回时，我们用记忆化数组保存返回的结果，以便下次再遇到相同情况时，回溯函数不会被调用，而直接返回记忆化数组里的值。


public class Solution {
   public boolean is_Interleave(String s1, int i, String s2, int j, String s3, int k, int[][] memo) {
       if (i == s1.length()) {
           return s2.substring(j).equals(s3.substring(k));
       }
       if (j == s2.length()) {
           return s1.substring(i).equals(s3.substring(k));
       }
       if (memo[i][j] >= 0) {
           return memo[i][j] == 1 ? true : false;
       }
       boolean ans = false;
       if (s3.charAt(k) == s1.charAt(i) && is_Interleave(s1, i + 1, s2, j, s3, k + 1, memo)
               || s3.charAt(k) == s2.charAt(j) && is_Interleave(s1, i, s2, j + 1, s3, k + 1, memo)) {
           ans = true;
       }
       memo[i][j] = ans ? 1 : 0;
       return ans;
   }
   public boolean isInterleave(String s1, String s2, String s3) {
       int memo[][] = new int[s1.length()][s2.length()];
       for (int i = 0; i < s1.length(); i++) {
           for (int j = 0; j < s2.length(); j++) {
               memo[i][j] = -1;
           }
       }
       return is_Interleave(s1, 0, s2, 0, s3, 0, memo);
   }
}
复杂度分析

时间复杂度：O(2^{m+n})O(2 
m+n
 ) 。mm 是 s1s1 的长度且 nn 是s2s2 的长度。

空间复杂度：O(m * n)O(m∗n) 。记忆化数组需要 m * nm∗n 的空间。



方法 3：使用二维动态规划
算法

上面提到的回溯方法包含每次从 s1s1 或者 s2s2 中选择一个字符并调用递归函数去检查 s1s1 和 s2s2 剩余部分能否形成 s3s3 剩余部分的交错字符串。在现在这种方法中，我们用另一种思路来考虑同样的问题。这里我们考虑用 s1s1 和 s2s2 的某个前缀是否能形成 s3s3 的一个前缀。

这个方法的前提建立于：判断一个 s3s3 的前缀（用下标 kk 表示），能否用 s1s1 和 s2s2 的前缀（下标分别为 ii 和 jj），仅仅依赖于 s1s1 前 ii 个字符和 s2s2 前 jj 个字符，而与后面的字符无关。

为了实现这个算法， 我们将使用一个 2D 的布尔数组 dpdp 。dp[i][j]dp[i][j] 表示用 s1s1 的前 (i+1)(i+1) 和 s2s2 的前 (j+1)(j+1) 个字符，总共 (i+j+2)(i+j+2) 个字符，是否交错构成 s3s3 的前缀。为了求出 dp[i][j]dp[i][j] ，我们需要考虑 2 种情况：

s1s1 的第 ii 个字符和 s2s2 的第 jj 个字符都不能匹配 s3s3 的第 kk 个字符，其中 k=i+j+1k=i+j+1 。这种情况下，s1s1 和 s2s2 的前缀无法交错形成 s3s3 长度为 k+1k+1 的前缀。因此，我们让 dp[i][j]dp[i][j] 为 FalseFalse。

s1s1 的第 ii 个字符或者 s2s2 的第 jj 个字符可以匹配 s3s3 的第 kk 个字符，其中 k=i+j+1k=i+j+1 。假设匹配的字符是 xx 且与 s1s1 的第 ii 个字符匹配，我们就需要把 xx 放在已经形成的交错字符串的最后一个位置。此时，为了我们必须确保 s1s1 的前 (i-1)(i−1) 个字符和 s2s2 的前 jj 个字符能形成 s3s3 的一个前缀。类似的，如果我们将 s2s2 的第 jj个字符与 s3s3 的第 kk 个字符匹配，我们需要确保 s1s1 的前 ii 个字符和 s2s2 的前 (j-1)(j−1) 个字符能形成 s3s3 的一个前缀，我们就让 dp[i][j]dp[i][j] 为 TrueTrue 。

可以用下面的例子进行说明：


s1="aabcc"
s2="dbbca"
s3="aadbbcbcac"



public class Solution {
    public boolean isInterleave(String s1, String s2, String s3) {
        if (s3.length() != s1.length() + s2.length()) {
            return false;
        }
        boolean dp[][] = new boolean[s1.length() + 1][s2.length() + 1];
        for (int i = 0; i <= s1.length(); i++) {
            for (int j = 0; j <= s2.length(); j++) {
                if (i == 0 && j == 0) {
                    dp[i][j] = true;
                } else if (i == 0) {
                    dp[i][j] = dp[i][j - 1] && s2.charAt(j - 1) == s3.charAt(i + j - 1);
                } else if (j == 0) {
                    dp[i][j] = dp[i - 1][j] && s1.charAt(i - 1) == s3.charAt(i + j - 1);
                } else {
                    dp[i][j] = (dp[i - 1][j] && s1.charAt(i - 1) == s3.charAt(i + j - 1)) || (dp[i][j - 1] && s2.charAt(j - 1) == s3.charAt(i + j - 1));
                }
            }
        }
        return dp[s1.length()][s2.length()];
    }
}
复杂度分析

时间复杂度：O(m \cdot n)O(m⋅n) 。计算 dpdp 数组需要 m*nm∗n 的时间。

空间复杂度：O(m \cdot n)O(m⋅n)。2 维的 dpdp 数组需要 (m+1)*(n+1)(m+1)∗(n+1) 的空间。 mm 和 nn 分别是 s1s1 和 s2s2 字符串的长度。



方法 4：使用一维动态规划
算法

这种方法与前一种方法基本一致，除了我们仅使用一维 dpdp 数组去储存前缀结果。我们利用 dp[i-1]dp[i−1] 的结果和 dp[i]dp[i] 之前的结果来计算 dp[i]dp[i] ，即滚动数组。


public class Solution {
   public boolean isInterleave(String s1, String s2, String s3) {
       if (s3.length() != s1.length() + s2.length()) {
           return false;
       }
       boolean dp[] = new boolean[s2.length() + 1];
       for (int i = 0; i <= s1.length(); i++) {
           for (int j = 0; j <= s2.length(); j++) {
               if (i == 0 && j == 0) {
                   dp[j] = true;
               } else if (i == 0) {
                   dp[j] = dp[j - 1] && s2.charAt(j - 1) == s3.charAt(i + j - 1);
               } else if (j == 0) {
                   dp[j] = dp[j] && s1.charAt(i - 1) == s3.charAt(i + j - 1);
               } else {
                   dp[j] = (dp[j] && s1.charAt(i - 1) == s3.charAt(i + j - 1)) || (dp[j - 1] && s2.charAt(j - 1) == s3.charAt(i + j - 1));
               }
           }
       }
       return dp[s2.length()];
   }
}
复杂度分析

时间复杂度：O(m \cdot n)O(m⋅n)；长度为 nn 的 dpdp 数组需要被填充 mm 次。

空间复杂度：O(n)O(n)；nn 是字符串 s1s1 的长度

public class Solution {
    public bool IsInterleave(string s1, string s2, string s3)
    {
        if((s1.Length  + s2.Length) != s3.Length)
            return false;
        bool[,] dp = new bool[s1.Length + 1, s2.Length + 1];

        for(int i = 0; i <= s1.Length; i++)
        {
            for(int j = 0; j <= s2.Length; j++)
            {
                if(i == 0 && j == 0)
                    dp[i, j] = true;
                else if(j == 0)
                    dp[i, j] = dp[i - 1, j] && s1[i - 1] == s3[i+ j - 1];
                else if(i == 0)
                    dp[i, j] = dp[i, j - 1] && s2[j - 1] == s3[i+ j - 1];
                else
                    dp[i, j] = (dp[i - 1, j] && s1[i - 1] == s3[i+ j - 1]) || dp[i, j - 1] && s2[j - 1] == s3[i+ j - 1];
            }
        }
        return dp[s1.Length, s2.Length];
    }
}


*/
