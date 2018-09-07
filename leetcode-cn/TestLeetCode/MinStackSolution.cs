using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// 设计一个支持 push，pop，top 操作，并能在常数时间内检索到最小元素的栈。
/// </summary>
class MinStackSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    /** initialize your data structure here. */
    public MinStackSolution()
    {
        
    }

    private List<int> InnerStack { get; } = new List<int>();
    //private SortedSet<int> SortedSet { get; } = new SortedSet<int>();
    private SortedDictionary<int, int> SortedDictionary = new SortedDictionary<int, int>();

    public void Push(int x)
    {
        InnerStack.Insert(0,x);
        if (SortedDictionary.ContainsKey(x)) SortedDictionary[x]++;
        else SortedDictionary[x] = 1;
    }

    public void Pop()
    {
        var top = InnerStack[0];

        SortedDictionary[top]--;
        if (SortedDictionary[top] == 0) SortedDictionary.Remove(top);

        InnerStack.RemoveAt(0);
    }

    public int Top()
    {
        return InnerStack[0];
    }

    public int GetMin()
    {
        return 0 < SortedDictionary.Count ?  SortedDictionary.First().Key : 0;
    }
}