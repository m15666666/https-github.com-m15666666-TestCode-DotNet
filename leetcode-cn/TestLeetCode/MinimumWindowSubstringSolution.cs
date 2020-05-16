using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给你一个字符串 S、一个字符串 T，请在字符串 S 里面找出：包含 T 所有字符的最小子串。

示例：

输入: S = "ADOBECODEBANC", T = "ABC"
输出: "BANC"
说明：

如果 S 中不存这样的子串，则返回空字符串 ""。
如果 S 中存在这样的子串，我们保证它是唯一的答案。
     
*/
/// <summary>
/// https://leetcode-cn.com/problems/minimum-window-substring/
/// 76. 最小覆盖子串
/// 
/// </summary>
class MinimumWindowSubstringSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public string MinWindow(string s, string t) {

        if (string.IsNullOrEmpty(s) || string.IsNullOrEmpty(t)) return ""; 

        var dictT = new Dictionary<char,int>();
        foreach (var c in t) 
            if (dictT.ContainsKey(c)) dictT[c]++;
            else dictT.Add(c, 1);

        int required = dictT.Count;
        List<(int, char)> filteredS = new List<(int,char)>();
        for (int i = 0; i < s.Length; i++) 
        {
            char c = s[i];
            if (dictT.ContainsKey(c)) filteredS.Add((i, c));
        }

