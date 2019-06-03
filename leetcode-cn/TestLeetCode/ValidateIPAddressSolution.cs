using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
编写一个函数来验证输入的字符串是否是有效的 IPv4 或 IPv6 地址。

IPv4 地址由十进制数和点来表示，每个地址包含4个十进制数，其范围为 0 - 255， 用(".")分割。比如，172.16.254.1；

同时，IPv4 地址内的数不会以 0 开头。比如，地址 172.16.254.01 是不合法的。

IPv6 地址由8组16进制的数字来表示，每组表示 16 比特。这些组数字通过 (":")分割。比如,  2001:0db8:85a3:0000:0000:8a2e:0370:7334 是一个有效的地址。而且，我们可以加入一些以 0 开头的数字，字母可以使用大写，也可以是小写。所以， 2001:db8:85a3:0:0:8A2E:0370:7334 也是一个有效的 IPv6 address地址 (即，忽略 0 开头，忽略大小写)。

然而，我们不能因为某个组的值为 0，而使用一个空的组，以至于出现 (::) 的情况。 比如， 2001:0db8:85a3::8A2E:0370:7334 是无效的 IPv6 地址。

同时，在 IPv6 地址中，多余的 0 也是不被允许的。比如， 02001:0db8:85a3:0000:0000:8a2e:0370:7334 是无效的。

说明: 你可以认为给定的字符串里没有空格或者其他特殊字符。

示例 1:

输入: "172.16.254.1"

输出: "IPv4"

解释: 这是一个有效的 IPv4 地址, 所以返回 "IPv4"。
示例 2:

输入: "2001:0db8:85a3:0:0:8A2E:0370:7334"

输出: "IPv6"

解释: 这是一个有效的 IPv6 地址, 所以返回 "IPv6"。
示例 3:

输入: "256.256.256.256"

输出: "Neither"

解释: 这个地址既不是 IPv4 也不是 IPv6 地址。 
*/
/// <summary>
/// https://leetcode-cn.com/problems/validate-ip-address/
/// 468. 验证IP地址
/// </summary>
class ValidateIPAddressSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public string ValidIPAddress(string IP)
    {
        const string IPv4 = "IPv4";
        const string IPv6 = "IPv6";
        const string Neither = "Neither";

        if (string.IsNullOrWhiteSpace(IP)) return Neither;
        IP = IP.ToLowerInvariant();

        if (IP.Contains('.'))
        {
            if (IsIPV4(IP)) return IPv4;
        }
        else if (IP.Contains(':'))
        {
            if (IsIPV6(IP)) return IPv6;
        }
        return Neither;
    }

    private static bool IsIPV4(string ip)
    {
        var parts = ip.Split('.');
        if (parts.Length != 4) return false;

        foreach( var part in parts)
        {
            if (string.IsNullOrEmpty(part)) return false;
            if(1 < part.Length && part[0] == '0') return false;

            int sum = 0;
            foreach( var c in part)
            {
                if (!('0' <= c && c <= '9')) return false;
                sum = 10 * sum + (c - '0');
            }

            if (!(0 <= sum && sum <= 255 )) return false;
        }

        return true;
    }

    private static bool IsIPV6(string ip)
    {
        ip = ip.ToLowerInvariant();
        var parts = ip.Split(':');

        if (parts.Length != 8) return false;

        foreach (var part in parts)
        {
            if (string.IsNullOrEmpty(part)) return false;
            if ( 4 < part.Length ) return false;

            foreach (var c in part)
            {
                if (!(('0' <= c && c <= '9') || ('a' <= c && c <= 'f') )) return false;
            }
        }

        return true;
    }
}
/*
public class Solution {
    public string ValidIPAddress(string IP) {
        if (IP.Contains('.'))
        {
            string[] nums = IP.Split('.');
            if (nums.Length != 4)
                return "Neither";
            for (int i = 0; i < nums.Length; i++)
            {
                int num = 0;
                if (nums[i] == "") return "Neither";
                try
                {
                    num = Convert.ToInt32(nums[i]);
                }
                catch
                {
                    return "Neither";
                }
                if (num.ToString() != nums[i] || (num < 0 || num > 255))
                {
                    return "Neither";
                }
            }
            return "IPv4";
        };
        if (IP.Contains(':'))
        {
            string[] nums = IP.Split(':');
            if (nums.Length != 8)
                return "Neither";
            for (int i = 0; i < nums.Length; i++)
            {
                if (nums[i] == "" || nums[i].Length > 4) return "Neither";
                bool flag = true;
                for (int j = 0; j < nums[i].Length; j++)
                {
                    if (!((nums[i][j] >= '0' && nums[i][j] <= '9') || (nums[i][j] >= 'A' && nums[i][j] <= 'F') || (nums[i][j] >= 'a' && nums[i][j] <= 'f')))
                        flag = false;
                }
                if (!flag)
                    return "Neither";
            }
            return "IPv6";
        }
        return "Neither";
    }
}
 using System.Text.RegularExpressions;
 using System.Linq;
public class Solution {
    public string ValidIPAddress(string IP) {
        var arr=new string[]{"IPv4","IPv6","Neither"};
        if(IP.Length<7||IP.Length>71)
        {
            return  arr[2];
        }
        if(IP.Length<16&&IP.IndexOf('.')>-1)
        {
            var strs=IP.Split(".");
            if(strs.Length!=4)
            {
                return arr[2];
            }
            for (int i = 0; i < strs.Length; i++)
            {
                if(string.IsNullOrEmpty(strs[i]))
                {
                    return arr[2];
                }
                if(strs[i].Length>1&&strs[i][0]=='0')
                {
                    return arr[2];
                }
                if(!Regex.IsMatch(strs[i],@"^\d{1,3}$"))
                {
                    return arr[2];
                }
                var value=0;
                if(int.TryParse(strs[i],out value))
                {
                    if(value>255)
                    {
                        return arr[2];
                    }
                }
                else
                {
                    return arr[2];
                }
            }
            return arr[0];
        }
        var str_v6=IP.Split(":");
        if(str_v6.Length!=8)
        {
            return arr[2];
        }
        for (int i = 0; i < str_v6.Length; i++)
        {
            if(string.IsNullOrEmpty(str_v6[i]))
            {
                return  arr[2];
            }
            if(str_v6[i].Length>4)
            {
                return arr[2];
            }
            
            if(!Regex.IsMatch(str_v6[i],@"(^(\d|[A-F]|[a-f]){2,4})$|(^(0|[a-f]|[A-F])$)"))
            {
                return arr[2];
            }

        }
        return arr[1];

    }
}
*/
