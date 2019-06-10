using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
神奇的字符串 S 只包含 '1' 和 '2'，并遵守以下规则：

字符串 S 是神奇的，因为串联字符 '1' 和 '2' 的连续出现次数会生成字符串 S 本身。

字符串 S 的前几个元素如下：S = “1221121221221121122 ......”

如果我们将 S 中连续的 1 和 2 进行分组，它将变成：

1 22 11 2 1 22 1 22 11 2 11 22 ......

并且每个组中 '1' 或 '2' 的出现次数分别是：

1 2 2 1 1 2 1 2 2 1 2 2 ......

你可以看到上面的出现次数就是 S 本身。

给定一个整数 N 作为输入，返回神奇字符串 S 中前 N 个数字中的 '1' 的数目。

注意：N 不会超过 100,000。

示例：

输入：6
输出：3
解释：神奇字符串 S 的前 6 个元素是 “12211”，它包含三个 1，因此返回 3。 
*/
/// <summary>
/// https://leetcode-cn.com/problems/magical-string/
/// 481. 神奇字符串
/// https://blog.csdn.net/SundyGuo/article/details/81096152
/// </summary>
class MagicalStringSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int MagicalString(int n)
    {
        int retData = 1;
        List<int> dataList = new List<int>() { 1,2,2};
        if (n <= 3)
        {
            if (n <= 0)
            {
                return 0;
            }
            return retData;
        }
        int currentPosition = 2;
        while (dataList.Count < n)
        {
            int needData = dataList[currentPosition++];
            int lastData = dataList[dataList.Count - 1];
            if (lastData == 1)
            {
                for (int i = 0; i < needData; i++)
                    dataList.Add(2);
            }
            else
            {
                retData += needData;
                for (int i = 0; i < needData; i++)
                    dataList.Add(1);
            }
        }
        if (dataList.Count > n && dataList[dataList.Count - 1] == 1)
        {
            retData--;
        }
        return retData;
    }
}
/*
public class Solution {
    public int MagicalString(int n) {
     StringBuilder s = new StringBuilder(1000);
        s.Append("1221121221221121122");

        int ptr = 12;
        int res = 0;
        while (s.Length < n)
        {
            if (s[ptr] == '1')
            {
                if (s[s.Length - 1] == '1')
                {
                    s.Append("2");
                }
                else
                {
                    s.Append("1");
                }
            }
            else if (s[ptr] == '2')
            {
                if (s[s.Length - 1] == '1')
                {
                    s.Append("22");
                }
                else
                {
                    s.Append("11");
                }
            }
            ptr++;
        }

        for (int i = 0; i < n; i++)
        {
            if (s[i] == '1')
            {
                res++;
            }
        }
        return res;
    }
} 
*/
