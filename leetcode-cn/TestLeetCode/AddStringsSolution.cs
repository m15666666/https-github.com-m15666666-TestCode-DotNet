using System.Text;

/*
给定两个字符串形式的非负整数 num1 和num2 ，计算它们的和。

 

提示：

num1 和num2 的长度都小于 5100
num1 和num2 都只包含数字 0-9
num1 和num2 都不包含任何前导零
你不能使用任何內建 BigInteger 库， 也不能直接将输入的字符串转换为整数形式

*/

/// <summary>
/// https://leetcode-cn.com/problems/add-strings/
/// 415. 字符串相加
///
///
/// </summary>
internal class AddStringsSolution
{
    public string AddStrings(string num1, string num2)
    {
        int i = num1.Length - 1, j = num2.Length - 1, add = 0;
        StringBuilder ret = new StringBuilder();
        while (i >= 0 || j >= 0 || add != 0)
        {
            int x = -1 < i ? num1[i] - '0' : 0;
            int y = -1 < j ? num2[j] - '0' : 0;
            int sum = x + y + add;
            ret.Insert(0, sum % 10);
            add = sum / 10;
            i--;
            j--;
        }
        return ret.ToString();
    }
}

/*

字符串相加
力扣官方题解
发布于 2020-08-02
37.4k
方法一：模拟
思路与算法

本题我们只需要对两个大整数模拟「竖式加法」的过程。竖式加法就是我们平常学习生活中常用的对两个整数相加的方法，回想一下我们在纸上对两个整数相加的操作，是不是如下图将相同数位对齐，从低到高逐位相加，如果当前位和超过 1010，则向高位进一位？因此我们只要将这个过程用代码写出来即可。

fig1

具体实现也不复杂，我们定义两个指针 ii 和 jj 分别指向 \textit{num}_1num 
1
​	
  和 \textit{num}_2num 
2
​	
  的末尾，即最低位，同时定义一个变量 \textit{add}add 维护当前是否有进位，然后从末尾到开头逐位相加即可。你可能会想两个数字位数不同怎么处理，这里我们统一在指针当前下标处于负数的时候返回 00，等价于对位数较短的数字进行了补零操作，这样就可以除去两个数字位数不同情况的处理，具体可以看下面的代码。


class Solution {
    public String addStrings(String num1, String num2) {
        int i = num1.length() - 1, j = num2.length() - 1, add = 0;
        StringBuffer ans = new StringBuffer();
        while (i >= 0 || j >= 0 || add != 0) {
            int x = i >= 0 ? num1.charAt(i) - '0' : 0;
            int y = j >= 0 ? num2.charAt(j) - '0' : 0;
            int result = x + y + add;
            ans.append(result % 10);
            add = result / 10;
            i--;
            j--;
        }
        // 计算完以后的答案需要翻转过来
        ans.reverse();
        return ans.toString();
    }
}
复杂度分析

时间复杂度：O(\max(\textit{len}_1,\textit{len}_2))O(max(len 
1
​	
 ,len 
2
​	
 ))，其中 \textit{len}_1=\textit{num}_1.\text{length}len 
1
​	
 =num 
1
​	
 .length，\textit{len}_2=\textit{num}_2.\text{length}len 
2
​	
 =num 
2
​	
 .length。竖式加法的次数取决于较大数的位数。
空间复杂度：O(1)O(1)。除答案外我们只需要常数空间存放若干变量。在 Java 解法中使用到了 StringBuffer，故 Java 解法的空间复杂度为 O(n)O(n)。


class Solution {
    public String addStrings(String num1, String num2) {
        StringBuilder res = new StringBuilder("");
        int i = num1.length() - 1, j = num2.length() - 1, carry = 0;
        while(i >= 0 || j >= 0){
            int n1 = i >= 0 ? num1.charAt(i) - '0' : 0;
            int n2 = j >= 0 ? num2.charAt(j) - '0' : 0;
            int tmp = n1 + n2 + carry;
            carry = tmp / 10;
            res.append(tmp % 10);
            i--; j--;
        }
        if(carry == 1) res.append(1);
        return res.reverse().toString();
    }
}

代码：
Python

class Solution:
    def addStrings(self, num1: str, num2: str) -> str:
        res = ""
        i, j, carry = len(num1) - 1, len(num2) - 1, 0
        while i >= 0 or j >= 0:
            n1 = int(num1[i]) if i >= 0 else 0
            n2 = int(num2[j]) if j >= 0 else 0
            tmp = n1 + n2 + carry
            carry = tmp // 10
            res = str(tmp % 10) + res
            i, j = i - 1, j - 1
        return "1" + res if carry else res

public class Solution {
    public string AddStrings(string num1, string num2) {
  
            int intLenA = num1 == null ? 0 : num1.Length-1;
            int intLenB = num2 == null ? 0 : num2.Length-1;
            int intCarry = 0;
            Stack<int> tempList = new Stack<int>();
            StringBuilder outList = new StringBuilder();
            while(intLenA>=0||intLenB>=0|| intCarry>0)
            {
                int Sum = (intLenA>=0?(num1[intLenA] - '0'):0)+ (intLenB >= 0 ? (num2[intLenB] - '0'):0)+ intCarry;
                intCarry = Sum / 10;
                tempList.Push(Sum%10);
                intLenA--;
                intLenB--;

            }
            while(tempList.Count>0)
            {
                outList.Append(tempList.Pop());
            }

            return outList.ToString();

    }
}

public class Solution {
    public string AddStrings(string num1, string num2) {
        List<int> l=new List<int>();
        int c=0;
        Action<int,int> f=(a,b)=>{
            a+=b+c;
            c=a/10;
            l.Add(a%10);
        };
        
        int i=num1.Length-1,j=num2.Length-1;
        while(-1<i&&-1<j){
            f(num1[i]-'0',num2[j]-'0');
            --i;
            --j;
        }
        while(-1<i){
            f(num1[i]-'0',0);
            --i;
        }
        while(-1<j){
            f(0,num2[j]-'0');
            --j;
        }
        
        if(0<c){
            l.Add(c);
        }

        StringBuilder x=new StringBuilder();
        i=l.Count;
        while(-1<--i){
            x.Append(l[i]);
        }

        return x.ToString();
    }
}


*/