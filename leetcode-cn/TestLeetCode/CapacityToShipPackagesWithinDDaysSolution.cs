using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
传送带上的包裹必须在 D 天内从一个港口运送到另一个港口。

传送带上的第 i 个包裹的重量为 weights[i]。每一天，我们都会按给出重量的顺序往传送带上装载包裹。我们装载的重量不会超过船的最大运载重量。

返回能在 D 天内将传送带上的所有包裹送达的船的最低运载能力。

 

示例 1：

输入：weights = [1,2,3,4,5,6,7,8,9,10], D = 5
输出：15
解释：
船舶最低载重 15 就能够在 5 天内送达所有包裹，如下所示：
第 1 天：1, 2, 3, 4, 5
第 2 天：6, 7
第 3 天：8
第 4 天：9
第 5 天：10

请注意，货物必须按照给定的顺序装运，因此使用载重能力为 14 的船舶并将包装分成 (2, 3, 4, 5), (1, 6, 7), (8), (9), (10) 是不允许的。 
示例 2：

输入：weights = [3,2,2,4,1,4], D = 3
输出：6
解释：
船舶最低载重 6 就能够在 3 天内送达所有包裹，如下所示：
第 1 天：3, 2
第 2 天：2, 4
第 3 天：1, 4
示例 3：

输入：weights = [1,2,3,1,1], D = 4
输出：3
解释：
第 1 天：1
第 2 天：2
第 3 天：3
第 4 天：1, 1
 

提示：

1 <= D <= weights.length <= 50000
1 <= weights[i] <= 500
*/
/// <summary>
/// https://leetcode-cn.com/problems/capacity-to-ship-packages-within-d-days/
/// 1011. 在 D 天内送达包裹的能力
/// 
/// </summary>
class CapacityToShipPackagesWithinDDaysSolution
{
    public void Test()
    {
        var ret = ShipWithinDays(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, 5);
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int ShipWithinDays(int[] weights, int D)
    {
        int low = 0;
        int high = weights.Sum();
        while (low < high)
        {
            int mid = low + (high - low) / 2;
            if (CanShip(weights, D, mid)) high = mid;
            else low = mid + 1;
        }
        return low;
    }

    private static bool CanShip(int[] weights, int D, int capacity)
    {
        int capacityOfToday = capacity;
        foreach (int weight in weights)
        {
            if ( capacity < weight ) return false;
            if (capacityOfToday < weight)
            {
                capacityOfToday = capacity;
                D--;
            }
            capacityOfToday -= weight;
        }
        return 0 < D;
    }
}
/*
在D天内送达包裹的能力
KLEA123
发布于 5 个月前
1.5k 阅读
方法一：二分查找
思路：
对于一艘承载力为K船来说，我们必然会在不超过其承载力的前提下贪心地往上装载货物，这样才能使得运送包裹所花费的时间最短。
如果船在承载力为K的条件下可以完成在D天内送达包裹的任务，那么任何承载力大于K的条件下依然也能完成任务。
我们可以让这个承载力K从max(weights)max(weights)开始（即所有包裹中质量最大包裹的重量，低于这个重量我们不可能完成任务），逐渐增大承载力K，直到K可以让我们在D天内送达包裹。此时K即为我们所要求的最低承载力。
逐渐增大承载力K的方法效率过低，让我们用二分查找的方法来优化它。
相似题目：

算法：
定义函数canShip(D, K)canShip(D,K),来判断在最低承载力为KK的情形下能否在DD天内送达所有包裹。我们所要做的就是按照传送带上货物的顺序，依次且尽可能多地往船上装载货物，当该艘船无法装下更多货物时，我们换一搜船，同时将天数加11。当运输完所有货物后，我们判断所用的天数是否小于等于DD。
用二分查找的方式，来查找这个最低承载力，如果midmid可以完成任务，我们把查找范围缩减至[lo, mid][lo,mid]（注意不是mid+1mid+1,因为midmid可能是我们所求的解），否则我们去[mid+1, hi][mid+1,hi]区间中继续查找，详情见代码。
class Solution {
    public int shipWithinDays(int[] weights, int D) {
        int lo = 0, hi = Integer.MAX_VALUE;
        while (lo < hi) {
        	int mid = lo + (hi - lo) / 2;
        	if (canShip(weights, D, mid)) {
        		hi = mid;
        	} else {
        		lo = mid+1;
        	}
        }
        return lo;
    }
    private boolean canShip(int[] weights, int D, int K) {
    	int cur = K; // cur 表示当前船的可用承载量
    	for (int weight: weights) {
    		if (weight > K) return false;
    		if (cur < weight) {
    			cur = K;
    			D--;
    		}
    		cur -= weight;
    	}
    	return D > 0; // 能否在D天内运完
    }
}
复杂度分析：
时间复杂度：O(NlogN)O(NlogN)
空间复杂度：O(1)O(1)

public class Solution {
    public int ShipWithinDays(int[] weights, int D) {
        int left = 0, right = 0;
        foreach(int weight in weights)
        {
            left = Math.Max(left, weight);
            right += weight;
        }
        
        while(left < right)
        {
            int mid = left + (right - left)/2, packageCount = 1, curWeight = 0;
            foreach(int w in weights)
            {
                if(curWeight + w > mid)
                {
                    packageCount++;
                    curWeight = w;
                }
                else
                    curWeight += w;
            }
            if(packageCount > D)
                left = mid + 1;
            else
                right = mid;
        }
        return left;
    }
}

 
*/
