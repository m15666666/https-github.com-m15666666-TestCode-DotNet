using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
给定正整数 n，找到若干个完全平方数（比如 1, 4, 9, 16, ...）
使得它们的和等于 n。你需要让组成和的完全平方数的个数最少。

示例 1:
输入: n = 12
输出: 3 
解释: 12 = 4 + 4 + 4.

示例 2:
输入: n = 13
输出: 2
解释: 13 = 4 + 9. 
     
*/

/// <summary>
/// https://leetcode-cn.com/problems/perfect-squares/
/// 279. 完全平方数
/// https://blog.csdn.net/qq_17550379/article/details/80875782
/// </summary>
class PerfectSquaresSolution
{
    public static void Test()
    {
        var ret = NumSquares(12);
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public static int NumSquares(int n)
    {
        if (n < 2) return 1;

        int[] nums = new int[n + 1];
        int num = 0;
        int squareNum = 0;
        for (int startIndex = 1; startIndex <= n; startIndex++)
        {
            num = startIndex;
            squareNum = 1;
            while (true)
            {
                int i = startIndex - squareNum * squareNum;
                if( i == 0)
                {
                    num = 1;
                    break;
                }
                if (i < 0) break;

                ++squareNum;
                num = Math.Min(num, nums[i] + 1);
            }
            nums[startIndex] = num;
        }
        return num;
    }
}
/*
//别人的算法
public class Solution {
    public int NumSquares(int n) {
            while (n % 4 == 0) n /= 4;
            if (n % 8 == 7) return 4;
            for (int i = 0; i * i <= n; i++)
            {
                int j = (int)Math.Sqrt(n - i * i);
                if (i * i + j * j == n)
                {
                    if (i > 0 && j > 0) return 2;
                    else return 1;
                }
            }
            return 3;
    }
}
public class Solution
{
    private class Pair
    {
        public int num;
        public int step;

        public Pair(int num, int step)
        {
            this.num = num;
            this.step = step;
        }
    }
    public int NumSquares(int n)
    {
        if (n <= 0)
            return -1;

        Queue<Pair> q = new Queue<Pair>();
        q.Enqueue(new Pair(n, 0));

        bool[] visited = new bool[n + 1];
        visited[n] = true;

        while (q.Count != 0)
        {
            Pair pair = q.Dequeue();
            int num = pair.num;
            int step = pair.step;

            for (int i = 0; ; i++)
            {
                int a = num - i * i;

                if (a < 0)
                    break;

                if (a == 0)
                    return step + 1;

                if (!visited[a])
                {
                    q.Enqueue(new Pair(a, step + 1));
                    visited[a] = true;
                }
            }
        }

        throw new ArgumentException("No Solution.");
    }
}     
public class Solution {
    public int NumSquares(int n) {
            Trace.Assert ( n > 0 );
            Queue<KeyValuePair<int,int>>queue = new Queue<KeyValuePair<int, int>>();
            queue.Enqueue ( new KeyValuePair<int,int> ( n,0 ) );
            
            bool[] visited = Enumerable.Repeat(false,n+1).ToArray();
            while ( queue.Count () != 0 ) {
                KeyValuePair<int,int> res = queue.Dequeue();
                int num = res.Key;
                int step = res.Value;
                
                for ( int i = 0 ;;i++ ) {
                    int a = num-i*i;
                    if(a<0){
                        break;
                    }
                    if(a==0){
                        return step+1;
                    }
                    if ( !visited[a] ) {
                        queue.Enqueue ( new KeyValuePair<int,int> ( ( a ),step + 1 ) );
                        visited[a] = true;
                    }
                }
            }
            throw new Exception ( "no result" );
    }
}     
public class Solution {
    public int NumSquares(int n) {
        List<int> list = new List<int>();
            int num = 1;
            list.Add(num);
            while (num*num <= n) 
            {
                num += 1;
                list.Add(num * num);
            }
            int[] arr = list.ToArray(), res = new int[n+1];
            for (int i = 1; i <= n; i++)
            {
                res[i] = i;
                foreach (int e in arr)
                {
                    if (e > i)
                        break;
                    res[i] = Math.Min(res[i],res[i-e]+1);
                }
            }
            return res[n];
    }
}
public class Solution {
    public int NumSquares(int n) {
        //dp[i]存储组成i需要的完全平方数的个数
        int[] dp = new int[n + 1];
        
        //初始化dp
        for(int i = 0; i <= n; ++i) {
            dp[i] = Int32.MaxValue;
        }
        
        dp[0] = 0;
        for(int i = 0; i <= n; ++i) {
            for(int j = 1; i + j * j <= n; ++j) {
                dp[i + j * j] = Math.Min(dp[i] + 1, dp[i + j * j]);
            }
        }
        
        return dp[n];
    }
}
public class Solution {
    public int NumSquares(int n) {
        if (n == 1)
            return 1;

        //数据从 1 开始
        //用 arr[i] 数组存储第 i 个数的完美平方数
        //所有的完美平方数都可以看做一个普通数加上一个完美平方数
        //认为 i 的完全平方数是从和为 i 的两个完全平方数 arr[j] 和 arr[i-j]之和，然后从中取最小。
        // arr[i] = Math.Min(arr[j] + arr[i - j], arr[i])
        // arr[i + j * j] = arr[i] + arr[j * j];
        // arr[i + j * j] = arr[i] + 1;
        // arr[i + j * j] = Math.Min(arr[i] + 1, arr[i + j * j]) 可以组成i的最小个数
        var arr = new int[n + 1];

        //初始化数据信息
        for (int i = 1; i <= n; i++)
        {
            //所有的平方数都只需自己就可以组成自己，所以是1，其他初始化为无穷大
            if(arr[i] != 1)
                arr[i] = int.MaxValue;

            if (i * i <= n)
                arr[i * i] = 1;
        }

        for (int i = 1; i <= n; i++)
        {
            for (int j = 1; i + j * j <= n; j++)
            {
                //从小到大查找
                //i 表示一个普通数，j 表示一个平方数的根
                //j * j 的平方数为1
                arr[i + j * j] = Math.Min(arr[i] + 1, arr[i + j * j]);
            }
        }

        return arr[n];
    }
}
public class Solution {
    public int NumSquares(int n) {
        if (n == 0) return 1;
            if (n == 1) return 1;
            if (n == 2) return 2;
            int[]dp=new int[n+1];
            dp[0] = 0;
            dp[1] = 1;
            dp[2] = 2;
            for (int i = 3; i <= n; i++)
            {
                int temp = (int)(Math.Sqrt(i));
                int currentValue = int.MaxValue; ;
                for (int j = temp; j >= 1; j--)
                {
                    currentValue = Math.Min(currentValue,dp[i-j*j]+1);
                }
                dp[i] = currentValue;
            }
            return dp[n];
    }
}

     
     
*/
