using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
在一个由 'L' , 'R' 和 'X' 三个字符组成的字符串（例如"RXXLRXRXL"）中进行移动操作。一次移动操作指用一个"LX"替换一个"XL"，或者用一个"XR"替换一个"RX"。现给定起始字符串start和结束字符串end，请编写代码，当且仅当存在一系列移动操作使得start可以转换成end时， 返回True。

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
start和end中的字符串仅限于'L', 'R'和'X'。
*/
/// <summary>
/// https://leetcode-cn.com/problems/swap-adjacent-in-lr-string/
/// 777. 在LR字符串中交换相邻字符
/// 
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

    }
}