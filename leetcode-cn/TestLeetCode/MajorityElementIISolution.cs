using System.Collections.Generic;

/*
给定一个大小为 n 的数组，找出其中所有出现超过 ⌊ n/3 ⌋ 次的元素。

说明: 要求算法的时间复杂度为 O(n)，空间复杂度为 O(1)。

示例 1:

输入: [3,2,3]
输出: [3]
示例 2:

输入: [1,1,1,3,3,2,2,2]
输出: [1,2]

*/

/// <summary>
/// https://leetcode-cn.com/problems/majority-element-ii/
/// 229. 求众数 II
/// 给定一个大小为 n 的数组，找出其中所有出现超过 ⌊ n/3 ⌋ 次的元素。
/// 说明: 要求算法的时间复杂度为 O(n)，空间复杂度为 O(1)。
/// 示例 1:
/// 输入: [3,2,3]
/// 输出: [3]
/// 示例 2:
/// 输入: [1,1,1,3,3,2,2,2]
/// 输出: [1,2]
/// </summary>
internal class MajorityElementIISolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    /// <summary>
    /// 摩尔投票法
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public IList<int> MajorityElement(int[] nums)
    {
        if (nums == null || nums.Length == 0) return new int[0];
        if (nums.Length == 1) return new int[] { nums[0] };

        var ret = new List<int>();
        int cand1 = nums[0], count1 = 0;
        int cand2 = cand1, count2 = 0;

        foreach (int num in nums)
        {
            if (cand1 == num)
            {
                count1++;
                continue;
            }
            if (cand2 == num)
            {
                count2++;
                continue;
            }

            if (count1 == 0)
            {
                cand1 = num;
                count1 = 1;
                continue;
            }
            if (count2 == 0)
            {
                cand2 = num;
                count2 = 1;
                continue;
            }

            count1--;
            count2--;
        }

        count1 = 0;
        count2 = 0;
        foreach (int num in nums)
            if (cand1 == num) count1++;
            else if (cand2 == num) count2++;

        int threshold = nums.Length / 3;
        if (threshold < count1) ret.Add(cand1);
        if (threshold < count2) ret.Add(cand2);

        return ret;
    }

    //public IList<int> MajorityElement(int[] nums)
    //{
    //    if (nums == null || nums.Length == 0) return new int[0];
    //    if (nums.Length == 1) return new int[] { nums[0] };

    //    Array.Sort(nums);

    //    int n = nums.Length;
    //    int threshold = n / 3 + 1;
    //    List<int> ret = new List<int>();
    //    int currentNum = nums[0];
    //    int count = 1;
    //    if (threshold < 2 )
    //    {
    //        for (int index = 1; index < nums.Length; index++)
    //        {
    //            if (1 == count) ret.Add(currentNum);

    //            var v = nums[index];
    //            if (v == currentNum)
    //            {
    //                ++count;
    //                continue;
    //            }
    //            currentNum = v;
    //            count = 1;
    //        }
    //        if (1 == count) ret.Add(currentNum);
    //    }
    //    else
    //    {
    //        for (int index = 1; index < nums.Length; index++)
    //        {
    //            var v = nums[index];
    //            if (v == currentNum)
    //            {
    //                ++count;
    //                if (threshold == count) ret.Add(currentNum);
    //                continue;
    //            }
    //            currentNum = v;
    //            count = 1;
    //        }
    //    }
    //    return ret;
    //}
}

