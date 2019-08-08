using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个整数数组  nums 和一个正整数 k，找出是否有可能把这个数组分成 k 个非空子集，其总和都相等。

示例 1：

输入： nums = [4, 3, 2, 3, 5, 2, 1], k = 4
输出： True
说明： 有可能将其分成 4 个子集（5），（1,4），（2,3），（2,3）等于总和。
 

注意:

1 <= k <= len(nums) <= 16
0 < nums[i] < 10000 
*/
/// <summary>
/// https://leetcode-cn.com/problems/partition-to-k-equal-sum-subsets/
/// 698. 划分为k个相等的子集
/// https://blog.csdn.net/wangjianxin97/article/details/89255388
/// </summary>
class PartitionToKEqualSumSubsetsSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public bool CanPartitionKSubsets(int[] nums, int k)
    {
        if (nums == null || nums.Length == 0 || k < 1) return false;

        int sum = nums.Sum();
        if (sum % k != 0) return false;

        bool[] visited = new bool[nums.Length];
        Array.Fill(visited, false);

        return Dfs(nums, k, sum / k, 0, 0, visited);
    }
    private static bool Dfs(int[] nums, int k, int target, int currentSum, int startIndex, bool[] visited)
    {
        if (k == 1) return true;//此时只需要一个子集合 当前的就是 满足

        if (currentSum > target) return false;

        if (currentSum == target)
        {
            return Dfs(nums, k - 1, target, 0, 0, visited);// 找到了当前符合的子集合，要继续查找下一个，同时要把currentSum和startIndex清零
        }

        for (int i = startIndex; i < nums.Length; ++i)
        {
            if (!visited[i])
            {
                visited[i] = true;//进行标记
                if ( Dfs(nums, k, target, currentSum + nums[i], i + 1, visited) ) return true;
                visited[i] = false;//否则重置设为未访问
            }
        }
        return false;
    }
}
/*
public class Solution {
    public bool CanPartitionKSubsets(int[] nums, int k) {
        if (nums.Length < 1) return false;
        if (k <= 1) return true;
        int i=nums.Sum() % k;
        if (i != 0) return false;
        Array.Sort(nums);
        List<int>list=new  List<int>(nums);
        int c = nums.Sum() / k;
        bool v = true;
        for (int s = list.Count() - 1; s >= 0; s--)
        {
            if (list[s] == c)
            {
                list.RemoveAt(s);
                k--;
                continue;
            }
            else if (list[s] > c) v = false;
            if (list[s] < 0) v = true;
                
        }
        if (!v) return false;
        return CanPartitionKSubsets(list, k, c);
    }
    public static bool CanPartitionKSubsets(List<int> list, int k, int target, int sum = 0)
    {
            
        if (k == 0) return true;
        for (int i = list.Count()-1; i >=0; i--)
        {
            if (sum + list[i] == target)
            {
                int temp = list[i];
                list.RemoveAt(i);
                if (CanPartitionKSubsets(list, k - 1, target,0))
                {
                    return true;
                }
                else
                {
                    list.Insert(i, temp);
                    //list.Add(temp);
                }
            }
            else if (sum + list[i] < target)
            {
                int temp = list[i];
                list.RemoveAt(i);
                if (CanPartitionKSubsets(list,k,target,sum + temp))
                {
                    return true;
                }
                else
                {
                    list.Insert(i,temp);
                    //list.Add(temp);
                }
            }
        }
        return false;
    }
} 
public class Solution {
    public bool CanPartitionKSubsets(int[] nums, int k) {
        float sum=0;
        int length=nums.Length;
        List<int>list=new List<int>();
        for(int i=0;i<length;i++)
        {
            sum+=nums[i];
            list.Add(nums[i]);
        }
        float sumK=sum/k;
        if(sumK!=(int)sum/k)return false;
        list.Sort();
        
        int []arr=new int[k];
        for (int i=0;i<k;i++)
        {
            arr[i]=(int)sumK;
        }
        
        return GetAns(list,nums.Length-1,arr,k);
    }
    
    bool GetAns(List<int> nums,int cur,int []arr,int k)
    {
        if(cur<0)return true;
        for(int i=0;i<k;i++)
        {
            if(arr[i]==nums[cur]||arr[i]-nums[cur]>=nums[0])
            {
                arr[i]-=nums[cur];
                if(GetAns(nums,cur-1,arr,k))return true;
                arr[i]+=nums[cur];
            }
        }
        return false;
    }
}
*/
