using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
在一排多米诺骨牌中，A[i] 和 B[i] 分别代表第 i 个多米诺骨牌的上半部分和下半部分。（一个多米诺是两个从 1 到 6 的数字同列平铺形成的 —— 该平铺的每一半上都有一个数字。）

我们可以旋转第 i 张多米诺，使得 A[i] 和 B[i] 的值交换。

返回能使 A 中所有值或者 B 中所有值都相同的最小旋转次数。

如果无法做到，返回 -1.

 

示例 1：



输入：A = [2,1,2,4,2,2], B = [5,2,6,2,3,2]
输出：2
解释：
图一表示：在我们旋转之前， A 和 B 给出的多米诺牌。
如果我们旋转第二个和第四个多米诺骨牌，我们可以使上面一行中的每个值都等于 2，如图二所示。
示例 2：

输入：A = [3,5,1,2,3], B = [3,6,3,3,4]
输出：-1
解释：
在这种情况下，不可能旋转多米诺牌使一行的值相等。
 

提示：

1 <= A[i], B[i] <= 6
2 <= A.length == B.length <= 20000
*/
/// <summary>
/// https://leetcode-cn.com/problems/minimum-domino-rotations-for-equal-row/
/// 1007. 行相等的最少多米诺旋转
/// 
/// </summary>
class MinimumDominoRotationsForEqualRowSolution
{
    public void Test()
    {
        var ret = MinDominoRotations(new int[] { 1, 2, 1, 1, 1, 2, 2, 2 }, new int[] { 2, 1, 2, 2, 2, 2, 2, 2 });
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int MinDominoRotations(int[] A, int[] B)
    {
        int len = A.Length;

        int GetRotations(int number)
        {
            int rotationsToA = 0;
            int rotationsToB = 0;
            int vA, vB;
            for (int i = 0; i < len; i++)
            {
                vA = A[i];
                vB = B[i];
                if (vA != number && vB != number) return -1;
                if (vA != number) rotationsToA += 1;
                else if (vB != number) rotationsToB += 1;
            }
            return Math.Min(rotationsToA, rotationsToB);
        }
        var rotationsA = GetRotations(A[0]);
        if (rotationsA != -1 || A[0] == B[0]) return rotationsA;
        return GetRotations(B[0]);
    }
}
/*
连续差相同的数字
力扣 (LeetCode)
发布于 1 年前
1.6k 阅读
方法一：贪心
分析

我们随便选其中的一个多米诺骨牌，它的标号为 i，上半部分的数字为 A[i]，下半部分的数字为 B[i]。

bla

此时可能会有三种情况：

以数字 A[i] 作为基准，将 A 或 B 中的所有值都变为 A[i]。例如，下图中，我们选择了第 00 个多米诺骨牌，这样可以将 A 中的所有值都变为 22。
bla

以数字 B[i] 作为基准，将 A 或 B 中的所有值都变为 B[i]。例如，下图中，我们选择了第 11 个多米诺骨牌，这样可以将 B 中的所有值都变为 22。
bla

无论选择 A[i] 还是 B[i] 都没有办法将 A 或 B 中的所有值变为都相同。例如，下图中，我们选择了最后一个多米诺骨牌，无论是它的上半部分 55 还是下半部分 44，都无法满足条件。
bla

如果要满足第 11 种或是第 22 种情况，就必须存在一块多米诺骨牌，它的上半部分或者下半部分的数字 x 在所有其它的多米诺骨牌中都出现过。若该条件满足，则说明所有多米诺骨牌中都出现了数字 x。因此，我们只要选择任意一块多米诺骨牌，判断它的上半部分或下半部分的数字是否可以作为 x 即可。

算法

选择第一块多米诺骨牌，它包含两个数字 A[0] 和 B[0]；

检查其余的多米诺骨牌中是否出现过 A[0]。如果都出现过，则求出最少的翻转次数，其为将 A[0] 全部翻到 A 和全部翻到 B 中的较少的次数。

检查其余的多米诺骨牌中是否出现过 B[0]。如果都出现过，则求出最少的翻转次数，其为将 B[0] 全部翻到 A 和全部翻到 B 中的较少的次数。

如果上述两次检查都失败，则返回 -1。

class Solution:        
    def minDominoRotations(self, A: List[int], B: List[int]) -> int:
        def check(x):
            """
            Return min number of swaps 
            if one could make all elements in A or B equal to x.
            Else return -1.
            """
            # how many rotations should be done
            # to have all elements in A equal to x
            # and to have all elements in B equal to x
            rotations_a = rotations_b = 0
            for i in range(n):
                # rotations coudn't be done
                if A[i] != x and B[i] != x:
                    return -1
                # A[i] != x and B[i] == x
                elif A[i] != x:
                    rotations_a += 1
                # A[i] == x and B[i] != x    
                elif B[i] != x:
                    rotations_b += 1
            # min number of rotations to have all
            # elements equal to x in A or B
            return min(rotations_a, rotations_b)
    
        n = len(A)
        rotations = check(A[0]) 
        # If one could make all elements in A or B equal to A[0]
        if rotations != -1 or A[0] == B[0]:
            return rotations 
        # If one could make all elements in A or B equal to B[0]
        else:
            return check(B[0])
复杂度分析

时间复杂度：O(N)O(N)。我们只会遍历所有的数组最多两次。

空间复杂度：O(1)O(1)。

public class Solution {
    public int MinDominoRotations(int[] A, int[] B) {
        int a = A[0];
        int b = B[0];
        int rot_a1 = 0, rot_a2 = 0;
        int rot_b1 = 0, rot_b2 = 0;
        bool flag_a = true;
        bool flag_b = true;

        for (int i = 0; i < A.Length; i++)
        {
            if (A[i] != a && B[i] != a)
            {
                flag_a = false;
                break;
            }    
            else if (A[i] == a && B[i] != a)
                rot_a2 ++;
            else if (A[i] != a && B[i] == a)
                rot_a1 ++;
        }
        if (flag_a == true)
            rot_a1 = rot_a1 > rot_a2 ? rot_a2 : rot_a1;
        for (int i = 0; i < A.Length; i++)
        {
            if (A[i] != b && B[i] != b)
            {
                flag_b = false;
                if (flag_a == false)
                    return -1;
                break;
            }    
            else if (A[i] != b && B[i] == b)
                rot_b2 ++;
            else if (A[i] == b && B[i] != b)
                rot_b1 ++;
        }
        if (flag_b == true)
            rot_b1 = rot_b1 > rot_b2 ? rot_b2 : rot_b1;
        //Console.WriteLine(rot_a1+","+rot_b1);
        if (flag_a == true && flag_b == true)
            return rot_a1 > rot_b1 ? rot_b1 : rot_a1;
        else if (flag_a == true && flag_b == false)
            return rot_a1;
        else
            return rot_b1;

    }
} 
*/
