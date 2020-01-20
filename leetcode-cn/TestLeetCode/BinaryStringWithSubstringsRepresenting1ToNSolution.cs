using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个二进制字符串 S（一个仅由若干 '0' 和 '1' 构成的字符串）和一个正整数 N，如果对于从 1 到 N 的每个整数 X，其二进制表示都是 S 的子串，就返回 true，否则返回 false。

 

示例 1：

输入：S = "0110", N = 3
输出：true
示例 2：

输入：S = "0110", N = 4
输出：false
 

提示：

1 <= S.length <= 1000
1 <= N <= 10^9
*/
/// <summary>
/// https://leetcode-cn.com/problems/binary-string-with-substrings-representing-1-to-n/
/// 1016. 子串能表示从 1 到 N 数字的二进制串
/// 
/// </summary>
class BinaryStringWithSubstringsRepresenting1ToNSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public bool QueryString(string S, int N)
    {
        for (int i = 1; i <= N; i++)
        {
            if (!S.Contains(Convert.ToString(i, 2))) return false;
        }
        return true;
    }
}
/*
class Solution {
public: 
    int bitLen(int n) {
        int res = 0;
        while (n > 0) {
            ++res;
            n >>= 1;
        }
        return max(res, 1);
    }
    bool queryString(string S, int N) {
        if (N == 0) return true;
        int k = bitLen(N);
        if (k > S.size()) return false;
        long h = 0;
        for (int i = 0; i < k; ++i) {
            h <<= 1;
            h |= S[i] - '0';
        }
        if (h == N) return queryString(S, N - 1);
        for (int i = k; i < S.size(); ++i) {
            h <<= 1;
            h |= S[i] - '0';
            h &= ~((S[i - k] - '0') << k);
            if (h == N) return queryString(S, N - 1);
        }
        return false;
    }
};

class Solution {
    public boolean queryString(String S, int N) {
        for(int i=1;i<=N;i++) {
        	if(!S.contains(Integer.toBinaryString(i)))
        		return false;
        }
        return true;
    }
}
 
*/
