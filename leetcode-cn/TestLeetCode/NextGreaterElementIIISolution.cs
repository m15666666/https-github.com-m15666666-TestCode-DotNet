using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个32位正整数 n，你需要找到最小的32位整数，其与 n 中存在的位数完全相同，并且其值大于n。如果不存在这样的32位整数，则返回-1。

示例 1:

输入: 12
输出: 21
示例 2:

输入: 21
输出: -1
*/
/// <summary>
/// https://leetcode-cn.com/problems/next-greater-element-iii/
/// 556. 下一个更大元素 III
/// </summary>
class NextGreaterElementIIISolution
{
    public void Test()
    {
        var ret = NextGreaterElement(1999999999);//
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int NextGreaterElement(int n)
    {
        var origin = n.ToString();
        char[] candidates = origin.ToCharArray();
        Array.Sort(candidates);
        bool[] indexExisting = new bool[candidates.Length];
        Array.Fill(indexExisting, false);
        Stack<char> ret = new Stack<char>();
        long current = 0;
        if ( BackTrack( n, origin, candidates, indexExisting, ret, ref current, 0 ) )
        {
            long r = 0;
            long multiple = 1;
            while( 0 < ret.Count)
            {
                r = (ret.Pop() - '0') * multiple + r;
                multiple *= 10;
            }
            return int.MaxValue < r ? -1 : (int)r;
        }
        return -1;
    }

    private bool BackTrack( long n, string origin, char[] candidates, bool[] indexExisting, Stack<char> ret, ref long current, long partN )
    {
        var oC = origin[ret.Count];
        var len = candidates.Length;
        var oNum = oC - '0';

        var partNext = partN * 10 + oNum;
        char lastC = 'A';
        for ( int i = 0; i < len; i++)
        {
            if (indexExisting[i]) continue;

            var c = candidates[i];
            if (partN == current && c < oC) continue;
            if (c == lastC) continue;

            lastC = c;
            int num = c - '0';

            ret.Push(c);
            indexExisting[i] = true;
            current = current * 10 + num;

            if (ret.Count < len)
            {
                if (BackTrack(n, origin, candidates, indexExisting, ret, ref current, partNext)) return true;
            }
            else if (n < current) return true;

            current = (current - num) / 10;
            indexExisting[i] = false;
            ret.Pop();
        }
        
        return false;
    }
}
/*
public class Solution {
    public int NextGreaterElement (int n)
    {
        StringBuilder s = new StringBuilder(n.ToString());
        int len = s.Length;
        for (int i = len - 2; i >= 0; i--)
        {
            for (int m = len - 1; m > i; m--)
            {
                if (s[m] > s[i])
                {
                    char c = s[m];
                    s[m] = s[i];
                    s[i] = c;
                    s.Append(s.ToString(i + 1, len - i - 1).OrderBy(a => a).ToArray()).Remove(i + 1, len - i - 1);
                    if (len >= 10 && int.Parse(s.ToString(0, len - 1)) > int.MaxValue / 10)
                        return -1;
                    return int.Parse(s.ToString());
                }
            }
        }
        return -1;
    }
}
public class Solution {
    public int NextGreaterElement(int n) {
        string nums = n.ToString();
        char[] c = nums.ToArray();
        Array.Sort(c);
        Array.Reverse(c);
        if(nums.Equals(new string(c)))
        {
            return -1;
        }
        else
        {
            for (int i = nums.Length - 2; i >= 0; i--)
            {
                for (int j = nums.Length - 1; j > i ; j--)
                {
                    if (nums[i] < nums[j])
                    {
                        char[] temp = nums.ToArray();
                        temp[i] = nums[j];
                        temp[j] = nums[i];
                        try
                        {
                            char[] sort = new char[nums.Length - i - 1];
                            Array.Copy(temp, i + 1, sort, 0, sort.Length);
                            Array.Sort(sort);
                            Array.Copy(sort, 0, temp, i + 1, sort.Length);
                            return Convert.ToInt32(new string(temp));
                        }
                        catch
                        {
                            return -1;
                        }
                    }
                }
            }
        }

        return 0;
    }
}

*/
