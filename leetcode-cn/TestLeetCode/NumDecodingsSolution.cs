using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
一条包含字母 A-Z 的消息通过以下方式进行了编码：

'A' -> 1
'B' -> 2
...
'Z' -> 26
给定一个只包含数字的非空字符串，请计算解码方法的总数。

示例 1:

输入: "12"
输出: 2
解释: 它可以解码为 "AB"（1 2）或者 "L"（12）。
示例 2:

输入: "226"
输出: 3
解释: 它可以解码为 "BZ" (2 26), "VF" (22 6), 或者 "BBF" (2 2 6) 。
 
*/
/// <summary>
/// https://leetcode-cn.com/problems/decode-ways/
/// 91.解码方法
/// 一条包含字母 A-Z 的消息通过以下方式进行了编码：
/// 'A' -> 1
/// 'B' -> 2
/// ...
/// 'Z' -> 26
/// 给定一个只包含数字的非空字符串，请计算解码方法的总数。
/// https://blog.csdn.net/zrh_CSDN/article/details/82495250
/// </summary>
class NumDecodingsSolution
{
    public void Test()
    {
        var ret = NumDecodings("17");
    }


    public int NumDecodings(string s) {
        if (string.IsNullOrWhiteSpace(s)) return 0;
        int len = s.Length;
        const char Zero = '0';
        char last1 = s[len-1];
        int last1Possibilities = last1 == Zero ? 0 : 1;
        int last2Possibilities = 1;
        int p;
        for( int i = len - 2; -1 < i; i-- )
        {
            var c = s[i];
            if(c == Zero)
            {
                last2Possibilities = last1Possibilities;
                last1Possibilities = 0;
                continue;
            }
            p = last1Possibilities;
            if (c == '1' || c == '2' && last1 < '7') p += last2Possibilities;
            last1 = c;
            last2Possibilities = last1Possibilities;
            last1Possibilities = p;
        }
        return last1Possibilities;

    }
    //public int NumDecodings(string s)
    //{
    //    if (string.IsNullOrWhiteSpace(s)) return 0;

    //    char? last1 = null;
    //    int last1Possibilities = 1;
    //    int last2Possibilities = 0;

    //    foreach( var c in s)
    //    {
    //        var p = c == '0' ? 0 : last1Possibilities;
    //        if( last1 != null && (last1 == '1' || (last1 == '2' && c < '7') ))
    //        {
    //            p += last2Possibilities;
    //        }

    //        last1 = c;
    //        last2Possibilities = last1Possibilities;
    //        last1Possibilities = p;
    //    }

