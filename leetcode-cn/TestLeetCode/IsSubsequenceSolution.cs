/*
给定字符串 s 和 t ，判断 s 是否为 t 的子序列。

你可以认为 s 和 t 中仅包含英文小写字母。字符串 t 可能会很长（长度 ~= 500,000），而 s 是个短字符串（长度 <=100）。

字符串的一个子序列是原始字符串删除一些（也可以不删除）字符而不改变剩余字符相对位置形成的新字符串。（例如，"ace"是"abcde"的一个子序列，而"aec"不是）。

示例 1:
s = "abc", t = "ahbgdc"

返回 true.

示例 2:
s = "axc", t = "ahbgdc"

返回 false.

后续挑战 :

如果有大量输入的 S，称作S1, S2, ... , Sk 其中 k >= 10亿，你需要依次检查它们是否为 T 的子序列。在这种情况下，你会怎样改变代码？
*/

/// <summary>
/// https://leetcode-cn.com/problems/is-subsequence/
/// 392. 判断子序列
/// </summary>
internal class IsSubsequenceSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public bool IsSubsequence(string s, string t)
    {
        int n = s.Length;
        int m = t.Length;

        const int End = -1;
        const char a = 'a';
        const int CharSetLen = 26;
        int[,] dp = new int[m + 1, CharSetLen]; // 第i（从1开始到m）个后面的第一个j（下标0~25）字符的下标(0~m-1)
        for (int i = 0; i < CharSetLen; i++) dp[m, i] = End;

        for (int i = m - 1; -1 < i; i--)
        {
            for (int j = 0; j < CharSetLen; j++) dp[i, j] = dp[i + 1, j];
            dp[i, t[i] - a] = i;
        }

        int charNumber = 0;
        for (int i = 0; i < n; i++)
        {
            var findIndex = dp[charNumber, s[i] - a];
            if (findIndex == End) return false;
            charNumber = findIndex + 1;
        }
        return true;
    }

    //public bool IsSubsequence(string s, string t)
    //{
    //    if (string.IsNullOrWhiteSpace(s)) return true;
    //    if (string.IsNullOrWhiteSpace(t) || t.Length < s.Length) return false;

    //    int sIndex = 0;

    //    var sChar = s[sIndex++];
    //    foreach( var c in t )
    //    {
    //        if (c != sChar) continue;

    //        if (sIndex < s.Length) sChar = s[sIndex++];
    //        else return true;
    //    }
    //    return false;
    //}
}

