using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给你一份工作时间表 hours，上面记录着某一位员工每天的工作小时数。

我们认为当员工一天中的工作小时数大于 8 小时的时候，那么这一天就是「劳累的一天」。

所谓「表现良好的时间段」，意味在这段时间内，「劳累的天数」是严格 大于「不劳累的天数」。

请你返回「表现良好时间段」的最大长度。

 

示例 1：

输入：hours = [9,9,6,0,6,6,9]
输出：3
解释：最长的表现良好时间段是 [9,9,6]。
 

提示：

1 <= hours.Length <= 10000
0 <= hours[i] <= 16
*/
/// <summary>
/// https://leetcode-cn.com/problems/longest-well-performing-interval/
/// 1124. 表现良好的最长时间段
/// 
/// </summary>
class LongestWellPerformingIntervalSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int LongestWPI(int[] hours)
    {
        //for (int i = 0; i < hours.Length; i++)
        //    hours[i] = hours[i] > 8 ? 1 : -1;
        int[] score = new int[hours.Length + 1];
        score[0] = 0;
        for (int i = 1; i < score.Length; i++)
            score[i] = score[i - 1] + (8 < hours[i - 1] ? 1 : -1);

        if (0 < score[score.Length - 1]) return hours.Length;

        Stack<int> stack = new Stack<int>();
        for (int i = 0; i < score.Length; i++)
        {
            if (0 < stack.Count && score[stack.Peek()] <= score[i]) continue;

            stack.Push(i);
        }
        int ret = 0;
        for (int i = score.Length - 1; i >= 0; i--)
        {
            if (score[i] > score[stack.Peek()])
            {
                ret = Math.Max(ret, i - stack.Peek());
                stack.Pop(); 
                i++;

                if (stack.Count == 0) break;
                continue;
            }
        }
        return ret;
    }
}
/*
JAVA 单调栈
Chadriy
发布于 1 个月前
451 阅读
确实很难理解的单调栈

class Solution {
    public int longestWPI(int[] hours) {
        for (int i = 0; i < hours.length; i++) {
                hours[i] = hours[i]>8?1:-1;
        }
        int[] score =new int[hours.length+1];score[0]=0;
        for (int i = 1; i < score.length; i++) {
            score[i]=score[i-1]+hours[i-1];
        }
        //递减栈
        Stack<Integer> stack =new Stack<>();
        for (int i = 0; i < score.length; i++) {
            if(!stack.empty()&&score[stack.peek()]<=score[i])
                continue;
            stack.push(i);
        }
        int ans = 0;
        for (int i = score.length-1; i >= 0; i--) {
            if(score[i]>score[stack.peek()]){
                ans=Math.max(ans,i-stack.peek());
                stack.pop();i++;
                if(stack.empty()) break;
                continue;
            }
        }
        return ans;
    } 
}
下一篇：别跟老夫提什么单调栈，就是查字典

前缀和+单调栈 Python3
刘岳
发布于 7 个月前
7.1k 阅读
思路：

本题有多种方法，暴力的方法时间复杂度是 O(N^2)O(N 
2
 )，用二分法可以将时间复杂度降为 O(NlogN)O(NlogN) ，下面介绍用单调栈可以实现 O(N)O(N) 时间复杂度的方法。其实本题变形后与 962. 最大宽度坡 类似。

以输入样例 hours = [9,9,6,0,6,6,9] 为例，我们将大于 88 小时的一天记为 11 分，小于等于 88 小时的一天记为 -1−1 分。那么处理后，我们得到 score = [1, 1, -1, -1, -1, -1, 1]，然后我们对得分数组计算前缀和 presum = [0, 1, 2, 1, 0, -1, -2, -1]。题目要求返回表现良好时间段的最大长度，即求最长的一段中，得分 11 的个数大于得分 -1−1 的个数，也就是求 score 数组中最长的一段子数组，其和大于 00，那么也就是找出前缀和数组 presum 中两个索引 i 和 j，使 j - i 最大，且保证 presum[j] - presum[i] 大于 00。到此，我们就将这道题转化为，求 presum 数组中的一个最长的上坡，可以用单调栈实现。我们维护一个单调栈，其中存储 presum 中的元素索引，栈中索引指向的元素严格单调递减，由 presum 数组求得单调栈为 stack = [0, 5, 6]， 其表示元素为 [0, -1, -2]。然后我们从后往前遍历 presum 数组，与栈顶索引指向元素比较，如果相减结果大于 00，则一直出栈，直到不大于 00 为止，然后更新当前最大宽度。

图解：

图解

代码：

class Solution:
    def longestWPI(self, hours: List[int]) -> int:
        n = len(hours)
        # 大于8小时计1分 小于等于8小时计-1分
        score = [0] * n
        for i in range(n):
            if hours[i] > 8:
                score[i] = 1
            else:
                score[i] = -1
        # 前缀和
        presum = [0] * (n + 1)
        for i in range(1, n + 1):
            presum[i] = presum[i - 1] + score[i - 1]
        ans = 0
        stack = []
        # 顺序生成单调栈，栈中元素从第一个元素开始严格单调递减，最后一个元素肯定是数组中的最小元素所在位置
        for i in range(n + 1):
            if not stack or presum[stack[-1]] > presum[i]:
                stack.append(i)
        # 倒序扫描数组，求最大长度坡
        i = n
        while i > ans:
            while stack and presum[stack[-1]] < presum[i]:
                ans = max(ans, i - stack[-1])
                stack.pop()
            i -= 1
        return ans
下一篇：参考了几个大神的题解之后总结下来非常详细的解题思路, 希望大家少走些弯路.


别跟老夫提什么单调栈，就是查字典
嘿嘿
发布于 2 个月前
215 阅读
用一个 cur 变量记录前缀和，当大于8时，cur++, 小于8时，cur--。
由于从前向后遍历，当 cur > 0时，说明从开始到现在满足条件，时间必然是最长的，直接更新 res = i + 1。
当 cur <= 0时呢？关键来了
这里用一个 字典记录所有 cur <= 0的最小下标，所谓最小，就是后面如果再碰到了同样的 cur，不需要更新，如果没有碰到过，则把这个下标记录下来。
然后用 cur - 1 去字典里找，如果找到了下标j，那么就说明从0到 j 的前缀和是 cur-1，而从0到 i 的前缀和是 cur，那么显然从 j 到 i的和是（cur - (cur - 1)） = 1 > 0，也就是说从 j+1到 i 的表现肯定是满足的，并且由于 j 是 cur-1中最小的，所以 i-j 是最大的。
此时再跟 res 比较看是否需要更新。

上面为什么只需要查找 cur-1？因为满足条件的前缀和只能是小于等于cur-1的，也就是说其实也可以查找 cur-2,cur-3...，但是，cur-2的下标一定不可能在 cur-1的下标左边。使用反证法，前提是cur-1代表的是最小下标，那么如果 cur-2在 cur-1左边，而cur-2的左边一定还会有 cur-1出现（cur值是从0开始的），这就和最小下标的前提矛盾了。
那么问题又来了，如果 cur-1不存在，是否要查找 cur-2,cur-3...呢？
也不需要，思路跟上面是一样的，如果 cur-1不存在，cur-2,cur-3...一定也不存在。举个例子，不可能从0跳到-2，-3，而中间没有-1。

通过上面有理有据的分析，下面的代码就很简单了。

class Solution {
public:
    int longestWPI(vector<int>& hours) {
        int n = hours.size();

        unordered_map<int, int> count;
        int cur = 0;
        int res = 0;
        for (int i = 0; i < n; ++i) {
            if (hours[i] > 8) {
                cur++;
            } else {
                cur--;
            }
            if (cur > 0) res = i + 1;
            else {
                if (count.count(cur-1) > 0) res = max(res, i - count[cur-1]);
                if (count.count(cur) < 1) count[cur] = i;
            }
        }
        return res;

    }
};
下一篇：单调栈

 
*/
