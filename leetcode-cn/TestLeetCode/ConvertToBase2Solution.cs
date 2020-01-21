using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给出数字 N，返回由若干 "0" 和 "1"组成的字符串，该字符串为 N 的负二进制（base -2）表示。

除非字符串就是 "0"，否则返回的字符串中不能含有前导零。

 

示例 1：

输入：2
输出："110"
解释：(-2) ^ 2 + (-2) ^ 1 = 2
示例 2：

输入：3
输出："111"
解释：(-2) ^ 2 + (-2) ^ 1 + (-2) ^ 0 = 3
示例 3：

输入：4
输出："100"
解释：(-2) ^ 2 = 4
 

提示：

0 <= N <= 10^9 
*/
/// <summary>
/// https://leetcode-cn.com/problems/convert-to-base-2/
/// 1017. 负二进制转换
/// 
/// </summary>
class ConvertToBase2Solution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public string BaseNeg2(int N)
    {
        if (N == 0) return "0";
        var nums = baseK(N, -2);
        StringBuilder ret = new StringBuilder();
        foreach (var x in nums) ret.Append(x);
        return ret.ToString();
    }

    private static IList<int> baseK(int N, int K)
    {
        if (N == 0) return new int[0];
        List<int> ret = new List<int>();
        while (N != 0)
        {
            int r = ((N % K) + Math.Abs(K)) % Math.Abs(K);
            ret.Add(r);
            N -= r;
            N /= K;
        }
        ret.Reverse();
        return ret;
    }
}
/*
C++ 正负K进制转换通用题解
大力王
322 阅读
通过数学推导可以得到+K/-K进制的通用转化法

class Solution {
public:
    // 无论K是正数还是负数都支持（只支持-10～10进制，因为更高进制需要引入字母）
    vector<int> baseK(int N, int K) {
        if (N == 0) return {0};
        vector<int> res;
        while (N != 0) {
            int r = ((N % K) + abs(K)) % abs(K); // 此处为关键
            res.push_back(r);
            N -= r;
            N /= K;
        }
        reverse(res.begin(), res.end());
        return res;
    }
    string baseNeg2(int N) {
        vector<int> nums = baseK(N, -2);
        string res;
        for (auto x : nums) res += to_string(x);
        return res;
    }
}; 
*/
