using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个可能含有重复元素的整数数组，要求随机输出给定的数字的索引。 您可以假设给定的数字一定存在于数组中。

注意：
数组大小可能非常大。 使用太多额外空间的解决方案将不会通过测试。

示例:

int[] nums = new int[] {1,2,3,3,3};
Solution solution = new Solution(nums);

// pick(3) 应该返回索引 2,3 或者 4。每个索引的返回概率应该相等。
solution.pick(3);

// pick(1) 应该返回 0。因为只有nums[0]等于1。
solution.pick(1); 
*/
/// <summary>
/// https://leetcode-cn.com/problems/random-pick-index/
/// 398. 随机数索引
/// </summary>
class RandomPickIndexSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public RandomPickIndexSolution(int[] nums)
    {
        _num = nums;
    }

    private int[] _num;
    private Random _random = new Random();
    private Dictionary<int, List<int>> _target2Found = new Dictionary<int, List<int>>();
    public int Pick(int target)
    {
        List<int> found = new List<int>();
        if (_target2Found.ContainsKey(target))
        {
            found = _target2Found[target];
        }
        else
        {
            _target2Found[target] = found;
            int index = -1;
            foreach( var v in _num)
            {
                ++index;
                if (v == target) found.Add(index);
            }
        }

        if (0 < found.Count) return found[_random.Next(found.Count)];
        return -1;
    }
}
/*
【蓄水池抽样】Python3 代码简洁（398. 随机数索引）
力扣加加
发布于 2021-01-11
144

class Solution:

    def __init__(self, nums: List[int]):
        self.nums = nums

    def pick(self, target: int) -> int:
        cnt = 0
        ans = None
        for i in range(len(self.nums)):
            if self.nums[i] == target:
                cnt += 1
                if random.randint(1, cnt) == cnt:
                    ans = i
        return ans
复杂度分析

时间复杂度：O(N)O(N)，其中 N 为数组长度。
空间复杂度：O(1)O(1) （nums 不计入算法额外的空间开销哦）

class Solution {

        private int[] nums ;
        public Solution(int[] nums) {
            this.nums = nums;
        }

        public int pick(int target) {
            List<Integer> list = new ArrayList<>();
            for (int i = 0; i < nums.length; i++) {
                if(nums[i] == target) {
                    list.add(i);
                }
            }
            Random random = new Random();
            return list.get(random.nextInt(list.size()));
 
*/