        int left = 0, right = 0, matchedChars = 0;
        var windowCounts = new Dictionary<char,int>();  
        int[] ans = {-1, 0, 0};
        (int,char) rightPair; 
        (int,char) leftPair; 
        while (right < filteredS.Count) 
        {
            rightPair = filteredS[right];
            char rightChar = rightPair.Item2;
            int count = windowCounts.ContainsKey(rightChar) ? windowCounts[rightChar] : 0;
            windowCounts[rightChar] = ++count;

            if (count == dictT[rightChar]) matchedChars++; 

            while (left <= right && matchedChars == required) 
            {
                leftPair = filteredS[left];
                int end = rightPair.Item1;
                int start = leftPair.Item1;
                if (ans[0] == -1 || end - start + 1 < ans[0]) 
                {
                    ans[0] = end - start + 1;
                    ans[1] = start;
                    ans[2] = end;
                }

                var leftChar = leftPair.Item2;
                windowCounts[leftChar]--;
                if (windowCounts[leftChar] == dictT[leftChar] - 1) matchedChars--; 
                left++;
            }
            right++;
        }
        return ans[0] == -1 ? "" : s.Substring(ans[1], ans[0]);
    }
}
/*

最小覆盖子串
力扣 (LeetCode)
发布于 1 年前
41.4k
方法一：滑动窗口
思路

本问题要求我们返回字符串 SS 中包含字符串 TT 的全部字符的最小窗口。我们称包含 TT 的全部字母的窗口为 可行 窗口。

可以用简单的滑动窗口法来解决本问题。

在滑动窗口类型的问题中都会有两个指针。一个用于延伸现有窗口的 rightright 指针，和一个用于收缩窗口的 leftleft 指针。在任意时刻，只有一个指针运动，而另一个保持静止。

本题的解法很符合直觉。我们通过移动 right 指针不断扩张窗口。当窗口包含全部所需的字符后，如果能收缩，我们就收缩窗口直到得到最小窗口。

答案就是最小的可行窗口。

举个例子，S = "ABAACBAB"，T = "ABC" 。则问题答案是 "ACB" ，下图是可行窗口中的一个。

image.png

算法

初始，leftleft 指针和 rightright 指针都指向 SS 的第一个元素.

将 rightright 指针右移，扩张窗口，直到得到一个可行窗口，亦即包含 TT 的全部字母的窗口。

得到可行的窗口后，将 lefttleftt 指针逐个右移，若得到的窗口依然可行，则更新最小窗口大小。

若窗口不再可行，则跳转至 22 。

image.png

重复以上步骤，直到遍历完全部窗口。返回最小的窗口。

image.png

class Solution {
  public String minWindow(String s, String t) {

      if (s.length() == 0 || t.length() == 0) {
          return "";
      }

      // Dictionary which keeps a count of all the unique characters in t.
      Map<Character, Integer> dictT = new HashMap<Character, Integer>();
      for (int i = 0; i < t.length(); i++) {
          int count = dictT.getOrDefault(t.charAt(i), 0);
          dictT.put(t.charAt(i), count + 1);
      }

      // Number of unique characters in t, which need to be present in the desired window.
      int required = dictT.size();

      // Left and Right pointer
      int l = 0, r = 0;

      // formed is used to keep track of how many unique characters in t
      // are present in the current window in its desired frequency.
      // e.g. if t is "AABC" then the window must have two A's, one B and one C.
      // Thus formed would be = 3 when all these conditions are met.
      int formed = 0;

      // Dictionary which keeps a count of all the unique characters in the current window.
      Map<Character, Integer> windowCounts = new HashMap<Character, Integer>();

      // ans list of the form (window length, left, right)
      int[] ans = {-1, 0, 0};

      while (r < s.length()) {
          // Add one character from the right to the window
          char c = s.charAt(r);
          int count = windowCounts.getOrDefault(c, 0);
          windowCounts.put(c, count + 1);

          // If the frequency of the current character added equals to the
          // desired count in t then increment the formed count by 1.
          if (dictT.containsKey(c) && windowCounts.get(c).intValue() == dictT.get(c).intValue()) {
              formed++;
          }

          // Try and contract the window till the point where it ceases to be 'desirable'.
          while (l <= r && formed == required) {
              c = s.charAt(l);
              // Save the smallest window until now.
              if (ans[0] == -1 || r - l + 1 < ans[0]) {
                  ans[0] = r - l + 1;
                  ans[1] = l;
                  ans[2] = r;
              }

              // The character at the position pointed by the
              // `Left` pointer is no longer a part of the window.
              windowCounts.put(c, windowCounts.get(c) - 1);
              if (dictT.containsKey(c) && windowCounts.get(c).intValue() < dictT.get(c).intValue()) {
                  formed--;
              }

              // Move the left pointer ahead, this would help to look for a new window.
              l++;
          }

          // Keep expanding the window once we are done contracting.
          r++;   
      }

      return ans[0] == -1 ? "" : s.substring(ans[1], ans[2] + 1);
  }
}
复杂度分析

时间复杂度: O(|S| + |T|)O(∣S∣+∣T∣)，其中 |S|∣S∣ 和 |T|∣T∣ 代表字符串 SS 和 TT 的长度。在最坏的情况下，可能会对 SS 中的每个元素遍历两遍，左指针和右指针各一遍。

空间复杂度: O(|S| + |T|)O(∣S∣+∣T∣)。当窗口大小等于 |S|∣S∣ 时为 SS 。当 |T|∣T∣ 包括全部唯一字符时为 TT 。

方法二：优化滑动窗口
思路

对上一方法进行改进，可以将时间复杂度下降到 O(2*|filtered\_S| + |S| + |T|)O(2∗∣filtered_S∣+∣S∣+∣T∣) ，其中 filtered\_Sfiltered_S 是从 SS 中去除所有在 TT 中不存在的元素后，得到的字符串。

当 |filtered\_S| <<< |S|∣filtered_S∣<<<∣S∣ 时，优化效果显著。这种情况可能是由于 TT 的长度远远小于 SS ，因此 SS 中包括大量 TT 中不存在的字符。

算法

我们建立一个 filtered\_Sfiltered_S 列表，其中包括 SS 中的全部字符以及它们在 SS 的下标，但这些字符必须在 TT 中出现。

S = "ABCDDDDDDEEAFFBC" T = "ABC"

filtered_S = [(0, 'A'), (1, 'B'), (2, 'C'), (11, 'A'), (14, 'B'), (15, 'C')]

此处的 (0, 'A') 表示字符 'A' 在字符串 S 中的下标为 0 。
现在我们可以在更短的字符串 filtered\_Sfiltered_S 中使用滑动窗口法。

class Solution {
    public String minWindow(String s, String t) {

        if (s.length() == 0 || t.length() == 0) {
            return "";
        }

        Map<Character, Integer> dictT = new HashMap<Character, Integer>();

        for (int i = 0; i < t.length(); i++) {
            int count = dictT.getOrDefault(t.charAt(i), 0);
            dictT.put(t.charAt(i), count + 1);
        }

        int required = dictT.size();

        // Filter all the characters from s into a new list along with their index.
        // The filtering criteria is that the character should be present in t.
        List<Pair<Integer, Character>> filteredS = new ArrayList<Pair<Integer, Character>>();
        for (int i = 0; i < s.length(); i++) {
            char c = s.charAt(i);
            if (dictT.containsKey(c)) {
                filteredS.add(new Pair<Integer, Character>(i, c));
            }
        }

        int l = 0, r = 0, formed = 0;
        Map<Character, Integer> windowCounts = new HashMap<Character, Integer>();  
        int[] ans = {-1, 0, 0};

        // Look for the characters only in the filtered list instead of entire s.
        // This helps to reduce our search.
        // Hence, we follow the sliding window approach on as small list.
        while (r < filteredS.size()) {
            char c = filteredS.get(r).getValue();
            int count = windowCounts.getOrDefault(c, 0);
            windowCounts.put(c, count + 1);

            if (dictT.containsKey(c) && windowCounts.get(c).intValue() == dictT.get(c).intValue()) {
                formed++;
            }

            // Try and contract the window till the point where it ceases to be 'desirable'.
            while (l <= r && formed == required) {
                c = filteredS.get(l).getValue();

                // Save the smallest window until now.
                int end = filteredS.get(r).getKey();
                int start = filteredS.get(l).getKey();
                if (ans[0] == -1 || end - start + 1 < ans[0]) {
                    ans[0] = end - start + 1;
                    ans[1] = start;
                    ans[2] = end;
                }

                windowCounts.put(c, windowCounts.get(c) - 1);
                if (dictT.containsKey(c) && windowCounts.get(c).intValue() < dictT.get(c).intValue()) {
                    formed--;
                }
                l++;
            }
            r++;
        }
        return ans[0] == -1 ? "" : s.substring(ans[1], ans[2] + 1);
    }
}
复杂度分析

时间复杂度 : O(|S| + |T|)O(∣S∣+∣T∣)， 其中 |S|∣S∣ 和 |T|∣T∣ 分别代表字符串 SS 和 TT 的长度。 本方法时间复杂度与方法一相同，但当 |filtered\_S|∣filtered_S∣ <<< |S|∣S∣ 时，复杂度会下降，因为此时迭代次数是 2*|filtered\_S| + |S| + |T|2∗∣filtered_S∣+∣S∣+∣T∣。
空间复杂度 : O(|S| + |T|)O(∣S∣+∣T∣)。

public class Solution {
    public string MinWindow(string s, string t) {
        int[] index = new int[128];
        int counter = t.Length;
        int head = 0, min = int.MaxValue;
        int left = 0, right = 0;
        foreach (var c in t)
        {
            index[c]++;
        }
        while(right < s.Length)
        {
            if (index[s[right]] > 0) counter--;

            index[s[right]]--;
            right++;

            while (counter == 0)
            {
                if (right - left < min)
                {
                    min = right - left;
                    head = left;
                }
                if (index[s[left]] == 0) counter++;
                index[s[left]]++;
                left++;
            }
        }

        return min == int.MaxValue ? "" : s.Substring(head, min);
    }
}

 
     
     
     
*/