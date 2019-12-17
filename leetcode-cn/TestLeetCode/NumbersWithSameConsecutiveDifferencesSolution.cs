using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
返回所有长度为 N 且满足其每两个连续位上的数字之间的差的绝对值为 K 的非负整数。

请注意，除了数字 0 本身之外，答案中的每个数字都不能有前导零。例如，01 因为有一个前导零，所以是无效的；但 0 是有效的。

你可以按任何顺序返回答案。

 

示例 1：

输入：N = 3, K = 7
输出：[181,292,707,818,929]
解释：注意，070 不是一个有效的数字，因为它有前导零。
示例 2：

输入：N = 2, K = 1
输出：[10,12,21,23,32,34,43,45,54,56,65,67,76,78,87,89,98]
 

提示：

1 <= N <= 9
0 <= K <= 9
*/
/// <summary>
/// https://leetcode-cn.com/problems/numbers-with-same-consecutive-differences/
/// 967. 连续差相同的数字
/// 
/// </summary>
class NumbersWithSameConsecutiveDifferencesSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int[] NumsSameConsecDiff(int N, int K)
    {
        if (N == 1) return new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        var ret = new List<int>();
        
        for (int i = 1; i < 10; i++) Dfs(i, i, N - 1, K, ret);

        return ret.ToArray();
    }

    private static void Dfs(int sum, int previousValue, int N, int K, List<int> ret)
    {
        if ( N <= 0 )
        {
            ret.Add(sum);
            return;
        }

        int t1 = previousValue - K;
        if (-1 < t1 && t1 < 10) Dfs(sum * 10 + t1, t1, N - 1, K, ret);

        if (0 < K)
        {
            t1 = previousValue + K;
            if (-1 < t1 && t1 < 10) Dfs(sum * 10 + t1, t1, N - 1, K, ret);
        }

        //for (int i = 0; i < 10; i++)
        //    if (previousValue - i == K || i - previousValue == K) Dfs(sum * 10 + i, i, N - 1, K, ret);
    }
}
/*

我们用队列的思想从最高位一个一个加进去，注意最高位除了N是1的情况都不可能是0。而N如果是1，那么答案必为0,1,2,3,4,5,6,7,8,9。
每次我们有两个选择，就是两个值相差为K。
代码：
class Solution {
    List<Integer> ans;
    public int[] numsSameConsecDiff(int N, int K) {
        ans = new ArrayList();
        if (N == 1) return new int[] {0,1,2,3,4,5,6,7,8,9};
        for (int i = 1; i < 10; i++) dfs(i, i, N - 1, K);
        int len = ans.size();
        int[] res = new int[len];
        for (int i = 0; i < len; i++) res[i] = ans.get(i);
        return res;
    }

    public void dfs(int val, int pre, int N, int K) {// pre 是前一位的值，val是目前的总值
        if (N <= 0) {
            ans.add(val);
            return;
        }
        for (int i = 0; i <= 9; i++) {
            if (pre - i == K || i - pre == K) { // 将当前位和前一位相比，是否相差K
                dfs(val * 10 + i, i, N - 1, K); // 符合条件就把当前位加入个位，其他位都向左移一位
            }
        }
    }
}

public class Solution {
    public int[] NumsSameConsecDiff(int N, int K) {
        if(N == 1){
            return new int[]{0,1,2,3,4,5,6,7,8,9};
        }
        List<int> tempList = new List<int>();
        for(int i =1;i <= 9;i++){
            AddNum(i,K,1,tempList,N);
        }
        int[] result = new int[tempList.Count];
        for(int i =0;i < result.Length;i++){
            result[i] = tempList[i];
        }
        return result;
    }
    
    public void AddNum(int num,int val,int j,List<int> list,int N){
        if(j == N){
            list.Add(num);
            return;
        }
        if (AddNumAble(num % 10, val))
        {
            int temp = num * 10;
            temp += (num % 10) + val;
            AddNum(temp, val, j + 1, list, N);
        }
        if (val != 0)
        {
            if (AddNumAble(num % 10, -val))
            {
                int temp = num * 10;
                temp += (num % 10) - val;
                AddNum(temp, -val, j + 1, list, N);
            }
        }
    }
    
    public bool AddNumAble(int num,int val){
        if (val > 0)
        {
            if (num + val > 9)
            {
                return false;
            }
        }
        else
        {
            if (num + val < 0)
            {
                return false;
            }
        }
        return true;
    }
} 
*/
