using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给出基数为 -2 的两个数 arr1 和 arr2，返回两数相加的结果。

数字以 数组形式 给出：数组由若干 0 和 1 组成，按最高有效位到最低有效位的顺序排列。例如，arr = [1,1,0,1] 表示数字 (-2)^3 + (-2)^2 + (-2)^0 = -3。数组形式 的数字也同样不含前导零：以 arr 为例，这意味着要么 arr == [0]，要么 arr[0] == 1。

返回相同表示形式的 arr1 和 arr2 相加的结果。两数的表示形式为：不含前导零、由若干 0 和 1 组成的数组。

 

示例：

输入：arr1 = [1,1,1,1,1], arr2 = [1,0,1]
输出：[1,0,0,0,0]
解释：arr1 表示 11，arr2 表示 5，输出表示 16 。

0(2=>1) 0(1+1=>1) 0(2+1+1=>2) 0(1+1+2=>2) 1(1+2+2=>2) 0(0+2+2=>2)  0(0+2+2=>2)

提示：

1 <= arr1.length <= 1000
1 <= arr2.length <= 1000
arr1 和 arr2 都不含前导零
arr1[i] 为 0 或 1
arr2[i] 为 0 或 1
*/
/// <summary>
/// https://leetcode-cn.com/problems/adding-two-negabinary-numbers/
/// 1073. 负二进制数相加
/// 
/// </summary>
class AddingTwoNegabinaryNumbersSolution
{
    public void Test()
    {
        //var ret = AddNegabinary(new int[] { 1 }, new int[] { 1, 1 });
        var ret = AddNegabinary(new int[] { 1,1,1,1,1 }, new int[] { 1, 0, 1 });


        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int[] AddNegabinary(int[] arr1, int[] arr2)
    {
        int capacity = Math.Max(arr1.Length , arr2.Length) + 4;
        var sum = new int[capacity];
        Array.Fill(sum, 0);

        for (int i = arr1.Length - 1, sumIndex = 0; -1 < i; i--, sumIndex++) sum[sumIndex] += arr1[i];
        for (int i = arr2.Length - 1, sumIndex = 0; -1 < i; i--, sumIndex++) sum[sumIndex] += arr2[i];

        int v;
        int carry;
        int upper = capacity - 2;
        for (int i = 0; i < upper; i++)
        {
            v = sum[i];
            if (1 < v)
            {
                // 进位
                carry = v >> 1;

                // 去掉进位，剩下当前位的值
                sum[i] = v & 1;

                // 进位影响后两位
                sum[i + 1] += carry;
                sum[i + 2] += carry;
            }
        }


        int last1Index = sum.Length - 3; // 避免类似11 + 1导致的不停进位。以及后两位是4，2等%2 == 0的数字
        while (0 < last1Index && sum[last1Index] == 0) --last1Index;

        var ret = new int[last1Index+1];
        for (int i = 0; -1 < last1Index; i++, last1Index--) ret[i] = sum[last1Index];

        return ret;
    }
}
/*
C++ 数学法题解
大力王
发布于 2 个月前
194 阅读
解题思路
1，推导发生进位时数字如何变化
(-2)^n + (-2)^n = (-2)^n * 2 = (-2)^n * (-2) * (-1) = (-2)^(n + 1) * -1 = (-2)^(n + 1) * (-2 + 1) = (-2)^(n + 2) + (-2)^(n + 1)
因此有：
(-2)^n + (-2)^n = (-2)^(n + 1) + (-2)^(n + 2)
也就是如果第n位发生进位时，第n+1与n+2位分别都要+1
2，根据进位规律进行迭代计算即可

这里有一个点需要注意，就是进位可能无限进行下去，这种情况下是遇到了从某一位开始出现加和为0的情况（比如11 + 1 = 0），因此事先确定数字长度上界，然后计算完成后进行截断即可。

代码
class Solution {
public:
    
    //(-2)^n + (-2)^n = (-2)^n * 2 = (-2)^n * (-2) * (-1) = (-2)^(n + 1) * -1 = (-2)^(n + 1) * (-2 + 1) = (-2)^(n + 2) + (-2)^(n + 1)
    //(-2)^n + (-2)^n = (-2)^(n + 1) + (-2)^(n + 2)
    
vector<int> addNegabinary(vector<int>& arr1, vector<int>& arr2)
{
    int N1 = arr1.size();
    int N2 = arr2.size();
    int N = max(N1, N2) + 4;
    vector<int> res(N, 0);
    for (int i = N1 - 1; i >= 0; --i)
    {
        res[N1 - 1 - i] += arr1[i];
    }
    for (int i = N2 - 1; i >= 0; --i)
    {
        res[N2 - 1 - i] += arr2[i];
    }
    for (int i = 0; i + 2 < N; ++i)
    {
        int c = res[i] >> 1;
        res[i] &= 1;
        res[i + 1] += c;
        res[i + 2] += c;
    }
    int k = N - 3;
    while (k > 0 && res[k] == 0) --k;
    reverse(res.begin(), res.begin() + k + 1);
    res.resize(k + 1);
    return res;
}
};

最后结束在奇数和：13，然后2项趋于稳定
0(=2) 1(2+1=3) 0(2+1+1=4) 1(2+2+1=5) 0(2+2+2=6) 1(2+2+3=7) 0(2+3+3=8) 1(2+3+4=9) 0(2+4+4=10) 1(2+4+5=11) 0(2+5+5=12) 1(2+5+6=13) 0(0+6+6=12) 0(0+6+6=12) 0(0+6+6=12)

最后结束在偶数和：14，然后4项趋于稳定
0(=2) 1(2+1=3) 0(2+1+1=4) 1(2+2+1=5) 0(2+2+2=6) 1(2+2+3=7) 0(2+3+3=8) 1(2+3+4=9) 0(2+4+4=10) 1(2+4+5=11) 0(2+5+5=12) 1(2+5+6=13) 0(2+6+6=14) 1(0+6+7=13) 1(0+7+6=13) 0(0+6+6=12) 0(0+6+6=12) 0(0+6+6=12) 
*/