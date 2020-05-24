using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

/*
给定 n 个非负整数，用来表示柱状图中各个柱子的高度。每个柱子彼此相邻，且宽度为 1 。

求在该柱状图中，能够勾勒出来的矩形的最大面积。

 



以上是柱状图的示例，其中每个柱子的宽度为 1，给定的高度为 [2,1,5,6,2,3]。

 



图中阴影部分为所能勾勒出的最大矩形面积，其面积为 10 个单位。

 

示例:

输入: [2,1,5,6,2,3]
输出: 10

*/
/// <summary>
/// https://leetcode-cn.com/problems/largest-rectangle-in-histogram/
/// 84. 柱状图中最大的矩形
/// 
/// 
/// 
/// </summary>
class LargestRectangleInHistogramSolution
{
    public void Test()
    {
        int[] nums = new int[] { 0,1,0,1};
        var ret = LargestRectangleArea(nums);
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    public int LargestRectangleArea(int[] heights) {
        const int Bottom = -1;
        var stack = new Stack <int>();
        stack.Push( Bottom );
        int maxarea = 0;
        int top;
        for (int i = 0; i < heights.Length; ++i) {
            while ((top = stack.Peek()) != Bottom && heights[i] < heights[top] )
                maxarea = Math.Max(maxarea, heights[stack.Pop()] * (i - stack.Peek() - 1));
            stack.Push(i);
        }
        while (stack.Peek() != Bottom)
            maxarea = Math.Max(maxarea, heights[stack.Pop()] * (heights.Length - stack.Peek() -1));
        return maxarea;
    }
}
/*


柱状图中最大的矩形
力扣 (LeetCode)
发布于 1 年前
67.6k
方法 1：暴力
首先，我们可以想到，两个柱子间矩形的高由它们之间最矮的柱子决定。如下图所示：

image.png

因此，我们可以考虑所有两两柱子之间形成的矩形面积，该矩形的高为它们之间最矮柱子的高度，宽为它们之间的距离，这样可以找到所要求的最大面积的矩形。

public class Solution {
   public int largestRectangleArea(int[] heights) {
       int maxarea = 0;
       for (int i = 0; i < heights.length; i++) {
           for (int j = i; j < heights.length; j++) {
               int minheight = Integer.MAX_VALUE;
               for (int k = i; k <= j; k++)
                   minheight = Math.min(minheight, heights[k]);
               maxarea = Math.max(maxarea, minheight * (j - i + 1));
           }
       }
       return maxarea;
   }
}
复杂度分析

时间复杂度：O(n^3)O(n 
3
 )。我们需要使用 O(n)O(n) 的时间找到 O(n^2)O(n 
2
 ) 枚举出来的所有柱子对之间的最矮柱子

空间复杂度：O(1)O(1)。 只需要常数空间的额外变量



方法 2：优化的暴力
算法

我们可以基于方法 1 进行一点点修改来优化算法。我们可以用前一对柱子之间的最低高度来求出当前柱子对间的最低高度。

用数学语言来表达，minheight=\min(minheight, heights(j))minheight=min(minheight,heights(j)) ，其中， heights(j)heights(j) 是第 j 个柱子的高度。

public class Solution {
   public int largestRectangleArea(int[] heights) {
       int maxarea = 0;
       for (int i = 0; i < heights.length; i++) {
           int minheight = Integer.MAX_VALUE;
           for (int j = i; j < heights.length; j++) {
               minheight = Math.min(minheight, heights[j]);
               maxarea = Math.max(maxarea, minheight * (j - i + 1));
           }
       }
       return maxarea;
   }
}
复杂度分析

时间复杂度：O(n^2)O(n 
2
 )。 需要枚举所有可能的柱子对。

空间复杂度：O(1)O(1) 。不需要额外的空间。



方法 3：分治
算法

通过观察，可以发现，最大面积矩形存在于以下几种情况：

确定了最矮柱子以后，矩形的宽尽可能往两边延伸。

在最矮柱子左边的最大面积矩形（子问题）。

在最矮柱子右边的最大面积矩形（子问题）。

举个例子：

[6, 4, 5, 2, 4, 3, 9]
这里最矮柱子高度为 2 。以 2 为高的最大子矩阵面积是 2x7=14 。现在，我们考虑上面提到的第二种和第三种情况。我们对高度为 2 柱子的左边和右边采用同样的过程。在 2 的左边， 4 是最小的，形成区域为 4x3=12 。将左边区域再继续分，矩形的面积分别为 6x1=6 和 5x1=5 。同样的，我们可以求出右边区域的面积为 3x3=9, 4x1=4 和 9x1=9 。因此，我们得到最大面积是 16 。具体过程可参考下图：

image.png

public class Solution {
    public int calculateArea(int[] heights, int start, int end) {
        if (start > end)
            return 0;
        int minindex = start;
        for (int i = start; i <= end; i++)
            if (heights[minindex] > heights[i])
                minindex = i;
        return Math.max(heights[minindex] * (end - start + 1), Math.max(calculateArea(heights, start, minindex - 1), calculateArea(heights, minindex + 1, end)));
    }
    public int largestRectangleArea(int[] heights) {
        return calculateArea(heights, 0, heights.length - 1);
    }
}
复杂度分析

时间复杂度：

平均开销：O\big(n \log n\big)O(nlogn)

最坏情况：O(n^2)O(n 
2
 )。如果数组中的数字是有序的，分治算法将没有任何优化效果。

空间复杂度：O(n)O(n)。最坏情况下递归需要 O(n)O(n) 的空间。



方法 4：优化的分治
算法

可以观察到，方法 3 中大的问题被分解成更小的子问题来求解，所以分治方法会有一定程度的优化。但是如果数组本身是升序或者降序的，将没有优化作用，原因是每次我们都需要在一个很大的 O(n)O(n) 级别的数组里找最小值。因此，最坏情况下总的时间复杂度变成了 O(n^2)O(n 
2
 ) 。我们可以用线段树代替遍历来找到区间最小值。单词查询复杂度就变成了O\big(\log n\big)O(logn) 。

对于具体实现，可以查看 这里 。

复杂度分析

时间复杂度：O\big(n\log n\big)O(nlogn) 。 对于长度为 nn 的查询，线段树需要 \log nlogn 的时间。

空间复杂度：O(n)O(n)。这是线段树的空间开销。



方法 5：栈
算法

在这种方法中，我们维护一个栈。一开始，我们把 -1 放进栈的顶部来表示开始。初始化时，按照从左到右的顺序，我们不断将柱子的序号放进栈中，直到遇到相邻柱子呈下降关系，也就是 a[i-1] > a[i]a[i−1]>a[i] 。现在，我们开始将栈中的序号弹出，直到遇到 stack[j]stack[j] 满足a\big[stack[j]\big] \leq a[i]a[stack[j]]≤a[i] 。每次我们弹出下标时，我们用弹出元素作为高形成的最大面积矩形的宽是当前元素与 stack[top-1]stack[top−1] 之间的那些柱子。也就是当我们弹出 stack[top]stack[top] 时，记当前元素在原数组中的下标为 i ，当前弹出元素为高的最大矩形面积为：

(i-stack[top-1]-1) \times a\big[stack[top]\big].
(i−stack[top−1]−1)×a[stack[top]].

更进一步，当我们到达数组的尾部时，我们将栈中剩余元素全部弹出栈。在弹出每一个元素是，我们用下面的式子来求面积： (stack[top]-stack[top-1]) \times a\big[stack[top]\big](stack[top]−stack[top−1])×a[stack[top]]，其中，stack[top]stack[top]表示刚刚被弹出的元素。因此，我们可以通过每次比较新计算的矩形面积来获得最大的矩形面积。

下面的例子将进一步解释：

[6, 7, 5, 2, 4, 5, 9, 3]


public class Solution {
    public int largestRectangleArea(int[] heights) {
        Stack < Integer > stack = new Stack < > ();
        stack.push(-1);
        int maxarea = 0;
        for (int i = 0; i < heights.length; ++i) {
            while (stack.peek() != -1 && heights[stack.peek()] >= heights[i])
                maxarea = Math.max(maxarea, heights[stack.pop()] * (i - stack.peek() - 1));
            stack.push(i);
        }
        while (stack.peek() != -1)
            maxarea = Math.max(maxarea, heights[stack.pop()] * (heights.length - stack.peek() -1));
        return maxarea;
    }
}
复杂度分析

时间复杂度：O(n)O(n)。 nn 个数字每个会被压栈弹栈各一次。

空间复杂度： O(n)O(n)。用来存放栈中元素。


 
 
 
*/
