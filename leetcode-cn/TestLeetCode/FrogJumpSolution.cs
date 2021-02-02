using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
一只青蛙想要过河。 假定河流被等分为 x 个单元格，并且在每一个单元格内都有可能放有一石子（也有可能没有）。 青蛙可以跳上石头，但是不可以跳入水中。

给定石子的位置列表（用单元格序号升序表示）， 请判定青蛙能否成功过河（即能否在最后一步跳至最后一个石子上）。 开始时， 青蛙默认已站在第一个石子上，并可以假定它第一步只能跳跃一个单位（即只能从单元格1跳至单元格2）。

如果青蛙上一步跳跃了 k 个单位，那么它接下来的跳跃距离只能选择为 k - 1、k 或 k + 1个单位。 另请注意，青蛙只能向前方（终点的方向）跳跃。

请注意：

石子的数量 ≥ 2 且 < 1100；
每一个石子的位置序号都是一个非负整数，且其 < 231；
第一个石子的位置永远是0。
示例 1:

[0,1,3,5,6,8,12,17]

总共有8个石子。
第一个石子处于序号为0的单元格的位置, 第二个石子处于序号为1的单元格的位置,
第三个石子在序号为3的单元格的位置， 以此定义整个数组...
最后一个石子处于序号为17的单元格的位置。

返回 true。即青蛙可以成功过河，按照如下方案跳跃： 
跳1个单位到第2块石子, 然后跳2个单位到第3块石子, 接着 
跳2个单位到第4块石子, 然后跳3个单位到第6块石子, 
跳4个单位到第7块石子, 最后，跳5个单位到第8个石子（即最后一块石子）。
示例 2:

[0,1,2,3,4,8,9,11]

返回 false。青蛙没有办法过河。 
这是因为第5和第6个石子之间的间距太大，没有可选的方案供青蛙跳跃过去。

