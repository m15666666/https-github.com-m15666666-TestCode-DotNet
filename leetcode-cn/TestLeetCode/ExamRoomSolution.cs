using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
在考场里，一排有 N 个座位，分别编号为 0, 1, 2, ..., N-1 。

当学生进入考场后，他必须坐在能够使他与离他最近的人之间的距离达到最大化的座位上。如果有多个这样的座位，他会坐在编号最小的座位上。(另外，如果考场里没有人，那么学生就坐在 0 号座位上。)

返回 ExamRoom(int N) 类，它有两个公开的函数：其中，函数 ExamRoom.seat() 会返回一个 int （整型数据），代表学生坐的位置；函数 ExamRoom.leave(int p) 代表坐在座位 p 上的学生现在离开了考场。每次调用 ExamRoom.leave(p) 时都保证有学生坐在座位 p 上。

 

示例：

输入：["ExamRoom","seat","seat","seat","seat","leave","seat"], [[10],[],[],[],[],[4],[]]
输出：[null,0,9,4,2,null,5]
解释：
ExamRoom(10) -> null
seat() -> 0，没有人在考场里，那么学生坐在 0 号座位上。
seat() -> 9，学生最后坐在 9 号座位上。
seat() -> 4，学生最后坐在 4 号座位上。
seat() -> 2，学生最后坐在 2 号座位上。
leave(4) -> null
seat() -> 5，学生最后坐在 5 号座位上。
 

提示：

1 <= N <= 10^9
在所有的测试样例中 ExamRoom.seat() 和 ExamRoom.leave() 最多被调用 10^4 次。
保证在调用 ExamRoom.leave(p) 时有学生正坐在座位 p 上。
*/
/// <summary>
/// https://leetcode-cn.com/problems/exam-room/
/// 855. 考场就座
/// 
/// </summary>
class ExamRoomSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public ExamRoomSolution(int N)
    {
        //SortedDictionary
        //SortedList
        this.N = N;
    }

    private int N;
    private SortedSet<int> students = new SortedSet<int>();
    public int Seat()
    {
        //Let's determine student, the position of the next
        //student to sit down.
        int student = 0;
        if (0 < students.Count)
        {
            //Tenatively, dist is the distance to the closest student,
            //which is achieved by sitting in the position 'student'.
            //We start by considering the left-most seat.
            int dist = students.First();
            int prev = -1;
            foreach (var s in students)
            {
                if (prev != -1)
                {
                    //For each pair of adjacent students in positions (prev, s),
                    //d is the distance to the closest student;
                    //achieved at position prev + d.
                    int d = (s - prev) / 2;
                    if (dist < d)
                    {
                        dist = d;
                        student = prev + d;
                    }
                }
                prev = s;
            }

            var last = prev;
            int rightMost = N - 1;
            //Considering the right-most seat.
            if (dist < rightMost  - last) student = rightMost;
        }

        //Add the student to our sorted TreeSet of positions.
        students.Add(student);
        return student;
    }

    public void Leave(int p)
    {
        students.Remove(p);
    }
}