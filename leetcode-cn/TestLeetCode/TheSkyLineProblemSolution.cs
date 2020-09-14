using System;
using System.Collections.Generic;

/*
城市的天际线是从远处观看该城市中所有建筑物形成的轮廓的外部轮廓。现在，假设您获得了城市风光照片（图A）上显示的所有建筑物的位置和高度，请编写一个程序以输出由这些建筑物形成的天际线（图B）。

每个建筑物的几何信息用三元组 [Li，Ri，Hi] 表示，其中 Li 和 Ri 分别是第 i 座建筑物左右边缘的 x 坐标，Hi 是其高度。可以保证 0 ≤ Li, Ri ≤ INT_MAX, 0 < Hi ≤ INT_MAX 和 Ri - Li > 0。您可以假设所有建筑物都是在绝对平坦且高度为 0 的表面上的完美矩形。

例如，图A中所有建筑物的尺寸记录为：[ [2 9 10], [3 7 15], [5 12 12], [15 20 10], [19 24 8] ] 。

输出是以 [ [x1,y1], [x2, y2], [x3, y3], ... ] 格式的“关键点”（图B中的红点）的列表，它们唯一地定义了天际线。关键点是水平线段的左端点。请注意，最右侧建筑物的最后一个关键点仅用于标记天际线的终点，并始终为零高度。此外，任何两个相邻建筑物之间的地面都应被视为天际线轮廓的一部分。

例如，图B中的天际线应该表示为：[ [2 10], [3 15], [7 12], [12 0], [15 10], [20 8], [24, 0] ]。

说明:

任何输入列表中的建筑物数量保证在 [0, 10000] 范围内。
输入列表已经按左 x 坐标 Li  进行升序排列。
输出列表必须按 x 位排序。
输出天际线中不得有连续的相同高度的水平线。例如 [...[2 3], [4 5], [7 5], [11 5], [12 7]...] 是不正确的答案；三条高度为 5 的线应该在最终输出中合并为一个：[...[2 3], [4 5], [12 7], ...]

*/

/// <summary>
/// https://leetcode-cn.com/problems/the-skyline-problem/
/// 218. 天际线问题
///
///
/// </summary>
internal class TheSkyLineProblemSolution
{
    public void Test()
    {
    }

    public IList<IList<int>> GetSkyline(int[][] buildings)
    {
        return GetSkyline(buildings.AsSpan());
    }
    public List<IList<int>> GetSkyline(Span<int[]> buildings)
    {
        int n = buildings.Length;
        var ret = new List<IList<int>>();

        if (n == 0) return ret;
        if (n == 1)
        {
            int xStart = buildings[0][0];
            int xEnd = buildings[0][1];
            int y = buildings[0][2];

            ret.Add(new List<int> { xStart, y });
            ret.Add(new List<int> { xEnd, 0 });
            return ret;
        }

        List<IList<int>> leftSkyline, rightSkyline;
        leftSkyline = GetSkyline(buildings.Slice(0, n / 2));
        rightSkyline = GetSkyline(buildings.Slice(n / 2));

        return MergeSkylines(leftSkyline, rightSkyline);

        List<IList<int>> MergeSkylines(List<IList<int>> left, List<IList<int>> right)
        {
            int nL = left.Count, nR = right.Count;
            int pL = 0, pR = 0;
            int currY = 0, leftY = 0, rightY = 0;
            int x, maxY;
            List<IList<int>> output = new List<IList<int>>();

            while ((pL < nL) && (pR < nR))
            {
                IList<int> pointL = left[pL];
                IList<int> pointR = right[pR];
                if (pointL[0] < pointR[0])
                {
                    x = pointL[0];
                    leftY = pointL[1];
                    pL++;
                }
                else
                {
                    x = pointR[0];
                    rightY = pointR[1];
                    pR++;
                }
                maxY = System.Math.Max(leftY, rightY);
                if (currY != maxY)
                {
                    UpdateOutput(output, x, maxY);
                    currY = maxY;
                }
            }

            AppendSkyline(output, left, pL, nL, currY);

            AppendSkyline(output, right, pR, nR, currY);

            return output;
        }

        void UpdateOutput(List<IList<int>> output, int x, int y)
        {
            if (output.Count == 0 || output[output.Count - 1][0] != x) output.Add(new List<int> { x, y });
            else output[output.Count - 1][1] = y;
        }

        void AppendSkyline(List<IList<int>> output, List<IList<int>> skyline,
                            int p, int n, int currY)
        {
            while (p < n)
            {
                IList<int> point = skyline[p];
                int x = point[0];
                int y = point[1];
                p++;

                if (currY != y)
                {
                    UpdateOutput(output, x, y);
                    currY = y;
                }
            }
        }
    }
}

