using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
初始时有 n 个灯泡关闭。 第 1 轮，你打开所有的灯泡。 第 2 轮，每两个灯泡你关闭一次。 第 3 轮，每三个灯泡切换一次开关（如果关闭则开启，如果开启则关闭）。第 i 轮，每 i 个灯泡切换一次开关。 对于第 n 轮，你只切换最后一个灯泡的开关。 找出 n 轮后有多少个亮着的灯泡。

示例:

输入: 3
输出: 1 
解释: 
初始时, 灯泡状态 [关闭, 关闭, 关闭].
第一轮后, 灯泡状态 [开启, 开启, 开启].
第二轮后, 灯泡状态 [开启, 关闭, 开启].
第三轮后, 灯泡状态 [开启, 关闭, 关闭]. 

你应该返回 1，因为只有一个灯泡还亮着。
*/
/// <summary>
/// https://leetcode-cn.com/problems/bulb-switcher/
/// 319. 灯泡开关
/// https://blog.csdn.net/xuxuxuqian1/article/details/81516081
/// http://www.dongcoder.com/detail-1112245.html
/// https://www.jianshu.com/p/94b404e174bd
/// </summary>
class BulbSwitcherSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int BulbSwitch(int n)
    {
        if ( n < 1 ) return 0;
        return (int)Math.Sqrt(n);//本题最后转化为求小于等于n的平方数的个数
    }
}
/*
public class Solution {
    public int BulbSwitch(int n) {
        int i;
        
        if(n == 0)
        {
            return 0;
        }
        else
        {
            for(i = 0; i <=n ; i++)
            {
                if(i*i > n)
                {
                    break;
                }
                else
                {
                    continue;
                }
            }
            return i-1;
        }
    }
}
public class Solution {
    public int BulbSwitch(int n) {
        int i =0;
        if(n==0)
        {
            return 0; 
        }
        else 
        {
            for(i=1;i*i<=n;i++)
            {
                
            }
            
        }
        return i-1;
    }
}
public class Solution {
    public int BulbSwitch(int n) {
       return (int)(Math.Sqrt(n));
    }
}
*/
