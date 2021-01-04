using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
如果连续数字之间的差严格地在正数和负数之间交替，则数字序列称为摆动序列。第一个差（如果存在的话）可能是正数或负数。少于两个元素的序列也是摆动序列。

例如， [1,7,4,9,2,5] 是一个摆动序列，因为差值 (6,-3,5,-7,3) 是正负交替出现的。相反, [1,4,7,2,5] 和 [1,7,4,5,5] 不是摆动序列，第一个序列是因为它的前两个差值都是正数，第二个序列是因为它的最后一个差值为零。

给定一个整数序列，返回作为摆动序列的最长子序列的长度。 通过从原始序列中删除一些（也可以不删除）元素来获得子序列，剩下的元素保持其原始顺序。

示例 1:

输入: [1,7,4,9,2,5]
输出: 6 
解释: 整个序列均为摆动序列。
示例 2:

输入: [1,17,5,10,13,15,10,5,16,8]
输出: 7
解释: 这个序列包含几个长度为 7 摆动序列，其中一个可为[1,17,10,13,10,16,8]。
示例 3:

输入: [1,2,3,4,5,6,7,8,9]
输出: 2
进阶:
你能否用 O(n) 时间复杂度完成此题? 
*/
/// <summary>
/// https://leetcode-cn.com/problems/wiggle-subsequence/
/// 376. 摆动序列
/// https://blog.csdn.net/xuchonghao/article/details/81082673
/// </summary>
class WiggleSubsequenceSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int WiggleMaxLength(int[] nums)
    {
        if (nums == null || nums.Length == 0) return 0;
        if (nums.Length == 1) return 1;
        int[] up = new int[nums.Length];
        int[] down = new int[nums.Length];
        up[0] = down[0] = 1;
        for (int i = 1; i < nums.Length; i++)
        {
            if (nums[i] > nums[i - 1])
            {
                up[i] = down[i - 1] + 1;
                down[i] = down[i - 1];
            }
            else if (nums[i] < nums[i - 1])
            {
                down[i] = up[i - 1] + 1;
                up[i] = up[i - 1];
            }
            else
            {
                down[i] = down[i - 1];
                up[i] = up[i - 1];
            }
        }
        return Math.Max(down[nums.Length - 1], up[nums.Length - 1]);
    }
}
/*

摆动序列
力扣官方题解
发布于 2020-12-11
18.9k
写在前面
解决本题前，我们先进行一些约定：

某个序列被称为「上升摆动序列」，当且仅当该序列是摆动序列，且最后一个元素呈上升趋势。如序列 [1,3,2,4][1,3,2,4] 即为「上升摆动序列」。

某个序列被称为「下降摆动序列」，当且仅当该序列是摆动序列，且最后一个元素呈下降趋势。如序列 [4,2,3,1][4,2,3,1] 即为「下降摆动序列」。

特别地，对于长度为 11 的序列，它既是「上升摆动序列」，也是「下降摆动序列」。

序列中的某个元素被称为「峰」，当且仅当该元素两侧的相邻元素均小于它。如序列 [1,3,2,4][1,3,2,4] 中，33 就是一个「峰」。

序列中的某个元素被称为「谷」，当且仅当该元素两侧的相邻元素均大于它。如序列 [1,3,2,4][1,3,2,4] 中，22 就是一个「谷」。

特别地，对于位于序列两端的元素，只有一侧的相邻元素小于或大于它，我们也称其为「峰」或「谷」。如序列 [1,3,2,4][1,3,2,4] 中，11 也是一个「谷」，44 也是一个「峰」。

因为一段相邻的相同元素中我们最多只能选择其中的一个，所以我们可以忽略相邻的相同元素。现在我们假定序列中任意两个相邻元素都不相同，即要么左侧大于右侧，要么右侧大于左侧。对于序列中既非「峰」也非「谷」的元素，我们称其为「过渡元素」。如序列 [1,2,3,4][1,2,3,4] 中，22 和 33 都是「过渡元素」。

方法一：动态规划
思路及解法

每当我们选择一个元素作为摆动序列的一部分时，这个元素要么是上升的，要么是下降的，这取决于前一个元素的大小。那么列出状态表达式为：

\textit{up}[i]up[i] 表示以前 ii 个元素中的某一个为结尾的最长的「上升摆动序列」的长度。

\textit{down}[i]down[i] 表示以前 ii 个元素中的某一个为结尾的最长的「下降摆动序列」的长度。

下面以 \textit{up}[i]up[i] 为例，说明其状态转移规则：

当 \textit{nums}[i] \leq \textit{nums}[i - 1]nums[i]≤nums[i−1] 时，我们无法选出更长的「上升摆动序列」的方案。因为对于任何以 \textit{nums}[i]nums[i] 结尾的「上升摆动序列」，我们都可以将 \textit{nums}[i]nums[i] 替换为 \textit{nums}[i - 1]nums[i−1]，使其成为以 \textit{nums}[i - 1]nums[i−1] 结尾的「上升摆动序列」。

当 \textit{nums}[i] > \textit{nums}[i - 1]nums[i]>nums[i−1] 时，我们既可以从 up[i - 1]up[i−1] 进行转移，也可以从 \textit{down}[i - 1]down[i−1] 进行转移。下面我们证明从 \textit{down}[i - 1]down[i−1] 转移是必然合法的，即必然存在一个 \textit{down}[i - 1]down[i−1] 对应的最长的「下降摆动序列」的末尾元素小于 \textit{nums}[i]nums[i]。

我们记这个末尾元素在原序列中的下标为 jj，假设从 jj 往前的第一个「谷」为 \textit{nums}[k]nums[k]，我们总可以让 jj 移动到 kk，使得这个最长的「下降摆动序列」的末尾元素为「谷」。

然后我们可以证明在这个末尾元素为「谷」的情况下，这个末尾元素必然是从 \textit{nums}[i]nums[i] 往前的第一个「谷」。证明非常简单，我们使用反证法，如果这个末尾元素不是从 \textit{nums}[i]nums[i] 往前的第一个「谷」，那么我们总可以在末尾元素和 \textit{nums}[i]nums[i] 之间取得一对「峰」与「谷」，接在这个「下降摆动序列」后，使其更长。

这样我们知道必然存在一个 \textit{down}[i - 1]down[i−1] 对应的最长的「下降摆动序列」的末尾元素为 \textit{nums}[i]nums[i] 往前的第一个「谷」。这个「谷」必然小于 \textit{nums}[i]nums[i]。证毕。

这样我们可以用同样的方法说明 \textit{down}[i]down[i] 的状态转移规则，最终的状态转移方程为：

\begin{aligned} &\textit{up}[i]= \begin{cases} \textit{up}[i - 1],& \textit{nums}[i] \leq \textit{nums}[i - 1] \\ \max(\textit{up}[i - 1], \textit{down}[i - 1] + 1),& \textit{nums}[i] > \textit{nums}[i - 1] \end{cases} \\\\ &\textit{down}[i]= \begin{cases} \textit{down}[i - 1],& \textit{nums}[i] \geq \textit{nums}[i - 1] \\ \max(\textit{up}[i - 1] + 1, \textit{down}[i - 1]),& \textit{nums}[i] < \textit{nums}[i - 1] \end{cases} \end{aligned}
​	
  
up[i]={ 
up[i−1],
max(up[i−1],down[i−1]+1),
​	
  
nums[i]≤nums[i−1]
nums[i]>nums[i−1]
​	
 
down[i]={ 
down[i−1],
max(up[i−1]+1,down[i−1]),
​	
  
nums[i]≥nums[i−1]
nums[i]<nums[i−1]
​	
 
​	
 

最终的答案即为 \textit{up}[n-1]up[n−1] 和 \textit{down}[n-1]down[n−1] 中的较大值，其中 nn 是序列的长度。

代码


class Solution {
    public int wiggleMaxLength(int[] nums) {
        int n = nums.length;
        if (n < 2) {
            return n;
        }
        int[] up = new int[n];
        int[] down = new int[n];
        up[0] = down[0] = 1;
        for (int i = 1; i < n; i++) {
            if (nums[i] > nums[i - 1]) {
                up[i] = Math.max(up[i - 1], down[i - 1] + 1);
                down[i] = down[i - 1];
            } else if (nums[i] < nums[i - 1]) {
                up[i] = up[i - 1];
                down[i] = Math.max(up[i - 1] + 1, down[i - 1]);
            } else {
                up[i] = up[i - 1];
                down[i] = down[i - 1];
            }
        }
        return Math.max(up[n - 1], down[n - 1]);
    }
}
复杂度分析

时间复杂度：O(n)O(n)，其中 nn 是序列的长度。我们只需要遍历该序列一次。

空间复杂度：O(n)O(n)，其中 nn 是序列的长度。我们需要开辟两个长度为 nn 的数组。

方法二：优化的动态规划
思路及解法

注意到方法一中，我们仅需要前一个状态来进行转移，所以我们维护两个变量即可。这样我们可以写出如下的代码：


class Solution {
    public int wiggleMaxLength(int[] nums) {
        int n = nums.length;
        if (n < 2) {
            return n;
        }
        int up = 1, down = 1;
        for (int i = 1; i < n; i++) {
            if (nums[i] > nums[i - 1]) {
                up = Math.max(up, down + 1);
            } else if (nums[i] < nums[i - 1]) {
                down = Math.max(up + 1, down);
            }
        }
        return Math.max(up, down);
    }
}
注意到每有一个「峰」到「谷」的下降趋势，\textit{down}down 值才会增加，每有一个「谷」到「峰」的上升趋势，\textit{up}up 值才会增加。且过程中 \textit{down}down 与 \textit{up}up 的差的绝对值值恒不大于 11，即 \textit{up} \leq \textit{down} + 1up≤down+1 且 \textit{down} \leq \textit{up} + 1down≤up+1，于是有 \max(\textit{up}, \textit{down} + 1) = \textit{down} + 1max(up,down+1)=down+1 且 \max(\textit{up} + 1, \textit{down}) = \textit{up} + 1max(up+1,down)=up+1。这样我们可以省去不必要的比较大小的过程。

代码


class Solution {
    public int wiggleMaxLength(int[] nums) {
        int n = nums.length;
        if (n < 2) {
            return n;
        }
        int up = 1, down = 1;
        for (int i = 1; i < n; i++) {
            if (nums[i] > nums[i - 1]) {
                up = down + 1;
            } else if (nums[i] < nums[i - 1]) {
                down = up + 1;
            }
        }
        return Math.max(up, down);
    }
}
复杂度分析

时间复杂度：O(n)O(n)，其中 nn 是序列的长度。我们只需要遍历该序列一次。

空间复杂度：O(1)O(1)。我们只需要常数空间来存放若干变量。

方法三：贪心
思路及解法

观察这个序列可以发现，我们不断地交错选择「峰」与「谷」，可以使得该序列尽可能长。证明非常简单：如果我们选择了一个「过渡元素」，那么在原序列中，这个「过渡元素」的两侧有一个「峰」和一个「谷」。不失一般性，我们假设在原序列中的出现顺序为「峰」「过渡元素」「谷」。如果「过渡元素」在选择的序列中小于其两侧的元素，那么「谷」一定没有在选择的序列中出现，我们可以将「过渡元素」替换成「谷」；同理，如果「过渡元素」在选择的序列中大于其两侧的元素，那么「峰」一定没有在选择的序列中出现，我们可以将「过渡元素」替换成「峰」。这样一来，我们总可以将任意满足要求的序列中的所有「过渡元素」替换成「峰」或「谷」。并且由于我们不断地交错选择「峰」与「谷」的方法就可以满足要求，因此这种选择方法就一定可以达到可选元素数量的最大值。

这样，我们只需要统计该序列中「峰」与「谷」的数量即可（注意序列两端的数也是「峰」或「谷」），但需要注意处理相邻的相同元素。

在实际代码中，我们记录当前序列的上升下降趋势。每次加入一个新元素时，用新的上升下降趋势与之前对比，如果出现了「峰」或「谷」，答案加一，并更新当前序列的上升下降趋势。

代码


class Solution {
    public int wiggleMaxLength(int[] nums) {
        int n = nums.length;
        if (n < 2) {
            return n;
        }
        int prevdiff = nums[1] - nums[0];
        int ret = prevdiff != 0 ? 2 : 1;
        for (int i = 2; i < n; i++) {
            int diff = nums[i] - nums[i - 1];
            if ((diff > 0 && prevdiff <= 0) || (diff < 0 && prevdiff >= 0)) {
                ret++;
                prevdiff = diff;
            }
        }
        return ret;
    }
}
复杂度分析

时间复杂度：O(n)O(n)，其中 nn 是序列的长度。我们只需要遍历该序列一次。

空间复杂度：O(1)O(1)。我们只需要常数空间来存放若干变量。

public class Solution {
    public int WiggleMaxLength(int[] nums) {
        int n = nums.Length;
      if(n<2)
          return n;
        int up=1;
        int down=1;
      for(int i=1;i<n;i++)
      {
          if(nums[i]-nums[i-1]>0)
              up=down+1;
          if(nums[i]-nums[i-1]<0)
              down = up +1;
      }
        
        return Math.Max(up,down);
    }
} 
*/