*/
/// <summary>
/// https://leetcode-cn.com/problems/frog-jump/
/// 403. 青蛙过河
/// 
/// 
/// </summary>
class FrogJumpSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public bool CanCross(int[] stones) {
        const int FirstStone = 0;
        const int FirstGap = 0;
        int len = stones.Length;
        int destStone = stones[len - 1];
        var map = new Dictionary<int, HashSet<int>>(len);
        foreach(var stone in stones) map[stone] = new HashSet<int>();
        map[FirstStone].Add(FirstGap);
        foreach (int stone in stones)
            foreach (int gap in map[stone])
                for (int nextGap = gap - 1, nextStone = stone + nextGap; nextGap <= gap + 1; nextGap++, nextStone++)
                    if (0 < nextGap && map.ContainsKey(nextStone) && !map[nextStone].Contains(nextGap))
                    {
                        if (destStone == nextStone) return true;
                        map[nextStone].Add(nextGap);
                    }
        return false;
    }

}
/*
青蛙过河
力扣 (LeetCode)

发布于 2019-07-01
14.5k
题述
给定石子的位置列表（用单元格序号升序表示），请判定青蛙能否成功过河（即能否在最后一步跳至最后一个石子上）。 开始时， 青蛙默认已站在第一个石子上，并可以假定它第一步只能跳跃一个单位（即只能从单元格 1 跳至单元格 2）。如果青蛙上一步跳跃了 k 个单位，那么它接下来的跳跃距离只能选择为 k - 1、k 或 k + 1个单位。

方法一： 暴力 【超时】
在暴力方法中，我们定义一个递归方法 canCrosscanCross，这个方法的输入参数为石子列表，当前位置和当前的 jumpsizejumpsize。算法从 currentPosition=0currentPosition=0，jumpsize=0jumpsize=0 开始，接下来每一次调用，从 currentPositioncurrentPosition 开始，检查在位置 (currentPostion + newjumpsize)(currentPostion+newjumpsize) 处有没有石子，其中 newjumpsizenewjumpsize 值依次为 jumpsizejumpsize， jumpsize+1jumpsize+1，jumpsize-1jumpsize−1。在这里我们直接遍历数组来检查特定位置上有没有石子。如果有一个石子的话，就再次调用递归方法，传入同样的石子列表，新的 currentPositioncurrentPosition 和 newjumpsizenewjumpsize。如果在经过数次递归之后抵达了终点的那块石头，返回 true。


public class Solution {
    public boolean canCross(int[] stones) {
        return can_Cross(stones, 0, 0);
    }
    public boolean can_Cross(int[] stones, int ind, int jumpsize) {
        for (int i = ind + 1; i < stones.length; i++) {
            int gap = stones[i] - stones[ind];
            if (gap >= jumpsize - 1 && gap <= jumpsize + 1) {
                if (can_Cross(stones, i, gap)) {
                    return true;
                }
            }
        }
        return ind == stones.length - 1;
    }
}
复杂度分析

时间复杂度： O(3^n)O(3 
n
 )。
递归树每层三个节点，最多 nn 层。

空间复杂度： O(n)O(n)
递归深度为 nn。

方法二 优雅地暴力 【超时】
算法

在前面的暴力算法中，在检查位置 (currentPosition + new jumpsize)(currentPosition+newjumpsize) 处有没有石头的时候，我们对整个数组做了遍历。其实这一步是可以优化二分搜索的。


public class Solution {
    public boolean canCross(int[] stones) {
        return can_Cross(stones, 0, 0);
    }
    public boolean can_Cross(int[] stones, int ind, int jumpsize) {
        if (ind == stones.length - 1) {
            return true;
        }
        int ind1 = Arrays.binarySearch(stones, ind + 1, stones.length, stones[ind] + jumpsize);
        if (ind1 >= 0 && can_Cross(stones, ind1, jumpsize)) {
            return true;
        }
        int ind2 = Arrays.binarySearch(stones, ind + 1, stones.length, stones[ind] + jumpsize - 1);
        if (ind2 >= 0 && can_Cross(stones, ind2, jumpsize - 1)) {
            return true;
        }
        int ind3 = Arrays.binarySearch(stones, ind + 1, stones.length, stones[ind] + jumpsize + 1);
        if (ind3 >= 0 && can_Cross(stones, ind3, jumpsize + 1)) {
            return true;
        }
        return false;
    }
}
复杂度分析

时间复杂度： O(3^n)O(3 
n
 )。
递归树每层三个节点，最多 nn 层。

空间复杂度： O(n)O(n)
递归深度为 nn。

方法三： 记忆化搜索【通过】
算法

前面的算法还有一个问题，那就是可能会多次调用同一个（即输入完全相同）方法。例如，在一次调用中，jumpsizejumpsize 为 nn，这次调用可能发生在某次 jumpsizejumpsize 为 n-1n−1，nn，n+1n+1 的之后。因此，实际运行的过程中可能重复执行了很多方法。为了防止重复执行，我们可以使用记忆化搜索。首先创建一个二维数组 memomemo，所有值都初始化为 -1−1。用这个数组去存储特定的 currentIndexcurrentIndex，jumpsizejumpsize 下的产生结果。这样一来，如果调用一样的 currentIndexcurrentIndex，jumpsizejumpsize，直接返回 memomemo 数组中对应的结果就可以了。这其实是在对搜索树进行高效剪枝。


public class Solution {
    public boolean canCross(int[] stones) {
        int[][] memo = new int[stones.length][stones.length];
        for (int[] row : memo) {
            Arrays.fill(row, -1);
        }
        return can_Cross(stones, 0, 0, memo) == 1;
    }
    public int can_Cross(int[] stones, int ind, int jumpsize, int[][] memo) {
        if (memo[ind][jumpsize] >= 0) {
            return memo[ind][jumpsize];
        }
        for (int i = ind + 1; i < stones.length; i++) {
            int gap = stones[i] - stones[ind];
            if (gap >= jumpsize - 1 && gap <= jumpsize + 1) {
                if (can_Cross(stones, i, gap, memo) == 1) {
                    memo[ind][gap] = 1;
                    return 1;
                }
            }
        }
        memo[ind][jumpsize] = (ind == stones.length - 1) ? 1 : 0;
        return memo[ind][jumpsize];
    }
}
复杂度分析

时间复杂度 O(n^3)O(n 
3
 )
记忆化搜索可以把时间复杂度降到 O(n^3)O(n 
3
 )。

空间复杂度 O(n^2)O(n 
2
 )
memomemo 数组大小为 n^2n 
2
 。

方法四 记忆化搜索 + 二分搜索 【通过】
算法

我们可以继续优化上述记忆化搜索算法，用二分搜索来查找在位置 currentPostion + newjumpsizecurrentPostion+newjumpsize 处是否有石头。


public class Solution {
    public boolean canCross(int[] stones) {
        int[][] memo = new int[stones.length][stones.length];
        for (int[] row : memo) {
            Arrays.fill(row, -1);
        }
        return can_Cross(stones, 0, 0, memo) == 1;
    }
    public int can_Cross(int[] stones, int ind, int jumpsize, int[][] memo) {
        if (memo[ind][jumpsize] >= 0) {
            return memo[ind][jumpsize];
        }
        int ind1 = Arrays.binarySearch(stones, ind + 1, stones.length, stones[ind] + jumpsize);
        if (ind1 >= 0 && can_Cross(stones, ind1, jumpsize, memo) == 1) {
            memo[ind][jumpsize] = 1;
            return 1;
        }
        int ind2 = Arrays.binarySearch(stones, ind + 1, stones.length, stones[ind] + jumpsize - 1);
        if (ind2 >= 0 && can_Cross(stones, ind2, jumpsize - 1, memo) == 1) {
            memo[ind][jumpsize - 1] = 1;
            return 1;
        }
        int ind3 = Arrays.binarySearch(stones, ind + 1, stones.length, stones[ind] + jumpsize + 1);
        if (ind3 >= 0 && can_Cross(stones, ind3, jumpsize + 1, memo) == 1) {
            memo[ind][jumpsize + 1] = 1;
            return 1;
        }
        memo[ind][jumpsize] = ((ind == stones.length - 1) ? 1 : 0);
        return memo[ind][jumpsize];
    }
}
复杂度分析

时间复杂度： O\big(n^2 log(n)\big)O(n 
2
 log(n))
递归次数为 (O(n^2))(O(n 
2
 ))，每次遍历中二分搜索的复杂度为 log(n)log(n)。

空间复杂度： O(n^2)O(n 
2
 )
memomemo 数组的大小为 n^2n 
2
 。

方法五 动态规划 【通过】
算法

在动态规划方法中，我们会利用散列表 mapmap，对于散列表中的 key:valuekey:value，keykey 表示当前石头的位置，valuevalue 是一个包含 jumpsizejumpsize 的集合，其中每个 jumpsizejumpsize 代表可以通过大小为 jumpysizejumpysize 的一跳到达当前位置。首先我们对散列表初始化，keykey 为所有石头的位置，除了位置 0 对应的 valuevalue 为包含一个值 0 的集合以外，其余都初始化为空集。接下来，依次遍历每个位置上的石头。对于每个 currentPositioncurrentPosition，遍历 valuevalue 中每个 jumpsizejumpsize，判断位置 currentPosition + newjumpsizecurrentPosition+newjumpsize 是否存在于 mapmap 中，对于每个 jumpsizejumpsize，newjumpsizenewjumpsize 分别为 jumpsize-1jumpsize−1，jumpsizejumpsize，jumpsize+1jumpsize+1。如果找到了，就在对应的 valuevalue 集合里新增 newjumpsizenewjumpsize。重复这个过程直到结束。如果在结束的时候，最后一个位置对应的集合非空，那也就意味着我们可以到达终点，如果还是空集那就意味着不能到达终点。

为了帮助你能更好地理解这一过程，可以看下面这些动画。




public class Solution {
    public boolean canCross(int[] stones) {
        HashMap<Integer, Set<Integer>> map = new HashMap<>();
        for (int i = 0; i < stones.length; i++) {
            map.put(stones[i], new HashSet<Integer>());
        }
        map.get(0).add(0);
        for (int i = 0; i < stones.length; i++) {
            for (int k : map.get(stones[i])) {
                for (int step = k - 1; step <= k + 1; step++) {
                    if (step > 0 && map.containsKey(stones[i] + step)) {
                        map.get(stones[i] + step).add(step);
                    }
                }
            }
        }
        return map.get(stones[stones.length - 1]).size() > 0;
    }
}
复杂度分析

时间复杂度： O(n^2)O(n 
2
 )
重点在于嵌套的两层循环。

空间复杂度： O(n^2)O(n 
2
 )
hashmaphashmap 大小最大为 n^2n 
2
 。

public class Solution {
    public bool CanCross(int[] stones) {
        Dictionary<int, HashSet<int>> map = new Dictionary<int, HashSet<int>>();
        for(int i = 0; i < stones.Length; i++){
            map.Add(stones[i], new HashSet<int>());
        }
        map[0].Add(0);
        for(int i = 0 ;i < stones.Length; i++){
            foreach(int step in map[stones[i]]){
                for(int next = step-1; next <= step+1; next++){
                    if(next > 0 && map.ContainsKey(stones[i] + next)){
                        map[stones[i]+next].Add(next);
                    }
                }
            }
        }
        return map[stones[^1]].Count > 0;
    }
}

*/
