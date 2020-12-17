using System;
using System.Collections.Generic;

/*
Range 模块是跟踪数字范围的模块。你的任务是以一种有效的方式设计和实现以下接口。

addRange(int left, int right) 添加半开区间 [left, right)，跟踪该区间中的每个实数。添加与当前跟踪的数字部分重叠的区间时，应当添加在区间 [left, right) 中尚未跟踪的任何数字到该区间中。
queryRange(int left, int right) 只有在当前正在跟踪区间 [left, right) 中的每一个实数时，才返回 true。
removeRange(int left, int right) 停止跟踪区间 [left, right) 中当前正在跟踪的每个实数。

示例：

addRange(10, 20): null
removeRange(14, 16): null
queryRange(10, 14): true （区间 [10, 14) 中的每个数都正在被跟踪）
queryRange(13, 15): false （未跟踪区间 [13, 15) 中像 14, 14.03, 14.17 这样的数字）
queryRange(16, 17): true （尽管执行了删除操作，区间 [16, 17) 中的数字 16 仍然会被跟踪）

提示：

半开区间 [left, right) 表示所有满足 left <= x < right 的实数。
对 addRange, queryRange, removeRange 的所有调用中 0 < left < right < 10^9。
在单个测试用例中，对 addRange 的调用总数不超过 1000 次。
在单个测试用例中，对  queryRange 的调用总数不超过 5000 次。
在单个测试用例中，对 removeRange 的调用总数不超过 1000 次。

*/

/// <summary>
/// https://leetcode-cn.com/problems/range-module/
/// 715. Range 模块
///
///
/// </summary>
internal class RangeModuleSolution
{
    public static void Test()
    {
        var test = new RangeModuleSolution();
        var r1 = test.QueryRange(1,4);
        r1 = test.QueryRange(6,10);
        test.AddRange(2,6);
        test.AddRange(2,8);
        test.AddRange(4,7);
        r1 = test.QueryRange(2,5);
        test.RemoveRange(1, 10);
        test.RemoveRange(3, 5);
        test.RemoveRange(1, 2);
    }

    public RangeModuleSolution()
    {
    }

    private readonly List<int[]> _ranges = new List<int[]>();
    public void AddRange(int left, int right)
    {
        if (_ranges.Count == 0)
        {
            _ranges.Add(new[] { left, right });
            return;
        }

        var first = _ranges[0];
        if (first[0] <= left && right <= first[1]) return;
        if (first[0] <= right && right <= first[1])
        {
            first[0] = left;
            return;
        }
        if (right < first[0])
        {
            _ranges.Insert(0, new[] { left, right });
            return;
        }

        var last = _ranges[_ranges.Count - 1];
        if (last[0] <= left && right <= last[1]) return;
        if (last[0] <= left && left <= last[1])
        {
            last[1] = right;
            return;
        }
        if (last[1] < left)
        {
            _ranges.Add(new[] { left, right });
            return;
        }
        if (left <= first[0] && last[1] <= right)
        {
            _ranges.Clear();
            _ranges.Add(new[] { left, right });
            return;
        }
        var (leftBound, rightBound) = Bound(left, right);
        if (leftBound <= rightBound)
        {
            left = Math.Min(left, _ranges[leftBound][0]);
            right = Math.Max(right, _ranges[rightBound][1]);
            _ranges.RemoveRange(leftBound, rightBound - leftBound + 1);
        }
        _ranges.Insert(leftBound, new[] { left, right });
    }

    public bool QueryRange(int left, int right)
    {
        if (_ranges.Count == 0) return false;

        var first = _ranges[0];
        if (left < first[0]) return false;
        var last = _ranges[_ranges.Count - 1];
        if (last[1] < right) return false;

        int L = 0, R = _ranges.Count - 1;
        while (L <= R)
        {
            var mid = (L + R) / 2;
            var range = _ranges[mid];
            if (range[0] <= left && left <= range[1])
                return range[0] <= right && right <= range[1];

            if (range[0] < left) L = mid + 1;
            else R = mid - 1;
        }
        return false;
    }

    public void RemoveRange(int left, int right)
    {
        if (_ranges.Count == 0) return;

        var first = _ranges[0];
        if (right <= first[0]) return;

        var last = _ranges[_ranges.Count - 1];
        if (last[1] <= left) return;

        if (left <= first[0] && last[1] <= right)
        {
            _ranges.Clear();
            return;
        }

        var (leftBound, rightBound) = Bound(left, right);
        if (leftBound <= rightBound)
        {
            List<int[]> inserted = new List<int[]>();
            for( int i = leftBound; i <= rightBound; i++)
            {
                if (_ranges[i][0] < left) inserted.Add(new[] { _ranges[i][0], left });
                if (right < _ranges[i][1]) inserted.Add(new[] { right, _ranges[i][1] });
            }
            //if (_ranges[leftBound][0] < left) inserted.Add(new[] { _ranges[leftBound][0], left });
            //if (right < _ranges[rightBound][1]) inserted.Add(new[] { right, _ranges[rightBound][1] });
            _ranges.RemoveRange(leftBound, rightBound - leftBound + 1);
            foreach (var item in inserted) _ranges.Insert(leftBound++, item);
            //if(0 < inserted.Count) _ranges.InsertRange(leftBound, inserted);
        }
    }

    private (int, int) Bound(int left, int right)
    {
        int L = 0, R = _ranges.Count - 1;
        int leftBound = 0, rightBound = _ranges.Count - 1;

        while (L <= R)
        {
            var mid = (L + R) / 2;
            if (_ranges[mid][1] < left)
            {
                leftBound = mid + 1;
                L = leftBound;
                continue;
            }
            R = mid - 1;
        }
        while (L <= R)
        {
            var mid = (L + R) / 2;
            if (right < _ranges[mid][0])
            {
                leftBound = mid - 1;
                R = leftBound;
                continue;
            }
            L = mid + 1;
        }

        return (leftBound, rightBound);
    }
}

/*

*/