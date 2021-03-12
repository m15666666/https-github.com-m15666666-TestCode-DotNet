/*
给定平面上 n 对 互不相同 的点 points ，其中 points[i] = [xi, yi] 。回旋镖 是由点 (i, j, k) 表示的元组 ，其中 i 和 j 之间的距离和 i 和 k 之间的距离相等（需要考虑元组的顺序）。

返回平面上所有回旋镖的数量。
 
示例 1：

输入：points = [[0,0],[1,0],[2,0]]
输出：2
解释：两个回旋镖为 [[1,0],[0,0],[2,0]] 和 [[1,0],[2,0],[0,0]]
示例 2：

输入：points = [[1,1],[2,2],[3,3]]
输出：2
示例 3：

输入：points = [[1,1]]
输出：0

提示：

n == points.length
1 <= n <= 500
points[i].length == 2
-104 <= xi, yi <= 104
所有点都 互不相同
通过次数18,396提交次数30,977
*/

using System.Collections.Generic;

/// <summary>
/// https://leetcode-cn.com/problems/number-of-boomerangs/
/// 447. 回旋镖的数量
///
///
/// </summary>
internal class NumberOfBoomerangsSolution
{
    public int NumberOfBoomerangs(int[][] points)
    {
        int ret = 0;
        int n = points.Length;
        var distance2Count = new Dictionary<int, int>();
        //var distanceCache = new Dictionary<int, int>(); // 加了距离的缓存之后反而慢了，因为key也有计算量
        for (int i = 0; i < n; i++)
        {
            var pI = points[i];
            var x = pI[0];
            var y = pI[1];
            for (int j = 0; j < n; j++)
            {
                if (j == i) continue;
                int distance;
                //int key = i < j ? i * 500 + j :  j + 500 + i;
                //if (distanceCache.ContainsKey(key)) distance = distanceCache[key];
                //else
                {
                    int dx = x - points[j][0];
                    int dy = y - points[j][1];
                    distance = dx * dx + dy * dy;
                    //distanceCache[key] = distance;
                }
                distance2Count[distance] = distance2Count.GetValueOrDefault(distance, 0) + 1;
            }
            foreach (int count in distance2Count.Values)
                ret += count * (count - 1); // 回旋标顶点之外的两个点两个位置排列的数量

            distance2Count.Clear();
        }
        return ret;
    }
}

/*
447. 回旋镖的数量，用哈希表记录距离
郁郁雨

发布于 2021-02-21
229
447. 回旋镖的数量
思路
用哈希表记录每个点到其他点的距离，每个距离相同的点个数为val
外层的for循环遍历所有点，作为回旋镖的第一个点，内层循环遍历所有点，计算回旋镖的第一个点和其他点的距离，并记录到哈希表中
遍历哈希表，从val个距离相同的点中选2个点，作为回旋镖的后两个点，共有val* (val - 1)种情况
代码

class Solution {
    public int numberOfBoomerangs(int[][] points) {
        int res = 0;
        for(int i = 0; i < points.length; i++){
            Map<Integer, Integer> hashmap = new HashMap<>();
            for(int j = 0; j < points.length; j++){
                int dx = points[i][0] - points[j][0];
                int dy = points[i][1] - points[j][1];
                int dis = dx * dx + dy * dy;
                hashmap.put(dis, hashmap.getOrDefault(dis, 0) + 1);
            }
            for(int val : hashmap.values()){
                res += val * (val - 1);
            }
        }
        return res;
    }
}
复杂度分析

时间复杂度：O(n^2)
空间复杂度：O(n)

public class Solution {
    public int NumberOfBoomerangs(int[][] points) {
                int res = 0;
                foreach (var point in points) {
                    var dict = new Dictionary<int, int>();
                    foreach (var targetPoint in points) {
                        int dx = point[0] - targetPoint[0];
                        int dy = point[1] - targetPoint[1];
                        var dis = dx * dx + dy * dy;
                        dict[dis] = dict.GetValueOrDefault(dis, 0) + 1;
                    }
                    foreach (var(key, value) in dict) {
                        res += (value * (value - 1));
                    }
                }
                return res;
    }
}

public class Solution {
    public int NumberOfBoomerangs(int[][] points) {
        int num = 0;
        for (int i = 0; i < points.Length; i++) {
            Dictionary<int, int> dic = new Dictionary<int, int> ();

            for (int j = 0; j < points.Length; j++) {
                int dx = points[i][0] - points[j][0], dy = points[i][1] - points[j][1];
                int dis = dx * dx + dy * dy;
                if (dic.ContainsKey (dis)) dic[dis]++;
                else dic[dis] = 1;
            }
            foreach (var item in dic) {
                var n = item.Value;
                if (n == 1) continue;
                num += n * (n - 1);
            }

        }
        return num;
    }
}

*/