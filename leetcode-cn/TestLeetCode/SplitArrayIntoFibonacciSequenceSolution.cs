using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个数字字符串 S，比如 S = "123456579"，我们可以将它分成斐波那契式的序列 [123, 456, 579]。

形式上，斐波那契式序列是一个非负整数列表 F，且满足：

0 <= F[i] <= 2^31 - 1，（也就是说，每个整数都符合 32 位有符号整数类型）；
F.length >= 3；
对于所有的0 <= i < F.length - 2，都有 F[i] + F[i+1] = F[i+2] 成立。
另外，请注意，将字符串拆分成小块时，每个块的数字一定不要以零开头，除非这个块是数字 0 本身。

返回从 S 拆分出来的所有斐波那契式的序列块，如果不能拆分则返回 []。

示例 1：

输入："123456579"
输出：[123,456,579]
示例 2：

输入: "11235813"
输出: [1,1,2,3,5,8,13]
示例 3：

输入: "112358130"
输出: []
解释: 这项任务无法完成。
示例 4：

输入："0123"
输出：[]
解释：每个块的数字不能以零开头，因此 "01"，"2"，"3" 不是有效答案。
示例 5：

输入: "1101111"
输出: [110, 1, 111]
解释: 输出 [11,0,11,11] 也同样被接受。
提示：

1 <= S.length <= 200
字符串 S 中只含有数字。
*/
/// <summary>
/// https://leetcode-cn.com/problems/split-array-into-fibonacci-sequence/
/// 842. 将数组拆分成斐波那契序列
/// 
/// </summary>
class SplitArrayIntoFibonacciSequenceSolution
{
    public void Test()
    {
        var ret = SplitIntoFibonacci("0000");
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    /// <summary>
    /// 第一个和第二个数确定后，整个备选序列已经确定，不需要使用dfs在剩余序列中试探第三个数是否存在，
    /// 直接相加找出第三个数是否存在即可；
    /// 剪枝：第一个数不能大于整个序列长度的一半，第三个数的长度不能小于第一个数和第二个数的长度最大者，
    /// 数字的开头为0但长度大于1者不留下。
    /// </summary>
    /// <param name="S"></param>
    /// <returns></returns>
    public IList<int> SplitIntoFibonacci(string S)
    {
        const char Zero = '0';
        var ret = new List<int>();
        if (string.IsNullOrEmpty(S)) return ret;

        int len = S.Length;
        int halfLen = len / 2;
        long tmp1, tmp2;
        Stack<char> stack = new Stack<char>(16);
        for (int i = 1; i <= halfLen; i++)
        {
            if (S[0] == Zero && i > 1) break;
            tmp1 = long.Parse(S.Substring(0, i));
            if (int.MaxValue < tmp1) break;

            ret.Add((int)tmp1);
            for (int j = 1; Math.Max(i, j) <= len - i - j; j++)
            {
                if (S[i] == Zero && j > 1) break;
                tmp2 = long.Parse(S.Substring(i, j));
                if (tmp2 > int.MaxValue) break;
                int num1 = (int)tmp1;
                int num2 = (int)tmp2;
                ret.Add(num2);
                int start;
                int sumLen;
                for (start = i + j; start < len; start += sumLen)
                {
                    (num1, num2) = (num2,num1+num2);

                    stack.Clear();
                    int s = num2;
                    if (0 < s)
                    {
                        while (0 < s)
                        {
                            stack.Push((char)(s % 10 + Zero));
                            s = s / 10;
                        }
                    }
                    else stack.Push(Zero);

                    sumLen = stack.Count;
                    bool match = true;
                    for (int i2 = start; 0 < stack.Count && i2 < len; i2++)
                    {
                        if(stack.Pop() != S[i2])
                        {
                            match = false;
                            break;
                        }
                    }
                    if (!match)
                    {
                        break;
                    }
                    ret.Add(num2);
                }
                if (len == start) return ret;
                ret.RemoveRange(1, ret.Count - 1);
            }
            ret.Clear();
        }

        return ret;
    }
}
/*
public class Solution {
    public IList<int> SplitIntoFibonacci(string S) {
        if(S== null || S.Length == 0)
            return new List<int>();
        List<int> ans = new List<int>();
        
        dfs(0,S,ans);
        
        return ans;
    }
    
    public bool dfs(int index, string S, List<int> ans){
        if(index == S.Length && ans.Count >=3){
            return true;
        }
        
        for(int i = index;i<S.Length;i++){
            if(i > index && S[index] == '0')
                break;
            
            long num = Convert.ToInt64(S.Substring(index,i-index+1));
            
            if(num > Int32.MaxValue)
                break;
            
            int size = ans.Count;
            
            if(size >=2 && num > ans[size-1]+ans[size-2]){
                break;
            }
            
            if(size <= 1 || num == ans[size-1]+ans[size-2]){
                ans.Add((int)num);
                if(dfs(i+1,S,ans))
                    return true;
                ans.RemoveAt(ans.Count-1);
            }
        }
        
        return false;
    }
} 
*/
