using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个由若干 0 和 1 组成的数组 A，我们最多可以将 K 个值从 0 变成 1 。

返回仅包含 1 的最长（连续）子数组的长度。

示例 1：

输入：A = [1,1,1,0,0,0,1,1,1,1,0], K = 2
输出：6
解释： 
[1,1,1,0,0,1,1,1,1,1,1]
粗体数字从 0 翻转到 1，最长的子数组长度为 6。
示例 2：

输入：A = [0,0,1,1,0,0,1,1,1,0,1,1,0,0,0,1,1,1,1], K = 3
输出：10
解释：
[0,0,1,1,1,1,1,1,1,1,1,1,0,0,0,1,1,1,1]
粗体数字从 0 翻转到 1，最长的子数组长度为 10。
 

提示：

1 <= A.length <= 20000
0 <= K <= A.length
A[i] 为 0 或 1 
*/
/// <summary>
/// https://leetcode-cn.com/problems/max-consecutive-ones-iii/
/// 1004. 最大连续1的个数 III
/// 
/// </summary>
class MaxConsecutiveOnesIIISolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int LongestOnes(int[] A, int K)
    {
        int left = 0, right = 0, count = 0;
        int ret = 0;
        int len = A.Length;

        while (right < len)
        {
            if(A[right] == 0) count += 1;
            while (K < count)
            {
                if (A[left] == 0) count -= 1;
                left++;
            }
            ret = Math.Max(ret, right - left + 1);
            right++;
        }
        return ret;
    }
}
/*
滑动窗口的常规解法
AlgsCG
发布于 3 个月前
874 阅读
本题与424. 替换后的最长重复字符一样的套路，不过更为简单一点，直接用count统计窗口内的0的个数。当窗口内0的个数大于K时，我们需要缩小窗口；当窗口内0的个数小于等于k时，我们就可将窗口大小来与result来进行比较来确定是否更新result了。

注：窗口内0的个数就是表示可以被1替换的个数！

代码如下：

class Solution {
public:
    int longestOnes(vector<int>& A, int K) {
        //count用来统计窗口中0的个数
        int left=0,right=0,count=0,result=0,size=A.size();
        
        while(right<size)
        {
            count+=A[right]==0;
            while(count>K)//当窗口内0的个数大于K时，需要缩小窗口
            {
                count-=A[left]==0;
                left++;
            }
            //窗口内0的个数小于等于k时，也就是可以该窗口内的0都可以替换，根据该窗口长度来确定是否更新result
            result=max(result,right-left+1);
            right++;
        }
        return result;
    }
};
下一篇：「leetcode」485.最大连续1的个数；487.最大连续1的个数 II；1004.最大连续1的个数 III

public class Solution {
    public int LongestOnes(int[] A, int K) {
        int left = 0;
        int result = int.MinValue;
        int count =0;
        int right = 0;
        for(int i=0;i<A.Length;i++)
        {
            if(A[i] == 0)
            {
                //需要更新左指针
                if(count >= K)
                {
                    //如果当前为1,代表着这个范围里面还是K个1，需要到0为止
                    //然后移除掉这个0（left++）
                    while(left<A.Length && A[left] == 1)
                        left ++;
                    left++;
                }
                else
                {
                    count++;
                }
            }
            right = i;
            result = Math.Max(result,right-left+1);
        }
        return result;
    }
}

public class Solution {
    //Translation: Find the longest subarray with at most k 0
    //For each A[j], try to find the longest subarray
    //if A[i] - A[j] has zero <= k, we continue to increment j
    //if A[i] - A[j] has zeros > k, we increase i
    public int LongestOnes(int[] A, int K) {
        int i = 0, j;
        
        for(j = 0; j < A.Length; j++)
        {
            if(A[j] == 0)
                K--;
            if(K < 0 && A[i++] == 0)
                K++;
        }
        
        return j - i;
    }
}
 
*/
