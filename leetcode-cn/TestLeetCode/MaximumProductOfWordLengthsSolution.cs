using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
给定一个字符串数组 words，找到 length(word[i]) * length(word[j]) 的最大值，并且这两个单词不含有公共字母。你可以认为每个单词只包含小写字母。如果不存在这样的两个单词，返回 0。

示例 1:

输入: ["abcw","baz","foo","bar","xtfn","abcdef"]
输出: 16 
解释: 这两个单词为 "abcw", "xtfn"。
示例 2:

输入: ["a","ab","abc","d","cd","bcd","abcd"]
输出: 4 
解释: 这两个单词为 "ab", "cd"。
示例 3:

输入: ["a","aa","aaa","aaaa"]
输出: 0 
解释: 不存在这样的两个单词。 
*/
/// <summary>
/// https://leetcode-cn.com/problems/maximum-product-of-word-lengths/
/// 318. 最大单词长度乘积
/// 
/// </summary>
class MaximumProductOfWordLengthsSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int MaxProduct(string[] words)
    {
        if (words == null || words.Length < 2) return 0;
        int ret = 0;
        var wordDescriptions = words.Select(word => new WordDescription(word)).OrderByDescending(wordDescription => wordDescription.Length).ToArray();
        bool containSameChar = false;
        for (int i = 0; i < wordDescriptions.Length - 1; i++)
        {
            var chars = wordDescriptions[i].Chars;
            for (int j = i + 1; j < wordDescriptions.Length; j++)
            {
                if ((chars & wordDescriptions[j].Chars) == 0)
                {
                    var len = wordDescriptions[i].Length * wordDescriptions[j].Length;
                    if (!containSameChar) return len;
                    if (ret < len) ret = len;
                }
                else containSameChar = true;
            }
        }
        return ret;
    }
    class WordDescription
    {
        public int Length { get; set; }
        public int Chars { get; set; }
        public WordDescription( string word )
        {
            if (word == null) return;
            Length = word.Length;
            foreach( var c in word)
            {
                var offset = c - 'a';
                Chars |= (1 << offset);
            }
        }
    }
}
/*
最大单词长度乘积
力扣 (LeetCode)
发布于 2020-02-17
5.5k
综述
从简单方法开始慢慢深入。

将每个单词与其他所有单词一一比较。如果两个单词没有公共字母，则更新 maxProd。

先不考虑实现方法 noCommonLetters，该解法的代码如下：


class Solution {
  public boolean noCommonLetters(String s1, String s2){
    // TODO
  }

  public int maxProduct(String[] words) {
    int n = words.length;

    int maxProd = 0;
    for (int i = 0; i < n; ++i)
      for (int j = i + 1; j < n; ++j)
        if (noCommonLetters(words[i], words[j]))
          maxProd = Math.max(maxProd, words[i].length() * words[j].length());

    return maxProd;
  }
}
嵌套循环执行的次数为：

(N - 1) + (N - 2) + ... + 2 + 1 = \frac{N(N - 1)}{2}(N−1)+(N−2)+...+2+1= 
2
N(N−1)
​	
 

达到 \mathcal{O}(N^2 \times f(L_1, L_2))O(N 
2
 ×f(L 
1
​	
 ,L 
2
​	
 )) 的时间复杂度。其中 f(L_1, L_2)f(L 
1
​	
 ,L 
2
​	
 ) 是方法 noCommonLetters(String s1, String s2) 的时间复杂度，代表比较两个长度为 L_1L 
1
​	
  和 L_2L 
2
​	
  的字符串。

接下来怎么做？



方法一：最小化方法 f(L_1, L_2)f(L 
1
​	
 ,L 
2
​	
 ) 的复杂度。

方法二：最小化单词的比较次数。不需要 \mathcal{O}(N^2)O(N 
2
 ) 次比较，在所有具有相同字符集的单词中只保留最长的一个单词。例：（abab，aaaaabaabaaabbaaaaabaabaaabb，bbabbabbabbabbabba）中只保留最长的一个单词（aaaaabaabaaabbaaaaabaabaaabb）。

方法一：优化的方法 noCommonLetters：位操作+预计算
首先最小化单词比较 f(L_1, L_2)f(L 
1
​	
 ,L 
2
​	
 ) 的复杂度。



简单解法：\mathcal{O}(L_1 \times L_2)O(L 
1
​	
 ×L 
2
​	
 )

逐个检查第一个单词的每个字母是否出现在第二个单词中，这种方法不是最优的。


public boolean noCommonLetters(String s1, String s2){
  for (char ch : s1.toCharArray())
    if (s2.indexOf(ch) != -1) return false;
  return true;
}


位操作：\mathcal{O}(L_1 + L_2)O(L 
1
​	
 +L 
2
​	
 )

更好的方法是使用位操作。

单词仅包含小写字母，可以使用 26 个字母的位掩码对单词的每个字母处理，判断是否存在某个字母。如果单词中存在字母 a，则将位掩码的第一位设为 1，否则设为 0。如果单词中存在字母 b，则将位掩码的第二位设为 1，否则设为 0。依次类推，一直判断到字母 z。



如何设置掩码的第 n 位？使用标准的位操作：n_th_bit = 1 << n。

如何计算一个单词的位掩码？遍历单词的每个字母，计算该字母在掩码中的位置 n = (int)ch - (int)'a'，然后创建一个第 n 位为 1 的掩码 n_th_bit = 1 << n，通过或操作将该码合并到位掩码中 bitmask |= n_th_bit。



该方法对每个字母计算一次掩码，花费时间 \mathcal{O}(L_1 + L_2)O(L 
1
​	
 +L 
2
​	
 )。单词比较可以在恒定时间内完成。


public int bitNumber(char ch) {
  return (int)ch - (int)'a';
}

public boolean noCommonLetters(String s1, String s2) {
  int bitmask1 = 0, bitmask2 = 0;
  for (char ch : s1.toCharArray())
    bitmask1 |= 1 << bitNumber(ch);
  for (char ch : s2.toCharArray())
    bitmask2 |= 1 << bitNumber(ch);

  return (bitmask1 & bitmask2) == 0;
}
位掩码+预计算：使用 \mathcal{O}(1)O(1) 的时间比较

前面方法中，每个单词需要计算 N 次位掩码。实际上，每个单词的位掩码可以预先计算并存储起来，之后每次直接拿来比较。

因为 Java 的优化，操作数组比 HashMap 更快，所以使用两个整数数组存储位掩码和字符串长度。

算法

预计算所有单词的位掩码，并把它们存储在数组 masks 中。使用数组 lens 存储所有单词的长度。

逐一两两比较单词。如果两个单词不存在公共字母，则更新最大单词长度乘积 maxProd。使用数组 masks 可以在常数时间内判断两个单词是否包含公共字母：(masks[i] & masks[j]) == 0。

返回 maxProd。


class Solution {
  public int bitNumber(char ch) {
    return (int)ch - (int)'a';
  }

  public int maxProduct(String[] words) {
    int n = words.length;
    int[] masks = new int[n];
    int[] lens = new int[n];

    int bitmask = 0;
    for (int i = 0; i < n; ++i) {
      bitmask = 0;
      for (char ch : words[i].toCharArray()) {
        // add bit number bit_number in bitmask
        bitmask |= 1 << bitNumber(ch);
      }
      masks[i] = bitmask;
      lens[i] = words[i].length();
    }

    int maxVal = 0;
    for (int i = 0; i < n; ++i)
      for (int j = i + 1; j < n; ++j)
        if ((masks[i] & masks[j]) == 0)
          maxVal = Math.max(maxVal, lens[i] * lens[j]);

    return maxVal;
  }
}
复杂度分析

时间复杂度：\mathcal{O}(N^2 + L)O(N 
2
 +L)，其中 NN 是单词数量，LL 是所有单词的总长度。预计算处理所有单词的所有字母的复杂度为 \mathcal{O}(L)O(L)。单词两两比较的复杂度为 \mathcal{O}(N^2)O(N 
2
 )。总复杂度为 \mathcal{O}(N^2 + L)O(N 
2
 +L)。

空间复杂度：\mathcal{O}(N)O(N)，存储 NN 个元素的两个数组。

方法二：优化比较次数：位操作+预计算+HashMap
方法一优化了比较过程，方法二优化比较次数。完成所有的两两比较需要 \mathcal{O}(N^2)O(N 
2
 )。一些单词具有相同的字符集，则只保留这些单词中最长的一个单词即可。例如：单词集合（abab，aaaaabaabaaabbaaaaabaabaaabb，bbabbabbabbabbabba）只保留单词 aaaaabaabaaabbaaaaabaabaaabb 即可。使用 HashMap 代替方法一中的两个长度为 NN 的数组，存储结构为：位掩码 -> 该掩码对应的最大长度字符串。



这种方法单词比较次数可能会减少，从而提高 Python 解法的效率。由于 Java 中 HashMap 的性能问题，这种方法不会改善 Java 解法的效率。

算法

预计算所有单词的位掩码，并将它们存储在 HashMap 中：位掩码 -> 该掩码对应的最大长度字符串。例如：单词 “aaaaaaa” 和 “a” 具有相同的掩码。

逐一两两比较 HashMap 中的单词。如果两个单词没有公共字母，更新最大单词长度乘积 maxProd。使用位掩码可以在常数时间内判断两个单词是否包含公共字母：(x & y) == 0。

返回 maxProd。


class Solution {
  public int bitNumber(char ch){
    return (int)ch - (int)'a';
  }

  public int maxProduct(String[] words) {
    Map<Integer, Integer> hashmap = new HashMap();

    int bitmask = 0, bitNum = 0;
    for (String word : words) {
      bitmask = 0;
      for (char ch : word.toCharArray()) {
        // add bit number bitNumber in bitmask
        bitmask |= 1 << bitNumber(ch);
      }
      // there could be different words with the same bitmask
      // ex. ab and aabb
      hashmap.put(bitmask, Math.max(hashmap.getOrDefault(bitmask, 0), word.length()));
    }

    int maxProd = 0;
    for (int x : hashmap.keySet())
      for (int y : hashmap.keySet())
        if ((x & y) == 0) maxProd = Math.max(maxProd, hashmap.get(x) * hashmap.get(y));

    return maxProd;
  }
}
复杂度分析

时间复杂度：\mathcal{O}(N^2 + L)O(N 
2
 +L)，其中 NN 是单词数量，LL 是所有单词的所有字母的数量。当 N > 2^{26}N>2 
26
  时，时间复杂度为 \mathcal{O}(L)O(L)。

空间复杂度：\mathcal{O}(N)O(N)，使用一个长度为 NN 的 HashMap。



public class Solution {
    public  int MaxProduct(string[] words)
    {
        var arr = new int[26];
        var words_bt = new int[words.Length];
        for (var i = 0; i < words.Length; i++)
            words_bt[i] = _s2b(arr, words[i]);
        var max = 0;
        for (var i = 0; i < words.Length-1; i++) {
            for (var j = i + 1; j < words.Length; j++) {
                if ((words_bt[i] & words_bt[j]) == 0)
                    max = Math.Max(max,words[i].Length*words[j].Length);
            }
        }
        return max;
    }
    private  int _s2b(int[] arr, string s)
    {
        for (var i = 0; i < arr.Length; i++)
        {
            arr[i] = 0;
        }
        foreach (var v in s)
        {
            arr[v - 'a'] = 1;
        }
        var res = 0;
        for (var i = 0; i < arr.Length; i++)
        {
            res += (int)Math.Pow(2, i) * arr[i];
        }
        return res;
    }
} 
*/
