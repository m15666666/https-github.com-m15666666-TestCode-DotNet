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

    public string GetHint(string secret, string guess)
    {
        if (string.IsNullOrEmpty(secret)) return string.Empty;

        int[] secrets = new int[10];
        Queue<int> queue = new Queue<int>();
        int count1 = 0, count2 = 0;
        for(int index1 = 0; index1 < secret.Length; index1++)
        {
            var v1 = secret[index1] - '0';
            var v2 = guess[index1] - '0';
            if ( v1 == v2 )
            {
                ++count1;
                continue;
            }

            ++secrets[v1];
            queue.Enqueue(v2);
        }
        while( 0 < queue.Count)
        {
            var v = queue.Dequeue();
            if(0 < secrets[v])
            {
                --secrets[v];
                ++count2;
            }
        }

        return $"{count1}A{count2}B";
    }
}
/*
//别人的算法
public class Solution {
    public string GetHint(string secret, string guess) {
        Dictionary<char,int> s=new Dictionary<char,int>();
        for(int i=0;i<secret.Length;i++)
        {
            if(s.ContainsKey(secret[i]))s[secret[i]]++;
               else s.Add(secret[i],1);
        }
        int a=0,b=0;
        
        for(int i=0;i<guess.Length;i++)
        {
            if(secret[i]==guess[i]){a++;s[secret[i]]--;}
        }
        
        for(int i=0;i<guess.Length;i++)
        {
            if(secret[i]==guess[i]){}
            else
            {
                if(s.ContainsKey(guess[i]))
                {
                    if(s[guess[i]]>0){
                    b++;s[guess[i]]--;
                    }
                }
            }
        }
        
        return String.Format("{0}A{1}B",a,b);
    }
}
public class Solution {
    public string GetHint(string secret, string guess) {
        int len = secret.Length;
        int retA = 0;
        int retB = 0;
        Dictionary<int,int> compareS = new Dictionary<int,int>();
        Dictionary<int,int> compareG = new Dictionary<int,int>();
        for (int i = 0; i < len; ++i)
        {
            if (secret[i] == guess[i])
            {
                ++retA;
            }
            else
            {
                if (compareS.ContainsKey(guess[i]))
                {
                    --compareS[guess[i]];
                    ++retB;
                    if (compareS[guess[i]] == 0)
                        compareS.Remove(guess[i]);
                }
                else
                {
                    if (compareG.ContainsKey(guess[i]))
                    {
                        ++compareG[guess[i]];
                    }
                    else
                    {
                        compareG.Add(guess[i],1);
                    }
                }
                
                if (compareG.ContainsKey(secret[i]))
                {
                    --compareG[secret[i]];
                    ++retB;
                    if (compareG[secret[i]] == 0)
                        compareG.Remove(secret[i]);
                }
                else
                {
                    if (compareS.ContainsKey(secret[i]))
                    {
                        ++compareS[secret[i]];
                    }
                    else
                    {
                        compareS.Add(secret[i],1);
                    }
                }
            }
        }
        
        string ret = string.Format("{0}A{1}B",retA,retB);
        return ret;
        
    }
}
     
*/
