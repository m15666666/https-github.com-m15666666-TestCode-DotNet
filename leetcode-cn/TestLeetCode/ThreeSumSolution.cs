﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给你一个包含 n 个整数的数组 nums，判断 nums 中是否存在三个元素 a，b，c ，
使得 a + b + c = 0 ？请你找出所有满足条件且不重复的三元组。

注意：答案中不可以包含重复的三元组。

 

示例：

给定数组 nums = [-1, 0, 1, 2, -1, -4]，

满足要求的三元组集合为：
[
  [-1, 0, 1],
  [-1, -1, 2]
]
*/
/// <summary>
/// https://leetcode-cn.com/problems/3sum/
/// 15. 三数之和
/// 
/// https://www.jianshu.com/p/19b0261c73b9
/// </summary>
class ThreeSumSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    public IList<IList<int>> ThreeSum(int[] nums)
    {
        List<IList<int>> ret = new List<IList<int>>();

        if (nums == null || nums.Length < 3) return ret;

        Array.Sort(nums);

        //int n1, n2, n3;
        //n1 = n2 = n3 = -1;
        int upper = nums.Length - 2;
        for (int i = 0; i < upper; ++i)
        {
            var v = nums[i];
            if (0 < v) break;

            // 跳过
            if (0 < i && nums[i - 1] == v) continue;

            int left = i + 1, right = nums.Length - 1;
            while (left < right)
            {
                int sum = v + nums[left] + nums[right];
                if (sum == 0
                    //&& !(n1 == v && n2 == nums[left] && n3 == nums[right])
                    )
                {
                    //if (!(n1 == v && n2 == nums[left] && n3 == nums[right]))
                    //{
                    //    n1 = v;
                    //    n2 = nums[left];
                    //    n3 = nums[right];

                    //    ret.Add(new int[] { n1, n2, n3 });
                    //}

                    ret.Add(new int[] { v, nums[left], nums[right] });

                    while (left < right && nums[left]== nums[left + 1]) ++left;
                    while (left < right && nums[right] == nums[right - 1]) --right;

                    ++left;
                    --right;
                }
                else if (sum < 0) ++left;
                else --right;
            }
        }

        return ret;
    }
}
/*
三数之和题解
王德福
发布于 10 个月前
121.2k
很多前端都觉得算法是特别高大上的东西，但其实和人工智能这种硬核（虽然调参也不怎么硬核）的东西不同，算法可能会复杂，也可能很容易理解。

比如这道题就是一个非常经典的题目，很多公司的面试都会聊这道题。

这道 medium 难度的问题，很适合展示一个有着丰富生活经验的程序员，如何在不需要算法基础的情况下，凭借人类的聪明才智，找到解决方案。

热身
首先，先找一下它的简化版 2sum 来热热身。

最简单的想法就是把每两个都拿出来加一下，看看结果是不是我们想要的。但是直觉告诉我们，这样子并不高效。举一个很实际的例子就能明白。

比如这个周末你去参加线下相亲会，全场有且只有两个人才是真爱。于是我们每个人都要去找其他所有人聊天，去寻找 ta 是不是自己要找的另一半。每个人都要和每个人说话，这样时间复杂度很高，翻译成计算机的表示就是 O(n2)。

    var twoSum = function(nums, target) {
      for (let i = 0; i < nums.length - 1; i++) { // 每个人
        for (let j = i + 1; j < nums.length; j++) { // 都去问其他的人
          if (nums[i]+nums[j] === target) {
            return [nums[i], nums[j]]
          }
        }
      }
    }
怎么样可以更高效一点？
这时候要引入哈希表，其实就是一个登记册，写上你的名字和你的要求。如果每个人都提前在主持人那里登记一遍，然后只要大家依次再报出自己名字，主持人就能够识别到，ta 就是你要找的人。

  var twoSum = function(nums, target) {
      let res = {}
      for (let i = 0; i < nums.length; i++) { // 每个人登记自己想要配对的人，让主持人记住
        res[target - nums[i]] = nums[i]
      }
      for (let j = 0; j < nums.length; j++) { // 每个人再次报数的时候，主持人看一下名单里有没有他
        if (res[nums[j]] !== undefined) {
          return [nums[j], res[nums[j]]]
        }
      }
    }
很容易看出来，上面的方案仍然可以优化。就是每个人都来问一下主持人，自己要找的人有没有来登记过，如果没有的话，就把自己的要求写下来，等着别人来找自己。

  var twoSum = function(nums, target) {
      let res = {}
      for (let i = 0; i < nums.length; i++) { // 每个人报出自己想要配对的人
        if (res[nums[i]] !== undefined) { // 如果有人被登记过
          return [nums[i], res[nums[i]]] // 就是他
        } else {  // 否则
          res[target - nums[i]] = nums[i] // 主持人记住他的需求
        }
      }
    }
2sum 问题最坏的情况是，第一个人和最后一个人配对，每个人都发了一次言。时间复杂度是 O(n)，空间复杂度也是 O(n)，因为主持人要用小本本记录下每个人的发言，最坏的时候，要把所有人的诉求都记一遍。

从生活经验中我们能感觉到什么方案是最好的，而且也能够知道，什么算法已经到达了优化的极限。这就是所谓的”只使用人类的聪明才智“就可以做出来。

当然， 如果没有编程的基础，可能不会想到哈希表-登记册这样的类比，但这应该只影响把解决方案转化为代码表达。想到解决方案这件事，其实是完全不依赖编码能力的。

面试很多年轻前端同学的时候，他们会一上来就直接开始写代码，然后把草纸写得一团糟。我都会给他们讲，要把算法和编码分开来看，你可以用伪代码，画图，甚至摆弄小道具的方法去寻求解法。有了明确的解法，再转化成可执行的代码，就是水到渠成的事情了。

three sum
好的，热身结束，现在来看 3sum 问题。这个问题复杂了一些，变成了一个人找两个人。

一个新的周末，你再次去现在参加了一个，额，这次不是相亲会，是参加一个街篮比赛，赛前当然要组队啦，现在要想一个方法找到队友。组队还有一个要求，就是队伍的平均实力要符合要求，比如菜鸟抱两个大腿，或者有王者带两个弱鸡。

我们先想一个保底的办法，再去慢慢优化。最简单的办法是，每个人都去依次拉上另一个人一起去找第三个人，这个时间复杂度是 O(n3)O(n3)。

    var threeSum = function(nums) {
      let res = []
      for (let i = 0; i < nums.length - 2; i++) { // 每个人
        for (let j = i + 1; j < nums.length - 1; j++) { // 依次拉上其他每个人
          for (let k = j + 1; k < nums.length; k++) { // 去问剩下的每个人
            if (nums[i] + nums[j] + nums[k] === 0) { // 我们是不是可以一起组队
              res.push([nums[i], nums[j], nums[k]])
            }
          }
        }
      }
      return res
    }
受到上题的启发，在凑齐两人以后，他们可以找主持人登记需求的第三人，而不需要在茫茫人海中去找队友。这样，我们就把问题优化成了每个人都要找其他每个人，即时间复杂度 O(n2)O(n2)，因为需要主持人记录数据，这里还有 O(n)O(n) 的空间复杂度。

    var threeSum = function(nums) {
        let res = []
        let hash = {}
        for (let i = 0; i < nums.length - 2; i++) { // 每个人
          for (let j = i + 1; j < nums.length - 1; j++) { // 依次拉上其他每个人
            if (hash[nums[j]] !== undefined) { // 已经有合适自己的两人组
              res.push([nums[j]].concat(hash[nums[j]]))
              hash[nums[j]] = undefined
            } else { // 没有合适自己的两人组
              let mark = 0 - nums[i] - nums[j]
              hash[mark] = [nums[i], nums[j]]
            }
          }
        }
        return res
    } // 示意代码 未AC
再优化
现在已经想到了可用的通用方案，根据题目的特点，看看还有哪里可以做一些优化。比如提前结束一些不可能的组合。

首先安排所有人按照顺序排队站好，这是一个需要花时间的操作，不过磨刀不误砍柴工，付出这个时间还是值得的。排序可以做到 O(nlogn)O(nlogn)，这是优于 O(n^2)O(n 
2
 ) 的。

然后我们选择一个人做C位，既然是C位，那么就需要左右各有一个人。先选择队伍最左边（最小值）和队伍最右边（最大值）两个人，加上你，算一下总和。如果大于 0，说明实力太强了，就把就把右侧的人选调左一位，反之，则调整左边的人选，增强一下实力。当某边选到紧挨着你的人的时候，就意味着组队结束，以你为 C位的所有可能都已经尝试完毕了。

    var threeSum = function (nums) {
      let res = []
      nums.sort((a, b) => a - b) // 先排个队，最左边是最弱（小）的，最右边是最强(大)的
      for (let i = 1; i < nums.length - 1; i++) { // C位人选
        let first = 0
        let last = nums.length - 1
        do {
          let result = nums[i] + nums[first] + nums[last]
          if (result === 0) { // 如果可以组队
            res.push([nums[i], nums[first], nums[last]])
          }
          if (result <= 0 && first < i) { // 实力太弱，把菜鸟那边右移一位
            while (nums[first] === nums[++first]); // 如果相等就跳过
          } else if (result > 0 && last > i) { // 实力太强，把大神那边右移一位
            while (nums[last] === nums[--last]);
          } else {
            break // 某一边已经没有人选了
          }
        } while (1) { }
      }
      return res
    } // 示意代码 未AC
于是我们开开心心的把解答发到了力扣，然后就得到了一个 WA（wrong answer）。因为力扣的测试用例往往会有很多边界数据，不针对这些特殊情况做考虑的话，几乎一定会翻车的。

针对特殊用例优化
我们刚刚的解法，会在 [0,0,0,0] 这个情况下败下阵来，此时有两个解决方案，第一种就是直接堵上漏洞，针对有问题的测试数据在代码中加补丁。如果运气好的话，可能下一次提交就会 AC 了。但是这种方法在竞赛中是不可取的，因为一次 WA 会有罚时。在工作中我们可能顺手就打补丁修了问题，然后等到测试报出其他问题再去修。如果测试恰好没有发现任何问题，这段代码就可以上线了。但是这样带来的问题，一是并没有在逻辑层面消除某一类特殊数据可能带来的问题，二是代码会很丑陋，变得难以维护。

所以更好的做法是：从这个不通过的数据提取特征，添加对应特征数据的处理逻辑，一次修复一类的错误，并且尽量保持代码的流畅。

此处的报错是由重复数据引起的，看样子完全一样的两个人没必要 C 位出道两次，但是在一个队伍中其实存在着有两个相同值的可能，比如 [-1, -1, 2] 这种组合。

于是我们新的问题就是：当有多个相同值的时候，选谁做 C 位？

像在[-1, -1, 2]这个例子中，由第二个-1做了 C 位，我们用人类的聪明才智想一下就很容易想明白。如果一个负数要做 C 位，它必须要保证队里有一个正数，所以选择最右边的那个就可以。反过来看 [-2, 1, 1] 这个案例，为了保证至少有一个负数，我们选择最左边的重复值。

对于 0 这个特殊值来说，左右一定是对称的，有一个极特殊的情况就是队列里面有超过三个 0，此时只有选择中间的 0 才能保证不遗漏这种情况。除此之外，选择哪个0居中都是无所谓的。

重构策略
等等，分析到这里，好像把事情搞得过于复杂了。我们在选择第一个人的时候就分了三种情况。
重新思考一下，一开始选择 C 位，实则是为了利用有序数组快速筛选方案。因为这个人位于中间，所以才会有复杂的选取策略。如果第一次直接选择最左边的那个人，后面的策略依然类似，以双指针从最大最小两端相向而行，直到相遇，或者即将筛选出来三个符号相同的结果，即停止。好像仍然可以找到正确答案，同时也恰好避开了复杂的选 C 位情况。

我们可以进一步把一些明显出界的条件加上判断，再一次剪除部分无用尝试。

    var threeSum = function (nums) {
      let res = []
      let length = nums.length;
      nums.sort((a, b) => a - b) // 先排个队，最左边是最弱（小）的，最右边是最强(大)的
      if (nums[0] <= 0 && nums[length - 1] >= 0) { // 优化1: 整个数组同符号，则无解
        for (let i = 0; i < length - 2;) {
          if (nums[i] > 0) break; // 优化2: 最左值为正数则一定无解
          let first = i + 1
          let last = length - 1
          do {
            if (first >= last || nums[i] * nums[last] > 0) break // 两人选相遇，或者三人同符号，则退出
            let result = nums[i] + nums[first] + nums[last]
            if (result === 0) { // 如果可以组队
              res.push([nums[i], nums[first], nums[last]])
            }
            if (result <= 0 ) { // 实力太弱，把菜鸟那边右移一位
              while (first < last && nums[first] === nums[++first]){} // 如果相等就跳过
            } else { // 实力太强，把大神那边右移一位
              while (first < last && nums[last] === nums[--last]) {}
            }
          } while (first < last)
          while (nums[i] === nums[++i]) {}
        }
      }
      return res
    }
面试中考察算法当然有可能就是比较纯粹的智商测试。既然你能够讲清楚这道题，至少说明你可以理解一个较复杂问题是如何解决的，思维和表达能力可以达到一定的水平。

另一种考察的可能，并不是要背算法，而是让面试者现场对问题进行分析，提出解决方案，然后进一步的转化为代码，再去验证。在这个过程中还可以持续的优化，包括对边界的优化补充，局部分支优化，甚至也包括对解法模型的优化。
这和我们日常的开发过程也很像。从需求抽象出技术方案，再转化为代码，经过联调测试，修bug，可能还有整合和重构。

一道力扣题目的解答，就是一个项目开发的缩影，这是我在面试中的体会。而最终打动我的同学，未必是因为完美的做对了问题，重要的是，展示了他们作为一个程序员，应有的聪明才智。

下一篇：排序 + 双指针，逐行解释


排序 + 双指针，逐行解释
吴彦祖
发布于 4 个月前
47.5k
排序 + 双指针
本题的难点在于如何去除重复解。

算法流程：
特判，对于数组长度 nn，如果数组为 nullnull 或者数组长度小于 33，返回 [][]。
对数组进行排序。
遍历排序后数组：
若 nums[i]>0nums[i]>0：因为已经排序好，所以后面不可能有三个数加和等于 00，直接返回结果。
对于重复元素：跳过，避免出现重复解
令左指针 L=i+1L=i+1，右指针 R=n-1R=n?1，当 L<RL<R 时，执行循环：
当 nums[i]+nums[L]+nums[R]==0nums[i]+nums[L]+nums[R]==0，执行循环，判断左界和右界是否和下一位置重复，去除重复解。并同时将 L,RL,R 移到下一位置，寻找新的解
若和大于 00，说明 nums[R]nums[R] 太大，RR 左移
若和小于 00，说明 nums[L]nums[L] 太小，LL 右移
复杂度分析
时间复杂度：O\left(n^{2}\right)O(n 
2
 )，数组排序 O(N \log N)O(NlogN)，遍历数组 O\left(n\right)O(n)，双指针遍历 O\left(n\right)O(n)，总体 O(N \log N)+O\left(n\right)*O\left(n\right)O(NlogN)+O(n)?O(n)，O\left(n^{2}\right)O(n 
2
 )
空间复杂度：O(1)O(1)
class Solution:
    def threeSum(self, nums: List[int]) -> List[List[int]]:
        
        n=len(nums)
        res=[]
        if(not nums or n<3):
            return []
        nums.sort()
        res=[]
        for i in range(n):
            if(nums[i]>0):
                return res
            if(i>0 and nums[i]==nums[i-1]):
                continue
            L=i+1
            R=n-1
            while(L<R):
                if(nums[i]+nums[L]+nums[R]==0):
                    res.append([nums[i],nums[L],nums[R]])
                    while(L<R and nums[L]==nums[L+1]):
                        L=L+1
                    while(L<R and nums[R]==nums[R-1]):
                        R=R-1
                    L=L+1
                    R=R-1
                elif(nums[i]+nums[L]+nums[R]>0):
                    R=R-1
                else:
                    L=L+1
        return res
下一篇：画解算法：15. 三数之和

public class Solution {
    public IList<IList<int>> ThreeSum(int[] nums) {
        
           var list = new List<IList<int>>();
            if (nums.Length < 3)
            {
                return list;
            }
            Array.Sort(nums);
            for (int i = 0; i < nums.Length-1; i++)
            {
                if (nums[i]>0)
                {
                    break;
                }
                if (i>0&&nums[i]==nums[i-1])
                {
                    continue;
                }
                var l = i + 1;
                var r = nums.Length - 1;
                while (l<r)
                {
                    var sum = nums[i] + nums[l] + nums[r];
                    if (sum<0)
                    {
                        l++;
                    }
                    else if (sum>0)
                    {
                        r--;
                    }
                    else
                    {
                        list.Add(new List<int> { nums[i], nums[l], nums[r] });
                        while (l<r&&nums[l]==nums[l+1])
                        {
                            l++;
                        }
                        while (l < r && nums[r] == nums[r-1])
                        {
                            r--;
                        }
                        l++;
                        r--;
                    }
                }
            }


            return list;
    }
}
 
*/
