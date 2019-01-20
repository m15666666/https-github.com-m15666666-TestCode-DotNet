﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
给定一位研究者论文被引用次数的数组（被引用次数是非负整数），数组已经按照升序排列。编写一个方法，计算出研究者的 h 指数。

h 指数的定义: “h 代表“高引用次数”（high citations），一名科研人员的 h 指数是指他（她）的 （N 篇论文中）至多有 h 篇论文分别被引用了至少 h 次。（其余的 N - h 篇论文每篇被引用次数不多于 h 次。）"

 

示例:

输入: citations = [0,1,3,5,6]
输出: 3 
解释: 给定数组表示研究者总共有 5 篇论文，每篇论文相应的被引用了 0, 1, 3, 5, 6 次。
     由于研究者有 3 篇论文每篇至少被引用了 3 次，其余两篇论文每篇被引用不多于 3 次，所以她的 h 指数是 3。
 

说明:

如果 h 有多有种可能的值 ，h 指数是其中最大的那个。

 

进阶：

这是 H指数 的延伸题目，本题中的 citations 数组是保证有序的。
你可以优化你的算法到对数时间复杂度吗？
     
*/
/// <summary>
/// https://leetcode-cn.com/problems/h-index-ii/
/// 275. H指数 II
/// </summary>
class H_IndexIISolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int HIndex(int[] citations)
    {
        if (citations == null || citations.Length == 0) return 0;

        var length = citations.Length;
        for (int index = 0; index < length; index++)
        {
            int hCount = length - index;
            if (hCount <= citations[index]) return hCount;
        }

        return 0;
    }
}
/*
//别人的算法
public class Solution {
    public int HIndex(int[] citations) {
        int len = citations.Length;
        for (int i = 0; i < len; ++i)
        {
            if(citations[len - i - 1] < i + 1) return i;
            if(citations[len - i - 1] == i + 1) return i + 1;
        }
        return citations.Length;
    }
}
public class Solution {
    public int HIndex(int[] citations) {
           if (citations.Length == 0)
                return 0;
            var left = 0;
            var right = citations.Length - 1;
            while (left <= right) {
                var mid = (left + right) / 2;
                if (citations.Length - mid <= citations[mid])
                {
                    if (mid==0||!(citations.Length - (mid - 1) <= citations[mid - 1])) {
                        return citations.Length- mid;
                    }
                    right = mid - 1;
                }
                else {
                    left = mid + 1;
                }
            }
            return 0;
    }
}
     
*/
