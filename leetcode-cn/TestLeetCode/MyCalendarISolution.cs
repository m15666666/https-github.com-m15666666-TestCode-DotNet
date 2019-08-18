using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
 
实现一个 MyCalendar 类来存放你的日程安排。如果要添加的时间内没有其他安排，则可以存储这个新的日程安排。

MyCalendar 有一个 book(int start, int end)方法。它意味着在 start 到 end 时间内增加一个日程安排，注意，这里的时间是半开区间，即 [start, end), 实数 x 的范围为，  start <= x < end。

当两个日程安排有一些时间上的交叉时（例如两个日程安排都在同一时间内），就会产生重复预订。

每次调用 MyCalendar.book方法时，如果可以将日程安排成功添加到日历中而不会导致重复预订，返回 true。否则，返回 false 并且不要将该日程安排添加到日历中。

请按照以下步骤调用 MyCalendar 类: MyCalendar cal = new MyCalendar(); MyCalendar.book(start, end)

示例 1:

MyCalendar();
MyCalendar.book(10, 20); // returns true
MyCalendar.book(15, 25); // returns false
MyCalendar.book(20, 30); // returns true
解释: 
第一个日程安排可以添加到日历中.  第二个日程安排不能添加到日历中，因为时间 15 已经被第一个日程安排预定了。
第三个日程安排可以添加到日历中，因为第一个日程安排并不包含时间 20 。
说明:

每个测试用例，调用 MyCalendar.book 函数最多不超过 100次。
调用函数 MyCalendar.book(start, end)时， start 和 end 的取值范围为 [0, 10^9]。
*/
/// <summary>
/// https://leetcode-cn.com/problems/my-calendar-i/
/// 729. 我的日程安排表 I
/// https://blog.csdn.net/qq_39618308/article/details/82188501
/// </summary>
class MyCalendarISolution
{
    public void Test()
    {
        Book(10, 20);
        Book(15, 25);
        Book(20, 30);
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public MyCalendarISolution()
    {

    }

    public bool Book(int start, int end)
    {
        if(list.Count == 0)
        {
            list.Add(( start, end ));
            return true;
        }

        if(end <= list[0].Item1)
        {
            list.Insert(0,(start, end));
            return true;
        }

        if (list[list.Count-1].Item2 <= start)
        {
            list.Add((start, end));
            return true;
        }

        if (list.Count == 1) return false;

        int left = 0;
        int right = list.Count - 1;

        while ( left < right)
        {
            int mid = (left + right)/2;
            var item = list[mid];
            if (item.Item1 > start) right = mid - 1;
            else if (item.Item1 < start) left = mid + 1;
            else return false;
        }

        if (list.Count <= left) return false;
        if ( list[left].Item1 > start )
        {
            int previous = left - 1;
            if ( -1 < previous && list[previous].Item2 <= start && list[left].Item1 >= end)
            {
                list.Insert(left, (start, end));
                return true;
            }
        }
        else
        {
            int next = left + 1;
            if (next< list.Count && list[next].Item1 >= end && list[left].Item2 <= start)
            {
                list.Insert(left + 1, (start, end));
                return true;
            }
        }

        return false;
    }
    private List<(int, int)> list = new List<(int, int)>();
}