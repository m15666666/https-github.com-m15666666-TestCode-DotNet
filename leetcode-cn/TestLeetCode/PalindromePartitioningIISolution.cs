/*
给定一个字符串 s，将 s 分割成一些子串，使每个子串都是回文串。

返回符合要求的最少分割次数。

示例:

输入: "aab"
输出: 1
解释: 进行一次分割就可将 s 分割成 ["aa","b"] 这样两个回文子串。

*/

/// <summary>
/// https://leetcode-cn.com/problems/palindrome-partitioning-ii/
/// 132.分隔回文串 II
///
///
/// </summary>
internal class PalindromePartitioningIISolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int MinCut(string s)
    {
        if (string.IsNullOrEmpty(s)) return 0;

        int length = s.Length;
        if (length < 2) return 0;

        var checkPalindrome = new bool[length, length];
        for (int right = 0; right < length; right++)
            for (int left = 0; left <= right; left++)
                if (s[left] == s[right] && (right - left <= 2 || checkPalindrome[left + 1, right - 1]))
                    checkPalindrome[left, right] = true;

        int[] dp = new int[length];
        for (int i = 0; i < length; i++) dp[i] = i;

        for (int i = 1; i < length; i++)
        {
            if (checkPalindrome[0, i])
            {
                dp[i] = 0;
                continue;
            }

            for (int j = 0; j < i; j++)
                if (checkPalindrome[j + 1, i]) dp[i] = System.Math.Min(dp[i], dp[j] + 1);
        }
        return dp[length - 1];
    }
}

