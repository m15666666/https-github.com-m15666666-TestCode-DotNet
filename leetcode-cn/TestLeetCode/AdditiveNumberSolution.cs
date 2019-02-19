using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
累加数是一个字符串，组成它的数字可以形成累加序列。

一个有效的累加序列必须至少包含 3 个数。除了最开始的两个数以外，字符串中的其他数都等于它之前两个数相加的和。

给定一个只包含数字 '0'-'9' 的字符串，编写一个算法来判断给定输入是否是累加数。

说明: 累加序列里的数不会以 0 开头，所以不会出现 1, 2, 03 或者 1, 02, 3 的情况。

示例 1:

输入: "112358"
输出: true 
解释: 累加序列为: 1, 1, 2, 3, 5, 8 。1 + 1 = 2, 1 + 2 = 3, 2 + 3 = 5, 3 + 5 = 8
示例 2:

输入: "199100199"
输出: true 
解释: 累加序列为: 1, 99, 100, 199。1 + 99 = 100, 99 + 100 = 199
进阶:
你如何处理一个溢出的过大的整数输入? 
*/
/// <summary>
/// https://leetcode-cn.com/problems/additive-number/
/// 306. 累加数
/// https://www.cnblogs.com/kexinxin/p/10235130.html
/// https://www.cnblogs.com/MrSaver/p/9974299.html
/// </summary>
class AdditiveNumberSolution
{
    public static void Test()
    {
        var ret = IsAdditiveNumber("199100199");
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public static bool IsAdditiveNumber(string num)
    {
        if (string.IsNullOrWhiteSpace(num)) return false;
        var len = num.Length;
        var firstChar = num[0];
        
        for (int num1size = 1; num1size <= len; num1size++)
        {
            if (firstChar == Zero && 1 < num1size) break;
            var nn1 = GetNumber(num, 0, num1size);
            for (int num3Index = num1size + 1; num3Index < len; num3Index++)
            {
                var num2size = num3Index - num1size;
                if (num[num1size] == Zero && 1 < num2size) break;

                var leftCount = len - num1size - num2size;
                if (leftCount < num2size) break;

                var index = num3Index;

                var n1 = nn1;
                var n2 = GetNumber(num, num1size, num2size);
                var n3 = n1 + n2;
                var sizeofn3 = GetSize(n3);
                
                while ( sizeofn3 <= leftCount)
                {
                    if(GetNumber(num, index, sizeofn3) != n3) break;

                    leftCount -= sizeofn3;
                    if (leftCount == 0) return true;

                    index += sizeofn3;
                    n1 = n2;
                    n2 = n3;
                    n3 = n1 + n2;
                    sizeofn3 = GetSize(n3);
                }
            }
        }

        return false;
    }

    private const char Zero = '0';
    private static long GetNumber( string num, int index, int size)
    {
        long ret = 0;
        for (int i = index, count = 0; count < size; count++, i++) ret = ret * 10 + (num[i] - Zero);
        return ret;
    }
    private static int GetSize( long n )
    {
        int ret = 1;
        while( 9 < n )
        {
            n = n / 10;
            ++ret;
        }
        return ret;
    }
}
/*
//别人的算法
public class Solution {
    public  bool IsAdditiveNumber(string num)
    {
        return _find(new Stack<string>(), 0, num.ToArray());
    }
    private  bool _find(Stack<string> stack, int index, char[] arr)
    {
        if (index == arr.Length && stack.Count > 2)
        {
            var n1 = stack.Pop();
            var n2 = stack.Pop();
            var n3 = stack.Pop();
            if (_add(n2, n3, n1))
                return true;
            stack.Push(n3);
            stack.Push(n2);
            stack.Push(n1);
        }
        else
        {
            for (var i = index; i < arr.Length; i++)
            {
                if (arr[index] == '0' && i > index)
                    return false;
                var num = new string(arr, index, i - index + 1);
                if (stack.Count < 2)
                {
                    stack.Push(num);
                    if (_find(stack, i + 1, arr))
                        return true;
                    else
                        stack.Pop();
                }
                else
                {
                    var n1 = stack.Pop();
                    var n2 = stack.Pop();
                    stack.Push(n2);
                    stack.Push(n1);
                    if (_add(n1, n2, num))
                    {
                        stack.Push(num);
                        if (_find(stack, i + 1, arr))
                            return true;
                        else
                            stack.Pop();
                    }
                }
            }

        }
        return false;
    }
    private  bool _add(string a, string b, string c)
    {
        var index1 = a.Length - 1;
        var index2 = b.Length - 1;
           
        var flag = 0;
        var result = new char[Math.Max(a.Length, b.Length) + 1];
        var index = result.Length - 1;
        while (index1 >= 0 || index2 >= 0)
        {
            var n1 = index1 >= 0 ? a[index1] - '0' : 0;
            var n2 = index2 >= 0 ? b[index2] - '0' : 0;
            var n3 = n1 + n2 + flag;
            if (n3 > 9)
            {
                flag = 1;
                n3 -= 10;
            }
            else
                flag = 0;
            result[index--] = (char)(n3 + '0');
            --index1;
            --index2;
        }
        if (flag == 1)
        {
            result[0] = '1';
            return new string(result) == c;
        }
        else {
            return new string(result, 1, result.Length - 1) == c;
        }
    }
}
public class Solution {
    public bool IsAdditiveNumber(string num) {
        char[] numStr = num.ToCharArray();
        return ProcessAdditiveNumber(numStr, "", "", 0);
    }
    
    public bool ProcessAdditiveNumber(char[] numStr, string preNum1, string preNum2, int k)
    {
        StringBuilder sb = new StringBuilder();
        bool ret = false;
        for (int i = k; i < numStr.Length; i++)
        {
            sb.Append(numStr[i].ToString());
            var s = sb.ToString();
            if (s.StartsWith("0")&&s.Length>1)
            {
                return false;
            }
            if (preNum1 == "")
            {
                preNum1 = s;
                ret = ProcessAdditiveNumber(numStr, preNum1, preNum2, ++i);
                if (ret)
                {
                    return ret;
                }
                preNum1 = "";
                i--;
            }
            else if (preNum2 == "")
            {
                preNum2 = s;
                ret = ProcessAdditiveNumber(numStr, preNum1, preNum2, ++i);
                if (ret)
                {
                    return ret;
                }
                preNum2 = "";
                i--;
            }
            else
            {
                string _s = AddStrings(preNum1, preNum2);
                if (s.Length > _s.Length)
                {
                    return false;
                }
                if (s == _s)
                {
                    if (i == numStr.Length - 1)
                    {
                        return true;
                    }
                    string tempStr = preNum1;
                    preNum1 = preNum2;
                    preNum2 = s;
                    ret = ProcessAdditiveNumber(numStr, preNum1, preNum2, ++i);
                    if (ret)
                    {
                        return ret;
                    }
                    preNum2 = preNum1;
                    preNum1 = tempStr;
                    i--;
                }
            }
        }
        return ret;
    }
    
    public static string AddStrings(string num1, string num2)
    {
        int len = Math.Max(num1.Length, num2.Length);
        int[] arr = new int[++len];
        char[] c1 = num1.ToCharArray();
        char[] c2 = num2.ToCharArray();
        int k = 0;
        while (k < c1.Length)
        {
            arr[len - 1 - k] = int.Parse(c1[c1.Length - 1 - k].ToString());
            k++;
        }
        k = 0;
        while (k < c2.Length)
        {
            int n = arr[len - 1 - k] + int.Parse(c2[c2.Length - 1 - k].ToString());
            if (n >= 10)
            {
                int m = len - 2 - k;
                while (m >= 0)
                {
                    if (arr[m] + 1 >= 10)
                    {
                        arr[m] = (arr[m] + 1) % 10;
                    }
                    else
                    {
                        arr[m] += 1;
                        break;
                    }
                    m--;
                }
            }
            arr[len - 1 - k] = n % 10;
            k++;
        }
        var ret = string.Join("", arr);
        if (ret[0] == '0')
        {
            return ret.Substring(1);
        }
        return ret;
    }
}
*/
