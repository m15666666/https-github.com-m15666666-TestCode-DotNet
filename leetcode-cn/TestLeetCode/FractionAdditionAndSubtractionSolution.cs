using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个表示分数加减运算表达式的字符串，你需要返回一个字符串形式的计算结果。 
这个结果应该是不可约分的分数，即最简分数。 如果最终结果是一个整数，
例如 2，你需要将它转换成分数形式，其分母为 1。所以在上述例子中, 2 应该被转换为 2/1。

示例 1:

输入:"-1/2+1/2"
输出: "0/1"
 示例 2:

输入:"-1/2+1/2+1/3"
输出: "1/3"
示例 3:

输入:"1/3-1/2"
输出: "-1/6"
示例 4:

输入:"5/3+1/3"
输出: "2/1"
说明:

输入和输出字符串只包含 '0' 到 '9' 的数字，以及 '/', '+' 和 '-'。 
输入和输出分数格式均为 ±分子/分母。如果输入的第一个分数或者输出的分数是正数，则 '+' 会被省略掉。
输入只包含合法的最简分数，每个分数的分子与分母的范围是  [1,10]。 如果分母是1，意味着这个分数实际上是一个整数。
输入的分数个数范围是 [1,10]。
最终结果的分子与分母保证是 32 位整数范围内的有效整数。 
*/
/// <summary>
/// https://leetcode-cn.com/problems/fraction-addition-and-subtraction/
/// 592. 分数加减运算
/// https://www.cnblogs.com/grandyang/p/6954197.html
/// </summary>
class FractionAdditionAndSubtractionSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }
    /// <summary>
    /// 这道题让我们做分数的加减法，给了我们一个分数加减法式子的字符串，
    /// 然我们算出结果，结果当然还是用分数表示了。那么其实这道题主要就是字符串的拆分处理，
    /// 再加上一点中学的数学运算的知识就可以了。这里我们使用字符流处理类来做，
    /// 每次按顺序读入一个数字，一个字符，和另一个数字。分别代表了分子，除号，分母。
    /// 我们初始化分子为0，分母为1，这样就可以进行任何加减法了。
    /// 中学数学告诉我们必须将分母变为同一个数，分子才能相加，为了简便，我们不求最小公倍数，
    /// 而是直接乘上另一个数的分母，然后相加。不过得到的结果需要化简一下，
    /// 我们求出分子分母的最大公约数，记得要取绝对值，然后分子分母分别除以这个最大公约数就是最后的结果了，
    /// 参见代码如下：
    /// </summary>
    /// <param name="expression"></param>
    /// <returns></returns>
    public string FractionAddition(string expression)
    {
        if (string.IsNullOrWhiteSpace(expression)) return string.Empty;

        StringBuilder sb = new StringBuilder();
        
        var parts = expression.Replace("-", "+-").Split('+', StringSplitOptions.RemoveEmptyEntries);
        int a = 0; // 分子
        int b = 1; // 分母
        var splitChars = new char[] { '-', '/' };
        foreach ( var part in parts )
        {
            var nums = part.Split( splitChars, StringSplitOptions.RemoveEmptyEntries );
            int num = int.Parse(nums[0]);
            if (part.Contains('-')) num = -num;
            int dem = int.Parse(nums[1]);

            // 同分，相加
            a = a * dem + num * b;
            b *= dem;

            int g = Math.Abs(Gcd(a, b));

            // 分子分母同除最大公因数
            a /= g;
            b /= g;
        }
        return $"{a}/{b}";

        int Gcd(int x, int y)
        {
            return (y == 0) ? x : Gcd(y, x % y);
        }
    }
}