using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
你正在和你的朋友玩 猜数字（Bulls and Cows）游戏：你写下一个数字让你的朋友猜。每次他猜测后，你给他一个提示，
告诉他有多少位数字和确切位置都猜对了（称为“Bulls”, 公牛），有多少位数字猜对了但是位置不对（称为“Cows”, 奶牛）。
你的朋友将会根据提示继续猜，直到猜出秘密数字。

请写出一个根据秘密数字和朋友的猜测数返回提示的函数，用 A 表示公牛，用 B 表示奶牛。

请注意秘密数字和朋友的猜测数都可能含有重复数字。

示例 1:

输入: secret = "1807", guess = "7810"

输出: "1A3B"

解释: 1 公牛和 3 奶牛。公牛是 8，奶牛是 0, 1 和 7。
示例 2:

输入: secret = "1123", guess = "0111"

输出: "1A1B"

解释: 朋友猜测数中的第一个 1 是公牛，第二个或第三个 1 可被视为奶牛。
说明: 你可以假设秘密数字和朋友的猜测数都只包含数字，并且它们的长度永远相等。
*/
/// <summary>
/// https://leetcode-cn.com/problems/bulls-and-cows/
/// 299. 猜数字游戏
/// https://www.cnblogs.com/zhizhiyu/p/10176167.html
/// </summary>
class BullsAndCowsSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    ///// <summary>
    ///// 这种解法不对,无法处理测试用例："1122" "1222"
    ///// </summary>
    ///// <param name="secret"></param>
    ///// <param name="guess"></param>
    ///// <returns></returns>
    //public string GetHint(string secret, string guess)
    //{
    //    if (string.IsNullOrEmpty(secret)) return string.Empty;

    //    int[] counts = new int[10];
    //    int len = secret.Length;
    //    int count1 = 0, count2 = 0;

    //    foreach(var c in secret) ++counts[c - '0'];
    //    for( int i = 0; i < len; i++)
    //    {
    //        var v1 = secret[i] - '0';
    //        var v2 = guess[i] - '0';
    //        if(v1 == v2)
    //        {
    //            ++count1;
    //            --counts[v2];
    //            continue;
    //        }
    //        if(0 < counts[v2])
    //        {
    //            --counts[v2];
    //            ++count2;
    //        }
    //    }
    //    return $"{count1}A{count2}B";
    //}
    public string GetHint(string secret, string guess)
    {
        if (string.IsNullOrEmpty(secret)) return string.Empty;

        int[] secrets = new int[10];
        Queue<int> queue = new Queue<int>();
        int count1 = 0, count2 = 0;
        for (int index1 = 0; index1 < secret.Length; index1++)
        {
            var v1 = secret[index1] - '0';
            var v2 = guess[index1] - '0';
            if (v1 == v2)
            {
                ++count1;
                continue;
            }

            ++secrets[v1];
            queue.Enqueue(v2);
        }
        while (0 < queue.Count)
        {
            var v = queue.Dequeue();
            if (0 < secrets[v])
            {
                --secrets[v];
                ++count2;
            }
        }

        return $"{count1}A{count2}B";
    }
}
/*
public class Solution {
    public string GetHint(string secret, string guess) {
            int[] cnt = new int[10];
            bool[] visited = new bool[secret.Length];
            int a = 0, b = 0;
            for(int i=0;i<secret.Length;i++)
            {
                cnt[secret[i] - '0']++;
            }
            for(int i=0;i<guess.Length;i++)
            {
                if (secret[i] == guess[i])
                {
                    a++;
                    cnt[guess[i] - '0']--;
                    visited[i] = true;
                }
            }
            for (int i = 0; i < guess.Length; i++)
            {
                if (cnt[guess[i] - '0'] > 0 && !visited[i])
                {
                    b++;
                    cnt[guess[i] - '0']--;
                }
            }

            return $"{a}A{b}B";
    }
}

public class Solution {
    public string GetHint(string secret, string guess) 
    {
       int aa = 0, bb = 0;//A，B 的次数变量
        int[] num = new int[10];//10个数字出现的状态数组
        for (int i = 0; i < secret.Length; i++)
        {
            if (secret[i] == guess[i]) aa++; //相等计公牛
            else
            {
                if (num[secret[i] - '0']++ < 0) bb++; //secret中的这个数字状态++, 负数代表以前在guess出现过，这时母牛次数+1
                if (num[guess[i] - '0']-- > 0) bb++; //guess中的这个数字状态--, 正数代表以前在secret出现过，这时母牛次数+1
            }
        }
        return aa + "A" + bb + "B";
    }
}

public class Solution {
    public string GetHint(string secret, string guess) {
        int n = secret.Length;
        int[] cnt = new int[10];
        foreach (char c in secret) {
            cnt[c - '0']++;
        }

        int shoot = 0;
        int match = 0;
        for (int i = 0; i < n; i++) {
            if (secret[i] == guess[i]) {
                match++;
            }
        }
        for (int i = 0; i < n; i++) {
            if (cnt[guess[i] - '0'] > 0) {
                cnt[guess[i] - '0']--;
                shoot++;
            }
        }
        shoot -= match;
        return match.ToString() + "A" + shoot.ToString() + "B";
    }
}

     
*/
