using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// https://leetcode-cn.com/problems/group-anagrams/
/// 49.字母异位词分组
/// 给定一个字符串数组，将字母异位词组合在一起。字母异位词指字母相同，但排列不同的字符串。
/// </summary>
class GroupAnagramsSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    public IList<IList<string>> GroupAnagrams(string[] strs)
    {
        //List<IList<string>> ret = new List<IList<string>>();

        if (strs == null || strs.Length == 0) return new List<IList<string>>();

        Dictionary<string, List<string>> map = new Dictionary<string, List<string>>();
        foreach( var str in strs)
        {
            var chars = str.ToArray();
            Array.Sort(chars);
            var key = string.Join("", chars);
            if (!map.ContainsKey(key)) map[key] = new List<string>();
            map[key].Add(str);
        }

        return map.Values.ToArray();
    }

}