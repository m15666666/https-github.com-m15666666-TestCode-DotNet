using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
罗马数字包含以下七种字符： I， V， X， L，C，D 和 M。

字符          数值
I             1
V             5
X             10
L             50
C             100
D             500
M             1000
例如， 罗马数字 2 写做 II ，即为两个并列的 1。12 写做 XII ，即为 X + II 。 27 写做  XXVII, 即为 XX + V + II 。

通常情况下，罗马数字中小的数字在大的数字的右边。但也存在特例，例如 4 不写做 IIII，而是 IV。数字 1 在数字 5 的左边，所表示的数等于大数 5 减小数 1 得到的数值 4 。同样地，数字 9 表示为 IX。这个特殊的规则只适用于以下六种情况：

I 可以放在 V (5) 和 X (10) 的左边，来表示 4 和 9。
X 可以放在 L (50) 和 C (100) 的左边，来表示 40 和 90。 
C 可以放在 D (500) 和 M (1000) 的左边，来表示 400 和 900。
给定一个整数，将其转为罗马数字。输入确保在 1 到 3999 的范围内。

示例 1:

输入: 3
输出: "III"
示例 2:

输入: 4
输出: "IV"
示例 3:

输入: 9
输出: "IX"
示例 4:

输入: 58
输出: "LVIII"
解释: L = 50, V = 5, III = 3.
示例 5:

