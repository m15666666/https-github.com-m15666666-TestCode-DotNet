using System;
using System.Collections.Generic;
using System.Text;

/*
给你一个字符串 s ，请你去除字符串中重复的字母，使得每个字母只出现一次。需保证 返回结果的字典序最小（要求不能打乱其他字符的相对位置）。

注意：该题与 1081 https://leetcode-cn.com/problems/smallest-subsequence-of-distinct-characters 相同

 

示例 1：

输入：s = "bcabc"
输出："abc"
示例 2：

输入：s = "cbacdcbc"
输出："acdb"

*/

/// <summary>
/// https://leetcode-cn.com/problems/remove-duplicate-letters/
/// 316. 去除重复字母
///
/// </summary>
internal class RemoveDuplicateLettersSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public string RemoveDuplicateLetters(string s)
    {
        const char a = 'a';
        int[] cnt = new int[26];
        int n = s.Length;
        for (int i = 0; i < n; i++) cnt[s[i] - a] = i;

        HashSet<char> seen = new HashSet<char>();
        Stack<char> stack = new Stack<char>();
        for (int i = 0; i < n; i++)
        {
            char c = s[i];
            if (seen.Contains(c)) continue;

            while (0 < stack.Count && c < stack.Peek() && i < cnt[stack.Peek() - a]) seen.Remove(stack.Pop());

            seen.Add(c);
            stack.Push(c);
        }
        var bytes = stack.ToArray();
        Array.Reverse(bytes);
        return new string(bytes);
        //var builder = new StringBuilder(stack.Count);
        //foreach (var c in stack) builder.Insert(0, c);
        //return builder.ToString();
    }
}

