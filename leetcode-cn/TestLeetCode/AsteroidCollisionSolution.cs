using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个整数数组 asteroids，表示在同一行的行星。

对于数组中的每一个元素，其绝对值表示行星的大小，正负表示行星的移动方向（正表示向右移动，负表示向左移动）。每一颗行星以相同的速度移动。

找出碰撞后剩下的所有行星。碰撞规则：两个行星相互碰撞，较小的行星会爆炸。如果两颗行星大小相同，则两颗行星都会爆炸。两颗移动方向相同的行星，永远不会发生碰撞。

示例 1:

输入: 
asteroids = [5, 10, -5]
输出: [5, 10]
解释: 
10 和 -5 碰撞后只剩下 10。 5 和 10 永远不会发生碰撞。
示例 2:

输入: 
asteroids = [8, -8]
输出: []
解释: 
8 和 -8 碰撞后，两者都发生爆炸。
示例 3:

输入: 
asteroids = [10, 2, -5]
输出: [10]
解释: 
2 和 -5 发生碰撞后剩下 -5。10 和 -5 发生碰撞后剩下 10。
示例 4:

输入: 
asteroids = [-2, -1, 1, 2]
输出: [-2, -1, 1, 2]
解释: 
-2 和 -1 向左移动，而 1 和 2 向右移动。
由于移动方向相同的行星不会发生碰撞，所以最终没有行星发生碰撞。
说明:

数组 asteroids 的长度不超过 10000。
每一颗行星的大小都是非零整数，范围是 [-1000, 1000] 。 
*/
/// <summary>
/// https://leetcode-cn.com/problems/asteroid-collision/
/// 735. 行星碰撞
/// https://blog.csdn.net/qiang_____0712/article/details/84900857
/// </summary>
class AsteroidCollisionSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int[] AsteroidCollision(int[] asteroids)
    {
        if (asteroids == null || asteroids.Length == 0) return new int[0];

        Stack<int> stack = new Stack<int>();
        foreach( var v in asteroids)
        {
            if (stack.Count == 0) //如果栈为空，就入栈
            {
                stack.Push(v);
                continue;
            }

            var top1 = stack.Peek();
            if ((top1 < 0 && v > 0) || (top1 * v > 0)) //如果当前行星和栈顶行星相背或同向运行，就入栈
            {
                stack.Push(v);
                continue;
            }

            var absCurrent = Math.Abs(v);
            var absTop1 = Math.Abs(top1);
            if (absTop1 == absCurrent) //如果当前行星和栈顶行星相向而行且大小相同，同时爆炸
            {
                stack.Pop();
                continue;
            }

            if (absCurrent < absTop1) continue;

            stack.Pop(); // 如果当前行星和栈顶行星相向而行且栈顶小于当前,则弹出栈顶

            bool flag = true; //flag标识当前行星是否会爆炸
            while ( 0 < stack.Count )
            {
                var top2 = stack.Peek();
                if (top2 <= 0) break;

                var absTop2 = Math.Abs(top2);
                if (absCurrent < absTop2) break;

                if (absCurrent == absTop2)
                {
                    flag = false;
                    stack.Pop();
                    break;
                }
                stack.Pop(); // 如果当前行星和栈顶行星相向而行且栈顶小于当前,则弹出栈顶
            }

            if (flag && (stack.Count == 0 || (stack.Peek() * v > 0))) stack.Push(v);
        }

        int[] ret = new int[stack.Count];
        for (int i = ret.Length - 1; -1 < i; i--)
        {
            ret[i] = stack.Pop();
        }
        return ret;
    }
}
/*
public class Solution {
    public int[] AsteroidCollision(int[] asteroids) {
        Stack<int> stack=new Stack<int>();
        
        for(int i=0;i<asteroids.Length;i++)
        {
            if(stack.Count!=0)
            {
                if(asteroids[i]<0)
                {
                    if(stack.Peek()>0)
                    {
                        if(Math.Abs(asteroids[i])<stack.Peek())
                        {
                            continue;
                        }
                        
                        if(Math.Abs(asteroids[i])==stack.Peek())
                        {
                            stack.Pop();
                            continue;
                        }
                        
                        if(Math.Abs(asteroids[i])>stack.Peek())
                        {
                            stack.Pop();
                            i--;
                            continue;
                        }
                    }
                    else
                    {
                        stack.Push(asteroids[i]);
                    }
                }
                else
                {
                    stack.Push(asteroids[i]);
                }
            }
            else
            {
                stack.Push(asteroids[i]);
            }
        }
        
        if(stack.Count==0)
            return new int[]{};            
        
        int[] arr=new int[stack.Count];
        for(int i=arr.Length-1;i>=0;i--)
        {
            arr[i]=stack.Pop();
        }
        
        return arr;
    }
}

*/
