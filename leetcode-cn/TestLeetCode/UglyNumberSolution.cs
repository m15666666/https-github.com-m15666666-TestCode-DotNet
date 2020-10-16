using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
编写一个程序判断给定的数是否为丑数。

丑数就是只包含质因数 2, 3, 5 的正整数。

示例 1:

输入: 6
输出: true
解释: 6 = 2 × 3
示例 2:

输入: 8
输出: true
解释: 8 = 2 × 2 × 2
示例 3:

输入: 14
输出: false 
解释: 14 不是丑数，因为它包含了另外一个质因数 7。
说明：

1 是丑数。
输入不会超过 32 位有符号整数的范围: [−231,  231 − 1]。

*/
/// <summary>
/// https://leetcode-cn.com/problems/ugly-number/
/// 263. 丑数
/// 
/// </summary>
class UglyNumberSolution
{
    public void Test()
    {

    }

    public bool IsUgly(int num) 
    {
        if (num < 1) return false;
        while (num % 2 == 0) num /= 2;
        while (num % 3 == 0) num /= 3;
        while (num % 5 == 0) num /= 5;
        return num == 1;
    }
}
/*
t0 = datenum('Aug. 17, 1939')
t1 = fix(now);
t = (t1-28):1:(t1+28);
y = 100*[sin(2*pi*(t-t0)/23)
sin(2*pi*(t-t0)/28)
sin(2*pi*(t-t0)/33)];
plot(t,y)

垦荒人 忽悠老人 旅行 巴马


public class Solution {
    public bool IsUgly(int num) {
        if(num < 1) return false;
        while(num % 2 == 0) num/=2;
        while(num % 3 == 0) num/=3;
        while(num % 5 == 0) num/=5;
        return num == 1;
    }
}

public class Solution {
    public bool IsUgly(int num) {
if( num < 1 )
            return false;
        int[] arr = new int[]{2,3,5};
        foreach( int i in arr )
            while( num % i == 0 )
                num = num/i;
        if( num == 1 )
            return true;
        return false;
    }
}

public class Solution {
    public bool IsUgly(int num) {
        if (num <= 0) return false;
        while (num != 1) {
            if (num % 2 == 0) num /= 2;
            else if (num % 3 == 0) num /= 3;
            else if (num % 5 == 0) num /= 5;
            else return false;
        }
        return true;
    }
}

public class Solution {
    public bool IsUgly(int num) {
        if(num<=0) return false;
        else if(num==1) return true;
        else if(num%2==0){
            return IsUgly(num/2);
        }
        else if(num%3==0){
            return IsUgly(num/3);
        }
        else if(num%5==0){
            return IsUgly(num/5);
        }
        else {
            return false;
        }
    }
}



*/