/*
去除重复字母
力扣 (LeetCode)
发布于 2020-03-26
29.1k
思路
首先要知道什么叫 “字典序”。字符串之间比较跟数字之间比较是不太一样的。字符串比较是从头往后一个字符一个字符比较的。哪个字符串大取决于两个字符串中 第一个对应不相等的字符 。根据这个规则，任意一个以 a 开头的字符串都大于任意一个以 b 开头的字符串。

综上所述，解题过程中我们将 最小的字符尽可能的放在前面 。下面将给出两种 O(N)O(N) 复杂度的解法：

方法一：题目要求最终返回的字符串必须包含所有出现过的字母，同时得让字符串的字典序最小。因此，对于最终返回的字符串，最左侧的字符是在能保证其他字符至少能出现一次情况下的最小字符。

方法二：在遍历字符串的过程中，如果字符 i 大于字符i+1，在字符 i 不是最后一次出现的情况下，删除字符 i。

方法一：贪心 - 一个字符一个字符处理
算法

每次递归中，在保证其他字符至少出现一次的情况下，确定最小左侧字符。之后再将未处理的后缀字符串继续递归。

实现


public class Solution {
    public String removeDuplicateLetters(String s) {
        // find pos - the index of the leftmost letter in our solution
        // we create a counter and end the iteration once the suffix doesn't have each unique character
        // pos will be the index of the smallest character we encounter before the iteration ends
        int[] cnt = new int[26];
        int pos = 0;
        for (int i = 0; i < s.length(); i++) cnt[s.charAt(i) - 'a']++;
        for (int i = 0; i < s.length(); i++) {
            if (s.charAt(i) < s.charAt(pos)) pos = i;
            if (--cnt[s.charAt(i) - 'a'] == 0) break;
        }
        // our answer is the leftmost letter plus the recursive call on the remainder of the string
        // note that we have to get rid of further occurrences of s[pos] to ensure that there are no duplicates
        return s.length() == 0 ? "" : s.charAt(pos) + removeDuplicateLetters(s.substring(pos + 1).replaceAll("" + s.charAt(pos), ""));
    }
}
复杂度分析

*时间复杂度：O(N)O(N)。 每次递归调用占用 O(N)O(N) 时间。递归调用的次数受常数限制(只有26个字母)，最终复杂度为 O(N) * C = O(N)O(N)∗C=O(N)。

*空间复杂度：O(N)O(N)，每次给字符串切片都会创建一个新的字符串（字符串不可变），切片的数量受常数限制，最终复杂度为 O(N) * C = O(N)O(N)∗C=O(N)。

方法二：贪心 - 用栈
算法

用栈来存储最终返回的字符串，并维持字符串的最小字典序。每遇到一个字符，如果这个字符不存在于栈中，就需要将该字符压入栈中。但在压入之前，需要先将之后还会出现，并且字典序比当前字符小的栈顶字符移除，然后再将当前字符压入。

详细过程如下动画所示：



实现


class Solution {
    public String removeDuplicateLetters(String s) {

        Stack<Character> stack = new Stack<>();

        // this lets us keep track of what's in our solution in O(1) time
        HashSet<Character> seen = new HashSet<>();

        // this will let us know if there are any more instances of s[i] left in s
        HashMap<Character, Integer> last_occurrence = new HashMap<>();
        for(int i = 0; i < s.length(); i++) last_occurrence.put(s.charAt(i), i);

        for(int i = 0; i < s.length(); i++){
            char c = s.charAt(i);
            // we can only try to add c if it's not already in our solution
            // this is to maintain only one of each character
            if (!seen.contains(c)){
                // if the last letter in our solution:
                //     1. exists
                //     2. is greater than c so removing it will make the string smaller
                //     3. it's not the last occurrence
                // we remove it from the solution to keep the solution optimal
                while(!stack.isEmpty() && c < stack.peek() && last_occurrence.get(stack.peek()) > i){
                    seen.remove(stack.pop());
                }
                seen.add(c);
                stack.push(c);
            }
        }
    StringBuilder sb = new StringBuilder(stack.size());
    for (Character c : stack) sb.append(c.charValue());
    return sb.toString();
    }
}
复杂度分析

时间复杂度：O(N)O(N)。虽然外循环里面还有一个内循环，但内循环的次数受栈中剩余字符总数的限制，因此最终复杂度仍为 O(N)O(N)。

空间复杂度：O(1)O(1)。看上去空间复杂度像是 O(N)O(N)，但这不对！首先， seen 中字符不重复，其大小受字母表大小的限制。其次，只有 stack 中不存在的元素才会被压入，因此 stack 中的元素也唯一。所以最终空间复杂度为 O(1)O(1)。



public class Solution {
    public string RemoveDuplicateLetters(string s) {
		var arr = new int[26];
		for (int i = 0; i < s.Length; i++)
		{
			arr[s[i] - 'a'] = i;
		}

		var stack = new Stack<char>();
		var visited = new bool[26];
		for (int i = 0; i < s.Length; i++)
		{
			char item = s[i];
			if (visited[item - 'a'])
				continue;

			while (stack.Any() && item < stack.Peek() && arr[stack.Peek() - 'a'] > i)
			{
				var top = stack.Pop();
				visited[top - 'a'] = false;
			}

			visited[item - 'a'] = true;
			stack.Push(item);
		}

		var sb = new StringBuilder();
		while (stack.Any())
		{
			sb.Insert(0, stack.Pop());
		}

		return sb.ToString();
    }
}

public class Solution {
    public string RemoveDuplicateLetters(string s) {
        int[] cnt = new int[26];
        int[] visited = new int[26];
        foreach (var c in s) {
            cnt[c - 'a']++;
        }
        StringBuilder sb = new StringBuilder(26);
        foreach (char c in s) {
            cnt[c - 'a']--;
            if (visited[c - 'a'] == 0) {
                while (sb.Length > 0 && c < sb[sb.Length - 1] && cnt[sb[sb.Length - 1] - 'a'] > 0) {
                    visited[sb[sb.Length - 1] - 'a'] = 0;
                    sb.Remove(sb.Length - 1, 1);
                }
                visited[c - 'a'] = 1;
                sb.Append(c);
            }
        }
        return sb.ToString();
    }
}

public class Solution {
    public string RemoveDuplicateLetters(string s) {
            if (s == string.Empty) return s;
            Stack<char> stack = new Stack<char>();
            for (int i = 0; i < s.Length; i++)
            {
                char ch = s[i];
                if (stack.Contains(ch)) continue;
                while (stack.Count > 0 && stack.Peek() > ch && s.Substring(i).Contains(stack.Peek()))
                {
                    stack.Pop();
                }
                stack.Push(ch);
            }
           
            string letters = string.Empty;
            foreach (char ch in stack)
            {
                letters = string.Format("{0}{1}", ch, letters);
            }
            return letters;
    }
}

*/