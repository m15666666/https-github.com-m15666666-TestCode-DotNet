using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
在一个仓库里，有一排条形码，其中第 i 个条形码为 barcodes[i]。

请你重新排列这些条形码，使其中两个相邻的条形码 不能 相等。 你可以返回任何满足该要求的答案，此题保证存在答案。

 

示例 1：

输入：[1,1,1,2,2,2]
输出：[2,1,2,1,2,1]
示例 2：

输入：[1,1,1,1,2,2,3,3]
输出：[1,3,1,3,2,1,2,1]
 

提示：

1 <= barcodes.length <= 10000
1 <= barcodes[i] <= 10000
*/
/// <summary>
/// https://leetcode-cn.com/problems/distant-barcodes/
/// 1054. 距离相等的条形码
/// 
/// </summary>
class DistantBarcodesSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int[] RearrangeBarcodes(int[] barcodes)
    {
        const int length = 10001;
        int len = barcodes.Length;
        int[] counts = new int[length];
        Array.Fill(counts, 0);
        foreach (var code in barcodes) counts[code]++;

        int maxCount = 0;
        int maxCountNumber = 0;
        for (int number = 1; number < length; number++)
            if (maxCount < counts[number]) (maxCount, maxCountNumber) = (counts[number],number);

        int[] ret = new int[len];
        int retIndex = 0;
        while (retIndex < len)
        {
            if (counts[maxCountNumber] < 1) break;

            counts[maxCountNumber]--;
            ret[retIndex] = maxCountNumber;
            retIndex += 2;
        }

        int countIndex = 0;
        FillResult(retIndex);
        //while (retIndex < len)
        //{
        //    if (counts[countIndex] < 1)
        //    {
        //        countIndex++;
        //        continue;
        //    }

        //    counts[countIndex]--;
        //    ret[retIndex] = countIndex;
        //    retIndex += 2;
        //}

        FillResult(1);
        //retIndex = 1;
        //while (retIndex < len)
        //{
        //    if (counts[countIndex] < 1)
        //    {
        //        countIndex++;
        //        continue;
        //    }

        //    counts[countIndex]--;
        //    ret[retIndex] = countIndex;
        //    retIndex += 2;
        //}

        void FillResult( int index )
        {
            while (index < len)
            {
                if (counts[countIndex] < 1)
                {
                    countIndex++;
                    continue;
                }

                counts[countIndex]--;
                ret[index] = countIndex;
                index += 2;
            }
        }

        return ret;
    }
}
/*
【包含一个特殊情况的处理】统计后排序即可，主要是思路，解法很简单。
张佳晨
2.0k 阅读
1054. 距离相等的条形码

解题思路：
为了保证可以实现相邻一定不相等，可以依次交错排列同一个数字。

首先统计每个数字的出现次数
最特殊的情况为，数组的长度为奇数，某一个数字出现 (length+1)/2(length+1)/2 次， 如 [2, 1, 2, 1, 2]，此时必须先从奇数位开始放置 2，之后才能防止别的数组。

首先从奇数位开始放置出现次数最多的数字。
将其余数字放置在奇数位。
将剩余数字依次放置在偶数位。
解题方案：( 9ms)
执行用时 : 9 ms, 在 Distant Barcodes 的 Java 提交中击败了 100.00% 的用户

内存消耗 : 51.9 MB, 在 Distant Barcodes 的 Java 提交中击败了 100.00% 的用户

class Solution {
    public int[] rearrangeBarcodes(int[] barcodes) {
        // 存在特殊情况结果类似 2, 1, 2, 1, 2
         // 因此优先使用出现次数最多的元素填充奇数位
         //
        // 统计每个数据的出现次数 //
        int len = barcodes.length;
        int[] count = new int[10001];
        for (int i = 0; i < len; i++) {
            count[barcodes[i]]++;
        }       
        // 得到出现次数最多的数字 //
        int maxCnt = 0;
        int maxNum = 0;
        for (int i = 1; i < 10001; i++) {
            if (count[i] > maxCnt) {
                maxCnt = count[i];
                maxNum = i;
            }
        }
        // 先填充奇数位 //
        int[] result = new int[len];
        int pos = 0;    // result 填充位置
        int idx = 0;    // count 使用位置
        // 先使用出现次数最多的数字填充奇数位, 最多恰好填满 //
        while (pos < len) {
            if (count[maxNum] <= 0) {
                break;  // 填充完毕
            } else {
                count[maxNum]--;
                result[pos] = maxNum;
                pos += 2;
            }
        }
        // 尝试继续填充奇数位 //
        while (pos < len) {
            if (count[idx] <= 0) {
                idx++;
                continue;
            } else {
                count[idx]--;
                result[pos] = idx;
                pos += 2;
            }
        }
        // 继续填充偶数位 //
        pos = 1;
        while (pos < len) {
            if (count[idx] <= 0) {
                idx++;
                continue;
            } else {
                count[idx]--;
                result[pos] = idx;
                pos += 2;
            }
        }
        return result;
        
    }
}
复杂度分析:
可以优化统计的方式，降低为 O(N)O(N) 
*/
