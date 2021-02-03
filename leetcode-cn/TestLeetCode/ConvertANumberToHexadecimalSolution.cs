using System.Text;

/*
给定一个整数，编写一个算法将这个数转换为十六进制数。对于负整数，我们通常使用 补码运算 方法。

注意:

十六进制中所有字母(a-f)都必须是小写。
十六进制字符串中不能包含多余的前导零。如果要转化的数为0，那么以单个字符'0'来表示；对于其他情况，十六进制字符串中的第一个字符将不会是0字符。 
给定的数确保在32位有符号整数范围内。
不能使用任何由库提供的将数字直接转换或格式化为十六进制的方法。
示例 1：

输入:
26

输出:
"1a"
示例 2：

输入:
-1

输出:
"ffffffff"

*/

/// <summary>
/// https://leetcode-cn.com/problems/convert-a-number-to-hexadecimal/
/// 405. 数字转换为十六进制数
///
/// </summary>
internal class ConvertANumberToHexadecimalSolution
{
    public void Test()
    {
        //var ret = ToHex(26);
        var ret = ToHex(-1);
    }

    public string ToHex(int num)
    {
        if (num == 0) return "0";
        const string Hex = "0123456789abcdef";
        const uint Mask = 0xf;
        StringBuilder builder = new StringBuilder(8);
        uint uNum = (uint)num;
        while (uNum != 0)
        {
            builder.Insert(0, Hex[(int)(uNum & Mask)]);
            uNum >>= 4;
        }
        return builder.ToString();
    }
}

/*
C++ 将num转为unsigned类型
zPatrick
发布于 2021-01-15
233
解题思路
C++ 将num转为unsigned类型，即可进行逻辑右移，此时对于负数而言，进行右移时，左侧添加的是0，而不是int类型的符号位。
image.png

代码

class Solution {
public:
    string toHex(int num) {
        if(num == 0)    return "0";
        string ans = "";
        unsigned num2 = num;
        string s = "0123456789abcdef";
        while(num2 != 0){
            ans = s[num2 & 15] + ans;
            num2 >>= 4;
        }
        return ans;
    }
};

class Solution {
public:
    string toHex(int num) {
        int resnum=1;
        string hex="0123456789abcdef";
        string res="";
        if(!num) return "0";
        else
        {
            while(num&&resnum<=8){
                res=hex[num&0x0000f]+res;
            num>>=4;
            resnum++;
             }
        }
        return res;

    }
};

public class Solution {
    public string ToHex(int num) {
        return num.ToString("X").ToLower();
    }
}

public class Solution {
    public string ToHex(int num) {
        if(num==0) return "0";
        string s="0123456789abcdef";
        string res="";
        uint n=(uint)num;
        while(n!=0){
            //最低4位
            uint tmp=n&15;
            res=s[(int)tmp]+res;
            n=n>>4;
        }
        return res;
    }
}

public class Solution {
    public string ToHex(int num) {
        if(num == 0) return "0";
        char[] map = new char[] {'0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f'};
        string s ="";
        uint n = (uint)num;
        while(n !=0)
        {
            s = map[n & 0b1111] + s;
            n >>= 4;
        }
        return s;
    }
}


*/