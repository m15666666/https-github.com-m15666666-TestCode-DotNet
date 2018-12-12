using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// https://leetcode-cn.com/problems/largest-number/
/// 179. 最大数 LargestNumber
/// 给定一组非负整数，重新排列它们的顺序使之组成一个最大的整数。
/// 示例 1:
/// 输入: [10,2]
/// 输出: 210
/// 示例 2:
/// 输入: [3,30,34,5,9]
/// 输出: 9534330
/// 说明: 输出结果可能非常大，所以你需要返回一个字符串而不是整数。
/// </summary>
class LargestNumberSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    private readonly static Comparison<string> comparison = (x, y) => {
        if (x[0] == '0') return -1;
        if (y[0] == '0') return 1;

        // 1: x + y
        // 2: y + x
        var array1 = x;
        var array2 = y;
        int array1Index = 0;
        int array2Index = 0;
        while( true)
        {
            var c1 = array1[array1Index++];
            var c2 = array2[array2Index++];
            if (c2 < c1) return 1;
            if (c1 < c2) return -1;

            if(array1Index == array1.Length)
            {
                if (array1 == y) break;
                array1 = y;
                array1Index = 0;
            }

            if (array2Index == array2.Length)
            {
                if (array2 == x) break;
                array2 = x;
                array2Index = 0;
            }
        }
        return 0;
    };

    public string LargestNumber(int[] nums)
    {
        if (nums == null || nums.Length == 0) return "";
        if (nums.Length == 1) return nums[0].ToString();

        string[] numTexts = nums.Select(item => item.ToString()).ToArray();
        
        Array.Sort(numTexts, comparison);
        StringBuilder sb = new StringBuilder();
        for (int i = numTexts.Length - 1; -1 < i; i--)
            sb.Append(numTexts[i]);
        var ret = sb.ToString();
        if (ret[0] == '0') return "0";
        return ret;
    }

}

/*
 // 别人的解法
    public  string LargestNumber(int[] nums)
    {
        for (var i = 1; i < nums.Length; i++) {
            var j = i;
            while (j > 0 && !_compare(nums[j - 1], nums[i]))
                --j;
            var temp = nums[i];
            for (var t = i; t > j; t--) {
                nums[t] = nums[t - 1];
            }
            nums[j] = temp;
        }
        var flag=0;
        while (nums[flag] == 0 && flag < nums.Length - 1)
            ++flag;
        return string.Join(string.Empty,nums.Skip(flag));
    }
    private  bool _compare(int a,int b) {
        if (a == b)
            return true;
        var s1 = a.ToString();
        var s2 = b.ToString();
        var index = 0;
        while (index<s1.Length+s2.Length-1) {
            var n1 = index >= s1.Length ? s2[index -s1.Length] : s1[index];
            var n2 = index >= s2.Length ? s1[index - s2.Length] : s2[index];
            ++index;
            if (n1 > n2)
                return true;
            else if (n1 < n2)
                return false;            
        }
        return true;
    }
*/