/*
动态规划（Java、Python）
liweiwei1419
发布于 2019-12-12
7.4k
方法一：动态规划
步骤 1：思考状态
状态就尝试定义成题目问的那样，看看状态转移方程是否容易得到。

dp[i]：表示前缀子串 s[0:i] 分割成若干个回文子串所需要最小分割次数。

步骤 2：思考状态转移方程
思考的方向是：大问题的最优解怎么由小问题的最优解得到。

即 dp[i] 如何与 dp[i - 1]、dp[i - 2]、...、dp[0] 建立联系。

比较容易想到的是：如果 s[0:i] 本身就是一个回文串，那么不用分割，即 dp[i] = 0 ，这是首先可以判断的，否则就需要去遍历；

接下来枚举可能分割的位置：即如果 s[0:i] 本身不是一个回文串，就尝试分割，枚举分割的边界 j。

如果 s[j + 1, i] 不是回文串，尝试下一个分割边界。

如果 s[j + 1, i] 是回文串，则 dp[i] 就是在 dp[j] 的基础上多一个分割。

于是枚举 j 所有可能的位置，取所有 dp[j] 中最小的再加 1 ，就是 dp[i]。

得到状态转移方程如下：


dp[i] = min([dp[j] + 1 for j in range(i) if s[j + 1, i] 是回文])
image.png

步骤 3：思考初始状态
初始状态：单个字符一定是回文串，因此 dp[0] = 0。

步骤 4：思考输出
状态转移方程可以得到，并且状态就是题目问的，因此返回最后一个状态即可，即 dp[len - 1]。

步骤 5：思考是否可以优化空间
每一个状态值都与之前的状态值有关，因此不能优化空间。

参考代码 1： （Python 代码会超时）


public class Solution {

    public int minCut(String s) {
        int len = s.length();
        if (len < 2) {
            return 0;
        }

        int[] dp = new int[len];
        for (int i = 0; i < len; i++) {
            dp[i] = i;
        }

        for (int i = 1; i < len; i++) {
            if (checkPalindrome(s, 0, i)) {
                dp[i] = 0;
                continue;
            }
            // 当 j == i 成立的时候，s[i] 就一个字符，一定是回文
            // 因此，枚举到 i - 1 即可
            for (int j = 0; j < i; j++) {
                if (checkPalindrome(s, j + 1, i)) {
                    dp[i] = Math.min(dp[i], dp[j] + 1);
                }
            }
        }
        return dp[len - 1];
    }

    private boolean checkPalindrome(String s, int left, int right) {
        while (left < right) {
            if (s.charAt(left) != s.charAt(right)) {
                return false;
            }
            left++;
            right--;
        }
        return true;
    }
}
Python 代码在面对输入：


"aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaabbaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"
的时候超时。

方法二：动态规划（优化）
上面判断回文串的时候方法 checkPalindrome() 是线性的，时间复杂度为 O(N)O(N)。我们可以借助「力扣」第 5 题：最长回文子串 的做法，依然是使用动态规划的做法，得到一个预处理的动态规划数组，这样就可以通过 O(1)O(1) 的时间复杂度，得到一个子串是否是回文的结果了。

参考代码 2：


public class Solution {

    public int minCut(String s) {
        int len = s.length();
        // 特判
        if (len < 2) {
            return 0;
        }

        // 状态定义：dp[i]：前缀子串 s[0:i] （包括索引 i 处的字符）符合要求的最少分割次数
        // 状态转移方程：
        // dp[i] = min(dp[j] + 1 if s[j + 1: i] 是回文 for j in range(i))

        int[] dp = new int[len];
        // 2 个字符最多分割 1 次；
        // 3 个字符最多分割 2 次
        // 初始化的时候，设置成为这个最多分割次数

        for (int i = 0; i < len; i++) {
            dp[i] = i;
        } 	

        // 参考「力扣」第 5 题：最长回文子串 动态规划 的解法
        boolean[][] checkPalindrome = new boolean[len][len];
        for (int right = 0; right < len; right++) {
            // 注意：left <= right 取等号表示 1 个字符的时候也需要判断
            for (int left = 0; left <= right; left++) {
                if (s.charAt(left) == s.charAt(right) && (right - left <= 2 || checkPalindrome[left + 1][right - 1])) {
                    checkPalindrome[left][right] = true;
                }
            }
        }

        // 1 个字符的时候，不用判断，因此 i 从 1 开始
        for (int i = 1; i < len; i++) {
            if (checkPalindrome[0][i]){
                dp[i] = 0;
                continue;
            }

            // 注意：这里是严格，要保证 s[j + 1:i] 至少得有一个字符串
            for (int j = 0; j < i; j++) {
                if (checkPalindrome[j + 1][i]) {
                    dp[i] = Math.min(dp[i], dp[j] + 1);
                }
            }
        }
        return dp[len - 1];
    }
}
下一篇：纯C，动态规划+DFS(优化)，【132.分割回文子串II】【思路清晰，代码易读】

public class Solution {
    public int MinCut (string s) {
        if (string.IsNullOrEmpty (s)) {
            return 0;
        }

        int total = s.Length;

        int[] dp = new int[total];
        Array.Fill (dp, total - 1);

        for (int i = 0; i < total; i++) {
            Core (s, i, i, dp);
            Core (s, i, i + 1, dp);
        }

        return dp[total - 1];
    }

    private static void Core (string s, int i, int j, int[] dp) {
        var len = s.Length;
        while (i >= 0 && j < len && s[i] == s[j]) {
            dp[j] = Math.Min (dp[j], (i == 0 ? -1 : dp[i - 1]) + 1);
            i--;
            j++;
        }
    }
}

public class Solution {
    public int MinCut(string s) {
        char[] c=s.ToCharArray();
        int len=c.Length;
        int[] min=new int[len];
        bool[,] dp=new bool[len,len];
        for(int i=0;i<len;++i){
            min[i]=i;
            for(int j=0;j<=i;++j){
                if(c[j]==c[i] && (i-j<=1 || dp[j+1,i-1])){
                    dp[j,i]=true;
                    min[i]=j!=0?Math.Min(min[i],min[j-1]+1):0;
                }
            }
        }
        return min[len-1];
    }
}


*/