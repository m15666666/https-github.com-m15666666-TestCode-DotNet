using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定两个大小相等的数组 A 和 B，A 相对于 B 的优势可以用满足 A[i] > B[i] 的索引 i 的数目来描述。

返回 A 的任意排列，使其相对于 B 的优势最大化。

 

示例 1：

输入：A = [2,7,11,15], B = [1,10,4,11]
输出：[2,11,7,15]
示例 2：

输入：A = [12,24,8,32], B = [13,25,32,11]
输出：[24,32,8,12]
 

提示：

1 <= A.Length = B.Length <= 10000
0 <= A[i] <= 10^9
0 <= B[i] <= 10^9
*/
/// <summary>
/// https://leetcode-cn.com/problems/advantage-shuffle/
/// 870. 优势洗牌
/// 
/// </summary>
class AdvantageShuffleSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int[] AdvantageCount(int[] A, int[] B)
    {
        int[] sortedA = A;
        Array.Sort(sortedA);
        int[] sortedB = (int[])B.Clone();
        Array.Sort(sortedB);

        Dictionary<int, Queue<int>> assigned = new Dictionary<int, Queue<int>>();
        foreach (int b in B) if(!assigned.ContainsKey(b)) assigned.Add(b, new Queue<int>());

        Queue<int>  remaining = new Queue<int>();
        int j = 0;
        foreach (int a in sortedA)
        {
            if (sortedB[j] < a)
            {
                assigned[sortedB[j++]].Enqueue(a);
                continue;
            }
            remaining.Enqueue(a);
        }

        int[] ret = new int[B.Length];
        for (int i = 0; i < B.Length; ++i)
        {
            var queue = assigned[B[i]];
            ret[i] =  0 < queue.Count ? queue.Dequeue(): remaining.Dequeue();
        }
        return ret;
    }
}
/*
public class Solution {
    public int[] AdvantageCount(int[] A, int[] B) {
        var bIndex = Enumerable.Range(0, B.Length).ToArray(); //生成B的索引
        var a = A.OrderBy(x => x).ToList();
        Array.Sort(B, bIndex); //排序B， 维持索引
        var result = new int[B.Length];
        var j = 0;
        var k = B.Length - 1;
        for (var i = 0; i < B.Length; i++)
        {
            if (a[i] > B[j])
            {
                result[bIndex[j]] = a[i]; //最小的A比最小的B大， 直接出结果
                j++; //找下一个B (大一点的B)
            }
            else
            {
                result[bIndex[k]] = a[i]; //最小的A比最小的B小， 也就是比所有的B小， 直接把他分配给最大的B
                k--;// 次大的B
            }
        }
        return result;
    }
}

*/
