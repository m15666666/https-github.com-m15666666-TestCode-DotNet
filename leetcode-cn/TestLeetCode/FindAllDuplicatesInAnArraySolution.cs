using System.Collections.Generic;

/*
给定一个整数数组 a，其中1 ≤ a[i] ≤ n （n为数组长度）, 其中有些元素出现两次而其他元素出现一次。

找到所有出现两次的元素。

你可以不用到任何额外空间并在O(n)时间复杂度内解决这个问题吗？

示例：

输入:
[4,3,2,7,8,2,3,1]
输出:
[2,3]
*/

/// <summary>
/// https://leetcode-cn.com/problems/find-all-duplicates-in-an-array/
/// 442. 数组中重复的数据
/// https://blog.csdn.net/weixin_42690125/article/details/81041119
/// </summary>
internal class FindAllDuplicatesInAnArraySolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public IList<int> FindDuplicates(int[] nums)
    {
        var ret = new List<int>();
        if (nums == null || nums.Length == 0) return ret;
        int n = nums.Length;
        for (int i = 0; i < n; i++)
            while (nums[nums[i] - 1] != nums[i])
                Swap(nums, i, nums[i] - 1);

        for (int i = 0; i < n; i++)
            if (nums[i] != i + 1)
                ret.Add(nums[i]);

        return ret;

        static void Swap(int[] arr, int index1, int index2)
        {
            int tmp = arr[index1];
            arr[index1] = arr[index2];
            arr[index2] = tmp;
        }
    }

    //public IList<int> FindDuplicates(int[] nums)
    //{
    //    List<int> ret = new List<int>();
    //    for (int i = 0; i < nums.Length; ++i)
    //    {
    //        int idx = Math.Abs(nums[i]) - 1;
    //        if (nums[idx] < 0) ret.Add(idx + 1);
    //        else nums[idx] = -nums[idx];
    //    }
    //    return ret;
    //}
}

/*
还是原地哈希
zh-spike

发布于 1 天前
36
思路
一种 满足可交换 满足条件

nums[[L] - 1] 当前数 和 以当前数为值 满足 nums[left - 1] != left 的数

？!= 9 交换两个数

期望不变 重新比较 ？

图解


              L              
num     1 2 3 9] ······ ？ [R ·····
index   0 1 2 3]        8   

              L              
num     1 2 3 ?] ······ 9  [R ·····
index   0 1 2 3]        8   


Code

    class Solution {
        public List<Integer> findDuplicates(int[] nums) {
            List<Integer> res = new ArrayList<>();

            if (nums == null || nums.length == 0) {
                return res;
            }

            // 原地 hash 排 和之前的那个差不多
            // 他这里相当于只用考虑 第3部分
            for (int i = 0; i < nums.length; i++) {
                while (nums[nums[i] - 1] != nums[i]) {
                    swap(nums, i, nums[i] - 1);
                }
            }

            // 把不对应的收集
            for (int i = 0; i < nums.length; i++) {
                if (nums[i] != i + 1) {
                    res.add(nums[i]);
                }
            }
            return res;
        }

        public void swap(int[] arr, int index1, int index2) {
            int tmp = arr[index1];
            arr[index1] = arr[index2];
            arr[index2] = tmp;
        }
    }


var findDuplicates = function(nums) {
    const res = []

    for (const num of nums) {
        const absNum = Math.abs(num)
        if (nums[absNum - 1] < 0) {
            res.push(absNum)
        } else {
            nums[absNum - 1] *= -1
        }
    }
    
    return res
};

public class Solution {
    public IList<int> FindDuplicates(int[] nums) {
        
        List<int> ans = new List<int>();
        for(int i = 0 ; i< nums.Length ; i++){

        int index = Math.Abs(nums[i])-1;
        if(nums[index]<0)
            ans.Add(index+1);
        nums[index] = -nums[index];

        }
        return ans;
    }
}

public class Solution {
    public IList<int> FindDuplicates(int[] nums) {
        int sum = 0;
        IList<int> result = new List<int>();
        for (int i = 0; i<nums.Length; i++)
        {
            int index = Math.Abs(nums[i])-1;
            if(nums[index]<0)
            {
                result.Add(Math.Abs(nums[i]));
            }
            else
            {
                nums[index] = (-1)*nums[index];
            }
        }
        return result;
    }
}
public class Solution {
    public IList<int> FindDuplicates(int[] nums) {
        HashSet<int> result = new HashSet<int>();
        for(int i = 0; i < nums.Length; i++)
        {
            while(nums[i]!=i+1)
            {
                if(nums[nums[i]-1] == nums[i])
                {
                    result.Add(nums[i]);
                    break;
                }
                int temp = nums[i]-1;
                nums[i]^=nums[temp];
                nums[temp]^=nums[i];
                nums[i]^=nums[temp];
            }
        }
        return new List<int>(result);
    }
}
public class Solution {
    public IList<int> FindDuplicates(int[] nums) {
        var ans = new List<int>();
        int ai;
        for (int i = 0; i < nums.Length; i++) {
            ai = Math.Abs(nums[i]);
            if (nums[ai - 1] > 0)
                nums[ai - 1] *= -1;
            else
                ans.Add(ai);
        }
        return ans;
    }
}
*/