/*
判断子序列
力扣官方题解
发布于 2020-07-25
40.6k
📺 视频题解

📖 文字题解
方法一：双指针
思路及算法

本题询问的是，ss 是否是 tt 的子序列，因此只要能找到任意一种 ss 在 tt 中出现的方式，即可认为 ss 是 tt 的子序列。

而当我们从前往后匹配，可以发现每次贪心地匹配靠前的字符是最优决策。

假定当前需要匹配字符 cc，而字符 cc 在 tt 中的位置 x_1x 
1
​	
  和 x_2x 
2
​	
  出现（x_1 < x_2x 
1
​	
 <x 
2
​	
 ），那么贪心取 x_1x 
1
​	
  是最优解，因为 x_2x 
2
​	
  后面能取到的字符，x_1x 
1
​	
  也都能取到，并且通过 x_1x 
1
​	
  与 x_2x 
2
​	
  之间的可选字符，更有希望能匹配成功。

这样，我们初始化两个指针 ii 和 jj，分别指向 ss 和 tt 的初始位置。每次贪心地匹配，匹配成功则 ii 和 jj 同时右移，匹配 ss 的下一个位置，匹配失败则 jj 右移，ii 不变，尝试用 tt 的下一个字符匹配 ss。

最终如果 ii 移动到 ss 的末尾，就说明 ss 是 tt 的子序列。

代码


class Solution {
    public boolean isSubsequence(String s, String t) {
        int n = s.length(), m = t.length();
        int i = 0, j = 0;
        while (i < n && j < m) {
            if (s.charAt(i) == t.charAt(j)) {
                i++;
            }
            j++;
        }
        return i == n;
    }
}
复杂度分析

时间复杂度：O(n+m)O(n+m)，其中 nn 为 ss 的长度，mm 为 tt 的长度。每次无论是匹配成功还是失败，都有至少一个指针发生右移，两指针能够位移的总距离为 n+mn+m。

空间复杂度：O(1)O(1)。

方法二：动态规划
思路及算法

考虑前面的双指针的做法，我们注意到我们有大量的时间用于在 tt 中找到下一个匹配字符。

这样我们可以预处理出对于 tt 的每一个位置，从该位置开始往后每一个字符第一次出现的位置。

我们可以使用动态规划的方法实现预处理，令 f[i][j]f[i][j] 表示字符串 tt 中从位置 ii 开始往后字符 jj 第一次出现的位置。在进行状态转移时，如果 tt 中位置 ii 的字符就是 jj，那么 f[i][j]=if[i][j]=i，否则 jj 出现在位置 i+1i+1 开始往后，即 f[i][j]=f[i+1][j]f[i][j]=f[i+1][j]，因此我们要倒过来进行动态规划，从后往前枚举 ii。

这样我们可以写出状态转移方程：

f[i][j]=\begin{cases} i, & t[i]=j\\ f[i+1][j], & t[i] \neq j \end{cases}
f[i][j]={ 
i,
f[i+1][j],
​	
  
t[i]=j
t[i] 

​	
 =j
​	
 

假定下标从 00 开始，那么 f[i][j]f[i][j] 中有 0 \leq i \leq m-10≤i≤m−1 ，对于边界状态 f[m-1][..]f[m−1][..]，我们置 f[m][..]f[m][..] 为 mm，让 f[m-1][..]f[m−1][..] 正常进行转移。这样如果 f[i][j]=mf[i][j]=m，则表示从位置 ii 开始往后不存在字符 jj。

这样，我们可以利用 ff 数组，每次 O(1)O(1) 地跳转到下一个位置，直到位置变为 mm 或 ss 中的每一个字符都匹配成功。

同时我们注意到，该解法中对 tt 的处理与 ss 无关，且预处理完成后，可以利用预处理数组的信息，线性地算出任意一个字符串 ss 是否为 tt 的子串。这样我们就可以解决「后续挑战」啦。

代码


class Solution {
    public boolean isSubsequence(String s, String t) {
        int n = s.length(), m = t.length();

        int[][] f = new int[m + 1][26];
        for (int i = 0; i < 26; i++) {
            f[m][i] = m;
        }

        for (int i = m - 1; i >= 0; i--) {
            for (int j = 0; j < 26; j++) {
                if (t.charAt(i) == j + 'a')
                    f[i][j] = i;
                else
                    f[i][j] = f[i + 1][j];
            }
        }
        int add = 0;
        for (int i = 0; i < n; i++) {
            if (f[add][s.charAt(i) - 'a'] == m) {
                return false;
            }
            add = f[add][s.charAt(i) - 'a'] + 1;
        }
        return true;
    }
}
复杂度分析

时间复杂度：O(m \times |\Sigma| + n)O(m×∣Σ∣+n)，其中 nn 为 ss 的长度，mm 为 tt 的长度，\SigmaΣ 为字符集，在本题中字符串只包含小写字母，|\Sigma| = 26∣Σ∣=26。预处理时间复杂度 O(m)O(m)，判断子序列时间复杂度 O(n)O(n)。

如果是计算 kk 个平均长度为 nn 的字符串是否为 tt 的子序列，则时间复杂度为 O(m \times |\Sigma| +k \times n)O(m×∣Σ∣+k×n)。
空间复杂度：O(m \times |\Sigma|)O(m×∣Σ∣)，为动态规划数组的开销。

完整实现
C++

class Solution {
public:
	bool isSubsequence(string s, string t) {
		t.insert(t.begin(), ' ');
		int len1 = s.size(), len2 = t.size();
		
		vector<vector<int> > dp(len2 , vector<int>(26, 0));

		for (char c = 'a'; c <= 'z'; c++) {
			int nextPos = -1; //表示接下来再不会出现该字符

			for (int i = len2 - 1; i>= 0; i--) {  //为了获得下一个字符的位置，要从后往前
				dp[i][c - 'a'] = nextPos;
				if (t[i] == c)
					nextPos = i;
			}
		}

		int index = 0;
		for (char c : s) {
			index = dp[index][c - 'a'];
			if (index == -1)
				return false;
		}
		return true;

	}
};

public class Solution {
    public bool IsSubsequence(string s, string t) {
        int n = t.Length;
        int m = s.Length;
        bool[][] dp = new bool[n + 1][];
        for(int i = 0; i <= n; i ++){
            dp[i] = new bool[m + 1];
            dp[i][0] = true;
        }
        
        for(int i = 1; i <= n; i ++){
            for(int j = 1; j <= m; j ++){
                if(t[i - 1] == s[j - 1]) dp[i][j] |= dp[i-1][j - 1];
                dp[i][j] |= dp[i - 1][j];
            }
        }
        return dp[n][m];
    }
}

public class Solution {
    public bool IsSubsequence(string s, string t) {
        int p = 0;
        int q = 0;

        for (p = 0; p < s.Length; p ++) {
            bool found = false;
            for (int j = q; j < t.Length; j ++, q ++) {
                if (t[q] == s[p]) {
                    found = true;
                    q ++;
                    break;
                }
            }

            if (found == false) {
                return false;
            }
        }

        return true;
    }
}
public class Solution {
    public bool IsSubsequence(string s, string t) {
        int si=0;
        int ti=0;
        while(si!=s.Length&&ti!=t.Length)
        {
            if(s[si]==t[ti]){si++;ti++;}
            else{ti++;}
        }
        if(si==s.Length)return true;
        else return false;
    }
}
public class Solution {
    public bool IsSubsequence(string s, string t) {
        int i1=0;
        int i2=0;
        while(i1<s.Length&&i2<t.Length){
            if(s[i1]==t[i2]){
                i1++;
                i2++;
            }else{
                i2++;
            }
        }
        return i1==s.Length;
    }
}
public class Solution {
    public bool IsSubsequence(string s, string t) {
        int index =0 ;
        for(int i = 0 ; i < t.Length && index<s.Length ; i++){
            if(s[index] == t[i]){
                index++;
            }
        }
        if(index == s.Length){
            return true;
        }else
        {
            return false;
        }
    }
}

    using System.Text.RegularExpressions;
public class Solution {
    public bool IsSubsequence(string s, string t) {
        foreach(char c in s)
            {
                int index = 0;
                index = t.IndexOf(c);
                if (index < 0)
                    return false;
                else
                    t = t.Substring(index+1, t.Length - (index+1));
            }
            return true;
    }
}
public class Solution {
    public bool IsSubsequence(string s, string t) {
        if(s == null || s.Length == 0)
        {
            return true;
        }
        if(t == null || t.Length == 0)
        {
            return false;
        }
        int i = 0,tmp = 0;
        for(i = 0;i < t.Length;i ++)
        {
            if(t[i] == s[tmp])
            {
                tmp ++;
                if(tmp == s.Length)
                {
                    return true;
                }
            }
        }
        return false;
    }
}

public class Solution {
    public bool IsSubsequence(string s, string t)
    {
        if (s.Length == 0)
            return true;
        int SIndex = 0;
        for (int i = 0; i < t.Length; i++)
        {
            if (t[i] == s[SIndex])
                SIndex++;
            if (SIndex >= s.Length)
                return true;
        }
        return false;
    }
}
*/