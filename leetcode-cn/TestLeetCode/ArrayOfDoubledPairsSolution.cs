using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个长度为偶数的整数数组 A，只有对 A 进行重组后可以满足 “对于每个 0 <= i < len(A) / 2，都有 A[2 * i + 1] = 2 * A[2 * i]” 时，返回 true；否则，返回 false。

示例 1：

输入：[3,1,3,6]
输出：false
示例 2：

输入：[2,1,2,6]
输出：false
示例 3：

输入：[4,-2,2,-4]
输出：true
解释：我们可以用 [-2,-4] 和 [2,4] 这两组组成 [-2,-4,2,4] 或是 [2,4,-2,-4]
示例 4：

输入：[1,2,4,16,8,4]
输出：false
 
提示：

0 <= A.length <= 30000
A.length 为偶数
-100000 <= A[i] <= 100000
*/
/// <summary>
/// https://leetcode-cn.com/problems/array-of-doubled-pairs/
/// 954. 二倍数对数组
/// 
/// </summary>
class ArrayOfDoubledPairsSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public bool CanReorderDoubled(int[] A)
    {
        Dictionary<int, int> number2Count = new Dictionary<int, int>();
        int[] keys = new int[A.Length];
        int index = 0;
        foreach (var v in A)
        {
            if (number2Count.ContainsKey(v)) number2Count[v]++;
            else number2Count.Add(v, 1);

            keys[index++] = -1 < v ? v : -v;
        }

        Array.Sort(keys, A);

        foreach (var v in A)
        {
            if (number2Count[v] == 0) continue;

            var v2 = 2 * v;
            if (!number2Count.ContainsKey(v2) || number2Count[v2] < 1) return false;

            number2Count[v]--;
            number2Count[v2]--;
        }

        return true;
    }
}
/*
想法
如果 x 是当前数组中绝对值最小的元素，那么它一定会配对 2*x，因为不存在 x/2 可以和它配对。

算法
直接改写最后的结果数组。

按照绝对值大小检查整个数组。当检查到元素 x 并且没有被使用时，它一定要配对 2*x。我们将尝试记录 x，2x。如果没有元素 2x 则返回答案 false。如果所有元素都被访问，答案是 true。

为了记录哪些元素还没有被访问，可以用 count 来记录。

javapython
class Solution {
    public boolean canReorderDoubled(int[] A) {
        // count[x] = the number of occurrences of x in A
        Map<Integer, Integer> count = new HashMap();
        for (int x: A)
            count.put(x, count.getOrDefault(x, 0) + 1);

        // B = A as Integer[], sorted by absolute value
        Integer[] B = new Integer[A.length];
        for (int i = 0; i < A.length; ++i)
            B[i] = A[i];
        Arrays.sort(B, Comparator.comparingInt(Math::abs));

        for (int x: B) {
            // If this can't be consumed, skip
            if (count.get(x) == 0) continue;
            // If this doesn't have a doubled partner, the answer is false
            if (count.getOrDefault(2*x, 0) <= 0) return false;

            // Write x, 2*x
            count.put(x, count.get(x) - 1);
            count.put(2*x, count.get(2*x) - 1);
        }

        // If we have written everything, the answer is true
        return true;
    }
}
算法复杂度
时间复杂度：O(N \log{N})O(NlogN)，其中 NN 是数组 A 的长度。
空间复杂度：O(N)O(N)。

public class Solution {
    public bool CanReorderDoubled(int[] A) {
        if (A == null && A.Length == 0)
            return false;
        
        List<int> lst0 = new List<int>();
        List<int> lst1 = new List<int>();
        Dictionary<int, int> dict = new Dictionary<int, int>();
        
        for (int i = 0; i < A.Length; ++i)
        {
            if (A[i] < 0)
                lst0.Add(A[i]);
            else if (A[i] > 0)
                lst1.Add(A[i]);
            
            if (dict.ContainsKey(A[i]))
                dict[A[i]] += 1;
            else
                dict[A[i]] = 1;
        }

        if (lst0.Count % 2 != 0 || lst1.Count % 2 != 0)
            return false;
        
        int[] arr0 = lst0.ToArray();
        Array.Sort(arr0);
        for (int i = arr0.Length - 1; i > 0; --i)
        {
            if (!dict.ContainsKey(arr0[i]))
                continue;
            
            int count = dict[arr0[i]];
            int next = 2 * arr0[i];
            if (!dict.ContainsKey(next) || dict[next] < count)
                return false;
            
            dict[next] -= count;
            if (dict[next] == 0)
                dict.Remove(next);
            
            dict.Remove(arr0[i]);
        }
        
        int[] arr1 = lst1.ToArray();
        Array.Sort(arr1);
        for (int i = 0; i < arr1.Length - 1; ++i)
        {
            if (!dict.ContainsKey(arr1[i]))
                continue;
            
            int count = dict[arr1[i]];
            int next = 2 * arr1[i];
            if (!dict.ContainsKey(next) || dict[next] < count)
                return false;
            
            dict[next] -= count;
            if (dict[next] == 0)
                dict.Remove(next);
            
            dict.Remove(arr1[i]);
        }
        
        return true;
    }
}
 
*/
