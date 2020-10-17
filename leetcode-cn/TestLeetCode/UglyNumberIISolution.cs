using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
编写一个程序，找出第 n 个丑数。

丑数就是只包含质因数 2, 3, 5 的正整数。

示例:

输入: n = 10
输出: 12
解释: 1, 2, 3, 4, 5, 6, 8, 9, 10, 12 是前 10 个丑数。
说明:  

1 是丑数。
n 不超过1690。
*/
/// <summary>
/// https://leetcode-cn.com/problems/ugly-number-ii/
/// 264. 丑数 II
/// https://blog.csdn.net/qq_34364995/article/details/80680354
/// </summary>
class UglyNumberIISolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int NthUglyNumber(int n)
    {
        if (n < 1) return 0;

        int[] results = new int[n];
        int index = 0;
        var lastUgly = 1;
        results[index++] = 1;
        int index2 = 0, index3 = 0, index5 = 0;
        while( index < n)
        {
            var v2 = results[index2] * 2;
            var v3 = results[index3] * 3;
            var v5 = results[index5] * 5;

            lastUgly = Math.Min( Math.Min( v2, v3 ), v5 );
            results[index++] = lastUgly;

            if (v2 == lastUgly) ++index2;
            if (v3 == lastUgly) ++index3;
            if (v5 == lastUgly) ++index5;
        }
        return lastUgly;
    }
}
/*
丑数 II
力扣 (LeetCode)
发布于 2020-02-24
24.6k
两级优化
假设我们以某种方式计算了第 n 个丑数，我们将这个解直接放到 Solution 类中的 nthUglyNumber 方法中。

让我们看一下上下文：有 596 个测试用例，其中大部分 n 是大于 50 小于 1691 的。

因此我们不必计算 596 \times 50 = 29800596×50=29800 的丑数，而是可以预计算 1690 个丑数，这样可以显著的加快提交速度。

如何预计算？使用另一个 Ugly 类在构造函数中完成所有丑数的预计算，然后声明一个 Ugly 类的实例对象，将该实例对象声明为 Solution 类的静态变量。

现在让我们来讨论两种不同的预计算方法。

方法一：堆
我们从堆中包含一个数字开始：1，去计算下一个丑数。将 1 从堆中弹出然后将三个数字添加到堆中：1 \times 21×2, 1 \times 31×3，和 1 \times 51×5。

现在堆中最小的数字是 2。为了计算下一个丑数，要将 2 从堆中弹出然后添加三个数字：2 \times 22×2, 2 \times 32×3，和 2 \times 52×5。

重复该步骤计算所有丑数。在每个步骤中，弹出堆中最小的丑数 kk，并在堆中添加三个丑数：k \times 2k×2, k \times 3k×3，和 k \times 5k×5。



算法：

预计算 1690 个丑数：
初始化预计算用到的数组 nums，堆 heap 和哈希表 seen 跟踪在堆中出现过的元素，避免重复。
循环计算丑数，每个步骤：
弹出堆中最小的数字 k 并添加到数组 nums 中。
若 2k，3k，5k 不存在在哈希表中，则将其添加到栈中并更新哈希表。
返回在数组中预先计算好的丑数。

class Ugly {
  public int[] nums = new int[1690];
  Ugly() {
    HashSet<Long> seen = new HashSet();
    PriorityQueue<Long> heap = new PriorityQueue<Long>();
    seen.add(1L);
    heap.add(1L);

    long currUgly, newUgly;
    int[] primes = new int[]{2, 3, 5};
    for(int i = 0; i < 1690; ++i) {
      currUgly = heap.poll();
      nums[i] = (int)currUgly;
      for(int j : primes) {
        newUgly = currUgly * j;
        if (!seen.contains(newUgly)) {
          seen.add(newUgly);
          heap.add(newUgly);
        }
      }
    }
  }
}

class Solution {
  public static Ugly u = new Ugly();
  public int nthUglyNumber(int n) {
    return u.nums[n - 1];
  }
}
复杂度分析

时间复杂度：\mathcal{O}(1)O(1) 的时间检索答案。和超过 12 \times 10^612×10 
6
  次预计算操作。我们估计一下预计算所需的操作次数。For 循环有 1690 步，且每一次循环执行一次 pop 操作和三次 push 操作和三次哈希表的 contains / in 操作。pop 和 push 操作具有对数时间复杂度因此比线性搜索更廉价，所以我们只估算 contains / in 带来的成本。该等差数列很容易估计：
1 + 2 + 3 + ... + 1690 \times 3 = \frac{(1 + 1690 \times 3) \times 1690 \times 3}{2} > 4.5 \times 1690^2
1+2+3+...+1690×3= 
2
(1+1690×3)×1690×3
​	
 >4.5×1690 
2
 

空间复杂度：常数空间存储 1690 个丑数，和堆中不超过 1690 \times 21690×2 的元素和哈希表中不超过 1690 \times 31690×3 的元素。
方法二： 动态规划
方法一中的预计算操作较为繁琐，可以通过动态规划优化。

让我们从数组中只包含一个丑数数字 1 开始，使用三个指针 i_2i 
2
​	
 , i_3i 
3
​	
  和 i_5i 
5
​	
 ，标记所指向丑数要乘以的因子。

算法很简单：在 2 \times \textrm{nums}[i_2]2×nums[i 
2
​	
 ]，3 \times \textrm{nums}[i_3]3×nums[i 
3
​	
 ] 和 5 \times \textrm{nums}[i_5]5×nums[i 
5
​	
 ] 选出最小的丑数并添加到数组中。并将该丑数对应的因子指针往前走一步。重复该步骤直到计算完 1690 个丑数。



算法：

预计算 1690 个丑数：
初始化数组 nums 和三个指针 i2，i3，i5 。
循环计算所有丑数。每一步：
在 nums[i2] * 2，nums[i3] * 3 和 nums[i5] * 5 选出最小的数字添加到数组 nums 中。
将该数字对应的因子指针向前移动一步。
在数组中返回所需的丑数。

class Ugly {
  public int[] nums = new int[1690];
  Ugly() {
    nums[0] = 1;
    int ugly, i2 = 0, i3 = 0, i5 = 0;

    for(int i = 1; i < 1690; ++i) {
      ugly = Math.min(Math.min(nums[i2] * 2, nums[i3] * 3), nums[i5] * 5);
      nums[i] = ugly;

      if (ugly == nums[i2] * 2) ++i2;
      if (ugly == nums[i3] * 3) ++i3;
      if (ugly == nums[i5] * 5) ++i5;
    }
  }
}

class Solution {
  public static Ugly u = new Ugly();
  public int nthUglyNumber(int n) {
    return u.nums[n - 1];
  }
}
复杂度分析

时间复杂度：\mathcal{O}(1)O(1) 时间检索答案和大约 1690 \times 5 = 84501690×5=8450 次的预计算操作。
空间复杂度：常数空间用保存 1690 个丑数。

public class Solution {
    public int NthUglyNumber(int n) {
       int[] ugly = new int[n];
            int n2 = 1, i2 = 0;
            int n3 = 1, i3 = 0;
            int n5 = 1, i5 = 0;
            for (int i = 0; i < n; i++)
            {
                ugly[i] = Math.Min(Math.Min(n2, n3), n5);
                if (ugly[i] == n2) n2 = ugly[i2++] * 2;
                if (ugly[i] == n3) n3 = ugly[i3++] * 3;
                if (ugly[i] == n5) n5 = ugly[i5++] * 5;
            }
            return ugly[n - 1];
    }
}
public class Solution {
    public int NthUglyNumber(int n) {
        int[] num=new int[n];
        num[0]=1;
        int index2=0,index3=0,index5=0;
        for(int i=1;i<n;i++){
            num[i]=min(num[index2]*2,num[index3]*3,num[index5]*5);
            if(num[i]==num[index2]*2) index2++;
            if(num[i]==num[index3]*3) index3++;
            if(num[i]==num[index5]*5) index5++;
        }
        return num[n-1];
        
    }
    
    public int min(int a,int b,int c){
        return a>b?(b>c?c:b):(a>c?c:a);
    }
}
*/
