using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
格雷编码是一个二进制数字系统，在该系统中，两个连续的数值仅有一个位数的差异。

给定一个代表编码总位数的非负整数 n，打印其格雷编码序列。即使有多个不同答案，你也只需要返回其中一种。

格雷编码序列必须以 0 开头。

 

示例 1:

输入: 2
输出: [0,1,3,2]
解释:
00 - 0
01 - 1
11 - 3
10 - 2

对于给定的 n，其格雷编码序列并不唯一。
例如，[0,2,3,1] 也是一个有效的格雷编码序列。

00 - 0
10 - 2
11 - 3
01 - 1
示例 2:

输入: 0
输出: [0]
解释: 我们定义格雷编码序列必须以 0 开头。
     给定编码总位数为 n 的格雷编码序列，其长度为 2n。当 n = 0 时，长度为 20 = 1。
     因此，当 n = 0 时，其格雷编码序列为 [0]。
 
 
*/
/// <summary>
/// https://leetcode-cn.com/problems/gray-code/
/// 89.格雷编码
/// 
/// 
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
        if (64 < n)
        {
            Console.WriteLine($"too big n: {n}");
            return new List<int> { 0 };
        }

        var len = (int)Math.Pow(2, n);

        //var ret = new List<int>() {0};
        var ret = new int[len];
        ret[0] = 0;
        int count = 1;
        int head = 1;
        for (int i = 0; i < n; i++) {
            //for (int j = ret.Count - 1; -1 < j; j--) // 前半段已经默认添加，之间倒序后半段头上添1
           //     ret.Add(head + ret[j]);

            for (int j = count - 1, index = count; -1 < j; j--) // 前半段已经默认添加，之间倒序后半段头上添1
                ret[index++] = head + ret[j];

            count += count;
            head <<= 1; // 升级2进制中添1的位置
        }
        return ret;
    }

    //public IList<int> GrayCode(int n)
    //{
    //    if (n == 0) return new List<int> { 0 };
    //    if (n == 1) return new List<int> { 0, 1 };
    //    if( 64 < n)
    //    {
    //        Console.WriteLine($"too big n: {n}");
    //        return new List<int> { 0 };
    //    }

    //    ulong interValue = 0;
    //    List<int> ret = new List<int>() { 0 };
    //    HashSet<int> existing = new HashSet<int>() { 0 };

    //    BackTrack(n, 1, ref interValue, ret, existing);

    //    return ret;
    //}

    //private void BackTrack(int n, int step, ref ulong interValue, List<int> ret, HashSet<int> existing)
    //{
    //    if ( n < step ) return;

    //    ulong mask = 1;
    //    int shift = n - step;
    //    if (0 < shift) mask <<= shift;
    //    for (int i = 0; i < 2; i++ )
    //    {
    //        var interValue1 = interValue ^ mask;
    //        int v = (int)interValue1;
    //        if (!existing.Contains( v ) )
    //        {
    //            existing.Add(v);
    //            ret.Add(v);
    //            interValue = interValue1;
    //        }
    //        BackTrack( n, step + 1, ref interValue, ret, existing );
    //    }
    //}
}
/*
Gray Code （镜像反射法，图解）
Krahets
发布于 1 年前
15.8k
思路：
设 nn 阶格雷码集合为 G(n)G(n)，则 G(n+1)G(n+1) 阶格雷码为：
给 G(n)G(n) 阶格雷码每个元素二进制形式前面添加 00，得到 G'(n)G 
′
 (n)；
设 G(n)G(n) 集合倒序（镜像）为 R(n)R(n)，给 R(n)R(n) 每个元素二进制形式前面添加 11，得到 R'(n)R 
′
 (n)；
G(n+1) = G'(n) ∪ R'(n)G(n+1)=G 
′
 (n)∪R 
′
 (n) 拼接两个集合即可得到下一阶格雷码。
根据以上规律，可从 00 阶格雷码推导致任何阶格雷码。
代码解析：
由于最高位前默认为 00，因此 G'(n) = G(n)G 
′
 (n)=G(n)，只需在 res(即 G(n)G(n) )后添加 R'(n)R 
′
 (n) 即可；
计算 R'(n)R 
′
 (n)：执行 head = 1 << i 计算出对应位数，以给 R(n)R(n) 前添加 11 得到对应 R'(n)R 
′
 (n)；
倒序遍历 res(即 G(n)G(n) )：依次求得 R'(n)R 
′
 (n) 各元素添加至 res 尾端，遍历完成后 res(即 G(n+1)G(n+1))。
图解：


代码：
class Solution {
    public List<Integer> grayCode(int n) {
        List<Integer> res = new ArrayList<Integer>() {{ add(0); }};
        int head = 1;
        for (int i = 0; i < n; i++) {
            for (int j = res.size() - 1; j >= 0; j--)
                res.add(head + res.get(j));
            head <<= 1;
        }
        return res;
    }
}
下一篇：Java最简单的版本 简单分析，详细注解
 
public class Solution 
{
    public IList<int> GrayCode(int n)
    {
          IList<int> list = new List<int> { 0 };
    int len;
    for (int i = 0; i < n; i++)
    {
        len = list.Count;//记录之前元素数量
        for (int j = len - 1; j >= 0; j--)
        {
            list.Add((int)(list[j] + Math.Pow(2, i)));//对之前元素进行逆序复制并加上“1”
        }
    }
    return list;
    }
}

public class Solution {
    public IList<int> GrayCode(int n) 
    {
        IList<int> num = new List<int>();
        int len=0;
        num.Add(0);
        for (int i = 0; i < n; i++)
        {
            len = num.Count;
            for (int j = len - 1; j >= 0; j--)
            {
                num.Add((int)(num[j] + Math.Pow(2, i)));
            }
        }
        return num;
    }
}
 
 
*/