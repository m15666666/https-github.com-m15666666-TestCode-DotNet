using System;
using System.Collections.Generic;
using System.Linq;

/*
一个强密码应满足以下所有条件：

由至少6个，至多20个字符组成。
至少包含一个小写字母，一个大写字母，和一个数字。
同一字符不能连续出现三次 (比如 "...aaa..." 是不允许的, 但是 "...aa...a..." 是可以的)。
编写函数 strongPasswordChecker(s)，s 代表输入字符串，如果 s 已经符合强密码条件，则返回0；否则返回要将 s 修改为满足强密码条件的字符串所需要进行修改的最小步数。

插入、删除、替换任一字符都算作一次修改。

*/

/// <summary>
/// https://leetcode-cn.com/problems/strong-password-checker/
/// 420. 强密码检验器
///
///
/// </summary>
internal class StrongPasswordCheckerSolution
{
    public void Test()
    {
        var ret = StrongPasswordChecker("bbaaaaaaaaaaaaaaacccccc");
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int StrongPasswordChecker(string password)
    {
        const int Len_Min = 6;
        const int Len_Max = 20;

        // 统计小写字符
        int lowerCase = 0;
        // 统计大写字符
        int upwerCase = 0;
        // 统计数字
        int number = 0;
        // 统计连续字符出现的位置
        List<SameChar> sameChars = new List<SameChar>();
        var chars = password;
        if (string.IsNullOrEmpty(password) || password.Length == 0) return Len_Min;
        int len = password.Length;

        // 记露连续出现的字符
        SameChar sameChar = new SameChar(0, 0, '\0');
        for (int i = 0; i < chars.Length; i++)
        {
            var c = chars[i];
            if ('a' <= c && c <= 'z') lowerCase++;
            else if ('A' <= c && c <= 'Z') upwerCase++;
            else if ('0' <= c && c <= '9') number++;

            if (sameChar.C != c)
            {
                if (sameChar.MatchLength) sameChars.Add(new SameChar(sameChar.Start, sameChar.End, sameChar.C));

                sameChar.C = c;
                sameChar.Start = i;
                sameChar.End = i;
            }
            else sameChar.End = i;
        }
        if (sameChar.MatchLength) sameChars.Add(new SameChar(sameChar.Start, sameChar.End, sameChar.C));

        // 缺失的类型. 只可能是1 or 2
        int needType = CountZero(lowerCase, upwerCase, number);

        // 如果长度小于6 , 很简单 要补的字符和缺失的类型择大
        if (len < Len_Min) return Math.Max(Len_Min - len, needType);

        // 连续的字符出现的要消除的个数 连续值-2
        var changes = sameChars.Select(item => item.Count - 2).ToArray();

        int ret = 0;
        // 删除的时候 要有优先概念
        if (Len_Max < len)
        {
            int index;

            int deleteCount = len - Len_Max;
            while (0 < deleteCount && -1 < (index = Find(changes, 1)))
            {
                deleteCount--;
                ret++;
                changes[index]--;
            }

            while (0 < needType && -1 < (index = Find(changes, 0)))
            {
                changes[index] = Math.Max(changes[index] - 3, 0);
                ret++;
                needType--;
            }

            foreach (int change in changes)
                ret += change % 3 == 0 ? change / 3 : change / 3 + 1;

            return ret + deleteCount + needType;
        }
        foreach (int change in changes)
            ret += change % 3 == 0 ? change / 3 : change / 3 + 1;

        return Math.Max(ret, needType);
    }

    private static int CountZero(params int[] array) => array.Count(item => item == 0);

    private static int Find(int[] array, int n)
    {
        int n0 = -1;
        int n1 = -1;
        int n2 = -1;

        for (int i = 0; i < array.Length; i++)
        {
            var len = array[i];
            if (len < 1) continue;
            switch (len % 3)
            {
                case 0: n0 = i; break;
                case 1: n1 = i; break;
                case 2: n2 = i; break;
            }
        }
        return n switch
        {
            0 => -1 < n0 ? n0 : (-1 < n2 ? n2 : n1),
            1 => -1 < n1 ? n1 : (-1 < n2 ? n2 : n0),
            _ => -1,
        };
    }

    public class SameChar
    {
        public int Start;
        public int End;
        public char C;

        public SameChar(int start, int end, char c)
        {
            Start = start;
            End = end;
            C = c;
        }

        public int Count => End - Start + 1;
        public bool MatchLength => 3 <= Count;
    }
}

/*
强密码检验器
出云曦仪
发布于 2019-07-10
6.5k
解题思路：
主要先考虑如果去消除连续字符，nn 代表步数，ss 代表连续的个数，最后的目标都是小于 33。

删除 效率最低 s-n*1<3
插入 效率其次 s-n*2<3
替换 效率最高 s-n*3<3
举例 aaaaa 五连字符，要正确的话如果只删除要 33 步， 如果插入的话要 22 步，如果替换只需要替换中间的 aa 一步就可以完成。

接下来 分情况讨论

长度 <6 ，步数=缺失类型和缺失长度取大者。
长度 (6,20)，这时候我们不需要低效的插入和删除来处理连续字符，直接替换步数就等于处理连续字和缺失类型取大者。
比较负载的是 >20，我们需要知道优先级，一样优先处理连续数组。
优先处理缺失类型，用替换的方式来处理，这时候要替换的连续组的连续数 %3==2 -> 连续数%3==1 -> 连续数%3==0，然后处理多余字符，删除的优先级是连续组的连续数 %3==0 -> 连续数%3==1 -> 连续数%3==2。
代码：

public class Solution3 {
    class SameChar {
        int st;
        int en;
        char c;

        SameChar(int st, int en, char c) {
            this.st = st;
            this.en = en;
            this.c = c;
        }

    }

    public int strongPasswordChecker(String str) {
        // 统计小写字符
        int lowerCase = 0;
        // 统计大写字符
        int upwerCase = 0;
        // 统计数字
        int number = 0;
        // 统计连续字符出现的位置
        java.util.ArrayList<SameChar> sameChars = new java.util.ArrayList<SameChar>();
        char[] chars = str.toCharArray();
        if (chars.length == 0) {
            return 6;
        }
        // 记露连续出现的字符
        SameChar sameChar = new SameChar(0, 0, '\0');
        for (int i = 0; i < chars.length; i++) {
            if (chars[i] >= 'a' && chars[i] <= 'z') {
                lowerCase++;
            } else if (chars[i] >= 'A' && chars[i] <= 'Z') {
                upwerCase++;
            } else if (chars[i] >= '0' && chars[i] <= '9') {
                number++;
            }
            if (sameChar.c != chars[i]) {
                if (sameChar.en - sameChar.st >= 2) {
                    sameChars.add(new SameChar(sameChar.st, sameChar.en, sameChar.c));
                }
                sameChar.c = chars[i];
                sameChar.st = i;
                sameChar.en = i;
            } else {
                sameChar.en = i;
            }
        }
        if (sameChar.en - sameChar.st >= 2) {
            sameChars.add(new SameChar(sameChar.st, sameChar.en, sameChar.c));
        }
        // 缺失的类型. 只可能是1 or 2
        int needType = count0(lowerCase, upwerCase, number);
        // 连续的字符出现的要消除的个数 连续值-2
        int[] chages = new int[sameChars.size()];
        for (int j = 0; j < sameChars.size(); j++) {
            chages[j] = sameChars.get(j).en - sameChars.get(j).st - 1;
        }
        int res = 0;
        // 如果长度小于6 , 很简单 要补的字符和缺失的类型择大
        if (str.length() < 6) {
            return Integer.max(6 - str.length(), needType);
        }
        // 删除的时候 要有优先概念
        if (str.length() > 20) {
            int index = -1;
            while (needType > 0 && (index = find(chages, 0)) > -1) {
                chages[index] = Integer.max(chages[index] - 3, 0);
                res++;
                needType--;
            }
            int d = str.length() - 20;
            while (d > 0 && (index = find(chages, 1)) > -1) {
                d--;
                res++;
                chages[index]--;
            }
            int n = 0;
            for (int l = 0; l < chages.length; l++) {
                n += chages[l] % 3 == 0 ? chages[l] / 3 : chages[l] / 3 + 1;
            }
            return res + d + needType + n;
        }
        int n = 0;
        for (int l = 0; l < chages.length; l++) {
            n += chages[l] % 3 == 0 ? chages[l] / 3 : chages[l] / 3 + 1;
        }
        return Integer.max(n, needType);
    }

    private int count0(int... array) {
        int n = 0;
        for (int i = 0; i < array.length; i++) {
            if (array[i] == 0) {
                n++;
            }
        }
        return n;
    }

    private int find(int[] array, int n) {
        int n0 = -1;
        int n1 = -1;
        int n2 = -1;
        for (int i = 0; i < array.length; i++) {
            if (array[i] > 0 && array[i] % 3 == 0) {
                n0 = i;
            }
            if (array[i] > 0 && array[i] % 3 == 1) {
                n1 = i;
            }
            if (array[i] > 0 && array[i] % 3 == 2) {
                n2 = i;
            }
        }
        if (n == 0) {
            return n0 > -1 ? n0 : (n2 > -1 ? n2 : n1);
        }
        if (n == 1) {
            return n1 > -1 ? n1 : (n2 > -1 ? n2 : n0);
        }
        return -1;
    }
}

*/