using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个包含 n 个整数的数组 nums 和一个目标值 target，判断 nums 中是否存在四个元素 a，b，c 和 d ，使得 a + b + c + d 的值与 target 相等？找出所有满足条件且不重复的四元组。

注意：

答案中不可以包含重复的四元组。

示例：

给定数组 nums = [1, 0, -1, 0, -2, 2]，和 target = 0。

满足要求的四元组集合为：
[
  [-1,  0, 0, 1],
  [-2, -1, 1, 2],
  [-2,  0, 0, 2]
]
*/
/// <summary>
/// https://leetcode-cn.com/problems/4sum/
/// 18. 四数之和
/// 
/// </summary>
class FourSumSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    public IList<IList<int>> FourSum(int[] nums, int target)
    {
        List<IList<int>> ret = new List<IList<int>>();

        if (nums == null || nums.Length < 4) return ret;

        Array.Sort(nums);

        //HashSet<string> matches = new HashSet<string>();

        int len = nums.Length;
        int upper = len - 3;
        //int end4ThreeSum = upper;
        Span<int> span = nums.AsSpan();
        for (int i = 0; i < upper; ++i)
        {
            var v = nums[i];
            if (0 < i && v == nums[i - 1]) continue;

            //var list = ThreeSum(nums, target - v, i + 1, end4ThreeSum);
            var list = ThreeSum(span.Slice( i + 1, len - i - 1 ), target - v);
            if (list.Count == 0) continue;

            foreach( var sublist in list)
            {
                //string key = $"{v}-{sublist[0]}-{sublist[1]}-{sublist[2]}";
                //if (matches.Contains(key)) continue;

                //matches.Add(key);

                //sublist.Insert(0, v);
                sublist[0] = v;
                ret.Add(sublist);
            }
        }

        return ret;
    }

    private static IList<int[]> ThreeSum(Span<int> nums, int target )
    {
        List<int[]> ret = new List<int[]>();

        //int startIndex = 0;
        int endIndex = nums.Length - 3;
        int end = nums.Length - 1;
        for (int i = 0; i <= endIndex; ++i)
        {
            var v = nums[i];
            if (0 < i && v == nums[i - 1]) continue;

            int target2 = target - v;
            int left = i + 1, right = end;
            while (left < right)
            {
                //int sum = v + nums[l] + nums[r];
                int sum2 = nums[left] + nums[right];
                //var difference = sum - target;
                var difference = sum2 - target2;

                if (difference == 0)
                {
                    ret.Add(new int[] { 0, v, nums[left], nums[right] });

                    while (left < right && nums[left] == nums[left + 1]) ++left;
                    while (left < right && nums[right] == nums[right - 1]) --right;

                    ++left;
                    --right;
                }
                else if (difference < 0) ++left;
                else --right;
            }
        }

        return ret;
    }
}
/*

双指针解法。参照三数之和，嗝。
MisakaSagiri
发布于 6 个月前
18.2k
思路：

 四数之和与前面三数之和的思路几乎是一样的，嗝。（刚好前些天才写了三数之和的题解）
 如果前面的三数之和会做了的话，这里其实就是在前面的基础上多添加一个遍历的指针而已。
 会做三数之和的可以不用看下面的了。。
  
 使用四个指针(a<b<c<d)。固定最小的a和b在左边，c=b+1,d=_size-1 移动两个指针包夹求解。
 保存使得nums[a]+nums[b]+nums[c]+nums[d]==target的解。偏大时d左移，偏小时c右移。c和d相
 遇时，表示以当前的a和b为最小值的解已经全部求得。b++,进入下一轮循环b循环，当b循环结束后。
 a++，进入下一轮a循环。 即(a在最外层循环，里面嵌套b循环，再嵌套双指针c,d包夹求解)。
准备工作：

 因为要使用双指针的方法，排序是必须要做der~。 时间复杂度O(NlogN).
解决重复解：

 上面的解法存在重复解的原因是因为移动指针时可能出现重复数字的情况。所以我们要确保移动指针后，
 对应的数字要发生改变才行哦。
时间复杂度：

a遍历O(N)里嵌套b遍历O(N)再嵌套c,d双指针O(N)--> O(N^3)。 总比暴力法O(N^4)好些吧。
1569476546(1).png

代码块

class Solution{
	public: 
	vector<vector<int>> fourSum(vector<int>& nums, int target) {
        sort(nums.begin(),nums.end());
        vector<vector<int> > res;
        if(nums.size()<4)
        return res;
        int a,b,c,d,_size=nums.size();
        for(a=0;a<=_size-4;a++){
        	if(a>0&&nums[a]==nums[a-1]) continue;      //确保nums[a] 改变了
        	for(b=a+1;b<=_size-3;b++){
        		if(b>a+1&&nums[b]==nums[b-1])continue;   //确保nums[b] 改变了
        		c=b+1,d=_size-1;
        		while(c<d){
        			if(nums[a]+nums[b]+nums[c]+nums[d]<target)
        			    c++;
        			else if(nums[a]+nums[b]+nums[c]+nums[d]>target)
        			    d--;
        			else{
        				res.push_back({nums[a],nums[b],nums[c],nums[d]});
        				while(c<d&&nums[c+1]==nums[c])      //确保nums[c] 改变了
        				    c++;
        				while(c<d&&nums[d-1]==nums[d])      //确保nums[d] 改变了
        				    d--;
        				c++;
        				d--;
					}
				}
			}
		}
		return res;
    }
};
有不对的地方麻烦帮忙指正一下😄。

觉得有用给点个赞呢，看到右上角小铃铛提示有人点赞的感觉简直不要太爽。

下一篇：被 95% 的用户击败！

public class Solution {
    public IList<IList<int>> FourSum(int[] nums, int target) {
        Array.Sort(nums);
        var res = new List<IList<int>>();
        var len = nums.Length;

        // move [i]
        for (int i = 0; i < len - 3; i++)
        {
            if (nums[i] > target && nums[i + 1] > 0) break;
            if (nums[i] + nums[i + 1] + nums[i + 2] + nums[i + 3] > target) break;
            if (i > 0 && nums[i] == nums[i - 1]) continue;
            if (nums[i] + nums[len - 1] + nums[len - 2] + nums[len - 3] < target) continue;

            // move [j]
            for (int j = i + 1; j < len - 2; j++)
            {
                if (nums[i] + nums[j] + nums[j + 1] + nums[j + 2] > target) break;
                if (j > i + 1 && nums[j] == nums[j - 1]) continue;
                if (nums[i] + nums[j] + nums[len - 1] + nums[len - 2] < target) continue;

                var L = j + 1;
                var R = len - 1;

                // move [L & R]
                while (L < R)
                {
                    var sum = nums[i] + nums[j] + nums[L] + nums[R];
                    if (sum == target)
                    {
                        res.Add(new List<int>(4) { nums[i], nums[j], nums[L], nums[R] });
                        
                        while (L < R && nums[L] == nums[L + 1]) L++;
                        L++;

                        while (L < R && nums[R] == nums[R - 1]) R--;
                        R--;
                    }
                    else if (sum < target)
                    {
                        L++;
                    }
                    else 
                    {
                        R--;
                    }
                }
            }
        }
        return res;
    }
} 
*/
