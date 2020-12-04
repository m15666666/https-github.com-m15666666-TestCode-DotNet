using System.Collections.Generic;

/*
给定一组 互不相同 的单词， 找出所有不同 的索引对(i, j)，使得列表中的两个单词， words[i] + words[j] ，可拼接成回文串。

 

示例 1：

输入：["abcd","dcba","lls","s","sssll"]
输出：[[0,1],[1,0],[3,2],[2,4]]
解释：可拼接成的回文串为 ["dcbaabcd","abcddcba","slls","llssssll"]
示例 2：

输入：["bat","tab","cat"]
输出：[[0,1],[1,0]]
解释：可拼接成的回文串为 ["battab","tabbat"]

*/

/// <summary>
/// https://leetcode-cn.com/problems/palindrome-pairs/
/// 336. 回文对
///
///
///
///
/// </summary>
internal class PalindromePairsSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public IList<IList<int>> PalindromePairs(string[] words)
    {
        _tree.Add(new Node());
        int n = words.Length;
        for (int i = 0; i < n; i++) Insert(words[i], i);

        var ret = new List<IList<int>>();
        for (int i = 0; i < n; i++)
        {
            var word = words[i];
            int m = word.Length;
            for (int j = 0; j <= m; j++)
            {
                if (IsPalindrome(word, j, m - 1))
                {
                    int leftId = FindWord(word, 0, j - 1);
                    if (leftId != -1 && leftId != i) ret.Add(new int[] { i, leftId });
                }
                if (j != 0 && IsPalindrome(word, 0, j - 1))
                {
                    int rightId = FindWord(word, j, m - 1);
                    if (rightId != -1 && rightId != i) ret.Add(new int[] { rightId, i });
                }
            }
        }
        return ret;
    }

    private void Insert(string s, int id)
    {
        int len = s.Length, nodeIndex = 0;
        for (int i = 0; i < len; i++)
        {
            int x = s[i] - 'a';
            if (_tree[nodeIndex].Char2Node[x] == 0)
            {
                _tree.Add(new Node());
                _tree[nodeIndex].Char2Node[x] = _tree.Count - 1;
            }
            nodeIndex = _tree[nodeIndex].Char2Node[x];
        }
        _tree[nodeIndex].Id = id;
    }

    private static bool IsPalindrome(string s, int left, int right)
    {
        int len = right - left + 1;
        for (int i = 0; i < len / 2; i++)
            if (s[left + i] != s[right - i]) return false;
        return true;
    }

    private int FindWord(string s, int left, int right)
    {
        int nodeIndex = 0;
        for (int i = right; i >= left; i--)
        {
            int x = s[i] - 'a';
            if (_tree[nodeIndex].Char2Node[x] == 0) return -1;
            nodeIndex = _tree[nodeIndex].Char2Node[x];
        }
        return _tree[nodeIndex].Id;
    }

    private readonly List<Node> _tree = new List<Node>();
    public class Node
    {
        public int[] Char2Node = new int[26];
        public int Id = -1;
    }
}

