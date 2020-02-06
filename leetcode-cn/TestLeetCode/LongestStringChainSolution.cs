using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给出一个单词列表，其中每个单词都由小写英文字母组成。

如果我们可以在 word1 的任何地方添加一个字母使其变成 word2，那么我们认为 word1 是 word2 的前身。例如，"abc" 是 "abac" 的前身。

词链是单词 [word_1, word_2, ..., word_k] 组成的序列，k >= 1，其中 word_1 是 word_2 的前身，word_2 是 word_3 的前身，依此类推。

从给定单词列表 words 中选择单词组成词链，返回词链的最长可能长度。
 

示例：

输入：["a","b","ba","bca","bda","bdca"]
输出：4
解释：最长单词链之一为 "a","ba","bda","bdca"。
 

提示：

1 <= words.length <= 1000
1 <= words[i].length <= 16
words[i] 仅由小写英文字母组成。
*/
/// <summary>
/// https://leetcode-cn.com/problems/longest-string-chain/
/// 1048. 最长字符串链
/// 
/// </summary>
class LongestStringChainSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int LongestStrChain(string[] words)
    {
        // 下面代码只是例子，演示Array.Sort排序，不是算法部分
        Array.Sort(words, Comparer<string>.Create(
            (a, b) => a.Length - b.Length
        ));
        Array.Sort(words, (string s1, string s2) => {
            if (s1.Length > s2.Length) return 1;
            else if (s1.Length == s2.Length) return 0;
            else return -1;
        });
        // 上面代码只是例子，演示Array.Sort排序，不是算法部分

        if (words.Length == 0) return 0;

        int minLength = words[0].Length;
        int maxLength = minLength;
        foreach (string word in words)
        {
            int len = word.Length;
            if (len < minLength) minLength = len;
            if (maxLength < len) maxLength = len;

            if (!map.ContainsKey(len)) map.Add(len, new List<string>());
            map[len].Add(word);
        }
        
        for (int len = minLength; len <= maxLength; len++)
        {
            if (!map.ContainsKey(len)) continue;

            if (maxLength + 1 - len <= Max) break;

            foreach (string current in map[len])
            {
                Dfs(len, current);
            }
        }

        return Max;
    }

    private void Dfs(int startLength, string current)
    {
        Max = Math.Max(Max, current.Length - startLength + 1);

        int key = current.Length + 1;
        if (!map.ContainsKey(key)) return;
        foreach( var one in map[key])
        {
            if (visited.Contains(one)) continue;
            if (Match(current, one))
            {
                visited.Add(one);
                Dfs(startLength, one);
            }
        }
    }

    private static bool Match(string current, string next)
    {
        int i = 0;
        int len = current.Length;
        while (i < len && next[i] == current[i]) i++;

        if (i == len) return true;

        while (i < len && next[i + 1] == current[i]) i++;

        return i == len;
    }

    private int Max = 1;
    private readonly Dictionary<int, List<string>> map = new Dictionary<int, List<string>>();
    private readonly HashSet<string> visited = new HashSet<string>();
}
/*
java解法，深度搜索算法实现：
~~Jausin123
584 阅读
java解法，深度搜索算法实现：

执行用时 :12 ms, 在所有 java 提交中击败了99.50%的用户
内存消耗 :38.2 MB, 在所有 java 提交中击败了100.00%的用户

class Solution {
    private int Max = 1;
    public int longestStrChain(String[] words) {
        if (words.length == 0) return 0;
        int  minlen = words[0].length();
        int maxlen = minlen;
        Map<Integer, Set<String>> map = new HashMap<>();
        for (String word : words) {
            int len = word.length();
            minlen = Math.min(minlen, len);
            maxlen = Math.max(maxlen, len);
            Set<String> set = map.get(len);
            if (set == null) {
                set = new HashSet<>();
                map.put(len, set);
            }
            set.add(word);
        }

        for (int len = minlen; len <= maxlen; len++) {
             Set<String> oneSet = map.get(len);
             if (oneSet == null) break;
             if (maxlen + 1 - len <= Max) break;
             
             for (String a: oneSet) {
                 findNext(map, len, a);
             }
        }

        return Max;
    }

    private void findNext(Map<Integer, Set<String>> map, int start, String base) {
        Max = Math.max(Max, (base.length() + 1) - start);
        Set<String> set = map.get(base.length() + 1);
        if (set == null) {
            return;
        }

        Iterator<String> iterator = set.iterator();
        while (iterator.hasNext()) {
            String one = iterator.next();
            if (match(base, one)) {
                findNext(map, start, one);
                iterator.remove();
            }
        }
    }

    private boolean match(String base, String next) {
        int i = 0;
        int len = base.length();
        while (i < len && next.charAt(i) == base.charAt(i)) {
            i++;
        }
        
        if (i == len) return true;

        while (i < len && next.charAt(i + 1) == base.charAt(i)) {
            i++;
        }
        
        return i == len;
    }
}

public class Solution {
    public int LongestStrChain(string[] words) {
        if(words is null || words.Length == 0) return 0;
        int l = words.Length;
        int[] dp = new int[1001];
        Array.Sort(words,(string s1,string s2)=>{
            if(s1.Length > s2.Length) return 1;
            else if(s1.Length == s2.Length) return 0;
            else return -1;
        });
        int[] len = new int[l];
        for(int i = 0; i < l;i++){
            len[i] = words[i].Length;
        }
        int max = 0;
        for(int i = 0; i < l;i++){
            for(int j = 0; j < l;j++){
                if(i == j || len[i] != len[j]+1) continue;
                if(vaild(words[i],words[j])){
                    dp[i] = dp[j]+1;
                    if(dp[i] > max) max = dp[i];
                }
            }
        }
        return max+1;
    }

    public bool vaild(string w1,string w2){
        int i = 0;
        bool re = false;
        while(i<w2.Length){
            if(!re && w1[i] == w2[i]) i++;
            else if(re && w1[i+1] == w2[i]) i++;
            else if(re) return false;
            else{
                re = true;
            }
        }
        return true;
    }
}

public class Solution {
    public int LongestStrChain(string[] words) {
        int[] dp = new int[words.Length];
        Array.Fill(dp,1);
        int ans = 0;
        Array.Sort(words, Comparer<string>.Create(
            (a,b) => a.Length-b.Length
        ));
        for(int i = 0;i<words.Length-1;i++){
            for(int j = i+1;j<words.Length;j++){
                if(isValid(words[i],words[j]))
                    dp[j] = Math.Max(dp[j],dp[i]+1);
                ans = Math.Max(ans,dp[j]);
            }
        }
        return ans;
    }

    public bool isValid(string a, string b){
        if(b.Length - a.Length != 1)
            return false;

        int cnt = 0;
        int j = 0;
        for(int i = 0;i<b.Length;i++){
            if(j>=a.Length || b[i] != a[j]){
                cnt++;
            }
            else
                j++;
            if(cnt >1)
                return false;
        }

        return true;


    }
}

详细动态规划解法，24ms，简单易懂
Ripple
693 阅读
分析：
动态规划题。
首先先把数组按照每一个字符串的长度排一下序，这样为了词链短的一定在前面，而后面的则可能有更长的词链。
然后我们再来看题目所说的前身，我认为就是前者是后者的子序列并且前后之间的长度就相差1
那么这样就很简单了，LeetCode有判断子序列的题(见392题)。
那么现在的问题是我要怎找状态，其实很简单。
状态就是当前位置字符串的词链长度，而转移方程就是从当前字符串长度-1的字符串中去找哪些符合子序列，符合就将它的词链长度+1和自己比较。
那么又出现了另一个问题，就是怎么找到长度为当前字符串-1的字符串。
我自己的方法是开辟一个长度17的数组(因为最长字符串为16),然后把它们的下标放在它们对应的长度位置上，覆盖之前的下标。
那么这样就能找到任意一个字符串长度的最后一个位置下标，由于数组按长度排序，那么它之前的也都是和它长度一样的字符串。
另一种方法是用Hash表，把长度一样的放在一起。
另外：由于我实在不会用Java的用关键字排序，所以自己实现了一个按字符串长度的快速排序，速度还不错。
如果有谁知道java怎么按照关键字排序，麻烦告诉我一声。。。。
代码：
class Solution {
    public int longestStrChain(String[] words) {
        int len = words.length;
        quickSort(words, 0, len - 1);
        int[] cnt = new int[17];
        Arrays.fill(cnt, -1);//-1就是不存在这样长度的字符串。
        for (int i = 0; i < len; i++) cnt[words[i].length()] = i;//存储长度一样的字符串最后那个的下标
        int[] dp = new int[len];
        Arrays.fill(dp, 1);//每个单词的词链至少也为1
        int ans = 1;
        for (int i = 0; i < len; i++) {
            int tarlen = words[i].length() - 1;//当前长度为words[i].length()，我要找比它小1的长度。
            int index = cnt[tarlen];//取出长度一样的最后那个下标
            if (index == -1) {//取不到说明没有这个长度的字符串，那么当前字符串的词链只可能是1。
                dp[i] = 1;
            } else {
                while (index >= 0 && words[index].length() == tarlen) {//长度一样的都检查一遍是不是子序列，注意下标不要越界。
                    if (check(words[index], words[i])) dp[i] = Math.max(dp[i], dp[index] + 1);
                    --index;
                }
            }
            ans = Math.max(ans, dp[i]);
        }
        return ans;
    }

    public boolean check(String s1, String s2) {//检查s1是否是s2的子序列，详情看392题
        int index = -1;
        for (char ch : s1.toCharArray()) {
            index = s2.indexOf(ch, index + 1);
            if (index == -1) return false;
        }
        return true;
    }

    public void quickSort(String[] A, int st, int en) {//快速排序，没什么好说的。
        if (st < en) {
            int point = partition(A, st, en);
            quickSort(A, st, point - 1);
            quickSort(A, point + 1, en);
        }
    }

    public int partition(String[] A, int st, int en) {
        Random r = new Random();
        int pivot = r.nextInt(en - st) + st + 1;
        String tmp = A[pivot];
        A[pivot] = A[en];A[en] = tmp;
        pivot = A[en].length();
        int i = st - 1;
        for (int j = st; j < en; j++) {
            if (A[j].length() < pivot) {
                tmp = A[j];
                A[j] = A[++i];
                A[i] = tmp;
            }
        }
        tmp = A[i+1];
        A[i+1] = A[en];
        A[en] = tmp;
        return i + 1;
    }
}
下一篇：python，双100%
 
*/