/*

https://leetcode-cn.com/problems/majority-element-ii/solution/liang-fu-dong-hua-yan-shi-mo-er-tou-piao-fa-zui-zh/

两幅动画演示摩尔投票法，最直观的理解方式
我脱下短袖
发布于 2020-03-02
11.5k
解题思路
这道题用 map 方法去做很简单，但是题目描述要求要达到线性的时间复杂度，还有常量级的空间复杂度。

这个就变得有点难了，不过有更好的算法可以达到题目的要求——摩尔投票法。

我们看完题目描述之后，如果不知道摩尔投票法的算法原理，是很难想出来如何达到题目要求的。

所以，俺就先介绍摩尔投票法的原理，再配上动画。学完之后再做这道题，就会变得非常简单，编程起来速度也杠杠的。

摩尔投票法，解决的问题是如何在任意多的候选人中，选出票数超过一半的那个人。注意，是超出一半票数的那个人。

假设投票是这样的，[A, C, A, A, B]，ABC 是指三个候选人。

第一张票与第二张票进行对坑，如果票不同则互相抵消掉；

接着第三票与第四票进行对坑，如果票相同，则增加这个候选人的可抵消票数；

这个候选人拿着可抵消票数与第五张票对坑，如果票不同，则互相抵消掉，即候选人的可抵消票数 -1。

看下面动画，就可以理解最直观最清晰的答案了。

动画：摩尔投票法抵消阶段

看完上面的动画之后，相信已经理解摩尔投票法是如何选取一个最有希望的候选人的。

但这不意味着这个候选人的票数一定能超过一半，例如 [A, B, C] 的抵消阶段，最后得到的结果是 [C,1]，C 候选人的票数也未能超过一半的票数。

但是俺在这里发现了一个优化，如果最后得到的可抵消票数是 0 的话，那他已经无缘票数能超过一半的那个人了。因为本来可能有希望的，但是被后面的一张不同的票抵消掉了。所以，在这里可以直接返回结果，无需后面的计算了。

如果最后得到的抵消票数不为 0 的话，那说明他可能希望的，这是我们需要一个阶段来验证这个候选人的票数是否超过一半—— 计数阶段。

所以摩尔投票法分为两个阶段：抵消阶段和计数阶段。

抵消阶段：两个不同投票进行对坑，并且同时抵消掉各一张票，如果两个投票相同，则累加可抵消的次数；

计数阶段：在抵消阶段最后得到的抵消计数只要不为 0，那这个候选人是有可能超过一半的票数的，为了验证，则需要遍历一次，统计票数，才可确定。

摩尔投票法经历两个阶段最多消耗 O(2n)O(2n) 的时间，也属于 O(n)O(n) 的线性时间复杂度，另外空间复杂度也达到常量级。

理解摩尔投票法之后，我们再回到题目描述，题目可以看作是：在任意多的候选人中，选出票数超过⌊ 1/3 ⌋的候选人。

我们可以这样理解，假设投票是这样的 [A, B, C, A, A, B, C]，ABC 是指三个候选人。

第 1 张票，第 2 张票和第3张票进行对坑，如果票都不同，则互相抵消掉；

第 4 张票，第 5 张票和第 6 张票进行对坑，如果有部分相同，则累计增加他们的可抵消票数，如 [A, 2] 和 [B, 1]；

接着将 [A, 2] 和 [B, 1] 与第 7 张票进行对坑，如果票都没匹配上，则互相抵消掉，变成 [A, 1] 和 `[B, 0] 。

看下面动画，就知道什么回事了。

动画：摩尔投票法升级

看完动画之后，是不是理解了，是不是也清晰了。

然后按照这个思路来进行编程，后面会贴上自己写的Java和Golang代码，已加上注释。

但贴代码之前，俺要来一个归纳。

如果至多选一个代表，那他的票数至少要超过一半（⌊ 1/2 ⌋）的票数；

如果至多选两个代表，那他们的票数至少要超过 ⌊ 1/3 ⌋ 的票数；

如果至多选m个代表，那他们的票数至少要超过 ⌊ 1/(m+1) ⌋ 的票数。

所以以后碰到这样的问题，而且要求达到线性的时间复杂度以及常量级的空间复杂度，直接套上摩尔投票法。

接下来贴上代码：


class Solution {
    public List<Integer> majorityElement(int[] nums) {
        // 创建返回值
        List<Integer> res = new ArrayList<>();
        if (nums == null || nums.length == 0) return res;
        // 初始化两个候选人candidate，和他们的计票
        int cand1 = nums[0], count1 = 0;
        int cand2 = nums[0], count2 = 0;

        // 摩尔投票法，分为两个阶段：配对阶段和计数阶段
        // 配对阶段
        for (int num : nums) {
            // 投票
            if (cand1 == num) {
                count1++;
                continue;
            }
            if (cand2 == num) {
                count2++;
                continue;
            }

            // 第1个候选人配对
            if (count1 == 0) {
                cand1 = num;
                count1++;
                continue;
            }
            // 第2个候选人配对
            if (count2 == 0) {
                cand2 = num;
                count2++;
                continue;
            }

            count1--;
            count2--;
        }

        // 计数阶段
        // 找到了两个候选人之后，需要确定票数是否满足大于 N/3
        count1 = 0;
        count2 = 0;
        for (int num : nums) {
            if (cand1 == num) count1++;
            else if (cand2 == num) count2++;
        }

        if (count1 > nums.length / 3) res.add(cand1);
        if (count2 > nums.length / 3) res.add(cand2);

        return res;
    }
}

执行用时 : 2 ms , 在所有 Java 提交中击败了 99.89% 的用户
内存消耗 : 45.5 MB , 在所有 Java 提交中击败了 5.38% 的用户

import "fmt"

func majorityElement(nums []int) []int {
	// 创建返回值
	var res = make([]int, 0)
	if nums == nil || len(nums) == 0 {
		return res
	}

	// 初始化两个候选人 candidate，以及他们的计数票
	cand1 := nums[0]
	count1 := 0
	cand2 := nums[0]
	count2 := 0

	//摩尔投票法
	// 配对阶段
	for _, num := range nums {
		// 投票
		if cand1 == num {
			count1++
			continue
		}
		if cand2 == num {
			count2++
			continue
		}

		if count1 == 0 {
			cand1 = num
			count1++
			continue
		}
		if count2 == 0 {
			cand2 = num
			count2++
            continue
		}

		count1--
		count2--
	}
	// 计数阶段
	count1 = 0
	count2 = 0
	for _, num := range nums {
		if cand1 == num {
			count1++
		} else if cand2 == num {
			count2++
		}
	}

	if count1 > len(nums)/3 {
		res = append(res, cand1)
	}
	if count2 > len(nums)/3 {
		res = append(res, cand2)
	}
	return res
}

执行用时 : 16 ms , 在所有 Go 提交中击败了 56.67% 的用户
内存消耗 : 5.1 MB , 在所有 Go 提交中击败了 100.00% 的用户
提交完之后，发现执行用时仅打败了 60% 不到，难道还有更多的优秀算法笔者更好吗？

当我点击显示详情的时候，原来是用 Go 语言提交的人都没几个。

执行用时

然后我们再看执行内存消耗，很优秀嘛！比Java的内存消耗要少的多。

内存消耗

下一篇：摩尔投票法

public class Solution {
    public IList<int> MajorityElement(int[] nums) {
        if(nums.Length == 0) return nums;

        int c1 = nums[0], count1 = 1;
        int c2 = nums[0], count2 = 0;
        for(int i = 1; i < nums.Length; i++){
            if(nums[i] == c1) count1++;
            else if(nums[i] == c2) count2++;
            else if(count1 == 0){
                    c1 = nums[i];
                    count1++;
                }
            else if(count2 == 0){
                    c2 = nums[i];
                    count2++;
                }
            else{
                count2--;
                count1--;
            }
        }

        var ans = new List<int>();
        if(c1 == c2) {ans.Add(c1);}
        else{
            count1 = 0;
            count2 = 0;
            for(int i = 0; i < nums.Length; i++){
                if(nums[i] == c1) count1++;
                if(nums[i] == c2) count2++;
            }
            if(count1 > nums.Length/3) ans.Add(c1);
            if(count2 > nums.Length/3) ans.Add(c2);
        }

        return ans;
    }
}

public class Solution {
    public IList<int> MajorityElement(int[] nums) {
int limit = nums.Length / 3;
var sets = new Dictionary<int,int>();
foreach(var i in nums){
    if(sets.ContainsKey(i)){
        sets[i] = sets[i]+1;
    }
    else{
        sets.Add(i,1);
    }
}
 return sets.Where(n=>n.Value>limit).Select(n=>n.Key).ToArray();

    }
}

public class Solution {
    public IList<int> MajorityElement(int[] nums) {
        int a = 0, count_a = 0;
        int b = 0, count_b = 0;
        foreach (var num in nums) {
            if (a == num) {
                ++count_a;
            } else if (b == num) {
                ++count_b;
            } else if (count_a == 0) {
                a = num;
                ++count_a;
            } else if (count_b == 0) {
                b = num;
                ++count_b;
            } else {
                --count_a;
                --count_b;
            }
        }

        var ans = new List<int>();
        count_a = 0;
        count_b = 0;
        foreach (var num in nums) {
            if (num == a) ++count_a;
            else if (num == b) ++count_b;
        }

        if (count_a > nums.Length / 3) {
            ans.Add(a);
        }
        if (count_b > nums.Length / 3) {
            ans.Add(b);
        }

        return ans;
    }
}

public class Solution {
    public IList<int> MajorityElement(int[] nums) {
        int len = nums.Length;
        len /= 3;
        return nums.GroupBy(a => a).Where(b => b.Count() > len).Select(c => c.Key).ToList();
    }
}


public class Solution
{
    public IList<int> MajorityElement(int[] nums)
    {
        List<int> res = new List<int>();

        int countN = 0, countM = 0, n = 0, m = 0;
        for (int i = 0; i < nums.Length; i++)
        {
            if (n == nums[i]) countN++;
            else if (m == nums[i]) countM++;
            else if (countN == 0) { n = nums[i]; countN++; }
            else if (countM == 0) { m = nums[i]; countM++; }
            else { countN--; countM--; }
        }
        countN = 0;
        countM = 0;

        for(int i = 0; i < nums.Length; i++)
        {
            if (nums[i] == n) countN++;
            else if (nums[i] == m) countM++;
        }

        if (countN > nums.Length / 3) res.Add(n);
        if (countM > nums.Length / 3) res.Add(m);
        return res;
    }
}
public class Solution {
    public IList<int> MajorityElement(int[] nums) {
       return nums.ToList().GroupBy(u => u).Select(u => new KeyValuePair<int, int>(u.Key, u.Count())).Where(u => u.Value> nums.Length / 3).Select(u=>u.Key).ToList();
    }
}
public class Solution {
    public IList<int> MajorityElement(int[] nums) {
        var r=new List<int>();
        if(nums.Length<1)
            return r;
        var group = nums.GroupBy(p=>p);
        var line = nums.Length/3;
        foreach(var item in group.Where(p=>p.Count()>line)){
                r.Add(item.Key);
        }
        return r;
    }
}
*/