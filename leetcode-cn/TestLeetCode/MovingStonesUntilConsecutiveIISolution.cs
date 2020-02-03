using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
在一个长度无限的数轴上，第 i 颗石子的位置为 stones[i]。如果一颗石子的位置最小/最大，那么该石子被称作端点石子。

每个回合，你可以将一颗端点石子拿起并移动到一个未占用的位置，使得该石子不再是一颗端点石子。

值得注意的是，如果石子像 stones = [1,2,5] 这样，你将无法移动位于位置 5 的端点石子，因为无论将它移动到任何位置（例如 0 或 3），该石子都仍然会是端点石子。

当你无法进行任何移动时，即，这些石子的位置连续时，游戏结束。

要使游戏结束，你可以执行的最小和最大移动次数分别是多少？ 以长度为 2 的数组形式返回答案：answer = [minimum_moves, maximum_moves] 。

 

示例 1：

输入：[7,4,9]
输出：[1,2]
解释：
我们可以移动一次，4 -> 8，游戏结束。
或者，我们可以移动两次 9 -> 5，4 -> 6，游戏结束。
示例 2：

输入：[6,5,4,3,10]
输出：[2,3]
解释：
我们可以移动 3 -> 8，接着是 10 -> 7，游戏结束。
或者，我们可以移动 3 -> 7, 4 -> 8, 5 -> 9，游戏结束。
注意，我们无法进行 10 -> 2 这样的移动来结束游戏，因为这是不合要求的移动。
示例 3：

输入：[100,101,104,102,103]
输出：[0,0]
 

提示：

3 <= stones.length <= 10^4
1 <= stones[i] <= 10^9
stones[i] 的值各不相同。 
*/
/// <summary>
/// https://leetcode-cn.com/problems/moving-stones-until-consecutive-ii/
/// 1040. 移动石子直到连续 II
/// 
/// </summary>
class MovingStonesUntilConsecutiveIISolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int[] NumMovesStonesII(int[] stones)
    {
        int n = stones.Length;

        Array.Sort(stones);
        
        int maxMove = stones[n - 1] - stones[0] + 1 - n;
        maxMove -= Math.Min(stones[n - 1] - stones[n - 2] - 1, stones[1] - stones[0] - 1);

        int minMove = maxMove;
        int stop = 0;
        for (int start = 0; start < n; start++)
        {
            while (stop + 1 < n && stones[stop + 1] - stones[start] + 1 <= n)
                ++stop;

            int span = stop - start + 1;
            int cost = (span == n - 1 && stones[stop] - stones[start] + 1 == n - 1) ? 2 : n - span;

            if (cost < minMove) minMove = cost;
        }
        return new int[]{ minMove, maxMove};
    }
}
/*
移动石子直到连续 II
owen
2.4k 阅读
解题思路：
题目是上周第一题的扩展，但是有点不同。

由题意可知，每进行一轮操作，石子的左右端点的距离会缩短，一轮一轮收敛。最后会石子都紧邻游戏结束。

举个例子：

初始时有 8 颗石子，在数轴上的有石子的刻度为：

4，6，8，9，15，16，19，20

最大值求解方法：

石子可以放置的空间，等于左右两端石子之间的未占用位置。在例子中，一共有 20-4+1-8 个位置。

用公式表示为：

s1=stones[n−1]−stones[0]+1−n。

但是第一次移动的左端点或右端点的石子后，这个移动的石子和它相邻的那颗石子之间的空间，后面就不能被放置了，因为与他相邻的那个点变为端点，他们之间的位置不可以被放置了。

例如第一步移动了 4，那么 5 这个位置就不可能放置石子了。所以要计算不能被访问的空间

s2=min(stones[n-1]-stones[n-2]-1, stones[1]-stones[0] -1)

最大值为 s1-s2。因为在后面的步骤里，我们都可以做出策略，让每一轮左右端点的差值只减 1。

最小值求解方法：

如果最后游戏结束，那么一定有 n 个连续坐标摆满了石子。如果我们要移动最少，必定要找一个石子序列，使得在 nn 大小连续的坐标内，初始时有最多的石子。

设想有个尺子，上面有 n 个刻度点，我们用这个尺子在石子从最左边到最右边移动，每动一次都查看下在尺子范围内有 m 个石子，那么要使这个区间填满，就需要移动 n-m 次。

只要在尺子外部有石子，就有策略填满尺子内的。

这些次数中最小的就为虽少次数。

但是有一种特例：
1，2，3，4，7

这种 1-4 是最好的序列，但是 7 不能移动到端点，只能 1 先移动到 6，然后 7 移动到 5 解决，这种情况要用 2 步。就是尺子内的石子都是连续的，中间没空洞，只在边上有空，要用 2 次。

代码：
class Solution {
public:
    vector<int> numMovesStonesII(vector<int>& stones) {
        sort(stones.begin(),stones.end());
        int n = stones.size();
        int mx = stones[n - 1] - stones[0] + 1 - n;
        mx -= min(stones[n-1]-stones[n-2] - 1, stones[1]-stones[0] -1);
        int mi = mx;
        int i = 0;
        int j = 0;
        for(i = 0; i < n; ++i)
        {
            while(j + 1 < n && stones[j + 1] - stones[i] + 1 <= n)
                ++j;
            int cost = n - (j - i + 1);
            if(j - i + 1 == n - 1 && stones[j] - stones[i] + 1 == n - 1)
                cost = 2;
            mi = min(mi, cost);
        }
        return vector<int>{mi, mx};
    }
};

C++ 双指针题解
大力王
86 阅读
解题思路
参考@owen的题解

代码
class Solution {
public:
    vector<int> numMovesStonesII(vector<int>& stones) {
        if (stones.size() < 3) return {0, 0};
        sort(stones.begin(), stones.end());
        int N = stones.size();
        int head = stones[0];
        int tail = stones[N - 1];
        int S = tail - head + 1;
        if (S == N) {
            return {0, 0};
        }
        int L = stones[1] - head - 1;
        int R = tail - stones[N - 2] - 1;
        int max_step = S - N - min(L, R);
        if ((L > 1 && S - N == L) || (R > 1 && S - N == R)) {
            return {min(2, max_step), max_step};
        }
        int min_step = max_step;
        int l = 0;
        for (int i = 0; i < N; ++i) {
            while (stones[i] - stones[l] + 1 > N) {
                ++l;
            }
            min_step = min(min_step, N - (i - l + 1));
        }
        return {min_step, max_step};
    }
}; 
 
*/
