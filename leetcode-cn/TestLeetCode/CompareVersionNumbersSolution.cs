using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// https://leetcode-cn.com/problems/compare-version-numbers/
/// 165. 比较版本号
/// 比较两个版本号 version1 和 version2。
/// 如果 version1 > version2 返回 1，如果 version1<version2 返回 -1， 除此之外返回 0。
/// 你可以假设版本字符串非空，并且只包含数字和 . 字符。
/// . 字符不代表小数点，而是用于分隔数字序列。
/// 例如，2.5 不是“两个半”，也不是“差一半到三”，而是第二版中的第五个小版本。
/// 示例 1:
/// 输入: version1 = "0.1", version2 = "1.1"
/// 输出: -1
/// 示例 2:
/// 输入: version1 = "1.0.1", version2 = "1"
/// 输出: 1
/// 示例 3:
/// 输入: version1 = "7.5.2.4", version2 = "7.5.3"
/// 输出: -1
/// </summary>
class CompareVersionNumbersSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    public int CompareVersion(string version1, string version2)
    {
        const string dot = ".";
        var parts1 = version1.Split(dot, StringSplitOptions.RemoveEmptyEntries);
        var parts2 = version2.Split(dot, StringSplitOptions.RemoveEmptyEntries);

        var minLenth = Math.Min(parts1.Length, parts2.Length);
        for( int index = 0; index < minLenth; index++)
        {
            var num1 = int.Parse(parts1[index]);
            var num2 = int.Parse(parts2[index]);
            if (num2 < num1) return 1;
            else if (num1 < num2) return -1;
        }

        if (parts1.Length == parts2.Length) return 0;

        var parts = minLenth < parts1.Length ? parts1 : parts2;
        var result = minLenth < parts1.Length ? 1 : -1;
        for (int index = minLenth; index < parts.Length; index++)
            if (0 < int.Parse(parts[index])) return result;
        return 0;
    }
}