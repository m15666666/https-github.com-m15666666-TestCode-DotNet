using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
假设你是一位顺风车司机，车上最初有 capacity 个空座位可以用来载客。由于道路的限制，车 只能 向一个方向行驶（也就是说，不允许掉头或改变方向，你可以将其想象为一个向量）。

这儿有一份行程计划表 trips[][]，其中 trips[i] = [num_passengers, start_location, end_location] 包含了你的第 i 次行程信息：

必须接送的乘客数量；
乘客的上车地点；
以及乘客的下车地点。
这些给出的地点位置是从你的 初始 出发位置向前行驶到这些地点所需的距离（它们一定在你的行驶方向上）。

请你根据给出的行程计划表和车子的座位数，来判断你的车是否可以顺利完成接送所用乘客的任务（当且仅当你可以在所有给定的行程中接送所有乘客时，返回 true，否则请返回 false）。

 

示例 1：

输入：trips = [[2,1,5],[3,3,7]], capacity = 4
输出：false
示例 2：

输入：trips = [[2,1,5],[3,3,7]], capacity = 5
输出：true
示例 3：

输入：trips = [[2,1,5],[3,5,7]], capacity = 3
输出：true
示例 4：

输入：trips = [[3,2,7],[3,7,9],[8,3,9]], capacity = 11
输出：true
 

提示：

你可以假设乘客会自觉遵守 “先下后上” 的良好素质
trips.length <= 1000
trips[i].length == 3
1 <= trips[i][0] <= 100
0 <= trips[i][1] < trips[i][2] <= 1000
1 <= capacity <= 100000
*/
/// <summary>
/// https://leetcode-cn.com/problems/car-pooling/
/// 1094. 拼车
/// 
/// </summary>
class CarPoolingSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public bool CarPooling(int[][] trips, int capacity)
    {
        var starts = new Dictionary<int,int>();
        var stops = new Dictionary<int, int>();
        int max = int.MinValue, min = int.MaxValue;
        int start, stop, passengers;
        foreach (int[] trip in trips)
        {
            start = trip[1];
            stop = trip[2];
            if (!starts.ContainsKey(start)) starts.Add(start, 0);
            if (!stops.ContainsKey(stop)) stops.Add(stop, 0);

            passengers = trip[0];
            starts[start] += passengers;
            stops[stop] += passengers;
            if (start < min) min = start;
            if (max < stop) max = stop;
        }

        int currentPassengers = 0;
        for (int site = min; site <= max; site++)
        {
            if (stops.ContainsKey(site)) currentPassengers -= stops[site];
            if (starts.ContainsKey(site)) currentPassengers += starts[site];
            if (capacity < currentPassengers) return false;
        }
        return true;
    }
}
/*
简单的哈希映射求解
WuSijia
发布于 21 天前
68 阅读
一个Map(上车点，上车人数)，一个Map(下车点，下车人数)。然后for循环整个路程，在每个下车点减下车的人数，每个上车点加上车的人数，期间如果总人数大于capacity，返回false，否则返回true。

class Solution {
    public boolean carPooling(int[][] trips, int capacity) {
        Map<Integer,Integer> start=new HashMap<>();
        Map<Integer,Integer> end=new HashMap<>();
        //min———max：整个行驶区间
        int max=trips[0][2],min=trips[0][1];
        for(int [] t:trips){
            //遇到上车点或者下车点相同，把它们的人数相加即可
            start.put(t[1],start.getOrDefault(t[1],0)+t[0]);
            end.put(t[2],end.getOrDefault(t[2],0)+t[0]);
            min=Math.min(min,t[1]);
            max=Math.max(max,t[2]);
        }
        int cur=0; //现在车上的人数
        for(int i=min;i<=max;i++){
            if(end.containsKey(i))
                cur-=end.get(i);
            if(start.containsKey(i))
                cur+=start.get(i);
            if(cur>capacity)
                return false;
        }
        return true;
    }
}
下一篇：统计

public class Solution {
    public bool CarPooling(int[][] trips, int capacity) {
        List<int> mPassenger = new List<int>(1001);
            for (var i = 0; i <= 1000; i++)
            {
                mPassenger.Add(0);
            }
            foreach (var aTrip in trips)
            {
                mPassenger[aTrip[1]] += aTrip[0];
                mPassenger[aTrip[2]] -= aTrip[0];
            }

            var p = 0;
            foreach (var i in mPassenger)
            {
                p += i;
                if (p > capacity) return false;
            }

            return true;
    }
}

public class Solution
{
	public bool CarPooling(int[][] trips, int capacity)
	{
		int[] count = new int[1005];
		for (int i=0;i<trips.Length;++i)
		{
			for (int j=trips[i][1];j<trips[i][2];++j)
			{
				count[j] += trips[i][0];
			}
		}

		for (int i = 0; i <= 1000; ++i)
		{
			if (count[i] > capacity)
			{
				return false;
			}
		}

		return true;
	}
}
 
*/
