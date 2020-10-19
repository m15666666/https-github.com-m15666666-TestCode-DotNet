using System.Text;

/*
将非负整数转换为其对应的英文表示。可以保证给定输入小于 231 - 1 。

示例 1:

输入: 123
输出: "One Hundred Twenty Three"
示例 2:

输入: 12345
输出: "Twelve Thousand Three Hundred Forty Five"
示例 3:

输入: 1234567
输出: "One Million Two Hundred Thirty Four Thousand Five Hundred Sixty Seven"
示例 4:

输入: 1234567891
输出: "One Billion Two Hundred Thirty Four Million Five Hundred Sixty Seven Thousand Eight Hundred Ninety One"

*/

/// <summary>
/// https://leetcode-cn.com/problems/integer-to-english-words/
/// 273. 整数转换英文表示
///
///
///
/// </summary>
internal class IntegerToEnglishWordsSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public string NumberToWords(int num)
    {
        if (num == 0) return "Zero";

        const int Billion = 1000_000_000;
        const int Million = 1000_000;
        const int Thousand = 1000;
        int billion = num / Billion;
        int million = (num % Billion) / Million;
        int thousand = (num % Million) / Thousand;
        int rest = num % Thousand;

        var result = new StringBuilder();
        if (0 < billion) result.Append(ThreeDigits(billion)).Append(" Billion");
        if (0 < million)
        {
            if (0 < result.Length) result.Append(" ");
            result.Append(ThreeDigits(million)).Append(" Million");
        }
        if (0 < thousand)
        {
            if (0 < result.Length) result.Append(" ");
            result.Append(ThreeDigits(thousand)).Append(" Thousand");
        }
        if (0 < rest)
        {
            if (0 < result.Length) result.Append(" ");
            result.Append(ThreeDigits(rest));
        }
        return result.ToString();
    }

    private static string OneDigit(int num) => num switch
    {
        1 => "One",
        2 => "Two",
        3 => "Three",
        4 => "Four",
        5 => "Five",
        6 => "Six",
        7 => "Seven",
        8 => "Eight",
        9 => "Nine",
        _ => "",
    };

    private static string TwoDigitsLessThan20(int num) => num switch
    {
        10 => "Ten",
        11 => "Eleven",
        12 => "Twelve",
        13 => "Thirteen",
        14 => "Fourteen",
        15 => "Fifteen",
        16 => "Sixteen",
        17 => "Seventeen",
        18 => "Eighteen",
        19 => "Nineteen",
        _ => "",
    };

    private static string Ten(int num) => num switch
    {
        2 => "Twenty",
        3 => "Thirty",
        4 => "Forty",
        5 => "Fifty",
        6 => "Sixty",
        7 => "Seventy",
        8 => "Eighty",
        9 => "Ninety",
        _ => "",
    };

    private static string TwoDigits(int num)
    {
        if (num == 0) return "";
        if (num < 10) return OneDigit(num);
        if (num < 20) return TwoDigitsLessThan20(num);
        int tenner = num / 10;
        int rest = num % 10;
        return 0 < rest ? $"{Ten(tenner)} {OneDigit(rest)}" : Ten(tenner);
    }

    private static string ThreeDigits(int num)
    {
        int hundred = num / 100;
        int rest = num % 100;
        if ((0 < hundred) && (0 < rest)) return $"{OneDigit(hundred)} Hundred {TwoDigits(rest)}";
        if (0 < rest) return TwoDigits(rest);
        if (0 < hundred) return OneDigit(hundred) + " Hundred";
        return "";
    }
}

