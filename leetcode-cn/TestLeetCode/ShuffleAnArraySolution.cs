using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
打乱一个没有重复元素的数组。

示例:

// 以数字集合 1, 2 和 3 初始化数组。
int[] nums = {1,2,3};
Solution solution = new Solution(nums);

// 打乱数组 [1,2,3] 并返回结果。任何 [1,2,3]的排列返回的概率应该相同。
solution.shuffle();

// 重设数组到它的初始状态[1,2,3]。
solution.reset();

// 随机返回数组[1,2,3]打乱后的结果。
solution.shuffle(); 
*/
/// <summary>
/// https://leetcode-cn.com/problems/shuffle-an-array/
/// 384. 打乱数组
/// https://blog.csdn.net/zrh_CSDN/article/details/83961002
/// </summary>
class ShuffleAnArraySolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public ShuffleAnArraySolution(int[] nums)
    {
        _originArray = nums;
        _array = (int[])_originArray.Clone();
    }

    private int[] _array = null;
    private int[] _originArray = null;
    private Random _random = new Random();

    public int[] Reset()
    {
        _array = (int[])_originArray.Clone();
        return _array;
    }

    public int[] Shuffle()
    {
        int[] ret = _array;// new int[_originArray.Length];
        //_originArray.CopyTo(ret, 0);
        int temp;
        for( int i = 0; i < ret.Length; i++ )
        {
            int target = _random.Next(ret.Length - i) + i;
            if (i == target) continue;
            temp = ret[i];
            ret[i] = ret[target];
            ret[target] = temp;
        }

        return ret;
    }
}
/*
打乱数组
力扣 (LeetCode)

发布于 2019-07-01
24.8k
绪章
一般来说，对于一个问题我通常会给出两种以上的解法，但是对于洗牌问题，Fisher-Yates 洗牌算法即是通俗解法，同时也是渐进最优的解法。

在我们开始之前需要了解一些关于随机化的知识 - 下面介绍的两个方法都假设编程语言中提供的伪随机数生成器是足够随机的。我们给出的示例代码也都采用了最简单的方法来得到伪随机数，但为了让数组的每个排列出现的可能性尽可能相等，还是有一些其他东西需要注意的。例如，一个长度为 nn 的数组有 n!n! 个不同的排列组合。因此，为了能将所有的排列在整数空间编码，我们需要 \lceil lg(n!)\rceil⌈lg(n!)⌉ 比特，这是默认的伪随机数不能保证的。

方法一： 暴力 【通过】
思路

假设我们把每个数都放在一个 ”帽子“ 里面，然后我们从帽子里面把它们一个个摸出来，摸出来的数按顺序放入数组，这个数组正好就是我们要的洗牌后的数组。

算法

暴力算法简单的来说就是把每个数放在一个 ”帽子“ 里面，每次从 ”帽子“ 里面随机摸一个数出来，直到 “帽子” 为空。下面是具体操作，首先我们把数组 array 复制一份给数组 aux，之后每次随机从 aux 中取一个数，为了防止数被重复取出，每次取完就把这个数从 aux 中移除。重置 的实现方式很简单，只需把 array 恢复称最开始的状态就可以了。

这个算法的正确性在于，每次 for 循环中，任何一个元素都会以等可能的概率被选中。为了证明这一点，我们可以算出来，一个特定的元素 ee 在第 kk 轮被选中的概率为 PP(ee 在第 kk 轮被选中) \cdot⋅ PP(ee 在前 kk 轮不被选中)。假设洗牌的数组有 nn 个元素，这个概率公式如下所示：

\frac{1}{n-k} \cdot \prod_{i=1}^{k} \frac{n-i}{n-i+1}
n−k
1
​	
 ⋅ 
i=1
∏
k
​	
  
n−i+1
n−i
​	
 

把这个式子展开一下是这样的：

(\frac{n-1}{n} \cdot \frac{n-2}{n-1} \cdot (\ldots) \cdot \frac{n-k+1}{n-k+2} \cdot \frac{n-k}{n-k+1}) \cdot \frac{1}{n-k}
( 
n
n−1
​	
 ⋅ 
n−1
n−2
​	
 ⋅(…)⋅ 
n−k+2
n−k+1
​	
 ⋅ 
n−k+1
n−k
​	
 )⋅ 
n−k
1
​	
 

在 k = 0k=0 的情况下，很显然 \frac{1}{n-k} = \frac{1}{n} 
n−k
1
​	
 = 
n
1
​	
 。 对于 k > 0k>0 的情况，前一个式子的分子正好能把下一个式子的分母约去，到最后也只有第一个式子分母还在。因此，不管是哪一轮摸到了哪一个数，概率都是 \frac{1}{n} 
n
1
​	
 ，所以这个数组的每个排列组合都是等概率的。


class Solution {
    private int[] array;
    private int[] original;

    private Random rand = new Random();

    private List<Integer> getArrayCopy() {
        List<Integer> asList = new ArrayList<Integer>();
        for (int i = 0; i < array.length; i++) {
            asList.add(array[i]);
        }
        return asList;
    }

    public Solution(int[] nums) {
        array = nums;
        original = nums.clone();
    }
    
    public int[] reset() {
        array = original;
        original = original.clone();
        return array;
    }
    
    public int[] shuffle() {
        List<Integer> aux = getArrayCopy();

        for (int i = 0; i < array.length; i++) {
            int removeIdx = rand.nextInt(aux.size());
            array[i] = aux.get(removeIdx);
            aux.remove(removeIdx);
        }

        return array;
    }
}
复杂度分析

时间复杂度： O(n^2)O(n 
2
 )
乘方时间复杂度来自于 list.remove（list.pop）。每次操作都是线性时间的，总共发生 nn 次。

空间复杂度： O(n)O(n)
因为需要实现 重置 方法，需要额外的空间把原始数组另存一份，在重置的时候用来恢复原始状态。

方法二： Fisher-Yates 洗牌算法 【通过】
思路

我们可以用一个简单的技巧来降低之前算法的时间复杂度和空间复杂度，那就是让数组中的元素互相交换，这样就可以避免掉每次迭代中用于修改列表的时间了。

算法

Fisher-Yates 洗牌算法跟暴力算法很像。在每次迭代中，生成一个范围在当前下标到数组末尾元素下标之间的随机整数。接下来，将当前元素和随机选出的下标所指的元素互相交换 - 这一步模拟了每次从 “帽子” 里面摸一个元素的过程，其中选取下标范围的依据在于每个被摸出的元素都不可能再被摸出来了。此外还有一个需要注意的细节，当前元素是可以和它本身互相交换的 - 否则生成最后的排列组合的概率就不对了。为了更清楚地理解这一过程，可以看下面这些动画：




class Solution {
    private int[] array;
    private int[] original;

    Random rand = new Random();

    private int randRange(int min, int max) {
        return rand.nextInt(max - min) + min;
    }

    private void swapAt(int i, int j) {
        int temp = array[i];
        array[i] = array[j];
        array[j] = temp;
    }

    public Solution(int[] nums) {
        array = nums;
        original = nums.clone();
    }
    
    public int[] reset() {
        array = original;
        original = original.clone();
        return original;
    }
    
    public int[] shuffle() {
        for (int i = 0; i < array.length; i++) {
            swapAt(i, randRange(i, array.length));
        }
        return array;
    }
}
复杂度分析

时间复杂度 ： O(n)O(n)
Fisher-Yates 洗牌算法时间复杂度是线性的，因为算法中生成随机序列，交换两个元素这两种操作都是常数时间复杂度的。

空间复杂度： O(n)O(n)
因为要实现 重置 功能，原始数组必须得保存一份，因此空间复杂度并没有优化。

public class Solution
{
    int [] _original;
    int _len;
    int [] _ToChange;
    Random rd;
    public Solution(int[] nums)
    {
        _len=nums.Length;
        _original=nums;
        _ToChange=new int[_len];
        rd=new Random();
        Array.Copy(nums,_ToChange,_len);
    }

    public int[] Reset()
    {

    return _original;
    }

    public int[] Shuffle()
    {
        int temp = 0;
        for (int i = 0; i < _len; i++)
        {
        int local = rd.Next(_len);
        temp = _ToChange[local];
        _ToChange[local] = _ToChange[i];
        _ToChange[i] = temp;
        }
        return _ToChange;
    }
}
*/