/*
天际线问题
力扣 (LeetCode)
发布于 2019-06-23
15.2k
方法：分治
想法

这个题是一道经典的分治算法题，通常如同归并排序一样。

我们依据下面的算法流程来求解分治问题：

定义基本问题。

将问题分解为子问题，并递归地分别求解。

将子问题合并成原问题的解。

算法

求解 n 栋楼的天际线：

如果 n == 0：返回一个空列表

如果 n == 1：返回一栋楼的天际线

leftSkyline = 求解前 n/2 栋楼的天际线。

rightSkyline = 求解后 n/2 栋楼的天际线。

合并 leftSkyline 和 rightSkyline.

现在，让我们进一步讨论每一个步骤的细节：

基本问题

最基本的情况就是建筑物列表为空，此时天际线也是一个空列表。

第二种基本情况就是列表中只有一栋楼，天际线是很显然的。

image.png

如何拆分问题

与归并排序的想法类似：在每一步将列表恰好拆分成两个部分：从 0 到 n/2 和 n/2 到 n ，对每一部分分别求解天际线。

image.png

如何合并两部分的天际线

合并的过程十分直接明了，基于相同的归并排序的逻辑：结果天际线的高度永远是左天际线和右天际线的较大值。

image.png

我们这里用两个指针 pR 和 pL 分别记录两个天际线的当前元素，再用三个整数变量 leftY，rightY 和 currY 分别记录 左 天际线、 右 天际线和 合并 天际线的当前高度。

mergeSkylines (left, right) :

currY = leftY = rightY = 0

当我们在一段两个天际线都可以见到的区域时（pR < nR 且 pL < nL）：

选择 x 坐标较小的一个元素，如果它是左天际线的元素，就移动 pL 同时更新 leftY 。如果是右天际线，则移动 pR 且更新 rightY 。

计算较高的高度作为当前点的高度：maxY = max(leftY, rightY) 。

更新结果天际线： (x, maxY)，前提是 maxY 不等于 currY.

如果左天际线还有元素没有被处理，也就是（pL < nL），则按照上述步骤处理这些元素。

如果右天际线还有元素没有被处理，也就是（pR < nR），则按照上述步骤处理这些元素。

返回结果天际线。

这里有 3 个样例来说明合并算法的过程。

image.png

image.png

image.png

实现


class Solution {

  public List<List<Integer>> getSkyline(int[][] buildings) {
    int n = buildings.length;
    List<List<Integer>> output = new ArrayList<List<Integer>>();

    // The base cases 
    if (n == 0) return output;
    if (n == 1) {
      int xStart = buildings[0][0];
      int xEnd = buildings[0][1];
      int y = buildings[0][2];

      output.add(new ArrayList<Integer>() {{add(xStart); add(y); }});
      output.add(new ArrayList<Integer>() {{add(xEnd); add(0); }});
      // output.add(new int[]{xStart, y});
      // output.add(new int[]{xEnd, 0});
      return output;
    }

    // If there is more than one building, 
    // recursively divide the input into two subproblems.
    List<List<Integer>> leftSkyline, rightSkyline;
    leftSkyline = getSkyline(Arrays.copyOfRange(buildings, 0, n / 2));
    rightSkyline = getSkyline(Arrays.copyOfRange(buildings, n / 2, n));

    // Merge the results of subproblem together.
    return mergeSkylines(leftSkyline, rightSkyline);
  }


  public List<List<Integer>> mergeSkylines(List<List<Integer>> left, List<List<Integer>> right) {
    int nL = left.size(), nR = right.size();
    int pL = 0, pR = 0;
    int currY = 0, leftY = 0, rightY = 0;
    int x, maxY;
    List<List<Integer>> output = new ArrayList<List<Integer>>();

    // while we're in the region where both skylines are present
    while ((pL < nL) && (pR < nR)) {
      List<Integer> pointL = left.get(pL);
      List<Integer> pointR = right.get(pR);
      // pick up the smallest x
      if (pointL.get(0) < pointR.get(0)) {
        x = pointL.get(0);
        leftY = pointL.get(1);
        pL++;
      }
      else {
        x = pointR.get(0);
        rightY = pointR.get(1);
        pR++;
      }
      // max height (i.e. y) between both skylines
      maxY = Math.max(leftY, rightY);
      // update output if there is a skyline change
      if (currY != maxY) {
        updateOutput(output, x, maxY);
        currY = maxY;
      }
    }

    // there is only left skyline
    appendSkyline(output, left, pL, nL, currY);

    // there is only right skyline
    appendSkyline(output, right, pR, nR, currY);

    return output;
  }


  public void updateOutput(List<List<Integer>> output, int x, int y) {
    // if skyline change is not vertical - 
    // add the new point
    if (output.isEmpty() || output.get(output.size() - 1).get(0) != x)
      output.add(new ArrayList<Integer>() {{add(x); add(y); }});
      // if skyline change is vertical - 
      // update the last point
    else {
      output.get(output.size() - 1).set(1, y);
    }
  }


  public void appendSkyline(List<List<Integer>> output, List<List<Integer>> skyline,
                            int p, int n, int currY) {
    while (p < n) {
      List<Integer> point = skyline.get(p);
      int x = point.get(0);
      int y = point.get(1);
      p++;

      // update output
      // if there is a skyline change
      if (currY != y) {
        updateOutput(output, x, y);
        currY = y;
      }
    }
  }
}
复杂度分析

时间复杂度：\mathcal{O}(N \log N)O(NlogN) ，其中 NN 是建筑物的数目。这个问题满足 主定理情况II
： T(N) = 2 T(\frac{N}{2}) + 2NT(N)=2T( 
2
N
​	
 )+2N ，求解结果是 \mathcal{O}(N \log N)O(NlogN) 的时间复杂度。
空间复杂度：\mathcal{O}(N)O(N) 。需要额外 O(n)O(n) 的空间来保存结果。

public class Solution {
    public IList<IList<int>> GetSkyline(int[][] buildings) {
        if (buildings.Length == 0) return new List<IList<int>>();
            return Merge(buildings, 0, buildings.Length - 1);
    }

    private IList<IList<int>> Merge(int[][] buildings, int start, int end) 
	{
		IList<IList<int>> res = new List<IList<int>>();
		//只有一个建筑, 将 [x, h], [y, 0] 加入结果
		if (start == end)
		{
			List<int> temp = new List<int>();
			temp.Add(buildings[start][0]);
			temp.Add(buildings[start][2]);
			res.Add(temp);

			temp = new List<int>();
			temp.Add(buildings[start][1]);
			temp.Add(0);
			res.Add(temp);
			return res;
		}
		int mid = (start + end) / 2;
		//第一组解
		IList<IList<int>> Skyline1  = Merge(buildings, start, mid);
		//第二组解
		IList<IList<int>> Skyline2  = Merge(buildings, mid + 1, end);
		//下边将两组解合并
		int h1 = 0;
		int h2 = 0;
		int i = 0;
		int j = 0;
		while (i < Skyline1.Count || j < Skyline2.Count)
		{
			long x1 = i < Skyline1.Count ? Skyline1[i][0] : long.MaxValue;
			long x2 = j < Skyline2.Count ? Skyline2[j][0] : long.MaxValue;
			long x = 0;
			//比较两个坐标
			if (x1 < x2) 
			{
				h1 = Skyline1[i][1];
				x = x1;
				i++;
			} 
			else if (x1 > x2)
			{
				h2 = Skyline2[j][1];
				x = x2;
				j++;
			} 
			else
			{
				h1 = Skyline1[i][1];
				h2 = Skyline2[j][1];
				x = x1;
				i++;
				j++;
			}
			//更新 height
			int height = Math.Max(h1, h2);
			//重复的解不要加入
			if (res.Count == 0 || height != res[res.Count - 1][1])
			{
				IList<int> temp = new List<int>();
				temp.Add((int) x);
				temp.Add(height);
				res.Add(temp);
			}
		}
		return res;
	}
}


*/