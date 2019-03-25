using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
给定一个嵌套的整型列表。设计一个迭代器，使其能够遍历这个整型列表中的所有整数。

列表中的项或者为一个整数，或者是另一个列表。

示例 1:

输入: [[1,1],2,[1,1]]
输出: [1,1,2,1,1]
解释: 通过重复调用 next 直到 hasNext 返回false，next 返回的元素的顺序应该是: [1,1,2,1,1]。
示例 2:

输入: [1,[4,[6]]]
输出: [1,4,6]
解释: 通过重复调用 next 直到 hasNext 返回false，next 返回的元素的顺序应该是: [1,4,6]。 
*/
/// <summary>
/// https://leetcode-cn.com/problems/flatten-nested-list-iterator/
/// 341. 扁平化嵌套列表迭代器
/// https://blog.csdn.net/zrh_CSDN/article/details/83897581
/// </summary>
class NestedIterator
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    private Stack<NestedInteger> _stack = new Stack<NestedInteger>();
    public NestedIterator(IList<NestedInteger> nestedList)
    {
        if (nestedList != null && 0 < nestedList.Count )
            for (int i = nestedList.Count - 1; -1 < i; --i ) _stack.Push(nestedList[i]);
    }

    public bool HasNext()
    {
        while ( 0 < _stack.Count )
        {
            NestedInteger t = _stack.Peek();
            if (t.IsInteger()) return true;

            var list = _stack.Pop().GetList();
            for ( int i = list.Count - 1; -1 < i; --i) _stack.Push(list[i] );
        }
        return false;
    }

    public int Next()
    {
        NestedInteger t = _stack.Pop();
        return t.GetInteger();
    }
}
/*
public class NestedIterator {

    private List<Int32> items;
    private Int32 index;
    public NestedIterator(IList<NestedInteger> nestedList)
    {
        items = new List<Int32>();
        index = 0;
        Read(nestedList);
    }

    public void Read(IList<NestedInteger> list)
    {
        for(Int32 i = 0; i < list.Count; ++i)
        {
            if(list[i].IsInteger())
            {
                items.Add(list[i].GetInteger());
            }
            else
            {
                Read(list[i].GetList());
            }
        }
    }

    public bool HasNext()
    {
        if(index >= items.Count)
        {
            return false;
        }
        return true;
    }

    public int Next()
    {
        return items[index++];
    }
}
public class NestedIterator {
   private Stack<Cursor> stack = new Stack<Cursor>();
    public NestedIterator(IList<NestedInteger> nestedList) {
        if (nestedList != null) stack.Push(new Cursor(nestedList));
    }

    public bool HasNext() {
        while (stack.Count!=0)
            {
                Cursor cursor = stack.Peek();
                if (cursor.i < cursor.list.Count)
                {
                    NestedInteger nested = cursor.list[cursor.i];
                    if (nested.IsInteger()) return true;
                    cursor.i++;
                    stack.Push(new Cursor(nested.GetList()));
                }
                else
                {
                    stack.Pop();
                }
            }
            return false;
    }

    public int Next() {
         while (stack.Count!=0)
            {
                Cursor cursor = stack.Peek();
                if (cursor.i < cursor.list.Count)
                {
                    NestedInteger nested = cursor.list[cursor.i++];
                    if (nested.IsInteger()) return nested.GetInteger();
                    stack.Push(new Cursor(nested.GetList()));
                }
                else
                {
                    stack.Pop();
                }
            }
            return -1;
    }
     public  class Cursor
    {
       public   IList<NestedInteger> list;
       public  int i;
       public   Cursor(IList<NestedInteger> list)
        {
            this.list = list;
        }
    }
}
public class NestedIterator {
    private Queue<int> q;

    public NestedIterator(IList<NestedInteger> nestedList) {
        q = new Queue<int>();
        
        Init(nestedList);
    }
    
    private void Init(IList<NestedInteger> nestedList)
    {
        foreach(NestedInteger item in nestedList)
        {
            if(item.IsInteger())
            {
                q.Enqueue(item.GetInteger());
            }
            else
            {
                Init(item.GetList());
            }
        }
    }

    public bool HasNext() {
        return q.Count() != 0;
    }

    public int Next() {
        return q.Dequeue();
    }
}
public class NestedIterator {
    private Stack<Cursor> stack = new Stack<Cursor>();
    public NestedIterator(IList<NestedInteger> nestedList) {
        stack.Push(new Cursor(nestedList));
    }

    public bool HasNext() {
          while (stack.Count!=0)
            {
                Cursor state = stack.Peek();
                if (state.i < state.list.Count)
                {
                    if (state.list[state.i].IsInteger()) return true;
                    stack.Push(new Cursor(state.list[state.i++].GetList()));
                }
                else
                {
                    stack.Pop();
                }
            }
            return false;
    }

    public int Next() {
          HasNext();
            Cursor state = stack.Peek();
            return state.list [state.i++].GetInteger();
    }
     public  class Cursor
    {
       public   IList<NestedInteger> list;
       public  int i;
       public   Cursor(IList<NestedInteger> list)
        {
            this.list = list;
        }
    }
}

*/

/**
 * // This is the interface that allows for creating nested lists.
 * // You should not implement it, or speculate about its implementation
 * interface NestedInteger {
 *
 *     // @return true if this NestedInteger holds a single integer, rather than a nested list.
 *     bool IsInteger();
 *
 *     // @return the single integer that this NestedInteger holds, if it holds a single integer
 *     // Return null if this NestedInteger holds a nested list
 *     int GetInteger();
 *
 *     // @return the nested list that this NestedInteger holds, if it holds a nested list
 *     // Return null if this NestedInteger holds a single integer
 *     IList<NestedInteger> GetList();
 * }
 */
interface NestedInteger
{
 
    // @return true if this NestedInteger holds a single integer, rather than a nested list.
    bool IsInteger();
 
    // @return the single integer that this NestedInteger holds, if it holds a single integer
    // Return null if this NestedInteger holds a nested list
    int GetInteger();
 
    // @return the nested list that this NestedInteger holds, if it holds a nested list
    // Return null if this NestedInteger holds a single integer
    IList<NestedInteger> GetList();
}