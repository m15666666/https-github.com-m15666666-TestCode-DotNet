using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
通常，正整数 n 的阶乘是所有小于或等于 n 的正整数的乘积。例如，factorial(10) = 10 * 9 * 8 * 7 * 6 * 5 * 4 * 3 * 2 * 1。

相反，我们设计了一个笨阶乘 clumsy：在整数的递减序列中，我们以一个固定顺序的操作符序列来依次替换原有的乘法操作符：乘法(*)，除法(/)，加法(+)和减法(-)。

例如，clumsy(10) = 10 * 9 / 8 + 7 - 6 * 5 / 4 + 3 - 2 * 1。然而，这些运算仍然使用通常的算术运算顺序：我们在任何加、减步骤之前执行所有的乘法和除法步骤，并且按从左到右处理乘法和除法步骤。

另外，我们使用的除法是地板除法（floor division），所以 10 * 9 / 8 等于 11。这保证结果是一个整数。

实现上面定义的笨函数：给定一个整数 N，它返回 N 的笨阶乘。

 

示例 1：

输入：4
输出：7
解释：7 = 4 * 3 / 2 + 1
示例 2：

输入：10
输出：12
解释：12 = 10 * 9 / 8 + 7 - 6 * 5 / 4 + 3 - 2 * 1
 

提示：

1 <= N <= 10000
-2^31 <= answer <= 2^31 - 1  （答案保证符合 32 位整数。）
*/
/// <summary>
/// https://leetcode-cn.com/problems/clumsy-factorial/
/// 1006. 笨阶乘
/// 
/// </summary>
class ClumsyFactorialSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int Clumsy(int N)
    {
        switch (N)
        {
            case 1: return 1;
            case 2: return 2;
            case 3: return 6;
            case 4: return 7;
        }
        
        switch((N - 5 ) % 4)
        {
            case 0: return N + 2;
            case 1: return N + 2;
            case 2: return N - 1;

            default:
            case 3: return N + 1;
        }
    }
}
/*
4ms python
vxiash
发布于 2 个月前
178 阅读
```python
class Solution(object):
    def clumsy(self, N):
        """
        :type N: int
        :rtype: int
        """
        if N <= 4:
            return [1, 2, 6, 7][N-1]
        mod = N & 3
        return N + [1, 2, 2, -1][mod]

(k+2)(k+1)/k
= (k^2+3k+2)/k
= k+3 + 2/k
于是k>=3时，2/k项变0抹掉，原式就等于k+3

而+(k+3) - (k+2)(k+1)/k就等于0，于是中间项全部约掉，只剩首尾需要处理

例如：N=101
clumsy(101)
=101x100/99 + 98 - 97x96/95 + 94 - 93x92/91 + ... + 6 - 5x4/3 + 2 - 1
=(99+3) + (98-98) + (94-94) + ... + (6-6) + 2 - 1
=99+3+2-1=103
=N+2

类似可得，从N=5开始，结果在[N+2, N+2, N-1, N+1]中间循环

再手算N<=4时的[1,2,6,7]，返回即可。

    (k+2)(k+1)/k = (k^2 + 3k + 2)/k = (k+3) + 2/k
    
    1 = 1
    5*4/3+2-1 = (3+3) + 2 - 1 = 7 = N+2
    9*8/7+6-5*4/3+2-1 = (7+3) + 6 - (3+3) + 2 - 1 = (7+3) + 2 - 1 = 11 = N+2
    
    2*1 = 2
    6*5/4+3-2*1 = (4+3) + 3 - 2 = 8 = N+2
    10*9/8+7-6*5/4+3-2*1 = (8+3) + 7 - (4+3) + 3 - 2 = (8+3) + 3 - 2 = 12 = N+2
    
    3*2/1 = 6
    7*6/5+4-3*2/1 = (5+3) + 4 - 6 = 6 = N-1
    11*10/9+8-7*6/5+4-3*2/1 = (9+3) + 8 - (5+3) + 4 - 6 = (9+3) + 4 - 6 = 10 = N-1
    
    4*3/2+1 = (2+3+1) + 1 = 7
    8*7/6+5-4*3/2+1 = (6+3) + 5 - (2+3+1) + 1 = 9 = N+1
	
 public class Solution
    {
        public int Clumsy(int N)
        {
            Queue<string> q = new Queue<string>();
            q.Enqueue("+");
            q.Enqueue("-");
            int res = 0;
            if (N > 2)
            {
                res = N * (N - 1) / (N - 2);
                N -= 3;
             
                while (N > 0)
                {
                    var opr = q.Dequeue();
                    if (opr == "+")
                    {
                        res += N;
                        N--;
                        q.Enqueue(opr);
                        continue;
                    }
                    if (opr == "-")
                    {
                        if (N > 2)
                        {
                            res -= N * (N - 1) / (N - 2);
                            N -= 3;
                        }
                        else {
                            res -= N;
                            return res;
                        }
                        q.Enqueue(opr);
                        continue;
                    }
            
                }
            }
     
            else {
                return N;
            }
            return res;
        }

    }

public class Solution {
    
    public int Clumsy(int N) {
        int SUM = -1;
        int addtion = 0;
        for(int i = N; i > 0; i -= 4){
            int g = 0;
            
            for(int j = 0; j < 4; ++j){
                int tmp = i - j;
                if(tmp <= 0)break;
                switch(j){
                    case 0: {
                        g = tmp;
                    }break;
                    case 1: {
                        g *= tmp;
                    }break;
                    case 2: {
                        g /= tmp;
                    }break;
                    case 3: {
                        addtion += tmp;
                    }break;
                }
            }
            
            //Debug
            //if(i == 6)return addtion;
            
            if(SUM == -1){
                SUM = g;
            }else{
                SUM -= g;
            }
        }
        SUM += addtion;
        //return addtion;
        return SUM;
    }
}
*/
