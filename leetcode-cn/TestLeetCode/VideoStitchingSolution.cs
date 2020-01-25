using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
你将会获得一系列视频片段，这些片段来自于一项持续时长为 T 秒的体育赛事。这些片段可能有所重叠，也可能长度不一。

视频片段 clips[i] 都用区间进行表示：开始于 clips[i][0] 并于 clips[i][1] 结束。我们甚至可以对这些片段自由地再剪辑，例如片段 [0, 7] 可以剪切成 [0, 1] + [1, 3] + [3, 7] 三部分。

我们需要将这些片段进行再剪辑，并将剪辑后的内容拼接成覆盖整个运动过程的片段（[0, T]）。返回所需片段的最小数目，如果无法完成该任务，则返回 -1 。

 

示例 1：

输入：clips = [[0,2],[4,6],[8,10],[1,9],[1,5],[5,9]], T = 10
输出：3
解释：
我们选中 [0,2], [8,10], [1,9] 这三个片段。
然后，按下面的方案重制比赛片段：
将 [1,9] 再剪辑为 [1,2] + [2,8] + [8,9] 。
现在我们手上有 [0,2] + [2,8] + [8,10]，而这些涵盖了整场比赛 [0, 10]。
示例 2：

输入：clips = [[0,1],[1,2]], T = 5
输出：-1
解释：
我们无法只用 [0,1] 和 [0,2] 覆盖 [0,5] 的整个过程。
示例 3：

输入：clips = [[0,1],[6,8],[0,2],[5,6],[0,4],[0,3],[6,7],[1,3],[4,7],[1,4],[2,5],[2,6],[3,4],[4,5],[5,7],[6,9]], T = 9
输出：3
解释： 
我们选取片段 [0,4], [4,7] 和 [6,9] 。
示例 4：

输入：clips = [[0,4],[2,8]], T = 5
输出：2
解释：
注意，你可能录制超过比赛结束时间的视频。
 

提示：

1 <= clips.length <= 100
0 <= clips[i][0], clips[i][1] <= 100
0 <= T <= 100 
*/
/// <summary>
/// https://leetcode-cn.com/problems/video-stitching/
/// 1024. 视频拼接
/// 
/// </summary>
class VideoStitchingSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int VideoStitching(int[][] clips, int T)
    {
        clips = clips.OrderBy(i => i[0]).ThenBy(i => i[1]).ToArray();
        
        int ret = 0;
        int end = 0; // 一开始取起始点为0的片段
        for( int index = 0; index < clips.Length && end < T; )
        {
            if (clips[index][0] <= end)
            {
                int previousEnd = end;
                while (index < clips.Length && clips[index][0] <= previousEnd)
                {
                    end = Math.Max(end, clips[index][1]);
                    index++;
                }
                ret++;
            }
            else break;
        }
        return T <= end ? ret : -1;
    }
}
/*
算法
(贪心) 时间：O(nlogn) & 空间：O(n)
对区间进行排序。
在已选区间最后边界为last ， 贪心地选后面区间开头能overlap last的区间能够覆盖到最远距离的区间。
直到last >= T为止。
若找不到overlap区间则无解，返回-1。
C++ 代码
class Solution {
public:
    int videoStitching(vector<vector<int>>& clips, int T) {
        int n = clips.size();
        
        sort(clips.begin(), clips.end());
        
        // last：代表上一个区间能覆盖的末尾
        int ans = 0, last = 0;
        for (int i = 0, j = 0; i < n; i++) {
            if (last >= T) break;
            int maxL = last;
            while (j < n && clips[j][0] <= last) {  // 贪心地在后面区间能够overlap的区间中找最长的
                maxL = max(maxL, clips[j][1]);
                j++;
            }
            if (j == i) return -1;  //没有能选的就不能覆盖掉区间
            last = maxL;
            i = j - 1;
            ans++;
        }
        
        return last < T ? -1 : ans;
    }
};

贪心算法C++
Dou Walker
737 阅读
这道题和算法书上的活动选择问题基本一致。

利用贪心算法：在开始时间不大于t的视频中选择一个结束时间最大的那一个视频，其中t是上一个选择视频的结束时间。

算法：

对视频进行升序排列
选择一个从零开始的视频，它的结束时间是所有从零开始的视频中最大的，赋值给t
然后在其他视频中选择一个开始时间不超过t的视频，这个被选择的视频的结束时间也应该是最大的，赋值给t
重复3，直到所有的视频都被遍历，或者t已经大于等于T
代码如下：

static bool cmp(vector<int>& v1, vector<int>& v2) {
    return v1[0] < v2[0] || (v1[0] == v2[0] && v1[1] < v2[1]);
}

int videoStitching(vector<vector<int>>& clips, int T) {
        int t = 0;
        sort(clips.begin(), clips.end(), cmp);
        int i = 0, ans = 0, last_t;
        while (i < clips.size() && t < T) {
            if (clips[i][0] <= t) {
                last_t = t;
                while (i<clips.size() && clips[i][0] <= last_t) {
                    t = max(t, clips[i][1]);
                    i++;
                }
                ans++;
            } else {
                break;
            }
        }
        return t>=T?ans:-1;
    }

public class Solution {
	public int VideoStitching(int[][] clips, int T)
	{
		//排序
		Array.Sort(clips, (int[] o1, int[] o2) =>
		{
			return o1[0].CompareTo(o2[0]);
		});
		if (clips[clips.Length - 1][1] < T)
			return -1;
		int[] dp = new int[101];//记录符合要求的区间
		int maxRight = 0;//记录右侧最大值
		Array.Fill(dp,-1);
		for (int i = 0; i < clips.Length; i++)
		{
			if (T < clips[i][0])//开始区间比T大就不需要判断
				break;
			if (clips[i][0] > maxRight)//开始区间比最大值大，就连接不上，跳过
				break;
			if (maxRight < clips[i][1])//结束区间比最大值大，就替换最大区间
			{
				dp[clips[i][0]] = clips[i][1];
				maxRight = dp[clips[i][0]];
			}
		}
		var newDP = dp;//去掉-1

		maxRight = 0;
		int result = 0;
		for (int i = 1; i <= T; i++)
		{
			bool found = false;
			if (maxRight < i)
		   
			{
				for (int j = 0; j < newDP.Length; j++)
				{
					if (i - 1 >= j && i <= newDP[j])
					{
						found = true;
						maxRight = Math.Max(maxRight, newDP[j]);
					}
				}
				if (found)
				{
					result++;
				}
				else
				{
					result = -1;
					break;
				}
			}
		}
		return result;
	}
}

public class Solution {
    public int VideoStitching(int[][] clips, int T) {
        int[] dp = new int[T + 1];
        for(var i=0;i<=T;i++){
            dp[i] = T+1;
        }
        dp[0] = 0;
        for (int i = 1; i <= T && dp[i - 1] < T; i++) {
            foreach (int[] c in clips) {
                if (c[0] <= i && i <= c[1])
                    dp[i] = Math.Min(dp[i], dp[c[0]] + 1);
            }
        }
        return dp[T] == T + 1 ? -1 : dp[T];
    }
} 
*/
