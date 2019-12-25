using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
当 A 的子数组 A[i], A[i+1], ..., A[j] 满足下列条件时，我们称其为湍流子数组：

若 i <= k < j，当 k 为奇数时， A[k] > A[k+1]，且当 k 为偶数时，A[k] < A[k+1]；
或 若 i <= k < j，当 k 为偶数时，A[k] > A[k+1] ，且当 k 为奇数时， A[k] < A[k+1]。
也就是说，如果比较符号在子数组中的每个相邻元素对之间翻转，则该子数组是湍流子数组。

返回 A 的最大湍流子数组的长度。

 

示例 1：

输入：[9,4,2,10,7,8,8,1,9]
输出：5
解释：(A[1] > A[2] < A[3] > A[4] < A[5])
示例 2：

输入：[4,8,12,16]
输出：2
示例 3：

输入：[100]
输出：1
 

提示：

1 <= A.length <= 40000
0 <= A[i] <= 10^9
*/
/// <summary>
/// https://leetcode-cn.com/problems/longest-turbulent-subarray/
/// 978. 最长湍流子数组
/// 
/// </summary>
class LongestTurbulentSubarraySolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int MaxTurbulenceSize(int[] A)
    {
        int len = A.Length;
        int ret = 1;

        int left = 0;
        for (int right = 1; right < len; ++right)
        {
            var v = A[right];
            int prev = A[right - 1].CompareTo(v);
            if (right == len - 1 || -1 < prev * v.CompareTo(A[right + 1]) )
            {
                if (prev != 0) ret = Math.Max(ret, right - left + 1);
                left = right;
            }
        }

        return ret;
    }
}
/*
最长湍流子数组
力扣 (LeetCode)
发布于 1 年前
2.2k 阅读
方法：滑动窗口
思路

显然，我们只需要关注相邻两个数字之间的符号就可以了。 如果用 -1, 0, 1 代表比较符的话（分别对应 <、 =、 >），那么我们的目标就是在符号序列中找到一个最长的元素交替子序列 1, -1, 1, -1, ...（从 1 或者 -1 开始都可以）。

这些交替的比较符会形成若干个连续的块 。我们知道何时一个块会结束：当已经到符号序列末尾的时候或者当序列元素不再交替的时候。

举一个例子，假设给定数组为 A = [9,4,2,10,7,8,8,1,9]。那么符号序列就是 [1,1,-1,1,-1,0,-1,1]。它可以被划分成的块为 [1], [1,-1,1,-1], [0], [-1,1]。

算法

从左往右扫描这个数组，如果我们扫描到了一个块的末尾（不再交替或者符号序列已经结束），那么就记录下这个块的答案并将其作为一个候选答案，然后设置下一个元素（如果有的话）为下一个块的开头。

class Solution {
    public int maxTurbulenceSize(int[] A) {
        int N = A.length;
        int ans = 1;
        int anchor = 0;

        for (int i = 1; i < N; ++i) {
            int c = Integer.compare(A[i-1], A[i]);
            if (i == N-1 || c * Integer.compare(A[i], A[i+1]) != -1) {
                if (c != 0) ans = Math.max(ans, i - anchor + 1);
                anchor = i;
            }
        }

        return ans;
    }
}
复杂度分析

时间复杂度：O(N)O(N)，其中 NN 是数组 A 的长度。
空间复杂度：O(1)O(1)。

两种方法：滑动窗口或动态规划
AlgsCG
发布于 2 个月前
283 阅读
题解1：滑动窗口

题目重点语句：如果比较符号在子数组中的每个相邻元素对之间翻转，则该子数组是湍流子数组。通俗点解释就是<、>、<、>要交替出现，不能出现连续的>、>或<、<或>、=或<、=。对于本题例子9 4 2 10 7 8 8 1 9，我们用-1，0，1来b表示两个数字之间的符号（小于，等于，大于），也就是说n个数字将产生n-1个符号，将例子转换后的结果为9 1>4 1> 2 -1< 10 1> 7 -1< 8 0= 8 1> 1 -1< 9 。窗口内的符号为1,1,-1,1,-1,0,1,-1，然后当我们遇到连续两个符号相等或符号为0时，也就是当我们发现湍流不成立时，我们需要划分窗口，所以可划分为四个窗口,1,1,-1,1,-1,0,1,-1。

代码如下：

class Solution {
public:
    int compare(int a,int b){
        return (a>b)?1:(a==b)?0:-1;
    }
    
    //题解1：滑动窗口
    int maxTurbulenceSize(vector<int>& A) {
        if(A.empty())return 0;
        
        //初始化窗口边界，result以及数组边界
        int left=0,right=1,result=1,N=A.size();
        
        while(right<N)
        {
            int flag=compare(A[right-1],A[right]);
            
            //到达数组末尾或者湍流不成立时，开始划分窗口
            if(right==N-1||flag*compare(A[right],A[right+1])!=-1)
            {
                if(flag!=0)result=max(result,right-left+1);//划分窗口，更新result值
                left=right;//窗口左边界右移
            }
            ++right;
        }
        return result;
    }
   }
};
题解2：动态规划
状态dp[i]表示以A[i]结尾的最长湍流子数组的长度，注意以A[i]结尾是必须的。
这样我们来分析状态转移方程了:

1）若A[i-2],A[i-1],A[i]构成一个湍流数组成立，那么dp[i]=dp[i-1]+1。通俗点解释就是上一个湍流子数组加上A[i]后生成的新的湍流数组，新的湍流数组长度也就是上一个最长湍流子数组的长度dp[i-1]+1。
2）若湍流数组不成立，那么我们需要比较A[i-1]与A[i]是否相等来确定dp[i]的值。若A[i-1]!=A[i],则dp[i]=2;若A[i-1]==A[i],则dp[i]=1。
代码如下：

class Solution {
public:
    int compare(int a,int b){
        return (a>b)?1:(a==b)?0:-1;
    }
     //题解2：动态规划
    int maxTurbulenceSize(vector<int>& A) {
        if(A.size()<2)return A.size();
        
        int N=A.size(),dp[N];
        memset(dp,1,sizeof(dp));
        
        dp[0]=1;
        dp[1]=(A[1]!=A[0])?2:1;
        
        int result=max(dp[0],dp[1]);
        
        for(int i=2;i<N;++i)
        {
           if(compare(A[i-2],A[i-1])*compare(A[i-1],A[i])==-1){//瑞流成立
               dp[i]=dp[i-1]+1;
               result=max(result,dp[i]);//更新result
           }else if(compare(A[i-1],A[i])!=0){//瑞流不成立且两数不相等，dp[i]=2
               dp[i]=2;
           }
        }
        return result;
    }
};
补充：对于这种连续子数组或者子序列的题，我们用动态规划思想的话，可以将状态方程设计为以下两种形式：

1）令dp[i]表示以A[i]开头（或结尾）的***
2）令dp[i][j]表示A[i]至A[j]区间的***
注：这里的***表示对原问题的描述。 

public class Solution {
    public int MaxTurbulenceSize(int[] A)
    {
        int maxSize = 1;
        int lastcompare = 0;
        int currSize = 1;
        int currcompare;
        for (int i = 1; i < A.Length; i++)
        {
            if (A[i] == A[i - 1])
                currcompare = 0;
            else
                currcompare = A[i] > A[i - 1] ? 1 : -1;
            if(currcompare == 0)
            {
                maxSize = Math.Max(maxSize, currSize);
                currSize = 1;
            }
            else
            {
                if (currcompare * lastcompare == 1)
                {
                    maxSize = Math.Max(maxSize, currSize);
                    currSize = 2;
                }
                else
                {
                    currSize++;
                }
            }
            lastcompare = currcompare;
        }
        maxSize = Math.Max(maxSize, currSize);
        return maxSize;
    }
}

public class Solution {
    public int MaxTurbulenceSize(int[] A) {
        int maxSize = 1;
        int len = A.Length;
        int lastFlag = 0;
        for (int i = 1, j = 0; i < len; i++)
        {
            int flag = Math.Sign(A[i] - A[i - 1]);
            if (flag == 0)
            {
                maxSize = Math.Max(maxSize, i - j);
                j = i; 
                lastFlag = 0;
            }
            else if (flag + lastFlag == 0)
            {
                lastFlag = flag;
                if (i == len - 1) maxSize = Math.Max(maxSize, i - j + 1);
            }
            else
            {
                maxSize = Math.Max(maxSize, i - j);
                j = i - 1; 
                lastFlag = flag;
            }
        }
        return maxSize;
    }
}
*/
