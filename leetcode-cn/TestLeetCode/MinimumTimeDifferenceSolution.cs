using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个 24 小时制（小时:分钟）的时间列表，找出列表中任意两个时间的最小时间差并已分钟数表示。


示例 1：

输入: ["23:59","00:00"]
输出: 1

备注:

列表中时间数在 2~20000 之间。
每个时间取值在 00:00~23:59 之间。
*/
/// <summary>
/// https://leetcode-cn.com/problems/minimum-time-difference/
/// 539. 最小时间差
/// </summary>
class MinimumTimeDifferenceSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int FindMinDifference(IList<string> timePoints)
    {
        if (timePoints.Count < 2) return 0;

        var times = timePoints.Select(t =>
        {
            int hour = 0;
            int i = 0;
            for (; t[i] != ':'; i++)
            {
                hour = hour * 10 + (t[i] - '0');
            }

            int min = 0;
            for (i = i + 1; i < t.Length; i++)
            {
                min = min * 10 + (t[i] - '0');
            }

            return 60 * hour + min;
        }).ToArray();

        Array.Sort(times);

        var ret = times[0] + 1440 - times[times.Length - 1];
        for( int i = 1; i < times.Length; i++ )
        {
            var diff = times[i] - times[i - 1];
            if (diff < ret) ret = diff;
        }
        return ret;
    }
}
/*
public class Solution {
    public  int FindMinDifference(IList<string> timePoints)
    {
        if (timePoints.Count < 2)
                return 0;

        List<int> sortedList = new List<int>();
        int mintime = 1440;

        int hr0 = Convert.ToInt32(timePoints[0].Split(':')[0]);
        int minutes0 = Convert.ToInt32(timePoints[0].Split(':')[1]);

        sortedList.Add(hr0 * 60 + minutes0);

        for (int i = 1; i < timePoints.Count; i++)
        {
            int hr = Convert.ToInt32(timePoints[i].Split(':')[0]);
            int minutes = Convert.ToInt32(timePoints[i].Split(':')[1]);

            int Mtotal = hr * 60 + minutes;

            bool added = false;
            for (int j = 0; j < sortedList.Count; j++)
            {
                if (Mtotal <= sortedList[j])
                {
                    if (j == 0)
                    {
                        sortedList.Insert(j, Mtotal);
                        int tempmin0 = sortedList[0] - sortedList[sortedList.Count - 1] + 1440;
                        int tempmin1 = sortedList[1] - sortedList[0];
                        mintime = Math.Min(mintime, Math.Min(tempmin0, tempmin1));

                    }
                    else
                    {
                        sortedList.Insert(j, Mtotal);
                        int tempmin0 = sortedList[j] - sortedList[j - 1];
                        int tempmin1 = sortedList[j + 1] - sortedList[j];
                        mintime = Math.Min(mintime, Math.Min(tempmin0, tempmin1));
                    }
                    added = true;
                    break;
                }
            }

            if (!added)
            {
                sortedList.Add(Mtotal);
                int tempmin0 = sortedList[0] - sortedList[sortedList.Count - 1] + 1440;
                int tempmin1 = sortedList[sortedList.Count - 1] - sortedList[sortedList.Count - 2];
                mintime = Math.Min(mintime, Math.Min(tempmin0, tempmin1));
            }

            if (mintime == 0)
                break;
        }
        return mintime;
    }
} 
public class Solution {
    public int FindMinDifference(IList<string> timePoints) {
        int n = timePoints.Count();
        int[] minuts = new int[n];
        
        for (int i = 0; i < n; i ++) {
            minuts[i] = TimeToMinute(timePoints[i]);
        }
        
        Array.Sort(minuts);
        
        int minDiff = int.MaxValue;
        for (int i = 0; i < n; i ++) {
            int diff;
            if (i + 1 == n) {
                diff = minuts[0] - minuts[i] + 24 * 60;
            } else {
                diff = minuts[i + 1] - minuts[i];
            }
            minDiff = Math.Min(minDiff, diff);
        }
        
        return minDiff;
        
    }
    
    int TimeToMinute(string time) {
        int hour = (int)(time[0]-'0') * 10 + (int)(time[1]-'0');
        int min = (int)(time[3]-'0') * 10 + (int)(time[4]-'0');
        return hour * 60 + min;
    }
}
public class Solution {
    public int FindMinDifference(IList<string> timePoints) {
        List<tm> ls=timePoints.Select(x=>new tm(x)).ToList();
        ls.Sort((x,y)=>{
            if(x.h>y.h)
                return 1;
            else if(x.h<y.h)
                return -1;
            else if(x.m>y.m)
                return 1;
            else if(x.m<y.m)
                return -1;
            else
                return 0;
        });
        
        int min=int.MaxValue;
        for(int i=0;i<ls.Count-1;i++)
        {
            int id=(i+1);
            int tmp=(ls[id].h-ls[i].h)*60+(ls[id].m-ls[i].m);
            if(tmp<min)
                min=tmp;
        }
        
        min=Math.Min(min,(ls[0].h-ls[ls.Count-1].h+24)*60+(ls[0].m-ls[ls.Count-1].m));
        
        return min;
    }
    
    public class tm
    {
        public int h;
        public int m;
        public tm(String txt)
        {
            String[] ls=txt.Split(':');
            h=int.Parse(ls[0]);
            m=int.Parse(ls[1]);
        }
    }
}
*/