/*
整数转换英文表示
力扣 (LeetCode)
发布于 2019-06-24
7.7k
方法一：分治
我们将这个问题分解成一系列子问题。例如，对于数字 1234567890，我们将它从低位开始每三个分成一组，得到 1,234,567,890，它的英文表示为 1 Billion 234 Million 567 Thousand 890。这样我们就将原问题分解成若干个三位整数转换为英文表示的问题了。

接下来，我们可以继续将三位整数分解，例如数字 234 可以分别成百位 2 和十位个位 34，它的英文表示为 2 Hundred 34。这样我们继续将原问题分解成一位整数和两位整数的英文表示。其中一位整数的表示是很容易的，而两位整数中除了 10 到 19 以外，其余整数的的表示可以分解成两个一位整数的表示，这样问题就被圆满地解决了。下面的幻灯片中给出了 1234567890 得到英文表示的例子。




class Solution {
    public String one(int num) {
        switch(num) {
            case 1: return "One";
            case 2: return "Two";
            case 3: return "Three";
            case 4: return "Four";
            case 5: return "Five";
            case 6: return "Six";
            case 7: return "Seven";
            case 8: return "Eight";
            case 9: return "Nine";
        }
        return "";
    }

    public String twoLessThan20(int num) {
        switch(num) {
            case 10: return "Ten";
            case 11: return "Eleven";
            case 12: return "Twelve";
            case 13: return "Thirteen";
            case 14: return "Fourteen";
            case 15: return "Fifteen";
            case 16: return "Sixteen";
            case 17: return "Seventeen";
            case 18: return "Eighteen";
            case 19: return "Nineteen";
        }
        return "";
    }

    public String ten(int num) {
        switch(num) {
            case 2: return "Twenty";
            case 3: return "Thirty";
            case 4: return "Forty";
            case 5: return "Fifty";
            case 6: return "Sixty";
            case 7: return "Seventy";
            case 8: return "Eighty";
            case 9: return "Ninety";
        }
        return "";
    }

    public String two(int num) {
        if (num == 0)
            return "";
        else if (num < 10)
            return one(num);
        else if (num < 20)
            return twoLessThan20(num);
        else {
            int tenner = num / 10;
            int rest = num - tenner * 10;
            if (rest != 0)
              return ten(tenner) + " " + one(rest);
            else
              return ten(tenner);
        }
    }

    public String three(int num) {
        int hundred = num / 100;
        int rest = num - hundred * 100;
        String res = "";
        if (hundred * rest != 0)
            res = one(hundred) + " Hundred " + two(rest);
        else if ((hundred == 0) && (rest != 0))
            res = two(rest);
        else if ((hundred != 0) && (rest == 0))
            res = one(hundred) + " Hundred";
        return res;
    }

    public String numberToWords(int num) {
        if (num == 0)
            return "Zero";

        int billion = num / 1000000000;
        int million = (num - billion * 1000000000) / 1000000;
        int thousand = (num - billion * 1000000000 - million * 1000000) / 1000;
        int rest = num - billion * 1000000000 - million * 1000000 - thousand * 1000;

        String result = "";
        if (billion != 0)
            result = three(billion) + " Billion";
        if (million != 0) {
            if (! result.isEmpty())
                result += " ";
            result += three(million) + " Million";
        }
        if (thousand != 0) {
            if (! result.isEmpty())
                result += " ";
            result += three(thousand) + " Thousand";
        }
        if (rest != 0) {
            if (! result.isEmpty())
                result += " ";
            result += three(rest);
        }
        return result;
    }
}
复杂度分析

时间复杂度：O(N)O(N)。其中 NN 是输入整数的长度。由于输出的英文表示长度和输入整数的长度是成正比的，因此时间复杂度为 O(N)O(N)。
空间复杂度：O(1)O(1)。

public class Solution {
    private string[] int2teen = {
        "",
        "One",
        "Two",
        "Three",
        "Four",
        "Five",
        "Six",
        "Seven",
        "Eight",
        "Nine",
        "Ten",
        "Eleven",
        "Twelve",
        "Thirteen",
        "Fourteen",
        "Fifteen",
        "Sixteen",
        "Seventeen",
        "Eighteen",
        "Nineteen"
    };

    private string[] int2ten = {
        "",
        "",
        "Twenty",
        "Thirty",
        "Forty",
        "Fifty",
        "Sixty",
        "Seventy",
        "Eighty",
        "Ninety"
    };

    public string NumberToWords(int num) {
        if (num == 0) {
            return "Zero";
        }

        return (this.NumberInThousandToWords(num / 1000000000, " Billion ") + 
            this.NumberInThousandToWords(num % 1000000000 / 1000000, " Million ") + 
            this.NumberInThousandToWords(num % 1000000 / 1000, " Thousand ") + 
            this.NumberInThousandToWords(num % 1000, string.Empty)).TrimEnd();
    }

    private string NumberInThousandToWords(int num, string unit) {
        if (num == 0) {
            return string.Empty;
        }
        var s = this.NumberInHundredToWords(num % 100);
        if (num >= 100) {
            s = this.int2teen[num/100] + " Hundred " + s;
        }
        return s.TrimEnd() + unit;
    }

    private string NumberInHundredToWords(int num) {
        if (num < 20) {
            return this.int2teen[num];
        }
        return this.int2ten[num/10] + " " + this.int2teen[num%10];
    }
}

public class Solution {
    public string NumberToWords(int num) {
        

        if(num==0) return "Zero";
        
        num_struct[0]=num/1000000000;
        num_struct[1]=(num%1000000000)/1000000;
        num_struct[2]=(num%1000000)/1000;
        num_struct[3]=num%1000;

        List<string> result=new List<string>();
        if(num_struct[0]>0)
        {
            int billion=num_struct[0];
            result.Add(basic_num[billion]);
            result.Add("Billion");
        }
        if(num_struct[1]>0)
        {
            result.AddRange(ThreeNumWords(num_struct[1]));
            result.Add("Million");
        }
        if(num_struct[2]>0)
        {
            result.AddRange(ThreeNumWords(num_struct[2]));
            result.Add("Thousand");
        }
        if(num_struct[3]>0)
        {
            result.AddRange(ThreeNumWords(num_struct[3]));
        }
        
        return string.Join(' ',result);
    }
    string[] basic_num=new string[]{"","One","Two","Three","Four","Five","Six","Seven","Eight","Nine"};
    string[] tens_num=new string[]{"","Ten","Twenty","Thirty","Forty","Fifty","Sixty","Seventy","Eighty","Ninety"};
    string[] teen_num=new string[]{"Ten","Eleven","Twelve","Thirteen","Fourteen","Fifteen","Sixteen","Seventeen","Eighteen","Nineteen"};
    string[] huge_num=new string[]{"","Thousand","Million","Billion"};
    int[] num_struct=new int[4];
    public List<string> ThreeNumWords(int s)
    {
        List<string> result=new List<string>();
        if(s>=100)
        {
            int hundred=s/100;
            result.Add(basic_num[hundred]);
            result.Add("Hundred");
            s=s%100;
        }
        int ten=s/10;
        int basic=s%10;
        if(s>=20)
        {
            result.Add(tens_num[ten]);
            if(basic>0) result.Add(basic_num[basic]);
        }
        if(s<20 && s>=10)
        {
            result.Add(teen_num[s-10]);
        }
        if(s<10 && s>0)
        {
            result.Add(basic_num[s]);
        }
        return result;
    }
}

public class Solution {
    public string NumberToWords(int num) {
  if (num < 20) { return nums[num]; }
            StringBuilder sb = new StringBuilder();
            if (num < 100) 
            {
                int x = num / 10;
                int y = num % 10;
                sb.Append(tys[x]);
                if (y > 0) { sb.Append(' ' + nums[y]); }
            }
            else if(num<1000)
            {
                int x = num / 100;
                int y = num % 100;
                sb.Append(nums[x]);
                sb.Append(" Hundred");
                if (y > 0) { sb.Append(' ' +NumberToWords(y)); }

            }
            else if (num < 1000000)
            {
                int x = num / 1000;
                int y = num % 1000;
                sb.Append(NumberToWords(x));
                sb.Append(" Thousand");
                if (y > 0) { sb.Append(' ' +NumberToWords(y)); }

            }
            else if (num < 1000000000)
            {
                int x = num / 1000000;
                int y = num % 1000000;
                sb.Append(NumberToWords(x));
                sb.Append(" Million");
                if (y > 0) { sb.Append(' ' +NumberToWords(y)); }

            }
            else 
            {
                int x = num / 1000000000;
                int y = num % 1000000000;
                sb.Append(NumberToWords(x));
                sb.Append(" Billion");
                if (y > 0) { sb.Append(' ' +NumberToWords(y)); }

            }
            return sb.ToString();
    }
           public Dictionary<int, string> nums = new Dictionary<int, string>()
{
    {0, "Zero"},
    {1, "One"},
    {2, "Two"},
    {3, "Three"},
    {4, "Four"},
    {5, "Five"},
    {6, "Six"},
    {7, "Seven"},
    {8, "Eight"},
    {9, "Nine"},
    {10, "Ten"},
    {11, "Eleven"},
    {12, "Twelve"},
    {13, "Thirteen"},
    {14, "Fourteen"},
    {15, "Fifteen"},
    {16, "Sixteen"},
    {17, "Seventeen"},
    {18, "Eighteen"},
    {19, "Nineteen"}
};

        public Dictionary<int, string> tys = new Dictionary<int, string>()
{
    {2, "Twenty"},
    {3, "Thirty"},
    {4, "Forty"},
    {5, "Fifty"},
    {6, "Sixty"},
    {7, "Seventy"},
    {8, "Eighty"},
    {9, "Ninety"}
};
}


*/