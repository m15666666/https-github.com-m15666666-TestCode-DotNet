using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// https://leetcode-cn.com/problems/majority-element-ii/
/// 229. 求众数 II
/// 给定一个大小为 n 的数组，找出其中所有出现超过 ⌊ n/3 ⌋ 次的元素。
/// 说明: 要求算法的时间复杂度为 O(n)，空间复杂度为 O(1)。
/// 示例 1:
/// 输入: [3,2,3]
/// 输出: [3]
/// 示例 2:
/// 输入: [1,1,1,3,3,2,2,2]
/// 输出: [1,2]
/// </summary>
class MajorityElementIISolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    public IList<int> MajorityElement(int[] nums)
    {
        if (nums == null || nums.Length == 0) return new int[0];
        if (nums.Length == 1) return new int[] { nums[0] };

        Array.Sort(nums);

        int n = nums.Length;
        int threshold = n / 3 + 1;
        List<int> ret = new List<int>();
        int currentNum = nums[0];
        int count = 1;
        if (threshold < 2 )
        {
            for (int index = 1; index < nums.Length; index++)
            {
                if (1 == count) ret.Add(currentNum);

                var v = nums[index];
                if (v == currentNum)
                {
                    ++count;
                    continue;
                }
                currentNum = v;
                count = 1;
            }
            if (1 == count) ret.Add(currentNum);
        }
        else
        {
            for (int index = 1; index < nums.Length; index++)
            {
                var v = nums[index];
                if (v == currentNum)
                {
                    ++count;
                    if (threshold == count) ret.Add(currentNum);
                    continue;
                }
                currentNum = v;
                count = 1;
            }
        }
        return ret;
    }
}
/*
//别人的代码
public class Solution
{
    public IList<int> MajorityElement(int[] nums)
    {
        List<int> res = new List<int>();

        int countN = 0, countM = 0, n = 0, m = 0;
        for (int i = 0; i < nums.Length; i++)
        {
            if (n == nums[i]) countN++;
            else if (m == nums[i]) countM++;
            else if (countN == 0) { n = nums[i]; countN++; }
            else if (countM == 0) { m = nums[i]; countM++; }
            else { countN--; countM--; }
        }
        countN = 0;
        countM = 0;

        for(int i = 0; i < nums.Length; i++)
        {
            if (nums[i] == n) countN++;
            else if (nums[i] == m) countM++;
        }

        if (countN > nums.Length / 3) res.Add(n);
        if (countM > nums.Length / 3) res.Add(m);
        return res;
    }
}
public class Solution {
    public IList<int> MajorityElement(int[] nums) {
       return nums.ToList().GroupBy(u => u).Select(u => new KeyValuePair<int, int>(u.Key, u.Count())).Where(u => u.Value> nums.Length / 3).Select(u=>u.Key).ToList();
    }
}
public class Solution {
    public IList<int> MajorityElement(int[] nums) {
        var r=new List<int>();
        if(nums.Length<1)
            return r;
        var group = nums.GroupBy(p=>p);
        var line = nums.Length/3;
        foreach(var item in group.Where(p=>p.Count()>line)){
                r.Add(item.Key);
        }
        return r;
    }
}
*/
