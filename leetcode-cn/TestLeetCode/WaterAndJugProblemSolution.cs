using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
有两个容量分别为 x升 和 y升 的水壶以及无限多的水。请判断能否通过使用这两个水壶，从而可以得到恰好 z升 的水？

如果可以，最后请用以上水壶中的一或两个来盛放取得的 z升 水。

你允许：

装满任意一个水壶
清空任意一个水壶
从一个水壶向另外一个水壶倒水，直到装满或者倒空
示例 1: (From the famous "Die Hard" example)

输入: x = 3, y = 5, z = 4
输出: True
示例 2:

输入: x = 2, y = 6, z = 5
输出: False 
*/
/// <summary>
/// https://leetcode-cn.com/problems/water-and-jug-problem/
/// 365. 水壶问题
/// https://www.cnblogs.com/grandyang/p/5628836.html
/// </summary>
class WaterAndJugProblemSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public bool CanMeasureWater(int x, int y, int z)
    {
        return z == 0 || (x + y >= z && z % Gcd(x, y) == 0);
    }

    int Gcd(int x, int y)
    {
        return y == 0 ? x : Gcd( y, x % y );
    }
}
/*
public class Solution
{
    public bool CanMeasureWater (int x, int y, int z)
    {
        if(z==0) return true;
        if(z>(x+y)) return false;
        while(x!=y){
            if(x<y){
                int t=x;
                x=y;
                y=t;
            }
            if(y==0) break;
            x=x-y;
        }
        if(x==0) return false;
        return z%x==0;
    }
}
public class Solution {
    public bool CanMeasureWater(int x, int y, int z)
    {
        if(z==0)
            return true;
        if(x==0)
            return z==y;
        if(y==0)
            return z==x;
        int m = Math.Max(x, y);
        int n = Math.Min(x, y);
        if(z>m+n)
            return false;
        while(m%n!=0){
            int rem=m%n;
            m=n;
            n=rem;
        }
        return z%n==0;
    }
}
public class Solution {
    public bool CanMeasureWater(int x, int y, int z) {

        if(((x==0&&y!=z)||(y==0&&x!=z))&&z!=0)
            return false;
        if(z>x+y)
            return false;
        if((x>z&&y>z&&Math.Abs(x-y)>z&&z>0)&&!(x%2==1&&y%2==1&&z%2==1))
            return false;

        if((x==2&&y==6&&z==5)||(x==6&&y==9&&z==1))
            return false;
        return true;
    }
}

*/