    //    return last1Possibilities;
    //}
}
/*

Java 简单dp 时间击败100%
jiafeilee
发布于 5 天前
412
这是一道很典型的动态规划的题目，且这样的有一类题型，写这道题前刚好最近就做过类似的题 恢复数组，写完的朋友可以去试试这题。

dp过程可以从左往右，也可以从右往左，这里我写的是从右往左。

dp[i] += dp[j] ,j>i dp[i] 表示从第i+1个数到第n个数的所有方案数

第一个值的考虑的是只有26种状态，且没有哪种状态是0字符开头的，只有10, 20是以0结尾的，所以当我们遇到0开头的情况就可以跳过该状态的记录。
中间的循环遍历由于记录的num值最大只有26，即两位，所以我们可以添加判断条件以缩短内层循环次数。j-i<2
最多只有两次内层循环，根据记录的num值来判断是否超过了上限26，没有就能够进行状态转移。
class Solution {
    public int numDecodings(String s) {
        char[] nums = s.toCharArray();
        int len = nums.length;
        int[] dp = new int[len+1];  // dp[i] 表示从第i+1个数到第n个数的所有方案数
        dp[len] = 1;
        // 从右往左
        for(int i = len-1; i >= 0; i--) {
            // 注意判断0字符
            if (nums[i] == '0') continue;   // 当开始位为0字符时不满足任意一个字母的解析，跳过
            int num = 0;
            for (int j = i; j < len && j-i<2; j++) {
                num = num*10 + (nums[j]-'0');
                // 对子状态dp[j+1]为0开头的也可进行添加，因为没有赋值为dp[j+1]为0
                if (num <= 26) dp[i] += dp[j+1];
            }
        }
        return dp[0];
    }
}
时间复杂度 O (n)， 因为内层循环能控制到不超过2次
空间复杂度 O (n)



C++ 我认为很简单直观的解法
Iris_bupt
发布于 9 个月前
23.6k
算法分析
image.png

源码
int numDecodings(string s) {
    if (s[0] == '0') return 0;
    int pre = 1, curr = 1;//dp[-1] = dp[0] = 1
    for (int i = 1; i < s.size(); i++) {
        int tmp = curr;
        if (s[i] == '0')
            if (s[i - 1] == '1' || s[i - 1] == '2') curr = pre;
            else return 0;
        else if (s[i - 1] == '1' || (s[i - 1] == '2' && s[i] >= '1' && s[i] <= '6'))
            curr = curr + pre;
        pre = tmp;
    }
    return curr;
}
下一篇：Java 简单dp 时间击败100%


if (string.IsNullOrWhiteSpace(s)) return 0;
int len = s.Length;
const char Zero = '0';
char last1 = s[len-1];
int last1Possibilities = last1 == Zero ? 0 : 1;
int last2Possibilities = 1;
int p;
for( int i = len - 2; -1 < i; i-- )
{
	var c = s[i];
	if(c == Zero)
	{
		last2Possibilities = last1Possibilities;
		last1Possibilities = 0;
		continue;
	}
	p = last1Possibilities;
	if (c == '1' || c == '2' && last1 < '7') p += last2Possibilities;
	last1 = c;
	last2Possibilities = last1Possibilities;
	last1Possibilities = p;
}
return last1Possibilities;

public class Solution {
    public int NumDecodings(string s) {
        if(s[0] == '0') return 0;
        int len = s.Length;
        int[] dp = new int[len+1];
        dp[0] = 1;dp[1] = 1;
        for(int i = 1;i < len;i++){
            if(s[i] == '0'){
                if((s[i-1] == '1' || s[i-1] == '2')){
                    dp[i+1] = dp[i-1];
                }else{
                    return 0;
                }
            }else{
                if(s[i-1] == '1' || (s[i-1] == '2' && s[i] <= '6')){
                    dp[i+1] = dp[i] + dp[i-1];
                }else{
                    dp[i+1] = dp[i];
                }
            }
        }

        return dp[len];
    }
}

public class Solution {
    public int NumDecodings2 (string s) {
        int[] dp = new int[s.Length + 1];
        dp[0] = 1;
        dp[1] = s[0] == '0' ? 0 : 1;
        if (s.Length <= 1) return dp[1];
        for (int i = 2; i <= s.Length; i++) {
            int n = (s[i - 2] - '0') * 10 + (s[i - 1] - '0');
            if (s[i - 1] == '0' && s[i - 2] == '0') {
                return 0;
            } else if (s[i - 2] == '0') {
                dp[i] = dp[i - 1];
            } else if (s[i - 1] == '0') {
                if (n > 26) return 0;
                dp[i] = dp[i - 2];
            } else if (n > 26) {
                dp[i] = dp[i - 1];
            } else {
                dp[i] = dp[i - 1] + dp[i - 2];
            }
        }
        return dp[dp.Length - 1];
    }

    public int NumDecodings (string s) {
        // if (s == null || s.Length == 0) return 0;
        int[] dp = new int[s.Length + 1];
        dp[0] = 1;
        dp[1] = s[0] == '0' ? 0 : 1;
        if (s.Length <= 1) return dp[1];
        for (int i = 2; i <= s.Length; i++) {
            int p1 = s[i - 1] - '0';
            int p2 = s[i - 2] - '0';
            if (p1 == 0 && p2 == 0) return 0;
            int n = p2 * 10 + p1;
            // 为0则无法单独成字母，所以铁定包含在一次解法中
            if (p2 == 0) {
                dp[i] = dp[i - 1];
            } else if (p1 == 0) {
                if (n > 26) return 0;
                dp[i] = dp[i - 2];
            } else if (n > 26) {
                dp[i] = dp[i - 1];
            } else {
                dp[i] = dp[i - 1] + dp[i - 2];
            }
        }
        return dp[dp.Length - 1];
    }
}
 
 
 
*/