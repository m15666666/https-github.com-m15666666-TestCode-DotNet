using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
根据每日 气温 列表，请重新生成一个列表，对应位置的输入是你需要再等待多久温度才会升高超过该日的天数。如果之后都不会升高，请在该位置用 0 来代替。

例如，给定一个列表 temperatures = [73, 74, 75, 71, 69, 72, 76, 73]，你的输出应该是 [1, 1, 4, 2, 1, 1, 0, 0]。

提示：气温 列表长度的范围是 [1, 30000]。每个气温的值的均为华氏度，都是在 [30, 100] 范围内的整数。
*/
/// <summary>
/// https://leetcode-cn.com/problems/daily-temperatures/
/// 739. 每日温度
/// 
/// </summary>
class DailyTemperaturesSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int[] DailyTemperatures(int[] T)
    {
        int[] ret = new int[T.Length];
        var stack = new Stack<int>();
        for (int i = T.Length - 1; -1 < i; i--)
        {
            var v = T[i];
            while (0 < stack.Count && T[stack.Peek()] <= v )
                stack.Pop();

            if (stack.Count == 0) ret[i] = 0;
            else ret[i] = stack.Peek() - i;

            stack.Push(i);
        }
        return ret;
    }
}
/*
public class Solution {
    public int[] DailyTemperatures(int[] T) {
      Stack<int> stack = new Stack<int>();

       int size = T.Length;
       int[] result = new int[size];

       for (int i = 0; i < size; i++)
       {
           while (stack.Count!=0 && T[stack.Peek()] < T[i])
           {
               int t = stack.Peek();
               stack.Pop();
               result[t] = i - t;
           }
           stack.Push(i);
       }

       return result;
    }
} 
public class Solution {
    public int[] DailyTemperatures(int[] T) {
        int[] result = new int[T.Length];
        Stack<int> tempIDStack = new Stack<int>();
        for (int i = 0; i < T.Length; i++)
        {
            if (tempIDStack.Count > 0)
            {
                while (T[i] > T[tempIDStack.Peek()])
                {
                    result[tempIDStack.Peek()] = i - tempIDStack.Pop();
                    if (0 == tempIDStack.Count)
                        break;
                }
                tempIDStack.Push(i);
            }
            else
            {
                tempIDStack.Push(i);
            }
        }

        return result;
    }
}
*/
