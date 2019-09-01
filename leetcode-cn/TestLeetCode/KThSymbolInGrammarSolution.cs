using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// https://leetcode-cn.com/problems/k-th-symbol-in-grammar/
/// 779. 第K个语法符号
/// https://blog.csdn.net/kyfant/article/details/82954555
/// </summary>
class KThSymbolInGrammarSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int KthGrammar(int N, int K)
    {
        if (N == 1) return 0;

        if (K % 2 == 1) return KthGrammar(N - 1, (K + 1) / 2);

        return 1 - KthGrammar(N - 1, K / 2);
    }
}
/*
public class Solution {
    public int KthGrammar(int N, int K)
    {
        if (K == 1) return 0;
        // 求父节点的索引
        int father = KthGrammar(N - 1, (K + 1) / 2);
        if (K % 2 == 1)
        {
            // K 为奇数则其值与父结点相同
            return father;
        }
        else
        {
            return 1 - father;
        }
    }
}
public class Solution
{
    public int KthGrammar(int N, int K)
    {
        bool sign = false;
        while (N != 1 && K != 1)
        {
            int tmp = 1 << --N - 1;
            if (K <= tmp) { continue; }
            sign = !sign;
            K -= tmp;
        }
        return sign ? 1 : 0;
    }
}
public class Solution {
    public int KthGrammar(int N, int K) {
        if (N == 1) return 0;
        return (1 - K%2) ^ KthGrammar(N-1, (K+1)/2);
        //string s = "0";
        //getS(N, ref s);
        //return Convert.ToInt32(s[K-1].ToString());
    }
    
    public string getS(int N,ref string s)
    {
        if (N == 1)
            return "0";
        getS(N - 1, ref s);
        string s2 = "";
        for (int i = 0; i < s.Length; i++)
        {
            s2 += s[i] == '0' ? "01" : "10";
        }
        s = s2;
        return s;
    }
}
*/
