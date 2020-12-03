using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个含有 n 个正数的数组 x。从点 (0,0) 开始，先向北移动 x[0] 米，然后向西移动 x[1] 米，向南移动 x[2] 米，向东移动 x[3] 米，持续移动。也就是说，每次移动后你的方位会发生逆时针变化。

编写一个 O(1) 空间复杂度的一趟扫描算法，判断你所经过的路径是否相交。

 

示例 1:

┌───┐
│   │
└───┼──>
    │

输入: [2,1,1,2]
输出: true 
示例 2:

┌──────┐
│      │
│
│
└────────────>

输入: [1,2,3,4]
输出: false 
示例 3:

┌───┐
│   │
└───┼>

输入: [1,1,1,1]
输出: true 


*/
/// <summary>
/// https://leetcode-cn.com/problems/self-crossing/
/// 335. 路径交叉
/// 
/// 
/// 
/// </summary>
class SelfCrossingSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public bool IsSelfCrossing(int[] x) 
    {
        int len = x.Length;
        if (len < 4) return false;
        // 4条边
        if (len == 4) return x[1] <= x[3] && x[2] <= x[0];
        for (int i = 3; i < len; i++) {
            // 4条边
            if (x[i - 2] <= x[i] && x[i - 1] <= x[i - 3]) return true;
            // 5条边
            if (3 < i && x[i - 1] == x[i - 3] && x[i - 4] +x[i] >= x[i - 2]) return true;
            // 6条边
            if (4 < i && (x[i - 3]-x[i - 5]) <= x[i - 1] && x[i - 1]<= x[i - 3] 
                && (x[i - 2]-x[i - 4]) <= x[i] && x[i] <= x[i - 2] && x[i-4] < x[i - 2]) return true;
        }
        return false;
    }

}
/*
335.路径交叉
zhangll
发布于 2019-06-26
1.7k
画画图，发现想要交叉只有三种可能：

第一种 条件为 i>=3 || x[i] >= x[i-2] || x[i-1] <= x[i-3]

335-1.png

第二种 条件为 i> 3 || x[i-1] == x[i-3] || x[i-4] + x[i] >= x[i-2]

335-2.png

第三种 条件为 i> 4 || x[i-3]-x[i-5] <= x[i-1] <= x[i-3] || x[i-2]-x[i-4] <= x[i] <= x[i-2] || x[i-2] > x[i-4]

335-3.png

最后补充下细节，数组小于4个时定不可能相交，等于4时，只有第一种可能。

代码如下：


class Solution:
    def isSelfCrossing(self, x) -> bool:
        if len(x) <  4:return False
        if len(x) == 4:return True if x[3] >= x[1] and x[2] <= x[0] else False
        for i in range(3, len(x)):
            if x[i] >= x[i-2] and x[i-1] <= x[i-3]:
                return True
            if i > 3 and x[i-1] == x[i-3] and x[i-4] + x[i] >= x[i-2]:
                return True
            if i > 4 and x[i-3]-x[i-5] <= x[i-1] <= x[i-3] and x[i-2]-x[i-4] <= x[i] <= x[i-2] and x[i-2] > x[i-4]:
                return True
        return False
下一篇：空间复杂度O(1)解法，速度打败100%

一步一步分析
Zhenghao-Liu
发布于 2020-03-24
1.6k
强推题解，又是学习的一天
首先明白给出的是正数数组，这帮我们省去好多难题
鼠标+win自带画图作图，画的不够直啊什么的多多包涵，起点用圆圈表示，在每个图片下面标注了序号
我们的分析方式多是将最后一条边设为i，前一条边设为i-1，再前面一条边设为i-2，这样以此类推
基本思路：从不相交推导到相交，从复杂模型简化到基本模型

1.首先我们来讨论下不会相交的情况
其实无非就是向内扩展螺旋状，向外扩展螺旋状，或者两种的结合
image.png
图1
还有情况就是在边的个数x_size<=3的时候无论如何也相交不了
image.png
图2

2.那就先看看边数=4的相交情况
其实很简单，也就只有一种
image.png
图3
那就先设最后遍历到的一条边为i，前面的边分别位i-1、i-2、i-3，从而得出这种情况相交的代码条件，即
image.png
图4
写成代码即if (i>=3 && x.at(i-1)<=x.at(i-3) && x.at(i)>=x.at(i-2)) return true;
这里的条件中的等于号，就包括了那种刚好相交就没有突出的情况了

3.再看看边数=5的情况
讨论边数=5相交的情况，我们不妨从边数=4时不相交的情况来开始推导，可以先固定[i-2]与[i-3]，对[i]和[i-1]做所有可能的分析
image.png
图5

对于图5的前三种情况，其实是同一种类型，发现要想相交，只能是 第5条x.at(4) 和 第二条边x.at(1) 相交才有可能达成在边数=5情况下相交，而这种情况下 第一条边x.at(0) 就没有讨论的意义了，你挡住 第一条边x.at(0) 就发现回到了图3的情况了，所以前面的判断条件足以判断出当前3种情况
对于图5的第四种情况，发现下面的边和上面的边刚好长度相等，这是可以构成在边数=5情况下有相交的，即只需要第 五条边x.at(4) 往上触碰到 第一条边x.at(0) 即可(最后一条边用不同的颜色表明)
image.png
图6
写成代码即if (i>=4 && x.at(i-3)==x.at(i-1) && x.at(i)+x.at(i-4)>=x.at(i-2)) return true;
对于图5的最后一种情况就有些特别了，发现他不仅在边数=4情况下无相交，在边数=5情况下也无法相交（因为 第5条边x.at(4) 竖直向上，和 第一条边x.at(0) 平行），只有在边数=6情况下才可能出现相交了
4.边数=6的情况
我们上面讨论到边数=4的情况下即图5的最后一种，我们先来讨论它在边数=5的情况不相交下有几种可能
image.png
图7

来看图7第一种情况，它要想在边数=6时有相交，那么只能是 下一条边 和 最左竖直的边x.at(2) 相交了，而这种情况又退化到了图3，所以不用单独考虑
image.png
图8
图7第二种情况，他要想相交只能 下一条边 和 最开始的第一条边x.at(0) 相交，这是一种全新的情况，我们单独考虑
image.png
图9
代码即if (i>=5 && x.at(i)+x.at(i-4)>=x.at(i-2) && x.at(i-1)+x.at(i-5)>=x.at(i-3) && x.at(i-2)>x.at(i-4) && x.at(i-3)>x.at(i-1)) return true;
某些题解的代码可能会在大于号改成大于等于号，其实没这个必要，部分的等于条件会退化到前面两种相交模型
图7的第三种情况，要想相交，只有 新增的一条边 与 第二条边x.at(1) 相交，而这又退化到了图6的情况
image.png
图10
图7的最后一种情况，再新增一条边怎样都构不成相交，要想相交只能在边数达到7的情况下，此时 第一条边x.at(0) 没有任何作用，而这恰好又退化到了图9的情况
image.png
图11
至此，3种基本的相交模型已经讨论完全了，后面在边数相加的基础上再发生相交，其实都可以退化成这3种相交模型情况之一

所以在新增一条边时，我们只需要判断他是否是3种相交模型的一种即可了


class Solution {
public:
    bool isSelfCrossing(vector<int>& x) {
        int x_size=x.size();
        for (int i=3;i<x_size;++i)
        {
            if (i>=3 && x.at(i-1)<=x.at(i-3) && x.at(i)>=x.at(i-2))
                return true;
            if (i>=4 && x.at(i-3)==x.at(i-1) && x.at(i)+x.at(i-4)>=x.at(i-2))
                return true;
            if (i>=5 && x.at(i)+x.at(i-4)>=x.at(i-2) && x.at(i-1)+x.at(i-5)>=x.at(i-3) && x.at(i-2)>x.at(i-4) && x.at(i-3)>x.at(i-1))
                return true;
        }
        return false;
    }
};
代码很简单，想的过程。。一言难尽

*/