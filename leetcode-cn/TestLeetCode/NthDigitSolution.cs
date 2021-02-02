/*
在无限的整数序列 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, ...中找到第 n 位数字。

注意：n 是正数且在 32 位整数范围内（n < 231）。

示例 1：

输入：3
输出：3
示例 2：

输入：11
输出：0
解释：第 11 位数字在序列 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, ... 里是 0 ，它是 10 的一部分。
*/

/// <summary>
/// https://leetcode-cn.com/problems/nth-digit/
/// 400. 第 N 位数字
///
///
///
///
/// </summary>
internal class NthDigitSolution
{
    public int FindNthDigit(int n)
    {
        long N = n;
        long countOfCurrentWidth = 9;// 9, 90, 900, ...
        int digitWidth = 1; // 1, 2, 3, ...
        long digitCountOfCurrentWidth = digitWidth * countOfCurrentWidth;
        int start = 1; // 1, 10, 100, ...
        while (digitCountOfCurrentWidth < N)
        {
            N -= digitCountOfCurrentWidth;
            digitWidth++;
            countOfCurrentWidth *= 10;
            digitCountOfCurrentWidth = digitWidth * countOfCurrentWidth;
            start *= 10;
        }

        n = (int)N;
        int index = (n - 1);
        start += index / digitWidth;
        var num = start.ToString();

        return num[index % digitWidth] - '0';
    }
}

/*

【中规中矩】数学题
卖码翁
发布于 2020-12-14
465
解题思路
长度为1的数字有9个
长度为2的数字有90个
长度为3的数字有900个
长度为4的数字有9000个
。。。
根据n的值找到n所在的范围是长度len为几的。然后(n - 1)/len (注意-1是因为从1开始)得到对应的数字，比如n=1000，我们可以算出来len=3，n = 811, (811 - 1) / 3 = 270, (811 - 1) % 3 = 0, num = to_string(100 + 270) = "370",返回 num[0] - '0' = 3

容易出错的地方：
下面一行如果count=1e9 len = 10，乘积为1e10, 100亿超过int表示的最大值21亿，会出现整数越界错误。为防止该类错误，需要将len和count定义为long类型，或者强制转换 (long)(len * count)

while (n > len * count)

最后的结果digits[rem] 别忘了减掉'0'，转换为数字。

代码

// Brute force TLE
class Solution1 {
public:
    int findNthDigit(int n) {
        assert(n > 0 && "Expect n to be a positive number!");
        int count = 0;
        int i = 1;
        char res = 0;
        while (true) {
            auto digits = to_string(i);
            if (count + digits.size() >= n) {
                int index = n - count - 1;
                res = digits.at(index) - '0';
                break;
            } else {
                count += digits.size();
            }
            i++;
        }

        return res;
    }
};

class Solution {
public:
    int findNthDigit(long n) {
        assert(n > 0 && "Expect n to be a positive number!");
        long count = 9;
        long len = 1;
        long start = 1;
        while (n > len * count) {
            n -= len * count;
            len++;
            count *= 10;
            start *= 10;
        }

        start += (n - 1) / len; // find the number
        auto num = to_string(start);

        return num[(n - 1) % len] - '0';
    }
};

public class Solution
{
    public int FindNthDigit(int n)
    {
        long digit = 1;//位数
        long count = 9;//有几个
        long start = 1;//从几开始
        long curN = 0;
        while(n > count)
        {
            n -= (int)count;
            digit++;
            start *= 10;
            count = (9 * digit * start);

        }

        curN = start + (n - 1) / digit;
        string str = curN.ToString();
        return str[(n - 1) % (int)digit] - '0';

    }
}
*/