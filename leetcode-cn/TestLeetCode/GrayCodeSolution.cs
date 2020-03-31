using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// https://leetcode-cn.com/problems/gray-code/
/// 89.格雷编码
/// 格雷编码是一个二进制数字系统，在该系统中，两个连续的数值仅有一个位数的差异。
/// 给定一个代表编码总位数的非负整数 n，打印其格雷编码序列。格雷编码序列必须以 0 开头。
/// </summary>
class GrayCodeSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    public IList<int> GrayCode(int n)
    {
        if (n == 0) return new List<int> { 0 };
        if (n == 1) return new List<int> { 0, 1 };
        if( 64 < n)
        {
            Console.WriteLine($"too big n: {n}");
            return new List<int> { 0 };
        }

        ulong interValue = 0;
        List<int> ret = new List<int>() { 0 };
        HashSet<int> existing = new HashSet<int>() { 0 };

        BackTrack(n, 1, ref interValue, ret, existing);

        return ret;
    }

    private void BackTrack(int n, int step, ref ulong interValue, List<int> ret, HashSet<int> existing)
    {
        if ( n < step ) return;

        ulong mask = 1;
        int shift = n - step;
        if (0 < shift) mask <<= shift;
        for (int i = 0; i < 2; i++ )
        {
            var interValue1 = interValue ^ mask;
            int v = (int)interValue1;
            if (!existing.Contains( v ) )
            {
                existing.Add(v);
                ret.Add(v);
                interValue = interValue1;
            }
            BackTrack( n, step + 1, ref interValue, ret, existing );
        }
    }
}