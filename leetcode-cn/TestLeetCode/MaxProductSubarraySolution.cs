using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给你一个整数数组 nums ，请你找出数组中乘积最大的连续子数组（该子数组中至少包含一个数字），并返回该子数组所对应的乘积。

 

示例 1:

输入: [2,3,-2,4]
输出: 6
解释: 子数组 [2,3] 有最大乘积 6。
示例 2:

输入: [-2,0,-1]
输出: 0
解释: 结果不能为 2, 因为 [-2,-1] 不是子数组。


*/
/// <summary>
/// https://leetcode-cn.com/problems/maximum-product-subarray/
/// 152. 乘积最大子数组
/// 
/// </summary>
class MaxProductSubarraySolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int MaxProduct(int[] nums) {
        int len = nums.Length;
        int maxF = nums[0], minF = nums[0], ret = nums[0];
        for (int i = 1; i < len; ++i) {
            var v = nums[i];
            int item1 = maxF*v, item2 = minF*v;
            maxF = Max(item1, v, item2);
            minF = Min(item2, v, item1);
            if (ret < maxF) ret = maxF;
        }
        return ret;

        int Max(int a, int b, int c)
        {
            return a < b ? Math.Max(b, c) : Math.Max(a, c);
        }
        int Min(int a, int b, int c)
        {
            return a < b ? Math.Min(a, c) : Math.Min(b, c);
        }

    }

}
/*

乘积最大子数组
力扣官方题解
发布于 2020-05-17
30.2k
方法一：动态规划
思路和算法

如果我们用 f_{\max}(i)f 
max
​	
 (i) 开表示以第 ii 个元素结尾的乘积最大子数组的乘积，aa 表示输入参数 nums，那么根据「53. 最大子序和」的经验，我们很容易推导出这样的状态转移方程：

f_{\max}(i) = \max_{i = 1}^{n} \{ f(i - 1) \times a_i, a_i \}
f 
max
​	
 (i)= 
i=1
max
n
​	
 {f(i−1)×a 
i
​	
 ,a 
i
​	
 }

它表示以第 ii 个元素结尾的乘积最大子数组的乘积可以考虑 a_ia 
i
​	
  加入前面的 f_{\max}(i - 1)f 
max
​	
 (i−1) 对应的一段，或者单独成为一段，这里两种情况下取最大值。求出所有的 f_{\max}(i)f 
max
​	
 (i) 之后选取最大的一个作为答案。

可是在这里，这样做是错误的。为什么呢？

因为这里的定义并不满足「最优子结构」。具体地讲，如果 a = \{ 5, 6, -3, 4, -3 \}a={5,6,−3,4,−3}，那么此时 f_{\max}f 
max
​	
  对应的序列是 \{ 5, 30, -3, 4, -3 \}{5,30,−3,4,−3}，按照前面的算法我们可以得到答案为 3030，即前两个数的乘积，而实际上答案应该是全体数字的乘积。我们来想一想问题出在哪里呢？问题出在最后一个 -3−3 所对应的 f_{\max}f 
max
​	
  的值既不是 -3−3，也不是 4 \times -34×−3，而是 5 \times 30 \times (-3) \times 4 \times (-3)5×30×(−3)×4×(−3)。所以我们得到了一个结论：当前位置的最优解未必是由前一个位置的最优解转移得到的。

我们可以根据正负性进行分类讨论。

考虑当前位置如果是一个负数的话，那么我们希望以它前一个位置结尾的某个段的积也是个负数，这样就可以负负得正，并且我们希望这个积尽可能「负得更多」，即尽可能小。如果当前位置是一个正数的话，我们更希望以它前一个位置结尾的某个段的积也是个正数，并且希望它尽可能地大。于是这里我们可以再维护一个 f_{\min}(i)f 
min
​	
 (i)，它表示以第 ii 个元素结尾的乘积最小子数组的乘积，那么我们可以得到这样的动态规划转移方程：

\begin{aligned} f_{\max}(i) &= \max_{i = 1}^{n} \{ f_{\max}(i - 1) \times a_i, f_{\min}(i - 1) \times a_i, a_i \} \\ f_{\min}(i) &= \min_{i = 1}^{n} \{ f_{\max}(i - 1) \times a_i, f_{\min}(i - 1) \times a_i, a_i \} \end{aligned}
f 
max
​	
 (i)
f 
min
​	
 (i)
​	
  
= 
i=1
max
n
​	
 {f 
max
​	
 (i−1)×a 
i
​	
 ,f 
min
​	
 (i−1)×a 
i
​	
 ,a 
i
​	
 }
= 
i=1
min
n
​	
 {f 
max
​	
 (i−1)×a 
i
​	
 ,f 
min
​	
 (i−1)×a 
i
​	
 ,a 
i
​	
 }
​	
 

它代表第 ii 个元素结尾的乘积最大子数组的乘积 f_{\max}(i)f 
max
​	
 (i)，可以考虑把 a_ia 
i
​	
  加入第 i - 1i−1 个元素结尾的乘积最大或最小的子数组的乘积中，二者加上 a_ia 
i
​	
 ，三者取大，就是第 ii 个元素结尾的乘积最大子数组的乘积。第 ii 个元素结尾的乘积最小子数组的乘积 f_{\min}(i)f 
min
​	
 (i) 同理。

不难给出这样的实现：


class Solution {
public:
    int maxProduct(vector<int>& nums) {
        vector <int> maxF(nums), minF(nums);
        for (int i = 1; i < nums.size(); ++i) {
            maxF[i] = max(maxF[i - 1] * nums[i], max(nums[i], minF[i - 1] * nums[i]));
            minF[i] = min(minF[i - 1] * nums[i], min(nums[i], maxF[i - 1] * nums[i]));
        }
        return *max_element(maxF.begin(), maxF.end());
    }
};
易得这里的渐进时间复杂度和渐进空间复杂度都是 O(n)O(n)。

考虑优化空间。

由于第 ii 个状态只和第 i - 1i−1 个状态相关，根据「滚动数组」思想，我们可以只用两个变量来维护 i - 1i−1 时刻的状态，一个维护 f_{\max}f 
max
​	
 ，一个维护 f_{\min}f 
min
​	
 。细节参见代码。

代码


class Solution {
public:
    int maxProduct(vector<int>& nums) {
        int maxF = nums[0], minF = nums[0], ans = nums[0];
        for (int i = 1; i < nums.size(); ++i) {
            int mx = maxF, mn = minF;
            maxF = max(mx * nums[i], max(nums[i], mn * nums[i]));
            minF = min(mn * nums[i], min(nums[i], mx * nums[i]));
            ans = max(maxF, ans);
        }
        return ans;
    }
};
复杂度分析

记 nums 元素个数为 nn。

时间复杂度：程序一次循环遍历了 nums，故渐进时间复杂度为 O(n)O(n)。

空间复杂度：优化后只使用常数个临时变量作为辅助空间，与 nn 无关，故渐进空间复杂度为 O(1)O(1)。

下一篇：画解算法：152. 乘积最大子序列


public class Solution {
    public int MaxProduct(int[] nums) {
        int maxProduct = int.MinValue;
            int[] oldList = new int[2] ;
            for (int i = 0; i < nums.Length; i++)
            {
                int min;
                int max;
                if (i == 0)
                {
                    max = nums[i];
                    min = nums[i];
                }
                else
                {
                    if (nums[i] > 0)
                    {
                        max = Math.Max(oldList[0] * nums[i], nums[i]);
                        min = Math.Min(oldList[1] * nums[i], nums[i]);
                        
                    }
                    else
                    {
                        max = Math.Max(oldList[1] * nums[i], nums[i]);
                        min = Math.Min(oldList[0] * nums[i], nums[i]);
                    }
                }
                oldList[0] = max;
                oldList[1] = min;
                maxProduct = Math.Max(maxProduct, max);
            }

            return maxProduct;
    }
}

public class Solution {
    public int MaxProduct(int[] nums) {
        int n = nums.Length;

        int ans = int.MinValue;
        int max = 1, min = 1;

        for(int i = 0; i< n ; i++){
           
           if(nums[i] < 0){
                int temp = max;
                max = min;
                min = temp;
           }

           max = Math.Max(nums[i], max*nums[i]);
           min = Math.Min(nums[i], min*nums[i]);

           ans = Math.Max(max, ans);
        }

        return ans;

    }
} 
 
 
 
 
*/
