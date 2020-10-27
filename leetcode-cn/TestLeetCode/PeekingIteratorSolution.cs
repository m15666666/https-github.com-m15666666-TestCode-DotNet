using System.Collections.Generic;

/*
给定一个迭代器类的接口，接口包含两个方法： next() 和 hasNext()。设计并实现一个支持 peek() 操作的顶端迭代器 -- 其本质就是把原本应由 next() 方法返回的元素 peek() 出来。

示例:

假设迭代器被初始化为列表 [1,2,3]。

调用 next() 返回 1，得到列表中的第一个元素。
现在调用 peek() 返回 2，下一个元素。在此之后调用 next() 仍然返回 2。
最后一次调用 next() 返回 3，末尾元素。在此之后调用 hasNext() 应该返回 false。

*/

/// <summary>
/// https://leetcode-cn.com/problems/peeking-iterator/
/// 284. 顶端迭代器
///
///
/// </summary>
internal class PeekingIteratorSolution
{
    public static void Test()
    {
        var list = new List<int>() { 1, 2, 3 };
        var enumerator = list.GetEnumerator();
        var solution = new PeekingIteratorSolution(enumerator);
        var v = solution.Next();
        v = solution.Peek();
        v = solution.Next();
        v = solution.Next();
        var has = solution.HasNext();
    }

    public PeekingIteratorSolution(IEnumerator<int> iterator)
    {
        _enumerator = iterator;
        MoveNext();
    }

    private IEnumerator<int> _enumerator;
    private bool _hasNext = false;
    private int _next;

    private void MoveNext()
    {
        _hasNext = _enumerator.MoveNext();
        if (_hasNext) _next = _enumerator.Current;
    }

    public int Peek()
    {
        return _hasNext ? _next : default;
    }

    public int Next()
    {
        if (!_hasNext) return default;
        var ret = _next;
        MoveNext();
        return ret;
    }

    public bool HasNext()
    {
        return _hasNext;
    }
}

/*

*/