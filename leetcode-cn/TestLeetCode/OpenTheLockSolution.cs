using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
你有一个带有四个圆形拨轮的转盘锁。每个拨轮都有10个数字： '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' 。每个拨轮可以自由旋转：例如把 '9' 变为  '0'，'0' 变为 '9' 。每次旋转都只能旋转一个拨轮的一位数字。

锁的初始数字为 '0000' ，一个代表四个拨轮的数字的字符串。

列表 deadends 包含了一组死亡数字，一旦拨轮的数字和列表里的任何一个元素相同，这个锁将会被永久锁定，无法再被旋转。

字符串 target 代表可以解锁的数字，你需要给出最小的旋转次数，如果无论如何不能解锁，返回 -1。

 

示例 1:

输入：deadends = ["0201","0101","0102","1212","2002"], target = "0202"
输出：6
解释：
可能的移动序列为 "0000" -> "1000" -> "1100" -> "1200" -> "1201" -> "1202" -> "0202"。
注意 "0000" -> "0001" -> "0002" -> "0102" -> "0202" 这样的序列是不能解锁的，
因为当拨动到 "0102" 时这个锁就会被锁定。
示例 2:

输入: deadends = ["8888"], target = "0009"
输出：1
解释：
把最后一位反向旋转一次即可 "0000" -> "0009"。
示例 3:

输入: deadends = ["8887","8889","8878","8898","8788","8988","7888","9888"], target = "8888"
输出：-1
解释：
无法旋转到目标数字且不被锁定。
示例 4:

输入: deadends = ["0000"], target = "8888"
输出：-1
 

提示：

死亡列表 deadends 的长度范围为 [1, 500]。
目标数字 target 不会在 deadends 之中。
每个 deadends 和 target 中的字符串的数字会在 10,000 个可能的情况 '0000' 到 '9999' 中产生。 
*/
/// <summary>
/// https://leetcode-cn.com/problems/open-the-lock/
/// 752. 打开转盘锁
/// https://blog.csdn.net/koukehui0292/article/details/84145706
/// </summary>
class OpenTheLockSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int OpenLock(string[] deadends, string target)
    {
        HashSet<int> deads = new HashSet<int>(deadends.Length);
        foreach (var d in deadends) deads.Add(GetHashCode(d));

        int targetHash = GetHashCode(target);

        int startHash = GetHashCode("0000");
        Queue<int> queue = new Queue<int>();
        queue.Enqueue(startHash);

        HashSet<int> visited = new HashSet<int>();
        visited.Add(startHash);

        int steps = 0;
        while (0 < queue.Count)
        {
            for( int i = queue.Count; 0 < i; i--)
            {
                var code = queue.Dequeue();
                if (deads.Contains(code)) continue;

                if (code == targetHash) return steps;

                int divisor = 1;
                for ( int j = 0; j < 4; j++)
                {
                    var (left,right) = ChangeHashCode(code,divisor);

                    if(!visited.Contains(left) && !deads.Contains(left))
                    {
                        visited.Add(left);
                        queue.Enqueue(left);
                    }
                    if (!visited.Contains(right) && !deads.Contains(right))
                    {
                        visited.Add(right);
                        queue.Enqueue(right);
                    }

                    divisor *= 10;
                }
            }
            steps++;
        }
        return -1;
    }

    private static int GetHashCode( string code )
    {
        const char Zero = '0';
        return (code[0] - Zero) * 1000 + (code[1] - Zero) * 100 + (code[2] - Zero) * 10 + (code[3] - Zero);
    }

    private static (int,int) ChangeHashCode(int code, int lowThreshold)
    {
        int upThreshold = lowThreshold * 10;
        int remain0 = code % upThreshold;
        int upRemain = code - remain0;
        int digit = remain0 / lowThreshold;
        int lowRemain = remain0 % lowThreshold;

        int temp1, temp2;
        if (digit == 0) {
            temp1 = 9;
            temp2 = 1;
        }
        else if (digit == 9) {
            temp1 = 8;
            temp2 = 0;
        }
        else {
            temp1 = digit - 1;
            temp2 = digit + 1;
        }

        return (upRemain + temp1 * lowThreshold + lowRemain, upRemain + temp2 * lowThreshold + lowRemain);
    }
}
/*
public class Solution {
    public int OpenLock(string[] deadends, string target) {
        
        Dictionary<int, bool> deadDic = new Dictionary<int, bool>();
        for (int i = 0; i < deadends.Length; i ++) {
            int temp = int.Parse(deadends[i]);
            deadDic[temp] = true;
        }
        
        int tgt = int.Parse(target);
        
        Dictionary<int, bool> hisDic = new Dictionary<int, bool>();
        
        Queue<int> queue = new Queue<int>();
        queue.Enqueue(0);
        
        int count = 0;
        while(queue.Count() > 0) {
            int size = queue.Count();
            for (int i = 0; i < size; i ++) {
                int temp = queue.Dequeue();
                if (hisDic.ContainsKey(temp) || deadDic.ContainsKey(temp)) {
                    
                } else {
                    if (temp == tgt) {
                        return count;
                    }
                    
                    hisDic[temp] = true;
                    
                    { // 各位
                        int val = temp % 10;
                        if (val == 9) {
                            queue.Enqueue((temp - 1 + 10000)%10000);
                            queue.Enqueue((temp - 9 + 10000)%10000);
                        } else if (val == 0) {
                            queue.Enqueue((temp + 1 + 10000)%10000);
                            queue.Enqueue((temp + 9 + 10000)%10000);
                        } else {
                            queue.Enqueue((temp + 1 + 10000)%10000);
                            queue.Enqueue((temp - 1 + 10000)%10000);
                        }
                    }
                    
                    {   // 十位
                        int val = temp / 10 % 10;
                        if (val == 9) {
                            queue.Enqueue((temp - 10 + 10000)%10000);
                            queue.Enqueue((temp - 90 + 10000)%10000);
                        } else if (val == 0) {
                            queue.Enqueue((temp + 10 + 10000)%10000);
                            queue.Enqueue((temp + 90 + 10000)%10000);
                        } else {
                            queue.Enqueue((temp + 10 + 10000)%10000);
                            queue.Enqueue((temp - 10 + 10000)%10000);
                        }
                    }
                    
                    {   // 百位
                        int val = temp / 100 % 10;
                        if (val == 9) {
                            queue.Enqueue((temp - 100 + 10000)%10000);
                            queue.Enqueue((temp - 900 + 10000)%10000);
                        } else if (val == 0) {
                            queue.Enqueue((temp + 100 + 10000)%10000);
                            queue.Enqueue((temp + 900 + 10000)%10000);
                        } else {
                            queue.Enqueue((temp + 100 + 10000)%10000);
                            queue.Enqueue((temp - 100 + 10000)%10000);
                        }
                    }
                    
                     {   // 百位
                        int val = temp / 1000 % 10;
                        if (val == 9) {
                            queue.Enqueue((temp - 1000 + 10000)%10000);
                            queue.Enqueue((temp - 9000 + 10000)%10000);
                        } else if (val == 0) {
                            queue.Enqueue((temp + 1000 + 10000)%10000);
                            queue.Enqueue((temp + 9000 + 10000)%10000);
                        } else {
                            queue.Enqueue((temp + 1000 + 10000)%10000);
                            queue.Enqueue((temp - 1000 + 10000)%10000);
                        }
                    }
                }
            }
            
            count ++;
        } 
        
        return -1;
    }
} 
*/
