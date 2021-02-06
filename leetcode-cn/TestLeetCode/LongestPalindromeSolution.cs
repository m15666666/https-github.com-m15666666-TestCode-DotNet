/*
给定一个包含大写字母和小写字母的字符串，找到通过这些字母构造成的最长的回文串。

在构造过程中，请注意区分大小写。比如 "Aa" 不能当做一个回文字符串。

注意:
假设字符串的长度不会超过 1010。

示例 1:

输入:
"abccccdd"

输出:
7

解释:
我们可以构造的最长的回文串是"dccaccd", 它的长度是 7。

*/

/// <summary>
/// https://leetcode-cn.com/problems/longest-palindrome/
/// 409. 最长回文串
///
/// </summary>
internal class LongestPalindromeSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int LongestPalindrome(string s)
    {
        int[] counts = new int[58];
        foreach (char c in s) counts[c - 'A']++;

        int ret = 0;
        foreach (int x in counts) ret += x - (x & 1);
        return ret < s.Length ? ret + 1 : ret;
    }
}

/*
最长回文串
力扣官方题解
发布于 2020-03-18
38.2k
📺 视频题解

📖 文字题解
方法一：贪心
思路

回文串是一个正着读和反着读都一样的字符串。以回文中心为分界线，对于回文串中左侧的字符 ch，在右侧对称的位置也会出现同样的字符。例如在字符串 "abba" 中，回文中心是 "ab|ba" 中竖线的位置，而在字符串 "abcba" 中，回文中心是 "ab(c)ba" 中的字符 "c" 本身。我们可以发现，在一个回文串中，只有最多一个字符出现了奇数次，其余的字符都出现偶数次。

那么我们如何通过给定的字符构造一个回文串呢？我们可以将每个字符使用偶数次，使得它们根据回文中心对称。在这之后，如果有剩余的字符，我们可以再取出一个，作为回文中心。

算法

对于每个字符 ch，假设它出现了 v 次，我们可以使用该字符 v / 2 * 2 次，在回文串的左侧和右侧分别放置 v / 2 个字符 ch，其中 / 为整数除法。例如若 "a" 出现了 5 次，那么我们可以使用 "a" 的次数为 4，回文串的左右两侧分别放置 2 个 "a"。

如果有任何一个字符 ch 的出现次数 v 为奇数（即 v % 2 == 1），那么可以将这个字符作为回文中心，注意只能最多有一个字符作为回文中心。在代码中，我们用 ans 存储回文串的长度，由于在遍历字符时，ans 每次会增加 v / 2 * 2，因此 ans 一直为偶数。但在发现了第一个出现次数为奇数的字符后，我们将 ans 增加 1，这样 ans 变为奇数，在后面发现其它出现奇数次的字符时，我们就不改变 ans 的值了。


class Solution {
    public int longestPalindrome(String s) {
        int[] count = new int[128];
        int length = s.length();
        for (int i = 0; i < length; ++i) {
            char c = s.charAt(i);
            count[c]++;
        }

        int ans = 0;
        for (int v: count) {
            ans += v / 2 * 2;
            if (v % 2 == 1 && ans % 2 == 0) {
                ans++;
            }
        }
        return ans;
    }
}
复杂度分析

时间复杂度：O(N)O(N)，其中 NN 为字符串 s 的长度。我们需要遍历每个字符一次。

空间复杂度：O(S)O(S)，其中 SS 为字符集大小。在 Java 代码中，我们使用了一个长度为 128 的数组，存储每个字符出现的次数，这是因为字符的 ASCII 值的范围为 [0, 128)。而由于题目中保证了给定的字符串 s 只包含大小写字母，因此我们也可以使用哈希映射（HashMap）来存储每个字符出现的次数，例如 Python 和 C++ 的代码。如果使用哈希映射，最多只会存储 52 个（即小写字母与大写字母的数量之和）键值对。

Java的2种实现方法~
Sweetiee 🍬
发布于 2020-03-19
9.2k
UPDATE: 米娜桑下午好～～ 刚刚我在公众号【甜姨的奇妙冒险】更新了本题更详细的题解，很多小tips等你来取嗷～～，欢迎大家围观❤️

-----s

🙋今日打卡~因为写了2种风格的代码所以慢了点~
这题其实是构造性的题目，所以只需要尽可能的左右对称地构造字符串就行了，所以回文串里每种字符都出现了偶数次，除了奇数长度的回文串的时候最中间的那个字符可以出现奇数次。
比如回文串 abba，每个字符都出现了偶数次。而奇数长度的回文串abcbcbcba，c出现了奇数次。

先是利用int数组计数的方法：


class Solution {
    public int longestPalindrome(String s) {
      int[] cnt = new int[58];
      for (char c : s.toCharArray()) {
        cnt[c - 'A'] += 1;
      }

      int ans = 0;
      for (int x: cnt) {
        // 字符出现的次数最多用偶数次。
        ans += x - (x & 1);
      }
      // 如果最终的长度小于原字符串的长度，说明里面某个字符出现了奇数次，那么那个字符可以放在回文串的中间，所以额外再加一。
      return ans < s.length() ? ans + 1 : ans;  
    }
}
然后可以用Java8的流式风格来写，好像是在小数据集上用stream会比较慢，这样写的耗时会长一点。
可以学习下stream的写法~

public class Solution {
    public int LongestPalindrome(string s) {
        if(string.IsNullOrEmpty(s)) return 0;
        if(s.Length == 1) return 1;
        var dic = new Dictionary<char,int>();
        foreach(var item in s)
        {
            if(dic.ContainsKey(item)) dic[item]++;
            else dic.Add(item,1);
        }

        var sum = 0;
        var hasSingle = false;
        foreach(var item in dic)
        {
            var value = item.Value / 2 * 2;         
            sum += value;
            if(!hasSingle && item.Value != value) hasSingle = true;           
        }

        return hasSingle ? sum + 1 : sum ;
    }
}



class Solution {
    public int longestPalindrome(String s) {
      Map<Integer, Integer> count = s.chars().boxed()
            .collect(Collectors.toMap(k -> k, v -> 1, Integer::sum));

      int ans = count.values().stream().mapToInt(i -> i - (i & 1)).sum();
      return ans < s.length() ? ans + 1 : ans;
    }
}
*/