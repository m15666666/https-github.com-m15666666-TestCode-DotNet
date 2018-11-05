using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// https://leetcode-cn.com/problems/restore-ip-addresses/
/// 93.复原IP地址
/// 给定一个只包含数字的字符串，复原它并返回所有可能的 IP 地址格式。
/// https://blog.csdn.net/u014253011/article/details/80003075
/// </summary>
class RestoreIpAddressesSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public IList<string> RestoreIpAddresses(string s)
    {
        List<string> ret = new List<string>();
        if (string.IsNullOrWhiteSpace(s) || s.Length < 4 ) return ret;

        int slotCount = s.Length - 1;

        List<int> list = new List<int>(4) { 0 };

        BackTrade(s, slotCount, 1, list, ret);

        return ret;
    }

    private static void BackTrade(string s, int slotCount, int startIndex, List<int> list, List<string> ret )
    {
        var strStartIndex = list[list.Count - 1];
        var sLength = s.Length;
        var needPartCount = 4 - ( list.Count - 1 ) - 1;
        var maxCharCount = 3 * needPartCount;
        var maxSlot = startIndex + 2;//(slotCount - (3 - list.Count));
        for ( int i = startIndex; i <= maxSlot; i++ )
        {
            var s1Length = i - strStartIndex;
            var firstChar = s[strStartIndex];
            if ( 3 < s1Length ) break;

            var restCount = sLength - i;
            if ( maxCharCount < restCount ) continue;
            if ( restCount < needPartCount ) break;

            if (1 < s1Length && firstChar == '0') break;
            if (3 == s1Length)
            {
                if ('2' < firstChar) break;

                var secondChar = s[strStartIndex + 1];
                var thirdChar = s[strStartIndex + 2];

                if (firstChar == '2' && ('5' < secondChar || ('5' == secondChar && '5' < thirdChar))) break;
            }

            //var pass = CheckIPPart(s, strStartIndex, s1Length);
            //if (!pass) break;

            if( needPartCount == 1 )
            {
                if( CheckIPPart(s, i, restCount ) )
                {
                    var index1 = list[1];
                    var ip = $"{s.Substring(0, index1)}.{s.Substring(index1, strStartIndex - index1)}.{s.Substring(strStartIndex, s1Length)}.{s.Substring(i, restCount)}";
                    ret.Add(ip);
                }
                continue;
            }

            list.Add(i);

            BackTrade( s, slotCount, i + 1, list, ret );

            list.RemoveAt(list.Count - 1);
        }
    }

    private static bool CheckIPPart(string s, int startIndex, int count)
    {
        var s1Length = count;
        var firstChar = s[startIndex];
        if ( 3 < s1Length ) return false;

        if (1 < s1Length && firstChar == '0') return false;
        //if ( list.Count == 1 && firstChar == '0' ) break;
        //if ( s1Length == 1 && firstChar == '0') break;
        if (3 == s1Length)
        {
            if ('2' < firstChar) return false;

            var secondChar = s[startIndex + 1];
            var thirdChar = s[startIndex + 2];

            if (firstChar == '2' && ('5' < secondChar || ('5' == secondChar && '5' < thirdChar))) return false;
        }

        return true;
    }
}