using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
我们把数组 A 中符合下列属性的任意连续子数组 B 称为 “山脉”：

B.length >= 3
存在 0 < i < B.length - 1 使得 B[0] < B[1] < ... B[i-1] < B[i] > B[i+1] > ... > B[B.length - 1]
（注意：B 可以是 A 的任意子数组，包括整个数组 A。）

给出一个整数数组 A，返回最长 “山脉” 的长度。

如果不含有 “山脉” 则返回 0。

 

示例 1：

输入：[2,1,4,7,3,2,5]
输出：5
解释：最长的 “山脉” 是 [1,4,7,3,2]，长度为 5。
示例 2：

输入：[2,2,2]
输出：0
解释：不含 “山脉”。
 

提示：

0 <= A.length <= 10000
0 <= v <= 10000
*/
/// <summary>
/// https://leetcode-cn.com/problems/longest-mountain-in-array/
/// 845. 数组中的最长山脉
/// 
/// </summary>
class LongestMountainInArraySolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int LongestMountain(int[] A)
    {
        if (A == null || A.Length < 3) return 0;

        //标记此时处于上山或下山的任意一中状态（上山为 真 ，反之）
        //由于第一次必然是上山，所以值为 真
        bool sign = true;
        //存储每一次测量的数据
        int length = 1;
        //记录截至最新的测量中的最大长度
        int max = 1;
        var preV = A[0];
        int v;
        //开始“登山”（默认此时处于第一个点）
        for (int i = 1; i < A.Length; i++, preV = v)
        {
            v = A[i];
            // i - 1 代表当前位置
            // i  代表眼前位置
            if (v == preV)//平地，结束登山
            {
                sign = true;
                length = 1;
                continue;
            }
            
            if (preV < v)
            {
                if (sign)//上山中
                    length++;
                else // 下山改上山
                {
                    sign = true;
                    length = 2;
                }
                continue;
            }

            //标记此时为下山状态
            sign = false;

            if (1 < length)//下山中（并且题目要求，最小山脉长度为 3，所以下山之前必须上山一次）
            {
                length++;
                //下山更新最大长度即可
                max = length > max ? length : max;
            }
        }
        return 2 < max ? max : 0;
    }
}