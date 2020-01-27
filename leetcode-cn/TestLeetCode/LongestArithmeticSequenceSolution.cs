using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个整数数组 A，返回 A 中最长等差子序列的长度。

回想一下，A 的子序列是列表 A[i_1], A[i_2], ..., A[i_k] 其中 0 <= i_1 < i_2 < ... < i_k <= A.Length - 1。并且如果 B[i+1] - B[i]( 0 <= i < B.Length - 1) 的值都相同，那么序列 B 是等差的。

 

示例 1：

输入：[3,6,9,12]
输出：4
解释： 
整个数组是公差为 3 的等差数列。
示例 2：

输入：[9,4,7,2,10]
输出：3
解释：
最长的等差子序列是 [4,7,10]。
示例 3：

输入：[20,1,15,3,10,5,8]
输出：4
解释：
最长的等差子序列是 [20,15,10,5]。
 

提示：

2 <= A.Length <= 2000
0 <= A[i] <= 10000 
*/
/// <summary>
/// https://leetcode-cn.com/problems/longest-arithmetic-sequence/
/// 1027. 最长等差数列
/// 
/// </summary>
class LongestArithmeticSequenceSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int LongestArithSeqLength(int[] A)
    {
        int len = A.Length;
        var map = new  Dictionary<int, List<int>>(len);
        for (int i = 0; i < len; ++i)
        {
            var v = A[i];
            if (!map.ContainsKey(v)) map.Add(v, new List<int>());
            map[v].Add(i);
        }

        int ret = 0;
        for (int i = 0; i < len - ret; ++i)
        {
            int iV = A[i];
            for (int j = i + 1; j < len && j < len - ret + 1; ++j)
            {
                int diff = A[j] - iV;
                if (diff == 0)
                {
                    ret = Math.Max(ret, map[iV].Count);
                    continue;
                }

                int count = 2;
                int next = A[j] + diff;
                int lastIndex = j;
                while (map.ContainsKey(next))
                {
                    bool found = false;
                    var list = map[next];
                    foreach (int index in list)
                    {
                        if (lastIndex < index)
                        {
                            lastIndex = index;
                            ++count;
                            next += diff;
                            found = true;
                            break;
                        }
                    }
                    if (!found) break;
                }

                if (ret < count) ret = count;
            }
        }
        return ret;
    }
}
/*
HashMap + 线性查找 或 二分查找(用时：击败96.42%, 内存: 击败100%)
̶.̶G̶F̶u̶＇̶ 、̶ ̶|
665 阅读
TIM图片20191109173939.png

这个方法其实是受第873题. 最长的斐波那契子序列的长度 的官方题解启发，在这个思路上写出来的。

思路：
等差数列 其实在 给定前2个数之后，就能求出整个等差数列(虽说是无限长的)。
既然如此，那就不断地 固定等差数列的 前2个数，去判断下1个理论值 是否在数组A[]中 且 在数组A[]中的下标 要 > 等差数列的最后1个实际值的下标(即理论值的 上1个值的下标)。

具体实现：
构建1个HashMap：数组A[]中的值作为key，值所对应的下标 去组成1个ArrayList 作为value。

线性查找版的代码(有注释版)：
class Solution {
    public int longestArithSeqLength(int[] A) {
        int len = A.length;
        //构建HashMap: key: 数的值， value: 由数在 数组A中的下标 所构成的ArrayList
        Map<Integer, List<Integer>> map = new HashMap<>();
        //遍历数组A并构造HashMap
        for (int i = 0; i < len; ++i) {
            List<Integer> temp_list = map.computeIfAbsent(A[i], unused -> new ArrayList<>());
            temp_list.add(i);
        }
        //存储最终的结果
        int res = 0;
        //计数器，初始化为2(从2起步)
        int count = 2;
        //拿来打杂的，存储 当前等差数列中，理论上的 下1个数的值
        int temp;
        //确定等差数列中的第1个数
        for (int i = 0; i < len - res; ++i) {
            //确定等差数列中的第2个数
            for (int j = i + 1; j < len - res + 1; ++j) {
                //计算等差数列的 差值
                int diff = A[j] - A[i];
                //若差值为0，则A[j]和A[i]相等，在HashMap中查找key为A[i]或A[j] 的value(ArrayList的长度，即下标的个数)
                if (diff == 0) {
                    //更新res的值
                    res = Math.max(res, map.get(A[i]).size());
                    //跳到下一次循环
                    continue;
                }
                //获取 当前等差数列中， 理论上的 下1个数的值
                temp = A[j] + diff;
                //记录 等差数列中，最后1个数的下标(初始化为j，即等差数列中第2个数的下标)
                int last_idx = j;
                //HashMap中 存在 当前等差数列， 下1个数的理论值
                search:
                while (map.containsKey(temp)) {
                    //获取key为理论值的 value(ArrayList)
                    List<Integer> temp_list = map.get(temp);
                    //遍历value(ArrayList)
                    for (int k = 0; k < temp_list.size(); ++k) {
                        //记录当前下标 所代表的值(值 代表 temp这个数 在 A数组中的下标)
                        int temp_idx = temp_list.get(k);
                        //若该值 > 等差数列中 最后1个值的下标
                        if (temp_idx > last_idx) {
                            //更新last_idx(等差数列中 最后1个值的下标)
                            last_idx = temp_idx;
                            //count + 1
                            ++count;
                            //计算 当前等差数列中， 理论上的 下1个数的值
                            temp += diff;
                            continue search;
                        }
                    }
                    //未找到则break
                    break search;
                }
                //更新res的值
                res = Math.max(res, count);
                //count计数 重置为2(从2起步)
                count = 2;
            }
        }
        return res;
    }
}
线性查找版的代码(无注释版)：
class Solution {
    public int longestArithSeqLength(int[] A) {
        int len = A.length;
        Map<Integer, List<Integer>> map = new HashMap<>();
        for (int i = 0; i < len; ++i) {
            List<Integer> temp_list = map.computeIfAbsent(A[i], unused -> new ArrayList<>());
            temp_list.add(i);
        }
        int res = 0, count = 2, temp;

        for (int i = 0; i < len - res; ++i) {
            for (int j = i + 1; j < len - res + 1; ++j) {
                int diff = A[j] - A[i];
                if (diff == 0) {
                    res = Math.max(res, map.get(A[i]).size());
                    continue;
                }
                temp = A[j] + diff;
                int last_idx = j;
                search:
                while (map.containsKey(temp)) {
                    List<Integer> temp_list = map.get(temp);
                    for (int k = 0; k < temp_list.size(); ++k) {
                        int temp_idx = temp_list.get(k);
                        if (temp_idx > last_idx) {
                            last_idx = temp_idx;
                            ++count;
                            temp += diff;
                            continue search;
                        }
                    }
                    break search;
                }
                res = Math.max(res, count);
                count = 2;
            }
        }
        return res;
    }
}
为了使代码更短更简洁，就将线性查找修改为二分查找：

二分查找的代码(有注释)
class Solution {
    public int longestArithSeqLength(int[] A) {
        int len = A.length;
        //构建HashMap: key: 数的值， value: 由数在 数组A中的下标 所构成的ArrayList
        Map<Integer, List<Integer>> map = new HashMap<>();
        //遍历数组A并构造HashMap
        for (int i = 0; i < len; ++i) {
            List<Integer> temp_list = map.computeIfAbsent(A[i], unused -> new ArrayList<>());
            temp_list.add(i);
        }
        //存储最终的结果
        int res = 0;
        //计数器，初始化为2(从2起步)
        int count = 2;
        //拿来打杂的，存储 当前等差数列中，理论上的 下1个数的值
        int temp;
        //确定等差数列中的第1个数
        for (int i = 0; i < len - res; ++i) {
            //确定等差数列中的第2个数
            for (int j = i + 1; j < len - res + 1; ++j) {
                //计算等差数列的 差值
                int diff = A[j] - A[i];
                //若差值为0，则A[j]和A[i]相等，在HashMap中查找key为A[i]或A[j] 的value(ArrayList的长度，即下标的个数)
                if (diff == 0) {
                    //更新res的值
                    res = Math.max(res, map.get(A[i]).size());
                    //跳到下一次循环
                    continue;
                }
                //获取 当前等差数列中， 理论上的 下1个数的值
                temp = A[j] + diff;
                //记录 等差数列中，最后1个数的下标(初始化为j，即等差数列中第2个数的下标)
                int last_idx = j;
                //HashMap中 存在 当前等差数列， 下1个数的理论值
                while (map.containsKey(temp)) {
                    //获取key为理论值的 value(ArrayList)
                    List<Integer> temp_list = map.get(temp);
                    //Collections内置函数二分查找
                    last_idx = -(Collections.binarySearch(temp_list, last_idx) + 1);
                    if (last_idx == temp_list.size())
                        break;
                    last_idx = temp_list.get(last_idx);
                    //count + 1
                    ++count;
                    //计算 当前等差数列中， 理论上的 下1个数的值
                    temp += diff;
                }
                //更新res的值
                res = Math.max(res, count);
                //count计数 重置为2(从2起步)
                count = 2;
            }
        }
        return res;
    }
}
二分查找的代码(无注释)
class Solution {
    public int longestArithSeqLength(int[] A) {
        int len = A.length;
        Map<Integer, List<Integer>> map = new HashMap<>();
        for (int i = 0; i < len; ++i) {
            List<Integer> temp_list = map.computeIfAbsent(A[i], unused -> new ArrayList<>());
            temp_list.add(i);
        }
        int res = 0, count = 2, temp;

        for (int i = 0; i < len - res; ++i) {
            for (int j = i + 1; j < len - res + 1; ++j) {
                int diff = A[j] - A[i];
                if (diff == 0) {
                    res = Math.max(res, map.get(A[i]).size());
                    continue;
                }
                temp = A[j] + diff;
                int last_idx = j;
                while (map.containsKey(temp)) {
                    List<Integer> temp_list = map.get(temp);
                    last_idx = -(Collections.binarySearch(temp_list, last_idx) + 1);
                    if (last_idx == temp_list.size())
                        break;
                    last_idx = temp_list.get(last_idx);
                    ++count;
                    temp += diff;
                }
                res = Math.max(res, count);
                count = 2;
            }
        }
        return res;
    }
}

public class Solution {
    //DP[diff][index] : the length of sequqnce ar indx with difference "diff"
    public int LongestArithSeqLength(int[] A)
    {
        int diff = 2;

        Dictionary<int, int>[] dp = new Dictionary<int, int>[A.Length];

        for (int j = 0; j < A.Length; j++)
        {
            dp[j] = new Dictionary<int, int>();

            for (int i = 0; i < j; i++)
            {
                int des = A[j] - A[i];

                dp[j][des] = dp[i].ContainsKey(des) ? 
                    dp[i][des] + 1 : 2;

                diff = Math.Max(diff, dp[j][des]);
                
            }
        }

        return diff;
    }
} 
*/
