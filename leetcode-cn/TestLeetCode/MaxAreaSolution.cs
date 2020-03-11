using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给你 n 个非负整数 a1，a2，...，an，每个数代表坐标中的一个点 (i, ai) 。在坐标内画 n 条垂直线，垂直线 i 的两个端点分别为 (i, ai) 和 (i, 0)。找出其中的两条线，使得它们与 x 轴共同构成的容器可以容纳最多的水。

说明：你不能倾斜容器，且 n 的值至少为 2。

 



图中垂直线代表输入数组 [1,8,6,2,5,4,8,3,7]。在此情况下，容器能够容纳水（表示为蓝色部分）的最大值为 49。

 

示例：

输入：[1,8,6,2,5,4,8,3,7]
输出：49
*/
/// <summary>
/// https://leetcode-cn.com/problems/container-with-most-water/
/// 11. 盛最多水的容器
/// </summary>
class MaxAreaSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    public int MaxArea(int[] height)
    {
        if (height == null || height.Length < 2) return 0;

        #region 方法一：暴力法

        //int maxArea = 0;
        //for(int i = 0; i < height.Length - 1; i++)
        //{
        //    for( int j = height.Length - 1; i < j; j--)
        //    {
        //        var area = (j - i) * Math.Min(height[i], height[j]);
        //        if (maxArea < area) maxArea = area;
        //    }
        //}

        //return maxArea;

        #endregion

        #region 方法二：双指针法

        //int maxArea = 0;
        //int startIndex = 0;
        //int endIndex = height.Length - 1;
        //while( startIndex < endIndex)
        //{
        //    var startValue = height[startIndex];
        //    var endValue = height[endIndex];
        //    var area = (endIndex - startIndex) * Math.Min(startValue, endValue);
        //    if (maxArea < area) maxArea = area;

        //    if (startValue < endValue) startIndex++;
        //    else endIndex--;
        //}

        //return maxArea;

        #endregion

        #region 方法三：单调法

        int maxArea = 0;
        int maxHeight = 0;
        int index = -1;
        List<(int, int)> maxHeights = new List<(int, int)>();
        foreach( var h in height)
        {
            index++;

            foreach( var item in maxHeights )
            {
                var area = (index - item.Item1) * Math.Min(h, item.Item2);
                if (maxArea < area) maxArea = area;
            }

            if (maxHeight < h)
            {
                maxHeight = h;
                maxHeights.Insert(0, (index, maxHeight));
            }
        }
        return maxArea;

        #endregion
    }

}
/*
盛最多水的容器
力扣 (LeetCode)
发布于 2 年前
109.6k
摘要
如题意，垂直的两条线段将会与坐标轴构成一个矩形区域，较短线段的长度将会作为矩形区域的宽度，两线间距将会作为矩形区域的长度，而我们必须最大化该矩形区域的面积。

解决方案
方法一：暴力法
算法

在这种情况下，我们将简单地考虑每对可能出现的线段组合并找出这些情况之下的最大面积。

public class Solution {
    public int maxArea(int[] height) {
        int maxarea = 0;
        for (int i = 0; i < height.length; i++)
            for (int j = i + 1; j < height.length; j++)
                maxarea = Math.max(maxarea, Math.min(height[i], height[j]) * (j - i));
        return maxarea;
    }
}
复杂度分析

时间复杂度：O(n^2)O(n 
2
 )，计算所有 \frac{n(n-1)}{2} 
2
n(n−1)
​	
  种高度组合的面积。
空间复杂度：O(1)O(1)，使用恒定的额外空间。
方法二：双指针法
算法

这种方法背后的思路在于，两线段之间形成的区域总是会受到其中较短那条长度的限制。此外，两线段距离越远，得到的面积就越大。

我们在由线段长度构成的数组中使用两个指针，一个放在开始，一个置于末尾。 此外，我们会使用变量 maxareamaxarea 来持续存储到目前为止所获得的最大面积。 在每一步中，我们会找出指针所指向的两条线段形成的区域，更新 maxareamaxarea，并将指向较短线段的指针向较长线段那端移动一步。

查看下面的例子将有助于你更好地理解该算法：

1 8 6 2 5 4 8 3 7


这种方法如何工作？

最初我们考虑由最外围两条线段构成的区域。现在，为了使面积最大化，我们需要考虑更长的两条线段之间的区域。如果我们试图将指向较长线段的指针向内侧移动，矩形区域的面积将受限于较短的线段而不会获得任何增加。但是，在同样的条件下，移动指向较短线段的指针尽管造成了矩形宽度的减小，但却可能会有助于面积的增大。因为移动较短线段的指针会得到一条相对较长的线段，这可以克服由宽度减小而引起的面积减小。

public class Solution {
    public int maxArea(int[] height) {
        int maxarea = 0, l = 0, r = height.length - 1;
        while (l < r) {
            maxarea = Math.max(maxarea, Math.min(height[l], height[r]) * (r - l));
            if (height[l] < height[r])
                l++;
            else
                r--;
        }
        return maxarea;
    }
}
复杂度分析

时间复杂度：O(n)O(n)，一次扫描。

空间复杂度：O(1)O(1)，使用恒定的空间。 
*/
