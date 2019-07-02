using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一组正整数，相邻的整数之间将会进行浮点除法操作。例如， [2,3,4] -> 2 / 3 / 4 。

但是，你可以在任意位置添加任意数目的括号，来改变算数的优先级。你需要找出怎么添加括号，
才能得到最大的结果，并且返回相应的字符串格式的表达式。你的表达式不应该含有冗余的括号。

示例：

输入: [1000,100,10,2]
输出: "1000/(100/10/2)"
解释:
1000/(100/10/2) = 1000/((100/10)/2) = 200
但是，以下加粗的括号 "1000/((100/10)/2)" 是冗余的，
因为他们并不影响操作的优先级，所以你需要返回 "1000/(100/10/2)"。

其他用例:
1000/(100/10)/2 = 50
1000/(100/(10/2)) = 50
1000/100/10/2 = 0.5
1000/100/(10/2) = 2
说明:

输入数组的长度在 [1, 10] 之间。
数组中每个元素的大小都在 [2, 1000] 之间。
每个测试用例只有一个最优除法解。 
*/
/// <summary>
/// https://leetcode-cn.com/problems/optimal-division/
/// 553. 最优除法
/// http://www.luyixian.cn/news_show_72151.aspx
/// 注：可以发现nums中第一个数永远在分子上，第二个数永远作为被除数在分母上，
/// 那么如果后面的数都可以在分子上，就可以获得最大结果。所以根据例子：给定 [1000,100,10,2]，输出 "1000/(100/10/2)"，
/// 我们发现要在第二个数的前面添加括号，并且后面都是除法。
/// 
/// https://leetcode-cn.com/submissions/detail/21744593/
/// </summary>
class OptimalDivisionSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public string OptimalDivision(int[] nums)
    {
        if (nums == null || nums.Length == 0) return string.Empty;
        var len = nums.Length;
        if (len == 1) return nums[0].ToString();
        if (len == 2) return $"{nums[0]}/{nums[1]}";

        StringBuilder ret = new StringBuilder();
        ret.Append($"{nums[0]}/(");
        int i = 1;
        for (; i < len - 1; i++)
            ret.Append($"{ nums[i]}/");
        ret.Append($"{ nums[i]})");
        return ret.ToString();
    }
}
/*
public class Solution {
    public string OptimalDivision(int[] nums) {
        
        
        StringBuilder sb = new StringBuilder();
        
        if (nums.Length <= 2) {
            sb.Append(nums[0]);
            for (int i = 1; i < nums.Length; i ++) {
                sb.Append("/");
                sb.Append(nums[i]);
            }
            
            return sb.ToString();
        }
        
        sb.Append(nums[0]);
        sb.Append("/(");
        for (int i = 1; i < nums.Length -1; i ++) {
            sb.Append(nums[i]);
            sb.Append('/');
        }
        
        sb.Append(nums[nums.Length - 1]);
        sb.Append(')');
        
        return sb.ToString();
    }
}

*/
