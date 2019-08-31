using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
在一个由 'L' , 'R' 和 X 三个字符组成的字符串（例如"RXXLRXRXL"）中进行移动操作。一次移动操作指用一个"LX"替换一个"XL"，或者用一个"XR"替换一个"RX"。现给定起始字符串start和结束字符串end，请编写代码，当且仅当存在一系列移动操作使得start可以转换成end时， 返回True。

示例 :

输入: start = "RXXLRXRXL", end = "XRLXXRRLX"
输出: True
解释:
我们可以通过以下几步将start转换成end:
RXXLRXRXL ->
XRXLRXRXL ->
XRLXRXRXL ->
XRLXXRRXL ->
XRLXXRRLX
注意:

1 <= len(start) = len(end) <= 10000。
start和end中的字符串仅限于'L', 'R'和X。
*/
/// <summary>
/// https://leetcode-cn.com/problems/swap-adjacent-in-lr-string/
/// 777. 在LR字符串中交换相邻字符
/// https://www.cnblogs.com/blog-of-zxf/p/11332420.html
/// </summary>
class SwapAdjacentInLrStringSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public bool CanTransform(string start, string end)
    {
        const char X = 'X';
        int len = start.Length, i = 0, j = 0;

        while (i < len && j < len)
        {
            //第一步：start 和 end 都跳过左边的X
            while (i < len && start[i] == X) i++;
            
            while (j < len && end[j] == X) j++;
            
            if (i == len && j == len) return true;  //XXLXXXX->XXLXXXX
            if (i == len || j == len) return false;  //X->R

            //第二步：查看当前两个指针指向的字符是否一样，不一样说明无法通过题意的移动方式转变为相同的字符串
            if (i < len && j < len && start[i] != end[j]) return false;

            //经过以上操作有：start[i] == end[j] 为‘R’或者‘L’
            //第三步：检查此时start和end中'R'左边的X的个数，如果前者大于后者，那不能成功。
            //为什么呢？因为按题意'R'只能向右移动，且移动时X会向左移，即‘R’左边的‘X’只会变多
            //如果start中‘R’左边的‘X’都比end中的多，那再怎么移动，也不能使start中‘R’左边的‘X’个数等于end中的

            if (i < len && j < len && start[i] == 'R' && i > j) return false;

            //第四步：同理，检查此时start和end中'L'左边的X的个数，如果后者大于前者，那不能成功。
            //因为‘L’只能向左移动，如果start中‘L’左边的‘X’个数比end中‘L’左边的少，那么start中移动‘L’
            //只会让这个数更少

            if (i < len && j < len && start[i] == 'L' && i < j) return false;

            //第五步：检查完都进入下一步
            i++;
            j++;
        }
        return true;
    }
}
/*
using System.Collections.Generic;
public class Solution
{
    Queue<int> Llist = new Queue<int>();
    Queue<int> Rlist = new Queue<int>();
    List<char> SChar = new List<char>();
    List<char> EChar = new List<char>();
    public bool CanTransform(string start, string end)
    {
        Llist.Clear();
        Rlist.Clear();
        SChar.Clear();
        EChar.Clear();
        for (int i = 0; i < start.Length; i++)
        {
            if (start[i] == 'L')
            {
                Llist.Enqueue(i);
                SChar.Add(start[i]);
            }
            else if (start[i] == 'R')
            {
                Rlist.Enqueue(i);
                SChar.Add(start[i]);
            }
        }
        for (int i = 0; i < end.Length; i++)
        {
            if (end[i] == 'L')
            {
                if (Llist.Count == 0 || Llist.Dequeue() < i)
                    return false;
                EChar.Add(end[i]);
            }
            else if (end[i] == 'R')
            {
                if (Rlist.Count == 0 || Rlist.Dequeue() > i)
                    return false;
                EChar.Add(end[i]);
            }
        }
        if (SChar.Count == EChar.Count && Llist.Count == 0 && Llist.Count == Rlist.Count  )
        {
            for (int i = 0; i < SChar.Count; i++)
            {
                if (SChar[i] != EChar[i])
                    return false;
            }
            return true;
        }
        return false;
    }
} 
*/
