using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
现有一个房间，墙上挂有 n 只已经打开的灯泡和 4 个按钮。在进行了 m 次未知操作后，你需要返回这 n 只灯泡可能有多少种不同的状态。

假设这 n 只灯泡被编号为 [1, 2, 3 ..., n]，这 4 个按钮的功能如下：

将所有灯泡的状态反转（即开变为关，关变为开）
将编号为偶数的灯泡的状态反转
将编号为奇数的灯泡的状态反转
将编号为 3k+1 的灯泡的状态反转（k = 0, 1, 2, ...)
示例 1:

输入: n = 1, m = 1.
输出: 2
说明: 状态为: [开], [关]
示例 2:

输入: n = 2, m = 1.
输出: 3
说明: 状态为: [开, 关], [关, 开], [关, 关]
示例 3:

输入: n = 3, m = 1.
输出: 4
说明: 状态为: [关, 开, 关], [开, 关, 开], [关, 关, 关], [关, 开, 开].
注意： n 和 m 都属于 [0, 1000]. 
*/
/// <summary>
/// https://leetcode-cn.com/problems/bulb-switcher-ii/
/// 672. 灯泡开关 Ⅱ
/// https://blog.csdn.net/xuxuxuqian1/article/details/81516163
/// https://blog.csdn.net/start_lie/article/details/83903071
/// </summary>
class BulbSwitcherIISolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int FlipLights(int n, int m)
    {
        // 六个灯为一组，后六个与前六个变化一致，但要保留最后六的余数的灯
        n = (n <= 6) ? n : (n % 6 + 6);

        // 初始状态全亮
        int startState = (1 << n) - 1;

        HashSet<int> set = new HashSet<int>();
        Queue<int> queue = new Queue<int>();
        queue.Enqueue(startState);
        for (int i = 0; i < m; ++i)
        {
            int size = queue.Count;
            set.Clear();
            for (int j = 0; j < size; ++j)
            {
                int state = queue.Dequeue();
                List<int> next = new List<int>{ FlipAll(state, n), FlipOdd(state, n), FlipEven(state, n), Flip3k1(state, n)};
                foreach (var num in next)
                {
                    if (set.Contains(num)) continue;
                    queue.Enqueue(num);
                    set.Add(num);
                }
            }
        }
        return queue.Count;
    }

    private static int FlipAll(int state, int n)
    {
        int x = (1 << n) - 1;
        return state ^ x;
    }

    private static int FlipOdd(int state, int n)
    {
        for (int i = 0; i < n; i += 2)
        {
            state ^= (1 << i);
        }
        return state;
    }

    private static int FlipEven(int state, int n)
    {
        for (int i = 1; i < n; i += 2)
        {
            state ^= (1 << i);
        }
        return state;
    }

    private static int Flip3k1(int state, int n)
    {
        for (int i = 0; i < n; i += 3)
        {
            state ^= (1 << i);
        }
        return state;
    }
}
/*
public class Solution {
    public int FlipLights(int n, int m) {
        if(n==0)
            return 0;
        if(m==0)
            return 1;
        if(n==1)
            return 2;
        if(n==2)
        {
            if(m==1)
                return 3;
            else return 4;
        }
        if(m==1)
            return 4;
        if(m==2)
            return 7;
        if(m%2==0)
            m=4;
        else
            m=3;
        if(m==3)
            return 8;
        if(m==4)
            return 8;
        return 0;
    }
} 
public class Solution {
    public int FlipLights(int n, int m) {
        if(n==0)
            return 0;
        if(m==0)
            return 1;
        if(n==1)
            return 2;
        if(n==2)
        {
            if(m==1)
                return 3;
            else return 4;
        }
        if(m==1)
            return 4;
        if(m==2)
            return 7;
        return 8;
        
    }
}  
*/
