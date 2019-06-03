using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
已有方法 rand7 可生成 1 到 7 范围内的均匀随机整数，试写一个方法 rand10 生成 1 到 10 范围内的均匀随机整数。

不要使用系统的 Math.random() 方法。

 

示例 1:

输入: 1
输出: [7]
示例 2:

输入: 2
输出: [8,4]
示例 3:

输入: 3
输出: [8,1,10]
 

提示:

rand7 已定义。
传入参数: n 表示 rand10 的调用次数。
 

进阶:

rand7()调用次数的 期望值 是多少 ?
你能否尽量少调用 rand7() ? 
*/
/// <summary>
/// https://leetcode-cn.com/problems/implement-rand10-using-rand7/
/// 470. 用 Rand7() 实现 Rand10()
/// https://blog.csdn.net/baidu_36094751/article/details/81284146
/// </summary>
class ImplementRand10UsingRand7Solution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int Rand10()
    {
        int row, col, idx;
        do
        {

            row = Rand7();

            col = Rand7();

            idx = col + (row - 1) * 7;

        } while (idx > 40);

        return 1 + (idx - 1) % 10;
    }

    private Random _random = new Random();
    private int Rand7() { return _random.Next(1,7); }
}
/*
public class Solution : SolBase {
    public int Rand10() {
        int first;
        do{
            first = Rand7();
        }while(first == 7);
            
        first = first % 2 == 0 ? 5 : 0;
        int second;
        do{
            second = Rand7();
        }while(second > 5);
        return first + second;
    }
} 
*/
