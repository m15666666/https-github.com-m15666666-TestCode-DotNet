using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
给定一个非负整数 num。对于 0 ≤ i ≤ num 范围中的每个数字 i ，计算其二进制数中的 1 的数目并将它们作为数组返回。

示例 1:

输入: 2
输出: [0,1,1]
示例 2:

输入: 5
输出: [0,1,1,2,1,2]
进阶:

给出时间复杂度为O(n*sizeof(integer))的解答非常容易。但你可以在线性时间O(n)内用一趟扫描做到吗？
要求算法的空间复杂度为O(n)。
你能进一步完善解法吗？要求在C++或任何其他语言中不使用任何内置函数（如 C++ 中的 __builtin_popcount）来执行此操作。 
*/
/// <summary>
/// https://leetcode-cn.com/problems/counting-bits/
/// 338. 比特位计数
/// </summary>
class CountingBitsSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int[] CountBits(int num)
    {
        if (num < 0) return new int[0];

        var ret = new int[num + 1];
        ret[0] = 0;
        if (num == 0) return ret;

        ret[1] = 1;
        if (num == 1) return ret;

        var slowIndex = 1;
        for(var i = 2; i <= num; i++ )
            if( (slowIndex + slowIndex) == i) ret[i] = ret[slowIndex++];
            else ret[i] = ret[i - 1] + 1;

        return ret;
    }
}
/*
public class Solution {
    public int[] CountBits(int num)
    {
        int[] array = new int[num + 1];

        array[0] = 0;
        if (num > 0)
            array[1] = 1;

        for (int i = 2; i <= num; i++)
        {
            int tep = i;
            array[i] = array[tep % 2];
            tep = tep >> 1;
            array[i] += array[tep];
        }

        return array;
    }
}
public class Solution {
    public int[] CountBits(int num)
    {
        int[] array = new int[num + 1];
        for (int i = 0; i <= num; i++)
        {
            array[i] = PopCount(i);
        }

        return array;
    }

    private static int PopCount(int n)
    {
        int count = 0;
        while (n > 0)
        {
            if (n % 2 == 1)
            {
                count++;
            }

            n /= 2;
        }

        return count;
    }
}
public class Solution {
    public int[] CountBits(int num) {
        int[] result=new int[num+1];
        result[0]=0;
        for(int i=1;i<result.Length;i++){
            if(i%2==0) result[i]=result[i/2];
            else result[i]=result[i-1]+1;
        }
        return result;
    }
}
*/
