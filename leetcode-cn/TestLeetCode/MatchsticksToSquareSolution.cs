using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
还记得童话《卖火柴的小女孩》吗？现在，你知道小女孩有多少根火柴，请找出一种能使用所有火柴拼成一个正方形的方法。不能折断火柴，可以把火柴连接起来，并且每根火柴都要用到。

输入为小女孩拥有火柴的数目，每根火柴用其长度表示。输出即为是否能用所有的火柴拼成正方形。

示例 1:

输入: [1,1,2,2,2]
输出: true

解释: 能拼成一个边长为2的正方形，每边两根火柴。
示例 2:

输入: [3,3,3,3,4]
输出: false

解释: 不能用所有火柴拼成一个正方形。
注意:

给定的火柴长度和在 0 到 10^9之间。
火柴数组的长度不超过15。
*/
/// <summary>
/// https://leetcode-cn.com/problems/matchsticks-to-square/
/// 473. 火柴拼正方形
/// </summary>
class MatchsticksToSquareSolution
{
    public void Test()
    {
        int[] nums = new int[] {3,3,3,3,4};
        var ret = Makesquare(nums);
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public bool Makesquare(int[] nums)
    {
        if (nums == null || nums.Length < 4) return false;

        var sum = nums.Sum();
        if (sum % 4 != 0) return false;

        Array.Sort(nums, (x, y) => y.CompareTo(x));
        int[] lines = new int[4] { 0, 0, 0, 0 };
        return BackTrade(nums, lines, 0, sum / 4);
    }

    private static bool BackTrade(int[] nums, int[] sums, int pos, int target)
    {
        if (nums.Length <= pos)
        {
            return sums[0] == target && sums[1] == target && sums[2] == target && sums[3] == target;
        }
        //对于当前这个火柴，尝试拼入上下左右四个边
        for (int i = 0; i < 4; ++i)
        {
            if (sums[i] + nums[pos] > target) continue;
            sums[i] += nums[pos]; //把当前火柴从i个边中拿出来，好尝试下一条边
            if (BackTrade(nums, sums, pos + 1, target)) return true;  //如果这个火柴被成功使用，就开始尝试拼下一根火柴
            sums[i] -= nums[pos];  //用当前火柴拼第i个边

            // 优化：如果第一条边已经尝试失败，后面也不必尝试了，都一样。
            if (i== 0 && sums[i] == 0) return false;
        }
        return false;
    }
}
/*
public class Solution {
    public bool Makesquare(int[] nums) {
        //从棍子个数判断
        if (nums.Length < 4)
            return false;
        //计算所有火柴的和
        int sum = 0;
        for(int i = 0; i < nums.Length; i++)
        {
            sum += nums[i];
        }
        //判断是否能被4整除，如果可以，计算出该正方形的边长 singleWallLen
        int singleWallLen = 0;
        if(sum % 4 != 0)
        {
            return false;
        }
        else
        {
            singleWallLen = sum / 4;
        }
        //从大到小排序数组
        HeadSort(nums);
        //找搭配
        int[] sums = new int[4];
        return (Find(nums, sums, 0, singleWallLen));
    }
    
    public bool Find(int[] nums, int[] sums, int step, int singleLength)
    {
        if(step >= nums.Length)
        {
            return sums[0] == singleLength && sums[1] == singleLength && sums[2] == singleLength && sums[3] == singleLength;
        }
        for(int i = 0; i < 4; i++)
        {
            if (sums[i] + nums[step] > singleLength) continue;
            sums[i] += nums[step];
            if (Find(nums, sums, step + 1, singleLength)) return true;
            sums[i] -= nums[step];
        }
        return false;
    }

    //堆排序
    public void HeadSort(int[] arr)
    {
        //循环建立堆结构
        for(int i = arr.Length / 2 - 1 ; i >= 0; i--)
        {
            HeadSortCore(arr, i, arr.Length - 1); //循环i课树
        }

        for (int i = arr.Length - 1; i >= 0; i--)
        {
            int temp = arr[0];
            arr[0] = arr[i];
            arr[i] = temp;
            HeadSortCore(arr, 0, i - 1);
        }
    }

    public void HeadSortCore(int[] arr, int low, int high)
    {
        int i = low;
        int j = i * 2 + 1; // i的左子树
        int temp = arr[low];
        while (j<high)
        {
            //拿到子树中的最大值
            if (arr[j].CompareTo(arr[j + 1]) > 0)
            {
                j++;
            }
            if (temp.CompareTo(arr[j]) > 0)
            {
                arr[i] = arr[j];
                i = j;
                j = i * 2 + 1;
            }else
            {
                break;
            }
            arr[i] = temp;
        }
    }
}

*/
