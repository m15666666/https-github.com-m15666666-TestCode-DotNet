using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
打乱一个没有重复元素的数组。

示例:

// 以数字集合 1, 2 和 3 初始化数组。
int[] nums = {1,2,3};
Solution solution = new Solution(nums);

// 打乱数组 [1,2,3] 并返回结果。任何 [1,2,3]的排列返回的概率应该相同。
solution.shuffle();

// 重设数组到它的初始状态[1,2,3]。
solution.reset();

// 随机返回数组[1,2,3]打乱后的结果。
solution.shuffle(); 
*/
/// <summary>
/// https://leetcode-cn.com/problems/shuffle-an-array/
/// 384. 打乱数组
/// https://blog.csdn.net/zrh_CSDN/article/details/83961002
/// </summary>
class ShuffleAnArraySolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public ShuffleAnArraySolution(int[] nums)
    {
        _originArray = nums;
    }

    private int[] _originArray = null;
    private Random _random = new Random();

    public int[] Reset()
    {
        return _originArray;
    }

    public int[] Shuffle()
    {
        int[] ret = new int[_originArray.Length];
        _originArray.CopyTo(ret, 0);
        for( int i = 0; i < ret.Length; i++ )
        {
            int target = _random.Next(ret.Length);
            if (i == target) continue;
            var temp = ret[i];
            ret[i] = ret[target];
            ret[target] = temp;
        }

        return ret;
    }
}
/*
public class Solution
{
    int [] _original;
    int _len;
    int [] _ToChange;
    Random rd;
    public Solution(int[] nums)
    {
        _len=nums.Length;
        _original=nums;
        _ToChange=new int[_len];
        rd=new Random();
        Array.Copy(nums,_ToChange,_len);
    }

    public int[] Reset()
    {

    return _original;
    }

    public int[] Shuffle()
    {
        int temp = 0;
        for (int i = 0; i < _len; i++)
        {
        int local = rd.Next(_len);
        temp = _ToChange[local];
        _ToChange[local] = _ToChange[i];
        _ToChange[i] = temp;
        }
        return _ToChange;
    }
}
*/