/*
回文对
力扣官方题解
发布于 2020-08-05
29.3k
写在前面
本题可以想到暴力做法，我们枚举每一对字符串的组合，暴力判断它们是否能够构成回文串即可。时间复杂度 O(n^2\times m)O(n 
2
 ×m)，其中 nn 是字符串的数量，mm 是字符串的平均长度。时间复杂度并不理想，考虑进行优化。

方法一：枚举前缀和后缀
思路及算法

假设存在两个字符串 s_1s 
1
​	
  和 s_2s 
2
​	
 ，s_1+s_2s 
1
​	
 +s 
2
​	
  是一个回文串，记这两个字符串的长度分别为 len_1len 
1
​	
  和 len_2len 
2
​	
 ，我们分三种情况进行讨论：

\textit{len}_1 = \textit{len}_2len 
1
​	
 =len 
2
​	
 ，这种情况下 s_1s 
1
​	
  是 s_2s 
2
​	
  的翻转。
\textit{len}_1 > \textit{len}_2len 
1
​	
 >len 
2
​	
 ，这种情况下我们可以将 s_1s 
1
​	
  拆成左右两部分：t_1t 
1
​	
  和 t_2t 
2
​	
 ，其中 t_1t 
1
​	
  是 s_2s 
2
​	
  的翻转，t_2t 
2
​	
  是一个回文串。
\textit{len}_1 < \textit{len}_2len 
1
​	
 <len 
2
​	
 ，这种情况下我们可以将 s_2s 
2
​	
  拆成左右两部分：t_1t 
1
​	
  和 t_2t 
2
​	
 ，其中 t_2t 
2
​	
  是 s_1s 
1
​	
  的翻转，t_1t 
1
​	
  是一个回文串。
这样，对于每一个字符串，我们令其为 s_1s 
1
​	
  和 s_2s 
2
​	
  中较长的那一个，然后找到可能和它构成回文串的字符串即可。

具体地说，我们枚举每一个字符串 kk，令其为 s_1s 
1
​	
  和 s_2s 
2
​	
  中较长的那一个，那么 kk 可以被拆分为两部分，t_1t 
1
​	
  和 t_2t 
2
​	
 。

当 t_1t 
1
​	
  是回文串时，符合情况 33，我们只需要查询给定的字符串序列中是否包含 t_2t 
2
​	
  的翻转。
当 t_2t 
2
​	
  是回文串时，符合情况 22，我们只需要查询给定的字符串序列中是否包含 t_1t 
1
​	
  的翻转。
也就是说，我们要枚举字符串 kk 的每一个前缀和后缀，判断其是否为回文串。如果是回文串，我们就查询其剩余部分的翻转是否在给定的字符串序列中出现即可。

注意到空串也是回文串，所以我们可以将 kk 拆解为 k+\varnothingk+∅ 或 \varnothing+k∅+k，这样我们就能将情况 11 也解释为特殊的情况 22 或情况 33。

而要实现这些操作，我们只需要设计一个能够在一系列字符串中查询「某个字符串的子串的翻转」是否存在的数据结构，有两种实现方法：

我们可以使用字典树存储所有的字符串。在进行查询时，我们将待查询串的子串逆序地在字典树上进行遍历，即可判断其是否存在。

我们可以使用哈希表存储所有字符串的翻转串。在进行查询时，我们判断带查询串的子串是否在哈希表中出现，就等价于判断了其翻转是否存在。

代码

下面给出的是使用字典树的代码：


class Solution {
    class Node {
        int[] ch = new int[26];
        int flag;

        public Node() {
            flag = -1;
        }
    }

    List<Node> tree = new ArrayList<Node>();

    public List<List<Integer>> palindromePairs(String[] words) {
        tree.add(new Node());
        int n = words.length;
        for (int i = 0; i < n; i++) {
            insert(words[i], i);
        }
        List<List<Integer>> ret = new ArrayList<List<Integer>>();
        for (int i = 0; i < n; i++) {
            int m = words[i].length();
            for (int j = 0; j <= m; j++) {
                if (isPalindrome(words[i], j, m - 1)) {
                    int leftId = findWord(words[i], 0, j - 1);
                    if (leftId != -1 && leftId != i) {
                        ret.add(Arrays.asList(i, leftId));
                    }
                }
                if (j != 0 && isPalindrome(words[i], 0, j - 1)) {
                    int rightId = findWord(words[i], j, m - 1);
                    if (rightId != -1 && rightId != i) {
                        ret.add(Arrays.asList(rightId, i));
                    }
                }
            }
        }
        return ret;
    }

    public void insert(String s, int id) {
        int len = s.length(), add = 0;
        for (int i = 0; i < len; i++) {
            int x = s.charAt(i) - 'a';
            if (tree.get(add).ch[x] == 0) {
                tree.add(new Node());
                tree.get(add).ch[x] = tree.size() - 1;
            }
            add = tree.get(add).ch[x];
        }
        tree.get(add).flag = id;
    }

    public boolean isPalindrome(String s, int left, int right) {
        int len = right - left + 1;
        for (int i = 0; i < len / 2; i++) {
            if (s.charAt(left + i) != s.charAt(right - i)) {
                return false;
            }
        }
        return true;
    }

    public int findWord(String s, int left, int right) {
        int add = 0;
        for (int i = right; i >= left; i--) {
            int x = s.charAt(i) - 'a';
            if (tree.get(add).ch[x] == 0) {
                return -1;
            }
            add = tree.get(add).ch[x];
        }
        return tree.get(add).flag;
    }
}
下面给出的是使用哈希表的代码：


class Solution {
    List<String> wordsRev = new ArrayList<String>();
    Map<String, Integer> indices = new HashMap<String, Integer>();

    public List<List<Integer>> palindromePairs(String[] words) {
        int n = words.length;
        for (String word: words) {
            wordsRev.add(new StringBuffer(word).reverse().toString());
        }
        for (int i = 0; i < n; ++i) {
            indices.put(wordsRev.get(i), i);
        }

        List<List<Integer>> ret = new ArrayList<List<Integer>>();
        for (int i = 0; i < n; i++) {
            String word = words[i];
            int m = words[i].length();
            if (m == 0) {
                continue;
            }
            for (int j = 0; j <= m; j++) {
                if (isPalindrome(word, j, m - 1)) {
                    int leftId = findWord(word, 0, j - 1);
                    if (leftId != -1 && leftId != i) {
                        ret.add(Arrays.asList(i, leftId));
                    }
                }
                if (j != 0 && isPalindrome(word, 0, j - 1)) {
                    int rightId = findWord(word, j, m - 1);
                    if (rightId != -1 && rightId != i) {
                        ret.add(Arrays.asList(rightId, i));
                    }
                }
            }
        }
        return ret;
    }

    public boolean isPalindrome(String s, int left, int right) {
        int len = right - left + 1;
        for (int i = 0; i < len / 2; i++) {
            if (s.charAt(left + i) != s.charAt(right - i)) {
                return false;
            }
        }
        return true;
    }

    public int findWord(String s, int left, int right) {
        return indices.getOrDefault(s.substring(left, right + 1), -1);
    }
}
复杂度分析

时间复杂度：O(n \times m^2)O(n×m 
2
 )，其中 nn 是字符串的数量，mm 是字符串的平均长度。对于每一个字符串，我们需要 O(m^2)O(m 
2
 ) 地判断其所有前缀与后缀是否是回文串，并 O(m^2)O(m 
2
 ) 地寻找其所有前缀与后缀是否在给定的字符串序列中出现。

空间复杂度：O(n \times m)O(n×m)，其中 nn 是字符串的数量，mm 是字符串的平均长度。为字典树的空间开销。

方法二：字典树 + manacher
说明

方法二为竞赛难度，在面试中不作要求。学有余力的读者可以学习在字符串中寻找最长回文串的「manacher 算法」。

思路及算法

注意到方法一中，对于每一个字符串 kk，我们需要 O(m^2)O(m 
2
 ) 地判断 kk 的所有前缀与后缀是否是回文串，还需要 O(m^2)O(m 
2
 ) 地判断 kk 的所有前缀与后缀是否在给定字符串序列中出现。我们可以优化这两部分的时间复杂度。

对于判断其所有前缀与后缀是否是回文串：

利用 manacher 算法，可以线性地处理出每一个前后缀是否是回文串。
对于判断其所有前缀与后缀是否在给定的字符串序列中出现：

对于给定的字符串序列，分别正向与反向建立字典树，利用正向建立的字典树验证 kk 的后缀的翻转，利用反向建立的字典树验证 kk 的前缀的翻转。
这样我们就可以快速找出能够和字符串 kk 构成回文串的字符串。

注意：因为该解法常数较大，因此在随机数据下的表现并没有方法一优秀。

代码


class Solution {
    public List<List<Integer>> palindromePairs(String[] words) {
        Trie trie1 = new Trie();
        Trie trie2 = new Trie();

        int n = words.length;
        for (int i = 0; i < n; i++) {
            trie1.insert(words[i], i);
            StringBuffer tmp = new StringBuffer(words[i]);
            tmp.reverse();
            trie2.insert(tmp.toString(), i);
        }

        List<List<Integer>> ret = new ArrayList<List<Integer>>();
        for (int i = 0; i < n; i++) {
            int[][] rec = manacher(words[i]);

            int[] id1 = trie2.query(words[i]);
            words[i] = new StringBuffer(words[i]).reverse().toString();
            int[] id2 = trie1.query(words[i]);

            int m = words[i].length();

            int allId = id1[m];
            if (allId != -1 && allId != i) {
                ret.add(Arrays.asList(i, allId));
            }
            for (int j = 0; j < m; j++) {
                if (rec[j][0] != 0) {
                    int leftId = id2[m - j - 1];
                    if (leftId != -1 && leftId != i) {
                        ret.add(Arrays.asList(leftId, i));
                    }
                }
                if (rec[j][1] != 0) {
                    int rightId = id1[j];
                    if (rightId != -1 && rightId != i) {
                        ret.add(Arrays.asList(i, rightId));
                    }
                }
            }
        }
        return ret;
    }

    public int[][] manacher(String s) {
        int n = s.length();
        StringBuffer tmp = new StringBuffer("#");
        for (int i = 0; i < n; i++) {
            if (i > 0) {
                tmp.append('*');
            }
            tmp.append(s.charAt(i));
        }
        tmp.append('!');
        int m = n * 2;
        int[] len = new int[m];
        int[][] ret = new int[n][2];
        int p = 0, maxn = -1;
        for (int i = 1; i < m; i++) {
            len[i] = maxn >= i ? Math.min(len[2 * p - i], maxn - i) : 0;
            while (tmp.charAt(i - len[i] - 1) == tmp.charAt(i + len[i] + 1)) {
                len[i]++;
            }
            if (i + len[i] > maxn) {
                p = i;
                maxn = i + len[i];
            }
            if (i - len[i] == 1) {
                ret[(i + len[i]) / 2][0] = 1;
            }
            if (i + len[i] == m - 1) {
                ret[(i - len[i]) / 2][1] = 1;
            }
        }
        return ret;
    }
}

class Trie {
    class Node {
        int[] ch = new int[26];
        int flag;

        public Node() {
            flag = -1;
        }
    }

    List<Node> tree = new ArrayList<Node>();

    public Trie() {
        tree.add(new Node());
    }

    public void insert(String s, int id) {
        int len = s.length(), add = 0;
        for (int i = 0; i < len; i++) {
            int x = s.charAt(i) - 'a';
            if (tree.get(add).ch[x] == 0) {
                tree.add(new Node());
                tree.get(add).ch[x] = tree.size() - 1;
            }
            add = tree.get(add).ch[x];
        }
        tree.get(add).flag = id;
    }

    public int[] query(String s) {
        int len = s.length(), add = 0;
        int[] ret = new int[len + 1];
        Arrays.fill(ret, -1);
        for (int i = 0; i < len; i++) {
            ret[i] = tree.get(add).flag;
            int x = s.charAt(i) - 'a';
            if (tree.get(add).ch[x] == 0) {
                return ret;
            }
            add = tree.get(add).ch[x];
        }
        ret[len] = tree.get(add).flag;
        return ret;
    }
}
复杂度分析

时间复杂度：O(n \times m)O(n×m)，其中 nn 是字符串的数量，mm 是字符串的平均长度。对于每一个字符串，我们需要 O(m)O(m) 地判断其所有前缀与后缀是否是回文串，并 O(m)O(m) 地寻找其所有前缀与后缀是否在给定的字符串序列中出现。

空间复杂度：O(n \times m)O(n×m)，其中 nn 是字符串的数量，mm 是字符串的平均长度。为字典树的空间开销。

public class Solution {
    public IList<IList<int>> PalindromePairs(string[] words) {
		var indexByReverseDic = new Dictionary<string, int>();
		for (int i = 0; i < words.Length; i++)
		{
			indexByReverseDic.Add(string.Join(string.Empty, words[i].Reverse()), i);
		}
		var result = new List<IList<int>>();
		for (int i = 0; i < words.Length; i++)
		{
			string word = words[i];
			if (string.IsNullOrEmpty(word)) continue;
			int wordLength = word.Length;
			for (int j = 0; j < wordLength + 1; j++)
			{
				if (IsPalindrome(word.Substring(0, j)) && indexByReverseDic.ContainsKey(word.Substring(j, wordLength - j)))
				{
					var pos = indexByReverseDic[word.Substring(j, wordLength - j)];
					if (pos != i)
					{
						result.Add(new[] { pos , i});
					}
				}
			}

			for (int j = 0; j < wordLength; j++)
			{
				if (IsPalindrome(word.Substring(j)) && indexByReverseDic.ContainsKey(word.Substring(0, j)))
				{
					var pos = indexByReverseDic[word.Substring(0, j)];
					if (pos != i)
					{
						result.Add(new[] { i, pos });
					}
				}
			}
		}

		return result;
    }

	private bool IsPalindrome(string word)
	{
		int length = word.Length;
		int n = (word.Length + 1) / 2;
		for (int i = 0; i < n; i++)
		{
			if (word[i] != word[length - i - 1])
			{
				return false;
			}
		}

		return true;
	}
}


*/