using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
返回字符串 text 中按字典序排列最小的子序列，该子序列包含 text 中所有不同字符一次。

 

示例 1：

输入："cdadabcc"
输出："adbc"
示例 2：

输入："abcd"
输出："abcd"
示例 3：

输入："ecbacba"
输出："eacb"
示例 4：

输入："leetcode"
输出："letcod"
 

提示：

1 <= text.length <= 1000
text 由小写英文字母组成
*/
/// <summary>
/// https://leetcode-cn.com/problems/smallest-subsequence-of-distinct-characters/
/// 1081. 不同字符的最小子序列
/// 
/// </summary>
class SmallestSubsequenceOfDistinctCharactersSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public string SmallestSubsequence(string text)
    {
        int len = text.Length;
        var stack = new Stack<char>();
        for (int i = 0; i < len; i++)
        {
            var c = text[i];
            if (stack.Contains(c)) continue;

            while (0 < stack.Count && c < stack.Peek() && -1 < text.IndexOf(stack.Peek(), i) ) stack.Pop();

            stack.Push(c);
        }
        StringBuilder ret = new StringBuilder(stack.Count);
        foreach (var c in stack) ret.Insert(0,c);
        return ret.ToString();
    }
}
/*
栈、位掩码（Python 代码、Java 代码）
liweiwei1419
发布于 8 个月前
1.7k 阅读
首先解释字典序和子序列。

字典序
字典序是指从前到后比较两个字符串大小的方法。

1、首先比较第 1 个字符，如果不同则第 1 个字符较小的字符串更小；
2、如果相同则继续比较第 2 个字符 …… 如此继续，比较整个字符串的大小。

例如 aabc、abac、aacb，它们的字典序为：aabc < aacb < abac。

子序列
1、子序列中各个字符的相对顺序应该与原字符串一致，例如 ca 就不是 aabc 的子序列；
2、并不要求是连续子序列，例如 ac 是 aabc 的一个子序列。

这道题要求我们返回的子序列包含 text 中所有不同字符一次，即要求我们找出的子序列包含的字符是 “不重不漏” 的。

思路分析
那如果那些字典序靠前的字符出现得比较晚该怎么办呢？此时就要看，已经出现过的字符将来还有没有可能出现，如果将来有可能出现，就把前面的字符依次删去，经过这样的流程，得到的子序列就符合题意，这是 贪心算法 的思想，局部最优则全局最优。

下面以例 1 cdadabcc 讲解一下算法的具体执行流程：

（温馨提示：下面的幻灯片中，有几页上有较多的文字，可能需要您停留一下，可以点击右下角的后退 “|◀” 或者前进 “▶|” 按钮控制幻灯片的播放。）



在第 1 步和第 2 步的时候，即在遍历索引为 0 和索引为 1 的字符的时候，字典序 c < d 成立，故 cd 是目前为止得到的字典序最靠前的子序列，这是显然的，关键是 当 a 来了之后，此时 a 前面的 d 是字典序靠后的字符，此时想到有没有可能后面还有 d，看了一眼，果然有 d ，那就把前面的 d 放弃，用同样的方式考察 c，发现后面 c 还有可能出现，因此 c 也被放弃了，此时我们就让字典序最靠前的 a 在最终得到的子序列的最前面（局部最优体现在这里）。

到了第 5 步的时候，虽然 b 的字典序比它前面的 d 要靠前，但此时 d 不会再出现，因此 d 就不能离开当前子序列。

第 7 步，c 在之前的子序列中已经出现过了，就不再考虑。

编码实现
import java.util.Stack;

public class Solution {

    public String smallestSubsequence(String text) {
        int len = text.length();
        Stack<Character> stack = new Stack<>();
        for (int i = 0; i < len; i++) {
            Character c = text.charAt(i);
            if (stack.contains(c)) {
                continue;
            }
            while (!stack.empty() && c < stack.peek() && text.indexOf(stack.peek(), i) != -1) {
                stack.pop();
            }
            stack.push(c);
        }
        StringBuilder sb = new StringBuilder();
        for (Character c : stack) {
            sb.append(c);
        }
        return sb.toString();
    }
}
解释：这里使用 text[i] in stack 看一个字符是不是已经出现过，使用 text.find(stack[-1], i) != -1 看栈顶的那个字符将来会不会出现。这里两个方法都用到了 Python 的库函数。

注意到，题目当中有说到 “text 由小写英文字母组成”，因此字母就一共只有 26 个，上面两个方法都是在判重，因此比较容易想到使用位掩码的技巧。

import java.util.Stack;

public class Solution {

    public String smallestSubsequence(String text) {
        int len = text.length();
        // 从索引 i 到索引 size - 1 的位掩码
        int[] post = new int[len];
        int pre = 0;
        Stack<Character> stack = new Stack<>();
        for (int i = 0; i < len; i++) {
            for (int j = i; j < len; j++) {
                // System.out.println(text.charAt(j));
                post[i] |= (1 << (text.charAt(j) - 'a'));
            }
        }

        // System.out.println(Arrays.toString(post));
        for (int i = 0; i < len; i++) {
            Character c = text.charAt(i);
            if ((pre & (1 << (c - 'a'))) > 0) {
                continue;
            }
            while (!stack.empty() && c < stack.peek() &&
                    (post[i] & (1 << (stack.peek() - 'a'))) != 0) {
                Character top = stack.pop();
                pre ^= (1 << top - 'a');
            }
            pre |= (1 << c - 'a');
            stack.push(c);
        }
        StringBuilder sb = new StringBuilder();
        for (Character c : stack) {
            sb.append(c);
        }
        return sb.toString();
    }
}
下一篇：1081. 不同字符的最小子序列：贪心，单栈实现（7行）/栈哈希实现
 
*/