输入: 1994
输出: "MCMXCIV"
解释: M = 1000, CM = 900, XC = 90, IV = 4.
*/
/// <summary>
/// https://leetcode-cn.com/problems/integer-to-roman/
/// 12. 整数转罗马数字
/// 
/// https://blog.csdn.net/net_wolf_007/article/details/51770112
/// </summary>
class IntToRomanSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    private static readonly int[] _number = { 1000, 900, 500, 400, 100, 90, 50, 40, 10, 9, 5, 4, 1 };
    private static readonly string[] _flags = { "M", "CM", "D", "CD", "C", "XC", "L", "XL", "X", "IX", "V", "IV", "I" };
    public string IntToRoman(int num)
    {
        if( num <= 0 ) return string.Empty;

        StringBuilder ret = new StringBuilder();
        
        for (int i = 0; i < _number.Length && 0 < num; i++)
        {
            var threshold = _number[i];
            if ( num < threshold ) continue;

            while ( threshold <= num )
            {
                num -= threshold;

                ret.Append(_flags[i]);
            }
        }
        return ret.ToString(); 
    }
}
/*

最快0ms！直接按照每一位0~10的情况存进字符数组里，遍历四位就行了
int myheart
发布于 21 天前
1.6k
解题思路
最简单的写法

代码
class Solution {
public:
    string intToRoman(int num)
    {
        char* c[4][10] = {
            {"","I","II","III","IV","V","VI","VII","VIII","IX"},
            {"","X","XX","XXX","XL","L","LX","LXX","LXXX","XC"},
            {"","C","CC","CCC","CD","D","DC","DCC","DCCC","CM"},
            {"","M","MM","MMM"}
        };
        string roman;
        roman.append(c[3][num / 1000]);
        roman.append(c[2][num / 100 % 10]);
        roman.append(c[1][num / 10 % 10]);
        roman.append(c[0][num % 10]);
         
        return roman;
    }
};

贪心算法（欢迎大家补充贪心算法的有效性证明）
liweiwei1419
发布于 8 个月前
20.0k
说明：

这道题能想到用贪心算法，是来源于生活中的经验，使用贪心算法有的时候是一种尝试，我查阅了一些资料，但是还是不能很清楚地证明贪心算法的有效性。

重点：贪心法之所有有效，完全是跟这道题给出的数据有关的，换一些数值就不一定可以使用，请大家留意。

生活中的经验：

在以前还使用现金购物的时候，如果我们不想让对方找钱，付款的时候我们会尽量选择面值大的纸币给对方，这样才会使得我们给对方的纸币张数最少，对方点钱的时候也最方便。

本题“整数转罗马数字”也有类似的思想：在表示一个较大整数的时候，“罗马数字”的设计者不会让你都用 11 加起来，我们总是希望写出来的“罗马数字”的个数越少越好，以方便表示，并且这种表示方式还应该是唯一的。

思路分析：

本题中，首先给出“罗马数字”与“阿拉伯数字”的对应关系如下：

罗马数字	阿拉伯数字
I	11
V	55
X	1010
L	5050
C	100100
D	500500
M	10001000
题目还给了一些特例，我们需要推导出“罗马数字”与“阿拉伯数字”其它的对应关系。为此，从头开始举例子，以发现规律：

阿拉伯数字	转换规则	罗马数字
11	直接看表	I
22	2 = 1 + 12=1+1，相同数字简单叠加	II
33	3 = 1 + 1 + 13=1+1+1，相同数字简单叠加	III
44	题目中说的特例：不能写成 4 = 1 + 1 + 1 + 14=1+1+1+1，44 应该看做 4 = 5 - 14=5−1	IV
55	直接看表	V
6	6 = 5 + 16=5+1，大数字在前，小数字在后	VI
7	7 = 5 + 1 + 17=5+1+1，大数字在前，小数字在后，相同数字简单叠加	VII
88	8 = 5 + 1 + 1 + 18=5+1+1+1，大数字在前，小数字在后，相同数字简单叠加	VIII
99	题目中说的特例：不能写成 9 = 5 + 1 + 1 + 1 + 19=5+1+1+1+1，99 应该看做 9 = 10 - 19=10−1	IX
1010	直接看表	X
说明：其实在题目中已经强调了一些特例，出现 44、99、4040、9090、400400、900900 （40004000、90009000 不讨论了，题目测试用例中有说，不会超过 39993999）的情况比较特殊一些，做的是减法，把它们也加入到“罗马数字”与阿拉伯数字的对应关系表中，并且按照从大到小的顺序排列。

罗马数字	阿拉伯数字
M	10001000
CM	900900
D	500500
CD	400400
C	100100
XC	9090
L	5050
XL	4040
X	1010
IX	99
V	55
IV	44
I	11
于是，“将整数转换为罗马数字”的过程，就是用上面这张表中右边的数字作为“加法因子”去分解一个整数，目的是“分解的整数个数”尽可能少，因此，对于这道问题，类似于用最少的纸币凑成一个整数，贪心算法的规则如下：

每一步都使用当前较大的罗马数字作为加法因子，最后得到罗马数字表示就是长度最少的。

参考代码：

public class Solution {

    public String intToRoman(int num) {
        // 把阿拉伯数字与罗马数字可能出现的所有情况和对应关系，放在两个数组中
        // 并且按照阿拉伯数字的大小降序排列，这是贪心选择思想
        int[] nums = {1000, 900, 500, 400, 100, 90, 50, 40, 10, 9, 5, 4, 1};
        String[] romans = {"M", "CM", "D", "CD", "C", "XC", "L", "XL", "X", "IX", "V", "IV", "I"};

        StringBuilder stringBuilder = new StringBuilder();
        int index = 0;
        while (index < 13) {
            // 特别注意：这里是等号
            while (num >= nums[index]) {
                // 注意：这里是等于号，表示尽量使用大的"面值"
                stringBuilder.append(romans[index]);
                num -= nums[index];
            }
            index++;
        }
        return stringBuilder.toString();
    }
}
复杂度分析：

时间复杂度：O(1)O(1)，虽然看起来是两层循环，但是外层循环的次数最多 1212，内层循环的此时其实也是有限次的，综合一下，时间复杂度是 O(1)O(1)。
空间复杂度：O(1)O(1)，这里使用了两个辅助数字，空间都为 1313，还有常数个变量，故空间复杂度是 O(1)O(1)。
 
*/
