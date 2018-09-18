using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// 三数之和
/// 给定一个包含 n 个整数的数组 nums，判断 nums 中是否存在三个元素 a，b，c ，使得 a + b + c = 0 ？找出所有满足条件且不重复的三元组。
/// https://www.jianshu.com/p/19b0261c73b9
/// </summary>
class ThreeSumSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    public IList<IList<int>> ThreeSum(int[] nums)
    {
        List<IList<int>> ret = new List<IList<int>>();

        if (nums == null || nums.Length < 3) return ret;

        Array.Sort(nums);

        int n1, n2, n3;
        n1 = n2 = n3 = -1;
        for (int i = 0; i < nums.Length; ++i)
        {
            // 跳过
            if (0 < i && nums[i] != 0 && nums[i - 1] == nums[i]) continue;

            int l = i + 1, r = nums.Length - 1;
            while (l < r)
            {
                int sum = nums[i] + nums[l] + nums[r];
                if (sum == 0 && !(n1 == nums[i] && n2 == nums[l] && n3 == nums[r]))
                {
                    n1 = nums[i];
                    n2 = nums[l];
                    n3 = nums[r];

                    ret.Add( new List<int>() { n1, n2, n3 } );
                }
                if (sum <= 0) ++l;
                else --r;
            }
        }

        return ret;
    }
}