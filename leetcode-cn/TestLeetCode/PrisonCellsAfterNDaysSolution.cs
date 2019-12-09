using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
8 间牢房排成一排，每间牢房不是有人住就是空着。

每天，无论牢房是被占用或空置，都会根据以下规则进行更改：

如果一间牢房的两个相邻的房间都被占用或都是空的，那么该牢房就会被占用。
否则，它就会被空置。
（请注意，由于监狱中的牢房排成一行，所以行中的第一个和最后一个房间无法有两个相邻的房间。）

我们用以下方式描述监狱的当前状态：如果第 i 间牢房被占用，则 cell[i]==1，否则 cell[i]==0。

根据监狱的初始状态，在 N 天后返回监狱的状况（和上述 N 种变化）。

 

示例 1：

输入：cells = [0,1,0,1,1,0,0,1], N = 7
输出：[0,0,1,1,0,0,0,0]
解释：
下表概述了监狱每天的状况：
Day 0: [0, 1, 0, 1, 1, 0, 0, 1]
Day 1: [0, 1, 1, 0, 0, 0, 0, 0]
Day 2: [0, 0, 0, 0, 1, 1, 1, 0]
Day 3: [0, 1, 1, 0, 0, 1, 0, 0]
Day 4: [0, 0, 0, 0, 0, 1, 0, 0]
Day 5: [0, 1, 1, 1, 0, 1, 0, 0]
Day 6: [0, 0, 1, 0, 1, 1, 0, 0]
Day 7: [0, 0, 1, 1, 0, 0, 0, 0]

示例 2：

输入：cells = [1,0,0,1,0,0,1,0], N = 1000000000
输出：[0,0,1,1,1,1,1,0]
 

提示：

cells.length == 8
cells[i] 的值为 0 或 1 
1 <= N <= 10^9
*/
/// <summary>
/// https://leetcode-cn.com/problems/prison-cells-after-n-days/
/// 957. N 天后的牢房
/// 
/// </summary>
class PrisonCellsAfterNDaysSolution
{
    public void Test()
    {
        var ret = PrisonAfterNDays(new int[] { 1, 0, 0, 1, 0, 0, 1, 0 }, 1000000000);
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int[] PrisonAfterNDays(int[] cells, int N)
    {
        // 2 的 8次幂个状态
        var state2N = new Dictionary<int, int>(256);

        int state = 0;
        for (int i = 0; i < 8; ++i)
            if (0 < cells[i] ) state |= (1 << i);

        int cycleCount = 0;
        int n = N;
        while (0 < n)
        {
            if (state2N.ContainsKey(state))
            {
                int previousN = state2N[state];
                cycleCount = previousN - n;

                var states = state2N.Where(item => item.Value <= previousN && n < item.Value).OrderByDescending(item => item.Value).Select(item => item.Key).ToArray();

                n %= cycleCount;
                state = states[n];
                break;
            }
            
            state2N.Add(state, n);
            n--;
            state = NextDay(state);
        }

        int[] ret = new int[8];
        for (int i = 0; i < 8; ++i)
            ret[i] = ((state >> i) & 1) == 1 ? 1 : 0;

        return ret;

    }

    private static int NextDay(int state)
    {
        int ret = 0;
        for (int i = 1; i <= 6; ++i)
            if (((state >> (i - 1)) & 1) == ((state >> (i + 1)) & 1)) ret |= 1 << i;
        return ret;
    }
}
/*
方法 1：模拟
想法

模拟每一天监狱的状态。

由于监狱最多只有 256 种可能的状态，所以状态一定会快速的形成一个循环。我们可以当状态循环出现的时候记录下循环的周期 t 然后跳过 t 的倍数的天数。

算法

实现一个简单的模拟，每次迭代一天的情况。对于每一天，我们减少剩余的天数 N，然后将监狱状态改变成（state -> nextDay(state)）。

如果我们到达一个已经访问的状态，并且知道距当前过去了多久，设为 t，那么由于这是一个循环，可以让 N %= t。这确保了我们的算法只需要执行 O(2^{\text{cells.length}})O(2 
cells.length
 ) 步。

JavaPython
class Solution {
    public int[] prisonAfterNDays(int[] cells, int N) {
        Map<Integer, Integer> seen = new HashMap();

        // state  = integer representing state of prison
        int state = 0;
        for (int i = 0; i < 8; ++i) {
            if (cells[i] > 0)
                state ^= 1 << i;
        }

        // While days remaining, simulate a day
        while (N > 0) {
            // If this is a cycle, fast forward by
            // seen.get(state) - N, the period of the cycle.
            if (seen.containsKey(state)) {
                N %= seen.get(state) - N;
            }
            seen.put(state, N);

            if (N >= 1) {
                N--;
                state = nextDay(state);
            }
        }

        // Convert the state back to the required answer.
        int[] ans = new int[8];
        for (int i = 0; i < 8; ++i) {
            if (((state >> i) & 1) > 0) {
                ans[i] = 1;
            }
        }

        return ans;
    }

    public int nextDay(int state) {
        int ans = 0;

        // We only loop from 1 to 6 because 0 and 7 are impossible,
        // as those cells only have one neighbor.
        for (int i = 1; i <= 6; ++i) {
            if (((state >> (i-1)) & 1) == ((state >> (i+1)) & 1)) {
                ans ^= 1 << i;
            }
        }

        return ans;
    }
}
复杂度分析

时间复杂度：O(2^N)O(2 
N
 )，其中 NN 是监狱房间的个数。
空间复杂度：O(2^N * N)O(2 
N
 ∗N)。

public class Solution {
    public int[] PrisonAfterNDays(int[] cells, int N) {
        Dictionary<string, int> map = new Dictionary<string, int>();
        int prev =0;
        int j = 0;
        while(j < N)        
        {            
            for(int i = 0; i< cells.Length; i++)
            {
                if(i == 0 || i == cells.Length-1)
                {
                    prev = cells[i];
                    cells[i] = 0;
                }
                else{                    
                    int sum = prev + cells[i+1];
                    prev = cells[i];
                    cells[i] = sum == 1? 0:1;
                }
            }            
            string state = string.Join(",", cells);
            if(map.ContainsKey(state))
            {
                int cycle = j - map[state];
                N = (N-1)%cycle;
                j = -1; // We are doing j++ at the end so resetting it to 0
                map.Clear();
            }
            else
                map.Add(state, j);
            j++;
        }
        return cells;
    }
}

public class Solution {
    public int[] PrisonAfterNDays(int[] cells, int N) {
         N = N % 14;
        if (N == 0)
        {
            N = 14;
        }
        for (int j = 0; j < N; j++)
        {
            int[] ret = new int[8];
            ret[0] = 0;
            ret[7] = 0;
            for (int i = 1; i < 7; i++)
            {
                if (cells[i - 1] == cells[i + 1])
                {
                    ret[i] = 1;
                }
                else 
                {
                    ret[i] = 0;
                }
              
            }
            cells = ret;
        }
        return cells;
    }
}
 
*/
