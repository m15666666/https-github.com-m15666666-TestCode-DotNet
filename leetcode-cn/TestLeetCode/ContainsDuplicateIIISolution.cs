using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// https://leetcode-cn.com/problems/contains-duplicate-iii/
/// 220. 存在重复元素 III
/// 给定一个整数数组，判断数组中是否有两个不同的索引 i 和 j，
/// 使得 nums [i] 和 nums [j] 的差的绝对值最大为 t，并且 i 和 j 之间的差的绝对值最大为 ķ。
/// 示例 1:
/// 输入: nums = [1,2,3,1], k = 3, t = 0
/// 输出: true
/// 示例 2:
/// 输入: nums = [1,0,1,1], k = 1, t = 2
/// 输出: true
/// 示例 3:
/// 输入: nums = [1,5,9,1,5,9], k = 2, t = 3
/// 输出: false
/// </summary>
class ContainsDuplicateIIISolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    public bool ContainsNearbyAlmostDuplicate(int[] nums, int k, int t)
    {
        if (t == 0) return ContainsNearbyDuplicate(nums, k);
        int length = nums.Length;
        int lengthMinusOne = length - 1;
        for ( int index = 0; index < lengthMinusOne; index++)
        {
            int lastIndex = Math.Min(index + k, lengthMinusOne);
            long v = nums[index];
            for (int j = index + 1; j <= lastIndex; j++)
            {
                if (Math.Abs(v - nums[j]) <= t) return true;
            }
        }
        return false;
    }

    private static bool ContainsNearbyDuplicate(int[] nums, int k)
    {
        if (nums == null || nums.Length < 2 || k < 1) return false;

        Dictionary<int, int> num2Index = new Dictionary<int, int>();
        int index = -1;
        foreach (var num in nums)
        {
            index++;
            if (!num2Index.ContainsKey(num))
            {
                num2Index.Add(num, index);
                continue;
            }
            if (index - num2Index[num] <= k) return true;
            num2Index[num] = index;
        }
        return false;
    }
}

/*
// 别人的算法
public class Solution {
    public bool ContainsNearbyAlmostDuplicate(int[] nums, int k, int t) {
            if (t < 0) return false;
            int length = nums.Length;
            if (length == 0 || length == 1) return false;
            Hashtable htb = new Hashtable();
            int head = 0;
            int tail = k < length - 1 ? k : length - 1;
            for (int i = head; i <= tail; i++)
            {
                if (IsContain(htb, nums[i], t)) return true;
                else { htb.Add(nums[i], 0); }
            }
            tail++;
            while (tail < length)
            {
                htb.Remove(nums[head]);
                if (IsContain(htb, nums[tail], t)) return true;
                else
                {
                    htb.Add(nums[tail], 0);
                }
                tail++; head++;
            }
            return false;
    }
            public bool IsContain(Hashtable htb,int tail,int t)
        {
            if (t == 0)
            {
                return htb.ContainsKey(tail);
            }
            foreach(int k in htb.Keys)
            {
                if ((long)tail - (long)k <= (long)t && (long)tail - (long)k >= (long)-t)
                {
                    return true;
                }
            }
            return false;
        }
    
}
public class Solution {
    public bool ContainsNearbyAlmostDuplicate(int[] nums, int k, int t) {
        if(nums == null || nums.Count()<2 || k < 1 || t<0)
            return false;
        var ss = new SortedSet<long>();
        for(int i = 0; i<nums.Count(); i++)
        {
            if(i>k)
            {
                ss.Remove(nums[i-k-1]);
            }
            if(ss.GetViewBetween((long)nums[i]-t, (long)nums[i]+t).Count()>0)
                return true;
            ss.Add(nums[i]);
        }
        return false;
    }
}
public class Solution {
    public bool ContainsNearbyAlmostDuplicate(int[] nums, int k, int t) {
        if(t < 0 || k < 1) return false;
        Dictionary<long,long> dict = new Dictionary<long,long>();
        
        for(int i = 0; i < nums.Length; ++i){
            int bucket = getBucket(nums[i], t);
            if(dict.ContainsKey(bucket)
              || (dict.ContainsKey(bucket - 1) && Math.Abs(dict[bucket - 1] - nums[i]) <= t) 
              || (dict.ContainsKey(bucket + 1) && Math.Abs(dict[bucket + 1] - nums[i]) <= t)){
                return true;
            }
            
            dict[bucket] = nums[i];
            if(dict.Count >= k + 1){
                dict.Remove(getBucket(nums[i - (k)], t));
            }

        }
        
        return false;
    }
    
    private int getBucket(int val, int t) {
        int bucket = val / (t + 1);
        if(val < 0){
            bucket -= 1;
        }
        return bucket;
    }
}
*/
