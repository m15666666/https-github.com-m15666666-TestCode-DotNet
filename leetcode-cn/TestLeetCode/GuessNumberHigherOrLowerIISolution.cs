using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
我们正在玩一个猜数游戏，游戏规则如下：

我从 1 到 n 之间选择一个数字，你来猜我选了哪个数字。

每次你猜错了，我都会告诉你，我选的数字比你的大了或者小了。

然而，当你猜了数字 x 并且猜错了的时候，你需要支付金额为 x 的现金。直到你猜到我选的数字，你才算赢得了这个游戏。

示例:

n = 10, 我选择了8.

第一轮: 你猜我选择的数字是5，我会告诉你，我的数字更大一些，然后你需要支付5块。
第二轮: 你猜是7，我告诉你，我的数字更大一些，你支付7块。
第三轮: 你猜是9，我告诉你，我的数字更小一些，你支付9块。

游戏结束。8 就是我选的数字。

你最终要支付 5 + 7 + 9 = 21 块钱。
给定 n ≥ 1，计算你至少需要拥有多少现金才能确保你能赢得这个游戏。
*/
/// <summary>
/// https://leetcode-cn.com/problems/guess-number-higher-or-lower-ii/
/// 375. 猜数字大小 II
/// https://blog.csdn.net/xuxuxuqian1/article/details/81636415
/// </summary>
class GuessNumberHigherOrLowerIISolution
{
    public void Test()
    {
        var ret = GetMoneyAmount(3);
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int GetMoneyAmount(int n)
    {
        if (n < 2) return 0;

        int[,] dp = new int[n + 1, n + 1];
        for (int i = 0; i < n; i++) dp[i, i] = 0;
        
        for (int j = 2; j <= n; j++)
        {
            for (int i = j - 1; i >= 0; i--)
            {
                if(i + 1 == j)
                {
                    dp[i, j] = i;
                    continue;
                }
                int global_min = int.MaxValue;
                for (int k = i + 1; k < j; k++)
                {
                    int max = k + Math.Max(dp[i,k - 1], dp[k + 1,j]);
                    if (max < global_min) global_min = max;
                }
                dp[i, j] = global_min;
                //dp[i,j] = i + 1 == j ? i : global_min;//当i == j - 1时，dp[i][j]即为i j中的较小者i
            }
        }
        return dp[1,n];
    }
}
/*
猜数字大小 II
力扣 (LeetCode)

发布于 2019-06-27
22.1k
概要
给一个数字 nn ，我们要用最优策略在 (1, n)(1,n) 范围内考虑猜中数字的最坏情况。每次错误的尝试 ii 都会导致总开销增加 ii 。

比方说：


n=5
1 2 3 4 5
假设答案是 5 ，如果我们一开始猜 3 ，那么我们下一次肯定猜 4 ，最终总代价为 4+3=74+3=7 。

假设答案是 3 或者 1, ，我们一开始猜 4 ，下一次猜 2 ，那么总代价为 4+2=64+2=6 ，这是最小总代价。


n=8
1 2 3 4 5 6 7 8
这种情况下我们先猜 5 然后猜 7 。总开销为 5+7=125+7=12 。
如果我们先猜 4 。总开销为 4+5+7=164+5+7=16 。

解法
方法 1：暴力
首先，我们需要意识到我们在范围 (1, n)(1,n) 中猜数字的时候，需要考虑最坏情况下的代价。也就是说要算每次都猜错的情况下的总体最大开销。

在暴力算法中，我们首先在 (1, n)(1,n) 中任意挑选一个数字，假设它是个错误的猜测（最坏情况），我们需要用最小代价去猜到需要的数字。那么在一次尝试以后，答案要么在我们猜的数字的左边要么在右边，为了考虑最坏情况，我们需要考虑两者的较大值。因此，如果我们选择 ii 作为第一次尝试，总体最小代价是：

\mathrm{cost}(1, n)=i + \max\big(\mathrm{cost}(1,i-1), \mathrm{cost}(i+1,n)\big)
cost(1,n)=i+max(cost(1,i−1),cost(i+1,n))

对于左右两段，我们分别考虑在段内选择一个数，并重复上面的过程来求得最小开销。

使用如上方法，我们能求得从 ii 开始猜，猜到答案的最小代价。同样地，我们遍历 (1, n)(1,n) 中的所有数字并分别作为第一次尝试，求出每一个的代价，并输入最小值即为答案。


public class Solution {
    public int calculate(int low, int high) {
        if (low >= high)
            return 0;
        int minres = Integer.MAX_VALUE;
        for (int i = low; i <= high; i++) {
            int res = i + Math.max(calculate(i + 1, high), calculate(low, i - 1));
            minres = Math.min(res, minres);
        }

        return minres;
    }
    public int getMoneyAmount(int n) {
        return calculate(1, n);
    }
}
复杂度分析

时间复杂度： O(n!)O(n!) 。我们选择一个数作为第一次尝试，然后递归中再选一个数，这样重复 nn 次的时间代价为 O(n!)O(n!) 。
空间复杂度： O(n)O(n) 。 nn 层递归的开销。
方法 2：修改后的暴力
算法

在暴力解中，对于范围 (i, j)(i,j) 中的每一个数字，我们都需要分别考虑选为当前的第一个猜测的代价，然后再分别考虑左右两个区间内的代价。但一个重要的发现是如果我们从范围 \big( i,\frac{i+j}{2} \big)(i, 
2
i+j
​	
 ) 内选择数字作为第一次尝试，右边区间都比左边区间大，所以我们只需要从右边区间获取最大开销即可，因为它的开销肯定比左边区间的要大。为了减少这个开销，我们第一次尝试肯定从 \big(\frac{i+j}{2}, j\big)( 
2
i+j
​	
 ,j) 中进行选数。这样子，两个区间的开销会更接近且总体开销会更小。

所以，我们不需要从 ii 到 jj 遍历每个数字，只需要从 \frac{i+j}{2} 
2
i+j
​	
  到 jj 遍历，且找到暴力解的最小开销即可。


public class Solution {
    public int calculate(int low, int high) {
        if (low >= high)
            return 0;
        int minres = Integer.MAX_VALUE;
        for (int i = (low + high) / 2; i <= high; i++) {
            int res = i + Math.max(calculate(i + 1, high), calculate(low, i - 1));
            minres = Math.min(res, minres);
        }
        return minres;
    }
    public int getMoneyAmount(int n) {
        return calculate(1, n);
    }
}
复杂度分析

时间复杂度： O(n!)O(n!) 。我们选择一个数作为当前第一次尝试，然后在递归中重复这个过程，总时间开销为 O(n!)O(n!) 。
空间复杂度： O(n)O(n) 。递归的深度为 nn 。
方法 3： DP
算法

以 ii 为第一次尝试找到最小开销的过程可以被分解为找左右区间内最小开销的子问题。对于每个区间，我们重复问题拆分的过程，得到更多子问题，这启发我们可以用 DP 解决这个问题。

我们需要使用一个 dpdp 矩阵，其中 dp(i, j)dp(i,j) 代表在 (i, j)(i,j) 中最坏情况下最小开销的代价。现在我们只需要考虑如何求出这个 dpdp 数组。如果区间只剩下一个数 kk ，那么猜中的代价永远为 0 ，因为我们区间里只剩下一个数字，也就是说，所有的 dp(k, k)dp(k,k) 都初始化为 0 。然后，对于长度为 2 的区间，我们需要所有长度为 1 的区间的结果。由此我们可以看出，为了求出长度为 lenlen 区间的解，我们需要所有长度为 len-1len−1 的解。因此我们按照区间长度从短到长求出 dpdp 数组。

现在，我们应该按照什么办法来求出 dpdp 矩阵呢？对于每个 dp(i, j)dp(i,j) ，当前长度为 len=j-i+1len=j−i+1 。我们遵照方法 1 中俄办法，依次挑选每个数字作为第一次尝试的答案，可以求出最小开销：

\mathrm{cost}(i, j)=\mathrm{pivot} + \max\big(\mathrm{cost}(i,\mathrm{pivot}-1), \mathrm{cost}(\mathrm{pivot}+1,n)\big)
cost(i,j)=pivot+max(cost(i,pivot−1),cost(pivot+1,n))

但是在计算开销的时候我们有一个便利之处，就是我们已经知道了小于 lenlen 长度 dpdp 数组的所有答案。因此 dp 方程式变成了：

\mathrm{dp}(i, j) = \min_{\mathrm{pivots}(i, j)} \big[ \mathrm{pivot} + \max \big( \mathrm{dp}(i,\mathrm{pivot}-1) , \mathrm{dp}(\mathrm{pivot}+1,n) \big) \big]
dp(i,j)= 
pivots(i,j)
min
​	
 [pivot+max(dp(i,pivot−1),dp(pivot+1,n))]

其中 \min_{\mathrm{pivots}(i, j)}min 
pivots(i,j)
​	
  表示将 (i, j)(i,j) 中的每个数作为第一个尝试的数。

下面的动画更好地说明了 n=5 的情况：




public class Solution {
    public int getMoneyAmount(int n) {
        int[][] dp = new int[n + 1][n + 1];
        for (int len = 2; len <= n; len++) {
            for (int start = 1; start <= n - len + 1; start++) {
                int minres = Integer.MAX_VALUE;
                for (int piv = start; piv < start + len - 1; piv++) {
                    int res = piv + Math.max(dp[start][piv - 1], dp[piv + 1][start + len - 1]);
                    minres = Math.min(res, minres);
                }
                dp[start][start + len - 1] = minres;
            }
        }
        return dp[1][n];
    }
}
复杂度分析

时间复杂度： O(n^3)O(n 
3
 ) 。我们遍历 dpdp 数组一遍需要 O(n^2)O(n 
2
 ) 的时间开销。对于数组中每个元素，我们最多需要遍历 nn 个数字。

空间复杂度： O(n^2)O(n 
2
 ) 。需要创建 n^2n 
2
  空间的 dpdp数组。

方法 4：优化的 DP
算法

在上一个方法中，我们尝试使用 (i, j)(i,j) 中的每一个数作为第一个选的数。但由于方法 2 中提到的原因，我们只需要从 \big(i+(len-1)/2,j\big)(i+(len−1)/2,j) 中选第一个数就可以了，其中 lenlen 是当前区间的长度。因此转移方程式为：

\mathrm{dp}(i, j)=\min_{\mathrm{pivots}\big(i+\frac{len-1}{2}, j\big)}\big[\mathrm{pivot} + \max\big(\mathrm{dp}(i,\mathrm{pivot}-1), \mathrm{dp}(\mathrm{pivot}+1,n)\big)\big]
dp(i,j)= 
pivots(i+ 
2
len−1
​	
 ,j)
min
​	
 [pivot+max(dp(i,pivot−1),dp(pivot+1,n))]

通过这种方法我们可以在一定程度上优化方法 3 。


public class Solution {
    public int getMoneyAmount(int n) {
        int[][] dp = new int[n + 1][n + 1];

        for (int len = 2; len <= n; len++) {
            for (int start = 1; start <= n - len + 1; start++) {
                int minres = Integer.MAX_VALUE;
                for (int piv = start + (len - 1) / 2; piv < start + len - 1; piv++) {
                    int res = piv + Math.max(dp[start][piv - 1], dp[piv + 1][start + len - 1]);
                    minres = Math.min(res, minres);
                }
                dp[start][start + len - 1] = minres;
            }

        }
        return dp[1][n];
    }
}
复杂度分析

时间复杂度： O(n^3)O(n 
3
 ) 。我们遍历整个 dpdp 矩阵一次需要 O(n^2)O(n 
2
 ) 的时间。对于数组中每个元素，我们最多需要遍历 \frac{n}{2} 
2
n
​	
  个元素。

空间复杂度： O(n^2)O(n 
2
 ) 。 dpdp 数组的空间开销为 n^2n 
2
  。
  
public class Solution
{
    private Dictionary<int, int> _dict;
    public int GetMoneyAmount(int n)
    {
        if(n==124) return 555;
        if (n <= 1)
        return 0;
        _dict = new Dictionary<int, int>();
        _dict[2] = 1;
        _dict[3] = 2;
        _dict[4] = 4;
        return f(n);
    }

    private int f(int n)
    {
        if (n <= 1)
        return 0;

        if (_dict.ContainsKey(n))
        {
            return _dict[n];
        }
        // return f(1,n);
        int res = 0;
        int tag = 1;
        int right = 0;

        // bool _continue = true;
        // while (_continue&&tag*2<n)
        while (tag  < n)
        {
        int temp = n - tag + Math.Max(f(n - tag - 1), right);
        if (temp < res || res == 0)
        {
            // _continue = true;
            res = temp;
        }
        right += n - tag;
        tag = 2 * tag + 1;
        }
        _dict[n] = res;
        return res;
    }
}
public class Solution
{
    private int [] _dict;
    public int GetMoneyAmount(int n)
    {
        if (n <= 1)
        return 0;
        if (n == 124) return 555;
        if(n>5)
        _dict=new int[n+1];
        else{
        _dict=new int[6];
        }
        _dict[2] = 1;
        _dict[3] = 2;
        _dict[4] = 4;
        _dict[5]= 6;
        return f(n);
    }

    private int f(int n)
    {
        if (n <= 1)
        return 0;

        if(_dict[n]!=0){
        return _dict[n];
        }
        int res = 0;
        int tag = 1;
        int right = 0;

        bool _continue = true;
        while (_continue && tag < n)
        {
            _continue = false;
            int temp = n - tag + Math.Max(f(n - tag - 1), right);
            if (temp < res || res == 0)
            {
                _continue = true;
                res = temp;
                right += n - tag;
                tag = 2 * tag + 1;
            }
        }
        _dict[n] = res;
        return res;
    }
}
*/
