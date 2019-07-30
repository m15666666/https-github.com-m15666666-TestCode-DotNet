using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个非负整数，你至多可以交换一次数字中的任意两位。返回你能得到的最大值。

示例 1 :

输入: 2736
输出: 7236
解释: 交换数字2和数字7。
示例 2 :

输入: 9973
输出: 9973
解释: 不需要交换。
注意:

给定数字的范围是 [0, 108] 
*/
/// <summary>
/// https://leetcode-cn.com/problems/maximum-swap/
/// 670. 最大交换
/// http://www.dongcoder.com/detail-1150097.html
/// https://blog.csdn.net/ch1209498273/article/details/81638937
/// </summary>
class MaximumSwapSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int MaximumSwap(int num)
    {
        var s = num.ToString();
        // 数字最后出现的下标，尽量使用最后出现的最大的数字替换最前面的较小的数字，使结果最大
        int[] lastIndex = new int[10];
        for (int i = 0; i < s.Length; i++) lastIndex[s[i] - '0'] = i;

        for (int i = 0; i < s.Length; i++)
        {
            var n = s[i] - '0';
            for (int d = 9; n < d; d--)
            {
                if ( i < lastIndex[d] ) // 下标必须当前数字i的后面
                {
                    var otherIndex = lastIndex[d];
                    StringBuilder ret = new StringBuilder(s);
                    ret[i] = s[otherIndex];
                    ret[otherIndex] = s[i];
                    return int.Parse(ret.ToString());
                }
            }
        }
        return num;
    }
}
/*
public class Solution {
    public int MaximumSwap(int num) {
        string str=num.ToString();
        int []sum=new int[10];
        char []data=new char[str.Length];
        for(int i=0;i<str.Length;i++){
            sum[str[i]-'0']++;
            data[i]=str[i];
        }
        int sum2=0;
        int weizhi=-1;
        char jiaohuan='0';
        int j=9;
        for(int i=0;i<str.Length;i++){
            while(sum2<=i){
                if(j<0){
                    return num;
                }else{
                    sum2+=sum[j];
                    j--;
                }
            }
            if(sum2>i&&(j+1+'0')>str[i]){
                jiaohuan=(char)(j+1+'0');
                weizhi=i;
                break;
            }
        }
        if(weizhi==-1)
            return num;
        char temp=data[weizhi];
        data[weizhi]=jiaohuan;
        for(int i=str.Length-1;i>=0;i--){
            if(str[i]==jiaohuan){
                data[i]=temp;
                break;
            }
        }
        string str2="";
        for(int i=0;i<str.Length;i++){
            str2+=data[i];
        }
        int jieguo=int.Parse(str2);
        return jieguo;
    }
} 
*/
