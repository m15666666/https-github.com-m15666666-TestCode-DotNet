using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
在无限的平面上，机器人最初位于 (0, 0) 处，面朝北方。机器人可以接受下列三条指令之一：

"G"：直走 1 个单位
"L"：左转 90 度
"R"：右转 90 度
机器人按顺序执行指令 instructions，并一直重复它们。

只有在平面中存在环使得机器人永远无法离开时，返回 true。否则，返回 false。

 

示例 1：

输入："GGLLGG"
输出：true
解释：
机器人从 (0,0) 移动到 (0,2)，转 180 度，然后回到 (0,0)。
重复这些指令，机器人将保持在以原点为中心，2 为半径的环中进行移动。
示例 2：

输入："GG"
输出：false
解释：
机器人无限向北移动。
示例 3：

输入："GL"
输出：true
解释：
机器人按 (0, 0) -> (0, 1) -> (-1, 1) -> (-1, 0) -> (0, 0) -> ... 进行移动。
 

提示：

1 <= instructions.length <= 100
instructions[i] 在 {'G', 'L', 'R'} 中
*/
/// <summary>
/// https://leetcode-cn.com/problems/robot-bounded-in-circle/
/// 1041. 困于环中的机器人
/// 
/// </summary>
class RobotBoundedInCircleSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public bool IsRobotBounded(string instructions)
    {
        DirectionPoint p = new DirectionPoint(0, 0, 0);
        foreach (var c in instructions) p.Move(c);

        if (p.X == 0 && p.Y == 0) return true;
        if (p.Direction == 0) return false;
        return true;
    }

    class DirectionPoint
    {
        public int X { get; set; }
        public int Y { get; set; }

        /// <summary>
        /// 方向
        /// </summary>
        public int Direction { get; set; }
        public DirectionPoint(int x, int y, int direction)
        {
            X = x;
            Y = y;
            Direction = direction;
        }
        public void Move(char c)
        {
            switch (c)
            {
                case 'L':
                    Direction = (Direction + 1) % 4;//左转
                    return;

                case 'R':
                    Direction = (Direction + 3) % 4;//右转
                    return;

                case 'G':
                    switch (Direction)
                    {

                        case 0:
                            X++;
                            break;

                        case 1:
                            Y++;
                            break;

                        case 2:
                            X--;
                            break;

                        case 3:
                            Y--;
                            break;
                    }
                    return;
            }
        }
    }
}
/*
判断起点和终点的坐标、方向关系
查可猜
331 阅读
很简单的原理：
1、若终点和起点坐标一致，则返回true；
2、若终点不一致，此时判断起点与终点的方向关系：
（1）不一致，则一定可以在有限次执行指令后回到原点，返回true；
（2）一致，则无限远离起点，返回false；

class Solution {
    public boolean isRobotBounded(String instructions) {
        Point p = new Point(0,0,0);
        for(int i=0; i<instructions.length(); i++) {
            p.move(instructions.charAt(i));
        }
        if (p.getX()==0 && p.getY()==0) {
            return true;
        }else if (p.getDerec() == 0) {
            return false;
        }
        return true;
    }
}

class Point {
    private int x;
    private int y;
    private int derec;//方向
    public Point(int x, int y, int derec) {
        this.x = x;
        this.y = y;
        this.derec = derec;
    }
    public void setX(int x) {
        this.x = x;
    }

    public void setY(int y) {
        this.y = y;
    }
    public void setDerec(int derec) {
        this.derec = derec;
    }
    public int getX() {
        return this.x;
    }
    public int getY() {
        return this.y;
    }
    public int getDerec() {
        return this.derec;
    }
    public void move(char c) {
        if (c == 'L') {
            derec=(derec+1)%4;//左转
        }else if (c == 'R') {
            derec = (derec+3)%4;//右转
        }else if (c == 'G') {
            switch (derec) {//在不同方向下前进一步的坐标变化
                case 0: this.x++;break;
                case 1: this.y++;break;
                case 2: this.x--;break;
                case 3: this.y--;break;
            }
        }
    }
}

直指原理，一步到位，双100%算法
yongyaoduan
824 阅读
一起来简化一下问题，一顿指令之后，位置从(0,0)到了(x,y)，其实可以把整个指令看成一步(0,0)->(x,y)
接下来第二次指令会怎么走呢，很简单，如果(x,y)等于(0,0)，那么相当于整体位移为0，自然是回去了，其余情况，保持第一轮操作位移的长度，方向有4种:
如果初始方向是向上，现在变成了向左，那么整体位移的方向向左偏转，就像例子里面的“GL”
如果现在变成向右，整体向右偏转，就像GR”
如果现在变成向下，整体旋转180度，就像“GRR”，直接下一次就走回去了，这三种情况，最后都能回去
而当现在方向变成向上，那么就保持位移方向不变，就像例子里面的“GG”一样，一去不复返
所以，总而言之，一次指令之后，只有(x,y)不是原点，并且方向和原来的方向一致，最后才回不去
代码如下:

bool isRobotBounded(string instructions) {
        if (!(instructions.size() >= 1 && instructions.size() <= 100)) return false;
        int d = 0;  //四个方向 0上1右2下3左  这样定是为了满足 d+1就是向左转 d+3就是向右转
        int dx[] = {0, 1, 0, -1};//索引和方向对应
        int dy[] = {1, 0, -1, 0};
        int x = 0;
        int y = 0;
        for (auto eachIns:instructions) {
            switch (eachIns) {
                case 'R':
                    d += 1;
                    break;
                case 'L':
                    d += 3; //不用d-=1 是因为当d变成负数的时候，取mod会出错
                    break;
                case 'G':
                    d = d % 4;
                    x = x + dx[d];
                    y = y + dy[d];
                    break;
            }
        }
        return ((x == 0 && y == 0) || d % 4 != 0);

    }
下一篇：☆≡═ 执行一趟指令

☆≡═ 执行一趟指令
hareyukai
308 阅读
执行一次指令，根据执行完毕后机器人的位置和方向判断。
当执行完成后机器人位置不变，仍在原点，机器人会困在环中。
或者机器人位置改变了，而方向也改变了，因为机器人每次移动的距离相等，偏转的角度相同，机器人会走过一个正多边形路径后回到原点。
只有沿着开始方向直线行走，机器人才能离开。
使用复数运算，计算机器人的位置和方向。
初始位置pos{0, 0}, 方向{0, 1}。
class Solution {
public:
    bool isRobotBounded(const string& instructions) {
        complex<int> pos{0, 0};
        complex<int> step{0, 1};
        const complex<int> left{0, 1};
        const complex<int> right{0, -1};
        for (const char ch : instructions)
            if ('G' == ch) pos += step;
            else if ('L' == ch) step *= left;
            else if ('R' == ch) step *= right;
        return (pos == complex<int>{0, 0}) || (step != complex<int>{0, 1});
    }
};
下一篇：思考考~只需要一轮循环即可得出结果

 
*/
